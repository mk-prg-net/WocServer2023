var __assign = (this && this.__assign) || function () {
    __assign = Object.assign || function(t) {
        for (var s, i = 1, n = arguments.length; i < n; i++) {
            s = arguments[i];
            for (var p in s) if (Object.prototype.hasOwnProperty.call(s, p))
                t[p] = s[p];
        }
        return t;
    };
    return __assign.apply(this, arguments);
};
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
        var _a = react_1.default.useState(""), greeting = _a[0], setGreting = _a[1];
        function handleGreetClick() {
            alert("Hello ".concat(greeting));
        }
        var charsRemainig = props.maxLength - greeting.length;
        var greetinginvalid = greeting.length === 0 || charsRemainig < 0;
        return ((0, jsx_runtime_1.jsxs)("div", { children: ["Greeting:", (0, jsx_runtime_1.jsx)("input", { value: greeting, onChange: function (e) { return setGreting(e.target.value); } }), (0, jsx_runtime_1.jsx)("span", { children: charsRemainig }), (0, jsx_runtime_1.jsx)("button", __assign({ disabled: greetinginvalid, onClick: handleGreetClick }, { children: "Greet" }))] }));
    }
    function WocHeaderReactCtrlSetUp(idElem) {
        react_dom_1.default.render((0, jsx_runtime_1.jsx)(Greeter, { maxlength: 20 }), (0, jquery_1.default)("#".concat(idElem))[0]);
    }
    exports.default = WocHeaderReactCtrlSetUp;
});
//# sourceMappingURL=WocHeaderReactCtrl.js.map