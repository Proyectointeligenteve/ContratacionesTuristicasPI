Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports ac_Funciones

Public Class cls_paquetes
    Dim var_Nombre_Tabla As String = "tbl_paquetes"
    Dim var_Campo_Id As String = "id"
    Dim var_Campo_Validacion As String = "nombre"
    Dim var_Campos As String = "nombre,fecha_inicio,fecha_fin,id_destino,tipo,grupo,activo,id_usuario_reg,fecha_reg,precio"
    Dim obj_Conex_int As New SqlConnection(ConfigurationManager.ConnectionStrings("connection").ConnectionString)

    Dim var_id As Integer = 0
    Dim var_nombre As String = ""
    Dim var_fecha_inicio As Date = Now
    Dim var_fecha_fin As Date = Now
    Dim var_id_destino As Integer = 0
    Dim var_tipo As Boolean = False
    Dim var_grupo As Boolean = False
    Dim var_activo As Boolean = False
    Dim var_id_usuario_reg As Integer
    Dim var_fecha_reg As Date = Now
    Dim var_precio As Double = 0

    Public Shared ReadOnly Property Nombre_Tabla() As String
        Get
            Return "tbl_paquetes"
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

    Public Shared ReadOnly Property Formulario() As String
        Get
            Return "frm_paquetes.aspx"
        End Get
    End Property

    Public Shared ReadOnly Property ListadoActivos() As String
        Get
            Return "lst_paquetesActivos"
        End Get
    End Property

    Public Shared ReadOnly Property ListadoAnulados() As String
        Get
            Return "lst_paquetesAnulados"
        End Get
    End Property
    Public Shared ReadOnly Property Listado() As String
        Get
            Return "lst_paquetes"
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
    Public Property fecha_inicio() As Date
        Get
            Return Me.var_fecha_inicio
        End Get
        Set(ByVal value As Date)
            Me.var_fecha_inicio = value
        End Set
    End Property
    Public Property fecha_fin() As Date
        Get
            Return Me.var_fecha_fin
        End Get
        Set(ByVal value As Date)
            Me.var_fecha_fin = value
        End Set
    End Property
    Public Property id_destino() As Integer
        Get
            Return Me.var_id_destino
        End Get
        Set(ByVal value As Integer)
            Me.var_id_destino = value
        End Set
    End Property
    Public Property tipo() As Boolean
        Get
            Return Me.var_tipo
        End Get
        Set(ByVal value As Boolean)
            Me.var_tipo = value
        End Set
    End Property
    Public Property grupo() As Boolean
        Get
            Return Me.var_grupo
        End Get
        Set(ByVal value As Boolean)
            Me.var_grupo = value
        End Set
    End Property
    Public Property activo() As Boolean
        Get
            Return Me.var_activo
        End Get
        Set(ByVal value As Boolean)
            Me.var_activo = value
        End Set
    End Property
    Public Property id_usuario_reg() As Integer
        Get
            Return Me.var_id_usuario_reg
        End Get
        Set(ByVal value As Integer)
            Me.var_id_usuario_reg = value
        End Set
    End Property

    Public Property fecha_reg() As Date
        Get
            Return Me.var_fecha_reg
        End Get
        Set(ByVal value As Date)
            Me.var_fecha_reg = value
        End Set
    End Property

    Public Property precio() As Double
        Get
            Return Me.var_precio
        End Get
        Set(ByVal value As Double)
            Me.var_precio = value
        End Set
    End Property
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
            Me.var_nombre = ac_Funciones.formato_Texto(obj_dt_int.Rows(0).Item("nombre").ToString)
            Me.var_fecha_inicio = ac_Funciones.formato_Fecha(obj_dt_int.Rows(0).Item("fecha_inicio").ToString)
            Me.var_fecha_fin = ac_Funciones.formato_Fecha(obj_dt_int.Rows(0).Item("fecha_fin").ToString)
            Me.var_id_destino = ac_Funciones.formato_Numero(obj_dt_int.Rows(0).Item("id_destino").ToString)
            Me.var_tipo = ac_Funciones.formato_boolean(obj_dt_int.Rows(0).Item("tipo").ToString)
            Me.var_grupo = ac_Funciones.formato_boolean(obj_dt_int.Rows(0).Item("grupo").ToString)
            Me.var_activo = ac_Funciones.formato_boolean(obj_dt_int.Rows(0).Item("activo").ToString)
            Me.var_id_usuario_reg = ac_Funciones.formato_Numero(obj_dt_int.Rows(0).Item("id_usuario_reg").ToString)
            Me.var_fecha_reg = ac_Funciones.formato_Fecha(obj_dt_int.Rows(0).Item("fecha_reg").ToString)
            Me.var_precio = ac_Funciones.formato_Numero(obj_dt_int.Rows(0).Item("precio").ToString, True)
        Else
            Me.var_id = 0
        End If
    End Sub

    Public Function Actualizar(ByRef var_msj As String) As Boolean
        If Validar_Existe(Me.obj_Conex_int, Me.var_Nombre_Tabla, Me.var_Campo_Validacion, Me.var_nombre, Me.var_Campo_Id, Me.var_id) Then
            If var_msj = "" Then
                var_msj = "El paquete '" & Me.var_nombre & "' ya existe en la base de datos"
            End If
            Return False
            Exit Function
        End If

        Dim var_error As String = ""
        If Me.var_id = 0 Then   'NUEVO
            If Not Ingresar(Me.obj_Conex_int, Me.var_Nombre_Tabla, Me.var_Campos, Sql_Texto(Me.var_nombre) & "," & Sql_Texto(Me.var_fecha_inicio) & "," & Sql_Texto(Me.var_fecha_fin) & "," & Sql_Texto(Me.var_id_destino) & "," & Sql_Texto(Me.var_tipo) & "," & Sql_Texto(Me.var_grupo) & "," & Sql_Texto(Me.var_activo) & "," & Sql_Texto(Me.var_id_usuario_reg) & "," & Sql_Texto(Me.var_fecha_reg) & "," & Sql_Texto(Me.var_precio), var_error) Then
                Dim obj_log As New cls_logs
                obj_log.ComentarioLog = "Error agregando paquete '" & Me.var_nombre & " - " & Me.var_fecha_inicio & " - " & Me.var_fecha_fin & "': " & var_error
                obj_log.FechaLog = Now
                obj_log.id_Menu = New cls_modulos("paquetes").Id
                obj_log.id_Usuario = var_id_usuario_reg
                obj_log.idAccion = New cls_acciones("agregar").Id
                obj_log.ResultadoLog = False
                obj_log.InsertarLog()

                Return False
                Exit Function
            Else
                Me.var_id = Valor_De(Me.obj_Conex_int, "select " & Me.var_Campo_Id & " from " & Me.var_Nombre_Tabla & " where nombre=" & Sql_Texto(Me.var_nombre))

                Dim obj_sb As New StringBuilder
                obj_sb.Append(Chr(34) & "Id" & Chr(34) & ":" & Chr(34) & Me.Id & Chr(34) & "")
                obj_sb.Append("," & Chr(34) & "nombre" & Chr(34) & ":" & Chr(34) & Me.nombre & Chr(34) & "")
                obj_sb.Append("," & Chr(34) & "fecha_inicio" & Chr(34) & ":" & Chr(34) & Me.fecha_inicio & Chr(34) & "")
                obj_sb.Append("," & Chr(34) & "fecha_fin" & Chr(34) & ":" & Chr(34) & Me.fecha_fin & Chr(34) & "")
                obj_sb.Append("," & Chr(34) & "id_destino" & Chr(34) & ":" & Chr(34) & Me.id_destino & Chr(34) & "")
                obj_sb.Append("," & Chr(34) & "tipo" & Chr(34) & ":" & Chr(34) & Me.tipo & Chr(34) & "")
                obj_sb.Append("," & Chr(34) & "grupo" & Chr(34) & ":" & Chr(34) & Me.grupo & Chr(34) & "")
                obj_sb.Append("," & Chr(34) & "precio" & Chr(34) & ":" & Chr(34) & Me.precio & Chr(34) & "")
                obj_sb.Append("," & Chr(34) & "activo" & Chr(34) & ":" & Chr(34) & Me.activo & Chr(34) & "")

                Dim obj_log As New cls_logs
                obj_log.ComentarioLog = "Paquete agregado: {" & obj_sb.ToString & "}"
                obj_log.FechaLog = Now
                obj_log.id_Menu = New cls_modulos("paquetes").Id
                obj_log.id_Usuario = var_id_usuario_reg
                obj_log.idAccion = New cls_acciones("agregar").Id
                obj_log.ResultadoLog = True
                obj_log.InsertarLog()

                Return True
            End If
        ElseIf Me.var_id > 0 Then 'EDICION
            If Not ac_Funciones.Actualizar(Me.obj_Conex_int, Me.var_Nombre_Tabla, "nombre=" & Sql_Texto(Me.var_nombre) & ",fecha_inicio=" & Sql_Texto(Me.var_fecha_inicio) & ",fecha_fin=" & Sql_Texto(Me.var_fecha_fin) & ",id_destino=" & Sql_Texto(Me.var_id_destino) & ",tipo=" & Sql_Texto(Me.var_tipo) & ",grupo=" & Sql_Texto(Me.var_grupo) & ",activo=" & Sql_Texto(Me.var_activo) & ",precio=" & Sql_Texto(Me.var_precio), Me.var_Campo_Id & "=" & Me.var_id) Then
                var_msj = var_error

                Dim obj_log As New cls_logs
                obj_log.ComentarioLog = "Error editando paquete '" & Me.var_nombre & " - " & Me.var_fecha_inicio & " - " & Me.var_fecha_fin & "': " & var_error
                obj_log.FechaLog = Now
                obj_log.id_Menu = New cls_modulos("paquetes").Id
                obj_log.id_Usuario = var_id_usuario_reg
                obj_log.idAccion = New cls_acciones("editar").Id
                obj_log.ResultadoLog = False
                obj_log.InsertarLog()

                Return False
                Exit Function

            Else

                Dim obj_sb As New StringBuilder
                obj_sb.Append(Chr(34) & "Id" & Chr(34) & ":" & Chr(34) & Me.Id & Chr(34) & "")
                obj_sb.Append("," & Chr(34) & "nombre" & Chr(34) & ":" & Chr(34) & Me.nombre & Chr(34) & "")
                obj_sb.Append("," & Chr(34) & "fecha_inicio" & Chr(34) & ":" & Chr(34) & Me.fecha_inicio & Chr(34) & "")
                obj_sb.Append("," & Chr(34) & "fecha_fin" & Chr(34) & ":" & Chr(34) & Me.fecha_fin & Chr(34) & "")
                obj_sb.Append("," & Chr(34) & "id_destino" & Chr(34) & ":" & Chr(34) & Me.id_destino & Chr(34) & "")
                obj_sb.Append("," & Chr(34) & "tipo" & Chr(34) & ":" & Chr(34) & Me.tipo & Chr(34) & "")
                obj_sb.Append("," & Chr(34) & "grupo" & Chr(34) & ":" & Chr(34) & Me.grupo & Chr(34) & "")
                obj_sb.Append("," & Chr(34) & "precio" & Chr(34) & ":" & Chr(34) & Me.precio & Chr(34) & "")
                obj_sb.Append("," & Chr(34) & "activo" & Chr(34) & ":" & Chr(34) & Me.activo & Chr(34) & "")

                Dim obj_log As New cls_logs
                obj_log.ComentarioLog = "Paquetes editado: {" & obj_sb.ToString & "}"
                obj_log.FechaLog = Now
                obj_log.id_Menu = New cls_modulos("paquetes").Id
                obj_log.id_Usuario = var_id_usuario_reg
                obj_log.idAccion = New cls_acciones("editar").Id
                obj_log.ResultadoLog = True
                obj_log.InsertarLog()
                Return True
            End If
        Else
            var_Error = "No se encontro el Registro"
            Return False
        End If
    End Function

    Public Shared Function Anular(ByVal var_id As Integer, ByVal var_id_usuario As Integer, ByRef var_mensaje As String) As Boolean
        Dim obj_Conex_int As New SqlConnection(ConfigurationManager.ConnectionStrings("connection").ConnectionString)
        Dim var_error As String = ""
        Dim obj_paquete As New cls_paquetes(var_id)
        If Not ac_Funciones.Actualizar(obj_Conex_int, cls_paquetes.Nombre_Tabla, "anulado= case when isnull(anulado,0)=0 then '1' else '0' end, fecha_anulacion=" & Sql_Texto(Now, True) & ", id_usuario_anulacion=" & var_id_usuario, cls_paquetes.Campo_Id & "=" & var_id, var_error) Then
            var_mensaje = var_error

            Dim obj_log As New cls_logs
            obj_log.ComentarioLog = "Error anulando paquete '" & obj_paquete.Id & " - " & obj_paquete.nombre & "': " & var_error
            obj_log.FechaLog = Now
            obj_log.id_Menu = New cls_modulos("paquete").Id
            obj_log.id_Usuario = var_id_usuario
            obj_log.idAccion = New cls_acciones("anular").Id
            obj_log.ResultadoLog = False
            obj_log.InsertarLog()

            Return False
        Else

            Dim obj_log As New cls_logs
            obj_log.ComentarioLog = "paquete anulado '" & obj_paquete.Id & " - " & obj_paquete.nombre
            obj_log.FechaLog = Now
            obj_log.id_Menu = New cls_modulos("paquete").Id
            obj_log.id_Usuario = var_id_usuario
            obj_log.idAccion = New cls_acciones("anular").Id
            obj_log.ResultadoLog = True
            obj_log.InsertarLog()

            Return True
        End If
        Return True
    End Function
    Public Shared Function Eliminar(ByVal var_id As Integer, ByVal obj_usuario As cls_usuarios, ByRef var_mensaje As String) As Boolean
        Dim obj_conex As New SqlConnection(ConfigurationManager.ConnectionStrings("connection").ConnectionString)
        Dim var_error As String = ""

        If ac_Funciones.Eliminar(obj_conex, cls_paquetes.Nombre_Tabla, cls_paquetes.Campo_Id & "=" & var_id) < 0 Then
            var_mensaje = var_error
            Return False
        Else
            Return True
        End If
    End Function

    Public Shared Function Lista(Optional ByVal var_filtro As String = "") As DataTable
        Dim obj_Conex_int As New SqlConnection(ConfigurationManager.ConnectionStrings("connection").ConnectionString)
        Dim obj_dt_int As DataTable = Abrir_Tabla(obj_Conex_int, "select " & cls_paquetes.Campo_Id & " as id, " & cls_paquetes.Campo_Validacion & " as des from " & cls_paquetes.Nombre_Tabla & IIf(var_filtro <> "", " where " & var_filtro, "") & " order by " & cls_paquetes.Campo_Validacion & "")
        obj_dt_int.Rows.Add(0, "Seleccione una opción")
        Return obj_dt_int
    End Function

    Public Shared Function ConsultaActivos(ByRef var_error As String) As DataTable
        Dim obj_Conex_int As New SqlConnection(ConfigurationManager.ConnectionStrings("connection").ConnectionString)
        Dim var_consulta As String = "Select * from " & cls_paquetes.ListadoActivos & "()"
        Dim var_msj As String = ""

        Dim obj_dt_int As System.Data.DataTable = Abrir_Tabla(obj_Conex_int, var_consulta, var_msj)
        var_error = var_msj
        Return obj_dt_int
    End Function

    Public Shared Function ConsultaAnulados(ByRef var_error As String) As DataTable
        Dim obj_Conex_int As New SqlConnection(ConfigurationManager.ConnectionStrings("connection").ConnectionString)
        Dim var_consulta As String = "Select * from " & cls_paquetes.ListadoAnulados & "()"
        Dim var_msj As String = ""

        Dim obj_dt_int As System.Data.DataTable = Abrir_Tabla(obj_Conex_int, var_consulta, var_msj)
        var_error = var_msj
        Return obj_dt_int
    End Function

    Public Shared Function Consulta(ByRef var_error As String) As DataTable
        Dim obj_Conex_int As New SqlConnection(ConfigurationManager.ConnectionStrings("connection").ConnectionString)
        Dim var_consulta As String = "Select * from " & cls_paquetes.Listado & "()"
        Dim var_msj As String = ""

        Dim obj_dt_int As System.Data.DataTable = Abrir_Tabla(obj_Conex_int, var_consulta, var_msj)
        var_error = var_msj
        Return obj_dt_int
    End Function
End Class
