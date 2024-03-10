using MKPRG.Tracing.DocuTerms;
using System;
using System.Collections.Generic;
using System.Text;

namespace MKPRG.Tracing
{
    /// <summary>
    /// mko, 10.3.2024
    /// Implementation von IRet 
    /// </summary>
    public class Ret
        : IRet
    {
        public bool ReturnedFromSuccessfulCall
        { 
            get; 
            internal set; 
        }   

        public bool ReturnedFromSuccessfulCallWithWarnings
        {
            get;
            internal set;
        }

        public bool MethodIsNotImplemented
        {
            get;
            internal set;
        }

        public bool AuthorizationFailed
        {
            get;
            internal set;
        }

        public bool ValidationOfMethodArgsFailed
        {
            get;
            internal set;
        }

        public bool BusinessRuleViolated
        {
            get;
            internal set;
        }

        public bool SubProcedureCallFailed
        {
            get;
            internal set;
        }

        public bool SubsystemCallFailed
        {
            get;
            internal set;
        }

        public bool DataInconsistencyOccured
        {
            get;
            internal set;
        }

        public bool GeneralError
        {
            get;
            internal set;
        }

        public IMethod DescriptorOfMethodCallAndReturnValue => throw new NotImplementedException();
    }
}
