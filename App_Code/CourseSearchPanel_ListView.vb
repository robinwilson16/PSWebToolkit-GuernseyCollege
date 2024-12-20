Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Text
Imports System.Web
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports CompassCC.CCCSystem.CCCCommon
Imports CompassCC.CCCSystem
Imports System.Web.UI.HtmlControls
Imports CompassCC.ProSolution.PSWebEnrolmentKit
Imports CompassCC.ProSolution.Business

Namespace CustomControl
    <ToolboxData("<{0}:CourseSearchPanel_ListView runat=server></{0}:CourseSearchPanel_ListView>")>
    Public Class CourseSearchPanel_ListView
        Inherits CompositeControl

        Event SearchClicked(ByVal sender As Object, ByVal e As EventArgs)

        Private m_txtKeyWords As New TextBox 'Searches through course info title and course info texts
        Private m_txtCode As New TextBox
        Private m_cboSites As New DropDownList
        Private m_cboCollegeLevel As New DropDownList
        Private m_cboItemsPerPage As New DropDownList
        Private m_cboAttendanceMode As New DropDownList
        Private m_ctlMarketingField1 As WebControl
        Private m_ctlMarketingField2 As WebControl
        Private m_ctlMarketingField3 As WebControl
        Private WithEvents m_btnSearch As New Button

        Property SearchOrientation As String


        Dim m_CoursesInBasket As System.Collections.Generic.List(Of Integer)

        Protected Overrides Sub OnLoad(ByVal e As System.EventArgs)
            MyBase.OnLoad(e)

            'Need to build the image table each time or we can hook up to the image clicks
            'Try
            LoadImageTable()
            'Catch ex As CCCBusinessException
            '    Throw New Exception("Unable to connect to database with the supplied credentials")
            'End Try

            If Not Page.IsPostBack Then
                'Initialise grid with previous search criteria
                If Page.Request("restoresearch") IsNot Nothing Then
                    PopulateDropDownLists()
                    LoadCriteriaFromSession()
                    LoadGridData()
                    'ImageTable.Visible = False

                Else
                    'Do nothing


                End If
            Else
                StoreCriteriaInSession()
                'If we have already done the search, hide the images
                'If CBool(ViewState("Searched")) Then ImageTable.Visible = False
                'Load websiteavailability reference data

                Me.MarketingField1 = CStr(Page.Session("MarketingField1"))
                Me.MarketingField2 = CStr(Page.Session("MarketingField2"))
                PopulateMarketingField(m_ctlMarketingField2, m_intMarketingField2CourseInformationTypeID, m_intMarketingField2ParentCourseInformationTypeID, MarketingField2)
            End If



        End Sub

        Public Sub LoadCriteriaFromSession()

            Me.Code = CStr(Page.Session("Code"))
            Me.SearchText = CStr(Page.Session("SearchText"))
            Me.SiteID = CStr(Page.Session("SiteID"))
            Me.CollegeLevelID = CStr(Page.Session("CollegeLevelID"))
            Me.AttendanceMode = CStr(Page.Session("AttendanceMode"))
            Me.MarketingField1 = CStr(Page.Session("MarketingField1"))
            Me.MarketingField2 = CStr(Page.Session("MarketingField2"))
            Me.MarketingField3 = CStr(Page.Session("MarketingField3"))

        End Sub

        Public Sub StoreCriteriaInSession()

            Page.Session("Code") = Me.Code
            Page.Session("SearchText") = Me.SearchText
            Page.Session("SiteID") = Me.SiteID
            Page.Session("CollegeLevelID") = Me.CollegeLevelID
            Page.Session("AttendanceMode") = Me.AttendanceMode
            Page.Session("MarketingField1") = Me.MarketingField1
            Page.Session("MarketingField2") = Me.MarketingField2
            Page.Session("MarketingField3") = Me.MarketingField3

        End Sub


        Public Sub PopulateDropDownLists()
            If Not CCCWebUILIbrary.IsDesignTime Then
                'Populate drop down lists etc.
                If m_blnShowSite Then
                    m_cboSites.Items.Add(New WebControls.ListItem("All", ""))
                    CCCListControlLibrary.PopulateListWithDescriptionColumns(New SiteDataTable, m_cboSites, True, m_strExcludedSiteIDs)
                End If
                If m_blnShowCollegeLevel Then
                    CCCListControlLibrary.PopulateListWithDescriptionColumns(New CollegeLevelDataTable, m_cboCollegeLevel, True)
                End If
                If m_blnShowModeOfAttendance Then
                    If m_cboAttendanceMode.Items.Count = 0 Then
                        m_cboAttendanceMode.Items.Add(New WebControls.ListItem("All", "All"))
                        m_cboAttendanceMode.Items.Add(New WebControls.ListItem("Part time", "PT"))
                        m_cboAttendanceMode.Items.Add(New WebControls.ListItem("Full time", "FT"))
                    End If
                End If
            End If
        End Sub

        Private Sub m_btnSearch_Click(ByVal sender As Object, ByVal e As EventArgs) Handles m_btnSearch.Click
            DoSearch()
        End Sub

        Public Sub DoSearch()
            PopulateDropDownLists()

            'Querystring parameters for HRC
            If Len(Me.SearchText) = 0 AndAlso Len(CStr(Page.Session("SearchText"))) > 0 Then
                Me.SearchText = CStr(Page.Session("SearchText"))
            End If

            If Len(Me.Code) = 0 AndAlso Len(CStr(Page.Session("Code"))) > 0 Then
                Me.Code = CStr(Page.Session("Code"))
            End If

            If Len(Me.MarketingField1) = 0 AndAlso Len(CStr(Page.Session("MarketingField1"))) > 0 Then
                Me.MarketingField1 = CStr(Page.Session("MarketingField1"))
            End If

            If Me.AttendanceMode = "All" AndAlso Len(CStr(Page.Session("AttendanceMode"))) > 0 Then
                Me.AttendanceMode = CStr(Page.Session("AttendanceMode"))
            End If
 If Len(Me.SiteID) = 0 AndAlso Len(CStr(Page.Session("SiteID"))) > 0 Then
                Me.SiteID = CStr(Page.Session("SiteID"))
            End If
            'Record text user has searched on
            Dim stb As New Text.StringBuilder
            For Each fld As SearchField In Me.GetSearchFields
                If stb.Length > 0 Then stb.Append(",")
                stb.Append(fld.ColumnName & ":" & CCCDataTypeConverter.Convert(Of String)(fld.Value))
            Next

            'Append the text to WebSiteSearchTexts in database (So college can review what students search on)
            If stb.Length > 0 Then
                Dim strText As String = WorkingData.EnrolmentRequestRow.WebSiteSearchTexts
                If Len(strText) > 0 Then strText &= ControlChars.CrLf
                strText &= stb.ToString
                If Len(strText) > WorkingData.EnrolmentRequest.WebSiteSearchTextsColumn.MaxLength Then
                    strText = strText.Substring(0, WorkingData.EnrolmentRequest.WebSiteSearchTextsColumn.MaxLength - 1)
                End If
                WorkingData.EnrolmentRequestRow.WebSiteSearchTexts = strText
            End If

            StartRowIndex = 0
            LoadGridData()

            'Hide the image table
            'ImageTable.Visible = False

            ViewState("Searched") = True
            LoadImageTable()

            RaiseEvent SearchClicked(Me, New EventArgs)
        End Sub

