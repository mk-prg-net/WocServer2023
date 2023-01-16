// Ausprobieren der Module

"use strict";

// 1. Konfigurieren von RequireJS
requirejs.config({
    //By default load any module IDs from js/lib
    baseUrl: '/Scripts',

//    paths: {
//        'QUnit': 'qunit'
//    },

//    shim: {
//        'QUnit': {
//            exports: 'QUnit',
//            init: function() {
//                QUnit.config.autoload = false;
//                QUnit.config.autostart = false;
//            }
//        } 
//    }
});


// 2. Starten der Anwendung
requirejs(['Polyfills', 'tsJs/RPNTests'],
    function (Polyfills, RPNTests) {
        debugger;

        RPNTests.default();

});