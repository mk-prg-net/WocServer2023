// mko, 29.3.2023
// **Lukasiewicz List Processor**

import IOpSym from "../IOpSym";

export default class RauteOpSyms implements IOpSym {
    rpnBoolType: "#b";
    rpnComment: "#/";
    rpnDblType: "#dbl";
    rpnFuncPrefix: "#f";
    rpnIntType: "#i";
    rpnListEnd: "#.";
    rpnListStart: "#_";
    rpnNoneToken: "#*";
    rpnSingleLineComment: "#//";
    rpnStrType: "#s";    
}