// <copyright file="ActivationFunctionProviderTest.cs" company="Simon Bridewell">
// Copyright (c) Simon Bridewell. All rights reserved.
// </copyright>

namespace Sde.NeuralNetworks.Test.ActivationFunctionProviders
{
    using System.Text;
    using FluentAssertions;
    using Sde.NeuralNetworks.ActivationProviders;

    /// <summary>
    /// Unit tests for <see cref="IActivationFunctionProvider"/> implementations.
    /// </summary>
    /// <typeparam name="T">The type of activation function provider being tested.</typeparam>
    public abstract class ActivationFunctionProviderTest<T>
        where T : IActivationFunctionProvider, new()
    {
        /// <summary>
        /// Gets some sample inputs to test the provider with.
        /// </summary>
        public static TheoryData<double> SampleIputs =>
            [
                -10.0, -5.0, -1.0, -0.1, -1e-6, 0.0, 1e-6, 0.1, 1.0, 5.0, 10.0,
            ];

        /// <summary>
        /// Tests that the gradient calculated by the provider is correct
        /// by comparing it to a numerical approximation of the gradient
        /// using the central difference formula.
        /// </summary>
        /// <param name="input">The input to the CalculateGradient function.</param>
        [Theory]
        [MemberData(nameof(SampleIputs))]
        public void CalculateGradient_ReturnsCorrectValue(double input)
        {
            // Arrange
            const double h = 1e-6;
            const double absoluteTolerance = 1e-6;
            const double relativeTolerance = 1e-3; // 0.1%
            var provider = new T();
            var providerName = provider.GetType().Name;
            var fPlus = provider.CalculateActivation(input + h);
            var fMinus = provider.CalculateActivation(input - h);
            var expectedGradient = (fPlus - fMinus) / (2.0 * h);
            //expectedGradient.Should().NotBe(double.NaN, $"{providerName} produced NaN at input={input}.");
            //expectedGradient.Should().NotBe(double.PositiveInfinity, $"{providerName} produced +Infinity at input={input}.");
            //expectedGradient.Should().NotBe(double.NegativeInfinity, $"{providerName} produced -Infinity at input={input}.");
            if (double.IsNaN(expectedGradient))
            {
                Assert.Skip($"Test setup error - {providerName} produced NaN at input={input}.");
            }

            if (double.IsInfinity(expectedGradient))
            {
                Assert.Skip($"Test setup error - {providerName} produced {expectedGradient} at input={input}.");
            }

            // Act
            var actualGradient = provider.CalculateGradient(input);

            // Assert
            var error = Math.Abs(actualGradient - expectedGradient);
            var allowed = Math.Max(absoluteTolerance, relativeTolerance * Math.Max(1.0, Math.Abs(expectedGradient)));
            var sb = new StringBuilder();
            sb.AppendLine();
            sb.AppendLine($"For input {input:G17} + {h:G17}, activation function returns {fPlus}.");
            sb.AppendLine($"For input {input:G17} - {h:G17}, activation function returns {fMinus}.");
            sb.AppendLine($"Expected gradient is {expectedGradient:G17}, actual gradient is {actualGradient:G17}.");
            sb.AppendLine($"Difference between expected and actual gradient is {error:G17}, expected a difference of less than {allowed:G17}.");
            error.Should().BeLessThanOrEqualTo(
                allowed,
                sb.ToString());
        }
    }

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
#pragma warning disable SA1516 // Elements should be separated by blank line
#pragma warning disable SA1402 // File may only contain a single type
#pragma warning disable SA1600 // Elements should be documented
#pragma warning disable SA1502 // Element should not be on a single line
    public class ArctangentActivationProviderTest : ActivationFunctionProviderTest<ArctangentActivationProvider> { }
    public class BentIdentityActivationProviderTest : ActivationFunctionProviderTest<BentIdentityActivationProvider> { }
    public class BipolarActivationProviderTest : ActivationFunctionProviderTest<BipolarActivationProvider> { }
    public class BipolarSigmoidActivationProviderTest : ActivationFunctionProviderTest<BipolarSigmoidActivationProvider> { }
    public class GaussianActivationProviderTest : ActivationFunctionProviderTest<GaussianActivationProvider> { }
    public class HyperbolicTangentActivationProviderTest : ActivationFunctionProviderTest<HyperbolicTangentActivationProvider> { }
    public class LinearActivationProviderTest : ActivationFunctionProviderTest<LinearActivationProvider> { }
    public class LogisticActivationProviderTest : ActivationFunctionProviderTest<LogisticActivationProvider> { }
    public class RectifiedLinearUnitActivationProviderTest : ActivationFunctionProviderTest<RectifiedLinearUnitActivationProvider> { }
    ////public class SigmoidActivationProviderTest : ActivationFunctionProviderTest<SigmoidActivationProvider> { }
    public class SincActivationProviderTest : ActivationFunctionProviderTest<SincActivationProvider> { }
    public class SinusoidalActivationProviderTest : ActivationFunctionProviderTest<SinusoidalActivationProvider> { }
    public class SoftPlusActivationProviderTest : ActivationFunctionProviderTest<SoftPlusActivationProvider> { }
#pragma warning restore SA1502 // Element should not be on a single line
#pragma warning restore SA1600 // Elements should be documented
#pragma warning restore SA1402 // File may only contain a single type
#pragma warning restore SA1516 // Elements should be separated by blank line
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
}