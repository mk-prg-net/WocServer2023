// Ausprobieren der Module
/// <reference path ="../typings/jquery.d.ts"/> 
// Konfigurieren der Laufzeitumgebung von Require
requirejs.config({
    //By default load any module IDs from js/lib
    baseUrl: '/js'
});

requirejs(['mod/EditWoc/Ctrl'],
    function(InstallEditWocIdCtrl) {

        InstallEditWocIdCtrl.default($, "ts/");

        $('#woc-descriptor').EditWocIdCtrl();

    });