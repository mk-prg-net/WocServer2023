// mko, 28.12.2023
// Allgemeine Dokumentstruktur

import ITextLineOverlay from "./ITextLineOverlay"

export default interface IDocument {

    // filename of Document
    documentName: string,

    // UserId of Document autor
    autorUserId: string,

    // Current Cursor Position: Line
    currentLineNo: number,

    // Current Cursor Position: Column
    currentColNo: number,

    // Overall LineCount of Document
    LineCount: number,

    // Overall row Count of Document
    ColCount: number,

    // the document- content/text
    text: string,

    // content of Document, divided in Text- Lines
    textLines : ITextLineOverlay[]
}