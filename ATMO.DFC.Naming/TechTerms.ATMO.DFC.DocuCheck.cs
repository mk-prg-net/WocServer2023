using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// mko, 5.3.2020
/// </summary>
namespace MKPRG.Naming.TechTerms.ATMO.DocuCheck
{

    /// <summary>
    /// mko, 2.7.2020
    /// Ein Dokument ist keiner Materialnummer zugeordnet
    /// </summary>
    public class DocuIdIsNotAssignedToMatNo
        : NamingBase
    {

        public const long UID = 0x8ED0CEF0;

        public DocuIdIsNotAssignedToMatNo()
            : base(UID)
        {
        }

        public override string CNT => "noDrawing";
        public override string CN => EN;
        public override string DE => "Einem Dokument ist keine Materialnummer zugewiesen";
        public override string EN => "DocId isn't associated to a MatNo";
        public override string ES => "No se asigna un número de material a un documento";
    }


    /// <summary>
    /// mko, 21.5.2019
    /// Zu einer Materialnummer gibt es keine Zeichnung (ok)
    /// </summary>
    public class NoDrawing
        : NamingBase
    {

        public const long UID = 0x98E4FCC7;

        public NoDrawing()
            : base(UID)
        {
        }

        public override string CNT => "noDrawing";
        public override string CN => EN;
        public override string DE => "Keine Zeichnung vorhanden";
        public override string EN => "No drawing available";
        public override string ES => "No hay dibujo disponible";
    }

    /// <summary>
    /// mko, 21.5.2019
    /// Zu einer Materialnummer gibt es eine ATD
    /// </summary>
    public class HasATD
    : NamingBase
    {

        public const long UID = 0x64E95B45;

        public HasATD()
            : base(UID)
        {
        }

        public override string CNT => "hasATD";
        public override string CN => EN;
        public override string DE => "Zeichnung vorhanden";
        public override string EN => "Drawing available";
        public override string ES => "Dibujo disponible";
    }

    /// <summary>
    /// mko, 21.5.2019
    /// Zu einer Materialnummer gibt es in der Path eine Zeichnung, jedoch ist das 
    /// Feld ZeichNr in der MaraPj leer.
    /// </summary>
    public class ATDExistsButNoDrawingNo
        : NamingBase
    {

        public const long UID = 0x892D5218;

        public ATDExistsButNoDrawingNo()
            : base(UID)
        {
        }

        public override string CNT => "atdWithoutDrawingNo";
        public override string CN => EN;
        public override string DE => "Zeichnung vorhanden, jedoch keine Zeichnungsnummer in MaraPJ definiert";
        public override string EN => "Drawing available, but no drawing number defined in MaraPJ";
        public override string ES => "Dibujo disponible, pero sin número de dibujo definido en MaraPJ";
    }

    /// <summary>
    /// mko, 21.5.2019
    /// Die ZeichnungsNr in der MaraPj ist nicht leer und ungleich der MatNr.
    /// Es wird auf eine Zeichnung aus einem anderen Projekt verwiesen (ATZ = Verweiszeichnung)
    /// </summary>
    public class HasATZ
    : NamingBase
    {

        public const long UID = 0x545F1AA3;

        public HasATZ()
            : base(UID)
        {
        }

        public override string CNT => "hasAtz";
        public override string CN => EN;
        public override string DE => "Hat eine Verweiszeichnung (ATZ)";
        public override string EN => "Has a reference drawing (ATZ)";
        public override string ES => "Tiene un dibujo de referencia (ATZ)";
    }

    /// <summary>
    /// mko, 21.5.2019
    /// Weder zur Materialnummer, noch zur Zeichnungsnummer existiert in der Path eine Zeichnung
    /// </summary>
    public class MissingDrawing
        : NamingBase
    {

        public const long UID = 0x7FCB7FED;

        public MissingDrawing()
            : base(UID)
        {
        }

        public override string CNT => "missingDrawing";
        public override string CN => EN;
        public override string DE => "Zeichnung fehlt";
        public override string EN => "missing drawing";
        public override string ES => "¡Falta el dibujo!";
    }

