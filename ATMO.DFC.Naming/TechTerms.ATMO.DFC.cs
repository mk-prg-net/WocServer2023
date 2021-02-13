using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DFCSecurity;
using DZAUtilities_Dictionaries;

/// <summary>
/// mko, 5.3.2020
/// 
/// mko, 2.9.2020
/// Schnittstelle ISAPDocType hinzugefügt und implementiert
/// </summary>
namespace MKPRG.Naming.TechTerms.ATMO.DFC
{

    /// <summary>
    /// mko, 2.9.2020
    /// Definiert den bis dato verwendeten SAP- Dokument- Typ enum. 
    /// Mit dieser Schnittele werden Naming Container erweitert, die SAP Dokument- und Stücklistentypen bezeichnen.
    /// </summary>
    public interface IDfcDocTypes
    {
        GlobalDictionaries.DocTypeSAP SAPDocType { get; }
        SecuredDocs DfcSecurityDocType { get; }
    }

    /// <summary>
    /// mko, 22.6.2020
    /// </summary>
    public class ApplicationNameDFCClient
     : NamingBase
    {

        public const long UID = 0x664AAB73;

        public ApplicationNameDFCClient()
            : base(UID)
        {
        }


        public override string CNT => "DFC2";
        public override string CN => CNT;
        public override string DE => CNT;
        public override string EN => CNT;
        public override string ES => CNT;
    }

    public class SAPDocType
     : NamingBase
    {

        public const long UID = 0xB6A682CE;

        public SAPDocType()
            : base(UID)
        {
        }

        public override string CNT => "docType";
        public override string CN => EN;
        public override string DE => "SAP Dokument Typ";
        public override string EN => "SAP Document Type";
        public override string ES => "Clase de documento SAP";
    }

    public class Assy
     : NamingBase,
        IDfcDocTypes
    {

        public const long UID = 0x9AB269DA;

        public Assy()
            : base(UID)
        {
        }

        public override string CNT => "assy";
        public override string CN => "装配";
        public override string DE => "Baugruppe";
        public override string EN => "Assembly group";
        public override string ES => "Asamblea";

        public GlobalDictionaries.DocTypeSAP SAPDocType => GlobalDictionaries.DocTypeSAP.BauGruppe;

        public SecuredDocs DfcSecurityDocType => SecuredDocs.Assy;

        public override string Glyph => Glyphs.DFC.Assy;
    }

    /// <summary>
    /// mko, 14.9.2020
    /// </summary>
    public class MechAssy
        : NamingBase,
        IDfcDocTypes
    {

        public const long UID = 0xCE8EAC8C;

        public MechAssy()
            : base(UID)
        {
        }

        public override string CNT => "mechAssy";
        public override string CN => EN;
        public override string DE => "Baugruppe der Mechanik- Konstruktion";
        public override string EN => "Assembly of the mechanical construction";
        public override string ES => "Montaje de la construcción mecánica";

        public GlobalDictionaries.DocTypeSAP SAPDocType => GlobalDictionaries.DocTypeSAP.BauGruppe;

        public SecuredDocs DfcSecurityDocType => SecuredDocs.Assy;

        public override string Glyph =>Glyphs.DFC.Assy;
    }

    /// <summary>
    /// mko, 14.9.2020
    /// </summary>
    public class ElectAssy
        : NamingBase,
        IDfcDocTypes
    {

        public const long UID = 0xAB059BD4;

        public ElectAssy()
            : base(UID)
        {
        }

        public override string CNT => "electAssy";
        public override string CN => EN;
        public override string DE => "Baugruppe der Eletro- Konstruktion";
        public override string EN => "Assembly of the electrical construction";
        public override string ES => "Montaje de la construcción eléctrica";

        public GlobalDictionaries.DocTypeSAP SAPDocType => GlobalDictionaries.DocTypeSAP.BauGruppe;

        public SecuredDocs DfcSecurityDocType => SecuredDocs.Assy;

        public override string Glyph => Glyphs.DFC.Assy;
    }



