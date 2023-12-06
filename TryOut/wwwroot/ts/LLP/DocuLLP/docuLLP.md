# 𝓛𝓛𝓟: Łukasiewicz List Processor

**LLP** soll eine minimalistische formale Sprache zur semantischen Auszeichung von Texten, zur funktionalen Formulierung von Algorithmen und zur generatorischen Beschreibung von Diagrammen, Bildern und Fräskopfbahnen werden.

## Grundlagen

### Runen als Präfix

Alle für den Parser unterscheidbaren Strukturen erhalten ein Präfix in Form einer nordischen **Rune**. 

Die *Runen* werden in keiner heute mehr existierenden Sprache gennutzt. Damit sind die Präfixe, durch die Sparachstrukturen kenntlich werden, eindeutig von Textdaten unterscheidbar. 

### Kommentare ᛭
**᛭** schließt den Rest vom Parsen aus. Damit können nach **᛭** beliebige Kommentare notiert werden.

## Literale elementarer Datentypen

### Präfixe für die Notation von Zahlenwerten
Eine Gleitpunktzahl wie **3.14** ist eine kulturspezifische Notation (**en-US**). 

Um die Notation von Zahlenwert von einer textuellen und kulturspezifischen Präsentation in einer Sprache zu unterscheiden, werden diese in **LLP** stets durch ein spezielles *Präfix* explizit gekennzeichnet.

🚨 Zahlen  können wie z.B. `ᚱ _Zähler_ _Nenner_` eine listenartige Struktur darstellen, sind aber keine Listen. Die einzelnen Partikel wie im Beispiel `_Zähler_` und `_Nenner_` dürfen nur Konstanten sein, wie `ᚱ 1 2`, jedoch keine Ausdrücke!

### Nummerische Datentpen
Die Notationsformen für Zahlenwerte haben Beschränkungen bezüglich der Genauigkeit. Deshalb korrespondieren die Notationsformen auch mit Teilmengen von **ℚ**. Diese Teilmengen Werden *Nummerische Datentypen* genannt. 

Die nummerischen Datentypen werden durch Kombination des speziellen Präfixes für eine Notation (z.B. **ᛕ**) mit dem allgemeinen Datentyp- Schalter **ᛠ** verbunden zum Datentyp Symbol **ᛕᛠ**.

**ᛠ** schaltet allgemein die Evaluierung einer Liste in die Evaluierung einer Typdeklaration um.

**ᛠ** alleine steht für jeden beliebigen Datentyp.

### Kardinalzahlen ᛕ

**ᛕ** ist das Präfix für ganze Zahlen:
```
ᛕ 1         ⟺ 1
ᛕ -123      ⟺ -123
ᛕ 16 AFD    ⟺ nat. Zahl zur Basis 16 (hex)
ᛕ  2 L00LLL ⟺ nat. Zahl zur Basis  2 (dual)  
ᛕ ᛞ         ⟺ + Unendlich
ᛕ -ᛞ        ⟺ - Unendlich
```
**ᛕᛠ** ist der Datentyp für Kardinalzahlen.

### Gebrochen Rationale Zahlen ᚱ

**ᚱ** ist das Präfix für gebrochen rationale Zahlen. Diese bestehen aus einem *Nenner* und einem *Zähler*, getrennt durch ein Leerzeichen: 

1. `ᚱ _Zähler_` hier ist der Nenner stets 1
2. `ᚱ _Zähler_ _Nenner_`
3. `ᚱ _Ganzzahlig_ _Zähler_ _Nenner_`

Beispiele:
```
ᚱ 2     ⟺ 2/1 = 2.0
ᚱ 1 2   ⟺ 1/2 = 0.5
ᚱ 1 2 3 ⟺ 1 2/3 = 1.666
ᚱ -4 16 ⟺ -4/16 = -0.25
```
Die rationalen Zahlen können z.B. als Zoll- Maße genutzt werden

**ᚱᛠ** ist der Datentyp für gebrochen rationale Zahlen.

### Gleitpunktzahlen ᚪ

**ᚩ** ist das Präfix für rationale Zahlen in der Gleitpunkt- Darstellung. Vor- und Nachkomma- Stellen bilden die beiden Elemente einer Liste. Kulturspezikfische Spearatoren wie **,** oder **.** sind damit überwunden.

```
ᚩ 3       ⟺  3.0
ᚩ 3 14    ⟺  3.14
ᚩ -2 72   ⟺ -2.72
ᚩ -2 72 3 ⟺ -2.72e3 = -2720 
```

**ᚩᛠ** ist der Datentyp für Gleitpunkt- Zahlen.

Die Datentypen **ᚱᛠ** und **ᚩᛠ** sind kompatibel bzw. austauschbar: Ein **ᚱᛠ** kann an ein **ᚩᛠ** zugewiesen werden und umgekehrt.

### Boolsche Werte ᛒ

**ᛒ** ist das Präfix für boolsche Werte. Die beiden möglichen boolschen Werte werden durch die Namen **true** und **false** ausgedrückt:
```
ᛒ true  ⟺ True
ᛒ false ⟺ False
```
**ᛒᛠ** ist der Datentyp für boolsche Werte.

### Namensreferenzen ᚻ

**ᚻ** ist das Präfix für eine *NamingID*. Eine *NamingID* ist ein eindeutiger Schlüssel zu Identifizierung eines Namenscontainers.

Beispiele:

`ᚻ milProgramm` ⟺ Referenz auf den Namenscontainer, der für Fräsenprogramme steht.

**ᚻᛠ** ist der Datentyp für Namensreferenzen.

### Hierarchieen ᚠ

