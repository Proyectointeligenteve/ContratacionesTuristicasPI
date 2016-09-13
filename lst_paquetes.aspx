<%@ Page Title="" Language="VB" MasterPageFile="principal.master" AutoEventWireup="false" CodeFile="lst_paquetes.aspx.vb" Inherits="lst_paquetes" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
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
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
        <div class="container">
            <div style ="width :100%">
            <span style="font-size: 14px;color:white">LISTADO DE PAQUETES</span><hr />            
        </div>
        <div class="izq"><select id="vista_estatus" class="form-control" style="margin:0 !important; width:150px !important" onchange="CargarListado()"><option value ="1">Ver Activos</option><option value ="2">Ver Inactivos</option><option value ="3">Ver Todos</option></select></div>
        <div class="hr">
            <hr />
        </div>
        <div class="der">
            <div class="btn-group">
                <img src='img/loading2.gif' class="loading" />
                <button class="btn hide"  id="btn_agregar" onclick="Nuevo();"><span class="glyphicon glyphicon-plus"></span>&nbsp;Nuevo</button>
                <button class="btn hide" id="btn_ver" onclick="Ver();"><span class="glyphicon glyphicon-eye-open"></span>&nbsp;Ver</button>
                <button class="btn hide"  id="btn_editar" onclick="Editar();"><span class="glyphicon glyphicon-edit"></span>&nbsp;Editar</button>
                <button class="btn hide"  id="btn_anular" onclick="ConfirmarAnular();"><span class="glyphicon glyphicon-ban-circle"></span>&nbsp;Activar/Inactivar</button>
                <button class="btn hide"  id="btn_eliminar" onclick="ConfirmarEliminar();"><span class="glyphicon glyphicon-remove"></span>&nbsp;Eliminar</button>
            </div>
        </div>
        
        <style>
            @media (max-width:400px) {
                .btn-group {
                    width: 100% !important;
                }

                .btn {
                    width: 33.33% !important;
                }
            }
        </style>
        </div>
    <div class="container">
        <div class="alert alert-danger" id="dv_error" name="dv_error">
        </div>
        <div class="alert alert-success" id="dv_mensaje" name="dv_mensaje">
        </div>
    </div>
    <div class="container" style="margin-top: 10px">
        <table id="tbDetails" cellpadding="0" cellspacing="0" border="0" class="table table-bordered table-striped" style ="background-color :white !important">
            <thead>
                <tr>
                    <td data-class="expand">NOMBRE</td>
                    <td data-class="phone,tablet">DESTINO</td>
                    <td data-class="phone,tablet">FECHA INICIO</td>
                    <td data-class="phone,tablet">FECHA FIN</td>
                    <td data-class="phone,tablet">TIPO</td>
                    <td data-class="phone,tablet">PRECIO</td>
                    <td data-class="phone,tablet">ESTATUS</td>
                </tr>
            </thead>
            <tbody>
            </tbody>
        </table>
    </div>
    <div class="remodal" data-remodal-id="modal" style="background-color:#013b63;color:white">

        <div class="modal-header">
            <h4 class="modal-title" id="myModalLabel">Formulario de paquetes</h4>
        </div>
        <div class="modal-body">
            <form id="form1" class="form-horizontal" role="form">
                <input type="hidden" id="id" name="id" />

                <div class="row-fluid">
                    <div class="span6">
                        <div class="control-group">
                            <label class="col-sm-2 control-label" for="Nombre">Nombre</label>
                            <div class="col-sm-10">
                                <input type="text" id="Nombre" name="Nombre" class="form-control" maxlength="50" />
                            </div>
                        </div>
                    </div>
                    <!--/span-->

                    <div class="span6">
                        <div class="control-group">
                            <label class="col-sm-2 control-label" for="Destino">Destino</label>
                            <div class="col-sm-10">
                                <select id="Destino" name="Destino" class="form-control"></select>
                                <input type="hidden" id="hfDestino" name="hfDestino" value ="0"/>
                            </div>
                        </div>
                    </div>
                    <!--/span-->
                </div>
                <br />
                <!--/row-->
                <div class="row-fluid">
                    <div class="span6">
                        <div class="control-group">
                            <label class="col-sm-2 control-label" for="FechaInicio">Fecha Inicio</label>
                            <div class="col-sm-10">
                                <input type="text" id="FechaInicio" name="FechaInicio" class="form-control" />
                            </div>
                        </div>
                    </div>
                    <!--/span-->

                    <div class="span6">
                        <div class="control-group">
                            <label class="col-sm-2 control-label" for="FechaFin">Fecha Fin</label>
                            <div class="col-sm-10">
                                <input type="text" id="FechaFin" name="FechaFin" class="form-control" />
                            </div>
                        </div>
                    </div>
                    <!--/span-->
                </div>                
                <br />
                 <div class="row-fluid">
                    <div class="span6">
                        <div class="control-group">
                            <label class="col-sm-2 control-label" for="Tipo">Tipo</label>
                            <div class="col-sm-10">
                                <select id="Tipo" name="Tipo" class="form-control">
                                    <option value="-1">Seleccione</option>
                                    <option value="0">Nacional</option>
                                    <option value="1">Internacional</option>
                                </select>
                            </div>
                        </div>
                    </div>
                    <!--/span-->

                    <div class="span6">
                        <div class="control-group">
                            <label class="col-sm-2 control-label" for="Grupo">Grupo</label>
                            <div class="col-sm-10">
                                <select id="Grupo" name="Grupo" class="form-control">
                                    <option value="-1">Seleccione</option>
                                    <option value="0">No</option>
                                    <option value="1">Si</option>
                                </select>
                            </div>
                        </div>
                    </div>
                    <!--/span-->
                </div>
                <br />                
                 <div class="row-fluid">
                    <div class="span6">
                        <div class="control-group">
                            <label class="col-sm-2 control-label" for="Precio">Precio</label>
                            <div class="col-sm-10">
                                <input type="text" id="Precio" name="Precio" class="form-control decimal" />                                
                            </div>
                        </div>
                    </div>
                    <!--/span-->
                    <div class="span6">
                        <div class="control-group">
                            <label class="col-sm-2 control-label" for="Activo">Activo</label>
                            <div class="col-sm-10">
                                <select id="Activo" name="Activo" class="form-control">
                                    <option value="-1">Seleccione</option>
                                    <option value="0">No</option>
                                    <option value="1">Si</option>
                                </select>
                            </div>
                        </div>
                    </div>
                    <!--/span-->
                </div>

            </form>
            <br />
           
        </div>
        <div class="modal-footer">
            <button type="button" class="btn btn-default" data-dismiss="modal" onclick="modal.close();CargarListado();">Cerrar</button>
            <button type="button" class="btn btn-primary" onclick="if(Validar()){Guardar()}">Aceptar</button>
        </div>
        <%--            </div>
        </div>
    </div>--%>
    </div>
    <script type="text/javascript">
        $(function () {
            $('#form1').validate({
                rules: {
                    Nombre: {
                        required: true
                    },
                    FechaInicio: {
                        required: true
                    },
                    FechaFin: {
                        required: true
                    },
                    Destino: {
                        Oselect: true
                    },
                    Precio: {
                        required: true
                    },
                    Grupo: {
                        select2: true
                    },
                    Tipo: {
                        select2: true
                    },
                    Activo: {
                        select2: true
                    }

                }
            });

            jQuery.validator.addMethod('select2', function (value) {
                return (value != '-1');
            }, "seleccione una opción");
            jQuery.validator.addMethod('Oselect', function (value) {
                return (value != '0');
            }, "seleccione una opción");
        });
    </script>

    <div class="remodal" data-remodal-id="anularmodal">
        <div class="modal-header">
            <h4>Activar/Inactivar</h4>
        </div>
        <div class="modal-body">
            <p>Estas seguro que deseas Activar/Inactivar el registro?</p>
        </div>
        <div class="modal-footer">
            <img src='img/loading2.gif' class="loading" />
            <button type="button" class="btn btn-default" data-dismiss="modal" onclick="anularmodal.close()">Cerrar</button>
            <button type="button" class="btn btn-danger" onclick="Anular()">Aceptar</button>
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

   <div class="remodal" data-remodal-id="msjModal">
            <div class="modal-header">
                <h4>Mensaje</h4>
            </div>
            <div class="modal-body">
                <p><span id="Span1" name="msj">El registro ha sido guardado.</span></p>
            </div>
            <div class="modal-footer">
                <button type="button" class="btn btn-success" onclick="paquetesLista()">Ok</button>
            </div>
        </div> 
    <script type="text/javascript" language="javascript" src="js/lst_paquetes.js?token=<% Response.Write(Replace(Format(Date.Now, "yyyyMMddHH:mm:ss"), ":", ""))%>"></script>
    <script>

        $(document).ready(function () {
            $("#dv_mensaje").hide();
            $("#dv_error").hide();
            Cargar();
        });

    </script>
</asp:Content>
