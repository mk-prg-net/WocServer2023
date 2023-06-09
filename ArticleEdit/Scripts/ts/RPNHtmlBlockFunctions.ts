﻿// mko, 13.1.2023

import IPrintable from "./IPrintable";
import IFunction from "./IFunction";
import RPNclass from "./RPN";
import StackElemStructsClass from "./StackElemFuncs"

export default class BlockFuncs {

    constructor(rpn: RPNclass) {
        this.RPN = rpn;
    }

    RPN: RPNclass;

    p(stack) {
        this.RPN.EvalBlockFunc(stack, "p", this.RPN.StackElemStructs.isBlockContent.bind(this.RPN.StackElemStructs));
    }

    pre(stack) {

        throw new Error("nicht implementiert");
    }

    ol(stack) {
        let that = this;
        this.RPN.EvalBlockFunc(
            stack,
            "ol",
            function (stackElem) {
                // Achtung: this ist hier das this von function. Deshalb anstatt this that
                return that.RPN.StackElemStructs.isFunc(stackElem, "li");
            });
    }

    ul(stack) {
        let that = this;
        this.RPN.EvalBlockFunc(
            stack,
            "ul",
            function (stackElem) {
                // Achtung: this ist hier das this von function. Deshalb anstatt this that
                return that.RPN.StackElemStructs.isFunc(stackElem, "li");
            });
    }

    li (stack) {
        if (this.RPN.StackElemStructs.isFunc(this.RPN.Peek(stack), "ol") || this.RPN.StackElemStructs.isFunc(this.RPN.Peek(stack), "ul")) {
            let liElem = this.RPN.StackElemStructs.CreateBlockFunc("li", [stack.pop()]);
            stack.push(liElem);
        } else {
            this.RPN.EvalBlockFunc(stack, "li", this.RPN.StackElemStructs.isBlockContent.bind(this.RPN.StackElemStructs));
        }
    }

    h1(stack) {
        this.RPN.EvalBlockFunc(stack, "h1", this.RPN.StackElemStructs.isBlockContent.bind(this.RPN.StackElemStructs));
    }

    h2(stack) {
        this.RPN.EvalBlockFunc(stack, "h2", this.RPN.StackElemStructs.isBlockContent.bind(this.RPN.StackElemStructs));
    }

    h3(stack) {
        this.RPN.EvalBlockFunc(stack, "h3", this.RPN.StackElemStructs.isBlockContent.bind(this.RPN.StackElemStructs));
    }

    h4(stack) {
        this.RPN.EvalBlockFunc(stack, "h4", this.RPN.StackElemStructs.isBlockContent.bind(this.RPN.StackElemStructs));
    }

    h5(stack) {
        this.RPN.EvalBlockFunc(stack, "h5", this.RPN.StackElemStructs.isBlockContent.bind(this.RPN.StackElemStructs));
    }

    h6(stack_) {
        this.RPN.EvalBlockFunc(stack_, "h6", this.RPN.StackElemStructs.isBlockContent.bind(this.RPN.StackElemStructs));
    }
}
