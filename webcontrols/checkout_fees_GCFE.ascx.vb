Imports CompassCC.CCCSystem.CCCCommon
Imports CompassCC.ProSolution.PSWebEnrolmentKit


Partial Class webcontrols_checkout_fees_GCFE
    Inherits CheckoutBaseControl

    Public HasFormErrors As Boolean = False
    Public Course As OfferingRow
    Public CourseFee As OfferingFeeRow

    Protected Overrides Sub OnLoad(e As EventArgs)
        MyBase.OnLoad(e)

        If Not IsNothing(WorkingData.ShoppingCart.Items(0)) Then
            Course = OfferingDataTable.FetchByOfferingID(WorkingData.ShoppingCart.Items(0).OfferingID)
        Else
            Course = Nothing
        End If

        If Not IsNothing(Course) Then
            CourseFee = OfferingFeeDataTableAdapter.GetOfferingFeeRow(CInt(Course.OfferingID), 1, 0)
        Else
            CourseFee = Nothing
        End If

        PaymentMethodDropdown.Items.Clear()
        PaymentMethodDropdown.Items.Add(New ListItem("-- Please Select --", String.Empty, False))
        PaymentMethodDropdown.Items.Add(New ListItem("Cash", "CASH", False))
        PaymentMethodDropdown.Items.Add(New ListItem("Cheque", "CHEQUE", False))
        PaymentMethodDropdown.Items.Add(New ListItem("Credit/Debit Card", "CARD", False))
    End Sub

    Public Overrides Sub ValidateControl()
        If Not IsNothing(SourceOfTuitionFees) Then
            If SourceOfTuitionFees.SelectedValue = "" Then
                SourceOfTuitionFeesValidator.ErrorMessage = "<i class=""fa-solid fa-triangle-exclamation""></i> Please select who is paying your course fees"
                SourceOfTuitionFeesValidator.IsValid = False
                SourceOfTuitionFeesValidator.CssClass = "error alert alert-danger"
                SourceOfTuitionFees.CssClass = "ErrorInput"
            ElseIf SourceOfTuitionFees.SelectedValue = "OTHER" And (fldApplicationUserDefined17.Value = "" Or fldApplicationUserDefined17.Value = "-- Please Select --") Then
                fldApplicationUserDefined17Validator.ErrorMessage = "<i class=""fa-solid fa-triangle-exclamation""></i> As you have stated someone else is paying your course fees, please confirm who this is"
                fldApplicationUserDefined17Validator.IsValid = False
                fldApplicationUserDefined17Validator.CssClass = "error alert alert-danger"
                fldApplicationUserDefined17.CssClass = "ErrorInput"
            End If
        End If

        If Not IsNothing(SourceOfTuitionFees) Then
            If SourceOfTuitionFees.SelectedValue <> "EMPLOYER" And (fldApplicationUserDefined18.Value = "" Or fldApplicationUserDefined18.Value = "-- Please Select --") Then
                fldApplicationUserDefined18Validator.ErrorMessage = "<i class=""fa-solid fa-triangle-exclamation""></i> Please confirm the payment method that will be used"
                fldApplicationUserDefined18Validator.IsValid = False
                fldApplicationUserDefined18Validator.CssClass = "error alert alert-danger"
                fldApplicationUserDefined18.CssClass = "ErrorInput"
            End If
        End If

        MyBase.ValidateControl()
    End Sub

    Private Sub btnContinue_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnContinue.Click
        WorkingData.ApplicationRequestRow.ApplicationUserDefined17 = SourceOfTuitionFees.SelectedValue
        WorkingData.ApplicationRequestRow.ApplicationUserDefined18 = PaymentMethodDropdown.Value

        Me.Page.Validate()

        If Me.Page.IsValid Then
            If SourceOfTuitionFees.SelectedValue = "EMPLOYER" Then
                Response.Redirect(GetResourceValue("checkout_employment_GCFE"))
            Else
                Response.Redirect(GetResourceValue("checkout_dataprotection_apponly_GCFE_aspx"))
            End If
        Else
            HasFormErrors = True
        End If
    End Sub

End Class
