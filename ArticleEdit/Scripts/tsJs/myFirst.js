// mko, 5.1.2023
// Testscript
define(["require", "exports", "jquery-3.6.3.min"], function (require, exports, jquery_3_6_3_min_1) {
    "use strict";
    Object.defineProperty(exports, "__esModule", { value: true });
    function greeting(txt, time) {
        (0, jquery_3_6_3_min_1.default)("#x").html("hallo");
        return `${time} ${txt}`;
    }
    var g = greeting("Hallo Welt", "9:00");
    let A = 99;
});
//# sourceMappingURL=myFirst.js.map