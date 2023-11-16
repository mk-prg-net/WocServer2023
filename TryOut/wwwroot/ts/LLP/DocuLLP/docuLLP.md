# 𝓛𝓛𝓟: Łukasiewicz List Processor

**LLP** soll eine minimalistische formale Sprache zur semantischen Auszeichung von Texten, zur funktionalen Formulierung von Algorithmen und zur generatorischen Beschreibung von Diagrammen, Bildern und Fräskopfbahnen werden.

Eine wesentliche Rolle spielen dabei **Namenscontainer** mit semantischen Beziehungen

## Grundlagen

### Runen als Präfix

Alle für den Parser unterscheidbaren Strukturen erhalten ein Präfix in Form einer nordischen **Rune**. 

Die *Runen* werden in keiner heute mehr existierenden Sprache gennutzt. Damit sind die Präfixe, durch die Sparachstrukturen kenntlich werden, eindeutig von Textdaten unterscheidbar. 

### Kommentare ᛭

`᛭` schließt den Rest vom Parsen aus. Damit können nach `᛭` beliebige Kommentare notiert werden.

### Präfixe für Zahlenwerte

Eine Gleitpunktzahl wie **3.14** ist eine kulturspezifische Notation (**en-US**). 

Um Zahlenwert von einer textuellen und kulturspezifischen Präsentation in einer Sprache zu unterscheiden, werden diese in **LLP** stets durch ein spezielles Präfix explizit gekennzeichnet.

🚨 Zahlen  können wie z.B. `ᚱ _Zähler_ _Nenner_` eine listenartige Struktur darstellen, sind aber keine Listen. Die einzelnen Partikel wie im Beispiel `_Zähler_` und `_Nenner_` dürfen nur Konstanten sein, wie `ᚱ 1 2`, jedoch keine Ausdrücke!

### Kardinalzahlen ᛕ

`ᛕ` ist das Präfix für ganze Zahlen:
```
ᛕ 1         ⟺ 1
ᛕ -123      ⟺ -123
ᛕ 16 AFD    ⟺ nat. Zahl zur Basis 16 (hex)
ᛕ  2 L00LLL ⟺ nat. Zahl zur Basis  2 (dual)  
ᛕ ᛞ         ⟺ + Unendlich
ᛕ -ᛞ        ⟺ - Unendlich
```

### Gebrochen Rationale Zahlen ᚱ

`ᚱ` ist das Präfix für gebrochen rationale Zahlen. Diese bestehen aus einem *Nenner* und einem *Zähler*, getrennt durch ein Leerzeichen: 

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

### Gleitpunktzahlen ᚪ

`ᚪ` ist das Präfix für rationale Zahlen in der Gleitpunkt- Darstellung. Vor- und Nachkomma- Stellen bilden die beiden Elemente einer Liste. Kulturspezikfische Spearatoren wie `,` oder `.` sind damit überwunden.

```
ᚪ 3       ⟺  3.0
ᚪ 3 14    ⟺  3.14
ᚪ -2 72   ⟺ -2.72
ᚪ -2 72 3 ⟺ -2.72e3 = -2720 
```

### Boolsche Werte ᛔ

`ᛔ` ist das Präfix für boolsche Werte. Die beiden möglichen boolschen Werte werden durch die Namen **true** und **false** ausgedrückt:

```
ᛔ true  ⟺ True
ᛔ false ⟺ False
```

### Namensreferenzen ᚻ

`ᚻ` ist das Präfix für eine *NamingID*. Eine *NamingID* ist ein eindeutiger Schlüssel zu Identifizierung eines Namenscontainers.

Beispiele:

`ᚻ milProgramm` ⟺ Referenz auf den Namenscontainer, der für Fräsenprogramme steht.

### Hierarchieen ᚠ

`ᚠ`ist das Präfix eines Pfades in einer Hierarchie. Der Pfad muß durch ein Listenendsymbol `ᛩ` abgeschlossen werden.

`ᚠ ᛕ23 ᛕ10 ᛕ15 ᛩ` ⟺ Kann z.B. eine Versionsnummer mit den drei Hierarchieebnen *Hauptversion*, *Nebenversion*, *Buildnummer* darstellen. Oder die Uhrzeit **23:10:15**. Oder das Datum **15.10.2023**.

