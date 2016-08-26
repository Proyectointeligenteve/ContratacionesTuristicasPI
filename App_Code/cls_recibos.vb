
Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports ac_Funciones

Public Class cls_recibos

#Region "VARIABLES"
    Dim var_Nombre_Tabla As String = "tbl_recibos"
    Dim var_Campo_Id As String = "id"
    Dim var_Campo_Validacion As String = "numero"
    Dim var_Campos As String = "numero,fecha,monto,id_venta,concepto,id_usuario_reg,fecha_reg,id_cliente,nombre,id_moneda,observaciones"
    Dim obj_Conex_int As New SqlConnection(ConfigurationManager.ConnectionStrings("connection").ConnectionString)

    Dim var_id As Integer
    Dim var_numero As String = ""
    Dim var_fecha As Date = Now.Date
    Dim var_monto As Double = 0
    Dim var_id_venta As Integer = 0
    Dim var_concepto As String = ""
    Dim var_id_usuario_reg As Integer = 0
    Dim var_fecha_reg As Date = Now
    Dim var_id_cliente As Integer = 0
    Dim var_nombre As String = ""
    Dim var_id_moneda As Integer = 0
    Dim var_observaciones As String = ""

    Dim obj_formaPago As New Generic.List(Of cls_recibos_formasPagos)
#End Region

