<%@ Page Title="" Language="VB" MasterPageFile="~/principal.master" AutoEventWireup="false" CodeFile="frm_recibos.aspx.vb" Inherits="frm_recibos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css" title="currentStyle">
        @import "css/jquery.dataTables.css";
        @import "css/frm_recibos.css";
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <!-- Wrap all page content here -->

    <!-- Begin page content -->
    <div class="container">
        <div class="alert alert-danger" id="dv_error"></div>
        <div class="alert alert-success" id="dv_mensaje"></div>

        <div class="modal-header">
            <div align="left">
                <h4 class="modal-title" id="H1">Formulario de Recibos</h4>
            </div>
            <div align="right">
                <img src='img/loading2.gif' class="loading" />
                <button type="button" class="btn btn-default" data-dismiss="modal" onclick="window.location.href='lst_recibos.aspx'">Volver</button>
                <button type="button" class="btn btn-primary" onclick="if(Validar()){Guardar()};return false;" id="btn_guardar">Guardar</button>
            </div>
        </div>

        <form id="form1" class="form-horizontal" role="form">
                <input type="hidden" id="id" name="id" />
                <input type="hidden" id="hf_venta" name="id" />
                <div class="espacio"></div>

                    <div class="row-fluid">
                        <div class="span6">                            
                            <div class="control-group">
                                <label class="span2 control-label" for="Numero">Nro. Recibo</label>
                                <div class="span10">
                                    <input type="text" id="Numero" name="Numero" class="form-control" disabled="disabled" />
                                </div>
                            </div>
                        </div>
                        <div class="span6">
                            <div class="control-group">
                                <label class="span2 control-label" for="Venta">Venta</label>
                                    <div class="span10">
                                        <div class="input-group">
                                            <div class="input-group-btn">
                                            <input type="text" class="form-control" placeholder="" name="venta" id="venta" onchange="BuscarVentas()" />
                                                <button id="btn_cargarVenta" class="btn btn-default" type="button" onclick="BuscarVentas();return false;"><i class="glyphicon glyphicon-search"></i></button>
                                                <div style="display: none" id="dvloaderVenta"><img src="img/loading.gif" width="32px" /></div>
                                            </div>
                                        </div>
                                        <input type="hidden" id="Venta" name="Venta" />
                                    </div>
                            </div>
                        </div>
                    </div>
                        <%--<div class="span6">
                            <div class="control-group">
                                <label class="span2 control-label" for="Cliente">Rif Cliente</label>
                                <div class="span10">
                                    <div class="input-group">
                                        <div class="input-group-btn">
                                        <input type="text" class="form-control" placeholder="" name="rif" id="rif" onchange="BuscarClientes()" />
                                            <button id="btn_cargar" class="btn btn-default" type="button" onclick="BuscarClientes();return false;"><i class="glyphicon glyphicon-search"></i></button>
                                            <div style="display: none" id="dvloader"><img src="img/loading.gif" width="32px" /></div>
                                        </div>
                                    </div>
                                    <input type="hidden" id="Cliente" name="Cliente" />
                                </div>
                            </div>
                        </div>
                    </div> --%>
                    <div class="row-fluid">
                        <div class="span6">
                            <div class="control-group">
                                <label class="span2 control-label" for="Nombre">Nombre</label>
                                <div class="span10">
                                    <input type="text" id="Nombre" name="Nombre" class="form-control" />
                                </div>
                            </div>
                            <div class="control-group">
                                <label class="span2 control-label" for="Telefono">Telefono</label>
                                <div class="span10">
                                    <input type="text" id="Telefono" name="Telefono" class="form-control" />
                                </div>
                            </div>
                        <!--/span-->
                        </div>
                        <div class="span6">
                            <div class="control-group">
                                <label class="span2 control-label" for="Direccion">Direccion</label>
                                <div class="span10" align="left">
                                     <textarea id="Direccion" name="Direccion" rows="3" class="form-control" style="width:100% !important" ></textarea>
                                </div>
                            </div>
                        </div>   
                    </div>
                
                <br />
                    <div class="row-fluid">  
                        <div class="span6">
                            <div class="control-group">
                                <label class="span2 control-label" for="Fecha">Fecha</label>
                                <div class="span10">
                                    <input type="text" id="Fecha" name="Fecha" class="form-control" />
                                </div>
                            </div>
                            <div class="control-group">
                                <label class="span2 control-label" for="monto" style="font-size:10px!important">Monto de la Venta</label>
                                <div class="span10">
                                    <input type="text" id="Monto" name="Monto" class="form-control" disabled="disabled" />
                                </div>
                            </div>
                            <%--<div class="control-group">
                                <label class="span2 control-label" for="abonado">Abonado</label>
                                <div class="span10">
                                    <div class="input-group">
                                        <div class="input-group-btn">
                                            <input type="text" id="abonado" name="abonado" class="form-control" disabled />
                                            <button id="btn_ver_abonos" class="btn btn-default" type="button" onclick="VerAbonos();return false;"><i class="glyphicon glyphicon-eye-open"></i></button>
                                            <div style="display: none" id="dvloaderabonos"><img src="img/loading.gif" width="32px" /></div>
                                        </div>
                                    </div>
                                </div>
                            </div>--%>
                        </div> 
                        <div class="span6">
                            <div class="control-group">
                                <label class="span2 control-label" for="Observaciones">Observaciones</label>
                                <div class="span10" align="left">
                                     <textarea rows="5" id="Observaciones" name="Observaciones" class="form-control" style="width:100% !important" ></textarea>
                                </div>
                            </div>
                        </div>   
                                   
                    </div>

        <style type="text/css" title="currentStyle">
        @import "css/jquery.dataTables.css";

        @media (min-width:400px) {
            .control-label {
                white-space: nowrap !important;
                text-align: left !important;
            }

            .form-control {
                margin-left: 25px !important;
                width: 250px !important;
            }
        }
    </style>
            <%--<div class="espacio"></div>--%>
            <hr /><div style="width: 50%; float: left"><b>ITEMS DE LA VENTA</b></div>
            <div class="container" style="margin-top: 10px">
                <center>
                    <table id="tbDetallesVenta" cellpadding="0" cellspacing="0" border="0" class="table table-bordered table-striped">
                        <thead>
                            <tr>
                                <td  data-class="expand" style="background:none !important"></td>
                                <%--<td class="hide">Tipo</td>--%>
                                <td  data-hide="phone,tablet">Fecha</td>
                                <td  data-hide="phone,tablet">Concepto</td>
                                <td  data-hide="phone,tablet">Cliente</td>
                                <td  data-hide="phone,tablet">Identificacion</td>
                                <td  data-hide="phone,tablet">Moneda</td>
                                <td  data-hide="phone,tablet">Monto</td>
                            </tr>
                        </thead>
                        <tbody>
                        </tbody>
                    </table>
                </center>
            </div>
            <hr />
           <div style="width: 50%; float: left"><b>FORMA DE PAGO</b></div>
        <div class="der">
            <div class="btn-group">
                <button class="btn" id="btn_agregarDetalle" onclick="NuevoDetalles();return false;"><span class="glyphicon glyphicon-plus"></span>&nbsp;Agregar</button>
                <button class="btn" id="btn_editarDetalle" onclick="EditarDetalles();return false;"><span class="glyphicon glyphicon-edit"></span>&nbsp;Editar</button>
                <button class="btn" id="btn_eliminarDetalle" onclick="ConfirmarDetalles();return false;"><span class="glyphicon glyphicon-remove"></span>&nbsp;Eliminar</button>
            </div>
        </div>
        <div class="espacio"></div>
            <div class="container" style="margin-top: 10px">
                <center>
                <table id="tbDetalles" cellpadding="0" cellspacing="0" border="0" class="table table-bordered table-striped">
                    <thead>
                        <tr>
                            <td  data-class="expand">Monto</td>
                            <td  data-hide="phone,tablet">Fecha</td>
                            <td  data-hide="phone,tablet">Forma Pago</td>
                            <td  data-hide="phone,tablet">Numero Documento</td>
                            <td  data-hide="phone,tablet">Banco</td>

                        </tr>
                    </thead>
                    <tbody>
                    </tbody>
                </table>
                </center>
            </div>
            <br />
            <div class="row-fluid" id="dv_total">
                <div class="span6" style="padding-left: 60px !important">
                    <div class="control-group">
                        <label class="span2 control-label" for=""></label>
                        <div class="span10">
                        </div>
                    </div>
                </div>
                <div class="span6">
                    <div class="control-group">
                        <label class="span2 control-label" for="TotalR">Total Recibo&nbsp;</label>
                        <div class="span10">
                            <input type="text" id="TotalR" name="TotalR" class="form-control" value="0" />
                        </div>
                    </div>
                </div>
            </div>
            <br />
        <script type="text/javascript">
            $(function () {
                $('#form1').validate({
                    rules: {
                        Fecha: {
                            required: true//,
                            //date: true
                        },
                        Numero: {
                            required: true
                        },
                        rif: {
                            required: true
                        },
                        Nombre: {
                            required: true
                        },
                        Direccion: {
                            required: true
                        },
                        Telefono: {
                            required: true
                        },
                        Fecha: {
                            required: true//,
                            //date: true
                        },
                        Monto: {
                            monto: true
                        },
                        TotalR: {
                            monto: true
                        }
                    }
                });

                jQuery.validator.addMethod('monto', function (value) {
                    return (value != '0');
                }, "Este campo es obligatorio.");
            });
        </script>
            </form>
        <%-- MENSAJE DE RECIBO GUARDADO CON EXITO --%>
        <div class="remodal" data-remodal-id="msjModal" style="background-color:#013b63;color:white;font-size:14px !important">
            <div class="modal-header">
                <h4>Mensaje</h4>
            </div>
            <div class="modal-body">
                <p><span id="Span1" name="msj">El registro ha sido guardado.</span></p>
            </div>
            <div class="modal-footer">
                <button type="button" class="btn btn-success" onclick="recibosLista()">Ok</button>
            </div>
        </div>          

        <div class="remodal" data-remodal-id="modal" style="background-color:#013b63;">
        <h4 class="modal-title" id="myModalLabel">DETALLE DE PAGO</h4>
        <div class="modal-body" style="background-color:#013b63;color:white;">
            <form id="form2" class="form-horizontal" role="form">
                <input type="hidden" id="iddp" name="iddp"/>
                <input type="hidden" id="hf_montoPago" name="iddp" value="0"/>
                <input type="hidden" id="hf_totalPago" name="iddp" value="0"/>

                <!--/row-->
                 <div class="row-fluid">
                    <div class="span6">
                        <div class="control-group">
                            <label class="span3 control-label" for="MontoPago">Monto</label>
                            <div class="span9">
                            <input type="text" id="MontoPago" name="MontoPago" class="form-control" />
                            </div>
                        </div>
                    </div>
                 </div>
                 <div class="row-fluid">
                    <div class="span6">
                        <div class="control-group">
                            <label class="span3 control-label" for="Fecha">Fecha</label>
                            <div class="span9">
                            <input type="text" id="FechaPago" name="Fecha" class="form-control" />
                            </div>
                        </div>
                    </div>
                    <!--/span-->
                </div>
                <%--<br />--%>
                <div class="row-fluid">
                    <div class="span6">
                        <div class="control-group">
                            <label class="span3 control-label" for="FormaPago">Forma de Pago</label>
                            <div class="span9">
                                <div class="input-group">
                                    <div class="input-group-btn">
                                    <select id="FormaPago" name="FormaPago" class="form-control" onchange="HabilitarNumeroBanco();"></select>
                                    <input type="hidden" id="hfFormaPago" name="FormaPago" />                                
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>   
                </div>

                <div class="row-fluid">
                    <div class="span6">
                        <div class="control-group">
                            <label class="span3 control-label" for="NumeroDocumento">N° Documento</label>
                            <div class="span9">
                                <input type="text" id="NumeroDocumento" name="NumeroDocumento" class="form-control" />
                            </div>
                        </div>
                    </div>
                    <!--/span-->

                </div>

                <div class="row-fluid">

                    <div class="span6">
                        <div class="control-group">
                            <label class="span3 control-label" for="Banco">Banco</label>
                            <div class="span9">
                                <select id="Banco" name="Banco" class="form-control"></select>
                                <input type="hidden" id="hfBanco" name="hfBanco" />                                
                            </div>
                        </div>
                    </div>
                    <!--/span-->
                </div>
                                      
                </form>

                </div>

                <div class="modal-footer">
                    <div class="alert alert-danger" id="dv_errordetalle" name="dv_errordetalle">
                    </div>
                    <div class="alert alert-success" id="dv_mensajedetalle" name="dv_mensajedetalle">
                    </div>
                    <div class="alert alert-warning" id="dv_advertenciadetalle" name="dv_advertenciadetalle">
                    </div>
                    <img src='img/loading2.gif' class="loading" />
                    <button type="button" class="btn btn-default" data-dismiss="modal" onclick="modal.close()" id="BtnCerrar">Cerrar</button>
                    <button type="button" class="btn btn-primary" onclick="if(ValidarDetalles()){GuardarDetalles()}">Aceptar</button>
                </div>
            </div>
            </div>
            <script type="text/javascript">
                $(function () {
                    $('#form2').validate({
                        rules: {
                            MontoPago: {
                                required: true,
                                maximo: true
                            },
                            Fecha: {
                                required: true//,
                                //date: true
                            },
                            FormaPago: {
                                select: true
                            }
                        }

                    });

                    jQuery.validator.addMethod('select', function (value) {
                        return (value != '0');
                    }, "seleccione una opción");

                    jQuery.validator.addMethod('valor', function (value) {
                        return (parseFloat(value) > 0);
                    }, "seleccione una opción");

                    jQuery.validator.addMethod('maximo', function (value) {
                        var m = $("#Monto").val();
                        m = m.replace(".", "");
                        m = m.replace(",", ".");

                        var t = $("#TotalR").val();
                        t = t.replace(".", "");
                        t = t.replace(",", ".");

                        var v = value;
                        v = v.replace(".", "");
                        v = v.replace(",", ".");

			r = m - t;
                        return (v <= r);
                    }, "El monto no puede ser mayor a la diferencia por pagar");
                });
            </script>
    <%-- MENSAJE DE TOTAL DE RECIBO PAGADO --%>
    <div class="remodal" data-remodal-id="msjModalPago" style="background-color:#013b63;color:white;font-size:14px !important">
            <div class="modal-header">
                <h4>Mensaje</h4>
            </div>
            <div class="modal-body">
                <p><span id="Span2" name="msj">No puede agregar otra forma de pago, ya ha sido cubierto el total del recibo.</span></p>
            </div>
            <div class="modal-footer">
                <button type="button" class="btn btn-success" onclick="msjModalPago.close()">Ok</button>
            </div>
    </div>
    <%-- MENSAJE DE ELIMINAR DETALLE DE PAGO --%>
     <div class="remodal" data-remodal-id="deletemodal" style="background-color:#013b63;color:white;font-size:14px !important">
        <div class="modal-body">
            <h4>Estas seguro que deseas eliminar el registro?</h4>
        </div>
        <div class="modal-footer">
            <img src='img/loading2.gif' class="loading" />
            <button type="button" class="btn btn-default" onclick="deletemodal.close()" id="btn_cerrardelete">Cerrar</button>
            <button type="button" class="btn btn-danger" onclick="EliminarDetalles()">Aceptar</button>
        </div>
     </div>
    <div class="remodal" data-remodal-id="modalVenta">
        <div class="modal-header">
            <h4 class="modal-title" id="H4">Seleccione el venta</h4>
        </div>
        <div class="modal-body">

            <%--<table style="width: 100% !important" id="tbVentas" cellpaddingger="0" cellspacing="0" border="0" class="table table-bordered table-striped">--%>
            <table style="font-size: 12px;background-color:#12abb8;width: 100% !important" id="tbVentas" cellpadding="0" cellspacing="0" border="0" class="table table-responsive table-striped table-bordered">
                <thead>
                    <tr>
                        <td  data-class="expand">Fecha</td>
                        <td  data-class="phone,tablet">Numero</td>
                        <td  data-hide="phone,tablet">Vendedor</td>
                        <td  data-hide="phone,tablet">Cliente </td>
                        <td  data-hide="phone,tablet">Total</td>
                    </tr>
                </thead>
                <tbody>
                </tbody>
            </table>
        </div>
        <div class="modal-footer">
            <div class="alert alert-danger" id="dv_error_modal1" name="dv_error_modal">
            </div>
            <img src='img/loading2.gif' class="loading" />
            <button type="button" class="btn btn-default" data-dismiss="modal" onclick="modalVenta.close()" id="btn_cerrarventa">Cerrar</button>
            <button type="button" class="btn btn-success" onclick="SeleccionarVenta()">Aceptar</button>
        </div>
    </div>
     <div class="remodal" data-remodal-id="modalcliente">
        <div class="modal-header">
            <h4 class="modal-title" id="H2">Seleccione el cliente</h4>
        </div>
        <div class="modal-body">
            <%--<table style="width: 100% !important" id="tbClientes" cellpaddingger="0" cellspacing="0" border="0" class="table table-bordered table-striped">--%>
            <table style="font-size: 12px;background-color:#12abb8" id="tbClientes" cellpadding="0" cellspacing="0" border="0" class="table table-responsive table-striped table-bordered">
                <thead>
                    <tr>
                        <td  data-class="expand">Nombre</td>
                        <td  data-hide="phone,tablet">Rif</td>
                        <td  data-hide="phone,tablet">Telefono</td>
                        <td  data-hide="phone,tablet">Direccion</td>
                    </tr>
                </thead>
                <tbody>
                </tbody>
            </table>
        </div>
        <div class="modal-footer">
            <div class="alert alert-danger" id="dv_error_modal" name="dv_error_modal">
            </div>
            <img src='img/loading2.gif' class="loading" />
            <button type="button" class="btn btn-default" data-dismiss="modal" onclick="modalcliente.close()" id="btn_cerrarcliente">Cerrar</button>
            <button type="button" class="btn btn-success" onclick="SeleccionarCliente()">Aceptar</button>
        </div>
    </div>
     <div class="remodal" data-remodal-id="modalabonos">
        <div class="modal-header">
            <h4 class="modal-title" id="H3">Abonos del Venta</h4>
        </div>
        <div class="modal-body">

            <table style="width: 100% !important" id="tb_abonos" cellpaddingger="0" cellspacing="0" border="0" class="table table-bordered table-striped">
                <thead>
                    <tr>
                        <td  data-class="expand">Recibo</td>
                        <td  data-hide="phone,tablet">Cliente</td>
                        <td  data-hide="phone,tablet">Rif</td>
                        <td  data-hide="phone,tablet">Fecha</td>
                        <td  data-hide="phone,tablet">Monto</td>
                        <td  data-hide="phone,tablet">Forma de Pago</td>
                        <td  data-hide="phone,tablet">Observaciones</td>
                    </tr>
                </thead>
                <tbody>
                </tbody>
            </table>
        </div>
        <div class="modal-footer">
            <div class="alert alert-danger" id="dv_error_modal_abonos" name="dv_error_modal_abonos">
            </div>
            <img src='img/loading2.gif' class="loading" />
            <button type="button" class="btn btn-default" data-dismiss="modal" onclick="modalabonos.close()" id="btn_cerrar_abonos">Cerrar</button>
        </div>
    </div>

    <script type="text/javascript" language="javascript" src="js/frm_recibos.js?token=<% Response.Write(Replace(Format(Date.Now, "yyyyMMddHH:mm:ss"), ":", ""))%>"></script>
    <script>

        $(document).ready(function () {
            //$.validator.methods.date = function (value, element) {
            //    return this.optional(element) || Date.parseExact(value, "dd-MM-yyyy");
            //};

            $("#dv_error").hide();
            $("#dv_mensaje").hide();
            $("#dv_advertencia").hide();

            $("#dv_errordetalle").hide();
            $("#dv_mensajedetalle").hide();
            $("#dv_advertenciadetalle").hide();

            $("#dv_error_modal").hide();            
            $("#dv_error_modal1").hide();
            $("#dv_error_modal_abonos").hide();
            
            Cargar()
        });

    </script>
</asp:Content>