#Region " Search Field Properites "
        <Browsable(False)> _
        Public Property Code() As String
            Get
                Return m_txtCode.Text
            End Get
            Set(ByVal value As String)
                m_txtCode.Text = value
                SetValue(m_txtCode, value)
            End Set
        End Property


        <Browsable(False)> _
        Public Property SearchText() As String
            Get
                Return m_txtKeyWords.Text
            End Get
            Set(ByVal value As String)
                m_txtKeyWords.Text = value
                SetValue(m_txtKeyWords, value)
            End Set
        End Property

        <Browsable(False)> _
        Public Property SiteID() As String
            Get
                Return m_cboSites.SelectedValue
            End Get
            Set(ByVal value As String)
                m_cboSites.SelectedValue = value
            End Set
        End Property

        <Browsable(False)> _
        Public Property CollegeLevelID() As String
            Get
                Return m_cboCollegeLevel.SelectedValue
            End Get
            Set(ByVal value As String)
                m_cboCollegeLevel.SelectedValue = value
            End Set
        End Property

        <Browsable(False)> _
        Public Property AttendanceMode() As String
            Get
                Return m_cboAttendanceMode.SelectedValue
            End Get
            Set(ByVal value As String)
                m_cboAttendanceMode.SelectedValue = value
                SetValue(m_cboAttendanceMode, value)
            End Set
        End Property

        Private m_strMarketingField1 As String

        <Browsable(False)> _
        Public Property MarketingField1() As String
            Get
                Return GetValue(m_ctlMarketingField1, m_strMarketingField1)
            End Get
            Set(ByVal value As String)
                m_strMarketingField1 = value 'This gets set in case control hasn't been created yet
                SetValue(m_ctlMarketingField1, value)
            End Set
        End Property

        Private m_strMarketingField2 As String

        <Browsable(False)> _
        Public Property MarketingField2() As String
            Get
                Return GetValue(m_ctlMarketingField2, m_strMarketingField2)
            End Get
            Set(ByVal value As String)
                m_strMarketingField2 = value 'This gets set in case control hasn't been created yet
                SetValue(m_ctlMarketingField2, value)
            End Set
        End Property

        Private m_strMarketingField3 As String

        <Browsable(False)> _
        Public Property MarketingField3() As String
            Get
                Return GetValue(m_ctlMarketingField3, m_strMarketingField3)
            End Get
            Set(ByVal value As String)
                m_strMarketingField3 = value 'This gets set in case control hasn't been created yet
                SetValue(m_ctlMarketingField3, value)
            End Set
        End Property

        'Gets value of control when control could be a dropdown or textbox
        Private Shared Function GetValue(ByVal ctl As WebControl, ByVal DefaultValue As String) As String
            Dim txt As TextBox = TryCast(ctl, TextBox)
            Dim cbo As DropDownList = TryCast(ctl, DropDownList)
            Dim strValue As String = Nothing
            If txt IsNot Nothing Then
                strValue = txt.Text
            ElseIf cbo IsNot Nothing Then
                strValue = cbo.SelectedValue
            Else
                strValue = DefaultValue
            End If
            Return strValue
        End Function

        'Sets value of control when control could be a dropdown or textbox
        Public Sub SetValue(ByVal ctl As WebControl, ByVal Value As String)
            Dim txt As TextBox = TryCast(ctl, TextBox)
            Dim cbo As DropDownList = TryCast(ctl, DropDownList)
            If txt IsNot Nothing Then
                txt.Text = Value
            ElseIf cbo IsNot Nothing Then
                cbo.SelectedValue = Value
            End If
        End Sub

#End Region

