var __importDefault = (this && this.__importDefault) || function (mod) {
    return (mod && mod.__esModule) ? mod : { "default": mod };
};
define(["require", "exports", "react", "./SiegelAndSowilo"], function (require, exports, react_1, SiegelAndSowilo_1) {
    "use strict";
    Object.defineProperty(exports, "__esModule", { value: true });
    exports.CrossWriterLine = void 0;
    react_1 = __importDefault(react_1);
    // List of all NYT Keywords. Must be loaded from Server
    var nytKeywords;
    function CrossWriterLine(properties) {
        // Die Liste der Schlüsselwörter wird einmalig in der Hauptkomponente CrossWriter
        // geladen. Hier wird nur eine referenz auf die Struktur abgelegt.
        nytKeywords = properties.nytKeywords;
        function getTextLine(props, succF, errF) {
            let lineNo = props.lineNo;
            let textLines = props.document.textLines;
            let res = react_1.default.createElement("div", null, "Error");
            const fname = "getLineText";
            // Check Line No
            if (lineNo >= textLines.length) {
                res = errF.apply(null, (0, SiegelAndSowilo_1.ArgumentValidationFailedDescriptor)(props, fname, "lineNo", lineNo, `lineNo is greater than textLines.length=${textLines.length}`));
            }
            else if (lineNo < 0) {
                res = errF.apply(null, (0, SiegelAndSowilo_1.ArgumentValidationFailedDescriptor)(props, fname, "lineNo", lineNo, `lineNo is lower than 0`));
            }
            else {
                let textLine = props.document.textLines[lineNo];
                res = succF(props, textLine);
            }
            return res;
        }
        return (getTextLine(
        // State of Component
        properties, 
        // SiegelSuccessFunc: if access to line was successful, it will be renderd here
        (state, line) => react_1.default.createElement("div", { className: "row" },
            react_1.default.createElement("div", { className: properties.cssClassLineNo },
                state.lineNo,
                ":"),
            react_1.default.createElement("div", { className: properties.cssClassLine }, line),
            react_1.default.createElement("div", { className: properties.cssClassLineFunction }, "\u2503\u00A0")), 
        // SowiloErrFunc: if access to line was not ksuccessful, an error message will be rendered here
        (state, calledFName, errCls, ...args) => react_1.default.createElement("div", { className: "row" },
            react_1.default.createElement("div", { className: properties.cssClassLineNo },
                state.lineNo,
                ":"),
            react_1.default.createElement("div", { className: properties.cssClassLine }, `${errCls}: called Function:${calledFName}, ${args.join()}`),
            react_1.default.createElement("div", { className: properties.cssClassLineFunction }, "\u2503\u00A0"))));
    }
    exports.CrossWriterLine = CrossWriterLine;
});
//# sourceMappingURL=CrossWriterLine.js.map