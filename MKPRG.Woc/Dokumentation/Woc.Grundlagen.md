# Woc

(C) Martin Korneffel, Stuttgart im Winter 2011/12, Herbst 2012, März 2013, September 2015, Februar 2021

## Problemstellung

Das Editieren von Dokumenten in Netzwerken ist problematisch. Denn ein editierbares Dokument kann im gleichen Zeitraum auf verschiedenen Arbeitsstationen geändert werden, so dass letztendlich verschiedene Versionen ein und desselben Dokumentes entstehen.

Ein weiteres Problem ist die Publikation. Werden Dokumente z.B. mittels Web- Server publiziert, dann sind diese an eine fest strukturierte Serverwelt gebunden. Z.B. referenziert ein absoluter Hyperlink einen Server und auf diesem einen Inhalt. Verändert sich die Serverlandschaft, dann werden die Links ungültig.

In den Grundeinstellungen werden Dokumente auf den Computern unverschlüsselt gespeichert und unverschlüsselt im Netz ausgetauscht. Das ist eine elementare Sicherheitslücke

## Lösung Woc

Die Probleme können durch ein spezielles Regelwerk für die Dokumentverwaltung  gelöst.

### § 1: Isolation der parallelen Arbeiten an einem Thema in *Kontexten*

Die Bearbeitung eines Themas/Inhaltes kann in einem Netz gleichzeitig an mehreren Orten durch ein und denselben, oder auch mehrere Autoren erfolgen. Um von vornherein Datenverluste durch mögliches gegenseitige Überschreiben zu vermeiden, werden die verschiedenen Arbeitsstände an ein und denselben Inhalt im Netz durch sogenannte *Kontexte* voneinander isoliert.

Ein *Kontext* ist dabei ein Paar `Ctx\Node: U Author: X` wobei `Node: U` einen Netzwerkknoten (Rechner) mit dem netzweit eindeutigen Namen **U** ist. Der Namen ist z.B. ein 64bit *GUID*.

Der *Author* ist das den Inhalt bearbeitende Subjekt (Mensch, Roboter etc.). Auch dieser wird durch einen netzweit eindeutigen Namen **X** bezeichnet. Auch dieser kann z.B. ein 64bit *GUID* sein.

### § 2: Netzweit eindeutige Bennenung eines Themas/Titels durch eine **WocId**

Jedem Thema/Titel, zu dem Inhalte im Netz erstellt werden, wird eine netzweit eindeutige Nummer, **WocId** genannt, eineindeutig zugeordnet. Diese **WocId** kann z.B. ein 64bit GUID sein.

Die Zuordnung erfolgt dabei durch einen **Author X** auf einem **Node U** (= *Kontext*), indem er zu dem Thema/Titel erstmalig im Netz einen Inhalt in Form eines **Woc** bereitstellt. 

### § 3: Versionierte Speicherung der Inhalte in einem **Woc**

**Woc** ist das Akronym für <u>W</u>eb D<u>oc</u>uments.

Ein **Woc** erhält bei der Erzeugung die eindeutige **WocId** und speichert zudem noch den *Kontext* ab, in welchem es erzeugt wurde. 

In einer Implementierung könnte das **Woc** den Inhalt direkt speichern, in einer anderen enthält es nur einen Verweis auf den Inhalt (der z.B. als ein Word- Dokument in einem Filestore liegt). In beiden Fällen wird im weiteren Verlauf vom im Woc "gespeicherten" Inhalt gesprochen.

Der im Woc gespeicherte Inhalt erhält eine *Versionsnummer*. Wird der Inhalt geändert, dann bleibt der alte erhalten, und der neue wird mit einer neuen *Versionsnummer* hinzugefügt.

### § 4: Beschränkung der Inhaltsänderungen auf den *Kontext*, in dem *Woc* erzeugt wurde

Der Inhalt eines *Woc* darf nur in dem Kontext bearbeit und als neue Version diesem hinzugefügt werden, in welchem das *Woc* erzeugt wurde.

### § 5: Abspaltung (*branch*) von der aktuellen Entwicklungslinie des Inhaltes zwecks Weiterbearbeitung in einem anderen Kontext

