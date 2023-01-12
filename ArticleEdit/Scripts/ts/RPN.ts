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
//  Datum.........: 9.1.2023
//  Änderungen....: Mit Typescript erweitert
//
//</unit_history>
//</unit_header> 



import IFunction from "./IFunction";
import IPrintable from "./IPrintable";
import IToken from "./IToken";
import StringHlp from "./StringHlp"

export default class RPN {

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

    public StackElemStructs = {

        CreateToken(token): IToken {
            return {
                tok: token,
                print: function () { return this.tok; },
                printRPN: function () { return this.tok; }
            };
        },

        isToken(stackElem: IPrintable): boolean {
            return "tok" in stackElem;
        },

        isBlockContent(stackElem): boolean {
            // Liefert true, wenn das Element zum Inhalt einer Blockfunktion gehört
            return this.isToken(stackElem) || this.isFunc(stackElem, "b") || this.isFunc(stackElem, "i") || this.isFunc(stackElem, "sub") || this.isFunc(stackElem, "sup");
        },

        CreateInlineFunc: function (TagName, args) {
            // Klassenfabrik für Stack- Elemente, die Inlinefunktionen darstellen

            let thisStackElemStructs = this;

            if (args.length > 0) {
                return {
                    Tag: TagName,
                    Args: args,
                    print: function () {
                        return "<" + this.Tag + ">"
                            + this.Args.map(function (arg) { return arg.print(); }).join(" ")
                            + "</" + this.Tag + ">";
                    },
                    printRPN: function () {
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
                    print: function () {
                        return "<" + this.Tag + "></" + this.Tag + ">\n";
                    },
                    printRPN: function () {
                        return "\n#" + this.Tag + "\n\n";
                    }
                };

            }
        },

        CreateBlockFunc(TagName: string, args: IPrintable[]): IFunction {
            // Klassenfabrik für Stack- Elemente, die Blockfunktionen darstellen

            let thisStackElemStructs = this;

            if (args.length > 0) {
                return {
                    Tag: TagName,
                    Args: args,
                    print(): string {
                        return "<" + this.Tag + ">\n"
                            + this.Args.map(function (arg) { return arg.print(); }).join(" ")
                            + "\n</" + this.Tag + ">\n";
                    },
                    printRPN(): string {
                        return "\n" + this.Args.map(function (arg) { return arg.printRPN(); }).join(" ")
                            + "\n#" + this.Tag + "\n\n";
                    }
                };
            } else {
                return {
                    Tag: TagName,
                    Args: [thisStackElemStructs.CreateToken("")],
                    print: function () {
                        return "<" + this.Tag + "></" + this.Tag + ">\n";
                    },
                    printRPN: function () {
                        return "\n#" + this.Tag + "\n\n";
                    }
                };

            }
        },


        isFunc(stackElem : IPrintable, Tag: string): boolean {
            // Prädikat, gibt true zurück, wenn auf dem Stack eine Blockfunktion liegt.            
            if (("Tag" in stackElem) && ("Args" in stackElem)) {
                let func = stackElem as IFunction;
                return func.Tag === Tag;
            }
            else {
                return false;
            }
        }
    };
};

