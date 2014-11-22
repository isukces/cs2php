<?php
/*
Generated with CS2PHP
*/
namespace {
    require_once(dirname(__FILE__) . '/class.multiplication-table.php');
}
namespace Lang\Php\Examples\BasicFeaturesExample {
    class Index {
        public static function h($title) {
            echo '<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">' . (PHP_EOL) . '<html>' . (PHP_EOL) . '<head>' . (PHP_EOL) . '<title>' . htmlentities($title) . '</title>' . (PHP_EOL) . '</head>' . (PHP_EOL) . '<body>' . (PHP_EOL);
        }
        public static function f() {
            echo '</body></html>';
        }
        public static function PhpMain() {
            self::h('Welcome to CS2PHP');
            echo '<p>You can use echo just like in PHP</p><p>Or use some helpers.</p>';
            MultiplicationTable::show_multiplication_table(10, 12);
            self::f();
        }
    }
}
namespace {
    \Lang\Php\Examples\BasicFeaturesExample\Index::PhpMain();
}
?>