Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports ac_Funciones

Public Class cls_freelances_configuracion
    Dim var_id_freelance_Tabla As String = "tbl_freelances_configuracion"
    Dim var_Campo_Id As String = "id"
    Dim var_Campo_Validacion As String = "nombre"
    Dim var_Campos As String = "id_freelance,id_tipo,id_aerolinea,id_hotel,id_vehiculo,comision,forma"
    Dim obj_Conex_int As New SqlConnection(ConfigurationManager.ConnectionStrings("connection").ConnectionString)

    Dim var_id As Integer = 0
    Dim var_id_freelance As Integer = 0
    Dim var_id_tipo As Integer = 0
    Dim var_id_aerolinea As cls_aerolineas = New cls_aerolineas
    Dim var_id_hotel As cls_hoteles = New cls_hoteles
    Dim var_id_vehiculo As cls_vehiculos = New cls_vehiculos
    Dim var_comision As Double = 0
    Dim var_forma As Integer = 0

    Public Shared ReadOnly Property Nombre_Tabla() As String
        Get
            Return "tbl_freelances_configuracion"
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
            Return "frm_freelances_configuracion.aspx"
        End Get
    End Property

    Public Shared ReadOnly Property ListadoActivos() As String
        Get
            Return "lst_freelances_configuracionActivos"
        End Get
    End Property

    Public Shared ReadOnly Property ListadoAnulados() As String
        Get
            Return "lst_freelances_configuracionAnulados"
        End Get
    End Property
    Public Shared ReadOnly Property Listado() As String
        Get
            Return "lst_freelances_configuracion"
        End Get
    End Property

    Public ReadOnly Property Id() As Integer
        Get
            Return Me.var_id
        End Get
    End Property

    Public Property freelance() As Integer
        Get
            Return Me.var_id_freelance
        End Get
        Set(ByVal value As Integer)
            Me.var_id_freelance = value
        End Set
    End Property

    Public Property Tipo() As Integer
        Get
            Return Me.var_id_tipo
        End Get
        Set(ByVal value As Integer)
            Me.var_id_tipo = value
        End Set
    End Property

    Public Property Aerolinea() As cls_aerolineas
        Get
            Return Me.var_id_aerolinea
        End Get
        Set(ByVal value As cls_aerolineas)
            Me.var_id_aerolinea = value
        End Set
    End Property

    Public Property Hotel() As cls_hoteles
        Get
            Return Me.var_id_hotel
        End Get
        Set(ByVal value As cls_hoteles)
            Me.var_id_hotel = value
        End Set
    End Property

    Public Property Vehiculo() As cls_vehiculos
        Get
            Return Me.var_id_vehiculo
        End Get
        Set(ByVal value As cls_vehiculos)
            Me.var_id_vehiculo = value
        End Set
    End Property

    Public Property Comision() As Double
        Get
            Return Me.var_comision
        End Get
        Set(ByVal value As Double)
            Me.var_comision = value
        End Set
    End Property

    Public Property Forma() As Integer
        Get
            Return Me.var_forma
        End Get
        Set(ByVal value As Integer)
            Me.var_forma = value
        End Set
    End Property
    'Propiedad obligatoria para verficar que el valor de la tabla en la base de datos exista o no
    Sub New(Optional ByVal var_Id_int As Integer = 0)

        If var_Id_int > 0 Then
            Cargar(var_Id_int)
        End If

    End Sub

    'agregada la linea de id_tipo
    Public Sub Cargar(ByVal var_id_int As Integer)
        Dim obj_dt_int As New DataTable
        obj_dt_int = Abrir_Tabla(Me.obj_Conex_int, "Select * from " & Me.var_id_freelance_Tabla & " WHERE " & Me.var_Campo_Id & "=" & var_id_int)
        If obj_dt_int.Rows.Count > 0 Then
            Me.var_id = ac_Funciones.formato_Numero(obj_dt_int.Rows(0).Item("id").ToString)
            Me.var_id_freelance = ac_Funciones.formato_Numero(obj_dt_int.Rows(0).Item("id_freelance").ToString)
            Me.var_id_tipo = ac_Funciones.formato_Numero(obj_dt_int.Rows(0).Item("id_tipo").ToString)
            Me.var_id_aerolinea = New cls_aerolineas(obj_dt_int.Rows(0).Item("id_aerolinea").ToString)
            Me.var_id_hotel = New cls_hoteles(obj_dt_int.Rows(0).Item("id_hotel").ToString)
            Me.var_id_vehiculo = New cls_vehiculos(obj_dt_int.Rows(0).Item("id_vehiculo").ToString)
            Me.var_comision = ac_Funciones.formato_Numero(obj_dt_int.Rows(0).Item("comision").ToString, True)
            Me.var_forma = ac_Funciones.formato_Numero(obj_dt_int.Rows(0).Item("forma").ToString)
        Else
            Me.var_id = 0
        End If
    End Sub

    Public Function Actualizar(ByRef var_Error As String) As Boolean
        If Validar_Existe(Me.obj_Conex_int, Me.var_id_freelance_Tabla, Me.var_Campo_Validacion, Me.var_id_freelance, Me.var_Campo_Id, Me.var_id) Then
            If var_Error = "" Then
                var_Error = "El freelance_configuracion '" & Me.var_id_freelance & "' ya existe en la base de datos"
            End If
            Return False
            Exit Function
        End If

        If Me.var_id = 0 Then   'NUEVO
            If Not Ingresar(Me.obj_Conex_int, Me.var_id_freelance_Tabla, Me.var_Campos, Sql_Texto(Me.var_id_freelance) & "," & Sql_Texto(Me.var_id_tipo) & "," & Sql_Texto(Me.var_id_aerolinea.Id) & "," & Sql_Texto(Me.var_id_hotel.Id) & "," & Sql_Texto(Me.var_id_vehiculo.Id) & "," & Sql_Texto(Me.var_comision) & "," & Sql_Texto(Me.var_forma), var_Error) Then
                Return False
                Exit Function
            End If
            Me.var_id = Valor_De(Me.obj_Conex_int, "select " & Me.var_Campo_Id & " from " & Me.var_id_freelance_Tabla & " order by id desc")
            Return True
        ElseIf Me.var_id > 0 Then 'EDICION
            If Not ac_Funciones.Actualizar(Me.obj_Conex_int, Me.var_id_freelance_Tabla, "id_freelance=" & Sql_Texto(Me.var_id_freelance) & ", id_tipo=" & Sql_Texto(Me.var_id_tipo) & ",id_aerolinea=" & Sql_Texto(Me.var_id_aerolinea.Id) & ",id_hotel=" & Sql_Texto(Me.var_id_hotel.Id) & ",id_vehiculo=" & Sql_Texto(Me.var_id_vehiculo.Id) & ",comision=" & Sql_Texto(Me.var_comision) & ",forma=" & Sql_Texto(Me.var_forma), Me.var_Campo_Id & "=" & Me.var_id) Then
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
        Dim obj_conex As New SqlConnection(ConfigurationManager.ConnectionStrings("connection").ConnectionString)
        Dim var_error As String = ""

        If ac_Funciones.Eliminar(obj_conex, cls_freelances_configuracion.Nombre_Tabla, cls_freelances_configuracion.Campo_Id & "=" & var_id) < 0 Then
            var_mensaje = var_error
            Return False
        Else
            Return True
        End If
    End Function

    Public Shared Function Lista(Optional ByVal var_filtro As String = "") As DataTable
        Dim obj_Conex_int As New SqlConnection(ConfigurationManager.ConnectionStrings("connection").ConnectionString)
        Dim obj_dt_int As DataTable = Abrir_Tabla(obj_Conex_int, "select " & cls_freelances_configuracion.Campo_Id & " as id, " & cls_freelances_configuracion.Campo_Validacion & " as des from " & cls_freelances_configuracion.Nombre_Tabla & IIf(var_filtro <> "", " where " & var_filtro, "") & " order by " & cls_freelances_configuracion.Campo_Validacion & "")
        obj_dt_int.Rows.Add(0, "Seleccione una opción")
        Return obj_dt_int
    End Function

    Public Shared Function Consulta(ByRef var_error As String) As DataTable
        Dim obj_Conex_int As New SqlConnection(ConfigurationManager.ConnectionStrings("connection").ConnectionString)
        Dim var_consulta As String = "Select * from " & cls_freelances_configuracion.Listado & "()"
        Dim var_msj As String = ""

        Dim obj_dt_int As System.Data.DataTable = Abrir_Tabla(obj_Conex_int, var_consulta, var_msj)
        var_error = var_msj
        Return obj_dt_int
    End Function
End Class
