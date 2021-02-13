//<unit_header>
//----------------------------------------------------------------
//
// Martin Korneffel: IT Beratung/Softwareentwicklung
// Stuttgart, den 5.1.2015
//
//  Projekt.......: ArticleEdit
//  Name..........: Parser.js
//  Aufgabe/Fkt...: Parsen von Html- Text in der RPN- Form.
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

define(["./StringHlp", "./RPN", "./RPNHtml"], function (StringHlp, RPN, Html) {

    return function (inTxt) {

        let token = StringHlp.tokenize(inTxt);        
        let stack = [];        

        for (let pos = 0; pos < token.length; pos++) {

            if (Html.isBlockFuncToken(token[pos])) {

                let fname = RPN.ExtractFuncName(token[pos]);
                Html.BlockFuncs[fname](stack);

            } else if (Html.isInlineFuncToken(token[pos])) {

                let fname = RPN.ExtractFuncName(token[pos]);
                Html.InlineFuncs[fname](stack, RPN.ArgCount(token[pos]));

            } else {
                Html.Token(stack, token[pos]);                
            }
        }

        // Nach dem Parsen und Evaluieren der Html- Funktionen stehen im Stack aufgelöste Blockfunktionen und Argumente 
        // noch später aufzulösender Blockfunktionen.

        let htmlText = "";
        let Rest = "";
        let i = 0;

        for (; i < stack.length; i++) {

            if (!RPN.StackElemStructs.isBlockContent(stack[i]) && !RPN.StackElemStructs.isFunc(stack[i], "li")) {
                htmlText += stack[i].print();
            } else {
                break;
            }
        }

        // Die Argumente von noch nicht aufgelösten Blockfunktionen wieder in die RPN- Schreibweise umwandeln
        for (let j = i; j < stack.length; j++) {
            Rest += stack[j].printRPN();
        }

        // Noch nicht aufgelöste Blockfunktionen aus Stack löschen
        while (stack.length > i + 1) {
            stack.pop();
        }

        return {
            html: htmlText,
            Rest: Rest,
            Stack: stack
        };

    };

});
