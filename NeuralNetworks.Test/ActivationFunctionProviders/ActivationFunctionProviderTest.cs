// <copyright file="ActivationFunctionProviderTest.cs" company="Simon Bridewell">
// Copyright (c) Simon Bridewell. All rights reserved.
// </copyright>

namespace Sde.NeuralNetworks.Test.ActivationFunctionProviders
{
    using System.Text;
    using FluentAssertions;
    using Sde.NeuralNetworks.ActivationFunctionProviders;

    /// <summary>
    /// Unit tests for <see cref="IActivationFunctionProvider"/> implementations.
    /// </summary>
    /// <typeparam name="TActivationFunctionProvider">
    /// The type of activation function provider being tested.
    /// </typeparam>
    public abstract class ActivationFunctionProviderTest<TActivationFunctionProvider>(ITestOutputHelper output)
        where TActivationFunctionProvider : IActivationFunctionProvider, new()
    {
        /// <summary>
        /// Gets some sample inputs to test the provider with.
        /// </summary>
        /// <remarks>
        /// We intentionally exclude zero from the sample inputs because some activation functions have a
        /// non-differentiable point at zero, and we want to avoid testing the provider's gradient
        /// calculation at such points. We also include a range of positive and negative values to test
        /// the provider's behavior across different input ranges.
        /// </remarks>
        public static TheoryData<double> SampleIputs =>
            [
                -10.0, -5.0, -1.0, -0.1, -0.01, -0.001, -0.0001, -0.00001, 0.00001, 0.0001, 0.001, 0.01, 0.1, 1.0, 5.0, 10.0,
            ];

        /// <summary>
        /// Tests that the gradient calculated by the provider is correct
        /// by comparing it to a numerical approximation of the gradient
        /// using the central difference formula.
        /// </summary>
        /// <param name="x">The input to the CalculateGradient function.</param>
        [Theory]
        [MemberData(nameof(SampleIputs))]
        public void CalculateGradient_ReturnsCorrectValue(double x)
        {
            // Arrange
            const double deltaX = 1e-6;
            const double absoluteTolerance = 1e-6;
            const double relativeTolerance = 1e-3; // 0.1%
            var provider = new TActivationFunctionProvider();
            output.WriteLine($"Activation function provider display name: '{provider.DisplayName}'");
            output.WriteLine($"Activation function provider type name: '{provider.GetType().Name}'");
            var expectedGradient = CalculateExpectedGradient(provider, x, deltaX);

            // Act
            var actualGradient = provider.CalculateGradient(x);

            // Assert
            var error = Math.Abs(actualGradient - expectedGradient);
            var allowed = Math.Max(absoluteTolerance, relativeTolerance * Math.Max(1.0, Math.Abs(expectedGradient)));
            var sb = new StringBuilder();
            sb.AppendLine();
            sb.AppendLine($"Expected gradient for input {x} is {expectedGradient:G7}, actual gradient is {actualGradient:G7}.");
            sb.AppendLine($"Difference between expected and actual gradient is {error:G7}, expected a difference of less than {allowed:G7}.");
            output.WriteLine(sb.ToString());
            error.Should().BeLessThanOrEqualTo(
                allowed,
                " (see standard output for more detail)");
        }

        private static double CalculateExpectedGradient(TActivationFunctionProvider provider, double x, double deltaX)
        {
            var fPlus = provider.CalculateActivation(x + deltaX);
            var fMinus = provider.CalculateActivation(x - deltaX);
            var expectedGradient = (fPlus - fMinus) / (2.0 * deltaX);
            if (double.IsNaN(expectedGradient))
            {
                Assert.Skip($"Test setup error - expected gradient produced NaN at x={x}.");
            }

            if (double.IsInfinity(expectedGradient))
            {
                Assert.Skip($"Test setup error - expected gradient produced {expectedGradient} at x={x}.");
            }

            return expectedGradient;
        }
    }

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
#pragma warning disable SA1516 // Elements should be separated by blank line
#pragma warning disable SA1402 // File may only contain a single type
#pragma warning disable SA1600 // Elements should be documented
#pragma warning disable SA1502 // Element should not be on a single line
    public class ArctangentActivationProviderTest(ITestOutputHelper helper)
        : ActivationFunctionProviderTest<ArctangentActivationFunctionProvider>(helper) { }
    public class BentIdentityActivationProviderTest(ITestOutputHelper helper)
        : ActivationFunctionProviderTest<BentIdentityActivationFunctionProvider>(helper) { }
    public class BipolarActivationProviderTest(ITestOutputHelper helper)
        : ActivationFunctionProviderTest<BipolarActivationFunctionProvider>(helper) { }
    public class BipolarSigmoidActivationProviderTest(ITestOutputHelper helper)
        : ActivationFunctionProviderTest<BipolarSigmoidActivationFunctionProvider>(helper) { }
    public class GaussianActivationProviderTest(ITestOutputHelper helper)
        : ActivationFunctionProviderTest<GaussianActivationFunctionProvider>(helper) { }
    public class HyperbolicTangentActivationProviderTest(ITestOutputHelper helper)
        : ActivationFunctionProviderTest<HyperbolicTangentActivationFunctionProvider>(helper) { }
    public class LinearActivationProviderTest(ITestOutputHelper helper)
        : ActivationFunctionProviderTest<LinearActivationFunctionProvider>(helper) { }
    public class LogisticActivationProviderTest(ITestOutputHelper helper)
        : ActivationFunctionProviderTest<LogisticActivationFunctionProvider>(helper) { }
    public class RectifiedLinearUnitActivationProviderTest(ITestOutputHelper helper)
        : ActivationFunctionProviderTest<RectifiedLinearUnitActivationFunctionProvider>(helper) { }
    public class SigmoidActivationProviderTest(ITestOutputHelper helper)
        : ActivationFunctionProviderTest<SigmoidActivationFunctionProvider>(helper) { }
    public class SincActivationProviderTest(ITestOutputHelper helper)
        : ActivationFunctionProviderTest<SincActivationFunctionProvider>(helper) { }
    public class SinusoidalActivationProviderTest(ITestOutputHelper helper)
        : ActivationFunctionProviderTest<SinusoidalActivationFunctionProvider>(helper) { }
    public class SoftPlusActivationProviderTest(ITestOutputHelper helper)
        : ActivationFunctionProviderTest<SoftPlusActivationFunctionProvider>(helper) { }
#pragma warning restore SA1502 // Element should not be on a single line
#pragma warning restore SA1600 // Elements should be documented
#pragma warning restore SA1402 // File may only contain a single type
#pragma warning restore SA1516 // Elements should be separated by blank line
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
}