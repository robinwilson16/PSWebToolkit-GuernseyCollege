<%@ WebHandler Language="VB" Class="PDFPrint" %>


Imports System.IO
Imports CompassCC.CCCSystem.CCCCommon
Imports System.Drawing


Public Class PDFPrint : Implements IHttpHandler

    Public ReadOnly Property CourseInformationID() As Integer
        Get
            If HttpContext.Current.Request("CourseInformationID") Is Nothing Then
                Return -1
            Else
                Return CInt(HttpContext.Current.Request("CourseInformationID"))
            End If
        End Get
    End Property

    Public Sub ProcessRequest(ByVal context As HttpContext) Implements IHttpHandler.ProcessRequest
        Dim ms As New MemoryStream()

        'build html in code rather than pass the rendered page over the network
        Dim html As New StringBuilder

        Dim img As Image = Image.FromFile(HttpContext.Current.Server.MapPath(GetResourceValue("collegeLogo")))
        Dim base As String = ImageToBase64(img, Imaging.ImageFormat.Png)

        html.AppendLine("<img style='width:200px' src='data:image/png;base64," & base & "' />")
        'get course info
        Dim tblCourseInfo As New CourseInformationDataTable
        Dim v As CCCDataViewDataSet = CCCDataViewDataSet.CreateDataView(tblCourseInfo)
        v.Columns.AddDBColumns(False, False)
        v.Filters.SetColumnFilter(tblCourseInfo.CourseInformationIDColumn, Me.CourseInformationID)
        tblCourseInfo.TableAdapter.Load(tblCourseInfo, v)
        If tblCourseInfo.Count > 0 Then
            html.AppendLine("<h2>" & tblCourseInfo(0).Description & "</h2>")
        End If

        'get offerings for that course info
        Dim tblOffering As New OfferingDataTable
        Dim vOffering As CCCDataViewDataSet = CCCDataViewDataSet.CreateDataViewDefault(tblOffering)
        vOffering.Columns.AddPKColumns()
        vOffering.Columns.EnsureColumnsAreSelected(True, False, tblOffering.StartTimeColumn, tblOffering.StartDateColumn, tblOffering.EndDateColumn, tblOffering.SiteDescriptionColumn, tblOffering.EndTimeColumn, tblOffering.WebSiteAvailabilityIDColumn, tblOffering.TotalFeeAmountColumn)
        vOffering.Columns.EnsureColumnIsSelected(False, False, tblOffering.KISCourseCodeColumn)
        vOffering.Filters.StartBracketBlock()
        vOffering.Filters.AddRow(LogicAndOr.And, tblOffering.CourseInformationIDColumn, FilterOperator.OperatorEquals, Me.CourseInformationID, False)
        vOffering.Filters.AddRow(LogicAndOr.And, tblOffering.WebSiteAvailabilityIDColumn, FilterOperator.OperatorNotEquals, CInt(WebSiteAvailability.Notavailableonwebsite), False)
        vOffering.Filters.EndBracketBlock()

        tblOffering.TableAdapter.Load(tblOffering, vOffering)

        If tblOffering.Rows.Count > 0 Then
            html.AppendLine("<table style='width:100%;text-align:center;'><tbody><tr><th>Course Name</th><th>Start Date</th><th>End Date</th><th>Site</th><th>Total Fees</th></tr>")
            For Each rowO As OfferingRow In tblOffering
                html.AppendLine(String.Format("<tr><td>{0}</td><td>{1}</td><td>{2}</td><td>{3}</td><td>{4}</td></tr>", CCCBlankLibrary.NoBlank(rowO.Name, "&nbsp;"), IIf(CDate(rowO.StartDate.GetValueOrDefault).ToShortDateString = "01/01/0001", "&nbsp;", CDate(rowO.StartDate.GetValueOrDefault).ToShortDateString), IIf(CDate(rowO.EndDate.GetValueOrDefault).ToShortDateString = "01/01/0001", "&nbsp;", CDate(rowO.EndDate.GetValueOrDefault).ToShortDateString), CCCBlankLibrary.NoBlank(rowO.SiteDescription, "&nbsp;"), CCCBlankLibrary.NoBlank(rowO.TotalFeeAmount, "&nbsp;")))
            Next
            html.AppendLine("</tbody></table>")
        End If

        'get info text
        Dim tblACourseInfoText As New CourseInformationTextDataTableAdapter
        Dim tblCI As CourseInformationTextDataTable = tblACourseInfoText.GetCourseInformationTypeConcatenated(CourseInformationID)

        For Each row As CourseInformationTextRow In tblCI
            html.AppendLine("<h3>" & row.CourseInformationType & "</h3>")
            html.AppendLine()
            html.AppendLine("<p>" & row.InformationText & "</p>")
            html.AppendLine()
        Next

        Dim pdf = TheArtOfDev.HtmlRenderer.PdfSharp.PdfGenerator.GeneratePdf(html.ToString, PdfSharp.PageSize.A4)


        pdf.Save(ms, False)



        With context.Response
            .ClearHeaders()
            .ClearContent()
            .ContentType = "application/pdf"
            .AddHeader("Content-Disposition", "attachment;filename=CourseInformation-" + Me.CourseInformationID.ToString + ".pdf")
            ms.WriteTo(.OutputStream)
            .Flush()
            '.Close() ''CCCPS-74852: This method is creating issue for some of the customer. We used context.ApplicationInstance.CompleteRequest() (as placed below) instead as suggested by Microsoft (https://docs.microsoft.com/en-us/dotnet/api/system.web.httpresponse.close?view=netframework-4.8)
        End With
        context.ApplicationInstance.CompleteRequest() ''CCCPS-74852
    End Sub

    Public ReadOnly Property IsReusable() As Boolean Implements IHttpHandler.IsReusable
        Get
            Return False
        End Get
    End Property

    Private Function ImageToBase64(image As Image, format As System.Drawing.Imaging.ImageFormat) As String
        Using ms As New MemoryStream()
            ' Convert Image to byte[]
            image.Save(ms, format)
            Dim imageBytes As Byte() = ms.ToArray()

            ' Convert byte[] to Base64 String
            Dim base64String As String = Convert.ToBase64String(imageBytes)
            Return base64String
        End Using
    End Function


End Class