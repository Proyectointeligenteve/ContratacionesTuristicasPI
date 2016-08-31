Imports System.Data
Imports System.Data.SqlClient
Imports System.Web.Services
Imports Newtonsoft.Json
Imports Newtonsoft.Json.Linq

Partial Class frm_recibos
    Inherits System.Web.UI.Page
    Dim obj_Session As cls_Sesion
    Dim obj_recibo As cls_recibos

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
        If Not IsNothing(Session.Contents("obj_recibo")) Then
            obj_recibo = Session.Contents("obj_recibo")
        Else
            obj_recibo = New cls_recibos
        End If

        If Not IsNothing(Request.QueryString("fn")) Then
            Dim var_fn As String = Request.QueryString("fn").ToString
            Select Case var_fn
                Case "permisos"
                    Permisos()
                Case "cargar"
                    cargar()
                Case "guardar"
                    guardar()
                Case "cargar_detalles"
                    cargar_detalles()
                Case "GuardarDetalles"
                    GuardarDetalles()
                Case "EditarDetalles"
                    EditarDetalles()
                Case "EliminarDetalles"
                    EliminarDetalles()
                Case "buscarclientes"
                    buscar_clientes()
                Case "cargarcliente"
                    cargar_cliente()
                Case "buscarventas"
                    buscar_venta()
                Case "cargarventas"
                    cargar_venta()
                Case "cargar_moneda"
                    moneda()
                Case "cargar_formaPago"
                    formaPago()
                Case "cargar_banco"
                    banco()
                Case "cargar_detalles_ventas"
                    cargar_detalles_ventas()
                    'Case "verabonos"
                    '    ver_abonos()
            End Select
        End If
    End Sub
    Sub Permisos()
        Dim var_error As String = ""
        Dim obj_sb As New StringBuilder
        Try
            If Not New cls_permisos(obj_Session.Usuario.Id, New cls_modulos("recibos").Id, New cls_acciones("ver").Id).permiso Then
                Dim obj_log As New cls_logs
                obj_log.ComentarioLog = "Listado de recibos"
                obj_log.FechaLog = Now
                obj_log.id_Menu = New cls_modulos("recibos").Id
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
                If Not New cls_permisos(obj_Session.Usuario.Id, New cls_modulos("recibos").Id, New cls_acciones("editar").Id).permiso Then
                    Dim obj_recibo As New cls_recibos(var_id)

                    Dim obj_log As New cls_logs
                    obj_log.ComentarioLog = "Editar recibo: " & obj_recibo.Id & " - " & obj_recibo.Numero & " - " & obj_recibo.id_cliente & " - " & obj_recibo.IdVenta
                    obj_log.FechaLog = Now
                    obj_log.id_Menu = New cls_modulos("recibos").Id
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
                If Not New cls_permisos(obj_Session.Usuario.Id, New cls_modulos("recibos").Id, New cls_acciones("agregar").Id).permiso Then
                    Dim obj_log As New cls_logs
                    obj_log.ComentarioLog = "Agregar recibo"
                    obj_log.FechaLog = Now
                    obj_log.id_Menu = New cls_modulos("recibos").Id
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

    Sub cargar()
        Dim var_error As String = ""
        Dim obj_sb As New StringBuilder
        Try
            Dim var_sr = New System.IO.StreamReader(Request.InputStream)
            Dim var_data As JObject = JObject.Parse(var_sr.ReadToEnd)
            Dim var_id As Integer = ac_Funciones.formato_Numero(var_data("id").ToString)
            obj_recibo = New cls_recibos(var_id)
            Session.Contents("obj_recibo") = obj_recibo

            Dim var_usuario As String = ""
            If obj_recibo.id > 0 Then
                var_usuario = obj_recibo.id_usuario_reg
            Else
                var_usuario = obj_Session.Usuario.nombre
            End If
            If obj_recibo.id > 0 Then
                obj_sb.Append("," & Chr(34) & "Numero" & Chr(34) & ":" & Chr(34) & obj_recibo.numero & Chr(34) & "")
            Else
                obj_sb.Append("," & Chr(34) & "Numero" & Chr(34) & ":" & Chr(34) & Right("00000" & (cls_recibos.SiguienteNumero() + 1).ToString, 5) & Chr(34) & "")
            End If
            obj_sb.Append("," & Chr(34) & "Venta" & Chr(34) & ":" & Chr(34) & New cls_ventas(obj_recibo.IdVenta).numero & Chr(34) & "")
            obj_sb.Append("," & Chr(34) & "idCliente" & Chr(34) & ":" & Chr(34) & obj_recibo.id_cliente & Chr(34) & "")
            obj_sb.Append("," & Chr(34) & "Nombre" & Chr(34) & ":" & Chr(34) & New cls_pasajeros(obj_recibo.id_cliente).nombre & Chr(34) & "")
            'obj_sb.Append("," & Chr(34) & "Rif" & Chr(34) & ":" & Chr(34) & obj_recibo.id_cliente.rif & Chr(34) & "")
            obj_sb.Append("," & Chr(34) & "Telefono" & Chr(34) & ":" & Chr(34) & New cls_pasajeros(obj_recibo.id_cliente).telefono & Chr(34) & "")
            obj_sb.Append("," & Chr(34) & "Direccion" & Chr(34) & ":" & Chr(34) & " " & Chr(34) & "")
            obj_sb.Append("," & Chr(34) & "Fecha" & Chr(34) & ":" & Chr(34) & obj_recibo.fecha & Chr(34) & "")
            'obj_sb.Append("," & Chr(34) & "Monto" & Chr(34) & ":" & Chr(34) & obj_recibo.total & Chr(34) & "")
            obj_sb.Append("," & Chr(34) & "Observaciones" & Chr(34) & ":" & Chr(34) & obj_recibo.observaciones & Chr(34) & "")
            obj_sb.Append("," & Chr(34) & "idventa" & Chr(34) & ":" & Chr(34) & obj_recibo.IdVenta & Chr(34) & "")
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
    Sub guardar()
        Dim var_sr = New System.IO.StreamReader(Request.InputStream)
        Dim var_data As JObject = JObject.Parse(var_sr.ReadToEnd)

        If Not IsNothing(Session.Contents("obj_recibo")) Then
            obj_recibo = Session.Contents("obj_recibo")
        Else
            obj_recibo = New cls_recibos
        End If

        If obj_recibo.id = 0 Then
            obj_recibo.id_usuario_reg = DirectCast(Session.Contents("obj_Session"), cls_Sesion).Usuario.Id
            obj_recibo.fecha_reg = Now.Date
            'Else
            '    obj_recibo.id_usuario_ult = DirectCast(Session.Contents("obj_Session"), cls_Sesion).Usuario
            '    obj_recibo.fecha_ult = Now.Date
        End If
        'obj_recibo.id_cliente = obj_cliente
        obj_recibo.id_cliente = New cls_pasajeros(CInt(ac_Funciones.formato_Numero(var_data("Cliente").ToString))).Id
        obj_recibo.IdVenta = New cls_ventas(CInt(ac_Funciones.formato_Numero(var_data("Venta").ToString))).id
        obj_recibo.numero = ac_Funciones.formato_Texto(var_data("Numero").ToString)
        obj_recibo.nombre = ac_Funciones.formato_Texto(var_data("Nombre").ToString)
        'obj_recibo.rif = ac_Funciones.formato_Texto(var_data("Rif").ToString)
        'obj_recibo.direccion = ac_Funciones.formato_Texto(var_data("Direccion").ToString)
        'obj_recibo.telefono = ac_Funciones.formato_Texto(var_data("Telefono").ToString)
        obj_recibo.fecha = ac_Funciones.formato_Texto(var_data("Fecha").ToString)
        obj_recibo.id_moneda = New cls_monedas(1).Id
        obj_recibo.Monto = ac_Funciones.formato_Texto(var_data("Monto").ToString)
        obj_recibo.observaciones = ac_Funciones.formato_Texto(var_data("Observaciones").ToString)

        Response.ContentType = "application/json"
        Response.Clear()
        Response.ClearHeaders()
        Response.ClearContent()
        Dim var_error As String = ""
        If Not obj_recibo.Actualizar(var_error) Then
            Response.Write("{" & Chr(34) & "rslt" & Chr(34) & ":" & Chr(34) & "error" & Chr(34) & "," & Chr(34) & "msj" & Chr(34) & ":" & Chr(34) & var_error & Chr(34) & "}")
        Else
            Response.Write("{" & Chr(34) & "rslt" & Chr(34) & ":" & Chr(34) & "exito" & Chr(34) & "," & Chr(34) & "msj" & Chr(34) & ":" & Chr(34) & "" & Chr(34) & "}")
        End If
        Response.End()
    End Sub
    Sub cargar_cliente()
        Dim var_error As String = ""
        Dim obj_sb As New StringBuilder
        Try
            Dim var_sr = New System.IO.StreamReader(Request.InputStream)
            Dim var_data As JObject = JObject.Parse(var_sr.ReadToEnd)
            Dim var_id As Integer = ac_Funciones.formato_Numero(var_data("id").ToString)
            Dim obj_cliente = New cls_pasajeros(var_id)
            If var_id > 0 Then
                obj_sb.Append("," & Chr(34) & "Rif" & Chr(34) & ":" & Chr(34) & obj_cliente.rif & Chr(34) & "")
                obj_sb.Append("," & Chr(34) & "Nombre" & Chr(34) & ":" & Chr(34) & obj_cliente.Nombre & Chr(34) & "")
                obj_sb.Append("," & Chr(34) & "Telefono" & Chr(34) & ":" & Chr(34) & obj_cliente.telefono & Chr(34) & "")
                obj_sb.Append("," & Chr(34) & "Email" & Chr(34) & ":" & Chr(34) & obj_cliente.email & Chr(34) & "")
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
    Sub cargar_venta()
        Dim var_error As String = ""
        Dim obj_sb As New StringBuilder
        Try
            Dim var_sr = New System.IO.StreamReader(Request.InputStream)
            Dim var_data As JObject = JObject.Parse(var_sr.ReadToEnd)
            Dim var_id As Integer = ac_Funciones.formato_Numero(var_data("id").ToString)
            Dim obj_venta = New cls_ventas(var_id)
            obj_sb.Append("," & Chr(34) & "Pedido" & Chr(34) & ":" & Chr(34) & obj_venta.numero & Chr(34) & "")
            obj_sb.Append("," & Chr(34) & "idCliente" & Chr(34) & ":" & Chr(34) & obj_venta.id_cliente & Chr(34) & "")
            obj_sb.Append("," & Chr(34) & "Monto" & Chr(34) & ":" & Chr(34) & ac_Funciones.formato_Numero_Pantalla(obj_venta.monto_pagar) & Chr(34) & "")
            'Dim obj_dt_int As System.Data.DataTable = cls_recibos.Abonos2(var_id, var_error)
            'If obj_dt_int.Rows.Count > 0 Then
            '    For Each obj_DataRow As DataRow In obj_dt_int.Rows
            '        obj_sb.Append("," & Chr(34) & "Abonado" & Chr(34) & ":" & Chr(34) & ac_Funciones.formato_Numero_Pantalla(obj_DataRow.Item("abono").ToString) & Chr(34) & "")
            '    Next
            'Else
            '    obj_sb.Append("," & Chr(34) & "Abonado" & Chr(34) & ":" & Chr(34) & obj_venta.abonado & Chr(34) & "")
            'End If

            obj_sb.Append("," & Chr(34) & "idPedido" & Chr(34) & ":" & Chr(34) & obj_venta.id & Chr(34) & "")
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

    Sub cargar_detalles()
        Dim var_error As String = ""
        If Not IsNothing(Session.Contents("obj_recibo")) Then
            obj_recibo = Session.Contents("obj_recibo")
        Else
            obj_recibo = New cls_recibos
        End If

        Dim obj_dt_int As System.Data.DataTable = obj_recibo.ListaDetalleFormasPagos
        Dim var_json As String = JsonConvert.SerializeObject(obj_dt_int)
        Response.Clear()
        Response.ClearHeaders()
        Response.ClearContent()
        Response.Write("{ " & Chr(34) & "aaData" & Chr(34) & ":" & var_json & "}")
        Response.End()
    End Sub
    Sub EditarDetalles()
        Dim var_error As String = ""
        If Not IsNothing(Session.Contents("obj_recibo")) Then
            obj_recibo = Session.Contents("obj_recibo")
        Else
            obj_recibo = New cls_recibos
        End If

        Dim obj_sb As New StringBuilder
        Try
            Dim var_sr = New System.IO.StreamReader(Request.InputStream)
            Dim var_data As JObject = JObject.Parse(var_sr.ReadToEnd)
            Dim var_pos As Integer = ac_Funciones.formato_Numero(var_data("iddp").ToString) - 1
            obj_sb.Append("," & Chr(34) & "MontoPago" & Chr(34) & ":" & Chr(34) & ac_Funciones.formato_Numero(obj_recibo.FormaPago(var_pos).Monto.ToString) & Chr(34) & "")
            obj_sb.Append("," & Chr(34) & "FechaPago" & Chr(34) & ":" & Chr(34) & ac_Funciones.formato_Texto_Pantalla(obj_recibo.FormaPago(var_pos).fecha) & Chr(34) & "")
            obj_sb.Append("," & Chr(34) & "FormaPago" & Chr(34) & ":" & Chr(34) & ac_Funciones.formato_Texto_Pantalla(obj_recibo.FormaPago(var_pos).FormaPago.Id) & Chr(34) & "")
            obj_sb.Append("," & Chr(34) & "NumeroDocumento" & Chr(34) & ":" & Chr(34) & ac_Funciones.formato_Texto_Pantalla(obj_recibo.FormaPago(var_pos).numero_documento) & Chr(34) & "")
            obj_sb.Append("," & Chr(34) & "Banco" & Chr(34) & ":" & Chr(34) & New cls_bancos(ac_Funciones.formato_Texto_Pantalla(obj_recibo.FormaPago(var_pos).banco)).nombre & Chr(34) & "")


        Catch ex As Exception
            var_error = ex.Message
        Finally
            Response.ContentType = "application/json"
            Response.Clear()
            Response.ClearHeaders()
            Response.ClearContent()
            If var_error.Trim.Length > 0 Then
                Response.Write("{" & Chr(34) & "rslt" & Chr(34) & ":" & Chr(34) & "error" & Chr(34) & "," & Chr(34) & "msj" & Chr(34) & ":" & Chr(34) & var_error & Chr(34) & var_error & "}")
            Else
                Response.Write("{" & Chr(34) & "rslt" & Chr(34) & ":" & Chr(34) & "exito" & Chr(34) & "," & Chr(34) & "msj" & Chr(34) & ":" & Chr(34) & var_error & Chr(34) & obj_sb.ToString & "}")
            End If
            Response.End()
        End Try

    End Sub
    Sub GuardarDetalles()
        Dim var_error As String = ""
        Try
            If Not IsNothing(Session.Contents("obj_recibo")) Then
                obj_recibo = Session.Contents("obj_recibo")
            Else
                obj_recibo = New cls_recibos
            End If

            Dim var_sr = New System.IO.StreamReader(Request.InputStream)
            Dim var_data As JObject = JObject.Parse(var_sr.ReadToEnd)
            Dim var_iddp As Integer = CInt(var_data("iddp").ToString)



            If var_iddp > 0 Then
                obj_recibo.FormaPago(var_iddp - 1).id_recibo = obj_recibo.Id
                obj_recibo.FormaPago(var_iddp - 1).Monto = ac_Funciones.formato_Texto(var_data("MontoPago").ToString)
                obj_recibo.FormaPago(var_iddp - 1).fecha = ac_Funciones.formato_Fecha(var_data("FechaPago").ToString)
                obj_recibo.FormaPago(var_iddp - 1).FormaPago = New cls_formas_pagos(ac_Funciones.formato_Numero(var_data("FormaPago").ToString))
                obj_recibo.FormaPago(var_iddp - 1).numero_documento = ac_Funciones.formato_Texto(var_data("NumeroDocumento").ToString)
                obj_recibo.FormaPago(var_iddp - 1).banco = New cls_bancos(ac_Funciones.formato_Texto(var_data("Banco").ToString)).Id

            Else
                Dim obj_detalle As New cls_recibos_formasPagos
                obj_detalle.id_recibo = obj_recibo.id
                obj_detalle.monto = ac_Funciones.formato_Texto(var_data("MontoPago").ToString)
                obj_detalle.fecha = ac_Funciones.formato_Fecha(var_data("FechaPago").ToString)
                obj_detalle.FormaPago = New cls_formas_pagos(ac_Funciones.formato_Numero(var_data("FormaPago").ToString))
                obj_detalle.numero_documento = ac_Funciones.formato_Texto(var_data("NumeroDocumento").ToString)
                obj_detalle.banco = New cls_bancos(ac_Funciones.formato_Numero(var_data("Banco").ToString)).Id


                obj_recibo.FormaPago.Add(obj_detalle)
            End If

            Session.Contents("obj_recibo") = obj_recibo
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
                Response.Write("{" & Chr(34) & "rslt" & Chr(34) & ":" & Chr(34) & "exito" & Chr(34) & "," & Chr(34) & "msj" & Chr(34) & ":" & Chr(34) & "" & Chr(34) & "}")
            End If
            Response.End()
        End Try
    End Sub
    Sub EliminarDetalles()
        Dim var_error As String = ""
        Try
            Dim var_sr = New System.IO.StreamReader(Request.InputStream)
            Dim var_data As JObject = JObject.Parse(var_sr.ReadToEnd)
            Dim var_ids As String = var_data("iddp")
            If var_ids.Trim.Length > 0 Then
                var_ids = var_ids.Substring(1)
            End If

            If Not IsNothing(Session.Contents("obj_recibo")) Then
                obj_recibo = Session.Contents("obj_recibo")
            Else
                obj_recibo = New cls_recibos
            End If

            Dim var_id() As String = var_ids.Split(",")
            For i As Integer = var_id.Count - 1 To 0 Step -1
                obj_recibo.FormaPago.RemoveAt(var_id(i) - 1)
            Next
            Session.Contents("obj_recibo") = obj_recibo
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
                Response.Write("{" & Chr(34) & "rslt" & Chr(34) & ":" & Chr(34) & "exito" & Chr(34) & "," & Chr(34) & "msj" & Chr(34) & ":" & Chr(34) & "" & Chr(34) & "}")
            End If
            Response.End()
        End Try
    End Sub

    Sub cargar_detalles_ventas()
        'Dim var_error As String = ""
        ''Dim obj_venta As New cls_ventas
        'Dim var_venta As String = 0
        'If Not IsNothing(Request.QueryString("id")) Then
        '    var_venta = Val(Request.QueryString("id").ToString())
        'End If

        'Dim obj_venta As New cls_ventas(var_venta)

        'Dim obj_dt_int As System.Data.DataTable = obj_venta.ListaDetallePedido()
        'Dim var_json As String = JsonConvert.SerializeObject(obj_dt_int)
        'Response.Clear()
        'Response.ClearHeaders()
        'Response.ClearContent()
        'Response.Write("{ " & Chr(34) & "aaData" & Chr(34) & ":" & var_json & "}")
        'Response.End()
    End Sub

    Private Sub buscar_clientes()
        Dim var_rif As String = Request.QueryString("rif").ToString
        Dim var_error As String = ""

        Dim obj_dt_int As System.Data.DataTable = cls_pasajeros.Consulta(var_rif)

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
                Response.Write("{" & Chr(34) & "idcliente" & Chr(34) & ":" & Chr(34) & obj_dt_int.Rows(0).Item(0) & Chr(34) & "}")
            Else
                Response.Write("{" & Chr(34) & "error" & Chr(34) & ":" & Chr(34) & "-1" & Chr(34) & "}")
            End If
        End If

        Response.End()
    End Sub

    Private Sub buscar_venta()
        Dim var_venta As String = Request.QueryString("venta").ToString
        Dim var_error As String = ""

        Dim obj_dt_int As System.Data.DataTable = cls_ventas.ConsultaActivosVentas(var_venta, var_error)

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
                Response.Write("{" & Chr(34) & "DT_RowId" & Chr(34) & ":" & Chr(34) & obj_dt_int.Rows(0).Item(0) & Chr(34) & "}")
            Else
                Response.Write("{" & Chr(34) & "error" & Chr(34) & ":" & Chr(34) & "-1" & Chr(34) & "}")
            End If
        End If

        Response.End()
    End Sub

    Sub moneda()
        Dim var_error As String = ""
        Dim obj_dt_int As System.Data.DataTable = cls_monedas.Lista
        Dim var_json As String = JsonConvert.SerializeObject(obj_dt_int)
        Response.Clear()
        Response.ClearHeaders()
        Response.ClearContent()
        Response.Write("{ " & Chr(34) & "aaData" & Chr(34) & ":" & var_json & "}")
        Response.End()
    End Sub
    Sub formaPago()
        Dim var_error As String = ""
        Dim obj_dt_int As System.Data.DataTable = cls_formas_pagos.Lista
        Dim var_json As String = JsonConvert.SerializeObject(obj_dt_int)
        Response.Clear()
        Response.ClearHeaders()
        Response.ClearContent()
        Response.Write("{ " & Chr(34) & "aaData" & Chr(34) & ":" & var_json & "}")
        Response.End()
    End Sub
    Sub banco()
        Dim var_error As String = ""
        Dim obj_dt_int As System.Data.DataTable = cls_bancos.Lista
        Dim var_json As String = JsonConvert.SerializeObject(obj_dt_int)
        Response.Clear()
        Response.ClearHeaders()
        Response.ClearContent()
        Response.Write("{ " & Chr(34) & "aaData" & Chr(34) & ":" & var_json & "}")
        Response.End()
    End Sub

    'Private Sub ver_abonos()
    '    Dim var_id As String = Request.QueryString("id").ToString
    '    Dim var_error As String = ""

    '    Dim obj_dt_int As System.Data.DataTable = cls_recibos.Abonos(var_id, var_error)

    '    Response.Clear()
    '    Response.ClearHeaders()
    '    Response.ClearContent()

    '    Dim var_json As String = ""
    '    If var_error.Trim.ToString.Length > 0 Then
    '        Response.Write("{" & Chr(34) & "error" & Chr(34) & ":" & Chr(34) & "-2" & Chr(34) & "}")
    '    Else
    '        var_json = JsonConvert.SerializeObject(obj_dt_int)
    '        Response.Write("{ " & Chr(34) & "aaData" & Chr(34) & ":" & var_json & "}")
    '    End If

    '    Response.End()
    'End Sub

End Class