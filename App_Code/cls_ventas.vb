Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports ac_Funciones

Public Class cls_ventas

#Region "VARIABLES"
    Dim var_Nombre_Tabla As String = "tbl_ventas"
    Dim var_Campo_Id As String = "id"
    Dim var_Campos As String = "numero,fecha,tipo,id_agencia,id_freelance,id_vendedor,total,tax,porcentaje_ct,comision_ct,porcentaje_ctg,comision_ctg,fee,impuesto_fee,total_reporte,monto_pagar,id_cliente,nombre_cliente,identificacion_cliente,numero_boleto,descripcion_boleto,id_aerolinea,id_sistema,pantalla,id_usuario_reg,fecha_reg,id_moneda,id_producto,cantidad,descripcion,comentario,precio,impuesto,id_hotel,fecha_inicio,fecha_fin"
    Dim obj_Conex_int As New SqlConnection(ConfigurationManager.ConnectionStrings("connection").ConnectionString)

    Dim var_id As Integer = 0
    Dim var_numero As String = ""
    Dim var_fecha As Date = Now.Date
    Dim var_tipo As Integer = 0
    Dim var_id_agencia As Integer = 0
    Dim var_id_freelance As Integer = 0
    Dim var_id_vendedor As Integer = 0
    Dim var_total As Double = 0
    Dim var_tax As Double = 0
    Dim var_porcentaje_ct As Double = 0
    Dim var_comision_ct As Double = 0
    Dim var_porcentaje_ctg As Double = 0
    Dim var_comision_ctg As Double = 0
    Dim var_fee As Double = 0
    Dim var_impuesto_fee As Double = 0
    Dim var_total_reporte As Double = 0
    Dim var_monto_pagar As Double = 0

    Dim var_id_cliente As Integer = 0
    Dim var_nombre_cliente As String = ""
    Dim var_identificacion_cliente As String = ""
    Dim var_numero_boleto As String = ""
    Dim var_descripcion_boleto As String = ""
    Dim var_id_aerolinea As Integer = 0
    Dim var_id_sistema As Integer = 0
    Dim var_pantalla As String = ""
    Dim var_id_usuario_reg As Integer = 0
    Dim var_fecha_reg As Date = Now.Date

    Dim var_id_producto As Integer = 0
    Dim var_cantidad As Double = 0
    Dim var_descripcion As String = ""
    Dim var_comentario As String = ""
    Dim var_precio As Double = 0
    Dim var_impuesto As Double = 0
    Dim var_id_moneda As Integer = 0
    Dim var_id_hotel As Integer = 0
    Dim var_fecha_inicio As Date = Now.Date
    Dim var_fecha_fin As Date = Now.Date

#End Region

