Imports CompassCC.CCCSystem.CCCCommon
Imports CompassCC.ProSolution.PSWebEnrolmentKit
Imports System.Reflection



Partial Class webcontrols_search_horizontal
    Inherits webenrolmentcontrolvalidate

    Protected Overrides Sub OnLoad(ByVal e As System.EventArgs)
        MyBase.OnLoad(e)



        'This ties the search panel to the grid, so the grid gets loaded with data using the criteria selected
        Me.CourseSearchPanel_ListView.Grid = Me.SearchGrid
        Me.CourseSearchPanel_ListView.ImageTable = Me.listviewGraphic
        Me.CourseSearchPanel_ListView.DataPager = Me.dpSearchGrid

        'Custom search param (see also CourseSearchPanel.LoadImageTable)
        If Not IsPostBack Then
            'If Not CCCBlankLibrary.IsBlank(Me.Request.QueryString.Item("search")) Then
            Page.Session("SearchText") = Me.Request.QueryString.Item("SearchText")
            Page.Session("MarketingField1") = Me.Request.QueryString.Item("SubjectArea")
            Page.Session("Code") = Me.Request.QueryString.Item("Code")
            Page.Session("AttendanceMode") = Me.Request.QueryString.Item("AttendanceMode")

            If Me.Request.QueryString.Keys.Count > 1 Then
                CourseSearchPanel_ListView.DoSearch()

                Dim isreadonly As PropertyInfo = GetType(System.Collections.Specialized.NameValueCollection).GetProperty("IsReadOnly", BindingFlags.Instance Or BindingFlags.NonPublic)
                ' make collection editable
                isreadonly.SetValue(Me.Request.QueryString, False, Nothing)
                ' remove
                Me.Request.QueryString.Remove("SearchText")
                Me.Request.QueryString.Remove("SubjectArea")
                Me.Request.QueryString.Remove("Code")
                Me.Request.QueryString.Remove("AttendanceMode")

                imgdiv.Visible = False
                dpSearchGrid.Visible = False


            Else
                imgdiv.Visible = True
                dpSearchGrid.Visible = True
            End If
        Else
            dpSearchGrid.Visible = True
        End If
    End Sub



End Class
