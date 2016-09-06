<%@ Page Title="" Language="VB" MasterPageFile="principal.master" AutoEventWireup="false" CodeFile="lst_ventas.aspx.vb" Inherits="lst_destinos" %>

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
        <div class="alert alert-danger" id="dv_error" name="dv_error">
        </div>
        <div class="alert alert-success" id="dv_mensaje" name="dv_mensaje">
        </div>
    </div>
        <div class="container">
            <div style ="width :100%">
            <span style="font-size: 14px;color:white">LISTADO DE DESTINOS</span><hr />            
        </div>
        <div class="izq"><select id="vista_estatus" class="form-control" style="margin:0 !important; width:150px !important" onchange="CargarListado()"><option value ="1">Ver Activos</option><option value ="2">Ver Inactivos</option><option value ="3">Ver Todos</option></select></div>
        <div class="hr">
            <hr />
        </div>
        <div class="der">
            <div class="btn-group">
                <img src='img/loading2.gif' class="loading" />
                <button class="btn hide"  id="btn_agregar" onclick="Nuevo();"><span class="glyphicon glyphicon-plus"></span>&nbsp;Nuevo</button>
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
    <div class="container" style="margin-top: 10px">
        <table id="tbDetails" cellpadding="0" cellspacing="0" border="0" class="table table-bordered table-striped" style ="background-color :white !important">
            <thead>
                <tr>
                    <td data-class="expand">Nombre</td>
                    <td data-class="phone,tablet">Estatus</td>
                </tr>
            </thead>
            <tbody>
            </tbody>
        </table>
    </div>
    <div class="remodal" data-remodal-id="modal" style="background-color:#013b63;color:white;font-size:14px !important">

        <div class="modal-header">
            <h4 class="modal-title" id="myModalLabel">Formulario de Destinos</h4>
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
                            <label class="col-sm-2 control-label" for="Numero"></label>
                            <div class="col-sm-10">
                                <%--<input type="text" id="Numero" name="Numero" class="form-control" />--%>
                            </div>
                        </div>
                    </div>
                    <!--/span-->
                </div>
                <!--/row-->

            </form>
            <br />
           
        </div>
        <div class="modal-footer">
            <button type="button" class="btn btn-default" data-dismiss="modal" onclick="modal.close()">Cerrar</button>
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
                    }

                }
            });

            jQuery.validator.addMethod('select', function (value) {
                return (value != '0');
            }, "seleccione una opción");
        });
    </script>

    <div class="remodal" data-remodal-id="anularmodal" style="background-color:#013b63;color:white;font-size:14px !important">
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

   <div class="remodal" data-remodal-id="msjModal" style="background-color:#013b63;color:white;font-size:14px !important">
            <div class="modal-header">
                <h4>Mensaje</h4>
            </div>
            <div class="modal-body">
                <p><span id="Span1" name="msj">El registro ha sido guardado.</span></p>
            </div>
            <div class="modal-footer">
                <button type="button" class="btn btn-success" onclick="destinosLista()">Ok</button>
            </div>
        </div> 
    <script type="text/javascript" language="javascript" src="js/lst_destinos.js?token=<% Response.Write(Replace(Format(Date.Now, "yyyyMMddHH:mm:ss"), ":", ""))%>"></script>
    <script>

        $(document).ready(function () {
            $("#dv_mensaje").hide();
            $("#dv_error").hide();
            Cargar();
        });

    </script>
</asp:Content>
