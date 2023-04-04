/// <reference path ="../../typings/jquery.d.ts"/>

// mko, 4.4.2023
// Installiert ein JQuery- Plugin für das editieren von WocId's
export default function InstallEditWocIdCtrl($: JQueryStatic, urlTSRoot: string) {

    // JQuery Collection Plugin.
    // Die hier definierte Funktion wird für jeden selektierten Knoten aufgerufen.
    // Jeder selektierte Knoten wird durch **this** repräsentiert. Durch $(this) wird der 
    // Knoten von einem JQuery- Objekt umhüllt. Es können dann alle JQuery- Funktionen auf diesem ausgeführt werden
    // An den selektierten Knoten wird ein WocIdEdit- Fenster eingefügt.
    // 
    $.fn.EditWocIdCtrl = function (options) {
        options = $.extend({ editWocIdClass: 'editWocId' }, options || {});

        // Prüfen, ob die EditWocId- Struktur bereits in den Kindfenstern eingefügt wurde
        let searchRes = $(this).find(".EditWocCtrlHtm");

        if (searchRes.length === 0) {
            // Edit Woc ist noch nicht definiert
            $(this).load(`${urlTSRoot}/EditWoc/View.htm`);
        }
    }
}


