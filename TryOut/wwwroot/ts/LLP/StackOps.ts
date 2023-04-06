// mko, 29.3.2023
// **Lukasiewicz List Processor**

import IOpSym from "./OpSyms/IOpSym";
import IFunction from "./Tokenizer/IFunction";
import IToken from "./Tokenizer/IToken";
import IRC from "./RC/IRC";
import IRCwithValue from "./RC/IRCwithValue";
import RC from "./RC/RC";
import RCwithVal from "./RC/RCwithValue";
import IIntToken from "./Tokenizer/IIntToken";
import INumToken from "./Tokenizer/INumToken";
import IStrToken from "./Tokenizer/IStrToken";
import IListToken from "./Tokenizer/IListToken";
import IBoolToken from "./Tokenizer/IBoolToken";
import ICommentToken from "./Tokenizer/ICommentToken";

import StackElemStructsClass from "./StackElemStructs";
import RCwithValue from "./RC/RCwithValue";

export default class StackOps
{
    stackStructs: StackElemStructsClass;

    constructor(opSym: IOpSym)
    {
        this.stackStructs = new StackElemStructsClass(opSym);
    }

    NewStack(): IToken[]
    {
        return [];
    }

    Peek(stack: IToken[]): RCwithValue<IToken>
    {
        // Liefert den obersten Eintrag im Stack.
        if (stack.length > 0)
            return new RCwithValue<IToken>(true, "ok", stack[stack.length-1]);
        else
            return new RCwithValue<IToken>(false, "stack is empty", this.stackStructs.CreateNoneToken());
    }

    Pop(stack: IToken[]): RCwithValue<IToken>
    {
        // Liefert den obersten Eintrag im Stack.
        if (stack.length > 0)
            return new RCwithValue<IToken>(true, "ok", stack.pop());
        else
            return new RCwithValue<IToken>(false, "stack is empty", this.stackStructs.CreateNoneToken());
    }

    Push(stack: IToken[], token: IToken) : boolean
    {
        stack.push(token);
        return true;
    }
}