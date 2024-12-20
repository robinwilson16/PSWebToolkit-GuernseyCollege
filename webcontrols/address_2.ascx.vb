
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
Imports com.qas.proweb
Imports com.qas.prowebintegration
Imports CompassCC.ProSolution.PSWebEnrolmentKit

Partial Class address_2
    Inherits CheckAddressBaseControl



    Protected FIELD_MUST_REFINE As String = "MustRefine"

    Protected m_Picklist As Picklist




    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

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
            DoSearch()
        End If
        'Else leave it to the event handlers (Next, Back)

    End Sub



    '** Search operations **


    ' Perform search using DataID and InputLines
    Private Sub DoSearch()

        Dim eRoute As Constants.Routes = Constants.Routes.Undefined

        ' If the picklist moniker is not present then we haven't been here before, and can auto-skip forwards
        Dim bAutoSkip As Boolean = (GetPicklistMoniker() Is Nothing)

        Try

            Dim searchService As QuickAddress = NewQuickAddress()
            searchService.Engine = QuickAddress.EngineTypes.SingleLine
            searchService.Flatten = True

            If bAutoSkip Then
                m_Picklist = searchService.Search(GetDataID(), GetInputLines(), GetPromptSet()).Picklist
                SetPicklistMoniker(m_Picklist.Moniker)
            Else
                ' Recreate picklist from moniker
                m_Picklist = searchService.StepIn(GetPicklistMoniker())
            End If

            'Handle 'failure' cases
            If m_Picklist.IsTimeout Then
                eRoute = Constants.Routes.Timeout
            ElseIf m_Picklist.IsMaxMatches Then
                eRoute = Constants.Routes.TooManyMatches
            ElseIf m_Picklist.Items Is Nothing Then
                eRoute = Constants.Routes.NoMatches
            End If

        Catch x As Exception

            GoErrorPage(x)

        End Try


        If Not eRoute.Equals(Constants.Routes.Undefined) Then

            GoErrorPage(eRoute)

        ElseIf bAutoSkip Then

            If m_Picklist.IsAutoFormatSafe Or m_Picklist.IsAutoFormatPastClose Then

                'Auto-step past picklist to format page
				eRoute = Constants.Routes.Okay
                Request.Cookies.Set(New HttpCookie("Moniker", m_Picklist.Items(0).Moniker))
                Server.Transfer(GetResourceValue("address_4_aspx"))
                'GoFormatPage(m_Picklist.Items(0).Moniker)

            ElseIf m_Picklist.Length = 1 And MustRefine(m_Picklist.Items(0)) Then

                'Auto-step past picklist to refinement page
                GoRefinementPage(m_Picklist.Items(0).Moniker)

            End If

        End If

        'Else let page render the picklist itself


    End Sub

    ' Helper function: must the picklist item be refined (extra text entered)
    Protected Function MustRefine(ByVal item As PicklistItem) As Boolean

        Return (item.IsIncompleteAddress Or item.IsUnresolvableRange Or item.IsPhantomPrimaryPoint)

    End Function

    Protected Sub btnContinue_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnContinue.Click
        'Pick up hidden field, set by client JavaScript when they selected an item
        Dim bMustRefine As Boolean = Not (Request(FIELD_MUST_REFINE) = "")
        If bMustRefine Then
            GoRefinementPage(GetMoniker())
        Else
            GoFormatPage(Nothing)                'Moniker already set on page
        End If

    End Sub

    Protected Sub btnBack_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBack.Click
        GoInputPage()

    End Sub
End Class
