Imports CompassCC.ProSolution.PSWebEnrolmentKit

Partial Class checkout_dataprotection
    Inherits CheckoutBaseControl


    Protected Overrides Sub OnLoad(e As EventArgs)
        MyBase.OnLoad(e)

        Dim p As New PaymentSubmitter

        If p.ShouldTransferToPaymentSite AndAlso (SystemSettings.UsePayPal Or SystemSettings.UseXPay Or SystemSettings.UseWPM) AndAlso WorkingData.ShoppingCart.GetTotalCost > 0 Then 'CCCPS-79462 add WPM.
            btnContinue.ImageResource = "PayPalButton"
        End If
    End Sub

    Public Overrides Sub ValidateControl()

        If Not chkConfirm.Checked Then
            Dim v As New CustomValidator()
            v.ErrorMessage = "Please confirm that you have read the declaration above."
            v.IsValid = False
            Me.Page.Validators.Add(v)

            Me.ValidationSummary1.ShowSummary = True
        End If
    End Sub

    Protected Sub btnContinue_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnContinue.Click

        ValidateControl()

        If Me.Page.IsValid Then

            Server.Transfer(GetResourceValue("checkout_makepayment_aspx"))
        End If

    End Sub

End Class
