<?php
// naam:
use Models\ModelException;
use Models\ProductModel;

require_once 'vendor/autoload.php';

$host = 'localhost'; //Xampp gebruikt dus veranderd in localhost
$db = 'examenwa2020';
$user = 'root';
//Xampp gebruikt , geen paswoord
$charset = 'utf8mb4';
$dsn = "mysql:host=$host;dbname=$db;charset=$charset";
$options = [
    PDO::ATTR_ERRMODE => PDO::ERRMODE_EXCEPTION,
    PDO::ATTR_DEFAULT_FETCH_MODE => PDO::FETCH_ASSOC
];

$id = $_GET["product_id"];
try {
    $pdo = new PDO($dsn, $user, '', $options);
    $productModel = new ProductModel($pdo);
    $productModel->deleteProduct($id);

} catch (ModelException $exception) {
    print("oops");
}

$pdo = null;
?>

<p><?php echo $id?> werd verwijderd</p>
