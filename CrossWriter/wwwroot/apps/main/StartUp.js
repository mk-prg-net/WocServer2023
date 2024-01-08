// mko, 12.4.2023
// Nach wie vor Probleme mit dem Laden einer React- Komponente. M�gliche L�sungen hier:
// - https://github.com/facebook/react/issues/28
// - https://github.com/podio/requirejs-react-jsx
// - https://github.com/facebook/react/pull/417
// - https://github.com/felipecrv/jsx-requirejs-plugin
requirejs.config({
    //By default load any module IDs from js/lib
    baseUrl: '../../js',
    paths: {
        jquery: "../node_modules/jquery/dist/jquery",
        react: "../node_modules/react/umd/react.development",
        "react-dom": "../node_modules/react-dom/umd/react-dom.development",
        "react/jsx-runtime": "../node_modules/react/jsx-runtime"
        //,WocHeaderReactCtrl: "mod/WocHeaderReactCtrl/WocHeaderReactCtrl"
    }
});

requirejs(['jquery', "mod/CrossWriter"],
    function ($, CrossWriter) {        

        // Disable AJAX Caching
        $.ajax({
            cache: false
        });        

        // Erweiterungspunkt setzen
        //SET CURSOR POSITION
        $.fn.setCursorPosition = function (pos) {
            this.each(function (index, elem) {
                if (elem.setSelectionRange) {
                    elem.setSelectionRange(pos, pos);
                } else if (elem.createTextRange) {
                    var range = elem.createTextRange();
                    range.collapse(true);
                    range.moveEnd('character', pos);
                    range.moveStart('character', pos);
                    range.select();
                }
            });
            return this;
        };

        // Get ServerOrigin
        let urlOrigin = $('#urlOrigin').val();

        // Load CrossWriter
        CrossWriter.default("crossWriter", urlOrigin, "nytDemoSquRoot.cwf");
        
    });