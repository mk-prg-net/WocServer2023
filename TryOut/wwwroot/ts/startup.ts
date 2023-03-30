requirejs.config({
    //By default load any module IDs from js/lib
    baseUrl: '/js',
    paths: {
        jquery: '/lib/jquery'
    }
});

// Start the main app logic.
requirejs(['tsJs/StringHlp', 'tsJs/Parser', 'tsJs/RPNHtml'],
function (StringHlp, Parser, RPNHtml) {
});