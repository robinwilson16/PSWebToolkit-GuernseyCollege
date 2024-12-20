Option Explicit On
Option Strict On

Imports CompassCC.ProSolution.PSWebEnrolmentKit


Partial Class webcontrols_checkout_enrolments
    Inherits CheckoutBaseControl

    Protected Overrides Sub OnLoad(e As EventArgs)

        If PaymentSubmitter.AllowEmptyBasket And WorkingData.ShoppingCart.Items.Count = 0 Then
            Session("RequestMode") = RequestMode.EnrolmentRequest
        End If


        If Not IsPostBack Then
            postcode.Value = WorkingData.EnrolmentRequestRow.PostcodeOut & WorkingData.EnrolmentRequestRow.PostcodeIn
            ''CCCPS-80557: Need to set Radio button manually as it's not bound to any field type.
            rdoLearnDiff.SelectedValue = WorkingData.EnrolmentRequestRow.LearningDiffOrDisID
        End If
        MyBase.OnLoad(e)
    End Sub
    Public Overrides Sub ValidateControl()

        Dim regexPostCode As New Regex("^([Gg][Ii][Rr] 0[Aa]{2})|((([A-Za-z][0-9]{1,2})|(([A-Za-z][A-Ha-hJ-Yj-y][0-9]{1,2})|(([A-Za-z][0-9][A-Za-z])|([A-Za-z][A-Ha-hJ-Yj-y][0-9][A-Za-z]?))))\s?[0-9][A-Za-z]{2})$")
        Dim match As Match = regexPostCode.Match(postcode.Value)
        If Not match.Success Then
            Dim v As New CustomValidator
            v.ErrorMessage = "Please enter a valid Postcode"
            v.IsValid = False
            Me.Page.Validators.Add(v)
        End If

        MyBase.ValidateControl()
    End Sub
    Private Sub btnContinue_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnContinue.Click

        Me.Page.Validate()

        If Me.Page.IsValid Then
            ''CCCPS-77326: Setting flag to false so Main Address screen opens when coming from this page.
            PSWebEnrolmentProperties.IsCurrentlyTakingAltAddress = False
            WorkingData.EnrolmentRequestRow.StudentSignature = StudentSignature.Signature
            WorkingData.EnrolmentRequestRow.LearningDiffOrDisID = rdoLearnDiff.SelectedValue
            ''CCCPS-80557: Clearing selection to remove previous values if user has opted NO/Rather Not to Say at the end.
            If rdoLearnDiff.SelectedValue <> "1" Then
                WorkingData.EnrolmentRequestRow.DisabilityCategory1ID = Nothing
                WorkingData.EnrolmentRequestRow.DisabilityCategory2ID = Nothing
            End If

            'postcode stuff            
            If Len(postcode.Value) > 0 Then
                WorkingData.EnrolmentRequestRow.PostcodeOut = postcode.Value.Substring(0, postcode.Value.Length - 3).Trim
                WorkingData.EnrolmentRequestRow.PostcodeIn = Right(postcode.Value, 3).Trim
            End If

        End If

    End Sub




End Class
