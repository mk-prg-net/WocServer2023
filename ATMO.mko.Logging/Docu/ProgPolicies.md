# Richtlinien der C# Programmierung

## Entwurfsmuster mit C# Grundstrukturen implementieren

### n Bedingungen für die Ausführung einer Operation prüfen

`````` C#
if(!cond1)
{
    // Report Error cond1 violated
}
else if(!cond2)
{
    // Report Error cond2 violated
}
...
else {
    // Execute Operation
}

``````

## Funktionen unabhängig vom Aufrufkontext definieren

Eine Funktion f(...) sollte stets unabhängig von ihren möglichen Aufrufkontexten definiert werden.
Negatives Beispiel:

`````` C#
// Der Parameter fromLoginDialog zeigt an, das die Funktion aus dem Login- Dialog aufgerufen wird.
List<string> LoadCustGroups(bool fromLoginDialog) {...}

``````

Ist das Verhalten vom Aufrufkontext abhängig, dann sind für jeden Aufrufkontext separate Funktionen zu definieren:

`````` C#
// Der Parameter fromLoginDialog zeigt an, das die Funktion aus dem Login- Dialog aufgerufen wird.
List<string> LoadCustGroupsForLogin() {...}

List<string> LoadCustGroups() {...}

``````
Vorteil: Weniger zyklomatische Komplexität.

## Schnittstellenzeiger als Rückgabewerte

Eine Funktion f(...) -> r as T liefere für einen Aufruf einen Wert r vom Typ T zurück. r darf ein Schnittstellenzeiger sein bzw. T ist eine Schnittstellentyp, wenn folgendes gilt:

1. f ist keine Klassenfabrik (=> Klassenfabriken liefern stets einen konkreten Typ)

## Eigenschaften vermeiden, deren Getter einen hohen Rechenaufwand verursachen

Sei `Obj` ein Objekt mit einer Eigenschaft `E`, wobei der Getter von `E` beim Abruf einen hohen Rechenaufwand erzeugt. 

```c#
// Großer Aufwand
var value = Obj.E.get();

```

Wenn die Implementierung des Getters von `E` im Aufruf einer Funktion `f(a, b, ...)` besteht, wobei `a, b, ...` konstant sind, dann sollte
`E` im Entwurf wie folgt ersetzt werden:

1. Die Eigenschaft `E` mit dem aufwendigen Getter wird durch eine `E2` mit einem simpelen Getter substituiert, welcher nur die konstanten Parameter `a, b, ...` des Funktionsaufrufes der Berechnung liefert.
2. `f(...)`  wird zu einer asynchrone Methode, die einem Repositories `R` zugeordnet ist

Die Abruf des Eigenschaftwertes erfolgt nun in 2 Phasen:

``` c#
// Abruf der Parameter
var (a, b, ...) = Obj.E2.get();

// Abruf des Eigenschaftswertes über Repository
var value = await R.f(a, b, c);
```

## Eigenschaften, deren Getter berechnete Singletons sind, vermeiden

Beispiel: In einem Setup- Util wird die aktuelle Version einer Assembly über eine Eigenschaft bereitgestellt,
die die Version der Assembly über das Singleton Pattern verwaltet:
1. Prüfen, ob Versionsnummer bereits in einer lokalen Variable bereitsteht 
2. Wenn nicht, Abrufen der Versionsnummer zur Assembly und speichern in lokaler Variable
3. Wert aus lokaler Variable lesen und an Aufrufer zurückgeben

Vorteil: mehrfache Aufrufe verursachen nur beim ersten Abruf einen hohen Berechnungsaufwand.
Nachteil ist jedoch, das z.B. im Hintergrund die Assembly während der Laufzeit des Setup- Utils ausgetauscht werden
kann. Die als Singleton- implementiert Eigenschaft würde dies jedoch nicht notwendigerweise widerspiegeln. So kann
es zu Fehlfunktionen kommen.

