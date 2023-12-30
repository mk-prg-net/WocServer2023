import React, { useEffect } from "react";
import ReacDom from "react-dom"
import $, { fn } from "jquery"

import NamingIds from "./NamingIds";
import {ErrorClasses, SiegelSuccessFunc, SowiloErrFunc, ArgumentValidationFailedDescriptor } from "./SiegelAndSowilo";

import INamingContainer from "./INamingContainer"
import IDocument from "./IDocument";

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


    function getTextLine(state: ICrossWriterLineState, succF: SiegelSuccessFunc<ICrossWriterLineState>, errF: SowiloErrFunc<ICrossWriterLineState>): any
    {
        let lineNo = state.lineNo;
        let textLines = state.document.textLines;
        let res = <div>Error</div>;

        const fname = "getLineText";

        // Check Line No
        if (lineNo >= textLines.length) {            
            res = errF.apply(null, ArgumentValidationFailedDescriptor(state, fname, "lineNo", lineNo, `lineNo is greater than textLines.length=${textLines.length}`));
        }
        else if (lineNo < 0) {
            res = errF.apply(null, ArgumentValidationFailedDescriptor(state, fname, "lineNo", lineNo, `lineNo is lower than 0`));
        }
        else {
            
            let textLine = state.document.textLines[lineNo];
            res = succF(state, textLine);            
        }
        return res;
    }

    return (
        
        getTextLine(
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
