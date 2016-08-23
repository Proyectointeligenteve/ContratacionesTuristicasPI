
Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports ac_Funciones

Public Class cls_recibos

#Region "VARIABLES"
    Dim var_Nombre_Tabla As String = "tbl_recibos"
    Dim var_Campo_Id As String = "id"
    Dim var_Campo_Validacion As String = "numero"
    Dim var_Campos As String = "numero,fecha,monto,id_venta,concepto,id_usuario_reg,fecha_reg"
    Dim obj_Conex_int As New SqlConnection(ConfigurationManager.ConnectionStrings("CCconexion").ConnectionString)

    Dim var_id As Integer
    Dim var_numero As String = ""
    Dim var_fecha As Date = Now.Date
    Dim var_monto As Double = 0
    Dim var_id_venta As Integer = 0
    Dim var_concepto As String = ""
    Dim var_id_usuario_reg As Integer = 0
    Dim var_fecha_reg As Date = Now

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

    Public Property Nombre() As String
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

            Dim obj_dt_formas_pago As DataTable = ac_Funciones.Abrir_Tabla(Me.obj_Conex_int, "select id from " & cls_recibos_formasPagos.Nombre_Tabla & " where id_recibo=" & Me.var_id)
            For i As Integer = 0 To obj_dt_formas_pago.Rows.Count - 1
                Me.obj_formaPago.Add(New cls_recibos_formasPagos(obj_dt_formas_pago.Rows(i).Item(0)))
            Next
        Else
            Me.var_id = -1
        End If
    End Sub

    Public Shared Function Lista(Optional ByVal var_filtro As String = "") As DataTable
        Dim obj_Conex_int As New SqlConnection(ConfigurationManager.ConnectionStrings("CCconexion").ConnectionString)
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
            If Not Ingresar(Me.obj_Conex_int, Me.var_Nombre_Tabla, Me.var_Campos, Sql_Texto(Me.var_numero) & "," & Sql_Texto(Me.var_fecha) & "," & Sql_Texto(Me.var_monto) & "," & Sql_Texto(Me.var_id_venta) & "," & Sql_Texto(Me.var_concepto) & "," & Sql_Texto(Me.var_id_usuario_reg) & "," & Sql_Texto(Me.var_fecha_reg), var_error) Then
                var_msj = var_error
                Return False
                Exit Function
            End If
            Me.var_id = Valor_De(Me.obj_Conex_int, "select " & Me.var_Campo_Id & " from " & Me.var_Nombre_Tabla & " order by id desc")

            For i As Integer = 0 To obj_formaPago.Count - 1
                obj_formaPago.Item(i).id_recibo = Me.Id
                Dim var_msj2 As String = ""
                If Not obj_formaPago.Item(i).Actualizar(var_error) Then
                    var_msj &= " - " & var_msj2
                End If
            Next
            Return True
        ElseIf Me.var_id > 0 Then 'EDICION
            If Not ac_Funciones.Actualizar(Me.obj_Conex_int, Me.var_Nombre_Tabla, "numero=" & Sql_Texto(Me.var_numero) & ",fecha=" & Sql_Texto(Me.var_fecha) & ",monto=" & Sql_Texto(Me.var_monto) & ",id_venta=" & Sql_Texto(Me.var_id_venta) & ",concepto=" & Sql_Texto(Me.var_concepto), Me.var_Campo_Id & "=" & Me.var_id) Then
                Return False
                Exit Function
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
        Dim obj_Conex_int As New SqlConnection(ConfigurationManager.ConnectionStrings("CCconexion").ConnectionString)
        Dim var_error As String = ""
        Dim obj_recibo As New cls_recibos(var_id)
        If Not ac_Funciones.Actualizar(obj_Conex_int, cls_recibos.Nombre_Tabla, "anulado= case when isnull(anulado,0)=0 then '1' else '0' end, fecha_anulacion=" & Sql_Texto(Now, True) & ", id_usuario_anulacion=" & var_id_usuario, cls_recibos.Campo_Id & "=" & var_id, var_error) Then
            var_mensaje = var_error

            Dim obj_log As New cls_logs
            obj_log.ComentarioLog = "Error anulando aerolinea '" & obj_recibo.Id & " - " & obj_recibo.Nombre & "': " & var_error
            obj_log.FechaLog = Now
            obj_log.id_Menu = New cls_modulos("aerolinea").Id
            obj_log.id_Usuario = var_id_usuario
            obj_log.idAccion = New cls_acciones("anular").Id
            obj_log.ResultadoLog = False
            obj_log.InsertarLog()

            Return False
        Else

            Dim obj_log As New cls_logs
            obj_log.ComentarioLog = "aerolinea anulado '" & obj_recibo.Id & " - " & obj_recibo.Nombre
            obj_log.FechaLog = Now
            obj_log.id_Menu = New cls_modulos("aerolinea").Id
            obj_log.id_Usuario = var_id_usuario
            obj_log.idAccion = New cls_acciones("anular").Id
            obj_log.ResultadoLog = True
            obj_log.InsertarLog()

            Return True
        End If
        Return True
    End Function
    Public Shared Function Eliminar(ByVal var_id As Integer, ByVal obj_usuario As cls_usuarios, ByRef var_mensaje As String) As Boolean
        Dim obj_Conex_int As New SqlConnection(ConfigurationManager.ConnectionStrings("CCconexion").ConnectionString)
        ac_Funciones.Eliminar(obj_Conex_int, cls_recibos.Nombre_Tabla, cls_recibos.Campo_Id & "=" & var_id)
        Return True
    End Function
    Public Shared Function Consulta(Optional ByVal var_filtro As String = "", Optional ByVal var_orden As String = "", Optional ByRef var_error As String = "") As DataTable
        Dim obj_Conex_int As New SqlConnection(ConfigurationManager.ConnectionStrings("CCconexion").ConnectionString)
        Dim var_consulta As String = "Select * from " & cls_recibos.Listado & "()"
        Return Abrir_Tabla(obj_Conex_int, var_consulta, var_error)
    End Function

#End Region
End Class

