using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MKPRG.Tracing.DocuTerms
{
    /// <summary>
    /// mko, 27.11.2021
    /// *WildCards* sind **DocuTerme**, die für einen beliebigen Namen eines benannten 
    /// **DocuTerms** oder einen beliebigen Wert einer **DocuTerm**- Eigenschaft stehen.    /// 
    ///     
    /// SubTree- Constraint streng typisiert definiert durch die Eigenschaften
    /// - HasSubTreeConstraint { get; }
    /// - SubTreeConstraint { get; }
    /// 
    /// Ein SubTree Constraint macht z.B. bei *WildCard's* für **DocuTerm** Eigenschaften Sinn:
    /// Jeder Beliebige Wert ist als Eigenschaftswert zulässig, sofern er den definierten Subtree enthält.
    /// </summary>
    public interface IWildCard
        : IPropertyValue,
        IEventParameter,
        IReturnValue
    {

        /// <summary>
        /// True, wenn eine SubTree- Einschränkung gilt
        /// </summary>
        bool HasSubTreeConstraint { get; }

        IDocuEntity SubTreeConstraint { get; }
    }
}
