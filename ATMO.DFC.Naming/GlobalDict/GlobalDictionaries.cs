using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;

namespace DZAUtilities_Dictionaries
{
    /// <summary>
    /// Map diferent object IDs, only available on DZA tables, to the corresponding displayed text in DZA, info available in OEL files
    /// 
    /// mko, 2.9.2020
    /// Aus DZAUtilities_Dictionaries.csprj hierher verschoben, um Zugriff auf enums in allen Projekten zu garantieren.
    /// </summary>
    public static partial class GlobalDictionaries
    {
        public static string SMTPServer = "rb-smtp-int.bosch.com";
        public static string DZADFC2Mail = "DZA_DFC2@de.bosch.com";

        /// <summary>
        /// originated by Jorge
        /// 
        /// mko, 2.9.2020
        /// Materialnummern für Profile
        /// </summary>
        public static Dictionary<string, string> dicProfiles = new Dictionary<string, string>()
        {
            {"30x30","3842990720"},
            {"45x45","3842990520"},
            {"45x45L","3842992425"},
            {"45x60","3842990570"},
            {"45x90","3842990300"},
            {"90x90","3842990500"}
        };

        /// <summary>
        /// originated by Jorge
        /// ?
        /// </summary>
        /// <param name="fieldName"></param>
        /// <param name="language"></param>
        /// <param name="tooltip"></param>
        /// <returns></returns>
        public static string GetColumnCaption(string fieldName, string language, bool tooltip = false)
        {
            string FieldCaption = fieldName;
            if (dicDFC2InternDictionaryPowerSearch.ContainsKey(fieldName))
            {
                string DicLanguages = dicDFC2InternDictionaryPowerSearch[fieldName];
                if (!DicLanguages.Contains(language + "#"))
                    return FieldCaption;
                string[] LanguageCaption = DicLanguages.Split(';');
                foreach (string langField in LanguageCaption)
                {
                    if (langField.StartsWith(language + "#"))
                        FieldCaption = langField.Replace(language + "#", "");
                }
            }
            return FieldCaption;
        }

        /// <summary>
        /// originated by Jorge
        /// </summary>
        public static Dictionary<string, string> dicDFC2InternDictionaryPowerSearch = new Dictionary<string, string>()
        {
            {"A1_Bestand","DE#Bestand A1|Lagerbestand ATMO1;EN#;ES#" },
            {"A1_PLZ","DE#PLZ A1|Planlieferzeit für ATMO1;EN#;ES#" },
            {"A1_Preis","DE#Preis A1|Preis für ATMO1;EN#;ES#" },
            {"ATMOsAllowed","DE#ATMOs Berrechtigt;EN#ATMOs Involved;ES#ATMOs permisos" },
            {"Creator","DE#Ersteller;EN#Creator;ES#Creado por" },
            {"Designation","DE#Name;EN#Name;ES#Nombre" },
            {"docBaseLocation","DE#Herkunft;EN#Base location;ES#Base location" },
            {"DocLocation","DE#Herkunft;EN#Base location;ES#Base location" },
            {"DocFamName","DE#Dokument-Familie|Dokument-Familie (ATD Zeichnung, ATB Stückliste, ATO Angebot, CAT Katalogteil, CTS Cycle Time Sequence, ;EN#;ES#" },
            {"DrawingNr","DE#zugeh. Zng.|Zugehörige Zeichnung in SAP; EN#Drawing Nr|Related Drawing in SAP;ES#Nr de plano ralacionado en SAP" },
            //{"EVW","DE#;EN#;ES#" },
            {"DocumentType","DE#Dokument Typ|DZA Dokument Typ;EN#Document Type;ES#Tipo Documento" },
            {"INFOS","DE#Zusatzangaben;EN#Infos;ES#Infos" },
            {"INFOTEXT","DE#;EN#;ES#" },
            {"isRelevantForDocumentation","DE#;EN#;ES#" },
            {"Lib3D","DE#Lib 3D|3D-Modell in Inventor-Lib vorhanden;EN#;ES#" },
            {"LUP","DE#LUP|Letze Update Datum;EN#LUP|Last Update;ES#LUP|Ultima actulizacion" },//LUP in Documents means Creation Date
            {"Manufacturer","DE#Hersteller;EN#Manufacturer; ES#Fabricante" },
            {"ManufacturerId","DE#Hers. Typ Bez.|Hersteller Typ Bezeichnung in SAP; EN#Manufacturer Id;ES#Fabricante Id" },
            {"ManufacturerOrderId","DE#Hers.Best. NR|Hersteller Bestellungsnummer in SAP; EN#Manufacturer Order Id;ES#" },
            {"MKLASSE","DE#MKLASSE|Material Klasse;EN#MKLASSE;ES#MKLASSE" },
            //State: 01:Blocked, 03:Discontinued,  11:Exchanged
            {"MSTAEx","DE#MSTAE|Werksübergreifender Materialstatus;EN#MSTAE|Cross plant material status;ES#MSTAE|Estado del material para todos los sitios de ATMO" },
            {"MSTAE_A1","DE#MMSTA_A1|Werkspezifischer Materialstatus ATMO1(1060);EN#MMSTA_A1|plant specific material status ATMO1(1060);ES#MMSTA_A1" },
            {"MSTAEx_A2","DE#MMSTA_A2|Werkspezifischer Materialstatus ATMO2(9651);EN#MMSTA_A2|plant specific material status ATMO2(9651);ES#MMSTA_A2|Estado del Material en SAP para ATMO2(9651)" },
            {"MSTAEx_A3","DE#MMSTA_A3|Werkspezifischer Materialstatus ATMO3(6755);EN#MMSTA_A3|plant specific material status ATMO3(6755);ES#MMSTA_A3|Estado del Material en SAP para ATMO3(6755)" },
            {"MSTAEx_A5","DE#MMSTA_A5|Werkspezifischer Materialstatus ATMO5(2576);EN#MMSTA_A5|plant specific material status ATMO5(2576);ES#MMSTA_A5|Estado del Material en SAP para ATMO5(2576)" },
            {"MSTAEx_A6","DE#MMSTA_A6|Werkspezifischer Materialstatus ATMO6(603B);EN#MMSTA_A6|plant specific material status ATMO6(603B);ES#MMSTA_A6|Estado del Material en SAP para ATMO6(603B)" },
            {"MSTAEx_A7","DE#MMSTA_A7|Werkspezifischer Materialstatus ATMO7(9046);EN#MMSTA_A7|plant specific material status ATMO7(9046);ES#MMSTA_A7|Estado del Material en SAP para ATMO7(9046)" },
            {"MSTAEx_A8","DE#MMSTA_A8|Werkspezifischer Materialstatus ATMO8(9395);EN#MMSTA_A8|plant specific material status ATMO8(9395);ES#MMSTA_A8|Estado del Material en SAP para ATMO8(9395)" },
            {"MSTAEx_MH","DE#MMSTA_MH|Werkspezifischer Materialstatus Moehwald(6740);EN#MMSTA_MH|plant specific material status Moehwald(6740);ES#MMSTA_MH|Estado del Material en SAP para Moehwald(6740)" },
            {"MTART","DE#MTART|Materialart im SAP;EN#MTART|Material Type in SAP;ES#MTART|Tipo de Material en SAP" },
            {"Name","DE#Benennung;EN#Name;ES#Nombre" },
            {"NameDE","DE#Benennung DE|Benennung deutsch;EN#Name DE|Name German;ES#Nombre alemán" },
            {"NameEN","DE#Benennung EN|Benennung englisch;EN#Name EN|Name english;ES#Nombre ingles" },
            {"NameES","DE#Benennung ES|Benennung spanisch;EN#Name ES|Name spanish;ES#Nombre español" },
            {"ProjectPSPNr","DE#Projekt PSP Nr;EN#Project PSP Nr;ES#Nr de proyecto PSP Nr" },
            {"PSPNr","DE#PSP Nr|PSP Nummer in SAP;EN#PSP Nr|PSP Number in SAP;ES#PSP Nr|Numero de PSP en SAP" },
            { "Purchase","DE#Beschaffung;EN#;ES#" },
            {"Quantity","DE#Menge;EN#Quantity;ES#Cantidad" },
            {"RawMaterial","DE#Werkstoff; EN#Raw Material; ES#Materia Prima" },
            {"StateEditor","DE#Status Editor;EN#State Editor;ES#Editor" },
            {"StationName","DE#Station Name;EN#Station Name;ES#Nombre de Estacion" },
            {"STLStatus","DE#STL State|Stücklistestatus in SAP;EN#BOM State|BOM State indicated in SAP;ES#BOM Estado|Estado de la lista de Materiales en SAP" },
            {"TDPTYPE","DE#TDP Typ|Typ Tecknische Dokumentation;EN#TDP Type|Type of Technical Documentation;ES#TDP Tipo|Tipo de documentación técnica " },
            {"TechDetails","DE#Tech. Details;EN#Tech. Details;ES#Detalles Tecnicos" },
            {"UserState","DE#Status|Dokument Status;EN#State|Document State;ES#Estado|Estado del documento" },
            {"Verbaut","DE#Verbaut|Anzahl Verbauungen;EN#Assembled|How often is this material assembled;ES#Construido|Cuantas veces se ha utilizado este material" },
            {"ZAT","DE#ZAT;EN#ZAT;ES#ZAT" },
            {"ZZREVSTAND","DE#;EN#;ES#" }
            
            //{"ANFE","DE#MTART;EN#MTART;ES#MTART" },                                        
        };

