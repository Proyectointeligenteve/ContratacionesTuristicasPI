    /* 
 * PARENT CODE
 */
function backToList() {
    var var_Id = $('#hdn_id_envio').val();
    window.location.href = 'lst_envios.aspx';
};
function load() {
    Permisos()
    Cargarpaquetes()
    $("#dv_Message").hide();
    $("#dv_Error").hide();
    $("#dv_error_modal").hide();
    $("#dv_error_modal2").hide();
    
    var recordId = $.url().param('id');
    var idt1 = $.url().param('idenvio');

    $('.loading').show()
    $('.btn').hide();
    $.ajax({
        type: "POST",
        url: "frm_envios.aspx?fn=loadAll",
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
                $('#Codigo').val(response.Codigo);
                $('#Id_cliente_emisor').val(response.Id_cliente_emisor);
                $('#Id_cliente_receptor').val(response.Id_cliente_emisor);
                $('#Id_pais_origen').val(response.Id_pais_origen);
                $('#Id_estado_origen').val(response.Id_estado_origen);
                $('#Id_ciudad_origen').val(response.Id_ciudad_origen);
                $('#Id_pais_destino').val(response.Id_pais_destino);
                $('#Id_estado_destino').val(response.Id_estado_destino);
                $('#Id_ciudad_destino').val(response.Id_ciudad_destino);
                $('#Direccion_envio').val(response.Direccion_envio);
                $('#Costo_envio').val(response.Costo_envio);
                $('#Estatus').val(response.Estatus);
                
                //professionLoad()
                paqueteLoad();
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
        url: "frm_envios.aspx?fn=permisos&v=" + v,
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

                    $('#btn_paqueteAdd').attr("disabled", "disabled");
                    $('#btn_paqueteEdit').attr("disabled", "disabled");
                    $('#btn_paqueteDelete').attr("disabled", "disabled");
                    
                    $("#Nombre").focus();
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
        record.hdn_id_envio = $('#hdn_id_envio').val();
        record.Nombre = $('#Nombre').val();
    
        $('.loading').show()
        $('.btn').hide();
        $.ajax({
            type: "POST",
            url: "frm_envios.aspx?fn=saveAll",
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

                    $('#hdn_id_envio').val(response.hdn_id_envio);
                    msjModal = $.remodal.lookup[$('[data-remodal-id=msjModal]').data('remodal')];
                    msjModal.open();
                    //$('#msjModal').modal(options);
                    //$('#msjModal').on('shown.bs.modal', function () {
                    if (response.msj != '') {
                        $("#msj").html("El record ha sido guardado con errores: " + response.msj);
                    }
                    else {
                        $('#hdn_id_envio').val(response.id)
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
        var result = $("#form2").valid();
        return result;
    };
    
    /* 
     * paquete CODE
     */
    function Cargarpaquetes() {

        $.ajax({
            type: "POST",
            url: "frm_envios.aspx?fn=Cargarpaquetes",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (response) {
                $.each(response.aaData, function (i, item) {
                    if (item.id == 0) {
                        $("#paquete").append("<option value=" + item.id + " selected='selected'>" + item.des + "</option>");

                    }
                    else {
                        $("#paquete").append("<option value=" + item.id + ">" + item.des + "</option>");
                    }
                });
                $("#paquete").val($("#hdn_paqueteId").val());
                //CargarpaquetesDetalles();
            },
            error: function () {
                $("#dv_mensaje").hide();
                $("#dv_error").html('Error de comunicación con el servidor al intentar cargar las formas de pago.');
                $("#dv_error").show();
                setTimeout(function () { $('#dv_error').hide(); }, 10000);
            }
        });
    }
    function paqueteAdd() {
        $('#hdn_paqueteId').val(0);
        $('#Contacto').val(0);
        
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
    function paqueteConfirm() {
        var options = {
            "backdrop": "static",
            "keyboard": "true"
        }
        deletemodal = $.remodal.lookup[$('[data-remodal-id=deleteModal2]').data('remodal')];
        deletemodal.open();
    };
    function paqueteDelete() {
        var id = '';
        $('#tbl_paquete tr').each(function () {
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
            url: "frm_envios.aspx?fn=paqueteDelete",
            data: '{"hdn_paqueteId":"' + id + '"}',
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
                    $('#tbl_paquete').dataTable().fnDestroy();
                    paqueteLoad();
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
    function paqueteEdit() {
        var id
        $('#tbl_paquete tr').each(function () {
            if ($(this).hasClass('row_selected')) {
                id = this.id;
                $("#hdn_paqueteId").val(id);
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
            url: "frm_envios.aspx?fn=paqueteEdit",
            data: '{"hdn_paqueteId":"' + id + '"}',
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (response) {
                $('.loading').hide()
                $('.btn').show();

                if (response.rslt == 'exito') {
                    $('#Contacto').val(response.Contacto);
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
    function paqueteLoad() {
        $('#tbl_paquete').dataTable().fnDestroy();
        $("#tbl_paquete tbody").click(function (event) {
            $(pTable.fnSettings().aoData).each(function () {
                $(this.nTr).removeClass('row_selected');
            });
            $(event.target.parentNode).addClass('row_selected');
        });
        pTable = $('#tbl_paquete').dataTable({
            "bProcessing": true,
            "bServerSide": false,
            "sAjaxSource": "frm_envios.aspx?fn=paqueteLoad",
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
                { "mDataProp": "Codigo" },
                { "mDataProp": "Nombre" },
                { "mDataProp": "Tipo" }
            ]
        });
    };
    function paqueteSave() {
        var record = {};
        record.hdn_paqueteId = $('#hdn_paqueteId').val();
        record.paquete = $('#paquete').val();

        $('.loading').show()
        $('.btn').hide();
        $.ajax({
            type: "POST",
            url: "frm_envios.aspx?fn=paqueteSave",
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
                    $("#dv_Message2").html('El record ha sido procesado con exito.');
                    $("#dv_Message2").show();
                    setTimeout(function () { $('#dv_Message2').hide(); }, 10000);
                    paqueteLoad()
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
                $("#dv_Error2").html('Error de comunicación con el servidor. El record no ha sido actualizado.');
                $("#dv_Error2").show();
                setTimeout(function () { $('#dv_Error2').hide(); }, 10000);
            }
        });
    };
    function paqueteValidate() {
        var result = $("#form2").valid();
        return result;
    };
     
    var modalcliente;
    function BuscarClientesE() {
        $("#NombreE").val('');
        //prueba

        $('#btn_cargar').hide();
        $('#dvloader').show();
        var var_identificador = $('#IdClienteEmisor').val();
        //anotacion: se borra la lista del modal 
        $('#tbClientesE').dataTable().fnDestroy();
        var giRedraw = false;
        var responsiveHelper;
        var breakpointDefinition = {
            tablet: 1024,
            phone: 480
        };

        $('.loading').show()
        $('.btn').hide();
        tableElement = $('#tbClientesE')
        tableElement.dataTable({
            "bProcessing": true,
            "bServerSide": false,
            "sAjaxSource": "frm_envios.aspx?fn=buscarclientesE&identificador=" + var_identificador,
            "bAutoWidth": false,
            "bFilter": false,
            "bSort": false,
            "bLengthChange": false,
            "bPaginate": false,
            "bInfo": false,
            "oLanguage": {
                "sInfo": "_TOTAL_ Registro(s)",
                "sInfoFiltered": " - de _MAX_ registros",
                "sInfoThousands": ",",
                "sLengthMenu": "Mostrar _MENU_ Registros",
                "sLoadingRecords": "<img src='img/loading2.gif' />",
                "sProcessing": "",
                "sSearch": "",
                "sZeroRecords": "No se encontraron registros"
            },
            "sDom": 'frt<"izq"i><"der"p>',
            "fnServerData": function (sSource, aoData, fnCallback) {
                $.getJSON(sSource, aoData, function (json) {
                    $('.loading').hide()
                    $('.btn').show();
                    $('#btn_cargar').show()
                    $('#dvloader').hide()
                    try {
                        if (json.error == '-1') {
                            alert(1);
                            $("#dv_advertencia").html("No se encontraron clientes con el identificador indicado");
                            $("#dv_advertencia").show();
                            $("#IdClienteEmisor").focus();
                            setTimeout(function () { $('#dv_advertencia').hide(); }, 10000);
                            return false;
                        }

                        if (json.error == '-2') {
                            $("#dv_error").html("Se ha producido un error en el sistema. Intente de nuevo. Si el problema persiste comuniquese con el administrador del sistema");
                            $("#dv_error").show();
                            setTimeout(function () { $('#dv_error').hide(); }, 10000);
                            $("#IdClienteEmisor").focus();
                            return false;
                        }

                        if (json.idcliente) {
                            var id = ""

                            try {
                                id = json.idcliente;
                            } catch (e) {
                                $("#dv_error").html("Error. " + e);
                                $("#dv_error").show();
                                setTimeout(function () { $('#dv_error').hide(); }, 10000);
                                return false;
                            }

                            $("#Cliente").val(id)
                            CargarCliente()
                            return false;
                        }
                    } catch (e) { }

                    modalcliente = $.remodal.lookup[$('[data-remodal-id=modalcliente]').data('remodal')];
                    modalcliente.open();
                    fnCallback(json);
                });
            },
            fnPreDrawCallback: function () {
                if (!responsiveHelper) {
                    responsiveHelper = new ResponsiveDatatablesHelper(tableElement, breakpointDefinition);
                }
            },
            fnRowCallback: function (nRow, aData, iDisplayIndex, iDisplayIndexFull) {
                responsiveHelper.createExpandIcon(nRow);
            },
            fnDrawCallback: function (oSettings) {
                responsiveHelper.respond();
            },
            "aoColumns": [
                    { "mDataProp": "Nombre" },
                    { "mDataProp": "Rif" },
                    { "mDataProp": "Telefono" },
                    { "mDataProp": "Direccion" }
            ]
        });

        //$(".first.paginate_button, .last.paginate_button").hide();
        var search_input = tableElement.closest('.dataTables_wrapper').find('div[id$=_filter] input');
        search_input.attr('placeholder', "Buscar");
    }

    function CargarClienteE() {
        var id = $("#Cliente").val();

        $('.loading').show()
        $('.btn').hide();
        $.ajax({
            type: "POST",
            url: "frm_envios.aspx?fn=cargarcliente",
            data: '{"id":"' + id + '"}',
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
                    $('#IdClienteEmisor').val(response.IdClienteEmisor);
                    $('#NombreE').val(response.NombreE);
                    $("#IdClienteReceptor").focus();
                }
                else {
                    $("#dv_error").html(response.msj);
                    $("#dv_error").show();
                    setTimeout(function () { $('#dv_error').hide(); }, 10000);
                    $("#rif").focus();
                }
            },
            error: function () {
                $('.loading').hide()
                $('.btn').show();
                $("#dv_error").html('Error de comunicación con el servidor. Función Cargarcliente().');
                $("#dv_error").show();
                setTimeout(function () { $('#dv_error').hide(); }, 10000);
                $("#IdClienteEmisor").focus();
            }
        });

    }

    function EventosListadoCliente() {
        $("#tbClientesE tbody").click(function (event) {

            $(tableElement.fnSettings().aoData).each(function () {
                $(this.nTr).removeClass('row_selected');
            });
            $(event.target.parentNode).addClass('row_selected');

        });

        $("#tbClientesE tbody").dblclick(function (event) {

            $(tableElement.fnSettings().aoData).each(function () {
                $(this.nTr).removeClass('row_selected');
            });
            $(event.target.parentNode).addClass('row_selected');
            SeleccionarCliente()
        });
    }

    function BuscarClientesR() {
        $("#NombreR").val('');

        $('#btn_cargar').hide();
        $('#dvloader').show();
        var var_rif = $('#IdClienteReceptor').val();

        $('#tbClientes').dataTable().fnDestroy();
        var giRedraw = false;
        var responsiveHelper;
        var breakpointDefinition = {
            tablet: 1024,
            phone: 480
        };

        $('.loading').show()
        $('.btn').hide();
        tableElement = $('#tbClientes')
        tableElement.dataTable({
            "bProcessing": true,
            "bServerSide": false,
            "sAjaxSource": "frm_envios.aspx?fn=buscarclientes&rif=" + var_rif,
            "bAutoWidth": false,
            "bFilter": false,
            "bSort": false,
            "bLengthChange": false,
            "bPaginate": false,
            "bInfo": false,
            "oLanguage": {
                "sInfo": "_TOTAL_ Registro(s)",
                "sInfoFiltered": " - de _MAX_ registros",
                "sInfoThousands": ",",
                "sLengthMenu": "Mostrar _MENU_ Registros",
                "sLoadingRecords": "<img src='img/loading2.gif' />",
                "sProcessing": "",
                "sSearch": "",
                "sZeroRecords": "No se encontraron registros"
            },
            "sDom": 'frt<"izq"i><"der"p>',
            "fnServerData": function (sSource, aoData, fnCallback) {
                $.getJSON(sSource, aoData, function (json) {
                    $('.loading').hide()
                    $('.btn').show();
                    $('#btn_cargar').show()
                    $('#dvloader').hide()
                    try {
                        if (json.error == '-1') {
                            $("#dv_advertencia").html("No se encontraron clientes con el identificador indicado");
                            $("#dv_advertencia").show();
                            $("#IdClienteEmisor").focus();
                            setTimeout(function () { $('#dv_advertencia').hide(); }, 10000);
                            return false;
                        }

                        if (json.error == '-2') {
                            $("#dv_error").html("Se ha producido un error en el sistema. Intente de nuevo. Si el problema persiste comuniquese con el administrador del sistema");
                            $("#dv_error").show();
                            setTimeout(function () { $('#dv_error').hide(); }, 10000);
                            $("#IdClienteEmisor").focus();
                            return false;
                        }

                        if (json.idcliente) {
                            var id = ""

                            try {
                                id = json.idcliente;
                            } catch (e) {
                                $("#dv_error").html("Error. " + e);
                                $("#dv_error").show();
                                setTimeout(function () { $('#dv_error').hide(); }, 10000);
                                return false;
                            }

                            $("#Cliente").val(id)
                            CargarCliente()
                            return false;
                        }
                    } catch (e) { }

                    modalcliente = $.remodal.lookup[$('[data-remodal-id=modalcliente]').data('remodal')];
                    modalcliente.open();
                    fnCallback(json);
                });
            },
            fnPreDrawCallback: function () {
                if (!responsiveHelper) {
                    responsiveHelper = new ResponsiveDatatablesHelper(tableElement, breakpointDefinition);
                }
            },
            fnRowCallback: function (nRow, aData, iDisplayIndex, iDisplayIndexFull) {
                responsiveHelper.createExpandIcon(nRow);
            },
            fnDrawCallback: function (oSettings) {
                responsiveHelper.respond();
            },
            "aoColumns": [
                    { "mDataProp": "Nombre" },
                    { "mDataProp": "Rif" },
                    { "mDataProp": "Telefono" },
                    { "mDataProp": "Direccion" }
            ]
        });

        //$(".first.paginate_button, .last.paginate_button").hide();
        var search_input = tableElement.closest('.dataTables_wrapper').find('div[id$=_filter] input');
        search_input.attr('placeholder', "Buscar");
    }

    function CargarClienteE() {
        var id = $("#Cliente").val();

        $('.loading').show()
        $('.btn').hide();
        $.ajax({
            type: "POST",
            url: "frm_envios.aspx?fn=cargarcliente",
            data: '{"id":"' + id + '"}',
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
                    $('#IdClienteEmisor').val(response.IdClienteEmisor);
                    $('#NombreE').val(response.NombreE);
                    $("#IdClienteReceptor").focus();
                }
                else {
                    $("#dv_error").html(response.msj);
                    $("#dv_error").show();
                    setTimeout(function () { $('#dv_error').hide(); }, 10000);
                    $("#rif").focus();
                }
            },
            error: function () {
                $('.loading').hide()
                $('.btn').show();
                $("#dv_error").html('Error de comunicación con el servidor. Función Cargarcliente().');
                $("#dv_error").show();
                setTimeout(function () { $('#dv_error').hide(); }, 10000);
                $("#IdClienteEmisor").focus();
            }
        });

    }