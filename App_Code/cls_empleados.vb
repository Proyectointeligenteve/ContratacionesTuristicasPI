
Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports ac_Funciones

Public Class cls_empleados

#Region "VARIABLES"
    Dim var_Nombre_Tabla As String = "tbl_empleados"
    Dim var_Campo_Id As String = "id"
    Dim var_Campo_Validacion As String = "cedula"
    Dim var_Campos As String = "nombre,apellido,cedula,fecha_nacimiento,email,cuenta_nomina,id_cargo,direccion,telefono,sueldo,fecha_ingreso,fecha_egreso,id_cuenta_bancaria_pago,numero_fideicomiso,id_horario,tipo_trabajo,foto,tipo_nomina,estatus,id_usuario_reg,fecha_reg,sexo,monto_poliza,numero_cestaTicket,vendedor"
    Dim obj_Conex_int As New SqlConnection(ConfigurationManager.ConnectionStrings("connection").ConnectionString)

    Dim var_id As Integer
    Dim var_nombre As String = ""
    Dim var_apellido As String = ""
    Dim var_cedula As String = ""
    Dim var_fecha_nacimiento As Date = "01/01/1900"
    Dim var_email As String = ""
    Dim var_cuenta_nomina As String = ""
    Dim var_id_cargo As Integer = 0
    Dim var_direccion As String = ""
    Dim var_telefono As String = ""
    Dim var_sueldo As Double = 0
    Dim var_fecha_ingreso As Date = "01/01/1900"
    Dim var_fecha_egreso As Date = "01/01/1900"
    Dim var_id_cuenta_bancaria_pago As Integer = 0
    Dim var_numero_fideicomiso As String = ""
    Dim var_id_horario As Integer = 0
    Dim var_tipo_trabajo As Integer = 0
    Dim var_foto As String = ""
    Dim var_tipo_nomina As Integer = 0
    Dim var_estatus As Integer = 0
    Dim var_id_usuario_reg As cls_usuarios = New cls_usuarios
    Dim var_fecha_reg As Date = Now
    Dim var_sexo As Integer = 0
    Dim var_monto_poliza As Double = 0
    Dim var_numero_cestaTicket As String = ""
    Dim var_vendedor As Boolean = False

#End Region

