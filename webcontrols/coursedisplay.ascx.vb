Option Explicit Off
Option Strict Off

Imports CompassCC.CCCSystem.CCCCommon
Imports CompassCC.ProSolution.PSWebEnrolmentKit
Imports CompassCC.ProSolution.Business


Partial Class coursedisplay
    Inherits webenrolmentcontrolvalidate

    Public ReadOnly Property CourseInformationID() As Integer
        Get
            If Me.Request("CourseInformationID") Is Nothing Then
                Return -1
            Else
                Return CInt(Request("CourseInformationID"))
            End If
        End Get
    End Property

    Protected Overrides Sub OnLoad(ByVal e As System.EventArgs)
        MyBase.OnLoad(e)

        Session.Item("_CCCCallingPage") = Me.Request.RawUrl

        btnPrintPDF.NavigateUrl = "~/Handlers/PDFPrint.ashx?CourseInformationID=" & CourseInformationID

        'Load main courseinfo record (to get title)
        Dim tblCourseInfo As New CourseInformationDataTable
        Dim v As CCCDataViewDataSet = CCCDataViewDataSet.CreateDataView(tblCourseInfo)
        v.Columns.AddDBColumns(False, False)
        v.Filters.SetColumnFilter(tblCourseInfo.CourseInformationIDColumn, CourseInformationID)
        tblCourseInfo.TableAdapter.Load(tblCourseInfo, v)
        If tblCourseInfo.Count > 0 Then
            lblCourseInfoTitle.Text = tblCourseInfo(0).Description
        End If

        'Load all offerings that map to this CourseInfo
        Dim tblOffering As New OfferingDataTable
        Dim vOffering As CCCDataViewDataSet = CCCDataViewDataSet.CreateDataViewDefault(tblOffering)
        vOffering.Columns.AddPKColumns()
        vOffering.Columns.EnsureColumnsAreSelected(True, False, tblOffering.StartTimeColumn, tblOffering.StartDateColumn, tblOffering.EndDateColumn, tblOffering.SiteDescriptionColumn, tblOffering.EndTimeColumn, tblOffering.WebSiteAvailabilityIDColumn, tblOffering.TotalFeeAmountColumn)
        vOffering.Columns.EnsureColumnIsSelected(False, False, tblOffering.KISCourseCodeColumn)

        vOffering.Filters.StartBracketBlock()
        vOffering.Filters.AddRow(LogicAndOr.And, tblOffering.CourseInformationIDColumn, FilterOperator.OperatorEquals, Me.CourseInformationID, False)
        vOffering.Filters.AddRow(LogicAndOr.And, tblOffering.WebSiteAvailabilityIDColumn, FilterOperator.OperatorNotEquals, CInt(WebSiteAvailability.Notavailableonwebsite), False)
        'vOffering.Filters.AddRow(LogicAndOr.And, tblOffering.AcademicYearIDColumn, FilterOperator.OperatorEquals, CCCConfigLibrary.GetValue("AcademicYearID"), False)
        'vOffering.Filters.AddRow(LogicAndOr.And, tblOffering.AllowDirectEnrolmentColumn, FilterOperator.OperatorEquals, True, False)

        vOffering.Filters.EndBracketBlock()


        tblOffering.TableAdapter.Load(tblOffering, vOffering)

        Dim template As New TemplateField
        Dim OfferingIDsInBasket As New System.Collections.Generic.List(Of Integer)
        For Each item As ShoppingCartItem In WorkingData.ShoppingCart.Items
            OfferingIDsInBasket.Add(item.OfferingID)
        Next

        template.ItemTemplate = New EnrolOrApplyTemplate(OfferingIDsInBasket)


        template.HeaderStyle.CssClass = "searchgridheader"
        If GridView1.Columns(GridView1.Columns.Count - 1).GetType Is GetType(TemplateField) Then
            GridView1.Columns.RemoveAt(GridView1.Columns.Count - 1)
        End If
        GridView1.Columns.Add(template)

        Me.GridView1.DataSource = tblOffering
        Me.GridView1.DataBind()
        Me.GridView1.Visible = True

        'Load course info text
        Dim tblACourseInfoText As New CourseInformationTextDataTableAdapter

        Me.CourseInfoTextList.DataSource = tblACourseInfoText.GetCourseInformationTypeConcatenated(CourseInformationID)
        Me.CourseInfoTextList.DataBind()

        LoadCourseInformationImages()
        LoadKISWidget(tblOffering)

    End Sub


    Private Sub LoadCourseInformationImages()
        Dim tblCII As New CourseInformationImageDataTable
        Dim v As CCCDataViewDataSet = CCCDataViewDataSet.CreateDataView(tblCII)
        v.Columns.AddDBColumns(False, False)
        v.Filters.SetColumnFilter(tblCII.CourseInformationIDColumn, Me.CourseInformationID)
        tblCII.TableAdapter.Load(tblCII, v)

        If tblCII.Rows.Count > 0 Then
            marketingimages.DataSource = tblCII
            marketingimages.DataBind()
            marketingimages.Visible = True
        Else
            marketingimages.Visible = False
        End If
    End Sub


    Protected Sub listviewGraphic_ItemDataBound(sender As Object, e As ListViewItemEventArgs) Handles marketingimages.ItemDataBound
        Dim item As ListViewItem = e.Item
        ' Verify if the item is a data item.
        If item.ItemType = ListViewItemType.DataItem Then
            'Get the specific data we are working with in this ListViewItem
            Dim rowView As System.Data.DataRowView
            rowView = CType(CType(e.Item, System.Web.UI.WebControls.ListViewDataItem).DataItem, Data.DataRowView)


            'Setup the image based on the rowview (name, thumbnail etc)
            Dim img As ImageButton = CType(e.Item.FindControl("btnMarketingGraphic"), ImageButton)
            Dim lbl As Label = e.Item.FindControl("lblImage")

            Dim Value As String = CStr(rowView("CourseInformationImageID"))
            Dim strDescription As String
            Try
                strDescription = CStr(rowView("Description"))
            Catch ex As Exception
                strDescription = ""
            End Try


            lbl.Text = strDescription

            img.ImageUrl = String.Format("~/Handlers/ImageHandler.ashx?CourseInformationImageID={0}", Value)
            img.AlternateText = strDescription
            img.Attributes.Add("runat", "server")
            img.Attributes.Add("OnClick", String.Format("javascript:window.open( 'Handlers/ImageHandler.ashx?CourseInformationImageID={0}','FullSize', '' );return false;", Value))
        End If


    End Sub

    Private Sub LoadKISWidget(tblOffering As OfferingDataTable)
        Dim offeringCount As Integer


        'Counts how many offerings are tied to the leaflet
        offeringCount = CInt(CStr(tblOffering.Rows.Count()))

        'If no offerings then we don't need the KIS widget.  This avoids error on tblOffering, which wont exist.
        If offeringCount = 0 Then

            'No widget, so empty html
            KISWidget1.Visible = False
        Else

            'Assume no widget by default
            KISWidget1.Visible = False

            'Loop through each offering to see if there is a KISCourseCode
            For i = 0 To offeringCount - 1

                'If a KISCourseCode is found against any offering, then show the widget with the following html
                If IsNothing(tblOffering(i).KISCourseCode) = False Then
                    KISWidget1.Visible = True
                    KISWidget1.KISCourseCode = tblOffering(i).KISCourseCode
                    Dim isPartTime As Boolean = False
                    Dim tblKISCourse As New KISCourseDataTable
                    isPartTime = CBool(CCCBlankLibrary.NoBlank(tblKISCourse.TableAdapter.DLookup(tblKISCourse.PartTimeOnlyColumn, tblKISCourse.CodeColumn, tblOffering(i).KISCourseCode), 0))
                    If isPartTime Then KISWidget1.KISMode = "Part Time" Else KISWidget1.KISMode = "Full Time"

                End If

            Next i

        End If


    End Sub
End Class

