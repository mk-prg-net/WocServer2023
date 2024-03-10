using MKPRG.Tracing.DocuTerms;
using System;
using System.Collections.Generic;
using System.Text;

namespace MKPRG.Tracing
{
    public class RetBld
        : IRetBld
    {
        public IRet AuthorizationFailed(IMethod DescriptionOfFailedAuthorizationProcess)
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

        public IRet ReturnOkButWarnings(IEventParameter Warnings)
        {
            throw new NotImplementedException();
        }

        public IRet ValidationOfArgumentFailed(string NameOfValidationRule)
        {
            throw new NotImplementedException();
        }
    }
}
