// mko, 29.3.2023
// Liste der Operatorsymbole
export default interface IOpSym
{
    // Kennzeichnet das Default/Empty/None Token
    rpnNoneToken: string;

    // Kennzeichnet eine NamensId in Form eines CNT oder NID Hexwertes
    rpnNidPrefix: string;

    // Kennzeichnet einen boolean in RPN, z.B. true #b
    rpnBoolType: string;

    // Kennzeichnet einen Integer in RPN, z.B. 99 #i
    rpnIntType: string;

    // Kennzeichnet einen double in RPN, z.B. 3.14 #n
    rpnNumType: string;

    // Kennzeichnet einen String in RPN, z.B. #$ Hallo Welt #.
    rpnStrType: string;

    rpnArrayPrefix: string;

    // Kennzeichnet einen Listenbegin in RPN, z.B. #_
    rpnListStart: string;

    // Kennzeichnet ein Listenende in RPN, z.B. #.
    rpnListEnd: string;


    // Kennzeichnet ganz allgemein ein Funktionsprefix, z.B. #
    rpnFuncPrefix: string; 

    // Markiert eine Token für einen Funktionskopf
    rpnFuncHeadPrefix: string;

    // Markiert einen Return- Wert.
    rpnReturnPrefix: string;

    // Markiert eine Instanz
    rpnInstancePrefix: string;

    // Markiert eine Property oder einen Funktionsparameter
    rpnPropPrefix: string;

    // Kennzeicnet ganz allgemein einen Kommentar, z.B. #/ Ein Kommentar #.
    rpnComment: string

    rpnSingleLineComment: string
}