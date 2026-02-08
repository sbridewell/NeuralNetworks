| Class | SingleNeuronNetwork | MsdnNeuralNetwork | QuadraticSolverNetwork |
|-|-|-|-|
| Hidden layers | 0 | 1 | 1 |
| Neurons per hidden layer | N/A | Configurable | Configurable |
| Activation functions | Injected | Hyperbolic tangent (hidden layer), SoftMax (output layer) | Injected (hidden layer only) |
| Gradient (derivative) functions | Injected | None | Injected |
| Input type | int[] (1 or 0) | double[] | double, double, double - change to double[]? |
| Output type | float | double[] | Tuple (Complex, Complex) - change to double[]? |
| Back propagation | No | Yes | Yes |
| Learning rate configurable? | No | Yes | Yes |
| Biases | Yes | Yes | Yes |
| Momentum | No | Yes | |
| Training method | Train | Train | Train |
| Prediction method | Think | ComputeOutputs | Predict |
| Error check method | None | Error | None? |
| Accuracy method | None | Accuracy | None? |