        /// <summary>
        /// originated by Jorge
        /// </summary>
        public static Dictionary<string, string> dicDFC2KataDoku = new Dictionary<string, string>()
        {
            {"DocuCheckError","DE#Fehler;EN#Error;ES#Error" },
            {"DocID","DE#;EN#;ES#" },
            {"DFCLink","DE#DFC2 Link|Material in neue DFC2 Instanz öfnnen;EN#DFC2 Link|Open Material in a new DFC2 Instance;ES#DFC2 Link" },
            { "StationNr","DE#;EN#;ES#" },
            { "Level01","DE#Green Frog Area;EN#Green Frog Area;ES#Green Frog Area" },

            {"Level02","DE#DE MatNr Docu;EN#DE MatNr Docu;ES#DE MatNr Docu" },
            {"Level03","DE#EN MatNr Docu;EN#EN MatNr Docu;ES#EN MatNr Docu" },
            {"Level04","DE#ES MatNr Docu;EN#ES MatNr Docu;ES#ES MatNr Docu" },
            {"Level05","DE#FR MatNr Docu;EN#FR MatNr Docu;ES#FR MatNr Docu" },
            {"Level06","DE#IT MatNr Docu;EN#IT MatNr Docu;ES#IT MatNr Docu" },
            {"Level07","DE#TR MatNr Docu;EN#TR MatNr Docu;ES#TR MatNr Docu" },
            {"Level08","DE#NL MatNr Docu;EN#NL MatNr Docu;ES#NL MatNr Docu" },
            {"Level09","DE#CS MatNr Docu;EN#CS MatNr Docu;ES#CS MatNr Docu" },
            {"Level10","DE#All MatNr Docu;EN#All MatNr Docu;ES#All MatNr Docu" },
            {"LastBG","DE#Mat Nr.Baugruppe;EN#Mat Nr. Assembly;ES#Mat Nr. Grupo" },
            {"PartOrAssemblyEN","DE#Baugruppe Name EN|Name der Teile oder Baugruppe wo es verbaut ist;EN#Assembly Name EN|Assembly Name where it is assembled;ES#Nombre Grupo Ingles" },
            {"PartOrAssembly","DE#Baugruppe Name|Name der Teile oder Baugruppe wo es verbaut ist;EN#Assembly Name where it is assembled;ES#" },
            {"Pos","DE#Pos Nr.;EN#Pos;ES#Pos" },
            {"MatNr","DE#Mat Nr;EN#;ES#" },
            {"DrawingNr","DE#zugeh. Zng.|Zugehörige Zeichnung in SAP; EN#Drawing Nr|Related Drawing in SAP;ES#Nr de plano ralacionado en SAP" },
            {"DescriptionGerman","DE#Benennung DE|Benennung deutsch;EN#Name DE|Name German;ES#Nombre alemán" },
            {"DescriptionEnglish","DE#Benennung EN|Benennung englisch;EN#Name EN|Name english;ES#Nombre ingles" },
            {"DescriptionSpanish","DE#Benennung ES|Benennung spanisch;EN#Name ES|Name spanish;ES#Nombre español" },
            {"DescriptionItalian","DE#Benennung IT|Benennung Italienisch;EN#Name IT|Name italian;ES#Nombre italiano" },
            {"DescriptionFrench","DE#Benennung FR|Benennung Französisch;EN#Name FR|Name French;ES#Nombre francés" },
            {"Manufacturer","DE#Hersteller;EN#Manufacturer; ES#Fabricante" },
            {"ManufacturerName","DE#Hers. Typ Bez.|Hersteller Typ Bezeichnung in SAP; EN#Manufacturer Id;ES#Fabricante Id" },
            {"ManufacturerPartNo","DE#Hers.Best. NR|Hersteller Bestellungsnummer in SAP; EN#Manufacturer Order Id;ES#" },
            {"MType","DE#Infos;EN#Infos;ES#Infos" },
            { "qty","DE#Menge;EN#Quantity;ES#Cantidad" },
            {"StationQuantity","DE#Stations Menge;EN#Quantity under Station;ES#Cantidad en la estación" },
            {"SparePartIndicator","DE#EVWPF|EVWPF-Kennung;EN#EVWPF|Spare Part Indicator;ES#EVWPF|Indicatodor pieza de respuesto" },
             {"TD_DE","DE#Tech. Details auf Deutsch;EN#Tech. Details in German;ES#Detalles Tecnicos en Alemán" },
             {"TD_EN","DE#Tech. Details auf Englisch;EN#Tech. Details in English;ES#Detalles Tecnicos en Inglés" },
            //{"MATUsageIndicator","DE#EL/ME|Elektrik oder Mekanik;EN#EL/ME|Electric or Mechanic;ES#EL/ME|Eléctrica o macánica" },

             {"MSTAE","DE#MSTAE|Material Status Standort übergreifend;EN#MSTAE|Material State for all ATMo plants;ES#MSTAE|Estado del material para todos los sitios de ATMO" },
            {"MSTAE_A1","DE#MSTAE_A1|Material Status in SAP für ATMO1;EN#MSTAE_A1|Material State in SAP for ATMO1;ES#MSTAE_A1" },
            { "MSTAE_A2","DE#MSTAE_A2|Material Status in SAP für ATMO2;EN#MSTAE_A2|Material State in SAP for ATMO2;ES#MSTAE_A2|Estado del Material en SAP para ATMO2" },
            {"MSTAE_A3","DE#MSTAE_A3|Material Status in SAP für ATMO3;EN#MSTAE_A3|Material State in SAP for ATMO3;ES#MSTAE_A3|Estado del Material en SAP para ATMO3" },
            {"MSTAE_A5","DE#MSTAE_A5|Material Status in SAP für ATMO5;EN#MSTAE_A5|Material State in SAP for ATMO5;ES#MSTAE_A5|Estado del Material en SAP para ATMO5" },
            {"MSTAE_A6","DE#MSTAE_A6|Material Status in SAP für ATMO6;EN#MSTAE_A6|Material State in SAP for ATMO6;ES#MSTAE_A6|Estado del Material en SAP para ATMO6" },
            {"MSTAE_A7","DE#MSTAE_A7|Material Status in SAP für ATMO7;EN#MSTAE_A7|Material State in SAP for ATMO7;ES#MSTAE_A7|Estado del Material en SAP para ATMO7" },
            {"MSTAE_A8","DE#MSTAE_A8|Material Status in SAP für ATMO8;EN#MSTAE_A8|Material State in SAP for ATMO8;ES#MSTAE_A8|Estado del Material en SAP para ATMO8" },
            {"MSTAE_MH","DE#MSTAE_MH|Material Status in SAP für Moehwald;EN#MSTAE_MH|Material State in SAP for Moehwald;ES#MSTAE_MH|Estado del Material en SAP para Moehwald" },

            {"MKLASSE","DE#MKLASSE|Material Klasse;EN#MKLASSE|Material Class;ES#MKLASSE" },

            //missing in Object
            {"MTART","DE#MTART|Material Art im SAP;EN#MTART|Material Type in SAP;ES#MTART|Tipo de Material en SAP" },
            { "Purchase","DE#Beschaffung;EN#;ES#" },
            { "isStandardBG","DE#STD;EN#STD;ES#STD" },
            { "BESCHAFF","DE#Beschaffung;EN#Purchase;ES#" },


            { "DocuCheck","DE#Dokuhaken|Für Dokumentation Relevant;EN#Docu Relevant|Relevant for Documentation;ES#" },
            {"STLStatus","DE#STL Status|Stücklistenstatus;EN#BOM-Status;ES#" },

            //{"Lib3D","DE#Lib 3D|3D-Modell in Inventor-Lib vorhanden;EN#;ES#" },
            //{"LUP","DE#LUP|Letze Update Datum;EN#LUP|Last Update;ES#LUP|Ultima actulizacion" },//LUP in Documents means Creation Date
            // {"ProjectPSPNr","DE#Projekt PSP Nr;EN#Project PSP Nr;ES#Nr de proyecto PSP Nr" },
            // {"PSPNr","DE#PSP Nr|PSP Nummer in SAP;EN#PSP Nr|PSP Number in SAP;ES#PSP Nr|Numero de PSP en SAP" },
            //{"UserState","DE#Status|Dokument Status;EN#State|Document State;ES#Estado|Estado del documento" }
            
            {"MEorEL","DE#ME/EL|ME, EL oder Standard;EN#ME/EL|ME/EL or Standard;ES#ME/EL|ME/EL o Estandar" },
             {"NEG","DE#NEG|Negativ Liste;EN#NEG|Negativ list State;ES#NEG|No relevante para documentacion" }
        };

        /// <summary>
        /// originated by Jorge
        /// </summary>
        public static Dictionary<string, string> dicDFC2Docucheck = new Dictionary<string, string>()
        {
            {"DocuCheckError","DE#Fehler;EN#Error;ES#Error" },
            {"DocID","DE#;EN#;ES#" },
            {"DFCLink","DE#DFC2 Link|Material in neue DFC2 Instanz öfnnen;EN#DFC2 Link|Open Material in a new DFC2 Instance;ES#DFC2 Link" },
            {"ProjectNr","DE#;EN#;ES#" },
            {"StationNr","DE#;EN#;ES#" },
            {"Level01","DE#;EN#;ES#" },
            {"Level02","DE#;EN#;ES#" },
            {"Level03","DE#;EN#;ES#" },
            {"Level04","DE#;EN#;ES#" },
            {"Level05","DE#;EN#;ES#" },
            {"Level06","DE#;EN#;ES#" },
            {"Level07","DE#;EN#;ES#" },
            {"Level08","DE#;EN#;ES#" },
            {"Level09","DE#;EN#;ES#" },
            {"Level10","DE#;EN#;ES#" },
            {"LastBG","DE#Mat Nr.Baugruppe;EN#Mat Nr. Assembly;ES#Mat Nr. Grupo" },
            {"BOM_ABT","DE#STL Abteilung;EN#BOM Department;ES#BOM Department" },
            {"BOM_ZZDRAWN","DE#STL Ersteller;EN#BOM Dseigner;ES#BOM Dseigner" },
            {"PartOrAssembly","DE#Baugruppe Name|Name der Teile oder Baugruppe wo es verbaut ist;EN#Assembly Name where it is assembled;ES#Nombre Grupo " },
            {"PartOrAssemblyEN","DE#Baugruppe Name EN|Name der Teile oder Baugruppe wo es verbaut ist;EN#Assembly Name EN|Assembly Name where it is assembled;ES#Nombre Grupo Ingles" },

            {"Pos","DE#Pos Nr.;EN#Pos;ES#Pos" },
            {"MatNr","DE#Mat Nr;EN#;ES#" },
            {"DrawingNr","DE#zugeh. Zng.|Zugehörige Zeichnung in SAP; EN#Drawing Nr|Related Drawing in SAP;ES#Nr de plano ralacionado en SAP" },
            {"DescriptionGerman","DE#Benennung DE|Benennung deutsch;EN#Name DE|Name German;ES#Nombre alemán" },
            {"DescriptionEnglish","DE#Benennung EN|Benennung englisch;EN#Name EN|Name english;ES#Nombre ingles" },
            {"DescriptionSpanish","DE#Benennung ES|Benennung spanisch;EN#Name ES|Name spanish;ES#Nombre español" },
            //{"Description Italian","DE#;EN#;ES#" },
            {"Manufacturer","DE#Hersteller;EN#Manufacturer; ES#Fabricante" },
            {"ManufacturerName","DE#Hers. Typ Bez.|Hersteller Typ Bezeichnung in SAP; EN#Manufacturer Id;ES#Fabricante Id" },
            {"ManufacturerPartNo","DE#Hers.Best. NR|Hersteller Bestellungsnummer in SAP; EN#Manufacturer Order Id;ES#" },
            {"MType","DE#Infos;EN#Infos;ES#Infos" },
            { "qty","DE#Menge;EN#Quantity;ES#Cantidad" },
            {"StationQuantity","DE#Stations Menge;EN#Quantity under Station;ES#Cantidad en la estación" },
            {"SparePartIndicator","DE#EVWPF|EVWPF-Kennung;EN#EVWPF|Spare Part Indicator;ES#EVWPF|Indicatodor pieza de respuesto" },
             {"TD_DE","DE#Tech. Details auf Deutsch;EN#Tech. Details in German;ES#Detalles Tecnicos en Alemán" },
             {"TD_EN","DE#Tech. Details auf Englisch;EN#Tech. Details in English;ES#Detalles Tecnicos en Inglés" },
            //{"MATUsageIndicator","DE#EL/ME|Elektrik oder Mekanik;EN#EL/ME|Electric or Mechanic;ES#EL/ME|Eléctrica o macánica" },

             {"MSTAE","DE#MSTAE|Material Status Standort übergreifend;EN#MSTAE|Material State for all ATMo plants;ES#MSTAE|Estado del material para todos los sitios de ATMO" },
            {"MSTAE_A1","DE#MSTAE_A1|Material Status in SAP für ATMO1;EN#MSTAE_A1|Material State in SAP for ATMO1;ES#MSTAE_A1" },
            { "MSTAE_A2","DE#MSTAE_A2|Material Status in SAP für ATMO2;EN#MSTAE_A2|Material State in SAP for ATMO2;ES#MSTAE_A2|Estado del Material en SAP para ATMO2" },
            {"MSTAE_A3","DE#MSTAE_A3|Material Status in SAP für ATMO3;EN#MSTAE_A3|Material State in SAP for ATMO3;ES#MSTAE_A3|Estado del Material en SAP para ATMO3" },
            {"MSTAE_A5","DE#MSTAE_A5|Material Status in SAP für ATMO5;EN#MSTAE_A5|Material State in SAP for ATMO5;ES#MSTAE_A5|Estado del Material en SAP para ATMO5" },
            {"MSTAE_A6","DE#MSTAE_A6|Material Status in SAP für ATMO6;EN#MSTAE_A6|Material State in SAP for ATMO6;ES#MSTAE_A6|Estado del Material en SAP para ATMO6" },
            {"MSTAE_A7","DE#MSTAE_A7|Material Status in SAP für ATMO7;EN#MSTAE_A7|Material State in SAP for ATMO7;ES#MSTAE_A7|Estado del Material en SAP para ATMO7" },
            {"MSTAE_A8","DE#MSTAE_A8|Material Status in SAP für ATMO8;EN#MSTAE_A8|Material State in SAP for ATMO8;ES#MSTAE_A8|Estado del Material en SAP para ATMO8" },
            {"MSTAE_MH","DE#MSTAE_MH|Material Status in SAP für Moehwald;EN#MSTAE_MH|Material State in SAP for Moehwald;ES#MSTAE_MH|Estado del Material en SAP para Moehwald" },

            {"MKLASSE","DE#MKLASSE|Material Klasse;EN#MKLASSE|Material Class;ES#MKLASSE" },

            //missing in Object
            {"MTART","DE#MTART|Material Art im SAP;EN#MTART|Material Type in SAP;ES#MTART|Tipo de Material en SAP" },
            { "Purchase","DE#Beschaffung;EN#;ES#" },
            { "isStandardBG","DE#STD;EN#STD;ES#STD" },
            { "BESCHAFF","DE#Beschaffung;EN#Purchase;ES#" },


            { "DocuCheck","DE#Dokuhaken|Für Dokumentation Relevant;EN#Docu Relevant|Relevant for Documentation;ES#" },
            {"STLStatus","DE#STL Status|Stücklistenstatus;EN#BOM-Status;ES#" },

            //{"Lib3D","DE#Lib 3D|3D-Modell in Inventor-Lib vorhanden;EN#;ES#" },
            //{"LUP","DE#LUP|Letze Update Datum;EN#LUP|Last Update;ES#LUP|Ultima actulizacion" },//LUP in Documents means Creation Date
            // {"ProjectPSPNr","DE#Projekt PSP Nr;EN#Project PSP Nr;ES#Nr de proyecto PSP Nr" },
            // {"PSPNr","DE#PSP Nr|PSP Nummer in SAP;EN#PSP Nr|PSP Number in SAP;ES#PSP Nr|Numero de PSP en SAP" },
            //{"UserState","DE#Status|Dokument Status;EN#State|Document State;ES#Estado|Estado del documento" }
            
            {"MEorEL","DE#ME/EL|ME, EL oder Standard;EN#ME/EL|ME/EL or Standard;ES#ME/EL|ME/EL o Estandar" },
             {"NEG","DE#NEG|Negativ Liste;EN#NEG|Negativ list State;ES#NEG|No relevante para documentacion" }
        };

