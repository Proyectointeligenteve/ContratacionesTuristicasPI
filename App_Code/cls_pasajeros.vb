Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports ac_Funciones

Public Class cls_pasajeros
    Dim var_Nombre_Tabla As String = "tbl_pasajeros"
    Dim var_Campo_Id As String = "id"
    Dim var_Campo_Validacion As String = "rif"
    Dim var_Campos As String = "nombre,apellido,rif,telefono,email,tipo,id_usuario_reg,fecha_reg,direccion,edad,pasaporte,pasaporte_fecha"
    Dim obj_Conex_int As New SqlConnection(ConfigurationManager.ConnectionStrings("connection").ConnectionString)

    Dim var_id As Integer = 0
    Dim var_nombre As String = ""
    Dim var_apellido As String = ""
    Dim var_rif As String = ""
    Dim var_telefono As String = ""
    Dim var_email As String = ""
    Dim var_tipo As Integer = 0
    Dim var_id_usuario_reg As Integer
    Dim var_fecha_reg As Date = Now
    Dim var_direccion As String = ""
    Dim var_edad As Integer = 0
    Dim var_pasaporte As String = 0
    Dim var_pasaporte_fecha As Date = Now.Date

    Public Shared ReadOnly Property Nombre_Tabla() As String
        Get
            Return "tbl_pasajeros"
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
            Return "rif"
        End Get
    End Property

    Public Shared ReadOnly Property Formulario() As String
        Get
            Return "frm_pasajeros.aspx"
        End Get
    End Property

    Public Shared ReadOnly Property ListadoActivos() As String
        Get
            Return "lst_pasajerosActivos"
        End Get
    End Property

    Public Shared ReadOnly Property ListadoAnulados() As String
        Get
            Return "lst_pasajerosAnulados"
        End Get
    End Property
    Public Shared ReadOnly Property Listado() As String
        Get
            Return "lst_pasajeros"
        End Get
    End Property

    Public Shared ReadOnly Property Datos() As String
        Get
            Return "lst_pasajerosDatos"
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
    Public Property apellido() As String
        Get
            Return Me.var_apellido
        End Get
        Set(ByVal value As String)
            Me.var_apellido = value
        End Set
    End Property
    Public Property rif() As String
        Get
            Return Me.var_rif
        End Get
        Set(ByVal value As String)
            Me.var_rif = value
        End Set
    End Property
    Public Property telefono() As String
        Get
            Return Me.var_telefono
        End Get
        Set(ByVal value As String)
            Me.var_telefono = value
        End Set
    End Property
    Public Property email() As String
        Get
            Return Me.var_email
        End Get
        Set(ByVal value As String)
            Me.var_email = value
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

    Public Property direccion() As String
        Get
            Return Me.var_direccion
        End Get
        Set(ByVal value As String)
            Me.var_direccion = value
        End Set
    End Property

    Public Property edad() As Integer
        Get
            Return Me.var_edad
        End Get
        Set(ByVal value As Integer)
            Me.var_edad = value
        End Set
    End Property
    Public Property pasaporte() As String
        Get
            Return Me.var_pasaporte
        End Get
        Set(ByVal value As String)
            Me.var_pasaporte = value
        End Set
    End Property
    Public Property pasaporte_fecha() As Date
        Get
            Return Me.var_pasaporte_fecha
        End Get
        Set(ByVal value As Date)
            Me.var_pasaporte_fecha = value
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
            Me.var_apellido = ac_Funciones.formato_Texto(obj_dt_int.Rows(0).Item("apellido").ToString)
            Me.var_rif = ac_Funciones.formato_Texto(obj_dt_int.Rows(0).Item("rif").ToString)
            Me.var_telefono = ac_Funciones.formato_Texto(obj_dt_int.Rows(0).Item("telefono").ToString)
            Me.var_email = ac_Funciones.formato_Texto(obj_dt_int.Rows(0).Item("email").ToString)
            Me.var_tipo = ac_Funciones.formato_Numero(obj_dt_int.Rows(0).Item("tipo").ToString)
            Me.var_id_usuario_reg = ac_Funciones.formato_Numero(obj_dt_int.Rows(0).Item("id_usuario_reg").ToString)
            Me.var_fecha_reg = ac_Funciones.formato_Fecha(obj_dt_int.Rows(0).Item("fecha_reg").ToString)
            Me.var_direccion = ac_Funciones.formato_Texto(obj_dt_int.Rows(0).Item("direccion").ToString)
            Me.var_edad = ac_Funciones.formato_Numero(obj_dt_int.Rows(0).Item("edad").ToString)
            Me.var_pasaporte = ac_Funciones.formato_Texto(obj_dt_int.Rows(0).Item("pasaporte").ToString)
            Me.var_pasaporte_fecha = ac_Funciones.formato_Fecha(obj_dt_int.Rows(0).Item("pasaporte_fecha").ToString)
        Else
            Me.var_id = 0
        End If
    End Sub

    Public Function Actualizar(ByRef var_msj As String) As Boolean
        If Validar_Existe(Me.obj_Conex_int, Me.var_Nombre_Tabla, Me.var_Campo_Validacion, Me.var_nombre, Me.var_Campo_Id, Me.var_id) Then
            If var_msj = "" Then
                var_msj = "El pasajero '" & Me.var_nombre & "' ya existe en la base de datos"
            End If
            Return False
            Exit Function
        End If

        Dim var_error As String = ""
        If Me.var_id = 0 Then   'NUEVO
            If Not Ingresar(Me.obj_Conex_int, Me.var_Nombre_Tabla, Me.var_Campos, Sql_Texto(Me.var_nombre) & "," & Sql_Texto(Me.var_apellido) & "," & Sql_Texto(Me.var_rif) & "," & Sql_Texto(Me.var_telefono) & "," & Sql_Texto(Me.var_email) & "," & Sql_Texto(Me.var_tipo) & "," & Sql_Texto(Me.var_id_usuario_reg) & "," & Sql_Texto(Me.var_fecha_reg) & "," & Sql_Texto(Me.var_direccion) & "," & Sql_Texto(Me.var_edad) & "," & Sql_Texto(Me.var_pasaporte) & "," & Sql_Texto(Me.var_pasaporte_fecha), var_error) Then
                Dim obj_log As New cls_logs
                obj_log.ComentarioLog = "Error agregando pasajero '" & Me.var_rif & " - " & Me.var_nombre & " - " & Me.var_apellido & "': " & var_error
                obj_log.FechaLog = Now
                obj_log.id_Menu = New cls_modulos("pasajeros").Id
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
                obj_sb.Append("," & Chr(34) & "rif" & Chr(34) & ":" & Chr(34) & Me.rif & Chr(34) & "")
                obj_sb.Append("," & Chr(34) & "nombre" & Chr(34) & ":" & Chr(34) & Me.nombre & Chr(34) & "")
                obj_sb.Append("," & Chr(34) & "apellido" & Chr(34) & ":" & Chr(34) & Me.apellido & Chr(34) & "")
                obj_sb.Append("," & Chr(34) & "telefono" & Chr(34) & ":" & Chr(34) & Me.telefono & Chr(34) & "")
                obj_sb.Append("," & Chr(34) & "email" & Chr(34) & ":" & Chr(34) & Me.email & Chr(34) & "")
                obj_sb.Append("," & Chr(34) & "tipo" & Chr(34) & ":" & Chr(34) & Me.tipo & Chr(34) & "")
                obj_sb.Append("," & Chr(34) & "direccion" & Chr(34) & ":" & Chr(34) & Me.direccion & Chr(34) & "")
                obj_sb.Append("," & Chr(34) & "edad" & Chr(34) & ":" & Chr(34) & Me.edad & Chr(34) & "")
                obj_sb.Append("," & Chr(34) & "pasaporte" & Chr(34) & ":" & Chr(34) & Me.pasaporte & Chr(34) & "")
                obj_sb.Append("," & Chr(34) & "pasaporte_fecha" & Chr(34) & ":" & Chr(34) & Me.pasaporte_fecha & Chr(34) & "")

                Dim obj_log As New cls_logs
                obj_log.ComentarioLog = "Pasajero agregado: {" & obj_sb.ToString & "}"
                obj_log.FechaLog = Now
                obj_log.id_Menu = New cls_modulos("pasajeros").Id
                obj_log.id_Usuario = var_id_usuario_reg
                obj_log.idAccion = New cls_acciones("agregar").Id
                obj_log.ResultadoLog = True
                obj_log.InsertarLog()

                Return True
            End If
        ElseIf Me.var_id > 0 Then 'EDICION
            If Not ac_Funciones.Actualizar(Me.obj_Conex_int, Me.var_Nombre_Tabla, "nombre=" & Sql_Texto(Me.var_nombre) & ",apellido=" & Sql_Texto(Me.var_apellido) & ",rif=" & Sql_Texto(Me.var_rif) & ",direccion=" & Sql_Texto(Me.var_direccion) & ",edad=" & Sql_Texto(Me.var_edad) & ",pasaporte=" & Sql_Texto(Me.var_pasaporte) & ",pasaporte_fecha=" & Sql_Texto(Me.var_pasaporte_fecha), Me.var_Campo_Id & "=" & Me.var_id) Then
                var_msj = var_error

                Dim obj_log As New cls_logs
                obj_log.ComentarioLog = "Error editando pasajero '" & Me.var_rif & " - " & Me.var_nombre & " - " & Me.var_apellido & "': " & var_error
                obj_log.FechaLog = Now
                obj_log.id_Menu = New cls_modulos("pasajeros").Id
                obj_log.id_Usuario = var_id_usuario_reg
                obj_log.idAccion = New cls_acciones("editar").Id
                obj_log.ResultadoLog = False
                obj_log.InsertarLog()

                Return False
                Exit Function

            Else

                Dim obj_sb As New StringBuilder
                obj_sb.Append(Chr(34) & "Id" & Chr(34) & ":" & Chr(34) & Me.Id & Chr(34) & "")
                obj_sb.Append("," & Chr(34) & "rif" & Chr(34) & ":" & Chr(34) & Me.rif & Chr(34) & "")
                obj_sb.Append("," & Chr(34) & "nombre" & Chr(34) & ":" & Chr(34) & Me.nombre & Chr(34) & "")
                obj_sb.Append("," & Chr(34) & "apellido" & Chr(34) & ":" & Chr(34) & Me.apellido & Chr(34) & "")
                obj_sb.Append("," & Chr(34) & "telefono" & Chr(34) & ":" & Chr(34) & Me.telefono & Chr(34) & "")
                obj_sb.Append("," & Chr(34) & "email" & Chr(34) & ":" & Chr(34) & Me.email & Chr(34) & "")
                obj_sb.Append("," & Chr(34) & "tipo" & Chr(34) & ":" & Chr(34) & Me.tipo & Chr(34) & "")
                obj_sb.Append("," & Chr(34) & "direccion" & Chr(34) & ":" & Chr(34) & Me.direccion & Chr(34) & "")
                obj_sb.Append("," & Chr(34) & "edad" & Chr(34) & ":" & Chr(34) & Me.edad & Chr(34) & "")
                obj_sb.Append("," & Chr(34) & "pasaporte" & Chr(34) & ":" & Chr(34) & Me.pasaporte & Chr(34) & "")
                obj_sb.Append("," & Chr(34) & "pasaporte_fecha" & Chr(34) & ":" & Chr(34) & Me.pasaporte_fecha & Chr(34) & "")

                Dim obj_log As New cls_logs
                obj_log.ComentarioLog = "Pasajero editada: {" & obj_sb.ToString & "}"
                obj_log.FechaLog = Now
                obj_log.id_Menu = New cls_modulos("pasajeros").Id
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
        Dim obj_pasajero As New cls_pasajeros(var_id)
        If Not ac_Funciones.Actualizar(obj_Conex_int, cls_pasajeros.Nombre_Tabla, "anulado= case when isnull(anulado,0)=0 then '1' else '0' end, fecha_anulacion=" & Sql_Texto(Now, True) & ", id_usuario_anulacion=" & var_id_usuario, cls_pasajeros.Campo_Id & "=" & var_id, var_error) Then
            var_mensaje = var_error

            Dim obj_log As New cls_logs
            obj_log.ComentarioLog = "Error anulando pasajero '" & obj_pasajero.Id & " - " & obj_pasajero.nombre & "': " & var_error
            obj_log.FechaLog = Now
            obj_log.id_Menu = New cls_modulos("pasajeros").Id
            obj_log.id_Usuario = var_id_usuario
            obj_log.idAccion = New cls_acciones("anular").Id
            obj_log.ResultadoLog = False
            obj_log.InsertarLog()

            Return False
        Else

            Dim obj_log As New cls_logs
            obj_log.ComentarioLog = "pasajero anulado '" & obj_pasajero.Id & " - " & obj_pasajero.nombre
            obj_log.FechaLog = Now
            obj_log.id_Menu = New cls_modulos("pasajeros").Id
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

        If ac_Funciones.Eliminar(obj_conex, cls_pasajeros.Nombre_Tabla, cls_pasajeros.Campo_Id & "=" & var_id) < 0 Then
            var_mensaje = var_error
            Return False
        Else
            Return True
        End If
    End Function

    Public Shared Function Lista(Optional ByVal var_filtro As String = "") As DataTable
        Dim obj_Conex_int As New SqlConnection(ConfigurationManager.ConnectionStrings("connection").ConnectionString)
        Dim obj_dt_int As DataTable = Abrir_Tabla(obj_Conex_int, "select " & cls_pasajeros.Campo_Id & " as id, " & cls_pasajeros.Campo_Validacion & " as des from " & cls_pasajeros.Nombre_Tabla & IIf(var_filtro <> "", " where " & var_filtro, "") & " order by " & cls_pasajeros.Campo_Validacion & "")
        obj_dt_int.Rows.Add(0, "Seleccione una opción")
        Return obj_dt_int
    End Function

    Public Shared Function ConsultaActivos(ByRef var_error As String) As DataTable
        Dim obj_Conex_int As New SqlConnection(ConfigurationManager.ConnectionStrings("connection").ConnectionString)
        Dim var_consulta As String = "Select * from " & cls_pasajeros.ListadoActivos & "()"
        Dim var_msj As String = ""

        Dim obj_dt_int As System.Data.DataTable = Abrir_Tabla(obj_Conex_int, var_consulta, var_msj)
        var_error = var_msj
        Return obj_dt_int
    End Function

    Public Shared Function ConsultaAnulados(ByRef var_error As String) As DataTable
        Dim obj_Conex_int As New SqlConnection(ConfigurationManager.ConnectionStrings("connection").ConnectionString)
        Dim var_consulta As String = "Select * from " & cls_pasajeros.ListadoAnulados & "()"
        Dim var_msj As String = ""

        Dim obj_dt_int As System.Data.DataTable = Abrir_Tabla(obj_Conex_int, var_consulta, var_msj)
        var_error = var_msj
        Return obj_dt_int
    End Function

    Public Shared Function Consulta(ByRef var_error As String, Optional ByVal var_filtro As String = "") As DataTable
        Dim obj_Conex_int As New SqlConnection(ConfigurationManager.ConnectionStrings("connection").ConnectionString)
        Dim var_consulta As String = "Select * from " & cls_pasajeros.Listado & "(" & Sql_Texto(var_filtro) & ")"
        Dim var_msj As String = ""

        Dim obj_dt_int As System.Data.DataTable = Abrir_Tabla(obj_Conex_int, var_consulta, var_msj)
        var_error = var_msj
        Return obj_dt_int
    End Function
    Public Shared Function ConsultaDatos(ByRef var_error As String, Optional ByVal var_filtro As String = "") As DataTable
        Dim obj_Conex_int As New SqlConnection(ConfigurationManager.ConnectionStrings("connection").ConnectionString)
        Dim var_consulta As String = "Select * from " & cls_pasajeros.Datos & "(" & Sql_Texto(var_filtro) & ")"
        Dim var_msj As String = ""

        Dim obj_dt_int As System.Data.DataTable = Abrir_Tabla(obj_Conex_int, var_consulta, var_msj)
        var_error = var_msj
        Return obj_dt_int
    End Function
End Class
