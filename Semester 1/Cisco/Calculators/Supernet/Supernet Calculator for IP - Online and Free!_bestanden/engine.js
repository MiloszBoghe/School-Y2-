// IE7, IE8 compatibility fix
if(!Array.indexOf){
	Array.prototype.indexOf = function(obj) {
		for(var i=0; i<this.length; i++){
			if(this[i]==obj){
				return i;
			}
		}
		return -1;
	}
}

function IpClass() {
	this.pad = function (str, length) {
		while (str.length < length) {
			str = "0" + str;
		}
		return str;
	},
	this.octet = function (number) {
		return (this.value << (number * 8)) >>> 24;
	},
	this.dottedDecimal = function () {
		return this.octet(0) + "." + this.octet(1) + "." + this.octet(2) + "." + this.octet(3);
	},
	this.dottedHex = function () {
		return this.pad(this.octet(0).toString(16), 2) + "." +
			   this.pad(this.octet(1).toString(16), 2) + "." +
			   this.pad(this.octet(2).toString(16), 2) + "." +
			   this.pad(this.octet(3).toString(16), 2);
	},
	this.hex = function () {
		return "0x" + this.value.toString(16);
	},
	this.dottedBinary = function () {
		return this.pad(this.octet(0).toString(2), 8) + "." +
			   this.pad(this.octet(1).toString(2), 8) + "." +
			   this.pad(this.octet(2).toString(2), 8) + "." +
			   this.pad(this.octet(3).toString(2), 8);
	},
	this.binary = function () {
		return this.pad(this.value.toString(2), 32);
	}
}

function Mask(mask) {
	this.value = (4294967295 << (32 - mask)) >>> 0;
}
Mask.prototype = new IpClass();
Mask.prototype.bits = function () {
	return 32 - Math.round(Math.log((~this.value) + 1) / Math.log(2));
}

function Wildcard(mask) {
	this.value = ~mask;
}
Wildcard.prototype = new IpClass();

function Network(ip, mask) {
	this.value = ip & mask;
}
Network.prototype = new IpClass();

function Broadcast(ip, mask) {
	this.value = ip | (~mask);
}
Broadcast.prototype = new IpClass();

function First(ip, mask) {
	this.value = (ip & mask) + 1;
	if (mask == 4294967294) {
		this.value = ip & mask;
	}
}
First.prototype = new IpClass();

function Last(ip, mask) {
	this.value = (ip | (~mask)) - 1;
	if (mask == 4294967294) {
		this.value = ip | (~mask);
	}
}
Last.prototype = new IpClass();