        /// <summary>
        /// originated by Jorge
        /// </summary>
        public static Dictionary<string, string> dicDFC2BOMXML = new Dictionary<string, string>()
        {
            {"ID","DE#Fehler;EN#Error;ES#Error" },
            {"ParentID","DE#Fehler;EN#Error;ES#Error" },
            {"DocID","DE#;EN#;ES#" },
            //{"DFCLink","DE#DFC2 Link|Material in neue DFC2 Instanz öfnnen;EN#DFC2 Link|Open Material in a new DFC2 Instance;ES#DFC2 Link" },
            {"ProjectNr","DE#;EN#;ES#" },
            {"StationNr","DE#;EN#;ES#" },
            //{"Level01","DE#;EN#;ES#" },
            //{"Level02","DE#;EN#;ES#" },
            //{"Level03","DE#;EN#;ES#" },
            //{"Level04","DE#;EN#;ES#" },
            //{"Level05","DE#;EN#;ES#" },
            //{"Level06","DE#;EN#;ES#" },
            //{"Level07","DE#;EN#;ES#" },
            //{"Level08","DE#;EN#;ES#" },
            //{"Level09","DE#;EN#;ES#" },
            //{"Level10","DE#;EN#;ES#" },
            {"LastBG","DE#Mat Nr.Baugruppe;EN#Mat Nr. Assembly;ES#Mat Nr. Grupo" },
            {"BOM_ABT","DE#STL Abteilung;EN#BOM Department;ES#BOM Department" },
            {"BOM_ZZDRAWN","DE#STL Ersteller;EN#BOM Dseigner;ES#BOM Dseigner" },
            {"PartOrAssembly","DE#Baugruppe Name|Name der Teile oder Baugruppe wo es verbaut ist;EN#Assembly Name where it is assembled;ES#Nombre Grupo " },
            //{"PartOrAssemblyEN","DE#Baugruppe Name EN|Name der Teile oder Baugruppe wo es verbaut ist;EN#Assembly Name EN|Assembly Name where it is assembled;ES#Nombre Grupo Ingles" },

            {"Pos","DE#Pos Nr.;EN#Pos;ES#Pos" },
            {"MatNr","DE#Mat Nr;EN#;ES#" },
            {"DrawingNr","DE#zugeh. Zng.|Zugehörige Zeichnung in SAP; EN#Drawing Nr|Related Drawing in SAP;ES#Nr de plano ralacionado en SAP" },
            {"DescriptionGerman","DE#Benennung DE|Benennung deutsch;EN#Name DE|Name German;ES#Nombre alemán" },
            {"DescriptionEnglish","DE#Benennung EN|Benennung englisch;EN#Name EN|Name english;ES#Nombre ingles" },
            {"DescriptionSpanish","DE#Benennung ES|Benennung spanisch;EN#Name ES|Name spanish;ES#Nombre español" },
            //{"Description Italian","DE#;EN#;ES#" },
            {"Manufacturer","DE#Hersteller;EN#Manufacturer; ES#Fabricante" },
            {"ManufacturerName","DE#Hers. Typ Bez.|Hersteller Typ Bezeichnung in SAP; EN#Manufacturer Id;ES#Fabricante Id" },
            {"ManufacturerPartNo","DE#Hers.Best. NR|Hersteller Bestellungsnummer in SAP; EN#Manufacturer Order Id;ES#" },
            {"MType","DE#Infos;EN#Infos;ES#Infos" },
            { "qty","DE#Menge;EN#Quantity;ES#Cantidad" },
            {"StationQuantity","DE#Stations Menge;EN#Quantity under Station;ES#Cantidad en la estación" },
            {"SparePartIndicator","DE#EVWPF|EVWPF-Kennung;EN#EVWPF|Spare Part Indicator;ES#EVWPF|Indicatodor pieza de respuesto" },
             {"TD_DE","DE#Tech. Details auf Deutsch;EN#Tech. Details in German;ES#Detalles Tecnicos en Alemán" },
             {"TD_EN","DE#Tech. Details auf Englisch;EN#Tech. Details in English;ES#Detalles Tecnicos en Inglés" },
            //{"MATUsageIndicator","DE#EL/ME|Elektrik oder Mekanik;EN#EL/ME|Electric or Mechanic;ES#EL/ME|Eléctrica o macánica" },

             {"MSTAE","DE#MSTAE|Material Status Standort übergreifend;EN#MSTAE|Material State for all ATMo plants;ES#MSTAE|Estado del material para todos los sitios de ATMO" },
            {"MSTAE_A1","DE#MSTAE_A1|Material Status in SAP für ATMO1;EN#MSTAE_A1|Material State in SAP for ATMO1;ES#MSTAE_A1" },
            { "MSTAE_A2","DE#MSTAE_A2|Material Status in SAP für ATMO2;EN#MSTAE_A2|Material State in SAP for ATMO2;ES#MSTAE_A2|Estado del Material en SAP para ATMO2" },
            {"MSTAE_A3","DE#MSTAE_A3|Material Status in SAP für ATMO3;EN#MSTAE_A3|Material State in SAP for ATMO3;ES#MSTAE_A3|Estado del Material en SAP para ATMO3" },
            {"MSTAE_A5","DE#MSTAE_A5|Material Status in SAP für ATMO5;EN#MSTAE_A5|Material State in SAP for ATMO5;ES#MSTAE_A5|Estado del Material en SAP para ATMO5" },
            {"MSTAE_A6","DE#MSTAE_A6|Material Status in SAP für ATMO6;EN#MSTAE_A6|Material State in SAP for ATMO6;ES#MSTAE_A6|Estado del Material en SAP para ATMO6" },
            {"MSTAE_A7","DE#MSTAE_A7|Material Status in SAP für ATMO7;EN#MSTAE_A7|Material State in SAP for ATMO7;ES#MSTAE_A7|Estado del Material en SAP para ATMO7" },
            {"MSTAE_A8","DE#MSTAE_A8|Material Status in SAP für ATMO8;EN#MSTAE_A8|Material State in SAP for ATMO8;ES#MSTAE_A8|Estado del Material en SAP para ATMO8" },
            {"MSTAE_MH","DE#MSTAE_MH|Material Status in SAP für Moehwald;EN#MSTAE_MH|Material State in SAP for Moehwald;ES#MSTAE_MH|Estado del Material en SAP para Moehwald" },

            {"MKLASSE","DE#MKLASSE|Material Klasse;EN#MKLASSE|Material Class;ES#MKLASSE" },

            //missing in Object
            {"MTART","DE#MTART|Material Art im SAP;EN#MTART|Material Type in SAP;ES#MTART|Tipo de Material en SAP" },
            { "Purchase","DE#Beschaffung;EN#;ES#" },
            { "isStandardBG","DE#STD;EN#STD;ES#STD" },
            { "BESCHAFF","DE#Beschaffung;EN#Purchase;ES#" },


            { "DocuCheck","DE#Dokuhaken|Für Dokumentation Relevant;EN#Docu Relevant|Relevant for Documentation;ES#" },
            {"STLStatus","DE#STL Status|Stücklistenstatus;EN#BOM-Status;ES#" },

            //{"Lib3D","DE#Lib 3D|3D-Modell in Inventor-Lib vorhanden;EN#;ES#" },
            //{"LUP","DE#LUP|Letze Update Datum;EN#LUP|Last Update;ES#LUP|Ultima actulizacion" },//LUP in Documents means Creation Date
            // {"ProjectPSPNr","DE#Projekt PSP Nr;EN#Project PSP Nr;ES#Nr de proyecto PSP Nr" },
            // {"PSPNr","DE#PSP Nr|PSP Nummer in SAP;EN#PSP Nr|PSP Number in SAP;ES#PSP Nr|Numero de PSP en SAP" },
            //{"UserState","DE#Status|Dokument Status;EN#State|Document State;ES#Estado|Estado del documento" }
            
            {"MEorEL","DE#ME/EL|ME, EL oder Standard;EN#ME/EL|ME/EL or Standard;ES#ME/EL|ME/EL o Estandar" },
             {"NEG","DE#NEG|Negativ Liste;EN#NEG|Negativ list State;ES#NEG|No relevante para documentacion" }
        };

        /// <summary>
        /// originated by Jorge
        /// </summary>
        static private Dictionary<string, string> m_SAPEndPoints_ZFEHLTEILE = new Dictionary<string, string>()
        {
			//{"P",       "http://rb3pd486.server.bosch.com:8050/sap/bc/srt/wsdl/flv_10002A111AD1/bndg_url/sap/bc/srt/xip/sap/zmissingpartsgetdetails_in/011/missingparts/externalservice?sap-client=011"},
			{"P",     "http://rb3p48a0.server.bosch.com:8050/sap/bc/srt/xip/sap/zmissingpartsgetdetails_in/011/missingparts/externalservice" },
            {"Q",       "http://rb3q48a0.server.bosch.com:8063/sap/bc/srt/pm/sap/zmissingpartsgetdetails_in/011/central/test_http_basic/1/binding_t_http_a_http_zmissingpartsgetdetails_in_test_http_basic_c" },
            {"D",       "http://rb3d48a0.server.bosch.com:8061/sap/bc/srt/xip/sap/zmissingpartsgetdetails_in/011/d48_011_missingparts/http_basic"},
            {"Dnew",    "http://rb3d48a0.server.bosch.com:8061/sap/bc/srt/wsdl/flv_10002A111AD1/srvc_url/sap/bc/srt/xip/sap/zmissingpartsgetdetails_in/011/d48_011_missingparts/http_basic?sap-client=011"},
        };

