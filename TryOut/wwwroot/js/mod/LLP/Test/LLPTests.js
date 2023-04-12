var __importDefault = (this && this.__importDefault) || function (mod) {
    return (mod && mod.__esModule) ? mod : { "default": mod };
};
define(["require", "exports", "qunit", "../OpSyms/RauteOpSyms", "../StackOps", "../StackElemStructs", "../Tokenizer/BasicTokenizer"], function (require, exports, qunit_1, RauteOpSyms_1, StackOps_1, StackElemStructs_1, BasicTokenizer_1) {
    "use strict";
    Object.defineProperty(exports, "__esModule", { value: true });
    qunit_1 = __importDefault(qunit_1);
    RauteOpSyms_1 = __importDefault(RauteOpSyms_1);
    StackOps_1 = __importDefault(StackOps_1);
    StackElemStructs_1 = __importDefault(StackElemStructs_1);
    BasicTokenizer_1 = __importDefault(BasicTokenizer_1);
    function LLPTest() {
        qunit_1.default.test("Test des Tokenizers", function (assert) {
            let opSyms = new RauteOpSyms_1.default();
            let stackElemStructs = new StackElemStructs_1.default(opSyms);
            let stackOps = new StackOps_1.default(opSyms);
            let tokenizer = new BasicTokenizer_1.default(opSyms, stackOps, stackElemStructs);
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
        qunit_1.default.start();
    }
    exports.default = LLPTest;
});
//# sourceMappingURL=LLPTests.js.map