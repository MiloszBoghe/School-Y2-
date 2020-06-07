/******/ (function(modules) { // webpackBootstrap
/******/ 	// The module cache
/******/ 	var installedModules = {};
/******/
/******/ 	// The require function
/******/ 	function __webpack_require__(moduleId) {
/******/
/******/ 		// Check if module is in cache
/******/ 		if(installedModules[moduleId]) {
/******/ 			return installedModules[moduleId].exports;
/******/ 		}
/******/ 		// Create a new module (and put it into the cache)
/******/ 		var module = installedModules[moduleId] = {
/******/ 			i: moduleId,
/******/ 			l: false,
/******/ 			exports: {}
/******/ 		};
/******/
/******/ 		// Execute the module function
/******/ 		modules[moduleId].call(module.exports, module, module.exports, __webpack_require__);
/******/
/******/ 		// Flag the module as loaded
/******/ 		module.l = true;
/******/
/******/ 		// Return the exports of the module
/******/ 		return module.exports;
/******/ 	}
/******/
/******/
/******/ 	// expose the modules object (__webpack_modules__)
/******/ 	__webpack_require__.m = modules;
/******/
/******/ 	// expose the module cache
/******/ 	__webpack_require__.c = installedModules;
/******/
/******/ 	// define getter function for harmony exports
/******/ 	__webpack_require__.d = function(exports, name, getter) {
/******/ 		if(!__webpack_require__.o(exports, name)) {
/******/ 			Object.defineProperty(exports, name, { enumerable: true, get: getter });
/******/ 		}
/******/ 	};
/******/
/******/ 	// define __esModule on exports
/******/ 	__webpack_require__.r = function(exports) {
/******/ 		if(typeof Symbol !== 'undefined' && Symbol.toStringTag) {
/******/ 			Object.defineProperty(exports, Symbol.toStringTag, { value: 'Module' });
/******/ 		}
/******/ 		Object.defineProperty(exports, '__esModule', { value: true });
/******/ 	};
/******/
/******/ 	// create a fake namespace object
/******/ 	// mode & 1: value is a module id, require it
/******/ 	// mode & 2: merge all properties of value into the ns
/******/ 	// mode & 4: return value when already ns object
/******/ 	// mode & 8|1: behave like require
/******/ 	__webpack_require__.t = function(value, mode) {
/******/ 		if(mode & 1) value = __webpack_require__(value);
/******/ 		if(mode & 8) return value;
/******/ 		if((mode & 4) && typeof value === 'object' && value && value.__esModule) return value;
/******/ 		var ns = Object.create(null);
/******/ 		__webpack_require__.r(ns);
/******/ 		Object.defineProperty(ns, 'default', { enumerable: true, value: value });
/******/ 		if(mode & 2 && typeof value != 'string') for(var key in value) __webpack_require__.d(ns, key, function(key) { return value[key]; }.bind(null, key));
/******/ 		return ns;
/******/ 	};
/******/
/******/ 	// getDefaultExport function for compatibility with non-harmony modules
/******/ 	__webpack_require__.n = function(module) {
/******/ 		var getter = module && module.__esModule ?
/******/ 			function getDefault() { return module['default']; } :
/******/ 			function getModuleExports() { return module; };
/******/ 		__webpack_require__.d(getter, 'a', getter);
/******/ 		return getter;
/******/ 	};
/******/
/******/ 	// Object.prototype.hasOwnProperty.call
/******/ 	__webpack_require__.o = function(object, property) { return Object.prototype.hasOwnProperty.call(object, property); };
/******/
/******/ 	// __webpack_public_path__
/******/ 	__webpack_require__.p = "";
/******/
/******/
/******/ 	// Load entry module and return exports
/******/ 	return __webpack_require__(__webpack_require__.s = "./src/js/app.js");
/******/ })
/************************************************************************/
/******/ ({

/***/ "./src/js/app.js":
/*!***********************!*\
  !*** ./src/js/app.js ***!
  \***********************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

"use strict";
eval("\n\nvar _Container = __webpack_require__(/*! ./cargo/Container */ \"./src/js/cargo/Container.js\");\n\nvar _Container2 = _interopRequireDefault(_Container);\n\nvar _Product = __webpack_require__(/*! ./cargo/Product */ \"./src/js/cargo/Product.js\");\n\nvar _Product2 = _interopRequireDefault(_Product);\n\nfunction _interopRequireDefault(obj) { return obj && obj.__esModule ? obj : { default: obj }; }\n\ntry {\n    var product1 = new _Product2.default(1, 200);\n    var product2 = new _Product2.default(2, 100);\n    var product3 = new _Product2.default(3, 400);\n    var container = new _Container2.default(700);\n    container.addProduct(product1);\n    container.addProduct(product2);\n    console.log(container.getProductAtIndex(1).weight);\n    container.addProduct(product3);\n} catch (error) {\n    console.log(error.message);\n}\n\n//# sourceURL=webpack:///./src/js/app.js?");

/***/ }),

