
Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports ac_Funciones

Public Class cls_agencias

#Region "VARIABLES"
    Dim var_Nombre_Tabla As String = "tbl_agencias"
    Dim var_Campo_Id As String = "id"
    Dim var_Campo_Validacion As String = "nombre"
    Dim var_Campos As String = "nombre,razon_social,rif,direccion,telefono_fijo,telefono_movil,email,web,id_usuario_reg,fecha_reg"
    Dim obj_Conex_int As New SqlConnection(ConfigurationManager.ConnectionStrings("CCconexion").ConnectionString)

    Dim var_id As Integer
    Dim var_nombre As String = ""
    Dim var_razon_social As String = ""
    Dim var_rif As String = ""
    Dim var_direccion As String = ""
    Dim var_telefono_fijo As String = ""
    Dim var_telefono_movil As String = ""
    Dim var_email As String = ""
    Dim var_web As String = ""
    Dim var_id_usuario_reg As Integer = 0
    Dim var_fecha_reg As Date = Now

    Dim obj_contactos As New Generic.List(Of cls_agencias_contactos)
#End Region

#Region "PROPIEDADES"

    Public Shared ReadOnly Property Nombre_Tabla() As String
        Get
            Return "tbl_agencias"
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
    'Public Shared ReadOnly Property Listado() As String
    '    Get
    '        Return "lst_agencias"
    '    End Get
    'End Property

    Public Shared ReadOnly Property ListadoActivos() As String
        Get
            Return "lst_agenciasActivos"
        End Get
    End Property

    Public Shared ReadOnly Property ListadoAnulados() As String
        Get
            Return "lst_agenciasAnulados"
        End Get
    End Property
    Public Shared ReadOnly Property Listado() As String
        Get
            Return "lst_agencias"
        End Get
    End Property

    Public ReadOnly Property Conceptos() As Generic.List(Of cls_agencias_contactos)
        Get
            Return Me.obj_contactos
        End Get
    End Property

    Public ReadOnly Property Id() As Integer
        Get
            Return Me.var_id
        End Get
    End Property

    Public Property Nombre() As String
        Get
            Return Me.var_nombre
        End Get
        Set(ByVal value As String)
            Me.var_nombre = value
        End Set
    End Property

    Public Property RazonSocial() As String
        Get
            Return Me.var_razon_social
        End Get
        Set(ByVal value As String)
            Me.var_razon_social = value
        End Set
    End Property

    Public Property Rif() As String
        Get
            Return Me.var_rif
        End Get
        Set(ByVal value As String)
            Me.var_rif = value
        End Set
    End Property

    Public Property Direccion() As String
        Get
            Return Me.var_direccion
        End Get
        Set(ByVal value As String)
            Me.var_direccion = value
        End Set
    End Property

    Public Property TelefonoFijo() As String
        Get
            Return Me.var_telefono_fijo
        End Get
        Set(ByVal value As String)
            Me.var_telefono_fijo = value
        End Set
    End Property

    Public Property TelefonoMovil() As String
        Get
            Return Me.var_telefono_movil
        End Get
        Set(ByVal value As String)
            Me.var_telefono_movil = value
        End Set
    End Property

    Public Property Email() As String
        Get
            Return Me.var_email
        End Get
        Set(ByVal value As String)
            Me.var_email = value
        End Set
    End Property

    Public Property Web() As String
        Get
            Return Me.var_web
        End Get
        Set(ByVal value As String)
            Me.var_web = value
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
            Me.var_nombre = ac_Funciones.formato_Texto(obj_dt_int.Rows(0).Item("nombre").ToString)
            Me.var_razon_social = ac_Funciones.formato_Texto(obj_dt_int.Rows(0).Item("razon_social").ToString)
            Me.var_rif = ac_Funciones.formato_Texto(obj_dt_int.Rows(0).Item("rif").ToString)
            Me.var_direccion = ac_Funciones.formato_Texto(obj_dt_int.Rows(0).Item("direccion").ToString)
            Me.var_telefono_fijo = ac_Funciones.formato_Texto(obj_dt_int.Rows(0).Item("telefono_fijo").ToString)
            Me.var_telefono_movil = ac_Funciones.formato_Texto(obj_dt_int.Rows(0).Item("telefono_movil").ToString)
            Me.var_email = ac_Funciones.formato_Texto(obj_dt_int.Rows(0).Item("email").ToString)
            Me.var_web = ac_Funciones.formato_Texto(obj_dt_int.Rows(0).Item("web").ToString)
            Me.var_id_usuario_reg = ac_Funciones.formato_Numero(obj_dt_int.Rows(0).Item("id_usuario_reg").ToString)
            Me.var_fecha_reg = ac_Funciones.formato_Fecha(obj_dt_int.Rows(0).Item("fecha_reg").ToString)

            Dim obj_dt_conceptos As DataTable = ac_Funciones.Abrir_Tabla(Me.obj_Conex_int, "select id from " & cls_agencias_contactos.Nombre_Tabla & " where id_agencia=" & Me.var_id)
            For i As Integer = 0 To obj_dt_conceptos.Rows.Count - 1
                Me.obj_contactos.Add(New cls_agencias_contactos(obj_dt_conceptos.Rows(i).Item(0)))
            Next
        Else
            Me.var_id = -1
        End If
    End Sub

    Public Shared Function Lista(Optional ByVal var_filtro As String = "") As DataTable
        Dim obj_Conex_int As New SqlConnection(ConfigurationManager.ConnectionStrings("CCconexion").ConnectionString)
        'Dim obj_dt_int As DataTable = Abrir_Tabla(obj_Conex_int, "select id, nombre as des from tbl_tipos_acciones" & IIf(var_filtro <> "", " where " & var_filtro, "") & " order by nombre")
        Dim obj_dt_int As DataTable = Abrir_Tabla(obj_Conex_int, "select " & cls_agencias.Campo_Id & " as id, " & cls_agencias.Campo_Validacion & " as des from " & cls_agencias.Nombre_Tabla & IIf(var_filtro <> "", " where " & var_filtro, "") & " order by " & cls_agencias.Campo_Validacion & "")
        obj_dt_int.Rows.Add(0, "SELECCIONE")
        Return obj_dt_int
    End Function

    Public Function Actualizar(ByRef var_msj As String) As Boolean
        Dim var_error As String = ""
        If Validar_Existe(Me.obj_Conex_int, Me.var_Nombre_Tabla, Me.var_Campo_Validacion, Me.var_nombre, Me.var_Campo_Id, Me.var_id) Then

            If var_error = "" Then
                var_error = Me.var_nombre & "' ya existe en la base de datos"
            End If
            Return False
            Exit Function
        End If

        If Me.var_id = 0 Then   'NUEVO
            If Not Ingresar(Me.obj_Conex_int, Me.var_Nombre_Tabla, Me.var_Campos, Sql_Texto(Me.var_nombre) & "," & Sql_Texto(Me.var_razon_social) & "," & Sql_Texto(Me.var_rif) & "," & Sql_Texto(Me.var_direccion) & "," & Sql_Texto(Me.var_telefono_fijo) & "," & Sql_Texto(Me.var_telefono_movil) & "," & Sql_Texto(Me.var_email) & "," & Sql_Texto(Me.var_web) & "," & Sql_Texto(Me.var_id_usuario_reg) & "," & Sql_Texto(Me.var_fecha_reg), var_error) Then
                var_msj = var_error
                Return False
                Exit Function
            End If
            Me.var_id = Valor_De(Me.obj_Conex_int, "select " & Me.var_Campo_Id & " from " & Me.var_Nombre_Tabla & " order by id desc")

            For i As Integer = 0 To obj_contactos.Count - 1
                obj_contactos.Item(i).Id_agencia = Me.Id
                Dim var_msj2 As String = ""
                If Not obj_contactos.Item(i).Actualizar(var_error) Then
                    var_msj &= " - " & var_msj2
                End If
            Next
            Return True
        ElseIf Me.var_id > 0 Then 'EDICION
            If Not ac_Funciones.Actualizar(Me.obj_Conex_int, Me.var_Nombre_Tabla, "nombre=" & Sql_Texto(Me.var_nombre) & ",razon_socal=" & Sql_Texto(Me.var_razon_social) & ",rif=" & Sql_Texto(Me.var_rif) & ",direccion=" & Sql_Texto(Me.var_direccion) & ",telefono_fijo=" & Sql_Texto(Me.var_telefono_fijo) & ",telefono_movil=" & Sql_Texto(Me.var_telefono_movil) & ",email=" & Sql_Texto(Me.var_email) & ",web=" & Sql_Texto(Me.var_web), Me.var_Campo_Id & "=" & Me.var_id) Then
                Return False
                Exit Function
            End If

            Dim var_ids As String = ""
            For i As Integer = 0 To Me.obj_contactos.Count - 1
                Me.obj_contactos.Item(i).Id_agencia = Me.Id
                Dim var_msj2 As String = ""
                Me.obj_contactos.Item(i).Actualizar(var_msj2)
                var_msj &= " - " & var_msj2
                var_ids &= "," & Me.obj_contactos.Item(i).Id

            Next
            If var_ids.Trim.Length > 1 Then
                ac_Funciones.Eliminar(Me.obj_Conex_int, cls_agencias_contactos.Nombre_Tabla, "id_agencia=" & Me.var_id & " and id not in(" & var_ids.Substring(1) & ")")
            Else
                ac_Funciones.Eliminar(Me.obj_Conex_int, cls_agencias_contactos.Nombre_Tabla, "id_agencia=" & Me.var_id)
            End If

            Return True
        Else
            var_error = "No se encontro el Registro"
            Return False
        End If
    End Function

    Public Function ListaContactos() As DataTable
        Dim obj_dt As New System.Data.DataTable
        obj_dt.Columns.Add("DT_RowId", GetType(System.Int32))
        obj_dt.Columns.Add("Agencia", GetType(System.String))
        obj_dt.Columns.Add("Nombre", GetType(System.String))
        obj_dt.Columns.Add("Cargo", GetType(System.String))
        obj_dt.Columns.Add("Telefono", GetType(System.String))
        For i As Integer = 0 To Me.Conceptos.Count - 1
            obj_dt.Rows.Add(i + 1, Me.obj_contactos(i).Id_Agencia, Me.obj_contactos(i).nombre, Me.obj_contactos(i).cargo, Me.obj_contactos(i).telefono)
        Next

        Return obj_dt
    End Function

    Public Shared Function Anular(ByVal var_id As Integer, ByVal var_id_usuario As Integer, ByRef var_mensaje As String) As Boolean
        Dim obj_Conex_int As New SqlConnection(ConfigurationManager.ConnectionStrings("CCconexion").ConnectionString)
        Dim var_error As String = ""
        Dim obj_agencia As New cls_agencias(var_id)
        If Not ac_Funciones.Actualizar(obj_Conex_int, cls_agencias.Nombre_Tabla, "anulado= case when isnull(anulado,0)=0 then '1' else '0' end, fecha_anulacion=" & Sql_Texto(Now, True) & ", id_usuario_anulacion=" & var_id_usuario, cls_agencias.Campo_Id & "=" & var_id, var_error) Then
            var_mensaje = var_error

            Dim obj_log As New cls_logs
            obj_log.ComentarioLog = "Error anulando aerolinea '" & obj_agencia.Id & " - " & obj_agencia.Nombre & "': " & var_error
            obj_log.FechaLog = Now
            obj_log.id_Menu = New cls_modulos("aerolinea").Id
            obj_log.id_Usuario = var_id_usuario
            obj_log.idAccion = New cls_acciones("anular").Id
            obj_log.ResultadoLog = False
            obj_log.InsertarLog()

            Return False
        Else

            Dim obj_log As New cls_logs
            obj_log.ComentarioLog = "aerolinea anulado '" & obj_agencia.Id & " - " & obj_agencia.Nombre
            obj_log.FechaLog = Now
            obj_log.id_Menu = New cls_modulos("aerolinea").Id
            obj_log.id_Usuario = var_id_usuario
            obj_log.idAccion = New cls_acciones("anular").Id
            obj_log.ResultadoLog = True
            obj_log.InsertarLog()

            Return True
        End If
        Return True
    End Function
    Public Shared Function Eliminar(ByVal var_id As Integer, ByVal obj_usuario As cls_usuarios, ByRef var_mensaje As String) As Boolean
        Dim obj_Conex_int As New SqlConnection(ConfigurationManager.ConnectionStrings("CCconexion").ConnectionString)
        ac_Funciones.Eliminar(obj_Conex_int, cls_agencias.Nombre_Tabla, cls_agencias.Campo_Id & "=" & var_id)
        Return True
    End Function

    Public Shared Function ConsultaActivos(ByRef var_error As String) As DataTable
        Dim obj_Conex_int As New SqlConnection(ConfigurationManager.ConnectionStrings("CCconexion").ConnectionString)
        Dim var_consulta As String = "Select * from " & cls_agencias.ListadoActivos & "()"
        Dim var_msj As String = ""

        Dim obj_dt_int As System.Data.DataTable = Abrir_Tabla(obj_Conex_int, var_consulta, var_msj)
        var_error = var_msj
        Return obj_dt_int
    End Function

    Public Shared Function ConsultaAnulados(ByRef var_error As String) As DataTable
        Dim obj_Conex_int As New SqlConnection(ConfigurationManager.ConnectionStrings("CCconexion").ConnectionString)
        Dim var_consulta As String = "Select * from " & cls_agencias.ListadoAnulados & "()"
        Dim var_msj As String = ""

        Dim obj_dt_int As System.Data.DataTable = Abrir_Tabla(obj_Conex_int, var_consulta, var_msj)
        var_error = var_msj
        Return obj_dt_int
    End Function

    Public Shared Function Consulta(ByRef var_error As String) As DataTable
        Dim obj_Conex_int As New SqlConnection(ConfigurationManager.ConnectionStrings("CCconexion").ConnectionString)
        Dim var_consulta As String = "Select * from " & cls_agencias.Listado & "()"
        Dim var_msj As String = ""

        Dim obj_dt_int As System.Data.DataTable = Abrir_Tabla(obj_Conex_int, var_consulta, var_msj)
        var_error = var_msj
        Return obj_dt_int
    End Function

    'Public Shared Function Consulta(Optional ByVal var_filtro As String = "", Optional ByVal var_orden As String = "", Optional ByRef var_error As String = "") As DataTable
    '    Dim obj_Conex_int As New SqlConnection(ConfigurationManager.ConnectionStrings("CCconexion").ConnectionString)
    '    Dim var_consulta As String = "Select * from " & cls_agencias.Listado & "()"
    '    Return Abrir_Tabla(obj_Conex_int, var_consulta, var_error)
    'End Function

#End Region
End Class

