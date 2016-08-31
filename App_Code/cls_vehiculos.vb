
Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports ac_Funciones

Public Class cls_vehiculos
    Dim var_Nombre_Tabla As String = "tbl_vehiculos"
    Dim var_Campo_Id As String = "id"
    Dim var_Campo_Validacion As String = "codigo"
    Dim var_Campos As String = "codigo,nombre,categoria,descripcion,precio,id_usuario_reg,fecha_reg,agencia"
    Dim obj_Conex_int As New SqlConnection(ConfigurationManager.ConnectionStrings("connection").ConnectionString)

    Dim var_id As Integer = 0
    Dim var_codigo As String = ""
    Dim var_nombre As String = ""
    Dim var_categoria As String = ""
    Dim var_descripcion As String = ""
    Dim var_precio As Double = 0
    Dim var_id_usuario_reg As Integer
    Dim var_fecha_reg As Date = Now
    Dim var_imagen As String = ""
    Dim var_id_agencia As Integer = 0

    Public Shared ReadOnly Property Nombre_Tabla() As String
        Get
            Return "tbl_vehiculos"
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
            Return "codigo"
        End Get
    End Property

    Public Shared ReadOnly Property Formulario() As String
        Get
            Return "frm_vehiculos.aspx"
        End Get
    End Property

    Public Shared ReadOnly Property ListadoActivos() As String
        Get
            Return "lst_vehiculosActivos"
        End Get
    End Property

    Public Shared ReadOnly Property ListadoAnulados() As String
        Get
            Return "lst_vehiculosAnulados"
        End Get
    End Property
    Public Shared ReadOnly Property Listado() As String
        Get
            Return "lst_vehiculos"
        End Get
    End Property

    Public ReadOnly Property Id() As Integer
        Get
            Return Me.var_id
        End Get
    End Property
    Public Property Codigo() As String
        Get
            Return Me.var_codigo
        End Get
        Set(ByVal value As String)
            Me.var_codigo = value
        End Set
    End Property

    Public Property Nombre() As String
        Get
            Return Me.var_nombre
        End Get
        Set(ByVal value As String)
            Me.var_nombre = value
        End Set
    End Property

    Public Property Categoria() As String
        Get
            Return Me.var_categoria
        End Get
        Set(ByVal value As String)
            Me.var_categoria = value
        End Set
    End Property

    Public Property Descripcion() As String
        Get
            Return Me.var_descripcion
        End Get
        Set(ByVal value As String)
            Me.var_descripcion = value
        End Set
    End Property

    Public Property Precio() As Double
        Get
            Return Me.var_precio
        End Get
        Set(ByVal value As Double)
            Me.var_precio = value
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
    Public ReadOnly Property Imagen() As String
        Get
            Return Me.var_imagen
        End Get
    End Property
    Public Property Agencia() As Integer
        Get
            Return Me.var_id_agencia
        End Get
        Set(ByVal value As Integer)
            Me.var_id_agencia = value
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
            Me.var_codigo = ac_Funciones.formato_Texto(obj_dt_int.Rows(0).Item("codigo").ToString)
            Me.var_nombre = ac_Funciones.formato_Texto(obj_dt_int.Rows(0).Item("nombre").ToString)
            Me.var_categoria = ac_Funciones.formato_Texto(obj_dt_int.Rows(0).Item("categoria").ToString)
            Me.var_descripcion = ac_Funciones.formato_Texto(obj_dt_int.Rows(0).Item("descripcion").ToString)
            Me.var_precio = ac_Funciones.formato_Numero(obj_dt_int.Rows(0).Item("precio").ToString, True)
            Me.var_id_usuario_reg = formato_Numero(obj_dt_int.Rows(0).Item("id_usuario_reg").ToString)
            Me.var_fecha_reg = formato_Fecha(obj_dt_int.Rows(0).Item("fecha_reg").ToString)
            Me.var_id_agencia = formato_Numero(obj_dt_int.Rows(0).Item("id_agencia").ToString)

            Me.var_imagen = Valor_De(obj_Conex_int, "select top 1 ruta from tbl_imagenes where id_vehiculo=" & Me.Id)
        Else
            Me.var_id = 0
        End If
    End Sub

    Public Function Actualizar(ByRef var_msj As String, ByVal var_id_usuario As Integer) As Boolean
        If Validar_Existe(Me.obj_Conex_int, Me.var_Nombre_Tabla, Me.var_Campo_Validacion, Me.var_nombre, Me.var_Campo_Id, Me.var_id) Then
            If var_msj = "" Then
                var_msj = "El vehiculo '" & Me.var_nombre & "' ya existe en la base de datos"
            End If
            Return False
            Exit Function
        End If

        Dim var_error As String = ""
        If Me.var_id = 0 Then   'NUEVO
            If Not Ingresar(Me.obj_Conex_int, Me.var_Nombre_Tabla, Me.var_Campos, Sql_Texto(Me.var_codigo) & "," & Sql_Texto(Me.var_nombre) & "," & Sql_Texto(Me.var_categoria) & "," & Sql_Texto(Me.var_descripcion) & "," & Sql_Texto(Me.var_precio) & "," & Sql_Texto(Me.var_id_usuario_reg) & "," & Sql_Texto(Me.var_fecha_reg) & "," & Sql_Texto(Me.var_id_agencia), var_error) Then
                Dim obj_log As New cls_logs
                obj_log.ComentarioLog = "Error agregando vehiculo '" & Me.var_nombre & "': " & var_error
                obj_log.FechaLog = Now
                obj_log.id_Menu = New cls_modulos("vehiculos").Id
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
                obj_sb.Append("," & Chr(34) & "Nombre" & Chr(34) & ":" & Chr(34) & Me.Nombre & Chr(34) & "")
                obj_sb.Append("," & Chr(34) & "Categoria" & Chr(34) & ":" & Chr(34) & Me.Categoria & Chr(34) & "")
                obj_sb.Append("," & Chr(34) & "Codigo" & Chr(34) & ":" & Chr(34) & Me.Codigo & Chr(34) & "")
                obj_sb.Append("," & Chr(34) & "Agencia" & Chr(34) & ":" & Chr(34) & Me.var_id_agencia & Chr(34) & "")
                obj_sb.Append("," & Chr(34) & "Descripcion" & Chr(34) & ":" & Chr(34) & Me.Descripcion & Chr(34) & "")

                Return True
            End If
        ElseIf Me.var_id > 0 Then 'EDICION
            If Not ac_Funciones.Actualizar(Me.obj_Conex_int, Me.var_Nombre_Tabla, "codigo=" & Sql_Texto(Me.var_codigo) & ",nombre=" & Sql_Texto(Me.var_nombre) & ",categoria=" & Sql_Texto(Me.var_categoria) & ",descripcion=" & Sql_Texto(Me.var_descripcion) & ",precio=" & Sql_Texto(Me.var_precio) & ",id_agencia=" & Sql_Texto(Me.var_id_agencia), Me.var_Campo_Id & "=" & Me.var_id) Then
                var_msj = var_error

                Dim obj_log As New cls_logs
                obj_log.ComentarioLog = "Error editando vehiculo '" & Me.var_id & " - " & Me.var_nombre & "': " & var_error
                obj_log.FechaLog = Now
                obj_log.id_Menu = New cls_modulos("vehiculos").Id
                obj_log.id_Usuario = var_id_usuario_reg
                obj_log.idAccion = New cls_acciones("editar").Id
                obj_log.ResultadoLog = False
                obj_log.InsertarLog()

                Return False
                Exit Function

            Else

                Dim obj_sb As New StringBuilder
                obj_sb.Append(Chr(34) & "Id" & Chr(34) & ":" & Chr(34) & Me.Id & Chr(34) & "")
                obj_sb.Append("," & Chr(34) & "Nombre" & Chr(34) & ":" & Chr(34) & Me.Nombre & Chr(34) & "")
                obj_sb.Append("," & Chr(34) & "Categoria" & Chr(34) & ":" & Chr(34) & Me.Categoria & Chr(34) & "")
                obj_sb.Append("," & Chr(34) & "Codigo" & Chr(34) & ":" & Chr(34) & Me.Codigo & Chr(34) & "")
                obj_sb.Append("," & Chr(34) & "Agencia" & Chr(34) & ":" & Chr(34) & Me.var_id_agencia & Chr(34) & "")
                obj_sb.Append("," & Chr(34) & "Descripcion" & Chr(34) & ":" & Chr(34) & Me.Descripcion & Chr(34) & "")

                Dim obj_log As New cls_logs
                obj_log.ComentarioLog = "vehiculo editada: {" & obj_sb.ToString & "}"
                obj_log.FechaLog = Now
                obj_log.id_Menu = New cls_modulos("vehiculos").Id
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
        Dim obj_vehiculo As New cls_vehiculos(var_id)
        If Not ac_Funciones.Actualizar(obj_Conex_int, cls_vehiculos.Nombre_Tabla, "anulado= case when isnull(anulado,0)=0 then '1' else '0' end, fecha_anulacion=" & Sql_Texto(Now, True) & ", id_usuario_anulacion=" & var_id_usuario, cls_vehiculos.Campo_Id & "=" & var_id, var_error) Then
            var_mensaje = var_error

            Dim obj_log As New cls_logs
            obj_log.ComentarioLog = "Error anulando vehiculo '" & obj_vehiculo.Id & " - " & obj_vehiculo.nombre & "': " & var_error
            obj_log.FechaLog = Now
            obj_log.id_Menu = New cls_modulos("vehiculos").Id
            obj_log.id_Usuario = var_id_usuario
            obj_log.idAccion = New cls_acciones("anular").Id
            obj_log.ResultadoLog = False
            obj_log.InsertarLog()

            Return False
        Else

            Dim obj_log As New cls_logs
            obj_log.ComentarioLog = "vehiculo anulado '" & obj_vehiculo.Id & " - " & obj_vehiculo.nombre
            obj_log.FechaLog = Now
            obj_log.id_Menu = New cls_modulos("vehiculos").Id
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

        If ac_Funciones.Eliminar(obj_conex, cls_vehiculos.Nombre_Tabla, cls_vehiculos.Campo_Id & "=" & var_id) < 0 Then
            var_mensaje = var_error
            Return False
        Else
            Return True
        End If
    End Function

    Public Shared Function Lista(Optional ByVal var_filtro As String = "") As DataTable
        Dim obj_Conex_int As New SqlConnection(ConfigurationManager.ConnectionStrings("connection").ConnectionString)
        Dim obj_dt_int As DataTable = Abrir_Tabla(obj_Conex_int, "select " & cls_vehiculos.Campo_Id & " as id, " & cls_vehiculos.Campo_Validacion & " as des from " & cls_vehiculos.Nombre_Tabla & IIf(var_filtro <> "", " where " & var_filtro, "") & " order by " & cls_vehiculos.Campo_Validacion & "")
        obj_dt_int.Rows.Add(0, "Seleccione una opción")
        Return obj_dt_int
    End Function

    Public Shared Function ConsultaActivos(ByRef var_error As String) As DataTable
        Dim obj_Conex_int As New SqlConnection(ConfigurationManager.ConnectionStrings("connection").ConnectionString)
        Dim var_consulta As String = "Select * from " & cls_vehiculos.ListadoActivos & "()"
        Dim var_msj As String = ""

        Dim obj_dt_int As System.Data.DataTable = Abrir_Tabla(obj_Conex_int, var_consulta, var_msj)
        var_error = var_msj
        Return obj_dt_int
    End Function

    Public Shared Function ConsultaAnulados(ByRef var_error As String) As DataTable
        Dim obj_Conex_int As New SqlConnection(ConfigurationManager.ConnectionStrings("connection").ConnectionString)
        Dim var_consulta As String = "Select * from " & cls_vehiculos.ListadoAnulados & "()"
        Dim var_msj As String = ""

        Dim obj_dt_int As System.Data.DataTable = Abrir_Tabla(obj_Conex_int, var_consulta, var_msj)
        var_error = var_msj
        Return obj_dt_int
    End Function

    Public Shared Function Consulta(ByRef var_error As String) As DataTable
        Dim obj_Conex_int As New SqlConnection(ConfigurationManager.ConnectionStrings("connection").ConnectionString)
        Dim var_consulta As String = "Select * from " & cls_vehiculos.Listado & "()"
        Dim var_msj As String = ""

        Dim obj_dt_int As System.Data.DataTable = Abrir_Tabla(obj_Conex_int, var_consulta, var_msj)
        var_error = var_msj
        Return obj_dt_int
    End Function

    Public Shared Function SiguienteNumero() As Integer
        Dim obj_Connection As New SqlConnection(ConfigurationManager.ConnectionStrings("connection").ConnectionString)
        Return ac_Funciones.formato_Numero(ac_Funciones.Valor_De(obj_Connection, "select isnull(max(right(num,4)),0) from (select case when ISNUMERIC(codigo)=1 then cast(codigo as int) else 0 end as num from tbl_vehiculos) as c").ToString)
    End Function

    Public Shared Function Imagenes(ByVal var_id_producto As Integer, ByRef var_error As String, Optional ByVal var_idsesion As String = "") As DataTable
        Dim obj_Conex_int As New SqlConnection(ConfigurationManager.ConnectionStrings("connection").ConnectionString)
        Dim var_consulta As String = ""
        If var_id_producto > 0 Then
            var_consulta = "Select id,ruta, rutaBig, posicion from tbl_imagenes where id_vehiculo=" & var_id_producto & " order by posicion"
        Else
            var_consulta = "Select id,ruta, rutaBig, posicion from tbl_imagenes where sessionid=" & Sql_Texto(var_idsesion) & " order by posicion"
        End If

        Dim var_msj As String = ""

        Dim obj_dt_int As System.Data.DataTable = Abrir_Tabla(obj_Conex_int, var_consulta, var_msj)
        var_error = var_msj
        Return obj_dt_int
    End Function
End Class
