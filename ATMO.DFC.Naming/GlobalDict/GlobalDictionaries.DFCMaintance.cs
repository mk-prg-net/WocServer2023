using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DZAUtilities_Dictionaries
{
    /// <summary>
    /// mko, 16.2.2018
    /// Gloabl constants of DFC2 maintance service process
    /// </summary>
    partial class GlobalDictionaries
    {
        /// <summary>
        /// mko, 16.2.2018
        /// Product owner tuple
        /// </summary>
        public static (
            string Name, 
            string UserId,
            string Department,
            string Email) ProductOwner = ("Joachim Rappold", "JPP2FE", "ATMO-1de/ICO", "Joachim.Rappold@de.bosch.com");

        /// <summary>
        /// mko, 16.2.2018
        /// Url's of documentation
        /// </summary>
        public static (
                string DocupediaUrl,
                string ReadMeUrl
            ) Docu = (
                "https://inside-docupedia.bosch.com/confluence/display/PAATMO1ICOWiki/DFC2_Windowsclient_EN",
                "http://10.3.194.189:8084/");


        /// <summary>
        /// mko, 16.2.2018
        /// Urls of msi files for installation of DFC2 components
        /// </summary>
        //public static (
        //        string DFC2SetupOldSystemUpTo1_8_5,
        //        string UpDowngradeToolVersions,
        //        string DFC2Versions
        //    ) MsiFileUrl = (
        //        @"file:///\\10.3.194.188\GDM_Public$\DocuToolForCustomer\DFC2\DFC2Setup.msi",
        //        @"file:///\\10.3.194.188\gdm_public$\DocuToolForCustomer\DFC2_2\ATMO\Installer",
        //        @"file:///\\10.3.194.188\gdm_public$\DocuToolForCustomer\DFC2_2\ATMO\Versions");

        /// <summary>
        /// mko, 6.8.2020
        /// Urls of msi files for installation of DFC2 components
        /// </summary>
        public static (
                string DFC2SetupOldSystemUpTo1_8_5,
                string UpDowngradeToolVersions,
                string DFC2Versions
            ) MsiFileUrl = (
                @"file:///\\fe0vmc0288.de.bosch.com\gdm_public$\DocuToolForCustomer\DFC2_2\ATMO\Installer\DFC.UpDowngrades.Cmd.SetUp.V3.msi",
                @"file:///\\fe0vmc0288.de.bosch.com\gdm_public$\DocuToolForCustomer\DFC2_2\ATMO\Installer",
                @"file:///\\fe0vmc0288.de.bosch.com\gdm_public$\DocuToolForCustomer\DFC2_2\ATMO\Versions");



    }
}
