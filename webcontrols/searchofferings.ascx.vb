Imports CompassCC.CCCSystem.CCCCommon
Imports CompassCC.ProSolution.PSWebEnrolmentKit
Imports System.Reflection

'NOTE: Use this page (searchofferings.ascx) when searching offerings directly without using CourseInformation.
'(Use search.ascx or searchnew.ascx when searching CourseInformation and then clicking link to move on to Offerings)

Partial Class webcontrols_searchofferings
    Inherits webenrolmentcontrolvalidate

    Protected Overrides Sub OnLoad(ByVal e As System.EventArgs)
        MyBase.OnLoad(e)

        If Not IsPostBack Then

            PopulateDropDownLists()

        End If

    End Sub

    Private Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        LoadGridData()
    End Sub

    Private Sub btnShowAll_Click(sender As Object, e As EventArgs) Handles btnShowAll.Click
        'Clear search values first...
        Me.txtKeywords.Text = ""
        cboCollegeLevel.SelectedValue = Nothing
        cboSite.SelectedValue = Nothing
        'Then load grid
        LoadGridData()
    End Sub

    Dim m_CoursesInBasket As System.Collections.Generic.List(Of Integer)

    'These properties will hode/show the example extra search fields
    Private ShowSite As Boolean = True
    Private ShowCollegeLevel As Boolean = True

    Private Sub PopulateDropDownLists()
        'Populate drop down lists etc.
        '... or alteratvively hide the saerch fields that aren't being used

        If ShowSite Then
            cboSite.Items.Add(New WebControls.ListItem("All", ""))
            Dim strExcludedSiteIDs = ""
            'NOTE: PopulateListWithDescriptionColumns is a v simple way of using CCCListControlLibrary
            CCCListControlLibrary.PopulateListWithDescriptionColumns(New SiteDataTable, cboSite, True, strExcludedSiteIDs)
        Else
            SearchAreaForSite.Visible = False
        End If

        If ShowCollegeLevel Then
            'NOTE: PopulateList with more flexible options on creating the lookup using the CCCListControlLibrary
            Dim t = New CollegeLevelDataTable
            Dim v = CCCDataViewDataSet.CreateDataViewDefault(t)
            v.Filters.SetColumnFilter(t.SIDColumn, 0, FilterOperator.OperatorNotEquals)  'Don't include "0, -----" (top level college level) by default
            'v.Filters.SetColumnFilter(t.DescriptionColumn, "a*", FilterOperator.OperatorNotLike)  'This example makes it only show CollegeLvels that don't start aith "a" (Or "A")
            'v.Filters.SetColumnFilter(t.IsAvailableForOfferingsColumn, True)  'This example filters on the CollegeLevel being available for assigning to offerings
            Dim l = New CCCLookup(t, v)
            CCCListControlLibrary.PopulateList(l, cboCollegeLevel, True)
        Else
            SearchAreaForCollegeLevel.Visible = False
        End If

    End Sub

