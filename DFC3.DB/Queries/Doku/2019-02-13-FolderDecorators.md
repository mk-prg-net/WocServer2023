# Folder- neue Dekoratoren zum Organisieren von Dokumentmengen in DFC

Aktuell können z.B. Uploads von TDP’s nicht auf jedem Knoten innerhalb eines Projekts/Station/Baugruppe gestartet werden. Ursache: Die von der Gui verwendeten Datenstrukturen für die Knoten liefern falsche Daten, weil Eigenschaften, die Nummern liefern sollen stattdessen informelle Knotenbezeichnungen liefern.

SFC, EDC oder TDP- Masterknoten innerhalb eines Projekts/Station/Baugruppe sind vom Typ Dokumentknoten. Ihre Materialnummer, Projekt- und Stationsnummereigenschaften enthalten statt der Nummern informelle Beschreibungen der Knoten (Überschriften). Die über das Kontextmenü gestartete Funktion kann so die Nummern nicht aus dem Knoten laden.

Die SFC-, EDC- und TDP Masterknoten liefern deshalb Überschriften anstatt Nummern, weil die Nummern- Eigenschaften direkt an die Treeview- Properties gebunden sind.

## Lösung 1. Stufe: Trennung zwischen Eigenschaften, die an das visuelle Steuerelement gebunden werden und Eigenschaften, welche die Daten enthalten

Anstatt z.B. die Materialnummer eines Dokument- Knotens direkt an die TreeView zu binden, wird eine DisplayMaterialnummer  Eigenschaft gebunden. Im Falle eines gewöhnlichen Dokuments enthält es die Materialnummer. Im Falle eines Masterknotens jedoch kann es eine informelle Beschreibung liefern.

## Lösung 2. Stufe: Weitere Differenzierung der Gui- Knotenklassen

Wird das Konzept von Ordnern, die Mengen von Dokumenten und Ordnern enthalten, durch eine einzige Klasse implementiert wie aktuell durch die Dokument- Klasse, dann wird diese sehr komplex (Verletzung des Singel responsibillity prinzip = SRP).

Eine Aufteilung in folgende Klassen ist notwendig, um die Komplexität beherrschbar zu halten:

1. Folder (+Props: Foldername, CountOfSubfolders)
   1. TDPFolder
   2. SFCFolder
   3. EDCFolder