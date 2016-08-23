Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports ac_Funciones

Public Class cls_configuracion
    Dim var_Nombre_Tabla As String = "tbl_impuestos"
    Dim var_Campo_Id As String = "id"
    Dim obj_Conex_int As New SqlConnection(ConfigurationManager.ConnectionStrings("CCconexion").ConnectionString)
    Dim var_id As Integer
    Dim var_nombre As String = ""
    Dim var_interes As Double = 0
    Public Shared ReadOnly Property Email As String
        Get
            Return "andresconde85@gmail.com"
        End Get
    End Property

    Public Shared ReadOnly Property ClaveCorreo As String
        Get
            Return "TRmail@123"
        End Get
    End Property
    Public Shared ReadOnly Property Nombre_Tabla() As String
        Get
            Return "tbl_impuestos"
        End Get
    End Property

    Public Shared ReadOnly Property Campo_Id() As String
        Get
            Return "id"
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

    Public Property interes() As Double
        Get
            Return Me.var_interes
        End Get
        Set(ByVal value As Double)
            Me.var_interes = value
        End Set
    End Property

    Sub New(Optional ByVal var_Id_int As Integer = 0)

        If var_Id_int > 0 Then
            Cargar(var_Id_int)
        End If

    End Sub

    Public Sub Cargar(ByVal var_id_int As Integer)
        Dim obj_dt_int As New DataTable
        obj_dt_int = Abrir_Tabla(Me.obj_Conex_int, "Select top 1 * from " & Me.var_Nombre_Tabla & " WHERE " & Me.var_Campo_Id & "=" & var_id_int)
        If obj_dt_int.Rows.Count > 0 Then
            Me.var_id = obj_dt_int.Rows(0).Item("id").ToString
            Me.var_nombre = ac_Funciones.formato_Texto(obj_dt_int.Rows(0).Item("nombre").ToString)
            Me.var_interes = ac_Funciones.formato_Numero(obj_dt_int.Rows(0).Item("interes").ToString, True)
        End If
    End Sub

    Public Function Actualizar(ByRef var_Error As String) As Boolean

        'EDICION
        If Not ac_Funciones.Actualizar(Me.obj_Conex_int, Me.var_Nombre_Tabla, "nombre=" & Sql_Texto(Me.var_nombre) & ",interes=" & Sql_Texto(Me.var_interes), Me.var_Campo_Id & "=" & Me.var_id) Then
            Return False
            Exit Function
        End If
            Return True

            var_Error = "No se encontro el Registro"
            Return False

    End Function

    Public Function ActualizarPermisos(ByRef var_Error As String, ByVal var_habilitar As Integer) As Boolean

        If Not ac_Funciones.Actualizar(Me.obj_Conex_int, "tbl_permisos", "permiso=" & var_habilitar.ToString(), "id_accion=8 and id_rol in (2,3)") Then
            Return False
            Exit Function
        End If
        Return True

    End Function

    Public Shared Function Consulta3(ByVal id_moneda As Integer) As Integer
        Dim obj_conex As New SqlConnection(ConfigurationManager.ConnectionStrings("CCconexion").ConnectionString)
        Return Valor_De(obj_conex, "SELECT id from tbl_impuestos where id_moneda=" & Sql_Texto(id_moneda) & "")
    End Function

End Class
