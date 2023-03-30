// mko, 29.3.2023
// **Lukasiewicz List Processor**
define(["require", "exports"], function (require, exports) {
    "use strict";
    Object.defineProperty(exports, "__esModule", { value: true });
    class StackElemStructs {
        constructor(opSym) {
            this.opSym = opSym;
        }
        // Definiert Klassenfabriken für Strukturen, die im Stack verwaltet werden
        CreateNoneToken() {
            return {
                tokOpSym: this.opSym.rpnNoneToken
            };
        }
        CreateIntToken(i) {
            return {
                tokOpSym: this.opSym.rpnIntType,
                intValue: i
            };
        }
        CreateDblToken(dbl) {
            return {
                tokOpSym: this.opSym.rpnDblType,
                dblValue: dbl
            };
        }
        CreateBoolToken(bool) {
            return {
                tokOpSym: this.opSym.rpnStrType,
                booValue: bool
            };
        }
        CreateStrToken(str) {
            return {
                tokOpSym: this.opSym.rpnStrType,
                strValue: str
            };
        }
        CreateCommentToken(str) {
            return {
                tokOpSym: this.opSym.rpnStrType,
                comment: str
            };
        }
        CreateListToken(listElems) {
            return {
                tokOpSym: this.opSym.rpnListStart,
                listElems: listElems
            };
        }
        isToken(stackElem) {
            return "tokOpSym" in stackElem;
        }
        isFunc(stackElem, fnName) {
            // Prädikat, gibt true zurück, wenn auf dem Stack eine Blockfunktion liegt.        
            if (stackElem.tokOpSym === this.opSym.rpnFuncPrefix && ("fnName" in stackElem) && ("Args" in stackElem)) {
                let func = stackElem;
                return func.fnName === fnName;
            }
            else {
                return false;
            }
        }
        CreateFunc(fnName, args) {
            // Klassenfabrik für Stack- Elemente, die Inlinefunktionen darstellen
            let thisStackElemStructs = this;
            if (args.length > 0) {
                return {
                    tokOpSym: this.opSym.rpnFuncPrefix,
                    fnName: fnName,
                    Args: args
                };
            }
            else {
                return {
                    tokOpSym: this.opSym.rpnFuncPrefix,
                    fnName: fnName,
                    Args: []
                };
            }
        }
    }
    exports.default = StackElemStructs;
});
//# sourceMappingURL=StackElemStructs.js.map