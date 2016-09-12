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

                Case "buscar_pasajero"
                    validarPasajero()

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
                Case "cargar_arrendadoras"
                    arrendadoras()
                Case "cargar_vehiculos"
                    vehiculos()
                Case "cargar_paquetes"
                    paquetes()

                Case "cargar_pasajero"
                    CargarPasajero()
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
            obj_sb.Append("," & Chr(34) & "hdn_id_venta" & Chr(34) & ":" & Chr(34) & obj_venta.id & Chr(34) & "")
            obj_sb.Append("," & Chr(34) & "Numero" & Chr(34) & ":" & Chr(34) & obj_venta.numero & Chr(34) & "")
            obj_sb.Append("," & Chr(34) & "Sucursal" & Chr(34) & ":" & Chr(34) & obj_venta.id_sucursal & Chr(34) & "")
            obj_sb.Append("," & Chr(34) & "TipoViaje" & Chr(34) & ":" & Chr(34) & IIf(obj_venta.nacional, 1, 0) & Chr(34) & "")
            obj_sb.Append("," & Chr(34) & "TipoVenta" & Chr(34) & ":" & Chr(34) & obj_venta.tipo & Chr(34) & "")
            obj_sb.Append("," & Chr(34) & "Vendedor" & Chr(34) & ":" & Chr(34) & obj_venta.IdVendedor & Chr(34) & "")
            obj_sb.Append("," & Chr(34) & "Agencia" & Chr(34) & ":" & Chr(34) & obj_venta.IdAgencia & Chr(34) & "")
            obj_sb.Append("," & Chr(34) & "Freelance" & Chr(34) & ":" & Chr(34) & obj_venta.IdFreelance & Chr(34) & "")

            obj_sb.Append("," & Chr(34) & "Aerolinea" & Chr(34) & ":" & Chr(34) & obj_venta.IdAerolinea & Chr(34) & "")
            obj_sb.Append("," & Chr(34) & "Paquete" & Chr(34) & ":" & Chr(34) & obj_venta.id_paquete & Chr(34) & "")
            obj_sb.Append("," & Chr(34) & "Vehiculo" & Chr(34) & ":" & Chr(34) & obj_venta.id_vehiculo & Chr(34) & "")
            obj_sb.Append("," & Chr(34) & "Arrendadora" & Chr(34) & ":" & Chr(34) & obj_venta.id_arrendadora & Chr(34) & "")
            obj_sb.Append("," & Chr(34) & "Hotel" & Chr(34) & ":" & Chr(34) & obj_venta.id_hotel & Chr(34) & "")
            obj_sb.Append("," & Chr(34) & "Habitacion" & Chr(34) & ":" & Chr(34) & obj_venta.tipo_habitacion & Chr(34) & "")
            obj_sb.Append("," & Chr(34) & "Boleto" & Chr(34) & ":" & Chr(34) & obj_venta.numero_boleto & Chr(34) & "")
            obj_sb.Append("," & Chr(34) & "Sistema" & Chr(34) & ":" & Chr(34) & obj_venta.IdSistema & Chr(34) & "")
            obj_sb.Append("," & Chr(34) & "Pantalla" & Chr(34) & ":" & Chr(34) & obj_venta.pantalla & Chr(34) & "")
            obj_sb.Append("," & Chr(34) & "Dias" & Chr(34) & ":" & Chr(34) & obj_venta.dias & Chr(34) & "")
            obj_sb.Append("," & Chr(34) & "TipoPersona" & Chr(34) & ":" & Chr(34) & obj_venta.tipo_persona & Chr(34) & "")
            obj_sb.Append("," & Chr(34) & "IdPasajero" & Chr(34) & ":" & Chr(34) & obj_venta.id_cliente & Chr(34) & "")
            obj_sb.Append("," & Chr(34) & "FechaInicio" & Chr(34) & ":" & Chr(34) & obj_venta.fechaInicio & Chr(34) & "")
            obj_sb.Append("," & Chr(34) & "FechaFin" & Chr(34) & ":" & Chr(34) & obj_venta.fechaFin & Chr(34) & "")
            obj_sb.Append("," & Chr(34) & "Destino" & Chr(34) & ":" & Chr(34) & obj_venta.destino & Chr(34) & "")
            obj_sb.Append("," & Chr(34) & "Moneda" & Chr(34) & ":" & Chr(34) & obj_venta.id_moneda & Chr(34) & "")
            obj_sb.Append("," & Chr(34) & "Tarifa" & Chr(34) & ":" & Chr(34) & obj_venta.tarifa & Chr(34) & "")
            obj_sb.Append("," & Chr(34) & "Tax" & Chr(34) & ":" & Chr(34) & obj_venta.tax & Chr(34) & "")
            obj_sb.Append("," & Chr(34) & "Total" & Chr(34) & ":" & Chr(34) & obj_venta.total & Chr(34) & "")
            obj_sb.Append("," & Chr(34) & "TipoVenta" & Chr(34) & ":" & Chr(34) & obj_venta.tipo_venta & Chr(34) & "")

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

        Dim obj_pasajero As New cls_pasajeros(ac_Funciones.formato_Numero(var_JObject("IdPasajero").ToString))
        If obj_pasajero.Id <= 0 Then
            obj_pasajero.rif = ac_Funciones.formato_Texto(var_JObject("Rif").ToString)
            obj_pasajero.pasaporte = ac_Funciones.formato_Texto(var_JObject("Pasaporte").ToString)
            obj_pasajero.pasaporte_fecha = ac_Funciones.formato_Fecha(var_JObject("PasaporteFecha").ToString)
            obj_pasajero.nombre = ac_Funciones.formato_Texto(var_JObject("Nombre").ToString)
            obj_pasajero.apellido = ac_Funciones.formato_Texto(var_JObject("Apellido").ToString)
            obj_pasajero.telefono = ac_Funciones.formato_Texto(var_JObject("TelefonoM").ToString)
            obj_pasajero.email = ac_Funciones.formato_Texto(var_JObject("Email").ToString)
            obj_pasajero.edad = ac_Funciones.formato_Numero(var_JObject("Edad").ToString)
            obj_pasajero.direccion = ac_Funciones.formato_Texto(var_JObject("Direccion").ToString)
            obj_pasajero.fecha_reg = Now
            obj_pasajero.id_usuario_reg = obj_Session.Usuario.Id

            Dim var_msj As String = ""
            If Not obj_pasajero.Actualizar(var_msj) Then
                Response.ContentType = "application/json"
                Response.Clear()
                Response.ClearHeaders()
                Response.ClearContent()
                Response.Write("{" & Chr(34) & "rslt" & Chr(34) & ":" & Chr(34) & "error" & Chr(34) & "," & Chr(34) & "msj" & Chr(34) & ":" & Chr(34) & var_msj & Chr(34) & "}")
                Response.End()
                Exit Sub
            End If

        End If

        If Not IsNothing(Session.Contents("obj_venta")) Then
            obj_venta = Session.Contents("obj_venta")
        Else
            obj_venta = New cls_ventas
        End If

        If obj_venta.id = 0 Then
            obj_venta.id_usuario_reg = DirectCast(Session.Contents("obj_Session"), cls_Sesion).Usuario.Id
            obj_venta.fecha_reg = Now.Date
            obj_venta.numero = Right("V" & Right("000000000000" & (cls_ventas.SiguienteNumero() + 1).ToString, 12), 13)
            obj_venta.fecha = Now.Date
            obj_venta.id_cliente = obj_pasajero.Id
        Else
            obj_venta.id_cliente = ac_Funciones.formato_Numero(var_JObject("IdPasajero").ToString)
            obj_venta.numero = ac_Funciones.formato_Texto(var_JObject("Codigo").ToString)
        End If

        'obj_venta.id = ac_Funciones.formato_Numero(var_JObject("hdn_id_venta").ToString).ToUpper
        obj_venta.id_sucursal = ac_Funciones.formato_Texto(var_JObject("Sucursal").ToString)
        obj_venta.nacional = ac_Funciones.formato_boolean(IIf(var_JObject("TipoViaje").ToString = 1, 1, 0).ToString)
        obj_venta.tipo = ac_Funciones.formato_Numero(var_JObject("TipoVenta").ToString)
        obj_venta.IdVendedor = ac_Funciones.formato_Numero(var_JObject("Vendedor").ToString)
        obj_venta.IdAgencia = ac_Funciones.formato_Numero(var_JObject("Agencia").ToString)
        obj_venta.IdFreelance = ac_Funciones.formato_Numero(var_JObject("Freelance").ToString)
        obj_venta.id_hotel = ac_Funciones.formato_Numero(var_JObject("Hotel").ToString)
        obj_venta.tipo_habitacion = ac_Funciones.formato_Numero(var_JObject("Habitacion").ToString)
        obj_venta.IdAerolinea = ac_Funciones.formato_Numero(var_JObject("Aerolinea").ToString)
        obj_venta.numero_boleto = ac_Funciones.formato_Texto(var_JObject("Boleto").ToString)
        obj_venta.IdSistema = ac_Funciones.formato_Numero(var_JObject("Sistema").ToString)
        obj_venta.pantalla = ac_Funciones.formato_Texto(var_JObject("Pantalla").ToString)
        obj_venta.dias = ac_Funciones.formato_Numero(var_JObject("Dias").ToString)
        obj_venta.tipo_persona = ac_Funciones.formato_Numero(var_JObject("TipoPersona").ToString)
        obj_venta.id_vehiculo = ac_Funciones.formato_Numero(var_JObject("Vehiculos").ToString)
        obj_venta.id_arrendadora = ac_Funciones.formato_Numero(var_JObject("Arrendadora").ToString)

        obj_venta.identificacion_cliente = ac_Funciones.formato_Texto(var_JObject("Rif").ToString)
        obj_venta.nombre_cliente = ac_Funciones.formato_Texto(var_JObject("Nombre").ToString)
        'obj_venta.ciudad = ac_Funciones.formato_Numero(var_JObject("IdCliente").ToString)

        obj_venta.fechaInicio = ac_Funciones.formato_Fecha(var_JObject("FechaInicio").ToString)
        obj_venta.fechaFin = ac_Funciones.formato_Fecha(var_JObject("FechaFin").ToString)
        obj_venta.destino = ac_Funciones.formato_Numero(var_JObject("Destino").ToString)
        obj_venta.id_moneda = ac_Funciones.formato_Numero(var_JObject("Moneda").ToString)
        obj_venta.tarifa = ac_Funciones.formato_Numero(var_JObject("Tarifa").ToString, True)
        obj_venta.tax = ac_Funciones.formato_Numero(var_JObject("Tax").ToString, True)
        obj_venta.total = ac_Funciones.formato_Numero(var_JObject("Total").ToString, True)
        obj_venta.tipo_venta = ac_Funciones.formato_Numero(var_JObject("TipoVenta").ToString)
        'obj_venta.ciudad = ac_Funciones.formato_Numero(var_JObject("Tarifa").ToString)

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

    Private Sub CargarPasajero()
        Dim var_error As String = ""
        Dim obj_sb As New StringBuilder

        Dim var_sr = New System.IO.StreamReader(Request.InputStream)
        Dim var_data As JObject = JObject.Parse(var_sr.ReadToEnd)
        Dim var_id_pasajero As String = ac_Funciones.formato_Texto(var_data("id_pasajero").ToString)


        Dim obj_dt_int As System.Data.DataTable = cls_pasajeros.ConsultaDatos("", var_id_pasajero)
        If obj_dt_int.Rows.Count > 0 Then
            obj_sb.Append("," & Chr(34) & "Rif" & Chr(34) & ":" & Chr(34) & obj_dt_int.Rows(0).Item("Rif").ToString() & Chr(34) & "")
            obj_sb.Append("," & Chr(34) & "Pasaporte" & Chr(34) & ":" & Chr(34) & obj_dt_int.Rows(0).Item("Pasaporte").ToString() & Chr(34) & "")
            obj_sb.Append("," & Chr(34) & "FechaVencimiento" & Chr(34) & ":" & Chr(34) & ac_Funciones.formato_Fecha(obj_dt_int.Rows(0).Item("FechaVencimiento").ToString()) & Chr(34) & "")
            obj_sb.Append("," & Chr(34) & "Nombre" & Chr(34) & ":" & Chr(34) & obj_dt_int.Rows(0).Item("Nombre").ToString() & Chr(34) & "")
            obj_sb.Append("," & Chr(34) & "Apellido" & Chr(34) & ":" & Chr(34) & obj_dt_int.Rows(0).Item("Apellido").ToString() & Chr(34) & "")
            obj_sb.Append("," & Chr(34) & "Telefono" & Chr(34) & ":" & Chr(34) & obj_dt_int.Rows(0).Item("Telefono").ToString() & Chr(34) & "")
            obj_sb.Append("," & Chr(34) & "Direccion" & Chr(34) & ":" & Chr(34) & obj_dt_int.Rows(0).Item("Direccion").ToString() & Chr(34) & "")
            obj_sb.Append("," & Chr(34) & "Email" & Chr(34) & ":" & Chr(34) & obj_dt_int.Rows(0).Item("Email").ToString() & Chr(34) & "")
            obj_sb.Append("," & Chr(34) & "Edad" & Chr(34) & ":" & Chr(34) & obj_dt_int.Rows(0).Item("Edad").ToString() & Chr(34) & "")
            'obj_sb.Append("," & Chr(34) & "Nombre" & Chr(34) & ":" & Chr(34) & obj_dt_int.Rows(0).Item("Nombre").ToString() & Chr(34) & "")
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

    Private Sub validarPasajero()
        Dim var_error As String = ""
        Dim obj_sb As New StringBuilder

        Dim var_sr = New System.IO.StreamReader(Request.InputStream)
        Dim var_data As JObject = JObject.Parse(var_sr.ReadToEnd)
        Dim var_rif As String = ac_Funciones.formato_Texto(var_data("rif").ToString)


        Dim obj_dt_int As System.Data.DataTable = cls_pasajeros.Consulta(var_error, var_rif)
        If obj_dt_int.Rows.Count > 0 Then
            obj_sb.Append("," & Chr(34) & "id_pasajero" & Chr(34) & ":" & Chr(34) & obj_dt_int.Rows(0).Item("DT_RowId").ToString() & Chr(34) & "")
        Else
            obj_sb.Append("," & Chr(34) & "id_pasajero" & Chr(34) & ":" & Chr(34) & 0 & Chr(34) & "")
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

    Sub arrendadoras()
        Dim var_error As String = ""
        Dim obj_dt_int As System.Data.DataTable = cls_vehiculos_agencias.Lista
        Dim var_json As String = JsonConvert.SerializeObject(obj_dt_int)
        Response.Clear()
        Response.ClearHeaders()
        Response.ClearContent()
        Response.Write("{ " & Chr(34) & "aaData" & Chr(34) & ":" & var_json & "}")
        Response.End()
    End Sub

    Sub vehiculos()
        Dim var_error As String = ""
        Dim obj_dt_int As System.Data.DataTable = cls_vehiculos.Lista()
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