#Region " Control Properties "

        Private m_blnShowModeOfAttendance As Boolean
        Public Property ShowModeOfAttendance() As Boolean
            Get
                Return m_blnShowModeOfAttendance
            End Get
            Set(ByVal value As Boolean)
                m_blnShowModeOfAttendance = value
            End Set
        End Property

        Private m_strExcludedModeOfAttendanceIDs As String
        Public Property ExcludedModeOfAttendanceIDs() As String
            Get
                Return m_strExcludedModeOfAttendanceIDs
            End Get
            Set(ByVal value As String)
                m_strExcludedModeOfAttendanceIDs = value
            End Set
        End Property

        Private m_blnShowCollegeLevel As Boolean
        Public Property ShowCollegeLevel() As Boolean
            Get
                Return m_blnShowCollegeLevel
            End Get
            Set(ByVal value As Boolean)
                m_blnShowCollegeLevel = value
                Me.ChildControlsCreated = False
            End Set
        End Property

        Private m_strCollegeLevelCaption As String
        Public Property CollegeLevelCaption() As String
            Get
                Return m_strCollegeLevelCaption
            End Get
            Set(ByVal value As String)
                m_strCollegeLevelCaption = value
            End Set
        End Property

        Private m_blnShowCode As Boolean
        Public Property ShowCode() As Boolean
            Get
                Return m_blnShowCode
            End Get
            Set(ByVal value As Boolean)
                m_blnShowCode = value
            End Set
        End Property

        Private m_blnShowSite As Boolean
        Public Property ShowSite() As Boolean
            Get
                Return m_blnShowSite
            End Get
            Set(ByVal value As Boolean)
                m_blnShowSite = value
                Me.ChildControlsCreated = False
            End Set
        End Property

        Private m_strExcludedSiteIDs As String
        Public Property ExcludedSiteIDs() As String
            Get
                Return m_strExcludedSiteIDs
            End Get
            Set(ByVal value As String)
                m_strExcludedSiteIDs = value
            End Set
        End Property

        Private m_blnShowMarketingField1 As Boolean
        Public Property ShowMarketingField1() As Boolean
            Get
                Return m_blnShowMarketingField1
            End Get
            Set(ByVal value As Boolean)
                m_blnShowMarketingField1 = value
                Me.ChildControlsCreated = False
            End Set
        End Property

        Private m_intMarketingField1CourseInformationTypeID As Integer
        Public Property MarketingField1CourseInformationTypeID() As Integer
            Get
                Return m_intMarketingField1CourseInformationTypeID
            End Get
            Set(ByVal value As Integer)
                m_intMarketingField1CourseInformationTypeID = value
            End Set
        End Property

        Private m_intMarketingField1ParentCourseInformationTypeID As Integer
        Public Property MarketingField1ParentCourseInformationTypeID() As Integer
            Get
                Return m_intMarketingField1ParentCourseInformationTypeID
            End Get
            Set(ByVal value As Integer)
                m_intMarketingField1ParentCourseInformationTypeID = value
            End Set
        End Property

        Private m_strMarketingField1Caption As String
        Public Property MarketingField1Caption() As String
            Get
                Return m_strMarketingField1Caption
            End Get
            Set(ByVal value As String)
                m_strMarketingField1Caption = value
                Me.ChildControlsCreated = False
            End Set
        End Property

        Private m_blnUseMarketingField1AsFilter As Boolean
        Public Property UseMarketingField1AsFilter() As Boolean
            Get
                Return m_blnUseMarketingField1AsFilter
            End Get
            Set(ByVal value As Boolean)
                m_blnUseMarketingField1AsFilter = value
            End Set
        End Property

        Private m_blnUseMarketingField1ForImage As Boolean
        Public Property UseMarketingField1ForImage() As Boolean
            Get
                Return m_blnUseMarketingField1ForImage
            End Get
            Set(ByVal value As Boolean)
                m_blnUseMarketingField1ForImage = value
            End Set
        End Property

        Private m_blnUseMarketingField2ForSubImage As Boolean
        Public Property UseMarketingField2ForSubImage() As Boolean
            Get
                Return m_blnUseMarketingField2ForSubImage
            End Get
            Set(ByVal value As Boolean)
                m_blnUseMarketingField2ForSubImage = value
            End Set
        End Property

        Private m_blnShowMarketingField2 As Boolean
        Public Property ShowMarketingField2() As Boolean
            Get
                Return m_blnShowMarketingField2
            End Get
            Set(ByVal value As Boolean)
                m_blnShowMarketingField2 = value
                Me.ChildControlsCreated = False
            End Set
        End Property

        Private m_intMarketingField2CourseInformationTypeID As Integer
        Public Property MarketingField2CourseInformationTypeID() As Integer
            Get
                Return m_intMarketingField2CourseInformationTypeID
            End Get
            Set(ByVal value As Integer)
                m_intMarketingField2CourseInformationTypeID = value
            End Set
        End Property

        Private m_intMarketingField2ParentCourseInformationTypeID As Integer
        Public Property MarketingField2ParentCourseInformationTypeID() As Integer
            Get
                Return m_intMarketingField2ParentCourseInformationTypeID
            End Get
            Set(ByVal value As Integer)
                m_intMarketingField2ParentCourseInformationTypeID = value
            End Set
        End Property

        Private m_strMarketingField2Caption As String
        Public Property MarketingField2Caption() As String
            Get
                Return m_strMarketingField2Caption
            End Get
            Set(ByVal value As String)
                m_strMarketingField2Caption = value
                Me.ChildControlsCreated = False
            End Set
        End Property

        Private m_blnUseMarketingField2AsFilter As Boolean
        Public Property UseMarketingField2AsFilter() As Boolean
            Get
                Return m_blnUseMarketingField2AsFilter
            End Get
            Set(ByVal value As Boolean)
                m_blnUseMarketingField2AsFilter = value
            End Set
        End Property


        Private m_blnShowMarketingField3 As Boolean
        Public Property ShowMarketingField3() As Boolean
            Get
                Return m_blnShowMarketingField3
            End Get
            Set(ByVal value As Boolean)
                m_blnShowMarketingField3 = value
                Me.ChildControlsCreated = False
            End Set
        End Property

        Private m_intMarketingField3CourseInformationTypeID As Integer
        Public Property MarketingField3CourseInformationTypeID() As Integer
            Get
                Return m_intMarketingField3CourseInformationTypeID
            End Get
            Set(ByVal value As Integer)
                m_intMarketingField3CourseInformationTypeID = value
            End Set
        End Property


        Private m_strMarketingField3Caption As String
        Public Property MarketingField3Caption() As String
            Get
                Return m_strMarketingField3Caption
            End Get
            Set(ByVal value As String)
                m_strMarketingField3Caption = value
                Me.ChildControlsCreated = False
            End Set
        End Property

        Private m_blnUseMarketingField3AsFilter As Boolean
        Public Property UseMarketingField3AsFilter() As Boolean
            Get
                Return m_blnUseMarketingField3AsFilter
            End Get
            Set(ByVal value As Boolean)
                m_blnUseMarketingField3AsFilter = value
            End Set
        End Property


        Private m_blnShowItemsPerPage As Boolean
        Public Property ShowItemsPerPage() As Boolean
            Get
                Return m_blnShowItemsPerPage
            End Get
            Set(ByVal value As Boolean)
                m_blnShowItemsPerPage = value
            End Set
        End Property

        Private m_intItemsPerPage As Integer = 10
        <DefaultValue(10)>
        Public Property ItemsPerPage() As Integer
            Get
                Return m_intItemsPerPage
            End Get
            Set(ByVal value As Integer)
                m_intItemsPerPage = value
            End Set
        End Property

        Private m_blnShowSearchButton As Boolean = True
        <DefaultValue(True)>
        Public Property ShowSearchButton() As Boolean
            Get
                Return m_blnShowSearchButton
            End Get
            Set(ByVal value As Boolean)
                m_blnShowSearchButton = value
            End Set
        End Property

        Private m_intSearchControlWidth As Integer = 150
        <DefaultValue(150)>
        Public Property SearchControlWidth() As Integer
            Get
                Return m_intSearchControlWidth
            End Get
            Set(ByVal value As Integer)
                m_intSearchControlWidth = value
            End Set
        End Property

#End Region

