using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MKPRG.Naming.TechTerms.ATMO.DFC.Installer
{

    /// <summary>
    /// mko, 6.7.2020
    /// </summary>
    public class ApplicationNameDfcINstaller
     : NamingBase
    {

        public const long UID = 0x29A90467;

        public ApplicationNameDfcINstaller()
            : base(UID)
        {
        }


        public override string CNT => "dfcInstaller";
        public override string CN => CNT;
        public override string DE => "Verwaltung der DFC2 Client Installation auf lokalem Rechner";
        public override string EN => CNT;
        public override string ES => CNT;
    }


    /// <summary>
    /// mko, 22.6.2020
    /// </summary>
    public class LogCurrentDfcInstallerVersion
     : NamingBase
    {

        public const long UID = 0xF8D4889D;

        public  LogCurrentDfcInstallerVersion()
            : base(UID)
        {
        }


        public override string CNT => "logCurrentDfcInstallerVersion";
        public override string CN => "记录当前安装的DFC版本";
        public override string DE => "Aktuelle installierte DFC Version protokollieren";
        public override string EN => "Log current used DFC version";
        public override string ES => "Registre la versión DFC instalada actualmente";
    }

    /// <summary>
    /// mko, 13.1.2021
    /// Name der MSI- Installationsdatei des DFC- Clients
    /// </summary>
    public class DfcClientMsiInstallationFile
     : NamingBase
    {

        public const long UID = 0xFB4D181F;

        public DfcClientMsiInstallationFile()
            : base(UID)
        {
        }

        public override string CNT => "dfcClientMsiInstallationFile";
        public override string CN => "用于安装DFC客户端的MSI文件。";
        public override string DE => "MSI Datei zur Installation des DFC- Clients";
        public override string EN => "MSI file for installing the DFC client";
        public override string ES => "Archivo MSI para la instalación del cliente DFC";
    }


    /// <summary>
    /// mko, 13.1.2021
    /// Name der MSI- Datei des TEF- Clients
    /// </summary>
    public class TefClientMsiInstallationFile
        : NamingBase
    {

        public const long UID = 0xC42EF1D8;

        public TefClientMsiInstallationFile()
            : base(UID)
        {
        }

        public override string CNT => "tefClientMsiInstallationFile";
        public override string CN => "用于安装Tef-客户端的MSI文件。";
        public override string DE => "MSI Datei zur Installation des TEF- Clients";
        public override string EN => "MSI file for the installation of the Tef- Client";
        public override string ES => "Archivo MSI para la instalación del cliente TEF";
    }


}
