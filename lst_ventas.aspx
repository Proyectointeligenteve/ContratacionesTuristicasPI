﻿<%@ Page Title="" Language="VB" MasterPageFile="principal.master" AutoEventWireup="false" CodeFile="lst_ventas.aspx.vb" Inherits="lst_ventas" %>

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
            <span style="font-size: 14px;color:white">LISTADO DE VENTAS</span><hr />            
        </div>
        <div class="izq"><select id="vista_estatus" class="form-control" style="margin:0 !important; width:150px !important" onchange="CargarListado()"><option value ="1">Ver Activos</option><option value ="2">Ver Inactivos</option><option value ="3">Ver Todos</option></select></div>
        <div class="hr">
            <hr />
        </div>
        <div class="der">
            <div class="btn-group">
                <img src='img/loading2.gif' class="loading" />
                <button class="btn hide"  id="btn_agregar" onclick="Nuevo();"><span class="glyphicon glyphicon-plus"></span>&nbsp;Nuevo</button>
                <button class="btn hide"  id="btn_ver" onclick="Ver();"><span class="glyphicon glyphicon-eye-ope"></span>&nbsp;Ver</button>
                <button class="btn hide"  id="btn_editar" onclick="Editar();"><span class="glyphicon glyphicon-edit"></span>&nbsp;Editar</button>
                <button class="btn hide"  id="btn_anular" onclick="ConfirmarAnular();"><span class="glyphicon glyphicon-ban-circle"></span>&nbsp;Activar/Inactivar</button>
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
                    <td data-class="expand">Numero</td>
                    <td data-class="phone,tablet">Fecha</td>
                    <td data-class="phone,tablet">Cliente</td>
                    <td data-class="phone,tablet">Identificación</td>
                    <td data-class="phone,tablet">Destino</td>
                    <td data-class="phone,tablet">Fecha Inicio</td>
                    <td data-class="phone,tablet">Fecha Fin</td>
                    <td data-class="phone,tablet">Vendedor</td>
                    <td data-class="phone,tablet">Total</td>
                    <td data-class="phone,tablet">Estatus</td>
                </tr>
            </thead>
            <tbody>
            </tbody>
        </table>
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
            <button type="button" class="btn btn-default" data-dismiss="modal" onclick="deletemodal.close()">Cerrar</button>
            <button type="button" class="btn btn-danger" onclick="Eliminar()">Aceptar</button>
        </div>
    </div>
        
    <script type="text/javascript" language="javascript" src="js/lst_ventas.js?token=<% Response.Write(Replace(Format(Date.Now, "yyyyMMddHH:mm:ss"), ":", ""))%>"></script>
    <script>

        $(document).ready(function () {
            $("#dv_mensaje").hide();
            $("#dv_error").hide();
            Cargar();
        });

    </script>
</asp:Content>
