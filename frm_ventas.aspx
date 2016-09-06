<%@ Page Title="" Language="VB" MasterPageFile="principal.master" AutoEventWireup="false" CodeFile="frm_ventas.aspx.vb" Inherits="frm_ventas" %>

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
                <h4 class="modal-title" id="H1">Formulario de ventas</h4>
            </div>
            <div align="right">
                <button type="button" class="btn btn-default" data-dismiss="modal" onclick="window.location.href='lst_ventas.aspx'">Volver</button>
                <button type="button" class="btn btn-primary" onclick="if(validate()){save();}" id="btn_guardar">Guardar</button>
            </div>
        </div>

        <form id="form1" style="margin-top: 10px">
            <input type="hidden" id="hdn_id_pasajero" name="hdn_id_pasajero" value="0"/>
            <div class="espacio"></div>
                 
            <div class="row-fluid">
                <div class="span6">
                    <div class="control-group">
                        <label class="span2 control-label" for="Numero">Numero</label>
                        <div class="span10">
                            <input type="text" id="Numero" name="Numero" class="form-control" disabled="disabled" />
                        </div>
                    </div>
                </div>
                <div class="span6">
                    <div class="control-group">
                        <label class="span2 control-label" for="Sucursal">Sucursal</label>
                        <div class="span10">
                            <select id="Sucursal" name="Sucursal" class="form-control"></select>
                            <input type="hidden" id="hfSucursal" name="hfSucursal" value="0" /> 
                            <ul class="error"></ul>
                        </div>
                    </div>
                </div>
            </div>
            <br />
            <div class="row-fluid">
                <!--/span-->
                <div class="span6">
                    <div class="control-group">
                        <label class="span2 control-label" for="TipoViaje">Tipo Viaje</label>
                        <div class="span10">
                            <select id="TipoViaje" name="TipoViaje" class="form-control" onchange ="CargarFee()">
                                <option value="-1">Seleccione</option>
                                <option value="0">Nacional</option>
                                <option value="1">Internacional</option>
                            </select>
                            <ul class="error"></ul>
                        </div>
                    </div>
                </div>
                <div class="span6">
                    <div class="control-group">
                        <label class="span2 control-label" for="TipoVenta">Tipo Venta</label>
                        <div class="span10">
                            <select id="TipoVenta" name="TipoVenta" class="form-control" onchange ="CargarTipo()"></select>
                            <input type="hidden" id="hfTipoVenta" name="hfTipoVenta" value="0" /> 
                            <ul class="error"></ul>
                        </div>
                    </div>
                </div>
            </div>
            <br />
            <div class="row-fluid">
                <div class="span6">
                    <div class="control-group">
                        <label class="span2 control-label" for="TipoVendedor">Tipo Vendedor</label>
                        <div class="span10">
                            <select id="TipoVendedor" name="TipoVendedor" class="form-control" onchange ="CargarTipoVendedor()">
                                <option value ="0">Seleccione</option>
                                <option value ="1">Agencia</option>
                                <option value ="2">Freelance</option>
                                <option value ="3">Vendedor</option>
                            </select>
                            <ul class="error"></ul>
                        </div>
                    </div>
                </div>
            <!--/row-->
                <div class="span6 hide" id="div_vendedor">
                    <div class="control-group">
                        <label class="span2 control-label" for="Vendedor">Vendedor</label>
                        <div class="span10">
                            <select id="Vendedor" name="Vendedor" class="form-control" onchange ="CargarComisiones()"></select>
                            <input type="hidden" id="hfVendedor" name="hfVendedor" value="0" /> 
                            <ul class="error"></ul>
                        </div>
                    </div>
                </div>
                <div class="span6 hide" id="div_agencia">
                    <div class="control-group">
                        <label class="span2 control-label" for="Agencia">Agencia</label>
                        <div class="span10">
                            <select id="Agencia" name="Agencia" class="form-control" onchange ="CargarComisiones()"></select>
                            <input type="hidden" id="hfAgencia" name="hfAgencia" value="0" /> 
                            <ul class="error"></ul>
                        </div>
                    </div>
                </div>
                <div class="span6 hide">
                    <div class="control-group" id="div_freelance">
                        <label class="span2 control-label" for="Freelance">Freelance</label>
                        <div class="span10">
                            <select id="Freelance" name="Freelance" class="form-control" onchange ="CargarComisiones()"></select>
                            <input type="hidden" id="hfFreelance" name="hfFreelance" value="0" /> 
                            <ul class="error"></ul>
                        </div>
                    </div>
                </div>
                <!--/span-->
            </div>
            <br />
            <div class="row-fluid paquete">
                <div class="span10">
                    <div class="control-group">
                        <label class="span2 control-label" for="Paquete">Paquete</label>
                        <div class="span10">
                            <select id="Paquete" name="Paquete" class="form-control"></select>
                            <input type="hidden" id="hfPaquete" name="hfPaquete" value="0" /> 
                            <ul class="error"></ul>
                        </div>
                    </div>
                </div>
                <%--<div class="span6">
                    <div class="control-group">
                        <label class="span2 control-label" for="Habitacion">Habitacion</label>
                        <div class="span10">
                            <select id="Select2" name="Habitacion" class="form-control"></select>
                            <input type="hidden" id="Hidden2" name="hfHabitacion" value="0" /> 
                            <ul class="error"></ul>
                        </div>
                    </div>
                </div>--%>
            </div>
            <br />
            <div class="row-fluid hotel1">
                <div class="span6">
                    <div class="control-group">
                        <label class="span2 control-label" for="Hotel">Hotel</label>
                        <div class="span10">
                            <select id="Hotel" name="Hotel" class="form-control"></select>
                            <input type="hidden" id="hfHotel" name="hfHotel" value="0" /> 
                            <ul class="error"></ul>
                        </div>
                    </div>
                </div>
                <div class="span6">
                    <div class="control-group">
                        <label class="span2 control-label" for="Habitacion">Habitacion</label>
                        <div class="span10">
                            <select id="Habitacion" name="Habitacion" class="form-control"></select>
                            <input type="hidden" id="hfHabitacion" name="hfHabitacion" value="0" /> 
                            <ul class="error"></ul>
                        </div>
                    </div>
                <!--/span-->
                </div>
            </div>
            <br />
            <div class="row-fluid boleto">
                <div class="span6">
                    <div class="control-group">
                        <label class="span2 control-label" for="Aerolinea">Aerolinea</label>
                        <div class="span10">
                            <select id="Aerolinea" name="Aerolinea" class="form-control"></select>
                            <input type="hidden" id="hfAerolinea" name="hfAerolinea" value="0" /> 
                            <ul class="error"></ul>
                        </div>
                    </div>
                </div>
                <div class="span6">
                    <div class="control-group">
                        <label class="span2 control-label" for="Boleto">Boleto</label>
                        <div class="span10">
                            <input type="text" id="Boleto" name="Boleto" class="form-control" />
                            <ul class="error"></ul>
                        </div>
                    </div>
                <!--/span-->
                </div>
            </div>
            <br />
            <div class="row-fluid boleto">
                <div class="span6">
                    <div class="control-group">
                        <label class="span2 control-label" for="Sistema">Sistema</label>
                        <div class="span10">
                            <select id="Sistema" name="Sistema" class="form-control"></select>
                            <input type="hidden" id="hfSistema" name="hfSistema" value="0" /> 
                            <ul class="error"></ul>
                        </div>
                    </div>
                </div>
                <div class="span6">
                    <div class="control-group">
                        <label class="span2 control-label" for="Pantalla">Pantalla</label>
                        <div class="span10">
                            <input type="text" id="Pantalla" name="Pantalla" class="form-control" />
                            <ul class="error"></ul>
                        </div>
                    </div>
                <!--/span-->
                </div>
            </div>
            <br />
            <div class="row-fluid hotel">
                <div class="span6">
                    <div class="control-group">
                        <label class="span2 control-label" for="Dias">Cantidad Dias</label>
                        <div class="span10">
                            <input type="text" id="Dias" name="Dias" class="form-control" />
                            <ul class="error"></ul>
                        </div>
                    </div>
                </div>
                <div class="span6">
                    <div class="control-group">
                        <label class="span2 control-label" for="TipoPersona">Tipo Persona</label>
                        <div class="span10">
                            <select id="TipoPersona" name="TipoPersona" class="form-control">
                                <option value="0">Seleccione</option>
                                <option value="1">Adulto</option>
                                <option value="2">Niño(a)</option>
                                <option value="3">Infante</option>
                            </select>
                            <ul class="error"></ul>
                        </div>
                    </div>
                <!--/span-->
                </div>
            </div>
            <hr />
            <div class="row-fluid">
                <div class="span6">
                    <div class="control-group">
                        <label class="span2 control-label" for="Rif">Rif/Pasaporte</label>
                        <div class="span10">
                            <input type="text" id="Rif" name="Rif" class="form-control" onchange="buscarPasajero()" style="text-transform: uppercase;" maxlength="10"/>
                            <ul class="error"></ul>
                        </div>
                    </div>
                </div>
            <!--/row-->
            </div>
            <br />            
            <div class="row-fluid hotel">
                <div class="span6">
                    <div class="control-group">
                        <label class="span2 control-label" for="Nombre">Nombre</label>
                        <div class="span10">
                            <input type="text" id="Nombre" name="Nombre" class="form-control" />
                            <ul class="error"></ul>
                        </div>
                    </div>
                </div>
            <!--/row-->
                <div class="span6">
                    <div class="control-group">
                        <label class="span2 control-label" for="Apellido">Apellido</label>
                        <div class="span10">
                            <input type="text" id="Apellido" name="Apellido" class="form-control" />
                            <ul class="error"></ul>
                        </div>
                    </div>
                </div>
                <!--/span-->
            </div>
            <br />
            <div class="row-fluid hotel">
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
                        <label class="span2 control-label" for="Edad">Edad</label>
                        <div class="span10">
                            <input type="text" id="Edad" name="Edad" class="form-control caracter" maxlength="2"/>
                            <ul class="error"></ul>
                        </div>
                    </div>
                    <div class="control-group">
                        <label class="span2 control-label" for="TelefonoM">Telefono Movil</label>
                        <div class="span10">
                            <input type="text" id="TelefonoM" name="TelefonoM" class="form-control caracter" maxlength="12"/>
                            <ul class="error"></ul>
                        </div>
                    </div>
                    <div class="control-group">
                        <label class="span2 control-label" for="Email">Email</label>
                        <div class="span10">
                            <input type="text" id="Email" name="Email" class="form-control" maxlength="50"/>
                            <ul class="error"></ul>
                        </div>
                    </div>
                <!--/span-->
                </div>  
            </div>
            <div class="row-fluid">
                <div class="span6">
                    <div class="control-group">
                        <label class="span2 control-label" for="FechaInicio">Fecha Inicio</label>
                        <div class="span10">
                            <input type="text" id="FechaInicio" name="FechaInicio" class="form-control" />
                            <ul class="error"></ul>
                        </div>
                    </div>
                </div>
                <div class="span6">
                    <div class="control-group">
                        <label class="span2 control-label" for="FechaFin">Fecha Fin</label>
                        <div class="span10">
                            <input type="text" id="FechaFin" name="FechaFin" class="form-control" />
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
                        <label class="span2 control-label" for="Destino">Destino</label>
                        <div class="span10">
                            <select id="Destino" name="Destino" class="form-control" onchange ="CargarCiudades()"></select>
                            <input type="hidden" id="hfDestino" name="hfDestino" value="0" /> 
                            <ul class="error"></ul>
                        </div>
                    </div>
                </div>
            </div>            
            <hr />
            <div class="row-fluid">
                <div class="span6">
                    <div class="control-group">
                        <label class="span2 control-label" for="Moneda">Moneda</label>
                        <div class="span10">
                            <select id="Moneda" name="Moneda" class="form-control" onchange ="Calcular()"></select>
                            <input type="hidden" id="hfMoneda" name="hfMoneda" value="0" /> 
                            <ul class="error"></ul>
                        </div>
                    </div>
                </div>
                <div class="span6">
                    <div class="control-group">
                        <label class="span2 control-label" for="Tarifa">Tarifa</label>
                        <div class="span10">
                            <input type="text" id="Tarifa" name="Tarifa" class="form-control decimal" onchange ="Calcular()"/>
                            <ul class="error"></ul>
                        </div>
                    </div>
                </div>
            </div>
                    
            <br />
            <div class="row-fluid">
                <div class="span6">
                    <div class="control-group">
                        <label class="span2 control-label" for="Tax">Tax</label>
                        <div class="span10">
                            <input type="text" id="Tax" name="Tax" class="form-control decimal" onchange ="Calcular()"/>
                            <ul class="error"></ul>
                        </div>
                    </div>
                </div>
                <div class="span6">
                    <div class="control-group">
                        <label class="span2 control-label" for="Total">Total</label>
                        <div class="span10">
                            <input type="text" id="Total" name="Total" class="form-control decimal" disabled="disabled"/>
                            <ul class="error"></ul>
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
         
        <br />
        
                  
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
    
    <script type="text/javascript" src="js/frm_ventas.js"></script>
    <script type="text/javascript">
        $(document).ready(function ()
        {
            $("#dv_Error").hide();
            $("#dv_Message").hide();
            //$("#dv_Error2").hide();
            //$("#dv_Message2").hide();
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
