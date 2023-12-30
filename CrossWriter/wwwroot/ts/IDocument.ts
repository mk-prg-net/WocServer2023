// mko, 28.12.2023
// Allgemeine Dokumentstruktur

import { SiegelSuccessFunc, SowiloErrFunc, ErrorClasses } from "./SiegelAndSowilo"

export interface IDocumentHead {

    // filename of Document
    documentName: string,

    // UserId of Document autor
    autorUserId: string,

}

export interface IDocumentCursor {
    // Current Cursor Position: Line
    currentLineNo: number,

    // Current Cursor Position: Column
    currentColNo: number
}

// Common Document structure
export interface IDocument extends IDocumentHead {

    // Overall LineCount of Document
    LineCount: () => number,

    // content of Document, divided in Text- Lines
    textLines : string[]
}

// Class Factory for Documents
export function CreateDocument(authorUserId: string, documentName: string, text: string | undefined, siegel: SiegelSuccessFunc<IDocument>, sowilo: SowiloErrFunc<  ) {

    if (text === undefined) {

    }
    else {

        let lines = text.split(/\s*\\n+\s*/);

        var doc: IDocument = {
            documentName: documentName,
            autorUserId: authorUserId,
            textLines: lines,
            LineCount: () => lines.length
        };
    }

    return doc;
}