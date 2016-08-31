function Cargar() {
    Permisos()
    $("#Venta").focus();
    EventosListado();
    //EventosListadoCliente();
    EventosListadoVentas();
    //cargar_moneda()
    cargar_formaPago()
    cargar_banco()

    $('#Fecha').datepicker({ dateFormat: 'dd/mm/yy' })
    $('#FechaPago').datepicker({ dateFormat: 'dd/mm/yy' })
    var idt = $.url().param('id');

    $('.decimal').keypress(function(evt) {
 	if(evt.which==46){
            $(this).val($(this).val()+',');
            evt.preventDefault();
        }
    });

    $('.loading').show()
    $('.btn').hide();
    $.ajax({
        type: "POST",
        url: "frm_recibos.aspx?fn=cargar",
        data: '{"id":"' + idt + '"}',
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
                if (response.idCliente > 0) {
                    $('#Numero').val(response.Numero);
                    $('#Venta').val(response.Venta);
                    $('#Cliente').val(response.idCliente);
                    $('#Nombre').val(response.Nombre);
                    //$('#rif').val(response.Rif);
                    $('#Direccion').val(response.Direccion);
                    $('#Telefono').val(response.Telefono);
                    $('#Fecha').val(response.Fecha);
                    $('#Monto').val(response.Monto);
                    $('#Observaciones').val(response.Observaciones);
                    $('#IdVenta').val(response.idventa);
                    $('#hf_venta').val(response.idventa);//Para ver si tengo que cargar el cliente ya que es nuevo
                    CargarVenta()
                    CargarDetalles()  
                } else {
                    $('#Fecha').val(response.Fecha);
                    $('#Numero').val(response.Numero);
                }
                    $("#Venta").focus();
                 }
            else {
                $("#dv_error").html(response.msj);
                $("#dv_error").show();
                setTimeout(function () { $('#dv_error').hide(); }, 10000);
                $("#Venta").focus();
            }
        },
        error: function () {
            $('.loading').hide()
            $('.btn').show();
            $("#dv_error").html('Error de comunicación con el servidor. Función Cargar().');
            $("#dv_error").show();
            setTimeout(function () { $('#dv_error').hide(); }, 10000);
            $("#Venta").focus();
        }
    });

}