    public class SinglePart
        : NamingBase,
        IDfcDocTypes
    {

        public const long UID = 0x12E68CDF;

        public SinglePart()
            : base(UID)
        {
        }

        public override string CNT => "singlePart";
        public override string CN => "项目";
        public override string DE => "Einzelteil";
        public override string EN => "single Part";
        public override string ES => "Artículo";

        public GlobalDictionaries.DocTypeSAP SAPDocType => GlobalDictionaries.DocTypeSAP.Einzelteile;

        public SecuredDocs DfcSecurityDocType => SecuredDocs.SingelPart;

        public override string Glyph => Glyphs.DFC.SinglePart;
    }

    public class MechSinglePart
        : NamingBase,
    IDfcDocTypes
    {

        public const long UID = 0xC075B7B1;

        public MechSinglePart()
            : base(UID)
        {
        }

        public override string CNT => "mechSinglePart";
        public override string CN => EN;
        public override string DE => "Einzelteil der Mechanik- Konstruktion";
        public override string EN => "Part of the mechanical construction";
        public override string ES => "Componente de la construcción mecánica";

        public GlobalDictionaries.DocTypeSAP SAPDocType => GlobalDictionaries.DocTypeSAP.Einzelteile;

        public SecuredDocs DfcSecurityDocType => SecuredDocs.SingelPart;

        public override string Glyph => Glyphs.DFC.SinglePart;
    }


    public class ElectSinglePart
        : NamingBase,
    IDfcDocTypes
    {

        public const long UID = 0xC150DA61;

        public ElectSinglePart()
            : base(UID)
        {
        }

        public override string CNT => "electSinglePart";
        public override string CN => EN;
        public override string DE => "Einzelteil der Elektro- Konstruktion";
        public override string EN => "Part of the electrical construction";
        public override string ES => "Componente de la construcción eléctrica";

        public GlobalDictionaries.DocTypeSAP SAPDocType => GlobalDictionaries.DocTypeSAP.Einzelteile;

        public SecuredDocs DfcSecurityDocType => SecuredDocs.SingelPart;

        public override string Glyph => Glyphs.DFC.SinglePart;
    }

    /// <summary>
    /// Hydraulik- Einzelteil (von Moehwald als MAterialklasse definiert, seit 29.9.2020 
    /// aus dem SAP Klassifizierungsschema entfernt)
    /// </summary>
    public class HSinglePart
    : NamingBase

    {

        public const long UID = 0x4C04B651;

        public HSinglePart()
            : base(UID)
        {
        }

        public override string CNT => "HSinglePart";
        public override string CN => EN;
        public override string DE => EN;
        public override string EN => "HSinglePart";
        public override string ES => EN;

        public override string Glyph => Glyphs.DFC.SinglePart;
    }




    public class BOM
        : NamingBase
    {

        public const long UID = 0xDE72C5BB;

        public BOM()
            : base(UID)
        {
        }

        public override string CNT => "bom";
        public override string CN => EN;
        public override string DE => "Stückliste";
        public override string EN => "Bill of materials";
        public override string ES => "Lista de piezas";

        public override string Glyph => Glyphs.DFC.ATB;
    }

    public class BOMType
        : NamingBase
    {

        public const long UID = 0xBF9C6DB4;

        public BOMType()
            : base(UID)
        {
        }

        public override string CNT => "bomType";
        public override string CN => CNT;
        public override string DE => "Stücklistenart";
        public override string EN => CNT;
        public override string ES => CNT;
    }

    public class BOMTypeME
     : NamingBase
    {

        public const long UID = 0xEE434561;

        public BOMTypeME()
            : base(UID)
        {
        }

        public override string CNT => "bomTypeMechanical";
        public override string CN => EN;
        public override string DE => "Stückliste der Mechanik- Konstruktion";
        public override string EN => "BOM of mechanical Engineering";
        public override string ES => EN;

        public override string Glyph => Glyphs.DFC.MechBom;
    }

    public class BOMTypeEL
        : NamingBase
    {

        public const long UID = 0xB970156E;

        public BOMTypeEL()
            : base(UID)
        {
        }

        public override string CNT => "bomTypeElectrical";
        public override string CN => EN;
        public override string DE => "Stückliste der Elektrokonstruktion";
        public override string EN => "BOM of electrical engineering";
        public override string ES => EN;

        public override string Glyph => Glyphs.DFC.ElectroBom;
    }


