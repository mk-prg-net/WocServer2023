# File ᛯ Mesher 

Martin Korneffel, 6.2.2024

## Klassische Dateiablage vs File ᛯ Mesher 

Typischerweise werden Dateien in einem Dateisystem abgelegt. Sie werden dabei Dateiordnern zugewiesen. Die Dateiordner selber können zueinander in einfachen, hierarchischen Beziehungen stehen.

Im **File Mesher** stehen die Dateien in vielfältigen semantischen Beziehungen, die im Dateikopf definiert werden.
Beispiele:

```
᛭ Dateikopf:
᛭ Zuerst wird der Name der Datei definiert, und einem Hex- NID zugewiesen
ᚻmilRotor ᛕ 16 1657111ACE7B20BD

᛭ Als nächstes wird der Namensraum festgelegt, in dem der oben definierte Name (unabhängig von der NID) eindeutig sein muss.
᛭ Der Namensraum korrespontiert 1:1 mit einer Ordnerhierarchie im Dateisystem.
ᚠ ᚻ millingMachine ᚻ millingPrograms ᚻflatMilling ᚻ millDisc ᛩ

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