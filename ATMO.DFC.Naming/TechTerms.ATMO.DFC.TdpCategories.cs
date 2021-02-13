using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DFCSecurity;
using DZAUtilities_Dictionaries;

/// <summary>
/// mko, 2.9.2020
/// </summary>
namespace MKPRG.Naming.TechTerms.ATMO.DFC.TdpCat
{

    /// <summary>
    /// mko, 2.9.2020
    /// </summary>
    public interface ITdpCat
    {
        GlobalDictionaries.TDPType GlobDictTDPType { get; }
        DFCSecurity.SecuredDocs DfcSecurityDocType { get; }
    }

    /// <summary>
    /// mko, 2.9.2020
    /// Klassifizierung von technischen Dokumentationen
    /// </summary>
    public class TDPCat
        : NamingBase
    {

        public const long UID = 0x522D695C;

        public TDPCat()
            : base(UID)
        {
        }

        public override string CNT => "tdpCat";
        public override string CN => "TDP类别";
        public override string DE => "TDP Kategorie";
        public override string EN => "TDP category";
        public override string ES => "Categoría TDP";
    }

    /// <summary>
    /// CE- Zertifikate, die für den Kunden bestimmt sind 
    /// </summary>
    public class CE_Customer
        : NamingBase, ITdpCat
    {

        public const long UID = 0xA73027A1;

        public CE_Customer()
            : base(UID)
        {
        }

        public override string CNT => "tdpCatCeCustomer";
        public override string CN => "为客户提供CE证书";
        public override string DE => "CE Zertifikate für Kunden";
        public override string EN => "CE certificates for customers";
        public override string ES => "Certificados CE para los clientes";

        public GlobalDictionaries.TDPType GlobDictTDPType => GlobalDictionaries.TDPType.CE_Kunde;

        public DFCSecurity.SecuredDocs DfcSecurityDocType =>DFCSecurity.SecuredDocs.TDP;
    }

    /// <summary>
    /// CE- Zertifikate, die für den Kunden bestimmt sind 
    /// </summary>
    public class CE_Intern
        : NamingBase, ITdpCat
    {

        public const long UID = 0x894117F7;

        public CE_Intern()
            : base(UID)
        {
        }

        public override string CNT => "tdpCatCeIntern";
        public override string CN => "CE证书ATMO-内部";
        public override string DE => "CE Zertifikate ATMO- intern";
        public override string EN => "CE certificates ATMO- internal";
        public override string ES => "Certificados CE ATMO- interno";

        public GlobalDictionaries.TDPType GlobDictTDPType => GlobalDictionaries.TDPType.CE_Intern;

        public DFCSecurity.SecuredDocs DfcSecurityDocType => DFCSecurity.SecuredDocs.TDP_internal;
    }

    /// <summary>
    /// Handbücher und Bedienungsanleitungen
    /// </summary>
    public class Manuals
        : NamingBase, ITdpCat
    {

        public const long UID = 0xFD20D4ED;

        public Manuals()
            : base(UID)
        {
        }

        public override string CNT => "tdpCatManuals";
        public override string CN => "指南";
        public override string DE => "Handbücher/Bedienungsanleitungen";
        public override string EN => "Manuals";
        public override string ES => "Manuales";

        public GlobalDictionaries.TDPType GlobDictTDPType => GlobalDictionaries.TDPType.Manuals;

        public DFCSecurity.SecuredDocs DfcSecurityDocType => DFCSecurity.SecuredDocs.TDP;
    }

    /// <summary>
    /// Handbücher und Bedienungsanleitungen
    /// </summary>
    public class ManualsForToolConversion
        : NamingBase, ITdpCat
    {

        public const long UID = 0x5EF08485;

        public ManualsForToolConversion()
            : base(UID)
        {
        }

        public override string CNT => "tdpCatManualsForToolConversion";
        public override string CN => "刀具转换说明";
        public override string DE => "Anleitungen zur Werkzeugumrüstung";
        public override string EN => "Instructions for tool conversion";
        public override string ES => "Instrucciones para la conversión de herramientas";

        public GlobalDictionaries.TDPType GlobDictTDPType => GlobalDictionaries.TDPType.Manual_changeover;

        public DFCSecurity.SecuredDocs DfcSecurityDocType => DFCSecurity.SecuredDocs.TDP;
    }


