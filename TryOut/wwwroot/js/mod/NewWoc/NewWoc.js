// mko, 13.4.2023
// React- Komponente zum Anlegen eines neuen Woc (Woc := Web Document)
// Achtung: in der tsjson.config muss unter Compileroptions festgelegt sein: "jsx": "react"
//
// Das Ergebnis ist (TitleId, AuthorId, NodeId, NameSpace) Triple. Dieses wird als DocuTerm an den Server Übermittelt
// #i wocHeader
//  #_
//      #p Title  #int TitleId          // Vordefiniert oder neu
//      #p Author #int AuthorId         // Muss aus einer Liste von vordefinierten entnommen werden
//      #p Node   #int NodeId           // Muss aus einer Liste von vordefinierten entnommen werden
//      #p NS     #str root/...         // Muss aus der Liste der existierenden ausgewählt werden
//  #.
// 
// Die Sparache kann ausgewählt werden
var __importDefault = (this && this.__importDefault) || function (mod) {
    return (mod && mod.__esModule) ? mod : { "default": mod };
};
define(["require", "exports", "react", "react-dom", "jquery", "../NamingIds"], function (require, exports, react_1, react_dom_1, jquery_1, NamingIds_1) {
    "use strict";
    Object.defineProperty(exports, "__esModule", { value: true });
    react_1 = __importDefault(react_1);
    react_dom_1 = __importDefault(react_dom_1);
    jquery_1 = __importDefault(jquery_1);
    NamingIds_1 = __importDefault(NamingIds_1);
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
            (0, jquery_1.default)("#wocTitleEdit").html("_");
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
            // Demo: get Neming- Id of Creator
            let CreatorNamingId = (0, NamingIds_1.default)().MKPRG.Naming.TechTerms.Lifecycle.Creator;
            userText = userText.trim();
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
                if (userText.endsWith("#")) {
                    userText = userText.substring(0, userText.length - 1);
                }
                else if (userText.endsWith("#1")) {
                    userText = userText.substring(0, userText.length - 2);
                }
                else if (userText.endsWith("#2")) {
                    userText = userText.substring(0, userText.length - 2);
                }
                else if (userText.endsWith("#3")) {
                    userText = userText.substring(0, userText.length - 2);
                }
                else if (userText.endsWith("#4")) {
                    userText = userText.substring(0, userText.length - 2);
                }
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
            react_1.default.createElement("div", { className: "wocHeaderEdit" },
                react_1.default.createElement("div", { className: "LLP-EditorLine" },
                    react_1.default.createElement("b", null, ">"),
                    react_1.default.createElement("span", { id: "#wocTitleEdit", contentEditable: true, onInput: e => processInput(e.currentTarget.textContent) }))
            // Hier wird der Autocomplete- Vorschlag eingeblendet
            ,
                "// Hier wird der Autocomplete- Vorschlag eingeblendet",
                react_1.default.createElement("ol", { className: "wocTitleAutocompletePart" }, wocHeaderState.ncList.map(nc => react_1.default.createElement("li", null, nc.de))),
                wocHeaderState.errLoadProposals ? react_1.default.createElement("div", null,
                    "Error: ",
                    wocHeaderState.errLoadProposalsTxt,
                    " ") : ""),
            react_1.default.createElement("div", { className: "wocHeaderView" },
                react_1.default.createElement("h1", null, "Woc Header"),
                react_1.default.createElement("dl", null,
                    react_1.default.createElement("dt", null, "Title"),
                    react_1.default.createElement("dd", null, wocHeaderState.title)))));
    }
    function WocHeaderReactCtrlSetUp(idRoot, ServerOrigin) {
        react_dom_1.default.render(react_1.default.createElement(NewWocReact, { ServerOrigin: ServerOrigin }), (0, jquery_1.default)(`#${idRoot}`)[0]);
    }
    exports.default = WocHeaderReactCtrlSetUp;
});
//# sourceMappingURL=NewWoc.js.map