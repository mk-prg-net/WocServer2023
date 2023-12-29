// mko, 28.12.2023
// Main react Component of CrossWriter

import React, { useEffect } from "react";
import ReacDom from "react-dom"
import $ from "jquery"

import { ErrorClasses, SiegelSuccessFunc, SowiloErrFunc, ArgumentValidationFailedDescriptor } from "./SiegelAndSowilo";

import NamingIds from "./NamingIds";
import INamingContainer from "./INamingContainer";

import IDocument from "./IDocument";
import CrossWriterLine from "./CrossWriterLine";
import ITextLineOverlay from "./ITextLineOverlay";

interface ICrossWriterProps {
    ServerOrigin: string,
    cssClass: string,
    UserId: string,
    NameSpaceNytNamingContainers: string,
    DocumentName: string
}


interface ICrossWriterState {
    init: boolean,

    // the document, that will be edited by this control
    document: IDocument,

    // A short text, describing current status of edit control
    statusText: string
}


// Default- Namingcontainer
var UnkownNC : INamingContainer = {
    CNT: "unknown",
    DE: "unbekannt",
    EditShortCut: "unknown",
    EN: "unknown",
    Glyph: " ",
    GlyphUniCode: " ",
    NIDstr: "unknown"
};

var nonOverlay: ITextLineOverlay = {
    LineBegin: -1,
    LineEnd: -1
}

// List of all NYT Keywords. Must be loaded from Server
var nytKeywords: INamingContainer[] = [UnkownNC];

// Mapping Key Board Shortcuts to Nyt Naming- Container.
var editShortCuts: Record<string, INamingContainer> = { "none": UnkownNC };

export default function CrossWriter(properties: ICrossWriterProps) {
    // Define initial State
    let [state, setState] = React.useState<ICrossWriterState>({
        init: true,
        document: {
            autorUserId: properties.UserId,
            ColCount: 0,
            currentColNo: 0,
            currentLineNo: 0,
            documentName: properties.DocumentName,
            LineCount: 0,
            text: "",
            textLines: [nonOverlay]
        },
        statusText: "start"
    });

    function LoadResourcesFromServer() {
        if (state.init) {
            $.ajax(`${properties.ServerOrigin}/NamingContainers?NC=${properties.NameSpaceNytNamingContainers}`, { method: "GET" })
                .done((data, textStatus, jqXhr) => {
                    let _ncList = data as Array<INamingContainer>;

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

    React.useEffect(() => LoadResourcesFromServer(), []);

    return (
        <div id="CrossWriter" className={properties.cssClass}>
            <header>
                <nav id="main_nav">
                    <button id="btnNewFile" className="btn btn-normal">🗋 New</button>
                    <button id="btnOpenFile" className="btn btn-normal">🖺 Open</button>
                    <button id="btnSave" className="btn btn-normal">🖫 Save</button>
                    <button id="help" className="btn btn-normal">🕮 Help</button>
                </nav>
                <div id="pre" className="row">
                    <div id="pre_text_L" className="col col-4">

                    </div>
                    <div id="pre_text" className="col col-8">

                    </div>
                    <div id="pre_text_R" className="col col-4">

                    </div>
                </div>
                <div id="edit" className="row">
                    <div id="edit_text_L" className="col col-4">

                    </div>
                    <div className="col col-8">
                        <div id="edit_text" contentEditable="true" className="EditLine">

                        </div>                        
                    </div>
                    <div id="edit_text_R" className="col col-4">
                        ⌨
                    </div>
                </div>
                <div id="post" className="row">
                    <div id="post_text_L" className="col col-4">

                    </div>
                    <div id="post_text" className="col col-8">

                    </div>
                    <div id="post_text_R" className="col col-4">
                        
                    </div>
                </div>

            </header>

        </div>        
    );

}


