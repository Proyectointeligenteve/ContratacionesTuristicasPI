#Region "IMPORTS"
Imports System.Data
Imports System.Data.SqlClient
Imports System.Web.Services
Imports Newtonsoft.Json
Imports Newtonsoft.Json.Linq
#End Region

Partial Class frm_ventas
    Inherits System.Web.UI.Page

#Region "VARIABLES"
    Dim obj_Session As cls_Sesion
    Dim obj_venta As cls_ventas
    Dim var_Error As String = ""
    'Dim obj_aseguradoras_clases As cls_aseguradoras_clases
#End Region

#Region "EVENTS"
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
        If Not IsNothing(Session.Contents("obj_venta")) Then
            obj_venta = Session.Contents("obj_venta")
        Else
            obj_venta = New cls_ventas
        End If

        If Not IsNothing(Request.QueryString("fn")) Then
            Dim var_fn As String = Request.QueryString("fn").ToString
            Select Case var_fn
                Case "permisos"
                    Permisos()
                    '* Parent
                Case "loadAll"
                    loadAll()
                Case "saveAll"
                    saveAll()
                    'Case "validar_venta"
                    '    validarventa()

                Case "cargar_sucursales"
                    sucursales()
                Case "cargar_tiposventas"
                    tiposventas()
                Case "cargar_vendedores"
                    vendedores()
                Case "cargar_agencias"
                    agencias()
                Case "cargar_freelances"
                    freelances()


                Case "cargar_sistemas"
                    sistemas()
                Case "cargar_tipos"
                    tipos()
                Case "cargar_monedas"
                    monedas()

                Case "cargar_aerolineas"
                    aerolineas()
                Case "cargar_hoteles"
                    hoteles()
                Case "cargar_destinos"
                    destinos()
                Case "cargar_vehiculos"
                    vehiculos()
                Case "cargar_paquetes"
                    paquetes()
            End Select
        End If
    End Sub
#End Region

