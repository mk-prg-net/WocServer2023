#DocuTerm Composer

## DocuTerms

**DocuTerms** dienen zur streng formalisierten  Dokumentation von Fehler-, Warn- und Hinweismeldungen in Ausnahmen und Rückgabewerten von Funktionen.

Ein Programmzustand wie ein mißglückter Methodenaufruf kann durchh einen *DocuTerm* direkt ausgedrückt werden:

``````
    #m Methodenname 
        #_ 
            #p Parametername_1 Parameterwert_1
            :
            #p Parametername_N Parameterwert_N

            #ret
                #_
                    #e fails
                #.
        #.
``````        

Zum Beispiel kann eine Fehlgeschlagene Validierung eines Funktionsparameters (arg1 sollte größer 100 sein) wie folgt ausgedrückt werden:

``````C#
    dct.m(TechTerms.Validation.mValidate,
        // Anzeige, das Validierung gescheitert ist
        dct.ret(dct.eFails(
            // Vorbedingung als boolsche Funktion, welche nicht erfüllt wurde (z.B. muss arg1 >= 100 sein)
            dct.i(TechTerms.Validation.iPreCondition,        
                dct.m(TechTerms.RelationalOperators.mGtEq,                          
                    dct.p(TechTerms.MetaData.Arg, "arg1"),
                    dct.p(TechTerms.MetaData.Val, 100),
                    dct.ret(false)))))));

``````
Um Programmzustände zu beschreiben können die aktiven Laufzeitobjekte wie *Objekte*, *Methoden* und *Ereignisse*  direkt durch *DocuTerms* beschrieben werden.

In **C#** werden die **DokuEntities** mittels der **MKPRG.Tracing.DocuTerms.Parser.DocuEntites.Composer** Bibliothek im Programm erzeugt.

## Struktur von *DocuTerms*

Die *DocuTerms* werden durch Objekte implementiert, die die Schnittstelle **MKPRG.Tracing.DocuTerms.IDocuEntity** implementieren:

``````C#

interface IDocuEntity {
   DocuEntityTypes EntityType {get;}
   IEnumerable<IDocuEntity> Childs {get;}
}

enum DocuEntityTypes { Name, Instance, Property, PropertySet, Version, Event, Method, List, String, Text, Date, ReturnValue, KillIfNot, none, ListToEmbed};

``````
*DocuTerms* bilden eine Baumstruktur.


## Analyse von *DokuTerms*

Meldet ein Unterprogramm mittels *DocuTerms* den Erfolg oder Misserfolg seines Aufrufes zurück, dann stellt sich die Aufgabe der Analyse der komplexen hierarchichen *DocuTerm* Bäume. Um die Aufgabe zu erleichtern, werden folgende Werkzeuge bereitgestellt.

### DocuEntityLinqDeco

Stellt einen Decorator für IDocuEntity- Objekte bereit. Das IDocuEntity- Objekt wird um Eigenschaften erweitert, welche potentielle Member wie *Instanzen*, *Eigenschaften*, *Methoden* auflisten. 

### DocuEntityHlp

Hier werden Erweiterungmethoden für die Schnittstelle **IDocuEntity** angeboten, mit denen in denen im durch das DocuEntity aufgespannten Baum nach bestimmten Strukturen gesucht werden kann.



#### IsSubTreeOf

Prüft, ob eine DocuTerm als Teil in einem anderen enthalten ist.

##### Wann passt ein Pattern auf eine Methode

Wenn ein Pattern auf eine Methode passen sollt, dann muss der Methodennamen des Patterns mit der Methode übereinstimmen. Hat das Pattern Parameter, dann müssen diese auch in der passenden Methode vorhanden sein. Dabei ist die Rehenfolge der Parameter von Bedeutung.
*Wildcards* haben in Methodenpattern keine Bedeutung.

##### Wann passt ein Pattern auf eine Instanz

