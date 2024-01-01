// mko, 28.12.2023
// Main react Component of CrossWriter
var __importDefault = (this && this.__importDefault) || function (mod) {
    return (mod && mod.__esModule) ? mod : { "default": mod };
};
define(["require", "exports", "react", "jquery", "./IDocument"], function (require, exports, react_1, jquery_1, IDocument_1) {
    "use strict";
    Object.defineProperty(exports, "__esModule", { value: true });
    react_1 = __importDefault(react_1);
    jquery_1 = __importDefault(jquery_1);
    const CountVisibleLines = 31;
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
    function CrossWriter(properties) {
        // Define initial State
        let [state, setState] = react_1.default.useState({
            init: true,
            nytKeywords: [UnkownNC],
            editShortCuts: { "none": UnkownNC },
            document: {
                autorUserId: properties.UserId,
                documentName: properties.DocumentName,
                textLines: [""],
                LineCount: () => 0
            },
            cursor: { currentLineNo: 0, currentColNo: 0 },
            visibleLines: CountVisibleLines,
            statusText: "start"
        });
        function LoadResourcesFromServer() {
            if (state.init) {
                jquery_1.default.ajax(`${properties.ServerOrigin}/NamingContainers?NC=${properties.NameSpaceNytNamingContainers}`, { method: "GET" })
                    .done((data, textStatus, jqXhr) => {
                    let _ncList = data;
                    let _editShortCuts;
                    // Dictionary mit den Short Cuts aufbauen
                    for (var i = 0, _ncListCount = _ncList.length; i < _ncListCount; i++) {
                        var nc = _ncList[i];
                        _editShortCuts[nc.EditShortCut] = nc;
                    }
                    if (properties.DocumentName !== "") {
                        // Laden des Beispieldokumentes
                        jquery_1.default.ajax(`${properties.ServerOrigin}/fileStore?fileName=${properties.DocumentName}`, { method: "GET" })
                            .done((data, textStatus, jqXhr) => {
                            let docContentAsString = data;
                            // 
                            (0, IDocument_1.CreateDocument)(properties.UserId, properties.DocumentName, docContentAsString, 
                            // Siegel
                            (doc) => {
                                setState({
                                    init: false,
                                    nytKeywords: _ncList,
                                    editShortCuts: _editShortCuts,
                                    document: doc,
                                    cursor: { currentColNo: 0, currentLineNo: 0 },
                                    visibleLines: CountVisibleLines,
                                    statusText: `Resources and document ${properties.DocumentName} loaded successful from Server`
                                });
                                return "";
                            }, 
                            // Sowilo
                            (txt, fName, errClass, ...args) => {
                                setState({
                                    init: false,
                                    nytKeywords: _ncList,
                                    editShortCuts: _editShortCuts,
                                    document: state.document,
                                    cursor: state.cursor,
                                    visibleLines: CountVisibleLines,
                                    statusText: `Resources loaded successful from Server, but not the Document. ${fName} failed, ErrClass: ${errClass}, ${args.join(", ")}`
                                });
                                return "";
                            });
                        })
                            .fail((jqXHR, textStatus, errorThrown) => {
                        });
                    }
                    else {
                        // Zustand der React- Komponente neu setzten und rendern
                        setState({
                            init: false,
                            nytKeywords: _ncList,
                            editShortCuts: _editShortCuts,
                            document: state.document,
                            cursor: state.cursor,
                            visibleLines: CountVisibleLines,
                            statusText: "Resources loaded successful from Server"
                        });
                    }
                })
                    .fail((jqXHR, textStatus, errorThrown) => {
                    let errTxt = `HTTP Status:${textStatus}, ${errorThrown}`;
                    // Zustand der React- Komponente neu setzten und rendern
                    setState({
                        init: false,
                        nytKeywords: state.nytKeywords,
                        editShortCuts: state.editShortCuts,
                        document: state.document,
                        cursor: state.cursor,
                        visibleLines: CountVisibleLines,
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
                    react_1.default.createElement("button", { id: "help", className: "btn btn-normal" }, "\uD83D\uDD6E Help"))),
            "\u2328",
            react_1.default.createElement("div", { id: "visibleLines" }),
            react_1.default.createElement("footer", null,
                react_1.default.createElement("div", { id: "statusLine" }))));
    }
    exports.default = CrossWriter;
});
//# sourceMappingURL=CrossWriter.js.map