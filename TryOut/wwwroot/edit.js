// Konfigurieren der Laufzeitumgebung von Require
requirejs.config({
    //By default load any module IDs from js/lib
    baseUrl: '/js',
    paths: {
        jquery: "../node_modules/jquery/dist/jquery"
    }
});

requirejs(['mod/rpnParser/html/Edit'],
    function(Edit) {

        Edit.default();

    });