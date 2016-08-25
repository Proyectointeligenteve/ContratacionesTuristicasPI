#Region "IMPORTS"
Imports System.Data
Imports System.Data.SqlClient
Imports System.Web.Services
Imports Newtonsoft.Json
Imports Newtonsoft.Json.Linq
#End Region

Partial Class frm_envios
    Inherits System.Web.UI.Page

#Region "VARIABLES"
    Dim obj_Session As cls_Sesion
    Dim obj_envio As cls_envios
    Dim var_Error As String = ""
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
        If Not IsNothing(Session.Contents("obj_envio")) Then
            obj_envio = Session.Contents("obj_envio")
        Else
            obj_envio = New cls_envios
        End If

        If Not IsNothing(Request.QueryString("fn")) Then
            Dim var_fn As String = Request.QueryString("fn").ToString
            Select Case var_fn
                Case "permisos"
                    Permisos()
                Case "loadAll"
                    loadAll()

                Case "saveAll"
                    saveAll()
                    '* clientes
                Case "buscarclientesE"
                    buscar_clientesE()
                Case "buscarclientesR"
                    buscar_clientesR()
                Case "cargarclienteE"
                    cargar_clienteE()
                Case "cargarclienteR"
                    cargar_clienteR()
                    'Case "clienteDelete"
                    '    clienteDelete()
                    'Case "clienteEdit"
                    '    clienteEdit()
                    'Case "clienteLoad"
                    '    clienteLoad()
                    'Case "clientesave"
                    '    clientesave()
                    '* paquetes
                Case "CargarPaquetes"
                    CargarPaquetes()
                Case "paqueteDelete"
                    paqueteDelete()
                Case "paqueteEdit"
                    paqueteEdit()
                Case "paqueteLoad"
                    paqueteLoad()
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
            End If

            Dim var_id As Integer = 0
            If Not IsNothing(Request.QueryString("id")) Then
                var_id = Val(Request.QueryString("id"))
            End If

            If var_id > 0 Then
                If Not New cls_permisos(obj_Session.Usuario.Id, New cls_modulos("envios").Id, New cls_acciones("editar").Id).permiso Then
                    Dim obj_envios As New cls_envios(var_id)

                    Dim obj_log As New cls_logs
                    obj_log.ComentarioLog = "Editar cliente: " & obj_envios.Id & " - " & obj_envios.codigo
                    obj_log.FechaLog = Now
                    obj_log.id_Menu = New cls_modulos("envios").Id
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
                If Not New cls_permisos(obj_Session.Usuario.Id, New cls_modulos("envios").Id, New cls_acciones("agregar").Id).permiso Then
                    Dim obj_log As New cls_logs
                    obj_log.ComentarioLog = "Agregar cliente"
                    obj_log.FechaLog = Now
                    obj_log.id_Menu = New cls_modulos("envios").Id
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
            obj_envio = New cls_envios(var_Id)
            Session.Contents("obj_envio") = obj_envio

            Dim var_User As String = ""
            If obj_envio.Id > 0 Then
                var_User = obj_envio.id_usuario_reg
            Else
                var_User = obj_Session.Usuario.nombre
            End If
            obj_sb.Append("," & Chr(34) & "Codigo" & Chr(34) & ":" & Chr(34) & obj_envio.codigo & Chr(34) & "")
            obj_sb.Append("," & Chr(34) & "Id_cliente_emisor" & Chr(34) & ":" & Chr(34) & obj_envio.id_cliente_emisor & Chr(34) & "")
            obj_sb.Append("," & Chr(34) & "Id_cliente_receptor" & Chr(34) & ":" & Chr(34) & obj_envio.id_cliente_receptor & Chr(34) & "")
            obj_sb.Append("," & Chr(34) & "Id_pais_origen" & Chr(34) & ":" & Chr(34) & obj_envio.id_pais_origen & Chr(34) & "")
            obj_sb.Append("," & Chr(34) & "Id_estado_origen" & Chr(34) & ":" & Chr(34) & obj_envio.id_estado_origen & Chr(34) & "")
            obj_sb.Append("," & Chr(34) & "Id_ciudad_origen" & Chr(34) & ":" & Chr(34) & obj_envio.id_ciudad_origen & Chr(34) & "")
            obj_sb.Append("," & Chr(34) & "Id_pais_origen" & Chr(34) & ":" & Chr(34) & obj_envio.id_pais_destino & Chr(34) & "")
            obj_sb.Append("," & Chr(34) & "Id_estado_origen" & Chr(34) & ":" & Chr(34) & obj_envio.id_estado_destino & Chr(34) & "")
            obj_sb.Append("," & Chr(34) & "Id_ciudad_origen" & Chr(34) & ":" & Chr(34) & obj_envio.id_ciudad_destino & Chr(34) & "")
            obj_sb.Append("," & Chr(34) & "Direccion_envio" & Chr(34) & ":" & Chr(34) & obj_envio.direccion_destino & Chr(34) & "")
            obj_sb.Append("," & Chr(34) & "Costo_envio" & Chr(34) & ":" & Chr(34) & obj_envio.costo_envio & Chr(34) & "")
            obj_sb.Append("," & Chr(34) & "Estatus" & Chr(34) & ":" & Chr(34) & obj_envio.estatus & Chr(34) & "")

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

        If Not IsNothing(Session.Contents("obj_envio")) Then
            obj_envio = Session.Contents("obj_envio")
        Else
            obj_envio = New cls_envios
        End If

        If obj_envio.Id = 0 Then
            obj_envio.id_usuario_reg = DirectCast(Session.Contents("obj_Session"), cls_Sesion).Usuario.Id
            obj_envio.fecha_reg = Now.Date
        End If
        obj_envio.codigo = ac_Funciones.formato_Texto(var_JObject("Codigo").ToString)
        obj_envio.id_cliente_emisor = ac_Funciones.formato_Texto(var_JObject("Id_cliente_emisor").ToString)
        obj_envio.id_cliente_receptor = ac_Funciones.formato_Texto(var_JObject("Id_cliente_receptor").ToString)
        obj_envio.id_pais_origen = ac_Funciones.formato_Texto(var_JObject("Id_pais_origen").ToString)
        obj_envio.id_estado_origen = ac_Funciones.formato_Texto(var_JObject("Id_estado_origen").ToString)
        obj_envio.id_ciudad_origen = ac_Funciones.formato_Texto(var_JObject("Id_ciudad_origen").ToString)
        obj_envio.id_pais_destino = ac_Funciones.formato_Texto(var_JObject("Id_pais_destino").ToString)
        obj_envio.id_estado_destino = ac_Funciones.formato_Texto(var_JObject("Id_estado_destino").ToString)
        obj_envio.id_ciudad_destino = ac_Funciones.formato_Texto(var_JObject("Id_ciudad_destino").ToString)
        obj_envio.direccion_destino = ac_Funciones.formato_Texto(var_JObject("Direccion_destino").ToString)
        obj_envio.costo_envio = ac_Funciones.formato_Texto(var_JObject("Costo_envio").ToString)
        obj_envio.estatus = ac_Funciones.formato_Texto(var_JObject("Estatus").ToString)

        Response.ContentType = "application/json"
        Response.Clear()
        Response.ClearHeaders()
        Response.ClearContent()
        If Not obj_envio.Actualizar(var_Error) Then
            Response.Write("{" & Chr(34) & "rslt" & Chr(34) & ":" & Chr(34) & "error" & Chr(34) & "," & Chr(34) & "msj" & Chr(34) & ":" & Chr(34) & var_Error & Chr(34) & "}")
        Else
            Response.Write("{" & Chr(34) & "rslt" & Chr(34) & ":" & Chr(34) & "exito" & Chr(34) & "," & Chr(34) & "msj" & Chr(34) & ":" & Chr(34) & "" & Chr(34) & "," & Chr(34) & "id" & Chr(34) & ":" & Chr(34) & obj_envio.Id & Chr(34) & "}")
        End If
        Response.End()
    End Sub
    
