<?php
require_once 'vendor/autoload.php';

use drawing\Point;
use drawing\Line;

try {
    $point = new Point(111, 1);
} catch (InvalidArgumentException $e) {
    print($e->getMessage());
}

$line = new Line();
$line->addPoint(Point::makePointAtOrigin());
$line->addPoint(Point::makePointAt(1, 2));
print("\n");
print($line);
