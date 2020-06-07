//OPEN THIS FILE IN NOTEPAD++

Classes{	
	export default class Item {
		
		//om getters en setters te generaten:
		//1. schrijf de constructor met parameters
		//2. alt+insert (op laptops is dit meestal alt+shift+0 op numpad)
		
		constructor(id, price) {
			this._price = price;
			this._id = id;
		}

		get price() {
			return this._price;
		}

		toString() {
			return this._id + "_" + this.price;
		}
	}
}

Tests{
	//install package jest.
	//in package.json: 
	  "scripts": {
		"test": "jest"
	  },
	//tests schrijven in map test/js/
	//filenaam moet eindigen op .test.js
	//eg: user.test.js
	//om te runnen: in terminal --> npm test of npm run test
	
	//nodige imports
	import User from "../../src/js/users/User";
	
	test("Error als naam te kort is", ()=>{
		//voor een error te testen expect(() => functieDieErrorMoetVeroorzaken()).toThrowError();
		expect(()=>new User("m")).toThrowError()
	})

	test("toString werkt correct", () => {
		let geert = new User("geert");
		//om variabelen te vergelijken gewoon expect(variabel).toBe(resultaat);
		expect(geert.toString()).toBe("(geert)");
	});

}
