using System;
using System.Collections.Generic;
using System.Text;

using TT = MKPRG.Naming.TechTerms;
using TTD = MKPRG.Naming.DocuTerms;

namespace MKPRG.Tracing
{
    /// <summary>
    /// mko, 31.3.2024
    /// 
    /// mko, 26.5.2024
    /// </summary>
    public class RetBld
        : IRetBld
    {
        DocuTerms.IComposer pnL;
        string methName;
        DocuTerms.IMethodParameter[] fcallParameterDescriptors;

        Func<DocuTerms.IEventParameter, DocuTerms.IMethod> mthCallSuccessful;
        Func<DocuTerms.IReturnValue, DocuTerms.IMethod> mthCall;

        public RetBld(DocuTerms.IComposer pnL, 
                      string netAssemblyName, 
                      string netClassName,
                      string methName, params DocuTerms.IMethodParameter[] fcallParameterDescriptors)
        {
            this.pnL = pnL;
            this.methName = methName;
            this.fcallParameterDescriptors = fcallParameterDescriptors;

            mthCallSuccessful = evp => pnL.m(methName,
                                        pnL.p(TT.Development.DotNetAssembly.UID, netAssemblyName),
                                        pnL.p(TT.Development.DotNetClass.UID, netClassName),
                                        pnL.EmbedMethodParameters(fcallParameterDescriptors),
                                        pnL.ret(pnL.eSucceeded(evp)));

            mthCall = rv => pnL.m(methName,
                                  pnL.p(TT.Development.DotNetAssembly.UID, netAssemblyName),
                                  pnL.p(TT.Development.DotNetClass.UID, netClassName),
                                  pnL.EmbedMethodParameters(fcallParameterDescriptors),
                                  pnL.ret(rv));

        }

        public IRet AuthenticationFailed(string UserIdToAuthenticate, DocuTerms.IMethod DescriptionOfFailedAuthorizationProcess)
            =>
            new Ret()
            {
                AuthenticationFailed = true,
                StatusDescriptionAfterMethodCall = pnL.m(methName, 
                                                        pnL.p(TT.Authentication.UserId.UID, UserIdToAuthenticate),
                                                        pnL.EmbedMethodParameters(fcallParameterDescriptors),
                                                        pnL.ret(pnL.eFails(pnL.List(
                                                            pnL.p(TTD.StateDescription.WhatsUp.UID, TT.Authentication.AuthenticationFailedForUserId.UID
                    )
            };
            

        public IRet AuthorizationFailed(DocuTerms.IMethod DescriptionOfFailedAuthorizationProcess)
        {
            throw new NotImplementedException();
        }

        public IRet AuthorizationFailed(long requestedAccessRightNID, long ResourceClassNID, DocuTerms.IMethod DescriptionOfFailedAuthorizationProcess)
        {
            throw new NotImplementedException();
        }

        public IRet BusinessRuleViolated(DocuTerms.IMethod docuTermForFailedBusinessRuleCheck)
        {
            throw new NotImplementedException();
        }

        public IRet DataInconsistencyOccured(DocuTerms.IMethod docuTermForFailedDataConsistencyCheck)
        {
            throw new NotImplementedException();
        }

        public IRet GeneralError(DocuTerms.IPropertyValue whatsUp, DocuTerms.IPropertyValue Why)
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

        public IRet ReturnOk(DocuTerms.IEventParameter AdditionalInfosAboutSuccessfulReturn)
        {
            throw new NotImplementedException();
        }

        public IRet ReturnOkButWarnings(DocuTerms.IEventParameter Warnings)
        {
            throw new NotImplementedException();
        }


        public IRet SqlDatabaseQueryFailed(string sqlQuery, DocuTerms.IMethod docuTermThatDescribesFailedQuery)
        {
            throw new NotImplementedException();
        }

        public IRet SubprocedureCallFailed(DocuTerms.IMethod docuTermThatDescribesFailedSubProcedureCall)
        {
            throw new NotImplementedException();
        }

        public IRet SubsytemCallFailed(DocuTerms.IMethod docuTermThatDescribesFailedSubsystemCall)
        {
            throw new NotImplementedException();
        }

        public IRet ValidationOfArgumentFailed(string NameOfValidationRule)
        {
            throw new NotImplementedException();
        }

        public IRet ValidationOfArgumentFailed(long NameOfValidationRule, DocuTerms.IProperty validatedArgument)
        {
            throw new NotImplementedException();
        }

        public IRet _GeneralErrorForDebugOnly(string preleminaryErrorDescription)
        {
            throw new NotImplementedException();
        }
    }
}
