// mko, 29.3.2023
// **Lukasiewicz List Processor**

import IOpSym from "./IOpSym";
import IFunction from "./IFunction";
import IToken from "./IToken";
import IIntToken from "./IIntToken";
import IDblToken from "./IDblToken";
import IStrToken from "./IStrToken";
import IListToken from "./IListToken";
import IBoolToken from "./IBoolToken";
import ICommentToken from "./ICommentToken";

import StackElemStructsClass from "./StackElemStructs";

export default class StackOps
{
    stackStructs: StackElemStructsClass;

    constructor(opSym: IOpSym)
    {
        this.stackStructs = new StackElemStructsClass(opSym);
    }

    Peek(stack: IToken[]): [stackIsNotEmpty: boolean, tok: IToken]
    {
        // Liefert den obersten Eintrag im Stack.
        if (stack.length > 0)
            return [true, stack[stack.length - 1]];
        else
            return [false,  this.stackStructs.CreateNoneToken()];
    }
}