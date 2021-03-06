﻿Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports ac_Funciones

Public Class cls_ventas_detalles

#Region "VARIABLES"

    Dim var_Nombre_Tabla As String = "tbl_ventas_detalles"
    Dim var_Campo_Id As String = "id"
    Dim var_Campos As String = " id_venta,id_producto,id_moneda,id_precio,cantidad,descripcion,comentario,precio,impuesto,total"
    Dim obj_Conex_int As New SqlConnection(ConfigurationManager.ConnectionStrings("connection").ConnectionString)

    Dim var_id As Integer = 0
    Dim var_id_venta As Integer = 0
    Dim var_id_producto As Integer = 0
    Dim var_id_moneda As Integer = 0
    Dim var_id_precio As Integer = 0
    Dim var_cantidad As Double = 0
    Dim var_descripcion As String = ""
    Dim var_comentario As String = ""
    Dim var_precio As Double = 0
    Dim var_impuesto As Double = 0
    Dim var_total As Double = 0

#End Region

#Region "PROPIEDADES"


    Public Shared ReadOnly Property Nombre_Tabla() As String
        Get
            Return "tbl_ventas_detalles"
        End Get
    End Property

    Public Shared ReadOnly Property Campo_Id() As String
        Get
            Return "id"
        End Get
    End Property

    Public ReadOnly Property Id() As Integer
        Get
            Return Me.var_id
        End Get
    End Property

    Public Property id_venta() As Integer
        Get
            Return Me.var_id_venta
        End Get
        Set(ByVal value As Integer)
            Me.var_id_venta = value
        End Set
    End Property

    Public Property id_producto() As Integer
        Get
            Return Me.var_id_producto
        End Get
        Set(ByVal value As Integer)
            Me.var_id_producto = value
        End Set
    End Property

    Public Property id_moneda() As Integer
        Get
            Return Me.var_id_moneda
        End Get
        Set(ByVal value As Integer)
            Me.var_id_moneda = value
        End Set
    End Property

    Public Property id_precio() As Integer
        Get
            Return Me.var_id_precio
        End Get
        Set(ByVal value As Integer)
            Me.var_id_precio = value
        End Set
    End Property

    Public Property cantidad() As Double
        Get
            Return Me.var_cantidad
        End Get
        Set(ByVal value As Double)
            Me.var_cantidad = value
        End Set
    End Property

    Public Property descripcion() As String
        Get
            Return Me.var_descripcion
        End Get
        Set(ByVal value As String)
            Me.var_descripcion = value
        End Set
    End Property

    Public Property comentario() As String
        Get
            Return Me.var_comentario
        End Get
        Set(ByVal value As String)
            Me.var_comentario = value
        End Set
    End Property

    Public Property precio() As Double
        Get
            Return Me.var_precio
        End Get
        Set(ByVal value As Double)
            Me.var_precio = value
        End Set
    End Property

    Public Property impuesto() As Double
        Get
            Return Me.var_impuesto
        End Get
        Set(ByVal value As Double)
            Me.var_impuesto = value
        End Set
    End Property

    Public Property total() As Double
        Get
            Return Me.var_total
        End Get
        Set(ByVal value As Double)
            Me.var_total = value
        End Set
    End Property

    Public Shared ReadOnly Property Campo_lista() As String
        Get
            Return "descripcion"
        End Get
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
            Me.var_id = formato_Numero(obj_dt_int.Rows(0).Item("id").ToString)
            Me.var_id_venta = formato_Numero(obj_dt_int.Rows(0).Item("id_venta").ToString)
            Me.var_id_producto = formato_Numero(obj_dt_int.Rows(0).Item("id_producto").ToString)
            Me.var_id_moneda = formato_Numero(obj_dt_int.Rows(0).Item("id_moneda").ToString)
            Me.var_id_precio = formato_Numero(obj_dt_int.Rows(0).Item("id_precio").ToString)
            Me.var_cantidad = formato_Numero(obj_dt_int.Rows(0).Item("cantidad").ToString, True)
            Me.var_descripcion = formato_Texto(obj_dt_int.Rows(0).Item("descripcion").ToString)
            Me.var_comentario = formato_Texto(obj_dt_int.Rows(0).Item("comentario").ToString)
            Me.var_precio = formato_Numero(obj_dt_int.Rows(0).Item("precio").ToString, True)
            Me.var_impuesto = formato_Numero(obj_dt_int.Rows(0).Item("impuesto").ToString, True)
            Me.var_total = formato_Numero(obj_dt_int.Rows(0).Item("total").ToString, True)
        Else
            Me.var_id = -1
        End If
    End Sub

    Public Function Actualizar(ByRef var_Error As String) As Boolean
        Dim var_msj As String = ""
        If Me.var_id = 0 Then   'NUEVO
            If Not Ingresar(Me.obj_Conex_int, Me.var_Nombre_Tabla, Me.var_Campos, Sql_Texto(Me.id_venta) & "," & Sql_Texto(Me.var_id_producto) & "," & Sql_Texto(Me.var_id_moneda) & "," & Sql_Texto(Me.var_id_precio) & "," & Sql_Texto(Me.var_cantidad) & "," & Sql_Texto(Me.var_descripcion) & "," & Sql_Texto(Me.var_comentario) & "," & Sql_Texto(Me.var_precio) & "," & Sql_Texto(Me.var_impuesto) & "," & Sql_Texto(Me.var_total), var_msj) Then
                var_Error = var_msj

                Dim obj_log As New cls_logs
                obj_log.ComentarioLog = "Error agregando detalle de venta - " & Me.var_id_venta & " - " & Now.Date & ": " & var_Error
                obj_log.FechaLog = Now
                obj_log.id_Menu = New cls_modulos("venta").Id
                obj_log.id_Usuario = 0
                obj_log.idAccion = New cls_acciones("agregar").Id
                obj_log.ResultadoLog = False
                obj_log.InsertarLog()

                Return False
                Exit Function
            Else
                Me.var_id = Valor_De(Me.obj_Conex_int, "select " & Me.var_Campo_Id & " from " & Me.var_Nombre_Tabla & " order by id desc")

                Dim obj_sb As New StringBuilder
                obj_sb.Append("," & Chr(34) & "id_venta" & Chr(34) & ":" & Chr(34) & Me.id_venta & Chr(34) & "")
                obj_sb.Append("," & Chr(34) & "id_producto" & Chr(34) & ":" & Chr(34) & Me.id_producto & Chr(34) & "")
                'obj_sb.Append("," & Chr(34) & "id_precio" & Chr(34) & ":" & Chr(34) & Me.id_precio & Chr(34) & "")
                obj_sb.Append("," & Chr(34) & "cantidad" & Chr(34) & ":" & Chr(34) & Me.cantidad & Chr(34) & "")
                obj_sb.Append("," & Chr(34) & "comentario" & Chr(34) & ":" & Chr(34) & Me.comentario & Chr(34) & "")

                Dim obj_log As New cls_logs
                obj_log.ComentarioLog = "venta detalle agregado: {" & obj_sb.ToString & "}"
                obj_log.FechaLog = Now
                obj_log.id_Menu = New cls_modulos("venta").Id
                obj_log.id_Usuario = 0
                obj_log.idAccion = New cls_acciones("agregar").Id
                obj_log.ResultadoLog = True
                obj_log.InsertarLog()
                Return True
            End If
        ElseIf Me.var_id > 0 Then 'EDICION
            If Not ac_Funciones.Actualizar(Me.obj_Conex_int, Me.var_Nombre_Tabla, "id_venta=" & Sql_Texto(Me.var_id_venta) & ",id_producto=" & Sql_Texto(Me.var_id_producto) & ",id_moneda=" & Sql_Texto(Me.var_id_moneda) & ",id_precio=" & Sql_Texto(Me.var_id_precio) & ",cantidad=" & Sql_Texto(Me.var_cantidad) & ",descripcion=" & Sql_Texto(Me.var_descripcion) & ",comentario=" & Sql_Texto(Me.var_comentario) & ",precio=" & Sql_Texto(Me.var_precio) & ",impuesto=" & Sql_Texto(Me.var_impuesto) & ",total=" & Sql_Texto(Me.var_total), Me.var_Campo_Id & "=" & Me.var_id, var_msj) Then
                var_Error = var_msj

                Dim obj_log As New cls_logs
                obj_log.ComentarioLog = "Error editando detalle venta '" & Me.var_id & " - " & Me.var_id_venta & " - " & Now.Date & "': " & var_Error
                obj_log.FechaLog = Now
                obj_log.id_Menu = New cls_modulos("venta").Id
                obj_log.id_Usuario = 0
                obj_log.idAccion = New cls_acciones("editar").Id
                obj_log.ResultadoLog = False
                obj_log.InsertarLog()

                Return False
                Exit Function

            Else

                Dim obj_sb As New StringBuilder
                obj_sb.Append("," & Chr(34) & "id_venta" & Chr(34) & ":" & Chr(34) & Me.id_venta & Chr(34) & "")
                obj_sb.Append("," & Chr(34) & "id_producto" & Chr(34) & ":" & Chr(34) & Me.id_producto & Chr(34) & "")
                'obj_sb.Append("," & Chr(34) & "id_precio" & Chr(34) & ":" & Chr(34) & Me.id_precio & Chr(34) & "")
                obj_sb.Append("," & Chr(34) & "cantidad" & Chr(34) & ":" & Chr(34) & Me.cantidad & Chr(34) & "")
                obj_sb.Append("," & Chr(34) & "comentario" & Chr(34) & ":" & Chr(34) & Me.comentario & Chr(34) & "")

                Dim obj_log As New cls_logs
                obj_log.ComentarioLog = "venta detalle editado: {" & obj_sb.ToString & "}"
                obj_log.FechaLog = Now
                obj_log.id_Menu = New cls_modulos("venta").Id
                obj_log.id_Usuario = 0
                obj_log.idAccion = New cls_acciones("editar").Id
                obj_log.ResultadoLog = True
                obj_log.InsertarLog()

            End If
            Return True
        Else
            var_Error = "No se encontro el Registro"
            Return False
        End If
    End Function

    Public Shared Function EliminarDetallesventas(ByVal var_id As Integer, ByVal obj_usuario As cls_usuarios, ByRef var_mensaje As String) As Boolean
        Dim obj_Conex_int As New SqlConnection(ConfigurationManager.ConnectionStrings("connection").ConnectionString)
        ac_Funciones.Eliminar(obj_Conex_int, cls_ventas_detalles.Nombre_Tabla, cls_ventas_detalles.Campo_Id & "=" & var_id)
        Return True
    End Function
#End Region

End Class
