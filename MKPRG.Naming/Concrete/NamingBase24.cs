using MKPRG.Naming.DocuTerms.Formatting.Errors;
using MKPRG.SemanticNet;
using System;
using System.Collections.Generic;
using System.Text;

namespace MKPRG.Naming
{
    /// <summary>
    /// mkk, 1.4.2024
    /// Neuere, vereinfachte Basisklasse für Namenscontainer mit implementierten semantischen 
    /// Referenzen.
    /// </summary>
    public class NamingBase24
        : INaming, ISemRefList
    {
        public long ID => NID;

        public virtual string CNT => GetType().Name;

        public long NID { get; } = DocuTerms.Types.UndefinedNID.UID;

        public ISemRef[] SemanticRelations { get; } = Array.Empty<ISemRef>();

        public virtual string NameIn(Language lng)
        {
            if (lng == Language.CN && this is ILangCN cn)
                return cn.CN;
            else if (lng == Language.ES && this is ILangES es)
                return es.ES;
            else if (lng == Language.DE && this is ILangDE de)
                return de.DE;
            else if (lng == Language.EN && this is ILangEN en)
                return en.EN;
            else return CNT;
        }

        public NamingBase24(long NID)
        {
            this.NID = NID;
        }

        public NamingBase24(long NID, params ISemRef[] semanticRelations)
        {
            this.NID = NID;
            this.SemanticRelations = semanticRelations;
        }

        /// <summary>
        /// mko, 26.1.2021
        /// Als Standard wird ein geschütztes Leerraumzeichen ausgegeben.
        /// </summary>
        public virtual string Glyph => "&nbsp;";
    }
}
