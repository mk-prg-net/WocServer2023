import QUnit from "qunit";
import OpSyms from '../OpSyms/RauteOpSyms';
import StackOps from '../StackOps';
import StackElemStructs from '../StackElemStructs';
import BasicTokenizer from '../Tokenizer/BasicTokenizer';

export default function LLPTest() {  //($: JQueryStatic, QUnit: QUnit) {

    QUnit.test("Test des Tokenizers", function (assert) {

        let opSyms = new OpSyms();
        let stackElemStructs = new StackElemStructs(opSyms);
        let stackOps = new StackOps(opSyms);

        let tokenizer = new BasicTokenizer(opSyms, stackOps, stackElemStructs);

        {
            // String Tokens
            let line = "Hallo Welt";
            let getTokens = tokenizer.TokenizeLine(line);
            assert.ok(getTokens.Success);
            assert.equal(getTokens.ReturnValue.length, 2, `Aus '${line}''sollten zwei String Tokens eingelesen werden. Es wurden jedoch ${getTokens.ReturnValue.length} Toekn eingelesen`);
        }
        {
            // Bool Tokens 
            let line = `true ${opSyms.rpnBoolType} false ${opSyms.rpnBoolType}`;
            let getTokens = tokenizer.TokenizeLine(line);
            assert.ok(getTokens.Success);
            assert.equal(getTokens.ReturnValue.length, 2, `Aus '${line}''sollten zwei Bool Token eingelesen werden. Es wurden jedoch ${getTokens.ReturnValue.length} Token eingelesen`);

        }
        {
            // Int- Token
            let line = `12345678901234 ${opSyms.rpnIntType}`;
            let getTokens = tokenizer.TokenizeLine(line);
            assert.ok(getTokens.Success);
            assert.equal(getTokens.ReturnValue.length, 1, `Aus '${line}''sollten ein Int Token eingelesen werden. Es wurden jedoch ${getTokens.ReturnValue.length} Token eingelesen`);
        }
        {
            // Num- Token
            let line = `1234567890.1234 ${opSyms.rpnNumType}`;
            let getTokens = tokenizer.TokenizeLine(line);
            assert.ok(getTokens.Success);
            assert.equal(getTokens.ReturnValue.length, 1, `Aus '${line}''sollten ein Num Token eingelesen werden. Es wurden jedoch ${getTokens.ReturnValue.length} Token eingelesen`);
        }
        {
            // Kommentar
            let line = `true ${opSyms.rpnBoolType} ${opSyms.rpnComment} Das ist ein Kommentar`;
            let getTokens = tokenizer.TokenizeLine(line);
            assert.ok(getTokens.Success);
            assert.equal(getTokens.ReturnValue.length, 2, `Aus '${line}''sollten ein Kommentar Token eingelesen werden. Es wurden jedoch ${getTokens.ReturnValue.length} Token eingelesen`);
        }
        {
            // Liste
            let line = `${opSyms.rpnListStart} Das ist eine Liste ${opSyms.rpnListEnd}`;
            let getTokens = tokenizer.TokenizeLine(line);
            assert.ok(getTokens.Success);
            assert.equal(getTokens.ReturnValue.length, 6, `Aus '${line}''sollten eine Liste als 6 Tokens eingelesen werden. Es wurden jedoch ${getTokens.ReturnValue.length} Token eingelesen`);
        }
        {
            // Add Funktion
            let line = `1 ${opSyms.rpnIntType} 2 ${opSyms.rpnIntType} 3 ${opSyms.rpnIntType} ${opSyms.rpnFuncPrefix}Add`;
            let getTokens = tokenizer.TokenizeLine(line);
            assert.ok(getTokens.Success);
            assert.equal(getTokens.ReturnValue.length, 4, `Aus '${line}''sollten eine Add- Funktion, bestehend aus 4 Token  eingelesen werden. Es wurden jedoch ${getTokens.ReturnValue.length} Token eingelesen`);
        }

    });

    QUnit.start();
}