Alternative:
1. Getter in eine Get- Methode umwandeln, z.B. `GetVersion()`. Diese ermittelt stets beim Aufruf die aktuell gültige 
Versionsnummer!
2. Den Rückgabewert der Get- Methode in einer lokalen Variable zwischenspeichern (var currentVer = GetVersion()`)
und aus dieser lesen, solange im Programm die gültige Versionsnummer zum Zeitpunkt des Aufrufes von `GetVersion()` 
von Interesse ist.

## Eigenschaften, die eine Liste von Schnittstellenzeigern zurückliefern

Der Zugriff auf Objekte in einer Menge, die eine Schnittstelle `Ix` implementieren, könnte durch folgende Eigenschaft definiert werden:

``` c#
interface ISet {
    // Ermöglich zugriff auf eine Menge von Objekten, welche die Schnittstelle Ix implementieren
    IEnumerable<Ix> SetOfObjectsImplementingIx {get;}
}
```

Bei der Implementierung von ISet ist zu beachten, dass z.B. Serialisierer nur Listen aus
konkreten Objekten und nicht Listen aus Schnittstellenzeigern unterstützen. Zudem sollte die Verwaltung einer Menge von Objekten vom Typ T niemals den Zugriff auf die Eigenschaften von T grundsätzlich einschränken. 
Deshalb wird folgendes Muster für die Implementierung von Schnittstellen wie ISet vorgeschlagen:

``` c#

using System.Runtime.Serialization;


[DataContract]
public class SetOfTs
    : ISet
{
    // Der Schnittstellenmember, der Zugriff auf Objektmenge als Aufzählung
    // von Schnittstellenzeigern liefert, wird explizit implementiert
    // -> Zugriff nur noch über Schnittstellenzeiger ISet möglich.
    // Zudem wird er von der Serialisierung ausgeschlossen
    [IgnoreDataMember]
    IEnumerable<Ix> ISet.SetOfObjectsImplementingIx => TSet;

    // Die tatsächlische Objektmenge wird als Array/Liste vom konkreten
    // Typ T implementiert, und explizit in die Serialisierung mit eingeschlossen.
    [DataMember]
    public T[] TSet  { get; set; }
    
    ...
}

// Typ T implementiert die Schnittstelle Ix
[DataContract]
public class T : Ix
{
    ...
}

```
## Zugriff auf mögliche Schnittstellen, die ein Objekt unterstütz könnte (23.9.2020)

Wenn ein Algoritmus, wie ein *Visitor*, durch einen Objektbaum läuft, dann könnten die Objekte eine Vielfalt von Schnittstellen implementieren. Der Zugriff auf diese sollte durch virtuelle Methoden im Algorithmus gekapselt werden, deren Standardimplementierung in der Eigenschaft **Succeeded** des zurückgegeben Datensatzes stets false anzeigt:

``` c#
void Visit(INode n){ 
    :
    if(n is INodeDetails nDetails){
       :
    }
    else {
        var (bool Succeeded, INodeDetails details) = await GetNodeDetails(n);

        if(Succeded){
            VisitNodeDetails(n);
        }
    }
    :
}

// Standardimplementierung der virtuellen Funktion
virtual Task<(bool, INodeDetails)> GetNodeDetails(INode n)
{
    return Task.FromResult((false, (INodeDetails)null));
}
```

Dieses Muster lässt es dem Entwickler völlig frei, wo die `INodeDetails` herkommen. Sie  könnte direkt im Node n implementiert sein, oder müssten erst aufwändig aus einer DB abgerufen werden- alles ist möglich.

Ein Antipattern hingegen wäre den Accessor auf `INodeDetails` bereit in der `INode` Schnittstelle vorzusehen:

``` c#
interface INode 
{
    :
    Task<INodeDetails> GetDetails();

}
```

Nicht in jedem Fall ist die GetDetails sinnvoll implementierbar, und müsste null zurückliefern. Zudem wird von vornherein konzeptuell die Anzahl möglicher Schnittstellen eines INode eingeschränkt, und die Art und Weise des Abrufes dieser unnötig eingeschränkt.

### Verwaltung von Erweiterungen eines Objekts (6.10.2020)
Erweiterung von einem Objekt0 :<=> Dekorator von Objekt0, der um genau **eine** Schnittstelle **I1** erweitert.
```
+----------+---\
| Object0  |I0  +-O
+----------+---/
     A
     |     +------------+---\
     +-----| refObject0 |I0  +-O
           +------------+---<
           | Data 1     |I1  +-O
           +------------+---/
```           

In Fällen, wo Objekte zur Laufzeit dynamisch um Schnittstellen erweitert werden sollen, bietet sich das im Folgenden beschriebene Pattern der **Erweiterungsliste** an:

``````
+----------+---\
| Object0  |I0  +-O
+----------+---/
   |    A
   |    | 
   |    +-----------------+
   |                      |
   |   +----+     +-------+----+---\
   +->>| Ij |---->| refObject0 |I0  +-O
       +----+     +------------+---<
                  | Data j     |Ij  +-O
                  +------------+---/
``````
Das zur Laufzeit mit Schnittstellen zu erwleiternde Objekt 0 erhält eine Liste, in welcher die Schnittstelle **Ij**, um die erweitert wird, einem **Erweiterungsdekorator** zugeordnet ist. Auf der Liste sollte typischerweise folgende Operatoren ausführbar sein (Bereits implementiert im Projekt ATMO.mko.ObjectModel):

``` c#

    /// <summary>
    /// mko, 14.10.2020
    /// Schnittstelle für eine Erweiterungsliste.
    /// Eine Erweiterungsliste verwaltet für ein Objekt Dekoratoren, die es um jeweiles eine zusätzliche Schnittstelle 
    /// erweitern (siehe Konzept Erweiterungslisten vom 6.10.2020)
    /// </summary>
    public interface IListOfObjectExtensions
    {
        /// <summary>
        /// true, wenn das Objekt, welches diese Schnittstelle implementiert,
        /// eine Erweiterungsschnittstelle vom Typ TInterface hat
        /// </summary>
        /// <typeparam name="TInterface"></typeparam>
        /// <returns></returns>
        bool HasObjectExtension<TInterface>();

        /// <summary>
        /// Lädt eine Objekterweiterung
        /// </summary>
        /// <typeparam name="TInterface"></typeparam>
        /// <returns></returns>
        TInterface GetObjectExtension<TInterface>();

        /// <summary>
        /// Versucht, eine Erweiterung der Liste der Erweiterungen hinzuzufügen. 
        /// Wenn erfolgreich, dann wird true zurückgegeben, sonst false.
        /// </summary>
        /// <typeparam name="TInterface"></typeparam>
        /// <param name="extensionObject"></param>
        /// <returns></returns>
        bool TryAddObjectExtension<TInterface>(TInterface extensionObject);

        /// <summary>
        /// Versucht, eine bestehende Erwieterung aus der Liste der Erweiterungen zu entfernen.
        /// </summary>
        /// <typeparam name="TInterface"></typeparam>
        /// <returns></returns>
        bool TryRemoveObjectExtension<TInterface>();        
    }
}

