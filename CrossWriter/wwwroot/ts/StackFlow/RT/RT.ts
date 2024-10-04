// mko, 04.10.2024
//
// ᚱᛠ: Darstellung rationaler Zahlen in Stack ᛝ Flow

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
    return newRT(a.M()*a.X()+b.M()*b.X(), a.N()*a.X()*b.D() + b.N()*b.X()*a.D(), a.N()*b.N(), 1n)
}

// Multiplikation von ᚱa und ᚱb
export function mulRT(a: IRt, b: IRt): IRt {
    return newRT(a.M()*b.M(), a.M()*a.N()*b.D()+a.M()*b.N()*a.D()+a.N()*b.N(), a.D()*b.D(), a.X()*b.X());
}



