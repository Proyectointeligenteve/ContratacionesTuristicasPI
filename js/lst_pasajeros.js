﻿function Cargar() {
    Permisos()
    CargarListado()
    EventosListado()
}

function Permisos() {
    $.ajax({
        type: "POST",
        url: "lst_pasajeros.aspx?fn=permisos",
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
                //if (response.Eliminar == 1) { $("#btn_eliminar").removeClass('hide'); }
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

    //var v = $("#vista_estatus").val();
    tableElement = $('#tbDetails')
    tableElement.dataTable({
        "bProcessing": true,
        "bServerSide": false,
        "sAjaxSource": "lst_pasajeros.aspx?fn=cargar",
        "sPaginationType": "full_numbers",
        "bAutoWidth": false,
        "bLengthChange": false,
        "oLanguage": {
            "sInfo": "<span style='color:#fff'>_TOTAL_ Registro(s)</pan>",
            "sInfoFiltered": " - de _MAX_ registros",
            "sInfoThousands": ",",
            "sLengthMenu": "Mostrar _MENU_ Registros",
            "sLoadingRecords": "<img src='img/loading2.gif' />",
            "sProcessing": "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;PROCESANDO...",
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
	        { "mDataProp": "Apellido" },
	        { "mDataProp": "Rif" }
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

function Validar() {
    var valido = $("#form1").valid();
    return valido;
}

function Nuevo() {
    $('#id').val(0);
    $('#Nombre').val('');
    $('#Apellido').val('');
    $('#Rif').val('');
    //$('#Numero').val('');
    $.ajax({
        success: function (response) {
            modal = $.remodal.lookup[$('[data-remodal-id=modal]').data('remodal')];
            modal.open();
        },
        error: function () {
            $('#basicModal').hide();
            $("#dv_mensaje").hide();
            $("#dv_error").html('Error de comunicación con el servidor. El registro no ha sido actualizado.');
            $("#dv_error").show();
            $('#basicModal').modal('hide');
        }
    });
}

function Editar() {
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

    $('.loading').show()
    $('.btn').hide();
    window.location.href = 'frm_pasajeros.aspx?id=' + id;

    $.ajax({
        type: "POST",
        url: "lst_pasajeros.aspx?fn=editar",
        data: '{"id":"' + id + '"}',
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            $('.loading').hide()
            $('.btn').show();
            if (response.rslt == 'exito') {
                $('#Nombre').val(response.Nombre);
                $('#Apellido').val(response.Apellido);
                $('#Rif').val(response.Rif);
                //$('#Numero').val(response.Numero);
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
            $('#basicModal').hide();
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
    registro.Apellido = $('#Apellido').val();
    registro.Rif = $('#Rif').val();
    //registro.Numero = $('#Numero').val();
    $.ajax({
        type: "POST",
        url: "lst_pasajeros.aspx?fn=guardar",
        data: JSON.stringify(registro),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            //$('#basicModal').modal('hide');
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
        url: "lst_pasajeros.aspx?fn=eliminar",
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
        url: "lst_pasajeros.aspx?fn=Anular",
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
