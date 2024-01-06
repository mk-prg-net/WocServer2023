// mko, 2.1.2024
// Defines the only one editable Line in Crosswriter
var __importDefault = (this && this.__importDefault) || function (mod) {
    return (mod && mod.__esModule) ? mod : { "default": mod };
};
define(["require", "exports", "react", "jquery", "./SiegelAndSowilo"], function (require, exports, react_1, jquery_1, SiegelAndSowilo_1) {
    "use strict";
    Object.defineProperty(exports, "__esModule", { value: true });
    exports.CrossWriterEditLine = void 0;
    react_1 = __importDefault(react_1);
    jquery_1 = __importDefault(jquery_1);
    // List of all NYT Keywords. Must be loaded from Server
    var nytKeywords;
    function CrossWriterEditLine(properties) {
        // Die Liste der Schlüsselwörter wird einmalig in der Hauptkomponente CrossWriter
        // geladen. Hier wird nur eine referenz auf die Struktur abgelegt.
        nytKeywords = properties.nytKeywords;
        let editLineRef = react_1.default.useRef();
        function getTextLine(props, succF, errF) {
            let lineNo = props.cursor.currentLineNo;
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
                let cursorPos = props.cursor.currentColNo;
                let left = textLine.substring(0, cursorPos).replace(" ", " &nbsp; ");
                let right = textLine.substring(cursorPos).replace(" ", " &nbsp; ");
                res = succF(props, left, right);
            }
            return res;
        }
        function setFocus(lineNo) {
            (0, jquery_1.default)("#editLine").setCursorPosition(6);
        }
        return (getTextLine(
        // State of Component
        properties, 
        // SiegelSuccessFunc: if access to line was successful, it will be renderd here
        (state, left, right) => react_1.default.createElement("div", { className: "row" },
            react_1.default.createElement("div", { className: properties.cssClassLineNo },
                state.cursor.currentLineNo,
                ":"),
            react_1.default.createElement("div", { id: "editLine", className: properties.cssClassLine },
                left,
                " ",
                react_1.default.createElement("span", { className: state.cssClassCursor }, state.cursor.cursorSymbol),
                " ",
                right),
            react_1.default.createElement("div", { className: properties.cssClassLineFunction }, "\u2503\u00A0")), 
        // SowiloErrFunc: if access to line was not ksuccessful, an error message will be rendered here
        (state, calledFName, errCls, ...args) => react_1.default.createElement("div", { className: "row" },
            react_1.default.createElement("div", { className: properties.cssClassLineNo },
                state.cursor.currentLineNo,
                ":"),
            react_1.default.createElement("div", { className: properties.cssClassLine }, `${errCls}: called Function:${calledFName}, ${args.join()}`),
            react_1.default.createElement("div", { className: properties.cssClassLineFunction }, "\u2503\u00A0"))));
    }
    exports.CrossWriterEditLine = CrossWriterEditLine;
});
//# sourceMappingURL=CrossWriterEditLine.js.map