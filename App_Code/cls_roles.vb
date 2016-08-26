Imports System
Imports System.Data
Imports System.Data.SqlClient

Public Class cls_Roles

#Region "VARIABLES GLOBALES"
    Dim var_Nombre_Tabla As String = "tbl_roles"
    Dim var_Campo_Id As String = "id"
    Dim var_Campo_Validacion As String = "nombre"
    'Dim var_Campos As String = "nombre,descripcion, id_usuario_reg, fecha_modificacion"
    Dim obj_Conex_int As New SqlConnection(ConfigurationManager.ConnectionStrings("connection").ConnectionString)

    Dim var_id_Rol As Integer
    Dim var_nombre_Rol As String = ""
    Dim var_descripcion_Rol As String = ""
    Dim var_id_usuario As Integer = 0
    Dim var_fecha_modificacion As Date = Now.Date
#End Region

#Region "PROPIEDADES"

    Public Shared ReadOnly Property Nombre_Tabla() As String
        Get
            Return "tbl_Roles"
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

    Public Shared ReadOnly Property Formulario() As String
        Get
            Return "frm_Roles.aspx"
        End Get
    End Property

    Public ReadOnly Property Id() As Integer
        Get
            Return Me.var_id_Rol
        End Get
    End Property

    Public Property Nombre() As String
        Get
            Return Me.var_nombre_Rol
        End Get
        Set(ByVal value As String)
            Me.var_nombre_Rol = value
        End Set
    End Property

    Public Property Descripcion() As String
        Get
            Return Me.var_descripcion_Rol
        End Get
        Set(ByVal value As String)
            Me.var_descripcion_Rol = value
        End Set
    End Property

    Public Property Usuario() As String
        Get
            Return Me.var_id_usuario
        End Get
        Set(ByVal value As String)
            Me.var_id_usuario = value
        End Set
    End Property

    Public ReadOnly Property FechaModificacion() As Date
        Get
            Return Me.var_fecha_modificacion
        End Get
    End Property
#End Region