`ᚠ`ist das Präfix eines Pfades in einer Hierarchie. Der Pfad muß durch ein Listenendsymbol `ᛩ` abgeschlossen werden.

`ᚠ ᛕ23 ᛕ10 ᛕ15 ᛩ` ⟺ Kann z.B. eine Versionsnummer mit den drei Hierarchieebnen *Hauptversion*, *Nebenversion*, *Buildnummer* darstellen. Oder die Uhrzeit **23:10:15**. Oder das Datum **15.10.2023**.

`ᚠ ᚻ millingMachine ᚻ circelMilling ᚻ millDisc ᛩ` ⟺ Pfad in einem Namensraum

**ᚠᛠ** ist der Datentyp für Hierarchieen.

## Abstraktion durch benennen von Werten mittels ᛟ Operator

Werte können an einen *Namen* mittels dem **Bind** Operator **ᛟ** gebunden werden. Über diesen Namen wird der Wert dann referenziert und abgerufen.

`ᛟ _NameAlsString_ _Wert_` bindet den Wert an einen Namen, der nur im Kontext der aktuellen 𝓛𝓛𝓟 Datei eindeutig ist.

`ᛟ _MonikerForNamingIdAsString_ ᚻ _NamingID_` bindet lokal in der 𝓛𝓛𝓟 Datei einen Namen (Moniker)  an eine *NamingId*. Die *Naming* ID ist dabei ein 64bit Wert, der für einen global gültigen Namen steht (Namenskontainer).

```
᛭ Konstante PI definieren
ᛟ PI ᚩ 3 14 

᛭ Den lokal gültigen Namen PI an eine global gültige Naming ID binden.
ᛟ PI ᚻ ᛕ 16 7ABC123

᛭ Liste der ersten fünf Primzahlen an einen Namen binden
ᛟ ersteFünfPrimzahlen ᚤᛕ2 ᛕ3 ᛕ5 ᛕ7 ᛕ11 ᛩ
```
Die Bindung eines Namens an einen Wert kann auch als **Attribut Wertepaar** betrachtet werden!

### Zugriff Auf den Werte, die an Namen gebunden sind mittels ᛟᛡ

Wurde an einen Namen ein Wert gebunden, dann kann überall, wo normalerweise der Wert eingesetzt wird, der Name eingesetzt werden, dem aber der **Replace Name by Value** Operator `ᛟᛡ` vorangesetzt werden muss:

```
᛭ Konstante PI definieren
ᛟ PI ᚪ 3 14 

᛭ Den Wert von **PI** an den synonymen Namen **pie** binden
ᛟ pie ᛟᛡ PI

᛭ Den Wert der globalen mit Naming ID definierten Konstante **PI** an den synonymen Namen **piee** binden

ᛟ pie ᛟᛡ ᚻ ᛕ 16 7ABC123I

```

### Namensraum- Listen ᛟ ... ᚹ ... ᛩ

Eine Menge von *Bind* Operationen können in Listen zusammengefasst werden. Innerhalb einer solchen Liste darf ein bestimmter Name stets nur einmal an einen Wert gebunden werden.

```
᛭ Beschreibung einer Punktkoordinate durch eine Liste aus Namensbindungen
ᚹ ᛟx ᚪ2 72 ᛟy ᚪ3 14 ᛩ 
```
Die Liste kann selber mittels Bind an einen Namen gebunden. So entsteht ein *Namensraum* oder eine benannte Struktur:

```
᛭ Namensraum mathematischer Konstanten
ᛟMathConst
ᚹ
    ᛟPI ᚪ3 14
    ᛟe  ᚪ2 72
ᛩ

᛭ Benannte Datenstruktur, die einen Punkt darstellt
ᛟPunkt1 
ᚹ 
    ᛟx ᚪ2 72 
    ᛟy ᚪ3 14 
ᛩ 
```

Für den Zugriff auf die Werte in der benannten Liste kann wieder mittels **Replace by** Operator **ᛟᛡ** benutzt werden. In diesem Fall sind die Namen jedoch als Hierarchie anzugeben: `ᛟᛡ ᚠ _NameListe_ _NameAttribut_ ᛩ`

```
᛭ Organisation einer Mathematischen Bibliothek
ᛟMath
ᚹ
    ᛟConst
    ᚹ
        ᛟPI ᚪ3 14
        ᛟe  ᚪ2 72
    ᛩ

    ᛟBasicFunctions
    ᚹ
        ᛭ Naming- IDs der math. Grundrechenarten werden an lokale Namen gebunden
        ᛟadd ᛟᛡ ᚻ ᛕ 16 ADDADD
        ᛟsub ᛟᛡ ᚻ ᛕ 16 DE2323
    ᛩ
ᛩ

᛭ Zugriff auf PI
ᛟᛡ ᚠ Math Const PI ᛩ
```
### Semantische Referenzen zwischen Namensraum- Listen

Sei **milDiscCircularCenterOfDiskX** ein Namenscontainer, der die X- Koordinaten des Mittelpunktes einer auszufräsenden Kreisscheibe beschreibt. Dieser stehe mit anderen Namenscontainern in folgenden semantischen Beziehungen:

```
                                                            **circleGeoParameter**
                                                                        A
                                                                        | 
                                                                     isPartOf
                                                                        |
 **milProgramm**                                                **centerOfCircle**
     A                                                                  A
     |                                                                  |
  isInstanceOf                                                       isPartOf
     |                                                                  | 
**milDiskCircular**                                             **centerXOfCircle**
     A                                                                  A
     |                                                                  |
     +---isPartOf-- **milDiscCircularCenterOfDiskX** --- isSubTermOf ---+ 
```

