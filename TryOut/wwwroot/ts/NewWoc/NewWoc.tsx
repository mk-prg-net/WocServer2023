// mko, 13.4.2023
// React- Komponente zum Anlegen eines neuen Woc (Woc := Web Document)
// Achtung: in der tsjson.config muss unter Compileroptions festgelegt sein: "jsx": "react"
//
// Das Ergebnis ist (TitleId, AuthorId, NodeId, NameSpace) Triple. Dieses wird als DocuTerm an den Server Übermittelt
// #i wocHeader
//  #_
//      #p Title  #int TitleId          // Vordefiniert oder neu
//      #p Author #int AuthorId         // Muss aus einer Liste von vordefinierten entnommen werden
//      #p Node   #int NodeId           // Muss aus einer Liste von vordefinierten entnommen werden
//      #p NS     #str root/...         // Muss aus der Liste der existierenden ausgewählt werden
//  #.
// 
// Die Sparache kann ausgewählt werden


import React from 'react';
import ReactDOM from 'react-dom';
import $, { error } from 'jquery';

import NamingIds from '../NC/NamingIds';
import NIDStr from '../NC/NIDStr';
import INamingContainer from '../NC/INamingContainer'

interface IPropsParam {
    ServerOrigin: string
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

    let [wocHeaderState, setWocHeader] = React.useState<IWocHeaderState>({
        wocId: "none",
        title: "",
        authorId: "none",
        author: "",
        threadId: "none",
        errLoadProposals: false,
        errLoadProposalsTxt: "",
        ncList: []
    });

    React.useEffect(() => {
        $("#wocTitleEdit").focus();
        $("#wocTitleEdit").html("_");
    });

    function setProposalAsTitle(ix: number, wocHeaderState: IWocHeaderState): IWocHeaderState {
        return {
            wocId: wocHeaderState.ncList[ix].NIDstr,
            title: wocHeaderState.ncList[ix].DE,
            authorId: wocHeaderState.authorId,
            author: wocHeaderState.author,
            threadId: wocHeaderState.threadId,
            errLoadProposals: false,
            errLoadProposalsTxt: "",
            ncList: wocHeaderState.ncList
        }
    }

    function processInput(userText: string) {

        // Demo: get Neming- Id of Creator
        let CreatorNamingId = NamingIds().MKPRG.Naming.TechTerms.Lifecycle.Creator;

        userText = userText.trim();

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

            if (userText.endsWith("#")) {
                userText = userText.substring(0, userText.length - 1);
            }
            else if (userText.endsWith("#1")) {
                userText = userText.substring(0, userText.length - 2);
            }
            else if (userText.endsWith("#2")) {
                userText = userText.substring(0, userText.length - 2);
            }
            else if (userText.endsWith("#3")) {
                userText = userText.substring(0, userText.length - 2);
            }
            else if (userText.endsWith("#4")) {
                userText = userText.substring(0, userText.length - 2);
            }

            let params = JSON.stringify({ titleStart: userText });

            $.ajax(`${propsTyped.ServerOrigin}/WocTitlesStartsWith`, { method: "POST", contentType: "application/json", data: params })
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

    function txtHead(txt: string): string {
        if (txt.length > 1) {
            return txt.substring(txt.length - 2);
        }
        else {
            return "";
        }
    }

    function txtLast(txt: string): string {
        if (txt.length > 1) {
            return txt.substring(txt.length - 1, txt.length);
        }
        else if (txt.length == 1) {
            return txt;
        }
        else {
            return "";
        }
    }

    return (
        <div className="wocHeader">
            <div className="wocHeaderEdit">
                {
                    // Es kann ein neuer Titel definiert werden. Das erzeugt eine neue wocId
                    // Oder es wird ein vorhandener Titel ausgewählt.
                    // Die Auswahl kann explizit erfolgen, oder es wird eine Autocomplete- Vervollständigung angeboten.
                }
                <NIDStr
                    ServerOrigin={propsTyped.ServerOrigin}
                    lng={NamingIds().MKPRG.Naming.English}
                    nid={NamingIds().MKPRG.Naming.TechTerms.Development.Compiler}
                    cssClass="wocTitle"/>                

                <div className="LLP-EditorLine">
                    {
                        //wocHeaderState.title
                    }
                    <b>&gt;</b><span id="#wocTitleEdit" contentEditable onInput={e => processInput(e.currentTarget.textContent)}></span>
                </div>

                // Hier wird der Autocomplete- Vorschlag eingeblendet
                <ol className="wocTitleAutocompletePart">
                    {wocHeaderState.ncList.map(nc => <li>{nc.DE}</li>)}
                </ol>
                {wocHeaderState.errLoadProposals ? <div>Error: {wocHeaderState.errLoadProposalsTxt} </div> : ""}
            </div>
            <div className="wocHeaderView">
                <h1>Woc Header</h1>
                <dl>
                    <dt>Title</dt>
                    <dd>{wocHeaderState.title}</dd>
                </dl>
            </div>
        </div>

    );

}

export default function WocHeaderReactCtrlSetUp(idRoot: string, ServerOrigin: string) {

    ReactDOM.render(<NewWocReact ServerOrigin={ServerOrigin} />, $(`#${idRoot}`)[0]);

}
