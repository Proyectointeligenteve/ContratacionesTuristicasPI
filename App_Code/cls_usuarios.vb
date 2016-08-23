Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports ac_Funciones

Public Class cls_usuarios
    Dim var_Nombre_Tabla As String = "tbl_usuarios"
    Dim var_Campo_Id As String = "id"
    Dim var_Campo_Validacion As String = "email"
    Dim var_Campos As String = "id_rol,nombre,apellido,clave,email,activo,telefono,fecha_nacimiento,genero,boletin,codigo,cambiar_clave,id_usuario_reg,fecha_reg,id_usuario_ult,fecha_ult"
    Dim obj_Conex_int As New SqlConnection(ConfigurationManager.ConnectionStrings("CCconexion").ConnectionString)

    Dim var_id As Integer = 0
    Dim var_id_rol As Integer = 0
    Dim var_id_cliente As Integer = 0
    Dim var_nombre As String = ""
    Dim var_apellido As String = ""
    Dim var_clave As String = ""
    Dim var_email As String = ""
    Dim var_telefono As String = ""
    Dim var_activo As Boolean = False
    'Dim var_fecha_nacimiento As Date = CDate("1900/01/01")
    'Dim var_genero As Integer = 0
    'Dim var_boletin As Boolean = False
    'Dim var_codigo As String = ""
    Dim var_cambiar_clave As Boolean = False
    Dim var_id_usuario_reg As Integer = 0
    Dim var_fecha_reg As Date = Now
    Dim var_id_usuario_ult As Integer = 0
    Dim var_fecha_ult As Date = Now

    'Dim obj_carrito As New cls_carritos

    Public Shared ReadOnly Property Nombre_Tabla() As String
        Get
            Return "tbl_usuarios"
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
            Return "usuario"
        End Get
    End Property

    Public Shared ReadOnly Property Formulario() As String
        Get
            Return "frm_usuario.aspx"
        End Get
    End Property

    Public Shared ReadOnly Property Listado() As String
        Get
            Return "lst_usuario.aspx"
        End Get
    End Property

    Public ReadOnly Property Id() As Integer
        Get
            Return Me.var_id
        End Get
    End Property

    Public Property id_rol() As Integer
        Get
            Return Me.var_id_rol
        End Get
        Set(ByVal value As Integer)
            Me.var_id_rol = value
        End Set
    End Property

    Public Property id_cliente() As Integer
        Get
            Return Me.var_id_cliente
        End Get
        Set(ByVal value As Integer)
            Me.var_id_cliente = value
        End Set
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

    Public Property Clave() As String
        Get
            Return Me.var_clave
        End Get
        Set(ByVal value As String)
            Me.var_clave = value
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

    Public Property telefono() As String
        Get
            Return Me.var_telefono
        End Get
        Set(ByVal value As String)
            Me.var_telefono = value
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

    'Public Property fecha_nacimiento() As Date
    '    Get
    '        Return Me.var_fecha_nacimiento
    '    End Get
    '    Set(ByVal value As Date)
    '        Me.var_fecha_nacimiento = value
    '    End Set
    'End Property

    'Public Property genero() As Integer
    '    Get
    '        Return Me.var_genero
    '    End Get
    '    Set(ByVal value As Integer)
    '        Me.var_genero = value
    '    End Set
    'End Property

    'Public Property boletin() As Boolean
    '    Get
    '        Return Me.var_boletin
    '    End Get
    '    Set(ByVal value As Boolean)
    '        Me.var_boletin = value
    '    End Set
    'End Property

    'Public Property codigo() As String
    '    Get
    '        Return Me.var_codigo
    '    End Get
    '    Set(ByVal value As String)
    '        Me.var_codigo = value
    '    End Set
    'End Property
    Public Property cambiar_clave() As Boolean
        Get
            Return Me.var_cambiar_clave
        End Get
        Set(ByVal value As Boolean)
            Me.var_cambiar_clave = value
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

    Public Property id_usuario_ult() As Integer
        Get
            Return Me.var_id_usuario_ult
        End Get
        Set(ByVal value As Integer)
            Me.var_id_usuario_ult = value
        End Set
    End Property

    Public Property fecha_ult() As Date
        Get
            Return Me.var_fecha_ult
        End Get
        Set(ByVal value As Date)
            Me.var_fecha_ult = value
        End Set
    End Property

    'Public Property carrito() As cls_carritos
    '    Get
    '        Return Me.obj_carrito
    '    End Get
    '    Set(ByVal value As cls_carritos)
    '        Me.obj_carrito = value
    '    End Set
    'End Property

    Sub New(Optional ByVal var_Id_int As Integer = 0)

        If var_Id_int > 0 Then
            Cargar(var_Id_int)
        End If

    End Sub

    Sub New(ByVal var_email As String)
        Cargar(var_email)
    End Sub

    Public Sub Cargar(ByVal var_id_int As Integer)
        Dim obj_dt_int As New DataTable
        obj_dt_int = Abrir_Tabla(Me.obj_Conex_int, "Select * from " & Me.var_Nombre_Tabla & " WHERE " & Me.var_Campo_Id & "=" & var_id_int)
        If obj_dt_int.Rows.Count > 0 Then
            Me.var_id = ac_Funciones.formato_Numero(obj_dt_int.Rows(0).Item("id").ToString)
            Me.var_id_rol = ac_Funciones.formato_Numero(obj_dt_int.Rows(0).Item("id_rol").ToString)
            Me.var_nombre = ac_Funciones.formato_Texto(obj_dt_int.Rows(0).Item("nombre").ToString)
            Me.var_apellido = ac_Funciones.formato_Texto(obj_dt_int.Rows(0).Item("apellido").ToString)
            Me.var_clave = ac_Funciones.formato_Texto(obj_dt_int.Rows(0).Item("clave").ToString)
            Me.var_email = ac_Funciones.formato_Texto(obj_dt_int.Rows(0).Item("email").ToString)
            Me.var_telefono = ac_Funciones.formato_Texto(obj_dt_int.Rows(0).Item("telefono").ToString)
            Me.var_activo = ac_Funciones.formato_boolean(obj_dt_int.Rows(0).Item("activo").ToString)
            'Me.var_fecha_nacimiento = ac_Funciones.formato_Fecha(obj_dt_int.Rows(0).Item("fecha_nacimiento").ToString)
            'Me.var_genero = ac_Funciones.formato_Numero(obj_dt_int.Rows(0).Item("genero").ToString)
            'Me.var_boletin = ac_Funciones.formato_boolean(obj_dt_int.Rows(0).Item("boletin").ToString)
            'Me.var_codigo = ac_Funciones.formato_Texto(obj_dt_int.Rows(0).Item("codigo").ToString)
            Me.var_cambiar_clave = ac_Funciones.formato_boolean(obj_dt_int.Rows(0).Item("cambiar_clave").ToString)
            Me.var_id_usuario_reg = ac_Funciones.formato_Numero(obj_dt_int.Rows(0).Item("id_usuario_reg").ToString)
            Me.var_fecha_reg = ac_Funciones.formato_Fecha(obj_dt_int.Rows(0).Item("fecha_reg").ToString)
            Me.var_id_usuario_ult = ac_Funciones.formato_Numero(obj_dt_int.Rows(0).Item("id_usuario_ult").ToString)
            Me.var_fecha_ult = ac_Funciones.formato_Fecha(obj_dt_int.Rows(0).Item("fecha_ult").ToString)

            'Me.obj_carrito = New cls_carritos(Val(ac_Funciones.Valor_De(Me.obj_Conex_int, "select top 1 id from tbl_carritos where id_usuario=" & Me.var_id).ToString))
        Else
            Me.var_id = 0
        End If
    End Sub

    Public Sub Cargar(ByVal var_email As String)
        Dim obj_dt_int As New DataTable
        obj_dt_int = Abrir_Tabla(Me.obj_Conex_int, "Select * from " & Me.var_Nombre_Tabla & " WHERE ltrim(rtrim(upper(usuario)))=" & Sql_Texto(var_email.ToUpper))
        If obj_dt_int.Rows.Count > 0 Then
            Me.var_id = ac_Funciones.formato_Numero(obj_dt_int.Rows(0).Item("id").ToString)
            Me.var_id_rol = ac_Funciones.formato_Numero(obj_dt_int.Rows(0).Item("id_rol").ToString)
            Me.var_nombre = ac_Funciones.formato_Texto(obj_dt_int.Rows(0).Item("nombre").ToString)
            Me.var_apellido = ac_Funciones.formato_Texto(obj_dt_int.Rows(0).Item("apellido").ToString)
            Me.var_clave = ac_Funciones.formato_Texto(obj_dt_int.Rows(0).Item("clave").ToString)
            Me.var_email = ac_Funciones.formato_Texto(obj_dt_int.Rows(0).Item("email").ToString)
            Me.var_telefono = ac_Funciones.formato_Texto(obj_dt_int.Rows(0).Item("telefono").ToString)
            Me.var_activo = ac_Funciones.formato_boolean(obj_dt_int.Rows(0).Item("activo").ToString)
            'Me.var_fecha_nacimiento = ac_Funciones.formato_Fecha(obj_dt_int.Rows(0).Item("fecha_nacimiento").ToString)
            'Me.var_genero = ac_Funciones.formato_Numero(obj_dt_int.Rows(0).Item("genero").ToString)
            'Me.var_boletin = ac_Funciones.formato_boolean(obj_dt_int.Rows(0).Item("boletin").ToString)
            'Me.var_codigo = ac_Funciones.formato_Texto(obj_dt_int.Rows(0).Item("codigo").ToString)
            Me.var_cambiar_clave = ac_Funciones.formato_boolean(obj_dt_int.Rows(0).Item("cambiar_clave").ToString)
            Me.var_id_usuario_reg = ac_Funciones.formato_Numero(obj_dt_int.Rows(0).Item("id_usuario_reg").ToString)
            Me.var_fecha_reg = ac_Funciones.formato_Fecha(obj_dt_int.Rows(0).Item("fecha_reg").ToString)
            Me.var_id_usuario_ult = ac_Funciones.formato_Numero(obj_dt_int.Rows(0).Item("id_usuario_ult").ToString)
            Me.var_fecha_ult = ac_Funciones.formato_Fecha(obj_dt_int.Rows(0).Item("fecha_ult").ToString)

            'Me.obj_carrito = New cls_carritos(Val(ac_Funciones.Valor_De(Me.obj_Conex_int, "select top 1 id from tbl_carritos where id_usuario=" & Me.var_id).ToString))
        Else
            Me.var_id = 0
        End If
    End Sub

    Public Function Actualizar(ByRef var_Error As String) As Boolean
        If Validar_Existe(Me.obj_Conex_int, Me.var_Nombre_Tabla, Me.var_Campo_Validacion, Me.var_email, Me.var_Campo_Id, Me.var_id) Then
            If var_Error = "" Then
                var_Error = "El email '" & Me.var_email & "' ya esta registrado"
            End If
            Return False
            Exit Function
        End If

        If Me.var_id = 0 Then   'NUEVO
            If Not Ingresar(Me.obj_Conex_int, Me.var_Nombre_Tabla, Me.var_Campos, Sql_Texto(Me.var_id_rol) & "," & Sql_Texto(Me.var_nombre) & "," & Sql_Texto(Me.var_apellido) & "," & Sql_Texto(Me.var_clave) & "," & Sql_Texto(Me.var_email) & "," & Sql_Texto(Me.var_activo) & "," & Sql_Texto(Me.var_telefono) & "," & Sql_Texto(Me.var_cambiar_clave) & "," & Sql_Texto(Me.var_id_usuario_reg) & "," & Sql_Texto(Me.var_fecha_reg) & "," & Sql_Texto(Me.var_id_usuario_ult) & "," & Sql_Texto(Me.var_fecha_ult), var_Error) Then
                Return False
                Exit Function
            End If
            Me.var_id = Valor_De(Me.obj_Conex_int, "select " & Me.var_Campo_Id & " from " & Me.var_Nombre_Tabla & " where email=" & Sql_Texto(Me.var_email))
            Return True
        ElseIf Me.var_id > 0 Then 'EDICION
            If Not ac_Funciones.Actualizar(Me.obj_Conex_int, Me.var_Nombre_Tabla, "id_rol=" & Sql_Texto(Me.var_id_rol) & ", nombre=" & Sql_Texto(Me.var_nombre) & ", apellido=" & Sql_Texto(Me.var_apellido) & ", clave=" & Sql_Texto(Me.var_clave) & ", email=" & Sql_Texto(Me.var_email) & ", activo=" & Sql_Texto(Me.var_activo) & ", telefono=" & Sql_Texto(Me.var_telefono) & ", cambiar_clave=" & Sql_Texto(Me.var_cambiar_clave) & ", id_usuario_reg=" & Sql_Texto(Me.var_id_usuario_reg) & ", fecha_reg=" & Sql_Texto(Me.var_fecha_reg) & ", id_usuario_ult=" & Sql_Texto(Me.var_id_usuario_ult) & ", fecha_ult=" & Sql_Texto(Me.var_fecha_ult), Me.var_Campo_Id & "=" & Me.var_id) Then
                Return False
                Exit Function
            End If
            Return True
        Else
            var_Error = "No se encontro el Registro"
            Return False
        End If
    End Function

    Public Shared Function Eliminar(ByVal var_id As Integer, ByVal obj_usuario As cls_usuarios, ByRef var_mensaje As String) As Boolean
        Dim obj_conex As New SqlConnection(ConfigurationManager.ConnectionStrings("CCconexion").ConnectionString)
        Dim var_error As String = ""

        If ac_Funciones.Eliminar(obj_conex, cls_usuarios.Nombre_Tabla, cls_usuarios.Campo_Id & "=" & var_id) < 0 Then
            var_mensaje = var_error
            Return False
        Else
            Return True
        End If
    End Function

    Public Shared Function Lista(Optional ByVal var_filtro As String = "") As DataTable
        Dim obj_Conex_int As New SqlConnection(ConfigurationManager.ConnectionStrings("CCconexion").ConnectionString)
        Dim obj_dt_int As DataTable = Abrir_Tabla(obj_Conex_int, "select " & cls_usuarios.Campo_Id & " as id, " & cls_usuarios.Campo_Validacion & " as des from " & cls_usuarios.Nombre_Tabla & IIf(var_filtro <> "", " where " & var_filtro, "") & " order by " & cls_usuarios.Campo_Validacion & "")
        obj_dt_int.Rows.Add(0, "Seleccione una opción")
        Return obj_dt_int
    End Function

    Public Shared Function Consulta() As DataTable
        Dim obj_conex As New SqlConnection(ConfigurationManager.ConnectionStrings("CCconexion").ConnectionString)
        Return Abrir_Tabla(obj_conex, "SELECT * from lst_usuarios()")
    End Function

    'Public Shared Function suscribir(ByRef var_correo As String, ByRef var_Error As String) As Boolean
    '    Dim obj_conex As New SqlConnection(ConfigurationManager.ConnectionStrings("CCconexion").ConnectionString)
    '    If var_correo.Length <= 0 OrElse Not var_correo.Contains("@") OrElse Not var_correo.Contains(".") Then
    '        var_Error = "El email es inválido"
    '        Return False
    '        Exit Function
    '    End If

    '    If Validar_Existe(obj_conex, "tbl_suscripciones", "email", var_correo, "id", 0) Then
    '        If var_Error = "" Then
    '            var_Error = "El email '" & var_correo & "' ya existe en la base de datos"
    '        End If
    '    End If
    '    Dim var_err As String = ""
    '    If Not Ingresar(obj_conex, "tbl_suscripciones", "email", Sql_Texto(var_correo), var_err) Then
    '        var_Error = var_err
    '        Return False
    '    Else
    '        Return True
    '    End If
    'End Function

End Class
