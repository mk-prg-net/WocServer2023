var __importDefault = (this && this.__importDefault) || function (mod) {
    return (mod && mod.__esModule) ? mod : { "default": mod };
};
define(["require", "exports", "react", "jquery"], function (require, exports, react_1, jquery_1) {
    "use strict";
    Object.defineProperty(exports, "__esModule", { value: true });
    react_1 = __importDefault(react_1);
    jquery_1 = __importDefault(jquery_1);
    // Default- Namingcontainer
    var UnkownNC = {
        CNT: "unknown",
        DE: "unbekannt",
        EditShortCut: "unknown",
        EN: "unknown",
        Glyph: " ",
        GlyphUniCode: " ",
        NIDstr: "unknown"
    };
    // List of all NYT Keywords. Must be loaded from Server
    var nytKeywords = [UnkownNC];
    // Mapping Key Board Shortcuts to Nyt Naming- Container.
    var editShortCuts = { "none": UnkownNC };
    function CrossWriter(properties) {
        // Define initial State
        let [state, setState] = react_1.default.useState({
            init: true,
            document: {
                autorUserId: properties.UserId,
                ColCount: 0,
                currentColNo: 0,
                currentLineNo: 0,
                documentName: properties.DocumentName,
                LineCount: 0
            },
            statusText: "start"
        });
        function LoadResourcesFromServer() {
            if (state.init) {
                jquery_1.default.ajax(`${properties.ServerOrigin}/NamingContainers?NC=${properties.NameSpaceNytNamingContainers}`, { method: "GET" })
                    .done((data, textStatus, jqXhr) => {
                    let _ncList = data;
                    nytKeywords = _ncList;
                    // Dictionary mit den Short Cuts aufbauen
                    for (var i = 0, _ncListCount = _ncList.length; i < _ncListCount; i++) {
                        var nc = nytKeywords[i];
                        editShortCuts[nc.EditShortCut] = nc;
                    }
                    // Zustand der React- Komponente neu setzten und rendern
                    setState({
                        init: false,
                        document: state.document,
                        statusText: "Resources loaded successful from Server"
                    });
                })
                    .fail((jqXHR, textStatus, errorThrown) => {
                    let errTxt = `HTTP Status:${textStatus}, ${errorThrown}`;
                    // Zustand der React- Komponente neu setzten und rendern
                    setState({
                        init: false,
                        document: state.document,
                        statusText: errTxt
                    });
                });
            }
        }
        react_1.default.useEffect(() => LoadResourcesFromServer(), []);
        return (react_1.default.createElement("div", { id: "CrossWriter", className: properties.cssClass },
            react_1.default.createElement("header", null,
                react_1.default.createElement("nav", { id: "main_nav" },
                    react_1.default.createElement("button", { id: "btnNewFile", className: "btn btn-normal" }, "\uD83D\uDDCB New"),
                    react_1.default.createElement("button", { id: "btnOpenFile", className: "btn btn-normal" }, "\uD83D\uDDBA Open"),
                    react_1.default.createElement("button", { id: "btnSave", className: "btn btn-normal" }, "\uD83D\uDDAB Save"),
                    react_1.default.createElement("button", { id: "help", className: "btn btn-normal" }, "\uD83D\uDD6E Help")),
                react_1.default.createElement("div", { id: "pre", className: "row" },
                    react_1.default.createElement("div", { id: "pre_text_L", className: "col col-4" }),
                    react_1.default.createElement("div", { id: "pre_text", className: "col col-8" }),
                    react_1.default.createElement("div", { id: "pre_text_R", className: "col col-4" })),
                react_1.default.createElement("div", { id: "edit", className: "row" },
                    react_1.default.createElement("div", { id: "edit_text_L", className: "col col-4" }),
                    react_1.default.createElement("div", { className: "col col-8" },
                        react_1.default.createElement("div", { id: "edit_text", contentEditable: "true", className: "EditLine" })),
                    react_1.default.createElement("div", { id: "edit_text_R", className: "col col-4" }, "\u2328")),
                react_1.default.createElement("div", { id: "post", className: "row" },
                    react_1.default.createElement("div", { id: "post_text_L", className: "col col-4" }),
                    react_1.default.createElement("div", { id: "post_text", className: "col col-8" }),
                    react_1.default.createElement("div", { id: "post_text_R", className: "col col-4" })))));
    }
    exports.default = CrossWriter;
});
//# sourceMappingURL=CrossWriter.js.map