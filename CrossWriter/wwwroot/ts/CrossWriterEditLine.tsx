// mko, 2.1.2024
// Defines the only one editable Line in Crosswriter

import React, { useEffect } from "react";
import $, { fn } from "jquery"

import NamingIds from "./NamingIds";
import { ErrorClasses, SiegelSuccessFunc, SowiloErrFunc, ArgumentValidationFailedDescriptor } from "./SiegelAndSowilo";

import { INamingContainer, getNameFromNc } from "./INamingContainer"
import { IDocument, IDocumentCursor } from "./Document";

declare global {
    interface JQuery {
        setCursorPosition(arg: number): JQuery;
    }
}

export interface ICrossWriterEditLineProps {
    cssClassLineNo: string,
    cssClassLine: string,
    cssClassLineFunction: string, 
    cssClassCursor: string,
    document: IDocument,
    cursor : IDocumentCursor,
    nytKeywords: Record<string, INamingContainer>
}

// List of all NYT Keywords. Must be loaded from Server
var nytKeywords: Record<string, INamingContainer>


export function CrossWriterEditLine(properties: ICrossWriterEditLineProps) {
    // Die Liste der Schlüsselwörter wird einmalig in der Hauptkomponente CrossWriter
    // geladen. Hier wird nur eine referenz auf die Struktur abgelegt.
    nytKeywords = properties.nytKeywords;

    let editLineRef = React.useRef();

    function getTextLine(props: ICrossWriterEditLineProps, succF: SiegelSuccessFunc<ICrossWriterEditLineProps>, errF: SowiloErrFunc<ICrossWriterEditLineProps>): any {
        let lineNo = props.cursor.currentLineNo;
        let textLines = props.document.textLines;
        let res = <div>Error</div>;

        const fname = "getLineText";

        // Check Line No
        if (lineNo >= textLines.length) {
            res = errF.apply(null, ArgumentValidationFailedDescriptor(props, fname, "lineNo", lineNo, `lineNo is greater than textLines.length=${textLines.length}`));
        }
        else if (lineNo < 0) {
            res = errF.apply(null, ArgumentValidationFailedDescriptor(props, fname, "lineNo", lineNo, `lineNo is lower than 0`));
        }
        else {

            let textLine = props.document.textLines[lineNo];
            let cursorPos = props.cursor.currentColNo;
            let left = textLine.substring(0, cursorPos);
            let right = textLine.substring(cursorPos);

            res = succF(props, left, right);
        }
        return res;
    }

    function setFocus(lineNo: number): any {
        $("#editLine").setCursorPosition(6);
    }


    return (

        getTextLine(
            // State of Component
            properties,
            // SiegelSuccessFunc: if access to line was successful, it will be renderd here
            (state, left, right) =>
                <div className={"row"}>
                    <div className={properties.cssClassLineNo}>{state.cursor.currentLineNo}:</div>
                    <div id="editLine" className={properties.cssClassLine}>
                        {left}<span className={state.cssClassCursor}>{state.cursor.cursorSymbol}</span>{right}
                    </div>
                    <div className={properties.cssClassLineFunction}>┃&nbsp;</div>
                </div>,
            // SowiloErrFunc: if access to line was not ksuccessful, an error message will be rendered here
            (state, calledFName, errCls, ...args: any[]) =>
                <div className={"row"}>
                    <div className={properties.cssClassLineNo}>{state.cursor.currentLineNo}:</div>
                    <div className={properties.cssClassLine}>
                        {`${errCls}: called Function:${calledFName}, ${args.join()}`}
                    </div>
                    <div className={properties.cssClassLineFunction}>┃&nbsp;</div>
                </div>));
}
