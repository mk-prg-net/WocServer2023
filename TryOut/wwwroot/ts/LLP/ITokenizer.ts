// mko, 1.4.2023
//

import IToken from "./IToken"

// Schnittstelle eines Tokenizers, der Texte in der der Reverse Polish Notation (RPN), in die Grundbausteine
// wie Konstanten und Funktionsnamen auflöst.
// Die Tokens werden als IToken- Objekte zurückgegeben.
export default interface ITokenizer {
    /// <summary>
    /// Liest das nächte Token ein
    /// </summary>
    Read(): boolean;

    /// <summary>
    /// Liefert true, wenn kein weiteres Token mehr eingelesen werden kann
    /// </summary>
    EOF(): boolean;

    /// <summary>
    /// Liefert das zuletzt eingelesene Token
    /// </summary>
    Token(): IToken;

}