Wenn der Inhalt in einem anderen Kontext (Node &#x2260; **U** oder Author &#x2260; **X**) weiterbearbeitet werden soll



### Durch ein **Woc** einen Inhalt für eine WocId in einem Kontext bereitstellen

In einem Kontext U



1. 
1. Ein **Woc** definiert einen, durch eine *WocID* eindeutig referenzierten Dokumententitel. **Woc** ist das Akronym für <u>W</u>eb D<u>oc</u>uments.
2. Sequenzielles erzeugen von Wocs auf einem **Node**: Jedes Woc wird von einem **Autor** auf einem benannten Netzwerkknoten (**Node**) zu einem bestimmten Zeitpunkt erzeugt. Ein Autor kann auf einem Netzwerkknoten niemals gleichzeitig mehrere Wocs anlegen. Damit 
3. Sei **Node U** != **Node V**. Ein Woc X auf einem Node A ist identisch mit einem Woc Y auf einem Node B, wenn Woc X.WocID = Woc Y.WocID gilt
4. Die WocID ist ein Tupel mit folgendem Aufbau: 
WocID := (TitelID, AutorID, NodeID, ThronfolgerNr, DynastieID)
Die WocID- Komponenten können beliebig implementiert werden wie  64 bit Werte oder Zeichenketten. Im Folgenden werden die einzelnen Komponenten erläutert
1. Die TitelID klassifiziert den Inhalt, für den das Woc steht, durch eine Liste von Schlüsselworten. Z.B. T_.Woc.System.Definition
2. Die AuthorID bezeichnet die für den Inhalt verantwortliche Person. 
Z.B.: Martin Korneffel → A_.Martin.Korneffel
3. Die NodeID bezeichnet den Netzwerkknoten (Server, PC, Mobilgerät) auf dem das Woc erstellt wurde: z.B.: SRV_01 → N_.SRV_01
4. Der Zeitpunkt t, zu dem ein Woc angelegt wird, wird eindeutig auf einen ThronfolgerNr s ϵ NN abgebildet, wobei gilt:  ti = tj → si = sj ˄ ti < ti+n → si < si+n
Z.B. 2017-01-17 10:00:00 → 1000,   2017-01-17 10:00:01 → 1010
5. Eine Dynastie ist eine Folge von Wocs, welche die gleiche TitleID, AuthorID, NodeID, und DynastieID, jedoch verschiedene Nachfolger haben. Die  DynastieID ist die kleinste ThronfolgerNr aller Wocs innerhalb der Dynastie.
5. Der Gründer einer Dynastie ist das Woc, für das gilt: ThronfolgerNr = DynastieID
6. Erzeugt ein Autor auf einem Node für eine TitleID zum ersten Mal ein Woc, dann ist es der Gründer einer Dynastie. Die DynastieID ist gleich der ThronfolgerNr dieses Wocs.
7. Erzeugt ein Autor auf einem Node für eine TitleID wiederholt  ein Woc, dann kann er es der bestehenden Dynastie hinzufügen. In diesem Fall ist die DynastieID des neuen Wocs gleich der DynastieID des Gründers. Entschließt sich der Autor, eine neue Dynastie zu gründen, dann wird das neue Woc selbst zum Gründer (siehe 6).


## Lösung WocServer

Die Probleme können durch einen speziellen Dienst, WocServer genannt, gelöst werden, der auf folgender Definition eines Dokumentes aufbaut: 
Ein Dokument ist ein Datensatz (z.B. Datei oder Menge von Entities aus einer Datenbank), deren Inhalt nach dem Akt der Versiegelung nicht mehr geändert wird/ werden kann.
Der unveränderbare Inhalt von Dokumenten nach dieser Definition vereinfacht die Replikation der Dokumente auf Knoten in einem Netzwerk, erfordert aber zwingend eine Versionsverwaltung. Denn jede Änderung an einem Inhalt aus einem Dokument führt zur Erzeugung eines neuen Dokumentes. Falls technisch möglich, könnten solche Änderungen differentiell in den neuen Dokumenten abgelegt werden, so dass ein vollständiges Dokument einer bestimmten Version aus allen Vorläufer - Versionen zusammengesetzt werden muss. Dies wäre aber nur eine Option.
Für das WocServer- System ergeben sich folgende Anforderungen:
1. Eindeutige Dokument- ID
Jedes Dokument bekommt eine eineindeutige ID (z.B. GUID). Über diese ID kann das Dokument im gesamten Netzwerk identifiziert werden. Wird auf einem Knoten im Netz unter der ID ein Dokument gefunden, dann garantiert das Woc- Serversystem, dass unter derselben ID auf allen anderen Knoten nur 1:1 Kopien desselben Dokumentes gefunden werden können.
Weiter unten wird ein Verfahren beschrieben, mit dem die WocID aus den Komponenten <Doc_ID><WocID_Author><WocID_Origin><Version> erzeugt wird.

2. Delegieren der Dokumentspeicherung an Applikationen
Der WocServer beschränkt sich auf die Aufgabe, die Implementierung verteilter Dokumentenverwaltungssysteme zu vereinfachen.
Deshalb speichert der WocServer nicht die zu verwaltenden Dokumente. Diese Aufgabe übernehmen dafür spezialisierte Applikationen wie z.B. WebApps für den gesicherten Zugriff auf Dateifreigaben (z.B. Technik-Beton) oder das CMS einer Webseite.
3. Speichern von Merkmalen eines Dokumentes in sog. Woc- Containern.
Aus der in 2. definierte Einschränkung folgt eine radikale Reduktion von Informationen, die pro Dokument auf dem WocServer gespeichert werden müssen. Diese werden in Woc's abgelegt. WOC ist das Akronym für Web Documentcontainer. Für jedes Dokument existiert auf dem WocServer ein Woc- Container.
Ein Woc hat folgenden prinzipiellen Aufbau: