<%@ Page Title="" Language="VB" MasterPageFile="~/principal.master" AutoEventWireup="false" CodeFile="frm_envios.aspx.vb" Inherits="frm_envios" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css" title="currentStyle">
        @import "css/jquery.dataTables.css";
        @import "css/frm_envios.css";
    </style>
    <script type="text/javascript" src="jquery-1.3.2.min.js"></script>    
    <script type="text/javascript" src="jquery-barcode.js"></script>  
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <!-- Wrap all page content here -->

    
    <!-- Begin page content -->

    
    <div></div>
    <div class="container">
        <div class="alert alert-danger" id="dv_Error"></div>
        <div class="alert alert-success" id="dv_Message"></div>
        <div class="alert alert-warning" id="dv_advertencia"></div>

        <div class="modal-header">
            <div align="left">
                <h4 class="modal-title" id="H1">Formulario de Envios</h4>
            </div>
            <div align="right">
                <button type="button" class="btn btn-default" data-dismiss="modal" onclick="window.location.href='lst_envios.aspx'">Volver</button>
                <button type="button" class="btn btn-primary" onclick="if(validate()){save();}" id="btn_guardar">Guardar</button>
            </div>
        </div>

        <form id="form1" style="margin-top: 20px">
            <input type="hidden" id="hdn_id_envio" name="hdn_id_envio" value="0"/>
           
             <div class="row-fluid">
                    <div class="span6">
                        <label class="span3 control-label" for="Codigo">Codigo</label>
                        <div class="span9">
                            <div class="input-group">
                                <input type="text" id="Codigo" name="Codigo" class="form-control" disabled="disabled" />
                            </div>
                        </div>
                    </div>
                <div class="span6">
                    <label class="span3 control-label" for="TotalR">Total Envio&nbsp;</label>
                    <div class="span9">
                        <div class="input-group">
                            <input type="text" id="TotalR" name="TotalR" class="form-control" value="0" />
                        </div>
                    </div>
                </div>
            </div>
            <hr />
            <div class="row-fluid">
                <div class="span12">
                    <div class="control-group">
                        <label class="span12 control-label" for="Codigo" style ="font-size :14px !important; text-align :left !important"><b>EMISOR</b></label>
                    </div>
                </div>
            </div>
            <div class="row-fluid">
                <div class="span6">
                        <label class="span3 control-label" for="IdentificadorEmisor">Identificador</label>
                        <div class="span9">
                            <div class="input-group">
                                <div class="input-group-btn">
                                <input type="text" class="form-control" placeholder="" name="IdentificadorEmisor" id="IdentificadorEmisor" onchange="BuscarClientesE()" />
                                    <button id="btn_cargarE" class="btn btn-default" type="button" onclick="BuscarClientesE();return false;"><i class="glyphicon glyphicon-search"></i></button>
                                    <div style="display: none" id="dvloaderE"><img src="img/loading.gif" width="32px" /></div>
                                </div>
                            </div>
                            <input type="hidden" id="ClienteE" name="ClienteE" />
                        </div>
                </div>
                <div class="span6">
                    <label class="span3 control-label" for="NombreE">Nombre</label>
                    <div class="span9">
                        <div class="input-group">
                            <input type="text" id="NombreE" name="NombreE" class="form-control" />
                        </div>
                    </div>
                </div>
            </div>
            <br />
            <div class="row-fluid">   
                <div class="span6">
                    <label class="span3 control-label" for="PaisE">Pais</label>
                    <div class="span9">
                        <div class="input-group">
                        <select id="PaisE" name="PaisE" class="form-control" onchange="CargarEstadosE()"></select>
                        <input type="hidden" id="HfPaisE" name="HfPaisE" value="0" />
                        <ul class="error"></ul>
                        </div>
                    </div>
                 </div>
            </div>
            <hr />
            <div class="row-fluid">
                <div class="span12">
                    <div class="input-group">
                        <label class="span12 control-label" for="Codigo" style ="font-size :14px !important; text-align :left !important"><b>RECEPTOR</b></label>
                    </div>
                </div>
            </div>
            <div class="row-fluid">
                <div class="span6">
                    <label class="span3 control-label" for="IdentificadorReceptor">Identificador</label>
                    <div class="span9">
                        <div class="input-group">
                            <div class="input-group-btn">
                            <input type="text" class="form-control" placeholder="" name="IdentificadorReceptor" id="IdentificadorReceptor" onchange="BuscarClientesR()" />
                                <button id="btn_cargarR" class="btn btn-default" type="button" onclick="BuscarClientesR();return false;"><i class="glyphicon glyphicon-search"></i></button>
                                <div style="display: none" id="dvloaderR"><img src="img/loading.gif" width="32px" /></div>
                            </div>
                        </div>
                        <input type="hidden" id="ClienteR" name="ClienteR" />
                    </div>
                </div>
                <div class="span6">
                    <label class="span3 control-label" for="NombreR">Nombre</label>
                    <div class="span9">
                        <div class="input-group">
                            <input type="text" id="NombreR" name="NombreR" class="form-control" />
                        </div>
                    </div>
                </div>
            </div>
            <br />
            <div class="row-fluid">   
                <div class="span6">
                    <label class="span3 control-label" for="PaisR">Pais</label>
                    <div class="span9">
                        <div class="input-group">
                            <select id="PaisR" name="PaisR" class="form-control" onchange="CargarEstadosR()"></select>
                            <input type="hidden" id="HfPaisR" name="hfPaisR" value="0" />
                            <ul class="error"></ul>
                        </div>
                    </div>
                 </div>
            </div>
            <hr />
            <div class="row-fluid hide">
                <div class="span12">
                    <div class="control-group">
                        <label class="span1 control-label" for="DireccionEnvio">Direccion</label>
                        <div class="span11" align="left">
                        <textarea rows="2" id="DireccionEnvio" name="DireccionEnvio" class="form-control" style="width:100% !important" ></textarea>
                        </div>
                    </div>
                </div>
            </div>
            <div class="row-fluid">
                <div class="span6">
                    <label class="span3 control-label" for="Peso">Peso</label>
                    <div class="span9">
                        <div class="input-group">
                            <input type="text" id="PesoP" name="PesoP" class="form-control" />
                        </div>
                    </div>
                </div>
                <div class="span6">
                    <label class="span3 control-label" for="Volumen">Volumen</label>
                    <div class="span9">
                        <div class="input-group">
                            <input type="text" id="VolumenP" name="VolumenP" class="form-control" />
                        </div>
                    </div>
                </div>
            </div>
            <div class="row-fluid">
                <div class="span6">
                    <label class="span3 control-label" for="DescripcionP">Descripcion</label>
                    <div class="span9">
                        <div class="input-group">
                            <textarea id="DescripcionP" rows="2" name="DescripcionP" maxlength="250" style ="width:100%" class="form-control"></textarea>
                        </div>
                    </div>
                </div>
                <div class="span6">
                    <label class="span3 control-label" for="Costo">Costo</label>
                    <div class="span9">
                        <div class="input-group">
                            <input type="text" id="CostoP" name="CostoP" class="form-control" />
                        </div>
                    </div>
                </div>
            </div>

            </form>
            <br />
            <%--<hr />--%>
                                   
            <style type="text/css" title="currentStyle">
                @import "css/jquery.dataTables.css";
                @media (min-width:400px) 
                {
                    .control-label {
                    white-space: nowrap !important;
                    text-align: left !important;
                    }

                    .form-control {
                    margin-left: 25px !important;
                    width: 250px !important;
                    }
                    .control-group {
                    margin-left: 25px !important;
                    width: 200px !important;
                    }
                }
            </style>
        <script type="text/javascript">
            $(function () {
                $('#form1').validate({
                    rules: {
                        Codigo: {
                            required: true
                        },                    
                        IdentificadorEmisor: {
                            required: true
                        },
                        IdentificadorReceptor: {
                            required: true
                        },
                        PesoP: {
                            required: true
                        },
                        CostoP: {
                            required: true
                        }
                    }
                 });
                jQuery.validator.addMethod('select', function (value) {
                    return (value != '0');
                }, "seleccione una opción");
            });
            </script>

                <%--paqueteOS--%>
        <%--<hr />
        <div style="width: 50%; float: left">
            <h4>Paquetes</h4>
        </div>
        <div class="der">
            <div class="btn-group">
            <img src='img/loading2.gif' class="loading" />
                <button class="btn btn-default" id="btn_paqueteAdd" onclick="paqueteAdd(); return (false);"><span class="glyphicon glyphicon-plus"></span>&nbsp;Nuevo</button>
                <button class="btn btn-default" id="btn_paqueteEdit" onclick="paqueteEdit(); return (false);"><span class="glyphicon glyphicon-edit"></span>&nbsp;Editar</button>
                <button class="btn btn-default" id="btn_paqueteDelete" onclick="paqueteConfirm(); return (false);"><span class="glyphicon glyphicon-remove"></span>&nbsp;Eliminar</button>
            </div>
        </div>
        <div>
            <div>
                <table style="font-size: 12px;background-color:#12abb8;padding-top:10px" id="tbl_paquetes" cellpadding="0" cellspacing="0" border="0" class="table table-responsive table-striped table-bordered">
                    <thead>
                        <tr>
                            <td  class="hide">Envio</td>
                            <td  data-class="expand">Numero</td>
                            <td  data-hide="phone,tablet">Peso</td>
                            <td  data-hide="phone,tablet">Volumen</td>
                            <td  data-hide="phone,tablet">Descripcion</td>
                            <td  data-hide="phone,tablet">Costo</td>
                            <td  data-hide="phone,tablet">Codigo</td>
                        </tr>
                    </thead>
                    <tbody></tbody>
                </table>
                <input type="hidden" id="hdn_TotalpaqueteRecords" name="hdn_TotalpaqueteRecords" value="0" />
            </div>
        </div>--%>
        <br />
        <br />     

        <%--<div class="remodal" data-remodal-id="basicModal2" style="background-color:#013b63;">
            <div class="modal-header">
                <h4 class="modal-title" id="myModalLabel"><span style="color :white;">Formulario de Paquetes</span></h4>
            </div>
            <div class="modal-body" style="background-color:#013b63;color:white;">
                <form id="form2">
                    <input type="hidden" id="hdn_paqueteId" name="hdn_paqueteId" value="0" />
                    <div class="espacio"></div>
                    <div class="row-fluid">
                        <div class="span12">
                            <div class="control-group">
                                <div class="row-fluid">
                                    <div class="span12">
                                        <label class="span3 control-label" for="Paquetes">Numero</label>
                                        <div class="span9">
                                            <input type="text" id="NumeroP" name="NumeroP" class="form-control" disabled="disabled" />
                                        </div>
                                    </div>
                                </div>
                                <div class="row-fluid">
                                    <div class="span12">
                                        <label class="span3 control-label" for="Peso">Peso</label>
                                        <div class="span9">
                                            <input type="text" id="PesoP" name="PesoP" class="form-control" />
                                        </div>
                                    </div>
                                </div>
                                <div class="row-fluid">
                                    <div class="span12">
                                        <label class="span3 control-label" for="Volumen">Volumen</label>
                                        <div class="span9">
                                            <input type="text" id="VolumenP" name="VolumenP" class="form-control" />
                                        </div>
                                    </div>
                                </div>
                                <div class="row-fluid">
                                    <div class="span12">
                                        <label class="span3 control-label" for="DescripcionP">Descripcion</label>
                                        <div class="span9">
                                            <textarea id="DescripcionP" rows="2" name="DescripcionP" maxlength="250" style ="width:100%" class="form-control"></textarea>
                                        </div>
                                    </div>
                                </div>
                                <div class="row-fluid">
                                    <div class="span12">
                                        <label class="span3 control-label" for="Costo">Costo</label>
                                        <div class="span9">
                                            <input type="text" id="CostoP" name="CostoP" class="form-control" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    
                </form>
            </div>
            <div class="modal-footer">
                <div class="alert alert-danger" id="dv_Error2"></div>
                <div class="alert alert-success" id="dv_Message2"></div>    
                <button type="button" class="btn btn-default" data-dismiss="modal" onclick="basicModal2.close()">Cerrar</button>
                <button type="button" class="btn btn-primary" onclick="if(paqueteValidate()){paqueteSave();}">Aceptar</button>
            </div>
            <script type="text/javascript">
                $(function () {
                    $('#form2').validate({
                        rules: {
                            PesoP: {
                                required: true
                            },
                            CostoP: {
                                required: true
                            }
                        }
                    });

                });
                    jQuery.validator.addMethod('select', function (value) {
                        return (value != '0');
                    }, "Seleccione");
            </script>
        </div>--%>
    </div>
       
    <div class="remodal" data-remodal-id="modalclienteE" style="background-color:#013b63;color:white;font-size:14px !important">
        <div class="modal-header">
            <h4 class="modal-title" id="H2">Seleccione el Emisor</h4>
        </div>
        <div class="modal-body">
            <!--Anotacion: Listado de clientes emisores basado en el identificador ingresado en el formulario de envio-->
            <table style="width: 100% !important" id="tbClientesE" cellpaddingger="0" cellspacing="0" border="0" class="table table-bordered table-striped">
                <thead>
                    <tr>
                        <td  data-class="expand">Nombre</td>
                        <td  data-hide="phone,tablet">Identificador</td>
                        <td  data-hide="phone,tablet">Telefono</td>
                        <td  data-hide="phone,tablet">Email</td>
                    </tr>
                </thead>
                <tbody>
                </tbody>
            </table>
        </div>
        <div class="modal-footer">
            <div class="alert alert-danger" id="dv_error_modalE" name="dv_error_modalE">
            </div>
            <img src='img/loading2.gif' class="loading" />
            <button type="button" class="btn btn-default" data-dismiss="modal" onclick="modalclienteE.close()" id="btn_cerrarclienteE">Cerrar</button>
            <button type="button" class="btn btn-success" onclick="SeleccionarClienteE()">Aceptar</button>
        </div>
    </div>
         
    <div class="remodal" data-remodal-id="modalclienteR" style="background-color:#013b63;color:white;font-size:14px !important">
        <div class="modal-header">
            <h4 class="modal-title" id="H3">Seleccione el Receptor</h4>
        </div>
        <div class="modal-body">
            <!--Anotacion: Listado de clientes receptores basado en el identificador ingresado en el formulario de envio-->
            <table style="width: 100% !important" id="tbClientesR" cellpaddingger="0" cellspacing="0" border="0" class="table table-bordered table-striped">
                <thead>
                    <tr>
                        <td  data-class="expand">Nombre</td>
                        <td  data-hide="phone,tablet">Identificador</td>
                        <td  data-hide="phone,tablet">Telefono</td>
                        <td  data-hide="phone,tablet">Email</td>
                    </tr>
                </thead>
                <tbody>
                </tbody>
            </table>
        </div>
        <div class="modal-footer">
            <div class="alert alert-danger" id="dv_error_modalR" name="dv_error_modalR">
            </div>
            <img src='img/loading2.gif' class="loading" />
            <button type="button" class="btn btn-default" data-dismiss="modal" onclick="modalclienteR.close()" id="btn_cerrarClienteR">Cerrar</button>
            <button type="button" class="btn btn-success" onclick="SeleccionarClienteR()">Aceptar</button>
        </div>
    </div>

    <div class="remodal" data-remodal-id="nuevoCmodal" style="background-color:#013b63;color:white;font-size:14px !important">
        <div class="modal-header">
            <h4>Mensaje</h4>
        </div>
        <div class="modal-body">
            <p>No se encontraron clientes con el identificador indicado. Desea Registrarlo?</p>
        </div>
        <div class="modal-footer">
            <img src='img/loading2.gif' class="loading" />
            <button type="button" class="btn btn-default" data-dismiss="modal" onclick="nuevoCmodal.close()">Cerrar</button>
            <button type="button" class="btn btn-danger" onclick="NuevoCliente()">Aceptar</button>
        </div>
    </div>

    <div class="remodal" data-remodal-id="modalClienteN" style="background-color:#013b63;color:white;font-size:14px !important">
    <h4 class="modal-title" id="H6">Nuevo Cliente</h4>
    <div class="modal-body">
        <form id="form3" class="form-horizontal" role="form">
            <input type="hidden" id="hf_cliente_n" name="hf_cliente_n"/>
            <input type="hidden" id="tipo_cliente" name="tipo_cliente"/>

            <!--/row-->
            <div class="row-fluid">
                <div class="span12">
                    <label class="span3 control-label" for="IdentificadorC">Identificador</label>
                    <div class="span9">
                    <input type="text" id="IdentificadorC" name="IdentificadorC" class="form-control" />
                    </div>
                </div>
            </div>

            <div class="row-fluid">
                <div class="span12">
                    <label class="span3 control-label" for="NombreC">Nombre</label>
                    <div class="span9">
                    <input type="text" id="NombreC" name="NombreC" class="form-control" />
                    </div>
                </div>
                <!--/span-->
            </div>

            <div class="row-fluid">
                <div class="span12">
                    <label class="span3 control-label" for="TelefonoC">Telefono</label>
                    <div class="span9">
                    <input type="text" id="TelefonoC" name="TelefonoC" class="form-control" />
                    </div>
                </div>   
            </div>

            <div class="row-fluid">
                <div class="span12">
                    <label class="span3 control-label" for="EmailC">Email</label>
                    <div class="span9">
                        <input type="text" id="EmailC" name="EmailC" class="form-control" />
                    </div>
                </div>
                <!--/span-->  
            </div>                                    
            </form>
    </div>

    <div class="modal-footer">
            <div class="alert alert-danger" id="dv_errorClienteN" name="dv_errorClienteN">
            </div>
            <div class="alert alert-success" id="dv_mensajeClienteN" name="dv_mensajeClienteN">
            </div>
            <div class="alert alert-warning" id="dv_advertenciaClienteN" name="dv_advertenciaClienteN">
            </div>
            <img src='img/loading2.gif' class="loading" />
            <button type="button" class="btn btn-default" data-dismiss="modal" onclick="modalClienteN.close()" id="btn_cerrarClientes">Cerrar</button>
            <button type="button" class="btn btn-primary" onclick="if(ValidarClienteN()){GuardarClienteN()}">Aceptar</button>
    </div>
    </div>
        <script type="text/javascript">
            $(function () {
                $('#form3').validate({
                    rules: {
                        NombreC: {
                            required: true
                            //,
                            //maximo: true
                        },
                        IdentificadorC: {
                            required: true//,
                            //date: true
                        },
                        TelefonoC: {
                            required: true
                        }
                    }

                });

                jQuery.validator.addMethod('select', function (value) {
                    return (value != '0');
                }, "seleccione una opción");


            });
        </script>

    <div class="remodal" data-remodal-id="deleteModal2" style="background-color:#013b63;color:white;font-size:14px !important">
        <div class="modal-header">
            <h4>Eliminar</h4>
        </div>
        <div class="modal-body">
            <p>Estas seguro que deseas eliminar el registro?</p>
        </div>
        <div class="modal-footer">
            <img src='img/loading2.gif' class="loading" />
            <button type="button" class="btn btn-default" data-dismiss="modal" onclick="deleteModal2.close()">Cerrar</button>
            <button type="button" class="btn btn-danger" onclick="paqueteDelete();">Aceptar</button>
        </div>
    </div>
        
    <div class="remodal" data-remodal-id="msjModal" style="background-color:#013b63;color:white;font-size:14px !important">
        <div class="modal-header">
            <h4>Mensaje</h4>
        </div>
        <div class="modal-body">
            <p>El registro ha sido guardado.</p>
        </div>
        <div class="modal-footer">
            <img src='img/loading2.gif' class="loading" />
                <button type="button" class="btn btn-success" onclick="backToList();">Ok</button>
        </div>
    </div>
        
    <div class="remodal" data-remodal-id="deletemodal" style="background-color:#013b63;color:white;font-size:14px !important">
        <div class="modal-header">
            <h4>Eliminar</h4>
        </div>
        <div class="modal-body">
            <p>Estas seguro que deseas eliminar el registro?</p>
        </div>
        <div class="modal-footer">
            <img src='img/loading2.gif' class="loading" />
            <button type="button" class="btn btn-default" data-dismiss="modal" onclick="deletemodal.close()">Cerrar</button>
            <button type="button" class="btn btn-danger" onclick="Eliminar()">Aceptar</button>
        </div>
    </div>
    
    <%--</div>--%>
    
    <script type="text/javascript" src="js/frm_envios.js"></script>
    <script type="text/javascript">
        $(document).ready(function ()
        {
            $("#dv_Error").hide();
            $("#dv_Message").hide();
            $("#dv_advertencia").hide();            
            $("#dv_Error2").hide();
            $("#dv_Message2").hide();
            $("#dv_errorClienteN").hide();
            $("#dv_mensajeClienteN").hide();
            $("#dv_advertenciaClienteN").hide();
            $("#dv_error_modalR").hide();
            $("#dv_error_modalE").hide();
            //$("#dv_Message4").hide();
            //$("#dv_Error5").hide();
            //$("#dv_Message5").hide();
            //$("#dv_Error6").hide();
            //$("#dv_Message6").hide();
            load()
        });
</script>
</asp:Content>
