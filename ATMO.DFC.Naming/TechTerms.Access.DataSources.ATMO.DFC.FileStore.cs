using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// Beschreibung von Feldern und Strukturen der Path- Tabelle und des Filestores
/// </summary>
namespace MKPRG.Naming.TechTerms.Access.Datasources.ATMO.DFC.FileStore
{
    /// <summary>
    /// mko, 13.7.2020
    /// Klassifikation der Art und Weise der Ablage im DFC Filestore
    /// </summary>
    public class StorageType : NamingBase
    {
        public const long UID = 0x5C142FF5;

        public StorageType()
            : base(UID)
        {
        }

        public override string CNT => "storageType";
        public override string CN => EN;
        public override string DE => EN;
        public override string EN => "Storage Type";
        public override string ES => EN;
    }


    /// <summary>
    /// Allgemeine Bezeichnung der Felder in der Path- Tabelle, welche die Dateinamen zu einem Storagetype liefern
    /// </summary>
    public class StorageChamber : NamingBase
    {
        public const long UID = 0x63DD2838;

        public StorageChamber()
            : base(UID)
        {
        }

        public override string CNT => "storageChamber";
        public override string CN => "储藏室";
        public override string DE => "Speicherkammer";
        public override string EN => "Storage Chamber";
        public override string ES => "Cámara de almacenamiento";
    }

    /// <summary>
    /// Allgemeine Bezeichnung der Felder in der Path- Tabelle für die Aktualisierungszeiten
    /// </summary>
    public class StorageChamberLup : NamingBase
    {
        public const long UID = 0xC7D17BEF;

        public StorageChamberLup()
            : base(UID)
        {
        }

        public override string CNT => "storageChamberLup";
        public override string CN => "LUP的储存室";
        public override string DE => "Speicherkammer für Lup";
        public override string EN => "Storage Chamber for LUP";
        public override string ES => "Cámara de almacenamiento para LUP";
    }


}
