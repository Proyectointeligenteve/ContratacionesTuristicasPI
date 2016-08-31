<%@ Page Title="" Language="VB" MasterPageFile="principal.master" AutoEventWireup="false" CodeFile="frm_agencias.aspx.vb" Inherits="frm_agencias" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css" title="currentStyle">
        @import "css/jquery.dataTables.css";
        @import "css/frm_freelances.css";
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
                <h4 class="modal-title" id="H1">Formulario de Agencias</h4>
            </div>
            <div align="right">
                <button type="button" class="btn btn-default" data-dismiss="modal" onclick="window.location.href='lst_agencias.aspx'">Volver</button>
                <button type="button" class="btn btn-primary" onclick="if(validate()){save();}" id="btn_guardar">Guardar</button>
            </div>
        </div>

        <form id="form1" style="margin-top: 10px">
            <input type="hidden" id="hdn_id_cliente" name="hdn_id_cliente" value="0"/>
            <div class="espacio"></div>
                 
            <div class="row-fluid">
                <div class="span6">
                    <div class="control-group">
                        <label class="span2 control-label" for="Codigo">Codigo</label>
                        <div class="span10">
                            <input type="text" id="Codigo" name="Codigo" class="form-control" disabled="disabled" />
                        </div>
                    </div>
                </div>
                <div class="span6">
                    <div class="control-group">
                        <label class="span2 control-label" for="Rif">Rif</label>
                        <div class="span10">
                            <input type="text" id="Rif" name="Rif" class="form-control" onchange="buscarAgencia()" style="text-transform: uppercase;" maxlength="10"/>
                            <ul class="error"></ul>
                        </div>
                    </div>
                </div>
            </div>
            <br />
            <div class="row-fluid">
                <div class="span6">
                    <div class="control-group">
                        <label class="span2 control-label" for="Nombre">Nombre</label>
                        <div class="span10">
                            <input type="text" id="Nombre" name="Nombre" class="form-control" maxlength="50" />
                            <ul class="error"></ul>
                        </div>
                    </div>
                </div>
            <!--/row-->
                <div class="span6">
                    <div class="control-group">
                        <label class="span2 control-label" for="RazonSocial">Razón Social</label>
                        <div class="span10">
                            <input type="text" id="RazonSocial" name="RazonSocial" class="form-control"/>
                            <ul class="error"></ul>
                        </div>
                    </div>
                </div>
                <!--/span-->
            </div>
            <br />
            <div class="row-fluid">
                <div class="span6">
                    <div class="control-group">
                        <label class="span2 control-label" for="Direccion">Direccion</label>
                        <div class="span10" align="left">
                            <textarea id="Direccion" name="Direccion" rows="3" class="form-control "></textarea>
                            <ul class="error"></ul>
                        </div>
                    </div>
                </div> 
                <div class="span6">
                    <div class="control-group">
                        <label class="span2 control-label" for="TelefonoF">Telefono Fijo</label>
                        <div class="span10">
                            <input type="text" id="TelefonoF" name="TelefonoF" class="form-control caracter" maxlength="13"/>
                            <ul class="error"></ul>
                        </div>
                    </div>
                    <div class="control-group">
                        <label class="span2 control-label" for="TelefonoM">Telefono Movil</label>
                        <div class="span10">
                            <input type="text" id="TelefonoM" name="TelefonoM" class="form-control" maxlength="13"/>
                            <ul class="error"></ul>
                        </div>
                    </div>
                <!--/span-->
                </div>  
            </div>
            <br />
            <div class="row-fluid">
                <div class="span6">
                    <div class="control-group">
                        <label class="span2 control-label" for="Web">Web</label>
                        <div class="span10">
                            <input type="text" id="Web" name="Web" class="form-control caracter" maxlength="150" />
                            <ul class="error"></ul>
                        </div>
                    </div>
                </div>
                <div class="span6">
                    <div class="control-group">
                        <label class="span2 control-label" for="Email">Email</label>
                        <div class="span10">
                            <input type="text" id="Email" name="Email" class="form-control" maxlength="150" />
                            <ul class="error"></ul>
                        </div>
                    </div>
                <!--/span-->
                </div>
            </div>
            <br />
            <div class="row-fluid">
                <div class="span6">
                    <div class="control-group">
                        <label class="span2 control-label" for="LimiteCredito">Limite Credito</label>
                        <div class="span10">
                            <input type="text" id="LimiteCredito" name="LimiteCredito" class="form-control" />
                            <ul class="error"></ul>
                        </div>
                    </div>
                </div>
                <div class="span6">
                    <div class="control-group">
                        <label class="span2 control-label" for="Pais">Pais</label>
                        <div class="span10">
                            <select id="Pais" name="Pais" class="form-control" onchange="CargarEstados()"></select>
                            <input type="hidden" id="hfPais" name="hfPais" value="0" /> 
                            <ul class="error"></ul>
                        </div>
                    </div>
                <!--/span-->
                </div>
            </div>
            <br />
            <div class="row-fluid">
                <div class="span6">
                    <div class="control-group">
                        <label class="span2 control-label" for="Estado">Estado</label>
                        <div class="span10">
                            <select id="Estado" name="Estado" class="form-control" onchange ="CargarCiudades()"></select>
                            <input type="hidden" id="hfEstado" name="hfEstado" value="0" /> 
                            <ul class="error"></ul>
                        </div>
                    </div>
                </div>
                <div class="span6">
                    <div class="control-group">
                        <label class="span2 control-label" for="Ciudad">Ciudad</label>
                        <div class="span10">
                            <select id="Ciudad" name="Ciudad" class="form-control"></select>
                            <input type="hidden" id="hfCiudad" name="hfCiudad" value="0" /> 
                            <ul class="error"></ul>
                        </div>
                    </div>
                <!--/span-->
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
                        Rif: {
                            required: true
                        },
                        Nombre: {
                            required: true
                        },
                        RazonSocial: {
                            required: true
                        },
                        Direccion: {
                            required: true
                        },
                        Email: {
                            required: true
                        },
                        LimiteCredito: {
                            required: true
                        },
                        Pais: {
                            select: true
                        },
                        Ciudad: {
                            select: true
                        }
                    }
                });
                jQuery.validator.addMethod('select', function (value) {
                    return (value != '0');
                }, "seleccione una opción");
            });
         </script>
        
        <%--comisiones--%>
        <hr />
        <div style="width: 50%; float: left">
            <h4>Comisiones</h4>
        </div>
        <div class="der">
            <div class="btn-group">
            <img src='img/loading2.gif' class="loading" />
                <button class="btn btn-default" id="btn_comisionAdd" onclick="comisionAdd(); return (false);"><span class="glyphicon glyphicon-plus"></span>&nbsp;Nuevo</button>&nbsp;&nbsp
                <button class="btn btn-default" id="btn_comisionEdit" onclick="comisionEdit(); return (false);"><span class="glyphicon glyphicon-edit"></span>&nbsp;Editar</button>&nbsp;&nbsp
                <button class="btn btn-default" id="btn_comisionDelete" onclick="comisionConfirm(); return (false);"><span class="glyphicon glyphicon-remove"></span>&nbsp;Eliminar</button>
            </div>
        </div>
        <div>
            <div>
                <table style="font-size: 12px;background-color:#12abb8" id="tbl_comision" cellpadding="0" cellspacing="0" border="0" class="table table-responsive table-striped table-bordered">
                    <thead>
                        <tr>
                            <td  class="hide">Agencia</td>
                            <td  data-class="expand">Tipo</td>
                            <td  data-class="expand">Nombre</td>
                            <td  data-hide="phone,tablet">Comision</td>
                        </tr>
                    </thead>
                    <tbody></tbody>
                </table>
                <input type="hidden" id="hdn_TotalcomisionRecords" name="hdn_TotalcomisionRecords" value="0" />
            </div>
        </div>
                <%--contactoOS--%>
        <hr />
        <div style="width: 50%; float: left">
            <h4>Contactos</h4>
        </div>
        <div class="der">
            <div class="btn-group">
            <img src='img/loading2.gif' class="loading" />
                <button class="btn btn-default" id="btn_contactoAdd" onclick="contactoAdd(); return (false);"><span class="glyphicon glyphicon-plus"></span>&nbsp;Nuevo</button>&nbsp;&nbsp
                <button class="btn btn-default" id="btn_contactoEdit" onclick="contactoEdit(); return (false);"><span class="glyphicon glyphicon-edit"></span>&nbsp;Editar</button>&nbsp;&nbsp
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
                            <%--<td  data-hide="phone,tablet">Email</td>--%>
                        </tr>
                    </thead>
                    <tbody></tbody>
                </table>
                <input type="hidden" id="hdn_TotalcontactoRecords" name="hdn_TotalcontactoRecords" value="0" />
            </div>
        </div>
        <br />
        <br />
        <div class="remodal" data-remodal-id="modalC" style="background-color:#013b63;">
            <div class="modal-header">
                <h4 class="modal-title" id="H2"><span style="color :white;">Formulario de Comisiones</span></h4>
            </div>
            <div class="modal-body" style="background-color:#013b63;color:white;">
                <form id="form3">
                    <input type="hidden" id="hdn_comisionId" name="hdn_comisionId" value="0"/>
                    <div class="espacio"></div>
                    <div class="row-fluid">
                        <div class="span12">
                            <div class="control-group">
                                <label class="span2 control-label" for="Tipo">Tipo Comision</label>
                                <div class="span10">
                                    <select id="Tipo" name="Tipo" class="form-control" onchange="TipoComision()"></select>
                                    <input type="hidden" id="hfTipo" name="hfTipo" value="0" />
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row-fluid hide" id="div_aerolinea">
                        <div class="span12">
                            <div class="control-group">
                                <label class="span2 control-label" for="Aerolineas">Aerolineas</label>
                                <div class="span10">
                                    <select id="Aerolineas" name="Aerolineas" class="form-control"></select>
                                    <input type="hidden" id="hfAerolineas" name="hfAerolineas" value="0" />
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row-fluid hide" id="div_hotel">
                        <div class="span12">
                            <div class="control-group">
                                <label class="span2 control-label" for="Hoteles">Hoteles</label>
                                <div class="span10">
                                    <select id="Hoteles" name="Hoteles" class="form-control"></select>
                                    <input type="hidden" id="hfHoteles" name="hfHoteles" value="0" />
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row-fluid hide" id="div_vehiculo">
                        <div class="span12">
                            <div class="control-group">
                                <label class="span2 control-label" for="Vehiculos">Vehiculos</label>
                                <div class="span10">
                                    <select id="Vehiculos" name="Vehiculos" class="form-control"></select>
                                    <input type="hidden" id="hfVehiculos" name="hfVehiculos" value="0" />
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row-fluid">
                        <div class="span12">
                            <div class="control-group">
                                <label class="span2 control-label" for="Comision">Comision</label>
                                <div class="span10">
                                    <input type="text" id="Comision" name="Comision" class="form-control"/>
                                    <select id="Forma" name="Forma" class="form-control">
                                        <option value="0">--</option>
                                        <option value="1">Bs</option>
                                        <option value="2">%</option>
                                    </select>
                                </div>
                            </div>
                        </div>
                    </div>
                    
                </form>
            </div>
            <div class="modal-footer">
                <div class="alert alert-danger" id="dv_Error3"></div>
                <div class="alert alert-success" id="dv_Message3"></div>    
                <button type="button" class="btn btn-default" data-dismiss="modal" onclick="modalC.close()">Cerrar</button>
                <button type="button" class="btn btn-primary" onclick="if(comisionValidate()){comisionSave();}">Aceptar</button>
            </div>
            <script type="text/javascript">
                $(function () {
                    $('#form2').validate({
                        rules: {
                            Tipo: {
                                select: true
                            },
                            Comision: {
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
                                    <input type="text" id="NombreC" name="NombreC" class="form-control" maxlength="250"/>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row-fluid">
                        <div class="span12">
                            <div class="control-group">
                                <label class="span2 control-label" for="CargoC">Cargo</label>
                                <div class="span10">
                                    <input type="text" id="CargoC" name="CargoC" class="form-control"  maxlength="50"/>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row-fluid">
                        <div class="span12">
                            <div class="control-group">
                                <label class="span2 control-label" for="TelefonoC">Telefono</label>
                                <div class="span10">
                                    <input type="number" id="TelefonoC" name="TelefonoC" class="form-control" maxlength="13"/>
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

        <div class="remodal" data-remodal-id="deleteModal3">
            <div class="modal-header">
                <h4>Eliminar</h4>
            </div>
            <div class="modal-body">
                <p>Estas seguro que deseas eliminar el registro?</p>
            </div>
            <div class="modal-footer">
                <img src='img/loading2.gif' class="loading" />
                <button type="button" class="btn btn-default" data-dismiss="modal" onclick="deleteModal3.close()">Cerrar</button>
                <button type="button" class="btn btn-danger" onclick="comisionDelete();">Aceptar</button>
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
    
    <script type="text/javascript" src="js/frm_agencias.js"></script>
    <script type="text/javascript">
        $(document).ready(function ()
        {
            $("#dv_Error").hide();
            $("#dv_Message").hide();
            $("#dv_Error2").hide();
            $("#dv_Message2").hide();
            $("#dv_Error3").hide();
            $("#dv_Message3").hide();
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