    public class BOMPosList
        : NamingBase
    {

        public const long UID = 0xDD2BBEEE;

        public BOMPosList()
            : base(UID)
        {
        }

        public override string CNT => "bomPosList";
        public override string CN => EN;
        public override string DE => "Liste der Stücklistenpositionen";
        public override string EN => "List of BOM- positions";
        public override string ES => "Lista de artículos de la lista de materiales";
    }


    public class BOMPos
    : NamingBase
    {

        public const long UID = 0xE663E16E;

        public BOMPos()
            : base(UID)
        {
        }

        public override string CNT => "bomPos";
        public override string CN => EN;
        public override string DE => "Position";
        public override string EN => "Position";
        public override string ES => "Artículo";
    }

    public class BOMPosNo
        : NamingBase
    {

        public const long UID = 0xCE185093;

        public BOMPosNo()
            : base(UID)
        {
        }

        public override string CNT => "bomPosNo";
        public override string CN => EN;
        public override string DE => "Positionsnummer";
        public override string EN => "Position number";
        public override string ES => "Número de posición";
    }



    public class PSPNo
        : NamingBase
    {

        public const long UID = 0xF27CA628;

        public PSPNo()
            : base(UID)
        {
        }

        public override string CNT => "pspNo";
        public override string CN => EN;
        public override string DE => "PSP Nr.";
        public override string EN => "PSP No.";
        public override string ES => "PSP No.";
    }

    public class PSPNoLevel
    : NamingBase
    {

        public const long UID = 0x3D62A178;

        public PSPNoLevel()
            : base(UID)
        {
        }

        public override string CNT => "pspLevel";
        public override string CN => CNT;
        public override string DE => "PSP Ebene";
        public override string EN => CNT;
        public override string ES => CNT;
    }

    public class ProjectList
        : NamingBase
    {

        public const long UID = 0x8D526F44;

        public ProjectList()
            : base(UID)
        {
        }

        public override string CNT => "prjList";
        public override string CN => EN;
        public override string DE => "Projekte";
        public override string EN => "Projects";
        public override string ES => "Proyectos";
    }


    public class Project
    : NamingBase,
        IDfcDocTypes
    {

        public const long UID = 0x9831AD76;

        public Project()
            : base(UID)
        {
        }

        public override string CNT => "prj";
        public override string CN => "项目介绍";
        public override string DE => "Projekt";
        public override string EN => "Project";
        public override string ES => "Proyecto";

        public GlobalDictionaries.DocTypeSAP SAPDocType => GlobalDictionaries.DocTypeSAP.Projekt;

        public SecuredDocs DfcSecurityDocType => SecuredDocs.Project;

        public override string Glyph => Glyphs.DFC.Project;
    }

    /// <summary>
    /// mko, 14.9.2020
    /// </summary>
    public class ServiceAssignedToProject
        : NamingBase,
        IDfcDocTypes
    {

        public const long UID = 0xFFF2E168;

        public ServiceAssignedToProject()
            : base(UID)
        {
        }

        public override string CNT => "srvAssignedToPrj";
        public override string CN => EN;
        public override string DE => "Dienstleistung, die Projekt zugeordnet ist";
        public override string EN => "Service assigned to Project";
        public override string ES => "Servicio asignado al proyecto";

        public GlobalDictionaries.DocTypeSAP SAPDocType => GlobalDictionaries.DocTypeSAP.none;

        public SecuredDocs DfcSecurityDocType => SecuredDocs.Project;
    }

    /// <summary>
    /// mko, 14.9.2020
    /// </summary>
    public class Packaging
        : NamingBase
    {

        public const long UID = 0x1E6D58E0;

        public Packaging()
            : base(UID)
        {
        }

        public override string CNT => "packaging";
        public override string CN => EN;
        public override string DE => "Aufgewendetes Verpackungsmaterial für Lieferungen an Kunden";
        public override string EN => "Packaging material used for deliveries to customers";
        public override string ES => "El material de embalaje utilizado para las entregas a los clientes";
    }


