using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


/// <summary>
/// mko, 1.5.2022
/// Ermöglicht die Kompilation von C# 9 Code in "alten" .NET Framework Projekten
/// Siehe auch 
/// 1. [C#9](https://sergiopedri.medium.com/enabling-and-using-c-9-features-on-older-and-unsupported-runtimes-ce384d8debb)
/// 2. [bugFix](https://stackoverflow.com/questions/64749385/predefined-type-system-runtime-compilerservices-isexternalinit-is-not-defined)
/// 
/// </summary>
namespace System.Runtime.CompilerServices
{
    public static class IsExternalInit { }
}