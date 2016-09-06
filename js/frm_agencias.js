    /* 
 * PARENT CODE
 */
function backToList() {
    var var_Id = $('#hdn_id_agencia').val();
    window.location.href = 'lst_agencias.aspx';
};
function load() {
    Permisos()

    $("#Rif").focus();
    CargarPaises()
    CargarEstados()
    CargarCiudades()
    //Cargarcontactos()
    CargarTipos()
    $("#dv_Message").hide();
    $("#dv_Error").hide();
    $("#dv_Error_modal").hide();
    $("#dv_Error_modal").hide();

    $("#LimiteCredito").blur(function () {
        var number = $(this).val();
        number = number.replace('.', '');
        number = number.replace(',', '.');
        number = $.formatNumber(number, { format: "#,##0.00", locale: "es" });
        $("#LimiteCredito").val(number);
    });

    $("#Comision").blur(function () {
        var number = $(this).val();
        number = number.replace('.', '');
        number = number.replace(',', '.');
        number = $.formatNumber(number, { format: "#,##0.00", locale: "es" });
        $("#Comision").val(number);
    });

    var recordId = $.url().param('id');
    var idt1 = $.url().param('idcargo');

    $('.loading').show()
    $('.btn').hide();
    $.ajax({
        type: "POST",
        url: "frm_agencias.aspx?fn=loadAll",
        data: '{"id":"' + recordId + '"}',
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            $('.loading').hide()
            $('.btn').show();

            if (response.error == '-1') {
                window.location.href = 'info.aspx';
                return false;
            }
            if (response.rslt == 'exito') {
                $('#hdn_id').val(response.hdn_id);
                $('#Rif').val(response.Rif);
                $('#Nombre').val(response.Nombre);
                $('#RazonSocial').val(response.RazonSocial);
                $('#Direccion').val(response.Direccion);
                $('#TelefonoF').val(response.TelefonoF);
                $('#TelefonoM').val(response.TelefonoM);
                $('#Web').val(response.Web);
                $('#Email').val(response.Email);
                $('#Codigo').val(response.Codigo);
                $('#LimiteCredito').val(response.LimiteCredito);
                $('#hfPais').val(response.Pais);
                $('#hfEstado').val(response.Estado);
                $('#hfCiudad').val(response.Ciudad);
                CargarPaises()
                //professionLoad()
                contactoLoad()
                comisionLoad()
            }
            else {
                $("#dv_Message").hide()
                $("#dv_Error").html(response.msj);
                $("#dv_Error").show();
                setTimeout(function () { $('#dv_Error').hide(); }, 10000);
            }
        },
        error: function () {
            $('.loading').hide()
            $('.btn').show();
            $("#dv_Error").html('Error de comunicación con el servidor. El record no ha sido cargado.');
            $("#dv_Error").show();
            setTimeout(function () { $('#dv_Error').hide(); }, 10000);
        }
    });
};

function Permisos() {
    var v = $.url().param('v');
    $('.loading').show()
    $('.btn').hide();
    $.ajax({
        type: "POST",
        url: "frm_agencias.aspx?fn=permisos&v=" + v,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            $('.loading').hide()
            $('.btn').show();

            if (response.error == '-1') {
                window.location.href = 'info.aspx';
                return false;
            }

            if (response.rslt == 'exito') {
                if (response.Ver == 1) {
                    $("#btn_guardar").addClass('hide');
                    $("#Nombre").attr("disabled", "disabled");

                    $('#btn_contactoAdd').attr("disabled", "disabled");
                    $('#btn_contactoEdit').attr("disabled", "disabled");
                    $('#btn_contactoDelete').attr("disabled", "disabled");

                    $('#btn_comisionAdd').attr("disabled", "disabled");
                    $('#btn_comisionEdit').attr("disabled", "disabled");
                    $('#btn_comisionDelete').attr("disabled", "disabled");

                    $("#Rif").focus();
                }
            }
            else {
                $("#dv_Error").html(response.msj);
                $("#dv_Error").show();
                setTimeout(function () { $('#dv_Error').hide(); }, 10000);
            }
        },
        error: function () {
            $('.loading').hide()
            $('.btn').show();
            $("#dv_Error").html('Error de comunicación con el servidor. Función Permisos().');
            $("#dv_Error").show();
            setTimeout(function () { $('#dv_Error').hide(); }, 10000);
        }
    });
}
    //var msjModal
