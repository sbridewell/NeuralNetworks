# Derivatives cheat sheet

The derivative of a function is another function which returns the slope, or gradient, or rate of change, of the original function. If the function is called `f(x)` then `f` is the name of the function, and its derivative is often referred to as `f'(x)`, pronounced "f prime of x".

## Definition of the derivative (limit rule)

The gradient of a function `f` between two points `x` and `x + h` can be expressed as `(f(x + h) - f(x)) / h`. The limit rule states that the instantaneous gradient of `f` is the limit as h tends towards zero of `(f(x + h) - f(x)) / h`. The limit rule works for finding the derivative of any function, but there are easier ways of finding the derivatives of particular types of function.

## Power rule for polynomials

Multiply the coeficcient of the term by the exponent and decrement the exponent by 1: `(ax^n)' = anx^(n-1)`

A negative power is a reciprocal: `x^(-n) = 1 / (x^n)`

A fractional power is a root: `x^(a/b) = root_b(x^a)`

## Product rule

In each term, take the derivative of one of the functions and leave the others unchanged, then sum the results.

If `y = f(x)g(x)h(x)` then `y' = f'(x)g(x)h(x) + f(x)g'(x)h(x) + f(x)g(x)h'(x)`

## Quotient rule

If `y = f(x) / g(x)` then `y' = (f'(x)g(x) - f(x)g'(x)) / (g(x))^2`

## Trigonometric derivatives

| Function | Derivative |
| --- | --- |
| y = sin(x) | y' = cos(x) |
| y = cos(x) | y' = -sin(x) |
| y = tan(x) | y' = sec^2(x) |
| y = cot(x) | y' = -csc^2(x) |
| y = sec(x) | y' = sec(x)tan(x) |
| y = csc(x) | y' = -csc(x)cot(x) |

## Exponential derivatives

| Function | Derivative |
| --- | --- |
| y = e^x | y' = e^x |
| y = a^x | y' = (a^x)ln(a) |

## Logarithmic derivatives

| Function | Derivative |
| --- | --- |
| y = ln(x) | y' = 1/x |
| y = log_a(x) | y' = 1 / (x ln(a)) |

## Chain rule - for nested functions

If `y = outer(inner(x))` then `y' = outer'(inner(x))inner'(x)`