Die semantischen Beziehungen werden durch den ternären Operator **ᛯ** dargestellt:

`ᛯ _NID_Referring_ _NID_SemRefName_ _NID_Related_` 

```
ᛯ milDiscCircular isInstanceOf milProgram
       |               |          |
    referring       sem Rel     referred

```

#### Abfragen der semantisch referenzierten Instanz

```
ᛯᛏ _NID_Referring_ _NID_SemRefName_ 

```

Beispiel: Bestimmen, mit wem alles **milDiscCircular** in der semantischen Beziehung **isInstanceOf** steht:

```
ᛯᛏ ᚻ milDiscCircular ᚻ isInstanceOf 
```

#### Abfragen der semantisch referenzierenden Instanzen

```
ᛯᛏ _NID_SemRefName_ _NID_Related_
```

Beispiel: Bestimmen der Instanzen von **milProgram**:

```
ᛯᛏ isInstanceOf milProgram
```
### Strings ᛇ

*Strings* sind Listen aus beliebigen Zeichen. Sie können auch Leerzeichen enthalten.

*Strings*, die keine Leerzeichen enthalten, können direkt notiert werden.

```
᛭ geschlossener String, enthält keine Leerzeichen
Hallo

᛭ geschlossener Strings, die einzelne Hierarchieebenen benennen
ᚠ All Galaxieen Andromeda ᛩ 
```
Enthalten *Strings* Leerzeichen, dann müssen sie in ein **S-Array**: `ᛇ ... ᛩ`   gesetzt werden. **ᛇ** ist das Präfix für String- Listen.

Die Leerzeichen sind innerhalb eines String- Array geschützt.  

Sehr Lange Strings können mittels **ᛢ** auf mehrere Zeilen umgebrochen werden.
```
᛭ String aus mehreren Wörtern. Die Leerzeichen sind geschützt
ᛇHallo    Weltᛩ

᛭ Komplexe Texte als String, umgebrochen auf mehrere Zeilen mittels ᛢ
ᛇ Mit Strings können auch **MarkDown** formatierte Texte geschrieben werden.ᛢ
So wird *Text* und *Logik* vollständig vermischt.ᛩ
```
**ᛇᛠ** ist der Datentyp für Strings.

#### Zugriff auf Teilstrings

Auf Teile einer Zeichenkette kann mittels 


#### Stringinterpolation

Werden in einem String Namensreferenzen eingesetzt, die beim Abruf des Strings evaluiert werden, dann liegt eine Stringinterpolation vor.

Sei **ᛟ attrib schöne** eine Namensbindung. Dann kann eine Stringinterpolation wie folgt definiert werden:

**ᛇ Hallo *ᛟᛡ attrib* Welt ᛩ** 

Diese wird dann evaluiert zu:

**ᛇ Hallo schöne Welt ᛩ** 

### Arrays ᚤ

*Arrays* sind Listen von Werten. Die Werte können primitiv oder komplex sein.

**ᚤ** ist das Präfix, welches die Liste eines Arrays eröffnet. **ᛩ** beendet die Liste. 

``` 
᛭ Array mit den ersten fünf Primzahlen
ᚤ ᛕ2 ᛕ3 ᛕ5 ᛕ7 ᛕ11 ᛩ

᛭ Array mit zwei Koordinaten
ᚤ 
   ᚹ ᛟx ᚪ2 72 ᛟy ᚪ3 14 ᛩ 
   ᚹ ᛟx ᚪ5 3  ᛟy ᚪ1 7ᛩ ᛩ
ᛩ

᛭ Array aus Daten verschiedener Typen
ᚤ    
   ᚹ ᛟx ᛕ2 ᛟy ᛕ3 ᛩ 
   ᛕ13
   ᛇ Summe aus a² und b² ᛩ
ᛩ
```

**ᚤᛠ ᚻred ᚻgreen ᚻblue ᛩ** steht für einen Aufzählungstyp/Set: Eingesetzt werden dürfen nur die im Array aufgelistete Werte.

#### Zugriff auf Array Elemente

Auf einzelne Elemente eines Arrays kann mittels Operator `ᚤᛏ _array_ _index_ ᛋ _Ergebnis_` zugegriffen werden.

Dieser hat als Parameter den **0** basierte Index und das *Array*, aus dem der Wert zu entnehmen ist.

Soll im Falle eines Zugriffs auf ein nicht vorhandenes Element durch einen zu kleinen, oder zu großen Index keine Ausnahme, sondern eine benutzerdefinierte Fehlerbehandlung starten, dann ist  `ᚤᛏ _array_ _index_ ᛊ _errIndexOutOfRangeHandler_ ᛋ _Ergebnis_` einzusetzen.

Beispiele (hier enhalten Array selber wieder Arrays)

```
᛭ Array mit Elemente, die selber Arrays sind
ᛟa1
ᚤ    
   ᚤ ᛕ1 ᛕ2 ᛩ 
   ᚤ ᛕ3 ᛕ4 ᛩ 
ᛩ

᛭ Hier gilt: ᛟe1 == ᚤ ᛕ1 ᛕ2 ᛩ
ᚤᛏ ᛟa1 1 ᛋ ᛟe1

᛭ Hier gilt: ᛟe2 == ᚤ ᛕ3 ᛕ4 ᛩ
ᚤᛏ ᛟa1 2 ᛋ ᛟe2
```

#### Einbetten von Array in Array mittels Expand ᚷ Operator

Mittels des Expand- Operator **ᚷ** kann der Inhalt eines Array in ein anderes eingebettet werden

