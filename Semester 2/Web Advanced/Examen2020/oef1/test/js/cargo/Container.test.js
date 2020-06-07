import Product from '../../../src/js/cargo/Product';
import Container from '../../../src/js/cargo/Container';
//Milosz Boghe
let container = new Container(700);
let product = new Product(1,100);
container.addProduct(product);
test("Error als addProduct argument een string is", ()=>{
    expect(()=>container.addProduct("test")).toThrowError()
})

test("Error als id van product al in productenArray is", () => {
    expect(()=>container.addProduct(product)).toThrowError();
});

test("Error als product te zwaar is.", () => {
    let veryHeavyProduct = new Product(2,700);
    expect(()=>container.addProduct(veryHeavyProduct)).toThrowError("te zwaar geladen");
});


