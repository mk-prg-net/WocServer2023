// mko, 26.4.2023
// React Komponente, die eine Naming- Id in einen Namen auflöst
var __importDefault = (this && this.__importDefault) || function (mod) {
    return (mod && mod.__esModule) ? mod : { "default": mod };
};
define(["require", "exports", "react"], function (require, exports, react_1) {
    "use strict";
    Object.defineProperty(exports, "__esModule", { value: true });
    react_1 = __importDefault(react_1);
    // mko, 26.4.2023
    // Der Kontext sammelt in einem ersten Durchlauf alle NIDs der Kindelemente.
    // Für diese Liste von NIDs wird dann eine Liste von Naming- Containern angefordert.
    // Im zweiten Durchlauf werden die NIDs entsprechend der ausgewählten Sprache gerendert
    function NIDContext({ properties, children }) {
        let [first, setNIDContex] = react_1.default.useState(false);
        let nidCtxProps = properties;
        return react_1.default.createElement("div", null);
    }
});
//# sourceMappingURL=NIDSpan.js.map