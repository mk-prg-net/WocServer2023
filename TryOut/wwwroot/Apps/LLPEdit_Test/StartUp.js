// 1. Konfigurieren von RequireJS
requirejs.config({
    //By default load any module IDs from js/lib
    baseUrl: '/js'
});


// QUnit muss als Modul eingebunden werden
// JQuery wurde zuvor via Script- Tag global eingebunden.
requirejs(['lib/qunit', 'mod/LLP/Test/LLPTests'],
    function (QUnit, LLPTests) {
        LLPTests.default($, QUnit);
    });