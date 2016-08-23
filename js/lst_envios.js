function Cargar() {
    Permisos();
    CargarListados();
    EventosListado();
}

function Permisos() {
    $.ajax({
        type: "POST",
        url: "lst_envios.aspx?fn=permisos",
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
                if (response.Ver == 1) { $("#btn_ver").removeClass('hide'); }
                if (response.Editar == 1) { $("#btn_editar").removeClass('hide'); }
                //if (response.Imprimir == 1) { $("#btn_imprimir").removeClass('hide'); }
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

function EventosListado() {

    $("#tbDetails tbody").click(function (event) {

        $(tableElement.fnSettings().aoData).each(function () {
            $(this.nTr).removeClass('row_selected');
        });
        $(event.target.parentNode).addClass('row_selected');

    });

    $("#tbDetails tbody").dblclick(function (event) {

        $(tableElement.fnSettings().aoData).each(function () {
            $(this.nTr).removeClass('row_selected');
        });
        $(event.target.parentNode).addClass('row_selected');
        if ($("#btn_editar").hasClass('hide')) {
            Ver()
        }
        else {
            Editar()
        }
    });

}

var oTable;
function CargarListados() {
    //var oTable;
    $('#tbDetails').dataTable().fnDestroy();
    var giRedraw = false;
    var responsiveHelper;
    var breakpointDefinition = {
        tablet: 1024,
        phone: 480
    };


    //oTable = $('#tbDetails').dataTable({
        var v = $("#vista_estatus").val();
        tableElement = $('#tbDetails')
        tableElement.dataTable({
        "bProcessing": true,
        "bServerSide": true,
        "sAjaxSource": "lst_envios.aspx?fn=cargar&v=" + v,
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
        "sDom": 'frt<"izq"i><"der"p>',
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
            //informacion de la funcion de envios de la base de datos
        "aoColumns": [
            { "mDataProp": "Codigo" },
            { "mDataProp": "Fecha" },
            { "mDataProp": "Emisor" },
            { "mDataProp": "Receptor" },
            { "mDataProp": "" },
            { "mDataProp": "" },
            { "mDataProp": "Costo" },
            { "mDataProp": "Estatus" }
        ]
        });

        $(".first.paginate_button, .last.paginate_button").hide();
        var search_input = tableElement.closest('.dataTables_wrapper').find('div[id$=_filter] input');
        search_input.attr('placeholder', "Buscar");
}

function Nuevo() {
    $('.loading').show()
    $('.btn').hide();
    window.location.href = 'frm_envios.aspx';
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
        setTimeout(function () { $('#dv_error').hide(); }, 10000);
        return false;
    }

    $('.loading').show()
    $('.btn').hide();
    window.location.href = 'frm_envios.aspx?id=' + id + '&v=1';
}

function Editar() {
    var id = '';
    $('#tbDetails tr').each(function () {
        if ($(this).hasClass('row_selected')) {
            id = this.id;
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
    window.location.href = 'frm_envios.aspx?id=' + id;
}

//function Imprimir() {
//    var id
//    $('#tbDetails tr').each(function () {
//        if ($(this).hasClass('row_selected')) {
//            id = this.id;
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
//    window.location.href = 'rpt_envios.aspx?var_id=' + id;
//}


function Confirmar() {
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

    //$('#deletemodal').modal(options);
    //$('#deletemodal').on('shown.bs.modal', function () {

    //})
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
        url: "lst_envios.aspx?fn=eliminar",
        data: '{"id":"' + id + '"}',
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            //$('#deletemodal').modal('hide');
            deletemodal.close();
            if (response.rslt == 'exito') {
                $("#dv_error").hide()
                $("#dv_mensaje").html('El registro ha sido eliminado.');
                $("#dv_mensaje").show();
                setTimeout(function () { $('#dv_mensaje').hide(); }, 10000);
                $('#tbDetails').dataTable().fnDestroy();
                CargarListados();
            }
            else {
                $("#dv_mensaje").hide()
                $("#dv_error").html(response.msj);
                $("#dv_error").show();
                setTimeout(function () { $('#dv_error').hide(); }, 10000);
            }
        },
        error: function () {
            //$('#deletemodal').modal('hide');
            deletemodal.close();
            $("#dv_mensaje").hide();
            $("#dv_error").html('Error de comunicación con el servidor. Función Eliminar().');
            $("#dv_error").show();
            setTimeout(function () { $('#dv_error').hide(); }, 10000);
        }
    });

}