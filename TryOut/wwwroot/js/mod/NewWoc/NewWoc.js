// mko, 13.4.2023
// React- Komponente zum Anlegen eines neuen Woc (Woc := Web Document)
// Achtung: in der tsjson.config muss unter Compileroptions festgelegt sein: "jsx": "react"
var __importDefault = (this && this.__importDefault) || function (mod) {
    return (mod && mod.__esModule) ? mod : { "default": mod };
};
define(["require", "exports", "react", "react-dom", "jquery"], function (require, exports, react_1, react_dom_1, jquery_1) {
    "use strict";
    Object.defineProperty(exports, "__esModule", { value: true });
    react_1 = __importDefault(react_1);
    react_dom_1 = __importDefault(react_dom_1);
    jquery_1 = __importDefault(jquery_1);
    function NewWocReact(props) {
        let propsTyped = props;
        let [wocHeader, setWocHeader] = react_1.default.useState({
            wocId: "none",
            title: "",
            authorId: "none",
            author: "",
            threadId: "none",
            errLoadProposals: false,
            errLoadProposalsTxt: "",
            ncList: []
        });
        react_1.default.useEffect(() => {
            (0, jquery_1.default)("#wocTitleEdit").focus();
            let el = (0, jquery_1.default)("#wocTitleEdit")[0];
            //    window.getSelection().selectAllChildren(el);
            //    window.getSelection().collapseToEnd();        
        });
        let wocHeaderState = wocHeader;
        function setProposalAsTitle(ix, wocHeaderState) {
            return {
                wocId: wocHeaderState.ncList[ix].id,
                title: wocHeaderState.ncList[ix].de,
                authorId: wocHeaderState.authorId,
                author: wocHeaderState.author,
                threadId: wocHeaderState.threadId,
                errLoadProposals: false,
                errLoadProposalsTxt: "",
                ncList: wocHeaderState.ncList
            };
        }
        function processInput(userText) {
            if (userText === "") {
                // Noch kein Text eingegeben
            }
            else if (userText.endsWith("#0")) {
                // Der Title ist ohne Autocomplete zu übernehmen.            
            }
            else if (userText.endsWith("#1")) {
                // Der erste Vorschlag ist an den Titel anzuhängen            
                setWocHeader(setProposalAsTitle(0, wocHeaderState));
            }
            else if (userText.endsWith("#2")) {
                // Der zweite Vorschlag ist an den Titel anzuhängen
                setWocHeader(setProposalAsTitle(1, wocHeaderState));
            }
            else if (userText.endsWith("#3")) {
                // Der dritten Vorschlag ist an den Titel anzuhängen
                setWocHeader(setProposalAsTitle(2, wocHeaderState));
            }
            else if (userText.endsWith("#4")) {
                // Der vierte Vorschlag ist an den Titel anzuhängen
                setWocHeader(setProposalAsTitle(3, wocHeaderState));
            }
            else {
                // Vorschläge vom Server laden
                let params = JSON.stringify({ titleStart: userText });
                jquery_1.default.ajax(`${propsTyped.ServerOrigin}/WocTitlesStartsWith`, { method: "POST", contentType: "application/json", data: params })
                    .done((data, textStatus, jqXhr) => {
                    let _ncList = data;
                    setWocHeader({
                        wocId: wocHeaderState.wocId,
                        title: userText,
                        authorId: wocHeaderState.authorId,
                        author: wocHeaderState.author,
                        threadId: wocHeaderState.threadId,
                        errLoadProposals: false,
                        errLoadProposalsTxt: "",
                        ncList: _ncList
                    });
                })
                    .fail((jqXHR, textStatus, errorThrown) => {
                    setWocHeader({
                        wocId: wocHeaderState.wocId,
                        title: userText,
                        authorId: wocHeaderState.authorId,
                        author: wocHeaderState.author,
                        threadId: wocHeaderState.threadId,
                        errLoadProposals: true,
                        errLoadProposalsTxt: `HTTP Status:${textStatus}, ${errorThrown}`,
                        ncList: []
                    });
                });
            }
        }
        function txtHead(txt) {
            if (txt.length > 1) {
                return txt.substring(txt.length - 2);
            }
            else {
                return "";
            }
        }
        function txtLast(txt) {
            if (txt.length > 1) {
                return txt.substring(txt.length - 1, txt.length);
            }
            else if (txt.length == 1) {
                return txt;
            }
            else {
                return "";
            }
        }
        return (react_1.default.createElement("div", { className: "wocHeader" },
            "// Es kann ein neuer Titel definiert werden. Das erzeugt eine neue wocId // Oder es wird ein vorhandener Titel ausgew\u00E4hlt. // Die Auswahl kann explizit erfolgen, oder es wird eine Autocomplete- Vervollst\u00E4ndigung angeboten.",
            react_1.default.createElement("div", { id: "", className: "wocTitleMe" },
                txtHead(wocHeaderState.title),
                "[",
                react_1.default.createElement("span", { contentEditable: true, onInput: e => processInput(e.currentTarget.textContent) }, txtLast(wocHeaderState.title)),
                "]")
        // Hier wird der Autocomplete- vorschlag eingeblendet
        ,
            "// Hier wird der Autocomplete- vorschlag eingeblendet",
            react_1.default.createElement("ol", { className: "wocTitleAutocompletePart" }, wocHeaderState.ncList.map(nc => react_1.default.createElement("li", null, nc.de))),
            wocHeaderState.errLoadProposals ? react_1.default.createElement("div", null,
                "Error: ",
                wocHeaderState.errLoadProposalsTxt,
                " ") : ""));
    }
    function WocHeaderReactCtrlSetUp(idRoot, ServerOrigin) {
        react_dom_1.default.render(react_1.default.createElement(NewWocReact, { ServerOrigin: ServerOrigin }), (0, jquery_1.default)(`#${idRoot}`)[0]);
    }
    exports.default = WocHeaderReactCtrlSetUp;
});
//# sourceMappingURL=NewWoc.js.map