    /// <summary>
    /// Handbücher und Bedienungsanleitungen
    /// </summary>
    public class Maintenance
        : NamingBase, ITdpCat
    {

        public const long UID = 0xA4561C93;

        public Maintenance()
            : base(UID)
        {
        }

        public override string CNT => "tdpCatMaintenance";
        public override string CN => "维护";
        public override string DE => "Instandhaltung";
        public override string EN => "Maintenance";
        public override string ES => "Mantenimiento";

        public GlobalDictionaries.TDPType GlobDictTDPType => GlobalDictionaries.TDPType.Maintenance;

        public SecuredDocs DfcSecurityDocType => SecuredDocs.TDP;
    }

    /// <summary>
    /// Handbücher und Bedienungsanleitungen
    /// </summary>
    public class MaintenanceSchedule
        : NamingBase, ITdpCat
    {

        public const long UID = 0x23E42ED7;

        public MaintenanceSchedule()
            : base(UID)
        {
        }

        public override string CNT => "tdpCatMaintenanceSchedule";
        public override string CN => "维护时间表";
        public override string DE => "Instandhaltungsplan";
        public override string EN => "Maintenance schedule";
        public override string ES => "Plan de mantenimiento ";

        public GlobalDictionaries.TDPType GlobDictTDPType => GlobalDictionaries.TDPType.Maintenance_Schedule;

        public SecuredDocs DfcSecurityDocType => SecuredDocs.TDP;
    }

    /// <summary>
    /// Handbücher und Bedienungsanleitungen
    /// </summary>
    public class ExternalDrawings
        : NamingBase, ITdpCat
    {

        public const long UID = 0xA5F24507;

        public ExternalDrawings()
            : base(UID)
        {
        }

        public override string CNT => "tdpCatExternalDrawings";
        public override string CN => "外部图纸";
        public override string DE => "Externe Zeichnungen";
        public override string EN => "External Drawings";
        public override string ES => "Dibujos externos";

        public GlobalDictionaries.TDPType GlobDictTDPType => GlobalDictionaries.TDPType.External_Drawings;

        public SecuredDocs DfcSecurityDocType => SecuredDocs.TDP;
    }


    /// <summary>
    /// Handbücher und Bedienungsanleitungen
    /// </summary>
    public class ExternalBoms
        : NamingBase, ITdpCat
    {

        public const long UID = 0x66ADEC9F;

        public ExternalBoms()
            : base(UID)
        {
        }

        public override string CNT => "tdpCatExternalBoms";
        public override string CN => "外部炸弹";
        public override string DE => "Externe Stücklisten";
        public override string EN => "External Boms";
        public override string ES => "Listas de partes externas";

        public GlobalDictionaries.TDPType GlobDictTDPType => GlobalDictionaries.TDPType.External_BOM;

        public SecuredDocs DfcSecurityDocType => SecuredDocs.TDP;
    }

    /// <summary>
    /// Handbücher und Bedienungsanleitungen
    /// </summary>
    public class EplanBoms
        : NamingBase, ITdpCat
    {

        public const long UID = 0x4E95E378;

        public EplanBoms()
            : base(UID)
        {
        }

        public override string CNT => "tdpCatEplanBoms";
        public override string CN => EN;
        public override string DE => "EPlan Stücklisten";
        public override string EN => "EPlan Boms";
        public override string ES => "Listas de partes de EPlan";

        public GlobalDictionaries.TDPType GlobDictTDPType => GlobalDictionaries.TDPType.EPlan_BOM;

        public SecuredDocs DfcSecurityDocType => SecuredDocs.TDP;
    }

    /// <summary>
    /// Handbücher und Bedienungsanleitungen
    /// </summary>
    public class IndustrialEngineering
        : NamingBase, ITdpCat
    {

        public const long UID = 0xEAC47FC9;

        public IndustrialEngineering()
            : base(UID)
        {
        }

        public override string CNT => "tdpCatIndustrialEngineering";
        public override string CN => "工业工程";
        public override string DE => "Betriebstechnik";
        public override string EN => "Industrial engineering";
        public override string ES => "Tecnología operativa";

        public GlobalDictionaries.TDPType GlobDictTDPType => GlobalDictionaries.TDPType.Industrial_Engineering;

        public SecuredDocs DfcSecurityDocType => SecuredDocs.TDP;
    }

