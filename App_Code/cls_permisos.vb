Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports ac_Funciones

Public Class cls_permisos

    Dim var_Nombre_Tabla As String = "tbl_permisos"
    Dim obj_Conex_int As New SqlConnection(ConfigurationManager.ConnectionStrings("connection").ConnectionString)

    Dim var_id_permiso As Integer = 0
    Dim var_id_modulo As Integer = 0
    Dim var_id_accion As Integer = 0
    Dim var_id_rol As Integer = 0
    Dim var_permiso As Boolean = False

    Public ReadOnly Property Id() As Integer
        Get
            Return Me.var_id_permiso
        End Get
    End Property

    Public ReadOnly Property idmenu() As Integer
        Get
            Return Me.var_id_modulo
        End Get
    End Property

    Public ReadOnly Property idaccion() As Integer
        Get
            Return Me.var_id_accion
        End Get
    End Property

    Public ReadOnly Property idrol() As Integer
        Get
            Return Me.var_id_rol
        End Get
    End Property

    Public ReadOnly Property permiso() As Boolean
        Get
            Return Me.var_permiso
        End Get
    End Property


#Region "FUNCIONES"

    Sub New(ByVal var_user_int As Integer, ByVal var_menu_int As Integer, ByVal var_action_int As Integer)

        If var_user_int > 0 AndAlso var_menu_int > 0 AndAlso var_action_int > 0 Then
            Cargar(var_user_int, var_menu_int, var_action_int)
        End If

    End Sub

    Public Sub Cargar(ByVal var_user_int As Integer, ByVal var_menu_int As Integer, ByVal var_action_int As Integer)
        Dim obj_dt_int As New DataTable
        obj_dt_int = Abrir_Tabla(Me.obj_Conex_int, "Select * from " & Me.var_Nombre_Tabla & " WHERE id_rol=(select id_rol from tbl_usuarios where id=" & var_user_int & ") and id_modulo=" & var_menu_int & " and id_accion=" & var_action_int)
        If obj_dt_int.Rows.Count > 0 Then
            Me.var_id_permiso = ac_Funciones.formato_Numero(obj_dt_int.Rows(0).Item("id").ToString)
            Me.var_id_modulo = ac_Funciones.formato_Numero(obj_dt_int.Rows(0).Item("id_modulo").ToString)
            Me.var_id_accion = ac_Funciones.formato_Numero(obj_dt_int.Rows(0).Item("id_accion").ToString)
            Me.var_id_rol = ac_Funciones.formato_Numero(obj_dt_int.Rows(0).Item("id_rol").ToString)
            Me.var_permiso = ac_Funciones.formato_boolean(obj_dt_int.Rows(0).Item("permiso").ToString)
        Else
            Me.var_id_permiso = -1
        End If
    End Sub

#End Region

End Class
