Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports ac_Funciones

Public Class cls_vouchers_detalles
#Region "VARIABLES"
    Dim var_Nombre_Tabla As String = "tbl_vouchers_detalles"
    Dim var_Campo_Id As String = "id"
    ' Dim var_Campo_Validacion As String = "nombre"
    Dim var_Campos As String = "id_voucher,id_venta,nombre_pasajero"
    Dim obj_Conex_int As New SqlConnection(ConfigurationManager.ConnectionStrings("CCconexion").ConnectionString)

    Dim var_id As Integer
    Dim var_id_voucher As String = ""
    Dim var_id_venta As Integer = 0
    Dim var_nombre_pasajero As String = ""
#End Region

#Region "PROPIEDADES"
    'Friend Rows As Object

    Public Shared ReadOnly Property Nombre_Tabla() As String
        Get
            Return "tbl_vouchers_detalles"
        End Get
    End Property

    Public Shared ReadOnly Property Campo_Id() As String
        Get
            Return "id"
        End Get
    End Property

    Public Shared ReadOnly Property Campo_Validacion() As String
        Get
            Return "nombre"
        End Get
    End Property
    Public Shared ReadOnly Property Listado() As String
        Get
            Return "lst_vouchers_detalles"
        End Get
    End Property

    Public ReadOnly Property Id() As Integer
        Get
            Return Me.var_id
        End Get
    End Property

    Public Property Id_voucher() As Integer
        Get
            Return Me.var_id_voucher
        End Get
        Set(ByVal value As Integer)
            Me.var_id_voucher = value
        End Set
    End Property

    Public Property id_venta() As Integer
        Get
            Return Me.var_id_venta
        End Get
        Set(ByVal value As Integer)
            Me.var_id_venta = value
        End Set
    End Property

    Public Property nombre_pasajero() As String
        Get
            Return Me.var_nombre_pasajero
        End Get
        Set(ByVal value As String)
            Me.var_nombre_pasajero = value
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
            Me.var_id_voucher = obj_dt_int.Rows(0).Item("id_voucher")
            Me.var_id_venta = ac_Funciones.formato_Numero(obj_dt_int.Rows(0).Item("id_venta").ToString)
            Me.var_nombre_pasajero = ac_Funciones.formato_Texto(obj_dt_int.Rows(0).Item("nombre_pasajero").ToString)

        Else
            Me.var_id = -1
        End If
    End Sub

    Public Shared Function Lista(Optional ByVal var_filtro As String = "") As DataTable
        Dim obj_Conex_int As New SqlConnection(ConfigurationManager.ConnectionStrings("CCconexion").ConnectionString)
        'Dim obj_dt_int As DataTable = Abrir_Tabla(obj_Conex_int, "select id, nombre as des from tbl_tipos_acciones" & IIf(var_filtro <> "", " where " & var_filtro, "") & " order by nombre")
        Dim obj_dt_int As DataTable = Abrir_Tabla(obj_Conex_int, "select " & cls_vouchers_detalles.Campo_Id & " as id, " & cls_vouchers_detalles.Campo_Validacion & " as des from " & cls_vouchers_detalles.Nombre_Tabla & IIf(var_filtro <> "", " where " & var_filtro, "") & " order by " & cls_vouchers_detalles.Campo_Validacion & "")
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
            'If Not Ingresar(Me.obj_Conex_int, Me.var_Nombre_Tabla, Me.var_Campos, Sql_Texto(Me.var_nombre) & "," & Sql_Texto(Me.var_horas) & "," & Sql_Texto(Me.var_vouchers_detalles, True), var_Error) Then
            If Not Ingresar(Me.obj_Conex_int, Me.var_Nombre_Tabla, Me.var_Campos, Sql_Texto(Me.var_id_voucher) & "," & Sql_Texto(Me.var_id_venta) & "," & Sql_Texto(Me.var_nombre_pasajero), var_Error) Then
                Return False
                Exit Function
            End If
            Me.var_id = Valor_De(Me.obj_Conex_int, "select " & Me.var_Campo_Id & " from " & Me.var_Nombre_Tabla & " order by id desc")
            Return True
        ElseIf Me.var_id > 0 Then 'EDICION
            'If Not ac_Funciones.Actualizar(Me.obj_Conex_int, Me.var_Nombre_Tabla, "nombre=" & Sql_Texto(Me.var_nombre) & ", horas=" & Sql_Texto(Me.var_horas) & ", turno=" & Sql_Texto(Me.var_vouchers_detalles), Me.var_Campo_Id & "=" & Me.var_id) Then
            If Not ac_Funciones.Actualizar(Me.obj_Conex_int, Me.var_Nombre_Tabla, "id_voucher=" & Sql_Texto(Me.var_id_voucher) & ",id_venta=" & Sql_Texto(Me.var_id_venta) & ",nombre_pasajero=" & Sql_Texto(Me.var_nombre_pasajero), Me.var_Campo_Id & "=" & Me.var_id) Then
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
        ac_Funciones.Eliminar(obj_Conex_int, cls_vouchers_detalles.Nombre_Tabla, cls_vouchers_detalles.Campo_Id & "=" & var_id)
        Return True
    End Function
    Public Shared Function Consulta(Optional ByVal var_filtro As String = "", Optional ByVal var_orden As String = "", Optional ByRef var_error As String = "") As DataTable
        Dim obj_Conex_int As New SqlConnection(ConfigurationManager.ConnectionStrings("CCconexion").ConnectionString)
        Dim var_consulta As String = "Select * from " & cls_vouchers_detalles.Listado & "()"
        Return Abrir_Tabla(obj_Conex_int, var_consulta, var_error)
    End Function
#End Region

End Class
