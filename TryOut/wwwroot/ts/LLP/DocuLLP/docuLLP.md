# 𝓛𝓛𝓟: Łukasiewicz List Processor

**LLP** soll eine minimalistische formale Sprache zur semantischen Auszeichung von Texten, zur funktionalen Formulierung von Algorithmen und zur generatorischen Beschreibung von Diagrammen, Bildern und Fräskopfbahnen werden.

Eine wesentliche Rolle spielen dabei **Namenscontainer** mit semantischen Beziehungen

## Grundlagen

### Werte elementarer Datentypen

Um z.B. einen Zahlenwert wie 3.14 von einer textuellen Präsentation in einer Sprache zu unterscheiden, werden die Werte elementarer Datentypen in **LLP** stets durch ein spezielles Präfix explizit gekennzeichnet.

`ᛕ` ist das Präfix für ganze Zahlen:
```
ᛕ 1    ⟺ 1
ᛕ -123 ⟺ -123
```

`ᚱ` ist das Präfix für gebrochen rationale Zahlen. Diese bestehen aus einem Nenner und einem Zähler, getrennt durch ein Leerzeichen: `ᚱ _Zähler_ _Nenner_`

```
ᚱ 1 2   ⟺ 1/2 = 0.5
ᚱ 2 3   ⟺ 2/3 = 0.666
ᚱ -4 16 ⟺ -4/16 = -0.25
```

`ᚪ` ist das Präfix für rationale Zahlen in der Gleitpunkt- Darstellung. Vor- und Nachkomma- Stellen werden hier stets durch einen Punkt `.` getrennt.

```
ᚪ 3.14  ⟺  3.14
ᚪ -2.72 ⟺ -2.72
```

`ᛔ` ist das Präfix für boolsche Werte. Die beiden möglichen boolschen Werte werden durch die Namen **true** und **false** ausgedrückt:

```
ᛔ true  ⟺ True
ᛔ false ⟺ False
```

`ᚻ` ist das Präfix für eine *NamingID*. Eine *NamingID* ist ein eindeutiger Schlüssel zu Identifizierung eines Namenscontainers.

```
ᚻ milProgramm ⟺ Referenz auf den Namenscontainer, der für Fräsenprogramme steht.
```

### Strings

*Strings* sind Listen aus Zeichen. Enthalten sie Leerzeichen, dann müssen sie in Apostophe `'` gesetzt werden. Enthalten sie keine Leerzeichen, dann können die Apostrophe entfallen:

```
Hallo
'Hallo Welt'
```

### Arrays

*Arrays* sind Listen von Werten gleichen elementaren Typs. Sie stellen komplexe, zusammengesetzte Werte dar wie z.B. Real- und Imaginärteil einer komplexen Zahl, oder die Komponenten eines Vektors.

Sie dürfen deshalb nur als Werte von Eigenschaften, Parametern oder Rückgabewerte von Funktionen sein.

Arrays werden stets mittels `ᚤ` eingeleitet, und mittels `ᛩ` beendet werden.

Eine Array mit den ersten fünf Primzahlen kann z.B. wie folgt dargestellt werden:

``` 
ᚤ ᛕ2 ᛕ3 ᛕ5 ᛕ7 ᛕ11 ᛩ
```

#### Zugriff auf Array Elemente

Auf einzelne Elemente eines Arrays wird mittels der dreistelligen Funktion  `ᛖ᛫᛫᛫ ᚻ arrayIX _Array_ _ix_ _ErrIndexOutOfRangeHandler_` zugegriffen.

Diese hat als Parameter den **0** basierte Index, das *Array* aus dem der Wert zu entnehmen ist, und den Verweis auf eine *Fehlerbehandlungsmethode*, falls der Zugriff scheitert.

Wenn kein explizit definierte Fehlerbehandlungsmethode erforderlich ist, da der Default Fehlerhandler ausreicht, dann kann auch die zweistellige Funktion eingesetzt werden: `ᛖ᛫᛫ ᚻ arrayIX _Array_ _ix_`

Verglichen mit anderen Programmiersprachen ist der Indexzugriffsoperator für Arrays hier sehr aufwendig formuliert. Deshalb bieten sich Kurzformen an:

`ᛏᚤ` sei das vereinfachte Symbol für den zweistelligen Arrayzugriffs- Operator: `ᛏᚤ _array_ _index_`

`ᛏᚤᛊ` sei das vereinfachte Symbol für den dreistelligen Arrayzugriffs- Operator: `ᛏᚤᛊ _array_ _index_ _errIndexOutOfRangeHandler_`


### N- stellige Methoden und Funktionen

`ᛖ` ist das Prefix für eine Funktion/Methode. Diesem muss ein Name der Funktion, eine Liste von Parametern und eventuell ein Rückgabewert folgen. Diese Liste wird mit `ᛩ` abgeschlossen. So definierte Funktione werden allgemein als N- Stellig bezeichnet.

`ᛜ` ist das Präfix für einen *Parameter*. *Parameter* bestehen im allgemeinen immer aus einem Parameternamen und einem Wert, der an den Parameter gebunden ist: `ᛜ _paramName_ _paramValue_`

Wird der _paramName_ durch eine *NamingID* definiert, dann kann mittels semantischer Referenzen im Namenscontainer der Datentyp eines Funktionsparameters implizit festgelegt werden.

Alternativ könnte man den Datentyp eines Parameters durch eine Default- Wert eines elementaren Datentypen festlegen: `ᛜ CX ᚪ_ ⟺ CX ist vom Typ Gleitkommazahl`


#### Vereinfachte Methoden/Funktionsdefinitionen


`ᛖ᛫` definiert explizit eine einstellige Funktion. Diese hat genau einen Parameter: `ᛖ᛫ _funktionsName_ _parameter1_`

`ᛖ᛫᛫` definiert explizit eine zweistellige Funktion. Diese hat genau zwei Parameter: 


usw..

Funktionen werden mit `ᛏ` (Return) aufgerufen, und liefert den Funktionswert. Nach dem CQR Pattern verändern Funktionen den inneren Zustand nicht.

Beispiele:

```
᛭᛭ Methode, die keinen Parameter hat (0 Stellig): Stoppt die Fräse
ᛖ ᚻ milStop ᛩ

᛭᛭ Funktion, die keinen Parameter hat (0 Stellig): liefert die aktuelle Position X
ᛏ ᛖ ᚻ milCurrentPosX ᛩ

᛭᛭ explizit zweistellige Funktion mit zwei PArametern: liefert die Summe der beiden Gleitpunktzahlen.
ᛏ ᛖ᛫᛫ ᚻadd ᚪ 0.1 ᚪ 1.3
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

