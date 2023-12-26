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

requirejs(['jquery', "react", "react-dom", "mod/nyt/LoadConfig"],
    function ($, React, ReacDOM, LoadConfig) {        

        // Caching in ajax abschalten
        $.ajax({
            cache: false
        });        

        let urlOrigin = $('#urlOrigin').val();

        let loadConfig = LoadConfig.default($);
        
    });