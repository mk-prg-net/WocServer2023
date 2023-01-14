//<unit_header>
//----------------------------------------------------------------
//
// Martin Korneffel: IT Beratung/Softwareentwicklung
// Stuttgart, den 5.1.2016
//
//  Projekt.......: ArticleEdit
//  Name..........: Html.js
//  Aufgabe/Fkt...: 
//                  
//
//
//
//
//<unit_environment>
//------------------------------------------------------------------
//  Zielmaschine..: PC 
//  Betriebssystem: Windows 7 mit .NET 4.5
//  Werkzeuge.....: Visual Studio 2013
//  Autor.........: Martin Korneffel (mko)
//  Version 1.0...: 
//
// </unit_environment>
//
//<unit_history>
//------------------------------------------------------------------
//
//  Version.......: 1.1
//  Autor.........: Martin Korneffel (mko)
//  Datum.........: 12.1.2023
//  Änderungen....: Umgestellt auf Typescript
//
//</unit_history>
//</unit_header>   
define(["require", "exports"], function (require, exports) {
    "use strict";
    Object.defineProperty(exports, "__esModule", { value: true });
    class RPNHtml {
        constructor(rpn, inlineFuncs, blockFuncs) {
            this.RPN = rpn;
            this.InlineFuncs = inlineFuncs;
            this.BlockFuncs = blockFuncs;
        }
        isBlockFuncToken(strToken) {
            return this.RPN.isFuncOfTypeToken(strToken, this.BlockFuncs);
        }
        isInlineFuncToken(strToken) {
            return this.RPN.isFuncOfTypeToken(strToken, this.InlineFuncs);
        }
        Token(stack, tok) {
            stack.push(this.RPN.StackElemStructs.CreateToken(tok));
        }
    }
    exports.default = RPNHtml;
});
//# sourceMappingURL=RPNHtml.js.map