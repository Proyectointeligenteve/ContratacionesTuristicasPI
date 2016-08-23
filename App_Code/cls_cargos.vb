
Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports ac_Funciones

Public Class cls_cargos

#Region "VARIABLES"
    Dim var_Nombre_Tabla As String = "tbl_cargos"
    Dim var_Campo_Id As String = "id"
    Dim var_Campo_Validacion As String = "nombre"
    Dim var_Campos As String = "nombre,id_usuario_registro,fecha_registro"
    Dim obj_Conex_int As New SqlConnection(ConfigurationManager.ConnectionStrings("CCconexion").ConnectionString)

    Dim var_id As Integer
    Dim var_nombre As String = ""
    Dim var_id_usuario_registro As Integer = 0
    Dim var_fecha_registro As Date = Now

    Dim obj_conceptos As New Generic.List(Of cls_cargos_conceptos)
#End Region

#Region "PROPIEDADES"
   
    Public Shared ReadOnly Property Nombre_Tabla() As String
        Get
            Return "tbl_cargos"
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
    Public Shared ReadOnly Property Listado() As String
        Get
            Return "lst_cargos"
        End Get
    End Property

    Public ReadOnly Property Conceptos() As Generic.List(Of cls_cargos_conceptos)
        Get
            Return Me.obj_conceptos
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

    Public Property id_usuario_reg() As Integer
        Get
            Return Me.var_id_usuario_registro
        End Get
        Set(ByVal value As Integer)
            Me.var_id_usuario_registro = value
        End Set
    End Property

    Public Property fecha_reg() As Date
        Get
            Return Me.var_fecha_registro
        End Get
        Set(ByVal value As Date)
            Me.var_fecha_registro = value
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
            Me.var_nombre = obj_dt_int.Rows(0).Item("nombre")
            Me.var_id_usuario_registro = ac_Funciones.formato_Numero(obj_dt_int.Rows(0).Item("id_usuario_registro").ToString)
            Me.var_fecha_registro = ac_Funciones.formato_Fecha(obj_dt_int.Rows(0).Item("fecha_registro").ToString)

            Dim obj_dt_conceptos As DataTable = ac_Funciones.Abrir_Tabla(Me.obj_Conex_int, "select id from " & cls_cargos_conceptos.Nombre_Tabla & " where id_cargo=" & Me.var_id)
            For i As Integer = 0 To obj_dt_conceptos.Rows.Count - 1
                Me.obj_conceptos.Add(New cls_cargos_conceptos(obj_dt_conceptos.Rows(i).Item(0)))
            Next
        Else
            Me.var_id = -1
        End If
    End Sub

    Public Shared Function Lista(Optional ByVal var_filtro As String = "") As DataTable
        Dim obj_Conex_int As New SqlConnection(ConfigurationManager.ConnectionStrings("CCconexion").ConnectionString)
        'Dim obj_dt_int As DataTable = Abrir_Tabla(obj_Conex_int, "select id, nombre as des from tbl_tipos_acciones" & IIf(var_filtro <> "", " where " & var_filtro, "") & " order by nombre")
        Dim obj_dt_int As DataTable = Abrir_Tabla(obj_Conex_int, "select " & cls_cargos.Campo_Id & " as id, " & cls_cargos.Campo_Validacion & " as des from " & cls_cargos.Nombre_Tabla & IIf(var_filtro <> "", " where " & var_filtro, "") & " order by " & cls_cargos.Campo_Validacion & "")
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
            If Not Ingresar(Me.obj_Conex_int, Me.var_Nombre_Tabla, Me.var_Campos, Sql_Texto(Me.var_nombre) & "," & Sql_Texto(Me.var_id_usuario_registro) & "," & Sql_Texto(Me.var_fecha_registro), var_error) Then
                var_msj = var_error
                Return False
                Exit Function
            End If
            Me.var_id = Valor_De(Me.obj_Conex_int, "select " & Me.var_Campo_Id & " from " & Me.var_Nombre_Tabla & " order by id desc")

            For i As Integer = 0 To obj_conceptos.Count - 1
                obj_conceptos.Item(i).Id_cargo = Me.Id
                Dim var_msj2 As String = ""
                If Not obj_conceptos.Item(i).Actualizar(var_error) Then
                    var_msj &= " - " & var_msj2
                End If
            Next
            Return True
        ElseIf Me.var_id > 0 Then 'EDICION
            If Not ac_Funciones.Actualizar(Me.obj_Conex_int, Me.var_Nombre_Tabla, "nombre=" & Sql_Texto(Me.var_nombre), Me.var_Campo_Id & "=" & Me.var_id) Then
                Return False
                Exit Function
            End If

            Dim var_ids As String = ""
            For i As Integer = 0 To Me.obj_conceptos.Count - 1
                Me.obj_conceptos.Item(i).Id_cargo = Me.Id
                Dim var_msj2 As String = ""
                Me.obj_conceptos.Item(i).Actualizar(var_msj2)
                var_msj &= " - " & var_msj2
                var_ids &= "," & Me.obj_conceptos.Item(i).Id

            Next
            If var_ids.Trim.Length > 1 Then
                ac_Funciones.Eliminar(Me.obj_Conex_int, cls_cargos_conceptos.Nombre_Tabla, "id_cargo=" & Me.var_id & " and id not in(" & var_ids.Substring(1) & ")")
            Else
                ac_Funciones.Eliminar(Me.obj_Conex_int, cls_cargos_conceptos.Nombre_Tabla, "id_cargo=" & Me.var_id)
            End If

            Return True
        Else
            var_error = "No se encontro el Registro"
            Return False
        End If
    End Function

    Public Function ListaConceptos() As DataTable
        Dim obj_dt As New System.Data.DataTable
        obj_dt.Columns.Add("DT_RowId", GetType(System.Int32))
        obj_dt.Columns.Add("Codigo", GetType(System.String))
        obj_dt.Columns.Add("Nombre", GetType(System.String))
        obj_dt.Columns.Add("Tipo", GetType(System.String))
        For i As Integer = 0 To Me.Conceptos.Count - 1
            'obj_dt.Rows.Add(i + 1, New cls_conceptos_nominas(Me.obj_conceptos(i).id_concepto).codigo, New cls_conceptos_nominas(Me.obj_conceptos(i).id_concepto).nombre, IIf((New cls_conceptos_nominas(Me.obj_conceptos(i).id_concepto).tipo), "Asignación", "Deducción"))
        Next

        Return obj_dt
    End Function
    Public Shared Function Eliminar(ByVal var_id As Integer, ByVal obj_usuario As cls_usuarios, ByRef var_mensaje As String) As Boolean
        Dim obj_Conex_int As New SqlConnection(ConfigurationManager.ConnectionStrings("CCconexion").ConnectionString)
        ac_Funciones.Eliminar(obj_Conex_int, cls_cargos.Nombre_Tabla, cls_cargos.Campo_Id & "=" & var_id)
        Return True
    End Function
    Public Shared Function Consulta(Optional ByVal var_filtro As String = "", Optional ByVal var_orden As String = "", Optional ByRef var_error As String = "") As DataTable
        Dim obj_Conex_int As New SqlConnection(ConfigurationManager.ConnectionStrings("CCconexion").ConnectionString)
        Dim var_consulta As String = "Select * from " & cls_cargos.Listado & "()"
        Return Abrir_Tabla(obj_Conex_int, var_consulta, var_error)
    End Function

#End Region
End Class

