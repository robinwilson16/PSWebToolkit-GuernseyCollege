Imports CompassCC.ProSolution.PSWebEnrolmentKit
Imports CompassCC.CCCSystem.CCCCommon


Partial Class account_ccc_DoPasswordReset
    Inherits webenrolmentcontrolvalidate

    Property Email As String
    Property Verification As String


    Protected Overrides Sub OnLoad(e As EventArgs)
        MyBase.OnLoad(e)

        Email = Request("Email")
        Verification = Request("vc")
        ''CCCPS-80694: Setting information on password field to guide user to set strong password.
        lblPasswordInfo.Text = PasswordValidation.InfoMessage

        Dim result As VerficationResult = WebUserDataTable.CheckVerificationCodeIsValid(Email, Verification)

        Select Case result
            Case VerficationResult.Failed
                Dim v As New CustomValidator()
                v.ErrorMessage = "Verification Code does not match. Please check and try again."
                v.IsValid = False
                Me.Page.Validators.Add(v)
                Me.ValidationSummary1.ShowSummary = True
                btnUpdate.Enabled = False
            Case VerficationResult.Expired
                btnUpdate.Enabled = True
            Case VerficationResult.UserNotFound
                Dim v As New CustomValidator()
                v.ErrorMessage = "User not found. Please try to register again."
                v.IsValid = False
                Me.Page.Validators.Add(v)
                Me.ValidationSummary1.ShowSummary = True
                btnUpdate.Enabled = False
        End Select



    End Sub


    Public Overrides Sub ValidateControl()
        MyBase.ValidateControl()
        ''CCCPS-78454: Validating password based on applied policy.
        Dim m_errortext As String = String.Empty
        If Len(Request.Form("passwordnew")) > 0 AndAlso Len(Request.Form("password_mirror")) > 0 Then
            If Request.Form("passwordnew") <> Request.Form("password_mirror") Then
                m_errortext = "Password does not match"
            ElseIf Not PasswordValidation.IsValid(Request.Form("passwordnew")) Then
                m_errortext = PasswordValidation.ErrorMessage
            End If
        Else
            m_errortext = "You must enter a 'New Password' and 'Retype New Password' to update the password."
        End If

        If Len(m_errortext) > 0 Then
            Dim v As New CustomValidator()
            v.ErrorMessage = m_errortext
            v.IsValid = False
            Me.Page.Validators.Add(v)
            Me.ValidationSummary1.ShowSummary = True
        End If
    End Sub
    Protected Sub btnUpdate_Click(sender As Object, e As EventArgs) Handles btnUpdate.Click
        ValidateControl()

        If Me.Page.IsValid Then
            Dim tblWebUser As WebUserDataTable = WorkingData.WebUser
            Dim v As CCCDataViewDataSet = CCCDataViewDataSet.CreateDataView(tblWebUser)
            v.Columns.AddDBColumns(False, False)
            v.Filters.SetColumnFilter(tblWebUser.EmailColumn, Email)
            tblWebUser.TableAdapter.Load(tblWebUser, v)

            If tblWebUser.Rows.Count = 1 Then
                Dim DataRow As WebUserRow = tblWebUser.Rows(0)
                DataRow.Password = CCCAuthenticationLibrary.EncryptPassword(Request.Form("passwordnew"))

                tblWebUser.TableAdapter.Save(tblWebUser)
                'WorkingData.CurrentLoggedOnUserRow = DataRow
                Response.Redirect(GetResourceValue("UserAccount"))
            End If

        End If
    End Sub
End Class
