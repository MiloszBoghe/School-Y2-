function changeMaskRange(start, end) {
	var myarray=[
		["255.128.0.0 &nbsp;&nbsp; (/9)",1,2,23,8388608],
		["255.192.0.0 &nbsp;&nbsp; (/10)",2,4,22,4194302],
		["255.224.0.0 &nbsp;&nbsp; (/11)",3,8,21,2097150],
		["255.240.0.0 &nbsp;&nbsp; (/12)",4,16,20,1048574],
		["255.248.0.0 &nbsp;&nbsp; (/13)",5,32,19,524286],
		["255.252.0.0 &nbsp;&nbsp; (/14)",6,64,18,262142],
		["255.254.0.0 &nbsp;&nbsp; (/15)",7,128,17,131070],
		["255.255.0.0 &nbsp;&nbsp; (/16)",8,256,16,65534],
		["255.255.128.0 &nbsp;&nbsp; (/17)",9,512,15,32766],
		["255.255.192.0 &nbsp;&nbsp; (/18)",10,1024,14,16382],
		["255.255.224.0 &nbsp;&nbsp; (/19)",11,2048,13,8190],
		["255.255.240.0 &nbsp;&nbsp; (/20)",12,4094,12,4094],
		["255.255.248.0 &nbsp;&nbsp; (/21)",13,8192,11,2046],
		["255.255.252.0 &nbsp;&nbsp; (/22)",14,16384,10,1022],
		["255.255.254.0 &nbsp;&nbsp; (/23)",15,32768,9,510],
		["255.255.255.0 &nbsp;&nbsp; (/24)",16,65536,8,254],
		["255.255.255.128 &nbsp;&nbsp; (/25)",17,131072,7,126],
		["255.255.255.192 &nbsp;&nbsp; (/26)",18,262144,6,62],
		["255.255.255.224 &nbsp;&nbsp; (/27)",19,524288,5,30],
		["255.255.255.240 &nbsp;&nbsp; (/28)",20,1048576,4,14],
		["255.255.255.248 &nbsp;&nbsp; (/29)",21,2097152,3,6],
		["255.255.255.252 &nbsp;&nbsp; (/30)",22,4194304,2,2],
		["255.255.255.254 &nbsp;&nbsp; (/31)",23,8388608,1,2]
	]
	var mysubnet = $("#subnetMask").add("#subnetBits").add("#maxSubnets").add("#hostBits").add("#maxHosts");

	mysubnet.empty();
	
	for(var i = start - 9; i < (end - 8) ; i++) {
		$("#subnetMask").append("<option value=\"" + (i + 9) + "\">" + myarray[i][0] + "</option>");
		$("#subnetBits").append("<option value=\"" + (i + 9) + "\">" + myarray[i][1] + "</option>");
		$("#maxSubnets").append("<option value=\"" + (i + 9) + "\">" + myarray[i][2] + "</option>");
		$("#hostBits").append("<option value=\"" + (i + 9) + "\">" + myarray[i][3] + "</option>");
		$("#maxHosts").append("<option value=\"" + (i + 9) + "\">" + myarray[i][4] + "</option>");
	}
}

