# File ᛯ Mesher 

Martin Korneffel, 6.2.2024

**File ᛯ Mesher** ist das System der Dateiablage von **Stack ᛝ Flow** Dateien. 

**Stack ᛝ Flow** Dateien bilden Module, die in Namensräumen eingeschlossen sind. Der Name eines solchen Moduls ist stets ein global eindeutiger NID.

```
᛭ Jede Stack Flow Datei beginnt mit der Bindung eines globalen NID an einen Namensraum.
ᛟᚻᛕ ᛔ16 1657111ACE7B20BD
ᚹ
   ᛭ Hier wird ein Moniker für den NID der Stack Flow Datei definiert.
   ᛟFileNidMoniker BasicMathFunctions
   ...
ᛩ
```
Aus einer **Stack ᛝ Flow**- Datei können auf die Namen aus anderen **Stack ᛝ Flow** Dateien einfach referenziert werden via NID

```
᛭ Jede Stack ᛝ Flow Datei beginnt mit der Bindung eines globalen NID an den Datei- internen Namensraum.
᛭ Der NID ist der eindeutige Namen der Datei.
ᛟᚻᛕ ᛔ16 1657111ACE7B20BD
ᚹ
   ᛭ Hier wird ein Moniker für den NID der Stack ᛝ Flow Datei definiert.
   ᛟFileNidMoniker BasicMathFunctions

   ᛭ Hier wird der Inhalt einer anderen Stack ᛝ Flow eingebunden
   ᛟconsants ᛡᚻᛕ ᛔ16 AF123456789

   ᛭ Namen aus anderer Stack ᛝ Flow Datei auflösen
   ᛟPI ᛡᚠconstants PIᛩ
   ...
ᛩ

᛭ 2. Datei
ᛟᚻᛕ ᛔ16 AF123456789
ᚹ
   ᛭ Hier wird ein Moniker für den NID der Stack ᛝ Flow Datei definiert.
   ᛟFileNidMoniker MathConstants

   ᛭ Hier wird der Inhalt einer anderen Stack ᛝ Flow eingebunden
   ᛟPI ᚪ3 14
   ᛟe  ᚪ2 72   
   ...
ᛩ
```

Wenn der Moniker einer **Stack ᛝ Flow**- Datei über alle Dateien eindeutig ist, dann kann auf die Bindung der aufgelösten NID für eine andere Stack- Flow Datei verzichtet werden- dies übernimmt dann implizit das Laufzeitsystem:
```
ᛟᚻᛕ ᛔ16 1657111ACE7B20BD
ᚹ
   ᛭ Hier wird ein Moniker für den NID der Stack ᛝ Flow Datei definiert.
   ᛟFileNidMoniker BasicMathFunctions

   ᛭ Namen aus anderer Stack ᛝ Flow Datei auflösen
   ᛟPI ᛡᚠMathConstants PIᛩ
   ...
ᛩ
```

## Vereinfachung der Dateinamenszuweisung und Auflösung
Das Einkapseln des kompletten Dateiinhaltes in eine Namensraumliste, die einem NID als Dateinamen zugewiesen wird, ist syntaktisch exakt, jedoch im alltäglichen Gebrauch unhandlich.

Deshalb wird folgende Kurzform vereinbart:

1. In der ersten Zeile einer Datei wird die hexadezimale NID, und danach, getrennt durch ein Leerzeichen der Moniker für den NID definiert.
2. Der Moniker für den NID kann auch eine Hierarchie an Namen darstellen (hierarchischer Namespace)
3. Ab der Zeile 2 gilt der Datei als eine Namensraum- Liste (ᚹ und ᛩ werden implizit an Zeile 2 und am Dateiende gesetzt)

```
ᚻᛕ ᛔ16 1657111ACE7B20BD BasicMathFunctions
᛭ Die erste Zeile hat den NID und den Moniker definiert. Der Moniker ist optional.

᛭ Namen aus anderer Stack ᛝ Flow Datei auflösen, wobei anstatt der NID ein global eindeutiger Moniker benutzt wird.
ᛟPI ᛡᚠMathConstants PIᛩ
...
```

## Klassische Dateiablage vs File ᛯ Mesher 

Typischerweise werden Dateien in einem Dateisystem abgelegt. Sie werden dabei Dateiordnern zugewiesen. Die Dateiordner selber können zueinander in einfachen, hierarchischen Beziehungen stehen.

Im **File Mesher** stehen die Dateien in vielfältigen semantischen Beziehungen, die im Dateikopf definiert werden.
Beispiele:

```
᛭ Dateikopf:
ᛟᚻᛕ ᛔ16 1657111ACE7B20BD
ᚹ
   ᛟFileName milRotor
   ᛭ Zuerst wird für die Datei einen NID definiert, dem der Name der Datei zugewiesen ist     

ᛩ


᛭ Als nächstes wird der Namensraum festgelegt, in dem der oben definierte Name (unabhängig von der NID) eindeutig sein muss.
᛭ Der Namensraum korrespontiert 1:1 mit einer Ordnerhierarchie im Dateisystem.
ᛟNameSpace ᚠ ᚻ millingMachine ᚻ millingPrograms ᚻflatMilling ᚻ millDisc ᛩ

᛭ Dann werden weitere semantischen Beziehungen definiert. Der erste Parameter von ᛯ ist die NID der Beziehung selber, der zweite das referenzierte Objekt.
ᛯ ᚻisPartOf ᚻQuerstromturbine
ᛯ ᚻisInstanceOf ᚻmilPrograms
...
```

## Semantische Referenzen zwischen Namensraum- Listen

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

```
ᛯ ᚻmilDiscCircular ᚻisInstanceOf ᚻmilProgram
           |               |          |
    NID referring       sem Rel   NID referred

```

### Abfragen der semantisch referenzierten Instanz

```
ᛯᛏ _NID_Referring_ _NID_SemRefName_ 

```

Beispiel: Bestimmen, mit wem alles **milDiscCircular** in der semantischen Beziehung **isInstanceOf** steht:

```
ᛯᛏ ᚻmilDiscCircular ᚻisInstanceOf 
```

### Abfragen der semantisch referenzierenden Instanzen

```
ᛯᛏ _NID_SemRefName_ _NID_Related_
```

Beispiel: Bestimmen der Instanzen von **milProgram**:

```
ᛯᛏ isInstanceOf milProgram
```