```
᛭ Array mit Elemente, die selber Arrays sind
ᛟa2
ᚤ    
   ᛕ1
   ᚷᚤ ᛕ2 ᛕ3 ᛩ 
   ᛕ4 
ᛩ

᛭ Hier gilt: ᛟe1 == ᛕ1
ᚤᛏ ᛟa2 1 ᛋ ᛟe1

᛭ Hier gilt: ᛟe2 == ᛕ2
ᚤᛏ ᛟa1 2 ᛋ ᛟe2
```

#### Häufig benutzte Array- Operationen
Im folgenden werden Operationen auf Array beschreiben, die häufig in **LLP** einzusetzen sind.

##### Pop

`ᛖpop ᚤ a b ... ᛩ` entnimmt das erste Element von Links aus dem Array.

```
ᛟar1 ᚤ a b ᛩ

ᛖᛏpop ᛟᛡar1 ᛩ
ᛋ ᛟres             ᛭ ᛟᛡres == a
ᛗ ᛖᛏlog ᚥ          ᛭ loggt ᚤ b ᛩ

ᛖᛏpus ᛟᛡar1 ᛩ 
ᛗ ᛖᛏlog ᚥ          ᛭ loggt ᚤ b ᛩ


```


## Methoden ᛖ: Kommandos und Abfragen

Methoden sind ein Oberbegriff für den Zustand des Systems verändernde *Kommandos*, und *Abfragen* auf dem Systemzustand selbst:

```
                   Methoden ᛖ
                      |
       +--------------+-------------------+
       |              |                   | 
   Kommandos ᛈ   finale Kommandos ᛰ    Abfragen ᚢ
``` 
Die Kommandos können über eine Parameterliste parametriert werden. 

### Paramter

Jede Methode kann parameteiert werden. Zur Parametrierung wird der *Stapelspeicher* des *Laufzeitsystems* explizit angesprochen wie ein Array über das Symbol **ᚥ**.

Der Methode kann eine Liste von Parametern folgen. Dise werden von rechts nach links auf den Stapelspeicher gelegt.
```
             --+---+--+
ᛖm a b c ᛩ   a | b | c|
             --+---+--+
             ------------+
ᛋ             m(a, b, c) |
             ------------+
```
### Zweige
Eine Methode verarbeitet die übergebenen Parameter. Danach gibt es drei Möglichkeiten der Programmfortsetzung:

1. Es wird im **ᛋ Zweig** fortgesetzt
2. Es wird im **ᛊ Zweig** fortgesetzt
3. Es wird sofort zum Methodenausgang **ᛗ** gesprungen und damit die Methode formal beendet

In den Fällen 1 und 2 wird nach Durchlauf der Zweige ebenfalls der Methodenoaufruf am Ausgang **ᛗ** abgeschlossen.

Beispiele:

```
᛭ Wurzel aus einer Zahl a ziehen
ᛖ SQRT a
ᛋ _op_auf_√a_           ᛭ Hier wird die √ von a bereitgestellt
ᛊ _Fehlerbehandlung_    ᛭ 
ᛗ _Abschlussfunktion_
```

### Bereitstellung der Ergebnisse

```
             --+---+--+
ᛖ m a b c ᛩ  a | b | c| -----+----+
             --+---+--+      |    |
             ------------+   |    |
 +---  ᛋ      m(a, b, c) | <-+    | 
 |           ------------+        |
 |           ------------+        |
 | +-- ᛊ      m(a, b, c) | <------+
 | |         ------------+             
 | |  
 | |         ----------------------------------+
 +-+-> ᛗ      s(m(a, b, c)) oder e(m(a, b, c)) |
             ----------------------------------+   
```

### Weiterverarbeitung der Ergebnisse

#### Abrufen des Ergebnis- Stapelspeichers als Array ᚥ 

Der gesamte Stapelspeicher kann in einem  **ᛋ**, **ᛊ** und **ᛗ** Zweige als das spezielle Array **ᚥ** abgegriffen werden. Mittels **ᚥᛏ** Zugriffsoperator können einzelne Elemente herausgegriffen und gezielt weiterbearbeitet werden.
**ᚥᛏ** hat folgende Signatur:

```
ᚥᛏ _Index1_ [_index2 [ ... [index n]]]
ᛋ _meth_für_Zweig1_   ᛭ Methode, die auf den Wert mit Index 1 aus ᚥ angewendet wird
ᛋ _meth_für_Zweig2_   ᛭ Methode, die auf den Wert mit Index 2 aus ᚥ angewendet wird
:
ᛋ _meth_für_ZweigN_   ᛭ Methode, die auf den Wert mit Index N aus ᚥ angewendet wird
ᛊ _meth_für_einen_outOfRange_Fehler_
```

Damit kann eine Liste von Werten bereitstellen (z.B. einen Vektor)


### Weiterleiten der Ergebnisse auf dem Stapelspeicher an eine Folgemethode


### Datenflussgraphen

In jedem Zweig wird das **Kontext- Array** **ᚥ** bereitgestellt, welches die Aufrufparameter der Methode + der Ergebnisse enthält. Das Kontextarray ist Vergleichbar mit dem Aufrufstapel.

Methoden müssen nicht alle Zweige implementieren.

Implementiert eine Methode den Zweig **ᛋ**, dann ist sie eine **Abfrage ᚢ**. 

Implementiert eine Methode den Zweig **ᛋ** nicht, dann ist sie eine **Kommandos ᛈ**. 

Wird weder **ᛊ** noch **ᛋ** implementiert, dann ist es ein finales **Kommando ᛰ**.

