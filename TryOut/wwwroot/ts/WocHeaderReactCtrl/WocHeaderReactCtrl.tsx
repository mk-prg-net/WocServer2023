/// <reference types="requirejs"/>

// https://stackoverflow.com/questions/35788857/karma-unit-testing-module-name-react-has-not-been-loaded-yet-for-context
import React from 'react';
import ReactDOM from 'react-dom';
//import jsx from 'react/jsx-runtime';
//let React = require('react');
//let ReactDOM = require('react-dom');
import $ from "jquery";

function Greeter(props) {
    let [greeting, setGreting] = React.useState("");

    function handleGreetClick() {
        alert(`Hello ${greeting}`);
    }    

    let charsRemainig = props.maxLength - greeting.length;
    let greetinginvalid = greeting.length === 0 || charsRemainig < 0;

    return (
        <div>
            Greeting:
            <input value={greeting} onChange={e => setGreting(e.target.value)} />
            <span>{charsRemainig}</span>

            <button disabled={greetinginvalid} onClick={handleGreetClick}>Greet</button>
        </div>
    );
}

export default function WocHeaderReactCtrlSetUp(idElem: string) {

    ReactDOM.render(<Greeter maxlength={20} />, $(`#${idElem}`)[0]);

}