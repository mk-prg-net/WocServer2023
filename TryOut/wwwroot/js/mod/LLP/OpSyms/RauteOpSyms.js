// mko, 29.3.2023
// **Lukasiewicz List Processor**
define(["require", "exports"], function (require, exports) {
    "use strict";
    Object.defineProperty(exports, "__esModule", { value: true });
    class RauteOpSyms {
        constructor() {
            this.rpnFuncPrefix = "#";
            this.rpnNidPrefix = "#*";
            this.rpnNoneToken = "#0";
            this.rpnComment = "#/";
            this.rpnSingleLineComment = "#//";
            this.rpnBoolType = "#b";
            this.rpnNumType = "#q";
            this.rpnIntType = "#z";
            this.rpnStrType = "#s";
            this.rpnListStart = "#:";
            this.rpnListEnd = "#.";
            this.rpnFuncHeadPrefix = "#m";
            this.rpnInstancePrefix = "#i";
            this.rpnPropPrefix = "#+";
            this.rpnReturnPrefix = "#<";
        }
    }
    exports.default = RauteOpSyms;
});
//# sourceMappingURL=RauteOpSyms.js.map