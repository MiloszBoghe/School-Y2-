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

try {
    $pdo = new PDO($dsn, $user, '', $options);
    $productModel = new ProductModel($pdo);
    $products = $productModel->getAllProducts();

} catch (ModelException $exception) {
    print("oops");
}

$totalWeight = 0;
$selectOptions = "";
foreach ($products as $product) {
    $totalWeight += $product["weight"];
    $selectOptions .= "<option value='$product[id]'>$product[name] ($product[weight])</option>\n";
}
$pdo = null;
?>
<p>totaal gewicht = <?php echo $totalWeight ?></p>
<form action="verwerking.php" method="get">
    <select name='product_id'>
        <?php
        echo $selectOptions;
        ?>
    </select>
    <input type="submit" value="Submit">
</form>



