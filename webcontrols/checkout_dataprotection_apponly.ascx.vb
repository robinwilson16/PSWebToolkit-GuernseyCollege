Imports CompassCC.ProSolution.PSWebEnrolmentKit

Partial Class checkout_dataprotection_apponly
    Inherits CheckoutBaseControl

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
        Me.Page.Validate()

        If Me.Page.IsValid Then

            Server.Transfer(GetResourceValue("checkout_makepayment_aspx"))
        End If

    End Sub

End Class
