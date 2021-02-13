//<unit_header>
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
//  Datum.........: 
//  Änderungen....: 
//
//</unit_history>
//</unit_header> 

"use strict";

define(["./StringHlp"], function (StringHlp)
{
    return {

        isFuncName: function (inString) {
            // true, wenn das Token den Namen einer RPN- Funktion darstellt,  sonst false

            return inString.length > 0 && inString.substring(0, 1) === '#';            
        },

        ExtractFuncName: function(token){
            return token.substring(token.lastIndexOf("#") + 1);
        },

        ArgCount: function (rpnFuncName) {

            return rpnFuncName.lastIndexOf('#') + 1;
        },

        Peek : function(stack) {
            if (stack.length > 0)
                return stack[stack.length - 1];
            else
                return {};
        },

        EvalInlineFunc : function(stack, Tag, argc) {
            // Alle zur OL gehörenden li vom Stack entfernen
            let stackElems = [];

            for (let i = 0; i < argc; i++) {
                
                stackElems.unshift(stack.pop());
            }

            // Token in einem p- Element verpacken und auf den Stack legen.
            let newStackElem = this.StackElemStructs.CreateInlineFunc(Tag, stackElems);
            stack.push(newStackElem);

        },


        EvalBlockFunc : function(stack, Tag, isArgTypeTest) {
            // Alle zur OL gehörenden li vom Stack entfernen
            let stackElems = [];

            while(isArgTypeTest(this.Peek(stack)))
            {
                stackElems.unshift(stack.pop());
            }

            // Token in einem p- Element verpacken und auf den Stack legen.
            let newStackElem = this.StackElemStructs.CreateBlockFunc(Tag, stackElems);
            stack.push(newStackElem);

        },

        StackElemStructs: {

            CreateToken: function (token) {
                return {
                    tok: token,
                    print: function () { return this.tok;},
                    printRPN: function() { return this.tok;}
                };
            },

            isToken: function (stackElem) {
                return "tok" in stackElem;
            },

            isBlockContent: function(stackElem){
                // Liefert true, wenn das Element zum Inhalt einer Blockfunktion gehört

                return this.isToken(stackElem) || this.isFunc(stackElem, "b") || this.isFunc(stackElem, "i") || this.isFunc(stackElem, "sub") || this.isFunc(stackElem, "sup");
            },

            CreateInlineFunc: function (TagName, args) {
                // Klassenfabrik für Stack- Elemente, die Inlinefunktionen darstellen

                let thisStackElemStructs = this;

                if(args.length > 0){
                    return {
                        Tag: TagName,
                        Args: args,
                        print: function () {
                            return "<" + this.Tag + ">"
                                        + this.Args.map(function (arg) { return arg.print(); }).join(" ")
                                        + "</" + this.Tag + ">";
                        },
                        printRPN: function() { 
                            // Die Anzahl der Argumente wird bei Inline- Funktionen durch wiederholte # kodiert
                            return "\n" + this.Args.map(function (arg) { return arg.printRPN(); }).join(" ")
                                        + "\n"
                                        + "#".repeat(this.Args.length)
                                        + this.Tag
                                        + "\n\n";
                        }
                    };
                } else {
                    return {
                        Tag: TagName,
                        Args: [thisStackElemStructs.CreateToken("")],
                        print : function () {
                            return "<" + this.Tag + "></" + this.Tag + ">\n";
                        },
                        printRPN : function () {
                            return "\n#" + this.Tag + "\n\n";
                        }
                    };

                }
            },

            CreateBlockFunc: function (TagName, args) {
                // Klassenfabrik für Stack- Elemente, die Blockfunktionen darstellen

                let thisStackElemStructs = this;

                if(args.length > 0){
                    return {
                        Tag: TagName,
                        Args: args,
                        print: function () {
                            return "<" + this.Tag + ">\n"
                                        + this.Args.map(function (arg) { return arg.print(); }).join(" ")
                                        + "\n</" + this.Tag + ">\n";
                        },
                        printRPN: function() { 
                            return "\n" + this.Args.map(function (arg) { return arg.printRPN(); }).join(" ")
                                        + "\n#" + this.Tag + "\n\n";
                        }
                    };
                } else {
                    return {
                        Tag: TagName,
                        Args: [thisStackElemStructs.CreateToken("")],
                        print : function () {
                            return "<" + this.Tag + "></" + this.Tag + ">\n";
                        },
                        printRPN : function () {
                            return "\n#" + this.Tag + "\n\n";
                        }
                    };

                }
            },


            isFunc: function (stackElem, Tag) {
                // Prädikat, gibt true zurück, wenn auf dem Stack eine Blockfunktion liegt.

                return ("Tag" in stackElem) && ("Args" in stackElem) && (stackElem.Tag === Tag);                
            },


        },

    };
});
