// mko, 13.4.2023
// React- Komponente zum Anlegen eines neuen Woc (Woc := Web Document)
// Achtung: in der tsjson.config muss unter Compileroptions festgelegt sein: "jsx": "react"
var __importDefault = (this && this.__importDefault) || function (mod) {
    return (mod && mod.__esModule) ? mod : { "default": mod };
};
define(["require", "exports", "react"], function (require, exports, react_1) {
    "use strict";
    Object.defineProperty(exports, "__esModule", { value: true });
    react_1 = __importDefault(react_1);
    function NewWocReact(props) {
        let [wocHeader, setWocHeader] = react_1.default.useState({
            wocId: "none",
            title: "",
            authorId: "none",
            author: "",
            threadId: "none"
        });
        if (wocHeader.title.endsWith("+")) {
            // Der Autocomplete Vorschag wurde angenommen.
        }
        else if (wocHeader.title.endsWith("#")) {
            // Der Title ist ohne Autocomplete zu übernehmen.
        }
        // Bestimmen des Autocomplete Textvorschlages
        let Autocomplete = ""; // Hier muss ein ajax- Call erfolgen
        // Teil bestimmen, der als Autocomplete Part eingeblendet wird
        let AutocompletePart = "";
        return (react_1.default.createElement("div", { className: "wocHeader" },
            "// Es kann ein neuer Titel definiert werden. Das erzeugt eine neue wocId // Oder es wird ein vorhandener Titel ausgew\u00E4hlt. // Die Auswahl kann explizit erfolgen, oder es wird eine Autocomplete- Vervollst\u00E4ndigung angeboten.",
            react_1.default.createElement("span", { className: "wocTitleMe", contentEditable: true, onInput: e => setWocHeader({
                    wocId: wocHeader.wocId,
                    title: e.currentTarget.textContent,
                    authorId: wocHeader.authorId,
                    author: wocHeader.author,
                    threadId: wocHeader.threadId
                }) })
        // Hier wird der Autocomplete- vorschlag eingeblendet
        ,
            "// Hier wird der Autocomplete- vorschlag eingeblendet",
            react_1.default.createElement("span", { className: "wocTitleAutocompletePart" }, AutocompletePart)));
    }
});
//# sourceMappingURL=NewWoc.js.map