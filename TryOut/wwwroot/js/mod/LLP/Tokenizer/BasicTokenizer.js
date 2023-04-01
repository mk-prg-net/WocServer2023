// mko, 1.4.2023
// **Lukasiewicz List Processor**
define(["require", "exports", "../RC/RC", "../../rpnParser/StringHlp"], function (require, exports, RC_1, StringHlp_1) {
    "use strict";
    Object.defineProperty(exports, "__esModule", { value: true });
    class BasicTokenizer {
        constructor(opSym, stackOps, stackElemStructs) {
            this.opSym = opSym;
            this.strHlp = new StringHlp_1.default();
            this.stackElemStructs = stackElemStructs;
            this.stackOps = stackOps;
        }
        // mko, 1.4.2023
        // Übergibt die in Token aufzulösende Eingabezeile
        // In diesem Konzept wird der Token immer nach Eingabe einer Zeile aktiv.
        // Enthält die Zeile keine Operationssymbole, dann werden alle eingaben als einzelne Strings eingelesen.
        // Bei Operationen für elementare Typen wird davon ausgeganen, dass der Wert in derselben Zeile definiert ist
        // true #b // Ok
        // #b      // False
        SetTextLine(txt) {
            let ret = new RC_1.default(false, "Not completed");
            this.strTokens = this.strHlp.tokenize(txt);
            if (this.strTokens.length === 0) {
                ret = new RC_1.default(false, "tokenize of text provides to an empty strToken list!");
            }
            else {
                ret = new RC_1.default(true, `Number of strTokens: ${this.strTokens.length}`);
            }
            this.pos = 0;
            return ret;
        }
        Read() {
            let ret = false;
            if (this.strTokens[this.pos] === this.opSym.rpnBoolType)
                return ret;
        }
        EOF() {
            throw new Error("Method not implemented.");
        }
        Token() {
            throw new Error("Method not implemented.");
        }
    }
    exports.default = BasicTokenizer;
});
//# sourceMappingURL=BasicTokenizer.js.map