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
        /// mko, 24.6.2024
        /// Generische Implementierung.
        /// Holt den Wert aus einem Member eines komplexen Typs, der eine NID bezeichnet wird (z.B. Property einer Instanz).
        /// Falls der Member nicht existiert, wird False zurückgegeben und eine Fehlermeldung in ret.
        ///
        /// </summary>
        /// <param name="method"></param>
        /// <param name="propNameAsNid"></param>
        /// <param name="propVal"></param>
        /// <param name="ret"></param>
        /// <returns></returns>
        bool _TryGetPropFrom(IComplexType docuTerm, Func<IEnumerable<IMemberOfComplexType>> getMembers, long propNameAsNid, out IPropertyValue propVal, out IRet ret, out IRetBld retBld)                        
        {
            retBld = retBldFactory.CreateRetBld(pnL.p(TTD.MetaData.Arg.UID, docuTerm), pnL.p_NID(TTD.Types.Property.UID, propNameAsNid));
            ret = retBld.ReturnOK();

            var prop = getMembers()
                            .Select(r => r is IPropertyWithNameAsNID)?
                            .Cast<IPropertyWithNameAsNID>()?
                            .FirstOrDefault(r => r.DocuTermNid.NamingId == propNameAsNid) ?? default(IPropertyWithNameAsNID);

            if (prop == default(IPropertyWithNameAsNID))
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
        /// mko, 24.6.2024
        /// </summary>
        /// <param name="complexType"></param>
        /// <param name="propNameAsNid"></param>
        /// <param name="strPropVal"></param>
        /// <param name="ret"></param>
        /// <returns></returns>
        public bool TryGetPropFrom(IComplexType complexType, Func<IEnumerable<IMemberOfComplexType>> getMembers, long propNameAsNid, out string strPropVal, out IRet ret)
        {
            strPropVal = System.String.Empty;
            if(_TryGetPropFrom(complexType, getMembers, propNameAsNid, out IPropertyValue propVal, out ret, out IRetBld retBld)) 
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
        /// mko, 24.06.2024
        /// </summary>
        /// <param name="method"></param>
        /// <param name="propNameAsNid"></param>
        /// <param name="intPropVal"></param>
        /// <param name="ret"></param>
        /// <returns></returns>
        public bool TryGetPropFrom(IComplexType complexType, Func<IEnumerable<IMemberOfComplexType>> getMembers, long propNameAsNid, out long intPropVal, out IRet ret)
        {
            intPropVal = 0;
            if (_TryGetPropFrom(complexType, getMembers, propNameAsNid, out IPropertyValue propVal, out ret, out IRetBld retBld))
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

        /// <summary>
        /// mko, 24.6.2024
        /// </summary>
        /// <param name="instance"></param>
        /// <param name="propNameAsNid"></param>
        /// <param name="dblPropVal"></param>
        /// <param name="ret"></param>
        /// <returns></returns>
        public bool TryGetPropFrom(IComplexType complexType, Func<IEnumerable<IMemberOfComplexType>> getMembers, long propNameAsNid, out double dblPropVal, out IRet ret)
        {
            dblPropVal = 0.0;
            if (_TryGetPropFrom(complexType, getMembers, propNameAsNid, out IPropertyValue propVal, out ret, out IRetBld retBld))
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
        /// mko, 24.6.2024
        /// </summary>
        /// <param name="instance"></param>
        /// <param name="propNameAsNid"></param>
        /// <param name="intPropVal"></param>
        /// <param name="ret"></param>
        /// <returns></returns>
        public bool TryGetPropFrom(IComplexType complexType, Func<IEnumerable<IMemberOfComplexType>> getMembers, long propNameAsNid, out bool boolPropVal, out IRet ret)
        {
            boolPropVal = false;
            if (_TryGetPropFrom(complexType, getMembers, propNameAsNid, out IPropertyValue propVal, out ret, out IRetBld retBld))
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
        /// mko, 24.6.2024
        /// </summary>
        /// <param name="instance"></param>
        /// <param name="propNameAsNid"></param>
        /// <param name="nidPropVal"></param>
        /// <param name="ret"></param>
        /// <returns></returns>
        public bool TryGetPropFrom(IComplexType complexType, Func<IEnumerable<IMemberOfComplexType>> getMembers, long propNameAsNid, out INID nidPropVal, out IRet ret)
        {
            nidPropVal = new NID(TTD.Types.UndefinedNID.UID);
            if (_TryGetPropFrom(complexType, getMembers, propNameAsNid, out IPropertyValue propVal, out ret, out IRetBld retBld))
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
        /// mko, 24.6.2024
        /// </summary>
        /// <param name="instance"></param>
        /// <param name="propNameAsNid"></param>
        /// <param name="verPropVal"></param>
        /// <param name="ret"></param>
        /// <returns></returns>
        public bool TryGetPropFrom(IComplexType complexType, Func<IEnumerable<IMemberOfComplexType>> getMembers, long propNameAsNid, out IVer verPropVal, out IRet ret)
        {
            verPropVal = new Ver("0.0.0");
            if (_TryGetPropFrom(complexType, getMembers, propNameAsNid, out IPropertyValue propVal, out ret, out IRetBld retBld))
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
        /// mko, 24.6.2024
        /// </summary>
        /// <param name="instance"></param>
        /// <param name="propNameAsNid"></param>
        /// <param name="listPropVal"></param>
        /// <param name="ret"></param>
        /// <returns></returns>
        public bool TryGetPropFrom(IComplexType complexType, Func<IEnumerable<IMemberOfComplexType>> getMembers, long propNameAsNid, out IDTList listPropVal, out IRet ret)
        {
            listPropVal = pnL.L();
            if (_TryGetPropFrom(complexType, getMembers, propNameAsNid, out IPropertyValue propVal, out ret, out IRetBld retBld))
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

        /// <summary>
        /// mko, 24.6.2024
        /// </summary>
        /// <param name="instance"></param>
        /// <param name="propNameAsNid"></param>
        /// <param name="instancePropVal"></param>
        /// <param name="ret"></param>
        /// <returns></returns>
        public bool TryGetPropFrom(IComplexType complexType, Func<IEnumerable<IMemberOfComplexType>> getMembers, long propNameAsNid, out IInstance instancePropVal, out IRet ret)
        {
            instancePropVal = pnL.i(TTD.Types.UndefinedDocuTerm.UID);
            if (_TryGetPropFrom(complexType, getMembers, propNameAsNid, out IPropertyValue propVal, out ret, out IRetBld retBld))
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

        /// <summary>
        /// mko, 24.6.2024
        /// </summary>
        /// <param name="instance"></param>
        /// <param name="propNameAsNid"></param>
        /// <param name="methodPropVal"></param>
        /// <param name="ret"></param>
        /// <returns></returns>
        public bool TryGetPropFrom(IComplexType complexType, Func<IEnumerable<IMemberOfComplexType>> getMembers, long propNameAsNid, out IMethod methodPropVal, out IRet ret)
        {
            methodPropVal = pnL.m(TTD.Types.UndefinedDocuTerm.UID);
            if (_TryGetPropFrom(complexType, getMembers, propNameAsNid, out IPropertyValue propVal, out ret, out IRetBld retBld))
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