    public class ProjectNo
    : NamingBase
    {

        public const long UID = 0x23EF3685;

        public ProjectNo()
            : base(UID)
        {
        }

        public override string CNT => "prjNo";
        public override string CN => EN;
        public override string DE => "Projektnummer";
        public override string EN => "Project number";
        public override string ES => "Número de proyecto";
    }


    public class StationList
        : NamingBase
    {

        public const long UID = 0x5B457A8D;

        public StationList()
            : base(UID)
        {
        }

        public override string CNT => "statList";
        public override string CN => EN;
        public override string DE => "Stationen";
        public override string EN => "Stations";
        public override string ES => "Estaciónes";
    }



    public class Station
        : NamingBase,
        IDfcDocTypes
    {

        public const long UID = 0xB590578E;

        public Station()
            : base(UID)
        {
        }

        public override string CNT => "stat";
        public override string CN => "车站";
        public override string DE => "Station";
        public override string EN => "Station";
        public override string ES => "Estación";

        public GlobalDictionaries.DocTypeSAP SAPDocType => GlobalDictionaries.DocTypeSAP.Station;

        public SecuredDocs DfcSecurityDocType => SecuredDocs.Station;

        public override string Glyph => Glyphs.DFC.Station;
    }

    /// <summary>
    /// Stationsnummern im DFC- Tree
    /// </summary>
    public class StationNo
        : NamingBase
    {

        public const long UID = 0x4C6D043;

        public StationNo()
            : base(UID)
        {
        }

        public override string CNT => "statNo";
        public override string CN => EN;
        public override string DE => "Stationsnummer";
        public override string EN => "Station number";
        public override string ES => "Número de estación";
    }

    public class ProcessModulList
        : NamingBase
    {

        public const long UID = 0x1124960F;

        public ProcessModulList()
            : base(UID)
        {
        }

        public override string CNT => "procModList";
        public override string CN => EN;
        public override string DE => "Prozessmodule";
        public override string EN => "Process moduls";
        public override string ES => "Lista de módulos de proceso";
    }


    public class ProcessModul
        : NamingBase,
        IDfcDocTypes
    {

        public const long UID = 0x6186C4C6;

        public ProcessModul()
            : base(UID)
        {
        }

        public override string CNT => "procMod";
        public override string CN => "流程模块";
        public override string DE => "Prozessmodul";
        public override string EN => "Process modul";
        public override string ES => "Módulo de proceso";

        public GlobalDictionaries.DocTypeSAP SAPDocType => GlobalDictionaries.DocTypeSAP.FlexCon;

        public SecuredDocs DfcSecurityDocType => SecuredDocs.FlexCon;

        public override string Glyph => Glyphs.DFC.Processmodule;
    }

    public class ProcessmoduleNo
        : NamingBase
    {

        public const long UID = 0x15667E7D;

        public ProcessmoduleNo()
            : base(UID)
        {
        }

        public override string CNT => "procModNo";
        public override string CN => CNT;
        public override string DE => "Prozessmodulnummer";
        public override string EN => "Process module number";
        public override string ES => "Número del módulo de proceso";
    }

    public class MatNo
    : NamingBase
    {

        public const long UID = 0xDF9AB106;

        public MatNo()
            : base(UID)
        {
        }

        public override string CNT => "matNo";
        public override string CN => EN;
        public override string DE => "Mat Nr.";
        public override string EN => "Mat No.";
        public override string ES => "Mat No.";
    }

    public class DrawingNo
        : NamingBase
    {

        public const long UID = 0x7DDBC29A;

        public DrawingNo()
            : base(UID)
        {
        }

        public override string CNT => "drawingNo";
        public override string CN => EN;
        public override string DE => "Zeichnungsnummer";
        public override string EN => "Drawing number";
        public override string ES => "El número del dibujo";
    }


