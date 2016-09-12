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
    CargarArrendadoras()
    //CargarHabitaciones()

    $('#FechaInicio').datepicker({ dateFormat: 'dd/mm/yy' })
    $('#FechaFin').datepicker({ dateFormat: 'dd/mm/yy' })
    $('#FechaVencimiento').datepicker({ dateFormat: 'dd/mm/yy' })
    
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
        CalcularTotal();
    });

    $("#Tax").blur(function () {
        var number = $(this).val();
        number = number.replace('.', '');
        number = number.replace(',', '.');
        number = $.formatNumber(number, { format: "#,##0.00", locale: "es" });
        $("#Tax").val(number);
        CalcularTotal();
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
                alert(response.TipoVenta);
                $('#TipoVenta').val(response.TipoVenta);
                $('#Vendedor').val(response.Vendedor);
                $('#Agencia').val(response.Agencia);
                $('#Freelance').val(response.Freelance);
                if (response.Agencia > 0) {
                    $('#TipoVendedor').val(1);
                    $("#div_agencia").removeClass('hide');
                } else if (response.Freelance > 0) {
                    $('#TipoVendedor').val(2);
                    $("#div_freelance").removeClass('hide');
                } else if (response.Freelance > 0) {
                    $('#TipoVendedor').val(3);
                    $("#div_vendedor").removeClass('hide');
                } else {
                    $('#TipoVendedor').val(0);
                }

                $('#hfAerolineas').val(response.Aerolinea);
                $('#hfPaquetes').val(response.Paquete);
                $('#hfVehiculos').val(response.Vehiculo);
                $('#hfHoteles').val(response.Hotel);
                $('#Habitacion').val(response.Habitacion);
                $('#Boleto').val(response.Boleto);
                $('#hfSistema').val(response.Sistema);
                $('#Pantalla').val(response.Pantalla);
                $('#Dias').val(response.Dias);
                $('#TipoPersona').val(response.TipoPersona);
                $('#hfArrendadora').val(response.Arrendadora);


                $('#hdn_id_pasajero').val(response.IdPasajero);
                $('#FechaInicio').val(response.FechaInicio);
                $('#FechaFin').val(response.FechaFin);
                $('#hfDestino').val(response.Destino);
                $('#hfMoneda').val(response.Moneda);
                $('#Tarifa').val(response.Tarifa);
                $('#Tax').val(response.Tax);
                $('#Total').val(response.Total);
                $('#hfTipoVenta').val(response.TipoVenta);

                if (response.hdn_id_venta == 0) {
                    $('#TipoViaje').val(-1);
                }
                if (response.IdPasajero != 0) {
                    CargarPasajero();
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
function CargarPasajero() {
    var id = $("#hdn_id_pasajero").val();

    $('.loading').show()
    $('.btn').hide();
    $.ajax({
        type: "POST",
        url: "frm_ventas.aspx?fn=cargar_pasajero",
        data: '{"id_pasajero":"' + id + '"}',
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
                $('#Tipo').val(response.Tipo);
                $('#Rif').val(response.Rif);
                $('#Pasaporte').val(response.Pasaporte);
                $('#FechaVencimiento').val(response.FechaVencimiento);
                $('#Nombre').val(response.Nombre);
                $('#Apellido').val(response.Apellido);
                $('#TelefonoM').val(response.Telefono);
                $('#Direccion').val(response.Direccion);
                $('#Email').val(response.Email);
                $('#Edad').val(response.Edad);
                $("#Rif").focus();
            }
            else {
                $("#dv_error").html(response.msj);
                $("#dv_error").show();
                setTimeout(function () { $('#dv_error').hide(); }, 10000);
                $("#Rif").focus();
            }
        },
        error: function () {
            $('.loading').hide()
            $('.btn').show();
            $("#dv_error").html('Error de comunicación con el servidor. Función Cargarpasajero().');
            $("#dv_error").show();
            setTimeout(function () { $('#dv_error').hide(); }, 10000);
            $("#Rif").focus();
        }
    });

}
function buscarPasajero() {
    var rif = $("#Rif").val();

    $('.loading').show()
    $('.btn').hide();
    $.ajax({
        type: "POST",
        url: "frm_ventas.aspx?fn=buscar_pasajero",
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
                $("#hdn_id_pasajero").val(response.id_pasajero);
                CargarPasajero();
                return false;
            }
            if (response.rslt == '') {
                $("#dv_Error").show();
                return false;
            }
            if (response.rslt == 'vacio') {
                var t = $("#TipoViaje").val();
                if(t==0){
                    $("#Nombre").focus();
                } else {
                    $("#Pasaporte").focus();
            }

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
            $("#dv_Error").html('Error de comunicación con el servidor. Funcion CargarPasajero().');
            $("#dv_Error").show();
            setTimeout(function () { $('#dv_Error').hide(); }, 10000);
            $("#rif").focus();
        }
    });

}
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
        record.Hotel = $('#Hoteles').val();
        record.Habitacion = $('#Habitacion').val();
        record.Aerolinea = $('#Aerolineas').val();
        record.Boleto = $('#Boleto').val();
        record.Sistema = $('#Sistema').val();
        record.Pantalla = $('#Pantalla').val();
        record.Dias = $('#Dias').val();
        record.TipoPersona = $('#TipoPersona').val();
        record.Vehiculos = $('#Vehiculos').val();
        record.Arrendadora = $('#Arrendadora').val();

        record.IdPasajero = $('#hdn_id_pasajero').val();
        record.Rif = $('#Rif').val();
        record.Pasaporte = $('#Pasaporte').val();
        record.PasaporteFecha = $('#FechaVencimiento').val();
        record.Nombre = $('#Nombre').val();
        record.Apellido = $('#Apellido').val();
        record.Direccion = $('#Direccion').val();
        record.Edad = $('#Edad').val();
        record.TelefonoM = $('#TelefonoM').val();
        record.Email = $('#Email').val();

        record.FechaInicio = $('#FechaInicio').val();
        record.FechaFin = $('#FechaFin').val();
        record.Destino = $('#Destino').val();
        record.Moneda = $('#Moneda').val();
        record.Tarifa = $('#Tarifa').val();
        record.Tax = $('#Tax').val();
        record.Total = $('#Total').val();
    
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

                    //$('#hdn_id_venta').val(response.id);
                    msjModal = $.remodal.lookup[$('[data-remodal-id=msjModal]').data('remodal')];
                    msjModal.open();
                    if (response.msj != '') {
                        $("#msj").html("El record ha sido guardado con errores: " + response.msj);
                    }
                    else {
                        $('#hdn_id_venta').val(response.id)
                        $("#msj").html("El record ha sido guardado con exito.");
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
                $("#dv_Error").html('Error de comunicación con el servidor. El record no ha sido actualizado.');
                $("#dv_Error").show();
                setTimeout(function () { $('#dv_Error').hide(); }, 10000);
            }
        });
    }

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
    
    function CargarViajesTipos() {
        var tipoviaje = $("#TipoViaje option:selected").text();
        if (tipoviaje == 'Nacional') {
            $(".pasaporte").addClass('hide');
        }
        else if (tipoviaje == 'Internacional') {
            $(".pasaporte").removeClass('hide');
        }        
    }

    function CargarTipoVenta() {
        var tipoventa = $("#TipoVenta option:selected").text();
        if (tipoventa == 'Boletos') {
            CargarAerolineas()
            $(".boleto").removeClass('hide');
            $(".persona").removeClass('hide');
            $(".hotel").addClass('hide');
            $(".paquete").addClass('hide');
            $(".vehiculo").addClass('hide');
        }
        else if (tipoventa == 'Hoteles') {
            CargarHoteles()
            $(".hotel").removeClass('hide');
            $(".persona").removeClass('hide');
            $(".boleto").addClass('hide');
            $(".paquete").addClass('hide');
            $(".vehiculo").addClass('hide');
        }
        else if (tipoventa == 'Paquetes') {
            CargarPaquetes()
            $(".paquete").removeClass('hide');
            $(".persona").removeClass('hide');
            $(".boleto").removeClass('hide');
            $(".hotel").removeClass('hide');
            $(".vehiculo").addClass('hide');
        }
        else if (tipoventa == 'Vehiculos') {
            CargarVehiculos()
            $(".vehiculo").removeClass('hide');
            $(".persona").addClass('hide');
            $(".boleto").addClass('hide');
            $(".hotel").addClass('hide');
            $(".paquete").addClass('hide');
        }
        else {
            $(".boleto").addClass('hide');
            $(".persona").addClass('hide');
            $(".hotel").addClass('hide');
            $(".paquete").addClass('hide');
            $(".vehiculo").addClass('hide');
        }
    }
    function CargarTipoVendedor() {
        var tipovendedor = $("#TipoVendedor option:selected").text();
        if (tipovendedor == 'Agencia') {
            $("#div_agencia").removeClass('hide');
            $("#div_vendedor").addClass('hide');
            $("#div_freelance").addClass('hide');
        }
        else if (tipovendedor == 'div_vendedor') {
            $("#div_vendedor").removeClass('hide');
            $("#div_agencia").addClass('hide');
            $("#div_freelance").addClass('hide');
        }
        else if (tipovendedor == 'Freelance') {
            $("#div_freelance").removeClass('hide');
            $("#div_vendedor").addClass('hide');
            $("#div_agencia").addClass('hide');
        }
        else {
            $("#div_freelance").addClass('hide');
            $("#div_agencia").addClass('hide');
            $("#div_vendedor").addClass('hide');
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
                $("#Aerolineas").val($("#hfAerolineas").val());
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
                $("#Hoteles").val($("#hfHoteles").val());
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
    function CargarArrendadoras() {
        $("#Arrendadora").empty();;
        $.ajax({
            type: "POST",
            url: "frm_ventas.aspx?fn=cargar_arrendadoras",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (response) {
                $.each(response.aaData, function (i, item) {
                    if (item.id == 0) {
                        $("#Arrendadora").append("<option value=" + item.id + " selected='selected'>" + item.des + "</option>");
                    }
                    else {
                        $("#Arrendadora").append("<option value=" + item.id + ">" + item.des + "</option>");
                    }
                });
                $("#Arrendadora").val($("#hfArrendadora").val());
            },
            error: function () {
                $("#dv_mensaje").hide();
                $("#dv_Error").html('Error de comunicación con el servidor al intentar cargar la funcion Vehiculos.');
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
                $("#Vehiculos").val($("#hfVehiculos").val());
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

    function CalcularTotal() {
        var tarifa = $("#Tarifa").val();
        tarifa = $.parseNumber(tarifa, { format: "#,###.00", locale: "es" });

        var tax = $("#Tax").val();
        tax = $.parseNumber(tax, { format: "#,###.00", locale: "es" });
        if (!isNaN(tarifa) && !isNaN(tax)) {
            var total = parseFloat(tarifa) + parseFloat(tax)
            total = $.formatNumber(total, { format: "#,###.00", locale: "es" });
            $("#Total").val(total);
        }
        else {
            $("#Total").val(0);
        }
    }