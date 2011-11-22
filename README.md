NBeholder
=========

NBeholder is a C# .net DLL that watches which assemblies are loaded in the current application domain. It is built on .net 2.0, so it should work with any application using .net 2.0 or later.

Using it is as easy as:

	// open the NBeholder's eyes
	EyeOfTheBeholder moraggsEye = new EyeOfTheBeholder();
	// KINDLY ask it to tell us which assemblies are loaded and print them to stdout:
	moraggsEye.RunningAssemblies().ToList().ForEach(x => System.Console.WriteLine("AssemblyName: " + x["AssemblyName"]));


Currently, this is the info NBeholder gets from assemblies:

- Filename
- Filesize (KB)
- AssemblyName
- Version
- Culture
- PublicKeyToken

