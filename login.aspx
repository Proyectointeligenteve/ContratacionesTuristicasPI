<%@ Page Language="VB" AutoEventWireup="false" CodeFile="login.aspx.vb" Inherits="login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="X-UA-Compatible" content="IE=edge"/>
    <meta name="viewport" content="width=device-width, initial-scale=1, maximum-scale=1, user-scalable=no"/>
    <link rel="shortcut icon" href="ico/favicon.ico"/>
    <title>.::ContratacionesTuristicas::.</title>
    <link href="css/bootstrap.min.css" rel="stylesheet"/>
    <link href="css/login.css" rel="stylesheet"/>
    <style>
        .pnl_login {
            padding: 20px;
            background-color: #fff;
            border: 5px solid #00233e;
            /*-webkit-border-radius: 8px;
            -moz-border-radius: 8px;
            border-radius: 8px;
            -webkit-box-shadow: inset 0 1px 1px rgba(0, 0, 0, 0.05);
            -moz-box-shadow: inset 0 1px 1px rgba(0, 0, 0, 0.05);
            box-shadow: inset 0 1px 1px rgba(0, 0, 0, 0.05);*/
        }
        
    </style>
    <!-- HTML5 shim and Respond.js IE8 support of HTML5 elements and media queries -->
    <!--[if lt IE 9]>
      <script src="https://oss.maxcdn.com/libs/html5shiv/3.7.0/html5shiv.js"></script>
      <script src="https://oss.maxcdn.com/libs/respond.js/1.4.2/respond.min.js"></script>
    <![endif]-->
</head>
<body>
    <div class="container" align="center">
        <form id="Form1" class="form-signin" role="form" runat="server">

            <div class="pnl_login">
                <table style="width:100%;">
                    <tr>
                        <td>
                            <center>
                            <img src="img/logo.png" style="width: 64px" />
                                </center>
                        </td>
                    </tr>
                </table>
                <br />
                <table style="width:100%;">
                    <tr>
                        <td>
                            <span style="font-size:18px; color:#c3c3c3;">Inicio de sesión</span>
                        </td>
                    </tr>
                </table>
                <hr />
                <asp:TextBox ID="txt_usuario" runat="server" type="text" class="form-control" placeholder="Usuario" required autofocus></asp:TextBox>
                <asp:TextBox ID="txt_clave" runat="server" type="password" class="form-control" placeholder="Clave" required TextMode="Password"></asp:TextBox>
                <hr />
                <asp:LinkButton ID="lnk_olvido_clave" runat="server" class="btn btn-lg btn-link hide">Olvido su clave</asp:LinkButton>
                <asp:Button ID="btn_salir" runat="server" Text="Salir" class="btn btn-danger" onclientclick="self.close();return false;" UseSubmitBehavior="False" />
                <asp:Button ID="btn_guardar" runat="server" Text="Entrar" class="btn btn-primary" />
                <div class="alert alert-danger" runat="server" id="dv_error">
                </div>
                <div class="alert alert-success" runat="server" id="dv_mensaje">
                </div>
                <div class="alert alert-warning" runat="server" id="dv_advertencia">
                </div>
                <asp:HiddenField ID="hf_advertencia" runat="server" Value="" />
                <asp:HiddenField ID="hf_error" runat="server" Value="" />
                <asp:HiddenField ID="hf_mensaje" runat="server" Value="" />
            </div>
        </form>

    </div>

    <script src="js/jquery.min.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            var showerr = $("#hf_error");
            //la funcion trim elimina los espacios extras en el texto
            if ($.trim(showerr.val()) == "") { $("#dv_error").hide(); }
            else { $("#dv_error").show(); showerr.val(""); }

            var showmsg = $("#hf_mensaje");
            if ($.trim(showmsg.val()) == "") { $("#dv_mensaje").hide(); }
            else { $("#dv_mensaje").show(); showmsg.val(""); }

            var showadv = $("#hf_advertencia");
            if ($.trim(showadv.val()) == "") { $("#dv_advertencia").hide(); }
            else { $("#dv_advertencia").show(); showadv.val(""); }
        });
    </script>
</body>
</html>
