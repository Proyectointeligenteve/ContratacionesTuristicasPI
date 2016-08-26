Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports ac_Funciones

Public Class cls_cargas
    Dim var_Nombre_Tabla As String = "tbl_clases"
    Dim var_Campo_Id As String = "id"
    'Dim var_Campo_Validacion As String = "registros_fallidos"
    Dim var_Campos As String = "fecha,registros_procesados,registros_fallidos,observacion,tipo,id_usuario_reg,fecha_reg"
    Dim obj_Conex_int As New SqlConnection(ConfigurationManager.ConnectionStrings("connection").ConnectionString)

    Dim var_id As Integer = 0
    Dim var_fecha As Date = Now.Date
    Dim var_registros_procesados As Integer = 0
    Dim var_registros_fallidos As Integer = 0
    Dim var_observacion As String = ""
    Dim var_tipo As Integer = 0
    Dim var_id_usuario_reg As Integer = 0
    Dim var_fecha_reg As Date = Now

    Public Shared ReadOnly Property Nombre_Tabla() As String
        Get
            Return "tbl_cargas"
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
            Return "registros_fallidos"
        End Get
    End Property

    Public Shared ReadOnly Property Formulario() As String
        Get
            Return "frm_cargas.aspx"
        End Get
    End Property

    Public Shared ReadOnly Property ListadoActivos() As String
        Get
            Return "lst_cargasActivos"
        End Get
    End Property

    Public Shared ReadOnly Property ListadoAnulados() As String
        Get
            Return "lst_cargasAnulados"
        End Get
    End Property
    Public Shared ReadOnly Property Listado() As String
        Get
            Return "lst_cargas"
        End Get
    End Property

    Public ReadOnly Property Id() As Integer
        Get
            Return Me.var_id
        End Get
    End Property
    Public Property fecha() As Date
        Get
            Return Me.var_fecha
        End Get
        Set(ByVal value As Date)
            Me.var_fecha = value
        End Set
    End Property
    Public Property registros_procesados() As Integer
        Get
            Return Me.var_registros_procesados
        End Get
        Set(ByVal value As Integer)
            Me.var_registros_procesados = value
        End Set
    End Property
    Public Property registros_fallidos() As Integer
        Get
            Return Me.var_registros_fallidos
        End Get
        Set(ByVal value As Integer)
            Me.var_registros_fallidos = value
        End Set
    End Property
    Public Property observacion() As String
        Get
            Return Me.var_observacion
        End Get
        Set(ByVal value As String)
            Me.var_observacion = value
        End Set
    End Property
    Public Property tipo() As Integer
        Get
            Return Me.var_tipo
        End Get
        Set(ByVal value As Integer)
            Me.var_tipo = value
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
    Sub New(Optional ByVal var_Id_int As Integer = 0)

        If var_Id_int > 0 Then
            Cargar(var_Id_int)
        End If

    End Sub

    'Sub New(ByVal var_observacion As String)
    '    Cargar(var_observacion)
    'End Sub

    Public Sub Cargar(ByVal var_id_int As Integer)
        Dim obj_dt_int As New DataTable
        obj_dt_int = Abrir_Tabla(Me.obj_Conex_int, "Select * from " & Me.var_Nombre_Tabla & " WHERE " & Me.var_Campo_Id & "=" & var_id_int)
        If obj_dt_int.Rows.Count > 0 Then
            Me.var_id = ac_Funciones.formato_Numero(obj_dt_int.Rows(0).Item("id").ToString)
            Me.var_fecha = ac_Funciones.formato_Fecha(obj_dt_int.Rows(0).Item("fecha").ToString)
            Me.var_registros_procesados = ac_Funciones.formato_Numero(obj_dt_int.Rows(0).Item("registros_procesados").ToString)
            Me.var_registros_fallidos = ac_Funciones.formato_Numero(obj_dt_int.Rows(0).Item("registros_fallidos").ToString)
            Me.var_observacion = ac_Funciones.formato_Texto(obj_dt_int.Rows(0).Item("observacion").ToString)
            Me.var_tipo = ac_Funciones.formato_Numero(obj_dt_int.Rows(0).Item("tipo").ToString)
            Me.var_id_usuario_reg = ac_Funciones.formato_Numero(obj_dt_int.Rows(0).Item("id_usuario_reg").ToString)
            Me.var_fecha_reg = ac_Funciones.formato_Fecha(obj_dt_int.Rows(0).Item("fecha_reg").ToString)
        Else
            Me.var_id = 0
        End If
    End Sub

    Public Function Actualizar(ByRef var_Error As String) As Boolean

        If Me.var_id = 0 Then   'NUEVO
            If Not Ingresar(Me.obj_Conex_int, Me.var_Nombre_Tabla, Me.var_Campos, Sql_Texto(Me.var_fecha) & "," & Sql_Texto(Me.var_registros_procesados) & "," & Sql_Texto(Me.var_registros_fallidos) & "," & Sql_Texto(Me.var_observacion) & "," & Sql_Texto(Me.var_tipo) & "," & Sql_Texto(Me.var_id_usuario_reg) & "," & Sql_Texto(Me.var_fecha_reg), var_Error) Then
                Dim obj_log As New cls_logs
                obj_log.ComentarioLog = "Error agregando cargas '" & Me.fecha & " - " & Me.var_registros_procesados & " - " & Me.var_registros_fallidos & "': " & var_Error
                obj_log.FechaLog = Now
                obj_log.id_Menu = New cls_modulos("cargas").Id
                obj_log.id_Usuario = var_id_usuario_reg
                obj_log.idAccion = New cls_acciones("agregar").Id
                obj_log.ResultadoLog = False
                obj_log.InsertarLog()

                Return False
                Exit Function
            Else
                Me.var_id = Valor_De(Me.obj_Conex_int, "select " & Me.var_Campo_Id & " from " & Me.var_Nombre_Tabla & " order by id desc")

                Dim obj_sb As New StringBuilder
                obj_sb.Append(Chr(34) & "Id" & Chr(34) & ":" & Chr(34) & Me.Id & Chr(34) & "")
                obj_sb.Append("," & Chr(34) & "fecha" & Chr(34) & ":" & Chr(34) & Me.fecha & Chr(34) & "")
                obj_sb.Append("," & Chr(34) & "registros_procesados" & Chr(34) & ":" & Chr(34) & Me.registros_procesados & Chr(34) & "")
                obj_sb.Append("," & Chr(34) & "registros_fallidos" & Chr(34) & ":" & Chr(34) & Me.registros_fallidos & Chr(34) & "")
                obj_sb.Append("," & Chr(34) & "observacion" & Chr(34) & ":" & Chr(34) & Me.observacion & Chr(34) & "")
                obj_sb.Append("," & Chr(34) & "tipo" & Chr(34) & ":" & Chr(34) & Me.tipo & Chr(34) & "")

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
        Dim obj_pasajero As New cls_cargas(var_id)
        If Not ac_Funciones.Actualizar(obj_Conex_int, cls_cargas.Nombre_Tabla, "anulado= case when isnull(anulado,0)=0 then '1' else '0' end, fecha_anulacion=" & Sql_Texto(Now, True) & ", id_usuario_anulacion=" & var_id_usuario, cls_cargas.Campo_Id & "=" & var_id, var_error) Then
            var_mensaje = var_error

            Dim obj_log As New cls_logs
            obj_log.ComentarioLog = "Error anulando pasajero '" & obj_pasajero.Id & " - " & obj_pasajero.fecha & "': " & var_error
            obj_log.FechaLog = Now
            obj_log.id_Menu = New cls_modulos("pasajero").Id
            obj_log.id_Usuario = var_id_usuario
            obj_log.idAccion = New cls_acciones("anular").Id
            obj_log.ResultadoLog = False
            obj_log.InsertarLog()

            Return False
        Else

            Dim obj_log As New cls_logs
            obj_log.ComentarioLog = "pasajero anulado '" & obj_pasajero.Id & " - " & obj_pasajero.fecha
            obj_log.FechaLog = Now
            obj_log.id_Menu = New cls_modulos("pasajero").Id
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

        If ac_Funciones.Eliminar(obj_conex, cls_cargas.Nombre_Tabla, cls_cargas.Campo_Id & "=" & var_id) < 0 Then
            var_mensaje = var_error
            Return False
        Else
            Return True
        End If
    End Function

    Public Shared Function Lista(Optional ByVal var_filtro As String = "") As DataTable
        Dim obj_Conex_int As New SqlConnection(ConfigurationManager.ConnectionStrings("connection").ConnectionString)
        Dim obj_dt_int As DataTable = Abrir_Tabla(obj_Conex_int, "select " & cls_cargas.Campo_Id & " as id, " & cls_cargas.Campo_Validacion & " as des from " & cls_cargas.Nombre_Tabla & IIf(var_filtro <> "", " where " & var_filtro, "") & " order by " & cls_cargas.Campo_Validacion & "")
        obj_dt_int.Rows.Add(0, "Seleccione una opción")
        Return obj_dt_int
    End Function

    Public Shared Function ConsultaActivos(ByRef var_error As String) As DataTable
        Dim obj_Conex_int As New SqlConnection(ConfigurationManager.ConnectionStrings("connection").ConnectionString)
        Dim var_consulta As String = "Select * from " & cls_cargas.ListadoActivos & "()"
        Dim var_msj As String = ""

        Dim obj_dt_int As System.Data.DataTable = Abrir_Tabla(obj_Conex_int, var_consulta, var_msj)
        var_error = var_msj
        Return obj_dt_int
    End Function

    Public Shared Function ConsultaAnulados(ByRef var_error As String) As DataTable
        Dim obj_Conex_int As New SqlConnection(ConfigurationManager.ConnectionStrings("connection").ConnectionString)
        Dim var_consulta As String = "Select * from " & cls_cargas.ListadoAnulados & "()"
        Dim var_msj As String = ""

        Dim obj_dt_int As System.Data.DataTable = Abrir_Tabla(obj_Conex_int, var_consulta, var_msj)
        var_error = var_msj
        Return obj_dt_int
    End Function

    Public Shared Function Consulta(ByRef var_error As String) As DataTable
        Dim obj_Conex_int As New SqlConnection(ConfigurationManager.ConnectionStrings("connection").ConnectionString)
        Dim var_consulta As String = "Select * from " & cls_cargas.Listado & "()"
        Dim var_msj As String = ""

        Dim obj_dt_int As System.Data.DataTable = Abrir_Tabla(obj_Conex_int, var_consulta, var_msj)
        var_error = var_msj
        Return obj_dt_int
    End Function
End Class
