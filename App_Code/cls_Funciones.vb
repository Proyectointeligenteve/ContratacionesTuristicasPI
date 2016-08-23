Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Data.SqlClient
Imports System.IO

Public Class ac_Funciones
    Shared Function Ingresar(ByVal obj_Conex As SqlConnection, ByVal var_Tabla As String, ByVal var_Campos As String, ByVal var_Valores As String, Optional ByRef var_error As String = "") As Integer
        Dim var_Select As String = "INSERT INTO " & var_Tabla & " (" & var_Campos & ") VALUES (" & var_Valores & ")"
        Dim obj_cmd As New SqlCommand(var_Select, obj_Conex)
        Try
            If obj_Conex.State <> ConnectionState.Open Then obj_Conex.Open()
            obj_cmd.ExecuteNonQuery()
            Return True
        Catch ex As Exception
            var_error = ex.Message
            Return False
        Finally
            obj_Conex.Close()
        End Try
    End Function

    Shared Function Actualizar(ByVal obj_Conex As SqlConnection, ByVal var_Tabla As String, ByVal var_Campos_Valores As String, ByVal var_Filtro As String, Optional ByRef var_error As String = "") As Boolean
        Dim var_Select As String = "UPDATE " & var_Tabla & " SET " & var_Campos_Valores & IIf(var_Filtro = "", "", " WHERE " & var_Filtro)
        Dim obj_cmd As New SqlCommand(var_Select, obj_Conex)
        Try
            If obj_Conex.State <> ConnectionState.Open Then obj_Conex.Open()
            obj_cmd.ExecuteNonQuery()
            Return True
        Catch ex As Exception
            var_error = ex.Message
            Return False
        Finally
            obj_Conex.Close()
        End Try
    End Function

    Shared Function Eliminar(ByVal obj_Conex As SqlConnection, ByVal var_Tabla As String, Optional ByVal var_Filtro As String = "") As Integer
        Dim var_Select As String = "DELETE FROM " & var_Tabla & IIf(var_Filtro = "", "", " WHERE " & var_Filtro)
        Dim obj_cmd As New SqlCommand(var_Select, obj_Conex)
        Try
            If obj_Conex.State <> ConnectionState.Open Then obj_Conex.Open()
            Return obj_cmd.ExecuteNonQuery
        Catch ex As Exception
            Return -1
        Finally
            obj_Conex.Close()
        End Try
    End Function

    Shared Function Abrir_Tabla(ByVal obj_Conex As SqlConnection, ByVal var_Select As String, Optional ByVal var_error As String = "") As DataTable
        Dim obj_Dt As New DataTable
        Dim obj_Da As New SqlDataAdapter(var_Select, obj_Conex)
        Try
            obj_Da.Fill(obj_Dt)
            Return obj_Dt
        Catch ex As Exception
            var_error = ex.Message
            Return Nothing
        End Try
    End Function

    Shared Function Validar_Existe(ByVal obj_Conex As SqlConnection, ByVal var_Tabla As String, ByVal var_Campo As String, ByVal var_Valor As Object, ByVal var_Campo_id As String, ByVal var_Valor_id As Integer, Optional ByRef var_error As String = "", Optional ByVal var_filtro_add As String = "") As Boolean
        Try
            Dim var_Resultado As Object
            Dim var_Select As String = "Select " & var_Campo & " From " & var_Tabla & " Where LTRIM(RTRIM(" & var_Campo & ")) Like '" & var_Valor & "' And " & var_Campo_id & "<>" & var_Valor_id & IIf(var_filtro_add.Trim.Length > 0, " And " & var_filtro_add, "")
            Dim obj_Cmd As New SqlCommand(var_Select, obj_Conex)
            If obj_Conex.State <> ConnectionState.Open Then obj_Conex.Open()
            var_Resultado = obj_Cmd.ExecuteScalar
            If IsNothing(var_Resultado) Then
                Return False
            Else
                Return True
            End If
            obj_Conex.Close()
        Catch ex As Exception
            var_error = ex.Message
            Return False
        End Try
    End Function

    Shared Function Sql_Texto(ByVal var_Valor As Object, Optional ByVal con_Hora As Boolean = False) As String
        If IsDBNull(var_Valor) OrElse IsNothing(var_Valor) Then
            Sql_Texto = ""
            Exit Function
        End If
        Select Case var_Valor.GetType.ToString
            Case "System.String"
                var_Valor = CStr(var_Valor).Replace("'", "''")
                'Sql_Texto = "upper('" & var_Valor.ToString & "')"
                Sql_Texto = "'" & var_Valor.ToString & "'"
            Case "System.DateTime"
                If con_Hora Then
                    var_Valor = Format(var_Valor, "yyyyMMdd HH:mm:ss")
                Else
                    var_Valor = Format(var_Valor, "yyyyMMdd")
                End If
                Sql_Texto = "'" & var_Valor.ToString & "'"
            Case "System.Boolean"
                If var_Valor Then
                    var_Valor = 1
                Else
                    var_Valor = 0
                End If
                Sql_Texto = var_Valor.ToString
            Case "System.Double", "System.Single", "System.Decimal", "System.Int16", "System.Int32", "System.Int64"
                Sql_Texto = "'" & var_Valor.ToString.Replace(",", ".") & "'"
            Case Else
                Sql_Texto = var_Valor.ToString
        End Select
        'Sql_Texto = Sql_Texto.ToUpper
    End Function

    Shared Function Valor_De(ByVal obj_Conex_Int As SqlConnection, ByVal var_Campo As String, ByVal var_Tabla As String, Optional ByVal var_Filtro As String = "", Optional ByVal var_Orden As String = "") As Object
        Dim var_SQL As String = ""
        Try
            If obj_Conex_Int.State <> ConnectionState.Open Then obj_Conex_Int.Open()
            var_SQL = "SELECT " & var_Campo & " FROM " & var_Tabla
            If var_Filtro <> "" Then var_SQL &= " WHERE " & var_Filtro
            If var_Orden <> "" Then var_SQL &= " ORDER BY " & var_Orden
            Dim obj_Cmd As New SqlCommand(var_SQL, obj_Conex_Int)
            Dim var_Respuesta As Object = obj_Cmd.ExecuteScalar
            Return IIf(IsNothing(var_Respuesta), "", var_Respuesta)
        Catch ex As Exception
            Return -1
        Finally
            obj_Conex_Int.Close()
        End Try
    End Function

    Shared Function Valor_De(ByVal obj_Conex_Int As SqlConnection, ByVal var_SQL As String) As Object
        Try
            If obj_Conex_Int.State <> ConnectionState.Open Then obj_Conex_Int.Open()
            Dim obj_Cmd As New SqlCommand(var_SQL, obj_Conex_Int)
            Dim var_Respuesta As Object = obj_Cmd.ExecuteScalar
            Return IIf(IsNothing(var_Respuesta), "", var_Respuesta)
        Catch ex As Exception
            Return -1
        Finally
            obj_Conex_Int.Close()
        End Try
    End Function

    Shared Function formato_Texto(ByVal var_Texto As Object) As String
        If IsDBNull(var_Texto) OrElse IsNothing(var_Texto) Then var_Texto = ""
        Return Trim(var_Texto.Replace("'", "''"))
    End Function

    Shared Function formato_Texto_Pantalla(ByVal var_Texto As Object) As String
        If IsDBNull(var_Texto) OrElse IsNothing(var_Texto) Then var_Texto = ""
        Return Trim(var_Texto)
    End Function

    Shared Function formato_Fecha(ByVal var_Fecha As Object) As Date
        Dim var_fecha_valida As Date = CDate("1900/01/01")
        Dim var_Dia As String = ""
        Dim var_Mes As String = ""
        Dim var_Ano As String = ""
        If IsDBNull(var_Fecha) Then
            Return var_Fecha = Format(var_fecha_valida, "dd/MM/yyyy")
            Exit Function
        End If
        If var_Fecha.ToString.Length < 10 Then
            var_Fecha = Format(var_fecha_valida, "dd/MM/yyyy")
            Return var_Fecha
            Exit Function
        End If

        var_Dia = var_Fecha.ToString.Substring(0, 2)
        var_Mes = var_Fecha.ToString.Substring(3, 2)
        var_Ano = var_Fecha.ToString.Substring(6, 4)

        Try
            var_fecha_valida = CDate(var_Ano & "/" & var_Mes & "/" & var_Dia)
        Catch ex As Exception
            var_fecha_valida = CDate("1900/01/01")
            Return var_fecha_valida
        End Try

        Return var_fecha_valida
    End Function

    Shared Function formato_Fecha_Pantalla(ByVal var_Fecha As Object) As String
        If Not IsDBNull(var_Fecha) Then
            If IsDate(var_Fecha) Then
                var_Fecha = Format(CDate(var_Fecha), "dd/MM/yyyy")
            Else
                var_Fecha = Format(CDate(Now.Date), "dd/MM/yyyy")
            End If
        Else
            var_Fecha = Format(CDate(Now.Date), "dd/MM/yyyy")
        End If
        Return var_Fecha
    End Function

    Shared Function formato_boolean(ByVal var_texto) As Boolean
        Try
            Return CBool(var_texto)
        Catch ex As Exception
            Return False
        End Try
    End Function

    Shared Function formato_Numero(ByVal var_Numero As Object, Optional ByVal var_Decimales As Boolean = False) As Double
        If Not IsDBNull(var_Numero) Then
            If System.Globalization.CultureInfo.CurrentCulture.NumberFormat.CurrencyDecimalSeparator = "." Then
                var_Numero = var_Numero.Replace(".", "")
                var_Numero = var_Numero.Replace(",", ".")
            Else
                var_Numero = var_Numero.Replace(".", "")
            End If

            If IsNumeric(var_Numero) Then
                If var_Decimales Then
                    If var_Numero.Split(".").GetLongLength(0) > 2 Then
                        var_Numero = 0
                    End If
                Else
                    var_Numero = var_Numero.Replace(",", "")
                    var_Numero = var_Numero.Replace(".", "")
                End If
            Else
                var_Numero = 0
            End If
        Else
            var_Numero = 0
        End If
        Return var_Numero
    End Function

    Shared Function formato_Numero_Pantalla(ByVal var_numero As Object) As String
        var_numero = var_numero.ToString
        Dim var_config_reg As Boolean = System.Globalization.CultureInfo.CurrentCulture.NumberFormat.CurrencyDecimalSeparator = ","
        Dim var_resultado As String = ""
        Dim var_partes() As String = IIf(var_config_reg, var_numero.Split(","), var_numero.Split("."))
        Dim var_contador As Integer = 0
        For i As Integer = var_partes(0).Length - 1 To 0 Step -1
            If var_contador = 3 Then
                If var_partes(0).Chars(i) <> "-" Then
                    var_resultado = "." & var_resultado
                End If
                var_contador = 1
            Else
                var_contador += 1
            End If
            var_resultado = var_partes(0).Chars(i) + var_resultado
        Next

        If var_partes.GetLongLength(0) > 1 Then
            If var_partes(1).ToString.Length < 2 Then
                var_resultado = var_resultado & "," & var_partes(1) & "0"
            Else
                var_resultado = var_resultado & "," & var_partes(1)
            End If
        Else
            var_resultado = var_resultado & ",00"
        End If

        Return var_resultado
    End Function

    Shared Function MontoEscrito(ByVal Monto) As String
        Dim AMT
        Dim N
        Dim M
        Dim k
        Dim L
        Dim Rtn_String
        N = "Un    Dos   Tres  CuatroCinco Seis  Siete Ocho  Nueve "
        M = "Diez      Once      Doce      Trece     Catorce   Quince    Dieciseis DiecisieteDieciocho Diecinueve"
        k = "Veinte   Treinta  Cuarenta CincuentaSesenta  Setenta  Ochenta  Noventa  "
        L = "Cien         Doscientos   Trescientos  CuatrocientosQuinientos   Seiscientos  Setecientos  Ochocientos  Novecientos  "
        If Monto = 0 Then
            MontoEscrito = ""
            Exit Function
        End If
        AMT = FormatNumber(Monto, 2, -1, 0, 0)
        'response.Write(AMT & "<br>")
        'AMT = REPLACE(AMT, ",", "")
        'response.Write(AMT& "<br>")
        If Len(AMT) < 12 Then
            For i As Integer = 1 To 12 - Len(AMT)
                AMT = "0" & AMT
            Next
            'AMT = String(12 - Len(AMT), "0") & AMT
            'response.Write(AMT& "<br>")
        End If
        Rtn_String = ""
        If Mid(AMT, 1, 1) = 1 Then     ' 100 - 900 MILLONES
            Rtn_String = Trim(Mid(L, ((Mid(AMT, 1, 1) - 1) * 13) + 1, 13))
            If Trim(Mid(AMT, 1, 3)) > "100" Then
                Rtn_String = Trim(Rtn_String) & "to"
            End If
        ElseIf Mid(AMT, 1, 1) > 1 Then
            Rtn_String = Trim(Mid(L, ((Mid(AMT, 1, 1) - 1) * 13) + 1, 13))
        End If
        If Mid(AMT, 2, 1) = 1 Then     ' 10 - 99 MILLONES
            Rtn_String = Trim(Rtn_String) & " " & Mid(M, (Mid(AMT, 3, 1) * 10) + 1, 10)
        ElseIf Mid(AMT, 2, 1) > 1 Then
            Rtn_String = Trim(Rtn_String) & " " & Mid(k, ((Mid(AMT, 2, 1) - 2) * 9) + 1, 9)
            If Mid(AMT, 3, 1) > 0 Then
                Rtn_String = Trim(Rtn_String) & " y " & Mid(N, ((Mid(AMT, 3, 1) - 1) * 6) + 1, 6)
            End If
        ElseIf Mid(AMT, 3, 1) > 0 Then  ' 1 - 9 MILLONES
            Rtn_String = Trim(Rtn_String) & " " & Mid(N, ((Mid(AMT, 3, 1) - 1) * 6) + 1, 6)
        End If
        If Trim(Rtn_String) <> "" Then
            If Mid(AMT, 1, 3) > 1 Then
                Rtn_String = Trim(Rtn_String) & " Millones "
            Else
                Rtn_String = Trim(Rtn_String) & " Millón "
            End If
        End If

        If Mid(AMT, 4, 1) = 1 Then    ' 100 - 900 MIL
            Rtn_String = Trim(Rtn_String) & " " & Trim(Mid(L, ((Mid(AMT, 4, 1) - 1) * 13) + 1, 13))
            If Mid(AMT, 4, 3) > "100" Then
                Rtn_String = Trim(Rtn_String) & "to"
            End If
        ElseIf Mid(AMT, 4, 1) > 1 Then
            Rtn_String = Trim(Rtn_String) & " " & Mid(L, (((Mid(AMT, 4, 1) - 1) * 13) + 1), 13)
        End If
        If Mid(AMT, 5, 1) = 1 Then      ' 10 - 19 Miles
            Rtn_String = Trim(Rtn_String) & " " & Mid(M, (((Mid(AMT, 6, 1)) * 10) + 1), 10)
        ElseIf Mid(AMT, 5, 1) > 1 Then  ' 20 - 99 Miles
            Rtn_String = Trim(Rtn_String) & " " & Mid(k, (((Mid(AMT, 5, 1) - 2) * 9) + 1), 9)
            If Mid(AMT, 6, 1) > 0 Then   ' 2? - 9? Miles
                Rtn_String = Trim(Rtn_String) & " y " & Mid(N, (((Mid(AMT, 6, 1) - 1) * 6) + 1), 6)
            End If
        ElseIf Mid(AMT, 6, 1) > 0 Then   ' 1  - 9 Miles
            Rtn_String = Trim(Rtn_String) & " " & Mid(N, (((Mid(AMT, 6, 1) - 1) * 6) + 1), 6)
        End If
        If Mid(AMT, 1, 6) <> "000000" And Mid(AMT, 4, 3) <> "000" Then
            Rtn_String = Trim(Rtn_String) & " Mil "
        End If
        If Mid(AMT, 7, 1) = 1 Then
            Rtn_String = Trim(Rtn_String) & " " & Mid(L, (((Mid(AMT, 7, 1) - 1) * 13) + 1), 13)
            If Trim(Mid(AMT, 7, 3)) > "100" Then
                Rtn_String = Trim(Rtn_String) & "to"
            End If
        ElseIf Mid(AMT, 7, 1) > 1 Then
            Rtn_String = Trim(Rtn_String) & " " & Mid(L, (((Mid(AMT, 7, 1) - 1) * 13) + 1), 13)
        End If

        If Mid(AMT, 8, 1) = 1 Then
            Rtn_String = Trim(Rtn_String) & " " & Mid(M, ((Mid(AMT, 9, 1) * 10) + 1), 10)
        ElseIf Mid(AMT, 8, 1) > 1 Then
            Rtn_String = Trim(Rtn_String) & " " & Mid(k, (((Mid(AMT, 8, 1) - 2) * 9) + 1), 9)
            If Mid(AMT, 9, 1) > 0 Then
                Rtn_String = Trim(Rtn_String) & " y " & Mid(N, (((Mid(AMT, 9, 1) - 1) * 6) + 1), 6)
            End If
        ElseIf Mid(AMT, 9, 1) > 0 Then
            Rtn_String = Trim(Rtn_String) & " " & Trim(Mid(N, (((Mid(AMT, 9, 1) - 1) * 6) + 1), 6))
            If Mid(AMT, 9, 1) = 1 Then
                Rtn_String = Trim(Rtn_String) & "o"
            End If
        End If
        If Trim(Rtn_String) <> "" Then
            Rtn_String = Trim(Rtn_String) & " con "
        End If
        Rtn_String = Trim(Rtn_String) & " " & Mid(AMT, 11, 2) & "/100"
        'Rtn_String = Trim(Rtn_String) & " " & String(120, "*")
        MontoEscrito = Rtn_String
    End Function

End Class

