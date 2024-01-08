// mko, 28.12.2023
// Definiert den Abschnitt in einem String, der als Zeile zu interpretieren ist.
// Der String selber stellt einen Text dar.

export default interface ITextLineOverlay{
    // Zeichposition im Text, ab dem die Zeile beginnt
    LineBegin: number;

    // Zeichenposition im Text, wo die Zeile endet
    LineEnd: number;

}