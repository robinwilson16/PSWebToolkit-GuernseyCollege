Imports CompassCC.ProSolution.PSWebEnrolmentKit

Partial Class checkout_prior_attainment
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
        Me.panel1.Attributes("class") = ""



        If WorkingData.ShoppingCart.ContainsItemsOfType("Enrolment") Then
            Me.panel1.Attributes("class") = "panel panel-danger"
        ElseIf WorkingData.ShoppingCart.ContainsItemsOfType("Application") Then
            Me.panel1.Attributes("class") = "panel panel-success"
        ElseIf WorkingData.ShoppingCart.ContainsItemsOfType("Enquiry") Then
            Me.panel1.Attributes("class") = "panel panel-info"
        Else
            Me.panel1.Attributes("class") = "panel panel-danger"
        End If
    End Sub

    Private Sub btnBack_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBack.Click


        If WorkingData.ShoppingCart.ContainsItemsOfType("Application") AndAlso WorkingData.ShoppingCart.ContainsItemsOfType("Enrolment") Then
            Response.Redirect(GetResourceValue("checkout_school_employer_aspx"))
        ElseIf WorkingData.ShoppingCart.ContainsItemsOfType("Application") Then
            ''CCCPS-76339: Redirecting to new Other Contacts step of a wizard on Back click.
            Response.Redirect(GetResourceValue("checkout_other_contacts_aspx"))
        ElseIf WorkingData.ShoppingCart.ContainsItemsOfType("Enrolment") Then
            Response.Redirect(GetResourceValue("checkout_school_employer_aspx"))
        End If

    End Sub
End Class
