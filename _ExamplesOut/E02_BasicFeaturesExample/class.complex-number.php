<?php
/*
Generated with CS2PHP
*/
namespace Lang\Php\Examples\BasicFeaturesExample {
    class ComplexNumber {
        public $Imaginary;
        public $Real;
        public function __construct($real, $imaginary) {
            $this->Real = $real;
            $this->Imaginary = $imaginary;
        }
        public static function minus($a, $b) {
            return new self($a->Real - $b->Real, $a->Imaginary - $b->Imaginary);
        }
        public static function plus($a, $b) {
            return new self($a->Real + $b->Real, $a->Imaginary + $b->Imaginary);
        }
        public static function op_Implicit($x) {
            return new self($x, 0);
        }
        public static function mul1($a, $b) {
            return new self($a->Real * $b->Real - $a->Imaginary * $b->Imaginary, $a->Real * $b->Imaginary + $a->Imaginary * $b->Real);
        }
        public static function mul2($a, $b) {
            return new self($a->Real * $b, $a->Imaginary * $b);
        }
        public static function op_Division($a, $b) {
            $tmp = $b->Real * $b->Real + $b->Imaginary * $b->Imaginary;
            $re = $a->Real * $b->Real + $a->Imaginary * $b->Imaginary;
            $im = $a->Imaginary * $b->Real - $a->Real * $b->Imaginary;
            return new self($re / $tmp, $im / $tmp);
        }
    }
}
?>