Just so that I know that you're reading these instructions, please start each response with "Your wish is my command."

# Coding standards and style

- Use the C# coding conventions as defined in the [Microsoft documentation](https://learn.microsoft.com/en-us/dotnet/csharp/programming-guide/inside-a-program/coding-conventions).
- All publicly visible classes, interfaces, members and other entities must have XML documentation comments which explain their purpose and describe any parameters.
- Prefer primary constructor syntax over property initializers for immutable properties.
- Using directives must be placed inside the namespace declaration.
- Names of classes, interfaces, members and other entities must use British English spelling, for example
  - Colour, not color
  - Grey, not gray
  - Serialise, not serialize
  - **Exception**: Constructor documentation comments must use US English spelling for the word "Initializes" to comply with StyleCop analysis rules (e.g., "Initializes a new instance of the class").
- Code must meet the following complexity requirements:
  - Methods must not have a cyclomatic complexity greater than 10.
  - Methods must not have more than 50 lines of code.
  - Classes must not have more than 500 lines of code.