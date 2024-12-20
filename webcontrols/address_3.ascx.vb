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

Partial Class address_3
    Inherits CheckAddressBaseControl



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

        'Pre-set event to call when <Enter> is hit (otherwise no event will be raised)
        Me.Page.ClientScript.RegisterHiddenField("__EVENTTARGET", btnContinue.ClientID)
        'Page.RegisterHiddenField("__EVENTTARGET", btnContinue.ClientID)

        If Not IsPostBack Then
            RefineAddress()
        End If
        'Else leave it to the event handlers (Next, Back)

    End Sub



    '/** Search operations **


    ' Try to refine the current Moniker using the entered refinement text
    Protected Sub RefineAddress()

        Dim item As PicklistItem = Nothing
        Dim bFinalAddress As Boolean = False 'jump straight to the final address page?

        Try

            'Perform the refinement
            Dim searchService As QuickAddress = NewQuickAddress()
            Dim Picklist As Picklist = searchService.Refine(GetRefineMoniker(), RefinementText)

            ' If the refined search produces no results, recreate the picklist and update the message

            If Picklist.Length = 0 Then
                'No acceptable address match - recreate without using refinement text
                Picklist = searchService.Refine(GetRefineMoniker(), "")
            End If

            item = Picklist.Items(0)
            'Update page content
            LiteralRefineLine.Text = item.Text
            LiteralRefineAddress.Text = item.PartialAddress

            If RefinementText = "" Then

                'First time through - just display
                Dim bIsPhantom As Boolean = item.IsPhantomPrimaryPoint
                If bIsPhantom Then
                    LiteralMessage.Text = "Your selection requires secondary information. Enter your exact details."
                Else
                    LiteralMessage.Text = "Your selection covers a range of addresses. Enter your exact details."
                End If

            ElseIf (item.IsUnresolvableRange) Then

                'Redisplay with explanation
                LiteralMessage.Text = "You entered '" & RefinementText & "', but this address is outside of the range. " _
                 & "Please try again or click Back and select the correct range."

            Else

                'Accept (or force accept in the Phantom case)
                bFinalAddress = True

            End If

        Catch x As Exception

            GoErrorPage(x)

        End Try

        If bFinalAddress Then
            GoFormatPage(item.Moniker)
        End If


    End Sub


    ' 'Back' button clicked
    Private Sub ButtonBack_ServerClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonBack.ServerClick

        GoSearchPage()

    End Sub


    '** Page controls **


    Private Property RefinementText() As String

        Get
            Return TextRefinement.Value
        End Get

        Set(ByVal Value As String)
            TextRefinement.Value = Value
        End Set

    End Property


    Protected Sub btnContinue_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnContinue.Click
        RefineAddress()

    End Sub
End Class
