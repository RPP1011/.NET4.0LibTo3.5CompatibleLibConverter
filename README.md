# .NET4.0LibTo3.5CompatibleLibConverter

The goal of this project is to make a software that can automatically convert https://github.com/Facepunch/Facepunch.Steamworks/tree/master into a .NET 3.5 compatible SDK.

How it works
- TODO

Issues:
- Currently, there are empty interfaces and types are being imported incorrectly (i.e Void instead of void) or the concrete class is referenced instead of the interface. 