    /* 
 * PARENT CODE
 */
function backToList() {
    var var_Id = $('#hdn_id_envio').val();
    window.location.href = 'lst_envios.aspx';
};
function load() {
    Permisos()
    CargarPaisesE()
    CargarPaisesR()
    EventosListadoCliente()
    EventosListadoClienteR()
    //EventosListadoPaquetes()    

    $("#dv_error_modalE").hide();
    $("#dv_error_modalR").hide();

    $("#CostoP").blur(function () {
        var number = $(this).val();
        number = number.replace('.', '');
        number = number.replace(',', '.');
        number = $.formatNumber(number, { format: "#,##0.00", locale: "es" });
        $("#CostoP").val(number);
    });

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
                if (response.Codigo != '') {
                    $('#Codigo').val(response.Codigo);
                    $("#ClienteE").val(response.ClienteE);
                    $('#HfPaisE').val(response.HfPaisE);
                    $('#HfEstadoE').val(response.HfEstadoE);
                    $('#HfCiudadE').val(response.HfCiudadE);
                    $("#ClienteR").val(response.ClienteR);
                    $('#HfPaisR').val(response.HfPaisR);
                    $('#HfEstadoR').val(response.HfEstadoR);
                    $('#HfCiudadR').val(response.HfCiudadR);
                    $('#DireccionEnvio').val(response.DireccionEnvio);

                    $('#NumeroP').val(response.NumeroP);
                    $('#PesoP').val(response.PesoP);
                    $('#VolumenP').val(response.VolumenP);
                    $('#CostoP').val(response.CostoP);
                    $('#DescripcionP').val(response.DescripcionP);

                    $('#TotalR').val(response.TotalR);
                    if (response.ClienteE > 0) {
                        CargarClienteE()
                        CargarClienteR()
                        CargarPaisesE()
                        CargarPaisesR()

                    }
                }
                else {
                    $('#IdentificadorEmisor').focus();
                    paqueteLoad();
                }
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

                    $('#btn_paqueteAdd').attr("disabled", "disabled");
                    $('#btn_paqueteEdit').attr("disabled", "disabled");
                    $('#btn_paqueteDelete').attr("disabled", "disabled");
                    
                    //$("#Nombre").focus();
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
    
function save() {
        var record = {};
        record.hdn_id_envio = $('#hdn_id_envio').val();
        record.Codigo = $('#Codigo').val();
        record.ClienteE = $('#ClienteE').val();
        record.PaisE = $('#PaisE').val();
        record.EstadoE = $('#EstadoE').val();
        record.CiudadE = $('#CiudadE').val();
        record.ClienteR = $('#ClienteR').val();
        record.PaisR = $('#PaisR').val();
        record.EstadoR = $('#EstadoR').val();
        record.CiudadR = $('#CiudadR').val();
        record.DireccionEnvio = $('#DireccionEnvio').val();
        record.TotalR = $('#TotalR').val();
    
        record.NumeroP = $('#NumeroP').val();
        record.PesoP = $('#PesoP').val();
        record.VolumenP = $('#VolumenP').val();
        record.CostoP = $('#CostoP').val();
        record.DescripcionP = $('#DescripcionP').val();

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

                    $('#hdn_id_envio').val(response.id);
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
    
//PAQUETES
    //function paqueteAdd() {
    //    $('#hdn_paqueteId').val(0);
    //    $('#NumeroP').val('');
    //    $('#PesoP').val('');
    //    $('#VolumenP').val('');
    //    $('#DescripcionP').val('');
    //    $('#CostoP').val(0);
    //    var options = {
    //        "backdrop": "static",
    //        "keyboard": "true"
    //    }
    //    basicModal2 = $.remodal.lookup[$('[data-remodal-id=basicModal2]').data('remodal')];
    //    basicModal2.open();
    //    $('#PesoP').focus();
    //};

    //var deletemodal;
    //function paqueteConfirm() {
    //    var options = {
    //        "backdrop": "static",
    //        "keyboard": "true"
    //    }
    //    deletemodal = $.remodal.lookup[$('[data-remodal-id=deleteModal2]').data('remodal')];
    //    deletemodal.open();
//};

    //function paqueteDelete() {
    //    var id = '';
    //    $('#tbl_paquetes tr').each(function () {
    //        if ($(this).hasClass('row_selected')) {
    //            id += ',' + this.id;
    //        }
    //    });
    //    if (id == '') {
    //        $("#dv_error").html('Seleccione un registro');
    //        $("#dv_error").show();
    //        setTimeout(function () { $('#dv_error').hide(); }, 10000);
    //        return false;
    //    }

    //    $('.loading').show()
    //    $('.btn').hide();
    //    $.ajax({
    //        type: "POST",
    //        url: "frm_envios.aspx?fn=paqueteDelete",
    //        data: '{"hdn_paqueteId":"' + id + '"}',
    //        contentType: "application/json; charset=utf-8",
    //        dataType: "json",
    //        success: function (response) {
    //            $('.loading').hide()
    //            $('.btn').show();
    //            deletemodal.close()
    //            //$('#deleteModal2').modal('hide');
    //            if (response.rslt == 'exito') {
    //                $("#dv_Error2").hide()
    //                $("#dv_Message2").html('El record ha sido eliminado.');
    //                $("#dv_Message2").show();
    //                setTimeout(function () { $('#dv_Message2').hide(); }, 10000);
    //                $('#tbl_paquetes').dataTable().fnDestroy();
    //                paqueteLoad();
    //            }
    //            else {
    //                $("#dv_Error2").html(response.msj);
    //                $("#dv_Error2").show();
    //                setTimeout(function () { $('#dv_Error2').hide(); }, 10000);
    //            }
    //        },
    //        error: function () {
    //            $('.loading').hide()
    //            $('.btn').show();
    //            deletemodal.close()
    //            $("#dv_Error2").html('Error de comunicación con el servidor. El record no ha sido actualizado.');
    //            $("#dv_Error2").show();
    //            setTimeout(function () { $('#dv_Error2').hide(); }, 10000);
    //        }
    //    });
//};

    //function paqueteEdit() {
    //    var id = '';
    //    $('#tbl_paquetes tr').each(function () {
    //        if ($(this).hasClass('row_selected')) {
    //            id = this.id;
    //            $("#hdn_paqueteId").val(id);
    //        }
    //    });

    //    if (id == '') {
    //        $("#dv_error").html('Seleccione un registro');
    //        $("#dv_error").show();
    //        setTimeout(function () { $('#dv_error').hide(); }, 10000);
    //        return false;
    //    }
    //    $('.loading').show()
    //    $('.btn').hide();
    //    $.ajax({
    //        type: "POST",
    //        url: "frm_envios.aspx?fn=paqueteEdit",
    //        data: '{"hdn_paqueteId":"' + id + '"}',
    //        contentType: "application/json; charset=utf-8",
    //        dataType: "json",
    //        success: function (response) {
    //            $('.loading').hide()
    //            $('.btn').show();

    //            if (response.rslt == 'exito') {
    //                $('#hdn_paqueteId').val(response.Id_envio);
    //                $('#NumeroP').val(response.NumeroP);
    //                $('#PesoP').val(response.PesoP);
    //                $('#VolumenP').val(response.VolumenP);
    //                $('#CostoP').val(response.CostoP);
    //                $('#DescripcionP').val(response.DescripcionP);
    //                var options = {
    //                    "backdrop": "static",
    //                    "keyboard": "true"
    //                }
    //                basicModal2 = $.remodal.lookup[$('[data-remodal-id=basicModal2]').data('remodal')];
    //                basicModal2.open();
            //    }
            //    else {
            //        $("#dv_Message2").hide()
            //        $("#dv_Error2").html(response.msj);
            //        $("#dv_Error2").show();
            //        setTimeout(function () { $('#dv_Error').hide(); }, 10000);
            //    }
            //},
            //error: function () {
            //    $('.loading').hide()
            //    $('.btn').show();
            //    basicModal2.close();
    //            $("#dv_Error2").html('Error de comunicación con el servidor. El record no ha sido actualizado.');
    //            $("#dv_Error2").show();
    //            setTimeout(function () { $('#dv_Error').hide(); }, 10000);
    //        }
    //    });
    //};

    //var pTable;
    //var subtotales = new Array();
    //function paqueteLoad() {
    //    subtotales.length = 0;
    //    subtotales.push(0);
    //    var giRedraw = false;
    //    var responsiveHelper;
    //    var breakpointDefinition = {
    //        tablet: 1024,
    //        phone: 480
    //    };
    //    $('#tbl_paquetes').dataTable().fnDestroy();
    //    $("#tbl_paquetes tbody").click(function (event) {
    //        $(pTable.fnSettings().aoData).each(function () {
    //            $(this.nTr).removeClass('row_selected');
    //        });
    //        $(event.target.parentNode).addClass('row_selected');
    //    });
    //    $('#TotalR').val(0);

    //    tableElement2 = $('#tbl_paquetes')
    //    tableElement2.dataTable({
    //        "bProcessing": true,
    //        "bServerSide": false,
    //        "sAjaxSource": "frm_envios.aspx?fn=paqueteLoad",
    //        "bFilter": false,
    //        "bLengthChange": false,
    //        "bPaginate": false,
    //        "bInfo": false,
    //        "oLanguage": {
    //            "sInfo": "_TOTAL_ Registro(s) encontrado(s)",
    //            "sInfoFiltered": " - de _MAX_ registros",
    //            "sInfoThousands": ",",
    //            "sLengthMenu": "Mostrar _MENU_ Registros",
    //            "sLoadingRecords": "Por favor espere  - CARGANDO...",
    //            "sProcessing": "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;PROCESANDO...",
    //            "sSearch": "",
    //            "sZeroRecords": "No se encontraron registros",
    //            "oPaginate": {
    //                "sNext": " SIGUIENTE",
    //                "sPrevious": "ANTERIOR "
    //            }
    //        },
    //        "fnCreatedRow": function (row, data, index) {

    //            var subtotal = data.Costo;

    //            subtotales[0] += subtotal;

    //            var sub = $.formatNumber(subtotales[0], { format: "#,###.00", locale: "es" });
    //            $('#TotalR').val(sub);

    //        },
    //        fnPreDrawCallback: function () {
    //            if (!responsiveHelper) {
    //                responsiveHelper = new ResponsiveDatatablesHelper(tableElement2, breakpointDefinition);
    //            }
    //        },
    //        fnRowCallback: function (nRow, aData, iDisplayIndex, iDisplayIndexFull) {
    //            responsiveHelper.createExpandIcon(nRow);
    //        },
    //        fnDrawCallback: function (oSettings) {
    //            responsiveHelper.respond();
    //        },
    //        "aoColumns": [
    //            {
    //              "mDataProp": "Id_envio",
    //              "bVisible": false },
    //            { "mDataProp": "Numero" },
    //            { "mDataProp": "Peso" },
    //            { "mDataProp": "Volumen" },
    //            { "mDataProp": "Descripcion" },
    //            {
    //                "mDataProp": "Costo",
    //                "sType": 'numeric',
    //                "mRender": function (data) {
    //                    return $.formatNumber(data, { format: "#,###.00", locale: "es" });
    //                }
    //            }
    //        }
    //        ]
    //    });
    //    var search_input = tableElement2.closest('.dataTables_wrapper').find('div[id$=_filter] input');
    //    search_input.attr('placeholder', "Buscar");
    //};

    
    //function paqueteSave() {
    //    var record = {};
    //    record.IdEnvio = $('#hdn_paqueteId').val();
    //    record.PesoP = $('#PesoP').val();
    //    record.VolumenP = $('#VolumenP').val();
    //    record.CostoP = $('#CostoP').val();
    //    record.DescripcionP = $('#DescripcionP').val();

    //    $('.loading').show()
    //    $('.btn').hide();
    //    $.ajax({
    //        type: "POST",
    //        url: "frm_envios.aspx?fn=paqueteSave",
    //        data: JSON.stringify(record),
    //        contentType: "application/json; charset=utf-8",
    //        dataType: "json",
    //        success: function (response) {
    //            $('.loading').hide()
    //            $('.btn').show();
    //            basicModal2.close();
    //            if (response.rslt == 'exito') {
    //                $("#dv_Error2").hide()
    //                $("#dv_Message2").html('El record ha sido procesado con exito.');
    //                $("#dv_Message2").show();
    //                setTimeout(function () { $('#dv_Message2').hide(); }, 10000);
    //                paqueteLoad()
    //            }
    //            else {
    //                $("#dv_Error2").html(response.msj);
    //                $("#dv_Error2").show();
    //                setTimeout(function () { $('#dv_Error2').hide(); }, 10000);
    //            }
    //        },
    //        error: function () {
    //            $('.loading').hide()
    //            $('.btn').show();
    //            //$('#basicModal2').modal('hide');
    //            basicModal2.close();
    //            $("#dv_Error2").html('Error de comunicación con el servidor. El record no ha sido actualizado.');
    //            $("#dv_Error2").show();
    //            setTimeout(function () { $('#dv_Error2').hide(); }, 10000);
    //        }
    //    });
//};

    //function paqueteValidate() {
    //    var result = $("#form2").valid();
    //    return result;
    //};

    //function EventosListadoPaquetes() {
    //    $("#tbl_paquetes tbody").click(function (event) {

    //        $(tableElement2.fnSettings().aoData).each(function () {
    //            $(this.nTr).removeClass('row_selected');
    //        });
    //        $(event.target.parentNode).addClass('row_selected');

    //    });

    //    $("#tbl_paquetes tbody").dblclick(function (event) {

    //        $(tableElement2.fnSettings().aoData).each(function () {
    //            $(this.nTr).removeClass('row_selected');
    //        });
    //        $(event.target.parentNode).addClass('row_selected');
    //        paqueteEdit()
    //    });
//}

//CLIENTES
    var modalclienteE;
    function BuscarClientesE() {
        //asigna el string vacio en el campo NombreE
        $("#NombreE").val('');
        //esconde el boton btn_cargarE
        $('#btn_cargarE').hide();
        //muestra la imagen loading de dvloaderE
        $('#dvloaderE').show();
        //asigna el valor del input IdentificadorEmisor a var_identificador
        var var_identificador = $('#IdentificadorEmisor').val();
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
            //toma los datos del identificador con el querystring
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
                    $('#btn_cargarE').show()
                    $('#dvloaderE').hide()
                    try {
                        if (json.error == '-1') {
                            $("#tipo_cliente").val(1);                            
                            nuevoCmodal = $.remodal.lookup[$('[data-remodal-id=nuevoCmodal]').data('remodal')];
                            nuevoCmodal.open();
                            return false;
                        }

                        if (json.error == '-2') {
                            $("#dv_error").html("Se ha producido un error en el sistema. Intente de nuevo. Si el problema persiste comuniquese con el administrador del sistema");
                            $("#dv_error").show();
                            setTimeout(function () { $('#dv_error').hide(); }, 10000);
                            $("#IdentificadorEmisor").focus();
                        }

                        if (json.IdentificadorEmisor) {
                            var id = ""

                            try {
                                id = json.IdentificadorEmisor;
                            } catch (e) {
                                $("#dv_error").html("Error. " + e);
                                $("#dv_error").show();
                                setTimeout(function () { $('#dv_error').hide(); }, 10000);
                                return false;
                            }

                            $("#ClienteE").val(id)
                            CargarClienteE()
                            return false;
                        }
                    } catch (e) { }

                    modalclienteE = $.remodal.lookup[$('[data-remodal-id=modalclienteE]').data('remodal')];
                    modalclienteE.open();
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
                    { "mDataProp": "Identificador" },
                    { "mDataProp": "Telefono" },
                    { "mDataProp": "Email" }
            ]
        });

        //$(".first.paginate_button, .last.paginate_button").hide();
        var search_input = tableElement.closest('.dataTables_wrapper').find('div[id$=_filter] input');
        search_input.attr('placeholder', "Buscar");
    }

    function CargarClienteE() {
        var id = $("#ClienteE").val();

        $('.loading').show()
        $('.btn').hide();
        $.ajax({
            type: "POST",
            url: "frm_envios.aspx?fn=cargarclienteE",
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
                    $('#IdentificadorEmisor').val(response.IdentificadorEmisor);
                    $('#NombreE').val(response.NombreE);
                    //$("#IdentificadorReceptor").focus();
                }
                else {
                    $("#dv_error").html(response.msj);
                    $("#dv_error").show();
                    setTimeout(function () { $('#dv_error').hide(); }, 10000);
                    //$("#identificador").focus();
                }
            },
            error: function () {
                $('.loading').hide()
                $('.btn').show();
                $("#dv_error").html('Error de comunicación con el servidor. Función Cargarcliente().');
                $("#dv_error").show();
                setTimeout(function () { $('#dv_error').hide(); }, 10000);
                $("#IdentificadorEmisor").focus();
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
            SeleccionarClienteE()
        });
    }

    function SeleccionarClienteE() {
        var id = ""
        $('#tbClientesE tr').each(function () {
            if ($(this).hasClass('row_selected')) {
                try {
                    id = this.id;
                } catch (e) {
                    $("#dv_error_modal").html("Error. " + e);
                    $("#dv_error_modal").show();
                    setTimeout(function () { $('#dv_error_modal').hide(); }, 10000);
                    return false;
                }
            }
        });

        if (id == "") {
            $("#dv_error_modal").html("Debe seleccionar un cliente.");
            $("#dv_error_modal").show();
            setTimeout(function () { $('#dv_error_modal').hide(); }, 10000);
            return false;
        }
        else {
            $("#ClienteE").val(id)
            $("#btn_cerrarclienteE").click();
            CargarClienteE()
            return true;
        }
    }
    var modalclienteR;
    function BuscarClientesR() {
        $("#NombreR").val('');

        $('#btn_cargarR').hide();
        $('#dvloaderR').show();
        var var_identificador = $('#IdentificadorReceptor').val();

        $('#tbClientesR').dataTable().fnDestroy();
        var giRedraw = false;
        var responsiveHelper;
        var breakpointDefinition = {
            tablet: 1024,
            phone: 480
        };

        $('.loading').show()
        $('.btn').hide();
        tableElement = $('#tbClientesR')
        tableElement.dataTable({
            "bProcessing": true,
            "bServerSide": false,
            "sAjaxSource": "frm_envios.aspx?fn=buscarclientesR&identificador=" + var_identificador,
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
                    $('#btn_cargarR').show()
                    $('#dvloaderR').hide()
                    try {

                        if (json.error == '-1') {
                            $("#tipo_cliente").val(2);                            
                            nuevoCmodal = $.remodal.lookup[$('[data-remodal-id=nuevoCmodal]').data('remodal')];
                            nuevoCmodal.open();
                            return false;
                        }

                        if (json.error == '-2') {
                            $("#dv_error").html("Se ha producido un error en el sistema. Intente de nuevo. Si el problema persiste comuniquese con el administrador del sistema");
                            $("#dv_error").show();
                            setTimeout(function () { $('#dv_error').hide(); }, 10000);
                            //$("#IdentificadorReceptor").focus();
                            return false;
                        }

                        if (json.IdentificadorReceptor) {
                            var id = ""
                            try {
                                id = json.IdentificadorReceptor;
                            } catch (e) {
                                $("#dv_error").html("Error. " + e);
                                $("#dv_error").show();
                                setTimeout(function () { $('#dv_error').hide(); }, 10000);
                                return false;
                            }

                            $("#ClienteR").val(id)
                            CargarClienteR()
                            return false;
                        }
                    } catch (e) { }

                    modalclienteR = $.remodal.lookup[$('[data-remodal-id=modalclienteR]').data('remodal')];
                    modalclienteR.open();
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
                    { "mDataProp": "Identificador" },
                    { "mDataProp": "Telefono" },
                    { "mDataProp": "Email" }
            ]
        });

        //$(".first.paginate_button, .last.paginate_button").hide();
        var search_input = tableElement.closest('.dataTables_wrapper').find('div[id$=_filter] input');
        search_input.attr('placeholder', "Buscar");
    }

    function CargarClienteR() {
        var id = $("#ClienteR").val();
        $('.loading').show()
        $('.btn').hide();
        $.ajax({
            type: "POST",
            url: "frm_envios.aspx?fn=cargarclienteR",
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
                    $('#IdentificadorReceptor').val(response.IdentificadorReceptor);
                    $('#NombreR').val(response.NombreR);
                    //$("#IdentificadorReceptor").focus();
                }
                else {
                    $("#dv_error").html(response.msj);
                    $("#dv_error").show();
                    setTimeout(function () { $('#dv_error').hide(); }, 10000);
                    //$("#IdentificadorReceptor").focus();
                }
            },
            error: function () {
                $('.loading').hide()
                $('.btn').show();
                $("#dv_error").html('Error de comunicación con el servidor. Función CargarclienteR().');
                $("#dv_error").show();
                setTimeout(function () { $('#dv_error').hide(); }, 10000);
                $("#IdentificadorReceptor").focus();
            }
        });

    }

    function EventosListadoClienteR() {
        $("#tbClientesR tbody").click(function (event) {

            $(tableElement.fnSettings().aoData).each(function () {
                $(this.nTr).removeClass('row_selected');
            });
            $(event.target.parentNode).addClass('row_selected');

        });

        $("#tbClientesR tbody").dblclick(function (event) {

            $(tableElement.fnSettings().aoData).each(function () {
                $(this.nTr).removeClass('row_selected');
            });
            $(event.target.parentNode).addClass('row_selected');
            SeleccionarClienteR()
        });
    }

    function SeleccionarClienteR() {
        var id = ""
        $('#tbClientesR tr').each(function () {
            if ($(this).hasClass('row_selected')) {
                try {
                    id = this.id;
                } catch (e) {
                    $("#dv_error_modal").html("Error. " + e);
                    $("#dv_error_modal").show();
                    setTimeout(function () { $('#dv_error_modal').hide(); }, 10000);
                    return false;
                }
            }
        });

        if (id == "") {
            $("#dv_error_modal").html("Debe seleccionar un cliente.");
            $("#dv_error_modal").show();
            setTimeout(function () { $('#dv_error_modal').hide(); }, 10000);
            return false;
        }
        else {
            $("#ClienteR").val(id)
            $("#btn_cerrarClienteR").click();
            CargarClienteR()
            return true;
        }
    }
    var modalClienteN;
    function NuevoCliente(tipo) {
        nuevoCmodal.close()
        $("#IdentificadorC").val('');
        $("#NombreC").val('');
        $("#TelefonoC").val('');
        $("#EmailC").val('');

        modalClienteN = $.remodal.lookup[$('[data-remodal-id=modalClienteN]').data('remodal')];
        modalClienteN.open();
    }

    function ValidarClienteN() {
        var result = $("#form3").valid();
        return result;
    };

    var msjModal1;
    function GuardarClienteN() {
        var registro = {};
        registro.TipoCliente = $('#tipo_cliente').val();
        registro.IdentificadorC = $('#IdentificadorC').val();
        registro.NombreC = $('#NombreC').val();
        registro.TelefonoC = $('#TelefonoC').val();
        registro.EmailC = $('#EmailC').val();

        $('.loading').show()
        $('.btn').hide();
        $.ajax({
            type: "POST",
            url: "frm_envios.aspx?fn=guardarCliente",
            data: JSON.stringify(registro),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (response) {
                $('.loading').hide()
                $('.btn').show();
                modalClienteN.close();
                if (response.error == '-1') {
                    window.location.href = 'info.aspx';
                    return false;
                }
                if (response.rslt == 'exito') {

                    var options = {
                        "backdrop": "static",
                        "keyboard": "true"
                    }
                    if (response.Tipo == 1) {
                        $('#ClienteE').val(response.idCliente);
                        $('#IdentificadorEmisor').val(response.Identificador);
                        $('#NombreE').val(response.Nombre);
                        $("#dv_Error").hide()
                        $("#dv_Message").html('El cliente ha sido guardado.');
                        $("#dv_Message").show();
                        setTimeout(function () { $('#dv_Message').hide(); }, 10000);
                    }
                    else {
                        $('#ClienteR').val(response.idCliente);
                        $('#IdentificadorReceptor').val(response.Identificador);
                        $('#NombreR').val(response.Nombre);
                        $("#dv_Error").hide()
                        $("#dv_Message").html('El cliente ha sido guardado.');
                        $("#dv_Message").show();
                        setTimeout(function () { $('#dv_Message').hide(); }, 10000);
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
                $("#dv_errorClienteN").html('Error de comunicación con el servidor. Función GuardarCliente().');
                $("#dv_errorClienteN").show();
                setTimeout(function () { $('#dv_errorClienteN').hide(); }, 10000);
            }
        });
    }
    //COMBOS
    function CargarPaisesE() {
        $("#PaisE").empty();
        $.ajax({
            type: "POST",
            url: "frm_envios.aspx?fn=cargar_paises",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (response) {
                $.each(response.aaData, function (i, item) {
                    if (item.id == 0) {
                        $("#PaisE").append("<option value=" + item.id + " selected='selected'>" + item.des + "</option>");
                    }
                    else {
                        $("#PaisE").append("<option value=" + item.id + ">" + item.des + "</option>");
                    }
                });
                $("#PaisE").val($("#HfPaisE").val());
                CargarEstadosE()
            },
            error: function () {
                $("#dv_mensaje").hide();
                $("#dv_Error").html('Error de comunicación con el servidor al intentar cargar la funcion Pais.');
                $("#dv_Error").show();
                setTimeout(function () { $('#dv_Error').hide(); }, 10000);
            }
        });
    }

    function CargarEstadosE() {
        var p = $("#PaisE").val();
        $("#EstadoE").empty();
        $.ajax({
            type: "POST",
            url: "frm_envios.aspx?fn=cargar_estados&p=" + p,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (response) {
                $.each(response.aaData, function (i, item) {
                    if (item.id == 0) {
                        $("#EstadoE").append("<option value=" + item.id + " selected='selected'>" + item.des + "</option>");
                    }
                    else {
                        $("#EstadoE").append("<option value=" + item.id + ">" + item.des + "</option>");
                    }
                });
                $("#EstadoE").val($("#HfEstadoE").val());
                CargarCiudadesE();
            },
            error: function () {
                $("#dv_mensaje").hide();
                $("#dv_Error").html('Error de comunicación con el servidor al intentar cargar la funcion Estados.');
                $("#dv_Error").show();
                setTimeout(function () { $('#dv_Error').hide(); }, 10000);
            }
        });
    }

    function CargarCiudadesE() {
        var e = $("#EstadoE").val();
        $("#CiudadE").empty();
        $.ajax({
            type: "POST",
            url: "frm_envios.aspx?fn=cargar_ciudades&e=" + e,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (response) {
                $.each(response.aaData, function (i, item) {
                    if (item.id == 0) {
                        $("#CiudadE").append("<option value=" + item.id + " selected='selected'>" + item.des + "</option>");
                    }
                    else {
                        $("#CiudadE").append("<option value=" + item.id + ">" + item.des + "</option>");
                    }
                });
                $("#CiudadE").val($("#HfCiudadE").val());
            },
            error: function () {
                $("#dv_mensaje").hide();
                $("#dv_Error").html('Error de comunicación con el servidor al intentar cargar la funcion Ciudades.');
                $("#dv_Error").show();
                setTimeout(function () { $('#dv_Error').hide(); }, 10000);
            }
        });
    }


    function CargarPaisesR() {
        $("#PaisR").empty();
        $.ajax({
            type: "POST",
            url: "frm_envios.aspx?fn=cargar_paises",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (response) {
                $.each(response.aaData, function (i, item) {
                    if (item.id == 0) {
                        $("#PaisR").append("<option value=" + item.id + " selected='selected'>" + item.des + "</option>");
                    }
                    else {
                        $("#PaisR").append("<option value=" + item.id + ">" + item.des + "</option>");
                    }
                });
                $("#PaisR").val($("#HfPaisR").val());
                CargarEstadosR()
            },
            error: function () {
                $("#dv_mensaje").hide();
                $("#dv_Error").html('Error de comunicación con el servidor al intentar cargar la funcion Pais.');
                $("#dv_Error").show();
                setTimeout(function () { $('#dv_Error').hide(); }, 10000);
            }
        });
    }

    function CargarEstadosR() {
        var p = $("#PaisR").val();
        $("#EstadoR").empty();
        $.ajax({
            type: "POST",
            url: "frm_envios.aspx?fn=cargar_estados&p=" + p,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (response) {
                $.each(response.aaData, function (i, item) {
                    if (item.id == 0) {
                        $("#EstadoR").append("<option value=" + item.id + " selected='selected'>" + item.des + "</option>");
                    }
                    else {
                        $("#EstadoR").append("<option value=" + item.id + ">" + item.des + "</option>");
                    }
                });
                $("#EstadoR").val($("#HfEstadoR").val());
                CargarCiudadesR();
            },
            error: function () {
                $("#dv_mensaje").hide();
                $("#dv_Error").html('Error de comunicación con el servidor al intentar cargar la funcion Estados.');
                $("#dv_Error").show();
                setTimeout(function () { $('#dv_Error').hide(); }, 10000);
            }
        });
    }
    
    function CargarCiudadesR() {
        var e = $("#EstadoR").val();
        $("#CiudadR").empty();
        $.ajax({
            type: "POST",
            url: "frm_envios.aspx?fn=cargar_ciudades&e=" + e,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (response) {
                $.each(response.aaData, function (i, item) {
                    if (item.id == 0) {
                        $("#CiudadR").append("<option value=" + item.id + " selected='selected'>" + item.des + "</option>");
                    }
                    else {
                        $("#CiudadR").append("<option value=" + item.id + ">" + item.des + "</option>");
                    }
                });
                $("#CiudadR").val($("#HfCiudadR").val());
            },
            error: function () {
                $("#dv_mensaje").hide();
                $("#dv_Error").html('Error de comunicación con el servidor al intentar cargar la funcion Ciudades.');
                $("#dv_Error").show();
                setTimeout(function () { $('#dv_Error').hide(); }, 10000);
            }
        });
    }
