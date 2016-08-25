﻿<%@ Page Title="" Language="VB" MasterPageFile="~/Principal.master" AutoEventWireup="false" CodeFile="lst_freelances.aspx.vb" Inherits="lst_freelances" %>

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
            <br />
            <div style ="width :100%">
                <div class ="row-fluid">
                    <div class="span10">
                        <span style="font-size: 14px;color:white"><b>LISTADO DE FREELANCES</b></span>
                    </div> 
                    <div class="span2">
                        <img src="img/logo.png" width="32px" />
                    </div>
                </div>
            </div>
            <hr />
        <div class="izq">
            <select id="vista_estatus" class="form-control" style="margin:0 !important; width:150px !important" onchange="CargarListado()"><option value ="1">Ver Activos</option><option value ="2">Ver Inactivos</option><option value ="3">Ver Todos</option></select></div>
        <div class="hr">
            <br />
        </div>
        <div class="der">
            <div class="btn-group">
                <img src='img/loading2.gif' class="loading" />
                <button class="btn hide" id="btn_agregar" onclick="Nuevo();"><span class="glyphicon glyphicon-plus"></span>&nbsp;Nuevo</button>
                <button class="btn hide" id="btn_editar" onclick="Editar();"><span class="glyphicon glyphicon-edit"></span>&nbsp;Editar</button>
                <button class="btn hide" id="btn_anular" onclick="ConfirmarAnular();"><span class="glyphicon glyphicon-edit"></span>&nbsp;Activar/Inactivar</button>
                <button class="btn hide" id="btn_eliminar" onclick="ConfirmarEliminar();"><span class="glyphicon glyphicon-remove"></span>&nbsp;Eliminar</button>
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
        <table style= "font-size: 10px"  id="tbDetails" cellpadding="0" cellspacing="0" border="0" class="table table-responsive table-striped table-bordered">
            <thead>
                <tr>
                    <td  data-class="expand">Nombre</td>
                    <td  data-class="phone,tablet">Rif</td>
                    <td  data-class="phone,tablet">Telefono movil</td>
                    <td  data-class="phone,tablet">Email</td>
                    <td  data-class="phone,tablet">Estatus</td>
                </tr>
            </thead>
            <tbody>
            </tbody>
        </table>
    </div>
       <div class="remodal" data-remodal-id="modal">
        <div class="modal-header">
            <h4 class="modal-title" id="myModalLabel">Formulario de Freelances</h4>
        </div>
        <div class="modal-body">
            <form id="form1" class="form-horizontal" role="form">
                <input type="hidden" id="id" name="id" />
                <div class="row-fluid">
                    <div class="span6">
                        <div class="control-group">
                            <label class="col-sm-2 control-label" for="Nombre">Nombre</label>
                            <div class="col-sm-10">
                                <input type="text" id="nombre" name="Nombre" class="form-control" maxlength="50" />
                            </div>
                        </div>
                    </div>
                    <div class="span6">
                        <div class="control-group">
                            <label class="col-sm-2 control-label" for="rif">Rif</label>
                            <div class="col-sm-10">
                                <input type="text" id="rif" name="rif" class="form-control" maxlength="10" />
                            </div>
                         </div>
                    </div>
                    <div class="row-fluid">
                        <div class="span6">
                            <label class="col-sm-2 control-label" for="Direccion">Direccion</label>
                            <div class="col-sm-10">
                                <input type="text" id="direccion" name="direccion" class="form-control"/>
                            </div>
                        </div>
                        <div class="span6">
                            <label class="col-sm-2 control-label" for="Telefonofijo">Telefono fijo</label>
                            <div class="col-sm-10">
                                <input type="text" id="telefono_fijo" name="telefono_fijo" class="form-control" maxlength="13" />
                            </div>
                        </div>
                    </div>
                    <div class="row-fluid">
                        <div class="span6">
                            <label class="col-sm-2 control-label" for="Telefonomovil">Telefono movil</label>
                            <div class="col-sm-10">
                                <input type="text" id="telefono_movil" name="telefono_movil" class="form-control" maxlength="13" />
                            </div>
                        </div>
                        <div class="span6">
                            <label class="col-sm-2 control-label" for="Email">Email</label>
                            <div class="col-sm-10">
                                <input type="text" id="email" name="email" class="form-control" maxlength="150" />
                            </div>
                        </div>
                    </div>
                <div class="modal-footer">
                    <div class="btn btn-default" for="Aceptar" onclick="if(Validar()){Guardar()}">Aceptar</div>
                    <div class="btn btn-default" for="Cancelar" onclick="modal.close()">Cancelar</div>
                </div>
            </div>
          </form>
            <br />
            <hr />
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
            <button type="button" class="btn btn-danger" onclick="eliminar()">Aceptar</button>
        </div>
    </div>
           <div class="remodal" data-remodal-id="anularmodal">
        <div class="modal-header">
            <h4>Anular</h4>
        </div>
        <div class="modal-body">
            <p>Estas seguro que deseas anular el registro?</p>
        </div>
        <div class="modal-footer">
            <img src='img/loading2.gif' class="loading" />
            <button type="button" class="btn btn-default" data-dismiss="modal" onclick="anularmodal.close()">Cerrar</button>
            <button type="button" class="btn btn-danger" onclick="anular()">Aceptar</button>
        </div>
    </div>
    <script type="text/javascript" language="javascript" src="js/lst_freelances.js"></script>
    <script>

        $(document).ready(function () {
            $("#dv_mensaje").hide();
            $("#dv_error").hide();
            Cargar();
        });

    </script>
</asp:Content>