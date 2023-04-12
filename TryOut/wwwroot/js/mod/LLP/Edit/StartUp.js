var __importDefault = (this && this.__importDefault) || function (mod) {
    return (mod && mod.__esModule) ? mod : { "default": mod };
};
define(["require", "exports", "jquery"], function (require, exports, jquery_1) {
    "use strict";
    Object.defineProperty(exports, "__esModule", { value: true });
    jquery_1 = __importDefault(jquery_1);
    requirejs.config({
        //By default load any module IDs from js/lib
        baseUrl: '../../js',
        paths: {
            jquery: "../node_modules/jquery/dist/jquery",
            react: "../node_modules/react/index",
            "react-dom": "../node_modules/react-dom/client"
        }
    });
    requirejs(['mod/WocHeaderReactCtrl/WocHeaderReactCtrl', 'mod/WocHeaderCtrl/SetUp', 'mod/LLP/OpSyms/RauteOpSyms', 'mod/LLP/StackOps', 'mod/LLP/StackElemStructs'], function (WocHeaderReactCtrl, WocHeaderCtrlSetUp, OpSyms, StackOps, StackElemStructs) {
        let opSyms = new OpSyms.default();
        let stackOps = new StackOps.default(opSyms);
        let stackElemStructs = new StackElemStructs.default(opSyms);
        WocHeaderCtrlSetUp.default(jquery_1.default, "ts/", stackOps, stackElemStructs);
        var llpStack = stackOps.NewStack();
        jquery_1.default.ajax({
            cache: false
        });
        //$('#woc-descriptor').WocHeaderCtrl({ llpStack: llpStack });        
        WocHeaderReactCtrl.default("react_greeting");
    });
});
//# sourceMappingURL=StartUp.js.map