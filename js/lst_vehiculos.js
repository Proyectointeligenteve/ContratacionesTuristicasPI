function Cargar() {
    Permisos()
    CargarListado()
    EventosListado()
    CargarAgencias()
    
    
    $('.decimal').keypress(function (evt) {
        if (evt.which == 46) {
            $(this).val($(this).val() + ',');
            evt.preventDefault();
        }
    });

    $('.imp_fiscal').keypress(function(tecla) {
        if(!tecla_valida_fiscal(tecla.charCode)) return false;
    });

    $('.rif').keypress(function(tecla) {
        if(!tecla_valida_rif(tecla.charCode)) return false;
    });
}
function Permisos() {
    $.ajax({
        type: "POST",
        url: "lst_vehiculos.aspx?fn=permisos",
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
        "sAjaxSource": "lst_vehiculos.aspx?fn=cargar&v=" + v,
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
        "sDom": '<"izq">frt<"izq"i><"der"p>',
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
            { "mDataProp": "Codigo" },
            { "mDataProp": "Agencia" },
            { "mDataProp": "Nombre" },
            { "mDataProp": "Categoria" },
            { "mDataProp": "Estatus" },
            {
                "mDataProp": "Imagen",
                "mRender": function (data, type, full) {
                    var valor = "<img src='img/vehiculos/" + full.Imagen + "' width='32px' />"
                    return valor ;
                }
            }
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
function EventosListado()
{
    $("#tbDetails tbody").click(function (event) {

        $(tableElement.fnSettings().aoData).each(function () {
            $(this.nTr).removeClass('row_selected');
        });
        $(event.target.parentNode).addClass('row_selected');

    });

}

function Ver() {

    var id = "";
    $('#tbDetails tr').each(function () {
        if ($(this).hasClass('row_selected')) {
            id = this.id;
            $("#id").val(id);
        }
    });
    if (id == "") {
        $("#dv_error").html('Seleccione un registro');
        $("#dv_error").show();
        setTimeout(function () { $('#dv_error').hide(); }, 10000);
        return false;
    }
    $.ajax({
        type: "POST",
        url: "lst_vehiculos.aspx?fn=editar",
        data: '{"id":"' + id + '"}',
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            if (response.rslt == 'exito') {
                $('#Codigo').val(response.Codigo);
                $('#Nombre').val(response.Nombre);
                $('#Descripcion').val(response.Descripcion);
                $('#Categoria').val(response.Categoria);
                $('#Agencia').val(response.Agencia);
                $('#btn_aceptar').addClass('hide');
                CargarArchivos();
                modal = $.remodal.lookup[$('[data-remodal-id=modal]').data('remodal')];
                modal.open();

            }
            else {
                $("#dv_mensaje").hide()
                $("#dv_error").html(response.msj);
                $("#dv_error").show();
                setTimeout(function () { $('#dv_error').hide(); }, 10000);
            }
        },
        error: function () {
            $('#btn_cerrar').click();
            $("#dv_mensaje").hide();
            $("#dv_error").html('Error de comunicación con el servidor. El registro no ha sido actualizado.');
            $("#dv_error").show();
        }
    });

}

function CargarProducto() {
    var id = $("#provedor").val();

    $('.loading').show()
    $('.btn').hide();
    $.ajax({
        type: "POST",
        url: "frm_compras.aspx?fn=cargarprovedor",
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
                $('#rif').val(response.Rif);
                $('#Nombre').val(response.Nombre);
                $('#Atencion').val(response.Atencion);
                $('#Telefono').val(response.Telefono);
                $('#Direccion').val(response.Direccion);
                $("#Fecha").focus();
            }
            else {
                $("#dv_mensaje").hide()
                $("#dv_error").html(response.msj);
                $("#dv_error").show();
                setTimeout(function () { $('#dv_error').hide(); }, 10000);
                $("#rif").focus();
            }
        },
        error: function () {
            $('.loading').hide()
            $('.btn').show();
            $("#dv_error").html('Error de comunicación con el servidor. El registro no ha sido cargado.');
            $("#dv_error").show();
            setTimeout(function () { $('#dv_error').hide(); }, 10000);
            $("#rif").focus();
        }
    });

}
function Validar() {
    var valido = $("#form1").valid();
    return valido;
}

