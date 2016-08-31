#Region "IMPORTS"
Imports System.Data
Imports System.Data.SqlClient
Imports System.Web.Services
Imports Newtonsoft.Json
Imports Newtonsoft.Json.Linq
#End Region

Partial Class frm_freelances
    Inherits System.Web.UI.Page

#Region "VARIABLES"
    Dim obj_Session As cls_Sesion
    Dim obj_freelance As cls_freelances
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
        If Not IsNothing(Session.Contents("obj_freelance")) Then
            obj_freelance = Session.Contents("obj_freelance")
        Else
            obj_freelance = New cls_freelances
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
                Case "validar_freelance"
                    validarfreelance()

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
            If Not New cls_permisos(obj_Session.Usuario.Id, New cls_modulos("freelances").Id, New cls_acciones("ver").Id).permiso Then
                Dim obj_log As New cls_logs
                obj_log.ComentarioLog = "Listado de freelances"
                obj_log.FechaLog = Now
                obj_log.id_Menu = New cls_modulos("freelances").Id
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
                If Not New cls_permisos(obj_Session.Usuario.Id, New cls_modulos("freelances").Id, New cls_acciones("editar").Id).permiso Then
                    Dim obj_freelances As New cls_freelances(var_id)

                    Dim obj_log As New cls_logs
                    obj_log.ComentarioLog = "Editar cliente: " & obj_freelances.Id & " - " & obj_freelances.Nombre
                    obj_log.FechaLog = Now
                    obj_log.id_Menu = New cls_modulos("freelances").Id
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
                If Not New cls_permisos(obj_Session.Usuario.Id, New cls_modulos("freelances").Id, New cls_acciones("agregar").Id).permiso Then
                    Dim obj_log As New cls_logs
                    obj_log.ComentarioLog = "Agregar cliente"
                    obj_log.FechaLog = Now
                    obj_log.id_Menu = New cls_modulos("freelances").Id
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
            obj_freelance = New cls_freelances(var_Id)
            Session.Contents("obj_freelance") = obj_freelance

            Dim var_User As String = ""
            If obj_freelance.Id > 0 Then
                var_User = obj_freelance.id_usuario_reg
            Else
                var_User = obj_Session.Usuario.nombre
            End If
            obj_sb.Append("," & Chr(34) & "Rif" & Chr(34) & ":" & Chr(34) & obj_freelance.Rif & Chr(34) & "")
            obj_sb.Append("," & Chr(34) & "Nombre" & Chr(34) & ":" & Chr(34) & obj_freelance.Nombre & Chr(34) & "")
            obj_sb.Append("," & Chr(34) & "TipoVenta" & Chr(34) & ":" & Chr(34) & obj_freelance.tipo_venta & Chr(34) & "")
            obj_sb.Append("," & Chr(34) & "Direccion" & Chr(34) & ":" & Chr(34) & obj_freelance.Direccion & Chr(34) & "")
            obj_sb.Append("," & Chr(34) & "TelefonoF" & Chr(34) & ":" & Chr(34) & obj_freelance.telefono_fijo & Chr(34) & "")
            obj_sb.Append("," & Chr(34) & "TelefonoM" & Chr(34) & ":" & Chr(34) & obj_freelance.telefono_movil & Chr(34) & "")
            obj_sb.Append("," & Chr(34) & "Email" & Chr(34) & ":" & Chr(34) & obj_freelance.Email & Chr(34) & "")
            obj_sb.Append("," & Chr(34) & "Codigo" & Chr(34) & ":" & Chr(34) & obj_freelance.codigo & Chr(34) & "")
            obj_sb.Append("," & Chr(34) & "LimiteCredito" & Chr(34) & ":" & Chr(34) & obj_freelance.limite_credito & Chr(34) & "")
            obj_sb.Append("," & Chr(34) & "Pais" & Chr(34) & ":" & Chr(34) & obj_freelance.pais & Chr(34) & "")
            obj_sb.Append("," & Chr(34) & "Estado" & Chr(34) & ":" & Chr(34) & obj_freelance.estado & Chr(34) & "")
            obj_sb.Append("," & Chr(34) & "Ciudad" & Chr(34) & ":" & Chr(34) & obj_freelance.ciudad & Chr(34) & "")

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

        If Not IsNothing(Session.Contents("obj_freelance")) Then
            obj_freelance = Session.Contents("obj_freelance")
        Else
            obj_freelance = New cls_freelances
        End If

        If obj_freelance.Id = 0 Then
            obj_freelance.id_usuario_reg = DirectCast(Session.Contents("obj_Session"), cls_Sesion).Usuario.Id
            obj_freelance.fecha_reg = Now.Date
            obj_freelance.codigo = Right("FR" & New cls_paises(ac_Funciones.formato_Numero(var_JObject("Pais").ToString)).nombre.Substring(0, 1) & New cls_ciudades(ac_Funciones.formato_Numero(var_JObject("Ciudad").ToString)).nombre.Substring(0, 3).ToUpper & Right("0000" & (cls_freelances.SiguienteNumero() + 1).ToString, 4).ToString, 10)
            'Right("AE" & Right("0000" & (cls_aerolineas.SiguienteNumero() + 1).ToString, 4).ToString, 6)
        Else
            obj_freelance.codigo = ac_Funciones.formato_Texto(var_JObject("Codigo").ToString)
        End If

        obj_freelance.rif = ac_Funciones.formato_Texto(var_JObject("Rif").ToString).ToUpper
        obj_freelance.nombre = ac_Funciones.formato_Texto(var_JObject("Nombre").ToString)
        obj_freelance.Direccion = ac_Funciones.formato_Texto(var_JObject("Direccion").ToString)
        obj_freelance.telefono_fijo = ac_Funciones.formato_Texto(var_JObject("TelefonoF").ToString)
        obj_freelance.telefono_movil = ac_Funciones.formato_Texto(var_JObject("TelefonoM").ToString)
        obj_freelance.email = ac_Funciones.formato_Texto(var_JObject("Email").ToString)
        obj_freelance.tipo_venta = ac_Funciones.formato_Texto(var_JObject("TipoVenta").ToString)
        obj_freelance.limite_credito = ac_Funciones.formato_Numero(var_JObject("LimiteCredito").ToString, True)
        obj_freelance.pais = ac_Funciones.formato_Numero(var_JObject("Pais").ToString)
        obj_freelance.estado = ac_Funciones.formato_Numero(var_JObject("Estado").ToString)
        obj_freelance.ciudad = ac_Funciones.formato_Numero(var_JObject("Ciudad").ToString)

        Response.ContentType = "application/json"
        Response.Clear()
        Response.ClearHeaders()
        Response.ClearContent()
        If Not obj_freelance.Actualizar(var_Error, obj_Session.Usuario.Id) Then
            Response.Write("{" & Chr(34) & "rslt" & Chr(34) & ":" & Chr(34) & "error" & Chr(34) & "," & Chr(34) & "msj" & Chr(34) & ":" & Chr(34) & var_Error & Chr(34) & "}")
        Else
            Response.Write("{" & Chr(34) & "rslt" & Chr(34) & ":" & Chr(34) & "exito" & Chr(34) & "," & Chr(34) & "msj" & Chr(34) & ":" & Chr(34) & "" & Chr(34) & "," & Chr(34) & "id" & Chr(34) & ":" & Chr(34) & obj_freelance.Id & Chr(34) & "}")
        End If
        Response.End()
    End Sub
    Private Sub validarfreelance()
        Dim var_error As String = ""
        Dim obj_sb As New StringBuilder

        Dim var_sr = New System.IO.StreamReader(Request.InputStream)
        Dim var_data As JObject = JObject.Parse(var_sr.ReadToEnd)
        Dim var_rif As String = ac_Funciones.formato_Texto(var_data("rif").ToString)


        Dim obj_dt_int As System.Data.DataTable = cls_freelances.ConsultaFreelance(var_rif, var_error)
        If obj_dt_int.Rows.Count > 0 Then
            obj_sb.Append("," & Chr(34) & "Nombre" & Chr(34) & ":" & Chr(34) & obj_dt_int.Rows.Item("Nombre").ToString() & Chr(34) & "")
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

            If Not IsNothing(Session.Contents("obj_freelance")) Then
                obj_freelance = Session.Contents("obj_freelance")
            Else
                obj_freelance = New cls_freelances
            End If

            Dim var_Id() As String = var_Ids.Split(",")
            For i As Integer = var_Id.Count - 1 To 0 Step -1
                obj_freelance.comisiones.RemoveAt(var_Id(i) - 1)
            Next
            Session.Contents("obj_freelance") = obj_freelance
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
        If Not IsNothing(Session.Contents("obj_freelance")) Then
            obj_freelance = Session.Contents("obj_freelance")
        Else
            obj_freelance = New cls_freelances
        End If

        Dim obj_StringBuilder As New StringBuilder
        Try
            Dim var_StreamReader = New System.IO.StreamReader(Request.InputStream)
            Dim var_JObject As JObject = JObject.Parse(var_StreamReader.ReadToEnd)
            Dim var_Position As Integer = ac_Funciones.formato_Numero(var_JObject("hdn_comisionId").ToString) - 1
            obj_StringBuilder.Append("," & Chr(34) & "Freelance" & Chr(34) & ":" & Chr(34) & obj_freelance.Comisiones(var_Position).freelance & Chr(34) & "")
            obj_StringBuilder.Append("," & Chr(34) & "Tipo" & Chr(34) & ":" & Chr(34) & obj_freelance.Comisiones(var_Position).Tipo & Chr(34) & "")
            obj_StringBuilder.Append("," & Chr(34) & "Comision" & Chr(34) & ":" & Chr(34) & obj_freelance.Comisiones(var_Position).Comision & Chr(34) & "")
            obj_StringBuilder.Append("," & Chr(34) & "Forma" & Chr(34) & ":" & Chr(34) & obj_freelance.Comisiones(var_Position).Forma & Chr(34) & "")

            If obj_freelance.Comisiones(var_Position).Tipo = 1 Then
                obj_StringBuilder.Append("," & Chr(34) & "Detalle" & Chr(34) & ":" & Chr(34) & obj_freelance.Comisiones(var_Position).Aerolinea.Id & Chr(34) & "")
            End If
            If obj_freelance.Comisiones(var_Position).Tipo = 2 Then
                obj_StringBuilder.Append("," & Chr(34) & "Detalle" & Chr(34) & ":" & Chr(34) & obj_freelance.Comisiones(var_Position).Hotel.Id & Chr(34) & "")
            End If
            If obj_freelance.Comisiones(var_Position).Tipo = 3 Then
                obj_StringBuilder.Append("," & Chr(34) & "Detalle" & Chr(34) & ":" & Chr(34) & obj_freelance.Comisiones(var_Position).Vehiculo.Id & Chr(34) & "")
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
        If Not IsNothing(Session.Contents("obj_freelance")) Then
            obj_freelance = Session.Contents("obj_freelance")
        Else
            obj_freelance = New cls_freelances
        End If

        Dim obj_DataTable As System.Data.DataTable = obj_freelance.Listacomisiones
        Dim var_JSon As String = JsonConvert.SerializeObject(obj_DataTable)
        Response.Clear()
        Response.ClearHeaders()
        Response.ClearContent()
        Response.Write("{ " & Chr(34) & "aaData" & Chr(34) & ":" & var_JSon & "}")
        Response.End()
    End Sub

    Sub comisionesave()
        Try
            If Not IsNothing(Session.Contents("obj_freelance")) Then
                obj_freelance = Session.Contents("obj_freelance")
            Else
                obj_freelance = New cls_freelances
            End If

            Dim var_StreamReader = New System.IO.StreamReader(Request.InputStream)
            Dim var_JObject As JObject = JObject.Parse(var_StreamReader.ReadToEnd)
            Dim var_comisionId As Integer = CInt(var_JObject("hdn_comisionId").ToString)

            If var_comisionId > 0 Then
                obj_freelance.Comisiones(var_comisionId - 1).Tipo = var_JObject("Tipo").ToString
                obj_freelance.Comisiones(var_comisionId - 1).Comision = var_JObject("Comision").ToString
                obj_freelance.Comisiones(var_comisionId - 1).Forma = var_JObject("Forma").ToString

                If var_JObject("Tipo").ToString = 1 Then
                    obj_freelance.Comisiones(var_comisionId - 1).Aerolinea = New cls_aerolineas(var_JObject("Detalle").ToString)
                End If
                If var_JObject("Tipo").ToString = 2 Then
                    obj_freelance.Comisiones(var_comisionId - 1).Hotel = New cls_hoteles(var_JObject("Detalle").ToString)
                End If
                If var_JObject("Tipo").ToString = 3 Then
                    obj_freelance.Comisiones(var_comisionId - 1).Vehiculo = New cls_vehiculos(var_JObject("Detalle").ToString)
                End If
            Else
                Dim obj_freelances_comisiones As New cls_freelances_configuracion
                obj_freelances_comisiones.Tipo = var_JObject("Tipo").ToString
                obj_freelances_comisiones.Comision = var_JObject("Comision").ToString
                obj_freelances_comisiones.Forma = var_JObject("Forma").ToString

                If var_JObject("Tipo").ToString = 1 Then
                    obj_freelances_comisiones.Aerolinea = New cls_aerolineas(var_JObject("Detalle").ToString)
                End If
                If var_JObject("Tipo").ToString = 2 Then
                    obj_freelances_comisiones.Hotel = New cls_hoteles(var_JObject("Detalle").ToString)
                End If
                If var_JObject("Tipo").ToString = 3 Then
                    obj_freelances_comisiones.Vehiculo = New cls_vehiculos(var_JObject("Detalle").ToString)
                End If
                obj_freelance.comisiones.Add(obj_freelances_comisiones)
            End If
            Session.Contents("obj_freelance") = obj_freelance
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