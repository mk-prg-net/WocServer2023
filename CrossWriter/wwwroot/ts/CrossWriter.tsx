// mko, 28.12.2023
// Main react Component of CrossWriter

import $ from "jquery";
import React from "react";
import ReactDom from "react-dom";

import { ErrorClasses, SiegelSuccessFunc, SowiloErrFunc } from "./SiegelAndSowilo";
import NamingIds from "./NamingIds";
import { INamingContainer, getNameFromNc } from "./INamingContainer";

import { CrossWriterEditLine } from "./CrossWriterEditLine";
import { CrossWriterEmptyLine } from "./CrossWriterEmptyLine";
import { CrossWriterLine } from "./CrossWriterLine";
import { CreateDocument, IDocument, IDocumentCursor } from "./Document";

interface ICrossWriterProps {
    ServerOrigin: string,
    UserId: string,
    CursorSymbol: string,
    DocumentName: string
}


interface ICrossWriterState {
    init: boolean,

    // List of all NYT Keywords. Must be loaded from Server
    nytKeywords: Record<string, INamingContainer>,

    // Mapping Key Board Shortcuts to Nyt Naming- Container.
    editShortCuts: Record<string, INamingContainer>,

    // the document, that will be edited by this control
    document: IDocument,

    // the currently edited position
    cursor: IDocumentCursor,

    // true, if alt key was pressed. Influences next key stroke processing
    altKey: boolean,

    // true, if # key was pressed. Influences next key stroke processing
    rauteKey: boolean,

    // true, if ctrl key was pressed. Influences next key stroke processing
    ctrlKey: boolean,

    // Count of visible Lines in Edit- Window
    visibleLines,

    // A short text, describing current status of edit control
    statusText: string

    // Generates unique Keys
    keyGen: () => number,

    // Indicates a new edit Op
    countEditOp: number
}

// Key Generator for React Key Property
function CreateKeyGenerator(): () => number {
    let key = Math.floor(Math.random() * 1000000);

    return () => key++;
}

// This must be an uneven Number (count pre- Lines, edit- Line, count post- Lines)
const CountViewLines = 31;

// Default- Namingcontainer
var UnkownNC: INamingContainer = {
    CNT: "unknown",
    DE: "unbekannt",
    EditShortCut: "unknown",
    EN: "unknown",
    Glyph: " ",
    GlyphUniCode: " ",
    NIDstr: "unknown"
};