function save() {
        var record = {};
        record.hdn_id = $('#hdn_id').val();
        record.Rif = $('#Rif').val();
        record.Nombre = $('#Nombre').val();
        record.RazonSocial = $('#RazonSocial').val();
        record.Direccion = $('#Direccion').val();
        record.TelefonoF = $('#TelefonoF').val();
        record.TelefonoM = $('#TelefonoM').val();
        record.Web = $('#Web').val();
        record.Email = $('#Email').val();
        record.Codigo = $('#Codigo').val();
        record.LimiteCredito = $('#LimiteCredito').val();
        record.Pais = $('#Pais').val();
        record.Estado = $('#Estado').val();
        record.Ciudad = $('#Ciudad').val();
    
        $('.loading').show()
        $('.btn').hide();
        $.ajax({
            type: "POST",
            url: "frm_agencias.aspx?fn=saveAll",
            data: JSON.stringify(record),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (response) {

                $('.loading').hide()
                $('.btn').show();

                if (response.error == '-1') {
                    window.location.href = 'info.aspx';
                    return false;
                }
                if (response.rslt == 'exito') {
                    var options = {
                        "backdrop": "static",
                        "keyboard": "true"
                    }

                    $('#hdn_id').val(response.hdn_id);
                    msjModal = $.remodal.lookup[$('[data-remodal-id=msjModal]').data('remodal')];
                    msjModal.open();
                    //$('#msjModal').modal(options);
                    //$('#msjModal').on('shown.bs.modal', function () {
                    if (response.msj != '') {
                        $("#msj").html("El record ha sido guardado con errores: " + response.msj);
                    }
                    else {
                        $('#hdn_id').val(response.id)
                        $("#msj").html("El record ha sido guardado con exito.");
                    }
                    //})
                }
                else {
                    $("#dv_Error").html(response.msj);
                    $("#dv_Error").show();
                    setTimeout(function () { $('#dv_Error').hide(); }, 10000);
                }
            },
            error: function () {

                $('.loading').hide()
                $('.btn').show();
                $("#dv_Error").html('Error de comunicación con el servidor. El record no ha sido actualizado.');
                $("#dv_Error").show();
                setTimeout(function () { $('#dv_Error').hide(); }, 10000);
            }
        });
    }
//}
    function validate() {
        var result = $("#form1").valid();
        return result;
    };
    
