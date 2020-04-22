Imports System.Data.OleDb

Public Class OleDbDataProvider
    Private Shared connectionString As String = ConfigurationManager.ConnectionStrings("CarsDbConnection").ConnectionString
    Public Shared Function GetCars() As IEnumerable(Of Car)
        Dim cars As New List(Of Car)()
        Using dbc As New OleDbConnection(connectionString)
            Using cmd As OleDbCommand = dbc.CreateCommand()
                dbc.Open()
                cmd.CommandText = "SELECT [ID], [Trademark], [Model], [RtfContent] FROM [Cars]"
                Using reader As OleDbDataReader = cmd.ExecuteReader()
                    Do While reader.Read()
                        cars.Add(New Car With {.ID = Convert.ToInt32(reader("ID")), .TradeMark = reader("TradeMark").ToString(), .Model = reader("Model").ToString(), .RtfContent = reader("RtfContent").ToString()})
                    Loop
                End Using
                dbc.Close()
            End Using
        End Using
        Return cars
    End Function
    Public Shared Sub UpdateItem(ByVal item As Car)
        Using dbc As New OleDbConnection(connectionString)
            Using cmd As OleDbCommand = dbc.CreateCommand()
                cmd.CommandText = "UPDATE [Cars] SET [Trademark] = @TradeMark, [Model] = @Model, [RtfContent] = @RtfContent WHERE [ID] = @ID"
                cmd.Parameters.AddWithValue("@TradeMark", item.TradeMark)
                cmd.Parameters.AddWithValue("@Model", item.Model)
                cmd.Parameters.AddWithValue("@RtfContent", item.RtfContent)
                cmd.Parameters.AddWithValue("@ID", item.ID)
                dbc.Open()
                cmd.ExecuteNonQuery()
                dbc.Close()
            End Using
        End Using
    End Sub
    Public Shared Sub AddNewItem(ByVal item As Car)
        Using dbc As New OleDbConnection(connectionString)
            Using cmd As OleDbCommand = dbc.CreateCommand()
                cmd.CommandText = "INSERT INTO [Cars] ([Trademark], [Model], [RtfContent]) VALUES (@Trademark, @Model, @RtfContent)"
                cmd.Parameters.AddWithValue("@TradeMark", item.TradeMark)
                cmd.Parameters.AddWithValue("@Model", item.Model)
                cmd.Parameters.AddWithValue("@RtfContent", item.RtfContent)
                dbc.Open()
                cmd.ExecuteNonQuery()
                dbc.Close()
            End Using
        End Using
    End Sub
    Public Shared Sub DeleteItem(ByVal id As Integer)
        Using dbc As New OleDbConnection(connectionString)
            Using cmd As OleDbCommand = dbc.CreateCommand()
                cmd.CommandText = "DELETE * FROM [Cars] WHERE [ID] = @ID"
                cmd.Parameters.AddWithValue("@ID", id)

                dbc.Open()
                cmd.ExecuteNonQuery()
                dbc.Close()
            End Using
        End Using
    End Sub
End Class