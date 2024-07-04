# .NET4.0LibTo3.5CompatibleLibConverter

The goal of this project is to make a software that can automatically convert https://github.com/Facepunch/Facepunch.Steamworks/tree/master into a .NET 3.5 compatible SDK.

How it works
- The software will take the .NET 4.0 library and generate a .NET 3.5 compatible wrapper around it.
- The wrapper library will be generated in a new project and will use interop libraries to call the .NET 4.0 library.

Issues:
- Test cases are failing. Need to fix them.

- Dead project