Wenn ein Pattern auf eine instanz passen soll, dann muss der Name des Patterns mit dem Instanzname übereinstimmen. Enthält das Pattern Member, dann müssen diese auch in der Instanz vorhanden sein. Die Reihenfolge ist ohne Bedeutung.
*Wildcards* haben in Instanzpattern keine Bedeutung.

##### Wann passt ein Pattern auf eine Eigenschaft

Ein Pattern passt auf eine Eigenschaft, wenn das Pattern eine Eigenschaft ist, der Name der Eigenschaft im Pattern mit der zu untersuchenden Eigenschaft übereinstimmen und der Wert des Patterns mit dem Wert der Eigenschaft übereinstimmt.
Ist der Wert des Patterns ein *Wildcard* (z.B. `pnL.p("prj", pnL._())`), besteht stets Übereinstimmung mit dem Wert der Eigenschaft.
Ist der Wert des Patterns ein *Wildcard* mit einer Subtreeeinschränkung (z.B. `pnL.p("prj", pnL.p_(pnL.i("Station", 99)`), dann besteht Übereinstimmung mit jedem Wert, der diesen Subtree enhält.

##### Wann passt ein Pattern auf ein Event

Analog Eigenschaft.

#### Wann passt ein Pattern auf einen Return- Wert

Analog Eigenschaft.

``````C#
    // Hier wird nach einem DocuTerm erzeugt, der eine gescheiterte Suche 
    // nach Datensätzen mit der Projektnummer 2998 ausdrückt.
    var tree = dct.i(TechTerms.Dfc.Project,
                    dct.p(TechTerms.Dfc.Project, "2998"),
                    dct.p("Stations", dct.ReturnSearchWarnEmptyResult(
                                            dct.m(TechTerms.RelationalOperators.mEq,
                                                dct.p("col", "Project"),
                                                dct.p("val", "2998")))));



    // Hier wird nach einem Teilbaum gesucht, welcher erzeugt wird, wenn eine Abfrage 
    // keine Datensätze zurückliefert (leere Menge). Als Grund wird der gescheiterte 
    // Vergleich mit der Projektnummer 2998 genannt. 
    var subTree = dct.ReturnSearchWarnEmptyResult(
                    dct.m(TechTerms.RelationalOperators.mEq,
                        dct.p("col", "Project"),
                        dct.p("val", "2998")));

    bool res = subTree.IsSubTreeOf(tree);
    Assert.IsTrue(res);

    // Hier wird nach einem Teilbaum gesucht, welcher erzeugt wird, wenn eine Abfrage 
    // keine Datensätze zurückliefert (leere Menge). Als Grund wird der gescheiterte 
    // Vergleich mit der Projektnummer 2999 genannt. 
    var subTree2 = dct.ReturnSearchWarnEmptyResult(
                    dct.m(TechTerms.RelationalOperators.mEq,
                        dct.p("col", "Project"),
                        dct.p("val", "2999")));

    // Weil ein Vergleich mit einer Projektnummer 2999 im DocuTerm nicht vorkommt,
    // scheitert die Suche nach dem Subtree
    res = subTree2.IsSubTreeOf(tree);
    Assert.IsFalse(res);

    // Hier wird nach einem Teilbaum gesucht, welcher erzeugt wird, wenn eine Abfrage 
    // keine Datensätze zurückliefert (leere Menge). Weitere Details (Grund) werden nicht 
    // betrachtet
    var subTree3 = dct.ReturnSearchWarnEmptyResult();

    // subTree3 ist tatsächlich ein Teilbaum
    res = subTree3.IsSubTreeOf(tree);
    Assert.IsTrue(res);
``````

## Wildcard _



## Return Status Descriptor

Ein **Return Status Descriptor** ist eine strukturierte (=baumartige) Beschreibung des Ergebnisses eines 
Funktionsaufrufes. Z.B. ist die Eigenschaft MessageEntity eines RC Objektes ein **Return Status Descriptor**.







