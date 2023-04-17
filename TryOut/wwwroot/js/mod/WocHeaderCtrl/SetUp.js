var __importDefault = (this && this.__importDefault) || function (mod) {
    return (mod && mod.__esModule) ? mod : { "default": mod };
};
define(["require", "exports", "jquery"], function (require, exports, jquery_1) {
    "use strict";
    Object.defineProperty(exports, "__esModule", { value: true });
    jquery_1 = __importDefault(jquery_1);
    // mko, 4.4.2023
    // Installiert ein JQuery- Plugin für das editieren von WocId's
    function WocHeaderCtrlSetUp(urlTSRoot, stackOps, stackElemStructs) {
        // JQuery Collection Plugin.
        // Die hier definierte Funktion wird für jeden selektierten Knoten aufgerufen.
        // Jeder selektierte Knoten wird durch **this** repräsentiert. Durch $(this) wird der 
        // Knoten von einem JQuery- Objekt umhüllt. Es können dann alle JQuery- Funktionen auf diesem ausgeführt werden
        // An den selektierten Knoten wird ein WocIdEdit- Fenster eingefügt.
        // 
        jquery_1.default.fn.extend({
            WocHeaderCtrl: function (options) {
                options = jquery_1.default.extend({ llpStack: stackOps.NewStack() }, options || {});
                const c_wocHeaderCtrlId = "#WocHeaderCtrl";
                // Prüfen, ob die EditWocId- Struktur bereits in den Kindfenstern eingefügt wurde
                let searchRes = (0, jquery_1.default)(this).find(c_wocHeaderCtrlId);
                if (searchRes.length === 0) {
                    let that = this;
                    (0, jquery_1.default)(document).ajaxComplete(function (event, jqXHR, ajaxSettings) {
                        // Zugriff auf das Ctrl
                        let wocHeaderCtrl = (0, jquery_1.default)(that).find(c_wocHeaderCtrlId).first();
                        // Elemente ausuchen, an die Eventhandler zu binden sind
                        // Button mit dem 
                        let btnDef = (0, jquery_1.default)(wocHeaderCtrl).find("button").first();
                        (0, jquery_1.default)(btnDef).text("Testinhalt");
                        (0, jquery_1.default)(btnDef).click(function () {
                            // Validates the Input fields 
                            // Link to the input elements
                            let inTitle = (0, jquery_1.default)(wocHeaderCtrl).find("#WocHeaderCtrl-input-title").first();
                            let inTitleErrMsg = (0, jquery_1.default)(wocHeaderCtrl).find("#WocHeaderCtrlInputTitleValidate").first();
                            let inAuthor = (0, jquery_1.default)(wocHeaderCtrl).find("#WocHeaderCtrl-input-author").first();
                            let inAuthorErrMsg = (0, jquery_1.default)(wocHeaderCtrl).find("#WocHeaderCtrl-input-author-validate").first();
                            (0, jquery_1.default)(inTitleErrMsg).html("");
                            (0, jquery_1.default)(inAuthorErrMsg).html("");
                            if (!(0, jquery_1.default)(inTitle).val()) {
                                // Title was not defined
                                (0, jquery_1.default)(inTitleErrMsg).html("↯ title is not defined");
                            }
                            else if (!(0, jquery_1.default)(inAuthor).val()) {
                                // Author is not defined
                                (0, jquery_1.default)(inAuthorErrMsg).html("↯ author is not defined");
                            }
                            else {
                                // Create the initial Stack with the woc header
                                let wocHeader = stackElemStructs.CreateFuncDue("WocHeader", stackElemStructs.CreateFuncUno("WocTitle", stackElemStructs.CreateStrToken((0, jquery_1.default)(inTitle).val())), stackElemStructs.CreateFuncUno("WocAuthor", stackElemStructs.CreateStrToken((0, jquery_1.default)(inAuthor).val())));
                                let myOptions = options;
                                stackOps.Push(myOptions.llpStack, wocHeader);
                                // Remove the the control, after the woc header was successful defined
                                (0, jquery_1.default)(wocHeaderCtrl).remove();
                            }
                        });
                    }).ajaxError(function (event, jqXHR, ajaxSettings, thrownError) {
                        alert("Error");
                    });
                    // Ctrl ist noch nicht definiert- wird geladen und in das DOM integriert
                    (0, jquery_1.default)(this).load(`${urlTSRoot}/WocHeaderCtrl/View.htm`);
                }
                return this;
            }
        });
    }
    exports.default = WocHeaderCtrlSetUp;
});
//# sourceMappingURL=SetUp.js.map