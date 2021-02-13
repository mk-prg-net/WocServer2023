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
//  Datum.........: 
//  Änderungen....: 
//
//</unit_history>
//</unit_header>   

"use strict";

define(["./StringHlp", "./RPN"], function (StringHlp, RPN) {

    var isFuncOfTypeToken = function (token, FuncType) {
        // Hilfsfunktion. Liefert true zurück, wenn das Token dem Namen einer Funktion entspricht, 
        // die den erwarteten Funktionstyp hat.

        var res = false;

        if (RPN.isFuncName(token)) {

            // Abschneiden aller führender #
            let FuncName = RPN.ExtractFuncName(token);

            // Prüfen, ob token einem bekannten Funktionsnamen entspricht
            res = (FuncName in FuncType);            
        }

        return res;
    };

    var Html = {

        isBlockFuncToken: function (token) {
            return isFuncOfTypeToken(token, this.BlockFuncs);
        },

        isInlineFuncToken: function (token) {
            return isFuncOfTypeToken(token, this.InlineFuncs);
        },

        Token: function(stack, tok) {
            stack.push(RPN.StackElemStructs.CreateToken(tok));
        },

        InlineFuncs: {

            b: function (stack, argc) {
                RPN.EvalInlineFunc(stack, "b", argc);
            },

            i: function (stack, argc) {
                RPN.EvalInlineFunc(stack, "i", argc);
            },

            sub: function (stack, argc) {
                RPN.EvalInlineFunc(stack, "sub", argc);
            },

            sup: function (stack, argc) {
                RPN.EvalInlineFunc(stack, "sup", argc);
            },

        },

        BlockFuncs: {
            p: function (stack) {
                RPN.EvalBlockFunc(stack, "p", RPN.StackElemStructs.isBlockContent.bind(RPN.StackElemStructs));
            },

            pre: function (stack) {

                throw new Error("nicht implementiert");
            },

            ol: function (stack) {
                RPN.EvalBlockFunc(stack, "ol", function (stackElem) { return RPN.StackElemStructs.isFunc(stackElem, "li"); });
            },

            ul: function (stack) {
                RPN.EvalBlockFunc(stack, "ul", function (stackElem) { return RPN.StackElemStructs.isFunc(stackElem, "li"); });
            },

            li: function (stack) {
                if (RPN.StackElemStructs.isFunc(RPN.Peek(stack), "ol") || RPN.StackElemStructs.isFunc(RPN.Peek(stack), "ul")) {
                    let liElem = RPN.StackElemStructs.CreateBlockFunc("li", [stack.pop()]);
                    stack.push(liElem);
                } else {
                    RPN.EvalBlockFunc(stack, "li", RPN.StackElemStructs.isBlockContent.bind(RPN.StackElemStructs));
                }
            },

            h1: function (stack) {
                RPN.EvalBlockFunc(stack, "h1", RPN.StackElemStructs.isBlockContent.bind(RPN.StackElemStructs));
            },

            h2: function (stack) {
                RPN.EvalBlockFunc(stack, "h2", RPN.StackElemStructs.isBlockContent.bind(RPN.StackElemStructs));
            },

            h3: function (stack) {
                RPN.EvalBlockFunc(stack, "h3", RPN.StackElemStructs.isBlockContent.bind(RPN.StackElemStructs));
            },

            h4: function (stack) {
                RPN.EvalBlockFunc(stack, "h4", RPN.StackElemStructs.isBlockContent.bind(RPN.StackElemStructs));
            },

            h5: function (stack) {
                RPN.EvalBlockFunc(stack, "h5", RPN.StackElemStructs.isBlockContent.bind(RPN.StackElemStructs));
            },

            h6: function (stack) {
                RPN.EvalBlockFunc(stack, "h6", RPN.StackElemStructs.isBlockContent.bind(RPN.StackElemStructs));
            },
        }
    };

    return Html;
});