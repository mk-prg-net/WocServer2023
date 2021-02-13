using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using mko.RPN;

namespace MKPRG.Tracing.DocuTerms
{
    /// <summary>
    /// mko, 7.2.2018
    /// </summary>
    public class DocuEntity : IDocuEntity
    {
        protected IFormater fmt;
        protected IReadOnlyDictionary<long, MKPRG.Naming.INaming> NC;

        public DocuEntity(
            IFormater fmt,
            DocuEntityTypes docEntityType, 
            params IDocuEntity[] childs )
        {            
            this.fmt = fmt;            
            EntityType = docEntityType;
            Childs = childs;
        }

        /// <summary>
        /// mko, 8.6.2020
        /// Zur Implementierung der Literale Boolean etc.
        /// </summary>
        /// <param name="fn"></param>
        /// <param name="docEntityType"></param>
        /// <param name="childs"></param>
        internal DocuEntity(
            IFormater fmt,
            DocuEntityTypes docEntityType)
        {
            this.fmt = fmt;
            EntityType = docEntityType;
            Childs = new IDocuEntity[] { };
        }

        //IFn fn;

        public DocuEntityTypes EntityType { get; }

        public bool IsFunctionName => true;

        public bool IsInteger => false;

        public bool IsBoolean => false;

        public bool IsNummeric => false;

        string IToken.Value => EntityType.ToString();

        public virtual int CountOfEvaluatedTokens => Childs.Sum(r => r.CountOfEvaluatedTokens) + 1;

        public virtual IEnumerable<IDocuEntity> Childs { get; }

        public IToken Copy()
        {
            return new DocuEntity(fmt, EntityType, Childs.ToArray());
        }

        public override string ToString()
        {            
            return fmt.Print(this);
        }

    }
}
