<%@ Page Title="" Language="VB" MasterPageFile="~/principal.master" AutoEventWireup="false" CodeFile="lst_vehiculos.aspx.vb" Inherits="lst_vehiculos" %>

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
                width: 200px !important;
            }
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="container">
        <div class="container">
            <div class="alert alert-danger" id="dv_error" name="dv_error">
            </div>
            <div class="alert alert-success" id="dv_mensaje" name="dv_mensaje">
            </div>
        </div>
        <div style ="width :100%">
            <span style="font-size: 14px;color:white">LISTADO DE VEHICULOS</span><hr />            
        </div>
        <div class="izq"><select id="vista_estatus" class="form-control" style="margin:0 !important; width:150px !important" onchange="CargarListado()"><option value ="1">Ver Activos</option><option value ="2">Ver Inactivos</option><option value ="3">Ver Todos</option></select></div>
        <div class="hr">
            <hr />
        </div>
        <div class="der">
            <div class="btn-group">
                <button class="btn" onclick="Nuevo();"><span class="glyphicon glyphicon-plus"></span>&nbsp;Nuevo</button>
                <button class="btn hide" id="btn_ver" onclick="Ver();"><span class="glyphicon glyphicon-eye-open"></span>&nbsp;Ver</button>
                <button class="btn" onclick="Editar();"><span class="glyphicon glyphicon-edit"></span>&nbsp;Editar</button>
                <button class="btn" id="btn_anular" onclick="ConfirmarAnular();"><span class="glyphicon glyphicon-edit"></span>&nbsp;Activar/Inactivar</button>
                <button class="btn" id="btn_eliminar" onclick="ConfirmarEliminar();"><span class="glyphicon glyphicon-edit"></span>&nbsp;Eliminar</button>
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
        <table id="tbDetails" cellpadding="0" cellspacing="0" border="0" class="table table-bordered table-striped" style ="background-color :white !important;">
            <thead>
                <tr>
                    <td data-hide="phone,tablet">CODIGO</td>
                    <td data-hide="phone,tablet">AGENCIA</td>
                    <td data-class="expand">NOMBRE</td>
                    <td data-hide="phone,tablet">CATEGORIA</td>
                    <td data-hide="phone,tablet">ESTATUS</td>
                    <td data-hide="phone,tablet">IMAGEN</td>
                </tr>
            </thead>
            <tbody>
            </tbody>
        </table>
    </div>

    <div class="remodal" data-remodal-id="modal" style="background-color:#013b63;color:white;font-size:14px !important">
        <div class="modal-header">
            <h4 class="modal-title" id="myModalLabel">Formulario de vehiculos</h4>
        </div>
        <div class="modal-body">
            <form id="form1" class="form-horizontal" role="form">
                <input type="hidden" id="id" name="id" />

                <div class="row-fluid">
                    <div class="span6">
                        <div class="control-group">
                            <label class="col-sm-3 control-label" for="Codigo">C&oacute;digo</label>
                            <div class="col-sm-9">
                                <input type="text" id="Codigo" name="Codigo" class="form-control" maxlength="10" disabled="disabled" />
                            </div>
                        </div>
                    </div>                  
                    <div class="span6">
                        <div class="control-group">
                            <label class="col-sm-3 control-label" for="Nombre">Nombre</label>
                            <div class="col-sm-9">
                                <input type="text" id="Nombre" name="Nombre" class="form-control" maxlength="150" />
                            </div>
                        </div>
                    </div>
                </div> 
                <br />           
                <div class="row-fluid">
                    <div class="span6">
                        <div class="control-group">
                            <label class="col-sm-3 control-label" for="Categoria">Categoria</label>
                            <div class="col-sm-9">
                                <input type="text" id="Categoria" name="Categoria" class="form-control" maxlength="150" />
                            </div>
                        </div>
                    </div>  
                    <div class="span6">
                        <div class="control-group">
                            <label class="col-sm-3 control-label" for="Agencia">Agencia</label>
                            <div class="col-sm-9">
                                <select id="Agencia" name="Agencia" class="form-control"></select>
                                <input type="hidden" id="hfAgencia" name="hfAgencia" />  
                            </div>
                        </div>
                    </div>      
                </div> 
                <br />      
                <div class="row-fluid">
                    <div class="span12">
                        <%--<div class="control-group">--%>
                            <label class="col-sm-2 control-label" for="Descripcion">Descripcion</label>
                            <div class="col-sm-10">
                                <input type="text" id="Descripcion" name="Descripcion" style ="width :100% !important;margin-left:0 !important" class="form-control" maxlength="30" />
                            </div>
                        <%--</div>--%>
                    </div>
                </div>      
                <hr style="border-color:#E5E5E5!important" />
                <div class="row-fluid" id="Div1">
                    <div class="span12">
                        <h4>Imagen</h4>
                        </div>
                </div>
                <%--<hr style="border-color:#E5E5E5!important" />--%>
                <br />
                <div class="row-fluid" id="dv_adjuntar">
                    <div class="span12">
                        <div class="control-group">
                            <label class="col-sm-1 control-label" for="Archivo">Archivo</label>
                            <div class="col-sm-6">
                                <input id="inputFile" type="file" multiple="multiple" style="margin-left: 25px !important; width: 100% !important; height: 30px;" />
                                <a class="UploadButton" id="UploadButton"></a>
                            </div>
                            <div class="col-sm-1">
                                <div id="InfoBox"></div>
                                <input id="btn_subir" type="button" value="Subir" onclick="Subir()" class="btn btn-sm btn-primary form-control" />
                            </div>
                        </div>
                    </div>
                </div>
                <div class="row-fluid" id="dv_adjuntos">
                    <div class="span12">
                        <div class="control-group">
                            <label class="col-sm-1 control-label" for="Archivo">Imagen</label>
                            <div class="col-sm-11" id="dv_archivos" style="text-align:left">
                                <hr class="" />
                            </div>
                        </div>
                    </div>
                </div>
                <br /><br /><br />
            </form>        
        </div>
        <div class="modal-footer">
            <button type="button" class="btn btn-default" data-dismiss="modal" onclick="modal.close()" id="btn_cerrar">Cerrar</button>
            <button type="button" class="btn btn-primary" id="btn_aceptar" onclick="if(Validar()){Guardar()}">Aceptar</button>
        </div>
    </div>
    <script type="text/javascript">
        $(function () {
            $('#form1').validate({
                rules: {
                    Nombre: {
                        required: true
                    },
                    Codigo: {
                        required: true
                    },
                    Categoria: {
                        required: true
                    },
                    Descripcion: {
                        required: true
                    }                    
                }
            });

            jQuery.validator.addMethod('select', function (value) {
                return (value != '0');
            }, "seleccione una opción");
        });
    </script>

    <div class="remodal" data-remodal-id="modalProduct">
        <div class="modal-header">
            <h4 class="modal-title">Seleccione el Producto</h4>
        </div>
        <div class="modal-body">
            <table id="tbl_ListingProducts" style="background-color :white !important;width: 100% !important" cellpadding="0" cellspacing="0" border="0" class="table table-bordered table-striped">
                <thead>
                    <tr>
                       <td data-hide="phone,tablet">Cod Proveedor</td>
                        <td data-hide="phone,tablet">Cod Sistema</td>
                        <td data-class="expand">Descripción</td>
                    </tr>
                </thead>
                <tbody>
                </tbody>
            </table>
        </div>
        <div class="modal-footer">
            <div class="alert alert-danger" id="dv_ErrorProduct"></div>
            <button type="button" class="btn btn-default" data-dismiss="modal" onclick="modalProduct.close(); modal.open();" id="btn_cerrarproducto">Cerrar</button>
            <button type="button" class="btn btn-success" onclick="productSelect();">Aceptar</button>
        </div>
    </div>

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
            <button type="button" class="btn btn-default" data-dismiss="deletemodal" onclick="deletemodal.close()">Cerrar</button>
            <button type="button" class="btn btn-danger" onclick="Eliminar()">Aceptar</button>
        </div>
    </div>

     <div class="remodal" data-remodal-id="modalnuevo" style="background-color:#013b63;color:white;font-size:14px !important">
            <div class="modal-header">
                <h4>Mensaje</h4>
            </div>
            <div class="modal-body">
                <p>Desea agregar un nuevo producto?</p>
            </div>
            <div class="modal-footer">
                <button type="button" class="btn btn-default" data-dismiss="modalnuevo" onclick="modalnuevo.close()">Cerrar</button>
                <button type="button" class="btn btn-success" onclick="modalnuevo.close();Nuevo()">Aceptar</button>
            </div>
        </div>
    <script type="text/javascript" language="javascript" src="js/lst_vehiculos.js?token=<% Response.Write(Replace(Format(Date.Now, "yyyyMMddHH:mm:ss"), ":", ""))%>"></script>
    <script>

        $(document).ready(function () {
            $("#dv_mensaje").hide();
            $("#dv_error").hide();
            $("#dv_ErrorProduct").hide();
            Cargar();
        });

    </script>
</asp:Content>
