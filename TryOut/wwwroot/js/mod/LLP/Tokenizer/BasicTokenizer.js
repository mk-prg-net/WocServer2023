// mko, 1.4.2023
// **Lukasiewicz List Processor**
var __importDefault = (this && this.__importDefault) || function (mod) {
    return (mod && mod.__esModule) ? mod : { "default": mod };
};
define(["require", "exports", "../../rpnParser/StringHlp", "../RC/RCwithValue"], function (require, exports, StringHlp_1, RCwithValue_1) {
    "use strict";
    Object.defineProperty(exports, "__esModule", { value: true });
    StringHlp_1 = __importDefault(StringHlp_1);
    RCwithValue_1 = __importDefault(RCwithValue_1);
    class BasicTokenizer {
        constructor(opSym, stackOps, stackElemStructs) {
            this.opSym = opSym;
            this.strHlp = new StringHlp_1.default();
            this.stackElemStructs = stackElemStructs;
            this.stackOps = stackOps;
        }
        // mko, 3.4.2023
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
                // strToken Klassifizieren und in Elemente der entsprechenden Tokenklassen umwandeln + Ablage im Stack
                for (let pos = 0; pos < strTokens.length; pos++) {
                    let currentStrTok = strTokens[pos];
                    // Prüfen, ob ein NID vorliegt
                    if (currentStrTok == this.opSym.rpnNidPrefix) {
                    }
                    else if (currentStrTok === this.opSym.rpnBoolType) {
                        // Boolean Parsen in der Zeile, z.B. `true #b`
                        let retPeek = this.stackOps.Peek(tokenStack);
                        if (retPeek.Success && retPeek.ReturnValue.tokOpSym == this.opSym.rpnStrType) {
                            let lastStrTok = retPeek.ReturnValue;
                            if (lastStrTok.strValue.toUpperCase() == "TRUE") {
                                this.stackOps.Pop(tokenStack);
                                this.stackOps.Push(tokenStack, this.stackElemStructs.CreateBoolToken(true));
                            }
                            else if (lastStrTok.strValue.toUpperCase() == "FALSE") {
                                this.stackOps.Pop(tokenStack);
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
                        let commentTxt = "";
                        // alle tokens rechts vom Kommentarzeichen bis zum Zeilenende als Kommentartext zusammenfassen
                        for (pos = pos + 1; pos < strTokens.length; pos++) {
                            commentTxt += strTokens[pos] + " ";
                        }
                        this.stackOps.Push(tokenStack, this.stackElemStructs.CreateCommentToken(commentTxt));
                        // Alles wurde eingelesen
                        break;
                    }
                    // int einlesen
                    else if (currentStrTok == this.opSym.rpnIntType) {
                        let retPeek = this.stackOps.Peek(tokenStack);
                        if (retPeek.Success && retPeek.ReturnValue.tokOpSym == this.opSym.rpnStrType) {
                            let lastStrTok = retPeek.ReturnValue;
                            let val = BigInt(lastStrTok.strValue);
                            if (typeof val) {
                                this.stackOps.Pop(tokenStack);
                                this.stackOps.Push(tokenStack, this.stackElemStructs.CreateIntToken(val));
                            }
                            else {
                                ret = new RCwithValue_1.default(false, `pos ${pos}: int value is not a number ${lastStrTok}`, []);
                                break;
                            }
                        }
                        else {
                            ret = new RCwithValue_1.default(false, `pos ${pos}: int token without value`, []);
                        }
                    }
                    // dbl- Token einlesen
                    else if (currentStrTok == this.opSym.rpnNumType) {
                        let retPeek = this.stackOps.Peek(tokenStack);
                        if (retPeek.Success && retPeek.ReturnValue.tokOpSym == this.opSym.rpnStrType) {
                            let lastStrTok = retPeek.ReturnValue;
                            let val = Number(lastStrTok.strValue);
                            if (!isNaN(val)) {
                                this.stackOps.Pop(tokenStack);
                                this.stackOps.Push(tokenStack, this.stackElemStructs.CreateDblToken(val));
                            }
                            else {
                                ret = new RCwithValue_1.default(false, `pos ${pos}: dbl value is not a number ${lastStrTok}`, []);
                                break;
                            }
                        }
                        else {
                            ret = new RCwithValue_1.default(false, `pos ${pos}: dbl token without value`, []);
                        }
                    }
                    // allg. Listenanfang Markierung einlesen
                    else if (currentStrTok == this.opSym.rpnListStart) {
                        this.stackOps.Push(tokenStack, this.stackElemStructs.CreateListStartToken(pos));
                    }
                    // Listenende- Markierung einlesen 
                    else if (currentStrTok == this.opSym.rpnListEnd) {
                        this.stackOps.Push(tokenStack, this.stackElemStructs.CreateListEndToken(pos));
                    }
                    // Prüfen, ob nicht ein Funktionsname vorliegt
                    else if (currentStrTok.startsWith(this.opSym.rpnFuncPrefix)) {
                        // Funktion liegt vor
                        this.stackOps.Push(tokenStack, this.stackElemStructs.CreateFunctionHeadToken(currentStrTok, pos));
                    }
                    else {
                        // Ablage aller bis dato npicht klassifizierter strToken als Strings
                        this.stackOps.Push(tokenStack, this.stackElemStructs.CreateStrToken(currentStrTok));
                    }
                }
            }
            if (ret.Success) {
                //  Wenn die Eingabezeile erfolgreich in Token aufgelöst werden konnte, dann wird die Liste der Token (tokenStack)
                //  dem Rückgabewert hinzugefügt.
                ret = new RCwithValue_1.default(true, ret.ErrorMsgIfNotSuccesful, tokenStack);
            }
            return ret;
        }
    }
    exports.default = BasicTokenizer;
});
//# sourceMappingURL=BasicTokenizer.js.map