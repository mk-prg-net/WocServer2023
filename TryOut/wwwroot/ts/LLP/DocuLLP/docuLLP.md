# 𝓛𝓛𝓟: Łukasiewicz List Processor

**LLP** soll eine minimalistische formale Sprache zur semantischen Auszeichung von Texten, zur funktionalen Formulierung von Algorithmen und zur generatorischen Beschreibung von Diagrammen und Bildern werden.

Eine wesentliche Rolle spielen dabei **Namenscontainer** mit semantischen Beziehungen

## Beispiele 

### Einen Namingcontainer referenzieren

Sei **milDiscCircular** ein Namenscontainer, der eine Familie von Fräsprogrammen benennt, die Kresischeiben aus einer flachen Platte fräsen. Dann kann dieser Namenskontainer wie folgt referenziert werden:

```
ᛞ milDiscCircular
```
Diese Referenz kann selber zur Benennung von LLP Strukturen wie Instanzen etc dienen.

### Einen Namenscontainer definieren

```
ᛖ ᛞdefNamingContainer
    ᛜ ᛞ nid 0x1234567890
    ᛜ ᛞ cnt milDiscCircular
    ᛜ ᛞ lngDE 'Ein Fräsprogramm zum erstellen einer Kreisscheibe auf einer Platte'
    ᛜ ᛞ lngEN 'A milling Program for milling a circular disc.'
ᛩ
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

Die Semantischen Beziehungen können z.B. durch Funktionsausdrücke dargestellt werden:

```
ᚪ ᛞ isInstanceOf
    ᛜ ᛞsemRefReferring ᛞ milDiscCircular
    ᛏ ᛞ milProgram
```

Eine Kurzform für diese Definition semantischer Referenzen ist sinnvoll. `ᛣ _NID_Referring_ _NID_SemRefName_ _NID_Related_` ist dann das Äquivalent zum Ausdruck 

```
ᚪ _NID _SemRefName_
    ᛜ ᛞsemRefReferring _NID_Referring_
    ᛏ _NID_Related_
```
Damit kann das obige Beispiel vereinfacht werden zu:

```
ᛣ ᛞ milDiscCircular ᛞ isInstanceOf ᛞ milProgram

```

#### Abfragen der semantisch referenzierten Instanz

```
ᛣ ᛞ _NID_Referring_ ᛞ _NID_SemRefName_ ᛏ

```

Beispiel: Bestimmen, mit wem alles **milDiscCircular** in der semantischen Beziehung **isInstanceOf** steht:

```
ᛣ ᛞ milDiscCircular ᛞ isInstanceOf ᛏ
```

#### Abfragen der semantisch referenzierenden Instanzen

```
ᛣ ᛞ ᛏ ᛞ _NID_SemRefName_ ᛞ _NID_Related_

```

Beispiel: Bestimmen der Instanzen von **milProgram**:

```
ᛣ ᛏ ᛞ isInstanceOf ᛞ milProgram
```

### Fräsprogramm für einen Kreis

```
ᛞ milCirc ᛝ   
   ᛜ ᛞ milCircCx ᚪ ᛞ measureDistanceMillimeter ᛏ ᚱ 0.0
   ᛜ ᛞ milCircCy ᚪ ᛞ measureDistanceMillimeter ᛏ ᚱ 0.0
   ᛜ ᛞ milCircRadius ᚪ ᛞ measureDistanceMillimeter ᛏ ᚱ 100.0
   ᚪ ᛞ milCircNext ᛏ ᚱ 100.0
```
