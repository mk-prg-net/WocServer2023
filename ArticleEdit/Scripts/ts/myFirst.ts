﻿// mko, 5.1.2023
// Testscript

import $ from "jquery-3.6.3.min";

function greeting(txt: string, time: string): string {    
    $("#x").html("hallo");
    return `${time} ${txt}`;
}






var g = greeting("Hallo Welt", "9:00");

let A = 99;

