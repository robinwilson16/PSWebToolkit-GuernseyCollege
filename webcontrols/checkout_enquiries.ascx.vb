Option Explicit On
Option Strict On

Imports CompassCC.CCCSystem.CCCCommon
Imports CompassCC.ProSolution.PSWebEnrolmentKit

Partial Class webcontrols_checkout_enquiries
    Inherits CheckoutBaseControl

    Protected Overrides Sub OnLoad(e As EventArgs)
        MyBase.OnLoad(e)

        If PaymentSubmitter.AllowEmptyBasket And WorkingData.ShoppingCart.Items.Count = 0 Then
            Session("RequestMode") = RequestMode.EnquiryRequest
        End If


    End Sub
End Class