        /// <summary>
        /// originated by Jorge
        /// </summary>
        static private Dictionary<string, string> m_SAPEndPoints_ZBOMDOWN = new Dictionary<string, string>()
        {
            {"P",       "http://rb3p48a0.server.bosch.com:8050/sap/bc/srt/xip/sap/zgetorderbomdetails_in/011/p48_011_getorderbomd/http_basic" },
            {"Q",       "http://rb3q48a0.server.bosch.com:8063/sap/bc/srt/xip/sap/zgetorderbomdetails_in/011/q48_011_getorderbomd/http_basic" },
            {"D",       ""},
        };

        /// <summary>
        /// originated by Jorge
        /// </summary>
        static private Dictionary<string, string> m_SAPEndPoints_MaterialMaster = new Dictionary<string, string>()
        {
            {"P",       "http://rb3p48a0.server.bosch.com:8050/sap/bc/srt/xip/sap/zmaterialmastergetdetails_in/011/p48_011_materialmast/http_basic" },
            {"Q",       "http://rb3q48a0.server.bosch.com:8063/sap/bc/srt/xip/sap/zmaterialmastergetdetails_in/011/q48_011_materialmast/http_basic" },
            {"D",       ""},
        };

        /// <summary>
        /// originated by Jorge
        /// </summary>
        static private Dictionary<string, string> m_SAPEndPoints_Z48PS_TRACK = new Dictionary<string, string>()
        {
            {"P",       "http://rb3p48a0.server.bosch.com:8050/sap/bc/srt/xip/sap/zprojecttrackgetdetails_in/011/p48_011_projecttrack/http_basic" },
            {"Q",       "http://rb3q48a0.server.bosch.com:8063/sap/bc/srt/xip/sap/zprojecttrackgetdetails_in/011/q48_011_z48ps_track_project/z48ps_track_project" },
            {"D",       ""},
        };

        /// <summary>
        /// originated by Jorge
        /// </summary>
        public enum SAP_EndPoint
        {
            ZFEHLTEILE,
            ZBOM_DOWN,
            Z48PS_TRACK,
            MaterialMaster
        };

        /// <summary>
        /// originated by Jorge
        /// </summary>
        /// <param name="serverIP"></param>
        /// <param name="SAPWS"></param>
        /// <returns></returns>
        public static string GetSAPEndPoint(string serverIP, SAP_EndPoint SAPWS)
        {
            string EndPoint = "";
            if (SAPWS == SAP_EndPoint.ZFEHLTEILE)
            {
                if (!m_SAPEndPoints_ZFEHLTEILE.ContainsKey(serverIP))
                    return "";
                EndPoint = m_SAPEndPoints_ZFEHLTEILE[serverIP];
            }
            else if (SAPWS == SAP_EndPoint.ZBOM_DOWN)
            {
                if (!m_SAPEndPoints_ZBOMDOWN.ContainsKey(serverIP))
                    return "";
                EndPoint = m_SAPEndPoints_ZBOMDOWN[serverIP];
            }
            else if (SAPWS == SAP_EndPoint.Z48PS_TRACK)
            {
                if (!m_SAPEndPoints_Z48PS_TRACK.ContainsKey(serverIP))
                    return "";
                EndPoint = m_SAPEndPoints_Z48PS_TRACK[serverIP];
            }
            else if (SAPWS == SAP_EndPoint.MaterialMaster)
            {
                if (!m_SAPEndPoints_MaterialMaster.ContainsKey(serverIP))
                    return "";
                EndPoint = m_SAPEndPoints_MaterialMaster[serverIP];
            }
            return EndPoint;
        }

        /// <summary>
        /// originated by Jorge
        /// erlaubte Materialnummern für Profile 30x30
        /// </summary>
        public static List<string> lAllowedMatNr30x30 = new List<string>()
        {
            {"3842990720"},            {"3842990721"},            {"3842990723"},            {"3842990724"},
            {"3842990722"},            {"3842990725"},            {"3842990726"},            {"3842992965"}
        };

        /// <summary>
        /// originated by Jorge
        /// erlaubte Materialnummern für Profile 45x45
        /// </summary>
        public static List<string> lAllowedMatNr45x45 = new List<string>()
        {
            {"3842990520"},            {"3842990517"},            {"3842990518"},            {"3842990519"},
            {"3842990640"},            {"3842990521"},            {"3842990642"},            {"3842990648"},
            {"3842990644"},            {"3842990646"},            {"3842992969"}
        };

        /// <summary>
        /// originated by Jorge
        /// erlaubte Materialnummern für Profile 45x45L
        /// </summary>
        public static List<string> lAllowedMatNr45x45L = new List<string>()
        {
            {"3842992425"},            {"3842992426"},            {"3842992427"},            {"3842992960"},
            {"3842992953"},            {"3842992954"},            {"3842992956"},            {"3842992967"}
        };

        /// <summary>
        /// originated by Jorge
        /// erlaubte Materialnummern für Profile 45x60
        /// </summary>
        public static List<string> lAllowedMatNr45x60 = new List<string>()
        {
            {"3842990570"},            {"3842990571"},            {"3842990572"},            {"3842990575"},
            {"3842990573"},            {"3842990594"},            {"3842990584"},            {"3842993085"},
            {"3842992376"},            {"3842992375"},            {"3842990688"},            {"3842990672"},
            {"3842990690"},            {"3842990670"},            {"3842990674"}
        };

        /// <summary>
        /// originated by Jorge
        /// erlaubte Materialnummern für Profile 45x90
        /// </summary>
        public static List<string> lAllowedMatNr45x90 = new List<string>()
        {
            {"3842990300"},            {"3842990301"},            {"3842990302"},            {"3842990323"},
            {"3842990305"},            {"3842990303"},            {"3842990304"},            {"3842990325"},
            {"3842990307"},            {"3842990329"},            {"3842990313"},            {"3842990331"},
            {"3842990309"},            {"3842990311"},
        };

        /// <summary>
        /// originated by Jorge
        /// erlaubte Materialnummern für Profile 90x90
        /// </summary>
        public static List<string> lAllowedMatNr90x90 = new List<string>()
        {
            {"3842990500"},            {"3842990501"},            {"3842990502"},            {"3842992961"},
            {"3842992373"},            {"3842993083"},            {"3842990505"},            {"3842990507"},
            {"3842990092"},            {"3842990082"},            {"3842990093"},            {"3842990094"}
        };
        /// <summary>
        /// REferenced in DOCINFO als "STORAGE"
        /// defined in \...\project\config\settings\g_class.oel
        /// </summary>
        public static Dictionary<string, string> dicDocTypes = new Dictionary<string, string>
        {
            {"0","none"},
            {"1","gif"},
            {"3","tif"},
            {"4","ps"},
			#region more doc types
			{"5","ascii"},
            {"7","mail"},
            {"9","folder"},
            {"10","pcl"},
            {"12","imageform"},
            {"13","maskform"},
            {"14","jpg"},
            {"22","versionfolder"},
            {"30","pdf"},
            {"32","bmp"},
            {"38","ww80doc"},
            {"39","excel80"},
            {"40","layer"},
            {"43","excel80andtif"},
            {"44","ww80andtif"},
            {"45","oel"},
            {"47","exprfolder"},
            {"48","ppt80"},
            {"53","gzip"},
            {"55","asciiform"},
            {"63","excel80andpdf"},
            {"77","unknown"},
            {"79","guide"},
            {"81","html"},
            {"82","rtf"},
            {"84","url"},
            {"88","coldascii"},
            {"90","form"},
            {"101","acad14"},
            {"102","dwf"},
            {"103","acad14anddwf"},
            {"130","wwdocx"},
            {"132","xlsx"},
            {"134","pptx"},
            {"1001","pdfmi"},
            {"1002","dwg"},
            {"1003","pdfmidwg"},
            {"1004","pdfdwg"},
            {"1006","mi"},
            {"1007","modini"},
            {"1008","ini"},
            {"1009","mod"},
            {"1010","eplan"},
            {"1011","pdfeplan"},
            {"1012","pdfdwf"},
            {"1013","eplan_zw1"},
            {"1014","pdfeplanzw"},
            {"1101","acad15"},
            {"1103","acad15anddwf"},
			#endregion
			{"2001","acad15_pdf"},
           {"5001","png"}
        };

        /// <summary>
        /// mko, 1.10.2018
        /// Streng typisierte Form von dicDocTypes
        /// </summary>
        public enum StorageType
        {
            none = 0,
            gif = 1,
            tif = 3,
            ps = 4,
            #region more doc types
            ascii = 5,
            mail = 7,
            folder = 9,
            pcl = 10,
            imageform = 12,
            maskform = 13,
            jpg = 14,
            versionfolder = 22,
            pdf = 30,
            bmp = 32,
            ww80doc = 38,
            excel80 = 39,
            layer = 40,
            excel80andtif = 43,
            ww80andtif = 44,
            oel = 45,
            exprfolder = 47,
            ppt80 = 48,
            gzip = 53,
            asciiform = 55,
            excel80andpdf = 63,
            unknown = 77,
            guide = 79,
            html = 81,
            rtf = 82,
            url = 84,
            coldascii = 88,
            form = 90,
            acad14 = 101,
            dwf = 102,
            acad14anddwf = 103,
            wwdocx = 130,
            xlsx = 132,
            pptx = 134,
            pdfmi = 1001,
            dwg = 1002,
            pdfmidwg = 1003,
            pdfdwg = 1004,
            mi = 1006,
            modini = 1007,
            ini = 1008,
            mod = 1009,
            eplan = 1010,
            pdfeplan = 1011,
            pdfdwf = 1012,
            eplan_zw1 = 1013,
            pdfeplanzw = 1014,
            acad15 = 1101,
            acad15anddwf = 1103,

            // mko, 7.11.2018
            // Unter Materialnummer 0804CL0211 wurde eine ATB gefunden, die abweichend von der Norm diesen
            // Storagetype besitzt. Um Fehler zu vermeiden, erfolgte hier die Aufnahme. Die von diesem 
            // Storagetype unterstützten Dateiformate 
            atb_pdf = 2000,

            acad15_pdf = 2001,
            png = 5001,

            // mko, 28.01.21
            // Integration der neuen cts- Fileformate modx und ini x
            modinix = 2107,


        }


        public static Dictionary<string, string> dicDocTypesApplication = new Dictionary<string, string>
        {
            {"gif","Default"},
            {"tif","Default"},
            {"ps","Default"},
            {"ascii","Default"},
            {"mail","Default"},
            {"folder","Default"},
            {"pcl","Default"},
            {"imageform","Default"},
            {"maskform","Default"},
            {"jpg","Default"},
            {"versionfolder","Default"},
            {"pdf","Default"},
            {"bmp","Default"},
            {"ww80doc","Default"},
            {"excel80","Default"},
            {"layer","Default"},
            {"excel80andtif","Default"},
            {"ww80andtif","Default"},
            {"oel","Default"},
            {"exprfolder","Default"},
            {"ppt80","Default"},
            {"gzip","zip"},
            {"asciiform","Default"},
            {"excel80andpdf","xls"},
            {"unknown","Default"},
            {"guide","Default"},
            {"html","Default"},
            {"rtf","Default"},
            {"url","Default"},
            {"coldascii","Default"},
            {"form","Default"},
            {"acad14","Default"},
            {"dwf","Default"},
            {"acad14anddwf","dwf"},
            {"pdfmi","pdf"},
            {"dwg","dwg"},
            {"pdfmidwg","midwg"},
            {"pdfdwg","dwg"},
            {"mi","Default"},
            {"modini","Default"},
            {"ini","Default"},
            {"mod","Default"},
            {"eplan","Default"},
            {"pdfeplan","Default"},
            {"pdfdwf","dwf"},
            {"eplan_zw1","notallowed"},
            {"pdfeplanzw","pdf"},
            {"acad15","Default"},
            {"acad15anddwf","dwf"},
			#endregion
			{"acad15_pdf","pdf"}
        };