#End Region

#Region "   >> cliente"
    
    Sub cargar_clienteE()
        Dim var_error As String = ""
        Dim obj_sb As New StringBuilder
        Try
            Dim var_sr = New System.IO.StreamReader(Request.InputStream)
            Dim var_data As JObject = JObject.Parse(var_sr.ReadToEnd)
            Dim var_id As Integer = ac_Funciones.formato_Numero(var_data("id").ToString)
            Dim obj_cliente = New cls_envios_clientes(var_id)
            If var_id > 0 Then
                obj_sb.Append("," & Chr(34) & "IdClienteEmisor" & Chr(34) & ":" & Chr(34) & obj_cliente.identificador & Chr(34) & "")
                obj_sb.Append("," & Chr(34) & "NombreE" & Chr(34) & ":" & Chr(34) & obj_cliente.nombre & Chr(34) & "")
                obj_sb.Append("," & Chr(34) & "TelefonoE" & Chr(34) & ":" & Chr(34) & obj_cliente.telefono & Chr(34) & "")
                obj_sb.Append("," & Chr(34) & "EmailE" & Chr(34) & ":" & Chr(34) & obj_cliente.email & Chr(34) & "")
            End If
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
    Sub cargar_clienteR()
        Dim var_error As String = ""
        Dim obj_sb As New StringBuilder
        Try
            Dim var_sr = New System.IO.StreamReader(Request.InputStream)
            Dim var_data As JObject = JObject.Parse(var_sr.ReadToEnd)
            Dim var_id As Integer = ac_Funciones.formato_Numero(var_data("id").ToString)
            Dim obj_cliente = New cls_envios_clientes(var_id)
            If var_id > 0 Then
                obj_sb.Append("," & Chr(34) & "IdClienteReceptor" & Chr(34) & ":" & Chr(34) & obj_cliente.identificador & Chr(34) & "")
                obj_sb.Append("," & Chr(34) & "NombreR" & Chr(34) & ":" & Chr(34) & obj_cliente.nombre & Chr(34) & "")
                obj_sb.Append("," & Chr(34) & "TelefonoR" & Chr(34) & ":" & Chr(34) & obj_cliente.telefono & Chr(34) & "")
                obj_sb.Append("," & Chr(34) & "EmailR" & Chr(34) & ":" & Chr(34) & obj_cliente.email & Chr(34) & "")
            End If
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
    Private Sub buscar_clientesE()
        Dim var_identificador As String = Request.QueryString("identificador").ToString
        Dim var_error As String = ""

        Dim obj_dt_int As System.Data.DataTable = cls_envios_clientes.Consulta(var_identificador, var_error)

        Response.Clear()
        Response.ClearHeaders()
        Response.ClearContent()

        Dim var_json As String = ""
        If var_error.Trim.ToString.Length > 0 Then
            Response.Write("{" & Chr(34) & "error" & Chr(34) & ":" & Chr(34) & "-2" & Chr(34) & "}")
        Else
            If obj_dt_int.Rows.Count > 1 Then
                var_json = JsonConvert.SerializeObject(obj_dt_int)
                Response.Write("{ " & Chr(34) & "aaData" & Chr(34) & ":" & var_json & "}")
            ElseIf obj_dt_int.Rows.Count = 1 Then
                Response.Write("{" & Chr(34) & "idclienteEmisor" & Chr(34) & ":" & Chr(34) & obj_dt_int.Rows(0).Item(0) & Chr(34) & "}")
            Else
                Response.Write("{" & Chr(34) & "error" & Chr(34) & ":" & Chr(34) & "-1" & Chr(34) & "}")
            End If
        End If

        Response.End()
    End Sub
    Private Sub buscar_clientesR()
        Dim var_identificador As String = Request.QueryString("identificador").ToString
        Dim var_error As String = ""

        Dim obj_dt_int As System.Data.DataTable = cls_envios_clientes.Consulta(var_identificador, var_error)

        Response.Clear()
        Response.ClearHeaders()
        Response.ClearContent()

        Dim var_json As String = ""
        If var_error.Trim.ToString.Length > 0 Then
            Response.Write("{" & Chr(34) & "error" & Chr(34) & ":" & Chr(34) & "-2" & Chr(34) & "}")
        Else
            If obj_dt_int.Rows.Count > 1 Then
                var_json = JsonConvert.SerializeObject(obj_dt_int)
                Response.Write("{ " & Chr(34) & "aaData" & Chr(34) & ":" & var_json & "}")
            ElseIf obj_dt_int.Rows.Count = 1 Then
                Response.Write("{" & Chr(34) & "idclienteReceptor" & Chr(34) & ":" & Chr(34) & obj_dt_int.Rows(0).Item(0) & Chr(34) & "}")
            Else
                Response.Write("{" & Chr(34) & "error" & Chr(34) & ":" & Chr(34) & "-1" & Chr(34) & "}")
            End If
        End If

        Response.End()
    End Sub
    '    Sub clienteDelete()
    '        Dim var_Error As String = ""
    '        Try
    '            Dim var_StreamReader = New System.IO.StreamReader(Request.InputStream)
    '            Dim var_JObject As JObject = JObject.Parse(var_StreamReader.ReadToEnd)
    '            Dim var_Ids As String = var_JObject("hdn_clienteId")
    '            If var_Ids.Trim.Length > 0 Then
    '                var_Ids = var_Ids.Substring(1)
    '            End If

    '            If Not IsNothing(Session.Contents("obj_envio")) Then
    '                obj_envio = Session.Contents("obj_envio")
    '            Else
    '                obj_envio = New cls_envios
    '            End If

    '            Dim var_Id() As String = var_Ids.Split(",")
    '            For i As Integer = var_Id.Count - 1 To 0 Step -1
    '                obj_envio.Contacto.RemoveAt(var_Id(i) - 1)
    '            Next
    '            Session.Contents("obj_envio") = obj_envio
    '        Catch ex As Exception
    '            var_Error = ex.Message
    '        Finally
    '            Response.ContentType = "application/json"
    '            Response.Clear()
    '            Response.ClearHeaders()
    '            Response.ClearContent()
    '            If var_Error.Trim.Length > 0 Then
    '                Response.Write("{" & Chr(34) & "rslt" & Chr(34) & ":" & Chr(34) & "error" & Chr(34) & "," & Chr(34) & "msj" & Chr(34) & ":" & Chr(34) & var_Error & Chr(34) & "}")
    '            Else
    '                Response.Write("{" & Chr(34) & "rslt" & Chr(34) & ":" & Chr(34) & "exito" & Chr(34) & "," & Chr(34) & "msj" & Chr(34) & ":" & Chr(34) & "" & Chr(34) & "}")
    '            End If
    '            Response.End()
    '        End Try
    '    End Sub

    '    Sub clienteEdit()
    '        Dim var_Error As String = ""
    '        If Not IsNothing(Session.Contents("obj_envio")) Then
    '            obj_envio = Session.Contents("obj_envio")
    '        Else
    '            obj_envio = New cls_envios
    '        End If

    '        Dim obj_StringBuilder As New StringBuilder
    '        Try
    '            Dim var_StreamReader = New System.IO.StreamReader(Request.InputStream)
    '            Dim var_JObject As JObject = JObject.Parse(var_StreamReader.ReadToEnd)
    '            Dim var_Position As Integer = ac_Funciones.formato_Numero(var_JObject("hdn_clienteId").ToString) - 1
    '            obj_StringBuilder.Append("," & Chr(34) & "cliente" & Chr(34) & ":" & Chr(34) & obj_envio.Contacto(var_Position).id_envio & Chr(34) & "")
    '        Catch ex As Exception
    '            var_Error = ex.Message
    '        Finally
    '            Response.ContentType = "application/json"
    '            Response.Clear()
    '            Response.ClearHeaders()
    '            Response.ClearContent()
    '            If var_Error.Trim.Length > 0 Then
    '                Response.Write("{" & Chr(34) & "rslt" & Chr(34) & ":" & Chr(34) & "error" & Chr(34) & "," & Chr(34) & "msj" & Chr(34) & ":" & Chr(34) & var_Error & Chr(34) & var_Error & "}")
    '            Else
    '                Response.Write("{" & Chr(34) & "rslt" & Chr(34) & ":" & Chr(34) & "exito" & Chr(34) & "," & Chr(34) & "msj" & Chr(34) & ":" & Chr(34) & var_Error & Chr(34) & obj_StringBuilder.ToString & "}")
    '            End If
    '            Response.End()
    '        End Try

    '    End Sub

    '    Sub clienteLoad()
    '        Dim var_Error As String = ""
    '        If Not IsNothing(Session.Contents("obj_envio")) Then
    '            obj_envio = Session.Contents("obj_envio")
    '        Else
    '            obj_envio = New cls_envios
    '        End If

    '        Dim obj_DataTable As System.Data.DataTable = obj_envio.Listaclientes
    '        Dim var_JSon As String = JsonConvert.SerializeObject(obj_DataTable)
    '        Response.Clear()
    '        Response.ClearHeaders()
    '        Response.ClearContent()
    '        Response.Write("{ " & Chr(34) & "aaData" & Chr(34) & ":" & var_JSon & "}")
    '        Response.End()
    '    End Sub

    '    Sub clientesave()
    '        Try
    '            If Not IsNothing(Session.Contents("obj_envio")) Then
    '                obj_envio = Session.Contents("obj_envio")
    '            Else
    '                obj_envio = New cls_envios
    '            End If

    '            Dim var_StreamReader = New System.IO.StreamReader(Request.InputStream)
    '            Dim var_JObject As JObject = JObject.Parse(var_StreamReader.ReadToEnd)
    '            Dim var_clienteId As Integer = CInt(var_JObject("hdn_clienteId").ToString)

    '            'If var_clienteId > 0 Then
    '            '    obj_envio.clientes(var_clienteId - 1).id_cliente = var_JObject("cliente").ToString
    '            'Else
    '            '    Dim obj_envios_clientes As New cls_envios_clientes
    '            '    obj_envios_clientes.id_cliente = var_JObject("cliente").ToString
    '            '    obj_envio.clientes.Add(obj_envios_clientes)
    '            'End If
    '            Session.Contents("obj_envio") = obj_envio
    '        Catch ex As Exception
    '            var_Error = ex.Message
    '        Finally
    '            Response.ContentType = "application/json"
    '            Response.Clear()
    '            Response.ClearHeaders()
    '            Response.ClearContent()
    '            If var_Error.Trim.Length > 0 Then
    '                Response.Write("{" & Chr(34) & "rslt" & Chr(34) & ":" & Chr(34) & "error" & Chr(34) & "," & Chr(34) & "msj" & Chr(34) & ":" & Chr(34) & var_Error & Chr(34) & "}")
    '            Else
    '                Response.Write("{" & Chr(34) & "rslt" & Chr(34) & ":" & Chr(34) & "exito" & Chr(34) & "," & Chr(34) & "msj" & Chr(34) & ":" & Chr(34) & "" & Chr(34) & "}")
    '            End If
    '            Response.End()
    '        End Try
    '    End Sub