function CrossWriter(properties: ICrossWriterProps) {
    // Define initial State
    const [state, setState] = React.useState<ICrossWriterState>({
        init: true,
        nytKeywords: { "none": UnkownNC },
        editShortCuts: { "none": UnkownNC },
        document: {
            autorUserId: properties.UserId,
            documentName: properties.DocumentName,
            textLines: [""],
            LineCount: () => 0
        },
        cursor: { currentLineNo: 0, currentColNo: 0, cursorSymbol: properties.CursorSymbol },
        visibleLines: CountViewLines,
        statusText: "start",
        keyGen: CreateKeyGenerator(),
        altKey: false,
        rauteKey: false,
        ctrlKey: false,
        countEditOp: 0
    });

    const Nids = React.useMemo(() => NamingIds(), []);
    let AppName = "???";

    let invisibleInputFildForEdit = React.useRef<HTMLInputElement>(null);

    function LoadResourcesFromServer() {
        if (state.init) {
            let keyGenerator = CreateKeyGenerator();

            $.ajax(`${properties.ServerOrigin}/NamingContainers?NC=MKPRG.Naming.NYT.Keywords`, { method: "GET" })
                .done((data, textStatus, jqXhr) => {
                    let _ncList = data as Array<INamingContainer>;
                    let _nc: Record<string, INamingContainer> = {};

                    for (var i = 0, _ncListCount = _ncList.length; i < _ncListCount; i++) {
                        var nc = _ncList[i];
                        _nc[nc.NIDstr] = nc;
                    }


                    let _editShortCuts: Record<string, INamingContainer> = {};

                    // Dictionary mit den Short Cuts aufbauen
                    for (var i = 0, _ncListCount = _ncList.length; i < _ncListCount; i++) {
                        var nc = _ncList[i];
                        _editShortCuts[nc.EditShortCut] = nc;
                    }

                    if (properties.DocumentName !== "") {

                        // Laden des Beispieldokumentes
                        $.ajax(`${properties.ServerOrigin}/fileStore?fileName=${properties.DocumentName}`, { method: "GET" })
                            .done((data, textStatus, jqXhr) => {

                                let docContentAsString = data as string;

                                // 
                                CreateDocument(properties.UserId, properties.DocumentName,
                                    docContentAsString,
                                    // Siegel
                                    (doc) => {

                                        setState({
                                            init: false,
                                            nytKeywords: _nc,
                                            editShortCuts: _editShortCuts,
                                            document: doc,
                                            cursor: { currentColNo: doc.textLines[0].length, currentLineNo: 0, cursorSymbol: properties.CursorSymbol },
                                            visibleLines: CountViewLines,
                                            statusText: `Resources and document ${properties.DocumentName} loaded successful from Server`,
                                            keyGen: keyGenerator,
                                            altKey: false,
                                            rauteKey: false,
                                            ctrlKey: false,
                                            countEditOp: 1
                                        });
                                        return "";
                                    },
                                    // Sowilo
                                    (txt, fName, errClass, ...args) => {
                                        setState({
                                            init: false,
                                            nytKeywords: _nc,
                                            editShortCuts: _editShortCuts,
                                            document: state.document,
                                            cursor: state.cursor,
                                            visibleLines: CountViewLines,
                                            statusText: `Resources loaded successful from Server, but not the Document. ${fName} failed, ErrClass: ${errClass}, ${args.join(", ")}`,
                                            keyGen: keyGenerator,
                                            altKey: false,
                                            rauteKey: false,
                                            ctrlKey: false,
                                            countEditOp: 1
                                        });
                                        return "";
                                    }
                                );
                            })
                            .fail((jqXHR, textStatus, errorThrown) => {
                            });
                    } else {
                        // Zustand der React- Komponente neu setzten und rendern
                        setState({
                            init: false,
                            nytKeywords: _nc,
                            editShortCuts: _editShortCuts,
                            document: state.document,
                            cursor: state.cursor,
                            visibleLines: CountViewLines,
                            statusText: "Resources loaded successful from Server",
                            keyGen: keyGenerator,
                            altKey: false,
                            rauteKey: false,
                            ctrlKey: false,
                            countEditOp: 0
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
                        keyGen: keyGenerator,
                        altKey: false,
                        rauteKey: false,
                        ctrlKey: false,
                        countEditOp: 0
                    });
                });
        }
    }

    React.useEffect(() => LoadResourcesFromServer(), []);

    function SetFocusOnInputField(): any {
        if (invisibleInputFildForEdit !== null && invisibleInputFildForEdit !== undefined) {
            invisibleInputFildForEdit.current.focus();
            invisibleInputFildForEdit.current.value = "";
        }
    }

    function SetFocusOnInputField2(): any {
        if (invisibleInputFildForEdit !== null && invisibleInputFildForEdit !== undefined) {
            invisibleInputFildForEdit.current.focus();
            invisibleInputFildForEdit.current.value = "a";
        }
    }


    let countEditOp = 0;
    React.useEffect(SetFocusOnInputField, []);

    // Berechnet die Anzahl der sichtbaren Zeilen vor und nach der Editor- Zeile
    function CountPrePostLines() { return (CountViewLines - 1) / 2 };

    const fKeys = ["F1", "F2", "F3", "F4", "F5", "F6", "F7", "F8", "F9", "F10", "F11", "F12"];

    // mko, 2.1.2024
    // Erzeugt Visuelle ausgabe der Edit- Zeile und der unmittelbar vor und nach der Edit- Zeile befindlichen
    // Zeilen des Dokumentes
    function ViewLines(): any[] {

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
                vLines.push(<CrossWriterEmptyLine
                    key={state.keyGen()}
                    cssClassLineNo="col cw-3 lineNo"
                    cssClassLine="col cw-56 lineContent"
                    cssClassLineFunction="col cw-6 lineFunc"></CrossWriterEmptyLine>);
            }

            vLines.push(<CrossWriterEditLine
                key={state.keyGen()}
                document={state.document}
                cursor={state.cursor}
                cssClassCursor="Cursor"
                cssClassLineNo="col cw-3 lineNo"
                cssClassLine="col cw-56 EditLine"
                cssClassLineFunction="col cw-6 lineFunc"
                //ProcessKeyDownEventForVisibleLines={ProcessKeyDownEventForEditLine}
                nytKeywords={state.nytKeywords}
                SetFocusOnInputField={SetFocusOnInputField}
                countEditOps={state.countEditOp}>
            </CrossWriterEditLine>,
            );

            // Leerzeilen nach der Editor- zeile aufbauen
            for (var i = 0; i < prePostLines; i++) {
                vLines.push(<CrossWriterEmptyLine
                    key={state.keyGen()}
                    cssClassLineNo="col cw-3 lineNo"
                    cssClassLine="col cw-56 lineContent"
                    cssClassLineFunction="col cw-6 lineFunc"></CrossWriterEmptyLine>);
            }
        }
        else {
            AddPreLines(vLines, currentCursorLine);

            vLines.push(<CrossWriterEditLine
                key={state.keyGen()}
                document={state.document}
                cursor={state.cursor}
                cssClassCursor="Cursor"
                cssClassLineNo="col cw-3 lineNo"
                cssClassLine="col cw-56 EditLine"
                cssClassLineFunction="col cw-6 lineFunc"
                //ProcessKeyDownEventForVisibleLines={ProcessKeyDownEventForEditLine}
                nytKeywords={state.nytKeywords}
                SetFocusOnInputField={SetFocusOnInputField}
                countEditOps={state.countEditOp}>
            </CrossWriterEditLine>);

            AddPostLines(vLines, currentCursorLine, state.document.LineCount());
        }

        return vLines;
    }

    // KeyCodes siehe https://www.freecodecamp.org/news/javascript-keycode-list-keypress-event-key-codes/
    function ProcessKeyDownEventForEditLine(key: string, ctrlKey: boolean, altKey: boolean, shiftKey: boolean) {
        countEditOp = state.countEditOp + 1;

        let test = fKeys.find((val) => val === key);
        if (key == "#") {
            SetRauteKeyInState(true);
        }
        else if (state.rauteKey) {
            let runeShortCut = `#${key}`;
            if (Object.keys(state.editShortCuts).find(sc => sc === runeShortCut) != undefined) {
                let glyph = state.editShortCuts[runeShortCut].GlyphUniCode;
                InsertCharInText(glyph);
            } else {
                SetRauteKeyInState(false);
            }
        }
        else if (key == "Control") {
            SetCtrlKeyInState(true);
        }
        else if (state.ctrlKey) {
            if (key == "Delete") {
                // Delete current Line

                // Fälle                
                if (state.document.LineCount() == 0) {
                    // LineCount == 0                    
                }
                else if (state.document.LineCount() == 1) {
                    // LineCount == 1
                    state.document.textLines[0] = "";
                } else {
                    // LineCount > 1 
                    state.document.textLines.splice(state.cursor.currentLineNo, 1);
                }
                SetCtrlKeyInState(false);
            } else if (key == "Enter") {
                // Insert a new line
                // Fälle                
                if (state.document.LineCount() == 0) {
                    state.document.textLines.push("");
                } else {
                    // LineCount > 1
                    // Zwei Fälle: steht der Cursor am Zeilenanfang, dann vor der aktuellen Zeile einfügen.
                    // Sonst nach der aktuellen Zeile
                    if (state.cursor.currentColNo == 0) {                        
                        state.document.textLines.splice(state.cursor.currentLineNo, 0, "");                        
                    }
                    else {
                        state.cursor.currentLineNo++;
                        state.document.textLines.splice(state.cursor.currentLineNo, 0, "");                        
                    }                    
                }
                SetCtrlKeyInState(false);
            
            } else {
                SetCtrlKeyInState(false);
            }
        }
        else if (key == "Enter") {
            // Ctrl+Enter ⏎: Neuen Text übernehmen

            if (state.document.LineCount() !== 0 && state.cursor.currentLineNo < state.document.LineCount() - 1) {
                let newCursorColPos = state.document.textLines[state.cursor.currentLineNo + 1].length;
                SetNewCursorPos(state.cursor.currentLineNo + 1, newCursorColPos);
            } else if (state.document.LineCount() == 0) {
                // erste Zeile 
                state.document.textLines.push("");
            } else {
                state.document.textLines.push("");
                SetNewCursorPos(state.cursor.currentLineNo + 1, 0);
            }
        }
        else if (key == "ArrowUp") {
            // Ctrl+Arrow Up ↑: Vorausgehenden Textabschnitt bearbeiten

            if (state.document.LineCount() !== 0 && state.cursor.currentLineNo > 0) {

                let newCursorPos = state.document.textLines[state.cursor.currentLineNo - 1].length;
                SetNewCursorPos(state.cursor.currentLineNo - 1, newCursorPos);
            }
        }
        else if (key == "ArrowDown") {
            // Ctrl+Arrow Down ↓: Vorausgehenden Textabschnitt bearbeiten            

            if (state.document.LineCount() !== 0 && state.cursor.currentLineNo < state.document.LineCount() - 1) {
                let newCursorPos = state.document.textLines[state.cursor.currentLineNo + 1].length;
                SetNewCursorPos(state.cursor.currentLineNo + 1, newCursorPos);
            }
        }
        else if (key == "ArrowLeft") {
            // Arrow Left ←: Cursor nach links

            if (state.document.textLines[state.cursor.currentLineNo].length > 0
                && state.cursor.currentColNo > 0) {
                SetNewCursorPos(state.cursor.currentLineNo, state.cursor.currentColNo - 1);
            }
        }
        else if (key == "ArrowRight") {
            // Arrow Right →: Cursor nach rechts

            if (state.document.textLines[state.cursor.currentLineNo].length > 0
                && state.document.textLines[state.cursor.currentLineNo].length > state.cursor.currentColNo) {
                SetNewCursorPos(state.cursor.currentLineNo, state.cursor.currentColNo + 1);
            }
        }
        else if (key == "Home") {
            SetNewCursorPos(state.cursor.currentLineNo, 0);
        }
        else if (key == "End") {
            SetNewCursorPos(state.cursor.currentLineNo, state.document.textLines[state.cursor.currentLineNo].length);
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
                    currentLineNo: state.cursor.currentLineNo,
                    cursorSymbol: state.cursor.cursorSymbol
                },
                document: state.document,
                editShortCuts: state.editShortCuts,
                init: state.init,
                nytKeywords: state.nytKeywords,
                statusText: state.statusText,
                visibleLines: CountViewLines,
                keyGen: state.keyGen,
                altKey: false,
                rauteKey: false,
                ctrlKey: false,
                countEditOp: state.countEditOp + 1
            })
        }
        else if (key == "Delete") {
            // Zeichen rechts vom Cursor löschen
            let currentCursor = state.cursor.currentColNo;
            let currentLine = state.document.textLines[state.cursor.currentLineNo];

            if (currentLine.length > 0 && currentCursor < currentLine.length) {
                let left = currentLine.substring(0, currentCursor);
                let right = currentLine.substring(currentCursor + 1);
                currentLine = `${left}${right}`;
            }

            // Die aktuelle Zeile wird mit der modifizierten überschrieben
            state.document.textLines[state.cursor.currentLineNo] = currentLine;

            setState({
                cursor: {
                    currentColNo: currentCursor,
                    currentLineNo: state.cursor.currentLineNo,
                    cursorSymbol: state.cursor.cursorSymbol
                },
                document: state.document,
                editShortCuts: state.editShortCuts,
                init: state.init,
                nytKeywords: state.nytKeywords,
                statusText: state.statusText,
                visibleLines: CountViewLines,
                keyGen: state.keyGen,
                altKey: false,
                rauteKey: false,
                ctrlKey: false,
                countEditOp: state.countEditOp + 1
            })
        }
        else if (key == "Shift" || key == "Control") {
            // ignorieren
        }
        else if (fKeys.find((val) => val === key) != undefined) {
            // Funktionstasten aktuell ignorieren
            console.log(`${key} ignoriert`);
        }
        else {
            InsertCharInText(key);
        }
    }

    function SetNewCursorPos(cursorPosLine: number, cursorPosCol: number) {
        setState({
            cursor: {
                currentColNo: cursorPosCol,
                currentLineNo: cursorPosLine,
                cursorSymbol: state.cursor.cursorSymbol
            },
            document: state.document,
            editShortCuts: state.editShortCuts,
            init: state.init,
            nytKeywords: state.nytKeywords,
            statusText: state.statusText,
            visibleLines: CountViewLines,
            keyGen: state.keyGen,
            altKey: false,
            rauteKey: false,
            ctrlKey: false,
            countEditOp: state.countEditOp + 1
        })

    }

    function InsertCharInText(key: string) {

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
                currentLineNo: state.cursor.currentLineNo,
                cursorSymbol: state.cursor.cursorSymbol
            },
            document: state.document,
            editShortCuts: state.editShortCuts,
            init: state.init,
            nytKeywords: state.nytKeywords,
            statusText: state.statusText,
            visibleLines: CountViewLines,
            keyGen: state.keyGen,
            altKey: false,
            rauteKey: state.rauteKey,
            ctrlKey: state.ctrlKey,
            countEditOp: state.countEditOp + 1
        })
    }

    function SetAltKeyInState(altKey: boolean) {
        setState({
            cursor: {
                currentColNo: state.cursor.currentColNo,
                currentLineNo: state.cursor.currentLineNo,
                cursorSymbol: state.cursor.cursorSymbol
            },
            document: state.document,
            editShortCuts: state.editShortCuts,
            init: state.init,
            nytKeywords: state.nytKeywords,
            statusText: state.statusText,
            visibleLines: CountViewLines,
            keyGen: state.keyGen,
            altKey: altKey,
            rauteKey: state.rauteKey,
            ctrlKey: state.ctrlKey,
            countEditOp: state.countEditOp + 1
        })
    }

    function SetCtrlKeyInState(ctrlKey: boolean) {
        setState({
            cursor: {
                currentColNo: state.cursor.currentColNo,
                currentLineNo: state.cursor.currentLineNo,
                cursorSymbol: state.cursor.cursorSymbol
            },
            document: state.document,
            editShortCuts: state.editShortCuts,
            init: state.init,
            nytKeywords: state.nytKeywords,
            statusText: state.statusText,
            visibleLines: CountViewLines,
            keyGen: state.keyGen,
            altKey: state.altKey,
            rauteKey: state.rauteKey,
            ctrlKey: ctrlKey,
            countEditOp: state.countEditOp + 1
        })
    }

    function SetRauteKeyInState(rauteKey: boolean) {
        setState({
            cursor: {
                currentColNo: state.cursor.currentColNo,
                currentLineNo: state.cursor.currentLineNo,
                cursorSymbol: state.cursor.cursorSymbol
            },
            document: state.document,
            editShortCuts: state.editShortCuts,
            init: state.init,
            nytKeywords: state.nytKeywords,
            statusText: state.statusText,
            visibleLines: CountViewLines,
            keyGen: state.keyGen,
            altKey: state.altKey,
            rauteKey: rauteKey,
            ctrlKey: state.ctrlKey,
            countEditOp: state.countEditOp + 1
        })
    }



    function AddPreLines(vLines: any[], currentCursorLine: number) {

        let prePostLines = CountPrePostLines();

        if (prePostLines - currentCursorLine > 0) {
            // Leerraumzeilen am Anfang einfügen, falls Dokumentzeilen sichtbare Fläche nicht vollständig
            // ausfüllen.
            for (var i = 0, countEmptyLines = prePostLines - currentCursorLine; i < countEmptyLines; i++) {
                vLines.push(<CrossWriterEmptyLine
                    key={state.keyGen()}
                    cssClassLineNo="col cw-3 lineNo"
                    cssClassLine="col cw-56 lineContent"
                    cssClassLineFunction="col cw-6 lineFunc"></CrossWriterEmptyLine>);
            }

            // Der Editorzeile vorauseilende Zeilen des Dokumentes ausgeben
            for (var i = 0; i < currentCursorLine; i++, j++) {
                vLines.push(<CrossWriterLine
                    key={state.keyGen()}
                    document={state.document}
                    lineNo={i}
                    cssClassLineNo="col cw-3 lineNo"
                    cssClassLine="col cw-56 lineContent"
                    cssClassLineFunction="col cw-6 lineFunc"
                    nytKeywords={state.nytKeywords}></CrossWriterLine>);
            }

        }
        else {

            // Der Editorzeile vorauseilende Zeilen des Dokumentes ausgeben
            for (var i = 0, j = currentCursorLine - 1 - prePostLines; i < prePostLines; i++, j++) {
                vLines.push(<CrossWriterLine
                    key={state.keyGen()}
                    document={state.document}
                    lineNo={j}
                    cssClassLineNo="col cw-3 lineNo"
                    cssClassLine="col cw-56 lineContent"
                    cssClassLineFunction="col cw-6 lineFunc"
                    nytKeywords={state.nytKeywords}></CrossWriterLine>);
            }
        }
    }

    function AddPostLines(vLines: any[], currentCursorLine: number, LineCount: number) {

        let prePostLines = CountPrePostLines();

        if (LineCount - currentCursorLine < prePostLines) {
            for (var i = currentCursorLine + 1; i < LineCount; i++) {

                vLines.push(<CrossWriterLine
                    key={state.keyGen()}
                    document={state.document}
                    lineNo={i}
                    cssClassLineNo="col cw-3 lineNo"
                    cssClassLine="col cw-56 lineContent"
                    cssClassLineFunction="col cw-6 lineFunc"
                    nytKeywords={state.nytKeywords} ></CrossWriterLine >);
            }

            // Rest mit Leerzeilen auffüllen
            for (var i = 0, countEmptyLines = prePostLines - (LineCount - currentCursorLine); i < countEmptyLines; i++) {
                vLines.push(<CrossWriterEmptyLine
                    key={state.keyGen()}
                    cssClassLineNo="col cw-3 lineNo"
                    cssClassLine="col cw-56 lineContent"
                    cssClassLineFunction="col cw-6 lineFunc"></CrossWriterEmptyLine>);
            }
        }
        else {

            // Alle sichtbaren Zeilen nach der Edit- Zeile mit Zeilen aus dem Dokument füllen

            for (var i = 0, j = currentCursorLine + 1; i < prePostLines; i++, j++) {
                vLines.push(<CrossWriterLine
                    key={state.keyGen()}
                    document={state.document}
                    lineNo={j}
                    cssClassLineNo="col cw-3 lineNo"
                    cssClassLine="col cw-56 lineContent"
                    cssClassLineFunction="col cw-6 lineFunc"
                    nytKeywords={state.nytKeywords} ></CrossWriterLine >);
            }
        }
    }

    // Sicherer Abruf eines Namenscontainers
    //function getNameFromNc(NID: string, siegel: SiegelSuccessFunc<INamingContainer>, sowilo: SowiloErrFunc<ICrossWriterState>) : any {

    //    if (Object.keys(state.nytKeywords).find(key => key == NID) == undefined) {
    //        return sowilo(state, "getNameFromNc", ErrorClasses.ArgumentValidationFailed, `NID ${NID} cannot be found in state,´.nytKeyWords`);
    //    }
    //    else {
    //        let nc = state.nytKeywords[NID];
    //        return siegel(nc);
    //    }
    //}

    return (
        <div id="CrossWriterCtrl" className="CrossWriter">
            <header>
                <nav id="main_nav">
                    <div>
                        <button id="btnNewFile" className="btn btn-normal" tabIndex={10}>🗋 New</button>
                        <button id="btnOpenFile" className="btn btn-normal" tabIndex={20}>🖺 Open</button>
                        <button id="btnSave" className="btn btn-normal" tabIndex={30}>🖫 Save</button>
                        <button id="help" className="btn btn-normal" tabIndex={40}>🕮 Help</button>
                        <span id="currentDocName">{state.document.documentName == undefined ? "&nbsp;" : state.document.documentName}</span>
                    </div>
                    <div>
                        {getNameFromNc(state.nytKeywords, Nids.MKPRG.Naming.NYT.Keywords.CrossWriter,
                            (nc) => {
                                return <span className="progName">{nc.EN}</span>
                            },
                            (ncDict, fName, errClass, descr) => {
                                return <span className="progName">{`${fName} failed: Err Class: ${errClass}, ${descr}`}</span>
                            }
                        )}
                    </div>
                </nav>
            </header>            

            <div id="visibleLines" className="VisibleLines" >
                <input ref={invisibleInputFildForEdit}
                    onKeyDown={e => ProcessKeyDownEventForEditLine(e.key, e.ctrlKey, e.altKey, e.shiftKey)}
                    onBlur={e => {
                        // Keeps Foocus on Input field. See https://adueck.github.io/blog/keep-focus-when-clicking-on-element-react/
                        if (e.relatedTarget === null) {
                            e.target.focus();
                        }
                    }}>
                </input>
                {ViewLines()}
            </div>

            <footer className="row">
                <div id="statusLine" className="col col-10">Line: {state.cursor.currentLineNo} Col: {state.cursor.currentColNo} #Lines: {state.document.LineCount()} </div>
            </footer>
        </div>
    );
}

export default function CrossWriterSetUp(idRoot: string, ServerOrigin: string, documentName: string) {

    ReactDom.render(
        <React.StrictMode>

            <CrossWriter
                ServerOrigin={ServerOrigin}
                DocumentName={documentName}
                CursorSymbol="⧳"
                UserId="mko" />

        </React.StrictMode>
        , $(`#${idRoot}`)[0]);

}



