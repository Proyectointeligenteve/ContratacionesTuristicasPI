#Region "IMPORTS"
Imports System.Data
Imports System.Data.SqlClient
Imports System.Web.Services
Imports Newtonsoft.Json
Imports Newtonsoft.Json.Linq
#End Region

Partial Class frm_agencias
    Inherits System.Web.UI.Page

#Region "VARIABLES"
    Dim obj_Session As cls_Sesion
    Dim obj_Agencia As cls_agencias
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
        If Not IsNothing(Session.Contents("obj_Agencia")) Then
            obj_Agencia = Session.Contents("obj_Agencia")
        Else
            obj_Agencia = New cls_agencias
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
                Case "validar_agencia"
                    validarAgencia()

                    '* contactos
                Case "contactoDelete"
                    contactoDelete()
                Case "contactoEdit"
                    contactoEdit()
                Case "contactoLoad"
                    contactoLoad()
                Case "contactoSave"
                    contactoSave()
                    '* comisiones
                Case "comisionDelete"
                    comisionDelete()
                Case "comisionEdit"
                    comisionEdit()
                Case "comisionLoad"
                    comisionLoad()
                Case "comisionSave"
                    comisionesave()

                Case "cargar_paises"
                    paises()
                Case "cargar_estados"
                    estados()
                Case "cargar_ciudades"
                    ciudades()

                Case "cargar_tipos"
                    tipos()

                Case "cargar_aerolineas"
                    aerolineas()
                Case "cargar_hoteles"
                    hoteles()
                Case "cargar_vehiculos"
                    vehiculos()
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
            If Not New cls_permisos(obj_Session.Usuario.Id, New cls_modulos("agencias").Id, New cls_acciones("ver").Id).permiso Then
                Dim obj_log As New cls_logs
                obj_log.ComentarioLog = "Listado de agencias"
                obj_log.FechaLog = Now
                obj_log.id_Menu = New cls_modulos("agencias").Id
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
                If Not New cls_permisos(obj_Session.Usuario.Id, New cls_modulos("agencias").Id, New cls_acciones("editar").Id).permiso Then
                    Dim obj_agencias As New cls_agencias(var_id)

                    Dim obj_log As New cls_logs
                    obj_log.ComentarioLog = "Editar cliente: " & obj_agencias.Id & " - " & obj_agencias.Nombre
                    obj_log.FechaLog = Now
                    obj_log.id_Menu = New cls_modulos("agencias").Id
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
                If Not New cls_permisos(obj_Session.Usuario.Id, New cls_modulos("agencias").Id, New cls_acciones("agregar").Id).permiso Then
                    Dim obj_log As New cls_logs
                    obj_log.ComentarioLog = "Agregar cliente"
                    obj_log.FechaLog = Now
                    obj_log.id_Menu = New cls_modulos("agencias").Id
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
            obj_Agencia = New cls_agencias(var_Id)
            Session.Contents("obj_Agencia") = obj_Agencia

            Dim var_User As String = ""
            If obj_Agencia.Id > 0 Then
                var_User = obj_Agencia.id_usuario_reg
            Else
                var_User = obj_Session.Usuario.nombre
            End If
            obj_sb.Append("," & Chr(34) & "hdn_id" & Chr(34) & ":" & Chr(34) & obj_Agencia.Id & Chr(34) & "")
            obj_sb.Append("," & Chr(34) & "Rif" & Chr(34) & ":" & Chr(34) & obj_Agencia.Rif & Chr(34) & "")
            obj_sb.Append("," & Chr(34) & "Nombre" & Chr(34) & ":" & Chr(34) & obj_Agencia.Nombre & Chr(34) & "")
            obj_sb.Append("," & Chr(34) & "RazonSocial" & Chr(34) & ":" & Chr(34) & obj_Agencia.RazonSocial & Chr(34) & "")
            obj_sb.Append("," & Chr(34) & "Direccion" & Chr(34) & ":" & Chr(34) & obj_Agencia.Direccion & Chr(34) & "")
            obj_sb.Append("," & Chr(34) & "TelefonoF" & Chr(34) & ":" & Chr(34) & obj_Agencia.TelefonoFijo & Chr(34) & "")
            obj_sb.Append("," & Chr(34) & "TelefonoM" & Chr(34) & ":" & Chr(34) & obj_Agencia.TelefonoMovil & Chr(34) & "")
            obj_sb.Append("," & Chr(34) & "Web" & Chr(34) & ":" & Chr(34) & obj_Agencia.Web & Chr(34) & "")
            obj_sb.Append("," & Chr(34) & "Email" & Chr(34) & ":" & Chr(34) & obj_Agencia.Email & Chr(34) & "")
            obj_sb.Append("," & Chr(34) & "Codigo" & Chr(34) & ":" & Chr(34) & obj_Agencia.codigo & Chr(34) & "")
            obj_sb.Append("," & Chr(34) & "LimiteCredito" & Chr(34) & ":" & Chr(34) & obj_Agencia.limite_credito & Chr(34) & "")
            obj_sb.Append("," & Chr(34) & "Pais" & Chr(34) & ":" & Chr(34) & obj_Agencia.pais & Chr(34) & "")
            obj_sb.Append("," & Chr(34) & "Estado" & Chr(34) & ":" & Chr(34) & obj_Agencia.estado & Chr(34) & "")
            obj_sb.Append("," & Chr(34) & "Ciudad" & Chr(34) & ":" & Chr(34) & obj_Agencia.ciudad & Chr(34) & "")

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

        If Not IsNothing(Session.Contents("obj_Agencia")) Then
            obj_Agencia = Session.Contents("obj_Agencia")
        Else
            obj_Agencia = New cls_agencias
        End If

        If obj_Agencia.Id = 0 Then
            obj_Agencia.id_usuario_reg = DirectCast(Session.Contents("obj_Session"), cls_Sesion).Usuario.Id
            obj_Agencia.fecha_reg = Now.Date
            obj_Agencia.codigo = Right("AG" & New cls_paises(ac_Funciones.formato_Numero(var_JObject("Pais").ToString)).nombre.Substring(0, 1) & New cls_ciudades(ac_Funciones.formato_Numero(var_JObject("Ciudad").ToString)).nombre.Substring(0, 3).ToUpper & Right("0000" & (cls_agencias.SiguienteNumero() + 1).ToString, 4).ToString, 10)
            'Right("AE" & Right("0000" & (cls_aerolineas.SiguienteNumero() + 1).ToString, 4).ToString, 6)
        Else
            obj_Agencia.codigo = ac_Funciones.formato_Texto(var_JObject("Codigo").ToString)
        End If

        obj_Agencia.Rif = ac_Funciones.formato_Texto(var_JObject("Rif").ToString).ToUpper
        obj_Agencia.Nombre = ac_Funciones.formato_Texto(var_JObject("Nombre").ToString)
        obj_Agencia.RazonSocial = ac_Funciones.formato_Texto(var_JObject("RazonSocial").ToString)
        obj_Agencia.Direccion = ac_Funciones.formato_Texto(var_JObject("Direccion").ToString)
        obj_Agencia.TelefonoFijo = ac_Funciones.formato_Texto(var_JObject("TelefonoF").ToString)
        obj_Agencia.TelefonoMovil = ac_Funciones.formato_Texto(var_JObject("TelefonoM").ToString)
        obj_Agencia.Web = ac_Funciones.formato_Texto(var_JObject("Web").ToString)
        obj_Agencia.Email = ac_Funciones.formato_Texto(var_JObject("Email").ToString)
        obj_Agencia.limite_credito = ac_Funciones.formato_Numero(var_JObject("LimiteCredito").ToString, True)
        obj_Agencia.pais = ac_Funciones.formato_Numero(var_JObject("Pais").ToString)
        obj_Agencia.estado = ac_Funciones.formato_Numero(var_JObject("Estado").ToString)
        obj_Agencia.ciudad = ac_Funciones.formato_Numero(var_JObject("Ciudad").ToString)

        Response.ContentType = "application/json"
        Response.Clear()
        Response.ClearHeaders()
        Response.ClearContent()
        If Not obj_Agencia.Actualizar(var_Error) Then
            Response.Write("{" & Chr(34) & "rslt" & Chr(34) & ":" & Chr(34) & "error" & Chr(34) & "," & Chr(34) & "msj" & Chr(34) & ":" & Chr(34) & var_Error & Chr(34) & "}")
        Else
            Response.Write("{" & Chr(34) & "rslt" & Chr(34) & ":" & Chr(34) & "exito" & Chr(34) & "," & Chr(34) & "msj" & Chr(34) & ":" & Chr(34) & "" & Chr(34) & "," & Chr(34) & "id" & Chr(34) & ":" & Chr(34) & obj_Agencia.Id & Chr(34) & "}")
        End If
        Response.End()
    End Sub
    Private Sub validarAgencia()
        Dim var_error As String = ""
        Dim obj_sb As New StringBuilder

        Dim var_sr = New System.IO.StreamReader(Request.InputStream)
        Dim var_data As JObject = JObject.Parse(var_sr.ReadToEnd)
        Dim var_rif As String = ac_Funciones.formato_Texto(var_data("rif").ToString)


        Dim obj_dt_int As System.Data.DataTable = cls_agencias.ConsultaAgencia(var_rif, var_error)
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

    Sub Cargarcontactos()
        Dim var_error As String = ""

        Dim obj_dt_int As System.Data.DataTable = cls_agencias_contactos.Lista()
        Dim var_json As String = JsonConvert.SerializeObject(obj_dt_int)
        Response.Clear()
        Response.ClearHeaders()
        Response.ClearContent()
        Response.Write("{ " & Chr(34) & "aaData" & Chr(34) & ":" & var_json & "}")
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

    Sub tipos()
        Dim var_error As String = ""
        Dim var_estado As String = Val(Request.QueryString("e"))
        Dim obj_dt_int As System.Data.DataTable = cls_tipos_configuracion.Lista
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
#End Region

