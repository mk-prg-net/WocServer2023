using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using mko.RPN;

using TTD = MKPRG.Naming.DocuTerms;

namespace MKPRG.Tracing.DocuTerms
{
    /// <summary>
    /// mko, 8.5.2018
    /// Kills DocuEntity in Parameterlist, if condition were not met.
    /// Used to implement optional parts in DocuEntity- trees.
    /// 
    /// mko, 16.6.2020
    /// Markiert für ausschließlichen Einsatz in Listen, Instanzmember- Listen und Methodenparameter- Listen
    /// 
    /// mko, 9.8.2021
    /// Alle Member der ursprünglich zu implementierenden Schnittstelle mko.IToken entfernt.
    /// </summary>
    public class KillIfNot 
        : DocuEntity,
        IKillIfNot        
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Condition">Result of evaluated condition</param>
        /// <param name="createDocuEntity"></param>
        public KillIfNot(bool Condition, Func<IListMember> createDocuEntity)
            : base(DocuEntityTypes.KillIfNot)
        {
            this.Condition = Condition;

            if(createDocuEntity != null)
                _CreateDocEntity = createDocuEntity;
        }

        public static InstanceWithNameAsNID _defaultValue = new InstanceWithNameAsNID(new NID(TTD.Composer.Errors.KillIfNotParamIsNull.UID));

        Func<IListMember> _CreateDocEntity = new Func<IListMember>(() => _defaultValue);

        public IListMember DocuEntity => _CreateDocEntity();

        public bool Condition { get; }

    }
}
