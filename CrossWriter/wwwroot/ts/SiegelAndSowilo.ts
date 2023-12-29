// mko, 29.12.2023

export enum ErrorClasses {
    GeneralError,
    ArgumentValidationFailed,
    SubFunctionCallFailed,
    SubSystemCallFaild,
    DataInconsistencyDetected,
}

// The ᛋ Siegel Function is defined in NYT as the Branch that will be called in case of success
export type SiegelSuccessFunc<TState> = (state: TState, ...args: any[]) => any;

// The ᛊ Sowilo Function is defined in NYT as the Branch, that will be called in case of an error
export type SowiloErrFunc<TState> = (state: TState, calledFuncName: string, errorClass: ErrorClasses, ...args: any[]) => any;


export function ArgumentValidationFailedDescriptor<TState>(state: TState, calledFuncName: string, ArgName: string, ArgValue: any, ValidationFunction: string)
    : [state: TState, calledFuncName: string, errCls: ErrorClasses, argumentDescriptor: string, ValidationFunction: string]
{
    return [state, calledFuncName, ErrorClasses.ArgumentValidationFailed, `argument: ${ArgName}=${ArgValue}`, ValidationFunction];
}