var modal;
function Nuevo() {

    $('#id').val(0);
    $('#Codigo').val('');
    $('#Categoria').val('');
    $('#Nombre').val('');
    $('#Descripcion').val('');
    $('#Agencia').val(0);

    $.ajax({
        success: function (response) {
	    $("#tbDetails").empty();
            modal = $.remodal.lookup[$('[data-remodal-id=modal]').data('remodal')];
            modal.open();
        },
        error: function () {
            $('#btn_cerrar').click();
            $("#dv_mensaje").hide();
            $("#dv_error").html('Error de comunicación con el servidor. El registro no ha sido actualizado.');
            $("#dv_error").show();
        }
    });
}
function Editar() {

    var id = "";
    $('#tbDetails tr').each(function () {
        if ($(this).hasClass('row_selected')) {
            id = this.id;
            $("#id").val(id);
        }
    });
    if (id == "") {
        $("#dv_error").html('Seleccione un registro');
        $("#dv_error").show();
        setTimeout(function () { $('#dv_error').hide(); }, 10000);
        return false;
    }
    $.ajax({
        type: "POST",
        url: "lst_vehiculos.aspx?fn=editar",
        data: '{"id":"' + id  + '"}',
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            if (response.rslt == 'exito') {
                $('#Codigo').val(response.Codigo);
                $('#Nombre').val(response.Nombre);
                $('#Descripcion').val(response.Descripcion);
                $('#Categoria').val(response.Categoria);
                $('#Agencia').val(response.Agencia);

                CargarArchivos();
                modal = $.remodal.lookup[$('[data-remodal-id=modal]').data('remodal')];
                modal.open();
                
            }
            else {
                $("#dv_mensaje").hide()
                $("#dv_error").html(response.msj);
                $("#dv_error").show();
                setTimeout(function () { $('#dv_error').hide(); }, 10000);
            }
        },
        error: function () {
            $('#btn_cerrar').click();
            $("#dv_mensaje").hide();
            $("#dv_error").html('Error de comunicación con el servidor. El registro no ha sido actualizado.');
            $("#dv_error").show();
        }
    });
        
}
function Guardar() {
        var registro = {};
        registro.id = $('#id').val();
        registro.Codigo = $('#Codigo').val();
        registro.Nombre = $('#Nombre').val();
        registro.Descripcion = $('#Descripcion').val();
        registro.Categoria = $('#Categoria').val();
        registro.Agencia = $('#Agencia').val();
        
            $.ajax({
            type: "POST",
            url: "lst_vehiculos.aspx?fn=guardar",
            data: JSON.stringify(registro),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (response) {
                //
                if (response.rslt == 'exito') {
                    modal.close();
                    $("#dv_error").hide()
                    $("#dv_mensaje").html('El registro ha sido procesado con exito.');
                    $("#dv_mensaje").show();
                    setTimeout(function () { $('#dv_mensaje').hide(); }, 10000);
                }
                else {
                    $("#dv_mensaje").hide()
                    $("#dv_error").html(response.msj);
                    $("#dv_error").show();
                    setTimeout(function () { $('#dv_error').hide(); }, 10000);
                }
                //MODAL DE PREGUNTA
                $('#btn_cerrar').click();
                modalnuevo = $.remodal.lookup[$('[data-remodal-id=modalnuevo]').data('remodal')];
                modalnuevo.open();

                $('#tbDetails').dataTable().fnDestroy();
                CargarListado();
            },
            error: function () {
                $('#btn_cerrar').click();
                $("#dv_mensaje").hide();
                $("#dv_error").html('Error de comunicación con el servidor. El registro no ha sido actualizado.');
                $("#dv_error").show();
            }
        });
}
function Eliminar() {
    var id = "";
    $('#tbDetails tr').each(function () {
        if ($(this).hasClass('row_selected')) {
            id = this.id;
            $("#id").val(id);
        }
    });

    $.ajax({
        type: "POST",
        url: "lst_vehiculos.aspx?fn=eliminar",
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
        url: "lst_vehiculos.aspx?fn=Anular",
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
                $("#dv_mensaje").html('El registro ha sido anulado.');
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

function Subir() {
    var id = 0;
    id = $("#id").val();

    $('.btn').hide();
    $('.dvloader').show();

    var files = $("#inputFile").get(0).files;
    var data = new FormData();
    for (i = 0; i < files.length; i++) {
        data.append("file" + i, files[i]);
    }
    $.ajax({
        type: "POST",
        url: "lst_vehiculos.aspx?fn=subir_archivo&id=" + id,
        contentType: false,
        processData: false,
        data: data,
        success: function (result) {
            $('.btn').show();
            $('.dvloader').hide();
            CargarArchivos();
        },
        error: function () {
            $('.btn').show();
            $('.dvloader').hide();
            $("#dv_error_modal").html('Error de comunicación con el servidor. Función SubirArchivo().');
            $("#dv_error_modal").show();
            setTimeout(function () { $('#dv_error_modal').hide(); }, 10000);
        }
    });
}
function CargarArchivos() {
    var id = 0;
    id = $("#id").val();
    //if (id != 0) {
    $.ajax({
        type: "POST",
        url: "lst_vehiculos.aspx?fn=cargar_archivos&id=" + id,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (result) {
            if (result.rslt == 'exito') {
                $("#dv_archivos").empty();
                $("#dv_archivos").append("<br>");

                var $ul = $('<div/>').attr({ id: 'sortable' });

                for (var i = 0; i < result.Cantidad; i++) {
                    var $li = $('<div/>').addClass("imgdrag");

                    var $img2 = $('<img/>').attr({ src: 'img/remove.png', id: 'img_adj_' + i, onclick: 'EliminarArchivo("' + result["Archivo" + i] + '");' }).addClass("img_rmv_adj");
                    $li.append($img2);

                    var $img1 = $('<img/>').attr({ src: 'img/vehiculos/' + result["Archivo" + i], id: 'txt_adj_' + result["id" + i], width: '100px', height: '75px' });
                    $li.append($img1);

                    $ul.append($li);
                }

                $("#dv_archivos").append($ul);

                $("#sortable").sortable({
                    stop: function (event, ui) {
                        var imgs = '';
                        $(".imgdrag").each(function (index) {
                            imgs += this.children[0].id + ';';
                        });
                        ActualizarPosiciones(imgs);
                    }
                });
                $("#sortable").disableSelection();
            }
            else {
                $("#dv_error_modal").html(result.msj);
                $("#dv_error_modal").show();
                setTimeout(function () { $('#dv_error_modal').hide(); }, 10000);
            }
        },
        error: function () {
            $('.btn').show();
            $('.dvloader').hide();
            $("#dv_error_modal").html('Error de comunicación con el servidor. Función CargarArchivos().');
            $("#dv_error_modal").show();
            setTimeout(function () { $('#dv_error_modal').hide(); }, 10000);
        }
    });
    //}   

}
function EliminarArchivo(archivo) {
    var id = 0;
    id = $("#id").val();

    $('.btn').hide();
    $('.dvloader').show();

    $.ajax({
        type: "POST",
        url: "lst_vehiculos.aspx?fn=eliminar_archivo&id=" + id,
        data: '{"archivo":"' + archivo + '"}',
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            $('.btn').show();
            $('.dvloader').hide();
            if (response.rslt == 'exito') {
                $("#dv_mensaje").html('El archivo ha sido eliminado.');
                $("#dv_mensaje").show();
                setTimeout(function () { $('#dv_mensaje').hide(); }, 10000);
                CargarArchivos();
            }
            else {
                $("#dv_error").html(response.msj);
                $("#dv_error").show();
                setTimeout(function () { $('#dv_error').hide(); }, 10000);
            }
        },
        error: function () {
            $('.btn').show();
            $('.dvloader').hide();
            $("#dv_error").html('Error de comunicación con el servidor. Función EliminarArchivo().');
            $("#dv_error").show();
            setTimeout(function () { $('#dv_error').hide(); }, 10000);
        }
    });
}
function ActualizarPosiciones(posiciones) {
    var id = 0;
    id = $("#id").val();

    $('.btn').hide();
    $('.dvloader').show();

    $.ajax({
        type: "POST",
        url: "lst_vehiculos.aspx?fn=actualizar_posiciones&id=" + id,
        data: '{"posiciones":"' + posiciones + '"}',
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            $('.btn').show();
            $('.dvloader').hide();
            if (response.rslt == 'exito') {
            }
            else {
                $("#dv_error").html(response.msj);
                $("#dv_error").show();
                setTimeout(function () { $('#dv_error').hide(); }, 10000);
            }
        },
        error: function () {
            $('.btn').show();
            $('.dvloader').hide();
            $("#dv_error").html('Error de comunicación con el servidor. Función ActualizarPosiciones().');
            $("#dv_error").show();
            setTimeout(function () { $('#dv_error').hide(); }, 10000);
        }
    });
}

function CargarAgencias() {
    $("#Agencia").empty();
    $.ajax({
        type: "POST",
        url: "lst_vehiculos.aspx?fn=cargar_agencias",
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
            //$("#Pais").val($("#hfPais").val());
        },
        error: function () {
            $("#dv_mensaje").hide();
            $("#dv_Error").html('Error de comunicación con el servidor al intentar cargar la funcion Agencias.');
            $("#dv_Error").show();
            setTimeout(function () { $('#dv_Error').hide(); }, 10000);
        }
    });
}