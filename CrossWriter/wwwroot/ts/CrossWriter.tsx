import React, { useEffect } from "react";
import ReacDom from "react-dom"
import $ from "jquery"
import NamingIds from "./NamingIds";
import INamingContainer from "./INamingContainer"
import IDocument from "./IDocument";

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

    // A short text, describing current status of edit control
    statusText: string

}

var UnkownNC = {
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
        document: {
            autorUserId: properties.UserId,
            ColCount: 0,
            currentColNo: 0,
            currentLineNo: 0,
            documentName: properties.DocumentName,
            LineCount: 0
        },
        nytKeywords: [UnkownNC],
        editShortCuts: { "none": UnkownNC },
        statusText: "start"
    });

    function LoadResourcesFromServer() {
        if (state.init) {
            $.ajax(`${properties.ServerOrigin}/NamingContainers?NC=${properties.NameSpaceNytNamingContainers}`, { method: "GET" })
                .done((data, textStatus, jqXhr) => {
                    let _ncList = data as Array<INamingContainer>;

                    // Zustand der React- Komponente neu setzten und rendern
                    setState({
                        init: false,
                        document: state.document,
                        nytKeywords: _ncList,
                        editShortCuts: state.editShortCuts,
                        statusText: "Resources loaded successful from Server"
                    });
                })
                .fail((jqXHR, textStatus, errorThrown) => {

                    let errTxt = `HTTP Status:${textStatus}, ${errorThrown}`;

                    // Zustand der React- Komponente neu setzten und rendern
                    setState({
                        init: false,
                        document: state.document,
                        nytKeywords: state.nytKeywords,
                        editShortCuts: state.editShortCuts,
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
        $("#pre").height()
    );

}


