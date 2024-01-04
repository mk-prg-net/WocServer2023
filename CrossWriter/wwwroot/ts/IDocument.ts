// mko, 28.12.2023
// Allgemeine Dokumentstruktur

import { fn } from "jquery";
import { SiegelSuccessFunc, SowiloErrFunc, ErrorClasses, ArgumentValidationFailedDescriptor } from "./SiegelAndSowilo"

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
export function CreateDocument(
    authorUserId: string,
    documentName: string,
    text: string | undefined,
    siegel: SiegelSuccessFunc<IDocument>,
    sowilo: SowiloErrFunc<string>) {

    const fname = "CreateDocument";

    let doc: IDocument = {
        documentName: documentName,
        autorUserId: authorUserId,
        textLines: [""],
        LineCount: () => 0
    };

    let res: any = "";

    if (documentName === undefined) {
        res = sowilo.apply(null, ArgumentValidationFailedDescriptor(text, fname, "documentName", "undefined", "documentName must be defined"));
    }
    else if (!documentName.toLocaleLowerCase().endsWith(".cwf") && !documentName.toLocaleLowerCase().endsWith(".md")) {
        res = sowilo.apply(null, ArgumentValidationFailedDescriptor(text, fname, "documentName", documentName, "documentName ends with '.cwf' or '.md'"));
    }
    else if (text === undefined) {
        res = siegel(doc);
    }
    else {

        let lines = text.split(/[\r\n]+/);

        doc.textLines = lines;
        doc.LineCount = () => lines.length;       

        res = siegel(doc);
    }

    return res;
}