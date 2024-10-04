import QUnit, { assert } from "qunit";

import { IRt, newRT, addRT, mulRT} from "./RT";

export default function BigIntTest() {

    QUnit.test("Big Int Arithmetik Testen", function (assert) {

        let a = 12345678901234567890n;
        let b = 1n;

        let c = a + b;

        assert.equal(c, 12345678901234567891n, `Sum of BigInts ${a}+${b} was epected as 12345678901234567891n, but is ${c}`);

        let x = 1000000000000n
        let mul = 2n * x;

        assert.equal(mul, 2000000000000n, `Mul of BigInts ${x}*2n was epected as 2000000000000n, but is ${mul}`);

    });


    QUnit.test("RT Arithmetik Testen", function (assert) {

        let a = newRT(0n, 1n, 2n, 1n);
        let b = newRT(2n, 0n, 1n, 1n);

        let sum = addRT(a, a);

        assert.equal(sum.M(), 1n);
        assert.equal(sum.N(), 0n);
        assert.equal(sum.D(), 1n);
        assert.equal(sum.X(), 1n);


        let mul = mulRT(a, b);

        assert.equal(mul.M(), 1n);
        assert.equal(mul.N(), 0n);
        assert.equal(mul.D(), 1n);
        assert.equal(mul.X(), 1n);



    });

    QUnit.start();

}
