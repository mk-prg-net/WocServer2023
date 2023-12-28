// mko, 28.12.2023
//
define(["require", "exports"], function (require, exports) {
    "use strict";
    Object.defineProperty(exports, "__esModule", { value: true });
    exports.CreateDictionary = void 0;
    class Dict {
        constructor() {
            this.GetValue = {};
        }
    }
    function CreateDictionary() {
        return new Dict();
    }
    exports.CreateDictionary = CreateDictionary;
});
//# sourceMappingURL=Dictionary.js.map