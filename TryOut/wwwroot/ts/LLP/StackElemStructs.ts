// mko, 29.3.2023
// **Lukasiewicz List Processor**

import IOpSym from "./IOpSym";
import IFunction from "./IFunction";
import IToken from "./IToken";
import IIntToken from "./IIntToken";
import INumToken from "./INumToken";
import IStrToken from "./IStrToken";
import IListToken from "./IListToken";
import IBoolToken from "./IBoolToken";
import ICommentToken from "./ICommentToken";

export default class StackElemStructs {

    opSym: IOpSym;

    constructor(opSym: IOpSym) {
        this.opSym = opSym;
    }

    // Definiert Klassenfabriken für Strukturen, die im Stack verwaltet werden

    CreateNoneToken(): IToken {
        return {
            tokOpSym: this.opSym.rpnNoneToken
        };
    }

    CreateIntToken(i: BigInteger): IIntToken {
        return {
            tokOpSym: this.opSym.rpnIntType,
            intValue: i
        }
    }

    CreateDblToken(dbl: number): INumToken {
        return {
            tokOpSym: this.opSym.rpnNumType,
            numValue: dbl
        }
    }

    CreateBoolToken(bool: boolean): IBoolToken {
        return {
            tokOpSym: this.opSym.rpnStrType,
            booValue: bool
        }
    }

    CreateStrToken(str: string): IStrToken {
        return {
            tokOpSym: this.opSym.rpnStrType,
            strValue: str
        }
    }

    CreateCommentToken(str: string): ICommentToken {
        return {
            tokOpSym: this.opSym.rpnStrType,
            comment: str
        }
    }



    CreateListToken(listElems: IToken[]): IListToken {
        return {
            tokOpSym: this.opSym.rpnListStart,
            listElems: listElems
        }
    }

    isToken(stackElem): boolean {
        return "tokOpSym" in stackElem;
    }

    isFunc(stackElem: IToken, fnName: string): boolean {
        // Prädikat, gibt true zurück, wenn auf dem Stack eine Blockfunktion liegt.        
        if (stackElem.tokOpSym === this.opSym.rpnFuncPrefix && ("fnName" in stackElem) && ("Args" in stackElem)) {
            let func = stackElem as IFunction;
            return func.fnName === fnName;
        }
        else {
            return false;
        }
    }
    CreateFunc(fnName: string, args: IToken[]): IFunction {
        // Klassenfabrik für Stack- Elemente, die Inlinefunktionen darstellen

        let thisStackElemStructs = this;

        if (args.length > 0) {
            return {
                tokOpSym: this.opSym.rpnFuncPrefix,
                fnName: fnName,
                Args: args
            };
        } else {
            return {
                tokOpSym: this.opSym.rpnFuncPrefix,
                fnName: fnName,
                Args: []
            };
        }
    }
}
