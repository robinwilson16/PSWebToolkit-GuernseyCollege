Imports CompassCC.ProSolution.PSWebEnrolmentKit

Partial Class checkout_makepayment
    Inherits CheckoutBaseControl

    Protected Overrides Sub OnLoad(ByVal e As System.EventArgs)
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

        If SystemSettings.UseWorldPay Then
            PaymentSubmitter1.DisplayTextForWorldPayTransfer = "Thank you, you are now being forwarded to WorldPay to complete this transaction..."
        ElseIf SystemSettings.UseRealEx Then
            PaymentSubmitter1.DisplayTextForWorldPayTransfer = "Thank you, you are now being forwarded to RealEx to complete this transaction..."
        ElseIf SystemSettings.UsePayPal Then
            PaymentSubmitter1.DisplayTextForWorldPayTransfer = "Thank you, you are now being forwarded to PayPal to complete this transaction..."
        ElseIf SystemSettings.UseCapita Then
            PaymentSubmitter1.DisplayTextForWorldPayTransfer = "Thank you, you are now being forwarded to Capita Payments to complete this transaction..."
        ElseIf SystemSettings.UseCivica Then
            PaymentSubmitter1.DisplayTextForWorldPayTransfer = "Thank you, you are now being forwarded to Civica to complete this transaction..."
        ElseIf SystemSettings.UseXPay Then
            PaymentSubmitter1.DisplayTextForWorldPayTransfer = "Thank you, you are now being forwarded to X-Pay to complete this transaction..."
        ElseIf SystemSettings.UseWPM Then 'CCCPS-79462 check for thank you message.
            PaymentSubmitter1.DisplayTextForWorldPayTransfer = "Thank you, you are now being forwarded to WPM to complete this transaction..."
        End If

        'Show error message if no javascript 
        Panel1.Visible = Not Me.Request.Browser.EcmaScriptVersion.Major >= 1

    End Sub


End Class