function Permisos() {
    var v = $.url().param('v');
    $('.loading').show()
    $('.btn').hide();
    $.ajax({
        type: "POST",
        url: "frm_recibos.aspx?fn=permisos&v=" + v,
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
                    $("#btn_cargarVenta").attr("disabled", "disabled");
                    $("#dvloaderVenta").attr("disabled", "disabled");
                    $("#dvloader").attr("disabled", "disabled");
                    $("#btn_agregarDetalle").attr("disabled", "disabled");
                    $("#btn_editarDetalle").attr("disabled", "disabled");
                    $("#btn_eliminarDetalle").attr("disabled", "disabled");
                    //$('#pedido_factura').attr("disabled", "disabled");

                    $('#Numero').attr("disabled", "disabled");
                    //$('#rif').attr("disabled", "disabled");
                    $('#Venta').attr("disabled", "disabled");
                    $('#Nombre').attr("disabled", "disabled");
                    $('#Telefono').attr("disabled", "disabled");
                    $('#Direccion').attr("disabled", "disabled");
                    $('#Fecha').attr("disabled", "disabled");
                    $('#Monto').attr("disabled", "disabled");
                    $('#Observaciones').attr("disabled", "disabled");

                    $('#FormaPago').attr("disabled", "disabled");
                    $('#Fecha').attr("disabled", "disabled");
                    $('#MontoPago').attr("disabled", "disabled");
                    $('#NumeroDocumento').attr("disabled", "disabled");
                    $("#Venta").focus();
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

var modalVenta;
function BuscarVentas() {

    $("#Cliente").val('');

    $('#btn_cargarVenta').hide();
    $('#dvloaderVenta').show();

    var var_venta = $('#Venta').val();

    $('#tbPedidos').dataTable().fnDestroy();
    var giRedraw = false;
    var responsiveHelper;
    var breakpointDefinition = {
        tablet: 1024,
        phone: 480
    };

    tableElement = $('#tbPedidos')

    tableElement.dataTable({
        "bProcessing": true,
        "bServerSide": false,
        "sAjaxSource": "frm_recibos.aspx?fn=buscarventas&venta=" + var_venta,
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
                $('#btn_cargarVenta').show()
                $('#dvloaderVenta').hide()
                try {
                    if (json.error == '-1') {
                        $("#dv_advertencia1").html("No se encontraron ventas con el numero indicado");
                        $("#dv_advertencia1").show();
                        $("#Venta").focus();
                        setTimeout(function () { $('#dv_advertencia1').hide(); }, 10000);
                        return false;
                    }

                    if (json.error == '-2') {
                        $("#dv_error1").html("Se ha producido un error en el sistema. Intente de nuevo. Si el problema persiste comuniquese con el administrador del sistema");
                        $("#dv_error1").show();
                        setTimeout(function () { $('#dv_error1').hide(); }, 10000);
                        $("#Venta").focus();
                        return false;
                    }
                    if (json.DT_RowId) {
                        var id = ""

                        try {
                            id = json.DT_RowId;
                        } catch (e) {
                            $("#dv_error1").html("Error. " + e);
                            $("#dv_error1").show();
                            setTimeout(function () { $('#dv_error1').hide(); }, 10000);
                            return false;
                        }
                        $("#Venta").val(id)
                        CargarVenta();
                        return false;
                    }
                    
                } catch (e) { }

                modalVenta = $.remodal.lookup[$('[data-remodal-id=modalVenta]').data('remodal')];
                modalVenta.open();
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
                {
                    "mDataProp": "Fecha",
                    "stype": "date",
                    "fnRender": function (oObj) {
                        var d = new Date(oObj.aData.Fecha);
                        var m = d.getMonth() + 1;
                        var i = d.getDate();
                        if (navigator.userAgent.toLowerCase().indexOf('chrome') > -1) {

                            if (i == '31') {
                                i = '1';
                                m = m + 1;
                            } else {
                                i = d.getDate() + 1;
                            }

                        }
                        d = ((i < 10 ? '0' : '') + i) + "/" + ((m < 10 ? '0' : '') + m) + "/" + d.getFullYear();
                        return "<div class='date'>" + d + "<div>";
                    }
                },
                { "mDataProp": "Pasajero" },
                { "mDataProp": "Boleto" },
                { "mDataProp": "Aerolinea" },
                { "mDataProp": "Total" }
        ]
    });

    //$(".first.paginate_button, .last.paginate_button").hide();
    var search_input = tableElement.closest('.dataTables_wrapper').find('div[id$=_filter] input');
    search_input.attr('placeholder', "Buscar");
}
function EventosListadoVentas() {
    $("#tbPedidos tbody").click(function (event) {

        $(tableElement.fnSettings().aoData).each(function () {
            $(this.nTr).removeClass('row_selected');
        });
        $(event.target.parentNode).addClass('row_selected');

    });

    $("#tbPedidos tbody").dblclick(function (event) {

        $(tableElement.fnSettings().aoData).each(function () {
            $(this.nTr).removeClass('row_selected');
        });
        $(event.target.parentNode).addClass('row_selected');
        SeleccionarVenta()
    });

}
function SeleccionarVenta() {
    var id = ""
    $('#tbPedidos tr').each(function () {
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
        $("#Venta").val(id);
        CargarVenta()
        //CargarCliente()
        $("#btn_cerrarpedido").click();
        return true;
    }
}
function CargarVenta() {
    var id = $("#Venta").val();

    $.ajax({
        type: "POST",
        url: "frm_recibos.aspx?fn=cargarventas",
        data: '{"id":"' + id + '"}',
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            if (response.error == '-1') {
                window.location.href = 'info.aspx';
                return false;
            }
            if (response.rslt == 'exito') {
                $('#Venta').val(response.Numero);
                $("#Venta").focus();
                $('#Cliente').val(response.idCliente);
                $("#Monto").val(response.Monto);
                $("#IdVenta").val(response.idVenta);
                //$("#abonado").val(response.Abonado);
                if ($("#hf_pedido").val()<=0){
                    CargarCliente()
                }
                    cargar_detalles_ventas()

            }
            else {
                $("#dv_mensaje1").hide()
                $("#dv_error1").html(response.msj);
                $("#dv_error1").show();
                setTimeout(function () { $('#dv_error1').hide(); }, 10000);
                $("#Venta").focus();
            }
        },
        error: function () {
            $("#dv_mensaje1").hide();
            $("#dv_error1").html('Error de comunicación con el servidor. El registro no ha sido cargado.');
            $("#dv_error1").show();
            setTimeout(function () { $('#dv_error1').hide(); }, 10000);
            $("#Venta").focus();
        }
    });

}

function CargarCliente() {
    var id = $("#Cliente").val();

    $('.loading').show()
    $('.btn').hide();
    $.ajax({
        type: "POST",
        url: "frm_recibos.aspx?fn=cargarcliente",
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
                $('#Identificador').val(response.Identificador);
                $('#Nombre').val(response.Nombre);
                $('#Telefono').val(response.Telefono);
                $('#Direccion').val(response.Direccion);
                $("#Identificador").focus();
            }
            else {
                $("#dv_error").html(response.msj);
                $("#dv_error").show();
                setTimeout(function () { $('#dv_error').hide(); }, 10000);
                $("#Identificador").focus();
            }
        },
        error: function () {
            $('.loading').hide()
            $('.btn').show();
            $("#dv_error").html('Error de comunicación con el servidor. Función CargarCliente().');
            $("#dv_error").show();
            setTimeout(function () { $('#dv_error').hide(); }, 10000);
            $("#Identificador").focus();
        }
    });

}


