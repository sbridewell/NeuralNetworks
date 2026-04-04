# Draft plan — multi-layer neural network interface and implementation

<!--TOC-->
- [Draft plan — multi-layer neural network interface and implementation](#draft-plan--multi-layer-neural-network-interface-and-implementation)
  - [High-level design](#high-level-design)
  - [Proposed IMultiLayerNetwork API sketch](#proposed-imultilayernetwork-api-sketch)
  - [Events + Progress model (UI friendliness)](#events--progress-model-ui-friendliness)
  - [Implementation notes](#implementation-notes)
  - [Threading \& responsiveness](#threading--responsiveness)
  - [Serialization and persistence](#serialization-and-persistence)
  - [UI integration guidance (WinForms)](#ui-integration-guidance-winforms)
  - [Interactive (WinForms) vs unattended (console app) operation](#interactive-winforms-vs-unattended-console-app-operation)
  - [Migration strategy](#migration-strategy)
  - [Testing plan](#testing-plan)
  - [Example implementation tasks (ordered)](#example-implementation-tasks-ordered)
  - [Additional considerations](#additional-considerations)
<!--/TOC-->

Goal
- Provide a clean, UI-friendly, WinForms-usable interface for multi-layer feedforward networks that composes INeuralNetworkLayer instances.
- No WinForms dependencies in the core library. The API must expose simple properties and events so a WinForms control can use it in the same way as the existing `INeuralNetwork`.
- Provide a migration/adapter path so existing WinForms UI code that expects `INeuralNetwork` can work with multi-layer networks.

Non-goals
- GPU acceleration or distributed training.
- Changing `INeuralNetworkLayer` behaviour — use it as-is.

## High-level design

1.	New interface: `IMultiLayerNetwork` in `Sde.NeuralNetworks.Networks` that:
    - Composes an ordered `IReadOnlyList<INeuralNetworkLayer> Layers { get; }`.
    - Exposes simple scalar properties used by UI: `NumberOfInputs`, `NumberOfOutputs`, `LearningRate`, `Momentum`, `NumberOfIterations`, `CurrentIteration`, `NumberOfTrainingRecords`, `TimeSpentTraining`, `EstimatedTrainingTimeLeft`, `HiddenLayerMeanSquaredError`, `OutputLayerMeanSquaredError`.
    - Has `Predict(Vector)` and `Predict(double[])` convenience overloads.
    - Has `Train(...)` that accepts samples and supports `CancellationToken` and progress reporting.
    - Exposes events for training progress so UI can subscribe without polling.
2.	Implementation: `MultiLayerNetwork : IMultiLayerNetwork`
    - Orchestrates forward passes by calling `layer.FeedForward`.
    - Backpropagation implemented by calling `layer.CalculateGradients` on the output layer then propagating error backwards (synthesising per-layer expected outputs as described earlier).
    - Uses each layer's own `LearningRate` and `Momentum` (or mirrors a global default if preferred).
3.	Adapter for backwards compatibility: `NeuralNetworkAdapter : INeuralNetwork`
    - Wraps an `IMultiLayerNetwork` and implements `INeuralNetwork` members where possible (mapping `NumberOfInputs`, `NumberOfOutputs`, `Predict`, `Train`, iteration state).
    - For single-hidden-layer scenarios map layers to `InputToHiddenWeights`, `HiddenBiases`, `HiddenToOutputWeights`, `OutputBiases`. For networks that do not match the two-layer shape, throw `NotSupportedException` for those specific properties.

## Proposed IMultiLayerNetwork API sketch

(illustrative — include in plan, not code commit)

```csharp
public interface IMultiLayerNetwork
{
    IReadOnlyList<INeuralNetworkLayer> Layers { get; }

    int NumberOfInputs { get; }
    int NumberOfOutputs { get; }

    double LearningRate { get; set; }
    double Momentum { get; set; }

    int NumberOfIterations { get; set; }
    int CurrentIteration { get; }
    int NumberOfTrainingRecords { get; }

    TimeSpan TimeSpentTraining { get; set; }
    TimeSpan EstimatedTrainingTimeLeft { get; set; }

    double HiddenLayerMeanSquaredError { get; }
    double OutputLayerMeanSquaredError { get; }

    Vector Predict(Vector input);
    double[] Predict(double[] input);

    Task TrainAsync(IEnumerable<(Vector inputs, Vector expected)> samples, int numberOfiterations = 1, CancellationToken cancellationToken = default);

    event EventHandler<TrainingProgressEventArgs> TrainingProgressChanged;
    event EventHandler TrainingStarted;
    event EventHandler TrainingCompleted;
    event EventHandler TrainingStopped;
}
```

`TrainingProgressEventArgs` should carry `CurrentIteration`, `CurrentSampleIndex`, `TotalSamples`, `PercentComplete`, optional `LastLoss`.

## Events + Progress model (UI friendliness)

- - [ ] `TrainingStarted` / `TrainingStopped` / `TrainingCompleted` (simple notifications).
- - [ ] `TrainingProgressChanged(TrainingProgressEventArgs)` fired periodically (e.g., every N samples or every iteration) with:
    - `int CurrentIteration`, `int TotalIterations`
    - `int CurrentSample`, `int TotalSamples`
    - `double PercentComplete`
    - `double? LastOutputLayerMSE`, `double? LastHiddenLayerMSE`
- - [ ] Use `SynchronizationContext` in UI layer to marshal updates; core library must not reference WinForms.

## Implementation notes

- Forward pass: sequential `FeedForward` through layers; store per-layer inputs/outputs for backprop.
- Backpropagation:
    - Call `CalculateGradients` on output layer with inputs supplied to that layer and the expected final output. That call should mutate the layer (weights/biases) and return `inputError`.
    - For preceding layers, synthesize `expected_for_layer = outputs_of_layer + inputError` and call `CalculateGradients(layerInput, expected_for_layer)`.
    - Document reasoning: `ILayerMaths` computes `delta = f'(z) * (expected - output)`. Setting `expected = output + propagatedError` makes `(expected - output) = propagatedError`.
- Use `CancellationToken` inside training loop; honor it promptly.
- Keep method complexity low (<= 10) — factor helper methods like `ForwardPass(...)` and `Backpropagate(...)`.

## Threading & responsiveness

- `TrainAsync` should run on a background thread (`Task.Run`) and provide cancellation token usage.
- Fire progress events from background thread; expect UI to invoke marshaling. Do not access UI components directly.

## Serialization and persistence

- Provide plain DTO(s) for network state: list of per-layer `Matrix` weights and `Vector` biases plus layer metadata (activation provider name if serialisable).
- Provide `ExportState()` / `ImportState()` methods that return/consume DTOs to avoid coupling with `System.Text.Json` specifics and to keep tests simple.

Example persistence abstraction:
```csharp
// NeuralNetworks/Networks/NetworkState.cs
namespace Sde.NeuralNetworks.Networks
{
    using System.Collections.Generic;
    using Sde.NeuralNetworks.LinearAlgebra;

    /// <summary>
    /// DTO representing the persisted state of a multi-layer network, including per-layer parameters.
    /// </summary>
    public sealed record NetworkState(
        IReadOnlyList<LayerState> Layers,
        double LearningRate,
        double Momentum,
        int NumberOfIterations);

    /// <summary>
    /// DTO representing a single layer's serialisable state.
    /// </summary>
    public sealed record LayerState(string LayerType, Matrix Weights, Vector Biases);
}

// NeuralNetworks/Networks/INetworkPersistence.cs
namespace Sde.NeuralNetworks.Networks
{
    /// <summary>
    /// Abstraction for exporting and importing network state to and from <see cref="NetworkState"/> DTOs.
    /// </summary>
    public interface INetworkPersistence
    {
        /// <summary>
        /// Exports the state of the supplied network as a <see cref="NetworkState"/> DTO.
        /// </summary>
        NetworkState ExportState(IMultiLayerNetwork network);

        /// <summary>
        /// Imports the supplied <see cref="NetworkState"/> into the supplied network, restoring parameters.
        /// </summary>
        void ImportState(IMultiLayerNetwork network, NetworkState state);
    }
}
```

## UI integration guidance (WinForms)

- UI layer (WinForms) should:
    - Build layers via a factory (`LayerFactory.CreateFullyConnectedLayer(inputCount, outputCount, learningRate, momentum, activationFunctionProvider)`).
    - Compose `IMultiLayerNetwork` by passing the layers list.
    - Subscribe to `TrainingProgressChanged` to update progress bars, labels and charts. Use `Invoke/BeginInvoke` or `SynchronizationContext.Post`.
    - For inspectors (weights/bias editor), expose the `Layers` collection; the control can read `layer.Weights` / `layer.Biases` and present them.
- To remain compatible with existing code that expects `INeuralNetwork`:
    - Provide `NeuralNetworkAdapter` implementing `INeuralNetwork` that internally wraps an `IMultiLayerNetwork`. The WinForms code can call `adapter.Predict()` / `adapter.Train()` and continue to work.

## Interactive (WinForms) vs unattended (console app) operation
- Allow interactive use to show the user the state of the network during the training process, whilst unattended use does not sacrifice speed for observability concerns.

Suggested code for observability options:

```csharp
// NeuralNetworks/Networks/ObservabilityMode.cs
namespace Sde.NeuralNetworks.Networks
{
    /// <summary>
    /// Controls runtime observability/telemetry behaviour during training.
    /// </summary>
    public enum ObservabilityMode
    {
        /// <summary>Disable all non-essential telemetry and minimise allocations.</summary>
        None,

        /// <summary>Report coarse progress only (iteration, percent) at configured intervals.</summary>
        Minimal,

        /// <summary>Collect layer snapshots and diagnostic state for UI inspection.</summary>
        Full
    }
}

// NeuralNetworks/Networks/TrainingOptions.cs
namespace Sde.NeuralNetworks.Networks
{
    /// <summary>
    /// Options controlling training behaviour and observability.
    /// </summary>
    public sealed record TrainingOptions(
        ObservabilityMode Observability = ObservabilityMode.None,
        int ReportEveryNSamples = 100,
        int ReportEveryMilliseconds = 0);
}

// NeuralNetworks/Networks/ITrainingTelemetry.cs
namespace Sde.NeuralNetworks.Networks
{
    /// <summary>
    /// Optional provider for training telemetry. Implementations should be non-blocking and lightweight.
    /// </summary>
    public interface ITrainingTelemetry
    {
        void OnTrainingStarted();
        void OnTrainingProgress(in TrainingProgressEventArgs args);
        void OnLayerSnapshot(int layerIndex, in LayerState snapshot);
        void OnTrainingCompleted();
    }
}
```

Implementation guidance:

- Hot path discipline: check telemetry presence with a single boolean field (e.g., `_telemetryEnabled`) and return early. Avoid creating arrays/objects when `_telemetryEnabled` is false.
- Event frequency: default to coarse updates (every N samples or once per iteration). UI can request finer granularity if it accepts the cost.
- Background delivery: telemetry implementations should enqueue snapshots and publish on a background task so the trainer thread is not blocked. Provide a built-in BackgroundTelemetry helper.
- Profiling: add microbenchmarks to measure overhead of Minimal and Full modes and validate that `ObservabilityMode.None` overhead is negligible.

## Migration strategy

1.	- [ ] Add `IMultiLayerNetwork` and `MultiLayerNetwork` implementation in Networks namespace. No breaking changes.
2.	- [ ] Add `NeuralNetworkAdapter : INeuralNetwork` that delegates to `MultiLayerNetwork` for common operations. Use adapter for the WinForms UI initially.
3.	- [ ] Gradually update UI controls to use `IMultiLayerNetwork` directly (preferred) so they can handle arbitrary depth and multiple hidden layers.
4.	- [ ] Add unit tests for both `MultiLayerNetwork` and adapter.

## Testing plan

- Unit tests:
    - [ ] Forward-pass correctness using small deterministic layers (reuse `NeuralNetworkLayer` deterministic constructor).
    - [ ] Backpropagation integration test: single-layer/2-layer hand-worked example where updated weights can be checked.
    - [ ] Cancellation: verify `TrainAsync` respects `CancellationToken`.
    - [ ] Event firing: verify `TrainingProgressChanged` is fired and delivers expected fields.
    - [ ] Serialization round-trip tests for export/import state.
- Integration tests:
    - [ ] Use a tiny dataset and a deterministic random seed to ensure repeatable results.

## Example implementation tasks (ordered)

1.	- [ ] Create `IMultiLayerNetwork` interface and `TrainingProgressEventArgs`.
2.	- [ ] Implement `MultiLayerNetwork` class (forward, backpropagate, events, async training, cancellation).
3.	- [ ] Add `NeuralNetworkAdapter : INeuralNetwork` to wrap multi-layer into legacy UI contract.
4.	- [ ] Add factory helpers to build `NeuralNetworkLayer` instances for common layer types and sizes.
5.	- [ ] Write unit tests for forward/backprop, events, cancellation and serialization.
6.	- [ ] Update WinForms app to use adapter or `IMultiLayer` interface (non-breaking incremental approach).

## Additional considerations

- Validation: when constructing network or adding layers, validate dimension compatibility (weights' column counts / row counts / biases length). Throw well-documented exceptions.
- API ergonomics: prefer `Vector`/`Matrix` for internal operations and retain `double[]` overloads for UI convenience.
- Keep public XML docs, use British spelling for documentation and Initializes in constructor docs per repo standard.