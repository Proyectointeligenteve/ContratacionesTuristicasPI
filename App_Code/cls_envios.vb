Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports ac_Funciones

Public Class cls_envios
#Region "VARIABLES"
    Dim var_Nombre_Tabla As String = "tbl_envios"
    Dim var_Campo_Id As String = "id"
    Dim var_Campo_Validacion As String = "codigo"
    Dim var_Campos As String = "codigo,id_cliente_emisor,id_cliente_receptor,id_pais_origen,id_estado_origen,id_ciudad_origen,id_pais_destino,id_estado_destino,id_estado_destino,id_ciudad_destino,direccion_destino,costo_envio,estatus,id_usuario_reg,fecha_reg"
    Dim obj_Conex_int As New SqlConnection(ConfigurationManager.ConnectionStrings("connection").ConnectionString)

    Dim var_id As Integer = 0
    Dim var_codigo As String = ""
    Dim var_id_cliente_emisor As Integer = 0
    Dim var_id_cliente_receptor As Integer = 0
    Dim var_id_pais_origen As Integer = 0
    Dim var_id_estado_origen As Integer = 0
    Dim var_id_ciudad_origen As Integer = 0
    Dim var_id_pais_destino As Integer = 0
    Dim var_id_estado_destino As Integer = 0
    Dim var_id_ciudad_destino As Integer = 0
    Dim var_direccion_destino As String = ""
    Dim var_costo_envio As Double = 0
    'Estatus: 0 - por entregar, 1 - en transito, 2 - entregado, 3 - extraviado
    Dim var_estatus As Integer = 0
    Dim var_id_usuario_reg As Integer
    Dim var_fecha_reg As Date = Now

    Dim obj_paquetes As New Generic.List(Of cls_envios_paquetes)
