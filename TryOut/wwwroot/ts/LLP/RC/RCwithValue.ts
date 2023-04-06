// mko, 1.4.2023
// **Lukasiewicz List Processor**


import IRCwithValue from "../RC/IRCwithValue";
import IRC from "../RC/IRCwithValue";

export default class RCwithValue<TValue> implements IRCwithValue<TValue> {

    constructor(success: boolean, errMsg: string, value: TValue) {
        this.ErrorMsgIfNotSuccesful = errMsg;
        this.Success = success;
        this.ReturnValue = value;
    }

    Success: boolean;
    ErrorMsgIfNotSuccesful: string;
    ReturnValue: TValue;
}