#Region " Grid "

    Sub LoadGridData()

        Dim t As New OfferingDataTable

        Dim v As CCCDataViewDataSet = CCCDataViewDataSet.CreateDataViewDefault(t)
        v.Columns.Clear()
        v.Columns.AddPKColumns()
        v.Columns.EnsureColumnsAreSelected(True, False, t.NameColumn, t.AcademicYearIDColumn, t.WebSiteAvailabilityIDColumn, t.StartDateColumn, t.EndDateColumn, t.SiteColumn, t.TotalFeesColumn)
        v.Filters.SetColumnFilter(t.WebSiteAvailabilityIDColumn, 0, FilterOperator.OperatorNotEquals) 'Don't show not available
        v.SortOrders.Clear()
        v.SortOrders.SetColumnSortOrder(SortDirection.Ascending, t.NameColumn)

        'Add Search
        Dim SearchText = Me.txtKeywords.Text
        Dim ColumnsToSearch = {t.NameColumn}
        v.Filters.AddSearchColumns(ColumnsToSearch, SearchText)

        'Add filters as well (search will only return results above the data rows that matches these filters)
        If Not CCCBlankLibrary.IsBlank(cboCollegeLevel.SelectedValue) Then
            v.Filters.SetColumnFilter(t.SIDColumn, cboCollegeLevel.SelectedValue)
        End If
        If Not CCCBlankLibrary.IsBlank(cboSite.SelectedValue) Then
            v.Filters.SetColumnFilter(t.SiteIDColumn, cboSite.SelectedValue)
        End If

        t.TableAdapter.Load(t, v)

        SearchGrid.DataSource = t

        m_CoursesInBasket = New System.Collections.Generic.List(Of Integer)
        For Each item As ShoppingCartItem In WorkingData.ShoppingCart.Items
            m_CoursesInBasket.Add(item.OfferingID)
        Next

        SearchGrid.DataBind()

    End Sub

    Private Sub m_Grid_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.ListViewItemEventArgs) Handles SearchGrid.ItemDataBound#
        Dim item As ListViewItem = e.Item
        ' Verify if the item is a data item.
        If item.ItemType = ListViewItemType.DataItem Then
            Dim rowView As System.Data.DataRowView
            rowView = CType(e.Item, System.Web.UI.WebControls.ListViewDataItem).DataItem

            Dim strNavigateUrl = GetNavigateUrlForOffering(rowView)

            Dim img As HyperLink = e.Item.FindControl("ApplyOrEnrolmentBtn")
            SetWebSiteAvailabilityIcon(img, rowView)
            img.NavigateUrl = strNavigateUrl

            Dim link As HyperLink = e.Item.FindControl("hlnkDescription")
            link.NavigateUrl = strNavigateUrl

            If rowView.Row.Table.Columns.Contains("_SearchScore") Then
                Dim lbl As Label = e.Item.FindControl("lblScore")
                FormatScore(lbl, rowView)
            End If


        End If
    End Sub

    Dim m_blnPageChanged As Boolean = False
    Private Sub m_Grid_PagePropertiesChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles SearchGrid.PagePropertiesChanged
        LoadGridData()
        m_blnPageChanged = True
    End Sub

    Private Sub m_DataPager_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles dpSearchGrid.PreRender
        If ViewState("Searched") = True AndAlso Not m_blnPageChanged Then
            dpSearchGrid.SetPageProperties(0, dpSearchGrid.MaximumRows, True)
        End If
    End Sub

    Private Sub FormatScore(ByVal lblScore As Label, ByVal rowView As System.Data.DataRowView)
        Dim dataValue As String = rowView("_SearchScore").ToString
        lblScore.Text = CInt(dataValue).ToString & "%"
    End Sub

    Private Sub SetWebSiteAvailabilityIcon(ByVal img As HyperLink, ByVal rowView As System.Data.DataRowView)
        Dim dataValue As String = rowView("WebSiteAvailabilityID").ToString
        Dim intOfferingID As Integer = CInt(rowView("OfferingID"))
        Dim imageURL As String = String.Empty
        Dim imageText As String = String.Empty

        If m_CoursesInBasket.Contains(intOfferingID) Then
            imageText = "Course already in basket"
            imageURL = GetResourceValue("basketsmall")
        ElseIf CCCBlankLibrary.IsBlank(dataValue) Then
            imageURL = GetResourceValue("icon_noofferings")
        Else
            Dim Availability As Byte = CType(dataValue, Byte)
            Dim row As WebSiteAvailabilityRow = WebSiteAvailabilityTable.FindByWebSiteAvailabilityID(Availability)
            If row IsNot Nothing Then
                imageText = row.Description
                If Len(row.IconFileName) = 0 Then
                    imageURL = GetResourceValue("gap")
                Else
                    imageURL = GetResourceValue(Replace(row.IconFileName, ".png", ""))
                End If
            Else
                imageURL = GetResourceValue("icon_noofferings")
            End If

        End If

        img.Text = imageText
        img.ImageUrl = imageURL
    End Sub

    Private Function GetNavigateUrlForOffering(ByVal rowView As System.Data.DataRowView) As String
        Dim dataValue As String = rowView("WebSiteAvailabilityID").ToString
        Dim intOfferingID As Integer = CInt(rowView("OfferingID"))

        Dim NavigateUrl As String = String.Empty

        If m_CoursesInBasket.Contains(intOfferingID) Then
            NavigateUrl = GetResourceValue("checkout_aspx")
        ElseIf CCCBlankLibrary.IsBlank(dataValue) Then
            NavigateUrl = GetResourceValue("courseunavailable_aspx")
        Else
            Dim Availability As Byte = CType(dataValue, Byte)
            Dim row As WebSiteAvailabilityRow = WebSiteAvailabilityTable.FindByWebSiteAvailabilityID(Availability)
            If row IsNot Nothing Then
                Dim blnApply = True
                If Len(row.WebPageFileName) > 0 Then
                    Dim strWebPageResourceName = row.WebPageFileName.Replace(".", "_")
                    Dim strPageURL = GetResourceValue(strWebPageResourceName)
                    If Len(strPageURL) > 0 Then
                        NavigateUrl = strPageURL & "&OfferingID=" & intOfferingID.ToString
                    End If
                End If
            Else
                NavigateUrl = GetResourceValue("courseunavailable_aspx")
            End If
        End If

        Return NavigateUrl
    End Function

    Private m_tblWebSiteAvailability As WebSiteAvailabilityDataTable
    Private Property WebSiteAvailabilityTable() As WebSiteAvailabilityDataTable
        Get
            If m_tblWebSiteAvailability Is Nothing Then
                m_tblWebSiteAvailability = New WebSiteAvailabilityDataTable
                Dim v As CCCDataViewDataSet = CCCDataViewDataSet.CreateDataView(m_tblWebSiteAvailability)
                v.Columns.AddAllColumns(False, False)
                m_tblWebSiteAvailability.TableAdapter.Load(m_tblWebSiteAvailability, v)
            End If
            Return m_tblWebSiteAvailability
        End Get
        Set(ByVal value As WebSiteAvailabilityDataTable)
            m_tblWebSiteAvailability = value
        End Set
    End Property

#End Region

End Class
