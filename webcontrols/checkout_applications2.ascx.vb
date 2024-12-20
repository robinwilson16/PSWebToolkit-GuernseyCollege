Imports CompassCC.CCCSystem.CCCCommon
Imports CompassCC.ProSolution.PSWebEnrolmentKit


Partial Class webcontrols_checkout_applications2
    Inherits CheckoutBaseControl

    Protected Overrides Sub OnLoad(e As EventArgs)
        MyBase.OnLoad(e)

        WorkingData.EnrolmentRequestRow.EuroResidentID = True

        
    End Sub

    Private Sub btnContinue_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnContinue.Click

        Me.Page.Validate()

        If Me.Page.IsValid Then
            If fldEuroResidentID.Value = False Then
                Response.Redirect(GetResourceValue("onlineenrolmentnotavailable_e_aspx"))
            End If
        End If
    End Sub

End Class
