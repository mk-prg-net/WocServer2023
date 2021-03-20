using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MKPRG.Naming.TechTerms.Access.Datasources.WellKnown.FileSystem
{
    /// <summary>
    /// mko, 22.7.2020
    /// Dateisystem
    /// </summary>
    public class FileSystem
        : NamingBase
    {
        public const long UID = 0x24389B8D;

        public FileSystem()
            : base(UID)
        {
        }

        public override string CNT => "fileSys";
        public override string CN => "文件系统";
        public override string DE => "Dateisystem";
        public override string EN => "File system";
        public override string ES => "Sistema de archivos";
    }

    /// <summary>
    /// mko, 22.7.2020
    /// Dateiverzeichnis
    /// </summary>
    public class FileDir
        : NamingBase
    {
        public const long UID = 0x641C8BBB;

        public FileDir()
            : base(UID)
        {
        }

        public override string CNT => "fileDir";
        public override string CN => "文件目录";
        public override string DE => "Dateiverzeichnis";
        public override string EN => "File directory";
        public override string ES => "Directorio de archivos";
    }

    /// <summary>
    /// mko, 22.7.2020
    /// Datei
    /// </summary>
    public class File
        : NamingBase
    {
        public const long UID = 0xFD085F3E;

        public File()
            : base(UID)
        {
        }

        public override string CNT => "file";
        public override string CN => "文件";
        public override string DE => "Datei";
        public override string EN => "File";
        public override string ES => "Archivo";
    }

    /// <summary>
    /// mko, 16.02.2021
    /// Dateinamen
    /// </summary>
    public class FileName
        : NamingBase
    {
        public const long UID = 0x99B95861;

        public FileName()
            : base(UID)
        {
        }

        public override string CNT => "fileName";
        public override string CN => "文件名";
        public override string DE => "Dateiname";
        public override string EN => "File Name";
        public override string ES => "Nombre del archivo";
    }

    /// <summary>
    /// mko, 16.02.2021
    /// Dateinamenserweiterung
    /// </summary>
    public class FileExtension
        : NamingBase
    {
        public const long UID = 0xC55EA99C;

        public FileExtension()
            : base(UID)
        {
        }

        public override string CNT => "fileExt";
        public override string CN => "文件名扩展名";
        public override string DE => "Dateinamenserweiterung";
        public override string EN => "File Name Extension";
        public override string ES => "Extensión del nombre del archivo";
    }
}
