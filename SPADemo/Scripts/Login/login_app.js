"use strict";

// Konfigurieren der Laufzeitumgebung von Require
requirejs.config({
    //By default load any module IDs from js/lib
    baseUrl: '/Scripts',
});

// Start the main app logic.
requirejs(['tsJs/StringHlp', 'tsJs/Parser', 'tsJs/RPNHtml'],
    function (StringHlp, Parser, RPNHtml) {



    });