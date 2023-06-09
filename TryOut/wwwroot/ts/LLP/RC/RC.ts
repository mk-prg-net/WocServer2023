﻿// mko, 1.4.2023
// **Lukasiewicz List Processor**

import IRC from "../RC/IRC";

export default class RC implements IRC {

    constructor(success: boolean, errMsg: string) {
        this.ErrorMsgIfNotSuccesful = errMsg;
        this.Success = success;
    }
    
    public Success: boolean;
    public ErrorMsgIfNotSuccesful: string;
}