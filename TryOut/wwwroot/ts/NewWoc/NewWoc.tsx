// mko, 13.4.2023
// React- Komponente zum Anlegen eines neuen Woc (Woc := Web Document)
// Achtung: in der tsjson.config muss unter Compileroptions festgelegt sein: "jsx": "react"

import React from 'react';
import ReactDOM from 'react-dom';
import $, { error } from 'jquery';


interface IPropsParam {
    ServerOrigin: string
}

interface INamingContainer {
    id: string,
    cnt: string,
    cn: string,
    en: string,
    es: string,
    de: string
}

interface IWocHeaderState {
    wocId: string,
    title: string,
    authorId: string,
    author: string,
    threadId: string
    errLoadProposals: boolean,
    errLoadProposalsTxt: string,
    ncList: Array<INamingContainer>
}

function NewWocReact(props) {

    let propsTyped = props as IPropsParam;

    let [wocHeader, setWocHeader] = React.useState({
        wocId: "none",
        title: "",
        authorId: "none",
        author: "",
        threadId: "none",
        errLoadProposals: false,
        errLoadProposalsTxt: "",
        ncList: []
    });

    let wocHeaderState = wocHeader as IWocHeaderState;

    function setProposalAsTitle(ix: number, wocHeaderState: IWocHeaderState): IWocHeaderState {
        return {
            wocId: wocHeaderState.ncList[ix].id,
            title: wocHeaderState.ncList[ix].de,
            authorId: wocHeaderState.authorId,
            author: wocHeaderState.author,
            threadId: wocHeaderState.threadId,
            errLoadProposals: false,
            errLoadProposalsTxt: "",
            ncList: wocHeaderState.ncList
        }
    }

    function processInput(userText: string) {

        if (userText === "") {
            // Noch kein Text eingegeben
        }
        else if (userText.endsWith("#0")) {
            // Der Title ist ohne Autocomplete zu übernehmen.            
        }
        else if (userText.endsWith("#1")) {
            // Der erste Vorschlag ist an den Titel anzuhängen            
            setWocHeader(setProposalAsTitle(0, wocHeaderState));
        }
        else if (userText.endsWith("#2")) {
            // Der zweite Vorschlag ist an den Titel anzuhängen
            setWocHeader(setProposalAsTitle(1, wocHeaderState));
        }
        else if (userText.endsWith("#3")) {
            // Der dritten Vorschlag ist an den Titel anzuhängen
            setWocHeader(setProposalAsTitle(2, wocHeaderState));
        }
        else if (userText.endsWith("#4")) {
            // Der vierte Vorschlag ist an den Titel anzuhängen
            setWocHeader(setProposalAsTitle(3, wocHeaderState));
        } else {
            // Vorschläge vom Server laden

            let params = JSON.stringify({ titleStart: userText });

            $.ajax(`${propsTyped.ServerOrigin}/WocTitlesStartsWith`, { method: "POST", contentType: "application/json", data: params})
                .done((data, textStatus, jqXhr) => {
                    let _ncList = data as Array<INamingContainer>;
                    setWocHeader({
                        wocId: wocHeaderState.wocId,
                        title: userText,
                        authorId: wocHeaderState.authorId,
                        author: wocHeaderState.author,
                        threadId: wocHeaderState.threadId,
                        errLoadProposals: false,
                        errLoadProposalsTxt: "",
                        ncList: _ncList
                    });
                })
                .fail((jqXHR, textStatus, errorThrown) => {

                    setWocHeader({
                        wocId: wocHeaderState.wocId,
                        title: userText,
                        authorId: wocHeaderState.authorId,
                        author: wocHeaderState.author,
                        threadId: wocHeaderState.threadId,
                        errLoadProposals: true,
                        errLoadProposalsTxt: `HTTP Status:${textStatus}, ${errorThrown}`,
                        ncList: []

                    })
                });
        }
    }
    return (
        <div className="wocHeader">
            // Es kann ein neuer Titel definiert werden. Das erzeugt eine neue wocId
            // Oder es wird ein vorhandener Titel ausgewählt.
            // Die Auswahl kann explizit erfolgen, oder es wird eine Autocomplete- Vervollständigung angeboten.
            <div className="wocTitleMe" contentEditable onInput={e => processInput(e.currentTarget.textContent)}>
                {wocHeaderState.title}
            </div>
            // Hier wird der Autocomplete- vorschlag eingeblendet
            <ol className="wocTitleAutocompletePart">
                {wocHeaderState.ncList.map(nc => <li>{nc.de}</li>)}
            </ol>
            {wocHeaderState.errLoadProposals ? <div>Error: {wocHeaderState.errLoadProposalsTxt} </div>: ""}
        </div>
    );

}

export default function WocHeaderReactCtrlSetUp(idRoot: string, ServerOrigin: string) {

    ReactDOM.render(<NewWocReact ServerOrigin={ServerOrigin} />, $(`#${idRoot}`)[0]);

}
