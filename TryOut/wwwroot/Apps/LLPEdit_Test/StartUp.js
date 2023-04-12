// 1. Konfigurieren von RequireJS
requirejs.config({
    //By default load any module IDs from js/lib
    baseUrl: '/js',
    paths: {
        jquery: "../node_modules/jquery/dist/jquery",
        qunit: "../node_modules/qunit/qunit/qunit"
    }
});


// QUnit muss als Modul eingebunden werden
// JQuery wurde zuvor via Script- Tag global eingebunden.
requirejs(['mod/LLP/Test/LLPTests'],
    function (LLPTests) {
        LLPTests.default();
    });