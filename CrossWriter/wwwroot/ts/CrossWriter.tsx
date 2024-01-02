// mko, 28.12.2023
// Main react Component of CrossWriter

import React, { useEffect } from "react";
import ReacDom from "react-dom"
import $ from "jquery"

import { ErrorClasses, SiegelSuccessFunc, SowiloErrFunc, ArgumentValidationFailedDescriptor } from "./SiegelAndSowilo";

import NamingIds from "./NamingIds";
import INamingContainer from "./INamingContainer";

import { IDocument, IDocumentCursor, IDocumentHead, CreateDocument } from "./IDocument";
import { CrossWriterLine, ICrossWriterLineProps } from "./CrossWriterLine";

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

export default function CrossWriter(properties: ICrossWriterProps) {
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

    // mko, 2.1.2024
    // Erzeugt Visuelle ausgabe der Edit- Zeile und der unmittelbar vor und nach der Edit- Zeile befindlichen
    // Zeilen des Dokumentes
    function VisibleLines(): any[] {

        let vLines = [<div></div>];

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
        // []

        let prePostLines = (CountVisibleLines - 1) / 2;

        if (lineCount() === 0) {
            // Fall: []
            vLines.push(<CrossWriterLine
                document={state.document}
                lineNo={currentCursorLine}
                cssClassLineNo="col-1 lineNo"
                cssClassLine="col-10 EditLine"
                cssClassLineFunction="col-1 lineFunc"
                nytKeywords={state.nytKeywords}></CrossWriterLine>);

        }
        else if (currentCursorLine < prePostLines && (lineCount() - prePostLines)) {

            // Fälle
            // [*++]
            // [+*+]
            // [++*]

            AddPreLines(vLines, 0, currentCursorLine);

            vLines.push(<CrossWriterLine
                document={state.document}
                lineNo={currentCursorLine}
                cssClassLineNo="col-1 lineNo"
                cssClassLine="col-10 EditLine"
                cssClassLineFunction="col-1 lineFunc"
                nytKeywords={state.nytKeywords}></CrossWriterLine>);

            AddPostLines(vLines, currentCursorLine, state.document.LineCount());

        }
        else if (currentCursorLine < prePostLines) {
            // Fälle
            // [*++]++++++++++
            // [+*++]+++++++++
            // [++*++]++++++++

            AddPreLines(vLines, 0, currentCursorLine);

            vLines.push(<CrossWriterLine
                document={state.document}
                lineNo={currentCursorLine}
                cssClassLineNo="col-1 lineNo"
                cssClassLine="col-10 EditLine"
                cssClassLineFunction="col-1 lineFunc"
                nytKeywords={state.nytKeywords}></CrossWriterLine>);

            AddPostLines(vLines, currentCursorLine, currentCursorLine + prePostLines + 1);

        }
        else if (currentCursorLine > (lineCount() - prePostLines)) {

            // Fall +++[++*++]+++++

            AddPreLines(vLines, currentCursorLine - prePostLines - 1, currentCursorLine);

            vLines.push(<CrossWriterLine
                document={state.document}
                lineNo={currentCursorLine}
                cssClassLineNo="col-1 lineNo"
                cssClassLine="col-10 EditLine"
                cssClassLineFunction="col-1 lineFunc"
                nytKeywords={state.nytKeywords}></CrossWriterLine>);

            AddPostLines(vLines, currentCursorLine, state.document.LineCount());


        }
        else {

            // ++++++++[++*++]
            // +++++++++[++*+]
            // ++++++++++[++*]


            AddPreLines(vLines, currentCursorLine - prePostLines - 1, currentCursorLine);

            vLines.push(<CrossWriterLine
                document={state.document}
                lineNo={currentCursorLine}
                cssClassLineNo="col-1 lineNo"
                cssClassLine="col-10 EditLine"
                cssClassLineFunction="col-1 lineFunc"
                nytKeywords={state.nytKeywords}></CrossWriterLine>);

            AddPostLines(vLines, currentCursorLine, currentCursorLine + prePostLines + 1);
        }

        return vLines;
    }

    function AddPreLines(vLines: any[], start: number, currentCursorLine: number): void {
        for (var i = start; i < currentCursorLine; i++) {
            vLines.push(<CrossWriterLine
                document={state.document}
                lineNo={i}
                cssClassLineNo="col-1 lineNo"
                cssClassLine="col-10 lineContent"
                cssClassLineFunction="col-1 lineFunc"
                nytKeywords={state.nytKeywords}></CrossWriterLine>);
        }
    }

    function AddPostLines(vLines: any[], currentCursorLine: number, endLineNo: number) {
        for (var i = currentCursorLine + 1, end = endLineNo; i < end; i++) {
            vLines.push(<CrossWriterLine
                document={state.document}
                lineNo={i}
                cssClassLineNo="col-1 lineNo"
                cssClassLine="col-10 lineContent"
                cssClassLineFunction="col-1 lineFunc"
                nytKeywords={state.nytKeywords} ></CrossWriterLine >);
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