function EventosListado() {
    $("#tbDetalles tbody").click(function (event) {

        $(tableElement.fnSettings().aoData).each(function () {
            $(this.nTr).removeClass('row_selected');
        });
        $(event.target.parentNode).addClass('row_selected');

    });

    $("#tbDetalles tbody").dblclick(function (event) {

        $(tableElement.fnSettings().aoData).each(function () {
            $(this.nTr).removeClass('row_selected');
        });
        $(event.target.parentNode).addClass('row_selected');
        EditarDetalles()
    });
}

var msjModal;
function Guardar() {
    var registro = {};
    registro.id = $('#id').val();
    registro.Numero = $('#Numero').val();
    registro.Venta = $('#Venta').val();
    registro.Cliente = $('#Cliente').val();
    registro.Nombre = $('#Nombre').val();
    //registro.Rif = $('#rif').val();
    registro.Telefono = $('#Telefono').val();
    registro.Direccion = $('#Direccion').val();
    registro.Fecha = $('#Fecha').val();
    registro.Monto = $('#Monto').val();
    registro.Observaciones = $('#Observaciones').val();

    $('.loading').show()
    $('.btn').hide();
    $.ajax({
        type: "POST",
        url: "frm_recibos.aspx?fn=guardar",
        data: JSON.stringify(registro),
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

                $('#id').val(response.id);
                msjModal = $.remodal.lookup[$('[data-remodal-id=msjModal]').data('remodal')];
                msjModal.open();
                if (response.msj != '') {
                    $("#msj").html("El registro ha sido guardado con errores: " + response.msj);
                }
                else {
                    $("#msj").html("El registro ha sido guardado con exito.");
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
            $("#dv_error").html('Error de comunicación con el servidor. Función Guardar().');
            $("#dv_error").show();
            setTimeout(function () { $('#dv_error').hide(); }, 10000);
        }
    });
}
function recibosLista() {
    var var_id = $('#id').val();
    window.location.href = 'lst_recibos.aspx?id=' + var_id;
}
function Validar() {
    var valido = $("#form1").valid();
    return valido;
}

var subtotales = new Array();
function CargarDetalles() {

    subtotales.length = 0;
    subtotales.push(0);

$('#tbDetalles').dataTable().fnDestroy();
var giRedraw = false;
var responsiveHelper;
var breakpointDefinition = {
    tablet: 1024,
    phone: 480
};
   
$('#TotalR').val(0);

tableElement = $('#tbDetalles')

tableElement.dataTable({
    "bProcessing": true,
    "bServerSide": false,
    "sAjaxSource": "frm_recibos.aspx?fn=cargar_detalles",
    "sPaginationType": "full_numbers",
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
        "sZeroRecords": "No se encontraron registros",
        "oPaginate": {
            "sNext": " <span class='glyphicon glyphicon-chevron-right' /> ",
            "sPrevious": " <span class='glyphicon glyphicon-chevron-left' / ",
            "sFirst": " << ",
            "sLast": " >> "
        }
    },
    "sDom": 'frt<"izq"i><"der"p>',
    "fnCreatedRow": function (row, data, index) {
        
        var subtotal = data.Monto;
        //subtotal = $.parseNumber(subtotal, { format: "#,###.00", locale: "es" });

        subtotales[0] += subtotal;

        var sub = $.formatNumber(subtotales[0], { format: "#,###.00", locale: "es" });
        $('#TotalR').val(sub);

    },
    fnPreDrawCallback: function () {
        // Initialize the responsive datatables helper once.
        if (!responsiveHelper) {
            responsiveHelper = new ResponsiveDatatablesHelper(tableElement, breakpointDefinition);
        }
    },
    fnRowCallback  : function (nRow, aData, iDisplayIndex, iDisplayIndexFull) {
        responsiveHelper.createExpandIcon(nRow);
    },
    fnDrawCallback : function (oSettings) {
        responsiveHelper.respond();
    },
    "aoColumns": [
            //{ "mDataProp": "Monto", },
            {
                "mDataProp": "Monto",
                "sType": 'numeric',
                "mRender": function (data) {
                    return $.formatNumber(data, { format: "#,###.00", locale: "es" });
                }
            },
            {
                "mDataProp": "Fecha",
                "stype": "date",
                "fnRender": function (oObj) {
                    var d = new Date(oObj.aData.Fecha);
                    var m = d.getMonth() + 1;
                    var i = d.getDate();
                    if (navigator.userAgent.toLowerCase().indexOf('chrome') > -1) {

                        if (i == '31') {
                            i = '1';
                            m = m + 1;
                        } else {
                            i = d.getDate() + 1;
                        }

                    }
                    d = ((i < 10 ? '0' : '') + i) + "/" + ((m < 10 ? '0' : '') + m) + "/" + d.getFullYear();
                    return "<div class='date'>" + d + "<div>";
                }
            },
            { "mDataProp": "Forma Pago" },
            { "mDataProp": "Numero Documento" },
            { "mDataProp": "Banco" }
    ]
});