#End Region

#Region "   >> paquete"
    Sub CargarPaquetes()
        Dim var_error As String = ""

        Dim obj_dt_int As System.Data.DataTable = cls_envios_paquetes.Lista()
        Dim var_json As String = JsonConvert.SerializeObject(obj_dt_int)
        Response.Clear()
        Response.ClearHeaders()
        Response.ClearContent()
        Response.Write("{ " & Chr(34) & "aaData" & Chr(34) & ":" & var_json & "}")
        Response.End()
    End Sub
    Sub paqueteDelete()
        Dim var_Error As String = ""
        Try
            Dim var_StreamReader = New System.IO.StreamReader(Request.InputStream)
            Dim var_JObject As JObject = JObject.Parse(var_StreamReader.ReadToEnd)
            Dim var_Ids As String = var_JObject("hdn_paqueteId")
            If var_Ids.Trim.Length > 0 Then
                var_Ids = var_Ids.Substring(1)
            End If

            If Not IsNothing(Session.Contents("obj_envio")) Then
                obj_envio = Session.Contents("obj_envio")
            Else
                obj_envio = New cls_envios
            End If

            Dim var_Id() As String = var_Ids.Split(",")
            For i As Integer = var_Id.Count - 1 To 0 Step -1
                obj_envio.Paquete.RemoveAt(var_Id(i) - 1)
            Next
            Session.Contents("obj_envio") = obj_envio
        Catch ex As Exception
            var_Error = ex.Message
        Finally
            Response.ContentType = "application/json"
            Response.Clear()
            Response.ClearHeaders()
            Response.ClearContent()
            If var_Error.Trim.Length > 0 Then
                Response.Write("{" & Chr(34) & "rslt" & Chr(34) & ":" & Chr(34) & "error" & Chr(34) & "," & Chr(34) & "msj" & Chr(34) & ":" & Chr(34) & var_Error & Chr(34) & "}")
            Else
                Response.Write("{" & Chr(34) & "rslt" & Chr(34) & ":" & Chr(34) & "exito" & Chr(34) & "," & Chr(34) & "msj" & Chr(34) & ":" & Chr(34) & "" & Chr(34) & "}")
            End If
            Response.End()
        End Try
    End Sub

    Sub paqueteEdit()
        Dim var_Error As String = ""
        If Not IsNothing(Session.Contents("obj_envio")) Then
            obj_envio = Session.Contents("obj_envio")
        Else
            obj_envio = New cls_envios
        End If

        Dim obj_StringBuilder As New StringBuilder
        Try
            Dim var_StreamReader = New System.IO.StreamReader(Request.InputStream)
            Dim var_JObject As JObject = JObject.Parse(var_StreamReader.ReadToEnd)
            Dim var_Position As Integer = ac_Funciones.formato_Numero(var_JObject("hdn_paqueteId").ToString) - 1
            obj_StringBuilder.Append("," & Chr(34) & "Id_envio" & Chr(34) & ":" & Chr(34) & obj_envio.Paquete(var_Position).id_envio & Chr(34) & "")
            obj_StringBuilder.Append("," & Chr(34) & "Numero" & Chr(34) & ":" & Chr(34) & obj_envio.Paquete(var_Position).numero & Chr(34) & "")
            obj_StringBuilder.Append("," & Chr(34) & "Peso" & Chr(34) & ":" & Chr(34) & obj_envio.Paquete(var_Position).peso & Chr(34) & "")
            obj_StringBuilder.Append("," & Chr(34) & "Volumen" & Chr(34) & ":" & Chr(34) & obj_envio.Paquete(var_Position).volumen & Chr(34) & "")
            obj_StringBuilder.Append("," & Chr(34) & "Costo" & Chr(34) & ":" & Chr(34) & obj_envio.Paquete(var_Position).costo & Chr(34) & "")
            obj_StringBuilder.Append("," & Chr(34) & "Descripcion" & Chr(34) & ":" & Chr(34) & obj_envio.Paquete(var_Position).descripcion & Chr(34) & "")
        Catch ex As Exception
            var_Error = ex.Message
        Finally
            Response.ContentType = "application/json"
            Response.Clear()
            Response.ClearHeaders()
            Response.ClearContent()
            If var_Error.Trim.Length > 0 Then
                Response.Write("{" & Chr(34) & "rslt" & Chr(34) & ":" & Chr(34) & "error" & Chr(34) & "," & Chr(34) & "msj" & Chr(34) & ":" & Chr(34) & var_Error & Chr(34) & var_Error & "}")
            Else
                Response.Write("{" & Chr(34) & "rslt" & Chr(34) & ":" & Chr(34) & "exito" & Chr(34) & "," & Chr(34) & "msj" & Chr(34) & ":" & Chr(34) & var_Error & Chr(34) & obj_StringBuilder.ToString & "}")
            End If
            Response.End()
        End Try

    End Sub

    Sub paqueteLoad()
        Dim var_Error As String = ""
        If Not IsNothing(Session.Contents("obj_envio")) Then
            obj_envio = Session.Contents("obj_envio")
        Else
            obj_envio = New cls_envios
        End If

        Dim obj_DataTable As System.Data.DataTable = obj_envio.ListaPaquetes
        Dim var_JSon As String = JsonConvert.SerializeObject(obj_DataTable)
        Response.Clear()
        Response.ClearHeaders()
        Response.ClearContent()
        Response.Write("{ " & Chr(34) & "aaData" & Chr(34) & ":" & var_JSon & "}")
        Response.End()
    End Sub

    Sub paquetesave()
        Try
            If Not IsNothing(Session.Contents("obj_envio")) Then
                obj_envio = Session.Contents("obj_envio")
            Else
                obj_envio = New cls_envios
            End If

            Dim var_StreamReader = New System.IO.StreamReader(Request.InputStream)
            Dim var_JObject As JObject = JObject.Parse(var_StreamReader.ReadToEnd)
            Dim var_paqueteId As Integer = CInt(var_JObject("hdn_paqueteId").ToString)

            If var_paqueteId > 0 Then
                obj_envio.Paquete(var_paqueteId - 1).id_envio = var_JObject("IdEnvio").ToString
                obj_envio.Paquete(var_paqueteId - 1).numero = var_JObject("Numero").ToString
                obj_envio.Paquete(var_paqueteId - 1).peso = var_JObject("Peso").ToString
                obj_envio.Paquete(var_paqueteId - 1).volumen = var_JObject("Volumen").ToString
                obj_envio.Paquete(var_paqueteId - 1).costo = var_JObject("Costo").ToString
                obj_envio.Paquete(var_paqueteId - 1).descripcion = var_JObject("Descripcion").ToString
            Else
                Dim obj_envios_paquetes As New cls_envios_paquetes
                obj_envios_paquetes.numero = var_JObject("Numero").ToString
                obj_envios_paquetes.peso = var_JObject("Peso").ToString
                obj_envios_paquetes.volumen = var_JObject("Volumen").ToString
                obj_envios_paquetes.costo = var_JObject("Costo").ToString
                obj_envios_paquetes.descripcion = var_JObject("Descripcion").ToString
                obj_envio.Paquete.Add(obj_envios_paquetes)
            End If
            Session.Contents("obj_envio") = obj_envio
        Catch ex As Exception
            var_Error = ex.Message
        Finally
            Response.ContentType = "application/json"
            Response.Clear()
            Response.ClearHeaders()
            Response.ClearContent()
            If var_Error.Trim.Length > 0 Then
                Response.Write("{" & Chr(34) & "rslt" & Chr(34) & ":" & Chr(34) & "error" & Chr(34) & "," & Chr(34) & "msj" & Chr(34) & ":" & Chr(34) & var_Error & Chr(34) & "}")
            Else
                Response.Write("{" & Chr(34) & "rslt" & Chr(34) & ":" & Chr(34) & "exito" & Chr(34) & "," & Chr(34) & "msj" & Chr(34) & ":" & Chr(34) & "" & Chr(34) & "}")
            End If
            Response.End()
        End Try
    End Sub

#End Region

#End Region

End Class