#Region "PROPIEDADES"
    Public Shared ReadOnly Property Nombre_Tabla() As String
        Get
            Return "tbl_ventas"
        End Get
    End Property

    Public Shared ReadOnly Property Campo_Id() As String
        Get
            Return "id"
        End Get
    End Property

    Public Shared ReadOnly Property ListadoActivosRecibos() As String
        Get
            Return "lst_ventasActivosRecibos"
        End Get
    End Property

    Public Property id() As Integer
        Get
            Return Me.var_id
        End Get
        Set(ByVal value As Integer)
            Me.var_id = value
        End Set
    End Property

    Public Property numero() As String
        Get
            Return Me.var_numero
        End Get
        Set(ByVal value As String)
            Me.var_numero = value
        End Set
    End Property

    Public Property fecha() As Date
        Get
            Return Me.var_fecha
        End Get
        Set(ByVal value As Date)
            Me.var_fecha = value
        End Set
    End Property

    Public Property tipo() As Integer
        Get
            Return Me.var_tipo
        End Get
        Set(ByVal value As Integer)
            Me.var_tipo = value
        End Set
    End Property

    Public Property IdAgencia() As Integer
        Get
            Return Me.var_id_agencia
        End Get
        Set(ByVal value As Integer)
            Me.var_id_agencia = value
        End Set
    End Property

    Public Property IdFreelance() As Integer
        Get
            Return Me.var_id_freelance
        End Get
        Set(ByVal value As Integer)
            Me.var_id_freelance = value
        End Set
    End Property

    Public Property IdVendedor() As Integer
        Get
            Return Me.var_id_vendedor
        End Get
        Set(ByVal value As Integer)
            Me.var_id_vendedor = value
        End Set
    End Property

    Public Property total() As Double
        Get
            Return Me.var_total
        End Get
        Set(ByVal value As Double)
            Me.var_total = value
        End Set
    End Property

    Public Property tax() As Double
        Get
            Return Me.var_tax
        End Get
        Set(ByVal value As Double)
            Me.var_tax = value
        End Set
    End Property

    Public Property porcentaje_ct() As Double
        Get
            Return Me.var_porcentaje_ct
        End Get
        Set(ByVal value As Double)
            Me.var_porcentaje_ct = value
        End Set
    End Property

    Public Property comision_ct() As Double
        Get
            Return Me.var_comision_ct
        End Get
        Set(ByVal value As Double)
            Me.var_comision_ct = value
        End Set
    End Property

    Public Property porcentaje_ctg() As Double
        Get
            Return Me.var_porcentaje_ctg
        End Get
        Set(ByVal value As Double)
            Me.var_porcentaje_ctg = value
        End Set
    End Property

    Public Property comision_ctg() As Double
        Get
            Return Me.var_comision_ctg
        End Get
        Set(ByVal value As Double)
            Me.var_comision_ctg = value
        End Set
    End Property

    Public Property fee() As Double
        Get
            Return Me.var_fee
        End Get
        Set(ByVal value As Double)
            Me.var_fee = value
        End Set
    End Property

    Public Property impuesto_fee() As Double
        Get
            Return Me.var_impuesto_fee
        End Get
        Set(ByVal value As Double)
            Me.var_impuesto_fee = value
        End Set
    End Property

    Public Property total_reporte() As Double
        Get
            Return Me.var_total_reporte
        End Get
        Set(ByVal value As Double)
            Me.var_total_reporte = value
        End Set
    End Property

    Public Property monto_pagar() As Double
        Get
            Return Me.var_monto_pagar
        End Get
        Set(ByVal value As Double)
            Me.var_monto_pagar = value
        End Set
    End Property

    Public Property id_cliente() As Integer
        Get
            Return Me.var_id_cliente
        End Get
        Set(ByVal value As Integer)
            Me.var_id_cliente = value
        End Set
    End Property

    Public Property nombre_cliente() As String
        Get
            Return Me.var_nombre_cliente
        End Get
        Set(ByVal value As String)
            Me.var_nombre_cliente = value
        End Set
    End Property

    Public Property numero_boleto() As String
        Get
            Return Me.var_numero_boleto
        End Get
        Set(ByVal value As String)
            Me.var_numero_boleto = value
        End Set
    End Property

    Public Property identificacion_cliente() As String
        Get
            Return Me.var_identificacion_cliente
        End Get
        Set(ByVal value As String)
            Me.var_identificacion_cliente = value
        End Set
    End Property

    Public Property descripcion_boleto() As String
        Get
            Return Me.var_descripcion_boleto
        End Get
        Set(ByVal value As String)
            Me.var_descripcion_boleto = value
        End Set
    End Property

    Public Property IdAerolinea() As Integer
        Get
            Return Me.var_id_aerolinea
        End Get
        Set(ByVal value As Integer)
            Me.var_id_aerolinea = value
        End Set
    End Property

    Public Property IdSistema() As Integer
        Get
            Return Me.var_id_sistema
        End Get
        Set(ByVal value As Integer)
            Me.var_id_sistema = value
        End Set
    End Property

    Public Property pantalla() As String
        Get
            Return Me.var_pantalla
        End Get
        Set(ByVal value As String)
            Me.var_pantalla = value
        End Set
    End Property

    Public Property id_moneda() As Integer
        Get
            Return Me.var_id_moneda
        End Get
        Set(ByVal value As Integer)
            Me.var_id_moneda = value
        End Set
    End Property

    Public Property id_usuario_reg() As Integer
        Get
            Return Me.var_id_usuario_reg
        End Get
        Set(ByVal value As Integer)
            Me.var_id_usuario_reg = value
        End Set
    End Property

    Public Property fecha_reg() As Date
        Get
            Return Me.var_fecha_reg
        End Get
        Set(ByVal value As Date)
            Me.var_fecha_reg = value
        End Set
    End Property

    Public Property id_hotel() As Integer
        Get
            Return Me.var_id_hotel
        End Get
        Set(ByVal value As Integer)
            Me.var_id_hotel = value
        End Set
    End Property

    Public Property id_producto() As Integer
        Get
            Return Me.var_id_producto
        End Get
        Set(ByVal value As Integer)
            Me.var_id_producto = value
        End Set
    End Property

    Public Property cantidad() As Double
        Get
            Return Me.var_cantidad
        End Get
        Set(ByVal value As Double)
            Me.var_cantidad = value
        End Set
    End Property

    Public Property descripcion() As String
        Get
            Return Me.var_descripcion
        End Get
        Set(ByVal value As String)
            Me.var_descripcion = value
        End Set
    End Property

    Public Property comentario() As String
        Get
            Return Me.var_comentario
        End Get
        Set(ByVal value As String)
            Me.var_comentario = value
        End Set
    End Property

    Public Property precio() As Double
        Get
            Return Me.var_precio
        End Get
        Set(ByVal value As Double)
            Me.var_precio = value
        End Set
    End Property

    Public Property impuesto() As Double
        Get
            Return Me.var_impuesto
        End Get
        Set(ByVal value As Double)
            Me.var_impuesto = value
        End Set
    End Property

    Public Property fechaInicio() As Date
        Get
            Return Me.var_fecha_inicio
        End Get
        Set(ByVal value As Date)
            Me.var_fecha_inicio = value
        End Set
    End Property

    Public Property fechaFin() As Date
        Get
            Return Me.var_fecha_fin
        End Get
        Set(ByVal value As Date)
            Me.var_fecha_fin = value
        End Set
    End Property
