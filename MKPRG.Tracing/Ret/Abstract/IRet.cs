using System;
using System.Collections.Generic;
using System.Text;

namespace MKPRG.Tracing
{
    /// <summary>
    /// mko, 10.3.2024
    /// Descriptor for State after return from a Method Call.
    /// </summary>
    public  interface IRet
    {
        bool ReturnedFromSuccessfulCall
        {
            get;            
        }

        bool ReturnedFromSuccessfulCallWithWarnings
        {
            get;            
        }

        bool ReturnedBeforeExecutionCompleted
        {
            get;
        }

        bool MethodIsNotImplemented
        {
            get;
        }

        bool AuthorizationFailed
        {
            get;
        }

        bool ValidationOfArgumentsFailed
        {
            get;
        }

        bool BusinessRuleViolated
        {
            get;
        }

        bool SubProcedureCallFailed
        {
            get;
        }

        bool SubsystemCallFailed
        {
            get;
        }

        bool DataInconsistencyOccured
        {
            get;
        }

        bool GeneralError
        {
            get;
        }

        /// <summary>
        /// This DocuTerm describes the Situation after MethodCall
        /// </summary>
        DocuTerms.IMethod DescriptorOfMethodCallAndReturnValue
        {
            get;
        }



    }
}
