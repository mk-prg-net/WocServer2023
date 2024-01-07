// mko, 26.4.2023
//
// mko, 28.12.2023
// Angepasst an neue Namenscontainer

import { ErrorClasses, SiegelSuccessFunc, SowiloErrFunc } from "./SiegelAndSowilo";

export interface INamingContainer {
    NIDstr: string,
    DE: string,
    EN: string,
    CNT: string,
    EditShortCut: string,
    Glyph: string,
    GlyphUniCode: string
}

export function getNameFromNc(ncDict: Record<string, INamingContainer>, NID: string, siegel: SiegelSuccessFunc<INamingContainer>, sowilo: SowiloErrFunc<Record<string, INamingContainer>>): any {

    if (Object.keys(ncDict).find(key => key == NID) == undefined) {
        return sowilo(ncDict, "getNameFromNc", ErrorClasses.ArgumentValidationFailed, `NID ${NID} cannot be found in state,´.nytKeyWords`);
    }
    else {
        let nc = ncDict[NID];
        return siegel(nc);
    }
}


