// mko, 28.12.2023
// Main react Component of CrossWriter
var __importDefault = (this && this.__importDefault) || function (mod) {
    return (mod && mod.__esModule) ? mod : { "default": mod };
};
define(["require", "exports", "react", "jquery", "./IDocument", "./CrossWriterLine"], function (require, exports, react_1, jquery_1, IDocument_1, CrossWriterLine_1) {
    "use strict";
    Object.defineProperty(exports, "__esModule", { value: true });
    react_1 = __importDefault(react_1);
    jquery_1 = __importDefault(jquery_1);
    // This must be an uneven Number (count pre- Lines, edit- Line, count post- Lines)
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
        function VisibleLines() {
            let vLines = [react_1.default.createElement("div", null)];
            let lineCount = state.document.LineCount;
            let currentCursorLine = state.cursor.currentLineNo;
            // Fälle: Positionierung des Fensters [] mit sichbaren Edit- Zeilen. * ist die Eingabezeile
            // [*++]++++++++++
            // [+*++]+++++++++
            // [++*++]++++++++
            // +++[++*++]+++++
            // ++++++++[++*++]
            // +++++++++[++*+]
            // ++++++++++[++*]
            // [*++]
            // [+*+]
            // [++*]
            let prePostLines = (CountVisibleLines - 1) / 2;
            if (currentCursorLine < prePostLines && (lineCount() - prePostLines)) {
                // Fälle
                // [*++]
                // [+*+]
                // [++*]
                AddPreLines(vLines, 0, currentCursorLine);
                vLines.push(react_1.default.createElement(CrossWriterLine_1.CrossWriterLine, { document: state.document, lineNo: currentCursorLine, cssClassLineNo: "col-1 lineNo", cssClassLine: "col-10 EditLine", cssClassLineFunction: "col-1 lineFunc", nytKeywords: state.nytKeywords }));
                AddPostLines(vLines, currentCursorLine, state.document.LineCount());
            }
            else if (currentCursorLine < prePostLines) {
                // Fälle
                // [*++]++++++++++
                // [+*++]+++++++++
                // [++*++]++++++++
                AddPreLines(vLines, 0, currentCursorLine);
                vLines.push(react_1.default.createElement(CrossWriterLine_1.CrossWriterLine, { document: state.document, lineNo: currentCursorLine, cssClassLineNo: "col-1 lineNo", cssClassLine: "col-10 EditLine", cssClassLineFunction: "col-1 lineFunc", nytKeywords: state.nytKeywords }));
                AddPostLines(vLines, currentCursorLine, currentCursorLine + prePostLines + 1);
            }
            else if (currentCursorLine > (lineCount() - prePostLines)) {
                // Fall +++[++*++]+++++
                AddPreLines(vLines, currentCursorLine - prePostLines - 1, currentCursorLine);
                vLines.push(react_1.default.createElement(CrossWriterLine_1.CrossWriterLine, { document: state.document, lineNo: currentCursorLine, cssClassLineNo: "col-1 lineNo", cssClassLine: "col-10 EditLine", cssClassLineFunction: "col-1 lineFunc", nytKeywords: state.nytKeywords }));
                AddPostLines(vLines, currentCursorLine, state.document.LineCount());
            }
            else {
                // ++++++++[++*++]
                // +++++++++[++*+]
                // ++++++++++[++*]
                AddPreLines(vLines, currentCursorLine - prePostLines - 1, currentCursorLine);
                vLines.push(react_1.default.createElement(CrossWriterLine_1.CrossWriterLine, { document: state.document, lineNo: currentCursorLine, cssClassLineNo: "col-1 lineNo", cssClassLine: "col-10 EditLine", cssClassLineFunction: "col-1 lineFunc", nytKeywords: state.nytKeywords }));
                AddPostLines(vLines, currentCursorLine, currentCursorLine + prePostLines + 1);
            }
            return vLines;
        }
        function AddPreLines(vLines, start, currentCursorLine) {
            for (var i = start; i < currentCursorLine; i++) {
                vLines.push(react_1.default.createElement(CrossWriterLine_1.CrossWriterLine, { document: state.document, lineNo: i, cssClassLineNo: "col-1 lineNo", cssClassLine: "col-10 lineContent", cssClassLineFunction: "col-1 lineFunc", nytKeywords: state.nytKeywords }));
            }
        }
        function AddPostLines(vLines, currentCursorLine, endLineNo) {
            for (var i = currentCursorLine + 1, end = endLineNo; i < end; i++) {
                vLines.push(react_1.default.createElement(CrossWriterLine_1.CrossWriterLine, { document: state.document, lineNo: i, cssClassLineNo: "col-1 lineNo", cssClassLine: "col-10 lineContent", cssClassLineFunction: "col-1 lineFunc", nytKeywords: state.nytKeywords }));
            }
        }
        return (react_1.default.createElement("div", { id: "CrossWriter", className: properties.cssClass },
            react_1.default.createElement("header", null,
                react_1.default.createElement("nav", { id: "main_nav" },
                    react_1.default.createElement("button", { id: "btnNewFile", className: "btn btn-normal" }, "\uD83D\uDDCB New"),
                    react_1.default.createElement("button", { id: "btnOpenFile", className: "btn btn-normal" }, "\uD83D\uDDBA Open"),
                    react_1.default.createElement("button", { id: "btnSave", className: "btn btn-normal" }, "\uD83D\uDDAB Save"),
                    react_1.default.createElement("button", { id: "help", className: "btn btn-normal" }, "\uD83D\uDD6E Help"))),
            "\u2328",
            react_1.default.createElement("div", { id: "visibleLines" }, VisibleLines()),
            react_1.default.createElement("footer", null,
                react_1.default.createElement("div", { id: "statusLine" },
                    "Line: ",
                    state.cursor.currentLineNo,
                    " Col: ",
                    state.cursor.currentColNo,
                    " #Lines: ",
                    state.document.LineCount(),
                    " "))));
    }
    exports.default = CrossWriter;
});
//# sourceMappingURL=CrossWriter.js.map