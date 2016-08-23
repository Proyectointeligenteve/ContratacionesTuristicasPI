function CargarMaster() {
    //CargarEmergencias();
    CargarUsuarios();
    CargarReportes();
    //CargarFormularios();
}


function cargar_cerrar() {
    document.location.href = 'login.aspx';
}
function cargar_empleados() {
    document.location.href = 'lst_empleados.aspx';
}
function cargar_presentaciones() {
    document.location.href = 'lst_presentaciones.aspx';
}
function cargar_beneficiarios() {
    document.location.href = 'lst_beneficiarios.aspx';
}
function cargar_unidades() {
    document.location.href = 'lst_unidades.aspx';
}

function cargar_categorias() {
    document.location.href = 'lst_categorias.aspx';
}

function cargar_almacenes() {
    document.location.href = 'lst_almacenes.aspx';
}

function cargar_productos() {
    document.location.href = 'lst_productos.aspx';
}

function cargar_inventarios() {
    document.location.href = 'lst_inventarios.aspx';
}

function cargar_movimientos_tipos() {
    document.location.href = 'lst_movimientos_inventarios_tipos.aspx';
}

function cargar_movimientos() {
    document.location.href = 'lst_movimientos_inventarios.aspx';
}

function cargar_entradas() {
    document.location.href = 'lst_entradas.aspx';
}

function cargar_salidas() {
    document.location.href = 'lst_salidas.aspx';
}

function cargar_ContratacionesTuristicas() {
    document.location.href = 'lst_ContratacionesTuristicas.aspx';
}

function cargar_tickets() {
    document.location.href = 'lst_tickets.aspx';
}
function CargarUsuarios() {
    $.ajax({
        type: "POST",
        url: "principal.aspx?fn=cargar_usuarios",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            $("#dv_usuarios").html('');
            if (response.errorResult != "") {
                $("#dv_msj_error").html('<span class="glyphicon glyphicon-remove" onclick="cerrar_error()"></span>&nbsp;&nbsp;' + response.errorResult);
                $("#dv_msj_error").show();
                setTimeout(function () { $('#dv_msj_error').hide(); }, 10000);
            }
            else if (response.messageResult != "") {
                var var_htmlusuarios = '';
                var contador = 0;
                $.each(response.messageResult, function (i, item) {
                    var_htmlusuarios += "<div class='row record'>";
                    var_htmlusuarios += "<div class='col-sm-9'>" + item.des + "</div>";
                    var_htmlusuarios += "<div class='col-sm-3 text-right'><span class='glyphicon glyphicon-edit' onclick='editar_usuario(" + item.id + ")'></span>&nbsp;&nbsp;<span class='glyphicon glyphicon-remove' onclick='confirmar(" + String.fromCharCode(34) + "usuario" + String.fromCharCode(34) + "," + item.id + ")'></span></div>";
                    var_htmlusuarios += "</div>";
                });
                $("#dv_usuarios").html(var_htmlusuarios);
            }
            else if (response.warningResult != "") {
                $("#dv_msj_error").html('<span class="glyphicon glyphicon-remove" onclick="cerrar_error()"></span>&nbsp;&nbsp;' + response.warningResult);
                $("#dv_msj_error").show();
                setTimeout(function () { $('#dv_msj_error').hide(); }, 10000);
            }
        },
        error: function () {
            $('.loading').hide()
            $('.btn').show();
            $("#dv_msj_error").html('<span class="glyphicon glyphicon-remove" onclick="cerrar_error()"></span>&nbsp;&nbsp;' + 'Error de comunicación con el servidor. Función Cargar().');
            $("#dv_msj_error").show();
            setTimeout(function () { $('#dv_msj_error').hide(); }, 10000);
        }
    });
};

function cerrar_error() {
    $("#dv_msj_error").hide();
}

function cerrar_alerta() {
    $("#dv_msj_alerta").hide();
}

function nuevo_cliente() {
    document.location.href = 'frm_clientes.aspx';
}

function editar_cliente(id) {
    document.location.href = 'frm_clientes.aspx?id=' + id;
}

function nueva_data() {
    document.location.href = 'frm_datas.aspx';
}

function editar_data(id) {
    document.location.href = 'frm_datas.aspx?id=' + id;
}

function nueva_tabla(id) {
    document.location.href = 'frm_tablas.aspx?data=' + id;
}

function editar_tabla(id) {
    document.location.href = 'frm_tablas.aspx?id=' + id;
}

function nuevo_usuario() {
    document.location.href = 'frm_usuarios.aspx';
}

function editar_usuario(id) {
    document.location.href = 'frm_usuarios.aspx?id=' + id;
}

function ver_log() {
    document.location.href = 'lst_log.aspx';
}

function ver_campos(idt) {
    document.location.href = 'lst_campos.aspx?idt=' + idt;
}

function ver_registros(idt) {
    document.location.href = 'lst_registros.aspx?idt=' + idt;
}

function nuevo_formulario() {
    document.location.href = 'frm_formularios.aspx';
}

function editar_formulario(id) {
    document.location.href = 'frm_formularios.aspx?id=' + id;
}

var deletemodal;
function confirmar(mod, id) {
    $('#hf_eliminar_id').val(id)
    $('#hf_eliminar_mod').val(mod)
    deletemodal = $.remodal.lookup[$('[data-remodal-id=deletemodal]').data('remodal')];
    deletemodal.open();
}

