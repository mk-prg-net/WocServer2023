import React, { useEffect } from "react";
import ReacDom from "react-dom"
import $ from "jquery"
import NamingIds from "./NamingIds";
import INamingContainer from "./INamingContainer"
import IDocument from "./IDocument";
import ITextLineOverlay from "./ITextLineOverlay"

interface ICrossWriterLineProps {
    ServerOrigin: string,
    cssClass: string,
    lineNo: number,
    document:  IDocument,
    nytKeywords: INamingContainer[]    
}


interface ICrossWriterLineState {
    init: boolean,
    cssClass: string,
    lineNo: number,
    document: IDocument
}

// List of all NYT Keywords. Must be loaded from Server
var nytKeywords: INamingContainer[];


export default function CrossWriterLine(properties: ICrossWriterLineProps) {
    // Die Liste der Schlüsselwörter wird einmalig in der Hauptkomponente CrossWriter
    // geladen. Hier wird nur eine referenz auf die Struktur abgelegt.
    nytKeywords = properties.nytKeywords;

    // Define initial State
    let [state, setState] = React.useState<ICrossWriterLineState>({
        cssClass : properties.cssClass,
        document : properties.document,
        lineNo : properties.lineNo,
        init: true
    });

    function getLineText(doc: IDocument,  lineNo: number): string {

        let textLine = doc.textLines[lineNo];
        doc.text.substring(textLine.LineBegin,
    }

    return (
        <div className={state.cssClass}>
            {document. }
        </div>
    );

}