        public static string DocTypesApplication(string FileExtension)
        {
            string AssociatedExtension = "";
            if (!dicDocTypesApplication.ContainsKey(FileExtension))
                return "notallowed";
            AssociatedExtension = dicDocTypesApplication[FileExtension];
            return AssociatedExtension;
        }

        public static Dictionary<string, string> dicUsrPW = new Dictionary<string, string>
        {
            {"pz61fe","RW5zaW5nZXI=" },
        };
        public static Dictionary<string, string> dicATMOPlants = new Dictionary<string, string>()
       {
            {"1060","A1"},
            {"9651","A2"},
            {"6755","A3"},
            {"7910","A4"},
            {"2576","A5"},
            {"603B","A6"},
            {"9046","A7"},
            {"9395","A8"},
            {"6740","MH"},
       };

        public static string GetLocationPlantName(string valuePlant = "", string valueATMOSite = "")
        {
            string ATMOxyz = "";
            if (valuePlant != "")
            {
                if (dicATMOPlants.ContainsKey(valuePlant))
                    ATMOxyz = dicATMOPlants[valuePlant];
                return ATMOxyz;
            }
            if (valueATMOSite != "")
            {
                if (valueATMOSite.Contains("1"))//ok
                    return "1060";
                if (valueATMOSite.Contains("2"))//ok
                    return "9651";
                if (valueATMOSite.Contains("3"))//OK
                    return "6755";
                if (valueATMOSite.Contains("4"))//ok
                    return "7910";
                if (valueATMOSite.Contains("5"))//ok
                    return "2576";
                if (valueATMOSite.Contains("6"))//ok
                    return "603B";
                if (valueATMOSite.Contains("7"))//ok
                    return "9046";
                if (valueATMOSite.Contains("8"))//ok
                    return "9395";
                if (valueATMOSite.Contains("MH"))//ok
                    return "6740";

            }
            return "";
        }
        public static string GetPWFromUser(string user)
        {
            string PW = "";
            switch (user.ToLower())
            {
                case "pz61fe":
                    PW = Base64ToString(dicUsrPW["pz61fe"]) + "03";
                    break;
                case GlobalDictionaries.DZAServiceUser:
                    PW = Base64ToString("QTFhZG1pbmR6YXByb2Q=");
                    break;
                default:
                    break;
            }
            return PW;
        }
        public static string Base64ToString(string strB64)
        {
            byte[] data = Convert.FromBase64String(strB64);
            string decodedString = Encoding.UTF8.GetString(data);
            return decodedString;
        }

        public const string DZAServiceUser = "DE\\dza1fe";

        private static SecureString ToSecureString(SecureString Result, string Source)
        {
            if (string.IsNullOrWhiteSpace(Source))
                return null;
            else
            {
                foreach (char c in Source.ToCharArray())
                    Result.AppendChar(c);
                return Result;
            }
        }
        public static String SecureStringToString(SecureString value)
        {
            IntPtr valuePtr = IntPtr.Zero;
            try
            {
                valuePtr = Marshal.SecureStringToGlobalAllocUnicode(value);
                return Marshal.PtrToStringUni(valuePtr);
            }
            finally
            {
                Marshal.ZeroFreeGlobalAllocUnicode(valuePtr);
            }
        }
        private const string pw1 = "QTFhZG1pbm";//"YWRtaW5kem";
        public static SecureString GetPW(string user)
        {
            System.Security.SecureString spw = new SecureString();

            if (DZAServiceUser.Contains(user))
            {
                spw = ToSecureString(spw, pw1);
                spw = ToSecureString(spw, "R6YXByb2Q=");// Fwcm9kYTE=");
                spw.MakeReadOnly();
            }
            return spw;

        }
        public static DocTypeSAP GetDocTypeSAPFromText(string text)
        {
            DocTypeSAP dType = DocTypeSAP.Unknown;
            try
            {
                dType = (DocTypeSAP)Enum.Parse(typeof(DocTypeSAP), text);
            }
            catch (Exception)
            {
            }
            return dType;
        }



        public enum DocTypeSAP
        {

            /// <summary>
            /// SAP Part List (BOM) document as PDF            
            /// </summary>
            ATB,//Part List


            /// <summary>
            /// Drawing in 2D 
            /// </summary>
            ATD,


            /// <summary>
            /// Refecrence to a Drawing in another project
            /// </summary>
            ATZ,//2d or 3D related Drawing


            PMproj_No_Exist_WithStation,
            Unknown,
            BauGruppe,
            Einzelteile,

            STL,

            Station,

            /// <summary>
            /// Eplan document
            /// </summary>
            ECA, //EPLAN Document

            /// <summary>
            /// Technical Documentation
            /// A TDP is a document with technical information of a part in context of a project.
            /// Therefore a TDP is assined to a project or project/station
            /// </summary>
            TDP,

            /// <summary>
            /// Manual
            /// </summary>
            MAN,

            /// <summary>
            /// Catalogue part
            /// </summary>
            CAT,

            /// <summary>
            /// Offer
            /// </summary>
            ATO,

            /// <summary>
            /// Drawing in 3D
            /// </summary>
            AT3,

            /// <summary>
            /// Cycle Time Sequenzer
            /// </summary>
            CTS,

            /// <summary>
            /// Engineering design changes
            /// </summary>
            EDC,

            /// <summary>
            /// Shop floor changes
            /// </summary>
            SFC, //Shop Floor Change-->pdf

            Baseline,


            ExtensionProjects,
            ProjecAccess,
            ProjectTickets,

            /// <summary>
            /// Bom translated to turkey language
            /// </summary>
            ATB_TR,

            /// <summary>
            /// Referenz to a Drawing in another project.
            /// </summary>
            ATDATZ,

            /// <summary>
            /// Dokumente aus dem TEF- Raster
            /// 
            /// mko, 29.3.2019
            /// Umbenannt von TEF in TER, da in der DFC-DB Tabelle Path in der Spalte XTYPE, welche die SAP Typen den 
            /// Dokumenten zuordnet, anstatt TEF TER steht. Der Unterschied in der Bezeichnung würde Abfragen unnötig 
            /// verkomplizieren -> Umbennenung in TER
            /// </summary>
            TER,

            CharacteristicValues,


            ORD,

            ProductOwner,
            ProjManager,
            InStep,
            InStepInfo,
            OPL,
            DocuCheck,

            // mko, 12.4.2018
            // To express Permissions like Export
            All,

            /// <summary>
            /// mko, 23.9.2019
            /// Steht für undefinierten SAP- Typ
            /// </summary>
            none,

            /// <summary>
            /// mko, 2.9.2020
            /// Steht für ein SAP- Projekt
            /// </summary>
            Projekt,

            /// <summary>
            /// mko, 2.9.2020
            /// Prozessmodul
            /// </summary>
            FlexCon,
        };

