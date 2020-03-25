import Point from "../drawing/Point";

export default class WereldObject{
    constructor(x,y){
        this._location = new Point(x,y);
        this._x = x;
        this._y = y;
    }

    get x() {
        return this._x;
    }

    set x(value) {
        this._x = value;
    }

    get y() {
        return this._y;
    }

    set y(value) {
        this._y = value;
    }

    get location() {
        return this._location;
    }

    set location(value) {
        this._location = value;
    }
}