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
    public class RC
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
        public static DocuTerms.IComposer pnL;

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


        internal RC(
                    bool succeeded, 
                    DateTime dat, 
                    string User, 
                    string Assembly, 
                    string TypeName, 
                    string FunctionName, 
                    IComposer pnL, 
                    IDocuEntity Message)
        {
            _succeeded = succeeded;
            _User = User;
            _Assembly = Assembly;
            _TypeName = TypeName;
            _FunctionName = FunctionName;
            _Message = Message;
            _dat = dat;
            RC.pnL = pnL;
        }


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
        public DocuTerms.IDocuEntity Message => _Message;

        public override string ToString()
        {
            return $"{StartTimeSingleton.TimeDifferenceToStartTimeInMs(LogDate).ToString("D9")} "
                  + $"{AssemblyName}.{TypeName}.{FunctionName} {(Succeeded ? "-> ok" : "-> failed!")} "
                  + (!string.IsNullOrWhiteSpace(Message) ? $": {Message}" : "");
        }


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

        public static RC Ok(IComposer pnL, IDocuEntity docuEntity, string User = "*")
        {
            var mth = new System.Diagnostics.StackTrace().GetFrame(1).GetMethod();
            var cls = mth.ReflectedType.Name;
            var assembly = mth.ReflectedType.Assembly.GetName().Name;

            return new RC(true, DateTime.Now, User, assembly, cls, mth.Name, pnL, docuEntity);
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

            return new RC(false, DateTime.Now, User, assembly, cls, mth.Name, pnL, pnL.NID(TT.Sets.None.UID));
        }

        public static RC Failed(IComposer pnL, IDocuEntity ErrorDescription, string User = "*")
        {
            var mth = new System.Diagnostics.StackTrace().GetFrame(1).GetMethod();
            var cls = mth.ReflectedType.Name;
            var assembly = mth.ReflectedType.Assembly.GetName().Name;

            return new RC(false, DateTime.Now, User, assembly, cls, mth.Name, pnL, ErrorDescription);
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

            //var fmt = new PNDocuTerms.DocuEntities.PNFormater(pnL);

            return new RCV3(false, DateTime.Now, User, assembly, cls, mth.Name, TraceHlp.FlattenExceptionMessagesPN(ex));
        }

        /// <summary>
        /// mko, 22.10.2018
        /// Erstellt einen RCV3 parametrisch.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="Succedeed"></param>
        /// <param name="ErrorDescription"></param>
        /// <param name="User"></param>
        /// <param name="inner"></param>
        /// <returns></returns>
        public static RCV3 Create(bool Succedeed = true, IDocuEntity ErrorDescription = null, string User = "*", RCV3 inner = null, [CallerMemberName] string caller = "")
        {
            var mth = new System.Diagnostics.StackTrace().GetFrame(1).GetMethod();
            var cls = mth.ReflectedType.Name;
            var assembly = mth.ReflectedType.Assembly.GetName().Name;

            return new RCV3(Succedeed, DateTime.Now, User, assembly, cls, caller, ErrorDescription, inner);
        }



        //internal RCV3(bool succeeded, DateTime dat, string User, string Assembly, string TypeName, string FunctionName, string Message, IRCV2 innerRC = null)            
        //{
        //    _Succeeded = succeeded;
        //    _User = User;
        //    _AssemblyName = Assembly;
        //    _TypeName = TypeName;
        //    _FunctionName = FunctionName;
        //    this.Message = Message;
        //    _LogDate = dat;
        //    _InnerRC = TranformToRCV3(innerRC);
        //}

        internal RCV3(bool succeeded, DateTime dat, string User, string Assembly, string TypeName, string FunctionName, IDocuEntity Message, IRCV2 innerRC = null)
        {
            _Succeeded = succeeded;
            _User = User;
            _AssemblyName = Assembly;
            _TypeName = TypeName;
            _FunctionName = FunctionName;
            _MessageEntity = Message;
            _LogDate = dat;
            _InnerRC = TranformToRCV3(innerRC);
        }

        public RCV3(RCV3 ori)
            : this(ori.Succeeded, ori.LogDate, ori.User, ori.AssemblyName, ori.TypeName, ori.FunctionName, ori.MessageEntity)
        {
        }

        public RCV3 Clone()
        {
            return new RCV3(Succeeded, LogDate, User, AssemblyName, TypeName, FunctionName, pnL.txt(Message));
        }


        /// <summary>
        /// mko, 25.7.2018
        /// Transforms objects with IRCV2 Values in RCV3- values recursivly. 
        /// Because _InnerRC ist of type RCV3, this "reconstruction" of RCV3 object with data from IRCV2 
        /// objects is necessary.
        /// </summary>
        /// <param name="rc"></param>
        /// <returns></returns>
        public static RCV3 TranformToRCV3(IRCV2 rc)
        {
            if (rc != null)
            {
                return new RCV3(
                    rc.Succeeded,
                    rc.LogDate,
                    rc.User,
                    rc.AssemblyName,
                    rc.TypeName,
                    rc.FunctionName,
                    pnL.txt(rc.Message),
                    rc.InnerRCV2 != null ? TranformToRCV3(rc.InnerRCV2) : null);
            }
            else
            {
                return null;
            }
        }

        public static RCV3 TranformToRCV3(RC<ParserV2.Result> rc)
        {
            if (rc != null)
            {
                IRCV2 inner = null;
                if (rc.InnerRCV2 != null)
                {
                    inner = new RCV3(true, rc.InnerRCV2.LogDate, rc.InnerRCV2.User, rc.InnerRCV2.AssemblyName, rc.InnerRCV2.TypeName, rc.InnerRCV2.FunctionName, pnL.txt(rc.InnerRCV2.Message));
                }

                return new RCV3(
                    rc.Succeeded,
                    rc.LogDate,
                    rc.User,
                    rc.AssemblyName,
                    rc.TypeName,
                    rc.FunctionName,
                    pnL.txt(rc.Message),
                    inner);
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
        public static RCV3 TranformToRCV3(RC<IToken[]> rc)
        {
            if (rc != null)
            {
                IRCV2 inner = null;
                if (rc.InnerRCV2 != null)
                {
                    inner = new RCV3(true, rc.InnerRCV2.LogDate, rc.InnerRCV2.User, rc.InnerRCV2.AssemblyName, rc.InnerRCV2.TypeName, rc.InnerRCV2.FunctionName, pnL.txt(rc.InnerRCV2.Message));
                }

                return new RCV3(
                    rc.Succeeded,
                    rc.LogDate,
                    rc.User,
                    rc.AssemblyName,
                    rc.TypeName,
                    rc.FunctionName,
                    pnL.txt(rc.Message),
                    inner);
            }
            else
            {
                return null;
            }
        }


        public RCV3()
        {
        }

        public virtual IDocuEntity ToPlx()
        {
            PNDocuTerms.DocuEntities.IDocuEntity de = null;

            // To leave the IRCV2 interface untouched type check for _InnerIRCv2 is needed 
            // Otherwise IRCV2 needs to be extended with IRCV2 InnerIRCv2 { get; } Property

            var details = pnL.i(ANC.DocuTerms.MetaData.Details.UID,
                pnL.p(ANC.TechTerms.Timeline.DateStamp.UID, pnL.date(LogDate)),
                pnL.KillIf(string.IsNullOrWhiteSpace(User), () => (IInstanceMember)pnL.p(ANC.TechTerms.Authentication.UserId.UID, User)),
                pnL.KillIf(MessageEntity == null, () => (IInstanceMember)pnL.p(ANC.DocuTerms.MetaData.Msg.UID, pnL.EncapsulateAsPropertyValue(MessageEntity))),
                pnL.KillIf(InnerRCV2 == null || !(InnerRCV2 is IRCV2), () => (IInstanceMember)pnL.p("inner", pnL.EncapsulateAsPropertyValue(InnerRCV2.ToPlx())))
                );

            de = pnL.i($"{AssemblyName}.{TypeName}",
                    pnL.m(FunctionName,
                        pnL.ret(
                            pnL.IfElse(Succeeded,
                                () => (IReturnValue)pnL.eSucceeded(details),
                                () => pnL.eFails(details)))));

            return de;
        }

        /// <summary>
        /// mko, 18.9.2018
        /// Creates from plx
        /// </summary>
        /// <param name="plx"></param>
        public static RCV3 Parse(IDocuEntity plx)
        {
            var rc = new RCV3();

            TraceHlp.ThrowArgExIfNot(plx.EntityType == DocuEntityTypes.Instance, "plx is not a instantce");
            TraceHlp.ThrowArgExIfNot(System.Text.RegularExpressions.Regex.IsMatch(plx.Name(), @"[\w\.\<\>]+\.[\w\<\>]+\.[\w\<\>]+$"), "plx instance name do not contains assembly.typename.functionname");
            {
                var parts = plx.Name().Split(new char[] { '.' }, StringSplitOptions.RemoveEmptyEntries);
                TraceHlp.ThrowArgExIfNot(parts.Length >= 3, "plx instance name is incomplete");
                rc._TypeName = parts[parts.Length - 2];
                rc._FunctionName = parts[parts.Length - 1];

                rc._AssemblyName = string.Join(".", parts.Take(parts.Length - 2));
            }

            TraceHlp.ThrowArgExIfNot(plx.HasValue(), "plx of RCV3 do not contains content");
            {
                rc._Succeeded = null != plx.FindNamedEntity(DocuEntityTypes.Event, "succeeded", 2);

                var dat = plx.FindNamedEntity(DocuEntityTypes.Property, "logDate", 2);
                if (null != dat)
                {
                    rc._LogDate = DateTime.Parse(dat.EntityValue().GetText());
                }

                var user = plx.FindNamedEntity(DocuEntityTypes.Property, "user", 2);
                if (null != user)
                {
                    rc._User = user.EntityValue().GetText();
                }

                var msg = plx.FindNamedEntity(DocuEntityTypes.Property, "msg", 2);
                if (null != msg)
                {
                    rc._MessageEntity = msg.EntityValue();
                }

                var inner = plx.FindNamedEntity(DocuEntityTypes.Property, "inner", 2);
                if (inner != null)
                {
                    rc._InnerRC = Parse(inner.EntityValue());
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
        public static RC<T> Ok(T value, string User = "*", string Message = "", RC inner = null)
        {
            var mth = new System.Diagnostics.StackTrace().GetFrame(1).GetMethod();
            var cls = mth.ReflectedType.Name;
            var assembly = mth.ReflectedType.Assembly.GetName().Name;

            return new RC<T>(true, value, DateTime.Now, User, assembly, cls, mth.Name, Message, inner);
        }

        /// <summary>
        /// Indicates a failed function call.
        /// </summary>
        /// <param name="User"></param>
        /// <param name="ErrorDescription"></param>
        /// <returns></returns>
        public static RC<T> Failed(T value, string User = "*", string ErrorDescription = "", RC inner = null)
        {
            var mth = new System.Diagnostics.StackTrace().GetFrame(1).GetMethod();
            var cls = mth.ReflectedType.Name;
            var assembly = mth.ReflectedType.Assembly.GetName().Name;

            return new RC<T>(false, value, DateTime.Now, User, assembly, cls, mth.Name, ErrorDescription, inner);
        }


        internal RC(bool succeeded, T value, DateTime dat, string User, string Assembly, string TypeName, string FunctionName, string Message, RC inner)
            : base(succeeded, dat, User, Assembly, TypeName, FunctionName, Message, inner)
        {
            _value = value;
        }

        T _value;

        public T Value => _value;

        public override string ToString()
        {
            return $"{StartTimeSingleton.TimeDifferenceToStartTimeInMs(LogDate).ToString("D9")} "
                  + $"{AssemblyName}.{TypeName}.{FunctionName} " + (Succeeded ? $"-> {Value}" : "-> failed!")
                  + (!string.IsNullOrWhiteSpace(Message) ? $": {Message}" : "");
        }
    }
}