/***/ "./src/js/cargo/Container.js":
/*!***********************************!*\
  !*** ./src/js/cargo/Container.js ***!
  \***********************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

"use strict";
eval("\n//Milosz Boghe\n\nObject.defineProperty(exports, \"__esModule\", {\n    value: true\n});\n\nvar _createClass = function () { function defineProperties(target, props) { for (var i = 0; i < props.length; i++) { var descriptor = props[i]; descriptor.enumerable = descriptor.enumerable || false; descriptor.configurable = true; if (\"value\" in descriptor) descriptor.writable = true; Object.defineProperty(target, descriptor.key, descriptor); } } return function (Constructor, protoProps, staticProps) { if (protoProps) defineProperties(Constructor.prototype, protoProps); if (staticProps) defineProperties(Constructor, staticProps); return Constructor; }; }();\n\nvar _Product = __webpack_require__(/*! ./Product */ \"./src/js/cargo/Product.js\");\n\nvar _Product2 = _interopRequireDefault(_Product);\n\nfunction _interopRequireDefault(obj) { return obj && obj.__esModule ? obj : { default: obj }; }\n\nfunction _classCallCheck(instance, Constructor) { if (!(instance instanceof Constructor)) { throw new TypeError(\"Cannot call a class as a function\"); } }\n\nvar Container = function () {\n    function Container(maxWeight) {\n        _classCallCheck(this, Container);\n\n        if (maxWeight > 0 && typeof maxWeight == \"number\") {\n            this._maxWeight = maxWeight;\n            this._products = [];\n        } else {\n            throw new Error(\"container error: max weight invalid\");\n        }\n    }\n\n    _createClass(Container, [{\n        key: \"addProduct\",\n        value: function addProduct(product) {\n            var alreadyExists = false;\n            var totalWeight = product.weight;\n            this._products.forEach(function (p) {\n                if (p.id === product.id) {\n                    alreadyExists = true;\n                }\n                totalWeight += p.weight;\n            });\n            if (!(product instanceof _Product2.default)) {\n                throw new Error(\"addProduct error: invalid product.\");\n            }\n            if (alreadyExists) {\n                throw new Error(\"addProduct error: already added.\");\n            }\n            if (this._maxWeight <= totalWeight) {\n                throw new Error(\"addProduct error: te zwaar geladen.\");\n            }\n            this._products.push(product);\n        }\n    }, {\n        key: \"getProductAtIndex\",\n        value: function getProductAtIndex(index) {\n            if (Number.isInteger(index) && index > 0 && index < this._products.length) {\n                return this._products[index];\n            } else {\n                throw new Error(\"invalid index.\");\n            }\n        }\n    }]);\n\n    return Container;\n}();\n\nexports.default = Container;\n\n//# sourceURL=webpack:///./src/js/cargo/Container.js?");

/***/ }),

/***/ "./src/js/cargo/Product.js":
/*!*********************************!*\
  !*** ./src/js/cargo/Product.js ***!
  \*********************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

"use strict";
eval("\n//Milosz Boghe\n\nObject.defineProperty(exports, \"__esModule\", {\n    value: true\n});\n\nvar _createClass = function () { function defineProperties(target, props) { for (var i = 0; i < props.length; i++) { var descriptor = props[i]; descriptor.enumerable = descriptor.enumerable || false; descriptor.configurable = true; if (\"value\" in descriptor) descriptor.writable = true; Object.defineProperty(target, descriptor.key, descriptor); } } return function (Constructor, protoProps, staticProps) { if (protoProps) defineProperties(Constructor.prototype, protoProps); if (staticProps) defineProperties(Constructor, staticProps); return Constructor; }; }();\n\nfunction _classCallCheck(instance, Constructor) { if (!(instance instanceof Constructor)) { throw new TypeError(\"Cannot call a class as a function\"); } }\n\nvar Product = function () {\n    function Product(id, weight) {\n        _classCallCheck(this, Product);\n\n        if (Number.isInteger(id) && id > 0 && typeof weight == \"number\") {\n            this._id = id;\n            this._weight = weight;\n        } else {\n            throw new Error(\"product error: id of weight invalid\");\n        }\n    }\n\n    _createClass(Product, [{\n        key: \"id\",\n        get: function get() {\n            return this._id;\n        }\n    }, {\n        key: \"weight\",\n        get: function get() {\n            return this._weight;\n        }\n    }]);\n\n    return Product;\n}();\n\nexports.default = Product;\n\n//# sourceURL=webpack:///./src/js/cargo/Product.js?");

/***/ })

/******/ });