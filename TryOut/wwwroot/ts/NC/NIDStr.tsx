// mko, 26.4.2023
// React Komponente, die eine Naming- Id in einen Namen auflöst

import React, { useEffect } from "react";
import ReacDom from "react-dom"
import $ from "jquery"
import NamingIds from "./NamingIds";
import INamingContainer from "./INamingContainer"


interface INidStrProps {
    ServerOrigin: string,
    cssClass: string,
    lng: string,
    nid: string,
    //onLanguageChanged: (Lng: string) => void
}

interface INIDStrState {
    FirstRun: boolean,
    Lng: string,
    NC: INamingContainer
}


export default function NIDStr(properties: INidStrProps) {

    let [state, setState] = React.useState<INIDStrState>({
        FirstRun: true,
        Lng: "CNT",
        NC: {
            CN: "noneCN",
            CNT: "noneCNT",
            DE: "noneDE",
            EN: "nonEN",
            ES: "nonES",
            NIDstr: "0000",
            PL: "nonPL"
        }
    });

    function getStringFromNamingContainer(lng: string): string {
        if (lng === NamingIds().MKPRG.Naming.CultureNeutral) {
            return state.NC.CNT;
        }
        else if (lng === NamingIds().MKPRG.Naming.English) {
            return state.NC.EN;
        }
        else if (lng === NamingIds().MKPRG.Naming.German) {
            return state.NC.DE;
        }
        else if (lng === NamingIds().MKPRG.Naming.Spanish) {
            return state.NC.ES;
        }
        else if (lng === NamingIds().MKPRG.Naming.Chinese) {
            return state.NC.EN;
        }
        else if (lng === NamingIds().MKPRG.Naming.Polish) {
            return state.NC.PL;
        }
    }

    function LoadNCfromServer() {

        if (state.FirstRun) {
            $.ajax(`${properties.ServerOrigin}/NamingContainers?NC=${properties.nid}`, { method: "GET" })
                .done((data, textStatus, jqXhr) => {
                    let _ncList = data as Array<INamingContainer>;

                    // Zustand der React- Komponente neu setzten und rendern
                    setState({
                        FirstRun: false,
                        Lng: properties.lng,
                        NC: _ncList[0]
                    });
                })
                .fail((jqXHR, textStatus, errorThrown) => {

                    let errTxt = `HTTP Status:${textStatus}, ${errorThrown}`;

                    // Zustand der React- Komponente neu setzten und rendern
                    setState({
                        FirstRun: false,
                        Lng: properties.lng,
                        NC: {
                            CN: errTxt,
                            CNT: errTxt,
                            DE: errTxt,
                            EN: errTxt,
                            ES: errTxt,
                            NIDstr: "0000",
                            PL: errTxt
                        }
                    });
                });
        }
    }

    // Hier wird das Laden des zugehörigen Naming- Container initiert
    React.useEffect(() => LoadNCfromServer(), []);

    return (
        <span className={properties.cssClass} data-nid={properties.nid}>
            {getStringFromNamingContainer(properties.lng)}
        </span>
    );
}