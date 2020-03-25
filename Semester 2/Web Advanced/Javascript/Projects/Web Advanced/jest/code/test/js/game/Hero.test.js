import Hero from "../../../src/js/game/Hero";

test('test moveLeft',
    () => {
        let hero = new Hero(1, 2);
        hero.moveLeft();
        expect(hero.x === 0);
    });

test('test moveRight',
    () => {
        let hero = new Hero(1, 2);
        hero.moveRight();
        expect(hero.x === 2)
    });