#Region " Build Control HTML "

        Public Overrides Sub RenderBeginTag(writer As HtmlTextWriter)
            writer.AddAttribute(HtmlTextWriterAttribute.Id, "CourseSearchPanel_ListView")
            writer.RenderBeginTag(HtmlTextWriterTag.Div)
        End Sub
        Protected Overrides Sub CreateChildControls()

            Dim pnl As New Panel
            pnl.ID = "Panel1"
            Me.Controls.Add(pnl)
            With pnl.Controls

                If SearchOrientation = "Horizontal" Then
                    .Add(New LiteralControl("<div class=""row text-center"">"))
                    .Add(New LiteralControl("<div class=""form-group col-xs-4  text-left"">"))

                    .Add(New LiteralControl("<label for=""txtKeywords"" class=""control-label"">Keywords</label>"))



                    m_txtKeyWords.CssClass = "form-control"
                    m_txtKeyWords.ID = "txtKeywords"
                    .Add(m_txtKeyWords)
                    .Add(New LiteralControl("</div>"))

                    If m_blnShowCode Then
                        .Add(New LiteralControl("<div class=""form-group col-xs-4  text-left"">"))
                        .Add(New LiteralControl("<label for=""txtCode"" class=""control-label"">Code</label>"))


                        m_txtCode.CssClass = "form-control"
                        m_txtCode.ID = "txtCode"
                        .Add(m_txtCode)
                        .Add(New LiteralControl("</div>"))
                    End If

                    If m_blnShowSite Then
                        .Add(New LiteralControl("<div class=""form-group col-xs-3 text-left"">"))
                        .Add(New LiteralControl("<label for=""cboSite"" class=""control-label"">Site</label>"))

                        m_cboSites.CssClass = "form-control"
                        m_cboSites.ID = "cboSite"
                        .Add(m_cboSites)
                        .Add(New LiteralControl("</div>"))
                    End If

                    If m_blnShowModeOfAttendance Then
                        .Add(New LiteralControl("<div class=""form-group col-xs-3 text-left"">"))
                        .Add(New LiteralControl("<label for=""cboMOA"" class=""control-label"">Mode of Attendance</label>"))

                        m_cboAttendanceMode.CssClass = "form-control"
                        m_cboAttendanceMode.ID = "cboMOA"
                        .Add(m_cboAttendanceMode)
                        .Add(New LiteralControl("</div>"))
                    End If

                    If m_blnShowCollegeLevel Then
                        .Add(New LiteralControl("<div class=""form-group col-xs-3 text-left"">"))
                        .Add(New LiteralControl("<label for=""cboSID"" class=""control-label"">College Level</label>"))

                        m_cboCollegeLevel.CssClass = "form-control"
                        m_cboCollegeLevel.ID = "cboSID"
                        .Add(m_cboCollegeLevel)
                        .Add(New LiteralControl("</div>"))
                    End If

                    If m_blnShowMarketingField1 AndAlso m_intMarketingField1CourseInformationTypeID <> 0 Then
                        m_ctlMarketingField1 = CreateMarketingField(m_intMarketingField1CourseInformationTypeID, m_strMarketingField1, m_intMarketingField1ParentCourseInformationTypeID)
                        If m_ctlMarketingField1 IsNot Nothing Then
                            .Add(New LiteralControl("<div class=""form-group col-xs-4  text-left"">"))
                            .Add(New LiteralControl("<label for=""txtMF1"" class=""control-label"">" & m_strMarketingField1Caption & "</label>"))


                            m_ctlMarketingField1.CssClass = "form-control"
                            m_ctlMarketingField1.ID = "txtMF1"
                            .Add(m_ctlMarketingField1)
                            .Add(New LiteralControl("</div>"))
                        End If
                    End If

                    If m_blnShowMarketingField2 AndAlso m_intMarketingField2CourseInformationTypeID <> 0 Then
                        m_ctlMarketingField2 = CreateMarketingField(m_intMarketingField2CourseInformationTypeID, m_strMarketingField2, m_intMarketingField2ParentCourseInformationTypeID)
                        If m_ctlMarketingField2 IsNot Nothing Then
                            .Add(New LiteralControl("<div class=""form-group col-xs-3 text-left"">"))
                            .Add(New LiteralControl("<label for=""txtMF2"" class=""control-label"">" & m_strMarketingField2Caption & "</label>"))


                            m_ctlMarketingField2.CssClass = "form-control"
                            m_ctlMarketingField2.ID = "txtMF2"
                            .Add(m_ctlMarketingField2)
                            .Add(New LiteralControl("</div>"))
                        End If
                    End If

                    If m_blnShowMarketingField3 AndAlso m_intMarketingField3CourseInformationTypeID <> 0 Then
                        m_ctlMarketingField3 = CreateMarketingField(m_intMarketingField3CourseInformationTypeID, m_strMarketingField3, 0)
                        If m_ctlMarketingField3 IsNot Nothing Then
                            .Add(New LiteralControl("<div class=""form-group col-xs-3 text-left"">"))
                            .Add(New LiteralControl("<label for=""txtMF3"" class=""control-label"">" & m_strMarketingField3Caption & "</label>"))


                            m_ctlMarketingField3.CssClass = "form-control"
                            m_ctlMarketingField3.ID = "txtMF3"
                            .Add(m_ctlMarketingField3)
                            .Add(New LiteralControl("</div>"))
                        End If
                    End If

                    If m_blnShowSearchButton Then
                        .Add(New LiteralControl("<div class=""form-group col-xs-1"">"))
                        .Add(New LiteralControl("<label for=""txtMF3"" class=""control-label""> </label>"))
                        m_btnSearch.Text = "Search"
                        m_btnSearch.CssClass = "btn btn-info"
                        m_btnSearch.Style.Add("margin-top", "3px")
                        m_btnSearch.ID = "SearchButton"
                        .Add(m_btnSearch)
                        pnl.DefaultButton = m_btnSearch.ID
                        .Add(New LiteralControl("</div>"))
                    End If

                    .Add(New LiteralControl("</div>"))

                Else


                    .Add(New LiteralControl("<div ID=""searchcriteriatable"">")) 'Table Start

                    .Add(New LiteralControl("<div ID=""SearchCriteriaKeywordsRow"">")) 'Row Start
                    .Add(New LiteralControl("<div ID=""SearchCriteriaKeywordsLabelCell"">")) 'Cell Start
                    .Add(New LiteralControl("<p>Keywords&nbsp;</p>"))
                    .Add(New LiteralControl("</div>")) 'Cell End
                    .Add(New LiteralControl("<div ID=""SearchCriteriaKeywordsControlCell"">")) 'Cell Start.
                    'm_txtKeyWords.Width = New Unit(m_intSearchControlWidth) 'Pixels 
                    m_txtKeyWords.CssClass = "searchBox textfield form-control"
                    .Add(m_txtKeyWords)
                    .Add(New LiteralControl("</div>")) 'Cell End
                    .Add(New LiteralControl("</div>")) 'Row End


                    If m_blnShowCode Then
                        .Add(New LiteralControl("<div ID=""SearchCriteriaCodeRow"">")) 'Row Start
                        .Add(New LiteralControl("<div ID=""SearchCriteriaCodeLabelCell"">")) 'Cell Start
                        .Add(New LiteralControl("<p>Code&nbsp;</p>"))
                        .Add(New LiteralControl("</div>")) 'Cell End
                        .Add(New LiteralControl("<div ID=""SearchCriteriaCodeControlCell"">")) 'Cell Start.
                        'm_txtCode.Width = New Unit(m_intSearchControlWidth) 'Pixels 
                        m_txtCode.CssClass = "searchBox textfield form-control"
                        .Add(m_txtCode)
                        .Add(New LiteralControl("</div>")) 'Cell End
                        .Add(New LiteralControl("</div>")) 'Row End
                    End If

                    If m_blnShowSite Then
                        .Add(New LiteralControl("<div ID=""SearchCriteriaLocationRow"">")) 'Row Start
                        .Add(New LiteralControl("<div ID=""SearchCriteriaLocationLabelCell"">")) 'Cell Start
                        .Add(New LiteralControl("<p>Location&nbsp;</p>"))
                        .Add(New LiteralControl("</div>")) 'Cell End
                        .Add(New LiteralControl("<div ID=""SearchCriteriaLocationControlCell"">")) 'Cell Start.
                        'm_cboSites.Width = New Unit(m_intSearchControlWidth) 'Pixels 
                        m_cboSites.CssClass = "searchBox textfield form-control"
                        .Add(m_cboSites)
                        .Add(New LiteralControl("</div>")) 'Cell End
                        .Add(New LiteralControl("</div>")) 'Row End
                    End If

                    If m_blnShowModeOfAttendance Then
                        .Add(New LiteralControl("<div ID=""SearchCriteriaModeRow"">")) 'Row Start
                        .Add(New LiteralControl("<div ID=""SearchCriteriaModeLabelCell"">")) 'Cell Start
                        .Add(New LiteralControl("<p>Attendance Mode&nbsp;</p>"))
                        .Add(New LiteralControl("</div>")) 'Cell End
                        .Add(New LiteralControl("<div ID=""SearchCriteriaModeControlCell"">")) 'Cell Start.
                        'm_cboAttendanceMode.Width = New Unit(m_intSearchControlWidth) 'Pixels 
                        m_cboAttendanceMode.CssClass = "searchBox textfield form-control"
                        .Add(m_cboAttendanceMode)
                        .Add(New LiteralControl("</div>")) 'Cell End
                        .Add(New LiteralControl("</div>")) 'Row End
                    End If

                    If m_blnShowCollegeLevel Then
                        .Add(New LiteralControl("<div ID=""SearchCriteriaCollegeLevelRow"">")) 'Row Start
                        .Add(New LiteralControl("<div ID=""SearchCriteriaCollegeLevelLabelCell"">")) 'Cell Start
                        .Add(New LiteralControl("<p>" & m_strCollegeLevelCaption & "&nbsp;</p>"))
                        .Add(New LiteralControl("</div>")) 'Cell End
                        .Add(New LiteralControl("<div ID=""SearchCriteriaCollegeLevelControlCell"">")) 'Cell Start.
                        'm_cboCollegeLevel.Width = New Unit(m_intSearchControlWidth) 'Pixels 
                        m_cboCollegeLevel.CssClass = "searchBox textfield form-control"
                        .Add(m_cboCollegeLevel)
                        .Add(New LiteralControl("</div>")) 'Cell End
                        .Add(New LiteralControl("</div>")) 'Row End
                    End If

                    If m_blnShowMarketingField1 AndAlso m_intMarketingField1CourseInformationTypeID <> 0 Then
                        m_ctlMarketingField1 = CreateMarketingField(m_intMarketingField1CourseInformationTypeID, m_strMarketingField1, m_intMarketingField1ParentCourseInformationTypeID)
                        If m_ctlMarketingField1 IsNot Nothing Then
                            .Add(New LiteralControl("<div ID=""SearchCriteriaMarketingField1Row"">")) 'Row Start
                            .Add(New LiteralControl("<div ID=""SearchCriteriaMarketingField1LabelCell"">")) 'Cell Start
                            .Add(New LiteralControl("<p>" & m_strMarketingField1Caption & "&nbsp;</p>"))
                            .Add(New LiteralControl("</div>")) 'Cell End
                            .Add(New LiteralControl("<div ID=""SearchCriteriaMarketingField1ControlCell"">")) 'Cell Start.
                            'm_ctlMarketingField1.Width = New Unit(m_intSearchControlWidth) 'Pixels 
                            m_ctlMarketingField1.CssClass = "searchBox textfield form-control"
                            .Add(m_ctlMarketingField1)
                            .Add(New LiteralControl("</div>")) 'Cell End
                            .Add(New LiteralControl("</div>")) 'Row End
                        End If
                    End If

                    If m_blnShowMarketingField2 AndAlso m_intMarketingField2CourseInformationTypeID <> 0 Then
                        m_ctlMarketingField2 = CreateMarketingField(m_intMarketingField2CourseInformationTypeID, m_strMarketingField2, m_intMarketingField2ParentCourseInformationTypeID)
                        If m_ctlMarketingField2 IsNot Nothing Then
                            .Add(New LiteralControl("<div ID=""SearchCriteriaMarketingField2Row"">")) 'Row Start
                            .Add(New LiteralControl("<div ID=""SearchCriteriaMarketingField2LabelCell"" >")) 'Cell Start
                            .Add(New LiteralControl("<p>" & m_strMarketingField2Caption & "&nbsp;</p>"))
                            .Add(New LiteralControl("</div>")) 'Cell End
                            .Add(New LiteralControl("<div ID=""SearchCriteriaMarketingField2ControlCell"">")) 'Cell Start.
                            'm_ctlMarketingField2.Width = New Unit(m_intSearchControlWidth) 'Pixels 
                            m_ctlMarketingField2.CssClass = "searchBox textfield form-control"
                            .Add(m_ctlMarketingField2)
                            .Add(New LiteralControl("</div>")) 'Cell End
                            .Add(New LiteralControl("</div>")) 'Row End
                        End If
                    End If

                    If m_blnShowMarketingField3 AndAlso m_intMarketingField3CourseInformationTypeID <> 0 Then
                        m_ctlMarketingField3 = CreateMarketingField(m_intMarketingField3CourseInformationTypeID, m_strMarketingField3, 0)
                        If m_ctlMarketingField3 IsNot Nothing Then
                            .Add(New LiteralControl("<div ID=""SearchCriteriaMarketingField3Row"">")) 'Row Start
                            .Add(New LiteralControl("<div ID=""SearchCriteriaMarketingField3LabelCell"">")) 'Cell Start
                            .Add(New LiteralControl("<p>" & m_strMarketingField3Caption & "&nbsp;</p>"))
                            .Add(New LiteralControl("</div>")) 'Cell End
                            .Add(New LiteralControl("<div ID=""SearchCriteriaMarketingField3ControlCell"">")) 'Cell Start.
                            'm_ctlMarketingField3.Width = New Unit(m_intSearchControlWidth) 'Pixels 
                            m_ctlMarketingField3.CssClass = "searchBox textfield form-control"
                            .Add(m_ctlMarketingField3)
                            .Add(New LiteralControl("</div>")) 'Cell End
                            .Add(New LiteralControl("</div>")) 'Row End
                        End If
                    End If

                    If m_blnShowItemsPerPage Then
                        .Add(New LiteralControl("<div ID=""SearchCriteriaResultsperpageRow"">")) 'Row Start
                        .Add(New LiteralControl("<div ID=""SearchCriteriaResultsperpageLabelCell"">")) 'Cell Start
                        .Add(New LiteralControl("<p>Results per page&nbsp;</p>"))
                        .Add(New LiteralControl("</div>")) 'Cell End
                        .Add(New LiteralControl("<div ID=""SearchCriteriaResultsperpageControlCell"">")) 'Cell Start.
                        m_cboItemsPerPage.Items.Add("10")
                        m_cboItemsPerPage.Items.Add("15")
                        m_cboItemsPerPage.Items.Add("20")
                        m_cboItemsPerPage.Items.Add("25")
                        m_cboItemsPerPage.Items.Add("30")
                        m_cboItemsPerPage.CssClass = "searchBox textfield form-control"
                        .Add(m_cboItemsPerPage)
                        .Add(New LiteralControl("</div>")) 'Cell End
                        .Add(New LiteralControl("</div>")) 'Row End
                    End If
                    .Add(New LiteralControl("</div>")) 'Table End


                    If m_blnShowSearchButton Then
                        .Add(New LiteralControl("<div ID=""SearchCriteriaSearchButtonTable"">")) 'Table Start
                        .Add(New LiteralControl("<div ID=""SearchCriteriaSearchButtonRow"">")) 'Row Start
                        .Add(New LiteralControl("<div ID=""SearchCriteriaSearchButtonCell"">")) 'Cell Start
                        m_btnSearch.Text = "Search"
                        m_btnSearch.CssClass = "btn btn-info"
                        m_btnSearch.ID = "SearchButton"
                        .Add(m_btnSearch)
                        .Add(New LiteralControl("</div>")) 'Cell End
                        .Add(New LiteralControl("</div>")) 'Row End
                        pnl.DefaultButton = m_btnSearch.ID
                        .Add(New LiteralControl("</div>")) 'Table End
                    End If
                End If
            End With

            PopulateDropDownLists()

        End Sub


        Public Function GetMarketingFieldValue(ByVal CourseInformationTypeID As Integer) As String
            Dim strMarketingFieldValue As String = Nothing
            Select Case CourseInformationTypeID
                Case MarketingField1CourseInformationTypeID
                    strMarketingFieldValue = CStr(Page.Session("MarketingField1"))
                Case MarketingField2CourseInformationTypeID
                    strMarketingFieldValue = CStr(Page.Session("MarketingField2"))
                Case MarketingField3CourseInformationTypeID
                    strMarketingFieldValue = CStr(Page.Session("MarketingField3"))
            End Select
            If strMarketingFieldValue = "All" Then strMarketingFieldValue = Nothing
            Return strMarketingFieldValue
        End Function
        Public Function GetMarketingField(ByVal CourseInformationTypeID As Integer) As String
            Dim strMarketingField As String = Nothing
            Select Case CourseInformationTypeID
                Case MarketingField1CourseInformationTypeID
                    strMarketingField = "MarketingField1"
                Case MarketingField2CourseInformationTypeID
                    strMarketingField = "MarketingField2"
                Case MarketingField3CourseInformationTypeID
                    strMarketingField = "MarketingField3"
            End Select
            Return strMarketingField
        End Function
        Public Function CreateMarketingField(ByVal CourseInformationTypeID As Integer, ByVal DefaultValue As String, ByVal ParentCourseInformationTypeValueID As Integer) As WebControl
            Dim ctl As WebControl = Nothing
            If Not Me.DesignMode Then

                Dim tbl As New CourseInformationTypeDataTable

                Dim v As CCCDataViewDataSet = CCCDataViewDataSet.CreateDataView(tbl)
                v.Columns.AddDBColumns(False, False)
                v.Filters.SetColumnFilter(tbl.CourseInformationTypeIDColumn, CourseInformationTypeID)
                tbl.TableAdapter.Load(tbl, v)
                If tbl.Rows.Count = 1 Then
                    Dim row As CourseInformationTypeRow = tbl(0)
                    If row.IsListOfVals.HasValue AndAlso row.IsListOfVals.Value Then
                        Dim cbo As New WebControls.DropDownList
                        Dim tblItems As New CourseInformationTypeValueDataTable
                        Dim vItems As CCCDataViewDataSet = CCCDataViewDataSet.CreateDataView(tblItems)
                        vItems.Columns.EnsureColumnsAreSelected(True, False, tblItems.DescriptionColumn)
                        vItems.Filters.SetColumnFilter(tblItems.CourseInformationTypeIDColumn, CourseInformationTypeID)
                        If ParentCourseInformationTypeValueID > 0 Then
                            Dim str As String = GetMarketingFieldValue(ParentCourseInformationTypeValueID)
                            If str IsNot Nothing Then
                                vItems.Filters.SetColumnFilter(tblItems.ParentValueColumn, GetMarketingFieldValue(ParentCourseInformationTypeValueID))
                            End If
                        End If
                        vItems.SortOrders.SetColumnsSortOrder(CCCCommon.SortDirection.Ascending, tblItems.DescriptionColumn)
                        Dim l As New CCCLookup(tblItems, vItems, tblItems.ValueColumn.ColumnName)
                        CCCListControlLibrary.PopulateList(l, cbo, True, ",")
                        cbo.Items(0).Text = "All"

                        If UseMarketingField2ForSubImage AndAlso GetMarketingField(CourseInformationTypeID) = "MarketingField1" Then
                            cbo.AutoPostBack = True
                        End If

                        cbo.SelectedValue = DefaultValue
                        ctl = cbo
                    Else
                        Dim txt As New WebControls.TextBox
                        txt.Text = DefaultValue
                        ctl = txt
                    End If

                End If

            Else
                Dim txt As New WebControls.TextBox
                txt.Text = DefaultValue
                ctl = txt

            End If
            Return ctl
        End Function

        Public Sub PopulateMarketingField(ByVal ctl As WebControl, ByVal CourseInformationTypeID As Integer, ByVal ParentCourseInformationTypeValueID As Integer, ByVal DefaultValue As String)
            If ctl Is Nothing Then Exit Sub
            Dim tbl As New CourseInformationTypeDataTable
            Dim v As CCCDataViewDataSet = CCCDataViewDataSet.CreateDataView(tbl)
            v.Columns.AddDBColumns(False, False)
            v.Filters.SetColumnFilter(tbl.CourseInformationTypeIDColumn, CourseInformationTypeID)
            tbl.TableAdapter.Load(tbl, v)
            If tbl.Rows.Count = 1 Then
                Dim row As CourseInformationTypeRow = tbl(0)
                If row.IsListOfVals.HasValue AndAlso row.IsListOfVals.Value Then
                    Dim cbo As WebControls.DropDownList = CType(ctl, WebControls.DropDownList)
                    Dim tblItems As New CourseInformationTypeValueDataTable
                    Dim vItems As CCCDataViewDataSet = CCCDataViewDataSet.CreateDataView(tblItems)
                    vItems.Columns.EnsureColumnsAreSelected(True, False, tblItems.DescriptionColumn)
                    vItems.Filters.SetColumnFilter(tblItems.CourseInformationTypeIDColumn, CourseInformationTypeID)

                    If ParentCourseInformationTypeValueID > 0 Then
                        Dim str As String = GetMarketingFieldValue(ParentCourseInformationTypeValueID)
                        If str IsNot Nothing Then
                            vItems.Filters.SetColumnFilter(tblItems.ParentValueColumn, GetMarketingFieldValue(ParentCourseInformationTypeValueID))
                        End If
                    End If
                    vItems.SortOrders.SetColumnsSortOrder(CCCCommon.SortDirection.Ascending, tblItems.DescriptionColumn)
                    Dim l As New CCCLookup(tblItems, vItems, tblItems.ValueColumn.ColumnName)
                    CCCListControlLibrary.PopulateList(l, cbo, True, ",")

                    cbo.Items(0).Text = "All"
                    Try
                        cbo.SelectedValue = DefaultValue
                    Catch ex As Exception
                        cbo.SelectedValue = Nothing
                    End Try

                End If

            End If

        End Sub

        Public Function GetSearchFields() As SearchFieldList
            Dim lst As New SearchFieldList

            If Len(Me.Code) > 0 Then
                lst.Add(New SearchField("Code", Me.Code))
            End If
            If Len(Me.SearchText) > 0 Then
                lst.Add(New SearchField("Keywords", Me.SearchText))
            End If
            If m_blnShowSite AndAlso Len(Me.SiteID) > 0 Then
                lst.Add(New SearchField("SiteID", Me.SiteID))
            End If
            If m_blnShowModeOfAttendance AndAlso Len(m_cboAttendanceMode.SelectedValue) > 0 Then
                lst.Add(New SearchField("AttendanceMode", CCCBlankLibrary.NoBlank(Me.AttendanceMode, Page.Session("AttendanceMode"))))
            End If
            If m_blnShowCollegeLevel AndAlso Len(m_cboCollegeLevel.SelectedValue) > 0 Then
                lst.Add(New SearchField("CollegeLevelID", Me.CollegeLevelID))
            End If
            If m_blnShowMarketingField1 AndAlso Len(MarketingField1) > 0 AndAlso Not MarketingField1 = "All" Then
                lst.Add(New SearchField("MarketingField1_" & m_intMarketingField1CourseInformationTypeID.ToString, MarketingField1, m_blnUseMarketingField1AsFilter))
            End If
            If m_blnShowMarketingField2 AndAlso Len(MarketingField2) > 0 AndAlso Not MarketingField2 = "All" Then
                lst.Add(New SearchField("MarketingField2_" & m_intMarketingField2CourseInformationTypeID.ToString, MarketingField2, m_blnUseMarketingField2AsFilter))
            End If
            If m_blnShowMarketingField3 AndAlso Len(MarketingField3) > 0 AndAlso Not MarketingField3 = "All" Then
                lst.Add(New SearchField("MarketingField3_" & m_intMarketingField3CourseInformationTypeID.ToString, MarketingField3, m_blnUseMarketingField3AsFilter))
            End If
            Return lst
        End Function

