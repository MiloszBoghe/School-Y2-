<?php
// naam:
require_once 'vendor/autoload.php';
use \users\UserFactory;
use \assets\Item;
$person1=UserFactory::makePerson(1,"geert");
$person2=UserFactory::makePerson(2,"leen");
$company1=UserFactory::makeCompany(3,"aabbcc");
$item1=new Item(4,$person1);
print($item1);
$item1->setBorrower($person2);
$item1->setBorrower($company1);
$item1->setBorrower($person2);

print($item1);

