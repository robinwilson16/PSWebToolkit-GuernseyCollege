Imports CompassCC.ProSolution.PSWebEnrolmentKit

Partial Class checkout_dataprotection_apponly_GCFE
    Inherits CheckoutBaseControl

    Public HasFormErrors As Boolean = False

    Public Course As OfferingRow

    Protected Overrides Sub OnLoad(e As EventArgs)
        MyBase.OnLoad(e)

        If Not IsNothing(WorkingData.ShoppingCart.Items(0)) Then
            Course = OfferingDataTable.FetchByOfferingID(WorkingData.ShoppingCart.Items(0).OfferingID)
        Else
            Course = Nothing
        End If
    End Sub
    Public Overrides Sub ValidateControl()

        If Not IsNothing(fldCriminalConvictionID) Then
            If String.IsNullOrEmpty(fldCriminalConvictionID.Value) And Not (Course.EnrolmentTypeID = 6 Or Course.EnrolmentTypeID = 7) Then
                fldCriminalConvictionIDValidator.ErrorMessage = "<i class=""fa-solid fa-triangle-exclamation""></i> Please state whether you have any criminal convictions"
                fldCriminalConvictionIDValidator.IsValid = False
                fldCriminalConvictionIDValidator.CssClass = "error alert alert-danger"
                fldCriminalConvictionID.CssClass = "ErrorInput"
            End If
        End If

        If Not IsNothing(chkConfirm1) Then
            If Not chkConfirm1.Value = True Then
                chkConfirm1Validator.ErrorMessage = "<i class=""fa-solid fa-triangle-exclamation""></i> Please confirm you agree to observe all TGI Policies and Procedures"
                chkConfirm1Validator.IsValid = False
                chkConfirm1Validator.CssClass = "error alert alert-danger"
                chkConfirm1.CssClass = "ErrorInput"
            End If
        End If


        MyBase.ValidateControl()
    End Sub

    Protected Sub btnBack_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBack.Click
        Dim numQuals = WorkingData.ApplicationRequestQualsOnEntry.Count()
        If Course.EnrolmentTypeID = 6 Or Course.EnrolmentTypeID = 7 Then 'Part-Time Work Related or ACL
            If WorkingData.ApplicationRequestRow.ApplicationUserDefined17 = "EMPLOYER" Then
                Response.Redirect(GetResourceValue("checkout_employment_GCFE"))
            Else
                Response.Redirect(GetResourceValue("checkout_fees_GCFE_aspx"))
            End If
        ElseIf Not IsNothing(Course) And Course.UserDefined23 = "True" Then
                If numQuals > 0 Then
                    Response.Redirect(GetResourceValue("checkout_attachments_GCFE_aspx"))
                Else
                    Response.Redirect(GetResourceValue("checkout_quals_on_entry_GCFE_aspx"))
                End If
                'Course has option to Skip Employment Details Ticked
            Else
                Response.Redirect(GetResourceValue("checkout_employment_GCFE"))
        End If
    End Sub

    Protected Sub btnContinue_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnContinue.Click
        Me.Page.Validate()

        If Me.Page.IsValid Then

            'Set additional fields
            If Course.EnrolmentTypeID = 6 Or Course.EnrolmentTypeID = 7 Then
                WorkingData.ApplicationRequestRow.ApplicationTypeID = 4 '19+ Adult
            Else
                WorkingData.ApplicationRequestRow.ApplicationTypeID = 6 'Apprentice
            End If

            Server.Transfer(GetResourceValue("checkout_makepayment_aspx"))
        Else
            HasFormErrors = True
        End If

    End Sub

End Class