    /// <summary>
    /// Handbücher und Bedienungsanleitungen
    /// </summary>
    public class InstallationInstructions
        : NamingBase, ITdpCat
    {

        public const long UID = 0xFE1B9B3F;

        public InstallationInstructions()
            : base(UID)
        {
        }

        public override string CNT => "tdpCatInstallationInstructions";
        public override string CN => "安装说明";
        public override string DE => "Installationsanleitungen";
        public override string EN => "Installation Instructions";
        public override string ES => "Instrucciones de instalación";

        public GlobalDictionaries.TDPType GlobDictTDPType => GlobalDictionaries.TDPType.Installation_Descr;

        public SecuredDocs DfcSecurityDocType => SecuredDocs.TDP;
    }

    /// <summary>
    /// Interne Zertifikate- diese Dokumente dürfen nur von ATMO- Mitarbeitern eingesehen werden.
    /// </summary>
    public class KataCertificatesInternal
        : NamingBase, ITdpCat
    {

        public const long UID = 0xDB94AB08;

        public KataCertificatesInternal()
            : base(UID)
        {
        }

        public override string CNT => "tdpCatKataCertificatesInternal";
        public override string CN => "Kata：内部证书";
        public override string DE => "Kata: Interne Zertifikate ";
        public override string EN => "Kata: Internal certificates";
        public override string ES => "Kata: Certificados internos";

        public GlobalDictionaries.TDPType GlobDictTDPType => GlobalDictionaries.TDPType.Kata_Certificates_Internal;

        public SecuredDocs DfcSecurityDocType => SecuredDocs.TDP;
    }

    /// <summary>
    /// Hersteller- Erklärungen
    /// </summary>
    public class ManufacturerExplanations
        : NamingBase, ITdpCat
    {

        public const long UID = 0x40A5AE45;

        public ManufacturerExplanations()
            : base(UID)
        {
        }

        public override string CNT => "tdpCatKataCertificatesInternal";
        public override string CN => "厂商说明";
        public override string DE => "Herstellererklärungen";
        public override string EN => "Manufacturer explanations";
        public override string ES => "Explicaciones del fabricante";

        public GlobalDictionaries.TDPType GlobDictTDPType => GlobalDictionaries.TDPType.Manufacturers_explan;

        public SecuredDocs DfcSecurityDocType => SecuredDocs.TDP;
    }

    /// <summary>
    /// Hersteller- Erklärungen
    /// </summary>
    public class Software
        : NamingBase, ITdpCat
    {

        public const long UID = 0x4B806F67;

        public Software()
            : base(UID)
        {
        }

        public override string CNT => "tdpCatKataSoftware";
        public override string CN => "軟件";
        public override string DE => EN;
        public override string EN => "Software";
        public override string ES => EN;

        public GlobalDictionaries.TDPType GlobDictTDPType => GlobalDictionaries.TDPType.Software;

        public SecuredDocs DfcSecurityDocType => SecuredDocs.TDP;
    }

    /// <summary>
    /// Kalibrierungsanleitungen
    /// </summary>
    public class CalibrationInstructions
        : NamingBase, ITdpCat
    {

        public const long UID = 0x20EC2AB1;

        public CalibrationInstructions()
            : base(UID)
        {
        }

        public override string CNT => "tdpCatClibrationInstructions";
        public override string CN => "校准说明";
        public override string DE => "Kalibrierungsanleitungen";
        public override string EN => "Calibration instructions";
        public override string ES => "nstrucciones de calibración";

        public GlobalDictionaries.TDPType GlobDictTDPType => GlobalDictionaries.TDPType.Calibration_Instr;

        public SecuredDocs DfcSecurityDocType => SecuredDocs.TDP;
    }

    /// <summary>
    /// Kalibrierungsanleitungen
    /// </summary>
    public class CatalogeParts
        : NamingBase, ITdpCat
    {

        public const long UID = 0x19215220;

        public CatalogeParts()
            : base(UID)
        {
        }

        public override string CNT => "tdpCatCatalogeParts";
        public override string CN => "目录零件";
        public override string DE => "Katalogteile";
        public override string EN => "Catalog Parts";
        public override string ES => "Catálogo de piezas";

        public GlobalDictionaries.TDPType GlobDictTDPType => GlobalDictionaries.TDPType.Catalog_Parts;

        public SecuredDocs DfcSecurityDocType => SecuredDocs.TDP;
    }






}
