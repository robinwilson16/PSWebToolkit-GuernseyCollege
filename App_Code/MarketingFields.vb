Imports CompassCC.CCCSystem
Imports CompassCC.CCCSystem.CCCCommon
Imports CompassCC.ProSolution.PSWebEnrolmentKit
Imports Microsoft.VisualBasic
''' <summary>
''' CCCPS-76485: A class created to isolate most of the logic related to generating Marketing Field filters and keep overall implementation well managed and simple.
''' </summary>
Public Class MarketingFields

    ''' <summary>
    ''' Whether to show Marketing Field 1 as a filter or not
    ''' </summary>
    Public ShowMarketingField1 As Boolean = False
    ''' <summary>
    ''' Caption for the Marketing Field 1 filter.
    ''' </summary>
    Public MarketingField1Caption As String = ""
    ''' <summary>
    ''' Course Type ID for which we need to display Marketing Field 1 filter.
    ''' </summary>
    Public MarketingField1CourseInformationTypeID As Integer = 0
    ''' <summary>
    ''' Whether to display Marketing Field 1 as a List of images with link to filter data based on particular course information type value.
    ''' </summary>
    Public UseMarketingField1ForImage As Boolean = False
    ''' <summary>
    ''' Whether to show Marketing Field 2 as a filter or not
    ''' </summary>
    Public ShowMarketingField2 As Boolean = False
    ''' <summary>
    ''' Caption for the Marketing Field 2 filter.
    ''' </summary>
    Public MarketingField2Caption As String = ""
    ''' <summary>
    ''' Course Type ID for which we need to display Marketing Field 2 filter.
    ''' </summary>
    Public MarketingField2CourseInformationTypeID As Integer = 0
    ''' <summary>
    ''' Whether to display Marketing Field 2 (based on selected Marketing Field 1) as a List of images with link to filter data based on particular course information type value.
    ''' </summary>
    Public UseMarketingField2ForSubImage As Boolean = False
    ''' <summary>
    ''' Whether to show Marketing Field 3 as a filter or not
    ''' </summary>
    Public ShowMarketingField3 As Boolean = False
    ''' <summary>
    ''' Caption for the Marketing Field 3 filter.
    ''' </summary>
    Public MarketingField3Caption As String = ""
    ''' <summary>
    ''' Course Type ID for which we need to display Marketing Field 3 filter.
    ''' </summary>
    Public MarketingField3CourseInformationTypeID As Integer = 0
    ''' <summary>
    ''' Control object of Marketing field 1
    ''' </summary>
    Public CtlMarketingField1 As WebControl
    ''' <summary>
    ''' Control object of Marketing field 2
    ''' </summary>
    Public CtlMarketingField2 As WebControl
    ''' <summary>
    ''' Control object of Marketing field 3
    ''' </summary>
    Public CtlMarketingField3 As WebControl

    Private _strMarketingField1 As String
    ''' <summary>
    ''' Marketing Field 1 Value
    ''' </summary>
    ''' <returns>String</returns>
    Public Property MarketingField1() As String
        Get
            Return getValue(CtlMarketingField1, _strMarketingField1)
        End Get
        Set(ByVal value As String)
            _strMarketingField1 = value 'This gets set in case control hasn't been created yet
            setValue(CtlMarketingField1, value)
        End Set
    End Property

    Private _strMarketingField2 As String
    ''' <summary>
    ''' Marketing Field 2 Value
    ''' </summary>
    ''' <returns>String</returns>
    Public Property MarketingField2() As String
        Get
            Return getValue(CtlMarketingField2, _strMarketingField2)
        End Get
        Set(ByVal value As String)
            _strMarketingField2 = value 'This gets set in case control hasn't been created yet
            setValue(CtlMarketingField2, value)
        End Set
    End Property

    Private _strMarketingField3 As String
    ''' <summary>
    ''' Marketing Field 3 Value
    ''' </summary>
    ''' <returns>String</returns>
    Public Property MarketingField3() As String
        Get
            Return getValue(CtlMarketingField3, _strMarketingField3)
        End Get
        Set(ByVal value As String)
            _strMarketingField3 = value 'This gets set in case control hasn't been created yet
            setValue(CtlMarketingField3, value)
        End Set
    End Property

    ''' <summary>
    ''' To create Marketing field controls dynamically based on user settings.
    ''' </summary>
    ''' <param name="isDesignMode">True if a control is being used on a designer</param>
    ''' <param name="searchAreaForMarketingFields">Parent control in which all marketing fields to be added</param>
    Public Sub CreateMarketingFieldControls(isDesignMode As Boolean, searchAreaForMarketingFields As HtmlGenericControl)
        If ShowMarketingField1 AndAlso MarketingField1CourseInformationTypeID <> 0 Then
            CtlMarketingField1 = createMarketingField(isDesignMode, MarketingField1CourseInformationTypeID)
            If CtlMarketingField1 IsNot Nothing Then
                searchAreaForMarketingFields.Visible = True
                searchAreaForMarketingFields.Controls.Add(New LiteralControl("<div class=""col-md-4 text-left"">" & MarketingField1Caption & "</div>"))
                searchAreaForMarketingFields.Controls.Add(New LiteralControl("<div class=""col-md-8 text-left"">"))

                CtlMarketingField1.CssClass = "searchBox textfield form-control"
                CtlMarketingField1.ID = "txtMF1"
                CtlMarketingField1.Style.Add("margin", "2px")
                searchAreaForMarketingFields.Controls.Add(CtlMarketingField1)
                searchAreaForMarketingFields.Controls.Add(New LiteralControl("</div>"))
            End If
        End If

        If ShowMarketingField2 AndAlso MarketingField2CourseInformationTypeID <> 0 Then
            CtlMarketingField2 = createMarketingField(isDesignMode, MarketingField2CourseInformationTypeID)
            If CtlMarketingField2 IsNot Nothing Then
                searchAreaForMarketingFields.Visible = True
                searchAreaForMarketingFields.Controls.Add(New LiteralControl("<div class=""col-md-4 text-left"">" & MarketingField2Caption & "</div>"))
                searchAreaForMarketingFields.Controls.Add(New LiteralControl("<div class=""col-md-8 text-left"">"))

                CtlMarketingField2.CssClass = "searchBox textfield form-control"
                CtlMarketingField2.ID = "txtMF2"
                CtlMarketingField2.Style.Add("margin", "2px")
                searchAreaForMarketingFields.Controls.Add(CtlMarketingField2)
                searchAreaForMarketingFields.Controls.Add(New LiteralControl("</div>"))
            End If
        End If

        If ShowMarketingField3 AndAlso MarketingField3CourseInformationTypeID <> 0 Then
            CtlMarketingField3 = createMarketingField(isDesignMode, MarketingField3CourseInformationTypeID)
            If CtlMarketingField3 IsNot Nothing Then
                searchAreaForMarketingFields.Visible = True
                searchAreaForMarketingFields.Controls.Add(New LiteralControl("<div class=""col-md-4 text-left"">" & MarketingField3Caption & "</div>"))
                searchAreaForMarketingFields.Controls.Add(New LiteralControl("<div class=""col-md-8 text-left"">"))

                CtlMarketingField3.CssClass = "searchBox textfield form-control"
                CtlMarketingField3.ID = "txtMF3"
                CtlMarketingField3.Style.Add("margin", "2px")
                searchAreaForMarketingFields.Controls.Add(CtlMarketingField3)
                searchAreaForMarketingFields.Controls.Add(New LiteralControl("</div>"))
            End If
        End If
    End Sub
    ''' <summary>
    ''' Populating marketing field control if it's combo.
    ''' </summary>
    ''' <param name="ctlMarketingField">Control to populate</param>
    ''' <param name="courseInformationTypeID">Course Info Type ID based on which control to be populated</param>
    ''' <param name="defaultValue">Default value to set as selected on combo</param>
    Public Sub PopulateMarketingField(ByVal ctlMarketingField As WebControl, ByVal courseInformationTypeID As Integer, ByVal defaultValue As String)
        If ctlMarketingField Is Nothing Then Exit Sub
        Dim tblCourseInfo As New CourseInformationTypeDataTable
        Dim vCourseInfo As CCCDataViewDataSet = CCCDataViewDataSet.CreateDataView(tblCourseInfo)
        vCourseInfo.Columns.AddDBColumns(False, False)
        vCourseInfo.Filters.SetColumnFilter(tblCourseInfo.CourseInformationTypeIDColumn, courseInformationTypeID)
        tblCourseInfo.TableAdapter.Load(tblCourseInfo, vCourseInfo)
        If tblCourseInfo.Rows.Count = 1 Then
            Dim row As CourseInformationTypeRow = tblCourseInfo(0)
            If row.IsListOfVals.HasValue AndAlso row.IsListOfVals.Value Then
                Dim cboMarketingField As WebControls.DropDownList = CType(ctlMarketingField, WebControls.DropDownList)
                Dim tblItems As New CourseInformationTypeValueDataTable
                Dim vItems As CCCDataViewDataSet = CCCDataViewDataSet.CreateDataView(tblItems)
                vItems.Columns.EnsureColumnsAreSelected(True, False, tblItems.DescriptionColumn)
                vItems.Filters.SetColumnFilter(tblItems.CourseInformationTypeIDColumn, courseInformationTypeID)

                If row.ParentCourseInformationTypeID > 0 Then
                    Dim str As String = getMarketingFieldValue(row.ParentCourseInformationTypeID)
                    If str IsNot Nothing Then
                        vItems.Filters.SetColumnFilter(tblItems.ParentValueColumn, str)
                    End If
                End If
                vItems.SortOrders.SetColumnsSortOrder(CCCCommon.SortDirection.Ascending, tblItems.DescriptionColumn)
                Dim l As New CCCLookup(tblItems, vItems, tblItems.ValueColumn.ColumnName)
                CCCListControlLibrary.PopulateList(l, cboMarketingField, True, ",")

                cboMarketingField.Items(0).Text = "All"
                Try
                    cboMarketingField.SelectedValue = defaultValue
                Catch ex As Exception
                    cboMarketingField.SelectedValue = Nothing
                End Try
            End If
        End If
    End Sub
    ''' <summary>
    ''' Loading list of images based on set value in MarketingField1CourseInformationTypeID variable. This will also act as an filter to filter data based on course info type.
    ''' </summary>
    Public Sub LoadImageTable(listviewGraphic As ListView, isSearched As Boolean)
        If Me.UseMarketingField1ForImage Then
            Dim tblCourseInfoTypeValue As New CourseInformationTypeValueDataTable
            Dim vCourseInfoTypeValue As CCCDataViewDataSet = CCCDataViewDataSet.CreateDataViewDefault(tblCourseInfoTypeValue)
            vCourseInfoTypeValue.Columns.EnsureColumnsAreSelected(True, False, tblCourseInfoTypeValue.ValueColumn)
            If Me.MarketingField1 IsNot Nothing AndAlso Len(Me.MarketingField1) > 0 AndAlso isSearched Then
                vCourseInfoTypeValue.Filters.SetColumnFilter(tblCourseInfoTypeValue.ParentValueColumn, Me.MarketingField1)
                vCourseInfoTypeValue.Filters.SetColumnFilter(tblCourseInfoTypeValue.ParentCourseInformationTypeIDColumn, Me.MarketingField1CourseInformationTypeID)
            Else
                vCourseInfoTypeValue.Filters.SetColumnFilter(tblCourseInfoTypeValue.CourseInformationTypeIDColumn, Me.MarketingField1CourseInformationTypeID)
            End If
            vCourseInfoTypeValue.Filters.SetColumnFilter(tblCourseInfoTypeValue.DescriptionColumn, Nothing, FilterOperator.OperatorNotIsBlank)
            vCourseInfoTypeValue.SortOrders.Clear()
            vCourseInfoTypeValue.SortOrders.SetColumnSortOrder(CCCCommon.SortDirection.Ascending, tblCourseInfoTypeValue.ValueColumn)
            tblCourseInfoTypeValue.TableAdapter.Load(tblCourseInfoTypeValue, vCourseInfoTypeValue)
            listviewGraphic.DataSource = tblCourseInfoTypeValue
            listviewGraphic.DataBind()
            listviewGraphic.Visible = True
        Else
            listviewGraphic.Visible = False
        End If

        'Custom search param - hide image table after search
        'If Not Page.Request("search") Is Nothing Then
        '    ImageTable.Visible = False
        'End If

    End Sub

    ''' <summary>
    ''' Gets value of control when control could be a dropdown or textbox
    ''' </summary>
    ''' <param name="ctlMarketingField">Marketing field control</param>
    ''' <param name="DefaultValue">Default marketing field value if control is not available</param>
    ''' <returns>Marketing field value</returns>
    Private Function getValue(ByVal ctlMarketingField As WebControl, ByVal defaultValue As String) As String
        Dim txtMarketingField As TextBox = TryCast(ctlMarketingField, TextBox)
        Dim cboMarketingField As DropDownList = TryCast(ctlMarketingField, DropDownList)
        Dim strValue As String = Nothing
        If txtMarketingField IsNot Nothing Then
            strValue = txtMarketingField.Text
        ElseIf cboMarketingField IsNot Nothing Then
            strValue = cboMarketingField.SelectedValue
        Else
            strValue = defaultValue
        End If
        Return strValue
    End Function

    ''' <summary>
    ''' Sets value of control when control could be a dropdown or textbox
    ''' </summary>
    ''' <param name="ctlMarketingField">Marketing field control</param>
    ''' <param name="valueToSet">Marketing field value</param>
    Private Sub setValue(ByVal ctlMarketingField As WebControl, ByVal valueToSet As String)
        Dim txtMarketingField As TextBox = TryCast(ctlMarketingField, TextBox)
        Dim cboMarketingField As DropDownList = TryCast(ctlMarketingField, DropDownList)
        If txtMarketingField IsNot Nothing Then
            txtMarketingField.Text = valueToSet
        ElseIf cboMarketingField IsNot Nothing Then
            cboMarketingField.SelectedValue = valueToSet
        End If
    End Sub
    ''' <summary>
    ''' Generic logic to be used to generate controls for each marketing field.
    ''' </summary>
    ''' <param name="isDesignMode">True if a control is being used on a designer</param>
    ''' <param name="courseInformationTypeID"></param>
    ''' <returns>WebControl</returns>
    Private Function createMarketingField(isDesignMode As Boolean, courseInformationTypeID As Integer) As WebControl
        Dim ctlMarketingField As WebControl = Nothing
        If Not isDesignMode Then
            Dim tblCourseInfoType As New CourseInformationTypeDataTable
            Dim vCourseInfoType As CCCDataViewDataSet = CCCDataViewDataSet.CreateDataView(tblCourseInfoType)
            vCourseInfoType.Columns.AddDBColumns(False, False)
            vCourseInfoType.Filters.SetColumnFilter(tblCourseInfoType.CourseInformationTypeIDColumn, courseInformationTypeID)
            tblCourseInfoType.TableAdapter.Load(tblCourseInfoType, vCourseInfoType)
            If tblCourseInfoType.Rows.Count = 1 Then
                Dim row As CourseInformationTypeRow = tblCourseInfoType(0)
                If row.IsListOfVals.HasValue AndAlso row.IsListOfVals.Value Then
                    Dim cboMarketingField As New WebControls.DropDownList
                    Dim tblItems As New CourseInformationTypeValueDataTable
                    Dim vItems As CCCDataViewDataSet = CCCDataViewDataSet.CreateDataView(tblItems)
                    vItems.Columns.EnsureColumnsAreSelected(True, False, tblItems.DescriptionColumn)
                    vItems.Filters.SetColumnFilter(tblItems.CourseInformationTypeIDColumn, courseInformationTypeID)
                    If row.ParentCourseInformationTypeID > 0 Then
                        Dim str As String = getMarketingFieldValue(row.ParentCourseInformationTypeID)
                        If str IsNot Nothing Then
                            vItems.Filters.SetColumnFilter(tblItems.ParentValueColumn, str)
                        End If
                    End If
                    vItems.SortOrders.SetColumnsSortOrder(CCCCommon.SortDirection.Ascending, tblItems.DescriptionColumn)
                    Dim l As New CCCLookup(tblItems, vItems, tblItems.ValueColumn.ColumnName)
                    CCCListControlLibrary.PopulateList(l, cboMarketingField, True, ",")
                    cboMarketingField.Items(0).Text = "All"

                    If UseMarketingField2ForSubImage AndAlso getMarketingField(courseInformationTypeID) = "MarketingField1" Then
                        cboMarketingField.AutoPostBack = True
                    End If

                    ctlMarketingField = cboMarketingField
                Else
                    Dim txt As New WebControls.TextBox
                    ctlMarketingField = txt
                End If
            End If
        Else
            Dim txt As New WebControls.TextBox
            ctlMarketingField = txt
        End If
        Return ctlMarketingField
    End Function

    ''' <summary>
    ''' To retrieve marketing field value from session by course info type.
    ''' </summary>
    ''' <param name="courseInformationTypeID">Course Info Type ID for which marketing field value to be retrieved from session</param>
    ''' <returns>Marketing Field Value</returns>
    Private Function getMarketingFieldValue(ByVal courseInformationTypeID As Integer) As String
        Dim strMarketingFieldValue As String = Nothing
        Select Case courseInformationTypeID
            Case MarketingField1CourseInformationTypeID
                strMarketingFieldValue = Me.MarketingField1
            Case MarketingField2CourseInformationTypeID
                strMarketingFieldValue = Me.MarketingField2
            Case MarketingField3CourseInformationTypeID
                strMarketingFieldValue = Me.MarketingField3
        End Select
        If strMarketingFieldValue = "All" Then strMarketingFieldValue = Nothing
        Return strMarketingFieldValue
    End Function
    ''' <summary>
    ''' To get Marketing field name based on course information type.
    ''' </summary>
    ''' <param name="courseInformationTypeID">Course Info Type ID for which marketing field name to be retrieved</param>
    ''' <returns>Marketing Field</returns>
    Private Function getMarketingField(ByVal courseInformationTypeID As Integer) As String
        Dim strMarketingField As String = Nothing
        Select Case courseInformationTypeID
            Case MarketingField1CourseInformationTypeID
                strMarketingField = "MarketingField1"
            Case MarketingField2CourseInformationTypeID
                strMarketingField = "MarketingField2"
            Case MarketingField3CourseInformationTypeID
                strMarketingField = "MarketingField3"
        End Select
        Return strMarketingField
    End Function
End Class