function IpAddress (ip, mask, isDecimal) {
	var ipPatt=/\b(?:(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.){3}(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\b/;

	if (ipPatt.test(ip)) {
		var a = ip.split(".");

		this.value = a[0] * 16777216 + a[1] * 65536 + a[2] * 256 + a[3] * 1;
	} else {
		this.value = false;
	}
	
	if(isDecimal) {
		if(ip <= 4294967295){
			this.value = ip;
		} else {
			this.value = false;
		}
	}
	
	this.mask = new Mask(mask);
	this.network = new Network(this.value, this.mask.value);
	this.broadcast = new Broadcast(this.value, this.mask.value);
	this.first = new First(this.value, this.mask.value);
	this.last = new Last(this.value, this.mask.value);
	this.wildcard = new Wildcard(this.mask.value);
}
IpAddress.prototype = new IpClass();
IpAddress.prototype.nclass = function () {
	var x = this.octet(0);
	
	if (this.value == 0) {
		return 1;
	}
	if (this.value == 1) {
		return 2;
	}
	if (x < 127) {
		return 3;
	}
	if (x == 127) {
		return 4;
	}
	if (x >= 128 && x <= 191) {
		return 5;
	}
	if (x >= 192 && x <= 223) {
		return 6;
	}
	if (x >= 224 && x <= 239) {
		return 7;
	}
	if (this.value == 4294967295) {
		return 9;
	} else {
		return 8;
	}
}
IpAddress.prototype.subnetsBitUsage = function () {
	if(this.nclass() == 3) {
		var a = "0nnnnnnn";
		
		for(var i = 0; i < this.mask.bits() - 8; i++) {
			a = a + "s";
		}
		for(var i = 0; i < 32 - this.mask.bits(); i++) {
			a = a + "h";
		}
		
		a = a.split("");
		a.splice(24, 0, ".");
		a.splice(16, 0, ".");
		a.splice(8, 0, ".");
		
		a.push("</font>");
		a.splice(a.indexOf("h"), 0, "</font><font class=\"host\">");
		a.splice(a.indexOf("s"), 0, "</font><font class=\"subnet\">");
		a = a.join("");
		a = "<font class=\"network\">" + a;
	}
	
	if(this.nclass() == 5) {
		var a = "10nnnnnnnnnnnnnn";
		
		for(var i = 0; i < this.mask.bits() - 16; i++) {
			a = a + "s";
		}
		for(var i = 0; i < 32 - this.mask.bits(); i++) {
			a = a + "h";
		}
		
		a = a.split("");
		a.splice(24, 0, ".");
		a.splice(16, 0, ".");
		a.splice(8, 0, ".");
		
		a.push("</font>");
		a.splice(a.indexOf("h"), 0, "</font><font class=\"host\">");
		a.splice(a.indexOf("s"), 0, "</font><font class=\"subnet\">");
		a = a.join("");
		a = "<font class=\"network\">" + a;
	}
	
	if(this.nclass() == 6) {
		var a = "110nnnnnnnnnnnnnnnnnnnnn";
		
		for(var i = 0; i < this.mask.bits() - 24; i++) {
			a = a + "s";
		}
		for(var i = 0; i < 32 - this.mask.bits(); i++) {
			a = a + "h";
		}
		
		a = a.split("");
		a.splice(24, 0, ".");
		a.splice(16, 0, ".");
		a.splice(8, 0, ".");
		
		a.push("</font>");
		a.splice(a.indexOf("h"), 0, "</font><font class=\"host\">");
		a.splice(a.indexOf("s"), 0, "</font><font class=\"subnet\">");
		a = a.join("");
		a = "<font class=\"network\">" + a;
	}
	return a;
}
IpAddress.prototype.classBitUsage = function () {
	x = this.nclass();

	switch(x) {
		case 1: return "00000000.00000000.00000000.00000000"; break;
		case 2: return "00000000.00000000.00000000.00000001"; break;
		case 3: return "0nnnnnnn.hhhhhhhh.hhhhhhhh.hhhhhhhh"; break;
		case 4: return "01111111.xxxxxxxx.xxxxxxxx.xxxxxxxx"; break;
		case 5: return "10nnnnnn.nnnnnnnn.hhhhhhhh.hhhhhhhh"; break;
		case 6: return "110nnnnn.nnnnnnnn.nnnnnnnn.hhhhhhhh"; break;
		case 7: return "1110xxxx.xxxxxxxx.xxxxxxxx.xxxxxxxx"; break;
		case 8: return "1111xxxx.xxxxxxxx.xxxxxxxx.xxxxxxxx"; break;
		case 9: return "11111111.11111111.11111111.11111111"; break;
	}
}
IpAddress.prototype.classAddressRange = function () {
	x = this.nclass();

	switch(x) {
		case 1: return "0.0.0.0"; break;
		case 2: return "0.0.0.1-0.255.255.255"; break;
		case 3: return "1.0.0.0-126.255.255.255"; break;
		case 4: return "127.0.0.0-127.255.255.255"; break;
		case 5: return "128.0.0.0-192.255.255.255"; break;
		case 6: return "192.0.0.0-223.255.255.255"; break;
		case 7: return "224.0.0.0-239.255.255.255"; break;
		case 8: return "240.0.0.0-255.255.255.254"; break;
		case 9: return "255.255.255.255"; break;
	}
}