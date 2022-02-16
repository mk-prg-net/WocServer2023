using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using mko.RPN;

using TT = MKPRG.Naming.TechTerms;
using TTD = MKPRG.Naming.DocuTerms;

namespace MKPRG.Tracing.DocuTerms.Parser
{
    /// <summary>
    /// mko, 23.7.2021
    /// 
    /// mko, 6.8.2021 
    /// _defaultValue neu implementiert
    /// </summary>
    public class PropertyToken
        : DocuTermToken,
        IProperty,
        IMethodParameterToken,
        IInstanceMemberToken,
        IListMemberToken

    {
        public PropertyToken(IPropertyValueToken propertyValue)
            : base(DocuEntityTypes.Property)
        {
            if (propertyValue != null)
                PropertyValueToken = propertyValue;
        }

        public IPropertyValue PropertyValue => PropertyValueToken;

        public IPropertyValueToken PropertyValueToken { get; } = _defaultValue;

        public override int CountOfEvaluatedTokens => PropertyValueToken.CountOfEvaluatedTokens;

        public IPropertyValue DocuTermDefaultValue => _defaultValue;

        public bool IsSetToDefaultValue => PropertyValueToken is NIDToken nid && nid.NamingId == _defaultValue.NamingId;

        protected static NIDToken _defaultValue = new NIDToken(TTD.Types.UndefinedPropertyValue.UID);
        //public IDocuEntity DocuTermDefaultValue  => new NIDToken(TTD.Types.UndefinedPropertyValue.UID);
    }
}