#End Region

#Region " Image Table"

        Private WithEvents m_ImageTable As ListView
        Public Property ImageTable() As ListView
            Get
                Return m_ImageTable
            End Get
            Set(ByVal value As ListView)
                m_ImageTable = value
            End Set
        End Property

        Public Sub LoadImageTable()

            If UseMarketingField1ForImage AndAlso Page.Request("restoresearch") = Nothing Then
                Dim tblCourseInfoTypeValue As New CourseInformationTypeValueDataTable
                Dim v As CCCDataViewDataSet = CCCDataViewDataSet.CreateDataViewDefault(tblCourseInfoTypeValue)
                v.Columns.EnsureColumnsAreSelected(True, False, tblCourseInfoTypeValue.ValueColumn)
                If Me.MarketingField1 IsNot Nothing AndAlso Len(Me.MarketingField1) > 0 AndAlso CBool(ViewState("Searched")) = True Then
                    v.Filters.SetColumnFilter(tblCourseInfoTypeValue.ParentValueColumn, Me.MarketingField1)
                    v.Filters.SetColumnFilter(tblCourseInfoTypeValue.ParentCourseInformationTypeIDColumn, m_intMarketingField1CourseInformationTypeID)
                Else
                    v.Filters.SetColumnFilter(tblCourseInfoTypeValue.CourseInformationTypeIDColumn, m_intMarketingField1CourseInformationTypeID)
                End If
                v.Filters.SetColumnFilter(tblCourseInfoTypeValue.DescriptionColumn, Nothing, FilterOperator.OperatorNotIsBlank)
                v.SortOrders.Clear()
                v.SortOrders.SetColumnSortOrder(CCCCommon.SortDirection.Ascending, tblCourseInfoTypeValue.ValueColumn)
                tblCourseInfoTypeValue.TableAdapter.Load(tblCourseInfoTypeValue, v)
                ImageTable.DataSource = tblCourseInfoTypeValue
                ImageTable.DataBind()
                ImageTable.Visible = True
            Else
                ImageTable.Visible = False
            End If

            'Custom search param - hide image table after search
            'If Not Page.Request("search") Is Nothing Then
            '    ImageTable.Visible = False
            'End If

        End Sub

        Public Sub m_ImageTable_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.ListViewItemEventArgs) Handles m_ImageTable.ItemDataBound
            Dim item As ListViewItem = e.Item
            ' Verify if the item is a data item.
            If item.ItemType = ListViewItemType.DataItem Then
                Dim rowView As System.Data.DataRowView
                rowView = CType(e.Item, System.Web.UI.WebControls.ListViewDataItem).DataItem
                Dim img As Button = e.Item.FindControl("btnMarketingGraphic")
                Dim lbl As Label = e.Item.FindControl("lblCourseInformationType")
                SetMarketingImage(img, rowView, lbl)
            End If
        End Sub

        Public Sub SetMarketingImage(ByVal img As Button, ByVal rowView As System.Data.DataRowView, lbl As Label)

            Dim Value As String = rowView("Value")
            Dim strDescription As String = rowView("Description")
            Dim imageUrl As String = GetResourceValue(Value)
            If Len(imageUrl) = 0 Then
                imageUrl = String.Format("_images/search/{0}.png", Value)
            End If

            img.ID = "img_" & Value
            img.Attributes.Add("style", "height:120px;background-image:url(" & imageUrl & ")")

            'img.AlternateText = strDescription
            img.Attributes.Add("runat", "server")
            lbl.Text = strDescription
            AddHandler img.Click, AddressOf Button_Click

        End Sub

        Protected Sub Button_Click(ByVal sender As Object, ByVal e As EventArgs)
            Dim img As Button = CType(sender, Button)
            Dim fieldName As String = img.ID.Substring(img.ID.IndexOf("_") + 1)
            If Me.MarketingField1 <> "All" AndAlso ViewState("Searched") = True Then
                Me.MarketingField2 = fieldName
            Else
                Me.MarketingField1 = fieldName
                Me.MarketingField2 = Nothing
            End If

            DoSearch()

            'Store the criteria in the session here as there wont have been any when the page loaded
            '(when we click the search button the ddl have already been filled so they will be stored automatically)
            StoreCriteriaInSession()
            PopulateMarketingField(m_ctlMarketingField2, m_intMarketingField2CourseInformationTypeID, m_intMarketingField2ParentCourseInformationTypeID, MarketingField2)
        End Sub