//$(".first.paginate_button, .last.paginate_button").hide();
var search_input = tableElement.closest('.dataTables_wrapper').find('div[id$=_filter] input');
search_input.attr('placeholder', "Buscar");

}

var modal
var msjModalPago
function NuevoDetalles() {

    $('#iddp').val(0);
    var renglon = 0;
    $('#tbDetalles tr').each(function () {
        if ($(this)[0].cells.length > 1) {
            if (!isNaN($(this)[0].cells[0].innerText)) {
                if (renglon < (parseInt($(this)[0].cells[0].innerText) + 1)) {
                    renglon = $(this)[0].cells[0].innerText;
                }
            }
        }        
    });

    $('#MontoPago').val('');
    var d = new Date();
    var i = d.getDate();
    var m = d.getMonth() + 1;
    d = ((i < 10 ? '0' : '') + i) + "/" + ((m < 10 ? '0' : '') + m) + "/" + d.getFullYear();
    $('#FechaPago').val(d);
    $('#FormaPago').val(0);
    $('#NumeroDocumento').val('');
    $('#Banco').val(0);

    var k = $("#TotalR").val();
    var l = k.replace(".", "");
    var t = l.replace(",", ".");

    var o = $("#abonado").val();
    var m = o.replace(".", "");
    var b = m.replace(",", ".");

    var f = $.parseNumber(($("#Monto").val()), { format: "#,###.00", locale: "es" }) - t - b;
    //abonado
    if (f > 0) {
	    f = $.formatNumber(f, { format: "#,###.00", locale: "es" });
        $("#MontoPago").val(f);
        HabilitarNumeroBanco();

        modal = $.remodal.lookup[$('[data-remodal-id=modal]').data('remodal')];
        modal.open();
    }
    else {
        msjModalPago = $.remodal.lookup[$('[data-remodal-id=msjModalPago]').data('remodal')];
        msjModalPago.open();
    }


    $('#MontoPago').focus();

}
function EditarDetalles() {
    var id='';
    $('#tbDetalles tr').each(function () {
        if ($(this).hasClass('row_selected')) {
            id = this.id;
            $("#iddp").val(id);
        }
    });

    if (id == '') {
        $("#dv_mensajedetalle").hide()
        $("#dv_errordetalle").html('Seleccione un registro.');
        $("#dv_errordetalle").show();
        setTimeout(function () { $('#dv_errordetalle').hide(); }, 10000);
        return false;
    }

    $('.loading').show()
    $('.btn').hide();
    $.ajax({
        type: "POST",
        url: "frm_recibos.aspx?fn=EditarDetalles",
        data: '{"iddp":"' + id + '"}',
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            $('.loading').hide()
            $('.btn').show();

            if (response.rslt == 'exito') {
                $('#MontoPago').val(response.MontoPago);
                $('#FechaPago').val(response.FechaPago);
                $('#FormaPago').val(response.FormaPago);
                $('#NumeroDocumento').val(response.NumeroDocumento);
                $('#Banco').val(response.Banco);
                modal = $.remodal.lookup[$('[data-remodal-id=modal]').data('remodal')];
                modal.open();
            }
            else {
                $('#BtnCerrar').click();
                $("#dv_errordetalle").html(response.msj);
                $("#dv_errordetalle").show();
                setTimeout(function () { $('#dv_errordetalle').hide(); }, 10000);
            }
        },
        error: function () {
            $('#BtnCerrar').click();
            $('.loading').hide()
            $('.btn').show();
            $("#dv_mensajedetalle").hide();
            $("#dv_errordetalle").html('Error de comunicación con el servidor. El registro no ha sido actualizado.');
            $("#dv_errordetalle").show();
            setTimeout(function () { $('#dv_error2').hide(); }, 10000);
        }
    });

}
function GuardarDetalles() {

    var registro = {};
    registro.iddp = $('#iddp').val();
    registro.MontoPago = $('#MontoPago').val();
    registro.FechaPago = $('#FechaPago').val();
    registro.FormaPago = $('#FormaPago').val();
    registro.NumeroDocumento = $('#NumeroDocumento').val();
    registro.Banco = $('#Banco').val();

    $('.loading').show()
    $('.btn').hide();
    $.ajax({
        type: "POST",
        url: "frm_recibos.aspx?fn=GuardarDetalles",
        data: JSON.stringify(registro),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            $('.loading').hide()
            $('.btn').show();
            $('#BtnCerrar').click()
            if (response.rslt == 'exito') {
                $("#dv_errordetalle").hide()
                $("#dv_mensajedetalle").html('El registro ha sido procesado con exito.');
                $("#dv_mensajedetalle").show();
                setTimeout(function () { $('#dv_mensajedetalle').hide(); }, 10000);
                CargarDetalles()
            }
            else {
                $("#dv_errordetalle").html(response.msj);
                $("#dv_errordetalle").show();
                setTimeout(function () { $('#dv_errordetalle').hide(); }, 10000);
            }
        },
        error: function () {
            $('.loading').hide()
            $('.btn').show();
            $('#BtnCerrar').click()
            $("#dv_mensajedetalle").hide();
            $("#dv_errordetalle").html('Error de comunicación con el servidor. Función GuardarDetalles().');
            $("#dv_errordetalle").show();
            setTimeout(function () { $('#dv_error').hide(); }, 10000);

        }
    });
}
var deletemodal
function ConfirmarDetalles() {
    var id = ''
    $('#tbDetalles tr').each(function () {
        if ($(this).hasClass('row_selected')) {
            id += ',' + this.id;
        }
    });

    if (id == '') {
        $("#dv_mensaje").hide()
        $("#dv_error").html('Seleccione un registro.');
        $("#dv_error").show();
        setTimeout(function () { $('#dv_error').hide(); }, 10000);
        return false;
    }

    deletemodal = $.remodal.lookup[$('[data-remodal-id=deletemodal]').data('remodal')];
    deletemodal.open();
}
function ValidarDetalles() {
    var valido = $("#form2").valid();
    return valido;
}
function EliminarDetalles() {
    var id=''
    $('#tbDetalles tr').each(function () {
        if ($(this).hasClass('row_selected')) {
            id += ',' + this.id;
        }
    });

    if (id == '') {
        $("#dv_mensaje").hide()
        $("#dv_error").html('Seleccione un registro.');
        $("#dv_error").show();
        setTimeout(function () { $('#dv_error').hide(); }, 10000);
        return false;
    }

    $('.loading').show()
    $('.btn').hide();
    $.ajax({
        type: "POST",
        url: "frm_recibos.aspx?fn=EliminarDetalles",
        data: '{"iddp":"' + id + '"}',
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            $('.loading').hide()
            $('.btn').show();
            $('#btn_cerrardelete').click();
            if (response.rslt == 'exito') {
                $("#dv_error").hide()
                $("#dv_mensaje").html('El registro ha sido eliminado.');
                $("#dv_mensaje").show();
                setTimeout(function () { $('#dv_mensaje').hide(); }, 10000);
                CargarDetalles();
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
            $("#dv_error").html('Error de comunicación con el servidor. Función EliminarDetalles().');
            $("#dv_error").show();
            setTimeout(function () { $('#dv_error').hide(); }, 10000);

        }
    });

}