function cerrar_eliminar() {
    deletemodal.close()
    $('#hf_eliminar_id').val(0)
    $('#hf_eliminar_mod').val('')
}

function eliminar() {
    var id = $('#hf_eliminar_id').val();
    var mod = $('#hf_eliminar_mod').val();
    if (id=='0') {
        return false;
    }

    $('.loading').show()
    $('.btn').hide();
    $.ajax({
        type: "POST",
        url: "principal.aspx?fn=eliminar",
        data: '{"id":"' + id + '","mod":"' + mod + '"}',
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            deletemodal.close();
            $('.loading').hide()
            $('.btn').show();

            if (response.error == '-1') {
                window.location.href = 'info.aspx';
                return false;
            }

            if (response.rslt == 'exito') {
                $("#dv_msj_alerta").html('<span class="glyphicon glyphicon-remove" onclick="cerrar_error()"></span>&nbsp;&nbsp;' + 'El registro ha sido eliminado.');
                $("#dv_msj_alerta").show();
                setTimeout(function () { $('#dv_msj_alerta').hide(); }, 10000);
                CargarClientes()
                CargarUsuarios()
                CargarDatas()
                CargarFormularios()                
            }
            else {
                $("#dv_msj_error").html(response.msj);
                $("#dv_msj_error").show();
                setTimeout(function () { $('#dv_msj_error').hide(); }, 10000);
            }
        },
        error: function () {
            deletemodal.close();
            $('.loading').hide()
            $('.btn').show();
            $("#dv_msj_error").html('<span class="glyphicon glyphicon-remove" onclick="cerrar_error()"></span>&nbsp;&nbsp;' + 'Error de comunicación con el servidor. Función Eliminar().');
            $("#dv_msj_error").show();
            setTimeout(function () { $('#dv_msj_error').hide(); }, 10000);
        }
    });
}

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

function tecla_valida_fiscal(tecla) {
    if (tecla == 0) { return true }
    if (tecla == 38) { return true }
    if (tecla == 48) { return true }
    if (tecla == 49) { return true }
    if (tecla == 50) { return true }
    if (tecla == 51) { return true }
    if (tecla == 52) { return true }
    if (tecla == 53) { return true }
    if (tecla == 54) { return true }
    if (tecla == 55) { return true }
    if (tecla == 56) { return true }
    if (tecla == 57) { return true }
    if (tecla == 65) { return true }
    if (tecla == 66) { return true }
    if (tecla == 67) { return true }
    if (tecla == 68) { return true }
    if (tecla == 69) { return true }
    if (tecla == 70) { return true }
    if (tecla == 71) { return true }
    if (tecla == 72) { return true }
    if (tecla == 73) { return true }
    if (tecla == 74) { return true }
    if (tecla == 75) { return true }
    if (tecla == 76) { return true }
    if (tecla == 77) { return true }
    if (tecla == 78) { return true }
    if (tecla == 79) { return true }
    if (tecla == 80) { return true }
    if (tecla == 81) { return true }
    if (tecla == 82) { return true }
    if (tecla == 83) { return true }
    if (tecla == 84) { return true }
    if (tecla == 85) { return true }
    if (tecla == 86) { return true }
    if (tecla == 87) { return true }
    if (tecla == 88) { return true }
    if (tecla == 89) { return true }
    if (tecla == 90) { return true }


    if (tecla == 97) { return true }
    if (tecla == 98) { return true }
    if (tecla == 99) { return true }
    if (tecla == 100) { return true }
    if (tecla == 101) { return true }
    if (tecla == 102) { return true }
    if (tecla == 103) { return true }
    if (tecla == 104) { return true }
    if (tecla == 105) { return true }
    if (tecla == 106) { return true }
    if (tecla == 107) { return true }
    if (tecla == 108) { return true }
    if (tecla == 109) { return true }
    if (tecla == 110) { return true }
    if (tecla == 111) { return true }
    if (tecla == 112) { return true }
    if (tecla == 113) { return true }
    if (tecla == 114) { return true }
    if (tecla == 115) { return true }
    if (tecla == 116) { return true }
    if (tecla == 117) { return true }
    if (tecla == 118) { return true }
    if (tecla == 119) { return true }
    if (tecla == 120) { return true }
    if (tecla == 121) { return true }
    if (tecla == 122) { return true }


    if (tecla == 32) { return true }
    if (tecla == 44) { return true }
    if (tecla == 45) { return true }
    if (tecla == 46) { return true }

    return false;
}

function tecla_valida_rif(tecla) {
    if (tecla == 0) { return true }

    if (tecla == 48) { return true }
    if (tecla == 49) { return true }
    if (tecla == 50) { return true }
    if (tecla == 51) { return true }
    if (tecla == 52) { return true }
    if (tecla == 53) { return true }
    if (tecla == 54) { return true }
    if (tecla == 55) { return true }
    if (tecla == 56) { return true }
    if (tecla == 57) { return true }
    if (tecla == 69) { return true }
    if (tecla == 71) { return true }
    if (tecla == 74) { return true }
    if (tecla == 86) { return true }
    if (tecla == 101) { return true }
    if (tecla == 103) { return true }
    if (tecla == 106) { return true }
    if (tecla == 118) { return true }

    return false;
}