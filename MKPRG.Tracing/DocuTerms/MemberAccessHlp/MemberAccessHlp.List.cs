using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using TT = MKPRG.Naming.TechTerms;
using TTD = MKPRG.Naming.DocuTerms;

namespace MKPRG.Tracing.DocuTerms.MemberAccessHlp
{
    partial class MemberAccessHlp
    {
        /// <summary>
        /// mko, 13.6.2024
        /// </summary>
        /// <param name="list"></param>
        /// <param name="propNameAsNid"></param>
        /// <param name="propVal"></param>
        /// <param name="ret"></param>
        /// <returns></returns>
        public bool TryGetPropFrom(IDTList list, long propNameAsNid, out IPropertyValue propVal, out IRet ret)
        {
            var retBld = retBldFactory.CreateRetBld(pnL.p(TTD.MetaData.Arg.UID, list));
            ret = retBld.ReturnOK();

            var prop = list.ListMembers.Select(r => r is IPropertyWithNameAsNID).Cast<IPropertyWithNameAsNID>().FirstOrDefault(r => r.DocuTermNid.NamingId == propNameAsNid);
            if (prop == default(IListMember))
            {
                propVal = pnL.integer(0L);
                ret = retBld.DataInconsistencyOccured(pnL.m(TT.Operators.Sets.Exists.UID,
                                                            pnL.p_NID(TTD.Types.Property.UID, propNameAsNid),
                                                            pnL.ret(pnL.eFails())));
            }
            else
            {
                propVal = prop.PropertyValue;
            }

            return ret.ReturnedFromSuccessfulCall;
        }

        public bool TryGetPropFrom(IDTList list, long propNameAsNid, out string strPropVal, out IRet ret)
        {
            var retBld = retBldFactory.CreateRetBld(pnL.p(TTD.MetaData.Arg.UID, list));
            ret = retBld.ReturnOK();

            if(TryGetPropFrom(list, propNameAsNid, out IPropertyValue propVal, out ret))
            {
                if(propVal is IString str)
                {
                    strPropVal = str.ValueAsString;
                }
                else if(propVal is ITxt txt)

            }

            return ret.ReturnedFromSuccessfulCall;
        }
    }
}
