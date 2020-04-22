@Imports WebApplicationVB

@ModelType System.Collections.Generic.IEnumerable(Of Car)

@Code
    Dim grid = Html.DevExpress().GridView(Of Car)(Sub(settings)
                                                      settings.Name = "gridView"
                                                      settings.CallbackRouteValues = New With {Key .Controller = "Home", Key .Action = "GridViewPartial"}
                                                      settings.SettingsEditing.AddNewRowRouteValues = New With {Key .Controller = "Home", Key .Action = "GridViewPartialAddNew"}
                                                      settings.SettingsEditing.UpdateRowRouteValues = New With {Key .Controller = "Home", Key .Action = "GridViewPartialUpdate"}
                                                      settings.SettingsEditing.DeleteRowRouteValues = New With {Key .Controller = "Home", Key .Action = "GridViewPartialDelete"}
                                                      settings.SettingsEditing.Mode = GridViewEditingMode.EditFormAndDisplayRow
                                                      settings.SettingsEditing.EditFormColumnCount = 1
                                                      settings.CommandColumn.Visible = True
                                                      settings.CommandColumn.ShowNewButtonInHeader = True
                                                      settings.CommandColumn.ShowEditButton = True
                                                      settings.KeyFields(Function(m) m.ID)
                                                      settings.Columns.Add(Function(m) m.TradeMark, Sub(column) column.EditFormSettings.VisibleIndex = 0)
                                                      settings.Columns.Add(Function(m) m.Model, Sub(column) column.EditFormSettings.VisibleIndex = 1)
                                                      settings.Columns.Add(Function(m) m.RtfContent, Sub(column)
                                                                                                         column.Visible = False
                                                                                                         column.EditFormSettings.Visible = DefaultBoolean.True
                                                                                                         column.EditFormSettings.VisibleIndex = 2
                                                                                                         column.SetEditItemTemplateContent(Sub(tc)
                                                                                                                                               Dim reModel As New RichEditModel()
                                                                                                                                               If Not tc.Grid.IsNewRowEditing Then
                                                                                                                                                   reModel.documentID = RichEditHelper.GetDocumentID(tc.KeyValue.ToString())
                                                                                                                                                   reModel.rtfContent = DataBinder.Eval(tc.DataItem, "RtfContent").ToString()
                                                                                                                                               Else
                                                                                                                                                   reModel.documentID = RichEditHelper.GetDocumentID("")
                                                                                                                                                   reModel.rtfContent = ""
                                                                                                                                               End If
                                                                                                                                               Html.RenderPartial("_RichEditPartial", reModel)
                                                                                                                                           End Sub)
                                                                                                     End Sub)
                                                      settings.AfterPerformCallback = Sub(sender, e)
                                                                                          If e.CallbackName = "ADDNEWROW" OrElse e.CallbackName = "STARTEDIT" OrElse e.CallbackName = "CANCELEDIT" Then
                                                                                              RichEditHelper.CloseUnnecessaryDocuments()
                                                                                          End If
                                                                                      End Sub
                                                  End Sub)
    If ViewData("EditError") IsNot Nothing Then
        grid.SetEditErrorText(CStr(ViewData("EditError")))
    End If


End Code

@grid.Bind(Model).GetHtml()
