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
        /// Holt den Wert aus einer Eigenschaft, die durch eine NID bezeichnet ist. Falls die Eigenschaft nicht 
        /// existiert, wird False zurückgegeben und eine Fehlermeldung in ret.
        ///
        /// </summary>
        /// <param name="list"></param>
        /// <param name="propNameAsNid"></param>
        /// <param name="propVal"></param>
        /// <param name="ret"></param>
        /// <returns></returns>
        bool _TryGetPropFrom(IDTList list, long propNameAsNid, out IPropertyValue propVal, out IRet ret, out IRetBld retBld)
        {
            retBld = retBldFactory.CreateRetBld(pnL.p(TTD.MetaData.Arg.UID, list), pnL.p_NID(TTD.Types.Property.UID, propNameAsNid));
            ret = retBld.ReturnOK();

            var prop = list.ListMembers
                            .Select(r => r is IPropertyWithNameAsNID)?
                            .Cast<IPropertyWithNameAsNID>()?
                            .FirstOrDefault(r => r.DocuTermNid.NamingId == propNameAsNid) ?? default(IPropertyWithNameAsNID);

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

        /// <summary>
        /// mko, 20.6.2024
        /// </summary>
        /// <param name="list"></param>
        /// <param name="propNameAsNid"></param>
        /// <param name="strPropVal"></param>
        /// <param name="ret"></param>
        /// <returns></returns>
        public bool TryGetPropFrom(IDTList list, long propNameAsNid, out string strPropVal, out IRet ret)
        {
            strPropVal = System.String.Empty;
            if(_TryGetPropFrom(list, propNameAsNid, out IPropertyValue propVal, out ret, out IRetBld retBld)) 
            {
                if (propVal is IString str)
                {
                    strPropVal = str.ValueAsString;
                }
                else if (propVal is ITxt txt )
                {
                    strPropVal = System.String.Join(" ", txt.Words.Select(w => w.ValueAsString).ToArray());
                }
                else
                {
                    ret = retBld.DataInconsistencyOccured(pnL.m(TT.Operators.Sets.IsOfType.UID, pnL.p(TTD.MetaData.Arg.UID, "String"), pnL.ret(pnL.eFails())));
                }
            }
            return ret.ReturnedFromSuccessfulCall;
        }

        /// <summary>
        /// mko, 21.06.2024
        /// </summary>
        /// <param name="list"></param>
        /// <param name="propNameAsNid"></param>
        /// <param name="intPropVal"></param>
        /// <param name="ret"></param>
        /// <returns></returns>
        public bool TryGetPropFrom(IDTList list, long propNameAsNid, out long intPropVal, out IRet ret)
        {
            intPropVal = 0;
            if (_TryGetPropFrom(list, propNameAsNid, out IPropertyValue propVal, out ret, out IRetBld retBld))
            {
                if (propVal is Integer lng)
                {
                    intPropVal = lng.ValueAsLong;
                }
                else
                {                 
                    ret = retBld.DataInconsistencyOccured(pnL.m(TT.Operators.Sets.IsOfType.UID, pnL.p(TTD.MetaData.Arg.UID, "Integer"), pnL.ret(pnL.eFails())));
                }
            }
            return ret.ReturnedFromSuccessfulCall;
        }

        public bool TryGetPropFrom(IDTList list, long propNameAsNid, out double dblPropVal, out IRet ret)
        {
            dblPropVal = 0.0;
            if (_TryGetPropFrom(list, propNameAsNid, out IPropertyValue propVal, out ret, out IRetBld retBld))
            {
                if (propVal is Double dbl)
                {
                    dblPropVal = dbl.ValueAsDouble;
                }
                else
                {
                    ret = retBld.DataInconsistencyOccured(pnL.m(TT.Operators.Sets.IsOfType.UID, pnL.p(TTD.MetaData.Arg.UID, "Double"), pnL.ret(pnL.eFails())));
                }
            }
            return ret.ReturnedFromSuccessfulCall;
        }

        /// <summary>
        /// mko, 21.6.2024
        /// </summary>
        /// <param name="list"></param>
        /// <param name="propNameAsNid"></param>
        /// <param name="intPropVal"></param>
        /// <param name="ret"></param>
        /// <returns></returns>
        public bool TryGetPropFrom(IDTList list, long propNameAsNid, out bool boolPropVal, out IRet ret)
        {
            boolPropVal = false;
            if (_TryGetPropFrom(list, propNameAsNid, out IPropertyValue propVal, out ret, out IRetBld retBld))
            {
                if (propVal is Boolean b)
                {
                    boolPropVal = b.ValueAsBool;
                }
                else
                {
                    ret = retBld.DataInconsistencyOccured(pnL.m(TT.Operators.Sets.IsOfType.UID, pnL.p(TTD.MetaData.Arg.UID, "Boolean"), pnL.ret(pnL.eFails())));
                }
            }
            return ret.ReturnedFromSuccessfulCall;
        }

        /// <summary>
        /// mko, 21.6.2024
        /// </summary>
        /// <param name="list"></param>
        /// <param name="propNameAsNid"></param>
        /// <param name="nidPropVal"></param>
        /// <param name="ret"></param>
        /// <returns></returns>
        public bool TryGetPropFrom(IDTList list, long propNameAsNid, out INID nidPropVal, out IRet ret)
        {
            nidPropVal = new NID(TTD.Types.UndefinedNID.UID);
            if (_TryGetPropFrom(list, propNameAsNid, out IPropertyValue propVal, out ret, out IRetBld retBld))
            {
                if (propVal is INID nid)
                {
                    nidPropVal = nid;
                }
                else
                {
                    ret = retBld.DataInconsistencyOccured(pnL.m(TT.Operators.Sets.IsOfType.UID, pnL.p(TTD.MetaData.Arg.UID, "NID"), pnL.ret(pnL.eFails())));
                }
            }
            return ret.ReturnedFromSuccessfulCall;
        }


        /// <summary>
        /// mko, 21.6.2024
        /// </summary>
        /// <param name="list"></param>
        /// <param name="propNameAsNid"></param>
        /// <param name="verPropVal"></param>
        /// <param name="ret"></param>
        /// <returns></returns>
        public bool TryGetPropFrom(IDTList list, long propNameAsNid, out IVer verPropVal, out IRet ret)
        {
            verPropVal = new Ver("0.0.0");
            if (_TryGetPropFrom(list, propNameAsNid, out IPropertyValue propVal, out ret, out IRetBld retBld))
            {
                if (propVal is IVer v)
                {
                    verPropVal = v;
                }
                else
                {
                    ret = retBld.DataInconsistencyOccured(pnL.m(TT.Operators.Sets.IsOfType.UID, pnL.p(TTD.MetaData.Arg.UID, "Version"), pnL.ret(pnL.eFails())));
                }
            }
            return ret.ReturnedFromSuccessfulCall;
        }

        /// <summary>
        /// mko, 21.6.2024
        /// </summary>
        /// <param name="list"></param>
        /// <param name="propNameAsNid"></param>
        /// <param name="listPropVal"></param>
        /// <param name="ret"></param>
        /// <returns></returns>
        public bool TryGetPropFrom(IDTList list, long propNameAsNid, out IDTList listPropVal, out IRet ret)
        {
            listPropVal = pnL.L();
            if (_TryGetPropFrom(list, propNameAsNid, out IPropertyValue propVal, out ret, out IRetBld retBld))
            {
                if (propVal is IDTList l)
                {
                    listPropVal = l;
                }
                else
                {
                    ret = retBld.DataInconsistencyOccured(pnL.m(TT.Operators.Sets.IsOfType.UID, pnL.p(TTD.MetaData.Arg.UID, "List"), pnL.ret(pnL.eFails())));
                }
            }
            return ret.ReturnedFromSuccessfulCall;
        }

        public bool TryGetPropFrom(IDTList list, long propNameAsNid, out IInstance instancePropVal, out IRet ret)
        {
            instancePropVal = pnL.i(TTD.Types.UndefinedDocuTerm.UID);
            if (_TryGetPropFrom(list, propNameAsNid, out IPropertyValue propVal, out ret, out IRetBld retBld))
            {
                if (propVal is IInstance i)
                {
                    instancePropVal = i;
                }
                else
                {
                    ret = retBld.DataInconsistencyOccured(pnL.m(TT.Operators.Sets.IsOfType.UID, pnL.p(TTD.MetaData.Arg.UID, "Instance"), pnL.ret(pnL.eFails())));
                }
            }
            return ret.ReturnedFromSuccessfulCall;
        }

        public bool TryGetPropFrom(IDTList list, long propNameAsNid, out IMethod methodPropVal, out IRet ret)
        {
            methodPropVal = pnL.m(TTD.Types.UndefinedDocuTerm.UID);
            if (_TryGetPropFrom(list, propNameAsNid, out IPropertyValue propVal, out ret, out IRetBld retBld))
            {
                if (propVal is IMethod m)
                {
                    methodPropVal = m;
                }
                else
                {
                    ret = retBld.DataInconsistencyOccured(pnL.m(TT.Operators.Sets.IsOfType.UID, pnL.p(TTD.MetaData.Arg.UID, "IMethod"), pnL.ret(pnL.eFails())));
                }
            }
            return ret.ReturnedFromSuccessfulCall;
        }


    }
}
