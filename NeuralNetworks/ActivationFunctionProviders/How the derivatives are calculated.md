# How the activation function derivatives are calculated

| Function name | Unit tests passing? | Mathematically proven? |
| --- | --- | --- |
| Arctangent | No | No - need identity for atan(x) |
| BentIdentity | Yes | Yes |
| Bipolar | Yes | Yes (no calculus needed) |
| BipolarSigmoid | Yes | Still needs work |
| Gaussian | Yes | Yes (and improved) |
| HyperbolicTangent | Yes | No - need identity for tanh(x) |
| Linear | Yes | Yes |
| Logistic | Yes | Yes (equivalent) |
| RectifiedLinearUnit | Yes | Yes (no calculus needed) |
| Sigmoid | Yes | Yes |
| Sinc | Yes | Yes |
| Sinusoidal | Yes | Yes |
| SoftPlus | Yes | Yes (equivalent) |

- [How the activation function derivatives are calculated](#how-the-activation-function-derivatives-are-calculated)
  - [Arctangent](#arctangent)
    - [Activation function](#activation-function)
  - [BentIdentity](#bentidentity)
    - [Activation function](#activation-function-1)
    - [Calculating the derivative](#calculating-the-derivative)
    - [Derivative function](#derivative-function)
  - [Bipolar](#bipolar)
    - [Activation function](#activation-function-2)
    - [Derivative function](#derivative-function-1)
  - [BipolarSigmoid](#bipolarsigmoid)
    - [Activation function](#activation-function-3)
    - [Calculating the derivative (not working)](#calculating-the-derivative-not-working)
    - [Derivative function](#derivative-function-2)
  - [Gaussian](#gaussian)
    - [Activation function](#activation-function-4)
    - [Calculating the derivative](#calculating-the-derivative-1)
    - [Derivative function](#derivative-function-3)
  - [HyperbolicTangent](#hyperbolictangent)
    - [Activation function](#activation-function-5)
  - [Linear](#linear)
    - [Activation function](#activation-function-6)
    - [Derivative function](#derivative-function-4)
  - [Logistic](#logistic)
    - [Activation function](#activation-function-7)
    - [Calculating the derivative using quotient rule](#calculating-the-derivative-using-quotient-rule)
    - [Derivative function as implemented](#derivative-function-as-implemented)
  - [RectifiedLinearUnit](#rectifiedlinearunit)
    - [Activation function](#activation-function-8)
    - [Derivative function](#derivative-function-5)
  - [Sigmoid](#sigmoid)
    - [Activation function](#activation-function-9)
    - [Calculating the derivative](#calculating-the-derivative-2)
    - [Derivative function](#derivative-function-6)
  - [Sinc](#sinc)
    - [Activation function](#activation-function-10)
    - [Calculating the derivative](#calculating-the-derivative-3)
    - [Derivative function](#derivative-function-7)
  - [Sinusoidal](#sinusoidal)
    - [Activation function](#activation-function-11)
    - [Derivative function](#derivative-function-8)
  - [SoftPlus](#softplus)
    - [Activation function](#activation-function-12)
    - [Calculating the derivative](#calculating-the-derivative-4)
    - [Derivative function](#derivative-function-9)


## Arctangent

### Activation function

```csharp
Math.Atan(input)
```

**How do we find the derivative of arctangent?**

## BentIdentity

### Activation function
```csharp
return ((Math.Sqrt(Math.Pow(input, 2) + 1) - 1) / 2) + input;
```

```
y(x) = (((x^2 + 1)^(1/2) - 1) / 2) + x
```

### Calculating the derivative

`y = a + b` where

| Name | Function | Derivative |
| --- | --- | --- |
| a(x) | `(((x^2) + 1)^(1/2) - 1) / 2` | Take 1 term at a time |
| b(x) | x | 1 |

```
a = 0.5((x^2 + 1)^(1/2) - 1)
  = 0.5(x^2 + 1)^(1/2) - 0.5
```

`a = c + d` where

| Name | Function | Derivative |
| --- | --- | --- |
| c(x) | `0.5(x^2 + 1)^(1/2)` | Find using chain rule |
| d(x) | -0.5 | 0 |

Chain rule:

`c(x) = outer(inner(x))` where

| Name | Function | Derivative |
| --- | --- | --- |
| inner(x) | x^2 + 1 | 2x |
| outer(x) | 0.5x^(1/2) | 0.25x^(-1/2) |

```
c'(x) = outer'(inner(x))inner'(x)
      = 0.25(x^2 + 1)^(-1/2) * 2x
      = 0.5x / (x^2 + 1)^(1/2)
```

`a = c + d` where

| Name | Function | Derivative |
| --- | --- | --- |
| c(x) | `0.5(x^2 + 1)^(1/2)` | `0.5x / (x^2 + 1)^(1/2)` |
| d(x) | -0.5 | 0 |

```
a' = c' + d'
   = 0.5x / (x^2 + 1)^(1/2)

y' = a' + b'
   = (0.5x / (x^2 + 1)^(1/2)) + 1
   = (x / 2(x^2 + 1)^(1/2)) + 1
```


### Derivative function

```csharp
return (input / (2 * Math.Sqrt(Math.Pow(input, 2) + 1))) + 1;
```

## Bipolar

### Activation function
```csharp
return input < 0 ? -1 : 1;
```

No calculus needed here, allthough we do need to decide how to handle x = 0, because the gradient is infinite there.

### Derivative function

```csharp
return 0;
```

## BipolarSigmoid

### Activation function

```csharp
return (1 - Math.Exp(-input)) / (1 + Math.Exp(-input));
```

```
y(x) = (1 - e^-x) / (1 + e^-x)
     = (1 - (1 / e^x)) / (1 + (1 / e^x))
```

### Calculating the derivative (not working)

`y(x) = a(x) / b(x)` where

| Name | Function | Derivative |
| --- | --- | --- |
| a(x) | 1 - e^-x | e^-x |
| b(x) | 1 + e^-x | -e^-x |

Quotient rule:

```
y' = (a'b - ab') / b^2

a'b = (e^-x)(1 + e^-x)
    = e^-x + 1

ab' = (1 - e^-x)(-e^-x)
    = -e^-x + e^-2x
    = e^-2x - e^-x

a'b - ab' = (e^-x)(1 + e^-x) - (1 - e^-x)(-e^-x)
          = e^-x + 1 - e^-2x + e^-x
          = 2e^-x - e^-2x + 1

b^2 = (1 - e^-x)^2
    = 1 - 2e^-x + e^-2x

y' = (2e^-x - e^-2x + 1) / (1 - e^-x)^2
   = (2 * Math.Exp(-input) - Math.Exp(-2 * input) + 1) / Math.Pow(1 - Math.Exp(-input), 2)
```

### Derivative function
```csharp
// Unproven but passes unit tests
var activatedValue = this.CalculateActivation(input);
return 0.5 * (1 + activatedValue) * (1 - activatedValue);

// Calculated but fails unit tests
//return (-2 * Math.Exp(-input)) / (Math.Pow(1 + Math.Exp(-input), 2));
```

## Gaussian

### Activation function

```csharp
return Math.Exp(Math.Pow(-input, 2));
```

```
y(x) = e^(x^2)
```

### Calculating the derivative

Chain rule:

`y = outer(inner(x))` where

| Name | Function | Derivative |
| --- | --- | --- |
| inner(x) | x^2 | 2x |
| outer(x) | e^x | e^x |

```
y' = outer'(inner(x))inner'(x)
   = e^(x^2) * 2x
   = 2x * e^(x^2)
```

### Derivative function

```csharp
return 2 * input * Math.Exp(Math.Pow(input, 2));
```

## HyperbolicTangent

### Activation function

```csharp
return Math.Tanh(input);
```

## Linear

### Activation function

```csharp
return input;
```

Calculating the derivative is trivial, the gradient is always 1.

### Derivative function

```csharp
return 1;
```

## Logistic

### Activation function

```csharp
return 1 / (1 + Math.Exp(-input));
```

```
y(x) = 1 / (1 + e^-x)
```

### Calculating the derivative using quotient rule

`y = a / b` where

| Name | Function | Derivative |
| --- | --- | --- |
| a(x) | 1 | 0 |
| b(x) | 1 + e^-x | -e^-x | # TODO: revisit BipolarSigmoid knowing that `(e^-x)' = -e^-x`, not e^-x

Quotient rule:

```
y' = (a'b - ab') / b^2
   = (0 * (1 + e^-x) - 1 * (-e^-x)) / (1 + e^-x)^2
   = e^-x / (1 + e^-x)^2
```

This yields the following function, which passes the unit tests

```csharp
return Math.Exp(-input) / Math.Pow(1 + Math.Exp(-input), 2
```

### Derivative function as implemented

```csharp
var activatedValue = this.CalculateActivation(input) * (1 - this.CalculateActivationValue(input));
```

## RectifiedLinearUnit

### Activation function

```csharp
return input < 0 ? 0 : 1;
```

### Derivative function

No calculus needed

```csharp
return input < 0 ? 0 : 1
```

## Sigmoid

### Activation function

```csharp
return 1 / (1 + Math.Exp(-input));
```

```
y(x) = 1 / (1 + e^-x)
```

### Calculating the derivative

`y = a / b` where

| Name | Function | Derivative
| --- | --- | --- |
| a(x) | 1 | 0 |
| b(x) | 1 + e^-x | -e^-x |

Quotient rule:

```
y' = (a'b - ab') / b^2
   = (0(1 + e^-x) - 1(-e^-x)) / (1 + e^-x)^2
   = e^-x / (1 + e^-x)^2
```

### Derivative function

```csharp
return Math.Exp(-input) / Math.Pow(1 + Math.Exp(-input), 2);
```

## Sinc

### Activation function

```csharp
return input == 0 ? 1 : Math.Sin(input) / input;
```

If we ignore the special case at x = 0, this is

```
y(x) = sin(x) / x
```

### Calculating the derivative

`y = a / b` where

| Name | Function | Derivative |
| --- | --- | --- |
| a(x) | sin(x) | cos(x) |
| b(x) | x | 1 |

Quotient rule:

```
y' = (a'b - ab') / b^2
   = (cos(x)x - sin(x)) / x^2
   = (cos(x) / x) - (sin(x) / x^2)
```

### Derivative function

```csharp
return (Math.Cos(input) / input) - (Math.Sin(input) / Math.Pow(input, 2));
```

## Sinusoidal

### Activation function

```csharp
return Math.Sin(input);
```

No calculus needed = `sin'(x) = cos(x)` is a trig identity

### Derivative function

```csharp
return Math.Cos(input);
```

## SoftPlus

### Activation function

```csharp
return Math.Log(1 + Math.Exp(input));
```

```
y(x) = log(1 + e^x)
```

### Calculating the derivative

`y = outer(inner(x))` where

| Name | Function | Derivative |
| -- | -- | -- |
| outer(x) | ln(x) | 1/x |
| inner(x) | 1 + e^x | e^x |

Chain rule: 

```
y(x) = outer'(inner(x))inner'(x)
     = (1 / (1 + e^x))(e^x)
     = e^x / (1 + e^x)
     = Math.Exp(input) / (1 + Math.Exp(-input))
```

### Derivative function

```csharp
return 1 / (1 + Math.Exp(-input));
```