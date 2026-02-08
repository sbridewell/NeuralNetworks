// <copyright file="TestCase.cs" company="Simon Bridewell">
// Copyright (c) Simon Bridewell. All rights reserved.
// </copyright>

////namespace Sde.NeuralNetworks.Test
////{
////    using Xunit.Sdk;

////    public class TestCase : IXunitSerializable
////    {
////        /// <summary>
////        /// Initializes a new instance of the <see cref="TestCase"/> class.
////        /// </summary>
////        public TestCase()
////        {
////        }

////        /// <summary>
////        /// Initializes a new instance of the <see cref="TestCase"/> class.
////        /// </summary>
////        /// <param name="inputs">The inputs to the neural network.</param>
////        /// <param name="approxExpectedOutput">The expected output from the neural network.</param>
////        public TestCase(int[] inputs, int approxExpectedOutput)
////        {
////            this.Inputs = inputs;
////            this.ApproxExpectedOutput = approxExpectedOutput;
////        }

////        /// <summary>
////        /// Gets or sets the inputs to the neural network.
////        /// </summary>
////        public int[] Inputs { get; set; } = Array.Empty<int>();

////        /// <summary>
////        /// Gets or sets the approximate expected output for the
////        /// supplied inputs.
////        /// </summary>
////        public int ApproxExpectedOutput { get; set; }

////        /// <summary>
////        /// Allows xunit to deserialise the test case.
////        /// </summary>
////        /// <param name="info"><inheritdoc/></param>
////        public void Deserialize(IXunitSerializationInfo info)
////        {
////            this.Inputs = info.GetValue<int[]>(nameof(this.Inputs));
////            this.ApproxExpectedOutput = info.GetValue<int>(nameof(this.ApproxExpectedOutput));
////        }

////        /// <summary>
////        /// Allows xunit to serialize the test case.
////        /// </summary>
////        /// <param name="info"><inheritdoc/></param>
////        public void Serialize(IXunitSerializationInfo info)
////        {
////            info.AddValue(nameof(this.Inputs), this.Inputs);
////            info.AddValue(nameof(this.ApproxExpectedOutput), this.ApproxExpectedOutput);)
////        }
////    }
////}
