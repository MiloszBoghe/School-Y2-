import Date from "../../../src/js/accounting/Date";
let date = Date.make(10, 10, 2020);
test('test toString',
    () => {
        expect(date.toString() === "10/10/2020");
    });

test('test toStringMonth',
    () => {
        expect(date.toStringMonth() === "10/oct/2020");
    });