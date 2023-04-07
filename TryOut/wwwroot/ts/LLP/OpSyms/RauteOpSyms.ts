// mko, 29.3.2023
// **Lukasiewicz List Processor**

import IOpSym from "./IOpSym";

export default class RauteOpSyms implements IOpSym {

    public rpnFuncPrefix: string;
    public rpnBoolType: string;
    public rpnComment: string;
    public rpnNumType: string;
    public rpnIntType: string;
    public rpnListEnd: string;
    public rpnListStart: string;
    public rpnNoneToken: string;
    public rpnSingleLineComment: string;
    public rpnStrType: string;
    public rpnFuncHeadPrefix: string;

    constructor() {
        this.rpnFuncPrefix = "#";
        this.rpnBoolType = "#b";
        this.rpnComment = "#/";
        this.rpnNumType = "#n";
        this.rpnIntType = "#i";
        this.rpnListEnd = "#.";
        this.rpnListStart = "#_";
        this.rpnNoneToken = "#*";
        this.rpnSingleLineComment = "#//";
        this.rpnStrType = "#s";
        this.rpnFuncHeadPrefix = "#f";

    }
}