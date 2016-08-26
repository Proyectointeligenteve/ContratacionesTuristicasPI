
Partial Class principal
    Inherits System.Web.UI.MasterPage

    Public varHtmlMenu As String = ""

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If IsNothing(Session.Contents("obj_Session")) Then
            Response.Redirect("info.aspx", True)
        End If
        Cargar_Menu()
    End Sub

    Sub Cargar_Menu()

        Dim obj_Session As cls_Sesion
        obj_Session = Session.Contents("obj_Session")
        Dim objDTMenuMaster As System.Data.DataTable
        Dim objDTMenuItems As System.Data.DataTable
        Dim objDAAux As System.Data.SqlClient.SqlDataAdapter

        objDAAux = New System.Data.SqlClient.SqlDataAdapter("SELECT i.id AS Id_Menu, i.descripcion AS Menu, i.url AS Url FROM tbl_modulos as i inner join tbl_permisos as p on p.id_modulo=i.id where i.id_padre=0 and p.permiso=1 and p.id_accion=(select top 1 id from tbl_acciones where nombre like 'Ver') and p.id_rol=(select id_rol from tbl_usuarios where id=" & DirectCast(Session.Contents("obj_Session"), cls_Sesion).Usuario.Id & ") ORDER BY i.posicion", obj_Session.Conexion)
        objDTMenuMaster = New System.Data.DataTable
        objDAAux.Fill(objDTMenuMaster)

        Dim varItemSelected As Boolean = False
        For Each objMenu As System.Data.DataRow In objDTMenuMaster.Rows

            objDAAux = New System.Data.SqlClient.SqlDataAdapter("SELECT i.descripcion AS Item, i.url AS Url FROM tbl_modulos as i INNER JOIN tbl_permisos as p on p.id_modulo = i.id WHERE p.id_rol=(select id_rol from tbl_usuarios where id=" & DirectCast(Session.Contents("obj_Session"), cls_Sesion).Usuario.Id & ") and p.id_accion=(select top 1 id from tbl_acciones where nombre like 'Ver') and p.permiso=1 and i.id_padre = " & Val(objMenu.Item("Id_Menu").ToString) & " ORDER BY i.posicion", obj_Session.Conexion)
            objDTMenuItems = New System.Data.DataTable
            objDAAux.Fill(objDTMenuItems)

            If objDTMenuItems.Rows.Count > 0 Then
                varHtmlMenu &= "<li class='dropdown'><a style='font-size :14px !important' href='#' class='dropdown-toggle' data-toggle='dropdown'>" & objMenu.Item("Menu").ToString & "</a>"

                varHtmlMenu &= "<ul class='dropdown-menu'>"
                For Each objItems As System.Data.DataRow In objDTMenuItems.Rows
                    varHtmlMenu &= "<li " & IIf(Request.Url.AbsoluteUri.Substring(Request.Url.AbsoluteUri.LastIndexOf("/") + 1).Trim.ToUpper = objMenu.Item("Url").ToString.Trim.ToUpper, " class='active'", "") & ">"
                    varHtmlMenu &= "<a href='" & objItems.Item("Url").ToString & "'>" & objItems.Item("Item").ToString & "</a>"
                    varHtmlMenu &= "</li>"
                Next
                varHtmlMenu &= "</ul>"

                varHtmlMenu &= "</li>"
            Else
                varHtmlMenu &= "<li " & IIf(Request.Url.AbsoluteUri.Substring(Request.Url.AbsoluteUri.LastIndexOf("/") + 1).Trim.ToUpper = objMenu.Item("Url").ToString.Trim.ToUpper, " class='active'", "") & "><a href='" & IIf(objMenu.Item("Url").ToString.Trim.Length > 0, objMenu.Item("Url").ToString, "#") & "'>" & objMenu.Item("Menu").ToString & "</a></li>"
            End If

        Next
    End Sub
End Class