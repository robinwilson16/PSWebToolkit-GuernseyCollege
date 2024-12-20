Imports CompassCC.ProSolution.PSWebEnrolmentKit
''' <summary>
''' ''CCCPS-76339: New step in a wizard to enable user to provide additional 2 contact information.
''' </summary>
Partial Class webcontrols_checkout_other_contacts
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
        Me.Contact1panel.Attributes("class") = ""
        Me.Contact2panel.Attributes("class") = ""

        If WorkingData.ShoppingCart.ContainsItemsOfType("Application") AndAlso WorkingData.ShoppingCart.ContainsItemsOfType("Enrolment") Then
            Me.Contact1panel.Attributes("class") = "panel panel-danger"
            Me.Contact2panel.Attributes("class") = "panel panel-danger"
        ElseIf WorkingData.ShoppingCart.ContainsItemsOfType("Enrolment") Then
            Me.Contact1panel.Attributes("class") = "panel panel-danger"
            Me.Contact2panel.Attributes("class") = "panel panel-danger"
        ElseIf WorkingData.ShoppingCart.ContainsItemsOfType("Application") Then
            Me.Contact1panel.Attributes("class") = "panel panel-success"
            Me.Contact2panel.Attributes("class") = "panel panel-success"
        ElseIf WorkingData.ShoppingCart.ContainsItemsOfType("Enquiry") Then
            Me.Contact1panel.Attributes("class") = "panel panel-info"
            Me.Contact2panel.Attributes("class") = "panel panel-info"
        Else
            Me.Contact1panel.Attributes("class") = "panel panel-danger"
            Me.Contact2panel.Attributes("class") = "panel panel-danger"
        End If

    End Sub

    Protected Sub btnBack_Click(sender As Object, e As EventArgs) Handles btnBack.Click
        Response.Redirect(GetResourceValue("checkout_parentguardian_aspx"))
    End Sub

    Private Sub btnContinue_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnContinue.Click

        Me.Page.Validate()

        If Me.Page.IsValid Then
            If WorkingData.ShoppingCart.ContainsItemsOfType("Application") Then
                Response.Redirect(GetResourceValue("checkout_priorattainment_aspx"))
            ElseIf WorkingData.ShoppingCart.ContainsItemsOfType("Enrolment") Then
                Response.Redirect(GetResourceValue("checkout_school_employer_aspx"))
            End If
        End If
    End Sub

End Class
