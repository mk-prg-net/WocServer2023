// 1. Konfigurieren von RequireJS
requirejs.config({
    //By default load any module IDs from js/lib
    baseUrl: '/js',
    paths: {
        jquery: "../node_modules/jquery/dist/jquery",
        qunit: "../node_modules/qunit/qunit/qunit",
        mathjs: "../node_modules/mathjs/lib/browser/math"
    }
});


// QUnit muss als Modul eingebunden werden
// JQuery wurde zuvor via Script- Tag global eingebunden.
requirejs(['mod/StackFlow/RT/BigIntTest'],
    function (BigIntTest) {
        BigIntTest.default();
    });