    /// <summary>
    /// mko, 21.5.2019
    /// Beim Eintragen einer Zeichnungsnummer zu einer Baugruppe/Projekt/Station 
    /// ist es zu einem Tippfehler gekommen, der vom DFC- Import erkannt wurde.
    /// Die Zeichnungsnummer wurde deshalb auf 0000000000 gesetzt, um den Fehler zu signalisieren.
    /// </summary>
    public class InvalidDrawingNo
    : NamingBase
    {

        public const long UID = 0xCCE10901;

        public InvalidDrawingNo()
            : base(UID)
        {
        }

        public override string CNT => "invalidDrawingNo";
        public override string CN => EN;
        public override string DE => "ungültige Zeichnungsnummer";
        public override string EN => "invalid drawing no.";
        public override string ES => "Número de dibujo inválido!";        
    }

    /// <summary>
    /// mko, 21.5.2019
    /// Neben einer Verweiszeichnung (Zeichnungsnummer) gibt es noch eine ATD (MatNr).
    /// Dies ist ein Fehler und der Verantwortliche sollte eine der beiden Zeichnungen löschen.
    /// </summary>
    public class RedundantAtdBesideAtz
        : NamingBase
    {

        public const long UID = 0xC65B8B91;

        public RedundantAtdBesideAtz()
            : base(UID)
        {
        }

        public override string CNT => "redundantAtdBesideAtz";
        public override string CN => EN;
        public override string DE => "Mehrdeutige Zeichnungsverweise: ATD und ATZ wurden definiert. Die ATD wird als gültig betrachtet.";
        public override string EN => "Ambiguous drawing references: ATD and ATZ were defined. The ATD is considered valid.";
        public override string ES => "Referencias de dibujo ambiguas: ATD y ATZ fueron definidos. El ATD se considera válido";        
    }

    /// <summary>
    /// mko, 18.7.2019
    /// Für Manuals wurden bereits DokuMat- Nummern in der Datenbank definiert, jedoch die Manuals
    /// selbst als Dokumente in der Path- Tabelle noch nicht hinterlegt.
    /// </summary>
    public class ForManualWithDefinedDokumatNumberDoesNotExistsADocumentInPath
    : NamingBase
    {

        public const long UID = 0x77C6F833;

        public ForManualWithDefinedDokumatNumberDoesNotExistsADocumentInPath()
            : base(UID)
        {
        }

        public override string CNT => "forManualWithDefinedDokumatNumberDoesNotExistsADocumentInPath";
        public override string CN => EN;
        public override string DE => "Das Dokument (PDF, ZIP) zu einem Manual mit definierter DokuMat- Nummern ist noch nicht in DFC hochgeladen worden.";
        public override string EN => "The document (PDF, ZIP) for a manual with defined DokuMat numbers has not yet been uploaded to DFC.";
        public override string ES => "El documento (PDF, ZIP) para un manual con números definidos de DokuMat aún no se ha subido al DFC.";
    }

    /// <summary>
    /// mko, 9.9.2019
    /// Ein neues Manual wurde angelegt, diesem wurde aber noch keine Dokumat- Nummer zugeweisen.
    /// </summary>
    public class NoDokuMatNoIsCurrentlyAssignedToManual
        : NamingBase
    {

        public const long UID = 0x615D8A7D;

        public NoDokuMatNoIsCurrentlyAssignedToManual()
            : base(UID)
        {
        }

        public override string CNT => "noDokuMatNoIsCurrentlyAssignedToManual";
        public override string CN => EN;
        public override string DE => "Für das hochgeladene Manual wurde noch keine DokuMat Nummer definiert.";
        public override string EN => "No DokuMat number has yet been defined for the uploaded manual.";
        public override string ES => "No se ha definido ningún número de DokuMat para el manual cargado.";

    }

}
