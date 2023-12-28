var __importDefault = (this && this.__importDefault) || function (mod) {
    return (mod && mod.__esModule) ? mod : { "default": mod };
};
define(["require", "exports", "react"], function (require, exports, react_1) {
    "use strict";
    Object.defineProperty(exports, "__esModule", { value: true });
    react_1 = __importDefault(react_1);
    // List of all NYT Keywords. Must be loaded from Server
    var nytKeywords;
    function CrossWriterLine(properties) {
        // Die Liste der Schlüsselwörter wird einmalig in der Hauptkomponente CrossWriter
        // geladen. Hier wird nur eine referenz auf die Struktur abgelegt.
        nytKeywords = properties.nytKeywords;
        // Define initial State
        let [state, setState] = react_1.default.useState({
            cssClass: properties.cssClass,
            document: properties.document,
            lineNo: properties.lineNo,
            init: true
        });
        return (react_1.default.createElement("div", { className: state.cssClass }));
    }
    exports.default = CrossWriterLine;
});
//# sourceMappingURL=CrossWriterLine.js.map