$(document).ready(function() {
    var IP = new IpAddress("10.0.0.0", 9);
	var oldClass = 3;
	
    $("#dottedDecimal").on("input paste copy cut change drop keydown keyup keypress keydown keyup blur", function(event) {
        //this.value = this.value.replace(/^[\.]/g,"");
		this.value = this.value.replace(/[^0-9\.]/g,"");
        /*this.value = this.value.replace(/[\.\.]+/g,".");

        this.value = this.value.replace(/([3-9]\d)\d{1,}/g,"$1");
        this.value = this.value.replace(/([0-2][6-9])\d{1,}/g,"$1");
        this.value = this.value.replace(/([0-2][0-5])[6-9]/g,"$1");
        this.value = this.value.replace(/(\d\d\d)\d{1,}/g,"$1");
		this.value = this.value.replace(/^(\d{1,3}\.\d{1,3}\.\d{1,3}\.\d{1,3})\./g,"$1");*/

        var IP = new IpAddress(this.value, 1);

        if (IP.value !== false) {
            $("#dottedBinary").val(IP.dottedBinary());
            $("#binary").val(IP.binary());
            $("#dottedHexadecimal").val(IP.dottedHex());
            $("#hexadecimal").val(IP.hex());
            $("#decimal").val(IP.value);
        } else {
            $("#dottedBinary").val("");
            $("#binary").val("");
            $("#dottedHexadecimal").val("");
            $("#hexadecimal").val("");
            $("#decimal").val("");
        }

    });
	
	$("#dottedBinary").on("input paste copy cut change drop keypress keydown keyup blur", function(event) {
        this.value = this.value.replace(/[^0-1\.]/g,"");
		var a = this.value.split(".");

        var IP = new IpAddress(parseInt(a[0],2)+"."+parseInt(a[1],2)+"."+parseInt(a[2],2)+"."+parseInt(a[3],2), 1);

        if (IP.value !== false) {
            $("#dottedDecimal").val(IP.dottedDecimal());
            $("#binary").val(IP.binary());
            $("#dottedHexadecimal").val(IP.dottedHex());
            $("#hexadecimal").val(IP.hex());
            $("#decimal").val(IP.value);
        } else {
            $("#dottedDecimal").val("");
            $("#binary").val("");
            $("#dottedHexadecimal").val("");
            $("#hexadecimal").val("");
            $("#decimal").val("");
        }

    });
	
	$("#dottedHexadecimal").on("input paste copy cut change drop keypress keydown keyup blur", function(event) {
		this.value = this.value.replace(/[^0-9a-fA-F\.]/g,"");
		var a = this.value.split(".");

        var IP = new IpAddress(parseInt(a[0],16)+"."+parseInt(a[1],16)+"."+parseInt(a[2],16)+"."+parseInt(a[3],16), 1);

        if (IP.value !== false) {
            $("#dottedDecimal").val(IP.dottedDecimal());
            $("#binary").val(IP.binary());
            $("#dottedBinary").val(IP.dottedBinary());
            $("#hexadecimal").val(IP.hex());
            $("#decimal").val(IP.value);
        } else {
            $("#dottedDecimal").val("");
            $("#binary").val("");
            $("#dottedBinary").val("");
            $("#hexadecimal").val("");
            $("#decimal").val("");
        }

    });
	
	$("#decimal").on("input paste copy cut change drop keypress keydown keyup blur", function(event) {
		this.value = this.value.replace(/[^0-9]/g,"");

        var IP = new IpAddress(parseInt(this.value ,10), 1, true);

        if (IP.value !== false) {
            $("#dottedDecimal").val(IP.dottedDecimal());
            $("#binary").val(IP.binary());
            $("#dottedBinary").val(IP.dottedBinary());
            $("#hexadecimal").val(IP.hex());
            $("#dottedHexadecimal").val(IP.dottedHex());
        } else {
            $("#dottedDecimal").val("");
            $("#binary").val("");
            $("#dottedBinary").val("");
            $("#hexadecimal").val("");
            $("#dottedHexadecimal").val("");
        }

    });
	
	$("#binary").on("input paste copy cut change drop keypress keydown keyup blur", function(event) {
		this.value = this.value.replace(/[^0-1]/g,"");

        var IP = new IpAddress(parseInt(this.value ,2), 1, true);

        if (IP.value !== false) {
            $("#dottedDecimal").val(IP.dottedDecimal());
            $("#decimal").val(IP.value);
            $("#dottedBinary").val(IP.dottedBinary());
            $("#hexadecimal").val(IP.hex());
            $("#dottedHexadecimal").val(IP.dottedHex());
        } else {
            $("#dottedDecimal").val("");
            $("#decimal").val("");
            $("#dottedBinary").val("");
            $("#hexadecimal").val("");
            $("#dottedHexadecimal").val("");
        }

    });
	
	$("#hexadecimal").on("input paste copy cut change drop keypress keydown keyup blur", function(event) {
		this.value = this.value.replace(/[^0-9a-fA-F]/g,"");

        var IP = new IpAddress(parseInt(this.value ,16), 1, true);

        if (IP.value !== false) {
            $("#dottedDecimal").val(IP.dottedDecimal());
            $("#decimal").val(IP.value);
            $("#dottedBinary").val(IP.dottedBinary());
            $("#binary").val(IP.binary());
            $("#dottedHexadecimal").val(IP.dottedHex());
        } else {
            $("#dottedDecimal").val("");
            $("#decimal").val("");
            $("#dottedBinary").val("");
            $("#binary").val("");
            $("#dottedHexadecimal").val("");
        }

    });
	
/////////////////////////////////
	
    $("#address").on("input paste copy cut change drop keypress keydown keyup blur", function(event) {
		this.value = this.value.replace(/[^0-9\.]/g,"");

        var IP = new IpAddress(this.value, 1);
		
		$("#class option[value='0']").remove();

        if (IP.value !== false) {
            $("#class").val(IP.nclass());
			$("#classAddressRange").html(IP.classAddressRange());
			$("#classBitUsage").html(IP.classBitUsage());
        } else {
            $("#class").append($("<option></option>").attr("value","0")).val("0");
			$("#classAddressRange").html("");
			$("#classBitUsage").html("");
        }
    });
	
    $("#class").on("change", function(event) {
		if(this.value == 1) {
			$("#address").val("0.0.0.0");
			$("#address").trigger("input");
		}
		if(this.value == 2) {
			$("#address").val("0.0.0.1");
			$("#address").trigger("input");
		}
		if(this.value == 3) {
			$("#address").val("1.0.0.0");
			$("#address").trigger("input");
		}
		if(this.value == 4) {
			$("#address").val("127.0.0.0");
			$("#address").trigger("input");
		}
		if(this.value == 5) {
			$("#address").val("128.0.0.0");
			$("#address").trigger("input");
		}
		if(this.value == 6) {
			$("#address").val("192.0.0.0");
			$("#address").trigger("input");
		}
		if(this.value == 7) {
			$("#address").val("224.0.0.0");
			$("#address").trigger("input");
		}
		if(this.value == 8) {
			$("#address").val("240.0.0.0");
			$("#address").trigger("input");
		}
		if(this.value == 9) {
			$("#address").val("255.255.255.255");
			$("#address").trigger("input");
		}
    });	
//////////////////////////////////////////////////////////////////
	
	$("#subnetBits, #maxSubnets, #subnetMask, #hostBits, #maxHosts").on("change", function() {
		var mask = this.value;
		$("#subnetBits, #maxSubnets, #subnetMask, #hostBits, #maxHosts").val(this.value);
		
		var a = $("#addressSubnets").val();
		
		a = a.split(".");
		if(a[1] == undefined || a[1] == "") {
			a[1] = "0";
		}
		if(a[2] == undefined || a[2] == "") {
			a[2] = "0";
		}
		if(a[3] == undefined || a[3] == "") {
			a[3] = "0";
		}
		
		IP = new IpAddress(a[0]+"."+a[1]+"."+a[2]+"."+a[3], $("#subnetMask").val());

			$("#subnetId").html(IP.network.dottedDecimal());
			$("#subnetBroadcast").html(IP.broadcast.dottedDecimal());
			$("#hostAddresses").html(IP.first.dottedDecimal()+"-"+IP.last.dottedDecimal());
			$("#subnetsBitUsage").html(IP.subnetsBitUsage());
			$("#wildcardMask").html(IP.wildcard.dottedDecimal());
	});
	
    $("#addressSubnets").on("input paste copy cut change drop keypress keydown keyup blur", function(event) {
		this.value = this.value.replace(/[^0-9\.]/g,"");

		var a = this.value;

		if(a == "" || a == undefined) {
			oldClass = 0;
		} else {
			a = a.split(".");
			if(a[1] == undefined || a[1] == "") {
				a[1] = "0";
			}
			if(a[2] == undefined || a[2] == "") {
				a[2] = "0";
			}
			if(a[3] == undefined || a[3] == "") {
				a[3] = "0";
			}
		}
		
		//alert(a[0]+"."+a[1]+"."+a[2]+"."+a[3]);
		
        var IP = new IpAddress(a[0]+"."+a[1]+"."+a[2]+"."+a[3], $("#subnetMask").val());
		
        if (IP.value !== false) {
			if(IP.nclass() == 3 || IP.nclass() == 5 || IP.nclass() == 6) {
				var max = 30;
				if($("#rfc3021").attr("checked")) {
					max = 31;
				}
				
				if(oldClass != IP.nclass()) {
					if(IP.nclass() == 3) {
						changeMaskRange(9,max);
					} else {
						if(IP.nclass() == 5) {
							changeMaskRange(17,max);
						} else {
							changeMaskRange(25,max);
						}
					}
					oldClass = IP.nclass();
				}
				
				IP = new IpAddress(IP.dottedDecimal(), $("#subnetMask").val());
				
				$("#subnetId").html(IP.network.dottedDecimal());
				$("#subnetBroadcast").html(IP.broadcast.dottedDecimal());
				$("#hostAddresses").html(IP.first.dottedDecimal()+"-"+IP.last.dottedDecimal());
				$("#subnetsBitUsage").html(IP.subnetsBitUsage());
				$("#wildcardMask").html(IP.wildcard.dottedDecimal());	
			} else {
				$("#subnetId").html("");
				$("#subnetBroadcast").html("");
				$("#hostAddresses").html("");
				$("#subnetsBitUsage").html("");
				$("#wildcardMask").html("");
				
				oldClass = 0;

				$("#subnetBits, #maxSubnets, #subnetMask, #hostBits, #maxHosts").empty();
			}
        } else {
        	$("#subnetId").html("");
        	$("#subnetBroadcast").html("");
			$("#hostAddresses").html("");
			$("#subnetsBitUsage").html("");
			$("#wildcardMask").html("");
			
			oldClass = 0;
			
			$("#subnetBits, #maxSubnets, #subnetMask, #hostBits, #maxHosts").empty();
        }
    });
	
	$("#rfc3021").on("change", function() {
		var max = 31;
		if (this.checked == false) {
			max = 30;
		}

		if(IP.nclass() == 3 || IP.nclass() == 5 || IP.nclass() == 6) {
			if (IP.value !== false) {
				if(IP.nclass() == 3) {
					changeMaskRange(9,max);
				} else {
					if(IP.nclass() == 5) {
						changeMaskRange(17,max);
					} else {
						changeMaskRange(25,max);
					}
				}
			}
		}
		
		var a = $("#addressSubnets").val();
		
		a = a.split(".");
		if(a[1] == undefined || a[1] == "") {
			a[1] = "0";
		}
		if(a[2] == undefined || a[2] == "") {
			a[2] = "0";
		}
		if(a[3] == undefined || a[3] == "") {
			a[3] = "0";
		}
		
		IP = new IpAddress(a[0]+"."+a[1]+"."+a[2]+"."+a[3], $("#subnetMask").val());

			$("#subnetId").html(IP.network.dottedDecimal());
			$("#subnetBroadcast").html(IP.broadcast.dottedDecimal());
			$("#hostAddresses").html(IP.first.dottedDecimal()+"-"+IP.last.dottedDecimal());
			$("#subnetsBitUsage").html(IP.subnetsBitUsage());
			$("#wildcardMask").html(IP.wildcard.dottedDecimal());
	});
	
// CIDR
	$("#cidrMask, #cidrMaximumSubnets, #cidrMaximumAddresses").on("change", function() {
		$("#cidrMask, #cidrMaximumSubnets, #cidrMaximumAddresses").val(this.value);
		
		var a = $("#cidrAddress").val();

		if(a == "" || a == undefined) {
			a = "0.0.0.0";
		} else {
			a = a.split(".");
			if(a[1] == undefined || a[1] == "") {
				a[1] = "0";
			}
			if(a[2] == undefined || a[2] == "") {
				a[2] = "0";
			}
			if(a[3] == undefined || a[3] == "") {
				a[3] = "0";
			}
		}
		IP = new IpAddress(a[0]+"."+a[1]+"."+a[2]+"."+a[3], $("#cidrMask").val());
		
		$("#cidrWildcardMask").html(IP.wildcard.dottedDecimal());
		
		if (IP.value !== false) {
        	$("#cidrAddressRange").html(IP.network.dottedDecimal() + " - " + IP.broadcast.dottedDecimal());
			$("#cidrNotation").html(IP.network.dottedDecimal()+"/"+IP.mask.bits());
        } else {
        	$("#cidrAddressRange").html("");
			$("#cidrNotation").html("");
        }
	});
	
    $("#cidrAddress").on("input paste copy cut change drop keypress keydown keyup blur", function(event) {
		this.value = this.value.replace(/[^0-9\.]/g,"");

		var a = this.value;

		if(a == "" || a == undefined) {
			a = "0.0.0.0";
		} else {
			a = a.split(".");
			if(a[1] == undefined || a[1] == "") {
				a[1] = "0";
			}
			if(a[2] == undefined || a[2] == "") {
				a[2] = "0";
			}
			if(a[3] == undefined || a[3] == "") {
				a[3] = "0";
			}
		}

        var IP = new IpAddress(a[0]+"."+a[1]+"."+a[2]+"."+a[3], $("#cidrMask").val());
		
        if (IP.value !== false) {
			$("#cidrAddress").css("color","gray");
        	$("#cidrNotation").html(IP.network.dottedDecimal()+"/"+IP.mask.bits());
        	$("#cidrAddressRange").html(IP.network.dottedDecimal() + " - " + IP.broadcast.dottedDecimal());
        } else {
        	$("#cidrAddressRange").html("");
			$("#cidrAddress").css("color","red");
			$("#cidrNotation").html("");
        }
    });
	
//Supernets
	$("#supernetsAggregate").on("click", function(event) {
		var goodNetworksCount = 0;
		var goodNetworks = new Array();
		var a;
		var sampleMask;
		var networks = $("#supernetsNetworks").val().split("\n");
		
		for( i = 0; i < networks.length; i++) {
			a = networks[i].split("/");
			
			IP = new IpAddress(a[0], 1);
			
			if (IP.value !== false) {
				if (a[1] == "" || a[1] == undefined) {
					a[1] = 32;
				}
				
				if (parseInt(a[1], 10) >= 1 && parseInt(a[1], 10) <= 32) {
					IP = new IpAddress(a[0], a[1]);
					goodNetworks[goodNetworksCount] = IP.network.value;
					goodNetworksCount++;
					sampleMask = a[1];
				} else {
					networks[i] = networks[i] + " *"; // Invalid
				}
			} else {
				networks[i] = networks[i] + " *"; // Invalid
			}
		}
		
		var g;
		var stop = false;
		var checkBit = sampleMask;
		var n1, n2;
		
		if(goodNetworksCount > 0) {
			if(goodNetworksCount > 1) {
				var checkBit = -1;
			
				while(!stop) {
					checkBit++;
					for(g = 0; g < goodNetworksCount - 1; g++) {
						n1 = new IpAddress(goodNetworks[g], 32, true);
						n2 = new IpAddress(goodNetworks[g+1], 32, true);
						
						if(n1.binary()[checkBit] != n2.binary()[checkBit]) {
							stop = true;
						}
					}
				}
			}
			var route = new IpAddress(goodNetworks[0], checkBit, true);
			route = route.network.dottedDecimal() + "/" + checkBit;
			
			alert(route);
		}
		else {
			alert("No good networks!");
		}
		
		$("#supernetsNetworks").val("");
		var k;
		for(k = 0; k < networks.length; k++) {
			if((k + 1) ==  networks.length) {
				$("#supernetsNetworks").val($("#supernetsNetworks").val() + networks[k]);
			} else {
				$("#supernetsNetworks").val($("#supernetsNetworks").val() + networks[k] + "\n");
			}
		}
	});
	
	$("form").submit(function(event) {
		if ($(this).attr("action") != "feedback.php") {
			return false;
		}
	})
});