#End Region

#Region " Grid "

        Public Property StartRowIndex() As Integer
            Get
                Dim value As Object = Page.Session("StartRowIndex")
                If value Is Nothing Then
                    Return 0
                Else
                    Return CInt(value)
                End If
            End Get
            Set(ByVal value As Integer)
                Page.Session("StartRowIndex") = value
            End Set
        End Property

        Private WithEvents m_Grid As ListView
        Public Property Grid() As ListView
            Get
                Return m_Grid
            End Get
            Set(ByVal value As ListView)
                m_Grid = value
            End Set
        End Property

        Private WithEvents m_DataPager As DataPager
        Public Property DataPager() As DataPager
            Get
                Return m_DataPager
            End Get
            Set(ByVal value As DataPager)
                m_DataPager = value
            End Set
        End Property

        Public Sub LoadGridData()
            If Grid Is Nothing Then Exit Sub
            'DataPager.SetPageProperties(Me.StartRowIndex, DataPager.MaximumRows, True)
            If GetSearchFields.Count > 0 Then
                LoadGridDataWithSearch()
            Else
                LoadGridDataNoSearch()
            End If
            'Hide the image table
            'ImageTable.Visible = False
        End Sub

        Public Sub LoadGridDataNoSearch()
            Dim t As New CourseInformationDataTable

            Dim v As CCCDataViewDataSet = CCCDataViewDataSet.CreateDataViewDefault(t)
            v.Columns.Clear()
            v.Columns.AddPKColumns()
            v.Columns.EnsureColumnsAreSelected(True, False, t.DescriptionColumn, t.AcademicYearIDColumn, t.WebsiteAvailabilityIDColumn)
            v.Filters.SetColumnFilter(t.ObsoleteColumn, False)
            v.Filters.SetColumnFilter(t.WebsiteAvailabilityIDColumn, 0, FilterOperator.OperatorNotEquals)
            v.SortOrders.Clear()
            v.SortOrders.SetColumnSortOrder(CCCCommon.SortDirection.Ascending, t.DescriptionColumn, False)


            t.Columns.Add("Score", GetType(Integer), "0")

            t.TableAdapter.Load(t, v)
            Grid.DataSource = t

            m_CoursesInBasket = New System.Collections.Generic.List(Of Integer)
            For Each item As ShoppingCartItem In WorkingData.ShoppingCart.Items
                m_CoursesInBasket.Add(item.CourseInfoID)
            Next

            Grid.DataBind()

        End Sub

        Public Sub LoadGridDataWithSearch()
            Dim t As New CourseInformationDataTable

            Dim v As CCCDataViewDataSet = CCCDataViewDataSet.CreateDataViewDefault(t)
            v.Columns.Clear()
            v.Columns.AddPKColumns()
            v.Columns.EnsureColumnsAreSelected(True, False, t.DescriptionColumn, t.AcademicYearIDColumn)
            v.SortOrders.Clear()

            t.TableAdapter.LoadSearch(t, Me.GetSearchFields)

            Grid.DataSource = t

            m_CoursesInBasket = New System.Collections.Generic.List(Of Integer)
            For Each item As ShoppingCartItem In WorkingData.ShoppingCart.Items
                m_CoursesInBasket.Add(item.CourseInfoID)
            Next

            Grid.DataBind()

        End Sub

        Dim m_blnPageChanged As Boolean = False
        Public Sub m_Grid_PagePropertiesChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles m_Grid.PagePropertiesChanged
            LoadGridData()
            m_blnPageChanged = True
        End Sub

