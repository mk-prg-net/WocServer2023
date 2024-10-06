// mko, 04.10.2024
//
// ᚱᛠ: Darstellung rationaler Zahlen in Stack ᛝ Flow

import { abs, mod } from 'mathjs';

// Rationale Zahl as Quadrupel
export interface IRt {

    // Main: ganzzahliger Teil
    M(): bigint;

    // Nominator: Zähler des gebrochnenen Anteils
    N(): bigint;

    // Denominator: Nenner des gebrochnenen Anteils
    D(): bigint;

    // Exponentialfaktor
    X(): bigint;
}

// Symbolische Konstanten für Positionen der RT Partikel in einem Array
enum P { M, N, D, X };

// Implementierung eines RT
class _RT implements IRt {

    constructor(M: bigint, N: bigint, D: bigint, X: bigint) {
        this._parts = [M, N, D, X];
    }    

    _parts: bigint[];

    M(): bigint {
        return this._parts[P.M];
    }

    N(): bigint {
        return this._parts[P.N];
    }

    D(): bigint {
        return this._parts[P.D];
    }

    X(): bigint {
        return this._parts[P.X];
    }
}

export function newRT(

    // Main: ganzzahliger Teil
    M: bigint,

    // Nominator: Zähler des gebrochnenen Anteils
    N: bigint,

    // Denominator: Nenner des gebrochnenen Anteils
    D: bigint,

    // Exponentialfactor
    X: bigint
): IRt
{
    return new _RT(M, N, D, X);
}

// Addition von ᚱa und  ᚱb
export function addRT(a: IRt, b: IRt): IRt {

    let sm = a.M() * a.X() + b.M() * b.X();
    let sn = b.D() * a.N() * a.X() + a.D() * b.N() * b.X();
    let sd = a.D() * b.D();

    return newRT(sm, sn, sd, 1n)
}

// Multiplikation von ᚱa und ᚱb
export function mulRT(a: IRt, b: IRt): IRt {

    let pm = a.M() * b.M();
    let pn = a.N() * b.M() * b.D() + a.M() * b.N() * a.D() + a.N() * b.N();
    let pd = a.D() * b.D();
    let px = a.X() * b.X();

    return newRT(pm, pn, pd, px);
}

// Größter gemeinsamer Teiler von a und b
export function GGT(a: bigint, b: bigint) {
    let h = 1n;
    if (a == 0n) return abs(b);
    if (b == 0n) return abs(a);

    do {
        h = mod(a, b);
        a = b;
        b = h;
    } while (b != 0n)

    return abs(a);
}



