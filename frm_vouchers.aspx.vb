#Region "IMPORTS"
Imports System.Data
Imports System.Data.SqlClient
Imports System.Web.Services
Imports Newtonsoft.Json
Imports Newtonsoft.Json.Linq
#End Region

Partial Class frm_vouchers
    Inherits System.Web.UI.Page

#Region "VARIABLES"
    Dim obj_Session As cls_Sesion
    Dim obj_voucher As cls_vouchers
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
        If Not IsNothing(Session.Contents("obj_voucher")) Then
            obj_voucher = Session.Contents("obj_voucher")
        Else
            obj_voucher = New cls_vouchers
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


                    '* contactos
                Case "contactoDelete"
                    contactoDelete()
                Case "contactoEdit"
                    contactoEdit()
                Case "contactoLoad"
                    contactoLoad()
                Case "contactosave"
                    contactosave()

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
            If Not New cls_permisos(obj_Session.Usuario.Id, New cls_modulos("vouchers").Id, New cls_acciones("ver").Id).permiso Then
                Dim obj_log As New cls_logs
                obj_log.ComentarioLog = "Listado de vouchers"
                obj_log.FechaLog = Now
                obj_log.id_Menu = New cls_modulos("vouchers").Id
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
                If Not New cls_permisos(obj_Session.Usuario.Id, New cls_modulos("vouchers").Id, New cls_acciones("editar").Id).permiso Then
                    Dim obj_vouchers As New cls_vouchers(var_id)

                    Dim obj_log As New cls_logs
                    obj_log.ComentarioLog = "Editar cliente: " & obj_vouchers.Id & " - " & obj_vouchers.Numero
                    obj_log.FechaLog = Now
                    obj_log.id_Menu = New cls_modulos("vouchers").Id
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
                If Not New cls_permisos(obj_Session.Usuario.Id, New cls_modulos("vouchers").Id, New cls_acciones("agregar").Id).permiso Then
                    Dim obj_log As New cls_logs
                    obj_log.ComentarioLog = "Agregar cliente"
                    obj_log.FechaLog = Now
                    obj_log.id_Menu = New cls_modulos("vouchers").Id
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
            obj_voucher = New cls_vouchers(var_Id)
            Session.Contents("obj_voucher") = obj_voucher

            Dim var_User As String = ""
            If obj_voucher.Id > 0 Then
                var_User = obj_voucher.id_usuario_reg
            Else
                var_User = obj_Session.Usuario.nombre
            End If
            If obj_voucher.Id > 0 Then
                'obj_sb.Append("," & Chr(34) & "Codigo" & Chr(34) & ":" & Chr(34) & obj_voucher.codigo & Chr(34) & "")
            Else
                obj_sb.Append("," & Chr(34) & "Codigo" & Chr(34) & ":" & Chr(34) & Right("AE" & Right("0000" & (cls_vouchers.SiguienteNumero() + 1).ToString, 4).ToString, 6) & Chr(34) & "")
            End If
            'obj_sb.Append("," & Chr(34) & "Nombre" & Chr(34) & ":" & Chr(34) & obj_voucher.nombre & Chr(34) & "")
            'obj_sb.Append("," & Chr(34) & "RazonSocial" & Chr(34) & ":" & Chr(34) & obj_voucher.razon_social & Chr(34) & "")
            'obj_sb.Append("," & Chr(34) & "Identificador" & Chr(34) & ":" & Chr(34) & obj_voucher.identificador & Chr(34) & "")
            'obj_sb.Append("," & Chr(34) & "Direccion" & Chr(34) & ":" & Chr(34) & obj_voucher.direccion & Chr(34) & "")
            'obj_sb.Append("," & Chr(34) & "TelefonoFijo" & Chr(34) & ":" & Chr(34) & obj_voucher.telefono_fijo & Chr(34) & "")
            'obj_sb.Append("," & Chr(34) & "TelefonoMovil" & Chr(34) & ":" & Chr(34) & obj_voucher.telefono_movil & Chr(34) & "")
            'obj_sb.Append("," & Chr(34) & "Email" & Chr(34) & ":" & Chr(34) & obj_voucher.email & Chr(34) & "")
            'obj_sb.Append("," & Chr(34) & "Web" & Chr(34) & ":" & Chr(34) & obj_voucher.web & Chr(34) & "")
            'obj_sb.Append("," & Chr(34) & "Comision" & Chr(34) & ":" & Chr(34) & obj_voucher.comision & Chr(34) & "")
            'obj_sb.Append("," & Chr(34) & "IATA" & Chr(34) & ":" & Chr(34) & obj_voucher.IATA & Chr(34) & "")

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

        If Not IsNothing(Session.Contents("obj_voucher")) Then
            obj_voucher = Session.Contents("obj_voucher")
        Else
            obj_voucher = New cls_vouchers
        End If

        If obj_voucher.Id = 0 Then
            obj_voucher.id_usuario_reg = DirectCast(Session.Contents("obj_Session"), cls_Sesion).Usuario.Id
            obj_voucher.fecha_reg = Now.Date
            'obj_voucher.codigo = Right("AE" & Right("0000" & (cls_vouchers.SiguienteNumero() + 1).ToString, 4).ToString, 6)
        Else
            'obj_voucher.codigo = ac_Funciones.formato_Texto(var_JObject("Codigo").ToString)
        End If

        'obj_voucher.nombre = ac_Funciones.formato_Texto(var_JObject("Nombre").ToString)
        'obj_voucher.razon_social = ac_Funciones.formato_Texto(var_JObject("RazonSocial").ToString)
        'obj_voucher.identificador = ac_Funciones.formato_Texto(var_JObject("Identificador").ToString)
        'obj_voucher.direccion = ac_Funciones.formato_Texto(var_JObject("Direccion").ToString)
        'obj_voucher.telefono_fijo = ac_Funciones.formato_Texto(var_JObject("TelefonoFijo").ToString)
        'obj_voucher.telefono_movil = ac_Funciones.formato_Texto(var_JObject("TelefonoMovil").ToString)
        'obj_voucher.email = ac_Funciones.formato_Texto(var_JObject("Email").ToString)
        'obj_voucher.web = ac_Funciones.formato_Texto(var_JObject("Web").ToString)
        'obj_voucher.comision = ac_Funciones.formato_Texto(var_JObject("Comision").ToString)
        'obj_voucher.IATA = ac_Funciones.formato_Texto(var_JObject("IATA").ToString)
        Response.ContentType = "application/json"
        Response.Clear()
        Response.ClearHeaders()
        Response.ClearContent()
        If Not obj_voucher.Actualizar(var_Error) Then
            Response.Write("{" & Chr(34) & "rslt" & Chr(34) & ":" & Chr(34) & "error" & Chr(34) & "," & Chr(34) & "msj" & Chr(34) & ":" & Chr(34) & var_Error & Chr(34) & "}")
        Else
            Response.Write("{" & Chr(34) & "rslt" & Chr(34) & ":" & Chr(34) & "exito" & Chr(34) & "," & Chr(34) & "msj" & Chr(34) & ":" & Chr(34) & "" & Chr(34) & "," & Chr(34) & "id" & Chr(34) & ":" & Chr(34) & obj_voucher.Id & Chr(34) & "}")
        End If
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

            If Not IsNothing(Session.Contents("obj_voucher")) Then
                obj_voucher = Session.Contents("obj_voucher")
            Else
                obj_voucher = New cls_vouchers
            End If

            Dim var_Id() As String = var_Ids.Split(",")
            For i As Integer = var_Id.Count - 1 To 0 Step -1
                obj_voucher.Detalle.RemoveAt(var_Id(i) - 1)
            Next
            Session.Contents("obj_voucher") = obj_voucher
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
        If Not IsNothing(Session.Contents("obj_voucher")) Then
            obj_voucher = Session.Contents("obj_voucher")
        Else
            obj_voucher = New cls_vouchers
        End If

        Dim obj_StringBuilder As New StringBuilder
        Try
            Dim var_StreamReader = New System.IO.StreamReader(Request.InputStream)
            Dim var_JObject As JObject = JObject.Parse(var_StreamReader.ReadToEnd)
            Dim var_Position As Integer = ac_Funciones.formato_Numero(var_JObject("hdn_contactoId").ToString) - 1
            'obj_StringBuilder.Append("," & Chr(34) & "Idvoucher" & Chr(34) & ":" & Chr(34) & obj_voucher.Contacto(var_Position).id_voucher & Chr(34) & "")
            'obj_StringBuilder.Append("," & Chr(34) & "NombreC" & Chr(34) & ":" & Chr(34) & obj_voucher.Contacto(var_Position).nombre & Chr(34) & "")
            'obj_StringBuilder.Append("," & Chr(34) & "CargoC" & Chr(34) & ":" & Chr(34) & obj_voucher.Contacto(var_Position).cargo & Chr(34) & "")
            'obj_StringBuilder.Append("," & Chr(34) & "TelefonoC" & Chr(34) & ":" & Chr(34) & obj_voucher.Contacto(var_Position).telefono & Chr(34) & "")

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
        If Not IsNothing(Session.Contents("obj_voucher")) Then
            obj_voucher = Session.Contents("obj_voucher")
        Else
            obj_voucher = New cls_vouchers
        End If

        Dim obj_DataTable As System.Data.DataTable = obj_voucher.ListaConceptos
        Dim var_JSon As String = JsonConvert.SerializeObject(obj_DataTable)
        Response.Clear()
        Response.ClearHeaders()
        Response.ClearContent()
        Response.Write("{ " & Chr(34) & "aaData" & Chr(34) & ":" & var_JSon & "}")
        Response.End()
    End Sub

    Sub contactosave()
        Try
            If Not IsNothing(Session.Contents("obj_voucher")) Then
                obj_voucher = Session.Contents("obj_voucher")
            Else
                obj_voucher = New cls_vouchers
            End If

            Dim var_StreamReader = New System.IO.StreamReader(Request.InputStream)
            Dim var_JObject As JObject = JObject.Parse(var_StreamReader.ReadToEnd)
            Dim var_contactoId As Integer = CInt(var_JObject("Idvoucher").ToString)

            If var_contactoId > 0 Then
                'obj_voucher.Contacto(var_contactoId - 1).id_voucher = var_JObject("Idvoucher").ToString
                'obj_voucher.Contacto(var_contactoId - 1).nombre = var_JObject("NombreC").ToString
                'obj_voucher.Contacto(var_contactoId - 1).cargo = var_JObject("CargoC").ToString
                'obj_voucher.Contacto(var_contactoId - 1).telefono = var_JObject("TelefonoC").ToString
            Else
                Dim obj_vouchers_contacto As New cls_vouchers_detalles
                'obj_vouchers_contacto.nombre = var_JObject("NombreC").ToString
                'obj_vouchers_contacto.cargo = var_JObject("CargoC").ToString
                'obj_vouchers_contacto.telefono = var_JObject("TelefonoC").ToString
                'obj_voucher.Contacto.Add(obj_vouchers_contacto)
            End If
            Session.Contents("obj_voucher") = obj_voucher
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