#End Region
#Region "PROPIEDADES"
    Public Shared ReadOnly Property Nombre_Tabla() As String
        Get
            Return "tbl_envios"
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
            Return "identificador"
        End Get
    End Property
    Public Shared ReadOnly Property Formulario() As String
        Get
            Return "frm_envios.aspx"
        End Get
    End Property
    Public Shared ReadOnly Property ListadoPorEntregar() As String
        Get
            Return "lst_enviosPorEntregar"
        End Get
    End Property
    Public Shared ReadOnly Property ListadoTransito() As String
        Get
            Return "lst_enviosTransito"
        End Get
    End Property
    Public Shared ReadOnly Property ListadoEntregados() As String
        Get
            Return "lst_enviosEntregados"
        End Get
    End Property
    Public Shared ReadOnly Property ListadoExtraviados() As String
        Get
            Return "lst_enviosExtraviados"
        End Get
    End Property
    Public Shared ReadOnly Property Listado() As String
        Get
            Return "lst_envios"
        End Get
    End Property
    Public ReadOnly Property Paquete() As Generic.List(Of cls_envios_paquetes)
        Get
            Return Me.obj_paquetes
        End Get
    End Property
    Public ReadOnly Property Id() As Integer
        Get
            Return Me.var_id
        End Get
    End Property
    Public Property codigo() As String
        Get
            Return Me.var_codigo
        End Get
        Set(ByVal value As String)
            Me.var_codigo = value
        End Set
    End Property
    Public Property id_cliente_emisor() As Integer
        Get
            Return Me.var_id_cliente_emisor
        End Get
        Set(ByVal value As Integer)
            Me.var_id_cliente_emisor = value
        End Set
    End Property
    Public Property id_cliente_receptor() As Integer
        Get
            Return Me.var_id_cliente_receptor
        End Get
        Set(ByVal value As Integer)
            Me.var_id_cliente_receptor = value
        End Set
    End Property
    Public Property id_pais_origen() As Integer
        Get
            Return Me.var_id_pais_origen
        End Get
        Set(ByVal value As Integer)
            Me.var_id_pais_origen = value
        End Set
    End Property
    Public Property id_estado_origen() As Integer
        Get
            Return Me.var_id_estado_origen
        End Get
        Set(ByVal value As Integer)
            Me.var_id_estado_origen = value
        End Set
    End Property
    Public Property id_ciudad_origen() As Integer
        Get
            Return Me.var_id_ciudad_origen
        End Get
        Set(ByVal value As Integer)
            Me.var_id_ciudad_origen = value
        End Set
    End Property
    Public Property id_pais_destino() As Integer
        Get
            Return Me.var_id_pais_destino
        End Get
        Set(ByVal value As Integer)
            Me.var_id_pais_destino = value
        End Set
    End Property
    Public Property id_estado_destino() As Integer
        Get
            Return Me.var_id_estado_destino
        End Get
        Set(ByVal value As Integer)
            Me.var_id_estado_destino = value
        End Set
    End Property
    Public Property id_ciudad_destino() As Integer
        Get
            Return Me.var_id_ciudad_destino
        End Get
        Set(ByVal value As Integer)
            Me.var_id_ciudad_destino = value
        End Set
    End Property
    Public Property direccion_destino() As String
        Get
            Return Me.var_direccion_destino
        End Get
        Set(ByVal value As String)
            Me.var_direccion_destino = value
        End Set
    End Property
    Public Property costo_envio() As Double
        Get
            Return Me.var_costo_envio
        End Get
        Set(ByVal value As Double)
            Me.var_costo_envio = value
        End Set
    End Property
    Public Property estatus() As Integer
        Get
            Return Me.var_estatus
        End Get
        Set(ByVal value As Integer)
            Me.var_estatus = value
        End Set
    End Property
    'Public Property anulado() As Boolean
    '    Get
    '        Return Me.var_anulado
    '    End Get
    '    Set(ByVal value As Boolean)
    '        Me.var_anulado = value
    '    End Set
    'End Property
    'Public Property id_usuario_anulacion() As Integer
    '    Get
    '        Return Me.var_id_usuario_anulacion
    '    End Get
    '    Set(ByVal value As Integer)
    '        Me.var_id_usuario_anulacion = value
    '    End Set
    'End Property
    'Public Property fecha_anulacion() As Date
    '    Get
    '        Return Me.var_fecha_anulacion
    '    End Get
    '    Set(ByVal value As Date)
    '        Me.var_fecha_anulacion = value
    '    End Set
    'End Property
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
            Me.var_id = ac_Funciones.formato_Numero(obj_dt_int.Rows(0).Item("id").ToString)
            Me.var_codigo = ac_Funciones.formato_Texto(obj_dt_int.Rows(0).Item("codigo").ToString)
            Me.var_id_cliente_emisor = ac_Funciones.formato_Numero(obj_dt_int.Rows(0).Item("id_cliente_emisor").ToString)
            Me.var_id_cliente_receptor = ac_Funciones.formato_Numero(obj_dt_int.Rows(0).Item("id_cliente_receptor").ToString)
            Me.var_id_pais_origen = ac_Funciones.formato_Numero(obj_dt_int.Rows(0).Item("id_pais_origen").ToString)
            Me.var_id_estado_origen = ac_Funciones.formato_Numero(obj_dt_int.Rows(0).Item("id_estado_origen").ToString)
            Me.var_id_ciudad_origen = ac_Funciones.formato_Numero(obj_dt_int.Rows(0).Item("id_ciudad_origen").ToString)
            Me.var_id_pais_destino = ac_Funciones.formato_Numero(obj_dt_int.Rows(0).Item("id_pais_destino").ToString)
            Me.var_id_estado_destino = ac_Funciones.formato_Numero(obj_dt_int.Rows(0).Item("id_estado_destino").ToString)
            Me.var_id_ciudad_destino = ac_Funciones.formato_Numero(obj_dt_int.Rows(0).Item("id_ciudad_destino").ToString)
            Me.var_direccion_destino = ac_Funciones.formato_Texto(obj_dt_int.Rows(0).Item("direccion_destino").ToString)
            Me.var_costo_envio = ac_Funciones.formato_Numero(obj_dt_int.Rows(0).Item("costo_envio").ToString, True)
            Me.var_estatus = ac_Funciones.formato_Numero(obj_dt_int.Rows(0).Item("estatus").ToString)
            Me.var_id_usuario_reg = ac_Funciones.formato_Numero(obj_dt_int.Rows(0).Item("id_usuario_reg").ToString)
            Me.var_fecha_reg = ac_Funciones.formato_Fecha(obj_dt_int.Rows(0).Item("fecha_reg").ToString)

            Dim obj_dt_paquete As DataTable = ac_Funciones.Abrir_Tabla(Me.obj_Conex_int, "select id from " & cls_envios_paquetes.Nombre_Tabla & " where id_envio=" & Me.var_id)
            For i As Integer = 0 To obj_dt_paquete.Rows.Count - 1
                Me.obj_paquetes.Add(New cls_envios_paquetes(obj_dt_paquete.Rows(i).Item(0)))
            Next
        Else
            Me.var_id = 0
        End If
    End Sub
    Public Function Actualizar(ByRef var_Error As String) As Boolean
        If Validar_Existe(Me.obj_Conex_int, Me.var_Nombre_Tabla, Me.var_Campo_Validacion, Me.var_codigo, Me.var_Campo_Id, Me.var_id) Then
            If var_Error = "" Then
                var_Error = "El envio '" & Me.var_codigo & "' ya existe en la base de datos"
            End If
            Return False
            Exit Function
        End If

        If Me.var_id = 0 Then   'NUEVO
            If Not Ingresar(Me.obj_Conex_int, Me.var_Nombre_Tabla, Me.var_Campos, Sql_Texto(Me.var_codigo) & "," & Sql_Texto(Me.var_id_cliente_emisor) & "," & Sql_Texto(Me.var_id_cliente_receptor) & "," & Sql_Texto(Me.var_id_pais_origen) & "," & Sql_Texto(Me.var_id_estado_origen) & "," & Sql_Texto(Me.var_id_ciudad_origen) & "," & Sql_Texto(Me.var_id_pais_destino) & "," & Sql_Texto(Me.var_id_estado_destino) & "," & Sql_Texto(Me.var_id_ciudad_destino) & "," & Sql_Texto(Me.var_direccion_destino) & "," & Sql_Texto(Me.var_costo_envio) & "," & Sql_Texto(Me.var_estatus) & "," & Sql_Texto(Me.var_id_usuario_reg) & "," & Sql_Texto(Me.var_fecha_reg), var_Error) Then
                Return False
                Exit Function
            End If
            Me.var_id = Valor_De(Me.obj_Conex_int, "select " & Me.var_Campo_Id & " from " & Me.var_Nombre_Tabla & " order by id desc")
            For i As Integer = 0 To obj_paquetes.Count - 1
                obj_paquetes.Item(i).id_envio = Me.Id
                Dim var_msj2 As String = ""
                If Not obj_paquetes.Item(i).Actualizar(var_Error) Then
                    var_Error &= " - " & var_msj2
                End If
            Next
            Return True
        ElseIf Me.var_id > 0 Then 'EDICION
            If Not ac_Funciones.Actualizar(Me.obj_Conex_int, Me.var_Nombre_Tabla, "codigo=" & Sql_Texto(Me.var_codigo) & ",id_cliente_emisor=" & Sql_Texto(Me.var_id_cliente_emisor) & ",id_cliente_receptor=" & Sql_Texto(Me.var_id_cliente_receptor) & ",id_pais_origen=" & Sql_Texto(Me.var_id_pais_origen) & ",id_estado_origen=" & Sql_Texto(Me.var_id_estado_origen) & ",id_ciudad_origen=" & Sql_Texto(Me.var_id_ciudad_origen) & ",id_pais_destino=" & Sql_Texto(Me.var_id_pais_destino) & ",id_estado_destino=" & Sql_Texto(Me.var_id_estado_destino) & ",id_ciudad_destino=" & Sql_Texto(Me.var_id_ciudad_destino) & ",direccion_destino=" & Sql_Texto(Me.var_direccion_destino) & ",costo_envio=" & Sql_Texto(Me.var_costo_envio) & ",estatus=" & Sql_Texto(Me.var_estatus), Me.var_Campo_Id & "=" & Me.var_id) Then
                Return False
                Exit Function
            End If
            Return True
        Else
            var_Error = "No se encontro el Registro"
            Return False
        End If
    End Function

    Public Function ListaPaquetes() As DataTable
        Dim obj_dt As New System.Data.DataTable
        obj_dt.Columns.Add("DT_RowId", GetType(System.Int32))
        obj_dt.Columns.Add("Id_envio", GetType(System.String))
        obj_dt.Columns.Add("Numero", GetType(System.String))
        obj_dt.Columns.Add("Peso", GetType(System.String))
        obj_dt.Columns.Add("Volumen", GetType(System.String))
        obj_dt.Columns.Add("Costo", GetType(System.String))
        obj_dt.Columns.Add("Descripcion", GetType(System.String))

        For i As Integer = 0 To Me.Paquete.Count - 1
            obj_dt.Rows.Add(i + 1, Me.obj_paquetes(i).id_envio, Me.obj_paquetes(i).numero, Me.obj_paquetes(i).peso, Me.obj_paquetes(i).volumen, Me.obj_paquetes(i).costo, Me.obj_paquetes(i).descripcion)
        Next

        Return obj_dt
    End Function

    Public Shared Function Anular(ByVal var_id As Integer, ByVal var_id_usuario As Integer, ByRef var_mensaje As String) As Boolean
        Dim obj_Conex_int As New SqlConnection(ConfigurationManager.ConnectionStrings("connection").ConnectionString)
        Dim var_error As String = ""
        Dim obj_envio As New cls_envios(var_id)
        If Not ac_Funciones.Actualizar(obj_Conex_int, cls_envios.Nombre_Tabla, "anulado= case when isnull(anulado,0)=0 then '1' else '0' end, fecha_anulacion=" & Sql_Texto(Now, True) & ", id_usuario_anulacion=" & var_id_usuario, cls_envios.Campo_Id & "=" & var_id, var_error) Then
            var_mensaje = var_error

            Dim obj_log As New cls_logs
            obj_log.ComentarioLog = "Error anulando envio '" & obj_envio.Id & " - " & obj_envio.codigo & "': " & var_error
            obj_log.FechaLog = Now
            obj_log.id_Menu = New cls_modulos("envio").Id
            obj_log.id_Usuario = var_id_usuario
            obj_log.idAccion = New cls_acciones("anular").Id
            obj_log.ResultadoLog = False
            obj_log.InsertarLog()

            Return False
        Else

            Dim obj_log As New cls_logs
            obj_log.ComentarioLog = "envio anulado '" & obj_envio.Id & " - " & obj_envio.codigo
            obj_log.FechaLog = Now
            obj_log.id_Menu = New cls_modulos("envio").Id
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

        If ac_Funciones.Eliminar(obj_conex, cls_envios.Nombre_Tabla, cls_envios.Campo_Id & "=" & var_id) < 0 Then
            var_mensaje = var_error
            Return False
        Else
            Return True
        End If
    End Function

    Public Shared Function Lista(Optional ByVal var_filtro As String = "") As DataTable
        Dim obj_Conex_int As New SqlConnection(ConfigurationManager.ConnectionStrings("connection").ConnectionString)
        Dim obj_dt_int As DataTable = Abrir_Tabla(obj_Conex_int, "select " & cls_envios.Campo_Id & " as id, " & cls_envios.Campo_Validacion & " as des from " & cls_envios.Nombre_Tabla & IIf(var_filtro <> "", " where " & var_filtro, "") & " order by " & cls_envios.Campo_Validacion & "")
        obj_dt_int.Rows.Add(0, "Seleccione una opción")
        Return obj_dt_int
    End Function

    Public Shared Function ConsultaPorEntregar(ByRef var_error As String) As DataTable
        Dim obj_Conex_int As New SqlConnection(ConfigurationManager.ConnectionStrings("connection").ConnectionString)
        Dim var_consulta As String = "Select * from " & cls_envios.ListadoPorEntregar & "()"
        Dim var_msj As String = ""

        Dim obj_dt_int As System.Data.DataTable = Abrir_Tabla(obj_Conex_int, var_consulta, var_msj)
        var_error = var_msj
        Return obj_dt_int
    End Function

    Public Shared Function ConsultaTransito(ByRef var_error As String) As DataTable
        Dim obj_Conex_int As New SqlConnection(ConfigurationManager.ConnectionStrings("connection").ConnectionString)
        Dim var_consulta As String = "Select * from " & cls_envios.ListadoTransito & "()"
        Dim var_msj As String = ""

        Dim obj_dt_int As System.Data.DataTable = Abrir_Tabla(obj_Conex_int, var_consulta, var_msj)
        var_error = var_msj
        Return obj_dt_int
    End Function
    Public Shared Function ConsultaEntregados(ByRef var_error As String) As DataTable
        Dim obj_Conex_int As New SqlConnection(ConfigurationManager.ConnectionStrings("connection").ConnectionString)
        Dim var_consulta As String = "Select * from " & cls_envios.ListadoEntregados & "()"
        Dim var_msj As String = ""

        Dim obj_dt_int As System.Data.DataTable = Abrir_Tabla(obj_Conex_int, var_consulta, var_msj)
        var_error = var_msj
        Return obj_dt_int
    End Function
    Public Shared Function ConsultaExtraviados(ByRef var_error As String) As DataTable
        Dim obj_Conex_int As New SqlConnection(ConfigurationManager.ConnectionStrings("connection").ConnectionString)
        Dim var_consulta As String = "Select * from " & cls_envios.ListadoExtraviados & "()"
        Dim var_msj As String = ""

        Dim obj_dt_int As System.Data.DataTable = Abrir_Tabla(obj_Conex_int, var_consulta, var_msj)
        var_error = var_msj
        Return obj_dt_int
    End Function

    Public Shared Function Consulta(ByRef var_error As String) As DataTable
        Dim obj_Conex_int As New SqlConnection(ConfigurationManager.ConnectionStrings("connection").ConnectionString)
        Dim var_consulta As String = "Select * from " & cls_envios.Listado & "()"
        Dim var_msj As String = ""

        Dim obj_dt_int As System.Data.DataTable = Abrir_Tabla(obj_Conex_int, var_consulta, var_msj)
        var_error = var_msj
        Return obj_dt_int
    End Function
#End Region
End Class
