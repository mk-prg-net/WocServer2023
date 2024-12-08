# Stack ᛝ Flow (ehem. ᚾᚤᛏ = NYT, nützliche Datenflüsse)

Nyt (die nützliche) Flussname im Lied der Grímnismál (Edda): https://de.wikipedia.org/wiki/Liste_der_Fl%C3%BCsse_im_Lied_Gr%C3%ADmnism%C3%A1l

**Stack ᛝ Flow** ist eine minimalistische, formale Sprache zur Beschreibung aktiver Berechnungen aus laufendem Text heraus. Zum Beispiel kann eine auf dem newtonsche Grundgesetz **F=m⋆a** basierende Berechnung wie folgt definiert werden:

    ᛝS1 ᛭ Für die folgenden Berechnungen im Text wird ein separater Stack S1 angelegt.

    Die Beschleunigung auf der Erde beträgt ᚩ9,81ᛎ ᛇm/s²ᛎ. Ein Mensch mit einem Gewicht von ᛕ120ᛎ ᛇkgᛎ wird mit der Kraft ᛨm⋆a⟶F ᛨ⎙ angezogen.

Für die Berechnung relevante nummerische Werte als auch Strings werden vom Text durch spezielle Präfixe wie ᚩ, ᛕ und ᛇ separiert. Mittels des Operators ᛎ werden diese in einen Stapelspeicher im Hintergrund geschrieben, aus dem dann Funktionen wie ᛨm⋆a⟶F oder ᛨ⎙ diese einlesen, verarbeiten und auf den Stapel wieder zurückschreiben. ᛨ⎙ liest zum Beispiel den gesamten Stapel aus, und blendet ihn hinter dem Funktionsaufruf in den Text ein.

## Grundlagen

### Runen als Präfix

Alle für den Parser unterscheidbaren Strukturen erhalten ein Präfix in Form einer nordischen **Rune**. 

Die *Runen* werden in keiner heute mehr existierenden Sprache genutzt. Damit sind die Präfixe, durch die Sparachstrukturen kenntlich werden, eindeutig von Textdaten unterscheidbar. 

### Kommentare ᛭
**᛭** schließt den Rest vom Parsen aus. Damit können nach **᛭** beliebige Kommentare notiert werden.

### Stapelspeicher ᛝ und die Operatoren ᛎ (push) ᛏ (pop) und ᛨ (push-pop)

Stapelspeicher sind die einzigen, zur Laufzeit veränderliche Speicher in **Stack ᛝ Flow**. Operatoren und Programme können aus diesen lesen und ihre Ergebnisse wieder zurückschreiben.

Es können mittels dem Stack- Operator **ᛝ** beliebig viele Stapel zu Laufzeit definiert und aktiviert werden.

Mittels **ᛝ _Stack_Name_** wird ein Stack angelegt, an den Namen gebunden und aktiviert. Aktiviert bedeutet, dass alle nachfolgenden Stackoperationen für diesen gültig sind.

Auf einen bereits zuvor definierten Stack kann simple durch erneutes setzen von **ᛝ _Stack_Name_** zurückgeschaltet werden, wenn zwischenzeitlich ein anderer Stack aktiviert wurde. 

Ein durch **ᛝ _Stack_Name_** definierter Stack ist global gültig. Möchte man einen nur im aktuellen Block gültigen benannten Stack definieren, dann ist dem Namen die Rune ᛫ voranzustellen, z.B. **ᛝ ᛫ _Stack_Name_**. Blöcke sind z.B. der Siegel **ᛋ**-, oder der Sowilo **ᛊ** Block.

Die Stackoperation **_Wert_ᛎ** (push) speichert/legt den Wert auf den Stapel. Mittels der Stackoperation **ᛏ** (pop) kann der Wert wieder vom Stapel genommen werden. **ᛏᛟ_name_** nimmt einen Wert vom Stapel und bindet ihn an den Namen *_namen_* (siehe unten).

Die Kombination aus **ᛎ** (push) und **ᛏ** pop ist **ᛨ** (push-pop). Dieser Operator kann auf Funktionsnamen angewendet werden. Die Funktionen lesen dann alle Argumente vom Stapel ein, und legen den Funktionswert auf den Stapel zurück. Zum Beispiel nimmt **ᛨm⋆a⟶F** den Wert für m und a vom Stapel und schreibt das Ergebnis **F** zurück auf den Stapel. ᛨ⎙

Wird ein weiterer Stapel mittels **ᛝ _Stack_Name_2_** angelegt und aktiviert, dann existiert der unter _Stack_Name_ zuerst weiter, ist jedoch nicht aktiv. Soll er wieder aktiv werden, dann muss **ᛝ _Stack_Name_** erneut aufgerufen werden.

```
᛭ Ein neuer Stack mit dem Namen S1 wird angelegt. Der Stack ist leer []
ᛝS1

᛭ Die drei Werte 1, 2, 3 werden auf dem Stack abgelegt
ᛕ1ᛎ ᛕ2ᛎ ᛕ3ᛎ

᛭ Nun hat der Stack den Inhalt Bottom[1][2][3]Top
᛭ Es wird ein Wert vom Stack genommen, und an den Namen x gebunden
ᛏᛟx
᛭ Der Stack S1 hat nun den Inhalt Bottom[1][2]Top

᛭ Ein neuer Stack mit dem Namen S2 wird angelegt. Der Stack ist leer []
᛭ Der Stack S1 existiert jedoch weiter
ᛝS2

᛭ In S2 werden zwei weitere Werte abgelegt:
ᛕ4ᛎ ᛕ5ᛎ ᛡxᛎ
᛭ S1 hat den Inhalt Bottom[1][2]Top
᛭ S2 hat den Inhalt Bottom[4][5][3]Top

ᚪᛏᛏ᛬ᛖᚱ+                 ᛭ Hier werden zwei Werte vom Stack S2 entnommen und addiert. Er hat nun den Inhalt Bottom[4]Top
   ᛋ ᛝ᛫Slokalᛎ ᚱ2ᛎ      ᛭ Ein zu ᛋ lokaler Stack wird eingerichtet. Er hat den Inhalt Bottom[8][2]Top
     ᚪᛏᛏ᛬ᛖᚱ/            ᛭ Vom ᛋ lokalen Stack werden nun zwei Werte für die Division entnommen. Er ist nun leer.
        ᛋ ᛝS2 ᛎ ᛩᛩ     ᛭ Es wird wieder auf den globalen Stack S2 zurückgeschaltet, und
                       ᛭ in diesen der Quotient geschrieben: Bottom[4][4]Top
```

