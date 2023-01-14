﻿//<unit_header>
//----------------------------------------------------------------
//
// Martin Korneffel: IT Beratung/Softwareentwicklung
// Stuttgart, den 5.12.2016
//
//  Projekt.......: ArticleEdit
//  Name..........: RPN.js
//  Aufgabe/Fkt...: Hilfsfunktionen für einen Reverse Polish Notation Parser und Evaluator
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
//  Datum.........: 9.1.2023
//  Änderungen....: Mit Typescript erweitert
//
//</unit_history>
//</unit_header> 



import IFunction from "./IFunction";
import IPrintable from "./IPrintable";
import IToken from "./IToken";
import StringHlp from "./StringHlp"
import StackElemStructsClass from "./StackElemFuncs"

export default class RPN {

    StackElemStructs = new StackElemStructsClass();

    isFuncName(inString: string): boolean {
        // true, wenn das Token den Namen einer RPN- Funktion darstellt,  sonst false
        return inString.length > 0 && inString.substring(0, 1) === '#';            
    }

    ExtractFuncName(token: string): string{
        return token.substring(token.lastIndexOf("#") + 1);
    }

    ArgCount(rpnFuncName: string): number {
        return rpnFuncName.lastIndexOf('#') + 1;
    }

    isFuncOfTypeToken(strToken: string, FuncType): boolean {
        // Hilfsfunktion. Liefert true zurück, wenn das Token dem Namen einer Funktion entspricht, 
        // die den erwarteten Funktionstyp hat.

        var res = false;

        if (this.isFuncName(strToken)) {

            // Abschneiden aller führender #
            let FuncName = this.ExtractFuncName(strToken);

            // Prüfen, ob token einem bekannten Funktionsnamen entspricht
            res = (FuncName in FuncType);
        }

        return res;
    };


    Peek(stack: IPrintable[]) : IPrintable {
        if (stack.length > 0)
            return stack[stack.length - 1];
        else
            return {
                print: () => "",
                printRPN: () => ""
            };
    }

    EvalInlineFunc(stack: IPrintable[], Tag: string, argc: number) {
        // Alle zur OL gehörenden li vom Stack entfernen
        let stackElems: IPrintable[] = [];

        for (let i = 0; i < argc; i++) {                
            stackElems.unshift(stack.pop());
        }

        // Token in einem p- Element verpacken und auf den Stack legen.
        let newStackElem = this.StackElemStructs.CreateInlineFunc(Tag, stackElems);
        stack.push(newStackElem);
    }

    EvalBlockFunc(stack, Tag, isArgTypeTest) {
        // Alle zur OL gehörenden li vom Stack entfernen
        let stackElems = [];

        while(isArgTypeTest(this.Peek(stack)))
        {
            stackElems.unshift(stack.pop());
        }

        // Token in einem p- Element verpacken und auf den Stack legen.
        let newStackElem = this.StackElemStructs.CreateBlockFunc(Tag, stackElems);
        stack.push(newStackElem);
    }
};