#End Region


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

        Public Sub m_Grid_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.ListViewItemEventArgs) Handles m_Grid.ItemDataBound#
            Dim item As ListViewItem = e.Item
            ' Verify if the item is a data item.
            If item.ItemType = ListViewItemType.DataItem Then
                Dim rowView As System.Data.DataRowView
                rowView = CType(e.Item, System.Web.UI.WebControls.ListViewDataItem).DataItem
                Dim img As HyperLink = e.Item.FindControl("ApplyOrEnrolmentBtn")
                SetWebSiteAvailabilityIcon(img, rowView)

                Dim lbl As Label = e.Item.FindControl("lblScore")
                FormatScore(lbl, rowView)
            End If
        End Sub

        Public Sub SetWebSiteAvailabilityIcon(ByVal img As HyperLink, ByVal rowView As System.Data.DataRowView)
            Dim dataValue As String = rowView("WebSiteAvailabilityID").ToString
            Dim intCourseInfoID As Integer = CInt(rowView("CourseInformationID"))
            Dim imageURL As String = String.Empty
            Dim imageText As String = String.Empty
            Dim NavigateUrl As String = String.Empty

            If m_CoursesInBasket.Contains(intCourseInfoID) Then
                imageText = "Course already in basket"
                imageURL = GetResourceValue("basketsmall")
                NavigateUrl = GetResourceValue("checkout_aspx")
            ElseIf CCCBlankLibrary.IsBlank(dataValue) Then
                imageURL = GetResourceValue("icon_noofferings")
                NavigateUrl = GetResourceValue("courseunavailable_aspx")
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
                    NavigateUrl = GetResourceValue("coursedisplay_aspx") & IIf(GetResourceValue("coursedisplay_aspx").Contains("?"), "&", "?").ToString & "CourseInformationID=" & intCourseInfoID.ToString
                Else
                    imageURL = GetResourceValue("icon_noofferings")
                    NavigateUrl = GetResourceValue("courseunavailable_aspx")
                End If

            End If

            img.Text = imageText
            img.ImageUrl = imageURL
            img.NavigateUrl = NavigateUrl
        End Sub

        Public Sub FormatScore(ByVal lblScore As Label, ByVal rowView As System.Data.DataRowView)
            Dim dataValue As String = rowView("Score").ToString
            lblScore.Text = CInt(dataValue).ToString & "%"
        End Sub

        Public Sub m_DataPager_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles m_DataPager.PreRender
            If ViewState("Searched") = True AndAlso Not m_blnPageChanged Then
                DataPager.SetPageProperties(0, DataPager.MaximumRows, True)
            End If
        End Sub
    End Class

End Namespace