## Literale elementarer Datentypen

### Präfixe für die Notation von Zahlenwerten
Eine Gleitpunktzahl wie **3.14** ist eine kulturspezifische Notation (**en-US**). 

Um die Notation von Zahlenwert von einer textuellen und kulturspezifischen Präsentation in einer Sprache zu unterscheiden, werden diese in **Stack ᛝ Flow** stets durch ein spezielles *Präfix* explizit gekennzeichnet.

🚨 Zahlen  können wie z.B. `ᚱ *Zähler*/*Nenner*` eine listenartige Struktur darstellen, sind aber keine Listen. Die einzelnen Partikel wie im Beispiel `*Zähler*` und `*Nenner*` dürfen nur Konstanten sein, wie `ᚱ 1/2`, jedoch keine Ausdrücke!

### Nummerische Datentpen
Die Notationsformen für Zahlenwerte haben Beschränkungen bezüglich der Genauigkeit. Deshalb korrespondieren die Notationsformen auch mit Teilmengen von **ℚ**. Diese Teilmengen Werden *Nummerische Datentypen* genannt. 

Die nummerischen Datentypen werden durch Kombination des speziellen Präfixes für eine Notation (z.B. **ᛕ**) mit dem allgemeinen Datentyp- Schalter **ᛠ** verbunden zum Datentyp Symbol **ᛕᛠ**.

**ᛠ** schaltet allgemein die Evaluierung einer Liste in die Evaluierung einer Typdeklaration um.

**ᛠ** alleine steht für jeden beliebigen Datentyp.

### Basis des Zahlensystems ᛔ 
Zahlen können über verschiedenen *Basen* dargestellt werden. So kann die **26** dargestellt werden dekadisch mit der Basis **10** als **26**, hexadezimal mit der Basis **16** als **1A**, und binär mit der Basis **2** als **LL0L0**. 

Die Basis kann in einen nummerischen Typ explizit definiert werden mit dem Präfix **ᛔ**

```
᛭ Basis 2
ᛔ2

᛭ Basis 10- kann in der Regel entfallen, da Default
ᛔ10

᛭ Basis 16
ᛔ16
```

### Allgemeine, rationale Zahlen ᚱ

**Stack ᛝ Flow** strebt eine exakte Zahlendarstellung an, und versucht so die Probleme von Gleitpunktzahlen zu vermeiden. Da technisch nur endliche Ziffernfolgen darstellbar sind, beschränkt sich **Stack ᛝ Flow** von vornherein auf die Darstellung rationaler Zahlen. 
Spezielle transzendentalen Zahlen wie **π** oder **𝑒** werden durch ebendiese Symbole ausgedrückt, und mit der vom System maximal bereitstellbare Genauigkeit geliefert.

In **Stack ᛝ Flow** wird nicht weiter unterteilt in Festkomma und Gleitpunktzahlen. Die allgemeine, rationale Zahlendarstellung **ᚱ** ermöglicht die exakte Präsentation beider Datentypen.

**ᚱ** ist das Präfix für Zahlen in **Stack ᛝ Flow**. Diese sind ein Quadrupel wie folgt: **ᚱ** = **(m, n, d, x)**. 

Komponente | Bedeutung
-----------|--------------------------------
**m**      | Ganzzahliger Anteil einer Zahl
**n**      | Nominator = Zähler des gebrochenen Anteils
**d**      | Denominator = Nenner der gebrochenen Anteils
**x**      | Faktor für Größenordnung (z.B. Zehnerpotenz)

Notiert wird das durch **ᚱ m  n/d Xx)**. Der Zahlenwert errechnet sich dann zu **Wert= (m+n/d)*x**.