#Region "   >> contacto"
    Sub contactoDelete()
        Dim var_Error As String = ""
        Try
            Dim var_StreamReader = New System.IO.StreamReader(Request.InputStream)
            Dim var_JObject As JObject = JObject.Parse(var_StreamReader.ReadToEnd)
            Dim var_Ids As String = var_JObject("hdn_contactoId")
            If var_Ids.Trim.Length > 0 Then
                var_Ids = var_Ids.Substring(1)
            End If

            If Not IsNothing(Session.Contents("obj_Agencia")) Then
                obj_Agencia = Session.Contents("obj_Agencia")
            Else
                obj_Agencia = New cls_agencias
            End If

            Dim var_Id() As String = var_Ids.Split(",")
            For i As Integer = var_Id.Count - 1 To 0 Step -1
                obj_Agencia.Contactos.RemoveAt(var_Id(i) - 1)
            Next
            Session.Contents("obj_Agencia") = obj_Agencia
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

    Sub contactoEdit()
        Dim var_Error As String = ""
        If Not IsNothing(Session.Contents("obj_Agencia")) Then
            obj_Agencia = Session.Contents("obj_Agencia")
        Else
            obj_Agencia = New cls_agencias
        End If

        Dim obj_StringBuilder As New StringBuilder
        Try
            Dim var_StreamReader = New System.IO.StreamReader(Request.InputStream)
            Dim var_JObject As JObject = JObject.Parse(var_StreamReader.ReadToEnd)
            Dim var_Position As Integer = ac_Funciones.formato_Numero(var_JObject("hdn_contactoId").ToString) - 1
            obj_StringBuilder.Append("," & Chr(34) & "Agencia" & Chr(34) & ":" & Chr(34) & obj_Agencia.Contactos(var_Position).Id_Agencia & Chr(34) & "")
            obj_StringBuilder.Append("," & Chr(34) & "NombreC" & Chr(34) & ":" & Chr(34) & obj_Agencia.Contactos(var_Position).nombre & Chr(34) & "")
            obj_StringBuilder.Append("," & Chr(34) & "CargoC" & Chr(34) & ":" & Chr(34) & obj_Agencia.Contactos(var_Position).cargo & Chr(34) & "")
            obj_StringBuilder.Append("," & Chr(34) & "TelefonoC" & Chr(34) & ":" & Chr(34) & obj_Agencia.Contactos(var_Position).telefono & Chr(34) & "")
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

    Sub contactoLoad()
        Dim var_Error As String = ""
        If Not IsNothing(Session.Contents("obj_Agencia")) Then
            obj_Agencia = Session.Contents("obj_Agencia")
        Else
            obj_Agencia = New cls_agencias
        End If

        Dim obj_DataTable As System.Data.DataTable = obj_Agencia.ListaContactos
        Dim var_JSon As String = JsonConvert.SerializeObject(obj_DataTable)
        Response.Clear()
        Response.ClearHeaders()
        Response.ClearContent()
        Response.Write("{ " & Chr(34) & "aaData" & Chr(34) & ":" & var_JSon & "}")
        Response.End()
    End Sub

    Sub contactoSave()
        Try
            If Not IsNothing(Session.Contents("obj_Agencia")) Then
                obj_Agencia = Session.Contents("obj_Agencia")
            Else
                obj_Agencia = New cls_agencias
            End If

            Dim var_StreamReader = New System.IO.StreamReader(Request.InputStream)
            Dim var_JObject As JObject = JObject.Parse(var_StreamReader.ReadToEnd)
            Dim var_contactoId As Integer = CInt(var_JObject("hdn_contactoId").ToString)

            If var_contactoId > 0 Then
                obj_Agencia.Contactos(var_contactoId - 1).nombre = var_JObject("NombreC").ToString
                obj_Agencia.Contactos(var_contactoId - 1).cargo = var_JObject("CargoC").ToString
                obj_Agencia.Contactos(var_contactoId - 1).telefono = var_JObject("TelefonoC").ToString
            Else
                Dim obj_agencias_contactos As New cls_agencias_contactos
                obj_agencias_contactos.nombre = var_JObject("NombreC").ToString
                obj_agencias_contactos.cargo = var_JObject("CargoC").ToString
                obj_agencias_contactos.telefono = var_JObject("TelefonoC").ToString
                obj_Agencia.contactos.Add(obj_agencias_contactos)
            End If
            Session.Contents("obj_Agencia") = obj_Agencia
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

