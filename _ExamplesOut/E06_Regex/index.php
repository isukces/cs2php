<?php
/*
Generated with CS2PHP
*/
class Index {
    public static function PhpMain() {
        $a = preg_match('/hello/i', 'Hello world', $matches);
        if ($a === false)
            echo 'preg_match: Error';
        else if ($a == 1)
            echo 'preg_match: Match';
        else
            echo "preg_match: Don't match";
    }
}
Index::PhpMain();
?>