*Nenner* und *Zähler* des gebrochenen Anteils werden durch ein **/** getrennt. Die Rune **ᚷ** (Gebo) präfixed den Exponenten. Per default ist die *Basis* **10**, auch für den *Exponenten*. Mittels **ᛔ** kann eine abweichende *Basis* vereinbart werden.

Einzelne Komponenten des Tupels können in der **ᚱ** Definition auch weggelassen werden. So ergeben sich folgende Varianten

1. **ᚱ m**
2. **ᚱ n/d**
3. **ᚱ m n/d**
4. **ᚱ m n/d ᚷx**
5. **ᚱ ᛔ *Basis* m n/d ᚷx**

Beispiele:
```
ᚱ 2     ⟺ 2
ᚱ 1/2   ⟺ 1/2 = 0.5
ᚱ 1 2/3 ⟺ 1 2/3 = 1.666
ᚱ -4/16 ⟺ -4/16 = -0.25
ᚱ ᛔ2 -L00/L0000 ⟺ -4/16 = -0.25 im binärsystem
ᚱ ᛔ2 -L/L000 ᚷLL ⟺ -1 = -1/8 * 2^3
```
Die rationalen Zahlen können z.B. als Zoll- Maße genutzt werden

**ᚱᛠ** ist der Datentyp für Zahlen in **Stack ᛝ Flow**.

### Mit imaginäre Zahlen 𝒾 erweitern zu den komplexen Zahlen

Komplexe Zahlen kann man als die algebraische Kombination **re + 𝒾im** von reellen und imaginären Zahlen betrachten. Dabei ist **re ∈ ℝ** und **𝒾im ∈ 𝕀**. Der Imaginärteil erhält dabei das mathematische Schreibschrift **𝒾** (U +1D4BE) als Präfix. 

```
𝒾ᚱ1                        ⟺ 𝒾 ∈ 𝕀
ᚱ1+𝒾ᚱ2                     ⟺ 1+𝒾2 ∈ 𝕀
ᚱ-𝑒 + 𝒾ᚱπ                  ⟺ -2.72+𝒾3.14 ∈ 𝕀
ᚱ1 2/3 + 𝒾ᚱ7/8             ⟺ 1 2/3 + 𝒾7/8
```

### Nummerische Vereinfachung von ᚱ

Sei **ᚱ m n/d ᚷ(a10^b+c)**, x kann also als Vielfaches einer 10-er Potenz plus einem Offset dargestellt werden, dann kann der Ausdruck vereinfacht werden zu: **ᚱ m(a10^b+c) n(a10^b+c)/d**


### Grundrechenoprationen auf ᚱ

Sei **ᚱa** = **R m n/d ᚷx** und **ᚱb** = **R M N/D ᚷX**. Dann gilt:

1. **ᚱa + ᚱb** = **ᚱ (mx+MX) (Dnx+dNX)/dD ᚷ1**
2. **ᚱa x ᚱb** = **ᚱ mM (nMD+mNd+nN)/dD ᚷxX**


### Boolsche Werte ᛒ

**ᛒ** ist das Präfix für boolsche Werte. Die beiden möglichen boolschen Werte werden durch die Namen **true** und **false** ausgedrückt:
```
ᛒ true  ⟺ True
ᛒ false ⟺ False
```
**ᛒᛠ** ist der Datentyp für boolsche Werte.

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

#### Stringinterpolation

Werden in einem String Namensreferenzen eingesetzt, die beim Abruf des Strings evaluiert werden, dann liegt eine Stringinterpolation vor.

Sei **ᛟattrib schöne** eine Namensbindung. Dann kann eine Stringinterpolation wie folgt definiert werden:

**ᛇ Hallo *ᛡattrib* Welt ᛩ** 

Diese wird dann evaluiert zu:

**ᛇ Hallo schöne Welt ᛩ** 

### Hierarchieen ᚠ

**ᚠ** (runic Fehu) ist das Präfix eines Pfades in einer Hierarchie. Der Pfad muß durch ein Listenendsymbol **ᛩ** (runic Q) abgeschlossen werden.

`ᚠ ᛕ23 ᛕ10 ᛕ15 ᛩ` ⟺ Kann z.B. eine Versionsnummer mit den drei Hierarchieebnen *Hauptversion*, *Nebenversion*, *Buildnummer* darstellen. Oder die Uhrzeit **23:10:15**. Oder das Datum **15.10.2023**.

`ᚠ millingMachine circelMilling millDiscᛩ` ⟺ Pfad in einem Namensraum

**ᚠᛠ** ist der Datentyp für Hierarchieen.

## Informationen darstellen durch Binden von Werten an Namen mittels ᛟ Operator

Informationen bestimmen den Ausgang von Entscheidungen. Entscheidungen manifestieren sich durch die Zuordnung/Belegung/Bindung von Werten an Systemparametern.

Die Bindung eines Wertes an einen Systemparameter wird **Attibut** genannt.

Mittels dem **Bind** Operator **ᛟ** (runic Othalan) kann in **Stack ᛝ Flow** ein Wert an einen Namen gebunden, und somit ein Attribute gebildet werden. Über den Namen ist der Wert dann referenzier- und abgerufbar. 

Der Namen muss innerhalb seines *Namensraumes* (siehe unten) eindeutig sein. Ein typischer Namensraum ist die **Stack ᛝ Flow** Datei.

Attribute bzw Namensbindungen sind wie folgt aufgebaut: `ᛟ <Name als String> <Wert>`

Beispiele:
```
᛭ Konstante PI definieren
ᛟPI ᚩ3,14 

᛭ Liste der ersten fünf Primzahlen an einen Namen binden
ᛟersteFünfPrimzahlen ᚤᛕ2 ᛕ3 ᛕ5 ᛕ7 ᛕ11 ᛩ
```
Die Bindung eines Namens an einen Wert kann auch als **Attribut Wertepaar** betrachtet werden!

Zu bindende Werte könne mit **ᛏ** vom Stapel gelesen werden:
```
Die Erdbeschleunigung beträgt ᚩ9,81ᛎ ᛇm/s²ᛎ.
ᛏᛟg         ᛭ Wert für Erdbeschleunigung vom Stack lesen und an Name g binden
ᛏᛟgunit     ᛭ Einheit für Erdbeschleunigung vom Stack lesen und an Name gunit binden
```

### Naming ID's ᚻ

**ᚻ** ist das Präfix für eine *NamingID*. Eine *NamingID* ist ein global eindeutiger Name in Form einer **64bit** *GUID*.

An eine global eindeutige *NamingID* kann wie an einen lokalen *Namen* ein Wert gebunden werden in der Form `ᛟᚻ ᛕ ᛔ16 <Hex- Wert Naming ID> _Attribut-Wert_`.

Beispiele:
```
᛭ An die global gültige Naming ID 0x7ABC123 wird der Wert 3,1427 gebunden.
ᛟᚻ ᛕ ᛔ16 7ABC123 ᚩ3,1427
```

Naming- IDs sind eindeutig und excellent für reine Machine to Machine Kommunikation. Für einen menschlichen Programmierer hingegen sind sie hingegen zu abstrakt. Deshalb können ihnen im lokalen Namensraum eingängie *Moniker* verpasst werden durch `ᛟ<Moniker> ᚻ ᛕ ᛔ16 <Hex- Wert Naming ID> _Attribut-Wert_`

Beispiel:
```
᛭ Die global gültige Naming ID 0x7ABC123 mit dem gebundenen Wert 3,1427 wird zusätzlich an den lokal gültigen Moniker PI gebunden
ᛟPI ᚻ ᛕ ᛔ16 7ABC123 ᚩ3,1427
```

**ᚻᛠ** ist der Datentyp für Namensreferenzen.

### Namen in einen Wert auflösen mittels ᛡ (Ior)

Wurde an einen Namen ein Wert gebunden, dann kann überall, wo normalerweise der Wert eingesetzt wird, der Name eingesetzt werden. Dazu ist dem Namen das runic Ior **ᛡ** voranzusetzen:

```
᛭ Konstante PI definieren
ᛟPI ᚪ3,14 

᛭ Den Wert von **PI** an den synonymen Namen **pie** binden
ᛟpie ᛡPI

᛭ Den Wert der globalen mit Naming ID definierten Konstante **PI** an den synonymen Namen **piGlob** binden
ᛟpiGlob ᛡᚻ ᛕ ᛔ16 7ABC123I
```

### Namensraum- Strukturen ᛟ ... ᚹ ... ᛩ

Eine Menge von *Bind* Operationen können in Listen **ᚹ ... ᛩ** zusammengefasst werden. Innerhalb einer solchen Liste darf ein bestimmter Name stets nur einmal an einen Wert gebunden werden. Diese Listen werden **Namensraumstruktur**, oder kurz **Struktur** genannt.

```
᛭ Beschreibung einer Punktkoordinate durch eine Namensraumstruktur
ᚹ ᛟx ᚪ2,72 ᛟy ᚪ3,14 ᛩ 
```
Die Struktur kann selber als Wert mittels Bind an einen Namen gebunden werden. So entsteht ein **Namensraum**

```
᛭ Namensraum mathematischer Konstanten
ᛟMathConst
ᚹ
    ᛟPI ᚪ3,14
    ᛟe  ᚪ2,72
ᛩ

᛭ Namensraum, der einen Punkt darstellt
ᛟPunkt1 
ᚹ 
    ᛟx ᚪ2,72 
    ᛟy ᚪ3,14 
ᛩ 
```

#### Hierarchische Namensraum Referenzen ᚻᚠ ... ᛩ

Die Namen in einem Namensraum sind außerhalb dieses nicht mehr eindeutig. Eine eindeutige Addressierung der Attribute aus einer Position im Code außerhalb eines Namensraumes werden *Hierarchische Namensraum Referenzen* benötigt. Diese haben folgenden Aufbau: `ᚻᚠ <Name 1. Level> <Name 2. Level> ... <Name N. Level>ᛩ`.

Für den Zugriff auf den Werte ist er hierarchichen Referenz der *Ior* Operator **ᛡ** voranzustellen: `ᛡᚻᚠ <Name 1. Level> <Name 2. Level> ... <Name N. Level>ᛩ`. 

Beispiel
```
᛭ Organisation einer mathematischen Bibliothek
ᛟMath
ᚹ
    ᛟConst
    ᚹ
        ᛟPI ᚪ3,14
        ᛟe  ᚪ2,72
    ᛩ   
ᛩ

᛭ Zugriff auf PI
ᛡᚻᚠMath Const PIᛩ
```

#### Namensräume auf Basis globaler Naming- IDs
Um abstrakte Naming- IDs besser zu handhaben, können sie an lesbare Namen mittels **ᛟ** gebunden, und diese lesbaren Namen in Namensraumstrukturen organisiert werden:

```
᛭ Organisation einer mathematischen Bibliothek, 2
ᛟMath
ᚹ    
    ᛟBasicFunctions
    ᚹ
        ᛭ Naming- IDs der math. Grundrechenarten werden an lokale Namen gebunden
        ᛟadd ᛡᚻ ᛕ ᛔ16 ADDADD
        ᛟsub ᛡᚻ ᛕ ᛔ16 DE2323
    ᛩ
ᛩ

᛭ Zugriff auf add
ᛡᚻᚠMath BasicFunctions addᛩ

᛭ Hier wird über den hierarchichen Namen die Funktion aufgerufen
ᛣᚻᚠMath BasicFunctions addᛩ  ᛕ1 ᛕ2
᛭ ᛟsum ist nur innerhalb des Siegel - Zweiges sichtbar
ᛋ ᛏᛟsum ᛣprint ᛇ ᛡsum ist die Summe aus 1 uns 2 ᛩ
```
### Arrays ᚤ

*Arrays* sind Listen von Werten. Die Werte können primitiv oder komplex sein.

**ᚤ** (runic Y) ist das Präfix, welches die Liste eines Arrays eröffnet. **ᛩ** beendet die Liste. 

``` 
᛭ Array mit den ersten fünf Primzahlen
ᚤ ᛕ2 ᛕ3 ᛕ5 ᛕ7 ᛕ11 ᛩ

᛭ Array mit zwei Koordinaten
ᚤ 
   ᚹ ᛟx ᚪ2,72 ᛟy ᚪ3,14 ᛩ 
   ᚹ ᛟx ᚪ5,3  ᛟy ᚪ1,7ᛩ ᛩ
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

Array sind wie alle Werte unveränderlich (immutable): sie können nur gelesen, jedoch nicht verändert werden!

Auf einzelne Elemente eines Arrays kann mittels Indexzugriffs- Operator **[_index_]** lesend zugegriffen werden. Dieser hat als Parameter den **0** basierte Index. Erw wird direkt auf Array angewendet:
```
᛭ An den Namen **dritterEintrag** ist nun der Wert ᛕ5 gebunden.
ᛟdritterEintrag ᚤ ᛕ2 ᛕ3 ᛕ5 ᛕ7 ᛕ11 ᛩ[2] 

᛭ An den Namen **eineListe** wird ein Array gebunden
ᛟeineListe ᚤ ᛕ2 ᛕ3 ᛕ5 ᛕ7 ᛕ11 ᛩ

᛭ Der 3. Eintrag im Array wird ausgelesen und auf den Stapel gestellt.
ᛡeineListe[2]ᛎ
```
Im letzten Beispiel wird die Priorität der Operatoren deutlich: höchste Priorität hat ᛡ, dann kommt [], und schließlich ᛎ.

#### Einbetten von Array in Array mittels Expand ᚷ Operator

Mittels des Expand- Operator **ᚷ** kann der Inhalt eines Array in ein anderes eingebettet werden

```
ᛟsubArray ᚤ ᛕ2 ᛕ3 ᛩ 

᛭ ᛡnotExpandedArray ist ᚤ ᛕ1 ᚤ ᛕ2 ᛕ3 ᛩ ᛕ4ᛩ
ᛟnotExpandedArray
ᚤ    
   ᛕ1
   ᛡsubArray 
   ᛕ4 
ᛩ

᛭ ᛡres1 hat den Wert ᚤ ᛕ2 ᛕ3 ᛩ (Array) 
ᛟres1 ᛡnotExpandedArry[2]

᛭ ᛡexpandedArray ist ᚤ ᛕ1 ᛕ2 ᛕ3 ᛕ4ᛩ
ᛟexpandedArray
ᚤ    
   ᛕ1
   ᚷᛡsubArray
   ᛕ4 
ᛩ

᛭ ᛡres21 hat den Wert ᛕ3 (einzelnener, ganzzahliger Wert) 
ᛟres2 ᛡexpandedArry[2]
```

#### Einbetten von Arrays auf den aktuell aktiven Stack mittels Expand ᚷ Operator

Mittels des Expand- Operator **ᚷ** können alle Elemente eines Array hintereinander auf den Stapel kopiert werden:

```
ᛟmyArray ᚤ ᛕ1 ᛕ2 ᛩ 

᛭ Array auf den Stapel kopieren
ᛡmyArrayᛎ

᛭ Der Stapel hat nun die Belegung:
᛭ [ᚤ ᛕ1 ᛕ2 ᛩ] Top

᛭ Jetzt werden anstatt des Array selbst die einzelnen Werte des Array  auf den Stapel kopiert
ᛡmyArrayᚷᛎ

᛭ Der Stapel hat nun die Belegung:
᛭ [ᛕ2       ] Top
᛭ [ᛕ1       ]
᛭ [ᚤ ᛕ1 ᛕ2 ᛩ] Bottom

```

## EVA: Eingabe, Datenverarbeitung, Ausgabe


```
          +---------------+
    E1 -->|               |
     :    | Verarbeitung  |--> Ausgabe
    En -->|               |
          +---------------+
```

Dieses uralte Prinzip der elektronischen Datenverarbeitung wird wieder in den Fokus gestellt. 
Die Verarbeitung von Daten erfolgt durch einzelne, benannte *Verarbeitungsstufen* (kurz *Stufe*). Jede Verarbeitungsstufe wird mit der Rune *CALC* **ᛣ** eingeleitet, ihr folgt der Name der Verarbeitungsstufe, die Eingangsparameter, die Verarbeitungszweige und schließlich der Ausgang, der mit der Rune  *EOLHX* **ᛉ** gekennzeichnet wird.

```
᛭ Syntaktischer Aufbau einer Verarbeitungsstufe
ᛣ _NameVStufe_ _E1_ ... _En_ 
ᛋ _Nachfolgende_Verarbeitungsfunktion_im_SIGEL_Zweig_
ᛊ _Nachfolgende_Verarbeitungsfunktion_im_SOWILO_Zweig_
ᛉ _Abschluss_oder_Nachfolge_Funktion_am_Ausgang_
```

In **Stack Flow** kann die Verarbeitung in einer Stufe stets in zwei alternative Pfade erfolgen. Damit wird das grundlegende Prinzip der Verzweigung eingeführt. 

- **ᛋ**: SIEGEL Zweig
- **ᛊ**: SOWILO Zweig

Am Ende müssen aber beide Pfade wieder am Ausgang zu einem Pfad zusammengeführt werden. Damit können komplexe, jedoch strukturierte Datenflussgraphen konstruiert werden:

```
        +-----------------------+
 E1 ⟶  |           +------+    |
  :     |     ᛋ ⟶  | V1.1 | ⟶ | 
  :     |           +------+    |
  :     | ᛣ V1                  |⟶ ᛉ Ausgang
  :     |           +------+    |
  :     |     ᛊ ⟶  | V1.2 | ⟶ |
 En ⟶  |           +------+    |
        +-----------------------+
```
### Eingangswerte/Paramter

Jede Stufe kann parametriert werden. Die Parameter (oder Eingangswerte) werden auf dem Stapelspeicher bereitgestellt. Der Stapelspeicher kann unmittelbar nach dem Stufennamen mittel ᛎ (push) Operatoren vor Aufruf der Stufe mit den benötigten Parametern befüllt werden.

Eine einfache Verarbeitungsstufe, die dieses Prinzip direkt auzsnutzt, ist die **push** Stufe. Sie legt alle Eingangsparameter unverändert auf dem Stapel des Laufzeisystems ab:

```
                Inhalt Stapelspeicher
                   --+---+--+
ᛣ push  a ... z    a | b | c|
                   --+---+--+
             ⇠ ᛋ   ↵   
             ⇠ ᛊ   ↵
             ↳ ᛉ
```

#### Annahmen zum Stapelspeicher definieren

Da jede Stufe ihre Parameter vom Stapel liest, muss sichergestellt werden, dass auch alle benötigten Parameter auf dem Stapel für die Stufe bereitstehen. Die Prüfung des Stapelspeichers erfolgt durch die Stufe zur Laufzeit. NYT stellt zudem eine generische Implementierung für solche Prüfungen bereit durch **Musterbelegungen**:

𝑫𝒆𝒇 **Musterbelegung**: ist eine Liste von Typnamen nach der INGWAZ Rune: `ᛜ ᛠ1 … ᛠn`. Der erste Typname `ᛠ1` bezeichnet dabei den Datentyp des ersten Wertes auf dem Stapelspeicher, der zweite `ᛠ2` den des zweiten Wertes auf dem Stapelspeicher usw.. 

Die **Musterbelegung** kann an die Parameterliste einer Stufe angehangen werden, und definiert eine Annahme über die Belegung des Stapelspeichers vor dem Einkellern der Parameter einer Stufe:
```

p1ᛎ  …  pnᛎ ᛣstufenName ᛜ ᛠ1  …  ᛠm ᛉ
\---+---/               \---+---/
    |                       | 
Einzukellernde   Annahme über die bereits auf     
Parameter        dem Stapel liegenden Parameter
```

Wenn eine **Musterbelegung** nicht zutrifft, dann wird eine Fehlermeldung erzeugt und auf dem Stapel abgelegt. Anschließend wird im **ᛊ (Sowilo)** Zweig der Stufe fortgesetzt.

🚨 Achtung: Die Musterbelegung scheint einer formalen Parameterliste einer Prozedur in einer Programmiersprache wie **C#** zu entsprechen. Jedoch handelt es sich hier um ein automatisiertes Prüfverfahren für die Stapelspeicherbelegung zur Laufzeit (keine Prüfung zur Entwurfszeit via Compiler!), die beim konkreten Start der Stufe stattfindet. Es kann deshalb für verschiedene Stufenstarts auch verschiedene Musterbelegungen geben:

```
ᛕ77ᛎ ᛕ88ᛎ

᛭ Hier wird eine Musterbelegung von zwei Kardinalzahlen auf dem Stapelspeicher angenommen.
ᛣadd ᛜ ᛕᛠ ᛕᛠ
ᛊ ᛣprintᛉ
ᛉ
```

### Zweige
Eine Methode verarbeitet die übergebenen Parameter. Danach gibt es drei Möglichkeiten der Programmfortsetzung:

1. Es wird im **ᛋ (Siegel) Zweig** fortgesetzt
2. Es wird im **ᛊ (Sowilo) Zweig** fortgesetzt
3. Es wird sofort zum Stufenausgang **ᛉ (Eolhx)** gesprungen und diese damit formal beendet 

In den Fällen 1 und 2 wird nach Durchlauf der Zweige ebenfalls am Ausgang **ᛉ** abgeschlossen.

Beispiele:

```
᛭ Wurzel aus einer Zahl a ziehen
aᛎ ᛣSQRT
     ᛋ _op_auf_√a_           ᛭ Hier wird die √ von a bereitgestellt
     ᛊ _Fehlerbehandlung_    ᛭ z.B. im Fall a < 0
   ᛉ _Abschlussfunktion_  ᛭ Hier wird der Stapelspeicher Nach Ausführung von ᛋ oder ᛊ bereitgestellt
```

#### Bereitstellung der Ergebnisse in den Zweigen

ᛎ (push) und ᛏ (pop)
```             
aᛎ bᛎ cᛎ      
              --+---+--+
ᛣm            a | b | c| -----+----+     ᛭ Ablage der Parameter auf dem Stapelspeicher
              --+---+--+      |    |
              ------------+   |    |
 +---  ᛋ ᛣsᛉ   m(a, b, c) | <-+    |     ᛭ Ergebnis der Methode s im ᛋ Zweig bereitstellen
 |            ------------+        |
 |            ------------+        |
 | +-- ᛊ ᛣeᛉ   m(a, b, c) | <------+     ᛭ Ergebnis der Methode e im ᛊ Zweig bereitstellen
 | |         ------------+             
 | |  
 | |         ----------------------------------+
 +-+-> ᛉ      s(m(a, b, c)) oder e(m(a, b, c)) |  ᛭ Ergebnis vom ᛋ oder ᛊ Zweig bereitstellen
             ----------------------------------+   
```

Beispiel: Berechnen der Quadratwurzel

```
ᛇa²=ᛩᛎ
ᛣ input 
ᛋ ᛏᛟaa
᛭ Ende von Input
ᛉ

ᛇ Es wird nun die Wurzel aus ᛡaa gezogen ᛩᛎ ᛣprintᛉ

᛭ Start Wurzel ziehen (Inhalt von ᛟaa wird auf den Stapel gelegt)
ᛡaaᛎ
ᛣ sqrt 

᛭ Weiterleiten des Ergebnisses an die Print- Methode. Achtung: Im AusgabeString findet
᛭ String- Interpolation statt.
ᛋ ᛇ√ᛡaa= ᛩᛎ ᛕ2ᛎ ᛣprint ᛜ ᚪᛠ ᛇᛠ ᛕᛠᛉ

᛭ Weiterleiten im Fehlerfall an die Print- Methode. Achtung: Im AusgabeString findet
᛭ String- Interpolation statt.
ᛊ ᛇ√ᛡaa ist konnte nicht ermittelt werden. Ursache: ᛩᛎ ᛕ2ᛎ ᛣprint ᛜ ᛇᛠ ᛇᛠ ᛕᛠᛉ

᛭ Hier werden die Ausführungspfade wieder zusammengeführt
ᛉ 
ᛇProgramm √ beendet.ᛩᛎ ᛕ1ᛎ ᛣprintᛉ
```

### Hintereinanderschalten von Stufen in Sequenzen

Verarbeitungsstufen können direkt hintereinander ausgeführt werden: `ᛣV1ᛉᛣV2ᛉ…ᛣVnᛉ`. Die Ausgaben der ersten landen dabei auf dem Stack, von dem sie die zweite Verarbeitungsstufe einlesen und weiterverarbeiten kann usw.

```
᛭ Input liest einen Wert von der Tastatur ein und legt ihn auf den Stack
ᛣinput ᛇ gib eine ganze Zahl z ein. Der absolute Betrag |z| wird ermittelt ! ᛩ ᛉ

᛭ Vergleichsoperator 0 > eingabe
ᛣa_gt_b ᛕ0 ᛜ ᛕᛠᛉ

᛭ Auf dem Stack liegt das Ergebnis von 0 > eingabe
ᛣifElse ᛜ ᛒᛠ ᛋ ᛣmul ᛕ-1 ᛜ ᛕᛠ ᛉ ᛭ Wenn eingabe < 0 ist, dann mit -1 multiplizieren

᛭ Print liest die nächsten beiden Werte vom Stack, und gibt sie aus.
ᛣprint ᛕ2 ᛇ Der absolute Betrag |z| = ᛩ ᛜ ᛕᛠ ᛉ
```
### Verschachtelung von Stufen

In den ᛋ, ᛊ und ᛉ Zweig kann der bereitgestellte Inhalt des Stapelspeichers jeweils durch weitere Verarbeitungsstufen verarbeitet werden:

```
        +--------------------------------------------------------------+
 E1 ⟶  |           +----------------------------------------------+   |
  :     |           |       +---------------+                      |   |
        |     ᛋ ⟶  | i1⟶  |        ᛋ -->… |                      |   | 
        |           |  :    | ᛣ V1.1        | ⟶  ᛉ V.1.1 Ausgang… |⟶ |
        |           | im⟶  |        ᛊ -->… |                      |   | 
        |           |       +---------------+                      |   |
        |           +----------------------------------------------+   |
        | ᛣ V1                                                         |⟶ ᛉ Ausgang
        |           +----------------------------------------------+   |
        |           |       +---------------+                      |   |
        |     ᛊ ⟶  | w1⟶  |        ᛋ -->… |                      |   |  
        |           |  :    | ᛣ V1.2        | ⟶  ᛉ V.1.2 Ausgang… |⟶ |
        |           | wm⟶  |        ᛊ -->… |                      |   | 
  :     |           |       +-------------- +                      |   |
 E1 ⟶  |           +----------------------------------------------+   |
        +--------------------------------------------------------------+
```
Durch Fortsetzen dieses Prinzips können tief verschachtelte Strukturen enstehen.

```
᛭ Input liest einen Wert von der Tastatur ein und legt ihn auf den Stack
ᛣinput ᛇ gib eine ganze Zahl z ein. Der absolute Betrag |z| wird ermittelt ! ᛩ

᛭ Falls keine Eingabe erfolgte (Abbruch), weiter im Sowilo Zweig
ᛊ  ᛣprint ᛕ1 ᛇ Die Eingabe wurde abgebrochen ᛩ ᛉ    

᛭ Eine Eingabe wurde erfolgreich durchgeführt: weiter im Siegel Zweig
ᛋ  ᛣa_gt_b ᛕ0             ᛭ Vergleichsoperator 0 > eingabe
   ᛊ print ᛇ Fehler: Der Wert auf dem Stack ist keine Zahl und kann nicht verglichen werden !ᛩ ᛉ
   ᛋ ᛭ Auf dem Stack liegt das Ergebnis von 0 > eingabe
     ᛣifElse    

     ᛭ Wenn eingabe < 0 ist, dann mit -1 multiplizieren          
     ᛋ ᛣmul ᛕ-1ᛉ          

     ᛭ Wenn eingabe >= 0 ist, dann mit 1 multiplizieren
     ᛊ ᛣmul ᛕ1ᛉ           
     ᛉ   
   ᛉ
ᛉ ᛭ Hier kann nun der absolute Betrag auf dem Stapel

ᛣprint ᛕ2 ᛇ Der absolute Betrag |z| = ᛩ
ᛊ print ᛕ1 ᛇ Der Stapel ist leer ᛩ
ᛉ

```
### Abrufen des Ergebnis- Stapelspeichers als Array ᚥ 

Der gesamte Stapelspeicher kann in einem  **ᛋ**, **ᛊ** und **ᛉ** Zweige als das spezielle Array **ᚥ** abgegriffen werden. Mittels **ᛥᚥ** Parallel- Zugriffsoperator können einzelne Elemente herausgegriffen und gezielt weiterbearbeitet werden. Zur Laufzeit wird jedes herausgegriffenen Element in einem eigenen *Laufzeittask* bearbeitet- der **ᛥᚥ** ist damit das primäre Instrument zur Parallel- Programmierung.
**ᛥᚥ** hat folgende Signatur:

```
ᛥᚥ _Index1_ [_index2 [ ... [index n]]]
ᛋ _meth_für_Zweig1_   ᛭ Methode, die auf den Wert mit Index 1 aus ᚥ angewendet wird
ᛋ _meth_für_Zweig2_   ᛭ Methode, die auf den Wert mit Index 2 aus ᚥ angewendet wird
:
ᛋ _meth_für_ZweigN_   ᛭ Methode, die auf den Wert mit Index N aus ᚥ angewendet wird
ᛊ _meth_für_einen_outOfRange_Fehler_
ᛉ _Folgefunktion_
```
Der Wert zu jedem Index wird an einen korrespondierenden ᛋ Zweig geleitet, und kann dort mit einer Folge- Methode weiterbearbeitet werden.

Sollte ein Index außerhalb des Stapelspeicher- Array **ᚥ** liegen, dann wird kein ᛋ Zweig betreten, sondern nur der ᛊ Zweig. In diesem kann eine Fehlerbehandlung stattfinden.

**ᛉ** wird in jedem Fall am Ende durchlaufen. Hier kann eine Folgefunktion gestartet werden. Im Kontext der parallelen *Laufzeittasks* stellt hier **ᛉ** einen *Join* dar.

### Benennen von Ergebnissen einer Stufe

Alternativ zum Abruf und Weiterverarbeitung der Ergebnisse mit **ᚥᛏ** können die Einträge am Stufenausgang auch aus dem Stapelspeicher gelesen und benannt werden mit **ᛟ**:

```
             --+---+--+
ᛣ m a b c    a | b | c| -----+----+     ᛭ Ablage der Parameter auf dem Stapelspeicher
             --+---+--+      |    |
             ------------+   |    |
 +--- ᛋ s     m(a, b, c) | <-+    |     ᛭ Ergebnis der Methode im ᛋ Zweig bereitstellen
 |           ------------+        |
 |           ------------+        |
 | +-- ᛊ e    m(a, b, c) | <------+     ᛭ Ergebnis der Methode im ᛊ Zweig bereitstellen
 | |         ------------+             
 | |  
 | |          ----------------------------------+
 +-+-> ᛉ ᛟres  s(m(a, b, c)) oder e(m(a, b, c)) |  ᛭ Ergebnis aus ᛋ oder ᛊ an Namen res binden 
              ----------------------------------+   
```

Das benannte Ergebnis kann dann im Folgenden weiterverwendet werden:

```
ᛟa ᛕ2
ᛟb ᛕ3

ᛣsqu ᛡa  
ᛉ ᛟaa

ᛣsqu ᛡb  
ᛉ ᛟbb

ᛣadd ᛡaa ᛡbb  
᛭ Weiterleiten des Ergebnisses an die Print- Methode. Achtung: Im AusgabeString findet
᛭ String- Interpolation statt.
ᛉ print ᛇ ᛡa² + ᛡb² = ᛩ
```

#### Benennungen innerhalb von ᛋ und ᛊ Zweig

Eine Benennung inner halb eines ᛋ und ᛊ Zweiges ist nur lokal innerhalb dieses sichtbar. 
```
ᛟaa ᛕ2

ᛣsquRoot ᛡaa  
᛭ ᛟa_lok ist nur innerhalb des Siegel - Zweiges sichtbar
ᛋ ᛟa_lok ᛣprint ᛇ ᛡa_lok ist die Wurzel aus ᛡaa ᛩ
ᛊ ᛣprint ᛇ ᛡaa ist  keine reele Quadratzahl! ᛩ ᛉ
  ᛣpush ᛕ0 ᛉ
᛭ ᛟa_glob ist für den gesamten Kontext sichtbar, innerhalb dessen squRoot aufgerufen wurde
ᛉ ᛟa_glob

```

## Benennen von Datenflussgraphen: Module ᛖ ... ᛗ

Komplett ausprogrammierte Datenflussgraphen können zwecks Wiederverwendung in Modul- Deklarationen eingeschlossen werden. 

Eine Moduldeklaration ist ein Block, der zwichen **ᛖ** und **ᛗ** eingeschlossen wird. Dem **ᛖ** folgt der Name des Moduls: 

```
ᛖ modulName
 ᛭ Hier wird der Wiederzuverwendende Datenflussgraph definiert. 
ᛗ
```
Das Modul kann dann später wie eine elementare Datenverarbeitungsstufe mit den Zweigen **ᛋ** und **ᛊ** verwendet werden. Wann **ᛋ** und wann **ᛊ** aufgerufen werden, kann innerhalb des Moduls mit **ᛋᛏ** und **ᛊᛏ** definiert werden:

```
ᛖ divKardinal
 ᛭ Hier wird der Wiederzuverwendende Datenflussgraph definiert. 
 ᛣpop ᛜ ᛕᛠ ᛕᛠ 
 ᛊ ᛣpush ᛇ Err divKardinal ᛩ
  ᛊᛏ
 ᛉ ᛟNom ᛟDenom

 ᛣifElse ᛜ ᛕᛠ 

 ᛉ

ᛗ
```

## Von der Laufzeitumgebung bereitgestellte Stufen

Die Laufzeitumgebung hat bereits eine Reihe von Stufen vordefiniert und implementiert. 

### Programende

Diese Stufe beendet in jedem Fall das Programm.

```
ᛣfinᛉ
```

### Fehlerlog

Diese Stufe gibt die als Eingang E1 vorliegende Meldung und den aktuellen Stapelspeicherinhalt in einem Fehlerlog aus.
Nach Ausführung dieser Stufe ist der Stapelspeicher in genau dem gleichen Zustand wie vor der Stufe.

```
ᛣlogErr ᛇ FEHLERMELDUNG ᛩᛉ
```

### Infolog

Diese Stufe gibt die als Eingang E1 vorliegende Meldung und den aktuellen Stapelspeicherinhalt in einem Info- Log aus.
Nach Ausführung dieser Stufe ist der Stapelspeicher in genau dem gleichen Zustand wie vor der Stufe.

```
ᛣlogInf ᛇ FEHLERMELDUNG ᛩᛉ
```

### Stapelspeicher mit Werten füllen

Mit dieser Stufe kann eine Liste von Werten auf den Stapelspeicher gelegt werden. Eine weitere Bearbeitung der Werte auf dem Stapelspeicher findet nicht statt.

Nachfolgende Stufen können die unveränderten Werte auf dem Stapelspeicher dann weiterverarbeiten. 

```
                Inhalt Stapelspeicher
                   --+---+--+
ᛣ push  a ... z    a |...| z|
                   --+---+--+
             ⇠ ᛋ   ↵   
             ⇠ ᛊ   ↵
             ↳ ᛉ
```

### Alternative Verarbeitung ifElse

Diese Stufe nutzt die Stuktur der alternativen Ausführungspfade ᛋ und ᛊ aus, um eine elementare Verzweigung in Abhängigkeit eines boolschen Wertes zu implementieren.

Der Eingangsparameter von **ifElse** muss ein boolscher Wert sein. Ist er True, dann wird der **ᛋ** Zweig, sonst der **ᛊ** ausgeführt. Am Ende wird wieder im **ᛉ** Zweig zusammengeführt.

```
ᛣifElse _boolscherEingang_
ᛋ _Folgestufe_if_TRUE_   
ᛊ _Folgestufe_if_FALSE_  
ᛉ _FolgeStufe_von_ifElse_
```


### Datenstrom- orientierte Ausgabe

Es können Ausgaben in Dateien erfolgen. Dazu sind diese in einer Stufe zuerst als Datenströme zu öffnen, und dann können Teile des Stapelspeichers in diese ausgegeben werden.
```
ᛣout _name_output_Stream_  _E1_ ... _En_
ᛊ _Verarbeitungsstufe_falls_Ausgabe_scheitert_
ᛉ 

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

