// mko, 28.12.2023
// Main react Component of CrossWriter

import React, { useEffect } from "react";
import ReactDom from "react-dom"
import $ from "jquery"

import { ErrorClasses, SiegelSuccessFunc, SowiloErrFunc, ArgumentValidationFailedDescriptor } from "./SiegelAndSowilo";

import NamingIds from "./NamingIds";
import INamingContainer from "./INamingContainer";

import { IDocument, IDocumentCursor, IDocumentHead, CreateDocument } from "./IDocument";
import { CrossWriterLine, ICrossWriterLineProps } from "./CrossWriterLine";
import { CrossWriterEditLine, ICrossWriterEditLineProps } from "./CrossWriterEditLine";
import { CrossWriterEmptyLine, ICrossWriterEmptyLineProps } from "./CrossWriterEmptyLine";

interface ICrossWriterProps {
    ServerOrigin: string,
    cssClass: string,
    UserId: string,
    NameSpaceNytNamingContainers: string,
    DocumentName: string
}


interface ICrossWriterState {
    init: boolean,

    // List of all NYT Keywords. Must be loaded from Server
    nytKeywords: INamingContainer[],

    // Mapping Key Board Shortcuts to Nyt Naming- Container.
    editShortCuts: Record<string, INamingContainer>,

    // the document, that will be edited by this control
    document: IDocument,

    // the currently edited position
    cursor: IDocumentCursor,

    // Count of visible Lines in Edit- Window
    visibleLines,

    // A short text, describing current status of edit control
    statusText: string
}

