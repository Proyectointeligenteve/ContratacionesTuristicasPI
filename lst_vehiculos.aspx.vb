Imports System.Data
Imports System.Data.SqlClient
Imports System.Web.Services
Imports Newtonsoft.Json
Imports Newtonsoft.Json.Linq

Partial Class lst_vehiculos
    Inherits System.Web.UI.Page

    Dim obj_Session As cls_Sesion

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If IsNothing(Session.Contents("obj_Session")) Then
            Response.Redirect("info.aspx", True)
        End If
        obj_Session = Session.Contents("obj_Session")

        If Not IsNothing(Request.QueryString("fn")) Then
            Dim var_fn As String = Request.QueryString("fn").ToString
            Select Case var_fn
                Case "permisos"
                    Permisos()
                Case "cargar"
                    Cargar()
                Case "guardar"
                    Guardar()
                Case "editar"
                    Editar()
                Case "eliminar"
                    Eliminar()
                Case "Anular"
                    Anular()

                Case "nuevo"
                    Nuevo()
                Case "subir_archivo"
                    SubirArchivo()
                Case "cargar_archivos"
                    CargarArchivos()
                Case "eliminar_archivo"
                    EliminarArchivo()
                Case "actualizar_posiciones"
                    ActualizarPosiciones()

                Case "cargar_agencias"
                    Agencias()
            End Select
        End If
    End Sub
    Sub Permisos()
        Dim var_error As String = ""
        Dim obj_sb As New StringBuilder
        Try
            If Not New cls_permisos(obj_Session.Usuario.Id, New cls_modulos("vehiculos").Id, New cls_acciones("ver").Id).permiso Then
                Dim obj_log As New cls_logs
                obj_log.ComentarioLog = "Listado de vehiculos"
                obj_log.FechaLog = Now
                obj_log.id_Menu = New cls_modulos("vehiculos").Id
                obj_log.id_Usuario = obj_Session.Usuario.Id
                obj_log.idAccion = New cls_acciones("ver").Id
                obj_log.ResultadoLog = False
                obj_log.InsertarLog()

                Response.Clear()
                Response.ClearHeaders()
                Response.ClearContent()
                Response.Write("{" & Chr(34) & "error" & Chr(34) & ":" & Chr(34) & "-1" & Chr(34) & "}")
                Response.End()
            Else

                Dim obj_log As New cls_logs
                obj_log.ComentarioLog = "Listado de vehiculos"
                obj_log.FechaLog = Now
                obj_log.id_Menu = New cls_modulos("vehiculos").Id
                obj_log.id_Usuario = obj_Session.Usuario.Id
                obj_log.idAccion = New cls_acciones("ver").Id
                obj_log.ResultadoLog = True
                obj_log.InsertarLog()

            End If

            obj_sb.Append("," & Chr(34) & "Agregar" & Chr(34) & ":" & Chr(34) & IIf(New cls_permisos(obj_Session.Usuario.Id, New cls_modulos("vehiculos").Id, New cls_acciones("agregar").Id).permiso, 1, 0) & Chr(34) & "")
            obj_sb.Append("," & Chr(34) & "Ver" & Chr(34) & ":" & Chr(34) & IIf(New cls_permisos(obj_Session.Usuario.Id, New cls_modulos("vehiculos").Id, New cls_acciones("ver").Id).permiso, 1, 0) & Chr(34) & "")
            obj_sb.Append("," & Chr(34) & "Editar" & Chr(34) & ":" & Chr(34) & IIf(New cls_permisos(obj_Session.Usuario.Id, New cls_modulos("vehiculos").Id, New cls_acciones("editar").Id).permiso, 1, 0) & Chr(34) & "")
            obj_sb.Append("," & Chr(34) & "Anular" & Chr(34) & ":" & Chr(34) & IIf(New cls_permisos(obj_Session.Usuario.Id, New cls_modulos("vehiculos").Id, New cls_acciones("anular").Id).permiso, 1, 0) & Chr(34) & "")
            obj_sb.Append("," & Chr(34) & "Eliminar" & Chr(34) & ":" & Chr(34) & IIf(New cls_permisos(obj_Session.Usuario.Id, New cls_modulos("vehiculos").Id, New cls_acciones("eliminar").Id).permiso, 1, 0) & Chr(34) & "")

        Catch ex As Exception
            var_error = ex.Message
        End Try

        Response.ContentType = "application/json"
        Response.Clear()
        Response.ClearHeaders()
        Response.ClearContent()
        If var_error.Trim.Length > 0 Then
            Response.Write("{" & Chr(34) & "rslt" & Chr(34) & ":" & Chr(34) & "error" & Chr(34) & "," & Chr(34) & "msj" & Chr(34) & ":" & Chr(34) & var_error & Chr(34) & "}")
        Else
            Response.Write("{" & Chr(34) & "rslt" & Chr(34) & ":" & Chr(34) & "exito" & Chr(34) & "," & Chr(34) & "msj" & Chr(34) & ":" & Chr(34) & "" & Chr(34) & obj_sb.ToString & "}")
        End If
        Response.End()
    End Sub

    Sub Cargar()
        Dim var_error As String = ""
        Dim var_estatus As String = 1

        If Not IsNothing(Request.QueryString("v")) Then
            var_estatus = Request.QueryString("v")
        End If

        Dim obj_dt_int As System.Data.DataTable
        If var_estatus = 1 Then
            obj_dt_int = cls_vehiculos.ConsultaActivos(var_error)
        ElseIf var_estatus = 2 Then
            obj_dt_int = cls_vehiculos.ConsultaAnulados(var_error)
        Else 'Todos
            obj_dt_int = cls_vehiculos.Consulta(var_error)
        End If
        Dim var_json As String = JsonConvert.SerializeObject(obj_dt_int)

        Response.Clear()
        Response.ClearHeaders()
        Response.ClearContent()
        Response.Write("{ " & Chr(34) & "aaData" & Chr(34) & ":" & var_json & "}")
        Response.End()
    End Sub
    Sub Guardar()
        Dim var_sr = New System.IO.StreamReader(Request.InputStream)
        Dim var_data As JObject = JObject.Parse(var_sr.ReadToEnd)

        Dim obj_vehiculos As New cls_vehiculos(CInt(var_data("id")))
        If obj_vehiculos.Id = 0 Then
            obj_vehiculos.id_usuario_reg = DirectCast(Session.Contents("obj_Session"), cls_Sesion).Usuario.Id
            obj_vehiculos.fecha_reg = Now.Date
            obj_vehiculos.Codigo = Right("VH" & Right("0000" & (cls_vehiculos.SiguienteNumero() + 1).ToString, 4).ToString, 6)
        Else
            obj_vehiculos.Codigo = var_data("Codigo")
        End If
        obj_vehiculos.categoria = var_data("Categoria")
        obj_vehiculos.Descripcion = var_data("Descripcion")
        obj_vehiculos.Nombre = var_data("Nombre")
        obj_vehiculos.Agencia = var_data("Agencia")

        Response.ContentType = "application/json"
        Response.Clear()
        Response.ClearHeaders()
        Response.ClearContent()
        Dim var_error As String = ""
        If Not obj_vehiculos.Actualizar(var_error, obj_Session.Usuario.Id) Then
            Response.Write("{" & Chr(34) & "rslt" & Chr(34) & ":" & Chr(34) & "error" & Chr(34) & "," & Chr(34) & "msj" & Chr(34) & ":" & Chr(34) & var_error & Chr(34) & "}")
        Else

            Dim var_mensj As String = ""
            Dim var_ruta As String = Server.MapPath("img/vehiculos/")

            Dim i As Integer = 0
            Try
                Dim sArchivos() As String = System.IO.Directory.GetFiles(var_ruta)
                For Each archivo As String In sArchivos
                    Dim archivoInfo As System.IO.FileInfo = New System.IO.FileInfo(archivo)
                    If archivoInfo.Name.Contains(Session.SessionID) Then
                        System.IO.File.Move(archivoInfo.FullName, archivoInfo.FullName.Replace(Session.SessionID, "V" & Right("00000" & obj_vehiculos.Id, 5)))
                        Dim obj_imagen As New cls_imagenes
                        obj_imagen.fecha = Now
                        obj_imagen.id_vehiculo = obj_vehiculos.Id
                        obj_imagen.id_usuario = obj_Session.Usuario
                        obj_imagen.ruta = archivoInfo.Name.Replace(Session.SessionID, "V" & Right("00000" & obj_vehiculos.Id, 5))

                        Dim var_err As String = ""
                        If Not obj_imagen.Actualizar(var_err) Then
                            var_mensj &= " - " & var_err
                        End If

                        i += 1
                    End If
                Next

                cls_imagenes.EliminarSessionId(Session.SessionID, obj_Session.Usuario, "")
            Catch ex As Exception
                var_mensj &= " - " & ex.Message
            End Try
            Response.Write("{" & Chr(34) & "rslt" & Chr(34) & ":" & Chr(34) & "exito" & Chr(34) & "," & Chr(34) & "msj" & Chr(34) & ":" & Chr(34) & "" & Chr(34) & "}")
        End If
        Response.End()
    End Sub

    Sub Editar()
        Dim var_sr = New System.IO.StreamReader(Request.InputStream)
        Dim var_data As JObject = JObject.Parse(var_sr.ReadToEnd)

        Dim obj_vehiculos As New cls_vehiculos(CInt(var_data("id").ToString))
        Dim obj_sb As New StringBuilder
        obj_sb.Append("," & Chr(34) & "Codigo" & Chr(34) & ":" & Chr(34) & obj_vehiculos.Codigo & Chr(34) & "")
        obj_sb.Append("," & Chr(34) & "Categoria" & Chr(34) & ":" & Chr(34) & obj_vehiculos.categoria & Chr(34) & "")
        obj_sb.Append("," & Chr(34) & "Descripcion" & Chr(34) & ":" & Chr(34) & obj_vehiculos.descripcion & Chr(34) & "")
        obj_sb.Append("," & Chr(34) & "Nombre" & Chr(34) & ":" & Chr(34) & obj_vehiculos.Nombre & Chr(34) & "")
        obj_sb.Append("," & Chr(34) & "Agencia" & Chr(34) & ":" & Chr(34) & obj_vehiculos.Agencia & Chr(34) & "")
        Response.ContentType = "application/json"
        Response.Clear()
        Response.ClearHeaders()
        Response.ClearContent()
        Dim var_error As String = ""
        Response.Write("{" & Chr(34) & "rslt" & Chr(34) & ":" & Chr(34) & "exito" & Chr(34) & "," & Chr(34) & "msj" & Chr(34) & ":" & Chr(34) & var_error & Chr(34) & obj_sb.ToString & "}")
        Response.End()
    End Sub

    Sub Eliminar()
        Dim var_sr = New System.IO.StreamReader(Request.InputStream)
        Dim var_data As JObject = JObject.Parse(var_sr.ReadToEnd)
        Dim var_error As String = ""

        Response.ContentType = "application/json"
        Response.Clear()
        Response.ClearHeaders()
        Response.ClearContent()
        If Not cls_vehiculos.Eliminar(CInt(var_data("id").ToString), obj_Session.Usuario, var_error) Then
            Response.Write("{" & Chr(34) & "rslt" & Chr(34) & ":" & Chr(34) & "error" & Chr(34) & "," & Chr(34) & "msj" & Chr(34) & ":" & Chr(34) & var_error & Chr(34) & "}")
        Else
            Response.Write("{" & Chr(34) & "rslt" & Chr(34) & ":" & Chr(34) & "exito" & Chr(34) & "," & Chr(34) & "msj" & Chr(34) & ":" & Chr(34) & Chr(34) & "}")
        End If
        Response.End()
    End Sub

    Sub Anular()
        Dim var_sr = New System.IO.StreamReader(Request.InputStream)
        Dim var_data As JObject = JObject.Parse(var_sr.ReadToEnd)
        Dim var_error As String = ""

        Response.ContentType = "application/json"
        Response.Clear()
        Response.ClearHeaders()
        Response.ClearContent()
        If Not cls_vehiculos.Anular(CInt(var_data("id").ToString), obj_Session.Usuario.Id, var_error) Then
            Response.Write("{" & Chr(34) & "rslt" & Chr(34) & ":" & Chr(34) & "error" & Chr(34) & "," & Chr(34) & "msj" & Chr(34) & ":" & Chr(34) & var_error & Chr(34) & "}")
        Else
            Response.Write("{" & Chr(34) & "rslt" & Chr(34) & ":" & Chr(34) & "exito" & Chr(34) & "," & Chr(34) & "msj" & Chr(34) & ":" & Chr(34) & Chr(34) & "}")
        End If
        Response.End()
    End Sub

    Private Sub SubirArchivo()
        Dim var_mensj As String = ""
        Try
            Dim rqst = HttpContext.Current.Request
            If rqst.Files.Count > 0 Then
                For Each file As String In Request.Files
                    Dim postedFile = Request.Files(file)
                    Dim filePath As String = Server.MapPath("img/vehiculos/" & Session.SessionID & "_" & postedFile.FileName.Substring(postedFile.FileName.LastIndexOf("\") + 1))
                    postedFile.SaveAs(filePath)

                    Dim obj_imagen As New cls_imagenes
                    obj_imagen.fecha = Now
                    obj_imagen.id_usuario = obj_Session.Usuario
                    obj_imagen.ruta = Session.SessionID & "_" & postedFile.FileName.Substring(postedFile.FileName.LastIndexOf("\") + 1)
                    obj_imagen.sessionid = Session.SessionID

                    Dim var_err As String = ""
                    If Not obj_imagen.Actualizar(var_err) Then
                        var_mensj &= " - " & var_err
                    End If
                Next
            End If
        Catch ex As Exception
            var_mensj = ex.Message
        End Try

        If var_mensj.Trim.Length > 0 Then
            Response.Write("{" & Chr(34) & "rslt" & Chr(34) & ":" & Chr(34) & "error" & Chr(34) & "," & Chr(34) & "msj" & Chr(34) & ":" & Chr(34) & var_mensj & Chr(34) & "}")
        Else
            Response.Write("{" & Chr(34) & "rslt" & Chr(34) & ":" & Chr(34) & "exito" & Chr(34) & "," & Chr(34) & "msj" & Chr(34) & ":" & Chr(34) & Chr(34) & "}")
        End If
        Response.End()
    End Sub

    Private Sub CargarArchivos()
        Dim var_id As String = Request.QueryString("id").ToString

        Dim var_mensj As String = ""
        Dim obj_sb As New StringBuilder
        Dim i As Integer = 0
        Try
            Dim obj_dt As System.Data.DataTable = cls_vehiculos.Imagenes(var_id, "", Session.SessionID)

            For j As Integer = 0 To obj_dt.Rows.Count - 1
                obj_sb.Append("," & Chr(34) & "Archivo" & i.ToString() & Chr(34) & ":" & Chr(34) & obj_dt.Rows(j).Item("ruta") & Chr(34) & "," & Chr(34) & "id" & i.ToString() & Chr(34) & ":" & Chr(34) & obj_dt.Rows(j).Item("id") & Chr(34) & "")
                i += 1
            Next

            obj_sb.Append("," & Chr(34) & "Cantidad" & Chr(34) & ":" & Chr(34) & i.ToString & Chr(34) & "")
        Catch ex As Exception
            var_mensj = ex.Message
        End Try
        If var_mensj.Trim.Length > 0 Then
            Response.Write("{" & Chr(34) & "rslt" & Chr(34) & ":" & Chr(34) & "error" & Chr(34) & "," & Chr(34) & "msj" & Chr(34) & ":" & Chr(34) & var_mensj & Chr(34) & "}")
        Else
            Response.Write("{" & Chr(34) & "rslt" & Chr(34) & ":" & Chr(34) & "exito" & Chr(34) & "," & Chr(34) & "msj" & Chr(34) & ":" & Chr(34) & Chr(34) & obj_sb.ToString & "}")
        End If
        Response.End()
    End Sub

    Sub EliminarArchivo()
        Dim var_id As Integer = Val(Request.QueryString("id").ToString)

        Dim var_sr = New System.IO.StreamReader(Request.InputStream)
        Dim var_data As JObject = JObject.Parse(var_sr.ReadToEnd)
        'Dim filePath As String = IIf(var_id <= 0, Server.MapPath("Adjuntos/" & Session.SessionID & "_" & var_data("archivo").ToString), Server.MapPath("Adjuntos/" & var_data("archivo").ToString))
        Dim filePath As String = Server.MapPath("img/vehiculos/" & var_data("archivo").ToString)

        Dim var_mensj As String = ""
        Try
            If System.IO.File.Exists(filePath) Then
                System.IO.File.Delete(filePath)
                If var_id > 0 Then
                    cls_imagenes.Eliminar(var_data("archivo").ToString, obj_Session.Usuario, "")
                End If
            End If
        Catch ex As Exception
            var_mensj = ex.Message
        End Try

        cls_imagenes.Eliminar(var_data("archivo").ToString, obj_Session.Usuario, "")

        If var_mensj.Trim.Length > 0 Then
            Response.Write("{" & Chr(34) & "rslt" & Chr(34) & ":" & Chr(34) & "error" & Chr(34) & "," & Chr(34) & "msj" & Chr(34) & ":" & Chr(34) & var_mensj & Chr(34) & "}")
        Else
            Response.Write("{" & Chr(34) & "rslt" & Chr(34) & ":" & Chr(34) & "exito" & Chr(34) & "," & Chr(34) & "msj" & Chr(34) & ":" & Chr(34) & Chr(34) & "}")
        End If
        Response.End()
    End Sub

    Sub Nuevo()
        Dim var_mensj As String = ""
        Try
            Dim obj_dt As System.Data.DataTable
            cls_imagenes.EliminarSessionId("", obj_Session.Usuario, var_mensj)

            Dim var_ruta As String = ""
            For Each obj_dr As System.Data.DataRow In obj_dt.Rows
                var_ruta = Server.MapPath("img/vehiculos/" & obj_dr.Item("ruta").ToString)
                If System.IO.File.Exists(var_ruta) Then
                    System.IO.File.Delete(var_ruta)
                End If
            Next

        Catch ex As Exception
            var_mensj = ex.Message
        End Try

        If var_mensj.Trim.Length > 0 Then
            Response.Write("{" & Chr(34) & "rslt" & Chr(34) & ":" & Chr(34) & "error" & Chr(34) & "," & Chr(34) & "msj" & Chr(34) & ":" & Chr(34) & var_mensj & Chr(34) & "}")
        Else
            Response.Write("{" & Chr(34) & "rslt" & Chr(34) & ":" & Chr(34) & "exito" & Chr(34) & "," & Chr(34) & "msj" & Chr(34) & ":" & Chr(34) & Chr(34) & "}")
        End If
        Response.End()
    End Sub

    Sub ActualizarPosiciones()
        Dim var_id As Integer = Val(Request.QueryString("id").ToString)

        Dim var_sr = New System.IO.StreamReader(Request.InputStream)
        Dim var_data As JObject = JObject.Parse(var_sr.ReadToEnd)
        Dim posiciones As String = var_data("posiciones").ToString

        Dim var_mensj As String = ""

        Try
            Dim var_imagenes() As String = posiciones.Split(";")
            Dim i As Integer = 1
            For Each var_imagen As String In var_imagenes
                Dim var_error As String = ""
                Dim id As String = var_imagen.Substring(var_imagen.LastIndexOf("_") + 1)
                If Not cls_imagenes.actualizarPosicion(id, i, var_error) Then
                    var_mensj &= var_error & ". "
                End If
                i += 1
            Next
        Catch ex As Exception
            var_mensj &= "Error. " & ex.Message
        End Try

        If var_mensj.Trim.Length > 0 Then
            Response.Write("{" & Chr(34) & "rslt" & Chr(34) & ":" & Chr(34) & "error" & Chr(34) & "," & Chr(34) & "msj" & Chr(34) & ":" & Chr(34) & var_mensj & Chr(34) & "}")
        Else
            Response.Write("{" & Chr(34) & "rslt" & Chr(34) & ":" & Chr(34) & "exito" & Chr(34) & "," & Chr(34) & "msj" & Chr(34) & ":" & Chr(34) & Chr(34) & "}")
        End If
        Response.End()
    End Sub

    Sub Agencias()
        Dim var_error As String = ""
        Dim obj_dt_int As System.Data.DataTable = cls_vehiculos_agencias.Lista
        Dim var_json As String = JsonConvert.SerializeObject(obj_dt_int)
        Response.Clear()
        Response.ClearHeaders()
        Response.ClearContent()
        Response.Write("{ " & Chr(34) & "aaData" & Chr(34) & ":" & var_json & "}")
        Response.End()
    End Sub
End Class
