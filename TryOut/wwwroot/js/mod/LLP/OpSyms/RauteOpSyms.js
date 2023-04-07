// mko, 29.3.2023
// **Lukasiewicz List Processor**
define(["require", "exports"], function (require, exports) {
    "use strict";
    Object.defineProperty(exports, "__esModule", { value: true });
    class RauteOpSyms {
        constructor() {
            this.rpnFuncPrefix = "#";
            this.rpnBoolType = "#b";
            this.rpnComment = "#/";
            this.rpnNumType = "#n";
            this.rpnIntType = "#i";
            this.rpnListEnd = "#.";
            this.rpnListStart = "#_";
            this.rpnNoneToken = "#*";
            this.rpnSingleLineComment = "#//";
            this.rpnStrType = "#s";
            this.rpnFuncHeadPrefix = "#f";
        }
    }
    exports.default = RauteOpSyms;
});
//# sourceMappingURL=RauteOpSyms.js.map