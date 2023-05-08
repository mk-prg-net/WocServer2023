/// <reference types="requirejs"/>
var __importDefault = (this && this.__importDefault) || function (mod) {
    return (mod && mod.__esModule) ? mod : { "default": mod };
};
define(["require", "exports", "react", "react-dom", "jquery"], function (require, exports, react_1, react_dom_1, jquery_1) {
    "use strict";
    Object.defineProperty(exports, "__esModule", { value: true });
    react_1 = __importDefault(react_1);
    react_dom_1 = __importDefault(react_dom_1);
    jquery_1 = __importDefault(jquery_1);
    function Greeter(props) {
        let [greeting, setGreting] = react_1.default.useState("");
        function handleGreetClick() {
            alert(`Hello ${greeting}`);
        }
        let charsRemainig = props.maxLength - greeting.length;
        let greetinginvalid = greeting.length === 0 || charsRemainig < 0;
        return (react_1.default.createElement("div", null,
            "Greeting:",
            react_1.default.createElement("input", { value: greeting, onChange: e => setGreting(e.target.value) }),
            react_1.default.createElement("span", null, charsRemainig),
            react_1.default.createElement("button", { disabled: greetinginvalid, onClick: handleGreetClick }, "Greet")));
    }
    function WocHeaderReactCtrlSetUp(idElem) {
        react_dom_1.default.render(react_1.default.createElement(Greeter, { maxLength: 20 }), (0, jquery_1.default)(`#${idElem}`)[0]);
    }
    exports.default = WocHeaderReactCtrlSetUp;
});
//# sourceMappingURL=WocHeaderReactCtrl.js.map