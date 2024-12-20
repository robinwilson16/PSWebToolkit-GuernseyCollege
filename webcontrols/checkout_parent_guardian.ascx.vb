Imports CompassCC.ProSolution.PSWebEnrolmentKit

Partial Class webcontrols_checkout_parent_guardian
    Inherits CheckoutBaseControl


    Protected Overrides Sub OnLoad(e As EventArgs)
        MyBase.OnLoad(e)

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

        'Custom code to colour the form based on the contents of the shopping basket
        'Use this when sharing pages between types (Application/Enrolment)

        'Clear class attributes first
        Me.parent1panel.Attributes("class") = ""
        Me.parent2panel.Attributes("class") = ""

        If WorkingData.ShoppingCart.ContainsItemsOfType("Application") AndAlso WorkingData.ShoppingCart.ContainsItemsOfType("Enrolment") Then
            Me.parent1panel.Attributes("class") = "panel panel-danger"
            Me.parent2panel.Attributes("class") = "panel panel-danger"
        ElseIf WorkingData.ShoppingCart.ContainsItemsOfType("Enrolment") Then
            Me.parent1panel.Attributes("class") = "panel panel-danger"
            Me.parent2panel.Attributes("class") = "panel panel-danger"
        ElseIf WorkingData.ShoppingCart.ContainsItemsOfType("Application") Then
            Me.parent1panel.Attributes("class") = "panel panel-success"
            Me.parent2panel.Attributes("class") = "panel panel-success"
        ElseIf WorkingData.ShoppingCart.ContainsItemsOfType("Enquiry") Then
            Me.parent1panel.Attributes("class") = "panel panel-info"
            Me.parent2panel.Attributes("class") = "panel panel-info"
        Else
            Me.parent1panel.Attributes("class") = "panel panel-danger"
            Me.parent2panel.Attributes("class") = "panel panel-danger"
        End If

    End Sub

    Protected Sub btnBack_Click(sender As Object, e As EventArgs) Handles btnBack.Click


        If WorkingData.ShoppingCart.ContainsItemsOfType("Application") Then
            Response.Redirect(GetResourceValue("checkout_applications2_aspx"))
        ElseIf WorkingData.ShoppingCart.ContainsItemsOfType("Enrolment") Then
            Response.Redirect(GetResourceValue("checkout_enrolments2_aspx"))
        ElseIf WorkingData.ShoppingCart.ContainsItemsOfType("Enquiry") Then
            Response.Redirect(GetResourceValue("checkout_enquiries_aspx"))
        End If

    End Sub

    Private Sub btnContinue_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnContinue.Click

        Me.Page.Validate()

        If Me.Page.IsValid Then
            ''CCCPS-76339: Redirecting to new Other Contacts step of a wizard on Continue click.
            Response.Redirect(GetResourceValue("checkout_other_contacts_aspx"))
        End If
    End Sub

End Class
