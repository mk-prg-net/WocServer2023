var __importDefault = (this && this.__importDefault) || function (mod) {
    return (mod && mod.__esModule) ? mod : { "default": mod };
};
define(["require", "exports", "qunit", "./RT"], function (require, exports, qunit_1, RT_1) {
    "use strict";
    Object.defineProperty(exports, "__esModule", { value: true });
    qunit_1 = __importDefault(qunit_1);
    function BigIntTest() {
        BigInt.prototype["toJSON"] = function () {
            return this.toString();
        };
        //QUnit.dump.setParser('bigint', function (bint) {
        //    return bint.toString();
        //});
        qunit_1.default.test("Big Int Arithmetik Testen", function (assert) {
            let a = 12345678901234567890n;
            let b = 1n;
            let c = a + b;
            assert.equal(c, 12345678901234567891n, `Sum of BigInts ${a}+${b} was epected as 12345678901234567891n, but is ${c}`);
            let x = 1000000000000n;
            let mul = 2n * x;
            assert.equal(mul, 2000000000000n, `Mul of BigInts ${x}*2n was epected as 2000000000000n, but is ${mul}`);
        });
        qunit_1.default.test("GGT von a und b", function (assert) {
            let ggt = (0, RT_1.GGT)(9n, 15n);
            assert.equal(ggt, 3n, `GGT(9n, 15n) was expected as 3n, but is ${ggt}`);
            ggt = (0, RT_1.GGT)(3528n, 3780n);
            assert.equal(ggt, 252n, `GGT(3528n, 3780n) was expected as 252n, but is ${ggt}`);
        });
        qunit_1.default.test("RT Arithmetik Testen", function (assert) {
            let a = (0, RT_1.newRT)(0n, 1n, 2n, 1n);
            let b = (0, RT_1.newRT)(2n, 0n, 1n, 1n);
            {
                let sum = (0, RT_1.addRT)(a, a);
                assert.equal(sum.M(), 0n);
                assert.equal(sum.N(), 4n);
                assert.equal(sum.D(), 4n);
                assert.equal(sum.X(), 1n);
            }
            {
                let sum = (0, RT_1.addRT)(b, b);
                assert.equal(sum.M(), 4n);
                assert.equal(sum.N(), 0n);
                assert.equal(sum.D(), 1n);
                assert.equal(sum.X(), 1n);
            }
            {
                let sum = (0, RT_1.addRT)(a, b);
                assert.equal(sum.M(), 2n);
                assert.equal(sum.N(), 1n);
                assert.equal(sum.D(), 2n);
                assert.equal(sum.X(), 1n);
            }
            {
                let mul = (0, RT_1.mulRT)(b, a);
                assert.equal(mul.M(), 0n);
                assert.equal(mul.N(), 2n);
                assert.equal(mul.D(), 2n);
                assert.equal(mul.X(), 1n);
            }
            {
                let mul = (0, RT_1.mulRT)(a, a);
                assert.equal(mul.M(), 0n);
                assert.equal(mul.N(), 2n);
                assert.equal(mul.D(), 8n);
                assert.equal(mul.X(), 1n);
            }
            {
                let mul = (0, RT_1.mulRT)(b, b);
                assert.equal(mul.M(), 4n);
                assert.equal(mul.N(), 0n);
                assert.equal(mul.D(), 1n);
                assert.equal(mul.X(), 1n);
            }
        });
        qunit_1.default.start();
    }
    exports.default = BigIntTest;
});
//# sourceMappingURL=BigIntTest.js.map