Jede Methode hat einen Ausgang **ᛗ**. Diese wird stets durchlaufen. Dadurch wird folgendes Grundprinzip der *strukturierten Programmierung* realisiert:

    Ein Block wird oben während des Programmflusses betreten und unten verlassen.

In den Zweigen **ᛋ** und **ᛊ** können beliebige Methoden aufgerufen werden. Im Ausgang **ᛗ** dürfen hingegen nur Kommandos aufgerufen werden.

#### Datenfluss Parameter- Array
Im Normalfall entnimmt eine Methode von links Werte aus einem Array und stellt rechts die Ergebnisse ein.
Beispiel:

```
Startwerte      (a, b)             
                  ↓                ᛭ In 𝓛𝓛𝓟  
1. Berechnung    [x²]              ᛖsqu ᚤ ᛕ2 ᛕ3 ᛩ 
                  ↓                ᛋ ᛖsqu ᚥ
                (a², b)              ᛋ ᛖadd ᚥ                
                  ↓                    ᛋ ᛟaabb                                    
2. Berechnung    [x²]                  ᛗ ᛖlog ᚥ      ᛭ loggt (a²+b²)
                  ↓                  ᛗ ᛖlog ᚥ        ᛭ loggt (a²+b²)
                (a², b²)           ᛗ ᛖlog ᚥ          ᛭ loggt (a²+b²)
                  ↓
3. Berechnung    [+]         
                  ↓
                (a²+b²)                  
```


```
  i1      i2           ᛭ Eingangsdaten
  ↓       ↓
  ᚢ1 ᛊ ⟶ ᚢ2 ᛊ ⟶ ᛰ3   ᛭ Finales Kommando ᛰ3 beendet Datenfluss
  ᛋ       ᛋ
  |       ↓  
  |       ᛰ4    	   ᛭ Finales Kommando ᛰ4 beendet Datenfluss 
  ↓
  ᚢ5 ᛊ ⟶ ᛰ6           ᛭ Finales Kommando ᛰ6 beendet Datenfluss  
  ᛋ                    ᛭ Ausgang mit Ergebnis von ᚢ5 ist Eingang von ᛈ7
  ↓
  ᛈ7 ᛊ ⟶ ᛰ8           ᛭ Finales Kommando ᛰ3 beendet Datenfluss
```

#### Präfixe für Methodenoperatoren

Operator      | Bedeutung
--------------|----------------------------
**ᛖ _name_**  | Präfix, Definition einer benannten Methode
**ᛖ᛫**         | Präfix, Definition einer anonymen Methode
**ᛖᛠ**        | Präfix einer Methodentypen- Signatur
**ᛖᛡ _name_** | Präfix einer Methodenreferenz
**ᛖᛏ _name_** | Präfix eines Aufrufes einer benannten Methode
**ᛖᛏ᛫**        | Präfix eines Aufrufes einer anonymen Methode

### Parameterlisten von Methoden

Die Mengen der möglichen Eingangsdaten/Parameter einer Methode werden durch die Parameterlisten definiert.

### Methodentypen ᛖᛠ

Wie bei den elementaren Datentypen können auch Methoden klassifiziert werden. Dabei ist der Aufbau der Parameterliste entscheidend. 

**ᛖᛠ** ist das Präfix für einen Methodentyp. Diesem folgt eine Liste von Methodenparameter- Typdeklarationen:

**ᛖᛠ ᛟ _paramName1_ _TypName1_ ... ᛟ _paramNameN_ _TypNameN_ ᛩ**

### Definition von Methoden

Eine Methodendefintion startet mit dem Präfix **ᛖ**, dem folgende Strukturen folgen:

1. Methodenname
2. Parameterliste
3. Ausgänge mit Methodentypen der einsetzbaren Folge- Methoden
4. Implementierende Sequenz von Operartionen

```
ᛖ _name_ ᛟ _paramName1_ _TypName1_ ... ᛟ _paramNameN_ _TypNameN_ ᛩ
ᛊᛠ ᛟ _paramName1_ _TypName1_ ... ᛟ _paramNameM_ _TypNameM_ ᛩ 
ᛋᛠ ᛟ _paramName1_ _TypName1_ ... ᛟ _paramNameM_ _TypNameP_ ᛩ 
ᛜ _Methodenaufruf_etc_ ᛭ 1. Schritt in der Sequenz
...
ᛜ _Methodenaufruf_etc_ ᛭ N. Schritt in der Sequenz
ᛜᛜ ᛭ Sequenzende
```

### Finales Kommando ᛰ

Ein **finales Kommando** ist eine parametrierbare Methode, die weder eine Fehlermeldung, noch ein Ergebnis zurückliefert. Es findet lediglich eine Änderung des Systemzustandes auf Basis der übergebenen Parameter statt.

Finale Kommandos haben das Präfix **ᛰ**

Beispiele für *finale Kommandos* sind z.B. das reguläre Programmende und  der vorzeitige Programmabbruch.

```
Parameter
↓  
ᛰ Finales Kommando

᛭ Finales Kommando in LLP aufrufen
ᛏᛰ namensReferenz ᛟparam1 wert1 ... ᛟparamN wertN  ᛩ

᛭ Konkretes Beispiel: Text auf der Log- Konsole ausgeben
ᛰᛏlogConsole ᛟtxt ᛒ Es wurden ᛟᛡcount Datensätze gelesenᛩ ᛩ

```
### Kommandos ᛈ

*Kommandos* haben das Präfix **ᛈ**, und verändern den Systemzustand (z.B. Insert- Operation in einer DB- Tabelle). Ein Ergebnis liefern sie nicht, können aber scheitern, und haben folglich einen Fehler- Handler **ᛊ**.

