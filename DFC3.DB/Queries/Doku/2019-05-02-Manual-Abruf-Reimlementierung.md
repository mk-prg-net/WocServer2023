
# Neuimplementierung der Manual- Dokumentnodes

Mko, 2.5.2019
Manuals sind Dokumente die Bedienungsanleitungen von Baugruppen und Einzelteilen enthalten. Sie werden von der MAT4 für die betroffenen Komponenten in DFC2 eingepflegt.
Ein Manual kann für mehrere Baugruppen gelten: 
``````
Manual <->> Baugruppe/Singlepart
``````
Manuals werden für mehrere Sprachen bereitgestellt:
``````
Manual <->> Sprache
``````
Dabei kann ein Manual Bedienungsanleitungen in nur einer, aber auch in mehreren Sprachen enthalten.

Diese Beziehungen werden in der Bosch106DOKUMAT- Tabelle implementiert:

|MatNr      | LAN | DOKUMAT
|-----------|-----|-------------
|0532004208 | DE  | 3843AH3238
|0532004208 | EN  | 3843AH3238

Häufig werden Bedienungsanleitungen für mehrere Sprachen in ein Dokument verpackt. Dieses erhält dann eine Dokumat- Nummer und wird dann in der Dokumat- Tabelle via Spalte LAISO den  Sprachen zugeordnet. Auch kann, wie das folgende Beispiel zeigt, eine Manual für verschiedene Baugruppen gelten:

| MATNR      |LAISO  | DOKUMATNR
|------------|-------|------------
| 0608800064 | DE    | 3609929254
| 0608800064 | EN    | 3609929254
| 0608800064 | ES    | 3609929254
| 0608800064 | FR    | 3609929254
| 0608800064 | IT    | 3609929254
| 0608800064 | PT    | 3609929254
| 0608800079 | DE    | 3609929254
| 0608800079 | EN    | 3609929254
| 0608800079 | ES    | 3609929254
| 0608800079 | FR    | 3609929254
| 0608800079 | IT    | 3609929254
| 0608800079 | PT    | 3609929254

Es gibt auch Fälle, in denen für jede Sprache ein eigenes Dokument existiert:

|MATNR      |LAISO |DOKUMATNR
|-----------|------|----------
|0608090625 |DE    |3843AH0511
|0608090625 |EN    |3843AH0512

Fall (10.7.2019): Dokument mit vielen Sprachverknüpfungen:
DokuMat- Nr: 3843AF9203
![DokuMat](http://10.4.4.53/DFC01/DfcDocu/issues/2019-07-10-dokumat-wiederholungen-sprachen-im-namen.png "Zu viele Sprachen im Namen")