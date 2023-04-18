var __importDefault = (this && this.__importDefault) || function (mod) {
    return (mod && mod.__esModule) ? mod : { "default": mod };
};
define(["require", "exports", "jquery", "./RPNHtml", "./Parser"], function (require, exports, jquery_1, RPNHtml_1, Parser_1) {
    "use strict";
    Object.defineProperty(exports, "__esModule", { value: true });
    jquery_1 = __importDefault(jquery_1);
    RPNHtml_1 = __importDefault(RPNHtml_1);
    Parser_1 = __importDefault(Parser_1);
    function Edit() {
        let main_data = [];
        let pos = 0;
        let rpnHtml = new RPNHtml_1.default();
        let parser = new Parser_1.default();
        //RPNHtml.Token(main_data, "Bitte Text eingeben !");
        rpnHtml.Token(main_data, "Bitte Text eingeben !");
        jquery_1.default("#edit_text").keydown(function () {
            if (window.event.keyCode == 13 && window.event.ctrlKey) {
                // Ctrl+Enter: Neuen Text übernehmen
                let txt = jquery_1.default(this).html();
                txt = txt.replace(/\&nbsp;/gi, " ")
                    .replace(/<div>/gi, " ")
                    .replace(/<\/div>/gi, " ")
                    .replace(/<p>/gi, " ")
                    .replace(/<\/p>/gi, " ")
                    .replace(/<br>/gi, " ")
                    .replace(/<br\/>/gi, " ");
                let res = parser.Parse(txt);
                let spliceargs = [];
                spliceargs.push(pos);
                spliceargs.push(1);
                res.Stack.forEach(elem => spliceargs.push(elem));
                main_data.splice.apply(main_data, spliceargs);
                pos = pos + res.Stack.length;
                // Analyse des Textes hier
                jquery_1.default("#pre_text").html(main_data.slice(0, pos).map(fn => fn.print()).join(" "));
                jquery_1.default(this).text(res.Rest);
                jquery_1.default("#post_text").html(main_data.slice(pos + 1, main_data.length).map(fn => fn.print()).join(" "));
            }
            else if (window.event.keyCode == 38 && window.event.ctrlKey) {
                // Ctrl+Arrow Up: Vorausgehenden Textabschnitt bearbeiten                
                if (pos > 0)
                    pos--;
                if (pos > 0) {
                    jquery_1.default("#pre_text").html(main_data.slice(0, pos).map(fn => fn.print()).join(" "));
                }
                else {
                    jquery_1.default("#pre_text").html("");
                }
                jquery_1.default(this).text(main_data[pos].printRPN());
                if (main_data.length > pos + 1) {
                    jquery_1.default("#post_text").html(main_data.slice(pos + 1, main_data.length).map(fn => fn.print()).join(" "));
                }
                else {
                    jquery_1.default("#post_text").html("");
                }
            }
            else if (window.event.keyCode == 40 && window.event.ctrlKey) {
                // Ctrl+Arrow Down: Vorausgehenden Textabschnitt bearbeiten                
                if (pos < main_data.length - 1)
                    pos++;
                if (pos > 0) {
                    jquery_1.default("#pre_text").html(main_data.slice(0, pos).map(fn => fn.print()).join(" "));
                }
                else {
                    jquery_1.default("#pre_text").html("");
                }
                jquery_1.default(this).text(main_data[pos].printRPN());
                if (main_data.length > pos + 1) {
                    jquery_1.default("#post_text").html(main_data.slice(pos + 1, main_data.length).map(fn => fn.print()).join(" "));
                }
                else {
                    jquery_1.default("#post_text").html("");
                }
            }
        });
    }
    exports.default = Edit;
});
//# sourceMappingURL=edit.js.map