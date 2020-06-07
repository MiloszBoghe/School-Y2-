"use strict";

import User from './users/User';
import Item from './shopping/Item';
import ShoppingCart from './shopping/ShoppingCart';

window.addEventListener('load', (event) => {
    let user=new User('geert');
    let item=new Item(1,2.333);
    let item2=new Item(2,3.32);
    let shoppingCart=new ShoppingCart(user);
    shoppingCart.addItem(item);
    shoppingCart.addItem(item);
    shoppingCart.addItem(item2);

    let preElement=document.createElement("pre");
    let textNode=document.createTextNode(shoppingCart.toString());

    preElement.appendChild(textNode) ;

    document.getElementById('output').appendChild(preElement);
});

