Imports System
Imports System.Collections
Imports System.ComponentModel
Imports System.Data
Imports System.Drawing
Imports System.Web
Imports System.Web.SessionState
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.HtmlControls
Imports com.qas.proweb       'QuickAddress services
Imports com.qas.prowebintegration
Imports CompassCC.ProSolution.PSWebEnrolmentKit

'Namespace com.qas.prowebintegration
Partial Class address_1
    Inherits CheckAddressBaseControl

    Protected m_atPromptLines As PromptLine()

    Private Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load

        'Breadcrumbs
        If WorkingData.ShoppingCart.ContainsItemsOfType("Application") AndAlso WorkingData.ShoppingCart.ContainsItemsOfType("Enrolment") Then
            breadcrumbenrols.Visible = True
            breadcrumbapps.Visible = False
        ElseIf WorkingData.ShoppingCart.ContainsItemsOfType("Application") Then
            breadcrumbenrols.Visible = False
            breadcrumbapps.Visible = True
        ElseIf WorkingData.ShoppingCart.ContainsItemsOfType("Enrolment") Then
            breadcrumbenrols.Visible = True
            breadcrumbapps.Visible = False
        ElseIf WorkingData.ShoppingCart.ContainsItemsOfType("Enquiry") Then
            breadcrumbenrols.Visible = False
            breadcrumbapps.Visible = False
        Else
            breadcrumbenrols.Visible = False
            breadcrumbapps.Visible = False
        End If

        If Not IsPostBack Then

            'Populate hidden field
            PagePromptSet = GetPromptSet()
            'Get the search prompts for the page
            RetrieveSearchPrompts()
        End If

        'Else leave it to the event handlers (Next, Back, Alternate Prompts)

    End Sub


    '** Operations **


    ' Check if searching is available for this country/layout then retrieve the appropriate prompts
    Sub RetrieveSearchPrompts()

        Dim bPreSearchFailed As Boolean = False

        Try

            Dim searchService As QuickAddress = NewQuickAddress()
            searchService.Engine = QuickAddress.EngineTypes.SingleLine

            'Retrieve layout from web.config
            Dim sLayout As String = SystemSettings.QAS_Layout 'Constants.KEY_LAYOUT

            'Is automatic address capture available for this country & layout
            Try

                m_atPromptLines = searchService.GetPromptSet(GetDataID(), PagePromptSet).Lines

            Catch

                'QuickAddress is not available for this country
                bPreSearchFailed = True
            End Try

        Catch x As Exception

            GoErrorPage(x)

        End Try


        If (bPreSearchFailed) Then

            GoErrorPage(Constants.Routes.PreSearchFailed)

        Else

            HyperlinkAlternate.Visible = Not IsAlternate()
            'And let page render prompts

        End If

    End Sub


    'Hyperlink for alternate prompt set clicked: update and redisplay
    Private Sub HyperlinkAlternate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles HyperlinkAlternate.Click

        PagePromptSet = PromptSet.Types.Alternate
        RetrieveSearchPrompts()

    End Sub


    ' Get/set the value of the Prompt Set field on the form
    Private Property PagePromptSet() As PromptSet.Types

        Get
            Dim sValue As String = HiddenPromptSet.Value
            If Not sValue Is Nothing Then
                Return System.Enum.Parse(GetType(PromptSet.Types), sValue)
            Else
                Return PromptSet.Types.Optimal
            End If
        End Get

        Set(ByVal Value As PromptSet.Types)
            HiddenPromptSet.Value = Value.ToString()
        End Set

    End Property

    ' Are we currently using the standard or alternate prompt set?
    Private Function IsAlternate() As Boolean

        Return Not (PagePromptSet.Equals(PromptSet.Types.Optimal))

    End Function

    Public Overrides Sub ValidateControl()
        Dim strLines As String() = CStr(Me.Request(com.qas.prowebintegration.Constants.FIELD_INPUT_LINES)).Split(",")
        If strLines.Length >= 2 And strLines.Length <= 3 Then
            For Each str As String In strLines
                If Len(str) = 0 AndAlso Me.Page.Validators.Count = 0 Then
                    Dim v As New CustomValidator
                    v.ErrorMessage = "You must enter all the address values"
                    v.IsValid = False
                    Me.Page.Validators.Add(v)
                End If
            Next
        End If
    End Sub

    Protected Sub btnContinue_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnContinue.Click
        Me.Page.Validate()

        If Me.Page.IsValid Then
            SetPromptSet(PagePromptSet)
            GoSearchPage()
        Else
            'Populate hidden field
            PagePromptSet = GetPromptSet()
            'Get the search prompts for the page
            RetrieveSearchPrompts()

        End If

    End Sub

    Protected Sub btnBack_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBack.Click
        'If IsAlternate() Then

        '    PagePromptSet = PromptSet.Types.Optimal
        '    RetrieveSearchPrompts()


        'Else

        '    GoFirstPage()
        'End If
		If WorkingData.ShoppingCart.ContainsItemsOfType("Enrolment") Then
            Response.Redirect(GetResourceValue("checkout_enrolments"))
        ElseIf WorkingData.ShoppingCart.ContainsItemsOfType("Application") Then
            Response.Redirect(GetResourceValue("checkout_applications"))
        Elseif WorkingData.ShoppingCart.ContainsItemsOfType("Enquiry")
            Response.Redirect(GetResourceValue("checkout_enquiries"))
        End If

    End Sub
End Class

'End Namespace