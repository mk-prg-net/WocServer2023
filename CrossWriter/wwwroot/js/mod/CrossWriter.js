// mko, 28.12.2023
// Main react Component of CrossWriter
var __importDefault = (this && this.__importDefault) || function (mod) {
    return (mod && mod.__esModule) ? mod : { "default": mod };
};
define(["require", "exports", "jquery", "react", "react-dom", "./CrossWriterEditLine", "./CrossWriterEmptyLine", "./CrossWriterLine", "./IDocument"], function (require, exports, jquery_1, react_1, react_dom_1, CrossWriterEditLine_1, CrossWriterEmptyLine_1, CrossWriterLine_1, IDocument_1) {
    "use strict";
    Object.defineProperty(exports, "__esModule", { value: true });
    jquery_1 = __importDefault(jquery_1);
    react_1 = __importDefault(react_1);
    react_dom_1 = __importDefault(react_dom_1);
    function CreateKeyGenerator() {
        let key = Math.floor(Math.random() * 1000000);
        return () => key++;
    }
    // This must be an uneven Number (count pre- Lines, edit- Line, count post- Lines)
    const CountViewLines = 31;
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
            visibleLines: CountViewLines,
            statusText: "start",
            keyGen: CreateKeyGenerator()
        });
        let invisibleInputFildForEdit = react_1.default.useRef(null);
        function LoadResourcesFromServer() {
            if (state.init) {
                let keyGenerator = CreateKeyGenerator();
                jquery_1.default.ajax(`${properties.ServerOrigin}/NamingContainers?NC=${properties.NameSpaceNytNamingContainers}`, { method: "GET" })
                    .done((data, textStatus, jqXhr) => {
                    let _ncList = data;
                    let _editShortCuts = {};
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
                                    cursor: { currentColNo: doc.textLines[0].length, currentLineNo: 0 },
                                    visibleLines: CountViewLines,
                                    statusText: `Resources and document ${properties.DocumentName} loaded successful from Server`,
                                    keyGen: keyGenerator
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
                                    visibleLines: CountViewLines,
                                    statusText: `Resources loaded successful from Server, but not the Document. ${fName} failed, ErrClass: ${errClass}, ${args.join(", ")}`,
                                    keyGen: keyGenerator
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
                            visibleLines: CountViewLines,
                            statusText: "Resources loaded successful from Server",
                            keyGen: keyGenerator
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
                        visibleLines: CountViewLines,
                        statusText: errTxt,
                        keyGen: keyGenerator
                    });
                });
            }
            if (invisibleInputFildForEdit !== null && invisibleInputFildForEdit !== undefined) {
                invisibleInputFildForEdit.current.focus();
                invisibleInputFildForEdit.current.value = "";
            }
        }
        react_1.default.useEffect(() => LoadResourcesFromServer(), []);
        // Berechnet die Anzahl der sichtbaren Zeilen vor und nach der Editor- Zeile
        function CountPrePostLines() { return (CountViewLines - 1) / 2; }
        ;
        // mko, 2.1.2024
        // Erzeugt Visuelle ausgabe der Edit- Zeile und der unmittelbar vor und nach der Edit- Zeile befindlichen
        // Zeilen des Dokumentes
        function ViewLines() {
            let vLines = [];
            let lineCount = state.document.LineCount;
            let currentCursorLine = state.cursor.currentLineNo;
            // Fälle: Positionierung des Fensters [] mit sichbaren Edit- Zeilen. * ist die Eingabezeile
            // [00E00]      LineCount == 0 currentLineNo == 0
            // [00*00]      LineCount == 1 currentLineNo == 0 LineCount <= prePostCount
            // [00*+0]      LineCount == 2 currentLineNo == 0 LineCount <= prePostCount
            // [0+*00]      LineCount == 2 currentLineNo == 1 LineCount <= prePostCount
            // [00*++]      LineCount == 3 currentLineNo == 0 LineCount <= prePostCount
            // [0+*+0]      LineCount == 3 currentLineNo == 1 LineCount <= prePostCount
            // [++*00]      LineCount == 3 currentLineNo == 2 LineCount <= prePostCount
            // [00*++]++++  LineCount == 7 currentLineNo == 0 LineCount > prePostCount
            // [0+*++]+++   LineCount == 7 currentLineNo == 1 LineCount > prePostCount
            // [++*++]++    LineCount == 7 currentLineNo == 2 LineCount > prePostCount
            // +[++*++]+    LineCount == 7 currentLineNo == 3 LineCount > prePostCount
            // ++[++*++]    LineCount == 7 currentLineNo == 4 LineCount > prePostCount
            // +++[++*+0]   LineCount == 7 currentLineNo == 5 LineCount > prePostCount
            // ++++[++*00]  LineCount == 7 currentLineNo == 6 LineCount > prePostCount
            let prePostLines = CountPrePostLines();
            if (lineCount() === 0) {
                // Fall: [00E00] leeres Dokument            
                // Leerzeilen vor der Editor- zeile aufbauen
                for (var i = 0; i < prePostLines; i++) {
                    vLines.push(react_1.default.createElement(CrossWriterEmptyLine_1.CrossWriterEmptyLine, { key: state.keyGen(), cssClassLineNo: "col cw-3 lineNo", cssClassLine: "col cw-56 lineContent", cssClassLineFunction: "col cw-6 lineFunc" }));
                }
                vLines.push(react_1.default.createElement(CrossWriterEditLine_1.CrossWriterEditLine, { key: state.keyGen(), document: state.document, lineNo: currentCursorLine, cssClassLineNo: "col cw-3 lineNo", cssClassLine: "col cw-56 EditLine", cssClassLineFunction: "col cw-6 lineFunc", 
                    //ProcessKeyDownEventForVisibleLines={ProcessKeyDownEventForEditLine}
                    nytKeywords: state.nytKeywords }));
                // Leerzeilen nach der Editor- zeile aufbauen
                for (var i = 0; i < prePostLines; i++) {
                    vLines.push(react_1.default.createElement(CrossWriterEmptyLine_1.CrossWriterEmptyLine, { key: state.keyGen(), cssClassLineNo: "col cw-3 lineNo", cssClassLine: "col cw-56 lineContent", cssClassLineFunction: "col cw-6 lineFunc" }));
                }
            }
            else {
                AddPreLines(vLines, currentCursorLine);
                vLines.push(react_1.default.createElement(CrossWriterEditLine_1.CrossWriterEditLine, { key: state.keyGen(), document: state.document, lineNo: currentCursorLine, cssClassLineNo: "col cw-3 lineNo", cssClassLine: "col cw-56 EditLine", cssClassLineFunction: "col cw-6 lineFunc", 
                    //ProcessKeyDownEventForVisibleLines={ProcessKeyDownEventForEditLine}
                    nytKeywords: state.nytKeywords }));
                AddPostLines(vLines, currentCursorLine, state.document.LineCount());
            }
            return vLines;
        }
        // KeyCodes siehe https://www.freecodecamp.org/news/javascript-keycode-list-keypress-event-key-codes/
        function ProcessKeyDownEventForEditLine(key, ctrlKey) {
            if (key == "Enter") {
                // Ctrl+Enter ⏎: Neuen Text übernehmen
            }
            else if (key == "ArrowUp") {
                // Ctrl+Arrow Up ↑: Vorausgehenden Textabschnitt bearbeiten
                if (state.document.LineCount() !== 0 && state.cursor.currentLineNo > 0) {
                    setState({
                        cursor: {
                            currentColNo: state.cursor.currentColNo,
                            currentLineNo: state.cursor.currentLineNo - 1
                        },
                        document: state.document,
                        editShortCuts: state.editShortCuts,
                        init: state.init,
                        nytKeywords: state.nytKeywords,
                        statusText: state.statusText,
                        visibleLines: CountViewLines,
                        keyGen: state.keyGen
                    });
                }
            }
            else if (key == "ArrowDown") {
                // Ctrl+Arrow Down ↓: Vorausgehenden Textabschnitt bearbeiten
                if (state.document.LineCount() !== 0 && state.cursor.currentLineNo < state.document.LineCount() - 1) {
                    setState({
                        cursor: {
                            currentColNo: state.cursor.currentColNo,
                            currentLineNo: state.cursor.currentLineNo + 1
                        },
                        document: state.document,
                        editShortCuts: state.editShortCuts,
                        init: state.init,
                        nytKeywords: state.nytKeywords,
                        statusText: state.statusText,
                        visibleLines: CountViewLines,
                        keyGen: state.keyGen
                    });
                }
            }
            else if (key == "ArrowLeft") {
                // Arrow Left ←: Cursor nach links
                if (state.document.textLines[state.cursor.currentLineNo].length > 0
                    && state.cursor.currentColNo > 0) {
                    setState({
                        cursor: {
                            currentColNo: state.cursor.currentColNo - 1,
                            currentLineNo: state.cursor.currentLineNo
                        },
                        document: state.document,
                        editShortCuts: state.editShortCuts,
                        init: state.init,
                        nytKeywords: state.nytKeywords,
                        statusText: state.statusText,
                        visibleLines: CountViewLines,
                        keyGen: state.keyGen
                    });
                }
            }
            else if (key == "ArrowRight") {
                // Arrow Right →: Cursor nach rechts
                if (state.document.textLines[state.cursor.currentLineNo].length > 0
                    && state.document.textLines[state.cursor.currentLineNo].length > state.cursor.currentColNo) {
                    setState({
                        cursor: {
                            currentColNo: state.cursor.currentColNo + 1,
                            currentLineNo: state.cursor.currentLineNo
                        },
                        document: state.document,
                        editShortCuts: state.editShortCuts,
                        init: state.init,
                        nytKeywords: state.nytKeywords,
                        statusText: state.statusText,
                        visibleLines: CountViewLines,
                        keyGen: state.keyGen
                    });
                }
            }
            else if (key == "Backspace") {
                // Zeichen links vom Cursor löschen
                let currentCursor = state.cursor.currentColNo;
                let currentLine = state.document.textLines[state.cursor.currentLineNo];
                if (currentLine.length > 0) {
                    let left = currentLine.substring(0, currentCursor - 1);
                    let right = currentLine.substring(currentCursor);
                    currentCursor--;
                    currentLine = `${left}${right}`;
                }
                // Die aktuelle Zeile wird mit der modifizierten überschrieben
                state.document.textLines[state.cursor.currentLineNo] = currentLine;
                setState({
                    cursor: {
                        currentColNo: currentCursor,
                        currentLineNo: state.cursor.currentLineNo
                    },
                    document: state.document,
                    editShortCuts: state.editShortCuts,
                    init: state.init,
                    nytKeywords: state.nytKeywords,
                    statusText: state.statusText,
                    visibleLines: CountViewLines,
                    keyGen: state.keyGen
                });
            }
            else if (key == "Delete") {
                // Zeichen rechts vom Cursor löschen
                let currentCursor = state.cursor.currentColNo;
                let currentLine = state.document.textLines[state.cursor.currentLineNo];
                if (currentLine.length > 0 && currentCursor < currentLine.length) {
                    let left = currentLine.substring(0, currentCursor);
                    let right = currentLine.substring(currentCursor + 1);
                    currentCursor--;
                    currentLine = `${left}${right}`;
                }
                // Die aktuelle Zeile wird mit der modifizierten überschrieben
                state.document.textLines[state.cursor.currentLineNo] = currentLine;
                setState({
                    cursor: {
                        currentColNo: currentCursor,
                        currentLineNo: state.cursor.currentLineNo
                    },
                    document: state.document,
                    editShortCuts: state.editShortCuts,
                    init: state.init,
                    nytKeywords: state.nytKeywords,
                    statusText: state.statusText,
                    visibleLines: CountViewLines,
                    keyGen: state.keyGen
                });
            }
            else if (key == "Shift") {
                // ignorieren
            }
            else {
                // Das Zeichen wird an der Cursorposition eingefügt
                let currentCursor = state.cursor.currentColNo;
                let currentLine = state.document.textLines[state.cursor.currentLineNo];
                if (currentLine.length == 0) {
                    currentLine = key;
                }
                else if (currentLine.length - 1 == currentCursor) {
                    currentLine += key;
                }
                else {
                    let left = currentLine.substring(0, currentCursor);
                    let right = currentLine.substring(currentCursor);
                    currentLine = `${left}${key}${right}`;
                }
                // Die aktuelle Zeile wird mit der modifizierten überschrieben
                state.document.textLines[state.cursor.currentLineNo] = currentLine;
                setState({
                    cursor: {
                        currentColNo: state.cursor.currentColNo + 1,
                        currentLineNo: state.cursor.currentLineNo
                    },
                    document: state.document,
                    editShortCuts: state.editShortCuts,
                    init: state.init,
                    nytKeywords: state.nytKeywords,
                    statusText: state.statusText,
                    visibleLines: CountViewLines,
                    keyGen: state.keyGen
                });
            }
        }
        function AddPreLines(vLines, currentCursorLine) {
            let prePostLines = CountPrePostLines();
            if (prePostLines - currentCursorLine > 0) {
                // Leerraumzeilen am Anfang einfügen, falls Dokumentzeilen sichtbare Fläche nicht vollständig
                // ausfüllen.
                for (var i = 0, countEmptyLines = prePostLines - currentCursorLine; i < countEmptyLines; i++) {
                    vLines.push(react_1.default.createElement(CrossWriterEmptyLine_1.CrossWriterEmptyLine, { key: state.keyGen(), cssClassLineNo: "col cw-3 lineNo", cssClassLine: "col cw-56 lineContent", cssClassLineFunction: "col cw-6 lineFunc" }));
                }
                // Der Editorzeile vorauseilende Zeilen des Dokumentes ausgeben
                for (var i = 0; i < currentCursorLine; i++, j++) {
                    vLines.push(react_1.default.createElement(CrossWriterLine_1.CrossWriterLine, { key: state.keyGen(), document: state.document, lineNo: i, cssClassLineNo: "col cw-3 lineNo", cssClassLine: "col cw-56 lineContent", cssClassLineFunction: "col cw-6 lineFunc", nytKeywords: state.nytKeywords }));
                }
            }
            else {
                // Der Editorzeile vorauseilende Zeilen des Dokumentes ausgeben
                for (var i = 0, j = currentCursorLine - 1 - prePostLines; i < prePostLines; i++, j++) {
                    vLines.push(react_1.default.createElement(CrossWriterLine_1.CrossWriterLine, { key: state.keyGen(), document: state.document, lineNo: j, cssClassLineNo: "col cw-3 lineNo", cssClassLine: "col cw-56 lineContent", cssClassLineFunction: "col cw-6 lineFunc", nytKeywords: state.nytKeywords }));
                }
            }
        }
        function AddPostLines(vLines, currentCursorLine, LineCount) {
            let prePostLines = CountPrePostLines();
            if (LineCount - currentCursorLine < prePostLines) {
                for (var i = currentCursorLine + 1; i < LineCount; i++) {
                    vLines.push(react_1.default.createElement(CrossWriterLine_1.CrossWriterLine, { key: state.keyGen(), document: state.document, lineNo: i, cssClassLineNo: "col cw-3 lineNo", cssClassLine: "col cw-56 lineContent", cssClassLineFunction: "col cw-6 lineFunc", nytKeywords: state.nytKeywords }));
                }
                // Rest mit Leerzeilen auffüllen
                for (var i = 0, countEmptyLines = prePostLines - (LineCount - currentCursorLine); i < countEmptyLines; i++) {
                    vLines.push(react_1.default.createElement(CrossWriterEmptyLine_1.CrossWriterEmptyLine, { key: state.keyGen(), cssClassLineNo: "col cw-3 lineNo", cssClassLine: "col cw-56 lineContent", cssClassLineFunction: "col cw-6 lineFunc" }));
                }
            }
            else {
                // Alle sichtbaren Zeilen nach der Edit- Zeile mit Zeilen aus dem Dokument füllen
                for (var i = 0, j = currentCursorLine + 1; i < prePostLines; i++, j++) {
                    vLines.push(react_1.default.createElement(CrossWriterLine_1.CrossWriterLine, { key: state.keyGen(), document: state.document, lineNo: j, cssClassLineNo: "col cw-3 lineNo", cssClassLine: "col cw-56 lineContent", cssClassLineFunction: "col cw-6 lineFunc", nytKeywords: state.nytKeywords }));
                }
            }
        }
        return (react_1.default.createElement("div", { id: "CrossWriterCtrl", className: "CrossWriter" },
            react_1.default.createElement("header", null,
                react_1.default.createElement("nav", { id: "main_nav" },
                    react_1.default.createElement("button", { id: "btnNewFile", className: "btn btn-normal" }, "\uD83D\uDDCB New"),
                    react_1.default.createElement("button", { id: "btnOpenFile", className: "btn btn-normal" }, "\uD83D\uDDBA Open"),
                    react_1.default.createElement("button", { id: "btnSave", className: "btn btn-normal" }, "\uD83D\uDDAB Save"),
                    react_1.default.createElement("button", { id: "help", className: "btn btn-normal" }, "\uD83D\uDD6E Help"))),
            react_1.default.createElement("input", { ref: invisibleInputFildForEdit, onKeyDown: e => ProcessKeyDownEventForEditLine(e.key, e.ctrlKey) }),
            react_1.default.createElement("div", { id: "visibleLines", className: "VisibleLines" },
                ViewLines(),
                "input"),
            react_1.default.createElement("footer", { className: "row" },
                react_1.default.createElement("div", { id: "statusLine", className: "col col-10" },
                    "Line: ",
                    state.cursor.currentLineNo,
                    " Col: ",
                    state.cursor.currentColNo,
                    " #Lines: ",
                    state.document.LineCount(),
                    " "))));
    }
    function CrossWriterSetUp(idRoot, ServerOrigin, documentName) {
        react_dom_1.default.render(react_1.default.createElement(react_1.default.StrictMode, null,
            react_1.default.createElement(CrossWriter, { ServerOrigin: ServerOrigin, DocumentName: documentName, NameSpaceNytNamingContainers: "MKPRG.Naming.NYT.Keywords", UserId: "mko" })), (0, jquery_1.default)(`#${idRoot}`)[0]);
    }
    exports.default = CrossWriterSetUp;
});
//# sourceMappingURL=CrossWriter.js.map