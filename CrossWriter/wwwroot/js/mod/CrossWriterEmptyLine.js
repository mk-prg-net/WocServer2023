// mko, 3.1.2023
// Leerraumzeile
var __importDefault = (this && this.__importDefault) || function (mod) {
    return (mod && mod.__esModule) ? mod : { "default": mod };
};
define(["require", "exports", "react"], function (require, exports, react_1) {
    "use strict";
    Object.defineProperty(exports, "__esModule", { value: true });
    exports.CrossWriterEmptyLine = void 0;
    react_1 = __importDefault(react_1);
    function CrossWriterEmptyLine(properties) {
        return (react_1.default.createElement("div", { className: "row" },
            react_1.default.createElement("div", { className: properties.cssClassLineNo }, "\u00A0"),
            react_1.default.createElement("div", { className: properties.cssClassLine }, "\u00A0"),
            react_1.default.createElement("div", { className: properties.cssClassLineFunction }, "\u00A0")));
    }
    exports.CrossWriterEmptyLine = CrossWriterEmptyLine;
});
//# sourceMappingURL=CrossWriterEmptyLine.js.map