`ᚠ ᚻ millingMachine ᚻ circelMilling ᚻ millDisc ᛩ` ⟺ Pfad in einem Namensraum

### Strings ᛒ

*Strings* sind Listen aus beliebigen Zeichen. Sie können auch Leerzeichen enthalten.

*Strings*, die keine Leerzeichen enthalten, können direkt notiert werden.

```
᛭ geschlossener String, enthält keine Leerzeichen
Hallo

᛭ geschlossener Strings, die einzelne Hierarchieebenen benennen
ᚠ All Galaxieen Andromeda ᛩ 
```

Enthalten *Strings* Leerzeichen, dann müssen sie in eine **B-Liste**: `ᛒ ... ᛩ`   gesetzt werden. 
```
᛭ String aus mehreren Wörtern
ᛒ Hallo Weltᛩ

᛭ Komplexe Texte als String
ᛒ 
    Mit *B- Liste* Strings können auch **MarkDown** formatierte Texte geschrieben werden.

    So wird *Text* und *Logik* vollständig vermischt.     
ᛩ
```

### Arrays ᚤ

*Arrays* sind Listen von Werten gleichen elementaren Typs. Sie stellen komplexe, zusammengesetzte Werte dar wie z.B. Real- und Imaginärteil einer komplexen Zahl, oder die Komponenten eines Vektors.

Arrays werden stets mittels `ᚤ` eingeleitet, und mittels `ᛩ` beendet werden. Der erste Element von links legt dabei den Datentyp für alle anderen Elemente des Array verbindlich fest. Diese Regel unterscheidet das *Array* im wesentlichen vom *String* (neben den unterschiedlichen Präfixen).

``` 
᛭ Array mit den ersten fünf Primzahlen
ᚤ ᛕ2 ᛕ3 ᛕ5 ᛕ7 ᛕ11 ᛩ

᛭ Fehlerhaft aufgebautes Array: Alle Elemente müssen vom gleichen Typ sein
ᚤ ᛕ2 ᚪ3 ᛕ5 ᛕ7 ᛕ11 ᛩ ⟹ ERROR!
```

#### Zugriff auf Array Elemente

Auf einzelne Elemente eines Arrays kann mittels Operator `ᛏᚤ _array_ _index_` zugegriffen werden.

Dieser hat als Parameter den **0** basierte Index und das *Array*, aus dem der Wert zu entnehmen ist.

Soll im Falle eines Zugriffs auf ein nicht vorhandenes Element durch einen zu kleinen, oder zu großen Index keine Ausnahme, sondern eine benutzerdefinierte Fehlerbehandlung starten, dann ist der `ᛏᚤᛊ` Operator einzusetzen: `ᛏᚤᛊ _array_ _index_ _errIndexOutOfRangeHandler_`.

#### Benennen von Werten mittels ᛝ Operator

Werte können an einen *Namen* mittels dem **Bind** Operator ᛝ gebunden Werden. Über diesen Namen kann der Wert dann referenziert und abgerufen werden.

`ᛝ _NameAlsString_ _Wert_` bindet den Wert an einen Namen, der nur im Kontext der aktuellen 𝓛𝓛𝓟 Datei eindeutig ist.

`ᛝ _MonikerForNamingIdAsString_ ᚻ _NamingID_` bindet lokal in der 𝓛𝓛𝓟 Datei einen Namen (Moniker)  an eine *NamingId*. Die *Naming* ID ist dabei ein 64bit Wert, der für einen global gültigen Namen steht (Namenskontainer).

```
᛭ Konstante PI definieren
ᛝ PI ᚪ 3 14 

᛭ Den lokal gültigen Namen PI an eine global gültige Naming ID binden.
ᛝ PI ᚻ ᛕ 16 7ABC123

᛭ Liste der ersten fünf Primzahlen an einen Namen binden
ᛝ ersteFünfPrimzahlen ᚤᛕ2 ᛕ3 ᛕ5 ᛕ7 ᛕ11 ᛩ
```
Die Bindung eines Namens an einen Wert kann auch als **Attribut Wertepaar** betrachtet werden!

#### Zugriff Auf den Wert, der an einen Namen gebunden ist mittels ᛏᚻ

