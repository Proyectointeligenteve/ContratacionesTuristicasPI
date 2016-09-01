Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports ac_Funciones

Public Class cls_freelances
    Dim var_Nombre_Tabla As String = "tbl_freelances"
    Dim var_Campo_Id As String = "id"
    Dim var_Campo_Validacion As String = "rif"
    Dim var_Campos As String = "nombre,rif,direccion,telefono_fijo,telefono_movil,email,id_usuario_reg,fecha_reg,tipo_venta,codigo,limite_credito,id_pais,id_estado,id_ciudad"
    Dim obj_Conex_int As New SqlConnection(ConfigurationManager.ConnectionStrings("connection").ConnectionString)

    Dim var_id As Integer = 0
    Dim var_nombre As String = ""
    Dim var_rif As String = ""
    Dim var_direccion As String = ""
    Dim var_telefono_fijo As String = ""
    Dim var_telefono_movil As String = ""
    Dim var_email As String = ""
    Dim var_id_usuario_reg As Integer
    Dim var_fecha_reg As Date = Now
    Dim var_tipo_venta As Integer = 0
    Dim var_codigo As String = ""
    Dim var_limite_credito As Double = 0
    Dim var_pais As Integer = 0
    Dim var_estado As Integer = 0
    Dim var_ciudad As Integer = 0

    Dim obj_comisiones As New Generic.List(Of cls_freelances_configuracion)

    Public Shared ReadOnly Property Nombre_Tabla() As String
        Get
            Return "tbl_freelances"
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
            Return "frm_freelances.aspx"
        End Get
    End Property

    Public Shared ReadOnly Property ListadoActivos() As String
        Get
            Return "lst_freelancesActivos"
        End Get
    End Property

    Public Shared ReadOnly Property ListadoAnulados() As String
        Get
            Return "lst_freelancesAnulados"
        End Get
    End Property
    Public Shared ReadOnly Property Listado() As String
        Get
            Return "lst_freelances"
        End Get
    End Property

    Public ReadOnly Property Comisiones() As Generic.List(Of cls_freelances_configuracion)
        Get
            Return Me.obj_comisiones
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
    Public Property rif() As String
        Get
            Return Me.var_rif
        End Get
        Set(ByVal value As String)
            Me.var_rif = value
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
    Public Property telefono_fijo() As String
        Get
            Return Me.var_telefono_fijo
        End Get
        Set(ByVal value As String)
            Me.var_telefono_fijo = value
        End Set
    End Property
    Public Property telefono_movil() As String
        Get
            Return Me.var_telefono_movil
        End Get
        Set(ByVal value As String)
            Me.var_telefono_movil = value
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
    Public Property tipo_venta() As Integer
        Get
            Return Me.var_tipo_venta
        End Get
        Set(ByVal value As Integer)
            Me.var_tipo_venta = value
        End Set
    End Property

    Public Property codigo() As String
        Get
            Return Me.var_codigo
        End Get
        Set(ByVal value As String)
            Me.var_codigo = value
        End Set
    End Property
    Public Property limite_credito() As Double
        Get
            Return Me.var_limite_credito
        End Get
        Set(ByVal value As Double)
            Me.var_limite_credito = value
        End Set
    End Property

    Public Property pais() As Integer
        Get
            Return Me.var_pais
        End Get
        Set(ByVal value As Integer)
            Me.var_pais = value
        End Set
    End Property

    Public Property estado() As Integer
        Get
            Return Me.var_estado
        End Get
        Set(ByVal value As Integer)
            Me.var_estado = value
        End Set
    End Property

    Public Property ciudad() As Integer
        Get
            Return Me.var_ciudad
        End Get
        Set(ByVal value As Integer)
            Me.var_ciudad = value
        End Set
    End Property

    Sub New(Optional ByVal var_Id_int As Integer = 0)

        If var_Id_int > 0 Then
            Cargar(var_Id_int)
        End If

    End Sub

    'Sub New(ByVal var_email As String)
    '    Cargar(var_email)
    'End Sub

    Public Sub Cargar(ByVal var_id_int As Integer)
        Dim obj_dt_int As New DataTable
        obj_dt_int = Abrir_Tabla(Me.obj_Conex_int, "Select * from " & Me.var_Nombre_Tabla & " WHERE " & Me.var_Campo_Id & "=" & var_id_int)
        If obj_dt_int.Rows.Count > 0 Then
            Me.var_id = ac_Funciones.formato_Numero(obj_dt_int.Rows(0).Item("id").ToString)
            Me.var_nombre = ac_Funciones.formato_Texto(obj_dt_int.Rows(0).Item("nombre").ToString)
            Me.var_rif = ac_Funciones.formato_Texto(obj_dt_int.Rows(0).Item("rif").ToString)
            Me.var_direccion = ac_Funciones.formato_Texto(obj_dt_int.Rows(0).Item("direccion").ToString)
            Me.var_telefono_fijo = ac_Funciones.formato_Texto(obj_dt_int.Rows(0).Item("telefono_fijo").ToString)
            Me.var_telefono_movil = ac_Funciones.formato_Texto(obj_dt_int.Rows(0).Item("telefono_movil").ToString)
            Me.var_email = ac_Funciones.formato_Texto(obj_dt_int.Rows(0).Item("email").ToString)
            Me.var_id_usuario_reg = ac_Funciones.formato_Numero(obj_dt_int.Rows(0).Item("id_usuario_reg").ToString)
            Me.var_fecha_reg = ac_Funciones.formato_Fecha(obj_dt_int.Rows(0).Item("fecha_reg").ToString)
            Me.var_tipo_venta = ac_Funciones.formato_Numero(obj_dt_int.Rows(0).Item("tipo_venta").ToString)
            Me.var_codigo = ac_Funciones.formato_Texto(obj_dt_int.Rows(0).Item("codigo").ToString)
            Me.var_limite_credito = ac_Funciones.formato_Numero(obj_dt_int.Rows(0).Item("limite_credito").ToString, True)
            Me.var_pais = ac_Funciones.formato_Numero(obj_dt_int.Rows(0).Item("id_pais").ToString)
            Me.var_estado = ac_Funciones.formato_Numero(obj_dt_int.Rows(0).Item("id_estado").ToString)
            Me.var_ciudad = ac_Funciones.formato_Numero(obj_dt_int.Rows(0).Item("id_ciudad").ToString)

            Dim obj_dt_comisiones As DataTable = ac_Funciones.Abrir_Tabla(Me.obj_Conex_int, "select id from " & cls_freelances_configuracion.Nombre_Tabla & " where id_freelance=" & Me.var_id)
            For i As Integer = 0 To obj_dt_comisiones.Rows.Count - 1
                Me.obj_comisiones.Add(New cls_freelances_configuracion(obj_dt_comisiones.Rows(i).Item(0)))
            Next
        Else
            Me.var_id = 0
        End If
    End Sub

    Public Function Actualizar(ByRef var_msj As String, ByVal var_id_usuario As Integer) As Boolean
        If Validar_Existe(Me.obj_Conex_int, Me.var_Nombre_Tabla, Me.var_Campo_Validacion, Me.var_nombre, Me.var_Campo_Id, Me.var_id) Then
            If var_msj = "" Then
                var_msj = "El freelance '" & Me.var_nombre & "' ya existe en la base de datos"
            End If
            Return False
            Exit Function
        End If

        Dim var_error As String = ""
        If Me.var_id = 0 Then   'NUEVO
            If Not Ingresar(Me.obj_Conex_int, Me.var_Nombre_Tabla, Me.var_Campos, Sql_Texto(Me.var_nombre) & "," & Sql_Texto(Me.var_rif) & "," & Sql_Texto(Me.var_direccion) & "," & Sql_Texto(Me.var_telefono_fijo) & "," & Sql_Texto(Me.var_telefono_movil) & "," & Sql_Texto(Me.var_email) & "," & Sql_Texto(Me.var_id_usuario_reg) & "," & Sql_Texto(Me.var_fecha_reg) & "," & Sql_Texto(Me.var_tipo_venta) & "," & Sql_Texto(Me.var_codigo) & "," & Sql_Texto(Me.var_limite_credito) & "," & Sql_Texto(Me.var_pais) & "," & Sql_Texto(Me.var_estado) & "," & Sql_Texto(Me.var_ciudad), var_error) Then
                Dim obj_log As New cls_logs
                obj_log.ComentarioLog = "Error agregando freelance '" & Me.var_nombre & "': " & var_error
                obj_log.FechaLog = Now
                obj_log.id_Menu = New cls_modulos("freelances").Id
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
                obj_sb.Append("," & Chr(34) & "nombre" & Chr(34) & ":" & Chr(34) & Me.nombre & Chr(34) & "")
                obj_sb.Append("," & Chr(34) & "rif" & Chr(34) & ":" & Chr(34) & Me.nombre & Chr(34) & "")
                obj_sb.Append("," & Chr(34) & "direccion" & Chr(34) & ":" & Chr(34) & Me.nombre & Chr(34) & "")
                obj_sb.Append("," & Chr(34) & "telefono_fijo" & Chr(34) & ":" & Chr(34) & Me.nombre & Chr(34) & "")
                obj_sb.Append("," & Chr(34) & "telefono_movil" & Chr(34) & ":" & Chr(34) & Me.nombre & Chr(34) & "")
                obj_sb.Append("," & Chr(34) & "email" & Chr(34) & ":" & Chr(34) & Me.nombre & Chr(34) & "")
                obj_sb.Append("," & Chr(34) & "tipo_venta" & Chr(34) & ":" & Chr(34) & Me.tipo_venta & Chr(34) & "")
                obj_sb.Append("," & Chr(34) & "limite_credito" & Chr(34) & ":" & Chr(34) & Me.limite_credito & Chr(34) & "")
                obj_sb.Append("," & Chr(34) & "pais" & Chr(34) & ":" & Chr(34) & Me.pais & Chr(34) & "")
                obj_sb.Append("," & Chr(34) & "estado" & Chr(34) & ":" & Chr(34) & Me.estado & Chr(34) & "")
                obj_sb.Append("," & Chr(34) & "ciudad" & Chr(34) & ":" & Chr(34) & Me.ciudad & Chr(34) & "")

                For i As Integer = 0 To obj_comisiones.Count - 1
                    obj_comisiones.Item(i).freelance = Me.Id
                    Dim var_msj3 As String = ""
                    If Not obj_comisiones.Item(i).Actualizar(var_error) Then
                        var_msj &= " - " & var_msj3
                    End If
                Next
                Return True
            End If
        ElseIf Me.var_id > 0 Then 'EDICION
            If Not ac_Funciones.Actualizar(Me.obj_Conex_int, Me.var_Nombre_Tabla, "nombre=" & Sql_Texto(Me.var_nombre) & ",rif=" & Sql_Texto(Me.var_rif) & ",telefono_fijo=" & Sql_Texto(Me.var_telefono_fijo) & ",telefono_movil=" & Sql_Texto(Me.var_telefono_movil) & ",email=" & Sql_Texto(Me.var_email) & ",tipo_venta=" & Sql_Texto(Me.var_tipo_venta) & ",codigo=" & Sql_Texto(Me.var_codigo) & ",limite_credito=" & Sql_Texto(Me.var_limite_credito) & ",id_pais=" & Sql_Texto(Me.var_pais) & ",id_estado=" & Sql_Texto(Me.var_estado) & ",id_ciudad=" & Sql_Texto(Me.var_ciudad), Me.var_Campo_Id & "=" & Me.var_id) Then
                var_msj = var_error

                Dim obj_log As New cls_logs
                obj_log.ComentarioLog = "Error editando freelance '" & Me.var_id & " - " & Me.var_nombre & "': " & var_error
                obj_log.FechaLog = Now
                obj_log.id_Menu = New cls_modulos("freelances").Id
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
                obj_sb.Append("," & Chr(34) & "rif" & Chr(34) & ":" & Chr(34) & Me.nombre & Chr(34) & "")
                obj_sb.Append("," & Chr(34) & "direccion" & Chr(34) & ":" & Chr(34) & Me.nombre & Chr(34) & "")
                obj_sb.Append("," & Chr(34) & "telefono_fijo" & Chr(34) & ":" & Chr(34) & Me.nombre & Chr(34) & "")
                obj_sb.Append("," & Chr(34) & "telefono_movil" & Chr(34) & ":" & Chr(34) & Me.nombre & Chr(34) & "")
                obj_sb.Append("," & Chr(34) & "email" & Chr(34) & ":" & Chr(34) & Me.nombre & Chr(34) & "")
                obj_sb.Append("," & Chr(34) & "tipo_venta" & Chr(34) & ":" & Chr(34) & Me.tipo_venta & Chr(34) & "")
                obj_sb.Append("," & Chr(34) & "limite_credito" & Chr(34) & ":" & Chr(34) & Me.limite_credito & Chr(34) & "")
                obj_sb.Append("," & Chr(34) & "pais" & Chr(34) & ":" & Chr(34) & Me.pais & Chr(34) & "")
                obj_sb.Append("," & Chr(34) & "estado" & Chr(34) & ":" & Chr(34) & Me.estado & Chr(34) & "")
                obj_sb.Append("," & Chr(34) & "ciudad" & Chr(34) & ":" & Chr(34) & Me.ciudad & Chr(34) & "")

                Dim obj_log As New cls_logs
                obj_log.ComentarioLog = "Freelance editada: {" & obj_sb.ToString & "}"
                obj_log.FechaLog = Now
                obj_log.id_Menu = New cls_modulos("freelances").Id
                obj_log.id_Usuario = var_id_usuario_reg
                obj_log.idAccion = New cls_acciones("editar").Id
                obj_log.ResultadoLog = True
                obj_log.InsertarLog()

                Dim var_ids2 As String = ""
                For i As Integer = 0 To Me.obj_comisiones.Count - 1
                    Me.obj_comisiones.Item(i).freelance = Me.Id
                    Dim var_msj3 As String = ""
                    Me.obj_comisiones.Item(i).Actualizar(var_msj3)
                    var_msj &= " - " & var_msj3
                    var_ids2 &= "," & Me.obj_comisiones.Item(i).Id

                Next
                If var_ids2.Trim.Length > 1 Then
                    ac_Funciones.Eliminar(Me.obj_Conex_int, cls_freelances_configuracion.Nombre_Tabla, "id_freelance=" & Me.var_id & " and id not in(" & var_ids2.Substring(1) & ")")
                Else
                    ac_Funciones.Eliminar(Me.obj_Conex_int, cls_freelances_configuracion.Nombre_Tabla, "id_freelance=" & Me.var_id)
                End If

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
        Dim obj_freelance As New cls_freelances(var_id)
        If Not ac_Funciones.Actualizar(obj_Conex_int, cls_freelances.Nombre_Tabla, "anulado= case when isnull(anulado,0)=0 then '1' else '0' end, fecha_anulacion=" & Sql_Texto(Now, True) & ", id_usuario_anulacion=" & var_id_usuario, cls_freelances.Campo_Id & "=" & var_id, var_error) Then
            var_mensaje = var_error

            Dim obj_log As New cls_logs
            obj_log.ComentarioLog = "Error anulando freelance '" & obj_freelance.Id & " - " & obj_freelance.nombre & "': " & var_error
            obj_log.FechaLog = Now
            obj_log.id_Menu = New cls_modulos("freelance").Id
            obj_log.id_Usuario = var_id_usuario
            obj_log.idAccion = New cls_acciones("anular").Id
            obj_log.ResultadoLog = False
            obj_log.InsertarLog()

            Return False
        Else

            Dim obj_log As New cls_logs
            obj_log.ComentarioLog = "freelance anulado '" & obj_freelance.Id & " - " & obj_freelance.nombre
            obj_log.FechaLog = Now
            obj_log.id_Menu = New cls_modulos("freelance").Id
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

        If ac_Funciones.Eliminar(obj_conex, cls_freelances.Nombre_Tabla, cls_freelances.Campo_Id & "=" & var_id) < 0 Then
            var_mensaje = var_error
            Return False
        Else
            Return True
        End If
    End Function

    Public Function ListaComisiones() As DataTable
        Dim obj_dt As New System.Data.DataTable
        obj_dt.Columns.Add("DT_RowId", GetType(System.Int32))
        obj_dt.Columns.Add("Freelance", GetType(System.String))
        obj_dt.Columns.Add("Tipo", GetType(System.String))
        obj_dt.Columns.Add("Tipo2", GetType(System.String))
        obj_dt.Columns.Add("Comision", GetType(System.String))
        For i As Integer = 0 To Me.Comisiones.Count - 1
            obj_dt.Rows.Add(i + 1, Me.obj_comisiones(i).freelance, New cls_tipos_configuracion(Me.obj_comisiones(i).Tipo).nombre, IIf(Me.obj_comisiones(i).Tipo = 1, New cls_aerolineas(Me.obj_comisiones(i).Aerolinea.Id).nombre, IIf(Me.obj_comisiones(i).Tipo = 2, Me.obj_comisiones(i).Hotel.nombre, IIf(Me.obj_comisiones(i).Tipo = 3, Me.obj_comisiones(i).Vehiculo.Nombre, ""))), Me.obj_comisiones(i).Comision & IIf(Me.obj_comisiones(i).Forma.ToString = 1, " Bs", " %"))
        Next

        Return obj_dt
    End Function

    Public Shared Function Lista(Optional ByVal var_filtro As String = "") As DataTable
        Dim obj_Conex_int As New SqlConnection(ConfigurationManager.ConnectionStrings("connection").ConnectionString)
        Dim obj_dt_int As DataTable = Abrir_Tabla(obj_Conex_int, "select " & cls_freelances.Campo_Id & " as id, " & cls_freelances.Campo_Validacion & " as des from " & cls_freelances.Nombre_Tabla & IIf(var_filtro <> "", " where " & var_filtro, "") & " order by " & cls_freelances.Campo_Validacion & "")
        obj_dt_int.Rows.Add(0, "Seleccione una opción")
        Return obj_dt_int
    End Function

    Public Shared Function ConsultaFreelance(Optional ByVal var_filtro As String = "", Optional ByVal var_orden As String = "", Optional ByRef var_error As String = "") As DataTable
        Dim obj_Conex_int As New SqlConnection(ConfigurationManager.ConnectionStrings("connection").ConnectionString)
        Dim var_consulta As String = "Select * from " & cls_freelances.Listado & "(" & Sql_Texto(var_filtro) & ")"
        Return Abrir_Tabla(obj_Conex_int, var_consulta, var_error)
    End Function

    Public Shared Function ConsultaActivos(ByRef var_error As String) As DataTable
        Dim obj_Conex_int As New SqlConnection(ConfigurationManager.ConnectionStrings("connection").ConnectionString)
        Dim var_consulta As String = "Select * from " & cls_freelances.ListadoActivos & "()"
        Dim var_msj As String = ""

        Dim obj_dt_int As System.Data.DataTable = Abrir_Tabla(obj_Conex_int, var_consulta, var_msj)
        var_error = var_msj
        Return obj_dt_int
    End Function

    Public Shared Function ConsultaAnulados(ByRef var_error As String) As DataTable
        Dim obj_Conex_int As New SqlConnection(ConfigurationManager.ConnectionStrings("connection").ConnectionString)
        Dim var_consulta As String = "Select * from " & cls_freelances.ListadoAnulados & "()"
        Dim var_msj As String = ""

        Dim obj_dt_int As System.Data.DataTable = Abrir_Tabla(obj_Conex_int, var_consulta, var_msj)
        var_error = var_msj
        Return obj_dt_int
    End Function

    Public Shared Function Consulta(ByRef var_error As String) As DataTable
        Dim obj_Conex_int As New SqlConnection(ConfigurationManager.ConnectionStrings("connection").ConnectionString)
        Dim var_consulta As String = "Select * from " & cls_freelances.Listado & "()"
        Dim var_msj As String = ""

        Dim obj_dt_int As System.Data.DataTable = Abrir_Tabla(obj_Conex_int, var_consulta, var_msj)
        var_error = var_msj
        Return obj_dt_int
    End Function

    Public Shared Function SiguienteNumero() As Integer
        Dim obj_Connection As New SqlConnection(ConfigurationManager.ConnectionStrings("connection").ConnectionString)
        Return ac_Funciones.formato_Numero(ac_Funciones.Valor_De(obj_Connection, "select isnull(max(right(num,4)),0) from (select case when cast(right(codigo,4) as int)=1 then cast(right(codigo,4) as int) else 0 end as num from tbl_freelances) as c").ToString)
    End Function
End Class
