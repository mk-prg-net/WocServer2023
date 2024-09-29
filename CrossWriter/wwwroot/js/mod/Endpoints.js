// mko, 29.9.2024
// Definition aller eingesetzer Endpunkte
define(["require", "exports"], function (require, exports) {
    "use strict";
    Object.defineProperty(exports, "__esModule", { value: true });
    exports.Endpoints = void 0;
    class Endpoints {
        constructor(serverOrigin) {
            this.serverOrigin = serverOrigin;
        }
        UrlForGetNamingContainers(rootNameSpace) {
            return `${this.serverOrigin}/NamingContainers?NC=${rootNameSpace}`;
        }
        UrlForDownloadFromFileStore(fileName) {
            return `${this.serverOrigin}/fileStore?fileName=${fileName}`;
        }
    }
    exports.Endpoints = Endpoints;
});
//# sourceMappingURL=Endpoints.js.map