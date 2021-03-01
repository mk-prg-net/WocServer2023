using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ATMO.mko.QueryBuilder;

namespace DFC3.DB.Tables
{
    /// <summary>
    /// mko, 28.11.2018
    /// Einheitliche Schnittstelle für Path und PathView
    /// </summary>
    public interface IPath : ITable
    {
        ColName StorageType { get; }
        ColName Baselocation { get; }
        ColName DocId { get; }
        ColName XType { get; }
        ColName IterationNo { get; }
        ColName TDPTyp { get; }
        ColName ProjectNo { get; }
        ColName ProjectDescr { get; }
        ColName StationNo { get; }
        ColName MatNr { get; }
        ColName SyncLUP { get; }
        ColName UserState { get; }
        ColName CreatorId { get; }
        ColName CreationTime { get; }
        ColName StatusChangeOriginator { get; }
        ColName XPath { get; }
        ColName FS1 { get; }
        ColName FS2 { get; }
        ColName FS3 { get; }
        ColName FS4 { get; }
        ColName FS5 { get; }
        ColName FilePdfName { get; }
        ColName FilePdfSize { get; }
        ColName FileName2 { get; }
        ColName FileName3 { get; }
        ColName File2Size { get; }

        // mko, 12.4.2019
        /// <summary>
        /// Datum letzte Aktualisierung der PDF- Datei
        /// </summary>
        ColName FilePDFLup { get; }

        /// <summary>
        /// Datum letzte Aktualisierung der Datei in Kammer 2
        /// </summary>
        ColName File2Lup { get; }

        /// <summary>
        /// Datum letzte Aktualisierung der Datei in Kammer 3
        /// </summary>
        ColName File3Lup { get; }

        ColName Infos { get; }

        // mko, 27.11.2018
        ColName LatestVersion { get; }

    }
}