    public class DocuMatNo
    : NamingBase
    {

        public const long UID = 0x4863B2FA;

        public DocuMatNo()
            : base(UID)
        {
        }

        public override string CNT => "docuMatNo";
        public override string CN => EN;
        public override string DE => "DokuMat Nr.";
        public override string EN => "DokuMat No.";
        public override string ES => "DokuMAt No.";
    }

    public class DocId
        : NamingBase
    {

        public const long UID = 0x3BCFBE2D;

        public DocId()
            : base(UID)
        {
        }

        public override string CNT => "docId";
        public override string CN => EN;
        public override string DE => EN;
        public override string EN => "DocId";
        public override string ES => EN;
    }

    public class Document
        : NamingBase
    {

        public const long UID = 0xD42AEE26;

        public Document()
            : base(UID)
        {
        }

        public override string CNT => "doc";
        public override string CN => EN;
        public override string DE => "Dokument";
        public override string EN => "Document";
        public override string ES => "Documento";
    }

    /// <summary>
    /// mko, 13.7.2020
    /// Status des Dokumentes
    /// </summary>
    public class DocUserState
        : NamingBase
    {

        public const long UID = 0x6F8AFCA2;

        public DocUserState()
            : base(UID)
        {
        }

        public override string CNT => "docUserState";
        public override string CN => EN;
        public override string DE => "Dokumentstatus";
        public override string EN => "Document user state";
        public override string ES => "Estado del documento";
    }


    public class DocSecurityFeature
        : NamingBase
    {

        public const long UID = 0x1C4CC5D4;

        public DocSecurityFeature()
            : base(UID)
        {
        }

        public override string CNT => "docSecF";
        public override string CN => EN;
        public override string DE => "Sicherheitsmerkmale eines Dokumentes";
        public override string EN => "security features of a document";
        public override string ES => "características de seguridad de un documento";
    }

    public class DfcSecurityDocType
    : NamingBase
    {

        public const long UID = 0xCC205F7C;

        public DfcSecurityDocType()
            : base(UID)
        {
        }

        public override string CNT => "docSecFDocType";
        public override string CN => EN;
        public override string DE => "Dokumentenklassifizierung bezüglich des Zugriffsschutzes";
        public override string EN => "Document classification regarding access protection";
        public override string ES => "Clasificación de documentos en relación con la protección del acceso";
    }

    /// <summary>
    /// Standortfreigabe als Standardbaugruppe
    /// </summary>
    public class ZAT
        : NamingBase
    {

        public const long UID = 0xCAB35592;

        public ZAT()
            : base(UID)
        {
        }

        public override string CNT => "zat";
        public override string CN => EN;
        public override string DE => "ZAT: Standortfreigabe als Standardbaugruppe";
        public override string EN => "ZAT: Location release as standard assembly group";
        public override string ES => "ZAT: Liberación de la localización como módulo estándar";
    }


    /// <summary>
    /// 9.7.2020
    /// </summary>
    public class ATB
        : NamingBase,
        IDfcDocTypes
    {
        public const long UID = 0xD768DB94;

        public ATB()
            : base(UID)
        {
        }

        public override string CNT => "ATB";
        public override string CN => EN;
        public override string DE => "Stückliste (ATB)";
        public override string EN => "Bill of material (ATB)";
        public override string ES => "Lista de piezas (ATB)";

        public GlobalDictionaries.DocTypeSAP SAPDocType => GlobalDictionaries.DocTypeSAP.ATB;

        public SecuredDocs DfcSecurityDocType => SecuredDocs.ATB;

        public override string Glyph => Glyphs.DFC.ATB;
    }

    /// <summary>
    /// 9.7.2020
    /// </summary>
    public class DesignData
        : NamingBase

    {
        public const long UID = 0xA21BD76C;

        public DesignData()
            : base(UID)
        {
        }

        public override string CNT => "designData";
        public override string CN => EN;
        public override string DE => "Entwurfsunterlagen";
        public override string EN => "Design documents";
        public override string ES => "Documentos de diseño";

    }



