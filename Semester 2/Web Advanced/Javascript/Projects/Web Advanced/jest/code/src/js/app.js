"use strict";

import Point from './drawing/Point';
import ColorPoint from './drawing/ColorPoint';
import Line from './drawing/Line';
import Hero from "./game/Hero";


let point = new Point(1, 2);
let point2 = new Point(20, 2);
let preElement = document.createElement("pre");
let textNode = document.createTextNode(point + "\n");

let colorPoint = new ColorPoint(1, 2, 'red');
let textNode2 = document.createTextNode(colorPoint+"\n");

let line = new Line(point, point2);
let textNode3 = document.createTextNode(line);

preElement.appendChild(textNode);
preElement.appendChild(textNode2);
preElement.appendChild(textNode3);
document.getElementById('output').appendChild(preElement);