Wurde an einen Namen ein Wert gebunden, dann kann überall, wo normalerweise der Wert eingesetzt wird, der Name eingesetzt werden, dem aber der **Replace by** Operator `ᛏᚻ` vorangesetzt werden muss:

```
᛭ Konstante PI definieren
ᛝ PI ᚪ 3 14 

᛭ Den Wert von **PI** an den synonymen Namen **pie** binden
ᛝ pie ᛏᚻ PI

᛭ Den Wert der globalen mit Naming ID definierten Konstante **PI** an den synonymen Namen **piee** binden

ᛝ pie ᛏᚻ ᚻ ᛕ 16 7ABC123I

```

### Attributlisten ᚹ ... ᛩ

Eine Menge von *Bind* Operationen können in Listen zusammengefasst werden. Innerhalb einer solchen Liste darf ein bestimmter Name stets nur einmal an einen Wert gebunden werden.

Diese Listen stellen damit auch Listen aus **Attribut- Wertepaare** dar.
```
᛭ Richtig: innerhalb der Liste wird der Name genau einmal gebunden
ᚹ
    ᛝ X ᚪ3 14
    ᛝ Y ᚪ2 72
ᛩ

᛭ Falsch: innerhalb der Liste wird der Name genau mehr als einmal gebunden
ᚹ
    ᛝ Value ᚪ3 14
    ᛝ Value ᚪ2 72
ᛩ
```

### Benannte Attributlisten

Attributlisten sind komplexe Werte, die ebenfalls mit `ᛝ` an einen Namen gebunden werden können:

```
᛭ Richtig: innerhalb der Liste wird der Name genau einmal gebunden
ᛝ Punkt1
ᚹ
    ᛝ X ᚪ3 14
    ᛝ Y ᚪ2 72
ᛩ
```

Für den Zugriff auf die Werte in der benannten Liste kann wieder mittels **Replace by** Operator `ᛏᚻ` benutzt werden. In diesem Fall sind die Namen jedoch als Hierarchie anzugeben: `ᛏᚻ ᚠ _NameListe_ _NameAttribut_ ᛩ`

```
᛭ Modelierung eines Vektors mit den Komponenten a und b als verschachtelte Attributliste
ᛝ Vek
ᚹ
    ᛝ a
    ᚹ
        ᛝ X ᚪ3 14
        ᛝ Y ᚪ2 72
    ᛩ

    ᛝ b
    ᚹ
        ᛝ X ᚪ12
        ᛝ Y ᚪ6 1
    ᛩ
ᛩ

᛭ Zugriff auf Komponente a
ᛏᚻ ᚠ Vek a ᛩ

᛭ Zugriff auf Y aus Komponente a
ᛏᚻ ᚠ Vek a Y ᛩ
```

### Typ- Definitionen

Um Parameterlisten von Funktionen und Methoden abstrakt definieren zu können, werden Typdefinitionen benötigt. Typen stehen für endliche Mengen von Werten. 

`ᛟ` schaltet die Evaluierung einer Liste in die Evaluierung einer Typdeklaration um.

`ᛕᛟ` steht für eine Zahl aus der Menge der ganzen Zahlen.

`ᛔᛟ` steht für einen boolschen Wert.

`ᚤᛟ ᛕᛟ ᛩ` steht für ein Array aus beliebig vielen ganzen Zahlen.

`ᚤᛟ ᛕᛟ ᛕ3 ᛩ` steht für ein Array aus drei ganzen Zahlen.

`ᚤ ᚻᛟ ᛩ` steht für ein Array aus beliebig vielen Namensreferenzen.

`ᚤᛟ ᚻred ᚻgreen ᚻblue ᛩ` steht für einen Aufzählungstyp/Set: Eingesetzt werden dürfen nur die im Array aufgelistete Werte.

### N- stellige Methoden und Funktionen

`ᛖ` ist das Prefix für eine Funktion/Methode. Diesem muss ein Name der Funktion, eine Liste von Parametern und eventuell ein Rückgabewert folgen. Diese Liste wird mit `ᛩ` abgeschlossen. So definierte Funktione werden allgemein als N- Stellig bezeichnet.

Beispiel: Addition der beiden Gleitpunktzahlen ᚪ3.14 und ᚪ2.72 
```
ᛖ ᚻadd 
    ᛜ A ᚪ3 14 
    ᛜ B ᚪ2 72
ᛩ
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