```
Parameter
↓  
ᛈ Command ᛊ⟶ Error Output  

᛭ Kommando in LLP aufrufen
ᛏᛈ namensReferenz ᛟparam1 wert1 ... ᛟparamN wertN  
ᛊ _Referenz_auf_Funktion_mit_Fehlerbehandlung_
ᛩ
```

### Abfragen ᚢ
*Abfragen* haben das Präfix **ᚢ**. Sie liefern Informationen über den aktuellen Systemzustand. Verändert wird der Systemzustand durch eine Abfrage explizit nicht.

Das Ergebnis einer Abfrage wird im Result- Output ausgegeben.

```
  Parameter
  ↓  
  ᚢ Query ᛊ⟶ Error Output  
  ᛋ 
  ↓
  Succeeded/Query Result Output
```

### Von der Laufzeitumgebung bereitgestellte Methoden

Die Laufzeitumgebung hat bereits eine Reihe von Methoden vordefiniert und Implementiert. Diese stammen aus folgenden Bereichen:

#### Datenstrom- orientierte Ausgabe

Wie in jeder Programmiersprache gibt es auch in LLP eine elementare Funktion zur Ausgabe von Daten in Datenströme: 

`ᛰout ᛟoStream _name output_Stream_  ᛟtxt ᛒ hier den auszugebenden Textᛩ ᛩ` 

##### Logs, Fehlerlogs

1. Fehlerlog `ᛰlogErr ᛟtxt ᛒ hier die logmeldungᛩ ᛩ`
2. Allgmeiner Log `ᛰlog ᛟtxt ᛒ hier die logmeldungᛩ ᛩ`
3. Grundrechenarten wie `ᚢadd ᛟa ᛕ1   ᛟb ᛕ2 ᛋ _Hier_Methode_referenzieren_die_das_Ergebnis_weiterverarbeitet_ ᛩ`
4. Basisfunktionen wie Potenzen, Wurzeln, 
5. grundlegende wissenschaftliche Funktionen wie Trigonometrische Fkt.
6. Zeichenketten- Funktionen wie Concatentation, String- Interpolation, Split, Trim, SubString

```
᛭᛭ a2 + b2

ᚢsqu ᛟx ᛕ99 ᛩ 
ᛋ ᛟxx

ᚢsqu ᛟx ᛕ77 ᛩ 
ᛋ ᛟyy

ᚢadd ᛟx ᛟᛡxx ᛟy ᛟᛡyy  ᛩ 
ᛋ ᛟsquSum

ᛰout ᛟconsole ᛒ Die Quadratsumme aus ᛕ99 und ᛕ77 ist ᛟᛡsquSum ᛩ

```


```
ᚢᛡsqu ᛟx ᛕ99 ᛩ 
ᛋ ᛖ᛫ ᛟxx ᛕᛠᛩ  
  ᛜ ᚢᛡsqu ᛟx ᛕ77 ᛩ 
    ᛋ ᛟyy
  ᛋ ᚢᛡadd ᛟx ᛟᛡxx ᛟy ᛟᛡyy  ᛩ 
    ᛋ ᛟsquSum

```


#### Parameter

`ᛜ` ist das Präfix für einen *Parameter*. *Parameter* bestehen im allgemeinen immer aus einem Parameternamen und einem Wert, der an den Parameter gebunden ist: `ᛜ _paramName_ _paramValue_`

Wird der _paramName_ durch eine *NamingID* definiert, dann kann mittels semantischer Referenzen im Namenscontainer der Datentyp eines Funktionsparameters implizit festgelegt werden.

Der Parameterwert kann direkt gesetzt, durch einen Funbktionsaufruf errechnet, aus einer Eigenschaft einer Instanz referenziert oder durch einen Platzhalter offen gehalten werden. Letzteres erfolgt, wenn die Funktion eine *Implementierung* für die Berechnung des Funktionswertes in dem *Return* Abschnitt enthält:

```
ᛖ ᚻadd 
    ᛜ A ᚪ3 14 
    ᛜ B ᚪ2 72
ᛩ

᛭᛭  Eine Instanz, die eine Punktkoordinate darstellt
ᛝ P1 
    ᛭᛭ die folgende semantische Beziehung hat den Charakter einer Typdeklaration
    ᛯ ᚻinstanceOf ᚻGeometricPoint

    ᛭᛭ Koordinaten des Punktes
    ᛜ ᚻpx ᚪ3.14 
    ᛜ ᚻpy ᚪ2.72
ᛩ    

᛭᛭  Addiert die Werte der Koordinaten von P1
ᛖ ᚻadd 
    ᛜ A ᛏ ᛝ P1 ᛜ ᚻpx
    ᛜ B ᛏ ᛝ P1 ᛜ ᚻpy
ᛩ

᛭᛭  Funtion mit einer Implementierung ᛣ

ᛖ radiusOfP 
    ᛜ ᚻpx ᛟ 
    ᛜ ᚻpx ᛟ
    ᛣ ᛏᛖ᛫ ᚻSQRT ᛏᛖ᛫᛫ ᚻadd
                    ᛏᛖ᛫SQU ᛏᛜ ᚻpx 
                    ᛏᛖ᛫SQU ᛏᛜ ᚻpy
ᛩ


``` 

Alternativ könnte man den Datentyp eines Parameters durch eine Default- Wert eines elementaren Datentypen festlegen: `ᛜ CX ᚪ_ ⟺ CX ist vom Typ Gleitkommazahl`


#### Vereinfachte Methoden/Funktionsdefinitionen


