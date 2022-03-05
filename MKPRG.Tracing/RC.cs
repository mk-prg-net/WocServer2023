using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Runtime.Serialization;
using System.Runtime.CompilerServices;

using mko.Logging;
using mko.RPN;
using MKPRG.Tracing.DocuTerms;

using ANC = MKPRG.Naming;
using TT = MKPRG.Naming.TechTerms;
using TTD = MKPRG.Naming.DocuTerms;
using System.Diagnostics;

namespace MKPRG.Tracing
{
    /// <summary>
    /// mko, 2.11.2017
    /// Enhanced RC
    /// 
    /// mko, 13.2.2021
    /// Umgezogen in Projekt MKPRG.Tracing.
    /// InnerException entfernt.
    /// MessageDocuTerm hinzugefügt.
    /// </summary>
    public class RC: ISucceeded, ITraceInfo
    {

        /// <summary>
        /// mko, 15.6.2020
        /// Globaler Naming- Container
        /// </summary>
        public static IReadOnlyDictionary<long, ANC.INaming> NC;

        /// <summary>
        /// mko
        /// Globaler Composer
        /// </summary>
        public static IComposer pnL;

        /// <summary>
        /// mko
        /// globaler PN- Formatter
        /// </summary>
        public static PNFormater fmtPN;

        /// <summary>
        /// mko, 15.6.2020
        /// Statischer Konstruktur wurde notwendig, um die genaue Zeitliche Reihenfolge der Initialisierung der
        /// Formater und Composer zu ermöglichen.
        /// </summary>
        static RC()
        {
            NC = new ANC.Tools().GetNamingContainerAsConcurrentDict("MKPRG.Naming");
            fmtPN = new PNFormater(DocuTerms.Parser.Fn._, NC, ANC.Language.CNT);
            pnL = new DocuTerms.Composer(fmtPN);
        }


        //internal RC(
        //            bool succeeded, 
        //            DateTime dat, 
        //            string User, 
        //            string Assembly, 
        //            string TypeName, 
        //            string FunctionName, 
        //            IComposer pnL, 
        //            IDocuEntity Message)
        //{
        //    _succeeded = succeeded;
        //    _User = User;
        //    _Assembly = Assembly;
        //    _TypeName = TypeName;
        //    _FunctionName = FunctionName;
        //    _Message = Message;
        //    _dat = dat;
        //    RC.pnL = pnL;
        //}


        [DataMember(Name = "Succeeded")]
        bool _succeeded = false;

        [DataMember(Name = "LogDate")]
        DateTime _dat;

        [DataMember(Name = "User")]
        string _User;

        [DataMember(Name = "AssemblyName")]
        string _Assembly;

        [DataMember(Name = "TypeName")]
        string _TypeName;

        [DataMember(Name = "FunctionName")]
        string _FunctionName;

        IDocuEntity _Message;

        /// <summary>
        /// If true, then function call was successful
        /// </summary>
        [Newtonsoft.Json.JsonIgnore]
        public bool Succeeded => _succeeded;

        /// <summary>
        /// Date when function call ended.
        /// </summary>
        [Newtonsoft.Json.JsonIgnore]
        public DateTime LogDate => _dat;

        /// <summary>
        /// User, who calls the function
        /// </summary>
        [Newtonsoft.Json.JsonIgnore]
        public string User => _User;

        /// <summary>
        /// Assembly were function is defined
        /// </summary>        
        [Newtonsoft.Json.JsonIgnore]
        public string AssemblyName => _Assembly;

        /// <summary>
        /// Type were function is definied
        /// </summary>
        [Newtonsoft.Json.JsonIgnore]
        public string TypeName => _TypeName;

        /// <summary>
        /// Name of function
        /// </summary>
        [Newtonsoft.Json.JsonIgnore]
        public string FunctionName => _FunctionName;

        [Newtonsoft.Json.JsonIgnore]
        public IDocuEntity Message => _Message;

        /// <summary>
        /// Indicates a successful function call.
        /// </summary>
        /// <param name="User"></param>
        /// <returns></returns>
        public static RC Ok(IComposer pnL, string User = "*")
        {
            var mth = new System.Diagnostics.StackTrace().GetFrame(1).GetMethod();
            var cls = mth.ReflectedType.Name;
            var assembly = mth.ReflectedType.Assembly.GetName().Name;

            return new RC(true, DateTime.Now, User, assembly, cls, mth.Name, pnL.NID(TT.Sets.None.UID));
        }

