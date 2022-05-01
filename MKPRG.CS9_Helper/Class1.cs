using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


/// <summary>
/// mko, 1.5.2022
/// Ermöglicht die Kompilation von C# 9 Code in "alten" .NET Framework Projekten
/// Siehe auch https://stackoverflow.com/questions/64749385/predefined-type-system-runtime-compilerservices-isexternalinit-is-not-defined
/// 
/// </summary>
namespace System.Runtime.CompilerServices
{
    public static class IsExternalInit { }
}