/* contacto CODE */
    function contactoAdd() {
        $('#hdn_contactoId').val(0);
        
        var options = {
            "backdrop": "static",
            "keyboard": "true"
        }

        modal = $.remodal.lookup[$('[data-remodal-id=modal]').data('remodal')];
        modal.open();
        $('#NombreC').focus();
    };
    var deletemodal;

    function contactoConfirm() {
        var id = '';
        $('#tbl_contacto tr').each(function () {
            if ($(this).hasClass('row_selected')) {
                id += ',' + this.id;
            }
        });
        if (id == '') {
            $("#dv_Error").html('Seleccione un registro');
            $("#dv_Error").show();
            setTimeout(function () { $('#dv_Error').hide(); }, 10000);
            return false;
        }
        var options = {
            "backdrop": "static",
            "keyboard": "true"
        }
        deleteModalCliente = $.remodal.lookup[$('[data-remodal-id=deleteModalCliente]').data('remodal')];
        deleteModalCliente.open();
    };

    function contactoDelete() {
        var id = '';
        $('#tbl_contacto tr').each(function () {
            if ($(this).hasClass('row_selected')) {
                id += ',' + this.id;
            }
        });
        if (id == '') {
            $("#dv_Error").html('Seleccione un registro');
            $("#dv_Error").show();
            setTimeout(function () { $('#dv_Error').hide(); }, 10000);
            return false;
        }

        $('.loading').show()
        $('.btn').hide();
        $.ajax({
            type: "POST",
            url: "frm_agencias.aspx?fn=contactoDelete",
            data: '{"hdn_contactoId":"' + id + '"}',
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (response) {
                $('.loading').hide()
                $('.btn').show();
                deletemodal.close()
                if (response.rslt == 'exito') {
                    $("#dv_Error2").hide()
                    $("#dv_Message2").html('El record ha sido eliminado.');
                    $("#dv_Message2").show();
                    setTimeout(function () { $('#dv_Message2').hide(); }, 10000);
                    $('#tbl_contacto').dataTable().fnDestroy();
                    contactoLoad();
                }
                else {
                    $("#dv_Error2").html(response.msj);
                    $("#dv_Error2").show();
                    setTimeout(function () { $('#dv_Error2').hide(); }, 10000);
                }
            },
            error: function () {
                $('.loading').hide()
                $('.btn').show();
                //$('#deletemodal').modal('hide');
                deletemodal.close()
                $("#dv_Error2").html('Error de comunicación con el servidor. El record no ha sido actualizado.');
                $("#dv_Error2").show();
                setTimeout(function () { $('#dv_Error2').hide(); }, 10000);
            }
        });
    };
    function contactoEdit() {
        var id
        $('#tbl_contacto tr').each(function () {
            if ($(this).hasClass('row_selected')) {
                id = this.id;
                $("#hdn_contactoId").val(id);
            }
        });
        if (id == '') {
            $("#dv_Error").html('Seleccione un registro');
            $("#dv_Error").show();
            setTimeout(function () { $('#dv_Error').hide(); }, 10000);
            return false;
        }
        $('.loading').show()
        $('.btn').hide();
        $.ajax({
            type: "POST",
            url: "frm_agencias.aspx?fn=contactoEdit",
            data: '{"hdn_contactoId":"' + id + '"}',
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (response) {
                $('.loading').hide()
                $('.btn').show();

                if (response.rslt == 'exito') {
                    $('#NombreC').val(response.NombreC);
                    $('#CargoC').val(response.CargoC);
                    $('#TelefonoC').val(response.TelefonoC);
                    var options = {
                        "backdrop": "static",
                        "keyboard": "true"
                    }
                    modal = $.remodal.lookup[$('[data-remodal-id=modal]').data('remodal')];
                    modal.open();
                    //$('#basicmodal').modal(options);
                    //$('#basicmodal').on('shown.bs.modal', function () {
                    //})
                }
                else {
                    $("#dv_Message2").hide()
                    $("#dv_Error2").html(response.msj);
                    $("#dv_Error2").show();
                    setTimeout(function () { $('#dv_Error').hide(); }, 10000);
                }
            },
            error: function () {
                $('.loading').hide()
                $('.btn').show();
                basicmodal.close();
                //$('#basicmodal').modal('hide');
                $("#dv_Error2").html('Error de comunicación con el servidor. El record no ha sido actualizado.');
                $("#dv_Error2").show();
                setTimeout(function () { $('#dv_Error').hide(); }, 10000);
            }
        });
    };

        var pTable;
    function contactoLoad() {
        $('#tbl_contacto').dataTable().fnDestroy();
        $("#tbl_contacto tbody").click(function (event) {
            $(pTable.fnSettings().aoData).each(function () {
                $(this.nTr).removeClass('row_selected');
            });
            $(event.target.parentNode).addClass('row_selected');
        });
        pTable = $('#tbl_contacto').dataTable({
            "bProcessing": true,
            "bServerSide": false,
            "sAjaxSource": "frm_agencias.aspx?fn=contactoLoad",
            "bFilter": false,
            "bLengthChange": false,
            "bPaginate": false,
            "bInfo": false,
            "oLanguage": {
                "sInfo": "_TOTAL_ Registro(s) encontrado(s)",
                "sInfoFiltered": " - de _MAX_ registros",
                "sInfoThousands": ",",
                "sLengthMenu": "Mostrar _MENU_ Registros",
                "sLoadingRecords": "Por favor espere  - CARGANDO...",
                "sProcessing": "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;PROCESANDO...",
                "sSearch": "",
                "sZeroRecords": "No se encontraron registros",
                "oPaginate": {
                    "sNext": " SIGUIENTE",
                    "sPrevious": "ANTERIOR "
                }
            },
            "aoColumns": [
                { "mDataProp": "Nombre" },
                { "mDataProp": "Cargo" },
                { "mDataProp": "Telefono" }
            ]
        });
    };
    function contactoSave() {
        var record = {};
        record.hdn_contactoId = $('#hdn_contactoId').val();
        record.NombreC = $('#NombreC').val();
        record.CargoC = $('#CargoC').val();
        record.TelefonoC = $('#TelefonoC').val();
        $('.loading').show()
        $('.btn').hide();
        $.ajax({
            type: "POST",
            url: "frm_agencias.aspx?fn=contactoSave",
            data: JSON.stringify(record),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (response) {
                $('.loading').hide()
                $('.btn').show();
                modal.close();
                //$('#basicmodal').modal('hide');
                if (response.rslt == 'exito') {
                    $("#dv_Error2").hide()
                    $("#dv_Message2").html('El record ha sido procesado con exito.');
                    $("#dv_Message2").show();
                    setTimeout(function () { $('#dv_Message2').hide(); }, 10000);
                    contactoLoad()
                }
                else {
                    $("#dv_Error2").html(response.msj);
                    $("#dv_Error2").show();
                    setTimeout(function () { $('#dv_Error2').hide(); }, 10000);
                }
            },
            error: function () {
                $('.loading').hide()
                $('.btn').show();
                //$('#basicmodal').modal('hide');
                basicmodal.close();
                $("#dv_Error2").html('Error de comunicación con el servidor. El record no ha sido actualizado.');
                $("#dv_Error2").show();
                setTimeout(function () { $('#dv_Error2').hide(); }, 10000);
            }
        });
    };
    function contactoValidate() {
        var result = $("#form2").valid();
        return result;
    };
