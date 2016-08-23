Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports ac_Funciones

Public Class cls_envios_paquetes
#Region "VARIABLES"
    Dim var_Nombre_Tabla As String = "tbl_envios_paquetes"
    Dim var_Campo_Id As String = "id"
    Dim var_Campo_Validacion As String = "descripcion"
    Dim var_Campos As String = "id_envio,descripcion,peso,volumen,costo"
    Dim obj_Conex_int As New SqlConnection(ConfigurationManager.ConnectionStrings("CCconexion").ConnectionString)

    Dim var_id As Integer = 0
    Dim var_id_envio As Integer = 0
    Dim var_numero As Integer = 0
    Dim var_peso As String = ""
    Dim var_volumen As String = ""
    Dim var_costo As Double = 0
    Dim var_descripcion As String = ""
#End Region
#Region "PROPIEDADES"
    Public Shared ReadOnly Property Nombre_Tabla() As String
        Get
            Return "tbl_envios_paquetes"
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
            Return "descripcion"
        End Get
    End Property
    Public ReadOnly Property Id() As Integer
        Get
            Return Me.var_id
        End Get
    End Property
    Public Property id_envio() As Integer
        Get
            Return Me.var_id_envio
        End Get
        Set(ByVal value As Integer)
            Me.var_id_envio = value
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
    Public Property numero() As String
        Get
            Return Me.var_numero
        End Get
        Set(ByVal value As String)
            Me.var_numero = value
        End Set
    End Property
    Public Property peso() As String
        Get
            Return Me.var_peso
        End Get
        Set(ByVal value As String)
            Me.var_peso = value
        End Set
    End Property
    Public Property volumen() As String
        Get
            Return Me.var_volumen
        End Get
        Set(ByVal value As String)
            Me.var_volumen = value
        End Set
    End Property
    Public Property costo() As Double
        Get
            Return Me.var_costo
        End Get
        Set(ByVal value As Double)
            Me.var_costo = value
        End Set
    End Property
    'Public Property id_usuario_reg() As Integer
    '    Get
    '        Return Me.var_id_usuario_reg
    '    End Get
    '    Set(ByVal value As Integer)
    '        Me.var_id_usuario_reg = value
    '    End Set
    'End Property
    'Public Property fecha_reg() As Date
    '    Get
    '        Return Me.var_fecha_reg
    '    End Get
    '    Set(ByVal value As Date)
    '        Me.var_fecha_reg = value
    '    End Set
    'End Property
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
            Me.var_id = ac_Funciones.formato_Numero(obj_dt_int.Rows(0).Item("id").ToString)
            Me.var_id_envio = ac_Funciones.formato_Numero(obj_dt_int.Rows(0).Item("nombre").ToString)
            Me.var_numero = ac_Funciones.formato_Numero(obj_dt_int.Rows(0).Item("descripcion").ToString)
            Me.var_peso = ac_Funciones.formato_Numero(obj_dt_int.Rows(0).Item("peso").ToString)
            Me.var_volumen = ac_Funciones.formato_Numero(obj_dt_int.Rows(0).Item("volumen").ToString)
            Me.var_costo = ac_Funciones.formato_Numero(obj_dt_int.Rows(0).Item("costo").ToString)
            Me.var_descripcion = ac_Funciones.formato_Numero(obj_dt_int.Rows(0).Item("descripcion").ToString)
        Else
            Me.var_id = -1
        End If
    End Sub
    Public Function Actualizar(ByRef var_Error As String) As Boolean
        'If Validar_Existe(Me.obj_Conex_int, Me.var_Nombre_Tabla, Me.var_Campo_Validacion, Me.var_nombre, Me.var_Campo_Id, Me.var_id) Then
        '    If var_Error = "" Then
        '        var_Error = "El cliente '" & Me.var_nombre & "' ya existe en la base de datos"
        '    End If
        '    Return False
        '    Exit Function
        'End If
        If Me.var_id = 0 Then   'NUEVO
            If Not Ingresar(Me.obj_Conex_int, Me.var_Nombre_Tabla, Me.var_Campos, Sql_Texto(Me.var_id_envio) & "," & Sql_Texto(Me.var_numero) & "," & Sql_Texto(Me.var_peso) & "," & Sql_Texto(Me.var_volumen) & "," & Sql_Texto(Me.var_costo) & "," & Sql_Texto(Me.var_descripcion), var_Error) Then
                Return False
                Exit Function
            End If
            Me.var_id = Valor_De(Me.obj_Conex_int, "select " & Me.var_Campo_Id & " from " & Me.var_Nombre_Tabla & " where id_envio=" & Sql_Texto(Me.var_id_envio))
            Return True
        ElseIf Me.var_id > 0 Then 'EDICION
            If Not ac_Funciones.Actualizar(Me.obj_Conex_int, Me.var_Nombre_Tabla, "id_envio=" & Sql_Texto(Me.var_id_envio) & ",numero=" & Sql_Texto(Me.var_numero) & ",peso=" & Sql_Texto(Me.var_peso) & ",volumen=" & Sql_Texto(Me.var_volumen) & ",costo=" & Sql_Texto(Me.var_costo) & ",descripcion=" & Sql_Texto(Me.var_descripcion), Me.var_Campo_Id & "=" & Me.var_id) Then
                Return False
                Exit Function
            End If
            Return True
        Else
            var_Error = "No se encontro el Registro"
            Return False
        End If
    End Function
    'Public Function ListaClientes() As DataTable
    '    Dim obj_dt As New System.Data.DataTable
    '    obj_dt.Columns.Add("DT_RowId", GetType(System.Int32))
    '    obj_dt.Columns.Add("Id_envio", GetType(System.String))
    '    obj_dt.Columns.Add("Descripcion", GetType(System.String))
    '    obj_dt.Columns.Add("Peso", GetType(System.String))
    '    obj_dt.Columns.Add("Volumen", GetType(System.String))
    '    obj_dt.Columns.Add("Costo", GetType(System.String))


    '    Return obj_dt
    'End Function
    Public Function ListaPaquetes() As DataTable
        Dim obj_dt As New System.Data.DataTable
        obj_dt.Columns.Add("DT_RowId", GetType(System.Int32))
        obj_dt.Columns.Add("Id_envio", GetType(System.String))
        obj_dt.Columns.Add("Descripcion", GetType(System.String))
        obj_dt.Columns.Add("Peso", GetType(System.String))
        obj_dt.Columns.Add("Volumen", GetType(System.String))
        obj_dt.Columns.Add("Costo", GetType(System.String))


        Return obj_dt
    End Function
    Public Shared Function Eliminar(ByVal var_id As Integer, ByVal obj_usuario As cls_usuarios, ByRef var_mensaje As String) As Boolean
        Dim obj_Conex_int As New SqlConnection(ConfigurationManager.ConnectionStrings("CCconexion").ConnectionString)
        ac_Funciones.Eliminar(obj_Conex_int, cls_envios_paquetes.Nombre_Tabla, cls_envios_paquetes.Campo_Id & "=" & var_id)
        Return True
    End Function
#End Region

    Shared Function Lista() As DataTable
        Throw New NotImplementedException
    End Function

End Class
