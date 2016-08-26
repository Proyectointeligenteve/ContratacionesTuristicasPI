    /* 
 * PARENT CODE
 */
function backToList() {
    var var_Id = $('#hdn_id_hotel').val();
    window.location.href = 'lst_hoteles.aspx';
};
function load() {
    Permisos()
    $("#identificador").focus();

    $("#dv_Message").hide();
    $("#dv_Error").hide();
    $("#dv_error_modal").hide();
    $("#dv_error_modal2").hide();
    
    $("#Comision").blur(function () {
        var number = $(this).val();
        number = number.replace('.', '');
        number = number.replace(',', '.');
        number = $.formatNumber(number, { format: "#,##0.00", locale: "es" });
        $("#Comision").val(number);
    });
    var recordId = $.url().param('id');
    var idt1 = $.url().param('idhotel');
    

    $('.loading').show()
    $('.btn').hide();
    $.ajax({
        type: "POST",
        url: "frm_hoteles.aspx?fn=loadAll",
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
                $('#Identificador').val(response.Identificador);
                $('#Nombre').val(response.Nombre);
                $('#RazonSocial').val(response.RazonSocial);
                $('#Direccion').val(response.Direccion);
                $('#TelefonoFijo').val(response.TelefonoFijo);
                $('#TelefonoMovil').val(response.TelefonoMovil);
                $('#Codigo').val(response.Codigo);
                $('#Email').val(response.Email);
                $('#Comision').val(response.Comision);
                
                //professionLoad()
                contactoLoad()
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
        url: "frm_hoteles.aspx?fn=permisos&v=" + v,
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
                    $("#Identificador").attr("disabled", "disabled");
                    $("#Nombre").attr("disabled", "disabled");
                    $("#RazonSocial").attr("disabled", "disabled");
                    $("#Direccion").attr("disabled", "disabled");
                    $("#TelefonoFijo").attr("disabled", "disabled");
                    $("#TelefonoMovil").attr("disabled", "disabled");
                    $("#Email").attr("disabled", "disabled");
                    $("#Codigo").attr("disabled", "disabled");
                    $("#Comision").attr("disabled", "disabled");

                    $('#btn_contactoAdd').attr("disabled", "disabled");
                    $('#btn_contactoEdit').attr("disabled", "disabled");
                    $('#btn_contactoDelete').attr("disabled", "disabled");
                    
                    $("#Identificador").focus();
                    //alert("class hide anhadida, atributo boton contacto add desabilitado");
                }
            }
            else {
                $("#dv_error").html(response.msj);
                $("#dv_error").show();
                setTimeout(function () { $('#dv_error').hide(); }, 10000);
            }
        },
        error: function () {
            $('.loading').hide()
            $('.btn').show();
            $("#dv_error").html('Error de comunicación con el servidor. Función Permisos().');
            $("#dv_error").show();
            setTimeout(function () { $('#dv_error').hide(); }, 10000);
        }
    });
}
    //var msjModal
function save() {
        var record = {};
        record.hdn_id_hotel = $('#hdn_id_hotel').val();
        record.Identificador = $('#Identificador').val();
        record.Nombre = $('#Nombre').val();
        record.RazonSocial = $('#RazonSocial').val();
        record.Direccion = $('#Direccion').val();
        record.TelefonoFijo = $('#TelefonoFijo').val();
        record.TelefonoMovil = $('#TelefonoMovil').val();
        record.Email = $('#Email').val();
        record.Codigo = $('#Codigo').val();
        record.Comision = $('#Comision').val();
    
        $('.loading').show()
        $('.btn').hide();
        $.ajax({
            type: "POST",
            url: "frm_hoteles.aspx?fn=saveAll",
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
                    $('#hdn_id_hotel').val(response.hdn_id_hotel);
                    msjModal = $.remodal.lookup[$('[data-remodal-id=msjModal]').data('remodal')];
                    msjModal.open();
                    //$('#msjModal').modal(options);
                    //$('#msjModal').on('shown.bs.modal', function () {
                    if (response.msj != '') {
                        $("#msj").html("El registro ha sido guardado con errores: " + response.msj);
                    }
                    else {
                        $('#hdn_id_hotel').val(response.id)
                        $("#msj").html("El registro ha sido guardado con exito.");
                       
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
                $("#dv_Error").html('Error de comunicación con el servidor. El registro no ha sido actualizado.');
                $("#dv_Error").show();
                setTimeout(function () { $('#dv_Error').hide(); }, 10000);
            }
        });
    }
