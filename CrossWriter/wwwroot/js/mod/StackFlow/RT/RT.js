// mko, 04.10.2024
//
// ᚱᛠ: Darstellung rationaler Zahlen in Stack ᛝ Flow
define(["require", "exports", "mathjs"], function (require, exports, mathjs_1) {
    "use strict";
    Object.defineProperty(exports, "__esModule", { value: true });
    exports.GGT = exports.mulRT = exports.addRT = exports.newRT = void 0;
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
        let sm = a.M() * a.X() + b.M() * b.X();
        let sn = b.D() * a.N() * a.X() + a.D() * b.N() * b.X();
        let sd = a.D() * b.D();
        return newRT(sm, sn, sd, 1n);
    }
    exports.addRT = addRT;
    // Multiplikation von ᚱa und ᚱb
    function mulRT(a, b) {
        let pm = a.M() * b.M();
        let pn = a.N() * b.M() * b.D() + a.M() * b.N() * a.D() + a.N() * b.N();
        let pd = a.D() * b.D();
        let px = a.X() * b.X();
        return newRT(pm, pn, pd, px);
    }
    exports.mulRT = mulRT;
    // Größter gemeinsamer Teiler von a und b
    function GGT(a, b) {
        let h = 1n;
        if (a == 0n)
            return (0, mathjs_1.abs)(b);
        if (b == 0n)
            return (0, mathjs_1.abs)(a);
        do {
            h = (0, mathjs_1.mod)(a, b);
            a = b;
            b = h;
        } while (b != 0n);
        return (0, mathjs_1.abs)(a);
    }
    exports.GGT = GGT;
});
//# sourceMappingURL=RT.js.map