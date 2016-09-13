<%@ Page Title="" Language="VB" MasterPageFile="~/principal.master" AutoEventWireup="false" CodeFile="lst_envios.aspx.vb" Inherits="lst_envios" %>

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
            <span style="font-size: 14px;color:white">LISTADO DE ENVIOS</span><hr />            
        </div>
        <div class="izq"><select id="vista_estatus" class="form-control" style="margin:0 !important; width:180px !important" onchange="CargarListados()">
            <option value ="0">Ver Por Entregar</option>
            <option value ="1">Ver En Transito</option>
            <option value ="2">Ver Entregados</option>
            <option value ="3">Ver Extraviados</option>
            <option value ="4">Ver Todos</option>
            </select></div>
        <div class="hr">
            <hr />
        </div>
        <div class="der">
            <div class="btn-group">
                <img src='img/loading2.gif' class="loading" />
                <button class="btn hide" id="btn_agregar" onclick="Nuevo();"><span class="glyphicon glyphicon-plus"></span>&nbsp;Nuevo</button>
                <button class="btn hide" id="btn_editar" onclick="Editar();"><span class="glyphicon glyphicon-edit"></span>&nbsp;Editar</button>
                <button class="btn hide" id="btn_actualizar" onclick="Seguimiento();"><span class="glyphicon glyphicon-list"></span>&nbsp;Actualizar</button>
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
    <div class="container" style="margin-top: 10px">
        <table id="tbDetails" cellpadding="0" cellspacing="0" border="0" class="table table-bordered table-striped" style ="background-color :white !important">
            <thead>
                <tr>
                    <td  data-class="expand">Codigo</td>
                    <td data-class="phone,tablet">Fecha</td>
                    <td data-class="phone,tablet">Emisor</td>
                    <td data-class="phone,tablet">Receptor</td>
                    <td data-class="phone,tablet">Pais Receptor</td>
                    <td data-class="phone,tablet">Ciudad Receptor</td>
                    <td data-class="phone,tablet">Costo</td>
                    <td data-class="phone,tablet">Estatus</td>
                </tr>
            </thead>
            <tbody>
            </tbody>
        </table>
    </div>
    <div class="remodal" data-remodal-id="modalseguimiento" style="background-color:#013b63;color:white;font-size:14px !important">
       <div class="modal-header">
           <h4 class="modal-title" id="H2">Seguimiento del Envio</h4>
            <div class="text-right"><button class="btn hide" id="Agregar_Seguimiento" onclick="NuevoSeguimiento();"><span class="glyphicon glyphicon-plus"></span>&nbsp;Agregar</button></div>
       </div>
       <div class="modal-body">

           <table style="width: 100% !important" id="tbSeguimiento" cellpadding="0" cellspacing="0" border="0" class="table table-bordered table-striped">
               <thead>
                   <tr>
                       <td  data-class="expand">Fecha</td>
                       <td  data-class="expand">Estatus</td>
                       <td  data-hide="phone,tablet">Observacion</td>
                       <td  data-hide="phone,tablet">Usuario</td>
                   </tr>
               </thead>
               <tbody>
               </tbody>
           </table>
       </div>
       <div class="modal-footer">
           <div class="alert alert-danger" id="dv_error_seg" name="dv_error_seg">
               </div>
           <div class="alert alert-success" id="dv_mensaje_seg" name="dv_mensaje_seg">
           </div>
           <img src='img/loading2.gif' class="loading" />
           <button type="button" class="btn btn-default" data-dismiss="modal" onclick="modalseguimiento.close()" id="btn_cerrarseguimiento">Cerrar</button>
       </div>
   </div>

   <div class="remodal" data-remodal-id="modalnuevoseg" style="background-color:#013b63;color:white;font-size:14px !important">

        <div class="modal-header">
            <h4 class="modal-title" id="myModalLabel">Nuevo Seguimiento</h4>
        </div>
        <div class="modal-body">
            <form id="form1" class="form-horizontal" role="form">
                <input type="hidden" id="id" name="id" />
                <input type="hidden" id="hd_estatus" name="hd_estatus" />

                <div class="row-fluid">
                    <div class="span12">
                        <div class="control-group">
                            <label class="col-sm-1 control-label" for="Fecha">Fecha</label>
                            <div class="col-sm-11">
                                <input type="text" id="Fecha" name="Fecha" class="form-control" />
                            </div>
                        </div>
                    </div>
                    <!--/span-->

                </div>

                <div class="row-fluid">
                    <div class="span12">
                        <div class="control-group">
                            <label class="col-sm-1 control-label" for="Estatus">Estatus</label>
                            <div class="col-sm-11">
                                <select id="Estatus" name="Estatus" class="form-control">
                                    <option value="0">Por Entregar</option>
                                    <option value="1">En transito</option>
                                    <option value="2">Entregados</option>
                                    <option value="3">Extraviados</option>
                                </select>
                            </div>
                        </div>
                    </div>
                    <!--/span-->

                </div>

                <div class="row-fluid">
                    <div class="span12">
                        <div class="control-group">
                            <label class="col-sm-1 control-label" for="Observacion">Observacion</label>
                            <div class="col-sm-11">
                                <textarea rows="4" id="Observacion" name="Observacion" class="form-control" style="width:100% !important" ></textarea>
                            </div>
                        </div>
                    </div>
                    <!--/span-->

                </div>

                 <script type="text/javascript">
                     $(function () {
                         $('#form1').validate({
                             rules: {
                                 Observacion: {
                                     required: true
                                 }
                             }
                         });
                     });
        </script>
            </form>
        </div>
        <div class="modal-footer">
            <div class="alert alert-danger" id="dv_error_nuevoseg" name="dv_error_nuevoseg"></div>
             <img src='img/loading2.gif' class="loading" />
            <button type="button" class="btn btn-default" data-dismiss="modal" onclick="modalnuevoseg.close();Seguimiento();" id="btn_cerrarnuevoseg">Cerrar</button>
            <button type="button" class="btn btn-primary" onclick="if(ValidarSeguimiento()){GuardarSeguimiento()}">Aceptar</button>
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
    <script type="text/javascript" language="javascript" src="js/lst_envios.js"></script>
    <script>

        $(document).ready(function () {
            $("#dv_mensaje").hide();
            $("#dv_error").hide();
            $("#dv_error_seg").hide();
            $("#dv_mensaje_seg").hide();
            $("#dv_error_nuevoseg").hide();
            Cargar();
        });

    </script>
</asp:Content>
