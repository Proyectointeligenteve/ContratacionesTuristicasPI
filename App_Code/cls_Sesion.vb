Imports System
Imports System.Data
Imports System.Data.SqlClient

Public Class cls_Sesion
    Private obj_usuario As cls_usuarios = New cls_usuarios
    Dim obj_Conex_int As New SqlConnection(ConfigurationManager.ConnectionStrings("CCconexion").ConnectionString)
    Dim var_moneda As Integer = 1

    Property Usuario() As cls_usuarios
        Get
            Return Me.obj_usuario
        End Get
        Set(ByVal value As cls_usuarios)
            Me.obj_usuario = value
        End Set
    End Property

    Property id_moneda() As Integer
        Get
            Return Me.var_moneda
        End Get
        Set(ByVal value As Integer)
            Me.var_moneda = value
        End Set
    End Property

    Public ReadOnly Property Conexion() As SqlConnection
        Get
            Return Me.obj_Conex_int
        End Get
    End Property

End Class
