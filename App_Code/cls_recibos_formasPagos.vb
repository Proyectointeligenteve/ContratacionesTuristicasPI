Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports ac_Funciones

Public Class cls_recibos_formasPagos
#Region "VARIABLES"
    Dim var_Nombre_Tabla As String = "tbl_recibos_formasPagos"
    Dim var_Campo_Id As String = "id"
    'Dim var_Campo_Validacion As String = "nombre"
    'Dim var_Campos As String = "monto,id_usuario_registro,fecha_registro,id_usuario_ult,fecha_ult"
    Dim var_Campos As String = "id_recibo,monto,banco,fecha,id_forma_pago"
    Dim obj_Conex_int As New SqlConnection(ConfigurationManager.ConnectionStrings("CCconexion").ConnectionString)

    Dim var_id As Integer
    Dim var_id_recibo As String = ""
    Dim var_monto As Double = 0
    Dim var_banco As Integer = 0
    Dim var_fecha As Date = Now.Date
    Dim var_id_forma_pago As cls_formas_pagos = New cls_formas_pagos
#End Region

#Region "PROPIEDADES"
    'Friend Rows As Object

    Public Shared ReadOnly Property Nombre_Tabla() As String
        Get
            Return "tbl_recibos_formasPagos"
        End Get
    End Property

    Public Shared ReadOnly Property Campo_Id() As String
        Get
            Return "id"
        End Get
    End Property

    Public Shared ReadOnly Property Campo_Validacion() As String
        Get
            Return "monto"
        End Get
    End Property
    Public Shared ReadOnly Property Listado() As String
        Get
            Return "lst_recibos_formasPagos"
        End Get
    End Property

    Public ReadOnly Property Id() As Integer
        Get
            Return Me.var_id
        End Get
    End Property

    Public Property id_recibo() As Integer
        Get
            Return Me.var_id_recibo
        End Get
        Set(ByVal value As Integer)
            Me.var_id_recibo = value
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

    Public Property banco() As Integer
        Get
            Return Me.var_banco
        End Get
        Set(ByVal value As Integer)
            Me.var_banco = value
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

    Public Property FormaPago() As cls_formas_pagos
        Get
            Return Me.var_id_forma_pago
        End Get
        Set(ByVal value As cls_formas_pagos)
            Me.var_id_forma_pago = value
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
            Me.var_id_recibo = obj_dt_int.Rows(0).Item("id_recibo")
            Me.var_monto = ac_Funciones.formato_Numero(obj_dt_int.Rows(0).Item("monto").ToString, True)
            Me.var_banco = ac_Funciones.formato_Numero(obj_dt_int.Rows(0).Item("banco").ToString)
            Me.var_fecha = ac_Funciones.formato_Fecha(obj_dt_int.Rows(0).Item("fecha").ToString)
            Me.var_id_forma_pago = New cls_formas_pagos(obj_dt_int.Rows(0).Item("id_forma_pago").ToString)

        Else
            Me.var_id = -1
        End If
    End Sub

    Public Shared Function Lista(Optional ByVal var_filtro As String = "") As DataTable
        Dim obj_Conex_int As New SqlConnection(ConfigurationManager.ConnectionStrings("CCconexion").ConnectionString)
        'Dim obj_dt_int As DataTable = Abrir_Tabla(obj_Conex_int, "select id, monto as des from tbl_tipos_acciones" & IIf(var_filtro <> "", " where " & var_filtro, "") & " order by nombre")
        Dim obj_dt_int As DataTable = Abrir_Tabla(obj_Conex_int, "select " & cls_recibos_formasPagos.Campo_Id & " as id, " & cls_recibos_formasPagos.Campo_Validacion & " as des from " & cls_recibos_formasPagos.Nombre_Tabla & IIf(var_filtro <> "", " where " & var_filtro, "") & " order by " & cls_recibos_formasPagos.Campo_Validacion & "")
        obj_dt_int.Rows.Add(0, "SELECCIONE")
        Return obj_dt_int
    End Function

    Public Function Actualizar(ByRef var_Error As String) As Boolean
        'If Validar_Existe(Me.obj_Conex_int, Me.var_Nombre_Tabla, Me.var_Campo_Validacion, Me.var_nombre, Me.var_Campo_Id, Me.var_id) Then
        '    If var_Error = "" Then
        '        var_Error = Me.var_nombre & "' ya existe en la base de datos"
        '    End If
        '    Return False
        '    Exit Function
        'End If

        If Me.var_id = 0 Then   'NUEVO
            'If Not Ingresar(Me.obj_Conex_int, Me.var_Nombre_Tabla, Me.var_Campos, Sql_Texto(Me.var_nombre) & "," & Sql_Texto(Me.var_horas) & "," & Sql_Texto(Me.var_recibos_formasPagos, True), var_Error) Then
            If Not Ingresar(Me.obj_Conex_int, Me.var_Nombre_Tabla, Me.var_Campos, Sql_Texto(Me.var_id_recibo) & "," & Sql_Texto(Me.var_monto) & "," & Sql_Texto(Me.var_banco) & "," & Sql_Texto(Me.var_fecha) & "," & Sql_Texto(Me.var_id_forma_pago.Id), var_Error) Then
                Return False
                Exit Function
            End If
            Me.var_id = Valor_De(Me.obj_Conex_int, "select " & Me.var_Campo_Id & " from " & Me.var_Nombre_Tabla & " order by id desc")
            Return True
        ElseIf Me.var_id > 0 Then 'EDICION
            'If Not ac_Funciones.Actualizar(Me.obj_Conex_int, Me.var_Nombre_Tabla, "nombre=" & Sql_Texto(Me.var_nombre) & ", horas=" & Sql_Texto(Me.var_horas) & ", turno=" & Sql_Texto(Me.var_recibos_formasPagos), Me.var_Campo_Id & "=" & Me.var_id) Then
            If Not ac_Funciones.Actualizar(Me.obj_Conex_int, Me.var_Nombre_Tabla, "id_recibo=" & Sql_Texto(Me.var_id_recibo) & ",monto=" & Sql_Texto(Me.var_monto) & ",banco=" & Sql_Texto(Me.var_banco) & ",fecha=" & Sql_Texto(Me.var_fecha) & ",id_forma_pago=" & Sql_Texto(Me.var_id_forma_pago.Id), Me.var_Campo_Id & "=" & Me.var_id) Then
                Return False
                Exit Function
            End If
            Return True
        Else
            var_Error = "No se encontro el Registro"
            Return False
        End If
    End Function

    Public Shared Function Eliminar(ByVal var_id As Integer, ByVal obj_usuario As cls_usuarios, ByRef var_mensaje As String) As Boolean
        Dim obj_Conex_int As New SqlConnection(ConfigurationManager.ConnectionStrings("CCconexion").ConnectionString)
        ac_Funciones.Eliminar(obj_Conex_int, cls_recibos_formasPagos.Nombre_Tabla, cls_recibos_formasPagos.Campo_Id & "=" & var_id)
        Return True
    End Function
    Public Shared Function Consulta(Optional ByVal var_filtro As String = "", Optional ByVal var_orden As String = "", Optional ByRef var_error As String = "") As DataTable
        Dim obj_Conex_int As New SqlConnection(ConfigurationManager.ConnectionStrings("CCconexion").ConnectionString)
        Dim var_consulta As String = "Select * from " & cls_recibos_formasPagos.Listado & "()"
        Return Abrir_Tabla(obj_Conex_int, var_consulta, var_error)
    End Function
#End Region

End Class
