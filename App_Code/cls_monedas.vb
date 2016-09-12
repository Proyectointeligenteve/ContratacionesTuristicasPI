Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports ac_Funciones

Public Class cls_monedas
#Region "VARIABLES"
    Dim var_Nombre_Tabla As String = "tbl_monedas"
    Dim var_Campo_Id As String = "id"
    Dim var_Campo_Validacion As String = "nombre"
    Dim var_Campos As String = "nombre,simbolo,principal"
    Dim obj_Conex_int As New SqlConnection(ConfigurationManager.ConnectionStrings("connection").ConnectionString)

    Dim var_id As Integer
    Dim var_nombre As String
    Dim var_simbolo As String
    Dim var_principal As Boolean
#End Region

#Region "PROPIEDADES"
    Public Shared ReadOnly Property Nombre_Tabla() As String
        Get
            Return "tbl_monedas"
        End Get
    End Property

    Public Shared ReadOnly Property Campo_Id() As String
        Get
            Return "id"
        End Get
    End Property
    Public Shared ReadOnly Property Listado() As String
        Get
            Return "lst_monedas"
        End Get
    End Property
    Public Shared ReadOnly Property Campo_Validacion() As String
        Get
            Return "nombre"
        End Get
    End Property
    Public ReadOnly Property Id() As Integer
        Get
            Return Me.var_id
        End Get
    End Property

    Public Property nombre() As String
        Get
            Return Me.var_nombre
        End Get
        Set(ByVal value As String)
            Me.var_nombre = value
        End Set
    End Property
    Public Property simbolo() As String
        Get
            Return Me.var_simbolo
        End Get
        Set(ByVal value As String)
            Me.var_simbolo = value
        End Set
    End Property
    Public Property principal() As Boolean
        Get
            Return Me.var_principal
        End Get
        Set(ByVal value As Boolean)
            Me.var_principal = value
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
            Me.var_nombre = formato_Texto(obj_dt_int.Rows(0).Item("nombre").ToString)
            Me.var_simbolo = formato_Texto(obj_dt_int.Rows(0).Item("simbolo").ToString)
            Me.var_principal = formato_boolean(obj_dt_int.Rows(0).Item("principal").ToString)
        Else
            Me.var_id = -1
        End If
    End Sub
    Public Shared Function Lista(Optional ByVal var_filtro As String = "") As DataTable
        Dim obj_Conex_int As New SqlConnection(ConfigurationManager.ConnectionStrings("connection").ConnectionString)
        Dim obj_dt_int As DataTable = Abrir_Tabla(obj_Conex_int, "select " & cls_monedas.Campo_Id & " as id, " & cls_monedas.Campo_Validacion & " as des, cast(principal as int) as principal from " & cls_monedas.Nombre_Tabla & IIf(var_filtro <> "", " where " & var_filtro, "") & " order by " & cls_monedas.Campo_Validacion & "")
        obj_dt_int.Rows.Add(0, "Seleccione una opción")
        Return obj_dt_int
    End Function

    Public Function Actualizar(ByRef var_Error As String) As Boolean
        If Validar_Existe(Me.obj_Conex_int, Me.var_Nombre_Tabla, Me.var_Campo_Validacion, Me.var_nombre, Me.var_Campo_Id, Me.var_id) Then
            If var_Error = "" Then
                var_Error = Me.var_nombre & "' ya existe en la base de datos"
            End If
            Return False
            Exit Function
        End If
        If Me.var_id = 0 Then   'NUEVO
            If Not Ingresar(Me.obj_Conex_int, Me.var_Nombre_Tabla, Me.var_Campos, Sql_Texto(Me.var_nombre) & "," & Sql_Texto(Me.var_simbolo) & "," & Sql_Texto(Me.var_principal), var_Error) Then
                Return False
                Exit Function
            End If
            Me.var_id = Valor_De(Me.obj_Conex_int, "select " & Me.var_Campo_Id & " from " & Me.var_Nombre_Tabla & "order by id desc" & Sql_Texto(Me.var_nombre))
            Return True
        ElseIf Me.var_id > 0 Then 'EDICION
            If Not ac_Funciones.Actualizar(Me.obj_Conex_int, Me.var_Nombre_Tabla, "nombre=" & Sql_Texto(Me.var_nombre) & ",simbolo=" & Sql_Texto(Me.var_simbolo) & ",principal=" & Sql_Texto(Me.var_principal), Me.var_Campo_Id & "=" & Me.var_id) Then
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
        Dim obj_Conex_int As New SqlConnection(ConfigurationManager.ConnectionStrings("connection").ConnectionString)
        ac_Funciones.Eliminar(obj_Conex_int, cls_monedas.Nombre_Tabla, cls_monedas.Campo_Id & "=" & var_id)
        Return True
    End Function
    Public Shared Function Consulta(ByVal var_orderBy As String, ByVal var_where As String, ByVal var_displayStart As Integer, ByVal var_displayEnd As Integer) As DataTable
        Dim obj_conex As New SqlConnection(ConfigurationManager.ConnectionStrings("connection").ConnectionString)
        Return Abrir_Tabla(obj_conex, "SELECT * FROM ( SELECT ROW_NUMBER() OVER (" & var_orderBy & ") AS RowNumber,* FROM ( SELECT ( SELECT COUNT(*) FROM lst_monedas() " & var_where & " ) AS TotalDisplayRows, (SELECT COUNT(*) FROM lst_monedas()) AS TotalRows,* FROM lst_monedas() " & var_where & " ) RawResults ) Results WHERE RowNumber BETWEEN " & var_displayStart & " AND " & var_displayEnd & "")
    End Function
#End Region

End Class
