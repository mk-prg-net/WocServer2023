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
    /// mko, 22.10.2018
    /// Inheritance changed. Previously RCV3sV inherited from RCV3. Now it 
    /// Inherits from RCV3WithValue
    /// 
    /// mko, 2.7.2019
    /// Erweiter um die Eigenschaft ValueOrException. Diese ermöglicht einen sicheren Zugriff auf den Wert in einem 
    /// funktionalen Kontext. Sollte der Rückgabewert anstatt eines Wertes einen Fehler darstellen, dann wird beim Zugriff
    /// auf den Wert über diese Eigenschaft eine Ausnahme geworfen, die den Returncode enthält.
    /// 
    /// mko, 1.8.2019
    /// Implementiert ab jetzt auch die Schnittstelle IRCV3sV(of TValue)
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <typeparam name="TInner"></typeparam>
    [DataContract]
    // public class RCV3sV<TValue> : RCV3<RCV3>, IRCV2, IValue<TValue>
    public class RCV3sV<TValue> : RCV3WithValue<RCV3, TValue>, IRCV3sV<TValue>, IRCV2, IValue<TValue>
    {
        /// <summary>
        /// Indicates a successful function call.
        /// 
        /// mko, 14.2.2019
        /// Added Parameter caller. It determines the caller more save in async environments.
        /// </summary>
        /// <param name="User"></param>
        /// <returns></returns>
        public static new RCV3sV<TValue> Ok(TValue value, IDocuEntity Message = null, string User = "*", RCV3 inner = null, [CallerMemberName] string caller="")
        {
            var mth = new System.Diagnostics.StackTrace().GetFrame(1).GetMethod();
            var cls = mth.ReflectedType.Name;
            var assembly = mth.ReflectedType.Assembly.GetName().Name;

            //return new RCV3sV<TValue>(true, value, DateTime.Now, User, assembly, cls, mth.Name, Message, inner);
            return new RCV3sV<TValue>(true, value, DateTime.Now, User, assembly, cls, caller, Message, inner);
        }

        /// <summary>
        /// Indicates a failed function call.
        /// </summary>
        /// <param name="User"></param>
        /// <param name="ErrorDescription"></param>
        /// <returns></returns>
        public static new RCV3sV<TValue> Failed(TValue value, IDocuEntity ErrorDescription, string User = "*", RCV3 inner = null, [CallerMemberName] string caller = "")
        {
            var mth = new System.Diagnostics.StackTrace().GetFrame(1).GetMethod();
            var cls = mth.ReflectedType.Name;
            var assembly = mth.ReflectedType.Assembly.GetName().Name;

            //return new RCV3sV<TValue>(false, value, DateTime.Now, User, assembly, cls, mth.Name, ErrorDescription, inner);
            return new RCV3sV<TValue>(false, value, DateTime.Now, User, assembly, cls, caller, ErrorDescription, inner);
        }

        /// <summary>
        /// mko, 22.10.2018
        /// Erstellt einen RCV3sV parametrisch.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="Succedeed"></param>
        /// <param name="ErrorDescription"></param>
        /// <param name="User"></param>
        /// <param name="inner"></param>
        /// <returns></returns>
        public static RCV3sV<TValue> Create(TValue value, bool Succedeed = true, IDocuEntity ErrorDescription= null, string User = "*", RCV3 inner = null, [CallerMemberName] string caller = "")
        {
            var mth = new System.Diagnostics.StackTrace().GetFrame(1).GetMethod();
            var cls = mth.ReflectedType.Name;
            var assembly = mth.ReflectedType.Assembly.GetName().Name;

            //return new RCV3sV<TValue>(Succedeed, value, DateTime.Now, User, assembly, cls, mth.Name, ErrorDescription, inner);
            return new RCV3sV<TValue>(Succedeed, value, DateTime.Now, User, assembly, cls, caller, ErrorDescription, inner);
        }

        /// <summary>
        /// mko, 1.8.2019
        /// Erstellt aus den Daten eines Objekts, dass die Schnittstelle IRCV3sV implementiert, ein RCV3sV- Objket. 
        /// Wird zur Serialisierung benötig. Die Json- Serialisierung funktioniert nur für konkrete Typen.
        /// </summary>
        /// <param name="ret"></param>
        public RCV3sV(IRCV3sV<TValue> ret)
            : base(ret.Succeeded, ret.Value, ret.LogDate, ret.User, ret.AssemblyName, ret.TypeName, ret.FunctionName, ret.MessageEntity, ret.InnerRC_T)
        {
        }



        /// <summary>
        /// Constructor only for deserialization purpose.
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
        public RCV3sV(bool succeeded, TValue value, DateTime dat, string User, string Assembly, string TypeName, string FunctionName, IDocuEntity Message, RCV3 inner)
            //: base(succeeded, dat, User, Assembly, TypeName, FunctionName, Message, inner)
            : base(succeeded, value, dat, User, Assembly, TypeName, FunctionName, Message, inner)
        {
            _value = value;
        }

        public RCV3sV(RCV3WithValue<RCV3, TValue> rc)
            //: base(succeeded, dat, User, Assembly, TypeName, FunctionName, Message, inner)
            : base(rc.Succeeded, rc.Value, rc.LogDate, rc.User, rc.AssemblyName, rc.TypeName, rc.FunctionName, rc.MessageEntity, rc.InnerRC_T)
        {            
        }

        public new RCV3sV<TValue> Clone()
        {
            return new RCV3sV<TValue>(Succeeded, Value, LogDate, User, AssemblyName, TypeName, FunctionName, pnL.txt(Message), _InnerRC);
        }

        public RCV3sV()
        {
        }

        //[DataMember(Name = "Value")]
        //TValue _value;

        //[Newtonsoft.Json.JsonIgnore]
        //public TValue Value => _value;

        public override string ToString()
        {
            return ToPlx().ToString();
        }

        public override IDocuEntity ToPlx()
        {
            PNDocuTerms.DocuEntities.IDocuEntity plx = null;

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