//}
    function validate() {
        var result = $("#form2").valid();
        return result;
        //alert(result);
    };
    
    /* 
     * contacto CODE
     */
    
    function contactoAdd() {
        $('#hdn_contactoId').val(0);
        $('#NombreC').val('');
        $('#CargoC').val('');
        $('#TelefonoC').val('');
        
        var options = {
            "backdrop": "static",
            "keyboard": "true"
        }
        basicModal2 = $.remodal.lookup[$('[data-remodal-id=basicModal2]').data('remodal')];
        basicModal2.open();
        //$('#basicModal2').modal(options);
        //$('#basicModal2').on('shown.bs.modal', function () {

        //})
    };

    var deletemodal;
    function contactoConfirm() {
        var options = {
            "backdrop": "static",
            "keyboard": "true"
        }
        deletemodal = $.remodal.lookup[$('[data-remodal-id=deleteModal2]').data('remodal')];
        deletemodal.open();
    };
    function contactoDelete() {
        var id = '';
        $('#tbl_contacto tr').each(function () {
            if ($(this).hasClass('row_selected')) {
                id += ',' + this.id;
            }
        });
        if (id == '') {
            $("#dv_error").html('Seleccione un registro');
            $("#dv_error").show();
            setTimeout(function () { $('#dv_error').hide(); }, 10000);
            return false;
        }

        $('.loading').show()
        $('.btn').hide();
        $.ajax({
            type: "POST",
            url: "frm_hoteles.aspx?fn=contactoDelete",
            data: '{"hdn_contactoId":"' + id + '"}',
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (response) {
                $('.loading').hide()
                $('.btn').show();
                deletemodal.close()
                //$('#deleteModal2').modal('hide');
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
                //$('#deleteModal2').modal('hide');
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
            $("#dv_error").html('Seleccione un registro');
            $("#dv_error").show();
            setTimeout(function () { $('#dv_error').hide(); }, 10000);
            return false;
        }
        $('.loading').show()
        $('.btn').hide();
        $.ajax({
            type: "POST",
            url: "frm_hoteles.aspx?fn=contactoEdit",
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
                    basicModal2 = $.remodal.lookup[$('[data-remodal-id=basicModal2]').data('remodal')];
                    basicModal2.open();
                    //$('#basicModal2').modal(options);
                    //$('#basicModal2').on('shown.bs.modal', function () {
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
                basicModal2.close();
                //$('#basicModal2').modal('hide');
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
            "sAjaxSource": "frm_hoteles.aspx?fn=contactoLoad",
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
                "sSearch": "Buscar:",
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
        record.IdHotel = $('#hdn_contactoId').val();
        record.NombreC = $('#NombreC').val();
        record.CargoC = $('#CargoC').val();
        record.TelefonoC = $('#TelefonoC').val();

        $('.loading').show()
        $('.btn').hide();
        $.ajax({
            type: "POST",
            url: "frm_hoteles.aspx?fn=contactosave",
            data: JSON.stringify(record),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (response) {
                $('.loading').hide()
                $('.btn').show();
                basicModal2.close();
                //$('#basicModal2').modal('hide');
                if (response.rslt == 'exito') {
                    $("#dv_Error2").hide()
                    $("#dv_Message2").html('El registro ha sido procesado con exito.');
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
                //$('#basicModal2').modal('hide');
                basicModal2.close();
                $("#dv_Error2").html('Error de comunicación con el servidor. El registro no ha sido actualizado.');
                $("#dv_Error2").show();
                setTimeout(function () { $('#dv_Error2').hide(); }, 10000);
            }
        });
    };
    function contactoValidate() {
        var result = $("#form2").valid();
        return result;
    };
     
    function buscarHotel() {
        var rif = $("#Identificador").val();

        $('.loading').show()
        $('.btn').hide();
        $.ajax({
            type: "POST",
            url: "frm_hoteles.aspx?fn=validar_hotel",
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
                    $("#Identificador").val('');
                    $("#Identificador").focus();
                    $("#dv_Error").html("El Hotel '" + response.Nombre + "' ya esta registrado.");
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
                    $("#Identificador").focus();
                }
            },
            error: function () {
                $('.loading').hide()
                $('.btn').show();
                $("#dv_Error").html('Error de comunicación con el servidor. Funcion CargarHotel().');
                $("#dv_Error").show();
                setTimeout(function () { $('#dv_Error').hide(); }, 10000);
                $("#Identificador").focus();
            }
        });

    }