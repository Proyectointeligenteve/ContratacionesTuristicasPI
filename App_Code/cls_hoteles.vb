﻿Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports ac_Funciones

Public Class cls_hoteles
    Dim var_Nombre_Tabla As String = "tbl_hoteles"
    Dim var_Campo_Id As String = "id"
    Dim var_Campo_Validacion As String = "identificador"
    Dim var_Campos As String = "nombre,identificador,direccion,telefono_fijo,telefono_movil,email,codigo,razon_social,id_usuario_reg,fecha_reg,comision"
    Dim obj_Conex_int As New SqlConnection(ConfigurationManager.ConnectionStrings("connection").ConnectionString)

    Dim var_id As Integer = 0
    Dim var_nombre As String = ""
    Dim var_identificador As String = ""
    Dim var_direccion As String = ""
    Dim var_telefono_fijo As String = ""
    Dim var_telefono_movil As String = ""
    Dim var_email As String = ""
    Dim var_codigo As String = ""
    Dim var_razon_social As String = ""
    Dim var_id_usuario_reg As Integer
    Dim var_fecha_reg As Date = Now
    Dim var_comision As Double = 0

    Dim obj_contactos As New Generic.List(Of cls_hoteles_contactos)

    Public Shared ReadOnly Property Nombre_Tabla() As String
        Get
            Return "tbl_hoteles"
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
            Return "frm_hoteles.aspx"
        End Get
    End Property

    Public Shared ReadOnly Property ListadoActivos() As String
        Get
            Return "lst_hotelesActivos"
        End Get
    End Property

    Public Shared ReadOnly Property ListadoAnulados() As String
        Get
            Return "lst_hotelesAnulados"
        End Get
    End Property
    Public Shared ReadOnly Property Listado() As String
        Get
            Return "lst_hoteles"
        End Get
    End Property
    Public ReadOnly Property Contacto() As Generic.List(Of cls_hoteles_contactos)
        Get
            Return Me.obj_contactos
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
    Public Property identificador() As String
        Get
            Return Me.var_identificador
        End Get
        Set(ByVal value As String)
            Me.var_identificador = value
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
    Public Property codigo() As String
        Get
            Return Me.var_codigo
        End Get
        Set(ByVal value As String)
            Me.var_codigo = value
        End Set
    End Property
    Public Property razon_social() As String
        Get
            Return Me.var_razon_social
        End Get
        Set(ByVal value As String)
            Me.var_razon_social = value
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
    Public Property comision() As Double
        Get
            Return Me.var_comision
        End Get
        Set(ByVal value As Double)
            Me.var_comision = value
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
            Me.var_identificador = ac_Funciones.formato_Texto(obj_dt_int.Rows(0).Item("identificador").ToString)
            Me.var_direccion = ac_Funciones.formato_Texto(obj_dt_int.Rows(0).Item("direccion").ToString)
            Me.var_telefono_fijo = ac_Funciones.formato_Texto(obj_dt_int.Rows(0).Item("telefono_fijo").ToString)
            Me.var_telefono_movil = ac_Funciones.formato_Texto(obj_dt_int.Rows(0).Item("telefono_movil").ToString)
            Me.var_email = ac_Funciones.formato_Texto(obj_dt_int.Rows(0).Item("email").ToString)
            Me.var_codigo = ac_Funciones.formato_Texto(obj_dt_int.Rows(0).Item("codigo").ToString)
            Me.var_razon_social = ac_Funciones.formato_Texto(obj_dt_int.Rows(0).Item("razon_social").ToString)
            Me.var_id_usuario_reg = formato_Numero(obj_dt_int.Rows(0).Item("id_usuario_reg").ToString)
            Me.var_fecha_reg = formato_Fecha(obj_dt_int.Rows(0).Item("fecha_reg").ToString)
            Me.var_comision = ac_Funciones.formato_Numero(obj_dt_int.Rows(0).Item("comision").ToString, True)

            Dim obj_dt_contacto As DataTable = ac_Funciones.Abrir_Tabla(Me.obj_Conex_int, "select id from " & cls_hoteles_contactos.Nombre_Tabla & " where id_hotel=" & Me.var_id)
            For i As Integer = 0 To obj_dt_contacto.Rows.Count - 1
                Me.obj_contactos.Add(New cls_hoteles_contactos(obj_dt_contacto.Rows(i).Item(0)))
            Next
        Else
            Me.var_id = 0
        End If
    End Sub

    Public Function Actualizar(ByRef var_msj As String) As Boolean
        If Validar_Existe(Me.obj_Conex_int, Me.var_Nombre_Tabla, Me.var_Campo_Validacion, Me.var_nombre, Me.var_Campo_Id, Me.var_id) Then
            If var_msj = "" Then
                var_msj = "El hotel '" & Me.var_nombre & "' ya existe en la base de datos"
            End If
            Return False
            Exit Function
        End If

        Dim var_error As String = ""
        If Me.var_id = 0 Then   'NUEVO
            If Not Ingresar(Me.obj_Conex_int, Me.var_Nombre_Tabla, Me.var_Campos, Sql_Texto(Me.var_nombre) & "," & Sql_Texto(Me.var_identificador) & "," & Sql_Texto(Me.var_direccion) & "," & Sql_Texto(Me.var_telefono_fijo) & "," & Sql_Texto(Me.var_telefono_movil) & "," & Sql_Texto(Me.var_email) & "," & Sql_Texto(Me.var_codigo) & "," & Sql_Texto(Me.var_razon_social) & "," & Sql_Texto(Me.var_id_usuario_reg) & "," & Sql_Texto(Me.var_fecha_reg) & "," & Sql_Texto(Me.var_comision), var_error) Then
                Dim obj_log As New cls_logs
                obj_log.ComentarioLog = "Error agregando hotel '" & Me.var_nombre & " - " & Me.var_identificador & " - " & Me.var_razon_social & "': " & var_error
                obj_log.FechaLog = Now
                obj_log.id_Menu = New cls_modulos("hoteles").Id
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
                obj_sb.Append("," & Chr(34) & "nombre" & Chr(34) & ":" & Chr(34) & Me.nombre & Chr(34) & "")
                obj_sb.Append("," & Chr(34) & "identificador" & Chr(34) & ":" & Chr(34) & Me.identificador & Chr(34) & "")
                obj_sb.Append("," & Chr(34) & "direccion" & Chr(34) & ":" & Chr(34) & Me.direccion & Chr(34) & "")
                obj_sb.Append("," & Chr(34) & "telefono_fijo" & Chr(34) & ":" & Chr(34) & Me.telefono_fijo & Chr(34) & "")
                obj_sb.Append("," & Chr(34) & "telefono_movil" & Chr(34) & ":" & Chr(34) & Me.telefono_movil & Chr(34) & "")
                obj_sb.Append("," & Chr(34) & "email" & Chr(34) & ":" & Chr(34) & Me.email & Chr(34) & "")
                obj_sb.Append("," & Chr(34) & "codigo" & Chr(34) & ":" & Chr(34) & Me.codigo & Chr(34) & "")
                obj_sb.Append("," & Chr(34) & "razon_social" & Chr(34) & ":" & Chr(34) & Me.razon_social & Chr(34) & "")

                Dim obj_log As New cls_logs
                obj_log.ComentarioLog = "Hotel agregado: {" & obj_sb.ToString & "}"
                obj_log.FechaLog = Now
                obj_log.id_Menu = New cls_modulos("hoteles").Id
                obj_log.id_Usuario = var_id_usuario_reg
                obj_log.idAccion = New cls_acciones("agregar").Id
                obj_log.ResultadoLog = True
                obj_log.InsertarLog()

                For i As Integer = 0 To obj_contactos.Count - 1
                    obj_contactos.Item(i).id_hotel = Me.Id
                    Dim var_msj2 As String = ""
                    If Not obj_contactos.Item(i).Actualizar(var_error) Then
                        var_msj &= " - " & var_msj2
                    End If
                Next

                Return True
            End If
        ElseIf Me.var_id > 0 Then 'EDICION
            If Not ac_Funciones.Actualizar(Me.obj_Conex_int, Me.var_Nombre_Tabla, "nombre=" & Sql_Texto(Me.var_nombre) & ",identificador=" & Sql_Texto(Me.var_identificador) & ",direccion=" & Sql_Texto(Me.var_direccion) & ",telefono_fijo=" & Sql_Texto(Me.var_telefono_fijo) & ",telefono_movil=" & Sql_Texto(Me.var_telefono_movil) & ",email=" & Sql_Texto(Me.var_email) & ",codigo=" & Sql_Texto(Me.var_codigo) & ",razon_social=" & Sql_Texto(Me.var_razon_social) & ",comision=" & Sql_Texto(Me.var_comision), Me.var_Campo_Id & "=" & Me.var_id) Then
                var_msj = var_error

                Dim obj_log As New cls_logs
                obj_log.ComentarioLog = "Error editando hotel '" & Me.var_nombre & " - " & Me.var_identificador & " - " & Me.var_razon_social & "': " & var_error
                obj_log.FechaLog = Now
                obj_log.id_Menu = New cls_modulos("hoteles").Id
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
                obj_sb.Append("," & Chr(34) & "identificador" & Chr(34) & ":" & Chr(34) & Me.identificador & Chr(34) & "")
                obj_sb.Append("," & Chr(34) & "direccion" & Chr(34) & ":" & Chr(34) & Me.direccion & Chr(34) & "")
                obj_sb.Append("," & Chr(34) & "telefono_fijo" & Chr(34) & ":" & Chr(34) & Me.telefono_fijo & Chr(34) & "")
                obj_sb.Append("," & Chr(34) & "telefono_movil" & Chr(34) & ":" & Chr(34) & Me.telefono_movil & Chr(34) & "")
                obj_sb.Append("," & Chr(34) & "email" & Chr(34) & ":" & Chr(34) & Me.email & Chr(34) & "")
                obj_sb.Append("," & Chr(34) & "codigo" & Chr(34) & ":" & Chr(34) & Me.codigo & Chr(34) & "")
                obj_sb.Append("," & Chr(34) & "razon_social" & Chr(34) & ":" & Chr(34) & Me.razon_social & Chr(34) & "")

                Dim obj_log As New cls_logs
                obj_log.ComentarioLog = "Hotel editado: {" & obj_sb.ToString & "}"
                obj_log.FechaLog = Now
                obj_log.id_Menu = New cls_modulos("hoteles").Id
                obj_log.id_Usuario = var_id_usuario_reg
                obj_log.idAccion = New cls_acciones("editar").Id
                obj_log.ResultadoLog = True
                obj_log.InsertarLog()

                Dim var_ids As String = ""
                For i As Integer = 0 To Me.obj_contactos.Count - 1
                    Me.obj_contactos.Item(i).id_hotel = Me.Id
                    Dim var_msj2 As String = ""
                    Me.obj_contactos.Item(i).Actualizar(var_msj2)
                    var_msj &= " - " & var_msj2
                    var_ids &= "," & Me.obj_contactos.Item(i).Id

                Next
                If var_ids.Trim.Length > 1 Then
                    ac_Funciones.Eliminar(Me.obj_Conex_int, cls_hoteles_contactos.Nombre_Tabla, "id_hotel=" & Me.var_id & " and id not in(" & var_ids.Substring(1) & ")")
                Else
                    ac_Funciones.Eliminar(Me.obj_Conex_int, cls_hoteles_contactos.Nombre_Tabla, "id_hotel=" & Me.var_id)
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
        Dim obj_hotel As New cls_hoteles(var_id)
        If Not ac_Funciones.Actualizar(obj_Conex_int, cls_hoteles.Nombre_Tabla, "anulado= case when isnull(anulado,0)=0 then '1' else '0' end, fecha_anulacion=" & Sql_Texto(Now, True) & ", id_usuario_anulacion=" & var_id_usuario, cls_hoteles.Campo_Id & "=" & var_id, var_error) Then
            var_mensaje = var_error

            Dim obj_log As New cls_logs
            obj_log.ComentarioLog = "Error anulando hotel '" & obj_hotel.Id & " - " & obj_hotel.nombre & "': " & var_error
            obj_log.FechaLog = Now
            obj_log.id_Menu = New cls_modulos("hoteles").Id
            obj_log.id_Usuario = var_id_usuario
            obj_log.idAccion = New cls_acciones("anular").Id
            obj_log.ResultadoLog = False
            obj_log.InsertarLog()

            Return False
        Else

            Dim obj_log As New cls_logs
            obj_log.ComentarioLog = "hotel anulado '" & obj_hotel.Id & " - " & obj_hotel.nombre
            obj_log.FechaLog = Now
            obj_log.id_Menu = New cls_modulos("hoteles").Id
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

        If ac_Funciones.Eliminar(obj_conex, cls_hoteles.Nombre_Tabla, cls_hoteles.Campo_Id & "=" & var_id) < 0 Then
            var_mensaje = var_error
            Return False
        Else
            Return True
        End If
    End Function

    Public Function ListaContactos() As DataTable
        Dim obj_dt As New System.Data.DataTable
        obj_dt.Columns.Add("DT_RowId", GetType(System.Int32))
        obj_dt.Columns.Add("Hotel", GetType(System.String))
        obj_dt.Columns.Add("Nombre", GetType(System.String))
        obj_dt.Columns.Add("Cargo", GetType(System.String))
        obj_dt.Columns.Add("Telefono", GetType(System.String))
        For i As Integer = 0 To Me.Contacto.Count - 1
            obj_dt.Rows.Add(i + 1, Me.obj_contactos(i).id_hotel, Me.obj_contactos(i).nombre, Me.obj_contactos(i).cargo, Me.obj_contactos(i).telefono)
        Next

        Return obj_dt
    End Function

    Public Shared Function Lista(Optional ByVal var_filtro As String = "") As DataTable
        Dim obj_Conex_int As New SqlConnection(ConfigurationManager.ConnectionStrings("connection").ConnectionString)
        Dim obj_dt_int As DataTable = Abrir_Tabla(obj_Conex_int, "select " & cls_hoteles.Campo_Id & " as id, " & cls_hoteles.Campo_Validacion & " as des from " & cls_hoteles.Nombre_Tabla & IIf(var_filtro <> "", " where " & var_filtro, "") & " order by " & cls_hoteles.Campo_Validacion & "")
        obj_dt_int.Rows.Add(0, "Seleccione una opción")
        Return obj_dt_int
    End Function

    Public Shared Function ConsultaActivos(ByRef var_error As String, Optional ByVal var_filtro As String = "") As DataTable
        Dim obj_Conex_int As New SqlConnection(ConfigurationManager.ConnectionStrings("connection").ConnectionString)
        Dim var_consulta As String = "Select * from " & cls_hoteles.ListadoActivos & "(" & Sql_Texto(var_filtro) & ")"
        Dim var_msj As String = ""

        Dim obj_dt_int As System.Data.DataTable = Abrir_Tabla(obj_Conex_int, var_consulta, var_msj)
        var_error = var_msj
        Return obj_dt_int
    End Function

    Public Shared Function ConsultaAnulados(ByRef var_error As String, Optional ByVal var_filtro As String = "") As DataTable
        Dim obj_Conex_int As New SqlConnection(ConfigurationManager.ConnectionStrings("connection").ConnectionString)
        Dim var_consulta As String = "Select * from " & cls_hoteles.ListadoAnulados & "(" & Sql_Texto(var_filtro) & ")"
        Dim var_msj As String = ""

        Dim obj_dt_int As System.Data.DataTable = Abrir_Tabla(obj_Conex_int, var_consulta, var_msj)
        var_error = var_msj
        Return obj_dt_int
    End Function

    Public Shared Function Consulta(ByRef var_error As String, Optional ByVal var_filtro As String = "") As DataTable
        Dim obj_Conex_int As New SqlConnection(ConfigurationManager.ConnectionStrings("connection").ConnectionString)
        Dim var_consulta As String = "Select * from " & cls_hoteles.Listado & "(" & Sql_Texto(var_filtro) & ")"
        Dim var_msj As String = ""

        Dim obj_dt_int As System.Data.DataTable = Abrir_Tabla(obj_Conex_int, var_consulta, var_msj)
        var_error = var_msj
        Return obj_dt_int
    End Function
    Public Shared Function ConsultaHotel(Optional ByVal var_filtro As String = "", Optional ByVal var_orden As String = "", Optional ByRef var_error As String = "") As DataTable
        Dim obj_Conex_int As New SqlConnection(ConfigurationManager.ConnectionStrings("connection").ConnectionString)
        Dim var_consulta As String = "Select * from " & cls_hoteles.Listado & "(" & Sql_Texto(var_filtro) & ")"
        Return Abrir_Tabla(obj_Conex_int, var_consulta, var_error)
    End Function
    Public Shared Function SiguienteNumero() As Integer
        Dim obj_Connection As New SqlConnection(ConfigurationManager.ConnectionStrings("connection").ConnectionString)
        Return ac_Funciones.formato_Numero(ac_Funciones.Valor_De(obj_Connection, "select isnull(max(right(num,4)),0) from (select case when ISNUMERIC(codigo)=1 then cast(codigo as int) else 0 end as num from tbl_hoteles) as c").ToString)
    End Function
End Class
