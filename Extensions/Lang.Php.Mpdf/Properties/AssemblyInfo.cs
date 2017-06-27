using System.Reflection;
using System.Runtime.InteropServices;

// General Information about an assembly is controlled through the following 
// set of attributes. Change these attribute values to modify the information
// associated with an assembly.
using Lang.Php;

 

// The following GUID is for the ID of the typelib if this project is exposed to COM
[assembly: Guid("fa979abe-9003-464d-a0ae-90e5feff29ea")]

 


[assembly: RootPath("/lib/mpdf")]
[assembly: PhpPackageSource("http://mpdf1.com/repos/MPDF57.zip", "MPDF57")]
[assembly: ModuleIncludeConst("MPDF_LIB_PATH")]