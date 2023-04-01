// mko, 1.4.2023
// **Lukasiewicz List Processor**


import IToken from "../IToken";
import ITokenizer from "../ITokenizer";
import IBoolToken from "../IBoolToken";
import IInttoken from "../IIntToken";
import ICommentToken from "../ICommentToken";
import IFunctionToken from "../IFunction";
import INumToken from "../INumToken";
import IStrToken from "../IStrToken";
import IOpSym from "../IOpSym";
import IRC from "../IRC";
import IRCwithValue from "../IRCwithValue";
import RC from "../RC/RC";

import StrHlp from "../StringHlp";
import StringHlp from "../../rpnParser/StringHlp";
import StackOps from "../StackOps";
import StackElemStructs from "../StackElemStructs";

export default class BasicTokenizer implements ITokenizer {

    constructor(opSym: IOpSym, stackOps: StackOps, stackElemStructs : StackElemStructs)
    {
        this.opSym = opSym;
        this.strHlp = new StringHlp();
        this.stackElemStructs = stackElemStructs;
        this.stackOps = stackOps;
    }

    strHlp: StringHlp;
    stackOps: StackOps;
    stackElemStructs: StackElemStructs;

    // mko, 1.4.2023
    // Übergibt die in Token aufzulösende Eingabezeile
    // In diesem Konzept wird der Token immer nach Eingabe einer Zeile aktiv.
    // Enthält die Zeile keine Operationssymbole, dann werden alle eingaben als einzelne Strings eingelesen.
    // Bei Operationen für elementare Typen wird davon ausgeganen, dass der Wert in derselben Zeile definiert ist
    // true #b // Ok
    // #b      // False
    SetTextLine(txt: string): IRC {

        let ret = new RC(false, "Not completed");  
        this.strTokens = this.strHlp.tokenize(txt);
        this.tokenStack = this.stackOps.NewStack();

        if (this.strTokens.length === 0) {
            ret = new RC(false, "tokenize of text provides to an empty strToken list!");
        }
        else {
            ret = new RC(true, `Number of strTokens: ${this.strTokens.length}`);
        }

        this.pos = 0;

        return ret;
    }    
    
    opSym: IOpSym;
    strTokens: string[];   
    pos: number;

    tokenStack: IToken[];

    Read(): boolean {

        let ret = false;

        if (this.strTokens[this.pos] === this.opSym.rpnBoolType)
        {
            this.stackOps.Peek(to)

        }

        return ret;
    }
    EOF(): boolean {
        throw new Error("Method not implemented.");
    }
    Token(): IToken {
        throw new Error("Method not implemented.");
    }
}


