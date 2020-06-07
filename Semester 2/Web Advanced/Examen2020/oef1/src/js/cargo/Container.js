"use strict";
//Milosz Boghe
import Product from './Product';

export default class Container {
    constructor(maxWeight) {
        if (maxWeight > 0 && typeof maxWeight == "number") {
            this._maxWeight = maxWeight;
            this._products = [];
        } else {
            throw new Error("container error: max weight invalid");
        }
    }

    addProduct(product) {
        let alreadyExists = false;
        let totalWeight = product.weight;
        this._products.forEach(p => {
            if (p.id === product.id) {
                alreadyExists = true;
            }
            totalWeight += p.weight
        });
        if (!(product instanceof Product)) {
            throw new Error("addProduct error: invalid product.");
        }
        if (alreadyExists) {
            throw new Error("addProduct error: already added.");
        }
        if (this._maxWeight <= totalWeight) {
            throw new Error("addProduct error: te zwaar geladen.");
        }
        this._products.push(product);

    }

    getProductAtIndex(index) {
        if (Number.isInteger(index) && index > 0 && index < this._products.length) {
            return this._products[index];
        } else {
            throw new Error("invalid index.");
        }
    }

}
