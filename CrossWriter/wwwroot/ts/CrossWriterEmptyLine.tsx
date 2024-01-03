// mko, 3.1.2023
// Leerraumzeile

import React, { useEffect } from "react";
import $, { fn } from "jquery"

import NamingIds from "./NamingIds";
import { ErrorClasses, SiegelSuccessFunc, SowiloErrFunc, ArgumentValidationFailedDescriptor } from "./SiegelAndSowilo";

import INamingContainer from "./INamingContainer"
import { IDocument } from "./IDocument";

export interface ICrossWriterEmptyLineProps {
    cssClassLineNo: string,
    cssClassLine: string,
    cssClassLineFunction: string
}


export function CrossWriterEmptyLine(properties: ICrossWriterEmptyLineProps) {

    return (
        <div className={"row"}>
            <div className={properties.cssClassLineNo}>&nbsp;</div>
            <div className={properties.cssClassLine}>
                &nbsp;
            </div>
            <div className={properties.cssClassLineFunction}>&nbsp;</div>
        </div>
    )
}