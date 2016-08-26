Imports System.Data
Imports System.Data.SqlClient
Imports System.Web.Services
Imports Newtonsoft.Json
Imports Newtonsoft.Json.Linq

Partial Class lst_paquetes
    Inherits System.Web.UI.Page
    Dim obj_Session As cls_Sesion
    Dim obj_marca As cls_paquetes

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
                Case "cargar_destinos"
                    cargar_destinos()
                Case "eliminar"
                    Eliminar()

            End Select
        End If
    End Sub

    Sub Permisos()
        Dim var_error As String = ""
        Dim obj_sb As New StringBuilder
        Try
            If Not New cls_permisos(obj_Session.Usuario.Id, New cls_modulos("paquetes").Id, New cls_acciones("ver").Id).permiso Then
                Dim obj_log As New cls_logs
                obj_log.ComentarioLog = "Listado de paquetes"
                obj_log.FechaLog = Now
                obj_log.id_Menu = New cls_modulos("paquetes").Id
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
                obj_log.ComentarioLog = "Listado de paquetes"
                obj_log.FechaLog = Now
                obj_log.id_Menu = New cls_modulos("paquetes").Id
                obj_log.id_Usuario = obj_Session.Usuario.Id
                obj_log.idAccion = New cls_acciones("ver").Id
                obj_log.ResultadoLog = True
                obj_log.InsertarLog()

            End If

            obj_sb.Append("," & Chr(34) & "Agregar" & Chr(34) & ":" & Chr(34) & IIf(New cls_permisos(obj_Session.Usuario.Id, New cls_modulos("paquetes").Id, New cls_acciones("agregar").Id).permiso, 1, 0) & Chr(34) & "")
            obj_sb.Append("," & Chr(34) & "Ver" & Chr(34) & ":" & Chr(34) & IIf(New cls_permisos(obj_Session.Usuario.Id, New cls_modulos("paquetes").Id, New cls_acciones("ver").Id).permiso, 1, 0) & Chr(34) & "")
            obj_sb.Append("," & Chr(34) & "Editar" & Chr(34) & ":" & Chr(34) & IIf(New cls_permisos(obj_Session.Usuario.Id, New cls_modulos("paquetes").Id, New cls_acciones("editar").Id).permiso, 1, 0) & Chr(34) & "")
            obj_sb.Append("," & Chr(34) & "Anular" & Chr(34) & ":" & Chr(34) & IIf(New cls_permisos(obj_Session.Usuario.Id, New cls_modulos("paquetes").Id, New cls_acciones("anular").Id).permiso, 1, 0) & Chr(34) & "")
            obj_sb.Append("," & Chr(34) & "Eliminar" & Chr(34) & ":" & Chr(34) & IIf(New cls_permisos(obj_Session.Usuario.Id, New cls_modulos("paquetes").Id, New cls_acciones("eliminar").Id).permiso, 1, 0) & Chr(34) & "")

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
            obj_dt_int = cls_paquetes.ConsultaActivos(var_error)
        ElseIf var_estatus = 2 Then
            obj_dt_int = cls_paquetes.ConsultaAnulados(var_error)
        Else 'Todos
            obj_dt_int = cls_paquetes.Consulta(var_error)
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

        Dim obj_paquetes As New cls_paquetes(CInt(var_data("id")))
        If obj_paquetes.Id = 0 Then
            obj_paquetes.id_usuario_reg = DirectCast(Session.Contents("obj_Session"), cls_Sesion).Usuario.Id
            obj_paquetes.fecha_reg = Now.Date
        End If
        obj_paquetes.nombre = var_data("Nombre")
        obj_paquetes.fecha_inicio = ac_Funciones.formato_Fecha(var_data("FechaInicio").ToString)
        obj_paquetes.fecha_fin = ac_Funciones.formato_Fecha(var_data("FechaFin").ToString)
        obj_paquetes.id_destino = var_data("Destino")
        obj_paquetes.tipo = ac_Funciones.formato_boolean(var_data("Tipo").ToString)
        obj_paquetes.grupo = ac_Funciones.formato_boolean(var_data("Grupo").ToString)
        obj_paquetes.precio = ac_Funciones.formato_Numero(var_data("Precio").ToString, True)
        obj_paquetes.activo = ac_Funciones.formato_boolean(var_data("Activo").ToString)

        Response.ContentType = "application/json"
        Response.Clear()
        Response.ClearHeaders()
        Response.ClearContent()
        Dim var_error As String = ""
        If Not obj_paquetes.Actualizar(var_error) Then
            Response.Write("{" & Chr(34) & "rslt" & Chr(34) & ":" & Chr(34) & "error" & Chr(34) & "," & Chr(34) & "msj" & Chr(34) & ":" & Chr(34) & var_error & Chr(34) & "}")
        Else
            Response.Write("{" & Chr(34) & "rslt" & Chr(34) & ":" & Chr(34) & "exito" & Chr(34) & "," & Chr(34) & "msj" & Chr(34) & ":" & Chr(34) & "" & Chr(34) & "}")
        End If
        Response.End()
    End Sub

    Sub Editar()
        Dim var_sr = New System.IO.StreamReader(Request.InputStream)
        Dim var_data As JObject = JObject.Parse(var_sr.ReadToEnd)

        Dim obj_paquetes As New cls_paquetes(CInt(var_data("id").ToString))
        Dim obj_sb As New StringBuilder
        obj_sb.Append("," & Chr(34) & "Nombre" & Chr(34) & ":" & Chr(34) & obj_paquetes.nombre & Chr(34) & "")
        obj_sb.Append("," & Chr(34) & "FechaInicio" & Chr(34) & ":" & Chr(34) & obj_paquetes.fecha_inicio & Chr(34) & "")
        obj_sb.Append("," & Chr(34) & "FechaFin" & Chr(34) & ":" & Chr(34) & obj_paquetes.fecha_fin & Chr(34) & "")
        obj_sb.Append("," & Chr(34) & "Destino" & Chr(34) & ":" & Chr(34) & obj_paquetes.id_destino & Chr(34) & "")
        obj_sb.Append("," & Chr(34) & "Tipo" & Chr(34) & ":" & Chr(34) & IIf(obj_paquetes.tipo, 1, 0) & Chr(34) & "")
        obj_sb.Append("," & Chr(34) & "Grupo" & Chr(34) & ":" & Chr(34) & IIf(obj_paquetes.grupo, 1, 0) & Chr(34) & "")
        obj_sb.Append("," & Chr(34) & "Precio" & Chr(34) & ":" & Chr(34) & ac_Funciones.formato_Numero_Pantalla(obj_paquetes.precio) & Chr(34) & "")
        obj_sb.Append("," & Chr(34) & "Activo" & Chr(34) & ":" & Chr(34) & IIf(obj_paquetes.activo, 1, 0) & Chr(34) & "")
        Response.ContentType = "application/json"
        Response.Clear()
        Response.ClearHeaders()
        Response.ClearContent()
        Dim var_error As String = ""
        Response.Write("{" & Chr(34) & "rslt" & Chr(34) & ":" & Chr(34) & "exito" & Chr(34) & "," & Chr(34) & "msj" & Chr(34) & ":" & Chr(34) & var_error & Chr(34) & obj_sb.ToString & "}")
        Response.End()
    End Sub

    Sub cargar_destinos()
        Dim var_error As String = ""
        Dim obj_dt_int As System.Data.DataTable = cls_destinos.Lista
        Dim var_json As String = JsonConvert.SerializeObject(obj_dt_int)
        Response.Clear()
        Response.ClearHeaders()
        Response.ClearContent()
        Response.Write("{ " & Chr(34) & "aaData" & Chr(34) & ":" & var_json & "}")
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
        If Not cls_paquetes.Eliminar(CInt(var_data("id").ToString), obj_Session.Usuario, var_error) Then
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
        If Not cls_paquetes.Anular(CInt(var_data("id").ToString), obj_Session.Usuario.Id, var_error) Then
            Response.Write("{" & Chr(34) & "rslt" & Chr(34) & ":" & Chr(34) & "error" & Chr(34) & "," & Chr(34) & "msj" & Chr(34) & ":" & Chr(34) & var_error & Chr(34) & "}")
        Else
            Response.Write("{" & Chr(34) & "rslt" & Chr(34) & ":" & Chr(34) & "exito" & Chr(34) & "," & Chr(34) & "msj" & Chr(34) & ":" & Chr(34) & Chr(34) & "}")
        End If
        Response.End()
    End Sub

End Class
