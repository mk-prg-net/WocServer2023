// mko, 31.10.2023
// **Lukasiewicz List Processor**
// OpSyms as runic

import IOpSym from "./IOpSym";

export default class RunicOpSyms implements IOpSym {
    public rpnNoneToken: string;
    public rpnNidPrefix: string;
    public rpnBoolType: string;
    public rpnIntType: string;
    public rpnNumType: string;
    public rpnStrType: string;
    public rpnArrayPrefix: string;
    public rpnListStart: string;
    public rpnListEnd: string;
    public rpnFuncPrefix: string;
    public rpnFuncHeadPrefix: string;
    public rpnReturnPrefix: string;
    public rpnInstancePrefix: string;
    public rpnPropPrefix: string;
    public rpnComment: string;
    public rpnSingleLineComment: string;


    constructo() {
        this.rpnArrayPrefix = "ᛥ";
        this.rpnBoolType = "ᛔ";
        this.rpnComment = "᛭";
        this.rpnFuncHeadPrefix = "ᚪ";
        this.rpnFuncPrefix = "ᚪ";             
        this.rpnInstancePrefix = "ᛝ";
        this.rpnIntType = "ᛕ";
        this.rpnListEnd = "ᛩ";
        this.rpnListStart = "ᚹ";            
        this.rpnNidPrefix = "ᚻ";
        this.rpnNoneToken = "";
        this.rpnNumType = "ᚱ";
        this.rpnPropPrefix = "ᛜ";
        this.rpnReturnPrefix = "ᛏ";
        this.rpnSingleLineComment = "᛭᛭";
        this.rpnStrType = ""

    }
}