```
Beispiel der Anwendung ist der ATMO.DFC.Tree.Visitor- Algorithmus für Baugruppen. Hier wird geprüft, ob das Baugruppenobjekt Erweiterungslisten unterstüztz, und wenn ja, dann wird die Erweiterung, über die auf alle Stücklistenpositionen zugegriffen werden kann, geholt(Auszug):

```c#
if (assy is mko.Objectmodel.Extensions.IListOfObjectExtensions extendedAssy && extendedAssy.HasObjectExtension<IAssyBomItems>())
{
    var assyBomItemsExt = extendedAssy.GetObjectExtension<IAssyBomItems>();
    await VisitBomItems(assyBomItemsExt, ...);
}

```
Die dynamische Dekoratorerweiterung können zum Beispiel in einem speziellen Visitor- Algorithmus durchgeführt werden, bei dem der Visitor den Tree in der DB durchläuft, und dabei den Baum im Arbeitsspeicher mittels Erweiterungen aufbaut:

```c#
/// <summary>
/// mko, 7.10.2020
/// </summary>
/// <param name="assy"></param>
/// <param name="accessPermissionGranted"></param>
/// <returns></returns>
public override Task EndVisitAssy(IAssy assy, bool accessPermissionGranted)
{
    bool stopp = false;

    // Erweitern einer Baugruppe, falls diese Erweiterungen unterstützt.
    if (assy is mko.Objectmodel.Extensions.IListOfObjectExtensions ExtensionsOfAssy)
    {
        while (!stopp && dfcTreeObjectsStack.TryPop(out object obj))
        {
            if (obj is IAssyBomItems bomItems && !ExtensionsOfAssy.HasObjectExtension<IAssyBomItems>())
            {
                ExtensionsOfAssy.TryAddObjectExtension(bomItems);
            }
            else if (obj is IAssyInBomContext bomCtx && !ExtensionsOfAssy.HasObjectExtension<IAssyInBomContext>())
            {
                ExtensionsOfAssy.TryAddObjectExtension(bomCtx);
            }
            else if (obj is IAssy)
            {
                stopp = true;
            }
        }
    }
}
```


