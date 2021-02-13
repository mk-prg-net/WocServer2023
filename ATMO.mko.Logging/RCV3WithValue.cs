using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ATMO.mko.Logging.PNDocuTerms.DocuEntities;
using mko.Logging;
using mko.RPN;

using System.Runtime.Serialization;

using System.Runtime.CompilerServices;

using ANC = MKPRG.Naming;

namespace ATMO.mko.Logging
{
    /// <summary>
    /// mko, 25.7.2018
    /// 
    /// mko, 2.7.2019
    /// Erweiter um die Eigenschaft ValueOrException. Diese ermöglicht einen sicheren Zugriff auf den Wert in einem 
    /// funktionalen Kontext. Sollte der Rückgabewert anstatt eines Wertes einen Fehler darstellen, dann wird beim Zugriff
    /// auf den Wert über diese Eigenschaft eine Ausnahme geworfen, die den Returncode enthält.
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <typeparam name="TInner"></typeparam>
    [DataContract]
    public class RCV3WithValue<TInner, TValue> : RCV3<TInner>, IRCV2, IValue<TValue>
        where TInner : class, IRCV2
        
    {
        /// <summary>
        /// Indicates a successful function call.
        /// </summary>
        /// <param name="User"></param>
        /// <returns></returns>
        public static RCV3WithValue<TInner, TValue> Ok(TValue value, string Message = "", string User = "*", TInner inner = null, [CallerMemberName] string caller = "")            
        {
            var mth = new System.Diagnostics.StackTrace().GetFrame(1).GetMethod();
            var cls = mth.ReflectedType.Name;
            var assembly = mth.ReflectedType.Assembly.GetName().Name;

            return new RCV3WithValue<TInner, TValue>(true, value, DateTime.Now, User, assembly, cls, caller, pnL.txt(Message), inner);
        }

        public static RCV3WithValue<TInner, TValue> Ok(TValue value, IDocuEntity Message, string User = "*", TInner inner = null, [CallerMemberName] string caller = "")
        {
            var mth = new System.Diagnostics.StackTrace().GetFrame(1).GetMethod();
            var cls = mth.ReflectedType.Name;
            var assembly = mth.ReflectedType.Assembly.GetName().Name;

            return new RCV3WithValue<TInner, TValue>(true, value, DateTime.Now, User, assembly, cls, caller, Message, inner);
        }

        /// <summary>
        /// Indicates a failed function call.
        /// </summary>
        /// <param name="User"></param>
        /// <param name="ErrorDescription"></param>
        /// <returns></returns>
        public static RCV3WithValue<TInner, TValue> Failed(TValue value, string ErrorDescription = "", string User = "*", TInner inner = null, [CallerMemberName] string caller = "")
        {
            var mth = new System.Diagnostics.StackTrace().GetFrame(1).GetMethod();
            var cls = mth.ReflectedType.Name;
            var assembly = mth.ReflectedType.Assembly.GetName().Name;

            return new RCV3WithValue<TInner, TValue>(false, value, DateTime.Now, User, assembly, cls, caller, pnL.txt(ErrorDescription), inner);
        }

        /// <summary>
        /// mko, 04.05.2018
        /// </summary>
        /// <param name="ex"></param>
        /// <param name="User"></param>
        /// <returns></returns>
        public static RCV3WithValue<TInner, TValue> Failed(TValue value, Exception ex, string User = "*", TInner inner = null, [CallerMemberName] string caller = "")
        {
            var mth = new System.Diagnostics.StackTrace().GetFrame(1).GetMethod();
            var cls = mth.ReflectedType.Name;
            var assembly = mth.ReflectedType.Assembly.GetName().Name;

            //var fmt = new PNDocuTerms.DocuEntities.PNFormater(pnL);

            return new RCV3WithValue<TInner, TValue>(false, value, DateTime.Now, User, assembly, cls, caller, TraceHlp.FlattenExceptionMessagesPN(ex), inner);
        }


        /// <summary>
        /// Indicates a failed function call.
        /// </summary>
        /// <param name="User"></param>
        /// <param name="ErrorDescription"></param>
        /// <returns></returns>
        public static RCV3WithValue<TInner, TValue> Failed(TValue value, IDocuEntity ErrorDescription, string User = "*", TInner inner = null, [CallerMemberName] string caller = "")
        {
            var mth = new System.Diagnostics.StackTrace().GetFrame(1).GetMethod();
            var cls = mth.ReflectedType.Name;
            var assembly = mth.ReflectedType.Assembly.GetName().Name;

            return new RCV3WithValue<TInner, TValue>(false, value, DateTime.Now, User, assembly, cls, caller, ErrorDescription, inner);
        }


        /// <summary>
        /// mko, 25.7.2018
        /// Konstruktor only for deserialization purpose.
        /// </summary>
        /// <param name="succeeded"></param>
        /// <param name="value"></param>
        /// <param name="dat"></param>
        /// <param name="User"></param>
        /// <param name="Assembly"></param>
        /// <param name="TypeName"></param>
        /// <param name="FunctionName"></param>
        /// <param name="Message"></param>
        /// <param name="inner"></param>
        //[Newtonsoft.Json.JsonConstructor]
        //public RCV3WithValue(bool succeeded, TValue value, DateTime dat, string User, string Assembly, string TypeName, string FunctionName, string Message, TInner inner)
        //    : base(succeeded, dat, User, Assembly, TypeName, FunctionName, pnL.txt(Message), inner)
        //{
        //    _value = value;
        //}

        
        public RCV3WithValue(bool succeeded, TValue value, DateTime dat, string User, string Assembly, string TypeName, string FunctionName, IDocuEntity Message, TInner inner)
            : base(succeeded, dat, User, Assembly, TypeName, FunctionName, Message, inner)
        {
            _value = value;
        }


        public new RCV3WithValue<TInner, TValue> Clone()
        {
            return new RCV3WithValue<TInner, TValue>(Succeeded, Value, LogDate, User, AssemblyName, TypeName, FunctionName, pnL.txt(Message), _InnerRC);
        }


        public RCV3WithValue()
        {
        }

        [DataMember(Name ="Value")]
        protected TValue _value;

        [Newtonsoft.Json.JsonIgnore]
        public TValue Value => _value;

        /// <summary>
        /// mko, 2.7.2019
        /// sicherer Abruf eines Wertes in einem funktionalen Kontext. Falls kein Wert existiert, weil diese 
        /// Rückgabeobjekt einen Fehler anzeigt, dann wird eine Ausnahme geworfen, die in einem catch- Handler 
        /// um einen funktionalen Ausdruck abgefangen werden kann.
        /// </summary>
        [Newtonsoft.Json.JsonIgnore]
        public TValue ValueOrException
            => Succeeded ? _value : throw new RCV3GetValueException(this.ToPlx());

        public override string ToString()
        {
            return ToPlx().ToString();
        }

        public override IDocuEntity ToPlx()
        {

            IDocuEntity plx = null;

            // To leave the IRCV2 interface untouched type check for _InnerIRCv2 is needed 
            // Otherwise IRCV2 needs to be extended with IRCV2 InnerIRCv2 { get; } Property

            var details = pnL.i(ANC.DocuTerms.MetaData.Details.UID,
                            pnL.p(ANC.TechTerms.Timeline.DateStamp.UID, pnL.date(LogDate)),
                            pnL.p(ANC.TechTerms.Timeline.TimeStamp.UID, pnL.time(LogDate.Hour, LogDate.Minute, LogDate.Second)),
                            pnL.KillIf(string.IsNullOrWhiteSpace(User), () => (IInstanceMember)pnL.p(ANC.TechTerms.Authentication.UserId.UID, User)),
                            pnL.KillIf(Value == null,
                                () => (IInstanceMember)pnL.p(ANC.DocuTerms.MetaData.Val.UID,
                                    pnL.IfElse(Value is IDocuEntity,
                                        () => pnL.EncapsulateAsPropertyValue((IDocuEntity)Value),
                                        () => new PNDocuTerms.DocuEntities.String(Value.ToString().Replace("#.", "").Replace("#", "").Replace("'", ""))))),
                            pnL.KillIf(MessageEntity == null, () => (IInstanceMember)pnL.p(ANC.DocuTerms.MetaData.Msg.UID, pnL.EncapsulateAsPropertyValue(MessageEntity))),
                            pnL.KillIf(InnerRC_T == null, () => (IInstanceMember)pnL.p("innerT", pnL.EncapsulateAsPropertyValue(InnerRC_T.ToPlx()))),
                            pnL.KillIf(InnerRCV2 == null || !(InnerRCV2 is IRCV2), () => (IInstanceMember)pnL.p("inner", pnL.EncapsulateAsPropertyValue(InnerRCV2.ToPlx()))));

            plx = pnL.i($"{AssemblyName}.{TypeName}",
                    pnL.m(FunctionName,
                        pnL.ret(
                            pnL.IfElse(Succeeded,
                                () => (IReturnValue)pnL.eSucceeded(details),
                                () => pnL.eFails(details)))));

            return plx;
        }
    }
}
