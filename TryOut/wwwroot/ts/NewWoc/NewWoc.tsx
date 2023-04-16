// mko, 13.4.2023
// React- Komponente zum Anlegen eines neuen Woc (Woc := Web Document)
// Achtung: in der tsjson.config muss unter Compileroptions festgelegt sein: "jsx": "react"

import React from 'react';
import ReactDOM from 'react-dom';
import $ from 'jquery';

function NewWocReact(props) {

    let [wocHeader, setWocHeader] = React.useState({
        wocId: "none",
        title: "",
        authorId: "none",
        author: "",
        threadId: "none"                
    });

    if (wocHeader.title.endsWith("+")) {
        // Der Autocomplete Vorschag wurde angenommen.
    }
    else if (wocHeader.title.endsWith("#")) {
        // Der Title ist ohne Autocomplete zu übernehmen.
    }

    // Bestimmen des Autocomplete Textvorschlages
    let Autocomplete = ""; // Hier muss ein ajax- Call erfolgen

    // Teil bestimmen, der als Autocomplete Part eingeblendet wird
    let AutocompletePart = "";

    return (
        <div className="wocHeader">
            // Es kann ein neuer Titel definiert werden. Das erzeugt eine neue wocId
            // Oder es wird ein vorhandener Titel ausgewählt.
            // Die Auswahl kann explizit erfolgen, oder es wird eine Autocomplete- Vervollständigung angeboten.
            <span className="wocTitleMe" contentEditable onInput={e => setWocHeader({
                wocId: wocHeader.wocId,
                title: e.currentTarget.textContent,
                authorId: wocHeader.authorId,
                author: wocHeader.author,
                threadId: wocHeader.threadId
            })}            
            >
            </span>
            // Hier wird der Autocomplete- vorschlag eingeblendet
            <span className="wocTitleAutocompletePart">
                {AutocompletePart}
            </span>           

        </div>

    );

}

