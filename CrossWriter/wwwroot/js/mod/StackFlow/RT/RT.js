// mko, 04.10.2024
//
// ᚱᛠ: Darstellung rationaler Zahlen in Stack ᛝ Flow
define(["require", "exports"], function (require, exports) {
    "use strict";
    Object.defineProperty(exports, "__esModule", { value: true });
    exports.mulRT = exports.addRT = exports.newRT = void 0;
    // Symbolische Konstanten für Positionen der RT Partikel in einem Array
    var P;
    (function (P) {
        P[P["M"] = 0] = "M";
        P[P["N"] = 1] = "N";
        P[P["D"] = 2] = "D";
        P[P["X"] = 3] = "X";
    })(P || (P = {}));
    ;
    // Implementierung eines RT
    class _RT {
        constructor(M, N, D, X) {
            this._parts = [M, N, D, X];
        }
        M() {
            return this._parts[P.M];
        }
        N() {
            return this._parts[P.N];
        }
        D() {
            return this._parts[P.D];
        }
        X() {
            return this._parts[P.X];
        }
    }
    function newRT(
    // Main: ganzzahliger Teil
    M, 
    // Nominator: Zähler des gebrochnenen Anteils
    N, 
    // Denominator: Nenner des gebrochnenen Anteils
    D, 
    // Exponentialfactor
    X) {
        return new _RT(M, N, D, X);
    }
    exports.newRT = newRT;
    // Addition von ᚱa und  ᚱb
    function addRT(a, b) {
        return newRT(a.M() * a.X() + b.M() * b.X(), a.N() * a.X() * b.D() + b.N() * b.X() * a.D(), a.N() * b.N(), 1n);
    }
    exports.addRT = addRT;
    // Multiplikation von ᚱa und ᚱb
    function mulRT(a, b) {
        return newRT(a.M() * b.M(), a.M() * a.N() * b.D() + a.M() * b.N() * a.D() + a.N() * b.N(), a.D() * b.D(), a.X() * b.X());
    }
    exports.mulRT = mulRT;
});
//# sourceMappingURL=RT.js.map