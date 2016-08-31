Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports ac_Funciones

Public Class cls_imagenes

#Region "VARIABLES"
    Dim var_Nombre_Tabla As String = "tbl_imagenes_vehiculos"
    Dim var_Campo_Id As String = "id"
    Dim var_Campos As String = "id_vehiculo,descripcion,ruta,id_usuario,fecha,posicion,sessionid,rutaBig,id_publicidad"
    Dim obj_Conex_int As New SqlConnection(ConfigurationManager.ConnectionStrings("connection").ConnectionString)

    Dim var_id As Integer = 0
    Dim var_id_vehiculo As Integer = 0
    Dim var_descripcion As String = ""
    Dim var_ruta As String = ""
    Dim var_id_usuario As cls_usuarios = New cls_usuarios
    Dim var_fecha As Date = Now
    Dim var_posicion As Integer = 0
    Dim var_sessionid As String = ""
    Dim var_rutaBig As String = ""
    Dim var_id_publicidad As Integer = 0
#End Region

#Region "PROPIEDADES"

    Public Shared ReadOnly Property Nombre_Tabla() As String
        Get
            Return "tbl_imagenes_vehiculos"
        End Get
    End Property

    Public Shared ReadOnly Property Campo_Id() As String
        Get
            Return "id"
        End Get
    End Property
    Public Shared ReadOnly Property Listado() As String
        Get
            Return "lst_imagenes"
        End Get
    End Property

    Public ReadOnly Property Id() As Integer
        Get
            Return Me.var_id
        End Get
    End Property

    Public Property id_vehiculo() As Integer
        Get
            Return Me.var_id_vehiculo
        End Get
        Set(ByVal value As Integer)
            Me.var_id_vehiculo = value
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

    Public Property ruta() As String
        Get
            Return Me.var_ruta
        End Get
        Set(ByVal value As String)
            Me.var_ruta = value
        End Set
    End Property

    Public Property id_usuario() As cls_usuarios
        Get
            Return Me.var_id_usuario
        End Get
        Set(ByVal value As cls_usuarios)
            Me.var_id_usuario = value
        End Set
    End Property

    Public Property fecha() As Date
        Get
            Return Me.var_fecha
        End Get
        Set(ByVal value As Date)
            Me.var_fecha = value
        End Set
    End Property

    Public Property posicion() As Integer
        Get
            Return Me.var_posicion
        End Get
        Set(ByVal value As Integer)
            Me.var_posicion = value
        End Set
    End Property

    Public Property sessionid() As String
        Get
            Return Me.var_sessionid
        End Get
        Set(ByVal value As String)
            Me.var_sessionid = value
        End Set
    End Property

    Public Property rutaBig() As String
        Get
            Return Me.var_rutaBig
        End Get
        Set(ByVal value As String)
            Me.var_rutaBig = value
        End Set
    End Property

    Public Property id_publicidad() As Integer
        Get
            Return Me.var_id_publicidad
        End Get
        Set(ByVal value As Integer)
            Me.var_id_publicidad = value
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
            Me.var_id_vehiculo = formato_Numero(obj_dt_int.Rows(0).Item("id_vehiculo").ToString)
            Me.var_descripcion = formato_Texto(obj_dt_int.Rows(0).Item("descripcion").ToString)
            Me.var_ruta = formato_Texto(obj_dt_int.Rows(0).Item("ruta").ToString)
            Me.var_id_usuario = New cls_usuarios(CInt(obj_dt_int.Rows(0).Item("id_usuario").ToString))
            Me.var_fecha = obj_dt_int.Rows(0).Item("fecha")
            Me.var_posicion = formato_Numero(obj_dt_int.Rows(0).Item("posicion").ToString)
            Me.var_sessionid = formato_Texto(obj_dt_int.Rows(0).Item("sessionid").ToString)
            Me.var_rutaBig = formato_Texto(obj_dt_int.Rows(0).Item("rutaBig").ToString)
            Me.var_id_publicidad = formato_Numero(obj_dt_int.Rows(0).Item("id_publicidad").ToString)
        Else
            Me.var_id = -1
        End If
    End Sub

    Public Shared Function Lista(Optional ByVal var_filtro As String = "") As DataTable
        Dim obj_Conex_int As New SqlConnection(ConfigurationManager.ConnectionStrings("connection").ConnectionString)
        Dim obj_dt_int As DataTable = Abrir_Tabla(obj_Conex_int, "select " & cls_imagenes.Campo_Id & " as id, " & " as des from " & cls_imagenes.Nombre_Tabla & IIf(var_filtro <> "", " where " & var_filtro, "") & " order by ")
        obj_dt_int.Rows.Add(0, "Seleccione una opción")
        Return obj_dt_int
    End Function

    Public Function Actualizar(ByRef var_Error As String) As Boolean
        Me.var_rutaBig = Me.var_ruta

        If Me.var_id = 0 Then   'NUEVO
            If Not Ingresar(Me.obj_Conex_int, Me.var_Nombre_Tabla, Me.var_Campos, Sql_Texto(Me.id_vehiculo) & "," & Sql_Texto(Me.var_descripcion) & "," & Sql_Texto(Me.var_ruta) & "," & Sql_Texto(Me.var_id_usuario.Id) & "," & Sql_Texto(Me.var_fecha) & "," & Sql_Texto(Me.var_posicion) & "," & Sql_Texto(Me.var_sessionid) & "," & Sql_Texto(Me.var_rutaBig) & "," & Sql_Texto(Me.var_id_publicidad), var_Error) Then
                Return False
                Exit Function
            End If
            Me.var_id = Valor_De(Me.obj_Conex_int, "select " & Me.var_Campo_Id & " from " & Me.var_Nombre_Tabla & " order by id desc")
            Return True
        ElseIf Me.var_id > 0 Then 'EDICION
            If Not ac_Funciones.Actualizar(Me.obj_Conex_int, Me.var_Nombre_Tabla, "id_vehiculo=" & Sql_Texto(Me.var_id_vehiculo) & ",descripcion=" & Sql_Texto(Me.var_descripcion) & ",ruta=" & Sql_Texto(Me.var_ruta) & ",id_usuario=" & Sql_Texto(Me.id_usuario.Id) & ",fecha=" & Sql_Texto(Me.var_fecha) & ",posicion=" & Sql_Texto(Me.var_posicion) & ",sessionid=" & Sql_Texto(Me.var_sessionid) & ",rutaBig=" & Sql_Texto(Me.var_rutaBig) & ",id_publicidad=" & Sql_Texto(Me.var_id_publicidad), Me.var_Campo_Id & "=" & Me.var_id, var_Error) Then
                Return False
                Exit Function
            End If
            Return True
        Else
            var_Error = "No se encontro el Registro"
            Return False
        End If
    End Function

    Public Shared Function Eliminar(ByVal var_imagen As String, ByVal obj_usuario As cls_usuarios, ByRef var_mensaje As String) As Boolean
        Dim obj_Conex_int As New SqlConnection(ConfigurationManager.ConnectionStrings("connection").ConnectionString)
        ac_Funciones.Eliminar(obj_Conex_int, cls_imagenes.Nombre_Tabla, "ruta=" & Sql_Texto(var_imagen))
        Return True
    End Function

    Public Shared Function EliminarSessionId(ByVal var_sessionid As String, ByVal obj_usuario As cls_usuarios, ByRef var_mensaje As String) As Boolean
        Dim obj_Conex_int As New SqlConnection(ConfigurationManager.ConnectionStrings("connection").ConnectionString)
        ac_Funciones.Eliminar(obj_Conex_int, cls_imagenes.Nombre_Tabla, "sessionid=" & Sql_Texto(var_sessionid))
        Return True
    End Function

    Public Shared Function EliminarSession(ByVal obj_usuario As cls_usuarios, ByRef var_mensaje As String, Optional ByRef obj_dt As System.Data.DataTable = Nothing) As Boolean
        Dim obj_Conex_int As New SqlConnection(ConfigurationManager.ConnectionStrings("connection").ConnectionString)

        obj_dt = ac_Funciones.Abrir_Tabla(obj_Conex_int, "select * from " & cls_imagenes.Nombre_Tabla & " where len(ltrim(rtrim(sessionid)))>0")
        ac_Funciones.Eliminar(obj_Conex_int, cls_imagenes.Nombre_Tabla, "len(ltrim(rtrim(sessionid)))>0")
        Return True
    End Function

    Public Shared Function Consulta(Optional ByVal var_filtro As String = "", Optional ByVal var_orden As String = "", Optional ByRef var_error As String = "") As DataTable
        Dim obj_Conex_int As New SqlConnection(ConfigurationManager.ConnectionStrings("connection").ConnectionString)
        Dim var_consulta As String = "Select * from " & cls_imagenes.Listado & "()"
        Return Abrir_Tabla(obj_Conex_int, var_consulta, var_error)
    End Function

    Public Shared Function actualizarPosicion(ByVal var_id As Integer, ByVal var_posicion As Integer, Optional ByRef var_msj As String = "") As Boolean
        Dim obj_Conex_int As New SqlConnection(ConfigurationManager.ConnectionStrings("connection").ConnectionString)
        Dim var_error As String = ""
        If Not ac_Funciones.Actualizar(obj_Conex_int, cls_imagenes.Nombre_Tabla, "posicion=" & Sql_Texto(var_posicion), "id=" & var_id, var_error) Then
            var_msj = var_error
            Return False
        Else
            Return True
        End If
    End Function

    Public Shared Function NuevaPosicion(ByVal var_id As Integer, ByVal var_sesion As String) As Integer
        Dim obj_Conex_int As New SqlConnection(ConfigurationManager.ConnectionStrings("connection").ConnectionString)
        Return Val(ac_Funciones.Valor_De(obj_Conex_int, "select isnull(max(posicion),0) as pos from tbl_imagenes_vehiculos where id=" & Sql_Texto(var_id) & " or sessionid=" & Sql_Texto(var_sesion)).ToString)
    End Function

#End Region

End Class
