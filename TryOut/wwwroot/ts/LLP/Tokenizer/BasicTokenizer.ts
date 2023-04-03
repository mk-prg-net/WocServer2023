﻿// mko, 1.4.2023
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

    // mko, 3.4.2023
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

            // strToken Klassifizieren und in Elemente der entsprechenden Tokenklassen umwandeln + Ablage im Stack
            for (let pos = 0; pos < strTokens.length; pos++) {

                let currentStrTok = strTokens[pos];

                // Boolean Parsen in der Zeile, z.B. `true #b`
                if (currentStrTok === this.opSym.rpnBoolType) {

                    let retPeek = this.stackOps.Peek(tokenStack);

                    if (retPeek.Success && retPeek.ReturnValue.tokOpSym == this.opSym.rpnStrType) {

                        let lastStrTok = retPeek.ReturnValue as IStrToken;

                        if (lastStrTok.strValue.toUpperCase() == "TRUE") {
                            this.stackOps.Pop(tokenStack);
                            this.stackOps.Push(tokenStack, this.stackElemStructs.CreateBoolToken(true));
                        }
                        else if (lastStrTok.strValue.toUpperCase() == "FALSE") {
                            this.stackOps.Pop(tokenStack);
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

                    // alle tokens rechts vom Kommentarzeichen bis zum Zeilenende als Kommentartext zusammenfassen
                    while (pc < strTokens.length) {
                        commentTxt += strTokens[pc] + " ";
                    }

                    this.stackOps.Push(tokenStack, this.stackElemStructs.CreateCommentToken(commentTxt));
                }
                // int einlesen
                else if (currentStrTok == this.opSym.rpnIntType) {

                    let retPeek = this.stackOps.Peek(tokenStack);

                    if (retPeek.Success && retPeek.ReturnValue.tokOpSym == this.opSym.rpnStrType) {

                        let lastStrTok = retPeek.ReturnValue as IStrToken;
                        let val = BigInt(lastStrTok.strValue);

                        if (typeof val) {
                            this.stackOps.Pop(tokenStack);
                            this.stackOps.Push(tokenStack, this.stackElemStructs.CreateIntToken(val))
                        }
                        else {
                            ret = new RCwithValue<IToken[]>(false, `pos ${pos}: int value is not a number ${lastStrTok}`, []);
                            break;
                        }
                    }
                    else {
                        ret = new RCwithValue<IToken[]>(false, `pos ${pos}: int token without value`, []);
                    }
                }
                // dbl- Token einlesen
                else if (currentStrTok == this.opSym.rpnNumType) {
                    let retPeek = this.stackOps.Peek(tokenStack);

                    if (retPeek.Success && retPeek.ReturnValue.tokOpSym == this.opSym.rpnStrType) {

                        let lastStrTok = retPeek.ReturnValue as IStrToken;
                        let val = Number(lastStrTok.strValue);

                        if (!isNaN(val)) {
                            this.stackOps.Pop(tokenStack);
                            this.stackOps.Push(tokenStack, this.stackElemStructs.CreateDblToken(val))
                        }
                        else {
                            ret = new RCwithValue<IToken[]>(false, `pos ${pos}: dbl value is not a number ${lastStrTok}`, []);
                            break;
                        }
                    }
                    else {
                        ret = new RCwithValue<IToken[]>(false, `pos ${pos}: dbl token without value`, []);
                    }
                }                
                // Listenende- Markierung einlesen 
                else if (currentStrTok == this.opSym.rpnListEnd) {

                    this.stackOps.Push(tokenStack, this.stackElemStructs.CreateListEndToken(pos));
                }
                // allg. Listenanfang Markierung einlesen
                else if (currentStrTok == this.opSym.rpnListStart) {

                    this.stackOps.Push(tokenStack, this.stackElemStructs.CreateListStartToken(pos));
                }
                else {

                    // Prüfen, ob nicht ein Funktionsname vorliegt
                    if (currentStrTok.startsWith(this.opSym.rpnFuncPrefix)) {

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
                ret = new RCwithValue<IToken[]>(true, ret.ErrorMsgIfNotSuccesful, tokenStack);
            }

            return ret;
        }
    }
}


