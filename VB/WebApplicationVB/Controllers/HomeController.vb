Imports DevExpress.Web.Mvc
Imports DevExpress.XtraRichEdit

Public Class HomeController
	Inherits System.Web.Mvc.Controller

	Public Function Index() As ActionResult
		Return View()
	End Function

	<HttpPost, ValidateInput(False)>
	Public Function RichEditPartial(ByVal documentID As String) As ActionResult
		Dim model As New RichEditModel()
		model.documentID = documentID
		model.rtfContent = Encoding.UTF8.GetString(RichEditExtension.SaveCopy("richEdit", DocumentFormat.Rtf))
		Return PartialView("_RichEditPartial", model)
	End Function

	<ValidateInput(False)>
	Public Function GridViewPartial() As ActionResult
		Dim model = OleDbDataProvider.GetCars()
		Return PartialView("_GridViewPartial", model)
	End Function

	<HttpPost, ValidateInput(False)>
	Public Function GridViewPartialAddNew(ByVal item As Car) As ActionResult
		If ModelState.IsValid Then
			Try
				item.RtfContent = Encoding.UTF8.GetString(RichEditExtension.SaveCopy("richEdit", DocumentFormat.Rtf))
				'Note that data modifications are not allowed in online demos. 
				'To allow editing in local/offline mode, download the example 
				'and comment out the line below and in GridViewPartialUpdate action method respectively.

				'OleDbDataProvider.AddNewItem(item);
				ViewData("EditError") = "Data modifications are not allowed in online demos"
			Catch e As Exception
				ViewData("EditError") = e.Message
			End Try
		Else
			ViewData("EditError") = "Please, correct all errors."
		End If
		Dim model = OleDbDataProvider.GetCars()
		Return PartialView("_GridViewPartial", model)
	End Function

	<HttpPost, ValidateInput(False)>
	Public Function GridViewPartialUpdate(ByVal item As Car) As ActionResult
		If ModelState.IsValid Then
			Try
				item.RtfContent = Encoding.UTF8.GetString(RichEditExtension.SaveCopy("richEdit", DocumentFormat.Rtf))
				'OleDbDataProvider.UpdateItem(item);
				ViewData("EditError") = "Data modifications are not allowed in online demos"
			Catch e As Exception
				ViewData("EditError") = e.Message
			End Try
		Else
			ViewData("EditError") = "Please, correct all errors."
		End If
		Dim model = OleDbDataProvider.GetCars()
		Return PartialView("_GridViewPartial", model)
	End Function

	<HttpPost, ValidateInput(False)>
	Public Function GridViewPartialDelete(ByVal ID As Int32) As ActionResult
		If ID >= 0 Then
			Try
				OleDbDataProvider.DeleteItem(ID)
			Catch e As Exception
				ViewData("EditError") = e.Message
			End Try
		End If
		Dim model = OleDbDataProvider.GetCars()
		Return PartialView("_GridViewPartial", model)
	End Function

End Class