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
 
# Suggesting changes to existing code

- When asked to suggest changes to existing code, do not provide the whole of the updated class, instead provide a diff of the relevant code sections with the proposed changes. Use the following format for the diff:
  ```diff
  - // Original code line
  + // Modified code line
  ```