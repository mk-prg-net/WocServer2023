// mko, 2.1.2024
// Defines the only one editable Line in Crosswriter

import React, { useEffect } from "react";
import $, { fn } from "jquery"

import NamingIds from "./NamingIds";
import { ErrorClasses, SiegelSuccessFunc, SowiloErrFunc, ArgumentValidationFailedDescriptor } from "./SiegelAndSowilo";

import INamingContainer from "./INamingContainer"
import { IDocument } from "./IDocument";

export interface ICrossWriterEditLineProps {
    cssClassLineNo: string,
    cssClassLine: string,
    cssClassLineFunction: string,
    lineNo: number,
    document: IDocument,
    nytKeywords: INamingContainer[]
}


// List of all NYT Keywords. Must be loaded from Server
var nytKeywords: INamingContainer[];


export function CrossWriterEditLine(properties: ICrossWriterEditLineProps) {
    // Die Liste der Schl�sselw�rter wird einmalig in der Hauptkomponente CrossWriter
    // geladen. Hier wird nur eine referenz auf die Struktur abgelegt.
    nytKeywords = properties.nytKeywords;

    function getTextLine(props: ICrossWriterEditLineProps, succF: SiegelSuccessFunc<ICrossWriterEditLineProps>, errF: SowiloErrFunc<ICrossWriterEditLineProps>): any {
        let lineNo = props.lineNo;
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
            res = succF(props, textLine);
        }
        return res;
    }

    function ProcessKeyDownEventForVisibleLines(keyCode: number, ctrlKey: boolean) {

    }

    function MoveLineUp() {

    }

    function MoveLineDown() {

    }


    return (

        getTextLine(
            // State of Component
            properties,
            // SiegelSuccessFunc: if access to line was successful, it will be renderd here
            (state, line) =>
                <div className={"row"}>
                    <div className={properties.cssClassLineNo}>{state.lineNo}</div>
                    <div className={properties.cssClassLine} contentEditable onKeyDown={e => ProcessKeyDownEventForVisibleLines(e.keyCode, e.ctrlKey) }>
                        {line}
                    </div>
                    <div className={properties.cssClassLineFunction}>&nbsp;</div>
                </div>,
            // SowiloErrFunc: if access to line was not ksuccessful, an error message will be rendered here
            (state, calledFName, errCls, ...args: any[]) =>
                <div className={"row"}>
                    <div className={properties.cssClassLineNo}>{state.lineNo}</div>
                    <div className={properties.cssClassLine}>
                        {`${errCls}: called Function:${calledFName}, ${args.join()}`}
                    </div>
                    <div className={properties.cssClassLineFunction}>&nbsp;</div>
                </div>));
}
