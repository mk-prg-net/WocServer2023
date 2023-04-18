using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MKPRG.Naming.TechTerms.Access.Datasources.WellKnown.FileSystem.Errors
{
    /// <summary>
    /// mko, 22.7.2020
    /// Dateisystem
    /// </summary>
    public class FileNotExists
        : NamingBase
    {
        public const long UID = 0x4E9EA274;

        public FileNotExists()
            : base(UID)
        {
        }

        public override string CNT => "fileNotExists";
        public override string CN => "该文件不存在";
        public override string DE => "Die Datei ist nicht vorhanden";
        public override string EN => "File not exists";
        public override string ES => "El archivo no existe";

        public override string Glyph => Glyphs.Signalization.ErrorOccured;
    }

    /// <summary>
    /// mko, 22.7.2020
    /// Dateisystem
    /// </summary>
    public class DirectoryNotExists
        : NamingBase
    {
        public const long UID = 0x47363EBE;

        public DirectoryNotExists()
            : base(UID)
        {
        }

        public override string CNT => "fileDirectoryNotExists";
        public override string CN => "文件目录不存在";
        public override string DE => "Das Dateiverzeichnis ist nicht vorhanden";
        public override string EN => "File directory not exists";
        public override string ES => "El directorio de archivos no existe";

        public override string Glyph => Glyphs.Signalization.ErrorOccured;
    }
}
