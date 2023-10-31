// mko, 31.10.2023
// **Lukasiewicz List Processor**
// OpSyms as runic
define(["require", "exports"], function (require, exports) {
    "use strict";
    Object.defineProperty(exports, "__esModule", { value: true });
    class RunicOpSyms {
        constructo() {
            this.rpnArrayPrefix = "ᛥ";
            this.rpnBoolType = "ᛔ";
            this.rpnComment = "᛭";
            this.rpnFuncHeadPrefix = "ᚪ";
            this.rpnFuncPrefix = "ᚪ";
            this.rpnInstancePrefix = "ᛝ";
            this.rpnIntType = "ᛕ";
            this.rpnListEnd = "ᛩ";
            this.rpnListStart = "ᚹ";
            this.rpnNidPrefix = "ᚻ";
            this.rpnNoneToken = "";
            this.rpnNumType = "ᚱ";
            this.rpnPropPrefix = "ᛜ";
            this.rpnReturnPrefix = "ᛏ";
            this.rpnSingleLineComment = "᛭᛭";
            this.rpnStrType = "";
        }
    }
    exports.default = RunicOpSyms;
});
//# sourceMappingURL=RunicOpSyms.js.map