function cargar_moneda() {
    $.ajax({
        type: "POST",
        url: "frm_recibos.aspx?fn=cargar_moneda",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            $.each(response.aaData, function (i, item) {
                if (item.principal == 1) {
                    $("#Moneda").append("<option value=" + item.id + " class='principal'>" + item.des + "</option>");
                }
                else {
                    $("#Moneda").append("<option value=" + item.id + ">" + item.des + "</option>");
                }
            });
            $("#Moneda").val($("#hfMoneda").val());
        },
        error: function () {
            $("#dv_mensaje").hide();
            $("#dv_error").html('Error de comunicación con el servidor. El registro no ha sido actualizado.');
            $("#dv_error").show();
        }
    });
}
function cargar_formaPago() {
    $.ajax({
        type: "POST",
        url: "frm_recibos.aspx?fn=cargar_formaPago",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            $.each(response.aaData, function (i, item) {
                if (item.principal == 1) {
                    $("#FormaPago").append("<option value=" + item.id + " class='principal'>" + item.des + "</option>");
                }
                else {
                    $("#FormaPago").append("<option value=" + item.id + ">" + item.des + "</option>");
                }
            });
            $("#FormaPago").val($("#hfFormaPago").val());
        },
        error: function () {
            $("#dv_mensaje").hide();
            $("#dv_error").html('Error de comunicación con el servidor. El registro no ha sido actualizado.');
            $("#dv_error").show();
        }
    });
}
function cargar_banco() {
    $.ajax({
        type: "POST",
        url: "frm_recibos.aspx?fn=cargar_banco",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            $.each(response.aaData, function (i, item) {
                if (item.principal == 1) {
                    $("#Banco").append("<option value=" + item.id + " class='principal'>" + item.des + "</option>");
                }
                else {
                    $("#Banco").append("<option value=" + item.id + ">" + item.des + "</option>");
                }
            });
            $("#Banco").val($("#hfBanco").val());
        },
        error: function () {
            $("#dv_mensaje").hide();
            $("#dv_error").html('Error de comunicación con el servidor. El registro no ha sido actualizado.');
            $("#dv_error").show();
        }
    });
}

