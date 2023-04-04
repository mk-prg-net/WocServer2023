/// <reference path ="../../typings/jquery.d.ts"/>

// mko, 4.4.2023
// Installiert ein JQuery- Plugin f�r das editieren von WocId's
export default function InstallEditWocIdCtrl($: JQueryStatic, urlTSRoot: string) {

    // JQuery Collection Plugin.
    // Die hier definierte Funktion wird f�r jeden selektierten Knoten aufgerufen.
    // Jeder selektierte Knoten wird durch **this** repr�sentiert. Durch $(this) wird der 
    // Knoten von einem JQuery- Objekt umh�llt. Es k�nnen dann alle JQuery- Funktionen auf diesem ausgef�hrt werden
    // An den selektierten Knoten wird ein WocIdEdit- Fenster eingef�gt.
    // 
    $.fn.EditWocIdCtrl = function (options) {
        options = $.extend({ editWocIdClass: 'editWocId' }, options || {});

        // Pr�fen, ob die EditWocId- Struktur bereits in den Kindfenstern eingef�gt wurde
        let searchRes = $(this).find(".EditWocCtrlHtm");

        if (searchRes.length === 0) {
            // Edit Woc ist noch nicht definiert
            $(this).load(`${urlTSRoot}/EditWoc/View.htm`);
        }
    }
}


