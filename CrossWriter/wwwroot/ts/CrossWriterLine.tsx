import React, { useEffect } from "react";
import ReacDom from "react-dom"
import $, { fn } from "jquery"

import NamingIds from "./NamingIds";
import {ErrorClasses, SiegelSuccessFunc, SowiloErrFunc, ArgumentValidationFailedDescriptor } from "./SiegelAndSowilo";

import INamingContainer from "./INamingContainer"
import IDocument from "./IDocument";
import ITextLineOverlay from "./ITextLineOverlay"

interface ICrossWriterLineProps {    
    cssClassLineNo: string,
    cssClassLine: string,
    cssClassLineFunction: string,
    lineNo: number,
    document:  IDocument,
    nytKeywords: INamingContainer[]    
}


interface ICrossWriterLineState {
    init: boolean,
    cssClassLineNo: string,
    cssClassLine: string,
    cssClassLineFunction: string,
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
        cssClassLine: properties.cssClassLine,
        cssClassLineFunction: properties.cssClassLineFunction,
        cssClassLineNo: properties.cssClassLineNo,
        document: properties.document,
        lineNo: properties.lineNo,
        init: true
    });


    function getLineText(state: ICrossWriterLineState, succF: SiegelSuccessFunc<ICrossWriterLineState>, errF: SowiloErrFunc<ICrossWriterLineState>): any
    {
        let line = "";
        let lineNo = state.lineNo;
        let textLines = state.document.textLines;
        let succeeded = false;
        let lenTxt = state.document.text.length;

        let res = <div>Error</div>;

        const fname = "getLineText";

        if (lineNo >= textLines.length) {
            res = errF.apply(null, ArgumentValidationFailedDescriptor(state, fname, "lineNo", lineNo, `lineNo is greater than textLines.length=${textLines.length}`));
        }
        else if (textLines[lineNo].LineBegin < 0) {
            res = errF.apply(null, ArgumentValidationFailedDescriptor(state, fname, `textLines[${lineNo}].LineBegin`, textLines[lineNo].LineBegin, `textLines[${lineNo}].LineBegin < 0`));
        }
        else if (textLines[lineNo].LineBegin >= textLines.length) {
            res = errF.apply(null, ArgumentValidationFailedDescriptor(state, fname, `textLines[${lineNo}].LineBegin`, textLines[lineNo].LineBegin, `textLines[${lineNo}].LineBegin >= ${textLines.length}`));
        }
        else if (textLines[lineNo].LineEnd < 0) {
            res = errF.apply(null, ArgumentValidationFailedDescriptor(state, fname, `textLines[${lineNo}].LineEnd`, textLines[lineNo].LineEnd, `textLines[${lineNo}].LineEnd < 0`));
        }
        else if (textLines[lineNo].LineEnd >= textLines.length) {
            res = errF.apply(null, ArgumentValidationFailedDescriptor(state, fname, `textLines[${lineNo}].LineEnd`, textLines[lineNo].LineEnd, `textLines[${lineNo}].LineEnd >= ${textLines.length}`));
        }
        else if (textLines[lineNo].LineEnd < 0) {
            res = errF.apply(null, ArgumentValidationFailedDescriptor(state, fname, `textLines[${lineNo}].LineEnd`, textLines[lineNo].LineEnd, `textLines[${lineNo}].LineEnd < 0`));
        }
        else if (textLines[lineNo].LineBegin >= textLines[lineNo].LineEnd) {
            res = errF.apply(null, ArgumentValidationFailedDescriptor(state, fname, `textLines[${lineNo}].LineBegin`, textLines[lineNo].LineBegin, `textLines[${lineNo}].LineBegin >= textLines[${lineNo}].LineEnd= ${textLines[lineNo].LineEnd}`));
        }
        else {
            
            let textLine = state.document.textLines[lineNo];
            line = state.document.text.substring(textLine.LineBegin, textLine.LineEnd);
            res = succF(state, line);            
        }
        return res;
    }

    return (
        
        getLineText(
            // State of Component
            state,          
            // SiegelSuccessFunc: if access to line was successful, it will be renderd here
            (state, line) =>
                <div className={"row"}>
                    <div className={state.cssClassLineNo}>{state.lineNo}</div>
                    <div className={state.cssClassLine}>
                        {line}
                    </div>
                    <div className={state.cssClassLineFunction}>&nbsp;</div>
                </div>,
            // SowiloErrFunc: if access to line was not ksuccessful, an error message will be rendered here
            (state, calledFName, errCls, ...args: any[]) =>
                <div className={"row"}>
                    <div className={state.cssClassLineNo}>{state.lineNo}</div>
                    <div className={state.cssClassLine}>
                        {`${errCls}: called Function:${calledFName}, ${args.join()}`}
                    </div>
                    <div className={state.cssClassLineFunction}>&nbsp;</div>                    
                </div>));
}
