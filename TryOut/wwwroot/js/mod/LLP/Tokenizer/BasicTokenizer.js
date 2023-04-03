// mko, 1.4.2023
// **Lukasiewicz List Processor**
define(["require", "exports", "../../rpnParser/StringHlp", "../RC/RCwithValue"], function (require, exports, StringHlp_1, RCwithValue_1) {
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
        // In diesem Konzept wird der Tokenizer immer nach Eingabe einer Zeile aktiv.
        // Enthält die Zeile keine Operationssymbole, dann werden alle Eingaben als einzelne Strings eingelesen.
        // Bei Operationen für elementare Typen wird davon ausgeganen, dass der Wert in derselben Zeile definiert ist.
        // true #b // Ok
        // #b      // False
        TokenizeLine(txt) {
            let ret = new RCwithValue_1.default(false, "Not completed", []);
            let strTokens = this.strHlp.tokenize(txt);
            let tokenStack = this.stackOps.NewStack();
            if (strTokens.length === 0) {
                ret = new RCwithValue_1.default(false, "tokenize of text provides to an empty strToken list!", []);
            }
            else {
                ret = new RCwithValue_1.default(true, `Number of strTokens: ${strTokens.length}`, []);
                for (let pos = 0; pos < strTokens.length; pos++) {
                    let currentStrTok = strTokens[pos];
                    // Boolean Parsen in der Zeile, z.B. `true #b`
                    if (currentStrTok === this.opSym.rpnBoolType) {
                        let retPeek = this.stackOps.Peek(tokenStack);
                        if (retPeek.Success && retPeek.ReturnValue.tokOpSym == this.opSym.rpnStrType) {
                            let lastStrTok = retPeek.ReturnValue;
                            if (lastStrTok.strValue.toUpperCase() == "TRUE") {
                                this.stackOps.Push(tokenStack, this.stackElemStructs.CreateBoolToken(true));
                            }
                            else if (lastStrTok.strValue.toUpperCase() == "FALSE") {
                                this.stackOps.Push(tokenStack, this.stackElemStructs.CreateBoolToken(false));
                            }
                            else {
                                ret = new RCwithValue_1.default(false, `pos ${pos}: illegal bool value ${lastStrTok.strValue}`, []);
                                break;
                            }
                        }
                        else {
                            ret = new RCwithValue_1.default(false, `pos ${pos}: bool token without value`, []);
                        }
                    }
                    // Kommentar einlesen
                    else if (currentStrTok === this.opSym.rpnComment) {
                        let pc = pos + 1;
                        let commentTxt = "";
                        while (pc < strTokens.length) {
                            commentTxt += strTokens[pc];
                        }
                        this.stackOps.Push(tokenStack, this.stackElemStructs.CreateCommentToken(commentTxt));
                    }
                    // int einlesen
                    else if (currentStrTok == this.opSym.rpnIntType) {
                        let retPeek = this.stackOps.Peek(tokenStack);
                        if (retPeek.Success && retPeek.ReturnValue.tokOpSym == this.opSym.rpnStrType) {
                            let lastStrTok = retPeek.ReturnValue;
                        }
                        else {
                            ret = new RCwithValue_1.default(false, `pos ${pos}: int token without value`, []);
                        }
                    }
                }
                return ret;
            }
        }
    }
    exports.default = BasicTokenizer;
});
//# sourceMappingURL=BasicTokenizer.js.map