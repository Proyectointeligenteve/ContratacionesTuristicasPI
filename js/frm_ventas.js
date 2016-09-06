 function backToList() {
    var var_Id = $('#hdn_id_venta').val();
    window.location.href = 'lst_ventas.aspx';
};
function load() {
    Permisos()

    $("#Sucursal").focus();
    CargarSucursales()
    CargarTiposVentas()
    CargarVendedores()
    CargarAgencias()
    CargarFreelances()
    CargarHoteles()
    CargarDestinos()
    CargarAerolineas()
    CargarVehiculos()
    CargarPaquetes()
    CargarSistemas()
    CargarMonedas()
    //CargarHabitaciones()

    
    $("#dv_Message").hide();
    $("#dv_Error").hide();
    //$("#dv_Error_modal").hide();
    //$("#dv_Error_modal").hide();

    $("#Tarifa").blur(function () {
        var number = $(this).val();
        number = number.replace('.', '');
        number = number.replace(',', '.');
        number = $.formatNumber(number, { format: "#,##0.00", locale: "es" });
        $("#Tarifa").val(number);
    });

    $("#Tax").blur(function () {
        var number = $(this).val();
        number = number.replace('.', '');
        number = number.replace(',', '.');
        number = $.formatNumber(number, { format: "#,##0.00", locale: "es" });
        $("#Tax").val(number);
    });

    var recordId = $.url().param('id');
    var idt1 = $.url().param('id');

    $('.loading').show()
    $('.btn').hide();
    $.ajax({
        type: "POST",
        url: "frm_ventas.aspx?fn=loadAll",
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
                $('#Numero').val(response.Numero);
                $('#hfSucursal').val(response.Sucursal);
                $('#TipoViaje').val(response.TipoViaje);
                $('#TipoVenta').val(response.TipoVenta);
                $('#Vendedor').val(response.Vendedor);
                $('#Agencia').val(response.Agencia);
                $('#Freelance').val(response.Freelance);

                $('#hfAerolinea').val(response.Aerolinea);
                $('#hfPaquete').val(response.Paquete);
                $('#hfVehiculo').val(response.Vehiculo);
                $('#hfHotel').val(response.Hotel);
                $('#Habitacion').val(response.Habitacion);
                $('#hfAerolinea').val(response.Aerolinea);
                $('#Boleto').val(response.Boleto);
                $('#hfSistema').val(response.Sistema);
                $('#Pantalla').val(response.Pantalla);
                $('#Dias').val(response.Dias);
                $('#TipoPersona').val(response.TipoPersona);
                $('#hdn_id_pasajero').val(response.IdPasajero);
                $('#FechaInicio').val(response.FechaInicio);
                $('#FechaFin').val(response.FechaFin);
                $('#hfDestino').val(response.Destino);
                $('#hfMoneda').val(response.Moneda);
                $('#Tarifa').val(response.Tarifa);
                $('#Tax').val(response.Tax);
                $('#Total').val(response.Total);
                //CargarPaises()
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
        url: "frm_ventas.aspx?fn=permisos&v=" + v,
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
        record.hdn_id_venta = $('#hdn_id_venta').val();
        record.Numero = $('#Numero').val();
        record.Sucursal = $('#Sucursal').val();
        record.TipoViaje = $('#TipoViaje').val();
        record.TipoVenta = $('#TipoVenta').val();
        record.TipoVendedor = $('#TipoVendedor').val();
        record.Vendedor = $('#Vendedor').val();
        record.Agencia = $('#Agencia').val();
        record.Freelance = $('#Freelance').val();
        record.Hotel = $('#Hotel').val();
        record.Habitacion = $('#Habitacion').val();
        record.Aerolinea = $('#Aerolinea').val();
        record.Boleto = $('#Boleto').val();
        record.Sistema = $('#Sistema').val();
        record.Pantalla = $('#Pantalla').val();
        record.Dias = $('#Dias').val();
        record.TipoPersona = $('#TipoPersona').val();

        record.Rif = $('#Rif').val();
        record.Nombre = $('#Nombre').val();
        record.RazonSocial = $('#RazonSocial').val();
        record.Direccion = $('#Direccion').val();
        record.Edad = $('#Edad').val();
        record.TelefonoM = $('#TelefonoM').val();
        record.Email = $('#Email').val();

        record.FechaInicio = $('#FechaInicio').val();
        record.FechaFin = $('#FechaFin').val();

        $('.loading').show()
        $('.btn').hide();
        $.ajax({
            type: "POST",
            url: "frm_ventas.aspx?fn=saveAll",
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

                    $('#hdn_id_contacto').val(response.hdn_id_contacto);
                    msjModal = $.remodal.lookup[$('[data-remodal-id=msjModal]').data('remodal')];
                    msjModal.open();
                    //$('#msjModal').modal(options);
                    //$('#msjModal').on('shown.bs.modal', function () {
                    if (response.msj != '') {
                        $("#msj").html("El record ha sido guardado con errores: " + response.msj);
                    }
                    else {
                        $('#hdn_id_contacto').val(response.id)
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

    function CargarSucursales() {
        $("#Sucursal").empty();
        $.ajax({
            type: "POST",
            url: "frm_ventas.aspx?fn=cargar_sucursales",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (response) {
                $.each(response.aaData, function (i, item) {
                    if (item.id == 0) {
                        $("#Sucursal").append("<option value=" + item.id + " selected='selected'>" + item.des + "</option>");
                    }
                    else {
                        $("#Sucursal").append("<option value=" + item.id + ">" + item.des + "</option>");
                    }
                });
                $("#Sucursal").val($("#hfSucursal").val());
            },
            error: function () {
                $("#dv_mensaje").hide();
                $("#dv_Error").html('Error de comunicación con el servidor al intentar cargar la funcion Sucursal.');
                $("#dv_Error").show();
                setTimeout(function () { $('#dv_Error').hide(); }, 10000);
            }
        });
    }
    function CargarTiposVentas() {
        $("#TipoVenta").empty();
        $.ajax({
            type: "POST",
            url: "frm_ventas.aspx?fn=cargar_tiposventas",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (response) {
                $.each(response.aaData, function (i, item) {
                    if (item.id == 0) {
                        $("#TipoVenta").append("<option value=" + item.id + " selected='selected'>" + item.des + "</option>");
                    }
                    else {
                        $("#TipoVenta").append("<option value=" + item.id + ">" + item.des + "</option>");
                    }
                });
                $("#TipoVenta").val($("#hfTipoVenta").val());
            },
            error: function () {
                $("#dv_mensaje").hide();
                $("#dv_Error").html('Error de comunicación con el servidor al intentar cargar la funcion Tipos Ventas.');
                $("#dv_Error").show();
                setTimeout(function () { $('#dv_Error').hide(); }, 10000);
            }
        });
    }
    function CargarVendedores() {
        $("#Vendedor").empty();
        $.ajax({
            type: "POST",
            url: "frm_ventas.aspx?fn=cargar_vendedores",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (response) {
                $.each(response.aaData, function (i, item) {
                    if (item.id == 0) {
                        $("#Vendedor").append("<option value=" + item.id + " selected='selected'>" + item.des + "</option>");
                    }
                    else {
                        $("#Vendedor").append("<option value=" + item.id + ">" + item.des + "</option>");
                    }
                });
                $("#Vendedor").val($("#hfVendedor").val());
            },
            error: function () {
                $("#dv_mensaje").hide();
                $("#dv_Error").html('Error de comunicación con el servidor al intentar cargar la funcion Vendedores.');
                $("#dv_Error").show();
                setTimeout(function () { $('#dv_Error').hide(); }, 10000);
            }
        });
    }
    function CargarAgencias() {
        $("#Agencia").empty();
        $.ajax({
            type: "POST",
            url: "frm_ventas.aspx?fn=cargar_agencias",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (response) {
                $.each(response.aaData, function (i, item) {
                    if (item.id == 0) {
                        $("#Agencia").append("<option value=" + item.id + " selected='selected'>" + item.des + "</option>");
                    }
                    else {
                        $("#Agencia").append("<option value=" + item.id + ">" + item.des + "</option>");
                    }
                });
                $("#Agencia").val($("#hfAgencia").val());
            },
            error: function () {
                $("#dv_mensaje").hide();
                $("#dv_Error").html('Error de comunicación con el servidor al intentar cargar la funcion Agencias.');
                $("#dv_Error").show();
                setTimeout(function () { $('#dv_Error').hide(); }, 10000);
            }
        });
    }
    function CargarFreelances() {
        $("#Freelance").empty();
        $.ajax({
            type: "POST",
            url: "frm_ventas.aspx?fn=cargar_freelances",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (response) {
                $.each(response.aaData, function (i, item) {
                    if (item.id == 0) {
                        $("#Freelance").append("<option value=" + item.id + " selected='selected'>" + item.des + "</option>");
                    }
                    else {
                        $("#Freelance").append("<option value=" + item.id + ">" + item.des + "</option>");
                    }
                });
                $("#Freelance").val($("#hfFreelance").val());
            },
            error: function () {
                $("#dv_mensaje").hide();
                $("#dv_Error").html('Error de comunicación con el servidor al intentar cargar la funcion Freelances.');
                $("#dv_Error").show();
                setTimeout(function () { $('#dv_Error').hide(); }, 10000);
            }
        });
    }

    function CargarSistemas() {
        $("#Sistema").empty();
        $.ajax({
            type: "POST",
            url: "frm_ventas.aspx?fn=cargar_sistemas",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (response) {
                $.each(response.aaData, function (i, item) {
                    if (item.id == 0) {
                        $("#Sistema").append("<option value=" + item.id + " selected='selected'>" + item.des + "</option>");
                    }
                    else {
                        $("#Sistema").append("<option value=" + item.id + ">" + item.des + "</option>");
                    }
                });
                $("#Sistema").val($("#hfSistema").val());
            },
            error: function () {
                $("#dv_mensaje").hide();
                $("#dv_Error").html('Error de comunicación con el servidor al intentar cargar la funcion Sistemas.');
                $("#dv_Error").show();
                setTimeout(function () { $('#dv_Error').hide(); }, 10000);
            }
        });
    }
    function CargarTipos() {
        $("#Tipo").empty();;
        $.ajax({
            type: "POST",
            url: "frm_ventas.aspx?fn=cargar_tipos",
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
    function CargarMonedas() {
        $("#Moneda").empty();
        $.ajax({
            type: "POST",
            url: "frm_ventas.aspx?fn=cargar_monedas",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (response) {
                $.each(response.aaData, function (i, item) {
                    if (item.id == 0) {
                        $("#Moneda").append("<option value=" + item.id + " selected='selected'>" + item.des + "</option>");
                    }
                    else {
                        $("#Moneda").append("<option value=" + item.id + ">" + item.des + "</option>");
                    }
                });
                $("#Moneda").val($("#hfMoneda").val());
            },
            error: function () {
                $("#dv_mensaje").hide();
                $("#dv_Error").html('Error de comunicación con el servidor al intentar cargar la funcion Monedas.');
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
            url: "frm_ventas.aspx?fn=cargar_aerolineas",
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
            url: "frm_ventas.aspx?fn=cargar_hoteles",
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
    function CargarDestinos() {
        $("#Destino").empty();;
        $.ajax({
            type: "POST",
            url: "frm_ventas.aspx?fn=cargar_destinos",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (response) {
                $.each(response.aaData, function (i, item) {
                    if (item.id == 0) {
                        $("#Destino").append("<option value=" + item.id + " selected='selected'>" + item.des + "</option>");
                    }
                    else {
                        $("#Destino").append("<option value=" + item.id + ">" + item.des + "</option>");
                    }
                });
                $("#Destino").val($("#hfDestino").val());
            },
            error: function () {
                $("#dv_mensaje").hide();
                $("#dv_Error").html('Error de comunicación con el servidor al intentar cargar la funcion Destinos.');
                $("#dv_Error").show();
                setTimeout(function () { $('#dv_Error').hide(); }, 10000);
            }
        });
    }
    function CargarVehiculos() {
        $("#Vehiculos").empty();;
        $.ajax({
            type: "POST",
            url: "frm_ventas.aspx?fn=cargar_vehiculos",
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
    function CargarPaquetes() {
        $("#Paquetes").empty();;
        $.ajax({
            type: "POST",
            url: "frm_ventas.aspx?fn=cargar_paquetes",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (response) {
                $.each(response.aaData, function (i, item) {
                    if (item.id == 0) {
                        $("#Paquetes").append("<option value=" + item.id + " selected='selected'>" + item.des + "</option>");
                    }
                    else {
                        $("#Paquetes").append("<option value=" + item.id + ">" + item.des + "</option>");
                    }
                });
                $("#Paquetes").val($("#hfPaquetes").val());
            },
            error: function () {
                $("#dv_mensaje").hide();
                $("#dv_Error").html('Error de comunicación con el servidor al intentar cargar la funcion Paquetes.');
                $("#dv_Error").show();
                setTimeout(function () { $('#dv_Error').hide(); }, 10000);
            }
        });
    }