#Region "FUNCIONES"

    Sub New(ByVal obj_conex As SqlConnection, Optional ByVal var_Id_int As Integer = 0)
        obj_Conex_int = obj_conex
        If var_Id_int > 0 Then
            Cargar(var_Id_int)
        End If
    End Sub

    Sub New(ByVal var_nombre As String)
        If var_nombre.Trim.Length > 0 Then
            Cargar(var_nombre)
        End If
    End Sub

    Public Sub Cargar(ByVal var_id_int As Integer)
        Dim obj_dt_int As New DataTable
        obj_dt_int = ac_Funciones.Abrir_Tabla(Me.obj_Conex_int, "Select * from " & Me.var_Nombre_Tabla & " WHERE " & Me.var_Campo_Id & "=" & var_id_int)
        If obj_dt_int.Rows.Count > 0 Then
            Me.var_id_Rol = obj_dt_int.Rows(0).Item("id").ToString
            Me.var_nombre_Rol = obj_dt_int.Rows(0).Item("nombre").ToString
            Me.var_descripcion_Rol = obj_dt_int.Rows(0).Item("descripcion").ToString
            'Me.var_id_usuario = ac_Funciones.formato_Numero(obj_dt_int.Rows(0).Item("id_usuario").ToString)
            'Me.var_fecha_modificacion = ac_Funciones.formato_Fecha(obj_dt_int.Rows(0).Item("fecha_modificacion").ToString)
        Else
            Me.var_id_Rol = -1
        End If
    End Sub

    Public Sub Cargar(ByVal var_nombre_int As String)
        Dim obj_dt_int As New DataTable
        obj_dt_int = ac_Funciones.Abrir_Tabla(Me.obj_Conex_int, "Select * from " & Me.var_Nombre_Tabla & " WHERE nombre like " & ac_Funciones.Sql_Texto(var_nombre_int))
        If obj_dt_int.Rows.Count > 0 Then
            Me.var_id_Rol = obj_dt_int.Rows(0).Item("id").ToString
            Me.var_nombre_Rol = obj_dt_int.Rows(0).Item("nombre").ToString
            Me.var_descripcion_Rol = obj_dt_int.Rows(0).Item("descripcion").ToString
            'Me.var_id_usuario = ac_Funciones.formato_Numero(obj_dt_int.Rows(0).Item("id_usuario").ToString)
            'Me.var_fecha_modificacion = ac_Funciones.formato_Fecha(obj_dt_int.Rows(0).Item("fecha_modificacion").ToString)
        Else
            Me.var_id_Rol = -1
        End If
    End Sub

    Public Function Actualizar(ByRef var_Error As String) As Boolean
        If ac_Funciones.Validar_Existe(Me.obj_Conex_int, Me.var_Nombre_Tabla, Me.var_Campo_Validacion, Me.var_nombre_Rol.ToUpper, Me.var_Campo_Id, Me.var_id_Rol) Then
            var_Error = "El nombre '" & Me.var_nombre_Rol & "' ya existe en la base de datos"
            Return False
        End If

        If Me.var_id_Rol = 0 Then   'NUEVO
            'If New cls_permisos(var_id_usuario, cls_Roles.IdMenu, 2).permiso Then
            '    Dim obj_log As New cls_logs
            '    obj_log.idAccion = 2
            '    obj_log.ComentarioLog = ""
            '    obj_log.id_Menu = cls_Roles.IdMenu
            '    obj_log.FechaLog = Now
            '    obj_log.ResultadoLog = True
            '    obj_log.id_Usuario = Me.var_id_usuario
            '    obj_log.InsertarLog("")

            'If Not ac_Funciones.Ingresar(Me.obj_Conex_int, Me.var_Nombre_Tabla, Me.var_Campos, ac_funciones.Sql_Texto(Me.var_nombre_Rol) & "," & ac_funciones.Sql_Texto(Me.var_descripcion_Rol) & "," & ac_funciones.Sql_Texto(Me.var_id_usuario) & "," & ac_funciones.Sql_Texto(Me.var_fecha_modificacion), var_Error) Then
            'obj_log = New cls_logs
            'obj_log.idAccion = 2
            'obj_log.ComentarioLog = var_Error
            'obj_log.id_Menu = cls_Roles.IdMenu
            'obj_log.FechaLog = Now
            'obj_log.ResultadoLog = False
            'obj_log.id_Usuario = Me.var_id_usuario
            'obj_log.InsertarLog("")
            Return False
            'End If

            Me.var_id_Rol = ac_Funciones.Valor_De(Me.obj_Conex_int, "select " & Me.var_Campo_Id & " from " & Me.var_Nombre_Tabla & " where id_usuario=" & Me.var_id_usuario & " order by " & Me.var_Campo_Id & " DESC")
            Return True
            'Else

            '    Dim obj_log As New cls_logs
            '    obj_log.idAccion = 2
            '    obj_log.ComentarioLog = "No tiene permiso para realizar esta acción"
            '    obj_log.id_Menu = cls_Roles.IdMenu
            '    obj_log.FechaLog = Now
            '    obj_log.ResultadoLog = False
            '    obj_log.id_Usuario = Me.var_id_usuario
            '    obj_log.InsertarLog("")
            '    var_Error = "No tiene permiso para realizar esta acción"
            '    Return False
            'End If
        ElseIf Me.var_id_Rol > 0 Then 'EDICION
            'If New cls_permisos(var_id_usuario, cls_Roles.IdMenu, 2).permiso Then
            '    Dim obj_log As New cls_logs
            '    obj_log.idAccion = 3
            '    obj_log.ComentarioLog = ""
            '    obj_log.id_Menu = cls_Roles.IdMenu
            '    obj_log.FechaLog = Now
            '    obj_log.ResultadoLog = True
            '    obj_log.id_Usuario = Me.var_id_usuario
            '    obj_log.InsertarLog("")

            'If Not ac_Funciones.Actualizar(Me.obj_Conex_int, Me.var_Nombre_Tabla, "nombre_Rol=" & ac_Funciones.Sql_Texto(Me.var_nombre_Rol) & ", descripcion_Rol=" & ac_Funciones.Sql_Texto(Me.var_descripcion_Rol) & ", id_usuario=" & ac_Funciones.Sql_Texto(var_id_usuario) & ", fecha_modificacion=" & ac_Funciones.Sql_Texto(var_fecha_modificacion), Me.var_Campo_Id & "=" & Me.var_id_Rol, var_Error) Then
            'obj_log = New cls_logs
            'obj_log.idAccion = 3
            'obj_log.ComentarioLog = var_Error
            'obj_log.id_Menu = cls_Roles.IdMenu
            'obj_log.FechaLog = Now
            'obj_log.ResultadoLog = False
            'obj_log.id_Usuario = Me.var_id_usuario
            'obj_log.InsertarLog("")
            Return False
            'End If

            Return True
            'Else

            '    Dim obj_log As New cls_logs
            '    obj_log.idAccion = 3
            '    obj_log.ComentarioLog = "No tiene permiso para realizar esta acción"
            '    obj_log.id_Menu = cls_Roles.IdMenu
            '    obj_log.FechaLog = Now
            '    obj_log.ResultadoLog = False
            '    obj_log.id_Usuario = Me.var_id_usuario
            '    obj_log.InsertarLog("")
            '    var_Error = "No tiene permiso para realizar esta acción"
            '    Return False
            'End If
        Else
            var_Error = "No se encontro el Registro"
            Return False
        End If
    End Function

    'Public Shared Sub Eliminar(ByVal obj_Conex As SqlConnection, ByVal var_id As Integer, ByVal var_id_usuario As Integer, ByRef var_error As String)
    '    If New cls_permisos(var_id_usuario, cls_Roles.IdMenu, 4).permiso Then
    '        Dim obj_log As New cls_logs
    '        obj_log.idAccion = 4
    '        obj_log.ComentarioLog = ""
    '        obj_log.id_Menu = cls_Roles.IdMenu
    '        obj_log.FechaLog = Now
    '        obj_log.ResultadoLog = True
    '        obj_log.id_Usuario = var_id_usuario
    '        obj_log.InsertarLog("")

    '        If Not ac_funciones.Eliminar(obj_Conex, cls_Roles.Nombre_Tabla, cls_Roles.Campo_Id & "=" & var_id, var_error) Then
    '            obj_log = New cls_logs
    '            obj_log.idAccion = 4
    '            obj_log.ComentarioLog = var_error
    '            obj_log.id_Menu = cls_Roles.IdMenu
    '            obj_log.FechaLog = Now
    '            obj_log.ResultadoLog = False
    '            obj_log.id_Usuario = var_id_usuario
    '            obj_log.InsertarLog("")
    '        End If
    '        ac_funciones.Eliminar(obj_Conex, "tbl_Polizas", cls_Roles.Campo_Id & "=" & var_id)
    '        ac_funciones.Eliminar(obj_Conex, "tbl_Aseguradoras_Riesgos", cls_Roles.Campo_Id & "=" & var_id)
    '    Else
    '        Dim obj_log As New cls_logs
    '        obj_log.idAccion = 4
    '        obj_log.ComentarioLog = "No tiene permiso para realizar esta acción"
    '        obj_log.id_Menu = cls_Roles.IdMenu
    '        obj_log.FechaLog = Now
    '        obj_log.ResultadoLog = False
    '        obj_log.id_Usuario = var_id_usuario
    '        obj_log.InsertarLog("")
    '        var_error = "No tiene permiso para realizar esta acción"
    '    End If

    'End Sub

    Public Shared Function Lista(Optional ByVal var_filtro As String = "") As DataTable
        Dim obj_Conex_int As New SqlConnection(ConfigurationManager.ConnectionStrings("connection").ConnectionString)
        Dim obj_dt_int As DataTable = ac_Funciones.Abrir_Tabla(obj_Conex_int, "select " & cls_Roles.Campo_Id & " as id, " & cls_Roles.Campo_Validacion & " as des from " & cls_Roles.Nombre_Tabla & IIf(var_filtro <> "", " where " & var_filtro, "") & " order by " & cls_Roles.Campo_Id & "")
        obj_dt_int.Rows.Add(0, "Seleccione una opción")
        Return obj_dt_int
    End Function

    'Public Shared Function Consulta(Optional ByVal var_filtro As String = "", Optional ByVal var_orden As String = "", Optional ByRef var_error As String = "", Optional ByRef var_cantidad As Integer = 0, Optional ByVal var_filtro_add As String = "") As DataTable
    '    Dim obj_conex As New SqlConnection(ConfigurationManager.ConnectionStrings("Conexion").ConnectionString)
    '    Dim var_consulta As String = "SELECT id_Rol, nombre_Rol, descripcion_Rol FROM tbl_Roles"
    '    If var_filtro.Trim <> "" Then
    '        var_consulta &= " where " & IIf(var_filtro_add.Trim.Length > 0, var_filtro_add & " and ", "")
    '        var_consulta &= "(upper(nombre_Rol) like '%" & var_filtro.Trim.ToUpper & "%')"
    '    Else
    '        If var_filtro_add.Trim.Length > 0 Then
    '            var_consulta &= " where " & var_filtro_add
    '        End If
    '    End If
    '    If var_orden <> "" Then
    '        var_consulta &= " order by " & var_orden.Trim
    '    End If

    '    Dim obj_dt As DataTable = ac_funciones.Abrir_Tabla(obj_conex, var_consulta, var_error)
    '    Try
    '        var_cantidad = obj_dt.Rows.Count
    '    Catch ex As Exception
    '        var_cantidad = 0
    '    End Try
    '    Return obj_dt
    'End Function
#End Region

End Class
