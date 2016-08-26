Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports ac_Funciones

Public Class cls_logs

    Dim var_Nombre_Tabla As String = "tbl_operaciones"
    Dim var_Campo_Id As String = "id"
    Dim var_Campos As String = "id_modulo,id_accion,id_usuario_registro,fecha_registro,comentario,resultado"
    Dim obj_Conex_int As New SqlConnection(ConfigurationManager.ConnectionStrings("connection").ConnectionString)

    Private var_id_menu As Integer = 0
    Private var_id_accion As Integer = 0
    Private var_id_usuario As Integer = 0
    Private var_fecha_log As Date = Now
    Private var_comentario_log As String = ""
    Private var_resultado_log As Boolean = False

    Public Shared ReadOnly Property Nombre_Tabla() As String
        Get
            Return "tbl_operaciones"
        End Get
    End Property

    Public Shared ReadOnly Property Formulario() As String
        Get
            Return "frm_log"
        End Get
    End Property

    Public Shared ReadOnly Property Campo_Id() As String
        Get
            Return "id"
        End Get
    End Property

    Public Shared ReadOnly Property Listado() As String
        Get
            Return "lst_logs.aspx"
        End Get
    End Property

    Public Shared ReadOnly Property idMenu() As Integer
        Get
            Return 46
        End Get
    End Property

    Public Property id_Menu() As Integer
        Get
            Return Me.var_id_menu
        End Get
        Set(ByVal value As Integer)
            Me.var_id_menu = value
        End Set
    End Property

    Public Property idAccion() As Integer
        Get
            Return Me.var_id_accion
        End Get
        Set(ByVal value As Integer)
            Me.var_id_accion = value
        End Set
    End Property

    Public Property id_Usuario() As Integer
        Get
            Return Me.var_id_usuario
        End Get
        Set(ByVal value As Integer)
            Me.var_id_usuario = value
        End Set
    End Property

    Public Property FechaLog() As Date
        Get
            Return Me.var_fecha_log
        End Get
        Set(ByVal value As Date)
            Me.var_fecha_log = value
        End Set
    End Property

    Public Property ComentarioLog() As String
        Get
            Return Me.var_comentario_log
        End Get
        Set(ByVal value As String)
            Me.var_comentario_log = value
        End Set
    End Property

    Public Property ResultadoLog() As Boolean
        Get
            Return Me.var_resultado_log
        End Get
        Set(ByVal value As Boolean)
            Me.var_resultado_log = value
        End Set
    End Property


#Region "FUNCIONES"

    Sub New()
    End Sub

    Public Function InsertarLog() As Boolean
        Dim var_Select As String = "INSERT INTO " & Me.var_Nombre_Tabla & " (" & var_Campos & ") VALUES (" & Sql_Texto(Me.var_id_menu) & "," & Sql_Texto(Me.var_id_accion) & "," & Sql_Texto(Me.var_id_usuario) & "," & Sql_Texto(Me.var_fecha_log, True) & "," & Sql_Texto(Me.var_comentario_log) & "," & Sql_Texto(Me.var_resultado_log) & ")"
        Dim obj_cmd As New SqlCommand(var_Select, Me.obj_Conex_int)
        If Me.obj_Conex_int.State <> ConnectionState.Open Then Me.obj_Conex_int.Open()
        obj_cmd.ExecuteNonQuery()
        Return True
    End Function

    Public Shared Function Consulta(Optional ByVal var_filtro As String = "", Optional ByVal var_orden As String = "", Optional ByRef var_error As String = "", Optional ByRef var_cantidad As Integer = 0) As DataTable
        Dim obj_conex As New SqlConnection(ConfigurationManager.ConnectionStrings("Conexion").ConnectionString)
        Dim var_consulta As String = "select l.id, m.tx_nombre as Modulo, a.nombre_accion as Accion, isnull(u.nombre_usuario,'') as nombre_usuario, l.fecha_log, l.comentario_log, l.resultado_log as Acceso from tbl_operaciones as l inner join tbl_menu_items as m on m.id_menu_items=l.id_menu inner join tbl_acciones as a on a.id_accion=l.id_accion left join tbl_usuarios as u on u.id_usuario=l.id_usuario"
        If var_filtro.Trim <> "" Then
            var_consulta &= " where (upper(m.tx_nombre) like '%" & formato_Texto(var_filtro.Trim.ToUpper) & "%'"
            var_consulta &= " or upper(a.nombre_accion) like '%" & formato_Texto(var_filtro.Trim.ToUpper) & "%'"
            var_consulta &= " or upper(isnull(u.nombre_usuario,'')) like '%" & formato_Texto(var_filtro.Trim.ToUpper) & "%'"
            var_consulta &= " or upper(l.comentario_log) like '%" & formato_Texto(var_filtro.Trim.ToUpper) & "%'"
            var_consulta &= " or upper(convert(varchar(20),l.fecha_log,103)) like '%" & formato_Texto(var_filtro.Trim.ToUpper) & "%'"
            var_consulta &= " or upper(l.resultado_log) like '%" & formato_Texto(var_filtro.Trim.ToUpper) & "%')"
        End If

        If var_orden <> "" Then
            var_consulta &= " order by " & var_orden.Trim
        Else
            var_consulta &= " order by l.fecha_log desc, m.id_menu_items"
        End If

        Dim obj_dt_int As DataTable = Abrir_Tabla(obj_conex, var_consulta, var_error)
        var_cantidad = obj_dt_int.Rows.Count
        Return obj_dt_int
    End Function

#End Region

End Class
