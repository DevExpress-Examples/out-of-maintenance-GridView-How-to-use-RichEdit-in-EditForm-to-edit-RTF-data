@Imports WebApplicationVB
@Imports DevExpress.XtraRichEdit
@Imports System.Text

@ModelType RichEditModel

@Html.DevExpress().RichEdit(Sub(settings)
	settings.Name = "richEdit"
	settings.CallbackRouteValues = New With {Key .Controller = "Home", Key .Action = "RichEditPartial", Key .documentID = Model.documentID}
	settings.Init = Sub(sender, e)
		Dim richEdit = TryCast(sender, MVCxRichEdit)
		richEdit.CreateDefaultRibbonTabs(True)
		richEdit.RibbonTabs(0).Visible = False
	End Sub
End Sub).Open(Model.documentID, DocumentFormat.Rtf, Function()
	If Model.rtfContent <> "" Then
		Return Encoding.UTF8.GetBytes(Model.rtfContent)
	Else
		Return Encoding.UTF8.GetBytes((New RichEditDocumentServer()).RtfText)
	End If
End Function).GetHtml()