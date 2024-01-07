// mko, 26.4.2023
//
// mko, 28.12.2023
// Angepasst an neue Namenscontainer
define(["require", "exports", "./SiegelAndSowilo"], function (require, exports, SiegelAndSowilo_1) {
    "use strict";
    Object.defineProperty(exports, "__esModule", { value: true });
    exports.getNameFromNc = void 0;
    function getNameFromNc(ncDict, NID, siegel, sowilo) {
        if (Object.keys(ncDict).find(key => key == NID) == undefined) {
            return sowilo(ncDict, "getNameFromNc", SiegelAndSowilo_1.ErrorClasses.ArgumentValidationFailed, `NID ${NID} cannot be found in state,�.nytKeyWords`);
        }
        else {
            let nc = ncDict[NID];
            return siegel(nc);
        }
    }
    exports.getNameFromNc = getNameFromNc;
});
//# sourceMappingURL=INamingContainer.js.map