<%@ Page Title="" Language="VB" MasterPageFile="~/principal.master" AutoEventWireup="false" CodeFile="principal.aspx.vb" Inherits="principal2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    
    <link href="css/principal.css" rel="stylesheet"/>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <style>

        @media (min-width:768px) {
            body {
                padding-top: 50px !important;
            }
        }
    </style>
    <div class="bs-docs-header" id="content">
        <div class="container">
            <div>
                <h2>Sistema Administrativo Integral</h2>
            </div>
            <br />
            <div class="hide">
                <input type="button" class="btn btn-warning" value="COTIZAR" onclick="window.location.href = 'frm_cotizaciones.aspx'" />
                <input type="button" class="btn btn-warning" value="PEDIDO" onclick="window.location.href = 'frm_pedidos.aspx'" />
                <input type="button" class="btn btn-warning" value="RECIBO" onclick="window.location.href = 'frm_recibo.aspx'" />
                <input type="button" class="btn btn-warning" value="FACTURA" onclick="window.location.href = 'frm_facturas.aspx'" />
                <input type="button" class="btn btn-warning" value="ORDEN DE COMPRA" onclick="window.location.href = 'frm_ordenes_compras.aspx'" />  
                <input type="button" class="btn btn-warning" value="COMPRA" onclick="window.location.href = 'frm_compras.aspx'" />   
                <input type="button" class="btn btn-warning" value="INVENTARIO" onclick="window.location.href = 'lst_inventarios.aspx'" />  
            </div>
        </div>
    </div>
</asp:Content>

