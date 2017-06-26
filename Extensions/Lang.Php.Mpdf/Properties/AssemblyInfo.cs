using System.Reflection;
using System.Runtime.InteropServices;

// General Information about an assembly is controlled through the following 
// set of attributes. Change these attribute values to modify the information
// associated with an assembly.
using Lang.Php;

[assembly: AssemblyTitle("Lang.Php.Mpdf")]
[assembly: AssemblyDescription("Simple facade for mPDF library")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("Internet Sukces Piotr Stęclik")]
[assembly: AssemblyProduct("C# to PHP package")]
[assembly: AssemblyCopyright("Copyright © Internet Sukces Piotr Stęclik 2014")]
[assembly: AssemblyTrademark("")]
[assembly: AssemblyCulture("")]

// Setting ComVisible to false makes the types in this assembly not visible 
// to COM components.  If you need to access a type in this assembly from 
// COM, set the ComVisible attribute to true on that type.
[assembly: ComVisible(false)]

// The following GUID is for the ID of the typelib if this project is exposed to COM
[assembly: Guid("fa979abe-9003-464d-a0ae-90e5feff29ea")]

// Version information for an assembly consists of the following four values:
//
//      Major Version
//      Minor Version 
//      Build Number
//      Revision
//
// You can specify all the values or you can default the Build and Revision Numbers 
// by using the '*' as shown below:
// [assembly: AssemblyVersion("1.0.*")]
[assembly: AssemblyVersion("1.0.14309.8")]
[assembly: AssemblyFileVersion("1.0.14309.8")]



[assembly: RootPath("/lib/mpdf")]
[assembly: PhpPackageSource("http://mpdf1.com/repos/MPDF57.zip", "MPDF57")]
[assembly: ModuleIncludeConst("MPDF_LIB_PATH")]