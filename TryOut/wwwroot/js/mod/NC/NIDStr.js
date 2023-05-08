// mko, 26.4.2023
// React Komponente, die eine Naming- Id in einen Namen aufl�st
var __importDefault = (this && this.__importDefault) || function (mod) {
    return (mod && mod.__esModule) ? mod : { "default": mod };
};
define(["require", "exports", "react", "jquery", "./NamingIds"], function (require, exports, react_1, jquery_1, NamingIds_1) {
    "use strict";
    Object.defineProperty(exports, "__esModule", { value: true });
    react_1 = __importDefault(react_1);
    jquery_1 = __importDefault(jquery_1);
    NamingIds_1 = __importDefault(NamingIds_1);
    function NIDStr(properties) {
        let [state, setState] = react_1.default.useState({
            FirstRun: true,
            Lng: "CNT",
            NC: {
                CN: "noneCN",
                CNT: "noneCNT",
                DE: "noneDE",
                EN: "nonEN",
                ES: "nonES",
                NIDstr: "0000",
                PL: "nonPL"
            }
        });
        function getStringFromNamingContainer(lng) {
            if (lng === (0, NamingIds_1.default)().MKPRG.Naming.CultureNeutral) {
                return state.NC.CNT;
            }
            else if (lng === (0, NamingIds_1.default)().MKPRG.Naming.English) {
                return state.NC.EN;
            }
            else if (lng === (0, NamingIds_1.default)().MKPRG.Naming.German) {
                return state.NC.DE;
            }
            else if (lng === (0, NamingIds_1.default)().MKPRG.Naming.Spanish) {
                return state.NC.ES;
            }
            else if (lng === (0, NamingIds_1.default)().MKPRG.Naming.Chinese) {
                return state.NC.EN;
            }
            else if (lng === (0, NamingIds_1.default)().MKPRG.Naming.Polish) {
                return state.NC.PL;
            }
        }
        function LoadNCfromServer() {
            if (state.FirstRun) {
                jquery_1.default.ajax(`${properties.ServerOrigin}/NamingContainers?NC=${properties.nid}`, { method: "GET" })
                    .done((data, textStatus, jqXhr) => {
                    let _ncList = data;
                    // Zustand der React- Komponente neu setzten und rendern
                    setState({
                        FirstRun: false,
                        Lng: properties.lng,
                        NC: _ncList[0]
                    });
                })
                    .fail((jqXHR, textStatus, errorThrown) => {
                    let errTxt = `HTTP Status:${textStatus}, ${errorThrown}`;
                    // Zustand der React- Komponente neu setzten und rendern
                    setState({
                        FirstRun: false,
                        Lng: properties.lng,
                        NC: {
                            CN: errTxt,
                            CNT: errTxt,
                            DE: errTxt,
                            EN: errTxt,
                            ES: errTxt,
                            NIDstr: "0000",
                            PL: errTxt
                        }
                    });
                });
            }
        }
        // Hier wird das Laden des zugeh�rigen Naming- Container initiert
        react_1.default.useEffect(() => LoadNCfromServer(), []);
        return (react_1.default.createElement("span", { className: properties.cssClass, "data-nid": properties.nid }, getStringFromNamingContainer(properties.lng)));
    }
    exports.default = NIDStr;
});
//# sourceMappingURL=NIDStr.js.map