#End Region

#Region "FUNCIONES"

    Sub New(Optional ByVal var_Id_int As Integer = 0)

        If var_Id_int > 0 Then
            Cargar(var_Id_int)
        End If

    End Sub

    Public Sub Cargar(ByVal var_id_int As Integer)
        Dim obj_dt_int As New DataTable
        obj_dt_int = Abrir_Tabla(Me.obj_Conex_int, "Select * from tbl_ventas WHERE id=" & var_id_int)
        If obj_dt_int.Rows.Count > 0 Then
            Me.var_id = obj_dt_int.Rows(0).Item("id").ToString
            Me.var_numero = formato_Texto(obj_dt_int.Rows(0).Item("numero").ToString)
            Me.var_fecha = formato_Fecha(obj_dt_int.Rows(0).Item("fecha").ToString)
            Me.var_numero = formato_Numero(obj_dt_int.Rows(0).Item("tipo").ToString)
            Me.var_id_agencia = formato_Numero(obj_dt_int.Rows(0).Item("id_agencia").ToString)
            Me.var_id_freelance = formato_Numero(obj_dt_int.Rows(0).Item("id_freelance").ToString)
            Me.var_id_vendedor = formato_Numero(obj_dt_int.Rows(0).Item("id_vendedor").ToString)
            Me.var_total = formato_Numero(obj_dt_int.Rows(0).Item("total").ToString, True)
            Me.var_tax = formato_Numero(obj_dt_int.Rows(0).Item("tax").ToString, True)
            Me.var_porcentaje_ct = formato_Numero(obj_dt_int.Rows(0).Item("porcentaje_ct").ToString, True)
            Me.var_comision_ct = formato_Numero(obj_dt_int.Rows(0).Item("comision_ct").ToString, True)
            Me.var_porcentaje_ctg = formato_Numero(obj_dt_int.Rows(0).Item("porcentaje_ctg").ToString, True)
            Me.var_comision_ctg = formato_Numero(obj_dt_int.Rows(0).Item("comision_ctg").ToString, True)
            Me.var_fee = formato_Numero(obj_dt_int.Rows(0).Item("fee").ToString, True)
            Me.var_impuesto_fee = formato_Numero(obj_dt_int.Rows(0).Item("impuesto_fee").ToString, True)
            Me.var_total_reporte = formato_Numero(obj_dt_int.Rows(0).Item("total_reporte").ToString, True)
            Me.var_monto_pagar = formato_Numero(obj_dt_int.Rows(0).Item("monto_pagar").ToString, True)
            Me.var_id_cliente = formato_Numero(obj_dt_int.Rows(0).Item("id_cliente").ToString)
            Me.var_nombre_cliente = formato_Texto(obj_dt_int.Rows(0).Item("nombre").ToString)
            Me.var_identificacion_cliente = formato_Texto(obj_dt_int.Rows(0).Item("identificacion_cliente").ToString)
            Me.var_numero_boleto = formato_Texto(obj_dt_int.Rows(0).Item("numero_boleto").ToString)
            Me.var_descripcion_boleto = formato_Texto(obj_dt_int.Rows(0).Item("descripcion_boleto").ToString)
            Me.var_id_aerolinea = formato_Numero(obj_dt_int.Rows(0).Item("id_aerolinea").ToString)
            Me.var_id_sistema = formato_Numero(obj_dt_int.Rows(0).Item("id_sistema").ToString)
            Me.var_pantalla = formato_Texto(obj_dt_int.Rows(0).Item("pantalla").ToString)
            Me.var_id_moneda = formato_Numero(obj_dt_int.Rows(0).Item("id_moneda").ToString)
            Me.var_id_usuario_reg = formato_Numero(obj_dt_int.Rows(0).Item("id_usuario_reg").ToString)
            Me.var_fecha_reg = formato_Fecha(obj_dt_int.Rows(0).Item("fecha_reg").ToString)

            Me.var_id_hotel = formato_Numero(obj_dt_int.Rows(0).Item("id_hotel").ToString)
            Me.var_id_producto = formato_Numero(obj_dt_int.Rows(0).Item("id_producto").ToString)
            Me.var_cantidad = formato_Numero(obj_dt_int.Rows(0).Item("cantidad").ToString, True)
            Me.var_descripcion = formato_Texto(obj_dt_int.Rows(0).Item("descripcion").ToString)
            Me.var_comentario = formato_Texto(obj_dt_int.Rows(0).Item("comentario").ToString)
            Me.var_precio = formato_Numero(obj_dt_int.Rows(0).Item("precio").ToString, True)
            Me.var_impuesto = formato_Numero(obj_dt_int.Rows(0).Item("impuesto").ToString, True)
            Me.var_id_hotel = formato_Texto(obj_dt_int.Rows(0).Item("id_hotel").ToString)
            Me.var_fecha_inicio = formato_Fecha(obj_dt_int.Rows(0).Item("fecha_inicio").ToString)
            Me.var_fecha_fin = formato_Fecha(obj_dt_int.Rows(0).Item("fecha_fin").ToString)
            'Dim obj_dt_detalles As DataTable = ac_Funciones.Abrir_Tabla(Me.obj_Conex_int, "select id from " & cls_ventas_detalles.Nombre_Tabla & " where id_hotel=" & Me.var_id)
            'For i As Integer = 0 To obj_dt_detalles.Rows.Count - 1
            '    Me.obj_detalle.Add(New cls_ventas_detalles(obj_dt_detalles.Rows(i).Item(0)))
            'Next

        Else
            Me.var_id = -1
        End If
    End Sub

    Public Function Actualizar(ByRef var_msj As String, ByVal var_id_usuario As Integer) As Boolean
        Dim var_error As String = ""
        If Me.var_id = 0 Then   'NUEVO
            If Not Ingresar(Me.obj_Conex_int, Me.var_Nombre_Tabla, Me.var_Campos, Sql_Texto(Me.var_numero) & "," & Sql_Texto(Me.var_fecha, True) & "," & Sql_Texto(Me.var_tipo) & "," & Sql_Texto(Me.var_id_agencia) & "," & Sql_Texto(Me.var_id_freelance) & "," & Sql_Texto(Me.var_id_vendedor) & "," & Sql_Texto(Me.var_total) & "," & Sql_Texto(Me.var_tax) & "," & Sql_Texto(Me.var_porcentaje_ct) & "," & Sql_Texto(Me.var_comision_ct) & "," & Sql_Texto(Me.var_porcentaje_ctg) & "," & Sql_Texto(Me.var_comision_ctg) & "," & Sql_Texto(Me.var_fee) & "," & Sql_Texto(Me.var_impuesto_fee) & "," & Sql_Texto(Me.var_total_reporte) & "," & Sql_Texto(Me.var_monto_pagar) & "," & Sql_Texto(Me.var_id_cliente) & "," & Sql_Texto(Me.var_nombre_cliente) & "," & Sql_Texto(Me.var_identificacion_cliente) & "," & Sql_Texto(Me.var_numero_boleto) & "," & Sql_Texto(Me.var_descripcion_boleto) & "," & Sql_Texto(Me.var_id_aerolinea) & "," & Sql_Texto(Me.var_id_sistema) & "," & Sql_Texto(Me.var_pantalla) & "," & Sql_Texto(Me.var_id_usuario_reg) & "," & Sql_Texto(Me.var_fecha_reg, True) & "," & Sql_Texto(Me.var_id_moneda) & "," & Sql_Texto(Me.id_hotel) & "," & Sql_Texto(Me.var_id_producto) & "," & Sql_Texto(Me.var_id_moneda) & "," & Sql_Texto(Me.var_cantidad) & "," & Sql_Texto(Me.var_descripcion) & "," & Sql_Texto(Me.var_comentario) & "," & Sql_Texto(Me.var_precio) & "," & Sql_Texto(Me.var_impuesto) & "," & Sql_Texto(Me.var_total) & "," & Sql_Texto(Me.var_id_hotel) & "," & Sql_Texto(Me.var_fecha_inicio) & "," & Sql_Texto(Me.var_fecha_fin), var_msj) Then
                var_msj = var_error

                Dim obj_log As New cls_logs
                obj_log.ComentarioLog = "Error agregando venta - " & var_id_usuario & " - " & Me.var_fecha & ": " & var_error
                obj_log.FechaLog = Now
                obj_log.id_Menu = New cls_modulos("ventas").Id
                obj_log.id_Usuario = var_id_usuario
                obj_log.idAccion = New cls_acciones("agregar").Id
                obj_log.ResultadoLog = False
                obj_log.InsertarLog()

                Return False
                Exit Function
            Else
                Me.var_id = Valor_De(Me.obj_Conex_int, "select " & Me.var_Campo_Id & " from " & Me.var_Nombre_Tabla & " order by id desc")

                Dim obj_sb As New StringBuilder
                obj_sb.Append("," & Chr(34) & "Numero" & Chr(34) & ":" & Chr(34) & Me.numero & Chr(34) & "")
                obj_sb.Append("," & Chr(34) & "Fecha" & Chr(34) & ":" & Chr(34) & Me.fecha & Chr(34) & "")
                obj_sb.Append("," & Chr(34) & "tipo" & Chr(34) & ":" & Chr(34) & Me.tipo & Chr(34) & "")
                obj_sb.Append("," & Chr(34) & "id_agencia" & Chr(34) & ":" & Chr(34) & Me.IdAgencia & Chr(34) & "")
                obj_sb.Append("," & Chr(34) & "id_freelance" & Chr(34) & ":" & Chr(34) & Me.IdFreelance & Chr(34) & "")
                obj_sb.Append("," & Chr(34) & "id_vendedor" & Chr(34) & ":" & Chr(34) & Me.IdVendedor & Chr(34) & "")
                obj_sb.Append("," & Chr(34) & "total" & Chr(34) & ":" & Chr(34) & Me.total & Chr(34) & "")
                obj_sb.Append("," & Chr(34) & "tax" & Chr(34) & ":" & Chr(34) & Me.tax & Chr(34) & "")
                obj_sb.Append("," & Chr(34) & "porcentaje_ct" & Chr(34) & ":" & Chr(34) & Me.porcentaje_ct & Chr(34) & "")
                obj_sb.Append("," & Chr(34) & "comision_ct" & Chr(34) & ":" & Chr(34) & Me.comision_ct & Chr(34) & "")
                obj_sb.Append("," & Chr(34) & "porcentaje_ctg" & Chr(34) & ":" & Chr(34) & Me.porcentaje_ctg & Chr(34) & "")
                obj_sb.Append("," & Chr(34) & "comision_ctg" & Chr(34) & ":" & Chr(34) & Me.comision_ctg & Chr(34) & "")
                obj_sb.Append("," & Chr(34) & "fee" & Chr(34) & ":" & Chr(34) & Me.fee & Chr(34) & "")
                obj_sb.Append("," & Chr(34) & "impuesto_fee" & Chr(34) & ":" & Chr(34) & Me.impuesto_fee & Chr(34) & "")
                obj_sb.Append("," & Chr(34) & "total_reporte" & Chr(34) & ":" & Chr(34) & Me.total_reporte & Chr(34) & "")
                obj_sb.Append("," & Chr(34) & "monto_pagar" & Chr(34) & ":" & Chr(34) & Me.monto_pagar & Chr(34) & "")
                obj_sb.Append("," & Chr(34) & "id_cliente" & Chr(34) & ":" & Chr(34) & Me.id_cliente & Chr(34) & "")
                obj_sb.Append("," & Chr(34) & "nombre_cliente" & Chr(34) & ":" & Chr(34) & Me.nombre_cliente & Chr(34) & "")
                obj_sb.Append("," & Chr(34) & "identificacion_cliente" & Chr(34) & ":" & Chr(34) & Me.identificacion_cliente & Chr(34) & "")
                obj_sb.Append("," & Chr(34) & "numero_boleto" & Chr(34) & ":" & Chr(34) & Me.numero_boleto & Chr(34) & "")
                obj_sb.Append("," & Chr(34) & "descripcion_boleto" & Chr(34) & ":" & Chr(34) & Me.descripcion_boleto & Chr(34) & "")
                obj_sb.Append("," & Chr(34) & "id_aerolinea" & Chr(34) & ":" & Chr(34) & Me.IdAerolinea & Chr(34) & "")
                obj_sb.Append("," & Chr(34) & "id_sistema" & Chr(34) & ":" & Chr(34) & Me.IdSistema & Chr(34) & "")
                obj_sb.Append("," & Chr(34) & "pantalla" & Chr(34) & ":" & Chr(34) & Me.pantalla & Chr(34) & "")
                obj_sb.Append("," & Chr(34) & "id_usuario_reg" & Chr(34) & ":" & Chr(34) & Me.id_usuario_reg & Chr(34) & "")
                obj_sb.Append("," & Chr(34) & "fecha_reg" & Chr(34) & ":" & Chr(34) & Me.fecha_reg & Chr(34) & "")
                obj_sb.Append("," & Chr(34) & "id_moneda" & Chr(34) & ":" & Chr(34) & Me.id_moneda & Chr(34) & "")
                obj_sb.Append("," & Chr(34) & "id_producto" & Chr(34) & ":" & Chr(34) & Me.id_producto & Chr(34) & "")
                obj_sb.Append("," & Chr(34) & "cantidad" & Chr(34) & ":" & Chr(34) & Me.cantidad & Chr(34) & "")
                obj_sb.Append("," & Chr(34) & "descripcion" & Chr(34) & ":" & Chr(34) & Me.descripcion & Chr(34) & "")
                obj_sb.Append("," & Chr(34) & "comentario" & Chr(34) & ":" & Chr(34) & Me.comentario & Chr(34) & "")
                obj_sb.Append("," & Chr(34) & "precio" & Chr(34) & ":" & Chr(34) & Me.precio & Chr(34) & "")
                obj_sb.Append("," & Chr(34) & "impuesto" & Chr(34) & ":" & Chr(34) & Me.impuesto & Chr(34) & "")
                obj_sb.Append("," & Chr(34) & "id_hotel" & Chr(34) & ":" & Chr(34) & Me.id_hotel & Chr(34) & "")
                obj_sb.Append("," & Chr(34) & "fecha_inicio" & Chr(34) & ":" & Chr(34) & Me.fechaInicio & Chr(34) & "")
                obj_sb.Append("," & Chr(34) & "fecha_fin" & Chr(34) & ":" & Chr(34) & Me.fechaFin & Chr(34) & "")

                Dim obj_log As New cls_logs
                obj_log.ComentarioLog = "venta agregado: {" & obj_sb.ToString & "}"
                obj_log.FechaLog = Now
                obj_log.id_Menu = New cls_modulos("ventas").Id
                obj_log.id_Usuario = var_id_usuario
                obj_log.idAccion = New cls_acciones("agregar").Id
                obj_log.ResultadoLog = True
                obj_log.InsertarLog()

                Return True

            End If
        ElseIf Me.var_id > 0 Then 'EDICION
            If Not ac_Funciones.Actualizar(Me.obj_Conex_int, Me.var_Nombre_Tabla, "numero=" & Sql_Texto(Me.var_numero) & ",fecha=" & Sql_Texto(Me.var_fecha, True) & ",tipo=" & Sql_Texto(Me.var_tipo) & ",id_agencia=" & Sql_Texto(Me.var_id_agencia) & ",id_freelance=" & Sql_Texto(Me.var_id_freelance) & ",id_vendedor=" & Sql_Texto(Me.var_id_vendedor) & ",total=" & Sql_Texto(Me.var_total) & ",tax=" & Sql_Texto(Me.var_tax) & ",porcentaje_ct=" & Sql_Texto(Me.var_porcentaje_ct) & ",comision_ct=" & Sql_Texto(Me.var_comision_ct) & ",porcentaje_ctg=" & Sql_Texto(Me.var_porcentaje_ctg) & ",comision_ctg=" & Sql_Texto(Me.var_comision_ctg) & ",fee=" & Sql_Texto(Me.var_fee) & ",impuesto_fee=" & Sql_Texto(Me.var_impuesto_fee) & ",total_reporte=" & Sql_Texto(Me.var_total_reporte) & ",monto_pagar=" & Sql_Texto(Me.var_monto_pagar) & ",id_cliente=" & Sql_Texto(Me.var_id_cliente) & ",nombre_cliente=" & Sql_Texto(Me.var_nombre_cliente) & ",identificacion_cliente=" & Sql_Texto(Me.var_identificacion_cliente) & ",numero_boleto=" & Sql_Texto(Me.var_numero_boleto) & ",descripcion_boleto=" & Sql_Texto(Me.var_descripcion_boleto) & ",id_aerolinea=" & Sql_Texto(Me.var_id_aerolinea) & ",id_sistema=" & Sql_Texto(Me.var_id_sistema) & ",pantalla=" & Sql_Texto(Me.var_pantalla) & ",id_usuario_reg=" & Sql_Texto(Me.var_id_usuario_reg) & ",fecha_reg=" & Sql_Texto(Me.var_fecha_reg, True) & ",id_moneda=" & Sql_Texto(Me.var_id_moneda) & ",id_producto=" & Sql_Texto(Me.var_id_producto) & ",cantidad=" & Sql_Texto(Me.var_cantidad) & ",descripcion=" & Sql_Texto(Me.var_descripcion) & ",comentario=" & Sql_Texto(Me.var_comentario) & ",precio=" & Sql_Texto(Me.var_precio) & ",impuesto=" & Sql_Texto(Me.var_impuesto) & ",id_hotel=" & Sql_Texto(Me.var_id_hotel) & ",fecha_inicio=" & Sql_Texto(Me.var_fecha_inicio) & ",fecha_fin=" & Sql_Texto(Me.var_fecha_fin), Me.var_Campo_Id & "=" & Me.var_id) Then
                var_msj = var_error

                Dim obj_log As New cls_logs
                obj_log.ComentarioLog = "Error editando venta '" & Me.var_id & " - " & var_id_usuario & " - " & Me.var_fecha & "': " & var_error
                obj_log.FechaLog = Now
                obj_log.id_Menu = New cls_modulos("ventas").Id
                obj_log.id_Usuario = var_id_usuario
                obj_log.idAccion = New cls_acciones("editar").Id
                obj_log.ResultadoLog = False
                obj_log.InsertarLog()

                Return False
                Exit Function

            Else

                Dim obj_sb As New StringBuilder
                obj_sb.Append("," & Chr(34) & "Numero" & Chr(34) & ":" & Chr(34) & Me.numero & Chr(34) & "")
                obj_sb.Append("," & Chr(34) & "Fecha" & Chr(34) & ":" & Chr(34) & Me.fecha & Chr(34) & "")
                obj_sb.Append("," & Chr(34) & "tipo" & Chr(34) & ":" & Chr(34) & Me.tipo & Chr(34) & "")
                obj_sb.Append("," & Chr(34) & "id_agencia" & Chr(34) & ":" & Chr(34) & Me.IdAgencia & Chr(34) & "")
                obj_sb.Append("," & Chr(34) & "id_freelance" & Chr(34) & ":" & Chr(34) & Me.IdFreelance & Chr(34) & "")
                obj_sb.Append("," & Chr(34) & "id_vendedor" & Chr(34) & ":" & Chr(34) & Me.IdVendedor & Chr(34) & "")
                obj_sb.Append("," & Chr(34) & "total" & Chr(34) & ":" & Chr(34) & Me.total & Chr(34) & "")
                obj_sb.Append("," & Chr(34) & "tax" & Chr(34) & ":" & Chr(34) & Me.tax & Chr(34) & "")
                obj_sb.Append("," & Chr(34) & "porcentaje_ct" & Chr(34) & ":" & Chr(34) & Me.porcentaje_ct & Chr(34) & "")
                obj_sb.Append("," & Chr(34) & "comision_ct" & Chr(34) & ":" & Chr(34) & Me.comision_ct & Chr(34) & "")
                obj_sb.Append("," & Chr(34) & "porcentaje_ctg" & Chr(34) & ":" & Chr(34) & Me.porcentaje_ctg & Chr(34) & "")
                obj_sb.Append("," & Chr(34) & "comision_ctg" & Chr(34) & ":" & Chr(34) & Me.comision_ctg & Chr(34) & "")
                obj_sb.Append("," & Chr(34) & "fee" & Chr(34) & ":" & Chr(34) & Me.fee & Chr(34) & "")
                obj_sb.Append("," & Chr(34) & "impuesto_fee" & Chr(34) & ":" & Chr(34) & Me.impuesto_fee & Chr(34) & "")
                obj_sb.Append("," & Chr(34) & "total_reporte" & Chr(34) & ":" & Chr(34) & Me.total_reporte & Chr(34) & "")
                obj_sb.Append("," & Chr(34) & "monto_pagar" & Chr(34) & ":" & Chr(34) & Me.monto_pagar & Chr(34) & "")
                obj_sb.Append("," & Chr(34) & "id_cliente" & Chr(34) & ":" & Chr(34) & Me.id_cliente & Chr(34) & "")
                obj_sb.Append("," & Chr(34) & "nombre_cliente" & Chr(34) & ":" & Chr(34) & Me.nombre_cliente & Chr(34) & "")
                obj_sb.Append("," & Chr(34) & "identificacion_cliente" & Chr(34) & ":" & Chr(34) & Me.identificacion_cliente & Chr(34) & "")
                obj_sb.Append("," & Chr(34) & "numero_boleto" & Chr(34) & ":" & Chr(34) & Me.numero_boleto & Chr(34) & "")
                obj_sb.Append("," & Chr(34) & "descripcion_boleto" & Chr(34) & ":" & Chr(34) & Me.descripcion_boleto & Chr(34) & "")
                obj_sb.Append("," & Chr(34) & "id_aerolinea" & Chr(34) & ":" & Chr(34) & Me.IdAerolinea & Chr(34) & "")
                obj_sb.Append("," & Chr(34) & "id_sistema" & Chr(34) & ":" & Chr(34) & Me.IdSistema & Chr(34) & "")
                obj_sb.Append("," & Chr(34) & "pantalla" & Chr(34) & ":" & Chr(34) & Me.pantalla & Chr(34) & "")
                obj_sb.Append("," & Chr(34) & "id_moneda" & Chr(34) & ":" & Chr(34) & Me.id_moneda & Chr(34) & "")
                obj_sb.Append("," & Chr(34) & "id_producto" & Chr(34) & ":" & Chr(34) & Me.id_producto & Chr(34) & "")
                obj_sb.Append("," & Chr(34) & "cantidad" & Chr(34) & ":" & Chr(34) & Me.cantidad & Chr(34) & "")
                obj_sb.Append("," & Chr(34) & "descripcion" & Chr(34) & ":" & Chr(34) & Me.descripcion & Chr(34) & "")
                obj_sb.Append("," & Chr(34) & "comentario" & Chr(34) & ":" & Chr(34) & Me.comentario & Chr(34) & "")
                obj_sb.Append("," & Chr(34) & "precio" & Chr(34) & ":" & Chr(34) & Me.precio & Chr(34) & "")
                obj_sb.Append("," & Chr(34) & "impuesto" & Chr(34) & ":" & Chr(34) & Me.impuesto & Chr(34) & "")
                obj_sb.Append("," & Chr(34) & "id_hotel" & Chr(34) & ":" & Chr(34) & Me.id_hotel & Chr(34) & "")
                obj_sb.Append("," & Chr(34) & "fecha_inicio" & Chr(34) & ":" & Chr(34) & Me.fechaInicio & Chr(34) & "")
                obj_sb.Append("," & Chr(34) & "fecha_fin" & Chr(34) & ":" & Chr(34) & Me.fechaFin & Chr(34) & "")

                Dim obj_log As New cls_logs
                obj_log.ComentarioLog = "venta editado: {" & obj_sb.ToString & "}"
                obj_log.FechaLog = Now
                obj_log.id_Menu = New cls_modulos("ventas").Id
                obj_log.id_Usuario = var_id_usuario
                obj_log.idAccion = New cls_acciones("editar").Id
                obj_log.ResultadoLog = True
                obj_log.InsertarLog()

                Return True

            End If
        Else
            var_error = "No se encontro el Registro"
            Return False
        End If
    End Function

    Public Shared Function Eliminar(ByVal var_id As Integer, ByVal obj_usuario As cls_usuarios, ByRef var_mensaje As String) As Boolean
        Dim obj_Conex_int As New SqlConnection(ConfigurationManager.ConnectionStrings("connection").ConnectionString)
        ac_Funciones.Eliminar(obj_Conex_int, cls_ventas.Nombre_Tabla, cls_ventas.Campo_Id & "=" & var_id)
        ac_Funciones.Eliminar(obj_Conex_int, cls_ventas_detalles.Nombre_Tabla, "id_hotel=" & var_id)
        Return True
    End Function

    Public Shared Function Siguiente_numero() As Integer
        Dim obj_Conex_int As New SqlConnection(ConfigurationManager.ConnectionStrings("connection").ConnectionString)
        Return ac_Funciones.Valor_De(obj_Conex_int, "select isnull(max(numero),0)+1 from tbl_ventas")

    End Function

    Public Shared Function ConsultaActivosVentas(ByRef var_venta As String, ByRef var_error As String) As DataTable
        Dim obj_Conex_int As New SqlConnection(ConfigurationManager.ConnectionStrings("CCconexion").ConnectionString)
        Dim var_consulta As String = "Select * from " & cls_ventas.ListadoActivosRecibos & "(" & Sql_Texto(var_venta) & ")"
        Dim var_msj As String = ""

        Dim obj_dt_int As System.Data.DataTable = Abrir_Tabla(obj_Conex_int, var_consulta, var_msj)
        var_error = var_msj
        Return obj_dt_int
    End Function
#End Region

End Class