        public static RC Ok(IDocuEntity docuEntity, string User = "*")
        {
            var mth = new System.Diagnostics.StackTrace().GetFrame(1).GetMethod();
            var cls = mth.ReflectedType.Name;
            var assembly = mth.ReflectedType.Assembly.GetName().Name;

            return new RC(true, DateTime.Now, User, assembly, cls, mth.Name, docuEntity);
        }


        /// <summary>
        /// Indicates a failed function call.
        /// </summary>
        /// <param name="User"></param>
        /// <param name="ErrorDescription"></param>
        /// <returns></returns>
        public static RC Failed(IComposer pnL, string User = "*")
        {
            var mth = new System.Diagnostics.StackTrace().GetFrame(1).GetMethod();
            var cls = mth.ReflectedType.Name;
            var assembly = mth.ReflectedType.Assembly.GetName().Name;

            return new RC(false, DateTime.Now, User, assembly, cls, mth.Name, pnL.NID(TT.Sets.None.UID));
        }

        public static RC Failed(IDocuEntity ErrorDescription, string User = "*")
        {
            var mth = new System.Diagnostics.StackTrace().GetFrame(1).GetMethod();
            var cls = mth.ReflectedType.Name;
            var assembly = mth.ReflectedType.Assembly.GetName().Name;

            return new RC(false, DateTime.Now, User, assembly, cls, mth.Name, ErrorDescription);
        }


        /// <summary>
        /// mko, 04.05.2018
        /// </summary>
        /// <param name="ex"></param>
        /// <param name="User"></param>
        /// <returns></returns>
        public static RC Failed(Exception ex, string User = "*")
        {
            var mth = new System.Diagnostics.StackTrace().GetFrame(1).GetMethod();
            var cls = mth.ReflectedType.Name;
            var assembly = mth.ReflectedType.Assembly.GetName().Name;

            //var fmt = new PNFormater(pnL);

            return new RC(false, DateTime.Now, User, assembly, cls, mth.Name, TraceHlp.FlattenExceptionMessagesPN(ex));
        }

        /// <summary>
        /// mko, 22.10.2018
        /// Erstellt einen RC parametrisch.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="Succedeed"></param>
        /// <param name="ErrorDescription"></param>
        /// <param name="User"></param>
        /// <param name="inner"></param>
        /// <returns></returns>
        public static RC Create(bool Succedeed = true, IDocuEntity ErrorDescription = null, string User = "*", RC inner = null, [CallerMemberName] string caller = "")
        {
            var mth = new System.Diagnostics.StackTrace().GetFrame(1).GetMethod();
            var cls = mth.ReflectedType.Name;
            var assembly = mth.ReflectedType.Assembly.GetName().Name;

            return new RC(Succedeed, DateTime.Now, User, assembly, cls, caller, ErrorDescription);
        }

        internal RC(bool succeeded, DateTime dat, string User, string Assembly, string TypeName, string FunctionName, IDocuEntity Message)
        {
            _succeeded = succeeded;
            _User = User;
            _Assembly = Assembly;
            _TypeName = TypeName;
            _FunctionName = FunctionName;
            _Message = Message;
            _dat = dat;
        }

        public RC(RC ori)
            : this(ori.Succeeded, ori.LogDate, ori.User, ori.AssemblyName, ori.TypeName, ori.FunctionName, ori.Message)
        {
        }

        public RC Clone()
        {
            return new RC(Succeeded, LogDate, User, AssemblyName, TypeName, FunctionName, Message);
        }



