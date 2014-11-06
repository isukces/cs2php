<?php
/*
Generated with CS2PHP
*/
class Index {
    public static function PhpMain() {
        $a = preg_match('/hello/i', 'ąHello world', $matches, PREG_OFFSET_CAPTURE, 2);
        if ($a === false)
            echo 'preg_match: Error';
        else if ($a == 1)
            echo 'preg_match: Match';
        else
            echo "preg_match: Don't match";
var_dump($matches);
echo(gettype($matches));
	}
}
Index::PhpMain();
?>