#Region "FUNCTIONS"
#Region "   >> Parent"
    Sub Permisos()
        Dim var_error As String = ""
        Dim obj_sb As New StringBuilder
        Try
            If Not New cls_permisos(obj_Session.Usuario.Id, New cls_modulos("ventas").Id, New cls_acciones("ver").Id).permiso Then
                Dim obj_log As New cls_logs
                obj_log.ComentarioLog = "Listado de ventas"
                obj_log.FechaLog = Now
                obj_log.id_Menu = New cls_modulos("ventas").Id
                obj_log.id_Usuario = obj_Session.Usuario.Id
                obj_log.idAccion = New cls_acciones("ver").Id
                obj_log.ResultadoLog = False
                obj_log.InsertarLog()

                Response.Clear()
                Response.ClearHeaders()
                Response.ClearContent()
                Response.Write("{" & Chr(34) & "error" & Chr(34) & ":" & Chr(34) & "-1" & Chr(34) & "}")
                Response.End()
            End If

            Dim var_id As Integer = 0
            If Not IsNothing(Request.QueryString("id")) Then
                var_id = Val(Request.QueryString("id"))
            End If

            If var_id > 0 Then
                If Not New cls_permisos(obj_Session.Usuario.Id, New cls_modulos("ventas").Id, New cls_acciones("editar").Id).permiso Then
                    Dim obj_ventas As New cls_ventas(var_id)

                    Dim obj_log As New cls_logs
                    obj_log.ComentarioLog = "Editar venta: " & obj_ventas.id & " - " & obj_ventas.numero
                    obj_log.FechaLog = Now
                    obj_log.id_Menu = New cls_modulos("ventas").Id
                    obj_log.id_Usuario = obj_Session.Usuario.Id
                    obj_log.idAccion = New cls_acciones("editar").Id
                    obj_log.ResultadoLog = False
                    obj_log.InsertarLog()

                    Response.Clear()
                    Response.ClearHeaders()
                    Response.ClearContent()
                    Response.Write("{" & Chr(34) & "error" & Chr(34) & ":" & Chr(34) & "-1" & Chr(34) & "}")
                    Response.End()
                End If
            Else
                If Not New cls_permisos(obj_Session.Usuario.Id, New cls_modulos("ventas").Id, New cls_acciones("agregar").Id).permiso Then
                    Dim obj_log As New cls_logs
                    obj_log.ComentarioLog = "Agregar venta"
                    obj_log.FechaLog = Now
                    obj_log.id_Menu = New cls_modulos("ventas").Id
                    obj_log.id_Usuario = obj_Session.Usuario.Id
                    obj_log.idAccion = New cls_acciones("agregar").Id
                    obj_log.ResultadoLog = False
                    obj_log.InsertarLog()

                    Response.Clear()
                    Response.ClearHeaders()
                    Response.ClearContent()
                    Response.Write("{" & Chr(34) & "error" & Chr(34) & ":" & Chr(34) & "-1" & Chr(34) & "}")
                    Response.End()
                End If
            End If

            Dim var_ver As Integer = 0
            If Not IsNothing(Request.QueryString("v")) Then
                var_ver = Val(Request.QueryString("v"))
            End If

            obj_sb.Append("," & Chr(34) & "Ver" & Chr(34) & ":" & Chr(34) & var_ver & Chr(34) & "")

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

    Sub loadAll()
        Dim var_error As String = ""
        Dim obj_sb As New StringBuilder
        Try
            Dim var_sr = New System.IO.StreamReader(Request.InputStream)
            Dim var_data As JObject = JObject.Parse(var_sr.ReadToEnd)
            Dim var_Id As Integer = ac_Funciones.formato_Numero(var_data("id").ToString)
            obj_venta = New cls_ventas(var_Id)
            Session.Contents("obj_venta") = obj_venta

            Dim var_User As String = ""
            If obj_venta.Id > 0 Then
                var_User = obj_venta.id_usuario_reg
            Else
                var_User = obj_Session.Usuario.nombre
            End If
            'obj_sb.Append("," & Chr(34) & "Rif" & Chr(34) & ":" & Chr(34) & obj_venta.Rif & Chr(34) & "")
            'obj_sb.Append("," & Chr(34) & "Nombre" & Chr(34) & ":" & Chr(34) & obj_venta.Nombre & Chr(34) & "")
            'obj_sb.Append("," & Chr(34) & "RazonSocial" & Chr(34) & ":" & Chr(34) & obj_venta.RazonSocial & Chr(34) & "")
            'obj_sb.Append("," & Chr(34) & "Direccion" & Chr(34) & ":" & Chr(34) & obj_venta.Direccion & Chr(34) & "")
            'obj_sb.Append("," & Chr(34) & "TelefonoF" & Chr(34) & ":" & Chr(34) & obj_venta.TelefonoFijo & Chr(34) & "")
            'obj_sb.Append("," & Chr(34) & "TelefonoM" & Chr(34) & ":" & Chr(34) & obj_venta.TelefonoMovil & Chr(34) & "")
            'obj_sb.Append("," & Chr(34) & "Web" & Chr(34) & ":" & Chr(34) & obj_venta.Web & Chr(34) & "")
            'obj_sb.Append("," & Chr(34) & "Email" & Chr(34) & ":" & Chr(34) & obj_venta.Email & Chr(34) & "")
            'obj_sb.Append("," & Chr(34) & "Codigo" & Chr(34) & ":" & Chr(34) & obj_venta.codigo & Chr(34) & "")
            'obj_sb.Append("," & Chr(34) & "LimiteCredito" & Chr(34) & ":" & Chr(34) & obj_venta.limite_credito & Chr(34) & "")
            'obj_sb.Append("," & Chr(34) & "Pais" & Chr(34) & ":" & Chr(34) & obj_venta.pais & Chr(34) & "")
            'obj_sb.Append("," & Chr(34) & "Estado" & Chr(34) & ":" & Chr(34) & obj_venta.estado & Chr(34) & "")
            'obj_sb.Append("," & Chr(34) & "Ciudad" & Chr(34) & ":" & Chr(34) & obj_venta.ciudad & Chr(34) & "")

        Catch ex As Exception
            var_error = ex.Message
        Finally
            Response.ContentType = "application/json"
            Response.Clear()
            Response.ClearHeaders()
            Response.ClearContent()
            If var_error.Trim.Length > 0 Then
                Response.Write("{" & Chr(34) & "rslt" & Chr(34) & ":" & Chr(34) & "error" & Chr(34) & "," & Chr(34) & "msj" & Chr(34) & ":" & Chr(34) & var_error & Chr(34) & "}")
            Else
                Response.Write("{" & Chr(34) & "rslt" & Chr(34) & ":" & Chr(34) & "exito" & Chr(34) & "," & Chr(34) & "msj" & Chr(34) & ":" & Chr(34) & var_error & Chr(34) & obj_sb.ToString & "}")
            End If
            Response.End()
        End Try
    End Sub

    Sub saveAll()
        Dim var_StreamReader = New System.IO.StreamReader(Request.InputStream)
        Dim var_JObject As JObject = JObject.Parse(var_StreamReader.ReadToEnd)

        If Not IsNothing(Session.Contents("obj_venta")) Then
            obj_venta = Session.Contents("obj_venta")
        Else
            obj_venta = New cls_ventas
        End If

        If obj_venta.Id = 0 Then
            obj_venta.id_usuario_reg = DirectCast(Session.Contents("obj_Session"), cls_Sesion).Usuario.Id
            obj_venta.fecha_reg = Now.Date
            'obj_venta.codigo = Right("AG" & New cls_paises(ac_Funciones.formato_Numero(var_JObject("Pais").ToString)).nombre.Substring(0, 1) & New cls_ciudades(ac_Funciones.formato_Numero(var_JObject("Ciudad").ToString)).nombre.Substring(0, 3).ToUpper & Right("0000" & (cls_ventas.SiguienteNumero() + 1).ToString, 4).ToString, 10)
            'Right("AE" & Right("0000" & (cls_aerolineas.SiguienteNumero() + 1).ToString, 4).ToString, 6)
        Else
            'obj_venta.codigo = ac_Funciones.formato_Texto(var_JObject("Codigo").ToString)
        End If

        'obj_venta.Rif = ac_Funciones.formato_Texto(var_JObject("Rif").ToString).ToUpper
        'obj_venta.Nombre = ac_Funciones.formato_Texto(var_JObject("Nombre").ToString)
        'obj_venta.RazonSocial = ac_Funciones.formato_Texto(var_JObject("RazonSocial").ToString)
        'obj_venta.Direccion = ac_Funciones.formato_Texto(var_JObject("Direccion").ToString)
        'obj_venta.TelefonoFijo = ac_Funciones.formato_Texto(var_JObject("TelefonoF").ToString)
        'obj_venta.TelefonoMovil = ac_Funciones.formato_Texto(var_JObject("TelefonoM").ToString)
        'obj_venta.Web = ac_Funciones.formato_Texto(var_JObject("Web").ToString)
        'obj_venta.Email = ac_Funciones.formato_Texto(var_JObject("Email").ToString)
        'obj_venta.limite_credito = ac_Funciones.formato_Numero(var_JObject("LimiteCredito").ToString, True)
        'obj_venta.pais = ac_Funciones.formato_Numero(var_JObject("Pais").ToString)
        'obj_venta.estado = ac_Funciones.formato_Numero(var_JObject("Estado").ToString)
        'obj_venta.ciudad = ac_Funciones.formato_Numero(var_JObject("Ciudad").ToString)

        Response.ContentType = "application/json"
        Response.Clear()
        Response.ClearHeaders()
        Response.ClearContent()
        If Not obj_venta.Actualizar(var_Error, obj_Session.Usuario.Id) Then
            Response.Write("{" & Chr(34) & "rslt" & Chr(34) & ":" & Chr(34) & "error" & Chr(34) & "," & Chr(34) & "msj" & Chr(34) & ":" & Chr(34) & var_Error & Chr(34) & "}")
        Else
            Response.Write("{" & Chr(34) & "rslt" & Chr(34) & ":" & Chr(34) & "exito" & Chr(34) & "," & Chr(34) & "msj" & Chr(34) & ":" & Chr(34) & "" & Chr(34) & "," & Chr(34) & "id" & Chr(34) & ":" & Chr(34) & obj_venta.id & Chr(34) & "}")
        End If
        Response.End()
    End Sub
    'Private Sub validarventa()
    '    Dim var_error As String = ""
    '    Dim obj_sb As New StringBuilder

    '    Dim var_sr = New System.IO.StreamReader(Request.InputStream)
    '    Dim var_data As JObject = JObject.Parse(var_sr.ReadToEnd)
    '    Dim var_rif As String = ac_Funciones.formato_Texto(var_data("rif").ToString)


    '    Dim obj_dt_int As System.Data.DataTable = cls_ventas.Consultaventa(var_rif, var_error)
    '    If obj_dt_int.Rows.Count > 0 Then
    '        obj_sb.Append("," & Chr(34) & "Nombre" & Chr(34) & ":" & Chr(34) & obj_dt_int.Rows(0).Item("Nombre").ToString() & Chr(34) & "")
    '    Else
    '        obj_sb.Append("," & Chr(34) & "Nombre" & Chr(34) & ":" & Chr(34) & "" & Chr(34) & "")
    '    End If

    '    Response.Clear()
    '    Response.ClearHeaders()
    '    Response.ClearContent()

    '    Dim var_json As String = ""
    '    If var_error.Trim.Length > 0 Then
    '        Response.Write("{" & Chr(34) & "rslt" & Chr(34) & ":" & Chr(34) & "error" & Chr(34) & "," & Chr(34) & "msj" & Chr(34) & ":" & Chr(34) & var_error & Chr(34) & "}")
    '    ElseIf obj_dt_int.Rows.Count > 0 Then
    '        Response.Write("{" & Chr(34) & "rslt" & Chr(34) & ":" & Chr(34) & "exito" & Chr(34) & "," & Chr(34) & "msj" & Chr(34) & ":" & Chr(34) & var_error & Chr(34) & obj_sb.ToString & "}")
    '    Else
    '        Response.Write("{" & Chr(34) & "rslt" & Chr(34) & ":" & Chr(34) & "vacio" & Chr(34) & "," & Chr(34) & "msj" & Chr(34) & ":" & Chr(34) & var_error & Chr(34) & obj_sb.ToString & "}")
    '    End If

    '    Response.End()
    'End Sub

    Sub sucursales()
        Dim var_error As String = ""
        Dim obj_dt_int As System.Data.DataTable = cls_sucursales.Lista
        Dim var_json As String = JsonConvert.SerializeObject(obj_dt_int)
        Response.Clear()
        Response.ClearHeaders()
        Response.ClearContent()
        Response.Write("{ " & Chr(34) & "aaData" & Chr(34) & ":" & var_json & "}")
        Response.End()
    End Sub

    Sub tiposventas()
        Dim var_error As String = ""
        Dim obj_dt_int As System.Data.DataTable = cls_tipos_ventas.Lista()
        Dim var_json As String = JsonConvert.SerializeObject(obj_dt_int)
        Response.Clear()
        Response.ClearHeaders()
        Response.ClearContent()
        Response.Write("{ " & Chr(34) & "aaData" & Chr(34) & ":" & var_json & "}")
        Response.End()
    End Sub

    Sub vendedores()
        Dim var_error As String = ""
        Dim obj_dt_int As System.Data.DataTable = cls_empleados.Lista(" vendedor=1")
        Dim var_json As String = JsonConvert.SerializeObject(obj_dt_int)
        Response.Clear()
        Response.ClearHeaders()
        Response.ClearContent()
        Response.Write("{ " & Chr(34) & "aaData" & Chr(34) & ":" & var_json & "}")
        Response.End()
    End Sub

    Sub agencias()
        Dim var_error As String = ""
        Dim obj_dt_int As System.Data.DataTable = cls_agencias.Lista()
        Dim var_json As String = JsonConvert.SerializeObject(obj_dt_int)
        Response.Clear()
        Response.ClearHeaders()
        Response.ClearContent()
        Response.Write("{ " & Chr(34) & "aaData" & Chr(34) & ":" & var_json & "}")
        Response.End()
    End Sub

    Sub freelances()
        Dim var_error As String = ""
        Dim obj_dt_int As System.Data.DataTable = cls_freelances.Lista()
        Dim var_json As String = JsonConvert.SerializeObject(obj_dt_int)
        Response.Clear()
        Response.ClearHeaders()
        Response.ClearContent()
        Response.Write("{ " & Chr(34) & "aaData" & Chr(34) & ":" & var_json & "}")
        Response.End()
    End Sub


    Sub sistemas()
        Dim var_error As String = ""
        Dim obj_dt_int As System.Data.DataTable = cls_sistemas.Lista
        Dim var_json As String = JsonConvert.SerializeObject(obj_dt_int)
        Response.Clear()
        Response.ClearHeaders()
        Response.ClearContent()
        Response.Write("{ " & Chr(34) & "aaData" & Chr(34) & ":" & var_json & "}")
        Response.End()
    End Sub

    Sub tipos()
        Dim var_error As String = ""
        Dim obj_dt_int As System.Data.DataTable = cls_tipos_configuracion.Lista
        Dim var_json As String = JsonConvert.SerializeObject(obj_dt_int)
        Response.Clear()
        Response.ClearHeaders()
        Response.ClearContent()
        Response.Write("{ " & Chr(34) & "aaData" & Chr(34) & ":" & var_json & "}")
        Response.End()
    End Sub

    Sub monedas()
        Dim var_error As String = ""
        Dim obj_dt_int As System.Data.DataTable = cls_monedas.Lista
        Dim var_json As String = JsonConvert.SerializeObject(obj_dt_int)
        Response.Clear()
        Response.ClearHeaders()
        Response.ClearContent()
        Response.Write("{ " & Chr(34) & "aaData" & Chr(34) & ":" & var_json & "}")
        Response.End()
    End Sub

    Sub aerolineas()
        Dim var_error As String = ""
        Dim obj_dt_int As System.Data.DataTable = cls_aerolineas.Lista
        Dim var_json As String = JsonConvert.SerializeObject(obj_dt_int)
        Response.Clear()
        Response.ClearHeaders()
        Response.ClearContent()
        Response.Write("{ " & Chr(34) & "aaData" & Chr(34) & ":" & var_json & "}")
        Response.End()
    End Sub

    Sub hoteles()
        Dim var_error As String = ""
        Dim obj_dt_int As System.Data.DataTable = cls_hoteles.Lista
        Dim var_json As String = JsonConvert.SerializeObject(obj_dt_int)
        Response.Clear()
        Response.ClearHeaders()
        Response.ClearContent()
        Response.Write("{ " & Chr(34) & "aaData" & Chr(34) & ":" & var_json & "}")
        Response.End()
    End Sub

    Sub destinos()
        Dim var_error As String = ""
        Dim obj_dt_int As System.Data.DataTable = cls_destinos.Lista
        Dim var_json As String = JsonConvert.SerializeObject(obj_dt_int)
        Response.Clear()
        Response.ClearHeaders()
        Response.ClearContent()
        Response.Write("{ " & Chr(34) & "aaData" & Chr(34) & ":" & var_json & "}")
        Response.End()
    End Sub

    Sub vehiculos()
        Dim var_error As String = ""
        Dim obj_dt_int As System.Data.DataTable = cls_vehiculos.Lista
        Dim var_json As String = JsonConvert.SerializeObject(obj_dt_int)
        Response.Clear()
        Response.ClearHeaders()
        Response.ClearContent()
        Response.Write("{ " & Chr(34) & "aaData" & Chr(34) & ":" & var_json & "}")
        Response.End()
    End Sub

    Sub paquetes()
        Dim var_error As String = ""
        Dim obj_dt_int As System.Data.DataTable = cls_paquetes.Lista
        Dim var_json As String = JsonConvert.SerializeObject(obj_dt_int)
        Response.Clear()
        Response.ClearHeaders()
        Response.ClearContent()
        Response.Write("{ " & Chr(34) & "aaData" & Chr(34) & ":" & var_json & "}")
        Response.End()
    End Sub
#End Region

#End Region

End Class