`ᛖ᛫` definiert explizit eine einstellige Funktion. Diese hat genau einen Parameter: `ᛖ᛫ _funktionsName_ _parameter1_`

`ᛖ᛫᛫` definiert explizit eine zweistellige Funktion. Diese hat genau zwei Parameter: ᛖ᛫᛫ _funktionsName_ _parameter1_ _parameter2_`

usw..

#### Platzhalter ᛟ für Parameterwerte

`ᛟ` ist ein Platzhalter, der anstelle eines Parameterwertes notiert werden kann.

#### Aufruf von Funktionen

Funktionen werden mit `ᛏ` (Return) aufgerufen, und liefert den Funktionswert. Nach dem CQR Pattern verändern Funktionen den inneren Zustand nicht.

Beispiele:

```
᛭᛭ Methode, die keinen Parameter hat (0 Stellig): Stoppt die Fräse
ᛖ ᚻmilStop ᛩ

᛭᛭ Funktion, die keinen Parameter hat (0 Stellig): liefert die aktuelle Position X
ᛏ ᛖ ᚻmilCurrentPosX ᛩ

᛭᛭ explizit zweistellige Funktion mit zwei PArametern: liefert die Summe der beiden Gleitpunktzahlen.
ᛏ ᛖ᛫᛫ ᚻadd ᚪ0.1 ᚪ1.3
```



### Instanzen

`ᛝ` ist das Prefix für eine *Instanz*. Eine Instanz beschreibt ein ein Objekt aus dem Weltausschnitt, der durch das **LLP** Programm modelliert wird. 
*Instanzen* beginnen stets mit einem Namen. Diesem schließt sich eine Auflistung von *Eigenschaften*, *Methoden* und *Funktionen*.

Die *Eigenschaften* definieren den *inneren Zustand* eines Objektes. Sie werden als Attribut- Wertpaare aufgeliste. 

*Methoden* ermöglichen das Ändern des *inneren Zustandes*. 

Beispiel: 


### Einen Namingcontainer referenzieren

Sei **milDiscCircular** ein Namenscontainer, der eine Familie von Fräsprogrammen benennt, die Kresischeiben aus einer flachen Platte fräsen. Dann kann dieser Namenskontainer wie folgt referenziert werden:

```
ᚻ milDiscCircular
```
Diese Referenz kann selber zur Benennung von LLP Strukturen wie Instanzen etc dienen.

### Einen Namenscontainer definieren

```
ᛖ ᚻdefNC

    ᛭᛭ Bennennung und Einordnung in Taxionomien

    ᛜ ᚻ nid 0x1234567890
    ᛜ ᚻ cnt milDiscCircular
    ᛜ ᚻ basicNamespace TechTerms.Milling.MilProgs

    ᛭᛭ Beschreibung in verschiedenen Sprachen

    ᛜ ᚻ lngDE 'Ein Fräsprogramm zum erstellen einer Kreisscheibe auf einer Platte'
    ᛜ ᚻ lngEN 'A milling Program for milling a circular disc.'

    ᛭᛭ Semantische Beziehungen definieren

    ᛯ ᚻ instanceOf ᚻ namingContainers
    ᛯ ᚻ instanceOf ᚻ milProgram
ᛩ
```

Die Methode erzeugt in der Laufzeitumgebung einen Instanz mit der **NID** 0x1234567890. 

### Zugriff auf die Eigenschaften einer Instanz, z.B. Namenscontainer

Auf die Eigenschaften von Instanzen oder Parameter von Methodenblöcken kann mittels *getter* zugegriffen werden. Achtung: Eigenschaftsnamen innerhalb von Methoden oder Funktionen sind stets eindeutig.

```
᛭᛭ Getter innerhalb eines Instanz oder Methodenblockes
ᚲ ᛜ _PropertyName_ ᛖ _ErrorHandlerIfAccessToPropFails_ 

᛭᛭ Getter, der eine Property einer Instanz oder Methode addressiert
ᚲ _InstanceOrMEthodName_ ᛜ _PropertyName_ ᛖ _ErrorHandlerIfAccessToPropFails_ 
```
`ᛖ _ErrorHandlerIfAccessToPropFails_` verweist auf eine Methode, die aufgerufen wird, wenn der Zugriff auf die Eigenschaft zur Laufzeit fehlschlägt. Z.B. weil die referenzierte Eigenschaft nicht existiert. In diesem Fall ist stets ein Default- Wert zurückzugeben, so dass der Ausdruck, in dem der Getter steht evaluiert werden kann.

Beispiele:
```    
᛭᛭ Liefert den Wert der Eigenschaft ᛜ ᚻ lngDE der Instanz milDiscCircular
ᚲ ᛝ milDiscCircular ᛜ ᚻ lngDE ᛖ ᚻ errHndLngDoesNotExists 

```


```
ᛝ 'Berechnung 1'
    ᛜ 'liste Primzahlen'    ᛥ ᛕ2 ᛕ3 ᛕ5 ᛕ7 ᛕ11 ᛩ
    ᛜ 'erste Primzahl'      ᛊ ᛕ1 ᚲ ᛜ 'liste Primzahlen' ᛖ ᚻ errPopDoesNotExistsHndl ᛖ ᚻ errOutOfRangeHndl