    /// <summary>
    /// 9.7.2020
    /// </summary>
    public class ATD
        : NamingBase,
        IDfcDocTypes
    {

        public const long UID = 0x7D9A16C1;

        public ATD()
            : base(UID)
        {
        }

        public override string CNT => "ATD";
        public override string CN => EN;
        public override string DE => "Zeichnung (ATD)";
        public override string EN => "Drawing (ATD)";
        public override string ES => "Dibujo (ATD)";

        public GlobalDictionaries.DocTypeSAP SAPDocType => GlobalDictionaries.DocTypeSAP.ATD;

        public SecuredDocs DfcSecurityDocType => SecuredDocs.ATD;

        public override string Glyph => Glyphs.DFC.ATD;
    }

    /// <summary>
    /// 22.7.2020
    /// Zusammenbauzeichnung
    /// </summary>
    public class ATDB
        : NamingBase,
        IDfcDocTypes
    {

        public const long UID = 0x1B9FAA9E;

        public ATDB()
            : base(UID)
        {
        }

        public override string CNT => "ATDB";
        public override string CN => EN;
        public override string DE => "Zusammenbauzeichnung (ATDB)";
        public override string EN => "Assembly drawing (ATDB)";
        public override string ES => "Dibujo de ensamblaje (ATDB)";

        public GlobalDictionaries.DocTypeSAP SAPDocType => GlobalDictionaries.DocTypeSAP.ATD;

        public SecuredDocs DfcSecurityDocType => SecuredDocs.ATDB;

        public override string Glyph => Glyphs.DFC.ATD;
    }


    /// <summary>
    /// 9.7.2020
    /// </summary>
    public class ATZ
        : NamingBase,
        IDfcDocTypes
    {

        public const long UID = 0x683FB0D4;

        public ATZ()
            : base(UID)
        {
        }

        public override string CNT => "ATZ";
        public override string CN => EN;
        public override string DE => "Verweiszeichnung (ATZ)";
        public override string EN => "Reference drawing (ATZ)";
        public override string ES => "Dibujo de referencia (ATZ)";

        public GlobalDictionaries.DocTypeSAP SAPDocType => GlobalDictionaries.DocTypeSAP.ATDATZ;

        public SecuredDocs DfcSecurityDocType => SecuredDocs.ATD;

        public override string Glyph => Glyphs.DFC.ATZ;
    }

    /// <summary>
    /// mko, 9.7.2020
    /// </summary>
    public class AT3
        : NamingBase,
        IDfcDocTypes
    {

        public const long UID = 0x5568FB4A;

        public AT3()
            : base(UID)
        {
        }

        public override string CNT => "AT3";
        public override string CN => EN;
        public override string DE => "3D Modell (AT3)";
        public override string EN => "3D model (AT3)";
        public override string ES => "Modelo 3D (AT3)";

        public GlobalDictionaries.DocTypeSAP SAPDocType => GlobalDictionaries.DocTypeSAP.AT3;

        public SecuredDocs DfcSecurityDocType => SecuredDocs.ATD;

        public override string Glyph => Glyphs.DFC.AT3;
    }


    /// <summary>
    /// 9.7.2020
    /// </summary>
    public class CAT
        : NamingBase,
        IDfcDocTypes
    {

        public const long UID = 0x3F3CA340;

        public CAT()
            : base(UID)
        {
        }

        public override string CNT => "CAT";
        public override string CN => EN;
        public override string DE => "Katalogteile (CAT)";
        public override string EN => "Catálogo de piezas (CAT)";
        public override string ES => EN;

        public GlobalDictionaries.DocTypeSAP SAPDocType => GlobalDictionaries.DocTypeSAP.CAT;

        public SecuredDocs DfcSecurityDocType => SecuredDocs.CAT;        
    }

    /// <summary>
    /// 9.7.2020
    /// </summary>
    public class MAN
        : NamingBase,
        IDfcDocTypes
    {

        public const long UID = 0xA373FCE8;

        public MAN()
            : base(UID)
        {
        }

        public override string CNT => "MAN";
        public override string CN => EN;
        public override string DE => EN;
        public override string EN => "Manual (MAN)";
        public override string ES => EN;

        public GlobalDictionaries.DocTypeSAP SAPDocType => GlobalDictionaries.DocTypeSAP.MAN;

        public SecuredDocs DfcSecurityDocType => SecuredDocs.MAN;
        
    }


