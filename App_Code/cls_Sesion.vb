Imports System
Imports System.Data
Imports System.Data.SqlClient

Public Class cls_Sesion
    Private obj_usuario As cls_usuarios
    Dim obj_Conex_int As New SqlConnection(ConfigurationManager.ConnectionStrings("connection").ConnectionString)

    Property Usuario() As cls_usuarios
        Get
            Return Me.obj_usuario
        End Get
        Set(ByVal value As cls_usuarios)
            Me.obj_usuario = value
        End Set
    End Property

    Public ReadOnly Property Conexion() As SqlConnection
        Get
            Return Me.obj_Conex_int
        End Get
    End Property

End Class
