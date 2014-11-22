<?php
/*
Generated with CS2PHP
*/
namespace Lang\Php\Examples\BasicFeaturesExample {
    class SwichCaseTest {
        public static function PhpMain() {
            $a = 6;
            switch ($a) {
                case 1:
                case 2:
                case 3:
                    echo 'Something wrong';
                    break;
                case 6:
                    echo 'Everything is OK.';
                    break;
                default:
                    echo 'Something really wrong';
                    break;
            }
        }
    }
}
namespace {
    \Lang\Php\Examples\BasicFeaturesExample\SwichCaseTest::PhpMain();
}
?>