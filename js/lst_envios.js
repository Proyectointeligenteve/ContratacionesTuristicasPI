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
        "bServerSide": false,
        "sAjaxSource": "lst_envios.aspx?fn=cargar&v=" + v,
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
            if (!responsiveHelper) {
                responsiveHelper = new ResponsiveDatatablesHelper(tableElement, breakpointDefinition);
            }
        },
        fnRowCallback: function (nRow, aData, iDisplayIndex, iDisplayIndexFull) {
            responsiveHelper.createExpandIcon(nRow);
        },
        fnDrawCallback: function (oSettings) {
            responsiveHelper.respond();
        }, "fnInit": function (oSettings, nPaging, fnDraw) {
            var oLang = oSettings.oLanguage.oPaginate;
            var fnClickHandler = function (e) {
                e.preventDefault();
                if (oSettings.oApi._fnPageChange(oSettings, e.data.action)) { fnDraw(oSettings); }
            };

            $(nPaging).addClass('pagination').append("<ul></ul>");
            $('<li class="prev disabled"><a href="#">&laquo;</a></li>').appendTo($("ul", nPaging)).find("a").bind('click.DT', { action: "previous" }, fnClickHandler);
            $('<li class="next disabled"><a href="#">&raquo;</a></li>').appendTo($("ul", nPaging)).find("a").bind('click.DT', { action: "next" }, fnClickHandler);
        },
            //informacion de la funcion de envios de la base de datos
        "aoColumns": [
            { "mDataProp": "Codigo" },
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
                    return d;
                }
            },
            { "mDataProp": "Emisor" },
            { "mDataProp": "Receptor" },
            { "mDataProp": "Pais" },
            { "mDataProp": "Ciudad" },
            { "mDataProp": "Costo" },
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
    //lo que esta dentro de los parentesis tiene separado el tr para denotar que es para cada fila
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
function GuardarSeguimiento() {
    var registro = {};
    registro.idenvio = $("#id").val();
    registro.Observacion = $('#Observacion').val();
    registro.Estatus = $('#Estatus').val();

    $('.loading').show()
    $('.btn').hide();
    $.ajax({
        type: "POST",
        url: "lst_envios.aspx?fn=GuardarSeguimiento",
        data: JSON.stringify(registro),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            $('.loading').hide()
            $('.btn').show();
            $("#btn_cerrarnuevoseg").click();


            if (response.rslt == 'exito') {
                $("#dv_mensaje_seg").html('El registro ha sido procesado con exito.');
                $("#dv_mensaje_seg").show();
                setTimeout(function () { $('#dv_mensaje_seg').hide(); }, 10000);
            }
            else {
                $("#dv_error_seg").html(response.msj);
                $("#dv_error_seg").show();
                setTimeout(function () { $('#dv_error_seg').hide(); }, 10000);
            }
        },
        error: function () {
            $('.loading').hide()
            $('.btn').show();
            $("#dv_error_nuevoseg").html('Error de comunicación con el servidor. Funcion GuardarSeguimiento().');
            $("#dv_error_nuevoseg").show();
            setTimeout(function () { $('#dv_error').hide(); }, 10000);
        }
    });
}
var modalseguimiento;
var tableElementSeguimiento;
function Seguimiento() {
    var id = '';
    $('#tbDetails tr').each(function () {
        if ($(this).hasClass('row_selected')) {
            id = this.id;
            $("#id").val(id);
        }
    });

    if (id == '') {
        $("#dv_error").html("Seleccione un registro");
        $("#dv_error").show();
        setTimeout(function () { $('#dv_error').hide(); }, 10000);
        return false;
    }

    $('#btn_cargar').hide()
    $('#dvloader').show()

    $('#tbSeguimiento').dataTable().fnDestroy();
    var giRedraw = false;
    var responsiveHelper;
    var breakpointDefinition = {
        tablet: 1024,
        phone: 480
    };

    $('.loading').show()
    $('.btn').hide();
    tableElementSeguimiento = $('#tbSeguimiento')
    tableElementSeguimiento.dataTable({
        "bProcessing": true,
        "bServerSide": false,
        "sAjaxSource": "lst_envios.aspx?fn=Seguimiento&id=" + id,
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

                modalseguimiento = $.remodal.lookup[$('[data-remodal-id=modalseguimiento]').data('remodal')];
                modalseguimiento.open();
                fnCallback(json);
            });
        },
        fnPreDrawCallback: function () {
            if (!responsiveHelper) {
                responsiveHelper = new ResponsiveDatatablesHelper(tableElementSeguimiento, breakpointDefinition);
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
                      { "mDataProp": "Estatus" },
                     { "mDataProp": "Observacion" },
                     { "mDataProp": "Usuario" }
                    ]
});

//$(".first.paginate_button, .last.paginate_button").hide();
var search_input = tableElementSeguimiento.closest('.dataTables_wrapper').find('div[id$=_filter] input');
search_input.attr('placeholder', "Buscar");
}

var modalnuevoseg;
function NuevoSeguimiento() {
    $('#btn_cerrarseguimiento').click()
    $('#Observacion').val('');
    modalnuevoseg = $.remodal.lookup[$('[data-remodal-id=modalnuevoseg]').data('remodal')];
    modalnuevoseg.open();
}

function ValidarSeguimiento() {
    var valido = $("#form1").valid();
    return valido;
}