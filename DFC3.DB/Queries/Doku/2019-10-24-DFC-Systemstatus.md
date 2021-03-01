# DFC Systemstatus Anzeige

Martin Korneffel, 24.10.2019

Über den aktuellen Zustand von DFC informiert der **Systemstatus**. Dieser wird in der **DFC- Homepage** angezeigt:

![DFC Systemstatus Anzeige](http://10.4.4.53/DFC01/DfcDocu/2019-10-24-DFC-Systemstatus-View.png "Anzeige des aktuellen DFC- Systemstatus")

## Mögliche Systemzustände

### ok

Fehlfunktionen im System sind aktuell nicht bekannt.

### warnings

Es sind kritische Zustände und entdeckt worden, welche Fehlfunktionen zur Folge haben können.

### errors

Es wurden Fehlfunktionen detektiert. Das System liefert keine verlässlichen Daten mehr. Bitte nur in Ausnahmefällen benutzen.

### halted

Das System wurde wurde gestoppt, und darf in diesem Zustand nicht genutzt werden.

## Definieren des Systemzustandes

Der Systemzustand wird vom DFC- Administrator definiert in der Tabelle **DZA_ADMIN.BOSCH106DFCMASTER**. Hier ist in der Zeile mit dem **Operand** **DFC_SYSTEMSTATUS** im Feld **WERT** einer der oben beschriebenen Systemzustände einzutragen. Die Anzeige in der DFC- Homepage wird dann automatisch angepasst. 
![DFC Systemstatus definieren](http://10.4.4.53/DFC01/DfcDocu/2019-10-24-DFC-Systemstatus-Define.png "Definieren des aktuellen DFC- Systemstatus")

Zusätzlich kann noch eine Erläuterung der aktuellen Situation in der Spalte **DESCRIPTION** erfolgen. Hier kann Klartext, oder ein sog. [DokuTerms](https://inside-docupedia.bosch.com/confluence/display/PAATMO1ICOWiki/DocuTerm+Reference) eingetragen werden wie z.B.:

``````
#i Status #_ #p LUP #d 24.10.2019 #eWarn #$ Some BOMs are inconsistent. Therefore, DFC workflows cannot be executed on them. #. #.
``````

[DokuTerms](https://inside-docupedia.bosch.com/confluence/display/PAATMO1ICOWiki/DocuTerm+Reference) werden gemäß ihrer Bedeutung automatisch formatiert.

Die im Feld **Description** verfasste Meldung erscheint dann nach einem Klick auf die Statusanzeige in der **DFC- Homepage**.
