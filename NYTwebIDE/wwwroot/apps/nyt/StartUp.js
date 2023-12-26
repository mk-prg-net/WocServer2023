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

requirejs(['jquery', "react", "react-dom", 'mod/NewWoc/NewWoc', 'mod/WocHeaderReactCtrl/WocHeaderReactCtrl', 'mod/LLP/OpSyms/RauteOpSyms', 'mod/LLP/StackOps', 'mod/LLP/StackElemStructs'],
    function ($, React, ReacDOM, NewWoc, WocHeaderReactCtrl, OpSyms, StackOps, StackElemStructs) {        

        // Caching in ajax abschalten
        $.ajax({
            cache: false
        });        

        let urlOrigin = $('#urlOrigin').val();

        // LLP initialisieren
        let opSyms = new OpSyms.default();
        let stackOps = new StackOps.default(opSyms);
        let stackElemStructs = new StackElemStructs.default(opSyms);

        //WocHeaderCtrlSetUp.default($, "ts/", stackOps, stackElemStructs);
        //var llpStack = stackOps.NewStack();
        //$('#woc-descriptor').WocHeaderCtrl({ llpStack: llpStack });

        WocHeaderReactCtrl.default("react_greeting");


        
        .default ("woc-descriptor", urlOrigin);
    });