        public static RC TranformToRC(RC<ParserV2.Result> rc)
        {
            if (rc != null)
            {
                return new RC(
                    rc.Succeeded,
                    rc.LogDate,
                    rc.User,
                    rc.AssemblyName,
                    rc.TypeName,
                    rc.FunctionName,
                    rc.Message);
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// mko, 15.11.2018
        /// </summary>
        /// <param name="rc"></param>
        /// <returns></returns>
        public static RC TranformToRC(RC<IToken[]> rc, IComposer pnL)
        {
            if (rc != null)
            {
                return new RC(
                    rc.Succeeded,
                    rc.LogDate,
                    rc.User,
                    rc.AssemblyName,
                    rc.TypeName,
                    rc.FunctionName,                    
                    rc.Message);
            }
            else
            {
                return null;
            }
        }


        public RC()
        {
        }

        public const char PlxAssemblyTypeNameSeparator = '+';


        public virtual IDocuEntity ToPlx()
        {
            IInstance plx = pnL.i(TTD.Types.UndefinedDocuTerm.UID);

            try
            {

                var details = pnL.i(TTD.MetaData.Details.UID,
                        // mko, 15.3.2021
                        pnL.p(TT.Timeline.DateStamp.UID, pnL.date(LogDate)),
                        pnL.p(TT.Authentication.UserId.UID, User),
                        pnL.p(TTD.MetaData.Msg.UID, pnL.EncapsulateAsPropertyValue(Message)));


                var instanceName = string.IsNullOrWhiteSpace(AssemblyName) ? "AssemblynameIsNull" : AssemblyName;
                instanceName += string.IsNullOrWhiteSpace(TypeName) ? $"{PlxAssemblyTypeNameSeparator}TypenameIsNull" : $"{PlxAssemblyTypeNameSeparator}{TypeName}";

                plx = pnL.i(instanceName,
                            pnL.m(string.IsNullOrWhiteSpace(FunctionName) ? "FunctionnameIsNull" : FunctionName,
                                pnL.ret(
                                    pnL.IfElseRet(Succeeded,
                                        () => pnL.eSucceeded(details),
                                        () => pnL.eFails(details)))));
            }
            catch (Exception ex)
            {
                Debug.Write("RCV3.ToPlx throws an Exception");
                Debug.WriteLine(ex);
            }

            return plx;
        }

        /// <summary>
        /// mko, 18.9.2018
        /// Creates from plx
        /// </summary>
        /// <param name="plx"></param>
        public static RC Parse(IInstance rcv3AsDocuTerm)
        {
            var rc = new RC();

            /// Grundaufbau prüfen
            TraceHlp.ThrowArgExIfNot(
                pnL.i(pnL._n,
                    pnL.m(pnL._n,
                        pnL.ret(pnL._v()))).IsSubTreeOf(rcv3AsDocuTerm, false),

                pnL.ReturnAfterFailureWithDetails("Parse",
                    pnL.List(
                        pnL.p_NID(TTD.StateDescription.WhatsUp.UID, TTD.Parser.Errors.ParseRCfromDocuTerm_BaseStructureInstanceMethodReturnExpected.UID))));


            string AssemblyTypeNameregExPattern = @"[\w\.\<\>]+\" + PlxAssemblyTypeNameSeparator + @"[\w\<\>]+$";
            // Namen der instanz extrahieren
            TraceHlp.ThrowArgExIfNot(
                System.Text.RegularExpressions.Regex.IsMatch(rcv3AsDocuTerm.Name(), AssemblyTypeNameregExPattern),
                pnL.ReturnAfterFailureWithDetails("Parse",
                    pnL.List(
                        pnL.p_NID(TTD.StateDescription.WhatsUp.UID, TTD.Parser.Errors.ParseRCfromDocuTerm_InstanceNameDoesNotContainAssemblyAndClassName.UID))));

            {
                var parts = rcv3AsDocuTerm.Name().Split(new char[] { '+' }, StringSplitOptions.RemoveEmptyEntries);

                TraceHlp.ThrowArgExIfNot(
                    parts.Length >= 2,
                    pnL.ReturnAfterFailureWithDetails("Parse",
                        pnL.List(
                            pnL.p_NID(TTD.StateDescription.WhatsUp.UID, TTD.Parser.Errors.ParseRCfromDocuTerm_InstanceNameIsIncomplete.UID))));

                rc._TypeName = parts[parts.Length - 1];
                rc._Assembly = string.Join(".", parts.Take(parts.Length - 1));
            }

            var getMethod = pnL.m(pnL._n).AsSubTreeOf(rcv3AsDocuTerm, pnL);
            if (!getMethod.Succeeded)
            {
                TraceHlp.ThrowArgEx(
                    pnL.i(TTD.MetaData.Details.UID,
                        pnL.p_NID(TTD.StateDescription.WhatsUp.UID, TTD.Parser.Errors.Property_ChildIsNotValidPropertyValue.UID),
                        pnL.p(TTD.StateDescription.Why.UID, pnL.EncapsulateAsPropertyValue(getMethod.ToPlx()))));
            }
            else
            {
                {
                    var method = getMethod.Value.subTree;
                    rc._FunctionName = method.Name();

                    rc._succeeded = pnL.m(pnL._n, pnL.ret(pnL.eSucceeded())).IsSubTreeOf(method, false);


                    IInstance details = null;
                    if (rc.Succeeded)
                    {
                        var retDetails = pnL.ret(pnL.eSucceeded(pnL.i(TTD.MetaData.Details.UID))).AsSubTreeOf(method, pnL).ValueOrException.subTree;
                        details = (IInstance)pnL.i(TTD.MetaData.Details.UID).AsSubTreeOf(retDetails, pnL).ValueOrException.subTree;
                    }
                    else
                    {
                        var retDetails = pnL.ret(pnL.eFails(pnL.i(TTD.MetaData.Details.UID))).AsSubTreeOf(method, pnL).ValueOrException.subTree;
                        details = (IInstance)pnL.i(TTD.MetaData.Details.UID).AsSubTreeOf(retDetails, pnL).ValueOrException.subTree;
                    }

                    var getLogDate = pnL.p(TT.Timeline.DateStamp.UID, pnL._v()).AsSubTreeOf(details, pnL);
                    if (!getLogDate.Succeeded)
                    {
                        // LogDate muss vorhanden sein
                        TraceHlp.ThrowArgEx(
                            pnL.i(TTD.MetaData.Details.UID,
                                pnL.p_NID(TTD.StateDescription.WhatsUp.UID, TTD.Parser.Errors.ParseRCfromDocuTerm_LogDateMissing.UID),
                                pnL.p(TTD.StateDescription.Why.UID, pnL.EncapsulateAsPropertyValue(getLogDate.ToPlx()))));
                    }
                    else if (getLogDate.Value.subTree is IProperty propLogDat && propLogDat.PropertyValue is IDate logDat)
                    {
                        rc._dat = new DateTime(logDat.Year, logDat.Month, logDat.Day);
                    }
                    else
                    {
                        rc._dat = new DateTime(1900, 1, 1);
                    }

                    var getUser = pnL.p(TT.Authentication.UserId.UID, pnL._v()).AsSubTreeOf(details, pnL);
                    if (getUser.Succeeded)
                    {
                        var userProp = (IProperty)getUser.Value.subTree;
                        rc._User = userProp.PropertyValue.GetText();
                    }

                    var getMsg = pnL.p(TTD.MetaData.Msg.UID, pnL._v()).AsSubTreeOf(details, pnL);
                    if (getMsg.Succeeded)
                    {
                        var msgEntity = (IProperty)getMsg.Value.subTree;
                        rc._Message = msgEntity.PropertyValue;
                    }
                }
            }

            return rc;
        }

        public override string ToString()
        {
            return ToPlx().ToString();
        }

    }

    public class RC<T> : RC, ISucceeded, ITraceInfo
    {
        /// <summary>
        /// Indicates a successful function call.
        /// </summary>
        /// <param name="User"></param>
        /// <returns></returns>
        public static RC<T> Ok(T value, string User = "*", IDocuEntity Message = null)
        {
            var mth = new System.Diagnostics.StackTrace().GetFrame(1).GetMethod();
            var cls = mth.ReflectedType.Name;
            var assembly = mth.ReflectedType.Assembly.GetName().Name;

            return new RC<T>(true, value, DateTime.Now, User, assembly, cls, mth.Name, Message);
        }

        /// <summary>
        /// mko, 1.8.2019
        /// Erstellt aus den Daten eines Objekts, dass die Schnittstelle IRCV3sV implementiert, ein RCV3sV- Objket. 
        /// Wird zur Serialisierung benötig. Die Json- Serialisierung funktioniert nur für konkrete Typen.
        /// </summary>
        /// <param name="ret"></param>
        public RC(RC<T> ret)
            : base(ret)
        {
            _value = ret._value;
        }

        public RC(mko.Logging.RC<T> mkoRc)
            : base(mkoRc.Succeeded, mkoRc.LogDate, mkoRc.User, mkoRc.AssemblyName, mkoRc.TypeName, mkoRc.FunctionName, mkoRc.)



        /// <summary>
        /// Indicates a failed function call.
        /// </summary>
        /// <param name="User"></param>
        /// <param name="ErrorDescription"></param>
        /// <returns></returns>
        public static RC<T> Failed(T value, string User = "*", IDocuEntity ErrorDescription = null)
        {
            var mth = new System.Diagnostics.StackTrace().GetFrame(1).GetMethod();
            var cls = mth.ReflectedType.Name;
            var assembly = mth.ReflectedType.Assembly.GetName().Name;

            return new RC<T>(false, value, DateTime.Now, User, assembly, cls, mth.Name, ErrorDescription);
        }


        internal RC(bool succeeded, T value, DateTime dat, string User, string Assembly, string TypeName, string FunctionName, IDocuEntity Message)
            : base(succeeded, dat, User, Assembly, TypeName, FunctionName, Message)
        {
            _value = value;
        }

        T _value;

        public T Value => _value;

        public T ValueOrException
            => Succeeded ? _value : throw new RCException(this.ToPlx());


        public override string ToString()
        {
            return $"{StartTimeSingleton.TimeDifferenceToStartTimeInMs(LogDate).ToString("D9")} "
                  + $"{AssemblyName}.{TypeName}.{FunctionName} " + (Succeeded ? $"-> {Value}" : "-> failed!")
                  + (Message != null ? $": {RC.fmtPN.Print(Message)}" : "");
        }
    }
}
