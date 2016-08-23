<%@ Page Title="" Language="VB" MasterPageFile="~/principal.master" AutoEventWireup="false" CodeFile="frm_aerolineas.aspx.vb" Inherits="frm_aerolineas" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css" title="currentStyle">
        @import "css/jquery.dataTables.css";
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <!-- Wrap all page content here -->
    
    <!-- Begin page content -->
    <div class="container">
        <div class="alert alert-danger" id="dv_Error"></div>
        <div class="alert alert-success" id="dv_Message"></div>

        <div class="modal-header">
            <div align="left">
                <h4 class="modal-title" id="H1">Formulario de Aerolineas</h4>
            </div>
            <div align="right">
                <button type="button" class="btn btn-default" data-dismiss="modal" onclick="window.location.href='lst_aerolineas.aspx'">Volver</button>
                <button type="button" class="btn btn-primary" onclick="if(validate()){save();}" id="btn_guardar">Guardar</button>
            </div>
        </div>

        <form id="form1" style="margin-top: 20px">
            <input type="hidden" id="hdn_id_cliente" name="hdn_id_cliente" value="0"/>
            <div class="row-fluid">
                    <div class="span6">
                        <div class="control-group">
                            <label class="col-sm-2 control-label" for="Nombre">Nombre</label>
                            <div class="col-sm-10">
                                <input type="text" id="Nombre" name="Nombre" class="form-control" maxlength="200" />
                            </div>
                        </div>
                    </div>
                    <div class="span6">
                        <div class="control-group">
                            <label class="col-sm-2 control-label" for="Razonsocial">Razon social</label>
                            <div class="col-sm-10">
                                <input type="text" id="Razonsocial" name="Razonsocial" class="form-control" />
                            </div>
                        </div>
                    </div>
                </div>
                <div class="row-fluid">
                    <div class="span6">
                        <div class="control-group">
                            <label class="col-sm-2 control-label" for="Identificador">Identificador</label>
                            <div class="col-sm-10">
                                <input type="text" id="Identificador" name="Identificador" class="form-control" maxlength="15" />
                            </div>
                        </div>
                    </div>
                    <div class="span6">
                        <div class="control-group">
                            <label class="col-sm-2 control-label" for="Direccion">Direccion</label>
                            <div class="col-sm-10">
                                <input type="text" id="Direccion" name="Direccion" class="form-control" />
                            </div>
                        </div>
                    </div>
                </div>
                <div class="row-fluid">
                    <div class="span6">
                        <div class="control-group">
                            <label class="col-sm-2 control-label" for="TelefonoFijo">Telefono fijo</label>
                            <div class="col-sm-10">
                                <input type="text" id="TelefonoFijo" name="TelefonoFijo" class="form-control" maxlength="13" />
                            </div>
                        </div>
                    </div>
                    <div class="span6">
                        <div class="control-group">
                            <label class="col-sm-2 control-label" for="TelefonoMovil">Telefono movil</label>
                            <div class="col-sm-10">
                                <input type="text" id="TelefonoMovil" name="TelefonoMovil" class="form-control" maxlength="13"/>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="row-fluid">
                    <div class="span6">
                        <div class="control-group">
                            <label class="col-sm-2 control-label" for="Email">Email</label>
                            <div class="col-sm-10">
                                <input type="text" id="Email" name="Email" class="form-control" maxlength="150" />
                            </div>
                        </div>
                    </div>
                    <div class="span6">
                        <div class="control-group">
                            <label class="col-sm-2 control-label" for="web">Web</label>
                            <div class="col-sm-10">
                                <input type="text" id="Web" name="Web" class="form-control" maxlength="150" />
                            </div>
                        </div>
                    </div>
                </div>
                <div class="row-fluid">
                    <div class="span6">
                        <div class="control-group">
                            <label class="col-sm-2 control-label" for="Codigo">Codigo</label>
                            <div class="col-sm-10">
                                <input type="text" id="Codigo" name="Codigo" class="form-control" maxlength="10" />
                            </div>
                        </div>
                    </div>
                    <div class="span6">
                        <div class="control-group">
                            <label class="col-sm-2 control-label" for="IATA">IATA</label>
                            <div class="col-sm-10">
                                <input type="text" id="IATA" name="IATA" class="form-control" maxlength="3" />
                            </div>
                        </div>
                    </div>                    
                </div>
            <div class="modal-footer">
                    <div class="btn btn-default" for="Aceptar" onclick="if(Validar()){Guardar()}">Aceptar</div>
                    <div class="btn btn-default" for="Cancelar" onclick="modal.close()">Cancelar</div>
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
                        Nombre: {
                            required: true
                        }
                    }
                });
                jQuery.validator.addMethod('select', function (value) {
                    return (value != '0');
                }, "seleccione una opción");
            });
            </script>

                <%--contactoOS--%>
        <hr />
        <div style="width: 50%; float: left">
            <h4>contactos</h4>
        </div>
        <div class="der">
            <div class="btn-group">
            <img src='img/loading2.gif' class="loading" />
                <button class="btn" id="btn_contactoAdd" onclick="contactoAdd(); return (false);"><span class="glyphicon glyphicon-plus"></span>&nbsp;Nuevo</button>
                <button class="btn" id="btn_contactoEdit" onclick="contactoEdit(); return (false);"><span class="glyphicon glyphicon-edit"></span>&nbsp;Editar</button>
                <button class="btn" id="btn_contactoDelete" onclick="contactoConfirm(); return (false);"><span class="glyphicon glyphicon-remove"></span>&nbsp;Eliminar</button>
            </div>
        </div>
        <div>
            <div>
                <table style="font-size: 12px" id="tbl_contacto" cellpadding="0" cellspacing="0" border="0" class="table table-responsive table-striped table-bordered">
                    <thead>
                        <tr>
                            <td  data-class="expand">Codigo</td>
                            <td  data-hide="phone,tablet">Nombre</td>
                            <td  data-hide="phone,tablet">Tipo</td>
                            <%--<td  data-hide="phone,tablet">Email</td>--%>
                        </tr>
                    </thead>
                    <tbody></tbody>
                </table>
                <input type="hidden" id="hdn_TotalcontactoRecords" name="hdn_TotalcontactoRecords" value="0" />
            </div>
        </div>

     
        <div class="remodal" data-remodal-id="basicModal2">
            <div class="modal-header">
                <h4 class="modal-title" id="myModalLabel">Formulario de contactos</h4>
            </div>
            <div class="modal-body">
                <form id="form2">
                <input type="hidden" id="hdn_contactoId" name="hdn_contactoId" value="0"/>
                    <div class="espacio"></div>
                    <div class="row-fluid">
                        <div class="span12">
                            <div class="control-group">
                                <label class="span2 control-label" for="Contacto">Contacto</label>
                                <div class="span10">
                                    <select id="Contacto" name="Contacto" class="form-control"></select>
                                    <input type="hidden" id="hfContacto" name="hfContacto" value ="0"/>
                                </div>
                            </div>
                        </div>
                    </div>
                    <%--<div class="row-fluid">
                        <div class="span12">
                            <div class="control-group">
                                <label class="span2 control-label" for="txt_contactoCharge">Codigo</label>
                                <div class="span10">
                                    <input type="text" id="txt_contactoCharge" name="txt_contactoCharge" class="form-control" />
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row-fluid">
                        <div class="span12">
                            <div class="control-group">
                                <label class="span2 control-label" for="txt_contactoPhone">Telefono</label>
                                <div class="span10">
                                    <input type="text" id="txt_contactoPhone" name="txt_contactoPhone" class="form-control" />
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
                <button type="button" class="btn btn-primary" onclick="if(contactoValidate()){contactosave();}">Aceptar</button>
            </div>
            <script type="text/javascript">
                $(function () {
                    $('#form2').validate({
                        rules: {
                            Contacto: {
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
                <button type="button" class="btn btn-danger" onclick="contactoDelete();">Aceptar</button>
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
    
    <script type="text/javascript" src="js/frm_aerolineas.js"></script>
    <script type="text/javascript">
        $(document).ready(function ()
        {
            $("#dv_Error").hide();
            $("#dv_Message").hide();
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