#Region "   >> comision"
    Sub comisionDelete()
        Dim var_Error As String = ""
        Try
            Dim var_StreamReader = New System.IO.StreamReader(Request.InputStream)
            Dim var_JObject As JObject = JObject.Parse(var_StreamReader.ReadToEnd)
            Dim var_Ids As String = var_JObject("hdn_comisionId")
            If var_Ids.Trim.Length > 0 Then
                var_Ids = var_Ids.Substring(1)
            End If

            If Not IsNothing(Session.Contents("obj_Agencia")) Then
                obj_Agencia = Session.Contents("obj_Agencia")
            Else
                obj_Agencia = New cls_agencias
            End If

            Dim var_Id() As String = var_Ids.Split(",")
            For i As Integer = var_Id.Count - 1 To 0 Step -1
                obj_Agencia.comisiones.RemoveAt(var_Id(i) - 1)
            Next
            Session.Contents("obj_Agencia") = obj_Agencia
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

    Sub comisionEdit()
        Dim var_Error As String = ""
        If Not IsNothing(Session.Contents("obj_Agencia")) Then
            obj_Agencia = Session.Contents("obj_Agencia")
        Else
            obj_Agencia = New cls_agencias
        End If

        Dim obj_StringBuilder As New StringBuilder
        Try
            Dim var_StreamReader = New System.IO.StreamReader(Request.InputStream)
            Dim var_JObject As JObject = JObject.Parse(var_StreamReader.ReadToEnd)
            Dim var_Position As Integer = ac_Funciones.formato_Numero(var_JObject("hdn_comisionId").ToString) - 1
            obj_StringBuilder.Append("," & Chr(34) & "Agencia" & Chr(34) & ":" & Chr(34) & obj_Agencia.Comisiones(var_Position).Agencia & Chr(34) & "")
            obj_StringBuilder.Append("," & Chr(34) & "Tipo" & Chr(34) & ":" & Chr(34) & obj_Agencia.Comisiones(var_Position).Tipo & Chr(34) & "")
            obj_StringBuilder.Append("," & Chr(34) & "Comision" & Chr(34) & ":" & Chr(34) & obj_Agencia.Comisiones(var_Position).Comision & Chr(34) & "")
            obj_StringBuilder.Append("," & Chr(34) & "Forma" & Chr(34) & ":" & Chr(34) & obj_Agencia.Comisiones(var_Position).Forma & Chr(34) & "")

            If obj_Agencia.Comisiones(var_Position).Tipo = 1 Then
                obj_StringBuilder.Append("," & Chr(34) & "Detalle" & Chr(34) & ":" & Chr(34) & obj_Agencia.Comisiones(var_Position).Aerolinea.Id & Chr(34) & "")
            End If
            If obj_Agencia.Comisiones(var_Position).Tipo = 2 Then
                obj_StringBuilder.Append("," & Chr(34) & "Detalle" & Chr(34) & ":" & Chr(34) & obj_Agencia.Comisiones(var_Position).Hotel.Id & Chr(34) & "")
            End If
            If obj_Agencia.Comisiones(var_Position).Tipo = 3 Then
                obj_StringBuilder.Append("," & Chr(34) & "Detalle" & Chr(34) & ":" & Chr(34) & obj_Agencia.Comisiones(var_Position).Vehiculo.Id & Chr(34) & "")
            End If

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

    Sub comisionLoad()
        Dim var_Error As String = ""
        If Not IsNothing(Session.Contents("obj_Agencia")) Then
            obj_Agencia = Session.Contents("obj_Agencia")
        Else
            obj_Agencia = New cls_agencias
        End If

        Dim obj_DataTable As System.Data.DataTable = obj_Agencia.Listacomisiones
        Dim var_JSon As String = JsonConvert.SerializeObject(obj_DataTable)
        Response.Clear()
        Response.ClearHeaders()
        Response.ClearContent()
        Response.Write("{ " & Chr(34) & "aaData" & Chr(34) & ":" & var_JSon & "}")
        Response.End()
    End Sub

    Sub comisionesave()
        Try
            If Not IsNothing(Session.Contents("obj_Agencia")) Then
                obj_Agencia = Session.Contents("obj_Agencia")
            Else
                obj_Agencia = New cls_agencias
            End If

            Dim var_StreamReader = New System.IO.StreamReader(Request.InputStream)
            Dim var_JObject As JObject = JObject.Parse(var_StreamReader.ReadToEnd)
            Dim var_comisionId As Integer = CInt(var_JObject("hdn_comisionId").ToString)

            If var_comisionId > 0 Then
                obj_Agencia.Comisiones(var_comisionId - 1).Tipo = var_JObject("Tipo").ToString
                obj_Agencia.Comisiones(var_comisionId - 1).Comision = var_JObject("Comision").ToString
                obj_Agencia.Comisiones(var_comisionId - 1).Forma = var_JObject("Forma").ToString
                If var_JObject("Tipo").ToString = 1 Then
                    obj_Agencia.Comisiones(var_comisionId - 1).Aerolinea = New cls_aerolineas(var_JObject("Detalle").ToString)
                End If
                If var_JObject("Tipo").ToString = 2 Then
                    obj_Agencia.Comisiones(var_comisionId - 1).Hotel = New cls_hoteles(var_JObject("Detalle").ToString)
                End If
                If var_JObject("Tipo").ToString = 3 Then
                    obj_Agencia.Comisiones(var_comisionId - 1).Vehiculo = New cls_vehiculos(var_JObject("Detalle").ToString)
                End If
            Else
                Dim obj_agencias_comisiones As New cls_agencias_configuracion
                obj_agencias_comisiones.Tipo = var_JObject("Tipo").ToString
                obj_agencias_comisiones.Comision = var_JObject("Comision").ToString
                obj_agencias_comisiones.Forma = var_JObject("Forma").ToString
                If var_JObject("Tipo").ToString = 1 Then
                    obj_agencias_comisiones.Aerolinea = New cls_aerolineas(var_JObject("Detalle").ToString)
                End If
                If var_JObject("Tipo").ToString = 2 Then
                    obj_agencias_comisiones.Hotel = New cls_hoteles(var_JObject("Detalle").ToString)
                End If
                If var_JObject("Tipo").ToString = 3 Then
                    obj_agencias_comisiones.Vehiculo = New cls_vehiculos(var_JObject("Detalle").ToString)
                End If
                obj_Agencia.comisiones.Add(obj_agencias_comisiones)
            End If
            Session.Contents("obj_Agencia") = obj_Agencia
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