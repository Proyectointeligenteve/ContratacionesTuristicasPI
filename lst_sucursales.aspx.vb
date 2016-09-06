Imports System.Data
Imports System.Data.SqlClient
Imports System.Web.Services
Imports Newtonsoft.Json
Imports Newtonsoft.Json.Linq

Partial Class lst_sucursales
    Inherits System.Web.UI.Page
    Dim obj_Session As cls_Sesion
    Dim obj_marca As cls_sucursales

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If IsNothing(Session.Contents("obj_Session")) Then
            Response.Redirect("info.aspx", True)
        End If
        obj_Session = Session.Contents("obj_Session")

        If Not IsNothing(Request.QueryString("fn")) Then
            Dim var_fn As String = Request.QueryString("fn").ToString
            Select Case var_fn
                Case "permisos"
                    Permisos()
                Case "cargar"
                    Cargar()
                Case "guardar"
                    Guardar()
                Case "editar"
                    Editar()
                Case "Anular"
                    Anular()
                Case "eliminar"
                    Eliminar()

                Case "validar_sucursal"
                    validarSucursal()
            End Select
        End If
    End Sub

    Sub Permisos()
        Dim var_error As String = ""
        Dim obj_sb As New StringBuilder
        Try
            If Not New cls_permisos(obj_Session.Usuario.Id, New cls_modulos("sucursales").Id, New cls_acciones("ver").Id).permiso Then
                Dim obj_log As New cls_logs
                obj_log.ComentarioLog = "Listado de sucursales"
                obj_log.FechaLog = Now
                obj_log.id_Menu = New cls_modulos("sucursales").Id
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
                obj_log.ComentarioLog = "Listado de sucursales"
                obj_log.FechaLog = Now
                obj_log.id_Menu = New cls_modulos("sucursales").Id
                obj_log.id_Usuario = obj_Session.Usuario.Id
                obj_log.idAccion = New cls_acciones("ver").Id
                obj_log.ResultadoLog = True
                obj_log.InsertarLog()

            End If

            obj_sb.Append("," & Chr(34) & "Agregar" & Chr(34) & ":" & Chr(34) & IIf(New cls_permisos(obj_Session.Usuario.Id, New cls_modulos("sucursales").Id, New cls_acciones("agregar").Id).permiso, 1, 0) & Chr(34) & "")
            'obj_sb.Append("," & Chr(34) & "Ver" & Chr(34) & ":" & Chr(34) & IIf(New cls_permisos(obj_Session.Usuario.Id, New cls_modulos("sucursales").Id, New cls_acciones("ver").Id).permiso, 1, 0) & Chr(34) & "")
            obj_sb.Append("," & Chr(34) & "Editar" & Chr(34) & ":" & Chr(34) & IIf(New cls_permisos(obj_Session.Usuario.Id, New cls_modulos("sucursales").Id, New cls_acciones("editar").Id).permiso, 1, 0) & Chr(34) & "")
            obj_sb.Append("," & Chr(34) & "Anular" & Chr(34) & ":" & Chr(34) & IIf(New cls_permisos(obj_Session.Usuario.Id, New cls_modulos("sucursales").Id, New cls_acciones("anular").Id).permiso, 1, 0) & Chr(34) & "")
            obj_sb.Append("," & Chr(34) & "Eliminar" & Chr(34) & ":" & Chr(34) & IIf(New cls_permisos(obj_Session.Usuario.Id, New cls_modulos("sucursales").Id, New cls_acciones("eliminar").Id).permiso, 1, 0) & Chr(34) & "")

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
        If var_estatus = 1 Then
            obj_dt_int = cls_sucursales.ConsultaActivos("", var_error)
        ElseIf var_estatus = 2 Then
            obj_dt_int = cls_sucursales.ConsultaAnulados("", var_error)
        Else 'Todos
            obj_dt_int = cls_sucursales.Consulta("", var_error)
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

    Sub Guardar()
        Dim var_sr = New System.IO.StreamReader(Request.InputStream)
        Dim var_data As JObject = JObject.Parse(var_sr.ReadToEnd)

        Dim obj_sucursales As New cls_sucursales(CInt(var_data("id")))
        If obj_sucursales.Id = 0 Then
            obj_sucursales.id_usuario_reg = DirectCast(Session.Contents("obj_Session"), cls_Sesion).Usuario.Id
            obj_sucursales.fecha_reg = Now.Date
        End If
        obj_sucursales.nombre = var_data("Nombre")
        obj_sucursales.codigo = Right("SC" & Right("0000" & (cls_sucursales.SiguienteNumero() + 1).ToString, 4).ToString, 6)
        obj_sucursales.rif = var_data("Rif").ToString.ToUpper
        Response.ContentType = "application/json"
        Response.Clear()
        Response.ClearHeaders()
        Response.ClearContent()
        Dim var_error As String = ""
        If Not obj_sucursales.Actualizar(var_error) Then
            Response.Write("{" & Chr(34) & "rslt" & Chr(34) & ":" & Chr(34) & "error" & Chr(34) & "," & Chr(34) & "msj" & Chr(34) & ":" & Chr(34) & var_error & Chr(34) & "}")
        Else
            Response.Write("{" & Chr(34) & "rslt" & Chr(34) & ":" & Chr(34) & "exito" & Chr(34) & "," & Chr(34) & "msj" & Chr(34) & ":" & Chr(34) & "" & Chr(34) & "}")
        End If
        Response.End()
    End Sub

    Sub Editar()
        Dim var_sr = New System.IO.StreamReader(Request.InputStream)
        Dim var_data As JObject = JObject.Parse(var_sr.ReadToEnd)

        Dim obj_sucursales As New cls_sucursales(CInt(var_data("id").ToString))
        Dim obj_sb As New StringBuilder
        obj_sb.Append("," & Chr(34) & "Nombre" & Chr(34) & ":" & Chr(34) & obj_sucursales.nombre & Chr(34) & "")
        obj_sb.Append("," & Chr(34) & "Codigo" & Chr(34) & ":" & Chr(34) & obj_sucursales.codigo & Chr(34) & "")
        obj_sb.Append("," & Chr(34) & "Rif" & Chr(34) & ":" & Chr(34) & obj_sucursales.rif & Chr(34) & "")
        Response.ContentType = "application/json"
        Response.Clear()
        Response.ClearHeaders()
        Response.ClearContent()
        Dim var_error As String = ""
        Response.Write("{" & Chr(34) & "rslt" & Chr(34) & ":" & Chr(34) & "exito" & Chr(34) & "," & Chr(34) & "msj" & Chr(34) & ":" & Chr(34) & var_error & Chr(34) & obj_sb.ToString & "}")
        Response.End()
    End Sub

    Sub validarSucursal()
        Dim var_error As String = ""
        Dim obj_sb As New StringBuilder

        Dim var_sr = New System.IO.StreamReader(Request.InputStream)
        Dim var_data As JObject = JObject.Parse(var_sr.ReadToEnd)
        Dim var_rif As String = ac_Funciones.formato_Texto(var_data("rif").ToString)


        Dim obj_dt_int As System.Data.DataTable = cls_sucursales.ConsultaSucursal(var_rif, var_error)
        If obj_dt_int.Rows.Count > 0 Then
            obj_sb.Append("," & Chr(34) & "Nombre" & Chr(34) & ":" & Chr(34) & obj_dt_int.Rows(0).Item("Nombre").ToString() & Chr(34) & "")
        Else
            obj_sb.Append("," & Chr(34) & "Nombre" & Chr(34) & ":" & Chr(34) & "" & Chr(34) & "")
        End If

        Response.Clear()
        Response.ClearHeaders()
        Response.ClearContent()

        Dim var_json As String = ""
        If var_error.Trim.Length > 0 Then
            Response.Write("{" & Chr(34) & "rslt" & Chr(34) & ":" & Chr(34) & "error" & Chr(34) & "," & Chr(34) & "msj" & Chr(34) & ":" & Chr(34) & var_error & Chr(34) & "}")
        ElseIf obj_dt_int.Rows.Count > 0 Then
            Response.Write("{" & Chr(34) & "rslt" & Chr(34) & ":" & Chr(34) & "exito" & Chr(34) & "," & Chr(34) & "msj" & Chr(34) & ":" & Chr(34) & var_error & Chr(34) & obj_sb.ToString & "}")
        Else
            Response.Write("{" & Chr(34) & "rslt" & Chr(34) & ":" & Chr(34) & "vacio" & Chr(34) & "," & Chr(34) & "msj" & Chr(34) & ":" & Chr(34) & var_error & Chr(34) & obj_sb.ToString & "}")
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
        If Not cls_sucursales.Eliminar(CInt(var_data("id").ToString), obj_Session.Usuario, var_error) Then
            Response.Write("{" & Chr(34) & "rslt" & Chr(34) & ":" & Chr(34) & "error" & Chr(34) & "," & Chr(34) & "msj" & Chr(34) & ":" & Chr(34) & var_error & Chr(34) & "}")
        Else
            Response.Write("{" & Chr(34) & "rslt" & Chr(34) & ":" & Chr(34) & "exito" & Chr(34) & "," & Chr(34) & "msj" & Chr(34) & ":" & Chr(34) & Chr(34) & "}")
        End If
        Response.End()
    End Sub

    Sub Anular()
        Dim var_sr = New System.IO.StreamReader(Request.InputStream)
        Dim var_data As JObject = JObject.Parse(var_sr.ReadToEnd)
        Dim var_error As String = ""

        Response.ContentType = "application/json"
        Response.Clear()
        Response.ClearHeaders()
        Response.ClearContent()
        If Not cls_sucursales.Anular(CInt(var_data("id").ToString), obj_Session.Usuario.Id, var_error) Then
            Response.Write("{" & Chr(34) & "rslt" & Chr(34) & ":" & Chr(34) & "error" & Chr(34) & "," & Chr(34) & "msj" & Chr(34) & ":" & Chr(34) & var_error & Chr(34) & "}")
        Else
            Response.Write("{" & Chr(34) & "rslt" & Chr(34) & ":" & Chr(34) & "exito" & Chr(34) & "," & Chr(34) & "msj" & Chr(34) & ":" & Chr(34) & Chr(34) & "}")
        End If
        Response.End()
    End Sub
End Class
