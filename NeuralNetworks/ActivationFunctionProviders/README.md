# ActivationFunctionProviders folder

Activation function providers are classes that provide activation functions and their derivative functions for neural networks. The intent is that different providers can be injected into a general-purpose neural network implementation to allow different activation functions to be used without changing the neural network code itself.

Some of these providers are known to be broken, because if we chart the activation function and its derivative, it is visually clear that the derivative doesn't match the slope of the activation function at all points. At the time of writing, I lack the mathematical knowledge to implement these correctly, but I hope to do so once I learn about the chain rule.