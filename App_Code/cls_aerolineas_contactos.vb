Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports ac_Funciones

Public Class cls_aerolineas_contactos
#Region "VARIABLES"
    Dim var_Nombre_Tabla As String = "tbl_aerolineas_contactos"
    Dim var_Campo_Id As String = "id"
    Dim var_Campos As String = "id_aerolinea,nombre,cargo,telefono"
    Dim obj_Conex_int As New SqlConnection(ConfigurationManager.ConnectionStrings("CCconexion").ConnectionString)

    Dim var_id As Integer = 0
    Dim var_id_aerolinea As String = ""
    Dim var_nombre As Integer = 0
    Dim var_cargo As String = ""
    Dim var_telefono As String = ""

#End Region

#Region "PROPIEDADES"
    'Friend Rows As Object

    Public Shared ReadOnly Property Nombre_Tabla() As String
        Get
            Return "tbl_aerolineas_contactos"
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
    Public Property id_aerolinea() As Integer
        Get
            Return Me.var_id_aerolinea
        End Get
        Set(ByVal value As Integer)
            Me.var_id_aerolinea = value
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
    Public Property cargo() As String
        Get
            Return Me.var_cargo
        End Get
        Set(ByVal value As String)
            Me.var_cargo = value
        End Set
    End Property
    Public Property telefono() As String
        Get
            Return Me.var_telefono
        End Get
        Set(ByVal value As String)
            Me.var_telefono = value
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
            Me.var_id_aerolinea = obj_dt_int.Rows(0).Item("id_aerolinea")
            Me.var_nombre = ac_Funciones.formato_Texto(obj_dt_int.Rows(0).Item("nombre").ToString)
            Me.var_cargo = ac_Funciones.formato_Texto(obj_dt_int.Rows(0).Item("cargo").ToString)
            Me.var_telefono = ac_Funciones.formato_Texto(obj_dt_int.Rows(0).Item("telefono").ToString)
        Else
            Me.var_id = -1
        End If
    End Sub

    Public Shared Function Lista(Optional ByVal var_filtro As String = "") As DataTable
        Dim obj_Conex_int As New SqlConnection(ConfigurationManager.ConnectionStrings("CCconexion").ConnectionString)
        'Dim obj_dt_int As DataTable = Abrir_Tabla(obj_Conex_int, "select id, nombre as des from tbl_tipos_acciones" & IIf(var_filtro <> "", " where " & var_filtro, "") & " order by nombre")
        Dim obj_dt_int As DataTable = Abrir_Tabla(obj_Conex_int, "select " & cls_aerolineas_contactos.Campo_Id & " as id, " & cls_aerolineas_contactos.Campo_Validacion & " as des from " & cls_aerolineas_contactos.Nombre_Tabla & IIf(var_filtro <> "", " where " & var_filtro, "") & " order by " & cls_aerolineas_contactos.Campo_Validacion & "")
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
            'If Not Ingresar(Me.obj_Conex_int, Me.var_Nombre_Tabla, Me.var_Campos, Sql_Texto(Me.var_nombre) & "," & Sql_Texto(Me.var_horas) & "," & Sql_Texto(Me.var_aerolineas_contactos, True), var_Error) Then
            If Not Ingresar(Me.obj_Conex_int, Me.var_Nombre_Tabla, Me.var_Campos, Sql_Texto(Me.var_id_aerolinea) & "," & Sql_Texto(Me.var_nombre) & "," & Sql_Texto(Me.var_cargo) & "," & Sql_Texto(Me.var_telefono), var_Error) Then
                Return False
                Exit Function
            End If
            Me.var_id = Valor_De(Me.obj_Conex_int, "select " & Me.var_Campo_Id & " from " & Me.var_Nombre_Tabla & " order by id desc")
            Return True
        ElseIf Me.var_id > 0 Then 'EDICION
            'If Not ac_Funciones.Actualizar(Me.obj_Conex_int, Me.var_Nombre_Tabla, "nombre=" & Sql_Texto(Me.var_nombre) & ", horas=" & Sql_Texto(Me.var_horas) & ", turno=" & Sql_Texto(Me.var_aerolineas_contactos), Me.var_Campo_Id & "=" & Me.var_id) Then
            If Not ac_Funciones.Actualizar(Me.obj_Conex_int, Me.var_Nombre_Tabla, "id_aerolinea=" & Sql_Texto(Me.var_id_aerolinea) & ",nombre=" & Sql_Texto(Me.var_nombre) & ",cargo=" & Sql_Texto(Me.var_cargo) & ",telefono=" & Sql_Texto(Me.var_telefono), Me.var_Campo_Id & "=" & Me.var_id) Then
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
        ac_Funciones.Eliminar(obj_Conex_int, cls_aerolineas_contactos.Nombre_Tabla, cls_aerolineas_contactos.Campo_Id & "=" & var_id)
        Return True
    End Function
    'Public Shared Function Consulta(Optional ByVal var_filtro As String = "", Optional ByVal var_orden As String = "", Optional ByRef var_error As String = "") As DataTable
    '    Dim obj_Conex_int As New SqlConnection(ConfigurationManager.ConnectionStrings("CCconexion").ConnectionString)
    '    Dim var_consulta As String = "Select * from " & cls_aerolineas_contactos.Listado & "()"
    '    Return Abrir_Tabla(obj_Conex_int, var_consulta, var_error)
    'End Function
#End Region

End Class
