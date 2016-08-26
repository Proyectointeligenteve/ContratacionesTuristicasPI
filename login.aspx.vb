
Partial Class login
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Me.IsPostBack Then
            Session.RemoveAll()
            'If Not IsNothing(Request.QueryString("nc")) Then
            '    If Val(Request.QueryString("nc").ToString) = 1 Then
            '        hf_mensaje.Value = "La clave ha sido actualizada"
            '    End If
            'End If
        End If
    End Sub

    Protected Sub btn_guardar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_guardar.Click
        Dim obj_usuario As New cls_usuarios(txt_usuario.Text)
        If obj_usuario.Id > 0 Then
            If obj_usuario.Clave.Trim.ToUpper = txt_clave.Text.Trim.ToUpper Then
                Dim obj_sesion As New cls_Sesion
                obj_sesion.Usuario = obj_usuario
                Session.Contents("obj_Session") = obj_sesion
                'If obj_usuario.cambiar_clave Then
                'Dim var_nomina As Integer = 0
                'If Not IsNothing(Request.QueryString("n")) Then
                '    var_nomina = Val(Request.QueryString("n").ToString)
                'End If
                'Response.Redirect("frm_usuario.aspx?id=" & obj_usuario.Id & "&cc=1&n=" & var_nomina)
                'End If
                'If Val(Request.QueryString("n")) > 0 Then
                '    Response.Redirect("pop_nomina.aspx?id=" & Val(Request.QueryString("n")))
                'Else
                Response.Redirect("principal.aspx")
                'End If
            Else
                dv_error.InnerText = "Usuario o clave inválida"
                hf_error.Value = 1
            End If
        Else
            dv_error.InnerText = "Usuario o clave inválida"
            hf_error.Value = 1
        End If
    End Sub

    Protected Sub lnk_olvido_clave_Click(sender As Object, e As EventArgs) Handles lnk_olvido_clave.Click
        If txt_usuario.Text.Trim.Length <= 0 Then
            dv_advertencia.InnerText = "Ingrese el nombre de usuario"
            hf_advertencia.Value = 1
            Exit Sub
        End If

        Dim obj_usuario As New cls_usuarios(txt_usuario.Text)
        If obj_usuario.Id > 0 Then
            Dim var_cuerpo As String = ""
            Dim var_msjerror As String = ""

            var_cuerpo = "Usted ha solicitado recuperar su clave: " & obj_usuario.Clave
            If Not ac_Funciones.EnviarCorreo(var_cuerpo, var_msjerror, "Recuperar clave", obj_usuario.email) Then
                dv_error.InnerText = var_msjerror
                hf_error.Value = 1
            Else
                dv_mensaje.InnerText = "Se ha enviado un mensaje a su correo electrónico con su clave."
                hf_mensaje.Value = 1
            End If
        Else
            dv_error.InnerText = "Usuario inválido"
            hf_error.Value = 1
            Exit Sub
        End If
    End Sub

End Class
