Imports CompassCC.ProSolution.PSWebEnrolmentKit
Imports CompassCC.CCCSystem.CCCCommon

Partial Class webcontrols_checkout_school_employer
    Inherits CheckoutBaseControl

    Protected Overrides Sub OnLoad(e As EventArgs)
        'Me.Page.ClientScript.RegisterStartupScript(Me.Page.GetType, "Load", "<script type=""text/javascript""> $(function () {var c = $(""#employerpanel""); c.showLoading(); }); </script>")
        'Me.Page.ClientScript.RegisterStartupScript(Me.Page.GetType, "UnLoad", "<script type=""text/javascript""> $(function () {var c = $(""#employerpanel""); c.hideLoading(); }); </script>")
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
        Me.schoolpanel.Attributes("class") = ""
        Me.employerpanel.Attributes("class") = ""
        'Me.empnote.Attributes("class") = ""

        If WorkingData.ShoppingCart.ContainsItemsOfType("Application") AndAlso WorkingData.ShoppingCart.ContainsItemsOfType("Enrolment") Then
            Me.schoolpanel.Attributes("class") = "panel panel-danger"
            Me.employerpanel.Attributes("class") = "panel panel-danger"
            'Me.empnote.Attributes("class") = "alert alert-danger"
        ElseIf WorkingData.ShoppingCart.ContainsItemsOfType("Enrolment") Then
            Me.schoolpanel.Attributes("class") = "panel panel-danger"
            Me.employerpanel.Attributes("class") = "panel panel-danger"
            'Me.empnote.Attributes("class") = "alert alert-danger"
        ElseIf WorkingData.ShoppingCart.ContainsItemsOfType("Application") Then
            Me.schoolpanel.Attributes("class") = "panel panel-success"
            Me.employerpanel.Attributes("class") = "panel panel-success"
            'Me.empnote.Attributes("class") = "alert alert-success"
        ElseIf WorkingData.ShoppingCart.ContainsItemsOfType("Enquiry") Then
            Me.schoolpanel.Attributes("class") = "panel panel-info"
            Me.employerpanel.Attributes("class") = "panel panel-info"
            'Me.empnote.Attributes("class") = "alert alert-info"
        Else
            Me.schoolpanel.Attributes("class") = "panel panel-danger"
            Me.employerpanel.Attributes("class") = "panel panel-danger"
            'Me.empnote.Attributes("class") = "alert alert-danger"
        End If

    End Sub


    Private Sub btnContinue_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnContinue.Click

        Me.Page.Validate()

        If Me.Page.IsValid Then
            Dim pcode As String = Replace(employerpostcode.Value, " ", "")
            If Len(pcode) > 0 Then
                WorkingData.EnrolmentRequest(0).EmployerPostcodeOut = pcode.Substring(0, pcode.Length - 3)
                WorkingData.EnrolmentRequest(0).EmployerPostcodeIn = Right(pcode, 3)
            End If

            If WorkingData.ShoppingCart.ContainsItemsOfType("Application") Then
                ''CCCPS-75837: Redirecting user to new Attachment step instead of data protection directly.
                Response.Redirect(GetResourceValue("checkout_attachments_aspx"))
            Else
                Response.Redirect(GetResourceValue("checkout_priorattainment_aspx"))
            End If

        End If

    End Sub


End Class