/* End contacto CODE */
/* comision CODE */
    function comisionAdd() {
        $('#hdn_comisionId').val(0);
        // $('#comision').val(0);

        var options = {
            "backdrop": "static",
            "keyboard": "true"
        }

        modalC = $.remodal.lookup[$('[data-remodal-id=modalC]').data('remodal')];
        modalC.open();
        $('#Tipo').focus();
        //$('#basicmodal').modal(options);
        //$('#basicmodal').on('shown.bs.modal', function () {

        //})
    };
    var deletemodal;
    function comisionConfirm() {
        var options = {
            "backdrop": "static",
            "keyboard": "true"
        }
        deleteModal3 = $.remodal.lookup[$('[data-remodal-id=deleteModal3]').data('remodal')];
        deleteModal3.open();
    };
    function comisionDelete() {
        var id = '';
        $('#tbl_comision tr').each(function () {
            if ($(this).hasClass('row_selected')) {
                id += ',' + this.id;
            }
        });
        if (id == '') {
            $("#dv_Error").html('Seleccione un registro');
            $("#dv_Error").show();
            setTimeout(function () { $('#dv_Error').hide(); }, 10000);
            return false;
        }

        $('.loading').show()
        $('.btn').hide();
        $.ajax({
            type: "POST",
            url: "frm_agencias.aspx?fn=comisionDelete",
            data: '{"hdn_comisionId":"' + id + '"}',
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (response) {
                $('.loading').hide()
                $('.btn').show();
                deletemodal3.close()
                //$('#deletemodal').modal('hide');
                if (response.rslt == 'exito') {
                    $("#dv_Error3").hide()
                    $("#dv_Message3").html('El record ha sido eliminado.');
                    $("#dv_Message3").show();
                    setTimeout(function () { $('#dv_Message2').hide(); }, 10000);
                    $('#tbl_comision').dataTable().fnDestroy();
                    comisionLoad();
                }
                else {
                    $("#dv_Error3").html(response.msj);
                    $("#dv_Error3").show();
                    setTimeout(function () { $('#dv_Error3').hide(); }, 10000);
                }
            },
            error: function () {
                $('.loading').hide()
                $('.btn').show();
                //$('#deletemodal').modal('hide');
                deletemodal.close()
                $("#dv_Error3").html('Error de comunicación con el servidor. El record no ha sido actualizado.');
                $("#dv_Error3").show();
                setTimeout(function () { $('#dv_Error3').hide(); }, 10000);
            }
        });
    };
    function comisionEdit() {
        var id
        $('#tbl_comision tr').each(function () {
            if ($(this).hasClass('row_selected')) {
                id = this.id;
                $("#hdn_comisionId").val(id);
            }
        });
        if (id == '') {
            $("#dv_Error").html('Seleccione un registro');
            $("#dv_Error").show();
            setTimeout(function () { $('#dv_Error').hide(); }, 10000);
            return false;
        }
        $('.loading').show()
        $('.btn').hide();
        $.ajax({
            type: "POST",
            url: "frm_agencias.aspx?fn=comisionEdit",
            data: '{"hdn_comisionId":"' + id + '"}',
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (response) {
                $('.loading').hide()
                $('.btn').show();

                if (response.rslt == 'exito') {
                    $('#Tipo').val(response.Tipo);
                    $('#Comision').val(response.Comision);
                    $('#Forma').val(response.Forma);
                    if (response.Tipo == 1) {
                        CargarAerolineas();
                        $('#hfAerolineas').val(response.Detalle);
                        $("#div_aerolinea").removeClass('hide');
                    }
                    if (response.Tipo == 2) {
                        CargarHoteles();
                        $('#hfHoteles').val(response.Detalle);
                        $("#div_hotel").removeClass('hide');
                    }
                    if (response.Tipo == 3) {
                        CargarVehiculos();
                        $('#hfVehiculos').val(response.Detalle);
                        $("#div_vehiculo").removeClass('hide');
                    }
                    var options = {
                        "backdrop": "static",
                        "keyboard": "true"
                    }
                    modalC = $.remodal.lookup[$('[data-remodal-id=modalC]').data('remodal')];
                    modalC.open();
                }
                else {
                    $("#dv_Message3").hide()
                    $("#dv_Error3").html(response.msj);
                    $("#dv_Error3").show();
                    setTimeout(function () { $('#dv_Error3').hide(); }, 10000);
                }
            },
            error: function () {
                $('.loading').hide()
                $('.btn').show();
                basicmodal.close();
                //$('#basicmodal').modal('hide');
                $("#dv_Error3").html('Error de comunicación con el servidor. El record no ha sido actualizado.');
                $("#dv_Error3").show();
                setTimeout(function () { $('#dv_Error3').hide(); }, 10000);
            }
        });
    };

    var pTable;
    function comisionLoad() {
        $('#tbl_comision').dataTable().fnDestroy();
        $("#tbl_comision tbody").click(function (event) {
            $(pTable.fnSettings().aoData).each(function () {
                $(this.nTr).removeClass('row_selected');
            });
            $(event.target.parentNode).addClass('row_selected');
        });
        pTable = $('#tbl_comision').dataTable({
            "bProcessing": true,
            "bServerSide": false,
            "sAjaxSource": "frm_agencias.aspx?fn=comisionLoad",
            "bFilter": false,
            "bLengthChange": false,
            "bPaginate": false,
            "bInfo": false,
            "oLanguage": {
                "sInfo": "_TOTAL_ Registro(s) encontrado(s)",
                "sInfoFiltered": " - de _MAX_ registros",
                "sInfoThousands": ",",
                "sLengthMenu": "Mostrar _MENU_ Registros",
                "sLoadingRecords": "Por favor espere  - CARGANDO...",
                "sProcessing": "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;PROCESANDO...",
                "sSearch": "",
                "sZeroRecords": "No se encontraron registros",
                "oPaginate": {
                    "sNext": " SIGUIENTE",
                    "sPrevious": "ANTERIOR "
                }
            },
            "aoColumns": [{
                    "mDataProp": "Agencia",
                    "bVisible": false
                },
                { "mDataProp": "Tipo" },
                { "mDataProp": "Tipo2" },
                { "mDataProp": "Comision" }
            ]
        });
    };
    function comisionSave() {
        var record = {};
        record.hdn_comisionId = $('#hdn_comisionId').val();
        record.Tipo = $('#Tipo').val();
        record.Comision = $('#Comision').val();
        record.Forma = $('#Forma').val();
        var t = $('#Tipo').val();
        if (t ==1) {
            record.Detalle = $('#Aerolineas').val();
        }
        if (t ==2) {
            record.Detalle = $('#Hoteles').val();
        }
        if (t ==3) {
            record.Detalle = $('#Vehiculos').val();
        }
        
        $('.loading').show()
        $('.btn').hide();
        $.ajax({
            type: "POST",
            url: "frm_agencias.aspx?fn=comisionSave",
            data: JSON.stringify(record),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (response) {
                $('.loading').hide()
                $('.btn').show();
                modalC.close();
                //$('#basicmodal').modal('hide');
                if (response.rslt == 'exito') {
                    $("#dv_Error2").hide()
                    $("#dv_Message2").html('El record ha sido procesado con exito.');
                    $("#dv_Message2").show();
                    setTimeout(function () { $('#dv_Message2').hide(); }, 10000);
                    comisionLoad()
                }
                else {
                    $("#dv_Error2").html(response.msj);
                    $("#dv_Error2").show();
                    setTimeout(function () { $('#dv_Error2').hide(); }, 10000);
                }
            },
            error: function () {
                $('.loading').hide()
                $('.btn').show();
                //$('#basicmodal').modal('hide');
                modalC.close();
                $("#dv_Error2").html('Error de comunicación con el servidor. El record no ha sido actualizado.');
                $("#dv_Error2").show();
                setTimeout(function () { $('#dv_Error2').hide(); }, 10000);
            }
        });
    };
    function comisionValidate() {
        var result = $("#form3").valid();
        return result;
    };
    /* 
    * End comision CODE
    */

    function buscarAgencia() {
        var rif = $("#Rif").val();

        $('.loading').show()
        $('.btn').hide();
        $.ajax({
            type: "POST",
            url: "frm_agencias.aspx?fn=validar_agencia",
            data: '{"rif":"' + rif + '"}',
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (response) {
                $('.loading').hide()
                $('.btn').show();

                if (response.error == '-1') {
                    window.location.href = 'info.aspx';
                    return false;
                }
                if (response.rslt == 'exito') {
                    $("#Rif").val('');
                    $("#Rif").focus();
                    $("#dv_Error").html("La Agencia '" + response.Nombre + "' ya esta registrada.");
                    $("#dv_Error").show();
                    setTimeout(function () { $('#dv_Error').hide(); }, 10000);
                    return false;
                }
                if (response.rslt == '') {
                    $("#dv_Error").show();
                    return false;
                }
                if (response.rslt == 'vacio') {
                    $("#Nombre").focus();
                    return false;
                }
                else {
                    $("#dv_Error").html(response.msj);
                    $("#dv_Error").show();
                    setTimeout(function () { $('#dv_Error').hide(); }, 10000);
                    $("#Rif").focus();
                }
            },
            error: function () {
                $('.loading').hide()
                $('.btn').show();
                $("#dv_Error").html('Error de comunicación con el servidor. Funcion CargarCliente().');
                $("#dv_Error").show();
                setTimeout(function () { $('#dv_Error').hide(); }, 10000);
                $("#rif").focus();
            }
        });

    }
    function CargarPaises() {
        $("#Pais").empty();
        $.ajax({
            type: "POST",
            url: "frm_agencias.aspx?fn=cargar_paises",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (response) {
                $.each(response.aaData, function (i, item) {
                    if (item.id == 0) {
                        $("#Pais").append("<option value=" + item.id + " selected='selected'>" + item.des + "</option>");
                    }
                    else {
                        $("#Pais").append("<option value=" + item.id + ">" + item.des + "</option>");
                    }
                });
                $("#Pais").val($("#hfPais").val());
                CargarEstados()
            },
            error: function () {
                $("#dv_mensaje").hide();
                $("#dv_Error").html('Error de comunicación con el servidor al intentar cargar la funcion Pais.');
                $("#dv_Error").show();
                setTimeout(function () { $('#dv_Error').hide(); }, 10000);
            }
        });
    }
    function CargarEstados() {
        var p = $("#Pais").val();
        $("#Estado").empty();
        $.ajax({
            type: "POST",
            url: "frm_agencias.aspx?fn=cargar_estados&p=" + p,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (response) {
                $.each(response.aaData, function (i, item) {
                    if (item.id == 0) {
                        $("#Estado").append("<option value=" + item.id + " selected='selected'>" + item.des + "</option>");
                    }
                    else {
                        $("#Estado").append("<option value=" + item.id + ">" + item.des + "</option>");
                    }
                });
                $("#Estado").val($("#hfEstado").val());
                CargarCiudades();
            },
            error: function () {
                $("#dv_mensaje").hide();
                $("#dv_Error").html('Error de comunicación con el servidor al intentar cargar la funcion Estados.');
                $("#dv_Error").show();
                setTimeout(function () { $('#dv_Error').hide(); }, 10000);
            }
        });
    }
    function CargarCiudades() {
        $("#Ciudad").empty();
        var e = $("#Estado").val();
        $.ajax({
            type: "POST",
            url: "frm_agencias.aspx?fn=cargar_ciudades&e=" + e,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (response) {
                $.each(response.aaData, function (i, item) {
                    if (item.id == 0) {
                        $("#Ciudad").append("<option value=" + item.id + " selected='selected'>" + item.des + "</option>");
                    }
                    else {
                        $("#Ciudad").append("<option value=" + item.id + ">" + item.des + "</option>");
                    }
                });
                $("#Ciudad").val($("#hfCiudad").val());
            },
            error: function () {
                $("#dv_mensaje").hide();
                $("#dv_Error").html('Error de comunicación con el servidor al intentar cargar la funcion Ciudades.');
                $("#dv_Error").show();
                setTimeout(function () { $('#dv_Error').hide(); }, 10000);
            }
        });
    }
    function CargarTipos() {
        $("#Tipo").empty();;
        $.ajax({
            type: "POST",
            url: "frm_agencias.aspx?fn=cargar_tipos",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (response) {
                $.each(response.aaData, function (i, item) {
                    if (item.id == 0) {
                        $("#Tipo").append("<option value=" + item.id + " selected='selected'>" + item.des + "</option>");
                    }
                    else {
                        $("#Tipo").append("<option value=" + item.id + ">" + item.des + "</option>");
                    }
                });
            },
            error: function () {
                $("#dv_mensaje").hide();
                $("#dv_Error").html('Error de comunicación con el servidor al intentar cargar la funcion Tipos.');
                $("#dv_Error").show();
                setTimeout(function () { $('#dv_Error').hide(); }, 10000);
            }
        });
    }
    function TipoComision() {
        var tipocomision = $("#Tipo").val();

        if (tipocomision == 1) {
            CargarAerolineas()
            $("#div_hotel").addClass('hide');
            $("#div_vehiculo").addClass('hide');
            $("#div_aerolinea").removeClass('hide');
        }
        else if (tipocomision == 2) {
            CargarHoteles()
            $("#div_hotel").removeClass('hide');
            $("#div_aerolinea").addClass('hide');
            $("#div_vehiculo").addClass('hide');
        }
        else if (tipocomision == 3) {
            CargarVehiculos()
            $("#div_vehiculo").removeClass('hide');
            $("#div_aerolinea").addClass('hide');
            $("#div_hotel").addClass('hide');
        }
        else {
            $("#div_aerolinea").addClass('hide');
            $("#div_hotel").addClass('hide');
            $("#div_vehiculo").addClass('hide');
        }
    }

    function CargarAerolineas() {
        $("#Aerolineas").empty();;
        $.ajax({
            type: "POST",
            url: "frm_agencias.aspx?fn=cargar_aerolineas",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (response) {
                $.each(response.aaData, function (i, item) {
                    if (item.id == 0) {
                        $("#Aerolineas").append("<option value=" + item.id + " selected='selected'>" + item.des + "</option>");
                    }
                    else {
                        $("#Aerolineas").append("<option value=" + item.id + ">" + item.des + "</option>");
                    }
                });
            },
            error: function () {
                $("#dv_mensaje").hide();
                $("#dv_Error").html('Error de comunicación con el servidor al intentar cargar la funcion Aerolineas.');
                $("#dv_Error").show();
                setTimeout(function () { $('#dv_Error').hide(); }, 10000);
            }
        });
    }
    function CargarHoteles() {
        $("#Hoteles").empty();;
        $.ajax({
            type: "POST",
            url: "frm_agencias.aspx?fn=cargar_hoteles",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (response) {
                $.each(response.aaData, function (i, item) {
                    if (item.id == 0) {
                        $("#Hoteles").append("<option value=" + item.id + " selected='selected'>" + item.des + "</option>");
                    }
                    else {
                        $("#Hoteles").append("<option value=" + item.id + ">" + item.des + "</option>");
                    }
                });
            },
            error: function () {
                $("#dv_mensaje").hide();
                $("#dv_Error").html('Error de comunicación con el servidor al intentar cargar la funcion Hoteles.');
                $("#dv_Error").show();
                setTimeout(function () { $('#dv_Error').hide(); }, 10000);
            }
        });
    }
    function CargarVehiculos() {
        $("#Vehiculos").empty();;
        $.ajax({
            type: "POST",
            url: "frm_agencias.aspx?fn=cargar_vehiculos",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (response) {
                $.each(response.aaData, function (i, item) {
                    if (item.id == 0) {
                        $("#Vehiculos").append("<option value=" + item.id + " selected='selected'>" + item.des + "</option>");
                    }
                    else {
                        $("#Vehiculos").append("<option value=" + item.id + ">" + item.des + "</option>");
                    }
                });
            },
            error: function () {
                $("#dv_mensaje").hide();
                $("#dv_Error").html('Error de comunicación con el servidor al intentar cargar la funcion Vehiculos.');
                $("#dv_Error").show();
                setTimeout(function () { $('#dv_Error').hide(); }, 10000);
            }
        });
    }