<?php
/*
Generated with CS2PHP
*/
namespace {
    require_once(dirname(__FILE__) . '/class.complex-number.php');
}
namespace Lang\Php\Examples\BasicFeaturesExample {
    class ComplexNumberTest {
        public static function PhpMain() {
            $a = new ComplexNumber(3, 2);
            $b = ComplexNumber::op_Implicit(12);
            $c = ComplexNumber::mul1($a, $b);
            $d = ComplexNumber::plus($a, $b);
            $e = ComplexNumber::mul2(ComplexNumber::plus($a, ComplexNumber::op_Implicit(17)), 3);
            $m1 = ComplexNumber::mul1(new ComplexNumber(3, 2), new ComplexNumber(4, 5));
            $m2 = ComplexNumber::mul2(new ComplexNumber(3, 2), 5);
        }
    }
}
namespace {
    \Lang\Php\Examples\BasicFeaturesExample\ComplexNumberTest::PhpMain();
}
?>