var __importDefault = (this && this.__importDefault) || function (mod) {
    return (mod && mod.__esModule) ? mod : { "default": mod };
};
define(["require", "exports", "react/jsx-runtime", "react", "react-dom", "jquery"], function (require, exports, jsx_runtime_1, react_1, react_dom_1, jquery_1) {
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
        return (jsx_runtime_1.jsxs("div", { children: ["Greeting:", jsx_runtime_1.jsx("input", { value: greeting, onChange: e => setGreting(e.target.value) }, void 0), jsx_runtime_1.jsx("span", { children: charsRemainig }, void 0), jsx_runtime_1.jsx("button", Object.assign({ disabled: greetinginvalid, onClick: handleGreetClick }, { children: "Greet" }), void 0)] }, void 0));
    }
    function WocHeaderReactCtrlSetUp(idElem) {
        react_dom_1.default.render(jsx_runtime_1.jsx(Greeter, { maxlength: 20 }, void 0), jquery_1.default(`#${idElem}`)[0]);
    }
    exports.default = WocHeaderReactCtrlSetUp;
});
//# sourceMappingURL=WocHeaderReactCtrl.js.map