<%@ Page Title="" Language="VB" MasterPageFile="~/principal.master" AutoEventWireup="false" CodeFile="frm_envios.aspx.vb" Inherits="frm_envios" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css" title="currentStyle">
        @import "css/jquery.dataTables.css";
    </style>
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
                        <div class="control-group">
                            <label class="col-sm-4 control-label" for="Codigo">Codigo</label>
                            <div class="col-sm-8">
                                <input type="text" id="Codigo" name="Codigo" class="form-control" maxlength="200" />
                            </div>
                        </div>
                    </div>
            </div>
            <%--<div class="container"><h5>Emisor</h5></div>--%>
            <div class="row-fluid">
                <div class="span6">
                    <div class="control-group">
                        <label class="col-sm-2 control-label" for="IdClienteEmisor">Emisor</label>
                        <div class="col-sm-10">
                            <div class="input-group">
                                <div class="input-group-btn">
                                <input type="text" class="form-control" placeholder="" name="IdClienteEmisor" id="IdClienteEmisor" onchange="BuscarClientesE()" />
                                    <button id="btn_cargarE" class="btn btn-default" type="button" onclick="BuscarClientesE();return false;"><i class="glyphicon glyphicon-search"></i></button>
                                    <div style="display: none" id="dvloaderE"><img src="img/loading.gif" width="32px" /></div>
                                </div>
                            </div>
                            <input type="hidden" id="ClienteE" name="ClienteE" />
                        </div>
                    </div>
                </div>
                <div class="span6">
                    <div class="control-group">
                        <label class="col-sm-2 control-label" for="NombreE">Nombre</label>
                        <div class="col-sm-10">
                            <input type="text" id="NombreE" name="NombreE" class="form-control" />
                        </div>
                    </div>
                </div>
            </div>
            <div class="row-fluid">   
                <div class="span4">
                    <div class="control-group">
                    <label class="span3 control-label" for="PaisE">Pais</label>
                        <div class="span9">
                            <select id="PaisE" name="PaisE" class="form-control" onchange="CargarEstados()"></select>
                            <input type="hidden" id="HfPaisE" name="HfPaisE" value="0" />
                            <ul class="error"></ul>
                        </div>
                    </div>
                 </div>
                <div class="span4">
                    <div class="control-group">
                        <label class="span3 control-label" for="EstadoE">Estado</label>
                        <div class="span9">
                            <select id="EstadoE" name="EstadoE" class="form-control" onchange="CargarCiudades()"></select>
                            <input type="hidden" id="HfEstadoE" name="HfEstadoE" value="0" />
                            <ul class="error"></ul>
                        </div>
                    </div>
                </div>
                <div class="span4">
                    <div class="control-group">
                        <label class="span3  control-label" for="CiudadE">Ciudad</label>
                        <div class="span9">
                            <select id="CiudadE" name="CiudadE" class="form-control"></select>
                            <input type="hidden" id="HfCiudadE" name="hfCiudadE" value="0" />
                            <ul class="error"></ul>
                        </div>
                    </div>
                </div>
            </div>
                <div class="row-fluid">
                <div class="span6">
                    <div class="control-group">
                        <label class="col-sm-2 control-label" for="IdClienteReceptor">Receptor</label>
                        <div class="col-sm-10">
                            <div class="input-group">
                                <div class="input-group-btn">
                                <input type="text" class="form-control" placeholder="" name="IdClienteReceptor" id="IdClienteReceptor" onchange="BuscarClientesR()" />
                                    <button id="btn_cargarR" class="btn btn-default" type="button" onclick="BuscarClientesR();return false;"><i class="glyphicon glyphicon-search"></i></button>
                                    <div style="display: none" id="dvloaderR"><img src="img/loading.gif" width="32px" /></div>
                                </div>
                            </div>
                            <input type="hidden" id="ClienteR" name="ClienteR" />
                        </div>
                    </div>
                </div>
                    <div class="span6">
                        <div class="control-group">
                            <label class="col-sm-2 control-label" for="NombreR">Nombre</label>
                            <div class="col-sm-10">
                                <input type="text" id="NombreR" name="NombreR" class="form-control" />
                            </div>
                        </div>
                    </div>
                </div>
            <div class="row-fluid">   
                <div class="span4">
                    <div class="control-group">
                    <label class="span3 control-label" for="PaisR">Pais</label>
                        <div class="span9">
                            <select id="PaisR" name="PaisR" class="form-control" onchange="CargarEstados()"></select>
                            <input type="hidden" id="HfPaisR" name="hfPaisR" value="0" />
                            <ul class="error"></ul>
                        </div>
                    </div>
                 </div>
                <div class="span4">
                    <div class="control-group">
                        <label class="span3 control-label" for="EstadoR">Estado</label>
                        <div class="span9">
                            <select id="EstadoR" name="EstadoR" class="form-control" onchange="CargarCiudades()"></select>
                            <input type="hidden" id="hfEstado" name="hfEstado" value="0" />
                            <ul class="error"></ul>
                        </div>
                    </div>
                </div>
                <div class="span4">
                    <div class="control-group">
                        <label class="span3  control-label" for="CiudadR">Ciudad</label>
                        <div class="span9">
                            <select id="CiudadR" name="CiudadR" class="form-control"></select>
                            <input type="hidden" id="hfCiudadR" name="hfCiudadR" value="0" />
                            <ul class="error"></ul>
                        </div>
                    </div>
                </div>
            </div>
            <hr />
            <div class="row-fluid">
                <div class="span6">
                    <div class="control-group">
                        <label class="span6 control-label" for="DireccionEnvio">Direccion</label>
                        <div class="span6">
                        <textarea id="DireccionEnvio" rows="3" name="DireccionEnvio" maxlength="250"></textarea>
                        </div>
                    </div>
                </div>
                <div class="span6"></div>
            </div>
            </form>
            <br />
            <hr />
                                   
            <%--<style type="text/css" title="currentStyle">
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
                }
            </style>--%>
        <script type="text/javascript">
            $(function () {
                $('#form1').validate({
                    rules: {
                        Codigo: {
                        required: true  }
                    }
                });
                jQuery.validator.addMethod('select', function (value) {
                    return (value != '0');
                }, "seleccione una opción");
            });
            </script>

                <%--paqueteOS--%>
        <hr />
        <div style="width: 50%; float: left">
            <h4>Paquetes</h4>
        </div>
        <div class="der">
            <div class="btn-group">
            <img src='img/loading2.gif' class="loading" />
                <button class="btn" id="btn_paqueteAdd" onclick="paqueteAdd(); return (false);"><span class="glyphicon glyphicon-plus"></span>&nbsp;Nuevo</button>
                <button class="btn" id="btn_paqueteEdit" onclick="paqueteEdit(); return (false);"><span class="glyphicon glyphicon-edit"></span>&nbsp;Editar</button>
                <button class="btn" id="btn_paqueteDelete" onclick="paqueteConfirm(); return (false);"><span class="glyphicon glyphicon-remove"></span>&nbsp;Eliminar</button>
            </div>
        </div>
        <div>
            <div>
                <table style="font-size: 12px" id="tbl_paquete" cellpadding="0" cellspacing="0" border="0" class="table table-responsive table-striped table-bordered">
                    <thead>
                        <tr>
                            <td  data-class="expand">Numero</td>
                            <td  data-hide="phone,tablet">Descripcion</td>
                            <td  data-hide="phone,tablet">Peso</td>
                            <td  data-hide="phone,tablet">Volumen</td>
                            <td  data-hide="phone,tablet">Costo</td>
                        </tr>
                    </thead>
                    <tbody></tbody>
                </table>
                <input type="hidden" id="hdn_TotalpaqueteRecords" name="hdn_TotalpaqueteRecords" value="0" />
            </div>
        </div>

     
        <div class="remodal" data-remodal-id="basicModal2">
            <div class="modal-header">
                <h4 class="modal-title" id="myModalLabel">Formulario de paquetes</h4>
            </div>
            <div class="modal-body">
                <form id="form2">
                    <input type="hidden" id="hdn_paqueteId" name="hdn_paqueteId" value="0" />
                    <div class="espacio"></div>
                    <div class="row-fluid">
                        <div class="span12">
                            <div class="control-group">
                                <div class="row-fluid">
                                    <label class="span2 control-label" for="Paquetes">Numero</label>
                                    <div class="span10">
                                        <select id="NumeroP" name="NumeroP" class="form-control"></select>
                                        <input type="hidden" id="hfNumeroP" name="hfNumeroP" value="0" />
                                    </div>
                                </div>
                                <div class="row-fluid">
                                    <label class="span2 control-label" for="Peso">Peso</label>
                                    <div class="span10">
                                        <select id="PesoP" name="PesoP" class="form-control"></select>
                                        <input type="hidden" id="HfPesoP" name="HfPesoP" value="0" />
                                    </div>
                                </div>
                                <div class="row-fluid">
                                    <label class="span2 control-label" for="Volumen">Volumen</label>
                                    <div class="span10">
                                        <select id="VolumenP" name="VolumenP" class="form-control"></select>
                                        <input type="hidden" id="HfVolumenP" name="HfVolumenP" value="0" />
                                    </div>
                                </div>
                                <div class="row-fluid">
                                    <label class="span2 control-label" for="Costo">Costo</label>
                                    <div class="span10">
                                        <select id="CostoP" name="CostoP" class="form-control"></select>
                                        <input type="hidden" id="HfCostoP" name="HfCostoP" value="0" />
                                    </div>
                                </div>
                                <div class="row-fluid">
                                    <label class="span2 control-label" for="Descripcion">Descripcion</label>
                                    <div class="span10">
                                        <select id="DescripcionP" name="DescripcionP" class="form-control"></select>
                                        <input type="hidden" id="HfDescripcionP" name="HfCostoP" value="0" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <%--<div class="row-fluid">
                        <div class="span12">
                            <div class="control-group">
                                <label class="span2 control-label" for="txt_paqueteCharge">Codigo</label>
                                <div class="span10">
                                    <input type="text" id="txt_paqueteCharge" name="txt_paqueteCharge" class="form-control" />
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row-fluid">
                        <div class="span12">
                            <div class="control-group">
                                <label class="span2 control-label" for="txt_paquetePhone">Telefono</label>
                                <div class="span10">
                                    <input type="text" id="txt_paquetePhone" name="txt_paquetePhone" class="form-control" />
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row-fluid">
                        <div class="span12">
                            <div class="control-group">
                                <label class="span2 control-label" for="txt_Email">Email</label>
                                <div class="span10">
                                    <input type="text" id="txt_Email" name="txt_Email" class="form-control" />
                                </div>
                            </div>
                        </div>
                    </div>--%>
                </form>
            </div>
            <div class="modal-footer">
                <div class="alert alert-danger" id="dv_Error2"></div>
                <div class="alert alert-success" id="dv_Message2"></div>    
                <button type="button" class="btn btn-default" data-dismiss="modal" onclick="basicModal2.close()">Cerrar</button>
                <button type="button" class="btn btn-primary" onclick="if(paqueteValidate()){paquetesave();}">Aceptar</button>
            </div>
            <script type="text/javascript">
                $(function () {
                    $('#form2').validate({
                        rules: {
                            Paquetes: {
                                required: true
                            }
                        }
                    });

                });
                    jQuery.validator.addMethod('select', function (value) {
                        return (value != '0');
                    }, "Seleccione");
            </script>
        </div>
    </div>
       
    <div class="remodal" data-remodal-id="modalclienteE">
        <div class="modal-header">
            <h4 class="modal-title" id="H2">Seleccione el cliente Emisor</h4>
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
    <div class="remodal" data-remodal-id="modalclienteR">
        <div class="modal-header">
            <h4 class="modal-title" id="H3">Seleccione el cliente Receptor</h4>
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

        <div class="remodal" data-remodal-id="deleteModal2">
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
        
        <div class="remodal" data-remodal-id="msjModal">
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

        <div class="remodal" data-remodal-id="deletemodal">
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
            $("#dv_Error3").hide();
            $("#dv_Message3").hide();
            $("#dv_Error4").hide();
            $("#dv_Message4").hide();
            $("#dv_Error5").hide();
            $("#dv_Message5").hide();
            $("#dv_Error6").hide();
            $("#dv_Message6").hide();
            load()
        });
</script>
</asp:Content>
