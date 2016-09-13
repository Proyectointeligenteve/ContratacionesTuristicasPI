function Cargar() {
    Permisos()
    cargar_destinos()
    CargarListado()
    EventosListado()
    $('#FechaInicio').datepicker({ dateFormat: 'dd/mm/yy' })
    var idt = $.url().param('id');

    $('#FechaFin').datepicker({ dateFormat: 'dd/mm/yy' })
    var idt = $.url().param('id');

    $("#Precio").blur(function () {
        var number = $(this).val();
        number = number.replace('.', '');
        number = number.replace(',', '.');
        number = $.formatNumber(number, { format: "#,##0.00", locale: "es" });
        $("#Precio").val(number);
    });
}

function Permisos() {
    $.ajax({
        type: "POST",
        url: "lst_paquetes.aspx?fn=permisos",
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
                if (response.Agregar == 1) { $("#btn_agregar").removeClass('hide'); }
                if (response.Editar == 1) { $("#btn_editar").removeClass('hide'); }
                if (response.Anular == 1) { $("#btn_anular").removeClass('hide'); }
                if (response.Eliminar == 1) { $("#btn_eliminar").removeClass('hide'); }
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

function CargarListado() {
    $('#tbDetails').dataTable().fnDestroy();
    var giRedraw = false;
    var responsiveHelper;
    var breakpointDefinition = {
        tablet: 1024,
        phone: 480
    };

    var v = $("#vista_estatus").val();
    tableElement = $('#tbDetails')
    tableElement.dataTable({
        "bProcessing": true,
        "bServerSide": false,
        "sAjaxSource": "lst_paquetes.aspx?fn=cargar&v=" + v,
        "sPaginationType": "full_numbers",
        "bAutoWidth": false,
        "bLengthChange": false,
        "oLanguage": {
            "sInfo": "<span style='color:#fff'>_TOTAL_ Registro(s)</pan>",
            "sInfoFiltered": " - de _MAX_ registros",
            "sInfoThousands": ",",
            "sLengthMenu": "Mostrar _MENU_ Registros",
            "sLoadingRecords": "<img src='img/loading2.gif' />",
            "sProcessing": "",
            "sSearch": "",
            "sZeroRecords": "No se encontraron registros",
            "oPaginate": {
                "sNext": " > ",
                "sPrevious": " < ",
                "sFirst": " << ",
                "sLast": " >> "
            }
        },
        "sDom": 'frt<"izq"i><"der"p>',
        fnPreDrawCallback: function () {
            // Initialize the responsive datatables helper once.
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
        "fnInit": function (oSettings, nPaging, fnDraw) {
            var oLang = oSettings.oLanguage.oPaginate;
            var fnClickHandler = function (e) {
                e.preventDefault();
                if (oSettings.oApi._fnPageChange(oSettings, e.data.action)) { fnDraw(oSettings); }
            };

            $(nPaging).addClass('pagination').append("<ul></ul>");
            $('<li class="prev disabled"><a href="#">&laquo;</a></li>').appendTo($("ul", nPaging)).find("a").bind('click.DT', { action: "previous" }, fnClickHandler);
            $('<li class="next disabled"><a href="#">&raquo;</a></li>').appendTo($("ul", nPaging)).find("a").bind('click.DT', { action: "next" }, fnClickHandler);
        },
        "aoColumns": [
            { "mDataProp": "Nombre" },
	    { "mDataProp": "Destino" },
        {
            "mDataProp": "FechaInicio",
            "stype": "date",
            "fnRender": function (oObj) {
                var d = new Date(oObj.aData.FechaInicio);
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
                return d;
            }
        },
	    //{ "mDataProp": "FechaInicio" },
        {
            "mDataProp": "FechaFin",
            "stype": "date",
            "fnRender": function (oObj) {
                var d = new Date(oObj.aData.FechaFin);
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
                return d;
            }
        },
	    //{ "mDataProp": "FechaFin" },
	    { "mDataProp": "Tipo" },

             {
                 "mDataProp": "Precio",
                 "sType": 'numeric',
                 "mRender": function (data) {
                     return $.formatNumber(data, { format: "#,###.00", locale: "es" });
                 }
             },
	    //{ "mDataProp": "Precio" },
	    { "mDataProp": "Estatus" }
        ]
    });

    $(".first.paginate_button, .last.paginate_button ").hide();
    var $ul = $("<ul>");
    $("#tbDetails_paginate").children().each(function () {
        var $li = $("<li>").append($(this));
        $ul.append($li);
    });
    $("#tbDetails_paginate").append($ul);
    $("#tbDetails_paginate ul").addClass('pagination');

    $(".first.paginate_button, .last.paginate_button").hide();
    var search_input = tableElement.closest('.dataTables_wrapper').find('div[id$=_filter] input');
    search_input.attr('placeholder', "Buscar");
}

function EventosListado() {
    $("#tbDetails tbody").click(function (event) {

        $(tableElement.fnSettings().aoData).each(function () {
            $(this.nTr).removeClass('row_selected');
        });
        $(event.target.parentNode).addClass('row_selected');

    });

}
function Ver() {
    var id = '';
    $('#tbDetails tr').each(function () {
        if ($(this).hasClass('row_selected')) {
            id = this.id;
        }
    });

    if (id == '') {
        $("#dv_error").html('Seleccione un registro');
        $("#dv_error").show();
        setTimeout(function () { $('#dv_error').hide(); }, 3000);
        return false;
    }

    $('.loading').show()
    $('.btn').hide();
    //$("#tbDetails").empty();
    modal = $.remodal.lookup[$('[data-remodal-id=modal]').data('remodal')];
    modal.open();
}

function Validar() {
    var valido = $("#form1").valid();
    return valido;
}

function Nuevo() {
    $('#id').val(0);
    $('#Nombre').val('');
    $('#FechaInicio').val('');
    $('#FechaFin').val('');
    $('#Destino').val(0);
    $('#Tipo').val(-1);
    $('#Grupo').val(-1);
    $('#Precio').val(0);
    $('#Activo').val(-1);
    //$('#Numero').val('');
    $.ajax({
        success: function (response) {
            modal = $.remodal.lookup[$('[data-remodal-id=modal]').data('remodal')];
            modal.open();
        },
        error: function () {
            modal.close();
            $("#dv_mensaje").hide();
            $("#dv_error").html('Error de comunicación con el servidor. El registro no ha sido actualizado.');
            $("#dv_error").show();
        }
    });
}

function Editar() {
    var id
    $('#tbDetails tr').each(function () {
        if ($(this).hasClass('row_selected')) {
            id = this.id;
            $("#id").val(id);
        }
    });
    $('.decimal').keypress(function (evt) {
        if (evt.which == 46) {
            $(this).val($(this).val() + ',');
            evt.preventDefault();
        }
    });
    $.ajax({
        type: "POST",
        url: "lst_paquetes.aspx?fn=editar",
        data: '{"id":"' + id + '"}',
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            if (response.rslt == 'exito') {
                $('#Nombre').val(response.Nombre);
                $('#FechaInicio').val(response.FechaInicio);
                $('#FechaFin').val(response.FechaFin);
                $('#hfDestino').val(response.Destino);
                $('#Precio').val(response.Precio);
                $('#Tipo').val(response.Tipo);
                $('#Grupo').val(response.Grupo);
                $('#Activo').val(response.Activo);
                cargar_destinos()
                modal = $.remodal.lookup[$('[data-remodal-id=modal]').data('remodal')];
                modal.open();
            }
            else {
                $("#dv_error").html(response.msj);
                $("#dv_error").show();
                setTimeout(function () { $('#dv_error').hide(); }, 10000);
            }
        },
        error: function () {
            modal.close();
            $("#dv_error").html('Error de comunicación con el servidor. El registro no ha sido actualizado.');
            $("#dv_error").show();
            setTimeout(function () { $('#dv_error').hide(); }, 10000);
        }
    });

}

function Guardar() {
    var registro = {};
    registro.id = $('#id').val();
    registro.Nombre = $('#Nombre').val();
    registro.FechaInicio = $('#FechaInicio').val();
    registro.FechaFin = $('#FechaFin').val();
    registro.Destino = $('#Destino').val();
    registro.Tipo = $('#Tipo').val();
    registro.Grupo = $('#Grupo').val();
    registro.Precio = $('#Precio').val();
    registro.Activo = $('#Activo').val();
    
    $.ajax({
        type: "POST",
        url: "lst_paquetes.aspx?fn=guardar",
        data: JSON.stringify(registro),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            
            modal.close();
            if (response.rslt == 'exito') {
                $("#dv_error").hide()
                $("#dv_mensaje").html('El registro ha sido procesado con exito.');
                $("#dv_mensaje").show();
                setTimeout(function () { $('#dv_mensaje').hide(); }, 10000);
            }
            else {
                $("#dv_error").html(response.msj);
                $("#dv_error").show();
                setTimeout(function () { $('#dv_error').hide(); }, 10000);
            }
            $('#tbDetails').dataTable().fnDestroy();
            CargarListado();
        },
        error: function () {
            modal.close();
            $("#dv_error").html('Error de comunicación con el servidor. El registro no ha sido actualizado.');
            $("#dv_error").show();
            setTimeout(function () { $('#dv_error').hide(); }, 10000);
        }
    });
}

function ConfirmarAnular() {
    var id = '';
    $('#tbDetails tr').each(function () {
        if ($(this).hasClass('row_selected')) {
            id = this.id;
            $("#id").val(id);
        }
    });

    if (id == '') {
        $("#dv_error").html('Seleccione un registro');
        $("#dv_error").show();
        setTimeout(function () { $('#dv_error').hide(); }, 10000);
        return false;
    }


    anularmodal = $.remodal.lookup[$('[data-remodal-id=anularmodal]').data('remodal')];
    anularmodal.open();
}
function ConfirmarEliminar() {
    var id = '';
    $('#tbDetails tr').each(function () {
        if ($(this).hasClass('row_selected')) {
            id = this.id;
            $("#id").val(id);
        }
    });

    if (id == '') {
        $("#dv_error").html('Seleccione un registro');
        $("#dv_error").show();
        setTimeout(function () { $('#dv_error').hide(); }, 10000);
        return false;
    }


    deletemodal = $.remodal.lookup[$('[data-remodal-id=deletemodal]').data('remodal')];
    deletemodal.open();
}

function Eliminar() {
    var id
    $('#tbDetails tr').each(function () {
        if ($(this).hasClass('row_selected')) {
            id = this.id;
            $("#id").val(id);
        }
    });

    $.ajax({
        type: "POST",
        url: "lst_paquetes.aspx?fn=eliminar",
        data: '{"id":"' + id + '"}',
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            deletemodal.close();
            if (response.rslt == 'exito') {
                $("#dv_error").hide()
                $("#dv_mensaje").html('El registro ha sido eliminado.');
                $("#dv_mensaje").show();
                setTimeout(function () { $('#dv_mensaje').hide(); }, 10000);
                $('#tbDetails').dataTable().fnDestroy();
                CargarListado();
            }
            else {
                $("#dv_mensaje").hide()
                $("#dv_error").html(response.msj);
                $("#dv_error").show();
                setTimeout(function () { $('#dv_error').hide(); }, 10000);
            }
        },
        error: function () {
            deletemodal.close();
            $("#dv_mensaje").hide();
            $("#dv_error").html('Error de comunicación con el servidor. El registro no ha sido actualizado.');
            $("#dv_error").show();
            setTimeout(function () { $('#dv_error').hide(); }, 10000);
        }
    });

}
function Anular() {
    var id
    $('#tbDetails tr').each(function () {
        if ($(this).hasClass('row_selected')) {
            id = this.id;
            $("#id").val(id);
        }
    });

    $('.loading').show()
    $('.btn').hide();
    $.ajax({
        type: "POST",
        url: "lst_paquetes.aspx?fn=Anular",
        data: '{"id":"' + id + '"}',
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            anularmodal.close();
            $('.loading').hide()
            $('.btn').show();

            if (response.error == '-1') {
                window.location.href = 'info.aspx';
                return false;
            }

            if (response.rslt == 'exito') {
                $("#dv_error").hide()
                $("#dv_mensaje").html('El registro ha sido activado/inactivado.');
                $("#dv_mensaje").show();
                setTimeout(function () { $('#dv_mensaje').hide(); }, 10000);
                $('#tbDetails').dataTable().fnDestroy();
                CargarListado();
            }
            else {
                $("#dv_error").html(response.msj);
                $("#dv_error").show();
                setTimeout(function () { $('#dv_error').hide(); }, 10000);
            }
        },
        error: function () {
            anularmodal.close();
            $('.loading').hide()
            $('.btn').show();
            $("#dv_error").html('Error de comunicación con el servidor. Función Anular().');
            $("#dv_error").show();
            setTimeout(function () { $('#dv_error').hide(); }, 10000);
        }
    });

}

function cargar_destinos() {
    $.ajax({
        type: "POST",
        url: "lst_paquetes.aspx?fn=cargar_destinos",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            $.each(response.aaData, function (i, item) {
                if (item.principal == 1) {
                    $("#Destino").append("<option value=" + item.des + ">" + item.des + "</option>");
                }
                else {
                    $("#Destino").append("<option value=" + item.id + ">" + item.des + "</option>");
                }
            });
            $("#Destino").val($("#hfDestino").val());
        },
        error: function () {
            $("#dv_error").html('Error de comunicación con el servidor. Función cargar_Destino().');
            $("#dv_error").show();
            setTimeout(function () { $('#dv_error').hide(); }, 10000);
        }
    });
}