    /// <summary>
    /// SAP- Dokumententyp für technische Dokumentation.
    /// </summary>
    public class TDP
        : NamingBase,
        IDfcDocTypes
    {

        public const long UID = 0x35DC288C;

        public TDP()
            : base(UID)
        {
        }

        public override string CNT => "tdp";
        public override string CN => EN;
        public override string DE => "Technische Dokumentation";
        public override string EN => "Technical Documentation";
        public override string ES => "Documentación Técnica";

        public GlobalDictionaries.DocTypeSAP SAPDocType => GlobalDictionaries.DocTypeSAP.TDP;

        public SecuredDocs DfcSecurityDocType => SecuredDocs.TDP;

        public override string Glyph => Glyphs.DFC.TDP;
    }

    /// <summary>
    /// Oderner der technischen Dokumentationen
    /// </summary>
    public class TDPFolder
        : NamingBase
    {

        public const long UID = 0xA9830DE9;

        public TDPFolder()
            : base(UID)
        {
        }

        public override string CNT => "tdpFolder";
        public override string CN => EN;
        public override string DE => "Technische Dokumentationen (TDP's)";
        public override string EN => "Collection of technical documentation (TDP's)";
        public override string ES => "Recopilación de documentación técnica (TDP's)";
    }


    /// <summary>
    /// Klassifizierung von technischen Dokumentationen
    /// </summary>
    public class TDPGroup
        : NamingBase
    {

        public const long UID = 0xDDA3C8B6;

        public TDPGroup()
            : base(UID)
        {
        }

        public override string CNT => "tdpCat";
        public override string CN => EN;
        public override string DE => "TDP Kategorie";
        public override string EN => "TDP category";
        public override string ES => "Categoría TDP";
    }

    /// <summary>
    /// 9.7.2020
    /// </summary>
    public class EPlan
        : NamingBase,
        IDfcDocTypes
    {

        public const long UID = 0x96F250A9;

        public EPlan()
            : base(UID)
        {
        }

        public override string CNT => "EPlan";
        public override string CN => EN;
        public override string DE => "Elektrischer Schaltplan (EPlan)";
        public override string EN => "Electrical diagram (EPlan)";
        public override string ES => "Diagrama eléctrico (EPlan)";

        public GlobalDictionaries.DocTypeSAP SAPDocType => GlobalDictionaries.DocTypeSAP.ECA;

        public SecuredDocs DfcSecurityDocType => SecuredDocs.EPlan;

        public override string Glyph => Glyphs.DFC.Eplan;
    }


    /// <summary>
    /// 9.7.2020
    /// </summary>
    public class CTS
        : NamingBase,
        IDfcDocTypes
    {

        public const long UID = 0xDFC2A5C2;

        public CTS()
            : base(UID)
        {
        }

        public override string CNT => "CTS";
        public override string CN => EN;
        public override string DE => EN;
        public override string EN => "Cycle Time Sequencer (CTS)";
        public override string ES => EN;

        public GlobalDictionaries.DocTypeSAP SAPDocType => GlobalDictionaries.DocTypeSAP.CTS;

        public SecuredDocs DfcSecurityDocType => SecuredDocs.CTS;

        public override string Glyph => Glyphs.DFC.CTS;
    }

    /// <summary>
    /// 2.9.2020
    /// </summary>
    public class SFCFolder
        : NamingBase        
    {
        public const long UID = 0xFCFA9DAE;

        public SFCFolder()
            : base(UID)
        {
        }

        public override string CNT => "sfcFolder";
        public override string CN => EN;
        public override string DE => "Werkstattänderungen (SFC's)";
        public override string EN => "Shop floor changes (SFC's)";
        public override string ES => EN;

        public override string Glyph => Glyphs.Tools.Wrench;
    }


