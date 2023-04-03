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
import RCwithValue from "../RC/RCwithValue";

export default class BasicTokenizer implements ITokenizer {
    opSym: IOpSym;
    strHlp: StringHlp;
    stackOps: StackOps;
    stackElemStructs: StackElemStructs;

    constructor(opSym: IOpSym, stackOps: StackOps, stackElemStructs: StackElemStructs) {
        this.opSym = opSym;
        this.strHlp = new StringHlp();
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
    TokenizeLine(txt: string): IRCwithValue<IToken[]> {

        let ret = new RCwithValue<IToken[]>(false, "Not completed", []);
        let strTokens = this.strHlp.tokenize(txt);
        let tokenStack = this.stackOps.NewStack();

        if (strTokens.length === 0) {
            ret = new RCwithValue<IToken[]>(false, "tokenize of text provides to an empty strToken list!", []);
        }
        else {
            ret = new RCwithValue<IToken[]>(true, `Number of strTokens: ${strTokens.length}`, []);

            for (let pos = 0; pos < strTokens.length; pos++) {

                let currentStrTok = strTokens[pos];

                // Boolean Parsen in der Zeile, z.B. `true #b`
                if (currentStrTok === this.opSym.rpnBoolType) {

                    let retPeek = this.stackOps.Peek(tokenStack);

                    if (retPeek.Success && retPeek.ReturnValue.tokOpSym == this.opSym.rpnStrType) {

                        let lastStrTok = retPeek.ReturnValue as IStrToken;

                        if (lastStrTok.strValue.toUpperCase() == "TRUE") {
                            this.stackOps.Push(tokenStack, this.stackElemStructs.CreateBoolToken(true));
                        }
                        else if (lastStrTok.strValue.toUpperCase() == "FALSE") {
                            this.stackOps.Push(tokenStack, this.stackElemStructs.CreateBoolToken(false));
                        } else {
                            ret = new RCwithValue<IToken[]>(false, `pos ${pos}: illegal bool value ${lastStrTok.strValue}`, [])
                            break;
                        }
                    }
                    else {
                        ret = new RCwithValue<IToken[]>(false, `pos ${pos}: bool token without value`, [])
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

                        let lastStrTok = retPeek.ReturnValue as IStrToken;

                        parseInt(lastStrTok)

                    }
                    else {
                        ret = new RCwithValue<IToken[]>(false, `pos ${pos}: int token without value`, [])
                    }
                }

            } 

            return ret;
        }
    }
}


