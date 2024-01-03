// mko, 12.4.2023
// Nach wie vor Probleme mit dem Laden einer React- Komponente. Mögliche Lösungen hier:
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
    function ($, React, ReacDOM, CrossWriter) {        

        // Disable AJAX Caching
        $.ajax({
            cache: false
        });        

        // Get ServerOrigin
        let urlOrigin = $('#urlOrigin').val();

        // Load CrossWriter
        CrossWriter.default("crossWriter", urlOrigin, "CrossWriter", "nytDemoSquRoot.cwf");
        
    });