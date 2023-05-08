// mko, 27.4.2023
// React- Komponente Ausw�hlen eines Namenscontainers
// Achtung: in der tsjson.config muss unter Compileroptions festgelegt sein: "jsx": "react"
//
// Das Ergebnis ist die NID des ausgew�hlten Naming- Containers.
var __importDefault = (this && this.__importDefault) || function (mod) {
    return (mod && mod.__esModule) ? mod : { "default": mod };
};
define(["require", "exports", "react", "react-dom", "jquery", "./NamingIds", "./NIDStr"], function (require, exports, react_1, react_dom_1, jquery_1, NamingIds_1, NIDStr_1) {
    "use strict";
    Object.defineProperty(exports, "__esModule", { value: true });
    react_1 = __importDefault(react_1);
    react_dom_1 = __importDefault(react_dom_1);
    jquery_1 = __importDefault(jquery_1);
    NamingIds_1 = __importDefault(NamingIds_1);
    NIDStr_1 = __importDefault(NIDStr_1);
    function NCAutocomplete(props) {
        let propsTyped = props;
        let [ncAutocompleteState, setNcAutocompleteState] = react_1.default.useState({
            NID: "0000",
            title: "",
            errLoadProposals: false,
            errLoadProposalsTxt: "",
            ncList: []
        });
        react_1.default.useEffect(() => {
            (0, jquery_1.default)("#wocTitleEdit").focus();
            (0, jquery_1.default)("#wocTitleEdit").html("_");
        });
        function setProposalAsTitle(ix, wocHeaderState) {
            return {
                NID: wocHeaderState.ncList[ix].NIDstr,
                title: wocHeaderState.ncList[ix].DE,
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
                // Der Title ist ohne Autocomplete zu �bernehmen.            
            }
            else if (userText.endsWith("#1")) {
                // Der erste Vorschlag ist an den Titel anzuh�ngen            
                setNcAutocompleteState(setProposalAsTitle(0, ncAutocompleteState));
            }
            else if (userText.endsWith("#2")) {
                // Der zweite Vorschlag ist an den Titel anzuh�ngen
                setNcAutocompleteState(setProposalAsTitle(1, ncAutocompleteState));
            }
            else if (userText.endsWith("#3")) {
                // Der dritten Vorschlag ist an den Titel anzuh�ngen
                setNcAutocompleteState(setProposalAsTitle(2, ncAutocompleteState));
            }
            else if (userText.endsWith("#4")) {
                // Der vierte Vorschlag ist an den Titel anzuh�ngen
                setNcAutocompleteState(setProposalAsTitle(3, ncAutocompleteState));
            }
            else {
                // Vorschl�ge vom Server laden
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
                    setNcAutocompleteState({
                        NID: ncAutocompleteState.NID,
                        title: userText,
                        errLoadProposals: false,
                        errLoadProposalsTxt: "",
                        ncList: _ncList
                    });
                })
                    .fail((jqXHR, textStatus, errorThrown) => {
                    setNcAutocompleteState({
                        NID: ncAutocompleteState.NID,
                        title: userText,
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
        return (react_1.default.createElement("div", { className: "ncAutocomplete" },
            react_1.default.createElement("div", { className: "ncAutocompleteInput" },
                react_1.default.createElement(NIDStr_1.default, { ServerOrigin: propsTyped.ServerOrigin, lng: propsTyped.LanguageNid, nid: (0, NamingIds_1.default)().MKPRG.Naming.NamingContainerNC, cssClass: "wocTitle" }),
                react_1.default.createElement("div", { className: "LLP-EditorLine" },
                    react_1.default.createElement("b", null, ">"),
                    react_1.default.createElement("span", { id: "#ncInput", contentEditable: true, onInput: e => processInput(e.currentTarget.textContent) })),
                react_1.default.createElement("ol", { className: "ncAutocompletePart" }, ncAutocompleteState.ncList.map(nc => react_1.default.createElement("li", null, nc.DE))),
                ncAutocompleteState.errLoadProposals ? react_1.default.createElement("div", null,
                    "Error: ",
                    ncAutocompleteState.errLoadProposalsTxt,
                    " ") : ""),
            react_1.default.createElement("div", { className: "ncAutocompleteChoice" },
                react_1.default.createElement("h1", null, "Woc Header"),
                react_1.default.createElement("dl", null,
                    react_1.default.createElement("dt", null, "Title"),
                    react_1.default.createElement("dd", null, ncAutocompleteState.title)))));
    }
    function NCAutocompleteCtrlSetUp(idRoot, ServerOrigin) {
        react_dom_1.default.render(react_1.default.createElement(NCAutocomplete, { ServerOrigin: ServerOrigin }), (0, jquery_1.default)(`#${idRoot}`)[0]);
    }
    exports.default = NCAutocompleteCtrlSetUp;
});
//# sourceMappingURL=NCAutocomplete.js.map