    /// <summary>
    /// 9.7.2020
    /// </summary>
    public class SFC
        : NamingBase,
        IDfcDocTypes
    {

        public const long UID = 0x148B8994;

        public SFC()
            : base(UID)
        {
        }

        public override string CNT => "SFC";
        public override string CN => EN;
        public override string DE => "Werkstattänderung (SFC)";
        public override string EN => "Shop floor change (SFC)";
        public override string ES => EN;

        public GlobalDictionaries.DocTypeSAP SAPDocType => GlobalDictionaries.DocTypeSAP.SFC;

        public SecuredDocs DfcSecurityDocType => SecuredDocs.SFC;

        public override string Glyph => Glyphs.Tools.Wrench;
    }


    /// <summary>
    /// 2.9.2020
    /// </summary>
    public class EDCFolder
        : NamingBase
        
    {

        public const long UID = 0x3F762A4A;

        public EDCFolder()
            : base(UID)
        {
        }

        public override string CNT => "edcFolder";
        public override string CN => EN;
        public override string DE => "Konstruktionsänderungen (EDC's)";
        public override string EN => "Engineering design changes (EDC's)";
        public override string ES => "Cambios de diseño";

        public override string Glyph => Glyphs.Tools.Wrench;


    }


    /// <summary>
    /// 9.7.2020
    /// </summary>
    public class EDC
        : NamingBase,
        IDfcDocTypes
    {

        public const long UID = 0xF0ADD701;

        public EDC()
            : base(UID)
        {
        }

        public override string CNT => "EDC";
        public override string CN => EN;
        public override string DE => "Konstruktionsänderung (EDC)";
        public override string EN => "Engineering design change (EDC)";
        public override string ES => "Cambio de diseño de ingeniería";

        public GlobalDictionaries.DocTypeSAP SAPDocType => GlobalDictionaries.DocTypeSAP.EDC;

        public SecuredDocs DfcSecurityDocType => SecuredDocs.EDC;

        public override string Glyph => Glyphs.Tools.Wrench;
    }

    /// <summary>
    /// mko, 13.7.2020
    /// Klasse der Ersatz- und Verschleissteile
    /// </summary>
    public class MatClassEVWP
        : NamingBase
    {

        public const long UID = 0x4E828824;

        public MatClassEVWP()
            : base(UID)
        {
        }

        public override string CNT => "matClassEVWP";
        public override string CN => EN;
        public override string DE => "Ersatz- Verschleißteil oder Werkzeug (EVWP)";
        public override string EN => "Spare part (EVWP)";
        public override string ES => "Pieza de repuesto, pieza de desgaste o herramienta (EVWP)";

        public override string Glyph => Glyphs.Tools.Screw;
    }

    /// <summary>
    /// mko, 13.7.2020
    /// Klasse der Standardbaugruppen
    /// </summary>
    public class MatClassStandardAssy
        : NamingBase
    {

        public const long UID = 0xA07984B1;

        public MatClassStandardAssy()
            : base(UID)
        {
        }

        public override string CNT => "matClassStandardAssy";
        public override string CN => EN;
        public override string DE => "Standardbaugruppe (STDBG)";
        public override string EN => "Standard assembly (STDBG)";
        public override string ES => "Ensamblaje estándar (STDBG)";
    }

    /// <summary>
    /// mko, 13.7.2020
    /// Klasse der dokumentationsrelevanten Baugruppen
    /// </summary>
    public class MatClassDocuEnabled
    : NamingBase
    {

        public const long UID = 0x702E57D5;

        public MatClassDocuEnabled()
            : base(UID)
        {
        }

        public override string CNT => "matClassDocuEnabled";
        public override string CN => EN;
        public override string DE => "Dokuhaken gesetzt";
        public override string EN => "is relevant for documentation";
        public override string ES => "es relevante para la documentación";
    }


    /// <summary>
    /// mko, 13.7.2020
    /// Klasse der dokumentationsrelevanten Baugruppen
    /// </summary>
    public class MatClassPublicForAll
    : NamingBase
    {

        public const long UID = 0xB6117089;

        public MatClassPublicForAll()
            : base(UID)
        {
        }

        public override string CNT => "matClassPublicforAll";
        public override string CN => EN;
        public override string DE => "einsehbar für alle DFC Benutzer";
        public override string EN => "public for all DFC Users";
        public override string ES => "público para todos los usuarios de DFC";
    }






}
