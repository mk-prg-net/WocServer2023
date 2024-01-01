// mko, 28.12.2023
// Main react Component of CrossWriter

import React, { useEffect } from "react";
import ReacDom from "react-dom"
import $ from "jquery"

import { ErrorClasses, SiegelSuccessFunc, SowiloErrFunc, ArgumentValidationFailedDescriptor } from "./SiegelAndSowilo";

import NamingIds from "./NamingIds";
import INamingContainer from "./INamingContainer";

import { IDocument, IDocumentCursor, IDocumentHead, CreateDocument } from "./IDocument";
import CrossWriterLine from "./CrossWriterLine";

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


    function VisibleLines(): CrossWriterLine[] {





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
            <div id="visibleLines">
            </div>

            <footer>
                <div id="statusLine"></div>
            </footer>
        </div>
    );

}


