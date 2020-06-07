"use strict";
//Milosz Boghe
export default class Product {
    constructor(id, weight) {
        if (Number.isInteger(id) && id > 0 && typeof weight == "number") {
            this._id = id;
            this._weight = weight;
        } else {
            throw new Error("product error: id of weight invalid");
        }
    }

    get id() {
        return this._id;
    }

    get weight() {
        return this._weight;
    }

}
