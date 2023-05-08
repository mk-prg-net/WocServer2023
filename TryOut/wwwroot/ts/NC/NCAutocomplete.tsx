// mko, 27.4.2023
// React- Komponente Auswählen eines Namenscontainers
// Achtung: in der tsjson.config muss unter Compileroptions festgelegt sein: "jsx": "react"
//
// Das Ergebnis ist die NID des ausgewählten Naming- Containers.



import React from 'react';
import ReactDOM from 'react-dom';
import $, { error } from 'jquery';

import NamingIds from './NamingIds';
import NIDStr from './NIDStr';
import INamingContainer from './INamingContainer'

// Properties von NCAutocomplete
interface IPropsParam {
    ServerOrigin: string,
    LanguageNid: string
}

// Zustand von NCAutocomplete
interface INCAutocompleteState {
    NID: string,
    title: string,
    errLoadProposals: boolean,
    errLoadProposalsTxt: string,
    ncList: Array<INamingContainer>
}

function NCAutocomplete(props) {

    let propsTyped = props as IPropsParam;

    let [ncAutocompleteState, setNcAutocompleteState] = React.useState<INCAutocompleteState>({
        NID: "0000",
        title: "",
        errLoadProposals: false,
        errLoadProposalsTxt: "",
        ncList: []
    });

    React.useEffect(() => {
        $("#wocTitleEdit").focus();
        $("#wocTitleEdit").html("_");
    });

    function setProposalAsTitle(ix: number, wocHeaderState: INCAutocompleteState): INCAutocompleteState {
        return {
            NID: wocHeaderState.ncList[ix].NIDstr,
            title: wocHeaderState.ncList[ix].DE,
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
            setNcAutocompleteState(setProposalAsTitle(0, ncAutocompleteState));
        }
        else if (userText.endsWith("#2")) {
            // Der zweite Vorschlag ist an den Titel anzuhängen
            setNcAutocompleteState(setProposalAsTitle(1, ncAutocompleteState));
        }
        else if (userText.endsWith("#3")) {
            // Der dritten Vorschlag ist an den Titel anzuhängen
            setNcAutocompleteState(setProposalAsTitle(2, ncAutocompleteState));
        }
        else if (userText.endsWith("#4")) {
            // Der vierte Vorschlag ist an den Titel anzuhängen
            setNcAutocompleteState(setProposalAsTitle(3, ncAutocompleteState));
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
                    setNcAutocompleteState({
                        NID: ncAutocompleteState.NID,
                        title: userText,
                        errLoadProposals: false,
                        errLoadProposalsTxt: "",
                        ncList: _ncList
                    });
                })
                .fail((jqXHR, textStatus, errorThrown) => {

                    setNcAutocompleteState({
                        NID: ncAutocompleteState.NID,
                        title: userText,
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
        <div className="ncAutocomplete">
            <div className="ncAutocompleteInput">
                <NIDStr
                    ServerOrigin={propsTyped.ServerOrigin}
                    lng={propsTyped.LanguageNid}
                    nid={NamingIds().MKPRG.Naming.NamingContainerNC}
                    cssClass="wocTitle" />

                <div className="LLP-EditorLine">
                    <b>&gt;</b><span id="#ncInput" contentEditable onInput={e => processInput(e.currentTarget.textContent)}></span>
                </div>
                
                <ol className="ncAutocompletePart">
                    {ncAutocompleteState.ncList.map(nc => <li>{nc.DE}</li>)}
                </ol>
                {ncAutocompleteState.errLoadProposals ? <div>Error: {ncAutocompleteState.errLoadProposalsTxt} </div> : ""}
            </div>
            <div className="ncAutocompleteChoice">
                <h1>Woc Header</h1>
                <dl>
                    <dt>Title</dt>
                    <dd>{ncAutocompleteState.title}</dd>
                </dl>
            </div>
        </div>

    );

}

export default function NCAutocompleteCtrlSetUp(idRoot: string, ServerOrigin: string) {

    ReactDOM.render(<NCAutocomplete ServerOrigin={ServerOrigin} />, $(`#${idRoot}`)[0]);

}