// This must be an uneven Number (count pre- Lines, edit- Line, count post- Lines)
const CountVisibleLines = 31;

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
    let [state, setState] = React.useState<ICrossWriterState>({
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
            $.ajax(`${properties.ServerOrigin}/NamingContainers?NC=${properties.NameSpaceNytNamingContainers}`, { method: "GET" })
                .done((data, textStatus, jqXhr) => {
                    let _ncList = data as Array<INamingContainer>;

                    let _editShortCuts: Record<string, INamingContainer>

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
                                    }
                                );
                            })
                            .fail((jqXHR, textStatus, errorThrown) => {
                            });
                    } else {
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

    React.useEffect(() => LoadResourcesFromServer(), []);

    // Berechnet die Anzahl der sichtbaren Zeilen vor und nach der Editor- Zeile
    function CountPrePostLines() { return (CountVisibleLines - 1) / 2 };

    // mko, 2.1.2024
    // Erzeugt Visuelle ausgabe der Edit- Zeile und der unmittelbar vor und nach der Edit- Zeile befindlichen
    // Zeilen des Dokumentes
    function VisibleLines(): any[] {

        let vLines = [<div></div>];

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
                    cssClassLineNo="col-1 lineNo"
                    cssClassLine="col-10 lineContent"
                    cssClassLineFunction="col-1 lineFunc"></CrossWriterEmptyLine>);
            }

            vLines.push(<CrossWriterEditLine
                document={state.document}
                lineNo={currentCursorLine}
                cssClassLineNo="col-1 lineNo"
                cssClassLine="col-10 EditLine"
                cssClassLineFunction="col-1 lineFunc"
                ProcessKeyDownEventForVisibleLines={ProcessKeyDownEventForVisibleLines}
                nytKeywords={state.nytKeywords}></CrossWriterEditLine>);

            // Leerzeilen nach der Editor- zeile aufbauen
            for (var i = 0; i < prePostLines; i++) {
                vLines.push(<CrossWriterEmptyLine
                    cssClassLineNo="col-1 lineNo"
                    cssClassLine="col-10 lineContent"
                    cssClassLineFunction="col-1 lineFunc"></CrossWriterEmptyLine>);
            }
        }
        else
        {
            AddPreLines(vLines, currentCursorLine);

            vLines.push(<CrossWriterEditLine
                document={state.document}
                lineNo={currentCursorLine}
                cssClassLineNo="col-1 lineNo"
                cssClassLine="col-10 EditLine"
                cssClassLineFunction="col-1 lineFunc"
                ProcessKeyDownEventForVisibleLines={ProcessKeyDownEventForVisibleLines}
                nytKeywords={state.nytKeywords}></CrossWriterEditLine>);

            AddPostLines(vLines, currentCursorLine, state.document.LineCount());
        }

        return vLines;
    }

    // KeyCodes siehe https://www.freecodecamp.org/news/javascript-keycode-list-keypress-event-key-codes/
    function ProcessKeyDownEventForVisibleLines(key: string, ctrlKey: boolean) {

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
                    visibleLines: CountVisibleLines

                })
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
                    visibleLines: CountVisibleLines

                })
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
                    visibleLines: CountVisibleLines

                })
            }
        }
        else if (key == "ArrowRight") {
            // Arrow Right →: Cursor nach rechts

            if (state.document.textLines[state.cursor.currentLineNo].length > 0
                && state.document.textLines[state.cursor.currentLineNo].length - 1 > state.cursor.currentColNo) {
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
                    visibleLines: CountVisibleLines

                })
            }
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
                visibleLines: CountVisibleLines
            })
        }

    }

    function AddPreLines(vLines: any[], currentCursorLine: number) {

        let prePostLines = CountPrePostLines();

        if (prePostLines - currentCursorLine > 0) {
            // Leerraumzeilen am Anfang einfügen, falls Dokumentzeilen sichtbare Fläche nicht vollständig
            // ausfüllen.
            for (var i = 0, countEmptyLines = prePostLines - currentCursorLine; i < countEmptyLines; i++) {
                vLines.push(<CrossWriterEmptyLine
                    cssClassLineNo="col-1 lineNo"
                    cssClassLine="col-10 lineContent"
                    cssClassLineFunction="col-1 lineFunc"></CrossWriterEmptyLine>);
            }

            // Der Editorzeile vorauseilende Zeilen des Dokumentes ausgeben
            for (var i = 0; i < currentCursorLine; i++, j++) {
                vLines.push(<CrossWriterLine
                    document={state.document}
                    lineNo={i}
                    cssClassLineNo="col-1 lineNo"
                    cssClassLine="col-10 lineContent"
                    cssClassLineFunction="col-1 lineFunc"
                    nytKeywords={state.nytKeywords}></CrossWriterLine>);
            }

        }
        else {

            // Der Editorzeile vorauseilende Zeilen des Dokumentes ausgeben
            for (var i = 0, j = currentCursorLine - 1 - prePostLines; i < prePostLines; i++, j++) {
                vLines.push(<CrossWriterLine
                    document={state.document}
                    lineNo={j}
                    cssClassLineNo="col-1 lineNo"
                    cssClassLine="col-10 lineContent"
                    cssClassLineFunction="col-1 lineFunc"
                    nytKeywords={state.nytKeywords}></CrossWriterLine>);
            }
        }
    }

    function AddPostLines(vLines: any[], currentCursorLine: number, LineCount: number) {

        let prePostLines = CountPrePostLines();


        if (LineCount - currentCursorLine < prePostLines) {
            for (var i = currentCursorLine + 1; i < LineCount; i++) {
                vLines.push(<CrossWriterLine
                    document={state.document}
                    lineNo={i}
                    cssClassLineNo="col-1 lineNo"
                    cssClassLine="col-10 lineContent"
                    cssClassLineFunction="col-1 lineFunc"
                    nytKeywords={state.nytKeywords} ></CrossWriterLine >);
            }

            // Rest mit Leerzeilen auffüllen
            for (var i = 0, countEmptyLines = prePostLines - (LineCount - currentCursorLine); i < countEmptyLines; i++) {
                vLines.push(<CrossWriterEmptyLine
                    cssClassLineNo="col-1 lineNo"
                    cssClassLine="col-10 lineContent"
                    cssClassLineFunction="col-1 lineFunc"></CrossWriterEmptyLine>);
            }
        }
        else {

            // Alle sichtbaren Zeilen nach der Edit- Zeile mit Zeilen aus dem Dokument füllen

            for (var i = 0, j = currentCursorLine + 1; i < prePostLines; i++, j++) {
                vLines.push(<CrossWriterLine
                    document={state.document}
                    lineNo={j}
                    cssClassLineNo="col-1 lineNo"
                    cssClassLine="col-10 lineContent"
                    cssClassLineFunction="col-1 lineFunc"
                    nytKeywords={state.nytKeywords} ></CrossWriterLine >);
            }
        }        
    }

    return (
        <div id="CrossWriter" className={properties.cssClass}>
            <header>
                <nav id="main_nav">
                    <button id="btnNewFile" className="btn btn-normal">🗋 New</button>
                    <button id="btnOpenFile" className="btn btn-normal">🖺 Open</button>
                    <button id="btnSave" className="btn btn-normal">🖫 Save</button>
                    <button id="help" className="btn btn-normal">🕮 Help</button>
                </nav>
            </header>
            ⌨
            <div id="visibleLines" onKeyDown={e => ProcessKeyDownEventForVisibleLines(e., e.ctrlKey )}>
                {VisibleLines()}
            </div>

            <footer>
                <div id="statusLine">Line: {state.cursor.currentLineNo} Col: {state.cursor.currentColNo} #Lines: {state.document.LineCount()} </div>
            </footer>
        </div>
    );
}

export default function CrossWriterSetUp(idRoot: string, ServerOrigin: string, cssClass: string, documentName: string) {

    ReactDom.render(<CrossWriter
        cssClass={cssClass}
        ServerOrigin={ServerOrigin}
        DocumentName={documentName}
        NameSpaceNytNamingContainers="MKPRG.Naming.NYT.Keywords"
        UserId="mko"        
    />, $(`#${idRoot}`)[0]);

}



