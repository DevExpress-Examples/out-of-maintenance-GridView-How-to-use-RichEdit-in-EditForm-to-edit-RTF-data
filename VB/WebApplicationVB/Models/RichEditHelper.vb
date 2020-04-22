Imports DevExpress.Web.Office

Public Class RichEditHelper
    Private Shared OpenedCanceledDocumentIDsSessionKey As String = "OpenedCanceledDocumentIDsSessionKey"
    Private Shared Property OpenedCanceledDocumentIDs() As List(Of String)
        Get
            If HttpContext.Current.Session(OpenedCanceledDocumentIDsSessionKey) Is Nothing Then
                HttpContext.Current.Session(OpenedCanceledDocumentIDsSessionKey) = New List(Of String)()
            End If
            Return DirectCast(HttpContext.Current.Session(OpenedCanceledDocumentIDsSessionKey), List(Of String))
        End Get
        Set(ByVal value As List(Of String))
            HttpContext.Current.Session(OpenedCanceledDocumentIDsSessionKey) = value
        End Set
    End Property
    Public Shared Function GetDocumentID(ByVal keyValue As String) As String
        Dim documentID As String
        'TODO: For per-user editing, construct the DocumentID using the row's key plus user info, 
        'for example, System.Web.UI.User.Identity.Name. 
        'Then, close the document for editing by this DocumentID for this user only.
        If keyValue = "" Then
            documentID = Guid.NewGuid().ToString()
        Else
            documentID = keyValue
        End If

        If Not OpenedCanceledDocumentIDs.Contains(documentID) Then
            OpenedCanceledDocumentIDs.Add(documentID)
        End If
        Return documentID
    End Function
    Public Shared Sub CloseUnnecessaryDocuments()
        For Each id As String In OpenedCanceledDocumentIDs
            DocumentManager.CloseDocument(id)
        Next id

        OpenedCanceledDocumentIDs.Clear()
    End Sub
End Class
