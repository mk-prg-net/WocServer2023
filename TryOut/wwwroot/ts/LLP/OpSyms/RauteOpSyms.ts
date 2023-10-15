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
    public rpnNidPrefix: string;
    public rpnInstancePrefix: string;
    public rpnPropPrefix: string;
    public rpnReturnPrefix: string;
    public rpnArrayPrefix: string;


    constructor() {
        this.rpnFuncPrefix = "#";
        this.rpnNidPrefix = "#*";
        this.rpnNoneToken = "#0"
        this.rpnComment = "#/";
        this.rpnSingleLineComment = "#//";
        this.rpnBoolType = "#b";        
        this.rpnNumType = "#q";
        this.rpnIntType = "#z";
        this.rpnStrType = "#s";
        this.rpnListStart = "#:";
        this.rpnListEnd = "#.";                
        this.rpnFuncHeadPrefix = "#m";
        this.rpnInstancePrefix = "#i";
        this.rpnPropPrefix = "#+";
        this.rpnReturnPrefix = "#<";
        this.rpnArrayPrefix = "#a";
    }
}