var monedas = new Array();
var subtotales = new Array();
var impuestos = new Array();
var totales = new Array();

var oTable;
function cargar_detalles_ventas() {
    monedas.length = 0;
    monedas.push('');
    monedas.push('');
    monedas.push('');

    subtotales.length = 0;
    subtotales.push(0);
    subtotales.push(0);
    subtotales.push(0);

    impuestos.length = 0;
    impuestos.push(0);
    impuestos.push(0);
    impuestos.push(0);

    totales.length = 0;
    totales.push(0);
    totales.push(0);
    totales.push(0);

    $('#tbDetallesPedido').dataTable().fnDestroy();
    var giRedraw = false;
    var responsiveHelper;
    var breakpointDefinition = {
        tablet: 1024,
        phone: 480
    };

    var id = $("#Venta").val();
    oTable = $('#tbDetallesPedido')
    oTable.dataTable({
        "bProcessing": true,
        "bServerSide": false,
        "sAjaxSource": "frm_recibos.aspx?fn=cargar_detalles_ventas&id=" + id,
        "sPaginationType": "full_numbers",
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
            "sZeroRecords": "No se encontraron registros",
            "oPaginate": {
                "sNext": " <span class='glyphicon glyphicon-chevron-right' /> ",
                "sPrevious": " <span class='glyphicon glyphicon-chevron-left' / ",
                "sFirst": " << ",
                "sLast": " >> "
            }
        },
        "sDom": 'frt<"izq"i><"der"p>',
        //"oTableTools": {
        //    "sSwfPath": "swf/copy_csv_xls_pdf.swf"
        //},
        "fnCreatedRow": function (row, data, index) {
            var monedaencontrada = 0;
            var moneda = data.Moneda;
            var subtotal = data.Total;
            //subtotal = $.parseNumber(subtotal, { format: "#,###.00", locale: "es" });

            var impuesto = data.Impuesto;
            //impuesto = $.parseNumber(impuesto, { format: "#,###.00", locale: "es" });

            var total = 0;
            if (impuesto > 0) {
                impuesto = impuesto / 100;
                impuesto = impuesto * subtotal;
                total = subtotal + impuesto;
            }
            else {
                total = subtotal;
            }

            var count = (monedas.length);
            for (var i = 1; i <= count; i++) {
                if (moneda == monedas[i - 1]) {
                    subtotales[i - 1] += subtotal;
                    impuestos[i - 1] += impuesto;
                    totales[i - 1] += total;
                    monedaencontrada = 1;
                    break;
                }
            }

            if (monedaencontrada == 0) {
                for (var i = 1; i <= count; i++) {
                    if (monedas[i - 1] == '') {
                        monedas[i - 1] = moneda;
                        subtotales[i - 1] += subtotal;
                        impuestos[i - 1] += impuesto;
                        totales[i - 1] += total;
                        monedaencontrada = 1;
                        break;
                    }
                }
            }

            for (var i = 1; i <= count; i++) {
                if (monedas[i - 1] != '') {
                    if (impuestos[i - 1] > 0) {
                        $('#dv_subtotal' + i).removeClass('hide');
                        $('#dv_impuesto' + i).removeClass('hide');
                        $('#dv_total' + i).removeClass('hide');

                        $('#lbl_subtotal_' + i).html(monedas[i - 1]);
                        $('#lbl_impuesto_' + i).html(monedas[i - 1]);
                        $('#lbl_total_' + i).html(monedas[i - 1]);

                        var sub = $.formatNumber(subtotales[i - 1], { format: "#,###.00", locale: "es" });
                        $('#SubTotal' + i).val(sub);

                        var imp = $.formatNumber(impuestos[i - 1], { format: "#,###.00", locale: "es" });
                        $('#Impuesto' + i).val(imp);

                        var tot = $.formatNumber(totales[i - 1], { format: "#,###.00", locale: "es" });
                        $('#Total' + i).val(tot);
                    }
                    else {

                        $('#dv_subtotal' + i).addClass('hide');
                        $('#dv_impuesto' + i).addClass('hide');
                        $('#dv_total' + i).removeClass('hide');

                        $('#lbl_total_' + i).html(monedas[i - 1]);

                        var tot = $.formatNumber(totales[i - 1], { format: "#,###.00", locale: "es" });
                        $('#Total' + i).val(tot);
                    }
                }
                else {
                    $('#dv_subtotal' + i).addClass('hide');
                    $('#dv_impuesto' + i).addClass('hide');
                    $('#dv_total' + i).addClass('hide');
                }
            }

        },
        "fnInitComplete": function (oSettings, json) {
            if (oTable.fnGetData().length == 0) {
                $('#dv_subtotal1').removeClass('hide');
                $('#lbl_subtotal_1').html("");
                $('#SubTotal1').val(0);

                $('#dv_impuesto1').removeClass('hide');
                $('#lbl_impuesto_1').html("");
                $('#Impuesto1').val(0);

                $('#dv_total1').removeClass('hide');
                $('#lbl_total_1').html("");
                $('#Total1').val(0);

                $('#dv_subtotal2').addClass('hide');
                $('#dv_impuesto2').addClass('hide');
                $('#dv_total2').addClass('hide');

                $('#dv_subtotal3').addClass('hide');
                $('#dv_impuesto3').addClass('hide');
                $('#dv_total3').addClass('hide');
            }
        },
        fnPreDrawCallback: function () {
            // Initialize the responsive datatables helper once.
            if (!responsiveHelper) {
                responsiveHelper = new ResponsiveDatatablesHelper(oTable, breakpointDefinition);
            }
        },
        fnRowCallback: function (nRow, aData, iDisplayIndex, iDisplayIndexFull) {
            responsiveHelper.createExpandIcon(nRow);
        },
        fnDrawCallback: function (oSettings) {
            responsiveHelper.respond();
        },
        "aoColumns": [
                {
                    "sClass": "details",
                    "mDataProp": "Detalle"
                },
                {
                    "mDataProp": "Tipo",
                    "bVisible": false
                },
                { "mDataProp": "Fecha" },
                { "mDataProp": "Pasajero" },
                { "mDataProp": "Boleto" },
                //{
                //    "mDataProp": "Codigo",
                //    "bVisible": false
                //},
                { "mDataProp": "Aerolinea" },
                { "mDataProp": "Moneda" },
               // { "mDataProp": "Precio" },
                {
                 "mDataProp": "Monto",
                 "sType": 'numeric',
                 "mRender": function (data) {
                     return $.formatNumber(data, { format: "#,###.00", locale: "es" });
                 }
                }//,
                //{ "mDataProp": "Total" }
                //{
                // "mDataProp": "Total",
                // "sType": 'numeric',
                // "mRender": function (data) {
                //     return $.formatNumber(data, { format: "#,###.00", locale: "es" });
                // }
                //},
        ]
    });

    //$(".first.paginate_button, .last.paginate_button").hide();
    var search_input = oTable.closest('.dataTables_wrapper').find('div[id$=_filter] input');
    search_input.attr('placeholder', "Buscar");

}

