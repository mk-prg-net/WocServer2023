// mko, 29.3.2023
// **Lukasiewicz List Processor**
define(["require", "exports", "./StackElemStructs"], function (require, exports, StackElemStructs_1) {
    "use strict";
    Object.defineProperty(exports, "__esModule", { value: true });
    class StackOps {
        constructor(opSym) {
            this.stackStructs = new StackElemStructs_1.default(opSym);
        }
        Peek(stack) {
            // Liefert den obersten Eintrag im Stack.
            if (stack.length > 0)
                return [true, stack[stack.length - 1]];
            else
                return [false, this.stackStructs.CreateNoneToken()];
        }
    }
    exports.default = StackOps;
});
//# sourceMappingURL=StackOps.js.map