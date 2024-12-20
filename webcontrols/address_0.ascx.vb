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
Imports com.qas.prowebintegration
Imports CompassCC.ProSolution.PSWebEnrolmentKit

Partial Class address_0
    Inherits CheckAddressBaseControl

    Protected Overrides Sub OnLoad(e As System.EventArgs)
        MyBase.OnLoad(e)

        If SystemSettings.UseMatchcode360 Then
            Response.Redirect(GetResourceValue("address_matchcode360_aspx"))
            'ElseIf SystemSettings.UseCapscan Then
            '    Response.Redirect(GetResourceValue("address_capscan_aspx"))
        ElseIf SystemSettings.UseGetAddress Then
            Response.Redirect(GetResourceValue("address_getaddress_aspx"))
        ElseIf SystemSettings.UseAFDEvolution Then
            Response.Redirect(GetResourceValue("address_AFDEvolution_aspx"))
        ElseIf SystemSettings.UseAFD Then
            Response.Redirect(GetResourceValue("address_AFD_aspx"))
        ElseIf SystemSettings.UseQASProOnDemand Then
            Response.Redirect(GetResourceValue("address_QASProOnDemand_aspx"))
        End If

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

    End Sub


    Protected Sub btnContinue_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnContinue.Click
        GoInputPage()
    End Sub

    Protected Sub btnBack_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBack.Click


        'If CurrentWebSiteMode = WebSiteMode.StudentAssistedEnrolment OrElse CurrentWebSiteMode = WebSiteMode.StaffAssistedEnrolment Then
        '    Server.Transfer("checkout_enrolments.aspx")
        'End If


    End Sub
End Class