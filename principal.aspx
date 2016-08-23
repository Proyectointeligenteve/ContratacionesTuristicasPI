<%@ Page Title="" Language="VB" MasterPageFile="~/principal.master" AutoEventWireup="false" CodeFile="principal.aspx.vb" Inherits="principal" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    
    <link href="css/principal.css" rel="stylesheet"/>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

                <table style="width:100%; margin-top:150px">
                    <tr>
                        <td>
                            <center>
                                <img src="img/logo_ContratacionesTuristicas.png" style="width: 64px" />
                            </center>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <center>
                                <h3>Contrataciones Turisticas</h3>
                                <div class="btn btn-default" id="boton-envios" style="color:white" onclick="windows.location.href(~/lst_envios.aspx)">Envios</div>
                            </center>
                        </td>
                    </tr>
                </table>
</asp:Content>