#Region "PROPIEDADES"

    Public Shared ReadOnly Property Nombre_Tabla() As String
        Get
            Return "tbl_recibos"
        End Get
    End Property

    Public Shared ReadOnly Property Campo_Id() As String
        Get
            Return "id"
        End Get
    End Property

    Public Shared ReadOnly Property Campo_Validacion() As String
        Get
            Return "numero"
        End Get
    End Property
    Public Shared ReadOnly Property Listado() As String
        Get
            Return "lst_recibos"
        End Get
    End Property

    Public Shared ReadOnly Property ListadoActivos() As String
        Get
            Return "lst_recibosActivos"
        End Get
    End Property

    Public Shared ReadOnly Property ListadoAnulados() As String
        Get
            Return "lst_recibosAnulados"
        End Get
    End Property

    Public ReadOnly Property FormaPago() As Generic.List(Of cls_recibos_formasPagos)
        Get
            Return Me.obj_formaPago
        End Get
    End Property

    Public ReadOnly Property Id() As Integer
        Get
            Return Me.var_id
        End Get
    End Property

    Public Property Numero() As String
        Get
            Return Me.var_numero
        End Get
        Set(ByVal value As String)
            Me.var_numero = value
        End Set
    End Property

    Public Property Fecha() As Date
        Get
            Return Me.var_fecha
        End Get
        Set(ByVal value As Date)
            Me.var_fecha = value
        End Set
    End Property

    Public Property Monto() As Double
        Get
            Return Me.var_monto
        End Get
        Set(ByVal value As Double)
            Me.var_monto = value
        End Set
    End Property

    Public Property IdVenta() As Integer
        Get
            Return Me.var_id_venta
        End Get
        Set(ByVal value As Integer)
            Me.var_id_venta = value
        End Set
    End Property

    Public Property Concepto() As String
        Get
            Return Me.var_concepto
        End Get
        Set(ByVal value As String)
            Me.var_concepto = value
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

    Public Property id_cliente() As Integer
        Get
            Return Me.var_id_cliente
        End Get
        Set(ByVal value As Integer)
            Me.var_id_cliente = value
        End Set
    End Property

    Public Property nombre() As String
        Get
            Return Me.var_nombre
        End Get
        Set(ByVal value As String)
            Me.var_nombre = value
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

    Public Property observaciones() As String
        Get
            Return Me.var_observaciones
        End Get
        Set(ByVal value As String)
            Me.var_observaciones = value
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
        obj_dt_int = Abrir_Tabla(Me.obj_Conex_int, "Select * from " & Me.var_Nombre_Tabla & " WHERE " & Me.var_Campo_Id & "=" & var_id_int)
        If obj_dt_int.Rows.Count > 0 Then
            Me.var_id = obj_dt_int.Rows(0).Item("id").ToString
            Me.var_numero = ac_Funciones.formato_Texto(obj_dt_int.Rows(0).Item("numero").ToString)
            Me.var_fecha = ac_Funciones.formato_Fecha(obj_dt_int.Rows(0).Item("fecha").ToString)
            Me.var_monto = ac_Funciones.formato_Numero(obj_dt_int.Rows(0).Item("monto").ToString, True)
            Me.var_id_venta = ac_Funciones.formato_Numero(obj_dt_int.Rows(0).Item("id_venta").ToString)
            Me.var_concepto = ac_Funciones.formato_Texto(obj_dt_int.Rows(0).Item("concepto").ToString)
            Me.var_id_usuario_reg = ac_Funciones.formato_Numero(obj_dt_int.Rows(0).Item("id_usuario_reg").ToString)
            Me.var_fecha_reg = ac_Funciones.formato_Fecha(obj_dt_int.Rows(0).Item("fecha_reg").ToString)
            Me.var_id_cliente = ac_Funciones.formato_Numero(obj_dt_int.Rows(0).Item("id_cliente").ToString)
            Me.var_nombre = ac_Funciones.formato_Texto(obj_dt_int.Rows(0).Item("nombre").ToString)
            Me.var_id_moneda = ac_Funciones.formato_Numero(obj_dt_int.Rows(0).Item("id_moneda").ToString)
            Me.var_observaciones = ac_Funciones.formato_Texto(obj_dt_int.Rows(0).Item("observaciones").ToString)

            Dim obj_dt_formas_pago As DataTable = ac_Funciones.Abrir_Tabla(Me.obj_Conex_int, "select id from " & cls_recibos_formasPagos.Nombre_Tabla & " where id_recibo=" & Me.var_id)
            For i As Integer = 0 To obj_dt_formas_pago.Rows.Count - 1
                Me.obj_formaPago.Add(New cls_recibos_formasPagos(obj_dt_formas_pago.Rows(i).Item(0)))
            Next
        Else
            Me.var_id = -1
        End If
    End Sub

    Public Shared Function Lista(Optional ByVal var_filtro As String = "") As DataTable
        Dim obj_Conex_int As New SqlConnection(ConfigurationManager.ConnectionStrings("connection").ConnectionString)
        'Dim obj_dt_int As DataTable = Abrir_Tabla(obj_Conex_int, "select id, numero as des from tbl_tipos_acciones" & IIf(var_filtro <> "", " where " & var_filtro, "") & " order by numero")
        Dim obj_dt_int As DataTable = Abrir_Tabla(obj_Conex_int, "select " & cls_recibos.Campo_Id & " as id, " & cls_recibos.Campo_Validacion & " as des from " & cls_recibos.Nombre_Tabla & IIf(var_filtro <> "", " where " & var_filtro, "") & " order by " & cls_recibos.Campo_Validacion & "")
        obj_dt_int.Rows.Add(0, "SELECCIONE")
        Return obj_dt_int
    End Function

    Public Function Actualizar(ByRef var_msj As String) As Boolean
        Dim var_error As String = ""
        If Validar_Existe(Me.obj_Conex_int, Me.var_Nombre_Tabla, Me.var_Campo_Validacion, Me.var_numero, Me.var_Campo_Id, Me.var_id) Then

            If var_error = "" Then
                var_error = Me.var_numero & "' ya existe en la base de datos"
            End If
            Return False
            Exit Function
        End If

        If Me.var_id = 0 Then   'NUEVO
            If Not Ingresar(Me.obj_Conex_int, Me.var_Nombre_Tabla, Me.var_Campos, Sql_Texto(Me.var_numero) & "," & Sql_Texto(Me.var_fecha) & "," & Sql_Texto(Me.var_monto) & "," & Sql_Texto(Me.var_id_venta) & "," & Sql_Texto(Me.var_concepto) & "," & Sql_Texto(Me.var_id_usuario_reg) & "," & Sql_Texto(Me.var_fecha_reg) & "," & Sql_Texto(Me.var_id_cliente) & "," & Sql_Texto(Me.var_nombre) & "," & Sql_Texto(Me.var_id_moneda) & "," & Sql_Texto(Me.var_observaciones), var_error) Then
                Dim obj_log As New cls_logs
                obj_log.ComentarioLog = "Error agregando recibo '" & Me.var_numero & " - " & Me.var_fecha & " - " & Me.var_monto & "': " & var_error
                obj_log.FechaLog = Now
                obj_log.id_Menu = New cls_modulos("recibos").Id
                obj_log.id_Usuario = var_id_usuario_reg
                obj_log.idAccion = New cls_acciones("agregar").Id
                obj_log.ResultadoLog = False
                obj_log.InsertarLog()

                Return False
                Exit Function
            Else
                Me.var_id = Valor_De(Me.obj_Conex_int, "select " & Me.var_Campo_Id & " from " & Me.var_Nombre_Tabla & " where numero=" & Sql_Texto(Me.var_numero))

                Dim obj_sb As New StringBuilder
                obj_sb.Append(Chr(34) & "Id" & Chr(34) & ":" & Chr(34) & Me.Id & Chr(34) & "")
                obj_sb.Append("," & Chr(34) & "Numero" & Chr(34) & ":" & Chr(34) & Me.Numero & Chr(34) & "")
                obj_sb.Append("," & Chr(34) & "Fecha" & Chr(34) & ":" & Chr(34) & Me.Fecha & Chr(34) & "")
                obj_sb.Append("," & Chr(34) & "Monto" & Chr(34) & ":" & Chr(34) & Me.Monto & Chr(34) & "")
                obj_sb.Append("," & Chr(34) & "IdVenta" & Chr(34) & ":" & Chr(34) & Me.IdVenta & Chr(34) & "")
                obj_sb.Append("," & Chr(34) & "Concepto" & Chr(34) & ":" & Chr(34) & Me.Concepto & Chr(34) & "")

                Dim obj_log As New cls_logs
                obj_log.ComentarioLog = "Recibo agregada: {" & obj_sb.ToString & "}"
                obj_log.FechaLog = Now
                obj_log.id_Menu = New cls_modulos("recibos").Id
                obj_log.id_Usuario = var_id_usuario_reg
                obj_log.idAccion = New cls_acciones("agregar").Id
                obj_log.ResultadoLog = True
                obj_log.InsertarLog()

                For i As Integer = 0 To obj_formaPago.Count - 1
                    obj_formaPago.Item(i).id_recibo = Me.Id
                    Dim var_msj2 As String = ""
                    If Not obj_formaPago.Item(i).Actualizar(var_error) Then
                        var_msj &= " - " & var_msj2
                    End If
                Next
                Return True
            End If
        ElseIf Me.var_id > 0 Then 'EDICION
            If Not ac_Funciones.Actualizar(Me.obj_Conex_int, Me.var_Nombre_Tabla, "numero=" & Sql_Texto(Me.var_numero) & ",fecha=" & Sql_Texto(Me.var_fecha) & ",monto=" & Sql_Texto(Me.var_monto) & ",id_venta=" & Sql_Texto(Me.var_id_venta) & ",concepto=" & Sql_Texto(Me.var_concepto) & ",id_cliente=" & Sql_Texto(Me.var_id_cliente) & ",nombre=" & Sql_Texto(Me.var_nombre) & ",id_moneda=" & Sql_Texto(Me.var_id_moneda) & ",observaciones=" & Sql_Texto(Me.var_observaciones), Me.var_Campo_Id & "=" & Me.var_id) Then
                var_msj = var_error

                Dim obj_log As New cls_logs
                obj_log.ComentarioLog = "Error editando recibo '" & Me.var_id & " - " & Me.var_numero & " - " & Me.var_fecha & " - " & Me.var_monto & "': " & var_error
                obj_log.FechaLog = Now
                obj_log.id_Menu = New cls_modulos("recibos").Id
                obj_log.id_Usuario = var_id_usuario_reg
                obj_log.idAccion = New cls_acciones("editar").Id
                obj_log.ResultadoLog = False
                obj_log.InsertarLog()

                Return False
                Exit Function

            Else

                Dim obj_sb As New StringBuilder
                obj_sb.Append(Chr(34) & "Id" & Chr(34) & ":" & Chr(34) & Me.Id & Chr(34) & "")
                obj_sb.Append("," & Chr(34) & "Numero" & Chr(34) & ":" & Chr(34) & Me.Numero & Chr(34) & "")
                obj_sb.Append("," & Chr(34) & "Fecha" & Chr(34) & ":" & Chr(34) & Me.Fecha & Chr(34) & "")
                obj_sb.Append("," & Chr(34) & "Monto" & Chr(34) & ":" & Chr(34) & Me.Monto & Chr(34) & "")
                obj_sb.Append("," & Chr(34) & "IdVenta" & Chr(34) & ":" & Chr(34) & Me.IdVenta & Chr(34) & "")
                obj_sb.Append("," & Chr(34) & "Concepto" & Chr(34) & ":" & Chr(34) & Me.Concepto & Chr(34) & "")

                Dim obj_log As New cls_logs
                obj_log.ComentarioLog = "Recibo editado: {" & obj_sb.ToString & "}"
                obj_log.FechaLog = Now
                obj_log.id_Menu = New cls_modulos("recibos").Id
                obj_log.id_Usuario = var_id_usuario_reg
                obj_log.idAccion = New cls_acciones("editar").Id
                obj_log.ResultadoLog = True
                obj_log.InsertarLog()
            End If

            Dim var_ids As String = ""
            For i As Integer = 0 To Me.obj_formaPago.Count - 1
                Me.obj_formaPago.Item(i).id_recibo = Me.Id
                Dim var_msj2 As String = ""
                Me.obj_formaPago.Item(i).Actualizar(var_msj2)
                var_msj &= " - " & var_msj2
                var_ids &= "," & Me.obj_formaPago.Item(i).Id

            Next
            If var_ids.Trim.Length > 1 Then
                ac_Funciones.Eliminar(Me.obj_Conex_int, cls_recibos_formasPagos.Nombre_Tabla, "id_recibo=" & Me.var_id & " and id not in(" & var_ids.Substring(1) & ")")
            Else
                ac_Funciones.Eliminar(Me.obj_Conex_int, cls_recibos_formasPagos.Nombre_Tabla, "id_recibo=" & Me.var_id)
            End If

            Return True
        Else
            var_error = "No se encontro el Registro"
            Return False
        End If
    End Function

    Public Function ListaContactos() As DataTable
        Dim obj_dt As New System.Data.DataTable
        obj_dt.Columns.Add("DT_RowId", GetType(System.Int32))
        obj_dt.Columns.Add("Recibo", GetType(System.String))
        obj_dt.Columns.Add("Monto", GetType(System.String))
        obj_dt.Columns.Add("Banco", GetType(System.String))
        obj_dt.Columns.Add("Fecha", GetType(System.String))
        obj_dt.Columns.Add("FormaPago", GetType(System.String))
        For i As Integer = 0 To Me.FormaPago.Count - 1
            obj_dt.Rows.Add(i + 1, Me.obj_formaPago(i).id_recibo, Me.obj_formaPago(i).Monto, New cls_bancos(Me.obj_formaPago(i).banco).nombre, Me.obj_formaPago(i).fecha, Me.obj_formaPago(i).FormaPago.nombre)
        Next

        Return obj_dt
    End Function

    Public Shared Function Anular(ByVal var_id As Integer, ByVal var_id_usuario As Integer, ByRef var_mensaje As String) As Boolean
        Dim obj_Conex_int As New SqlConnection(ConfigurationManager.ConnectionStrings("connection").ConnectionString)
        Dim var_error As String = ""
        Dim obj_recibo As New cls_recibos(var_id)
        If Not ac_Funciones.Actualizar(obj_Conex_int, cls_recibos.Nombre_Tabla, "anulado= case when isnull(anulado,0)=0 then '1' else '0' end, fecha_anulacion=" & Sql_Texto(Now, True) & ", id_usuario_anulacion=" & var_id_usuario, cls_recibos.Campo_Id & "=" & var_id, var_error) Then
            var_mensaje = var_error

            Dim obj_log As New cls_logs
            obj_log.ComentarioLog = "Error anulando recibo '" & obj_recibo.Id & " - " & obj_recibo.Numero & "': " & var_error
            obj_log.FechaLog = Now
            obj_log.id_Menu = New cls_modulos("recibos").Id
            obj_log.id_Usuario = var_id_usuario
            obj_log.idAccion = New cls_acciones("anular").Id
            obj_log.ResultadoLog = False
            obj_log.InsertarLog()

            Return False
        Else

            Dim obj_log As New cls_logs
            obj_log.ComentarioLog = "Recibo anulado '" & obj_recibo.Id & " - " & obj_recibo.Numero
            obj_log.FechaLog = Now
            obj_log.id_Menu = New cls_modulos("recibos").Id
            obj_log.id_Usuario = var_id_usuario
            obj_log.idAccion = New cls_acciones("anular").Id
            obj_log.ResultadoLog = True
            obj_log.InsertarLog()

            Return True
        End If
        Return True
    End Function
    Public Shared Function Eliminar(ByVal var_id As Integer, ByVal obj_usuario As cls_usuarios, ByRef var_mensaje As String) As Boolean
        Dim obj_Conex_int As New SqlConnection(ConfigurationManager.ConnectionStrings("connection").ConnectionString)
        ac_Funciones.Eliminar(obj_Conex_int, cls_recibos.Nombre_Tabla, cls_recibos.Campo_Id & "=" & var_id)
        Return True
    End Function
    'Public Shared Function Consulta(Optional ByVal var_filtro As String = "", Optional ByVal var_orden As String = "", Optional ByRef var_error As String = "") As DataTable
    '    Dim obj_Conex_int As New SqlConnection(ConfigurationManager.ConnectionStrings("connection").ConnectionString)
    '    Dim var_consulta As String = "Select * from " & cls_recibos.Listado & "()"
    '    Return Abrir_Tabla(obj_Conex_int, var_consulta, var_error)
    'End Function

    Public Function ListaDetalleFormasPagos() As DataTable
        Dim obj_dt As New System.Data.DataTable
        obj_dt.Columns.Add("DT_RowId", GetType(System.Int32))
        obj_dt.Columns.Add("Monto", GetType(System.Double))
        obj_dt.Columns.Add("Fecha", GetType(System.DateTime))
        obj_dt.Columns.Add("Forma Pago", GetType(System.String))
        obj_dt.Columns.Add("Numero Documento", GetType(System.String))
        obj_dt.Columns.Add("Banco", GetType(System.String))
        For i As Integer = 0 To Me.obj_formaPago.Count - 1
            'obj_dt.Rows.Add(i + 1, "", Me.numero, Me.obj_detalle(i).id_producto.descripcion, Me.obj_detalle(i).renglon, Me.obj_detalle(i).cantidad, Me.obj_detalle(i).codigo, Me.obj_detalle(i).descripcion, Me.obj_detalle(i).unidad, Me.obj_detalle(i).precio, Me.obj_detalle(i).total)
            obj_dt.Rows.Add(i + 1, ac_Funciones.formato_Numero_Pantalla(Me.obj_formaPago(i).Monto.ToString()), Me.obj_formaPago(i).fecha, Me.obj_formaPago(i).FormaPago.nombre, Me.obj_formaPago(i).numero_documento, New cls_bancos(Me.obj_formaPago(i).banco).nombre)
        Next

        Return obj_dt
    End Function
    Public Shared Function Consulta(Optional ByVal var_filtro As String = "", Optional ByRef var_error As String = "") As DataTable
        Dim obj_Conex_int As New SqlConnection(ConfigurationManager.ConnectionStrings("connection").ConnectionString)
        Dim var_consulta As String = "Select * from " & cls_recibos.Listado & "()"
        Dim var_msj As String = ""

        Dim obj_dt_int As System.Data.DataTable = Abrir_Tabla(obj_Conex_int, var_consulta, var_msj)
        var_error = var_msj
        Return obj_dt_int
    End Function
    Public Shared Function ConsultaActivos(Optional ByVal var_filtro As String = "", Optional ByRef var_error As String = "") As DataTable
        Dim obj_Conex_int As New SqlConnection(ConfigurationManager.ConnectionStrings("connection").ConnectionString)
        Dim var_consulta As String = "Select * from " & cls_recibos.ListadoActivos & "()"
        Dim var_msj As String = ""

        Dim obj_dt_int As System.Data.DataTable = Abrir_Tabla(obj_Conex_int, var_consulta, var_msj)
        var_error = var_msj
        Return obj_dt_int
    End Function

    Public Shared Function ConsultaAnulados(Optional ByVal var_filtro As String = "", Optional ByRef var_error As String = "") As DataTable
        Dim obj_Conex_int As New SqlConnection(ConfigurationManager.ConnectionStrings("connection").ConnectionString)
        Dim var_consulta As String = "Select * from " & cls_recibos.ListadoAnulados & "()"
        Dim var_msj As String = ""

        Dim obj_dt_int As System.Data.DataTable = Abrir_Tabla(obj_Conex_int, var_consulta, var_msj)
        var_error = var_msj
        Return obj_dt_int
    End Function

    Public Shared Function SiguienteNumero() As Integer
        Dim obj_Connection As New SqlConnection(ConfigurationManager.ConnectionStrings("CCconexion").ConnectionString)
        Return ac_Funciones.formato_Numero(ac_Funciones.Valor_De(obj_Connection, "select isnull(max(num),0) from (select case when ISNUMERIC(numero)=1 then cast(numero as int) else 0 end as num from tbl_recibos) as c").ToString)
    End Function
#End Region
End Class

