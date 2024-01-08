// mko, 29.12.2023
define(["require", "exports"], function (require, exports) {
    "use strict";
    Object.defineProperty(exports, "__esModule", { value: true });
    exports.ArgumentValidationFailedDescriptor = exports.ErrorClasses = void 0;
    var ErrorClasses;
    (function (ErrorClasses) {
        ErrorClasses[ErrorClasses["GeneralError"] = 0] = "GeneralError";
        ErrorClasses[ErrorClasses["ArgumentValidationFailed"] = 1] = "ArgumentValidationFailed";
        ErrorClasses[ErrorClasses["SubFunctionCallFailed"] = 2] = "SubFunctionCallFailed";
        ErrorClasses[ErrorClasses["SubSystemCallFaild"] = 3] = "SubSystemCallFaild";
        ErrorClasses[ErrorClasses["DataInconsistencyDetected"] = 4] = "DataInconsistencyDetected";
    })(ErrorClasses || (exports.ErrorClasses = ErrorClasses = {}));
    function ArgumentValidationFailedDescriptor(state, calledFuncName, ArgName, ArgValue, ValidationFunction) {
        return [state, calledFuncName, ErrorClasses.ArgumentValidationFailed, `argument: ${ArgName}=${ArgValue}`, ValidationFunction];
    }
    exports.ArgumentValidationFailedDescriptor = ArgumentValidationFailedDescriptor;
});
//# sourceMappingURL=SiegelAndSowilo.js.map