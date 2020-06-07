<?php
//Milosz Boghe

namespace Models;

use PDO;
use PDOException;

class ProductModel
{
    private $pdo;

    public function __construct(PDO $pdo)
    {
        $this->pdo = $pdo;
    }

    public function getAllProducts()
    {
        $result = null;
        try {
            $statement = $this->pdo->prepare("SELECT * FROM product");
            $statement->execute();
            $result = $statement->fetchAll();
        } catch (PDOException $e) {
            throw new ModelException();
        }
        return $result;
    }

    public function deleteProduct($id)
    {
        $result = null;
        try {
            $statement = $this->pdo->prepare("DELETE FROM product WHERE id=:id");
            $statement->bindParam(":id", $id, PDO::PARAM_INT);
            $result = $statement->execute();
        } catch (PDOException $e) {
            throw new ModelException();
        }
        return $result;
    }

}
