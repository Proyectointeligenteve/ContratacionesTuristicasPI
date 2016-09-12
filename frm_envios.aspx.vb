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
                Case "guardarCliente"
                    guardarCliente()

                    'Case "CargarPaquetes"
                    '    CargarPaquetes()
                    'Case "paqueteDelete"
                    '    paqueteDelete()
                    'Case "paqueteEdit"
                    '    paqueteEdit()
                    'Case "paqueteLoad"
                    '    paqueteLoad()
                    'Case "paqueteSave"
                    '    paquetesave()

                Case "cargar_paises"
                    paises()
                Case "cargar_estados"
                    estados()
                Case "cargar_ciudades"
                    ciudades()
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
            obj_sb.Append("," & Chr(34) & "ClienteE" & Chr(34) & ":" & Chr(34) & obj_envio.id_cliente_emisor & Chr(34) & "")
            obj_sb.Append("," & Chr(34) & "HfPaisE" & Chr(34) & ":" & Chr(34) & obj_envio.id_pais_origen & Chr(34) & "")
            obj_sb.Append("," & Chr(34) & "HfEstadoE" & Chr(34) & ":" & Chr(34) & obj_envio.id_estado_origen & Chr(34) & "")
            obj_sb.Append("," & Chr(34) & "HfCiudadE" & Chr(34) & ":" & Chr(34) & obj_envio.id_ciudad_origen & Chr(34) & "")
            obj_sb.Append("," & Chr(34) & "ClienteR" & Chr(34) & ":" & Chr(34) & obj_envio.id_cliente_receptor & Chr(34) & "")
            obj_sb.Append("," & Chr(34) & "HfPaisR" & Chr(34) & ":" & Chr(34) & obj_envio.id_pais_destino & Chr(34) & "")
            obj_sb.Append("," & Chr(34) & "HfEstadoR" & Chr(34) & ":" & Chr(34) & obj_envio.id_estado_destino & Chr(34) & "")
            obj_sb.Append("," & Chr(34) & "HfCiudadR" & Chr(34) & ":" & Chr(34) & obj_envio.id_ciudad_destino & Chr(34) & "")
            obj_sb.Append("," & Chr(34) & "DireccionEnvio" & Chr(34) & ":" & Chr(34) & obj_envio.direccion_destino & Chr(34) & "")
            obj_sb.Append("," & Chr(34) & "TotalR" & Chr(34) & ":" & Chr(34) & obj_envio.costo_envio & Chr(34) & "")


            obj_sb.Append("," & Chr(34) & "NumeroP" & Chr(34) & ":" & Chr(34) & obj_envio.numero & Chr(34) & "")
            obj_sb.Append("," & Chr(34) & "PesoP" & Chr(34) & ":" & Chr(34) & obj_envio.peso & Chr(34) & "")
            obj_sb.Append("," & Chr(34) & "VolumenP" & Chr(34) & ":" & Chr(34) & obj_envio.volumen & Chr(34) & "")
            obj_sb.Append("," & Chr(34) & "CostoP" & Chr(34) & ":" & Chr(34) & obj_envio.costo & Chr(34) & "")
            obj_sb.Append("," & Chr(34) & "DescripcionP" & Chr(34) & ":" & Chr(34) & obj_envio.descripcion & Chr(34) & "")

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
            'obj_envio.codigo = Right("EN" & New cls_paises(ac_Funciones.formato_Numero(var_JObject("Id_pais_origen").ToString)).nombre.Substring(0, 1) & New cls_ciudades(ac_Funciones.formato_Numero(var_JObject("Id_ciudad_origen").ToString)).nombre.Substring(0, 3).ToUpper & Right("0000" & (cls_envios.SiguienteNumero() + 1).ToString, 4).ToString, 10)
            obj_envio.codigo = Right("EN" & Right("00000000" & (cls_envios.SiguienteNumero() + 1).ToString, 8), 9)
            obj_envio.estatus = 0
        Else
            obj_envio.codigo = ac_Funciones.formato_Texto(var_JObject("Codigo").ToString)
        End If

        ' obj_envio.codigo = ac_Funciones.formato_Texto(var_JObject("Codigo").ToString)
        obj_envio.id_cliente_emisor = ac_Funciones.formato_Texto(var_JObject("ClienteE").ToString)
        obj_envio.id_pais_origen = ac_Funciones.formato_Texto(var_JObject("PaisE").ToString)
        obj_envio.id_estado_origen = ac_Funciones.formato_Texto(var_JObject("EstadoE").ToString)
        obj_envio.id_ciudad_origen = ac_Funciones.formato_Texto(var_JObject("CiudadE").ToString)
        obj_envio.id_cliente_receptor = ac_Funciones.formato_Texto(var_JObject("ClienteE").ToString)
        obj_envio.id_pais_destino = ac_Funciones.formato_Texto(var_JObject("PaisR").ToString)
        obj_envio.id_estado_destino = ac_Funciones.formato_Texto(var_JObject("EstadoR").ToString)
        obj_envio.id_ciudad_destino = ac_Funciones.formato_Texto(var_JObject("CiudadR").ToString)
        obj_envio.direccion_destino = ac_Funciones.formato_Texto(var_JObject("DireccionEnvio").ToString)
        obj_envio.costo_envio = ac_Funciones.formato_Texto(var_JObject("TotalR").ToString)

        obj_envio.numero = Right("000000000000" & (cls_envios.SiguienteNumeroPaquete() + 1).ToString, 12)
        obj_envio.peso = var_JObject("PesoP").ToString
        obj_envio.volumen = var_JObject("VolumenP").ToString
        obj_envio.costo = var_JObject("CostoP").ToString
        obj_envio.descripcion = var_JObject("DescripcionP").ToString


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

    Sub guardarCliente()
        Dim var_StreamReader = New System.IO.StreamReader(Request.InputStream)
        Dim var_JObject As JObject = JObject.Parse(var_StreamReader.ReadToEnd)
        Dim var_tipo As Integer = var_JObject("TipoCliente").ToString

        Dim obj_cliente As New cls_envios_clientes
        obj_cliente.identificador = ac_Funciones.formato_Texto(var_JObject("IdentificadorC").ToString)
        obj_cliente.nombre = ac_Funciones.formato_Texto(var_JObject("NombreC").ToString)
        obj_cliente.telefono = ac_Funciones.formato_Texto(var_JObject("TelefonoC").ToString)
        obj_cliente.email = ac_Funciones.formato_Texto(var_JObject("EmailC").ToString)

        Dim var_msj As String = ""
        If Not obj_cliente.Actualizar(var_msj) Then
            Response.Write("{" & Chr(34) & "rslt" & Chr(34) & ":" & Chr(34) & "error" & Chr(34) & "," & Chr(34) & "msj" & Chr(34) & ":" & Chr(34) & var_Error & Chr(34) & "}")
        Else
            Response.Write("{" & Chr(34) & "rslt" & Chr(34) & ":" & Chr(34) & "exito" & Chr(34) & "," & Chr(34) & "msj" & Chr(34) & ":" & Chr(34) & "" & Chr(34) & "," & Chr(34) & "idCliente" & Chr(34) & ":" & Chr(34) & obj_cliente.Id & Chr(34) & "," & Chr(34) & "Identificador" & Chr(34) & ":" & Chr(34) & obj_cliente.identificador & Chr(34) & "," & Chr(34) & "Nombre" & Chr(34) & ":" & Chr(34) & obj_cliente.nombre & Chr(34) & "," & Chr(34) & "Tipo" & Chr(34) & ":" & Chr(34) & var_tipo & Chr(34) & "}")
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
                obj_sb.Append("," & Chr(34) & "IdentificadorEmisor" & Chr(34) & ":" & Chr(34) & obj_cliente.identificador & Chr(34) & "")
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
                obj_sb.Append("," & Chr(34) & "IdentificadorReceptor" & Chr(34) & ":" & Chr(34) & obj_cliente.identificador & Chr(34) & "")
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
                Response.Write("{" & Chr(34) & "IdentificadorEmisor" & Chr(34) & ":" & Chr(34) & obj_dt_int.Rows(0).Item(0) & Chr(34) & "}")
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
                Response.Write("{" & Chr(34) & "IdentificadorReceptor" & Chr(34) & ":" & Chr(34) & obj_dt_int.Rows(0).Item(0) & Chr(34) & "}")
            Else
                Response.Write("{" & Chr(34) & "error" & Chr(34) & ":" & Chr(34) & "-1" & Chr(34) & "}")
            End If
        End If

        Response.End()
    End Sub

    Sub paises()
        Dim var_error As String = ""
        Dim obj_dt_int As System.Data.DataTable = cls_paises.Lista
        Dim var_json As String = JsonConvert.SerializeObject(obj_dt_int)
        Response.Clear()
        Response.ClearHeaders()
        Response.ClearContent()
        Response.Write("{ " & Chr(34) & "aaData" & Chr(34) & ":" & var_json & "}")
        Response.End()
    End Sub

    Sub estados()
        Dim var_error As String = ""
        Dim var_pais As String = Val(Request.QueryString("p"))
        Dim obj_dt_int As System.Data.DataTable = cls_estados.Lista("id_pais=" & var_pais)
        Dim var_json As String = JsonConvert.SerializeObject(obj_dt_int)
        Response.Clear()
        Response.ClearHeaders()
        Response.ClearContent()
        Response.Write("{ " & Chr(34) & "aaData" & Chr(34) & ":" & var_json & "}")
        Response.End()
    End Sub

    Sub ciudades()
        Dim var_error As String = ""
        Dim var_estado As String = Val(Request.QueryString("e"))
        Dim obj_dt_int As System.Data.DataTable = cls_ciudades.Lista("id_estado=" & var_estado)
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