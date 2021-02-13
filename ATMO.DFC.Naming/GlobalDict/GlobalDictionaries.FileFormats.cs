using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DZAUtilities_Dictionaries
{
    partial class GlobalDictionaries
    {
        /// <summary>
        /// mko, 12.10.2018
        /// Entschlüsselt die Speicherung, die hinter einem Storagetype steht.
        /// Zu jedem StorageType gibt es eine Enumeration, die alle darunter verwalteten Speicherformate definiert.
        /// Der Wert eines jeden Eintrages wiederum korrespondiert mit dem Feld in der Path- Tabelle
        /// </summary>
        public class StorageTypeFileFormats
        {
            // Definiert die Felder in Path
            public enum MappingFileNameToFieldNo
            {
                // Feld FileNamePDF
                FileNamePDF = 0,

                // Feld FileName2
                FileName2 = 1,

                // Feld FileName3
                FileName3 = 2
            }


            public enum gzip
            {
                zip = MappingFileNameToFieldNo.FileName2
            }

            public enum wwdocx
            {
                docx = MappingFileNameToFieldNo.FileName2
            }

            /// <summary>
            /// mko, 26.11.2018
            /// geändert von docx in doc
            /// </summary>
            public enum ww80doc
            {
                doc = MappingFileNameToFieldNo.FileName2
            }

            public enum ppt80
            {
                ppt = MappingFileNameToFieldNo.FileName2
            }

            public enum xlsx
            {
                xlsx = MappingFileNameToFieldNo.FileName2
            }

            public enum excel80
            {
                xls = MappingFileNameToFieldNo.FileName2
            }

            public enum excel80andpdf
            {
                pdf = MappingFileNameToFieldNo.FileNamePDF,
                xls = MappingFileNameToFieldNo.FileName2
            }

            public enum excel80andtif
            {
                xls = MappingFileNameToFieldNo.FileName2,
                tif = MappingFileNameToFieldNo.FileName3
            }

            public enum pdf
            {
                pdf = MappingFileNameToFieldNo.FileNamePDF
            }

            public enum pdfdwf
            {
                pdf = MappingFileNameToFieldNo.FileNamePDF,
                dwf = MappingFileNameToFieldNo.FileName2
            }

            public enum pdfdwg
            {
                pdf = MappingFileNameToFieldNo.FileNamePDF,
                dwg = MappingFileNameToFieldNo.FileName2
            }

            public enum pdfeplan
            {
                pdf = MappingFileNameToFieldNo.FileNamePDF,
                eplan = MappingFileNameToFieldNo.FileName2
            }

            public enum pdfeplanzw
            {
                pdf = MappingFileNameToFieldNo.FileNamePDF,
                eplan = MappingFileNameToFieldNo.FileName2
            }


            public enum pdfmi
            {
                pdf = MappingFileNameToFieldNo.FileNamePDF,
                mi = MappingFileNameToFieldNo.FileName2
            }

            public enum pdfmidwg
            {
                pdf = MappingFileNameToFieldNo.FileNamePDF,
                mi = MappingFileNameToFieldNo.FileName2,
                dwg = MappingFileNameToFieldNo.FileName3
            }

            public enum dwf
            {
                dwf = MappingFileNameToFieldNo.FileName2
            }

            public enum dwg
            {
                dwg = MappingFileNameToFieldNo.FileName2
            }

            public enum modini
            {
                ini = MappingFileNameToFieldNo.FileName2,
                mod = MappingFileNameToFieldNo.FileName3
            }

            /// <summary>
            /// mko, 28.1.2021
            /// Integration des neuen CTS- Fileformates
            /// </summary>
            public enum modinix
            {
                ini = MappingFileNameToFieldNo.FileName2,
                modx = MappingFileNameToFieldNo.FileName3
            }

            public enum ini
            {
                ini = MappingFileNameToFieldNo.FileName2
            }

            public enum mi
            {
                mi = MappingFileNameToFieldNo.FileName2
            }

            public enum acad15_pdf
            {
                pdf = MappingFileNameToFieldNo.FileNamePDF,
                //acad15 = MappingFileNameToFieldNo.FileName2
                dwg = MappingFileNameToFieldNo.FileName2
            }

            public enum eplan
            {
                eplan = MappingFileNameToFieldNo.FileName2
            }

            public enum eplan_zw1
            {
                eplan = MappingFileNameToFieldNo.FileName2,
                zw1 = MappingFileNameToFieldNo.FileName3
            }
        }
    }
}