function HabilitarNumeroBanco() {
    var F = $("#FormaPago").val();
    if (F == 3) {
        $("#NumeroDocumento").attr("disabled", "disabled");
        $("#Banco").attr("disabled", "disabled");
    } else {
        $("#NumeroDocumento").removeAttr("disabled", "disabled");
        $("#Banco").removeAttr("disabled", "disabled");
    }
}

//var modalVenta;
//function VerAbonos() {

//    $("#Cliente").val('');

//    //$('#btn_cargarVenta').hide();
//    //$('#dvloaderVenta').show();

//    var var_venta = $('#Pedido').val();

//    $('#tb_abonos').dataTable().fnDestroy();
//    var giRedraw = false;
//    var responsiveHelper;
//    var breakpointDefinition = {
//        tablet: 1024,
//        phone: 480
//    };

//    tableElement = $('#tb_abonos')

//    tableElement.dataTable({
//        "bProcessing": true,
//        "bServerSide": false,
//        "sAjaxSource": "frm_recibos.aspx?fn=verabonos&id=" + var_venta,
//        "bAutoWidth": false,
//        "bFilter": false,
//        "bSort": false,
//        "bLengthChange": false,
//        "bPaginate": false,
//        "bInfo": false,
//        "oLanguage": {
//            "sInfo": "_TOTAL_ Registro(s)",
//            "sInfoFiltered": " - de _MAX_ registros",
//            "sInfoThousands": ",",
//            "sLengthMenu": "Mostrar _MENU_ Registros",
//            "sLoadingRecords": "<img src='img/loading2.gif' />",
//            "sProcessing": "",
//            "sSearch": "",
//            "sZeroRecords": "No se encontraron registros"
//        },
//        "sDom": 'frt<"izq"i><"der"p>',
//        "fnServerData": function (sSource, aoData, fnCallback) {
//            $.getJSON(sSource, aoData, function (json) {
//                //$('#btn_cargarVenta').show()
//                //$('#dvloaderVenta').hide()
//                try {
//                    if (json.error == '-1') {
//                        $("#dv_advertencia1").html("No se encontraron pedidos con el numero indicado");
//                        $("#dv_advertencia1").show();
//                        $("#Venta").focus();
//                        setTimeout(function () { $('#dv_advertencia1').hide(); }, 10000);
//                        return false;
//                    }

