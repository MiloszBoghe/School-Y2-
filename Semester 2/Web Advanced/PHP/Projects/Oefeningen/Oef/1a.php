<?php
$aantalLijnen = 7;
for ($i = $aantalLijnen; $i > 0; $i--) {
    $spaces = $i - 1;
    $content = str_repeat(" ", $spaces) . str_repeat("#", $aantalLijnen+1 - $i) . "\n";
    print($content);
}
