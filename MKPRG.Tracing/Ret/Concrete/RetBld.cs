using MKPRG.Naming.DocuTerms.StateDescription;
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

        public IRet AuthenticationFailed(string UserIdToAuthenticate)
            =>
            new Ret()
            {
                AuthenticationFailed = true,
                StatusAfterMethodCall = mthCall(pnL.eFails(pnL.L(pnL.p(TT.Authentication.AuthenticationFailedForUserId.UID, UserIdToAuthenticate))))
            };


        public IRet AuthenticationFailed(string UserIdToAuthenticate, DocuTerms.IMethod DescriptionOfFailedAuthorizationProcess)
            =>
            new Ret()
            {
                AuthenticationFailed = true,
                StatusAfterMethodCall = mthCall(pnL.eFails(pnL.L(
                                                            pnL.p(TT.Authentication.AuthenticationFailedForUserId.UID, UserIdToAuthenticate),
                                                            pnL.p(TTD.StateDescription.Why.UID, DescriptionOfFailedAuthorizationProcess))))
            };

        public IRet AuthorizationFailed(DocuTerms.IMethod DescriptionOfFailedAuthorizationProcess)
            =>
            new Ret()
            {
                AuthorizationFailed = true,
                StatusAfterMethodCall = mthCall(pnL.eFails(pnL.L(
                                                            pnL.p_NID(TTD.StateDescription.WhatsUp.UID, TT.Authorization.AccessDenied.UID), 
                                                            pnL.p(TTD.StateDescription.Why.UID, DescriptionOfFailedAuthorizationProcess))));
            };

        public IRet AuthorizationFailed(long requestedAccessRightNID, long ResourceClassNID, DocuTerms.IMethod DescriptionOfFailedAuthorizationProcess)
            =>
            new Ret()
            {
                AuthorizationFailed = true,
                StatusAfterMethodCall = mthCall(pnL.eFails(pnL.L(
                                                            pnL.p_NID(TTD.StateDescription.WhatsUp.UID, TT.Authorization.AccessDenied.UID),
                                                            pnL.p_NID(TT.Authorization.RequestedAccessRight.UID, requestedAccessRightNID),
                                                            pnL.p_NID(TT.Grammar.Prepositions.For.UID, ResourceClassNID),
                                                            pnL.p(TTD.StateDescription.Why.UID, DescriptionOfFailedAuthorizationProcess))));
            };

        public IRet BusinessRuleViolated(DocuTerms.IMethod docuTermForFailedBusinessRuleCheck)
            =>
            new Ret()
            {
                BusinessRuleViolated = true,
                StatusAfterMethodCall = mthCall(pnL.eFails(pnL.L(
                                                            pnL.p_NID(TTD.StateDescription.WhatsUp.UID, TT.Development.BusinessRuleViolated.UID),
                                                            pnL.p(TTD.StateDescription.Why.UID, docuTermForFailedBusinessRuleCheck))))
            };

        public IRet DataInconsistencyOccured(DocuTerms.IMethod docuTermForFailedDataConsistencyCheck)        
            =>
            new Ret()
            {
                DataInconsistencyOccured = true,
                StatusAfterMethodCall = mthCall(pnL.eFails(pnL.L(
                                                            pnL.p_NID(TTD.StateDescription.WhatsUp.UID, TT.Validation.Errors.DataInconsistency.UID),
                                                            pnL.p(TTD.StateDescription.Why.UID, docuTermForFailedDataConsistencyCheck))))
            };
        

        public IRet GeneralError(DocuTerms.IPropertyValue whatsUp, DocuTerms.IPropertyValue Why)
            =>
            new Ret()
            {
                GeneralError = true,
                StatusAfterMethodCall = mthCall(pnL.eFails(pnL.L(
                                                            pnL.p(TTD.StateDescription.WhatsUp.UID, whatsUp),
                                                            pnL.p(TTD.StateDescription.Why.UID, Why))))
            };

        public IRet MethodIsNotimplemented()
            =>
            new Ret()
            {
                MethodIsNotImplemented = true,
                StatusAfterMethodCall = mthCall(pnL.eFails())
            };

        public IRet NotCompleted()
            =>
            new Ret()
            {
                ReturnedBeforeExecutionCompleted = true,
                StatusAfterMethodCall = mthCall(pnL.eFails())
            };

        public IRet ReturnOK()
            =>
            new Ret()
            {
                ReturnedFromSuccessfulCall = true,
                StatusAfterMethodCall = mthCallSuccessful(pnL.L());
            };

        public IRet ReturnOk(DocuTerms.IEventParameter AdditionalInfosAboutSuccessfulReturn)
            =>
            new Ret()
            {
                ReturnedFromSuccessfulCall = true,
                StatusAfterMethodCall = mthCallSuccessful(AdditionalInfosAboutSuccessfulReturn)
            };

        public IRet ReturnOkButWarnings(DocuTerms.IEventParameter Warnings)
            => 
            new Ret()
            {
                ReturnedFromSuccessfulCall = true,
                StatusAfterMethodCall = mthCall(pnL.eWarn(Warnings))
            };


        public IRet SqlDatabaseQueryFailed(string sqlQuery, DocuTerms.IMethod docuTermThatDescribesFailedQuery)
            =>
            new Ret()
            {
                SqlDatabaseQueryFailed = true,
                StatusAfterMethodCall = mthCall(pnL.eFails(pnL.L(
                                                            pnL.p_NID(TTD.StateDescription.WhatsUp.UID, TT.Access.Datasources.WellKnown.Database.DatabaseQueryFailed.UID),
                                                            pnL.p(TTD.StateDescription.Why.UID, docuTermThatDescribesFailedQuery))))
            };

        public IRet SubprocedureCallFailed(DocuTerms.IMethod docuTermThatDescribesFailedSubProcedureCall)
            =>
            new Ret()
            {
                SubProcedureCallFailed = true,
                StatusAfterMethodCall = mthCall(pnL.eFails(pnL.L(
                                                            pnL.p_NID(TTD.StateDescription.WhatsUp.UID, TT.Development.SubProcedureCallFailed.UID),
                                                            pnL.p(TTD.StateDescription.Why.UID, docuTermThatDescribesFailedSubProcedureCall))))
            };

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
