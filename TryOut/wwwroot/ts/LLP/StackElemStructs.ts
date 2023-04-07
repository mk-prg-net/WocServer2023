// mko, 29.3.2023
// **Lukasiewicz List Processor**

import IOpSym from "./OpSyms/IOpSym";
import IFunction from "./Tokenizer/IFunction";
import IToken from "./Tokenizer/IToken";
import IIntToken from "./Tokenizer/IIntToken";
import INumToken from "./Tokenizer/INumToken";
import IStrToken from "./Tokenizer/IStrToken";
import IListToken from "./Tokenizer/IListToken";
import IBoolToken from "./Tokenizer/IBoolToken";
import ICommentToken from "./Tokenizer/ICommentToken";
import IListEndToken from "./Tokenizer/IListEndToken";
import IListStartToken from "./Tokenizer/IListStartToken";
import IFunctionHeadToken from "./Tokenizer/IFunctionHeadToken";

export default class StackElemStructs {

    opSym: IOpSym;

    constructor(opSym: IOpSym) {
        this.opSym = opSym;
    }

    // Definiert Klassenfabriken für Strukturen, die im Stack verwaltet werden

    public CreateNoneToken(): IToken {
        return {
            tokOpSym: this.opSym.rpnNoneToken
        };
    }

    public CreateIntToken(i: bigint): IIntToken {
        return {
            tokOpSym: this.opSym.rpnIntType,
            intValue: i
        }
    }

    public CreateDblToken(dbl: number): INumToken {
        return {
            tokOpSym: this.opSym.rpnNumType,
            numValue: dbl
        }
    }

    public CreateBoolToken(bool: boolean): IBoolToken {
        return {
            tokOpSym: this.opSym.rpnBoolType,
            booValue: bool
        }
    }

    public CreateStrToken(str: string): IStrToken {
        return {
            tokOpSym: this.opSym.rpnStrType,
            strValue: str
        }
    }

    public CreateCommentToken(str: string): ICommentToken {
        return {
            tokOpSym: this.opSym.rpnComment,
            comment: str
        }
    }


    public CreateListEndToken(pos: number) : IListEndToken {
        return {
            tokOpSym: this.opSym.rpnListEnd,
            ListEndPos: pos
        };
    }

    public CreateListStartToken(pos: number): IListStartToken {
        return {
            tokOpSym: this.opSym.rpnListEnd,
            ListBeginPos: pos
        };
    }

    public CreateListToken(listElems: IToken[]): IListToken {
        return {
            tokOpSym: this.opSym.rpnListStart,
            listElems: listElems
        }
    }

    public isToken(stackElem): boolean {
        return "tokOpSym" in stackElem;
    }

    public isFunc(stackElem: IToken, fnName: string): boolean {
        // Prädikat, gibt true zurück, wenn auf dem Stack eine Blockfunktion liegt.        
        if (stackElem.tokOpSym === this.opSym.rpnFuncPrefix && ("fnName" in stackElem) && ("Args" in stackElem)) {
            let func = stackElem as IFunction;
            return func.fnName === fnName;
        }
        else {
            return false;
        }
    }

    public CreateFunctionHeadToken(functionName: string, pos: number): IFunctionHeadToken {
        // Klassenfabrik für ein Token, das einen Funktionskopf markiert
        return {
            tokOpSym: this.opSym.rpnFuncHeadPrefix,
            FunctionName: functionName,
            FuntionHeadPos: pos
        };
    }

    public CreateFunc(fnName: string, args: IToken[]): IFunction {
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

    public CreateFuncUno(fnName: string, arg: IToken): IFunction {
        // Klassenfabrik für Stack- Elemente, die Inlinefunktionen darstellen

        let thisStackElemStructs = this;

            return {
                tokOpSym: this.opSym.rpnFuncPrefix,
                fnName: fnName,
                Args: [arg]
            };
    }

    public CreateFuncDue(fnName: string, arg1: IToken, arg2: IToken): IFunction {
        // Klassenfabrik für Stack- Elemente, die Inlinefunktionen darstellen

        let thisStackElemStructs = this;

        return {
            tokOpSym: this.opSym.rpnFuncPrefix,
            fnName: fnName,
            Args: [arg1, arg2]
        };
    }

    public CreateFuncTri(fnName: string, arg1: IToken, arg2: IToken, arg3: IToken): IFunction {
        // Klassenfabrik für Stack- Elemente, die Inlinefunktionen darstellen

        let thisStackElemStructs = this;

        return {
            tokOpSym: this.opSym.rpnFuncPrefix,
            fnName: fnName,
            Args: [arg1, arg2, arg3]
        };
    }

    public CreateFuncQuattro(fnName: string, arg1: IToken, arg2: IToken, arg3: IToken, arg4: IToken): IFunction {
        // Klassenfabrik für Stack- Elemente, die Inlinefunktionen darstellen

        let thisStackElemStructs = this;

        return {
            tokOpSym: this.opSym.rpnFuncPrefix,
            fnName: fnName,
            Args: [arg1, arg2, arg3, arg4]
        };
    }


}
