﻿Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports ac_Funciones

Public Class cls_freelances
    Dim var_Nombre_Tabla As String = "tbl_freelances"
    Dim var_Campo_Id As String = "id"
    Dim var_Campo_Validacion As String = "rif"
    Dim var_Campos As String = "nombre,rif,direccion,telefono_fijo,telefono_movil,email,id_usuario_reg,fecha_reg"
    Dim obj_Conex_int As New SqlConnection(ConfigurationManager.ConnectionStrings("CCconexion").ConnectionString)

    Dim var_id As Integer = 0
    Dim var_nombre As String = ""
    Dim var_rif As String = ""
    Dim var_direccion As String = ""
    Dim var_telefono_fijo As String = ""
    Dim var_telefono_movil As String = ""
    Dim var_email As String = ""
    Dim var_id_usuario_reg As Integer
    Dim var_fecha_reg As Date = Now

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
            Me.var_id_usuario_reg = formato_Numero(obj_dt_int.Rows(0).Item("id_usuario_reg").ToString)
            Me.var_fecha_reg = formato_Fecha(obj_dt_int.Rows(0).Item("fecha_reg").ToString)
        Else
            Me.var_id = 0
        End If
    End Sub

    'Public Sub Cargar(ByVal var_email As String)
    '    Dim obj_dt_int As New DataTable
    '    obj_dt_int = Abrir_Tabla(Me.obj_Conex_int, "Select * from " & Me.var_Nombre_Tabla & " WHERE ltrim(rtrim(upper(email)))=" & Sql_Texto(var_email.ToUpper))
    '    If obj_dt_int.Rows.Count > 0 Then
    '        Me.var_id = ac_Funciones.formato_Numero(obj_dt_int.Rows(0).Item("id").ToString)
    '        Me.var_nombre = ac_Funciones.formato_Texto(obj_dt_int.Rows(0).Item("nombre").ToString)
    '        Me.var_id_usuario_reg = formato_Numero(obj_dt_int.Rows(0).Item("id_usuario_reg").ToString)
    '        Me.var_fecha_reg = formato_Fecha(obj_dt_int.Rows(0).Item("fecha_reg").ToString)
    '    Else
    '        Me.var_id = 0
    '    End If
    'End Sub

    Public Function Actualizar(ByRef var_Error As String) As Boolean
        If Validar_Existe(Me.obj_Conex_int, Me.var_Nombre_Tabla, Me.var_Campo_Validacion, Me.var_nombre, Me.var_Campo_Id, Me.var_id) Then
            If var_Error = "" Then
                var_Error = "El freelance '" & Me.var_nombre & "' ya existe en la base de datos"
            End If
            Return False
            Exit Function
        End If

        If Me.var_id = 0 Then   'NUEVO
            If Not Ingresar(Me.obj_Conex_int, Me.var_Nombre_Tabla, Me.var_Campos, Sql_Texto(Me.var_nombre) & "," & Sql_Texto(Me.var_rif) & "," & Sql_Texto(Me.var_direccion) & "," & Sql_Texto(Me.var_telefono_fijo) & "," & Sql_Texto(Me.var_telefono_movil) & "," & Sql_Texto(Me.var_email) & "," & Sql_Texto(Me.var_id_usuario_reg) & "," & Sql_Texto(Me.var_fecha_reg), var_Error) Then
                Return False
                Exit Function
            End If
            Me.var_id = Valor_De(Me.obj_Conex_int, "select " & Me.var_Campo_Id & " from " & Me.var_Nombre_Tabla & " where nombre=" & Sql_Texto(Me.var_nombre))
            Return True
        ElseIf Me.var_id > 0 Then 'EDICION
            If Not ac_Funciones.Actualizar(Me.obj_Conex_int, Me.var_Nombre_Tabla, "nombre=" & Sql_Texto(Me.var_nombre) & ",rif=" & Sql_Texto(Me.var_rif) & ",telefono_fijo=" & Sql_Texto(Me.var_telefono_fijo) & ",telefono_movil=" & Sql_Texto(Me.var_telefono_movil) & ",email=" & Sql_Texto(Me.var_email), Me.var_Campo_Id & "=" & Me.var_id) Then
                Return False
                Exit Function
            End If
            Return True
        Else
            var_Error = "No se encontro el Registro"
            Return False
        End If
    End Function

    Public Shared Function Anular(ByVal var_id As Integer, ByVal var_id_usuario As Integer, ByRef var_mensaje As String) As Boolean
        Dim obj_Conex_int As New SqlConnection(ConfigurationManager.ConnectionStrings("CCconexion").ConnectionString)
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
        Dim obj_conex As New SqlConnection(ConfigurationManager.ConnectionStrings("CCconexion").ConnectionString)
        Dim var_error As String = ""

        If ac_Funciones.Eliminar(obj_conex, cls_freelances.Nombre_Tabla, cls_freelances.Campo_Id & "=" & var_id) < 0 Then
            var_mensaje = var_error
            Return False
        Else
            Return True
        End If
    End Function

    Public Shared Function Lista(Optional ByVal var_filtro As String = "") As DataTable
        Dim obj_Conex_int As New SqlConnection(ConfigurationManager.ConnectionStrings("CCconexion").ConnectionString)
        Dim obj_dt_int As DataTable = Abrir_Tabla(obj_Conex_int, "select " & cls_freelances.Campo_Id & " as id, " & cls_freelances.Campo_Validacion & " as des from " & cls_freelances.Nombre_Tabla & IIf(var_filtro <> "", " where " & var_filtro, "") & " order by " & cls_freelances.Campo_Validacion & "")
        obj_dt_int.Rows.Add(0, "Seleccione una opción")
        Return obj_dt_int
    End Function

    Public Shared Function ConsultaActivos(ByRef var_error As String) As DataTable
        Dim obj_Conex_int As New SqlConnection(ConfigurationManager.ConnectionStrings("CCconexion").ConnectionString)
        Dim var_consulta As String = "Select * from " & cls_freelances.ListadoActivos & "()"
        Dim var_msj As String = ""

        Dim obj_dt_int As System.Data.DataTable = Abrir_Tabla(obj_Conex_int, var_consulta, var_msj)
        var_error = var_msj
        Return obj_dt_int
    End Function

    Public Shared Function ConsultaAnulados(ByRef var_error As String) As DataTable
        Dim obj_Conex_int As New SqlConnection(ConfigurationManager.ConnectionStrings("CCconexion").ConnectionString)
        Dim var_consulta As String = "Select * from " & cls_freelances.ListadoAnulados & "()"
        Dim var_msj As String = ""

        Dim obj_dt_int As System.Data.DataTable = Abrir_Tabla(obj_Conex_int, var_consulta, var_msj)
        var_error = var_msj
        Return obj_dt_int
    End Function

    Public Shared Function Consulta(ByRef var_error As String) As DataTable
        Dim obj_Conex_int As New SqlConnection(ConfigurationManager.ConnectionStrings("CCconexion").ConnectionString)
        Dim var_consulta As String = "Select * from " & cls_freelances.Listado & "()"
        Dim var_msj As String = ""

        Dim obj_dt_int As System.Data.DataTable = Abrir_Tabla(obj_Conex_int, var_consulta, var_msj)
        var_error = var_msj
        Return obj_dt_int
    End Function
End Class