        /// <summary>
        /// mko, 14.6.2018
        /// Umbau des Statuswechsels: Alle Statuswechselanträge werden in STC_A1 abgelegt
        /// </summary>
        public static Dictionary<string, string> dicDZAPaths = new Dictionary<string, string>()
       {
            {"DFC_StatusChange_Path",@"\\Fe24983.de.bosch.com\atmo_mcad_files$\DZA_IN_ATMO\STC_A1\" },
            {"DFC_StatusChange_Path_ATMO1",@"\\Fe24983.de.bosch.com\atmo_mcad_files$\DZA_IN_ATMO\STC_A1\" },

            //{"DFC_StatusChange_Path_ATMO2",@"\\Fe24983.de.bosch.com\atmo_mcad_files$\DZA_IN_ATMO\STC_A2\" },
            {"DFC_StatusChange_Path_ATMO2",@"\\Fe24983.de.bosch.com\atmo_mcad_files$\DZA_IN_ATMO\STC_A1\" },

			//{"DFC_StatusChange_Path_ATMO3",@"\\szh2fs01.apac.bosch.com\bosch$\DZA\dza_folder\STC_A3\" },
            //{"DFC_StatusChange_Path_ATMO3",@"\\bosch.com\dfsrb\DfsCN\loc\szh\Public\L\DZA\dza_folder\STC_A3\" },
            {"DFC_StatusChange_Path_ATMO3",@"\\Fe24983.de.bosch.com\atmo_mcad_files$\DZA_IN_ATMO\STC_A1\" },

            //{"DFC_StatusChange_Path_ATMO8",@"\\bu0vm004.emea.bosch.com\gdm$\STC_A8\" },
            {"DFC_StatusChange_Path_ATMO8",@"\\Fe24983.de.bosch.com\atmo_mcad_files$\DZA_IN_ATMO\STC_A1\" },

            //{"DFC_StatusChange_Path_MH",@"\\Fe24983.de.bosch.com\atmo_mcad_files$\DZA_IN_ATMO\STC_MH"},//\\ho9-projekte\dzamh\DZA-Import\STC\" },
            {"DFC_StatusChange_Path_MH",@"\\Fe24983.de.bosch.com\atmo_mcad_files$\DZA_IN_ATMO\STC_A1\" },

            {"DFC_ShopfloorChange_Path",@"\\Fe24983.de.bosch.com\atmo_mcad_files$\DZA_IN_ATMO\SFC\"},
            {"DFC_EDC_Path",@"\\Fe24983.de.bosch.com\atmo_mcad_files$\DZA_IN_ATMO\EDC\"},
            {"DFC_CTS_Path",@"\\fe24983.de.bosch.com\atmo_mcad_files$\dza\CTS\"},
            {"DFC_ORD_Path",@"\\FE24983.de.bosch.com\atmo_mcad_files$\DZA_IN_ATMO\ORD\"},
            {"DZAEditClass",@"\\FE24983.de.bosch.com\atmo_mcad_files$\DZA_IN_ATMO\EDIT\"},
            {"DFC_TDP_Path_BigFiles",@"\\fe0vmc0288.de.bosch.com\GDM_TransferFolder$\DFC2BigFiles"},

            // mko, 20.12.2018
            // Nach Anweisung von Joachim für den TDP- Upload geändert
            //{"DFC_TDP_Path",@"\\Fe24983.de.bosch.com\atmo_mcad_files$\dza\Techdok\"},
            {"DFC_TDP_Path", @"\\Fe24983.de.bosch.com\atmo_mcad_files$\dza\TDP" },
            {"DFC_ProjectRelease_Path",@"\\bosch.com\dfsrb\DfsDE\DIV\PA\ATMO1\alle\DZA-EDL\ZVTOOL\DZA\P48\"},
            {"DFC_DokuLightUpload_Path",@"\\Fe24983.de.bosch.com\atmo_mcad_files$\dza\Scannen\"},

            // mko, 10.10.2018
            //{"DZA_Drawings",@"\\FE24983.de.bosch.com\atmo_mcad_files$\dza\Inventor"},
            //{"DZA_Drawings",@"\\FE24983.de.bosch.com\atmo_mcad_files$\dza\Scannen"},
            // mko, 12.5.2020
            // Neuer Pfad nach Serverumzug.
            {"DZA_Drawings",@"\\si0vmc2551.de.bosch.com\gdm_transferfolder$\A0_ATD_DFC_UPLOAD"},

            { "DFC_Images", @"\\fe0vmc0288.de.bosch.com\gdm_public$\DocuToolForCustomer\Images\"},
            {"DFC_ZSTLDRUCK2SAP",@"\\fe02fs70.de.bosch.com\P48$\r3INP\Zstl"},
            {"MAT4_GDM31DokuMat",@"\\fe24983.de.bosch.com\atmo_mcad_files$\dza\GDM\GDM_31_DOKUTAB_MAN\Error\CurrentErrorLog.txt" },
            {"MAT_ORD_Path",@"\\fe0vm812.de.bosch.com\zeichnungs_transfer$\Bestellfreigaben"},
            {"KATAReleasedXML",@"\\fe24983.de.bosch.com\atmo_mcad_files$\dza\DFC\KATA_Released.xml"},
            {"KATAInvalidXML",@"\\fe24983.de.bosch.com\atmo_mcad_files$\dza\DFC\KATA_Invalid.xml"},
            {"InStepOutPut",@"\\FE24983\atmo_mcad_files$\dza\GDM\GDM_04_ProjectList\Projectdata_from_inStep_to_GDM04.csv" },
            {"SAP_ProjectList",@"\\FE24983\atmo_mcad_files$\dza\GDM\GDM_04_ProjectList\ProjectList.TXT" },
            {"SSF_Path",@"\\bosch.com\dfsrb\DfsCN\loc\szh\Department\RBAC-ATMO\ATMO3-USER\TRANSFER\00_Project_Transfer\Smart shopfloor with BPS @ATMO3\Released SSF\SSF" }

       };
        public static string GetDZAImportPath(string value)
        {
            if (dicDZAPaths.ContainsKey(value))
                return dicDZAPaths[value];
            return "";
        }
        public static Dictionary<string, string> dicCredentials = new Dictionary<string, string>()
       {
            {"ATMO1","DE#ZHphMWZl#QTFhZG1pbmR6YXByb2Q=" },                                                                             //Connection für ATMO1}
			{"ATMO2","EMEA#ZHphMm1k#QTJhZG1pbmR6YXByb2Q="  },                                                                          //Connection für ATMO2
			{"ATMO3","APAC#ZHphM3N6aA==#QTNhZG1pbmR6YXByb2Q="  },                                                                         //Connection für ATMO3
			{"ATMO8","EMEA#ZHphOGJ1#QThhZG1pbmR6YXByb2Q="  },                                                                          //Connection für ATMO8
			{"MH","DE#TUg=#YWRtaW5kemFwcm9kbWg="   },                                                                             //Connection für MH
			{"Unknown","DE#ZHphMWZl#admindzaproda1"   }                                                                             //Connection für MH
	   };

        public static string GetLocationCredentials(string value)
        {
            if (value.Contains("1"))
                return dicCredentials["ATMO1"];
            if (value.Contains("2"))
                return dicCredentials["ATMO2"];
            if (value.Contains("3"))
                return dicCredentials["ATMO3"];
            if (value.Contains("8"))
                return dicCredentials["ATMO8"];
            if (value.Contains("MH"))
                return dicCredentials["MH"];
            else
                return dicCredentials["Unknown"];
        }

        //public BaseLocation docBaseLocation = BaseLocation.Unknown;
        public enum BaseLocation
        {
            ATMO1,
            ATMO2,
            ATMO3,
            ATMO4,
            ATMO5,
            ATMO6,
            ATMO7,
            ATMO8,
            MH,
            Unknown
        };

        public static BaseLocation GetLocationFromString(string value)
        {
            if (value.Contains("1"))
                return BaseLocation.ATMO1;
            if (value.Contains("2"))
                return BaseLocation.ATMO2;
            if (value.Contains("3"))
                return BaseLocation.ATMO3;
            if (value.Contains("4"))
                return BaseLocation.ATMO4;
            if (value.Contains("5"))
                return BaseLocation.ATMO5;
            if (value.Contains("6"))
                return BaseLocation.ATMO6;
            if (value.Contains("7"))
                return BaseLocation.ATMO7;
            if (value.Contains("8"))
                return BaseLocation.ATMO8;
            if (value.Contains("MH"))
                return BaseLocation.MH;
            else
                return BaseLocation.Unknown;
        }
        public static BaseLocation GetLocationFromDZADocInfo(string value)
        {
            if (value.Contains("1"))
                return BaseLocation.ATMO1;
            if (value.Contains("2"))
                return BaseLocation.MH;
            if (value.Contains("3"))
                return BaseLocation.ATMO2;
            if (value.Contains("4"))
                return BaseLocation.ATMO3;
            if (value.Contains("5"))
                return BaseLocation.ATMO8;
            else
                return BaseLocation.Unknown;
        }
        private const string srvA1 = "\\\\10.3.129.122";//                        'ATMO1 Fe
        private const string srvA2 = "\\\\10.15.64.84";//  'ATMO2 MD
        private const string srvA3 = "\\\\10.54.5.24";//'ATMO3 SZ
        private const string srvA8 = "\\\\10.50.194.76";//'ATMO8 BU
        private const string srvMH = "\\\\10.7.160.147";//'Moehwald


        public static string GetDocServerPathFromLocation(BaseLocation docBaseLoc)
        {
            switch (docBaseLoc)
            {
                case GlobalDictionaries.BaseLocation.ATMO1:
                    {
                        return srvA1;
                    }
                case GlobalDictionaries.BaseLocation.ATMO2:
                    {
                        return srvA2;
                    }
                case GlobalDictionaries.BaseLocation.ATMO3:
                    {
                        return srvA3;
                    }
                case GlobalDictionaries.BaseLocation.ATMO5:
                    {
                        return srvA3;
                    }
                case GlobalDictionaries.BaseLocation.ATMO6:
                    {
                        return srvA3;
                    }
                case GlobalDictionaries.BaseLocation.ATMO7:
                    {
                        return srvA1;
                    }
                case GlobalDictionaries.BaseLocation.ATMO8:
                    {
                        return srvA8;
                    }
                case GlobalDictionaries.BaseLocation.MH:
                    {
                        return srvMH;
                    }
                default:
                    {
                        return srvA1;
                    }

            }
        }
        public static Dictionary<string, string> dicDZAWorkDirs = new Dictionary<string, string>
        {
            {"ATMO1",@"\\fe-106-1.de.bosch.com\MCAD_FILES$\DZA\WORKDIR\"},
            {"ATMO2",@"\\md1fs0\atmo2share$\02_DZA\WORKDIR"},      
			//{"ATMO3",@"\\szh2fs01.apac.bosch.com\bosch$\DZA\Workdir"},
            {"ATMO3",@"\\bosch.com\dfsrb\DfsCN\loc\szh\Public\L\DZA\Workdir"},

            {"ATMO5",@"\\bosch.com\dfsrb\DfsCN\loc\CNG\PA-ATMO5\DZA\Workdir"},
            {"ATMO6",""},
            {"ATMO7",@"\\TL1VLTP01.us.bosch.com\DZADFC_USER_WORKDIR$\WORKDIR"},
            {"ATMO8",@"\\bosch.com\dfsrb\DfsTR\DIV\PA\PA-ATMO8_Work\02_PA-ATMO8\07_DZA\Workdir"},
            {"MH",@"\\ho9-projekte\autocad\DZA\WORKDIR"},

        };
        public static string GetDZAWorkDir(string sATMOSite)
        {
            string sDZAWorkDir = "";
            if (dicDZAWorkDirs.ContainsKey(sATMOSite.Trim()))
                sDZAWorkDir = dicDZAWorkDirs[sATMOSite.Trim()];
            return sDZAWorkDir;
        }

        /// <summary>
        /// REferenced in DOCINFO als "LOCATION"
        /// defined in \...\project\config\settings\docustat.oel
        /// </summary>
        public static Dictionary<string, string> dicLocations = new Dictionary<string, string>
        {
            {"1","ATMO1"},      //Feuerbach
			{"3","ATMO2"},      //Madrid
			{"4","ATMO3"},      //Suzhou
			{"5","ATMO8"},      //Bursa
			{"2","ATMO9"},      //unbekannt
		  //{"MH",              - Moehwald")
		  //{"ATMO4",           - Charleston")
		  //{"ATMO5",           - Changsha")
		  //{"ATMO6",           - Bangalore")
		  //{"ATMO7",           - Toluca")
	 };

        /// <summary>
        /// Return a list of locations where files is present. 
        /// </summary>
        /// <param name="LocationNr"> This is a binary number represented in decimal.
        /// For example. LocationNr=15 is in binary=01111. This means file is located in Location 1,2,3 and 4 </param>
        /// For example. LocationNr=8 is in binary=01000. This means file is located in Location 4-->Suzhou see dicLocations </param>
        /// <returns></returns>
        public static List<string> GetDZALocation(long LocationNr)
        {
            List<string> lLocations = new List<string>();
            string binary = Convert.ToString(LocationNr, 2);
            int loc = 0;
            foreach (char binCharacter in binary)
            {
                loc++;
                if (binCharacter.ToString() == "1")
                {
                    if (dicLocations.ContainsKey(loc.ToString()))
                        lLocations.Add(dicLocations[loc.ToString()]);
                }
            }
            return lLocations;
        }

        /// <summary>
        /// mko, 13.9.2017: Zustände von Dokumenten im Workflowprozess
        /// </summary>
        public enum DfcDocStates
        {
            /// <summary>
            /// No docstate defined
            /// </summary>
            none = 0,

            /// <summary>
            /// gesperrt 
            /// </summary>
            disabled = 3,

            /// <summary>
            /// erledigt
            /// </summary>
            finished = 4,

            /// <summary>
            /// erfasst
            /// </summary>
            captured = 5,

            /// <summary>
            /// In der Bearbeitung, z.B. Zeichnungen, CTS, TEF
            /// </summary>
            inWork = 6,

            /// <summary>
            /// Freigegeben, TEF, Orders
            /// </summary>
            enabled = 7,

            /// <summary>
            /// bestätigt
            /// </summary>
            confirmed = 8,

            /// <summary>
            /// Latest and valid Version. Drawings, CTS, TDP
            /// </summary>
            released = 10,

            /// <summary>
            /// Old versions
            /// </summary>
            obsolete = 11,

            /// <summary>
            /// A drawing has an open modification and is waiting for review
            /// </summary>
            shopFloor = 12,

            /// <summary>
            /// Drawing not valid
            /// </summary>
            invalid = 13,

            /// <summary>
            /// Drawing is migrating
            /// </summary>
            migration = 14,

            /// <summary>
            /// SFC, EDC
            /// </summary>
            open = 15,

            /// <summary>
            /// SFC, EDC
            /// </summary>
            solved = 16,

            /// <summary>
            /// A modification has been declined by the responisble employee. Only for SFC and EDC
            /// </summary>
            declined = 17,

            /// <summary>
            /// Drawings in work by the engineer
            /// </summary>
            inWorkEDC = 18

        }

        /// <summary>
        /// mko, 13.9.2017
        /// Returns DZA- code for docstate
        /// </summary>
        /// <param name="state">dza code as string. e.g. inWork -&gt; "6" </param>
        /// <returns></returns>
        public static string ToDZACode(this DfcDocStates state)
        {
            return ((int)state).ToString();
        }

        /// <summary>
        /// Referenced in DOCINFO als "USERSTATE"
        /// defined in \...\project\config\settings\docustat.oel
        /// </summary>
        public static Dictionary<string, string> docStates = new Dictionary<string, string>
        {
            {"0","-"},
            {"3","gesperrt"},
            {"4","erledigt"},
            {"5","erfaßt"},
            {"6","in work"}, // Drawing /CTS / TEF
			{"7","freigegeben"},//TEF / Orders
			{"8","bestätigt"},
            {"10","Released"},//Drawing /CTS / TDP / Kata / TEF
			{"11","Obsolete"},//Drawing /CTS / TEF
			{"12","Shop Floor"},//Drawing by Upload a SFC or EDC
			{"13","Invalid"},//Drawing / Kata  TEF
			{"14","Migration"},//Drawing
			{"15","open"},//SFC, EDC
			{"16","solved"},//SFC, EDC
			{"17","declined"},//SFC, EDC
            // mko, 16.10.2018
			//{"18","inWork"}, //EDC
            {"18","in work EDC"}, //EDC
		};

        public static string GetStateText(string StateID)
        {
            if (StateID == null)
                return "";
            string StateText = StateID;
            if (docStates.ContainsKey(StateID))
                StateText = docStates[StateID];
            return StateText;
        }

        /// <summary>
        /// mko, 6.7.2018
        /// Maps doc states to textual descriptions, used in presentation of BOM nodes.
        /// </summary>
        /// <param name="docType"></param>
        /// <returns></returns>
        public static string GetStateText(this DfcDocStates docState)
        {
            return docStates[((int)docState).ToString()];
        }

        public static string GetStateNrFromText(string text)
        {
            string myValue = "";
            if (docStates.ContainsValue(text))
                myValue = docStates.FirstOrDefault(x => x.Value == text).Key;
            else if (docStates.ContainsKey(text))
                myValue = text;
            return myValue;
        }

        /// <summary>
        /// mko, 06.07.2018
        /// Parse a Docstate from Text. The possible texts are defined in docStates 
        /// dictionary
        /// </summary>
        /// <param name="Text"></param>
        /// <returns></returns>
        public static DfcDocStates ParseDocState(string Text)
        {
            var res = DfcDocStates.none;
            if (!string.IsNullOrWhiteSpace(Text))
            {
                var txtLwr = Text.ToLower();
                var kv = docStates.FirstOrDefault(r => r.Value.ToLower() == txtLwr);

                // mko, 28.2.2020
                // Da KeyValue- Pair eine Struktur ist, (Wertetyp), kann kv niemals ein null sein.
                // Die Prüfung auf kv.Key ist damit sicher!
                if (kv.Key != null)
                {
                    res = (DfcDocStates)int.Parse(kv.Key);
                }
            }

            return res;
        }


        public static List<string> lDZATDPsTypes = new List<string>()
        { "01 CE-Customer", "02 CE-Internal", "03 Manual", "04 Certificates", "05 Maintenance", "06 External Drawings","07 External BOM",
          "08 Installation Description", "09 E-PLAN BOM", "10 Manufacturers explan.", "11 Calibration Instr.","12 Manual Changeover",
          "13 Parts Catalog", "14 Software", "15 Maintenance Schedule", "16 Industrial_Engineering","17 Kata_Certificates-Internal" };

        /// <summary>
        /// defined in \...\project\config\settings\p_selectionvalues.oel
        /// </summary>
        public static Dictionary<string, string> docTechDocTypes = new Dictionary<string, string>
        {
            {"01","CE-Kunde"},
            {"02","CE-Intern"},
            {"03","Manual - Bedienungsanleitung"},
            {"04","Certificates - Zertifikate"},
            {"05","Maintenance - Instandhaltung"},
            {"06","External Drawings - ext. Zeichnungen"},
            {"07","External BOM - ext. Stuecklisten"},
            {"08","Installation Descr. - Montageanleitung"},//former Einbauerklaerung
			{"09","E-Plan BOM - E-Plan_Stueckliste"},
            {"10","Manufacturers explan. - Herstellererkl."},
            {"11","Calibration Instr. - Kalibrieranleitung"},
            {"12","Manual changeover - Umruestanleitung"},
            {"13","Parts Catalog - Katalogteile"},
            {"14","Software"},
            {"15","Maintenance Schedule - Wartungsplan"},
            {"16","Industrial_Engineering"},
            {"17","Kata_Certificates-Internal"}
        };

        /// <summary>
        /// mko, 1.10.2018
        /// Streng typisierter Ersatz für die docTechDocTypes Dictionary. Dient zur sauberen 
        /// Abbildung des Typs in der DFCObjects.Common.Doc.IDocInfo Schnittstelle
        /// </summary>
        public enum TDPType
        {
            /// <summary>
            /// Wenn kein TDP- Dokument vorliegt
            /// </summary>
            none = 0,

            CE_Kunde = 1,

            CE_Intern = 2,

            /// <summary>
            /// Bedienungsanleitungen
            /// </summary>
            Manuals = 3,

            /// <summary>
            /// Zertifikate
            /// </summary>
            Certificates = 4,

            /// <summary>
            /// Instandhaltung
            /// </summary>
            Maintenance = 5,

            /// <summary>
            /// ext. Zeichnungen
            /// </summary>
            External_Drawings = 6,

            /// <summary>
            /// externe Stücklisten
            /// </summary>
            External_BOM = 7,

            /// <summary>
            /// Montageanleitung
            /// </summary>
            Installation_Descr = 8,

            /// <summary>
            ///  E-Plan_Stueckliste
            /// </summary>
            EPlan_BOM = 9,

            /// <summary>
            /// Herstellererklärung
            /// </summary>
            Manufacturers_explan = 10,


            /// <summary>
            ///  Kalibrieranleitung
            /// </summary>
            Calibration_Instr = 11,

            /// <summary>
            ///  Umruestanleitung
            /// </summary>
            Manual_changeover = 12,

            /// <summary>
            ///  Katalogteile
            /// </summary>
            Catalog_Parts = 13,

            Software = 14,

            /// <summary>
            ///  Wartungsplan
            /// </summary>
            Maintenance_Schedule = 15,


            /// <summary>
            /// 
            /// </summary>
            Industrial_Engineering = 16,


            Kata_Certificates_Internal = 17
        }

        /// <summary>
        /// defined in \...\project\config\settings\p_selectionvalues.oel
        /// </summary>
        public static Dictionary<string, string> docLevel1 = new Dictionary<string, string>
        {
            {"l1","electric part"},
            {"l2","mechanic part"},
            {"l3","plant"},
            {"l4","station"},
            {"l5","mechanic assembly"},
            {"l6","electric assembly"},
        };
        public static Dictionary<string, string> docLevel2 = new Dictionary<string, string>
        {
            {"l1","diagram / schema"},
            {"l2","catalog"},
            {"l3","manufacturing"},
            {"l4","migration"},
            {"l5","standard-optional"},
            {"l6","standard"},
            {"l7","offer"},
            {"l8","ReUse"},
        };

        /// <summary>
        /// defined in \...\project\config\settings\p_selectionvalues.oel
        /// </summary>
        public static Dictionary<string, string> docLevel3 = new Dictionary<string, string>
        {
            {"l1","ECM"},
            {"l2","Internal grinding"},
            {"l3","HE"},
            {"l4","Vision"},
            {"l5","EMI"},
            {"l6","Mounting"},
            {"17","Measurement"},
        };

        /// <summary>
        /// defined in \...\project\config\settings\b_famdef.oel
        /// </summary>
        public static Dictionary<string, string> dicFamilies = new Dictionary<string, string>
        {
		  //ID      Family name                     DZA table name                  sampleDOCID     INFOTEXT in DOCINFO      
			{"1","Allgemeines Dokument"},		    //ordinary
			{"2","Allgemeiner Ordner"},		        //ordinary_folder
			{"3","Notiz"},		                    //memo
			{"4","Formular"},		                //form
			{"5","EPLAN"},		                    //boscheplan                    //2679568   //M8010087.040|0843365965|WAA Line||
			{"6","TEF-Rasterdaten"},		        //boschtef                      //2687542   //EM1867P-GR.35_0|Abfrage|interrogation|interrogación||F023F32497
			{"7","Übersetzte Zeichnungen "},		//bosch_translated
			{"18","Unklassifiziertes Dokument"},	//unclassified
			{"29","Leitdokument"},		            //mail_guide
			{"40","trashcan"},		                //Benutzerabhängiger Papierkorb
			{"51","Technische Zeichnung (V"},		//drawing
			{"202","Benutzerabhängiger Systemordner"},//usersysfolder->DZA SchreibTisch
			{"302","Importiertes Logfile"},		    //logfile_imp
			{"9000","Problemreport"},		        //ProbRep
			{"9001","Gepeicherte Eingangspost"},	//StoredInMail
			{"9002","Eingangspostordner"},		    //inmailfolder
			{"9003","Zeichnung (neu)"},		        //TDrawing
			{"9004","Zeichnung"},		            //boschdokument                 //2525658   //3 843 AF0 218|Dokumentation|documentation|documentaciÃ³n|Erich Scheugenpflug||126011_02/POS.1-3/20.06.13||Deutsch|M-Teil|Katalog|Montage
			{"9012","Katalog"},		                //boschdatenblatt               //2534866   //1 840 201 442|Material Box|Material Box|Material Box|changsha dazhong workshop||DZ-04;BLUE|||MTEIL|Katalog|Montage|CAT
			{"9016","Altdaten"},		            //boschaltdaten
			{"9017","Angebotszeichnung"},		    //boschangebotszeichng
			{"9018","Werkstattänderung"},		    //boschwerkstattaendg           //2514803   //W 0 843 196 465
			{"9019","Projektordner"},		        //boschprojekt                  //2665904   //M5140202 Grinding_Machine_GRIBS_SC1c
			{"9020","Moehwald"},		            //moehwalddokument              //2347119   //0 804 BF2 982|Traegerplatte|carrying plate|placa portadora||||1.0570 S355J2G3||mechanic part|manufacturing|Mounting
			{"9021","Stuecklisten"},		        //boschstueli                   //2678872   //3 800 027 355_00|Ring Assembly|Ring Assembly|Ring Assembly|
			{"9400","Gespeicherte Attribute"},		//StoredTypeAttributes
			{"10739","CTS Datei"},		            //boschcts                      //2515972   //M4701287.060.0.1 EGS-PM2
			{"10740","Dokumentation"},		        //boschdoku                     //2525666   //3 843 AF0 228|Dokumentation|documentation|documentaciÃ³n|Bosch Rexroth Steuerungstechni|296958;HMS01.1N-W0020-A-07-NNNN+|R911305033/295323+328178+328741+330278||Englisch|E-Teil|Katalog|Montage
			{"10741","Technische Unterlagen"},		//boschtechdok                  //2334413   //0843172751|Certificates - Zertifikate|M4701016.020
			{"10743","Konstruktionsänderung"},       //boschkonstaendg              //2695670   //0 843 200 063
			{"10744","Orders"}                      //order only in DOCINFO         //2695670   //0 843 200 063
		};


        public static Dictionary<string, string> dicLogAction = new Dictionary<string, string>
        {
			#region DZA History Protocol
			{"0","archive_mag_disc"},
            {"1","archive_optical_disc"},
            {"2","create document"},
            {"3","created from"},
            {"4","delete document"},
            {"5","edit access"},
            {"6","edit class"},
            {"7","edit document"},
            {"8","import document"},
            {"9","scan document"},
            {"10","edit state to"},
            {"11","vers create document"},
            {"12","versimport document"},
            {"13","vers scan document"},
            {"14","transfer"},
            {"15","transfer_class"},
            {"16","transfer_content"},
            {"17","startedfolder"},
            {"18","cleanupcache"},
            {"19","setcurrentver"},
            {"20","dearchive"},
            {"21","setonfollowup"},
            {"22","send"},
            {"23","add to folder"},
            {"24","remove from folder"},
            {"25","checked in"},
            {"26","checked out"},
            {"27","co_recover"},
            {"28","change fam"},
            {"29","change type"},
            {"30","unsend"},
            {"31","is_skipped"},
            {"32","process_changed"},
            {"33","time_limit_exceeded"},
            {"34","is_executed"},
            {"35","process_deleted"},
            {"36","process_delegated"},
            {"37","condition_false"},
            {"38","variantcreated"},
            {"39","archive_cas"},
            {"40","export to"},
            {"41","print"},
            {"42","doc remove from folder"},
            {"43","doc add to folder"},
            {"44","add reference"},
            {"45","remove reference"},
            {"46","movetotrash"},
            {"47","recovered"},
            {"48","encrypted"},
            {"49","setonfollowupfor"},
            {"12000","ph_master_added"},
            {"12001","ph_master_changed"},
            {"12002","ph_master_removed"},
            {"12003","ph_rtf_created"},
            {"12004","ph_ctd_module_print"},
            {"12005","ph_ctd_print"},
            {"12006","ph_draft_print"},
            {"12007","ph_daily_print"},
            {"12008","ph_original_print"},
            {"12009","ph_overmax_print"},
            {"12010","ph_auto_numbering"},
            {"12011","ph_ctd_dossier_print"},
            {"12100","ph_resetArtwork"},
            {"12101","ph_discontinuedArtwork"},
            {"12200","ph_formatvariant_created"},
            {"12300","ph_xevprm_transmitted"},
            {"12501","ph_flv_dupl_from_doc"},
            {"12502","ph_flv_imp_from_doc"},
            {"12503","ph_flv_cre_from_doc"},
            {"12504","ph_flv_scan_from_doc"},
            {"12505","ph_flv_fldcre_from_fld"},
            {"12510","ph_eCTD_productive_exported"},
            {"15200","cm_changeterminationdate"},
            {"15201","cm_kuendchanged"},
            {"15202","cm_mailsent"},
            {"15203","cm_kuendzumchanged"},
            {"20001","exported"},
            {"20002","exported_all"},
            {"20003","exported_user"},
            {"20004","checkedout_all"},
            {"20005","checkedout_user"},
            {"20006","importdocmi"},
            {"20007","importdocdwg"},
            {"20008","checkedout_virt"},
            {"20009","log_show"},
            {"20010","log_print"},
            {"20011","log_export"}
				 #endregion
		};

        public static string GetDZALogAction(string textKey)
        {
            string myValue = "";
            if (dicLogAction.ContainsKey(textKey))
                myValue = dicLogAction[textKey];

            return myValue;
        }

        public static string GetDocServerPathPartListTurkish()
        {
            string Path1 = GlobalDictionaries.GetDocServerPathFromLocation(GlobalDictionaries.BaseLocation.ATMO8);
            return Path.Combine(Path1, "gdm$", "ATD_TR");
        }

        // ATMOx Intern Employees are allowed to see Documents for its ATMOx Site
        // Also Mechanical DEsigner roles allow see Drawings. for example External employees
        // mko, 20.10.2017
        // public geschaltet
        // 
        public static Dictionary<string, string> dicATMORoles = new Dictionary<string, string>()
            {
                {"7","a1"},     //Mechanical Designer ATMO1
				{"28","a1"},    //Path User ATMO1
				{"48","a1"},    //Path_KB (EDL von ATMO1)
				{"57","a1"},    //ATMO1_Externe_DL_Zeich
				{"147","a1"},   //Simple User ATMO1
                {"148", "a1"},  // ADD Drawings ATMO1
                {"180", "a1" }, // ATMO1_EES_TD
   				{"187","a1"},   //ATMO1_Externe_DL_EES no rights for Drawings only EPLANs

                {"27", "a2" },  // ATMO2_Externe_DL
				{"56","a2"},    //Path User ATMO2        
				{"93","a2"},    //Simple User ATMO2        
				{"146","a2"},   //Mechanical Designer ATMO2
  
				{"80","a3"},    //Mechanical Designer ATMO3
				{"82","a3"},    //Path User ATMO3
				{"107","a3"},   //Simple User ATMO3
				{"108","a3"},   //Mechanical Designer ATMO3 ALL

				{"18","a4"},    //Mechanical Designer ATMO4
				{"32","a4"},    //Simple User ATMO4
				{"39","a4"},    //Path User ATMO4

				{"84","a5"},    //Mechanical Designer ATMO5
   				{"122","a5"},   //Mechanical Designer ATMO5 ALL
				{"123","a5"},   //Simple User ATMO5
				{"135","a5"},   //Path User ATMO5

				{"104","a6"},   //Mechanical Designer ATMO6
				{"106","a6"},   //Path User ATMO6
				{"124","a6"},   //Mechanical Designer ATMO6 ALL
				{"125","a6"},   //Simple User ATMO6

				{"91","a7"},    //Simple User ATMO7
				{"117","a7"},   //Path User ATMO7				
				{"126","a7"},   //Mechanical Designer ATMO7
				{"127","a7"},   //Mechanical Designer ATMO7 ALL

				{"128","a8"},   //Mechanical Designer ATMO8
				{"129","a8"},   //Mechanical Designer ATMO8 ALL
				{"130","a8"},   //Simple User ATMO8
				{"139","a8"},   //Path User ATMO8

				{"20","mh"},    //Simple User MH
				{"29","mh"},     //Path User MH
                {"113","mh"},   //Mechanical Designer MH
			};


        /// <summary>
        /// mko, 20.10.2017
        /// Weist vemutlich einer DZA- Rolle eine AD- Gruppe zu
        /// </summary>
        private static Dictionary<string, string> dicATMO_ADRolesID = new Dictionary<string, string>()
            {
                {"11","a1"},//Internal ATMO1
				{"12","a1"},//External ATMO1
				{"21","a2"},//Internal ATMO2
				{"22","a2"},//External ATMO2
                {"31","a3"},//Internal ATMO3
				{"32","a3"},//External ATMO3
                {"41","a4"},//Internal ATMO4
				{"42","a4"},//External ATMO4
                {"51","a5"},//Internal ATMO5
				{"52","a5"},//External ATMO5
                {"61","a6"},//Internal ATMO6
				{"62","a6"},//External ATMO6
                {"71","a7"},//Internal ATMO7
				{"72","a7"},//External ATMO7
                {"81","a8"},//Internal ATMO8
				{"82","a8"},//External ATMO8
                {"91","mh"},//Internal MH
				{"92","mh"},//External MH
			};

        /// <summary>
        /// mko, 20.10.2017
        /// Prüft, ob die Rolle einem ATMO- Standort zugeordnet ist 
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        public static bool IsATMOSiteMappedDZARole(string role)
        {
            return dicATMORoles.ContainsKey(role);
        }
        public static string GetDZARoleATMOSite(string DZARoleID)
        {
            string myValue = "";
            if (dicATMORoles.ContainsKey(DZARoleID))
                myValue = dicATMORoles[DZARoleID];

            return myValue;
        }
        private static Dictionary<string, string> dicATMORights = new Dictionary<string, string>()
           {
               {"140","atmo8"},
               {"142","atmo9"},
               {"137","atmo5"},
               {"139","atmo7"},
               {"124","atmo3"},
               {"118","atmo0"},
               {"120","atmo1"},
               {"32","atmo1"},
               {"138","atmo6"},
               {"164","mh"},
               {"126","atmo4"},
               {"122","atmo2"}
			   //{"7","all"}
		   };
        public static bool HasADUserLocationReadRights(List<string> lUserRoles, string ATMOLocation, string NTUser = "")
        {
            if (ATMOLocation == "" || ATMOLocation == null)//Material is not restricted to a location-->it means allow access allways
                return true;
            ATMOLocation = ATMOLocation.ToLower();
            //return true is To disable Project prmissions
            //return true;
            bool bHasRights = false;
            if (lUserRoles.Contains("00"))//Admin Rights
                return true;
            if (lUserRoles.Contains("110"))//Fe_ATMO_DFC_InternalExternal_ATMOxDrawings_User_UM
                return true;
            string[] ATMOLocs = ATMOLocation.Split(',');
            List<string> ATMOLocationAllowed = new List<string>();
            foreach (string ATMOLoc in ATMOLocs)
                ATMOLocationAllowed.Add(ATMOLoc.ToLower());
            foreach (string ADroleID in lUserRoles)
            {
                if (!dicATMO_ADRolesID.ContainsKey(ADroleID))
                    continue;
                string rightNameLocation = dicATMO_ADRolesID[ADroleID];
                if (ATMOLocationAllowed.Contains(rightNameLocation))
                    return true;
            }
            return bHasRights;

        }

        /// <summary>
        /// mko, 6.10.2017
        /// Sprünge durch Blockstrukturen beseitigt
        /// </summary>
        /// <param name="lUserRoles"></param>
        /// <param name="ATMOLocation"></param>
        /// <param name="NTUser"></param>
        /// <returns></returns>
        public static bool HasDZAUserLocationReadRights(List<string> lUserRoles, string ATMOLocation, string NTUser = "")
        {
            if (string.IsNullOrEmpty(ATMOLocation))
            {
                //Material is not restricted to a location-->it means allow access allways
                return true;
            }
            else
            {
                ATMOLocation = ATMOLocation.ToLower();
                //return true is To disable Project prmissions
                //return true;
                bool bHasRights = false;
                if (lUserRoles.Contains("76"))
                {
                    //Admin Ricghts
                    bHasRights = true;
                }
                else if (lUserRoles.Contains("205"))
                {
                    //DFC_ALL_Drawing
                    bHasRights = true;
                }
                else
                {
                    string[] ATMOLocs = ATMOLocation.Split(',');
                    List<string> ATMOLocationAllowed = new List<string>();
                    foreach (string ATMOLoc in ATMOLocs)
                    {
                        ATMOLocationAllowed.Add(ATMOLoc.ToLower());
                    }

                    foreach (string DZAroleID in lUserRoles)
                    {
                        if (dicATMORoles.ContainsKey(DZAroleID))
                        {
                            string rightNameLocation = dicATMORoles[DZAroleID];
                            if (ATMOLocationAllowed.Contains(rightNameLocation))
                            {
                                bHasRights = true;
                                break;
                            }
                        }
                    }
                }
                return bHasRights;
            }
        }
        public static List<string> GetUserATMOLocations(List<string> lUserRoles)
        {
            List<string> lUserATMOs = new List<string>();
            if (lUserRoles.Contains("76") || lUserRoles.Contains("205"))//Admin Ricghts //DFC_ALL_Drawing
                lUserATMOs = new List<string>() { "A1", "A2", "A3", "A4", "A5", "A6", "A7", "A8", "MH" };
            foreach (string DZAroleID in lUserRoles)
            {
                if (!dicATMORoles.ContainsKey(DZAroleID))
                    continue;
                string rightNameLocation = dicATMORoles[DZAroleID];
                if (!lUserATMOs.Contains(rightNameLocation))
                    lUserATMOs.Add(rightNameLocation);
            }
            return lUserATMOs;
        }

        //    S.No SAP WS
        //private static Dictionary<string, string> dicWSFieldsZPSTrack = new Dictionary<string, string>()
        //   {
        //	{"stage","LEVEL" },
        //	{"kind","PROJECT_TYPE" },
        //	{"description","DESCRIPTION" },
        //	{"mat.-nr.","MATERIAL_NUMBER" },
        //	{"qty","QUANTITY" },
        //	{"pl.en.d.t.","CONSTRUCTION_PLAN_DATE" },
        //	{"r.en.d.t.","CONSTRUCTION_END_DATE" },
        //	{"e.of pr.","PROCUREMENTDATE" },
        //	{"dod","PROCUREMENT_END_DATE" },
        //	{"sdod","ITEM_DELIVERY_DATE" },
        //	{"cdod","POCONFIRMED_DEL_DATE" },
        //	{"good receive","GoodsReceipt" },
        //	{"stor typ","STORAGE_TYPE" },
        //	{"store-des","STORAGE_DESCRIPTION" },
        //	{"storage","STORAGE_BIN" },
        //	{"WBS","PROJECT_DEFINITION" },
        //	{"mat-demand","BOMSTART_DATE" },
        //	{"informat.","GENERAL_NAME" }
        //};
    }
}
