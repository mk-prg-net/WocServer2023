"use strict";

// Konfigurieren der Laufzeitumgebung von Require
requirejs.config({
    //By default load any module IDs from js/lib
    baseUrl: '/Scripts',
});

// Start the main app logic.
requirejs(['tsJs/StringHlp', 'tsJs/Parser', 'tsJs/RPNHtml'],
function (StringHlp, Parser, RPNHtml) {

    let main_data = [];
    let pos = 0;

    RPNHtml.Token(main_data, "Bitte Text eingeben !");

    $(document).ready(function () {

        $("#edit_text").keydown(function () {

            if (event.keyCode == 13 && event.ctrlKey) {
                // Ctrl+Enter: Neuen Text übernehmen

                let txt = $(this).html();
                txt = txt.replace(/\&nbsp;/gi, " ")
                         .replace(/<div>/gi, " ")
                         .replace(/<\/div>/gi, " ")
                         .replace(/<p>/gi, " ")
                         .replace(/<\/p>/gi, " ")
                         .replace(/<br>/gi, " ")
                         .replace(/<br\/>/gi, " ");
                let res = Parser(txt);

                let spliceargs = [];
                spliceargs.push(pos);
                spliceargs.push(1);
                res.Stack.forEach(function(elem) { spliceargs.push(elem);});

                main_data.splice.apply(main_data, spliceargs);
                pos = pos + res.Stack.length;

                // Analyse des Textes hier
                $("#pre_text").html(main_data.slice(0, pos).map(function (fn) { return fn.print();}).join(" "));                

                $(this).text(res.Rest);

                $("#post_text").html(main_data.slice(pos + 1, main_data.length).map(function (fn) { return fn.print(); }).join(" "));

            } else if (event.keyCode == 38 && event.ctrlKey) {
                // Ctrl+Arrow Up: Vorausgehenden Textabschnitt bearbeiten                

                if (pos > 0) pos--;

                if (pos > 0) {
                    $("#pre_text").html(main_data.slice(0, pos).map(function (fn) { return fn.print(); }).join(" "));
                } else {
                    $("#pre_text").html("");
                }

                $(this).text(main_data[pos].printRPN());

                if (main_data.length > pos + 1) {
                    $("#post_text").html(main_data.slice(pos + 1, main_data.length).map(function (fn) { return fn.print(); }).join(" "));
                } else {
                    $("#post_text").html("");
                }



            } else if (event.keyCode == 40 && event.ctrlKey) {
                // Ctrl+Arrow Down: Vorausgehenden Textabschnitt bearbeiten                

                if (pos < main_data.length - 1) pos++;

                if (pos > 0) {
                    $("#pre_text").html(main_data.slice(0, pos).map(function (fn) { return fn.print(); }).join(" "));
                } else {
                    $("#pre_text").html("");
                }

                $(this).text(main_data[pos].printRPN());

                if (main_data.length > pos + 1) {
                    $("#post_text").html(main_data.slice(pos + 1, main_data.length).map(function (fn) { return fn.print(); }).join(" "));
                } else {
                    $("#post_text").html("");
                }


            }


        })

    });

});



