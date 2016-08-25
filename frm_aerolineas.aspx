<%@ Page Title="" Language="VB" MasterPageFile="~/principal.master" AutoEventWireup="false" CodeFile="frm_aerolineas.aspx.vb" Inherits="frm_aerolineas" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css" title="currentStyle">
        @import "css/jquery.dataTables.css";
        @import "css/frm_aerolineas.css";
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
                <img src='img/loading2.gif' class="loading" />
                <button type="button" class="btn btn-default" data-dismiss="modal" onclick="window.location.href='lst_aerolineas.aspx'">Volver</button>
                <button type="button" class="btn btn-primary" onclick="if(validate()){save();}" id="btn_guardar">Guardar</button>
            </div>
        </div>

        <form id="form1" style="margin-top: 20px">
            <input type="hidden" id="hdn_id_aerolinea" name="hdn_id_aerolinea" value="0"/>
            <div class="espacio"></div>
                <div class="row-fluid">
                    <div class="span6">
                        <div class="control-group">
                            <label class="col-sm-2 control-label" for="codigo">Codigo</label>
                            <div class="col-sm-10">
                                <input type="text" id="codigo" name="codigo" class="form-control" disabled="disabled"/>
                            </div>
                        </div>
                    </div> 
                    <div class="span6">
                        <div class="control-group">
                            <label class="col-sm-2 control-label" for="identificador">Identificador</label>
                            <div class="col-sm-10">
                                <input type="text" id="identificador" name="identificador" class="form-control" maxlength="15" style="text-transform: uppercase;"/>
                            </div>
                        </div>
                    </div>               
                </div>
                <br />
                 <div class="row-fluid">
                    <div class="span6">
                        <div class="control-group">
                            <label class="col-sm-2 control-label" for="nombre">Nombre</label>
                            <div class="col-sm-10">
                                <input type="text" id="nombre" name="nombre" class="form-control" maxlength="200" />
                            </div>
                        </div>
                    </div>
                    <div class="span6">
                        <div class="control-group">
                            <label class="col-sm-2 control-label" for="razon_social">Razon Social</label>
                            <div class="col-sm-10">
                                <input type="text" id="razon_social" name="razon_social" class="form-control" />
                            </div>
                        </div>
                    </div>
                </div>
                <br />
                <div class="row-fluid">
                    <div class="span12">
                        <label class="col-sm-1 control-label" for="direccion">Direccion</label>
                        <div class="col-sm-11">                                
                            <textarea id="direccion" style ="width :87% !important" rows="3" name="direccion" class="form-control"></textarea>
                        </div>
                    </div>
                </div>
                <br />
                <div class="row-fluid">
                    <div class="span6">
                        <div class="control-group">
                            <label class="col-sm-2 control-label" for="telefono_fijo">Telefono Fijo</label>
                            <div class="col-sm-10">
                                <input type="text" id="telefono_fijo" name="telefono_fijo" class="form-control" maxlength="13" />
                            </div>
                        </div>
                    </div>
                    <div class="span6">
                        <div class="control-group">
                            <label class="col-sm-2 control-label" for="telefono_movil">Telefono Movil</label>
                            <div class="col-sm-10">
                                <input type="text" id="telefono_movil" name="telefono_movil" class="form-control" maxlength="13"  />
                            </div>
                        </div>
                    </div>
                </div>
                <br />
                <div class="row-fluid">
                    <div class="span6">
                        <div class="control-group">
                            <label class="col-sm-2 control-label" for="email">Email</label>
                            <div class="col-sm-10">
                                <input type="text" id="email" name="email" class="form-control" maxlength="150" />
                            </div>
                        </div>
                    </div>
                    <div class="span6">
                        <div class="control-group">
                            <label class="col-sm-2 control-label" for="web">Web</label>
                            <div class="col-sm-10">
                                <input type="text" id="web" name="web" class="form-control" maxlength="150" />
                            </div>
                        </div>
                    </div>
                </div>
                <br />
                <div class="row-fluid">
                    <div class="span6">
                        <div class="control-group">
                            <label class="col-sm-2 control-label" for="comision">Comision</label>
                            <div class="col-sm-10">
                                <input type="text" id="comision" name="comision" class="form-control" />
                            </div>
                        </div>
                    </div>
                    <div class="span6">
                        <div class="control-group">
                            <label class="col-sm-2 control-label" for="IATA">IATA</label>
                            <div class="col-sm-10">
                                <input type="text" id="IATA" name="IATA" class="form-control" maxlength="3"  style="text-transform: uppercase;"/>
                            </div>
                        </div>
                    </div>                    
                </div>
            
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
                }
            </style>
        </form>
        <script type="text/javascript">
            $(function () {
                $('#form1').validate({
                    rules: {
                        nombre: {
                            required: true
                        },
                        razon_social: {
                            required: true
                        },
                        identificador: {
                            required: true
                        },
                        direccion: {
                            required: true
                        },
                        telefono_fijo: {
                            required: true
                        },
                        IATA: {
                            required: true
                        },
                        comision: {
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
        <div style="width :50%; float: left">
            <h4 style="margin-bottom :20px !important">Contactos</h4>
        </div>
        <div class="der">
            <div class="btn-group">
            <img src='img/loading2.gif' class="loading" />
                <button class="btn btn-default" id="btn_contactoAdd" onclick="contactoAdd(); return (false);"><span class="glyphicon glyphicon-plus"></span>&nbsp;Nuevo</button>
                <button class="btn btn-default" id="btn_contactoEdit" onclick="contactoEdit(); return (false);"><span class="glyphicon glyphicon-edit"></span>&nbsp;Editar</button>
                <button class="btn btn-default" id="btn_contactoDelete" onclick="contactoConfirm(); return (false);"><span class="glyphicon glyphicon-remove"></span>&nbsp;Eliminar</button>
            </div>
        </div>
        <div>
            <div>
                <table style="font-size: 12px;background-color:#12abb8" id="tbl_contacto" cellpadding="0" cellspacing="0" border="0" class="table table-responsive table-striped table-bordered">
                    <thead>
                        <tr>
                            <td  data-class="expand">Nombre</td>
                            <td  data-hide="phone,tablet">Cargo</td>
                            <td  data-hide="phone,tablet">Telefono</td>
                        </tr>
                    </thead>
                    <tbody></tbody>
                </table>
                <input type="hidden" id="hdn_TotalcontactoRecords" name="hdn_TotalcontactoRecords" value="0" />
            </div>
        </div>
             
        <div class="remodal" data-remodal-id="modal" style="background-color:#013b63;">
            <div class="modal-header">
                <h4 class="modal-title" id="myModalLabel"><span style="color :white;">Formulario de Contactos</span></h4>
            </div>
            <div class="modal-body" style="background-color:#013b63;color:white;">
                <form id="form2">
                    <input type="hidden" id="hdn_contactoId" name="hdn_contactoId" value="0"/>
                    <div class="espacio"></div>
                    <div class="row-fluid">
                        <div class="span12">
                            <div class="control-group">
                                <label class="span2 control-label" for="NombreC">Nombre</label>
                                <div class="span10">
                                    <input type="text" id="NombreC" name="NombreC" class="form-control" />
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row-fluid">
                        <div class="span12">
                            <div class="control-group">
                                <label class="span2 control-label" for="CargoC">Cargo</label>
                                <div class="span10">
                                    <input type="text" id="CargoC" name="CargoC" class="form-control" />
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row-fluid">
                        <div class="span12">
                            <div class="control-group">
                                <label class="span2 control-label" for="TelefonoC">Telefono</label>
                                <div class="span10">
                                    <input type="text" id="TelefonoC" name="TelefonoC" class="form-control" />
                                </div>
                            </div>
                        </div>
                    </div>
                </form>
            </div>
            <div class="modal-footer">
                <div class="alert alert-danger" id="dv_Error2"></div>
                <div class="alert alert-success" id="dv_Message2"></div>    
                <button type="button" class="btn btn-default" data-dismiss="modal" onclick="modal.close()">Cerrar</button>
                <button type="button" class="btn btn-primary" onclick="if(contactoValidate()){contactoSave();}">Aceptar</button>
            </div>
            <script type="text/javascript">
                $(function () {
                    $('#form2').validate({
                        rules: {
                            NombreC: {
                                required: true
                            },
                            CargoC: {
                                required: true
                            },
                            TelefonoC: {
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
                <button type="button" class="btn btn-danger" onclick="contactoDelete();">Aceptar</button>
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
           
    <%--</div>--%>
    
    <script type="text/javascript" src="js/frm_aerolineas.js"></script>
    <script type="text/javascript">
        $(document).ready(function ()
        {
            $("#dv_Error").hide();
            $("#dv_Message").hide();
            $("#dv_Error2").hide();
            $("#dv_Message2").hide();
            //$("#dv_Error3").hide();
            //$("#dv_Message3").hide();
            //$("#dv_Error4").hide();
            //$("#dv_Message4").hide();
            //$("#dv_Error5").hide();
            //$("#dv_Message5").hide();
            //$("#dv_Error6").hide();
            //$("#dv_Message6").hide();
            load()
        });
</script>
</asp:Content>
