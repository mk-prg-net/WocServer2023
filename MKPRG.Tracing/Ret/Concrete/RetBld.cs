using MKPRG.Tracing.DocuTerms;
using System;
using System.Collections.Generic;
using System.Text;

namespace MKPRG.Tracing
{
    /// <summary>
    /// mko, 31.3.2024
    /// </summary>
    public class RetBld
        : IRetBld
    {
        string methName;
        DocuTerms.IProperty[] fcallParameterDescriptors;

        public RetBld(string methName, params IProperty[] fcallParameterDescriptors)
        {
            this.methName = methName;
            this.fcallParameterDescriptors = fcallParameterDescriptors;
        }

        public IRet AuthenticationFailed(string UserIdToAuthenticate, IMethod DescriptionOfFailedAuthorizationProcess)
        {
            throw new NotImplementedException();
        }

        public IRet AuthorizationFailed(IMethod DescriptionOfFailedAuthorizationProcess)
        {
            throw new NotImplementedException();
        }

        public IRet AuthorizationFailed(long requestedAccessRightNID, long ResourceClassNID, IMethod DescriptionOfFailedAuthorizationProcess)
        {
            throw new NotImplementedException();
        }

        public IRet BusinessRuleViolated(IMethod docuTermForFailedBusinessRuleCheck)
        {
            throw new NotImplementedException();
        }

        public IRet DataInconsistencyOccured(IMethod docuTermForFailedDataConsistencyCheck)
        {
            throw new NotImplementedException();
        }

        public IRet GenerlError(IPropertyValue whatsUp, IPropertyValue Why)
        {
            throw new NotImplementedException();
        }

        public IRet MethodIsNotimplemented()
        {
            throw new NotImplementedException();
        }

        public IRet NotCompleted()
        {
            throw new NotImplementedException();
        }

        public IRet ReturnOK()
        {
            throw new NotImplementedException();
        }

        public IRet ReturnOk(IEventParameter AdditionalInfosAboutSuccessfulReturn)
        {
            throw new NotImplementedException();
        }

        public IRet ReturnOk(IEventParameter AdditionalInfosAboutSuccessfulReturn)
        {
            throw new NotImplementedException();
        }

        public IRet ReturnOkButWarnings(IEventParameter Warnings)
        {
            throw new NotImplementedException();
        }

        public IRet ReturnOkButWarnings(IEventParameter Warnings)
        {
            throw new NotImplementedException();
        }

        public IRet SqlDatabaseQueryFailed(string sqlQuery, IMethod docuTermThatDescribesFailedQuery)
        {
            throw new NotImplementedException();
        }

        public IRet SubprocedureCallFailed(IMethod docuTermThatDescribesFailedSubProcedureCall)
        {
            throw new NotImplementedException();
        }

        public IRet SubsytemCallFailed(IMethod docuTermThatDescribesFailedSubsystemCall)
        {
            throw new NotImplementedException();
        }

        public IRet ValidationOfArgumentFailed(string NameOfValidationRule)
        {
            throw new NotImplementedException();
        }

        public IRet ValidationOfArgumentFailed(long NameOfValidationRule, IProperty validatedArgument)
        {
            throw new NotImplementedException();
        }

        public IRet _GeneralErrorForDebugOnly(string preleminaryErrorDescription)
        {
            throw new NotImplementedException();
        }
    }
}