//                    if (json.error == '-2') {
//                        $("#dv_error1").html("Se ha producido un error en el sistema. Intente de nuevo. Si el problema persiste comuniquese con el administrador del sistema");
//                        $("#dv_error1").show();
//                        setTimeout(function () { $('#dv_error1').hide(); }, 10000);
//                        $("#Venta").focus();
//                        return false;
//                    }
//                    if (json.DT_RowId) {
//                        var id = ""

//                        try {
//                            id = json.DT_RowId;
//                        } catch (e) {
//                            $("#dv_error1").html("Error. " + e);
//                            $("#dv_error1").show();
//                            setTimeout(function () { $('#dv_error1').hide(); }, 10000);
//                            return false;
//                        }
//                        $("#Venta").val(id)
//                        //CargarVenta();
//                        return false;
//                    }

//                } catch (e) { }

//                modalabonos = $.remodal.lookup[$('[data-remodal-id=modalabonos]').data('remodal')];
//                modalabonos.open();
//                fnCallback(json);
//            });
//        },
//        fnPreDrawCallback: function () {
//            if (!responsiveHelper) {
//                responsiveHelper = new ResponsiveDatatablesHelper(tableElement, breakpointDefinition);
//            }
//        },
//        fnRowCallback: function (nRow, aData, iDisplayIndex, iDisplayIndexFull) {
//            responsiveHelper.createExpandIcon(nRow);
//        },
//        fnDrawCallback: function (oSettings) {
//            responsiveHelper.respond();
//        },
//        "aoColumns": [
//                { "mDataProp": "Recibo" },
//                { "mDataProp": "Cliente" },
//                { "mDataProp": "Rif" },
//                {
//                    "mDataProp": "Fecha",
//                    "stype": "date",
//                    "fnRender": function (oObj) {
//                        var d = new Date(oObj.aData.Fecha);
//                        var m = d.getMonth() + 1;
//                        var i = d.getDate();
//                        if (navigator.userAgent.toLowerCase().indexOf('chrome') > -1) {

//                            if (i == '31') {
//                                i = '1';
//                                m = m + 1;
//                            } else {
//                                i = d.getDate() + 1;
//                            }

//                        }
//                        d = ((i < 10 ? '0' : '') + i) + "/" + ((m < 10 ? '0' : '') + m) + "/" + d.getFullYear();
//                        return "<div class='date'>" + d + "<div>";
//                    }
//                },
//                { "mDataProp": "Monto" },
//                { "mDataProp": "Forma de Pago" },
//                { "mDataProp": "Observaciones" }
//        ]
//    });

//    //$(".first.paginate_button, .last.paginate_button").hide();
//    var search_input = tableElement.closest('.dataTables_wrapper').find('div[id$=_filter] input');
//    search_input.attr('placeholder', "Buscar");
//}