#Region "PROPIEDADES"
    'Friend Rows As Object

    Public Shared ReadOnly Property Nombre_Tabla() As String
        Get
            Return "tbl_empleados"
        End Get
    End Property

    Public Shared ReadOnly Property Campo_Id() As String
        Get
            Return "id"
        End Get
    End Property

    Public Shared ReadOnly Property Campo_Validacion() As String
        Get
            Return "cedula"
        End Get
    End Property

    Public Shared ReadOnly Property campo_lista() As String
        Get
            Return "nombre"
        End Get
    End Property
    Public Shared ReadOnly Property Listado() As String
        Get
            Return "lst_empleados"
        End Get
    End Property
    Public Shared ReadOnly Property ListadoEgreso() As String
        Get
            Return "lst_empleadosEgresos"
        End Get
    End Property

    Public Shared ReadOnly Property ListadoNomina() As String
        Get
            Return "lst_empleadosNomina"
        End Get
    End Property

    Public Shared ReadOnly Property ListadoNominaLiquidacion() As String
        Get
            Return "lst_empleadosNominaLiquidacion"
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

    Public Property Apellido() As String
        Get
            Return Me.var_apellido
        End Get
        Set(ByVal value As String)
            Me.var_apellido = value
        End Set
    End Property

    Public Property cedula() As String
        Get
            Return Me.var_cedula
        End Get
        Set(ByVal value As String)
            Me.var_cedula = value
        End Set
    End Property

    Public Property Fecha_nacimiento() As Date
        Get
            Return Me.var_fecha_nacimiento
        End Get
        Set(ByVal value As Date)
            Me.var_fecha_nacimiento = value
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

    Public Property Cuenta_nomina() As String
        Get
            Return Me.var_cuenta_nomina
        End Get
        Set(ByVal value As String)
            Me.var_cuenta_nomina = value
        End Set
    End Property

    Public Property Cargo() As Integer
        Get
            Return Me.var_id_cargo
        End Get
        Set(ByVal value As Integer)
            Me.var_id_cargo = value
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

    Public Property Telefono() As String
        Get
            Return Me.var_telefono
        End Get
        Set(ByVal value As String)
            Me.var_telefono = value
        End Set
    End Property

    Public Property Sueldo() As Double
        Get
            Return Me.var_sueldo
        End Get
        Set(ByVal value As Double)
            Me.var_sueldo = value
        End Set
    End Property

    Public Property Fecha_ingreso() As Date
        Get
            Return Me.var_fecha_ingreso
        End Get
        Set(ByVal value As Date)
            Me.var_fecha_ingreso = value
        End Set
    End Property

    Public Property Fecha_egreso() As Date
        Get
            Return Me.var_fecha_egreso
        End Get
        Set(ByVal value As Date)
            Me.var_fecha_egreso = value
        End Set
    End Property

    Public Property Cuenta_bancaria_pago() As Integer
        Get
            Return Me.var_id_cuenta_bancaria_pago
        End Get
        Set(ByVal value As Integer)
            Me.var_id_cuenta_bancaria_pago = value
        End Set
    End Property

    Public Property Numero_fideicomiso() As String
        Get
            Return Me.var_numero_fideicomiso
        End Get
        Set(ByVal value As String)
            Me.var_numero_fideicomiso = value
        End Set
    End Property

    Public Property Horario() As Integer
        Get
            Return Me.var_id_horario
        End Get
        Set(ByVal value As Integer)
            Me.var_id_horario = value
        End Set
    End Property

    Public Property Tipo_trabajo() As Integer
        Get
            Return Me.var_tipo_trabajo
        End Get
        Set(ByVal value As Integer)
            Me.var_tipo_trabajo = value
        End Set
    End Property

    Public Property Foto() As String
        Get
            Return Me.var_foto
        End Get
        Set(ByVal value As String)
            Me.var_foto = value
        End Set
    End Property

    Public Property Tipo_nomina() As Integer
        Get
            Return Me.var_tipo_nomina
        End Get
        Set(ByVal value As Integer)
            Me.var_tipo_nomina = value
        End Set
    End Property

    Public Property Estatus() As Integer
        Get
            Return Me.var_estatus
        End Get
        Set(ByVal value As Integer)
            Me.var_estatus = value
        End Set
    End Property

    Public Property id_usuario_reg() As cls_usuarios
        Get
            Return Me.var_id_usuario_reg
        End Get
        Set(ByVal value As cls_usuarios)
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
    Public Property Sexo() As Integer
        Get
            Return Me.var_sexo
        End Get
        Set(ByVal value As Integer)
            Me.var_sexo = value
        End Set
    End Property

    Public Property Monto_poliza() As Double
        Get
            Return Me.var_monto_poliza
        End Get
        Set(ByVal value As Double)
            Me.var_monto_poliza = value
        End Set
    End Property

    Public Property Numero_cestaTicket() As String
        Get
            Return Me.var_numero_cestaTicket
        End Get
        Set(ByVal value As String)
            Me.var_numero_cestaTicket = value
        End Set
    End Property

    Public Property Vendedor() As Boolean
        Get
            Return Me.var_vendedor
        End Get
        Set(ByVal value As Boolean)
            Me.var_vendedor = value
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
        obj_dt_int = Abrir_Tabla(Me.obj_Conex_int, "Select id,nombre,apellido,nacionalidad,cedula,fecha_nacimiento,email,cuenta_nomina,id_cargo,direccion,telefono,sueldo,convert(varchar(12),fecha_ingreso,103) as fecha_ingreso,convert(varchar(12),fecha_egreso,103) as fecha_egreso,id_cuenta_bancaria_pago,numero_fideicomiso,id_horario,tipo_trabajo,foto,tipo_nomina,estatus,id_usuario_reg,fecha_reg,sexo,monto_poliza,numero_cestaTicket from " & Me.var_Nombre_Tabla & " WHERE " & Me.var_Campo_Id & "=" & var_id_int)
        If obj_dt_int.Rows.Count > 0 Then
            Me.var_id = obj_dt_int.Rows(0).Item("id").ToString
            Me.var_nombre = formato_Texto(obj_dt_int.Rows(0).Item("nombre").ToString)
            Me.var_apellido = formato_Texto(obj_dt_int.Rows(0).Item("apellido").ToString)
            Me.var_cedula = formato_Texto(obj_dt_int.Rows(0).Item("cedula").ToString)
            Me.var_fecha_nacimiento = formato_Fecha(obj_dt_int.Rows(0).Item("fecha_nacimiento").ToString)
            Me.var_email = formato_Texto(obj_dt_int.Rows(0).Item("email").ToString)
            Me.var_cuenta_nomina = formato_Texto(obj_dt_int.Rows(0).Item("cuenta_nomina").ToString)
            Me.var_id_cargo = formato_Numero(obj_dt_int.Rows(0).Item("id_cargo").ToString)
            Me.var_direccion = formato_Texto(obj_dt_int.Rows(0).Item("direccion").ToString)
            Me.var_telefono = formato_Texto(obj_dt_int.Rows(0).Item("telefono").ToString)
            Me.var_sueldo = formato_Numero(obj_dt_int.Rows(0).Item("sueldo").ToString, True)
            Me.var_fecha_ingreso = formato_Fecha(obj_dt_int.Rows(0).Item("fecha_ingreso").ToString)
            Me.var_fecha_egreso = formato_Fecha(obj_dt_int.Rows(0).Item("fecha_egreso").ToString)
            Me.var_id_cuenta_bancaria_pago = formato_Numero(obj_dt_int.Rows(0).Item("id_cuenta_bancaria_pago").ToString)
            Me.var_numero_fideicomiso = formato_Texto(obj_dt_int.Rows(0).Item("numero_fideicomiso").ToString)
            Me.var_id_horario = formato_Numero(obj_dt_int.Rows(0).Item("id_horario").ToString)
            Me.var_tipo_trabajo = formato_Numero(obj_dt_int.Rows(0).Item("tipo_trabajo").ToString)
            Me.var_foto = formato_Texto(obj_dt_int.Rows(0).Item("foto").ToString)
            Me.var_tipo_nomina = formato_Numero(obj_dt_int.Rows(0).Item("tipo_nomina").ToString)
            Me.var_estatus = formato_Numero(obj_dt_int.Rows(0).Item("estatus").ToString)
            Me.var_monto_poliza = formato_Numero(obj_dt_int.Rows(0).Item("monto_poliza").ToString, True)

            Me.var_id_usuario_reg = New cls_usuarios(CInt(obj_dt_int.Rows(0).Item("id_usuario_reg").ToString))
            Me.var_fecha_reg = formato_Fecha(obj_dt_int.Rows(0).Item("fecha_reg").ToString)

            Me.var_sexo = formato_Numero(obj_dt_int.Rows(0).Item("sexo").ToString)
            Me.var_numero_cestaTicket = formato_Texto(obj_dt_int.Rows(0).Item("numero_cestaTicket").ToString)
            Me.var_vendedor = formato_boolean(obj_dt_int.Rows(0).Item("vendedor").ToString)

        Else
            Me.var_id = -1
        End If
    End Sub

    Public Shared Function Lista(Optional ByVal var_filtro As String = "") As DataTable
        Dim obj_Conex_int As New SqlConnection(ConfigurationManager.ConnectionStrings("connection").ConnectionString)
        Dim obj_dt_int As DataTable = Abrir_Tabla(obj_Conex_int, "select " & cls_empleados.Campo_Id & " as value, " & cls_empleados.campo_lista & " as label from " & cls_empleados.Nombre_Tabla & IIf(var_filtro <> "", " where " & var_filtro, "") & " order by " & cls_empleados.Campo_Validacion & "")
        obj_dt_int.Rows.Add(0, "Seleccione una opción")
        Return obj_dt_int
    End Function

    Public Function Actualizar(ByRef var_msj As String, ByVal var_id_usuario As Integer) As Boolean
        If Validar_Existe(Me.obj_Conex_int, Me.var_Nombre_Tabla, Me.var_Campo_Validacion, Me.var_cedula, Me.var_Campo_Id, Me.var_id) Then
            If var_msj = "" Then
                var_msj = Me.var_cedula & "' ya existe en la base de datos"
            End If
            Return False
            Exit Function
        End If

        Dim var_error As String = ""
        If Me.var_id = 0 Then   'NUEVO
            If Not Ingresar(Me.obj_Conex_int, Me.var_Nombre_Tabla, Me.var_Campos, Sql_Texto(Me.var_nombre) & "," & Sql_Texto(Me.var_apellido) & "," & Sql_Texto(Me.var_cedula) & "," & Sql_Texto(Me.var_fecha_nacimiento) & "," & Sql_Texto(Me.var_email) & "," & Sql_Texto(Me.var_cuenta_nomina) & "," & Sql_Texto(Me.var_id_cargo) & "," & Sql_Texto(Me.var_direccion) & "," & Sql_Texto(Me.var_telefono) & "," & Sql_Texto(Me.var_sueldo) & "," & Sql_Texto(Me.var_fecha_ingreso) & "," & Sql_Texto(Me.var_fecha_egreso) & "," & Sql_Texto(Me.var_id_cuenta_bancaria_pago) & "," & Sql_Texto(Me.var_numero_fideicomiso) & "," & Sql_Texto(Me.var_id_horario) & "," & Sql_Texto(Me.var_tipo_trabajo) & "," & Sql_Texto(Me.var_foto) & "," & Sql_Texto(Me.var_tipo_nomina) & "," & Sql_Texto(Me.var_estatus) & "," & Sql_Texto(Me.var_id_usuario_reg.Id) & "," & Sql_Texto(Me.var_fecha_reg) & "," & Sql_Texto(Me.var_sexo) & "," & Sql_Texto(Me.var_monto_poliza) & "," & Sql_Texto(Me.var_numero_cestaTicket) & "," & Sql_Texto(Me.var_vendedor), var_error) Then
                var_msj = var_error

                Dim obj_log As New cls_logs
                obj_log.ComentarioLog = "Error agregando empleado '" & Me.var_nombre & " - " & Me.var_cedula & "': " & var_error
                obj_log.FechaLog = Now
                obj_log.id_Menu = New cls_modulos("empleado").Id
                obj_log.id_Usuario = var_id_usuario
                obj_log.idAccion = New cls_acciones("agregar").Id
                obj_log.ResultadoLog = False
                obj_log.InsertarLog()

                Return False
                Exit Function
            Else
                Me.var_id = Valor_De(Me.obj_Conex_int, "select " & Me.var_Campo_Id & " from " & Me.var_Nombre_Tabla & " Where id_usuario_reg=" & var_id_usuario & " order by id desc")

                Dim obj_sb As New StringBuilder
                obj_sb.Append(Chr(34) & "Id" & Chr(34) & ":" & Chr(34) & Me.Id & Chr(34) & "")
                obj_sb.Append("," & Chr(34) & "Nombre" & Chr(34) & ":" & Chr(34) & Me.Nombre & Chr(34) & "")
                obj_sb.Append("," & Chr(34) & "Apellido" & Chr(34) & ":" & Chr(34) & Me.Apellido & Chr(34) & "")
                obj_sb.Append("," & Chr(34) & "Cedula" & Chr(34) & ":" & Chr(34) & Me.cedula & Chr(34) & "")
                obj_sb.Append("," & Chr(34) & "FechaNacimiento" & Chr(34) & ":" & Chr(34) & Me.Fecha_nacimiento & Chr(34) & "")
                obj_sb.Append("," & Chr(34) & "Email" & Chr(34) & ":" & Chr(34) & Me.Email & Chr(34) & "")
                obj_sb.Append("," & Chr(34) & "CuentaNomina" & Chr(34) & ":" & Chr(34) & Me.Cuenta_nomina & Chr(34) & "")
                obj_sb.Append("," & Chr(34) & "Cargo" & Chr(34) & ":" & Chr(34) & Me.Cargo & Chr(34) & "")
                obj_sb.Append("," & Chr(34) & "Direccion" & Chr(34) & ":" & Chr(34) & Me.Direccion & Chr(34) & "")
                obj_sb.Append("," & Chr(34) & "Telefono" & Chr(34) & ":" & Chr(34) & Me.Telefono & Chr(34) & "")
                obj_sb.Append("," & Chr(34) & "Sueldo" & Chr(34) & ":" & Chr(34) & Me.Sueldo & Chr(34) & "")
                obj_sb.Append("," & Chr(34) & "FechaIngreso" & Chr(34) & ":" & Chr(34) & Me.Fecha_ingreso & Chr(34) & "")
                obj_sb.Append("," & Chr(34) & "FechaEgreso" & Chr(34) & ":" & Chr(34) & Me.Fecha_egreso & Chr(34) & "")
                obj_sb.Append("," & Chr(34) & "FechaEgreso" & Chr(34) & ":" & Chr(34) & Me.Fecha_egreso & Chr(34) & "")
                obj_sb.Append("," & Chr(34) & "CuentaBancariaPago" & Chr(34) & ":" & Chr(34) & Me.Cuenta_bancaria_pago & Chr(34) & "")
                obj_sb.Append("," & Chr(34) & "Sueldo" & Chr(34) & ":" & Chr(34) & Me.Sueldo & Chr(34) & "")
                obj_sb.Append("," & Chr(34) & "NumeroFideicomiso" & Chr(34) & ":" & Chr(34) & Me.Numero_fideicomiso & Chr(34) & "")
                obj_sb.Append("," & Chr(34) & "TipoTrabajo" & Chr(34) & ":" & Chr(34) & Me.Tipo_trabajo & Chr(34) & "")
                obj_sb.Append("," & Chr(34) & "Foto" & Chr(34) & ":" & Chr(34) & Me.Sueldo & Chr(34) & "")
                obj_sb.Append("," & Chr(34) & "TipoNomina" & Chr(34) & ":" & Chr(34) & Me.Tipo_nomina & Chr(34) & "")
                obj_sb.Append("," & Chr(34) & "Estatus" & Chr(34) & ":" & Chr(34) & Me.Estatus & Chr(34) & "")
                obj_sb.Append("," & Chr(34) & "Sexo" & Chr(34) & ":" & Chr(34) & Me.Sexo & Chr(34) & "")
                obj_sb.Append("," & Chr(34) & "NumeroCestaticket" & Chr(34) & ":" & Chr(34) & Me.Numero_cestaTicket & Chr(34) & "")
                obj_sb.Append("," & Chr(34) & "Vendedor" & Chr(34) & ":" & Chr(34) & Me.Vendedor & Chr(34) & "")

                Dim obj_log As New cls_logs
                obj_log.ComentarioLog = "empleado agregado: {" & obj_sb.ToString & "}"
                obj_log.FechaLog = Now
                obj_log.id_Menu = New cls_modulos("empleado").Id
                obj_log.id_Usuario = var_id_usuario
                obj_log.idAccion = New cls_acciones("agregar").Id
                obj_log.ResultadoLog = True
                obj_log.InsertarLog()

                Return True
            End If
        ElseIf Me.var_id > 0 Then 'EDICION
            If Not ac_Funciones.Actualizar(Me.obj_Conex_int, Me.var_Nombre_Tabla, "nombre=" & Sql_Texto(Me.var_nombre) & ",apellido=" & Sql_Texto(Me.var_apellido) & ",cedula=" & Sql_Texto(Me.var_cedula) & ",fecha_nacimiento=" & Sql_Texto(Me.var_fecha_nacimiento) & ",email=" & Sql_Texto(Me.var_email) & ",cuenta_nomina=" & Sql_Texto(Me.var_cuenta_nomina) & ",id_cargo=" & Sql_Texto(Me.var_id_cargo) & ",direccion=" & Sql_Texto(Me.var_direccion) & ",telefono=" & Sql_Texto(Me.var_telefono) & ",sueldo=" & Sql_Texto(Me.var_sueldo) & ",fecha_ingreso=" & Sql_Texto(Me.var_fecha_ingreso) & ",fecha_egreso=" & Sql_Texto(Me.var_fecha_egreso) & ",id_cuenta_bancaria_pago=" & Sql_Texto(Me.var_id_cuenta_bancaria_pago) & ",numero_fideicomiso=" & Sql_Texto(Me.var_numero_fideicomiso) & ",id_horario=" & Sql_Texto(Me.var_id_horario) & ",tipo_trabajo=" & Sql_Texto(Me.var_tipo_trabajo) & ",foto=" & Sql_Texto(Me.var_foto) & ",tipo_nomina=" & Sql_Texto(Me.var_tipo_nomina) & ",estatus=" & Sql_Texto(Me.var_estatus) & ",sexo=" & Sql_Texto(Me.var_sexo) & ",monto_poliza=" & Sql_Texto(Me.var_monto_poliza) & ",numero_cestaTicket=" & Sql_Texto(Me.var_numero_cestaTicket) & ",vendedor=" & Sql_Texto(Me.var_vendedor), Me.var_Campo_Id & "=" & Me.var_id, var_error) Then
                var_msj = var_error

                Dim obj_log As New cls_logs
                obj_log.ComentarioLog = "Error editando empleado '" & Me.var_id & " - " & Me.var_nombre & " - " & Me.var_cedula & "': " & var_error
                obj_log.FechaLog = Now
                obj_log.id_Menu = New cls_modulos("empleado").Id
                obj_log.id_Usuario = var_id_usuario
                obj_log.idAccion = New cls_acciones("editar").Id
                obj_log.ResultadoLog = False
                obj_log.InsertarLog()

                Return False
                Exit Function

            Else

                Dim obj_sb As New StringBuilder
                obj_sb.Append(Chr(34) & "Id" & Chr(34) & ":" & Chr(34) & Me.Id & Chr(34) & "")
                obj_sb.Append("," & Chr(34) & "Nombre" & Chr(34) & ":" & Chr(34) & Me.Nombre & Chr(34) & "")
                obj_sb.Append("," & Chr(34) & "Apellido" & Chr(34) & ":" & Chr(34) & Me.Apellido & Chr(34) & "")
                obj_sb.Append("," & Chr(34) & "Cedula" & Chr(34) & ":" & Chr(34) & Me.cedula & Chr(34) & "")
                obj_sb.Append("," & Chr(34) & "FechaNacimiento" & Chr(34) & ":" & Chr(34) & Me.Fecha_nacimiento & Chr(34) & "")
                obj_sb.Append("," & Chr(34) & "Email" & Chr(34) & ":" & Chr(34) & Me.Email & Chr(34) & "")
                obj_sb.Append("," & Chr(34) & "CuentaNomina" & Chr(34) & ":" & Chr(34) & Me.Cuenta_nomina & Chr(34) & "")
                obj_sb.Append("," & Chr(34) & "Cargo" & Chr(34) & ":" & Chr(34) & Me.Cargo & Chr(34) & "")
                obj_sb.Append("," & Chr(34) & "Direccion" & Chr(34) & ":" & Chr(34) & Me.Direccion & Chr(34) & "")
                obj_sb.Append("," & Chr(34) & "Telefono" & Chr(34) & ":" & Chr(34) & Me.Telefono & Chr(34) & "")
                obj_sb.Append("," & Chr(34) & "Sueldo" & Chr(34) & ":" & Chr(34) & Me.Sueldo & Chr(34) & "")
                obj_sb.Append("," & Chr(34) & "FechaIngreso" & Chr(34) & ":" & Chr(34) & Me.Fecha_ingreso & Chr(34) & "")
                obj_sb.Append("," & Chr(34) & "FechaEgreso" & Chr(34) & ":" & Chr(34) & Me.Fecha_egreso & Chr(34) & "")
                obj_sb.Append("," & Chr(34) & "FechaEgreso" & Chr(34) & ":" & Chr(34) & Me.Fecha_egreso & Chr(34) & "")
                obj_sb.Append("," & Chr(34) & "CuentaBancariaPago" & Chr(34) & ":" & Chr(34) & Me.Cuenta_bancaria_pago & Chr(34) & "")
                obj_sb.Append("," & Chr(34) & "Sueldo" & Chr(34) & ":" & Chr(34) & Me.Sueldo & Chr(34) & "")
                obj_sb.Append("," & Chr(34) & "NumeroFideicomiso" & Chr(34) & ":" & Chr(34) & Me.Numero_fideicomiso & Chr(34) & "")
                obj_sb.Append("," & Chr(34) & "TipoTrabajo" & Chr(34) & ":" & Chr(34) & Me.Tipo_trabajo & Chr(34) & "")
                obj_sb.Append("," & Chr(34) & "Foto" & Chr(34) & ":" & Chr(34) & Me.Sueldo & Chr(34) & "")
                obj_sb.Append("," & Chr(34) & "TipoNomina" & Chr(34) & ":" & Chr(34) & Me.Tipo_nomina & Chr(34) & "")
                obj_sb.Append("," & Chr(34) & "Estatus" & Chr(34) & ":" & Chr(34) & Me.Estatus & Chr(34) & "")
                obj_sb.Append("," & Chr(34) & "Sexo" & Chr(34) & ":" & Chr(34) & Me.Sexo & Chr(34) & "")
                obj_sb.Append("," & Chr(34) & "NumeroCestaticket" & Chr(34) & ":" & Chr(34) & Me.Numero_cestaTicket & Chr(34) & "")
                obj_sb.Append("," & Chr(34) & "Vendedor" & Chr(34) & ":" & Chr(34) & Me.Vendedor & Chr(34) & "")

                Dim obj_log As New cls_logs
                obj_log.ComentarioLog = "empleado editado: {" & obj_sb.ToString & "}"
                obj_log.FechaLog = Now
                obj_log.id_Menu = New cls_modulos("empleado").Id
                obj_log.id_Usuario = var_id_usuario
                obj_log.idAccion = New cls_acciones("editar").Id
                obj_log.ResultadoLog = True
                obj_log.InsertarLog()

                Return True
            End If
        Else
            var_msj = "No se encontro el Registro"
            Return False
        End If
    End Function

    Public Shared Function Eliminar(ByVal var_id As Integer, ByVal var_id_usuario As Integer, ByRef var_mensaje As String) As Boolean
        Dim obj_Conex_int As New SqlConnection(ConfigurationManager.ConnectionStrings("connection").ConnectionString)
        Dim var_error As String = ""
        Dim obj_empleado As New cls_empleados(var_id)
        If ac_Funciones.Eliminar(obj_Conex_int, cls_empleados.Nombre_Tabla, cls_empleados.Campo_Id & "=" & var_id) < 0 Then
            var_mensaje = var_error

            Dim obj_log As New cls_logs
            obj_log.ComentarioLog = "Error eliminando empleado '" & obj_empleado.Id & " - " & obj_empleado.Nombre & " - " & obj_empleado.cedula & "': " & var_error
            obj_log.FechaLog = Now
            obj_log.id_Menu = New cls_modulos("empleado").Id
            obj_log.id_Usuario = var_id_usuario
            obj_log.idAccion = New cls_acciones("eliminar").Id
            obj_log.ResultadoLog = False
            obj_log.InsertarLog()

            Return False
        Else

            Dim obj_log As New cls_logs
            obj_log.ComentarioLog = "empleado eliminado '" & obj_empleado.Id & " - " & obj_empleado.Nombre & " - " & obj_empleado.cedula
            obj_log.FechaLog = Now
            obj_log.id_Menu = New cls_modulos("empleado").Id
            obj_log.id_Usuario = var_id_usuario
            obj_log.idAccion = New cls_acciones("eliminar").Id
            obj_log.ResultadoLog = True
            obj_log.InsertarLog()

            Return True
        End If
    End Function

    Public Shared Function Consulta(ByRef var_cedula As String, ByRef var_error As String) As DataTable
        Dim obj_Conex_int As New SqlConnection(ConfigurationManager.ConnectionStrings("connection").ConnectionString)
        Dim var_consulta As String = "Select * from " & cls_empleados.Listado & "(" & Sql_Texto(var_cedula) & ")"
        Dim var_msj As String = ""

        Dim obj_dt_int As System.Data.DataTable = Abrir_Tabla(obj_Conex_int, var_consulta, var_msj)
        var_error = var_msj
        Return obj_dt_int
    End Function

    Public Shared Function ConsultaEgreso(ByRef var_cedula As String, ByRef var_error As String) As DataTable
        Dim obj_Conex_int As New SqlConnection(ConfigurationManager.ConnectionStrings("connection").ConnectionString)
        Dim var_consulta As String = "Select * from " & cls_empleados.ListadoEgreso & "(" & Sql_Texto(var_cedula) & ")"
        Dim var_msj As String = ""

        Dim obj_dt_int As System.Data.DataTable = Abrir_Tabla(obj_Conex_int, var_consulta, var_msj)
        var_error = var_msj
        Return obj_dt_int
    End Function

    Public Shared Function ConsultaNomina(ByRef var_filtro As String, ByRef var_error As String) As DataTable
        Dim obj_Conex_int As New SqlConnection(ConfigurationManager.ConnectionStrings("connection").ConnectionString)
        Dim var_consulta As String = "Select * from " & cls_empleados.ListadoNomina & "(" & var_filtro & ") where estatus=1"
        Dim var_msj As String = ""

        Dim obj_dt_int As System.Data.DataTable = Abrir_Tabla(obj_Conex_int, var_consulta, var_msj)
        var_error = var_msj
        Return obj_dt_int
    End Function

    Public Shared Function ConsultaNominaLiquidacion(ByRef var_filtro As String, empleado As Integer, ByRef var_error As String) As DataTable
        Dim obj_Conex_int As New SqlConnection(ConfigurationManager.ConnectionStrings("connection").ConnectionString)
        Dim var_consulta As String = "Select * from " & cls_empleados.ListadoNominaLiquidacion & "(" & var_filtro & "," & empleado & ") where estatus=1"
        Dim var_msj As String = ""

        Dim obj_dt_int As System.Data.DataTable = Abrir_Tabla(obj_Conex_int, var_consulta, var_msj)
        var_error = var_msj
        Return obj_dt_int
    End Function

    Public Shared Function Imagenes(ByVal var_id_e As Integer, ByRef var_error As String, Optional ByVal var_idsesion As String = "") As DataTable
        Dim obj_Conex_int As New SqlConnection(ConfigurationManager.ConnectionStrings("connection").ConnectionString)
        Dim var_consulta As String = ""
        If var_id_e > 0 Then
            var_consulta = "Select id,ruta from tbl_imagenes where id_empleado=" & var_id_e & ""
        Else
            var_consulta = "Select id,ruta from tbl_imagenes where sessionid=" & Sql_Texto(var_idsesion) & ""
        End If

        Dim var_msj As String = ""

        Dim obj_dt_int As System.Data.DataTable = Abrir_Tabla(obj_Conex_int, var_consulta, var_msj)
        var_error = var_msj
        Return obj_dt_int
    End Function

    Public Shared Function searchQuery(Optional ByVal searchFilter As String = "", Optional ByRef errorResult As String = "") As DataTable
        Dim obj_Conex_int As New SqlConnection(ConfigurationManager.ConnectionStrings("connection").ConnectionString)
        Dim var_Query As String = "SELECT DT_RowId,Cedula,Nombre,Cargo FROM " & cls_empleados.Listado & "('')" & IIf(searchFilter.Trim.Length > 0, " WHERE Empleados LIKE '%" & searchFilter & "%'  OR Cedula LIKE '%" & searchFilter & "%'", "")
        Return Abrir_Tabla(obj_Conex_int, var_Query, errorResult)
    End Function

    Public Shared Function empleadoNomina(ByVal idempleado As Integer, Optional ByRef errorResult As String = "") As DataTable
        Dim obj_Connection As New SqlConnection(ConfigurationManager.ConnectionStrings("connection").ConnectionString)
        Dim var_Query As String = "SELECT * FROM rpt_NominaEmpleado(" & idempleado & ")"
        Return Abrir_Tabla(obj_Connection, var_Query, errorResult)
    End Function
#End Region

End Class

