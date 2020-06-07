"use strict";

import Container from './cargo/Container';
import Product from './cargo/Product';


try{
    let product1=new Product(1,200);
    let product2=new Product(2,100);
    let product3=new Product(3,400);
    let container=new Container(700);
    container.addProduct(product1);
    container.addProduct(product2);
    console.log(container.getProductAtIndex(1).weight);
    container.addProduct(product3);
} catch(error){
    console.log(error.message);
}





