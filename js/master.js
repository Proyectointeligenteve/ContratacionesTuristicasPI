function Seleccionado(oTableLocal) {
    var aReturn = new Array();
    var aTrs = oTableLocal.fnGetNodes();

    for (var i = 0 ; i < aTrs.length ; i++) {
        if ($(aTrs[i]).hasClass('row_selected')) {
            aReturn.push(aTrs[i].id);
        }
    }

    return aReturn;
}

 function validateEmail(value) {
    var regex = /^([\w-\.]+@([\w-]+\.)+[\w-]{2,4})?$/;
    return (regex.test(value)) ? true : false;
}



function tecla_valida_fiscal(tecla){
	if(tecla==0) {return true}
	if(tecla==38) {return true}
	if(tecla==48) {return true}
	if(tecla==49) {return true}
	if(tecla==50) {return true}
	if(tecla==51) {return true}
	if(tecla==52) {return true}
	if(tecla==53) {return true}
	if(tecla==54) {return true}
	if(tecla==55) {return true}
	if(tecla==56) {return true}
	if(tecla==57) {return true}
	if(tecla==65) {return true}
	if(tecla==66) {return true}
	if(tecla==67) {return true}
	if(tecla==68) {return true}
	if(tecla==69) {return true}
	if(tecla==70) {return true}
	if(tecla==71) {return true}
	if(tecla==72) {return true}
	if(tecla==73) {return true}
	if(tecla==74) {return true}
	if(tecla==75) {return true}
	if(tecla==76) {return true}
	if(tecla==77) {return true}
	if(tecla==78) {return true}
	if(tecla==79) {return true}
	if(tecla==80) {return true}
	if(tecla==81) {return true}
	if(tecla==82) {return true}
	if(tecla==83) {return true}
	if(tecla==84) {return true}
	if(tecla==85) {return true}
	if(tecla==86) {return true}
	if(tecla==87) {return true}
	if(tecla==88) {return true}
	if(tecla==89) {return true}
	if(tecla==90) {return true}


	if(tecla==97) {return true}
	if(tecla==98) {return true}
	if(tecla==99) {return true}
	if(tecla==100) {return true}
	if(tecla==101) {return true}
	if(tecla==102) {return true}
	if(tecla==103) {return true}
	if(tecla==104) {return true}
	if(tecla==105) {return true}
	if(tecla==106) {return true}
	if(tecla==107) {return true}
	if(tecla==108) {return true}
	if(tecla==109) {return true}
	if(tecla==110) {return true}
	if(tecla==111) {return true}
	if(tecla==112) {return true}
	if(tecla==113) {return true}
	if(tecla==114) {return true}
	if(tecla==115) {return true}
	if(tecla==116) {return true}
	if(tecla==117) {return true}
	if(tecla==118) {return true}
	if(tecla==119) {return true}
	if(tecla==120) {return true}
	if(tecla==121) {return true}
	if(tecla==122) {return true}


	if(tecla==32) {return true}
	if(tecla==44) {return true}
	if(tecla==45) {return true}
	if(tecla==46) {return true}

	return false;	
}


function tecla_valida_rif(tecla){
	if(tecla==0) {return true}

	if(tecla==48) {return true}
	if(tecla==49) {return true}
	if(tecla==50) {return true}
	if(tecla==51) {return true}
	if(tecla==52) {return true}
	if(tecla==53) {return true}
	if(tecla==54) {return true}
	if(tecla==55) {return true}
	if(tecla==56) {return true}
	if(tecla==57) {return true}
	if(tecla==69) {return true}
	if(tecla==71) {return true}
	if(tecla==74) {return true}
	if(tecla==86) {return true}
	if(tecla==101) {return true}
	if(tecla==103) {return true}
	if(tecla==106) {return true}
	if(tecla==118) {return true}
	
	return false;	
}
