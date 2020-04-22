# GridView - How to use RichEdit in EditForm to edit RTF data
<!-- run online -->
**[[Run Online]](https://codecentral.devexpress.com/257868487/)**
<!-- run online end -->

This example shows the approach from the following thread in MVC: [How to use ASPxRichEdit to edit RTF data in ASPxGridView's EditForm](https://supportcenter.devexpress.com/ticket/details/t260978/how-to-use-aspxrichedit-to-edit-rtf-data-in-aspxgridview-s-editform).

RichEdit resides in a Partial View, and its DocumentID property is generated based on a grid row key. This DocumentID and RTF content are passed from the GridView's EditItemTemplate to the Partial View. The RichEdit's [CallbackRouteValues](https://docs.devexpress.com/AspNet/DevExpress.Web.Mvc.RichEditSettings.CallbackRouteValues) property has a parameter that preserves DocumentID values between callbacks.
After every callback when a grid row is added or edited, unnecessary documents are closed (see how the GridView's **AfterPerformCallback** event and the **RichEditHelper** class are implemented).
You can use the [SaveCopy](https://docs.devexpress.com/AspNet/DevExpress.Web.Mvc.RichEditExtension.SaveCopy.overloads) method to get modified content from the RichEdit control on the server side. This is necessary to prevent data loss when RichEdit sends callbacks, and also for saving the grid's row data.

***Note that data modifications are not allowed in online demos. To allow editing in local/offline mode, download the example and comment out the code line in the GridViewPartialAddNew and GridViewPartialUpdate action methods.***

*Files to look at*:

 - [HomeController.cs](./CS/WebApplicationCS/Controllers/HomeController.cs) (VB: [HomeController.vb](./VB/WebApplicationVB/Controllers/HomeController.vb))
 - [_GridViewPartial.cshtml](./CS/WebApplicationCS/Views/Home/_GridViewPartial.cshtml) (VB: [_GridViewPartial.vbhtml](./VB/WebApplicationVB/Views/Home/_GridViewPartial.vbhtml))
 - [_RichEditPartial.cshtml](./CS/WebApplicationCS/Views/Home/_RichEditPartial.cshtml) (VB: [_RichEditPartial.vbhtml](./VB/WebApplicationVB/Views/Home/_RichEditPartial.vbhtml))
 - [RichEditHelper.cs](./CS/WebApplicationCS/Models/RichEditHelper.cs) (VB: [RichEditHelper.vb](./VB/WebApplicationVB/Models/RichEditHelper.vb))
 - [RichModel.cs](./CS/WebApplicationCS/Models/RichModel.cs) (VB: [RichModel.vb](./VB/WebApplicationVB/Models/RichModel.vb))
 - [OleDbDataProvider.cs](./CS/WebApplicationCS/Models/OleDbDataProvider.cs) (VB: [OleDbDataProvider.vb](./VB/WebApplicationVB/Models/OleDbDataProvider.vb))
 - [Car.cs](./CS/WebApplicationCS/Models/Car.cs) (VB: [Car.vb](./VB/WebApplicationVB/Models/Car.vb))

**See also**:
[RichEdit - How to save and load documents from a database](https://supportcenter.devexpress.com/ticket/details/t352035/richedit-how-to-save-and-load-documents-from-a-database/)

