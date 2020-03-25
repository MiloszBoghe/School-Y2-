import WereldObject from './WereldObject';
export default class Hero extends WereldObject {
    constructor(x, y) {
        super(x, y);
    }

    moveDown() {
        this.y--;
    }

    moveUp() {
        this.y++;
    }

    moveLeft() {
        this.x--;
    }

    moveRight() {
        this.x++;
    }

}