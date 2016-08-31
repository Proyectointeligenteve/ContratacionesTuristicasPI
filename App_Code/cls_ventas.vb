Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports ac_Funciones

Public Class cls_ventas

#Region "VARIABLES"
    Dim var_Nombre_Tabla As String = "tbl_ventas"
    Dim var_Campo_Id As String = "id"
    Dim var_Campos As String = "numero,fecha,tipo,id_agencia,id_freelance,id_vendedor,total,tax,porcentaje_ct,comision_ct,porcentaje_ctg,comision_ctg,fee,impuesto_fee,total_reporte,monto_pagar,id_cliente,nombre_cliente,identificacion_cliente,numero_boleto,descripcion_boleto,id_aerolinea,id_sistema,pantalla,id_usuario_reg,fecha_reg,id_moneda"
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
    Dim var_id_moneda As Integer = 0

    'Dim obj_detalle As New Generic.List(Of cls_ventas_detalles)
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

    'Public ReadOnly Property Detalleventas() As Generic.List(Of cls_ventas_detalles)
    '    Get
    '        Return Me.obj_detalle
    '    End Get
    'End Property

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
            
            'Dim obj_dt_detalles As DataTable = ac_Funciones.Abrir_Tabla(Me.obj_Conex_int, "select id from " & cls_ventas_detalles.Nombre_Tabla & " where id_venta=" & Me.var_id)
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
            If Not Ingresar(Me.obj_Conex_int, Me.var_Nombre_Tabla, Me.var_Campos, Sql_Texto(Me.var_numero) & "," & Sql_Texto(Me.var_fecha, True) & "," & Sql_Texto(Me.var_tipo) & "," & Sql_Texto(Me.var_id_agencia) & "," & Sql_Texto(Me.var_id_freelance) & "," & Sql_Texto(Me.var_id_vendedor) & "," & Sql_Texto(Me.var_total) & "," & Sql_Texto(Me.var_tax) & "," & Sql_Texto(Me.var_porcentaje_ct) & "," & Sql_Texto(Me.var_comision_ct) & "," & Sql_Texto(Me.var_porcentaje_ctg) & "," & Sql_Texto(Me.var_comision_ctg) & "," & Sql_Texto(Me.var_fee) & "," & Sql_Texto(Me.var_impuesto_fee) & "," & Sql_Texto(Me.var_total_reporte) & "," & Sql_Texto(Me.var_monto_pagar) & "," & Sql_Texto(Me.var_id_cliente) & "," & Sql_Texto(Me.var_nombre_cliente) & "," & Sql_Texto(Me.var_identificacion_cliente) & "," & Sql_Texto(Me.var_numero_boleto) & "," & Sql_Texto(Me.var_descripcion_boleto) & "," & Sql_Texto(Me.var_id_aerolinea) & "," & Sql_Texto(Me.var_id_sistema) & "," & Sql_Texto(Me.var_pantalla) & "," & Sql_Texto(Me.var_id_usuario_reg) & "," & Sql_Texto(Me.var_fecha_reg, True) & "," & Sql_Texto(Me.var_id_moneda), var_msj) Then
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
                obj_sb.Append("," & Chr(34) & "Fecha" & Chr(34) & ":" & Chr(34) & Me.fecha & Chr(34) & "")
                obj_sb.Append("," & Chr(34) & "idusuario" & Chr(34) & ":" & Chr(34) & var_id_usuario & Chr(34) & "")

                Dim obj_log As New cls_logs
                obj_log.ComentarioLog = "venta agregado: {" & obj_sb.ToString & "}"
                obj_log.FechaLog = Now
                obj_log.id_Menu = New cls_modulos("ventas").Id
                obj_log.id_Usuario = var_id_usuario
                obj_log.idAccion = New cls_acciones("agregar").Id
                obj_log.ResultadoLog = True
                obj_log.InsertarLog()

                'For i As Integer = 0 To obj_detalle.Count - 1
                '    obj_detalle.Item(i).id_venta = Me.id
                '    Dim var_msj2 As String = ""
                '    If Not obj_detalle.Item(i).Actualizar(var_msj2) Then
                '        var_msj &= " - " & var_msj2
                '    End If
                'Next

                Return True

            End If
        ElseIf Me.var_id > 0 Then 'EDICION
            If Not ac_Funciones.Actualizar(Me.obj_Conex_int, Me.var_Nombre_Tabla, "numero=" & Sql_Texto(Me.var_numero) & ",fecha=" & Sql_Texto(Me.var_fecha, True) & ",tipo=" & Sql_Texto(Me.var_tipo) & ",id_agencia=" & Sql_Texto(Me.var_id_agencia) & ",id_freelance=" & Sql_Texto(Me.var_id_freelance) & ",id_vendedor=" & Sql_Texto(Me.var_id_vendedor) & ",total=" & Sql_Texto(Me.var_total) & ",tax=" & Sql_Texto(Me.var_tax) & ",porcentaje_ct=" & Sql_Texto(Me.var_porcentaje_ct) & ",comision_ct=" & Sql_Texto(Me.var_comision_ct) & ",porcentaje_ctg=" & Sql_Texto(Me.var_porcentaje_ctg) & ",comision_ctg=" & Sql_Texto(Me.var_comision_ctg) & ",fee=" & Sql_Texto(Me.var_fee) & ",impuesto_fee=" & Sql_Texto(Me.var_impuesto_fee) & ",total_reporte=" & Sql_Texto(Me.var_total_reporte) & ",monto_pagar=" & Sql_Texto(Me.var_monto_pagar) & ",id_cliente=" & Sql_Texto(Me.var_id_cliente) & ",nombre_cliente=" & Sql_Texto(Me.var_nombre_cliente) & ",identificacion_cliente=" & Sql_Texto(Me.var_identificacion_cliente) & ",numero_boleto=" & Sql_Texto(Me.var_numero_boleto) & ",descripcion_boleto=" & Sql_Texto(Me.var_descripcion_boleto) & ",id_aerolinea=" & Sql_Texto(Me.var_id_aerolinea) & ",id_sistema=" & Sql_Texto(Me.var_id_sistema) & ",pantalla=" & Sql_Texto(Me.var_pantalla) & ",id_usuario_reg=" & Sql_Texto(Me.var_id_usuario_reg) & ",fecha_reg=" & Sql_Texto(Me.var_fecha_reg, True) & ",id_moneda=" & Sql_Texto(Me.var_id_moneda), Me.var_Campo_Id & "=" & Me.var_id) Then
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
                obj_sb.Append("," & Chr(34) & "Fecha" & Chr(34) & ":" & Chr(34) & Me.fecha & Chr(34) & "")
                obj_sb.Append("," & Chr(34) & "idusuario" & Chr(34) & ":" & Chr(34) & var_id_usuario & Chr(34) & "")

                Dim obj_log As New cls_logs
                obj_log.ComentarioLog = "venta editado: {" & obj_sb.ToString & "}"
                obj_log.FechaLog = Now
                obj_log.id_Menu = New cls_modulos("ventas").Id
                obj_log.id_Usuario = var_id_usuario
                obj_log.idAccion = New cls_acciones("editar").Id
                obj_log.ResultadoLog = True
                obj_log.InsertarLog()

                'Dim var_ids As String = ""
                'For i As Integer = 0 To Me.obj_detalle.Count - 1
                '    Me.obj_detalle.Item(i).id_venta = Me.id
                '    Dim var_msj2 As String = ""
                '    Me.obj_detalle.Item(i).Actualizar(var_msj2)
                '    If var_msj2.Trim.Length > 0 Then
                '        var_msj &= " - " & var_msj2
                '    End If
                '    var_ids &= "," & Me.obj_detalle.Item(i).Id
                'Next
                'If var_ids.Trim.Length > 1 Then
                '    ac_Funciones.Eliminar(Me.obj_Conex_int, cls_ventas_detalles.Nombre_Tabla, "id_venta=" & Me.var_id & " and id not in(" & var_ids.Substring(1) & ")")
                'Else
                '    ac_Funciones.Eliminar(Me.obj_Conex_int, cls_ventas_detalles.Nombre_Tabla, "id_venta=" & Me.var_id)
                'End If

                Return True

            End If
        Else
            var_error = "No se encontro el Registro"
            Return False
        End If
    End Function

    Public Function ListaDetalleVenta() As DataTable
        'Dim obj_dt As New System.Data.DataTable
        'obj_dt.Columns.Add("DT_RowId", GetType(System.Int32))
        ''obj_dt.Columns.Add("Botones", GetType(System.String))
        'obj_dt.Columns.Add("Detalle", GetType(System.String))
        'obj_dt.Columns.Add("Pedido", GetType(System.String))
        'obj_dt.Columns.Add("Producto", GetType(System.String))
        'obj_dt.Columns.Add("Renglon", GetType(System.String))
        'obj_dt.Columns.Add("Cantidad", GetType(System.String))
        'obj_dt.Columns.Add("Codigo", GetType(System.String))
        'obj_dt.Columns.Add("Descripcion", GetType(System.String))
        'obj_dt.Columns.Add("Unidad", GetType(System.String))
        'obj_dt.Columns.Add("Moneda", GetType(System.String))
        'obj_dt.Columns.Add("Precio", GetType(System.Double))
        'obj_dt.Columns.Add("Total", GetType(System.Double))
        'obj_dt.Columns.Add("Impuesto", GetType(System.Double))
        'For i As Integer = 0 To Me.obj_detalle.Count - 1
        '    'obj_dt.Rows.Add(i + 1, "", Me.numero, Me.obj_detalle(i).id_producto.descripcion, Me.obj_detalle(i).renglon, Me.obj_detalle(i).cantidad, Me.obj_detalle(i).codigo, Me.obj_detalle(i).descripcion, Me.obj_detalle(i).unidad, Me.obj_detalle(i).precio, Me.obj_detalle(i).total)
        '    obj_dt.Rows.Add(i + 1, "", Me.numero, Me.obj_detalle(i).id_producto.descripcion, Me.obj_detalle(i).renglon, Me.obj_detalle(i).cantidad, Me.obj_detalle(i).codigo, Me.obj_detalle(i).descripcion & IIf((Me.obj_detalle(i).Comentario1).Trim.Length > 0, ". " & Me.obj_detalle(i).Comentario1, "") & IIf((Me.obj_detalle(i).Comentario2).Trim.Length > 0, ". " & Me.obj_detalle(i).Comentario2, "") & IIf((Me.obj_detalle(i).Comentario3).Trim.Length > 0, ". " & Me.obj_detalle(i).Comentario3, "") & IIf((Me.obj_detalle(i).Comentario4).Trim.Length > 0, ". " & Me.obj_detalle(i).Comentario4, "") & IIf((Me.obj_detalle(i).Comentario5).Trim.Length > 0, ". " & Me.obj_detalle(i).Comentario5, ""), New cls_unidades(Me.obj_detalle(i).unidad).nombre, New cls_monedas(Me.obj_detalle(i).id_moneda).simbolo, Me.obj_detalle(i).precio, Me.obj_detalle(i).total, Me.obj_detalle(i).impuesto)
        'Next

        'Return obj_dt
    End Function
    Public Shared Function Eliminar(ByVal var_id As Integer, ByVal obj_usuario As cls_usuarios, ByRef var_mensaje As String) As Boolean
        Dim obj_Conex_int As New SqlConnection(ConfigurationManager.ConnectionStrings("connection").ConnectionString)
        ac_Funciones.Eliminar(obj_Conex_int, cls_ventas.Nombre_Tabla, cls_ventas.Campo_Id & "=" & var_id)
        ac_Funciones.Eliminar(obj_Conex_int, cls_ventas_detalles.Nombre_Tabla, "id_venta=" & var_id)
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
