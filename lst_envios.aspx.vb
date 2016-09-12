Imports System.Data
Imports System.Data.SqlClient
Imports System.Web.Services
Imports Newtonsoft.Json
Imports Newtonsoft.Json.Linq

Partial Class lst_envios
    Inherits System.Web.UI.Page

    Dim obj_Session As cls_Sesion

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If IsNothing(Session.Contents("obj_Session")) Then
            If Not IsNothing(Request.QueryString("fn")) Then
                Response.Clear()
                Response.ClearHeaders()
                Response.ClearContent()
                Response.Write("{" & Chr(34) & "error" & Chr(34) & ":" & Chr(34) & "-1" & Chr(34) & "}")
                Response.End()
            Else
                Response.Redirect("info.aspx", True)
            End If
        End If
        obj_Session = Session.Contents("obj_Session")

        If Not IsNothing(Request.QueryString("fn")) Then
            Dim var_fn As String = Request.QueryString("fn").ToString
            Select Case var_fn
                Case "permisos"
                    Permisos()
                Case "cargar"
                    Cargar()
                Case "eliminar"
                    Eliminar()
                Case "GuardarSeguimiento"
                    GuardarSeguimiento()
                Case "Seguimiento"
                    Seguimiento()
            End Select
        End If
    End Sub

    Sub Permisos()
        Dim var_error As String = ""
        Dim obj_sb As New StringBuilder
        Try
            If Not New cls_permisos(obj_Session.Usuario.Id, New cls_modulos("envios").Id, New cls_acciones("ver").Id).permiso Then
                Dim obj_log As New cls_logs
                obj_log.ComentarioLog = "Listado de envios"
                obj_log.FechaLog = Now
                obj_log.id_Menu = New cls_modulos("envios").Id
                obj_log.id_Usuario = obj_Session.Usuario.Id
                obj_log.idAccion = New cls_acciones("ver").Id
                obj_log.ResultadoLog = False
                obj_log.InsertarLog()

                Response.Clear()
                Response.ClearHeaders()
                Response.ClearContent()
                Response.Write("{" & Chr(34) & "error" & Chr(34) & ":" & Chr(34) & "-1" & Chr(34) & "}")
                Response.End()
            Else

                Dim obj_log As New cls_logs
                obj_log.ComentarioLog = "Listado de envios"
                obj_log.FechaLog = Now
                obj_log.id_Menu = New cls_modulos("envios").Id
                obj_log.id_Usuario = obj_Session.Usuario.Id
                obj_log.idAccion = New cls_acciones("ver").Id
                obj_log.ResultadoLog = True
                obj_log.InsertarLog()

            End If

            obj_sb.Append("," & Chr(34) & "Agregar" & Chr(34) & ":" & Chr(34) & IIf(New cls_permisos(obj_Session.Usuario.Id, New cls_modulos("envios").Id, New cls_acciones("agregar").Id).permiso, 1, 0) & Chr(34) & "")
            obj_sb.Append("," & Chr(34) & "Ver" & Chr(34) & ":" & Chr(34) & IIf(New cls_permisos(obj_Session.Usuario.Id, New cls_modulos("envios").Id, New cls_acciones("ver").Id).permiso, 1, 0) & Chr(34) & "")
            obj_sb.Append("," & Chr(34) & "Editar" & Chr(34) & ":" & Chr(34) & IIf(New cls_permisos(obj_Session.Usuario.Id, New cls_modulos("envios").Id, New cls_acciones("editar").Id).permiso, 1, 0) & Chr(34) & "")
            obj_sb.Append("," & Chr(34) & "Eliminar" & Chr(34) & ":" & Chr(34) & IIf(New cls_permisos(obj_Session.Usuario.Id, New cls_modulos("envios").Id, New cls_acciones("eliminar").Id).permiso, 1, 0) & Chr(34) & "")

        Catch ex As Exception
            var_error = ex.Message
        End Try

        Response.ContentType = "application/json"
        Response.Clear()
        Response.ClearHeaders()
        Response.ClearContent()
        If var_error.Trim.Length > 0 Then
            Response.Write("{" & Chr(34) & "rslt" & Chr(34) & ":" & Chr(34) & "error" & Chr(34) & "," & Chr(34) & "msj" & Chr(34) & ":" & Chr(34) & var_error & Chr(34) & "}")
        Else
            Response.Write("{" & Chr(34) & "rslt" & Chr(34) & ":" & Chr(34) & "exito" & Chr(34) & "," & Chr(34) & "msj" & Chr(34) & ":" & Chr(34) & "" & Chr(34) & obj_sb.ToString & "}")
        End If
        Response.End()
    End Sub

    Sub Cargar()
        Dim var_error As String = ""
        Dim var_estatus As String = 1

        If Not IsNothing(Request.QueryString("v")) Then
            var_estatus = Request.QueryString("v")
        End If

        Dim obj_dt_int As System.Data.DataTable
        If var_estatus = 0 Then
            obj_dt_int = cls_envios.ConsultaPorEntregar(var_error)
        ElseIf var_estatus = 1 Then
            obj_dt_int = cls_envios.ConsultaTransito(var_error)
        ElseIf var_estatus = 2 Then
            obj_dt_int = cls_envios.ConsultaEntregados(var_error)
        ElseIf var_estatus = 3 Then
            obj_dt_int = cls_envios.ConsultaExtraviados(var_error)
        ElseIf var_estatus = 4 Then
            obj_dt_int = cls_envios.ConsultaAnulados(var_error)
        Else 'Todos
            obj_dt_int = cls_envios.Consulta(var_error)
        End If
        Dim var_json As String = JsonConvert.SerializeObject(obj_dt_int)

        Response.Clear()
        Response.ClearHeaders()
        Response.ClearContent()
        If var_error.Trim.Length > 0 Then
            Response.Write("{ " & Chr(34) & "error" & Chr(34) & ":" & Chr(34) & "-2" & Chr(34) & "," & Chr(34) & "msj" & Chr(34) & ":" & Chr(34) & var_error & Chr(34) & "}")
        Else
            Response.Write("{ " & Chr(34) & "aaData" & Chr(34) & ":" & var_json & "}")
        End If
        Response.End()
    End Sub

    Sub Eliminar()
        Dim var_sr = New System.IO.StreamReader(Request.InputStream)
        Dim var_data As JObject = JObject.Parse(var_sr.ReadToEnd)
        Dim var_error As String = ""

        Response.ContentType = "application/json"
        Response.Clear()
        Response.ClearHeaders()
        Response.ClearContent()
        If Not cls_envios.Eliminar(CInt(var_data("id").ToString), obj_Session.Usuario, var_error) Then
            Response.Write("{" & Chr(34) & "rslt" & Chr(34) & ":" & Chr(34) & "error" & Chr(34) & "," & Chr(34) & "msj" & Chr(34) & ":" & Chr(34) & var_error & Chr(34) & "}")
        Else
            Response.Write("{" & Chr(34) & "rslt" & Chr(34) & ":" & Chr(34) & "exito" & Chr(34) & "," & Chr(34) & "msj" & Chr(34) & ":" & Chr(34) & Chr(34) & "}")
        End If
        Response.End()
    End Sub

    Private Sub Seguimiento()
        Dim var_id As String = Request.QueryString("id").ToString
        Dim var_error As String = ""

        Dim obj_dt_int As System.Data.DataTable = cls_envios_seguimientos.Consulta(var_id, var_error)

        Response.Clear()
        Response.ClearHeaders()
        Response.ClearContent()

        Dim var_json As String = ""
        var_json = JsonConvert.SerializeObject(obj_dt_int)
        If var_error.Trim.Length > 0 Then
            Response.Write("{ " & Chr(34) & "error" & Chr(34) & ":" & Chr(34) & "-2" & Chr(34) & "," & Chr(34) & "msj" & Chr(34) & ":" & Chr(34) & var_error & Chr(34) & "}")
        Else
            Response.Write("{ " & Chr(34) & "aaData" & Chr(34) & ":" & var_json & "}")
        End If
        Response.End()
    End Sub

    Sub GuardarSeguimiento()
        Dim var_sr = New System.IO.StreamReader(Request.InputStream)
        Dim var_data As JObject = JObject.Parse(var_sr.ReadToEnd)

        Dim obj_seguimiento As New cls_envios_seguimientos()
        obj_seguimiento.Idenvio = var_data("Idenvio")
        obj_seguimiento.observacion = var_data("Observacion")
        obj_seguimiento.fecha = Now
        obj_seguimiento.fecha_reg = Now
        obj_seguimiento.id_usuario_reg = obj_Session.Usuario
        obj_seguimiento.estatus = var_data("Estatus")
        Response.ContentType = "application/json"
        Response.Clear()
        Response.ClearHeaders()
        Response.ClearContent()
        Dim var_error As String = ""
        If Not obj_seguimiento.Actualizar(var_error) Then
            Response.Write("{" & Chr(34) & "rslt" & Chr(34) & ":" & Chr(34) & "error" & Chr(34) & "," & Chr(34) & "msj" & Chr(34) & ":" & Chr(34) & var_error & Chr(34) & "}")
        Else
            Response.Write("{" & Chr(34) & "rslt" & Chr(34) & ":" & Chr(34) & "exito" & Chr(34) & "," & Chr(34) & "msj" & Chr(34) & ":" & Chr(34) & "" & Chr(34) & "}")
        End If
        Response.End()
    End Sub
End Class