```

Ein weiteres Beispiel ist die Beschreibung eines Namenscontainers in einer Wunsch- Sprache abrufen:

```
ᛖ ᚻ ConsoleWriteLine ᛏ ᚲ ᛝ milDiscCircular ᛜ ᚻ lngDE ᛖ ᚻ errHndLngDoesNotExists 
```


### Semantische Referenzen ausdrücken

Sei **milDiscCircularCenterOfDiskX** ein Namenscontainer, der die X- Koordinaten des Mittelpunktes einer auszufräsenden Kreisscheibe beschreibt. Dieser stehe mit anderen Namenscontainern in folgenden semantischen Beziehungen:

```
                                                            **circleGeoParameter**
                                                                        A
                                                                        | 
                                                                     isPartOf
                                                                        |
 **milProgramm**                                                **centerOfCircle**
     A                                                                  A
     |                                                                  |
  isInstanceOf                                                       isPartOf
     |                                                                  | 
**milDiskCircular**                                             **centerXOfCircle**
     A                                                                  A
     |                                                                  |
     +---isPartOf-- **milDiscCircularCenterOfDiskX** --- isSubTermOf ---+ 
```

Die semantischen Beziehungen können z.B. durch Funktionsausdrücke dargestellt werden:

```
ᚪ ᚻ isInstanceOf
    ᛜ ᚻsemRefReferring ᚻ milDiscCircular
    ᛏ ᚻ milProgram
```

Eine Kurzform für diese Definition semantischer Referenzen ist sinnvoll. Sei ᛯ ein neues Präfix für semantische Referenzen. Dann kann eine semantische Referenz definiert werden durch:

`ᛯ _NID_Referring_ _NID_SemRefName_ _NID_Related_` 

Das ist die Kurzform für 

```
ᚪ _NID _SemRefName_
    ᛜ ᚻsemRefReferring _NID_Referring_
    ᛏ _NID_Related_
```
Damit kann das obige Beispiel vereinfacht werden zu:

```
ᛯ ᚻ milDiscCircular ᚻ isInstanceOf ᚻ milProgram

```

#### Abfragen der semantisch referenzierten Instanz

```
ᛯ ᛏ ᚻ _NID_Referring_ ᚻ _NID_SemRefName_ 

```

Beispiel: Bestimmen, mit wem alles **milDiscCircular** in der semantischen Beziehung **isInstanceOf** steht:

```
ᛯ ᛏ ᚻ milDiscCircular ᚻ isInstanceOf 
```

#### Abfragen der semantisch referenzierenden Instanzen

```
ᛯ ᛏ ᚻ _NID_SemRefName_ ᚻ _NID_Related_

```

Beispiel: Bestimmen der Instanzen von **milProgram**:

```
ᛯ ᛏ ᚻ isInstanceOf ᚻ milProgram
```

### Fräsprogramm für einen Kreis

```
ᛝ ᚻ milCirc    
   ᛜ ᚻ Cx ᛏ ᛖ᛫᛫ ᚻ measureDistance ᚪ 0.0 ᚻ mm
   ᛜ ᚻ Cy ᛏ ᛖ᛫᛫ ᚻ measureDistance ᚪ 0.0 ᚻ mm 
   ᛜ ᚻ Cr ᛏ ᛖ᛫᛫ ᚻ measureDistance ᚪ 100.0 ᚻ mm
   ᛖ ᚻ Next
    ᛜ r _ 
    ᛏ ᛝ ᚻ milCircNext
        ᛜ ᚻ milCircCx ᚲ ᛝ ᚻ milCirc ᛜ Cx
        ᛜ ᚻ milCircCy ᚲ ᛝ ᚻ milCirc ᛜ Cy
        ᛜ ᚻ milCircRadius  ᛏ ᛖ᛫᛫ ᚻ ADD ᚪ 0.5 ᚲ ᛜ r _ 
    
```

## Interaktives Parsen von LLP

Es ist ein Editor für LLP zu implementieren, der den Benutzer aktiv bei der Eingabe unterstützt. 

Nach jedem vollständig eingegeben Wort kann z.B. der Parser gestartet werden. 

Z.B. folgende Sitzung:

```
ᛯ _
```
Der Parser erkennt das Prefix für semantische Beziehungen. Nun kann die Definition oder die Abfrage einer semantischen Beziehung folgen. 

```
ᛯ ᛏ _
> [#1] ᛏ - semantische Beziehung abfragen
> [#2] ᚻ - semantische Beziehung definieren: _NID_Referring
```
Nachdem [#1] gewählt wurde, ist nun eine der möglichen semantischen Beziehungen auszuwählen

```
ᛯ ᛏ ᚻ isInstanceOf
> [#1] ᚻ isPartOfSemContext
> [#2] ᚻ isInstanceOf
> [#3] ᚻ isPartOf
> [#4] ᚻ isSubTermOf
> [#5] ᚻ isSubNamespace
```
Nachdem [#2] gewählt wurde, gibt es eine große Auswahl von Namenscontainern, die Klassennamen von Klassen darstellen, mit denen andere Namenscontainer in der Beziehung **isInstanceOf** stehen können. Hier gibt es verschiedene Strategieen, um den gesuchten Namensconteiner des Klassennamens zu finden:

1. Über den Namensraum- Pfad zur Naming ID des gesuchten Namenscontainers navigieren. 
    1. Es werden nur Namensraum- Pfade unterstützt, die Klassennamen enthalten
    2. Es werden alle Namensraumpfade unterstützt. In einem Namensraum werden nur Namnescontainer von Klassennamen angezeigt.
2. Über ein Autocomplete- Textcontrol, das nur die CNT's von Klassennamen unterstützt, den CNT auswählen lassen.
Sei Variante 2 der Standard- Modus. In Variante 1 kann bei Bedarf umgeschaltet werden:
```
ᛯ ᛏ ᚻ isInstanceOf ᚻ milProgram
> [#1] Select Naming- Containear with class name via Namespace
```

