Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports ac_Funciones

Public Class cls_vehiculos_agencias
    Dim var_Nombre_Tabla As String = "tbl_vehiculos_agencias"
    Dim var_Campo_Id As String = "id"
    Dim var_Campo_Validacion As String = "nombre"
    Dim var_Campos As String = "nombre"
    Dim obj_Conex_int As New SqlConnection(ConfigurationManager.ConnectionStrings("connection").ConnectionString)

    Dim var_id As Integer = 0
    Dim var_nombre As String = ""

    Public Shared ReadOnly Property Nombre_Tabla() As String
        Get
            Return "tbl_vehiculos_agencias"
        End Get
    End Property

    Public Shared ReadOnly Property Menu() As Integer
        Get
            Return 1
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
    'Propiedad obligatoria para verficar que el valor de la tabla en la base de datos exista o no
    Sub New(Optional ByVal var_Id_int As Integer = 0)

        If var_Id_int > 0 Then
            Cargar(var_Id_int)
        End If

    End Sub

    'agregada la linea de id_pais
    Public Sub Cargar(ByVal var_id_int As Integer)
        Dim obj_dt_int As New DataTable
        obj_dt_int = Abrir_Tabla(Me.obj_Conex_int, "Select * from " & Me.var_Nombre_Tabla & " WHERE " & Me.var_Campo_Id & "=" & var_id_int)
        If obj_dt_int.Rows.Count > 0 Then
            Me.var_id = ac_Funciones.formato_Numero(obj_dt_int.Rows(0).Item("id").ToString)
            Me.var_nombre = ac_Funciones.formato_Texto(obj_dt_int.Rows(0).Item("nombre").ToString)
        Else
            Me.var_id = 0
        End If
    End Sub

    Public Function Actualizar(ByRef var_Error As String) As Boolean
        If Validar_Existe(Me.obj_Conex_int, Me.var_Nombre_Tabla, Me.var_Campo_Validacion, Me.var_nombre, Me.var_Campo_Id, Me.var_id) Then
            If var_Error = "" Then
                var_Error = "El Estado '" & Me.var_nombre & "' ya existe en la base de datos"
            End If
            Return False
            Exit Function
        End If

        If Me.var_id = 0 Then   'NUEVO
            If Not Ingresar(Me.obj_Conex_int, Me.var_Nombre_Tabla, Me.var_Campos, Sql_Texto(Me.var_nombre), var_Error) Then
                Return False
                Exit Function
            End If
            Me.var_id = Valor_De(Me.obj_Conex_int, "select " & Me.var_Campo_Id & " from " & Me.var_Nombre_Tabla & " order by id ")
            Return True
        ElseIf Me.var_id > 0 Then 'EDICION
            If Not ac_Funciones.Actualizar(Me.obj_Conex_int, Me.var_Nombre_Tabla, "nombre=" & Sql_Texto(Me.var_nombre), Me.var_Campo_Id & "=" & Me.var_id) Then
                Return False
                Exit Function
            End If
            Return True
        Else
            var_Error = "No se encontro el Registro"
            Return False
        End If
    End Function

    Public Shared Function Lista(Optional ByVal var_filtro As String = "") As DataTable
        Dim obj_Conex_int As New SqlConnection(ConfigurationManager.ConnectionStrings("connection").ConnectionString)
        Dim obj_dt_int As DataTable = Abrir_Tabla(obj_Conex_int, "select " & cls_vehiculos_agencias.Campo_Id & " as id, " & cls_vehiculos_agencias.Campo_Validacion & " as des from " & cls_vehiculos_agencias.Nombre_Tabla & IIf(var_filtro <> "", " where " & var_filtro, "") & " order by " & cls_vehiculos_agencias.Campo_Validacion & "")
        obj_dt_int.Rows.Add(0, "Seleccione una opción")
        Return obj_dt_int
    End Function

End Class
