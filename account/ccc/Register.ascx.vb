Imports CompassCC.ProSolution.PSWebEnrolmentKit
Imports CompassCC.CCCSystem.CCCCommon
Imports System.Data
Imports System.Net.Mail

Partial Class account_ccc_Register
    Inherits webenrolmentcontrolvalidate

    Protected Overrides Sub OnLoad(e As System.EventArgs)
        MyBase.OnLoad(e)
        ''CCCPS-80694: Setting information on password field to guide user to set strong password.
        lblPasswordInfo.Text = PasswordValidation.InfoMessage
    End Sub
    Public Overrides Sub ValidateControl()
        MyBase.ValidateControl()
        ''Moved validations here to isolate it from data saving logic.
        Dim tblWebUser As New WebUserDataTable
        Dim vWebUser As CCCDataViewDataSet = CCCDataViewDataSet.CreateDataView(tblWebUser)
        vWebUser.Columns.AddDBColumns(False, False)
        vWebUser.Filters.SetColumnFilter(tblWebUser.EmailColumn, txtEmail.Text.Trim)
        tblWebUser.TableAdapter.Load(tblWebUser, vWebUser)
        If tblWebUser.Rows.Count = 1 Then
            'User already exists
            Dim valWebUser As New CustomValidator()
            valWebUser.ErrorMessage = "User already exists."
            valWebUser.IsValid = False
            Me.Page.Validators.Add(valWebUser)
        End If

        Dim pcode As String = txtPostcode.Text.Trim
        'postcode stuff            
        If Len(pcode) > 0 Then
            Try
                Dim postcodeOut = Left(pcode.Substring(0, pcode.Length - 3), 4).Trim
                Dim postcodeIn = Right(pcode, 3).Trim
            Catch ex As Exception
                Dim valPostcode As New CustomValidator()
                valPostcode.ErrorMessage = "Please enter a valid postcode."
                valPostcode.IsValid = False
                Me.Page.Validators.Add(valPostcode)
            End Try
        End If
        ''CCCPS-78454: Validating password based on applied policy.
        Dim m_errortext As String = String.Empty
        If Len(txtPassword.Text) > 0 AndAlso Len(txtPassword_Mirror.Text) > 0 Then
            If txtPassword.Text <> txtPassword_Mirror.Text Then
                m_errortext = "Password does not match."
            ElseIf Not PasswordValidation.IsValid(txtPassword.Text) Then
                m_errortext = PasswordValidation.ErrorMessage
            End If
        Else
            m_errortext = "You must enter a 'Password' and 'Retype New Password' to continue."
        End If
        If Len(m_errortext) > 0 Then
            Dim valPassword As New CustomValidator()
            valPassword.ErrorMessage = m_errortext
            valPassword.IsValid = False
            Me.Page.Validators.Add(valPassword)
        End If
    End Sub

    Protected Sub btnRegister_Click(sender As Object, e As EventArgs) Handles btnRegister.Click

        ValidateControl()

        If Me.Page.IsValid Then
            Dim tblWebUser As New WebUserDataTable
            ''CCCPS-80694: Preventing auto-login when email verification is required.
            If Not SystemSettings.ShouldVerifyEmail Then
                tblWebUser = WorkingData.WebUser
            End If
            'Create user
            Dim row As WebUserRow = tblWebUser.NewRowWithDefaults
            row.Email = txtEmail.Text.Trim
            row.Password = CCCAuthenticationLibrary.EncryptPassword(txtPassword.Text)
            row.Surname = txtSurname.Text.Trim
            row.FirstForename = txtFirstName.Text.Trim()
            row.Title = ddlTitle.SelectedValue
            row.Sex = ddlSex.SelectedValue
            If txtDOB.Text IsNot Nothing AndAlso txtDOB.Text.Length > 0 Then
                row.DateOfBirth = txtDOB.Text.Trim
            End If
            row.Address1 = txtAddress1.Text.Trim
            row.Address2 = txtAddress2.Text.Trim
            row.Address3 = txtAddress3.Text.Trim
            row.Address4 = txtAddress4.Text.Trim
            Dim pcode As String = txtPostcode.Text.Trim
            'postcode stuff            
            row.PostcodeOut = Left(pcode.Substring(0, pcode.Length - 3), 4).Trim
            row.PostcodeIn = Right(pcode, 3).Trim

            tblWebUser.Rows.Add(row)

            If SystemSettings.ShouldVerifyEmail Then
                tblWebUser.TableAdapter.Save(tblWebUser)
                GenerateVerificationEmail(row.Email)
            Else
                row.VerificationStatusID = 1
                tblWebUser.TableAdapter.Save(tblWebUser)
                'WorkingData.CurrentLoggedOnUserRow = row
                Response.Redirect(GetResourceValue("AccountCreated"))
            End If
        Else
            Me.ValidationSummary1.ShowSummary = True
        End If
    End Sub


    Private Sub GenerateVerificationEmail(Email As String)
        Try
            CCCCheck.CheckNotNothing(Email, "Email")
            Dim row As WebUserRow = WebUserDataTable.FindUserRow(Email)
            row.VerificationCode = Guid.NewGuid
            row.VerificationStatusID = 0
            row.Table.TableAdapter.Save(row.Table)

            Dim WebMail As New SmtpClient("")
            Dim mailMessage As New MailMessage()
            mailMessage.[To].Add(Email)
            mailMessage.Subject = "Email Verification Requested"
            mailMessage.Body = String.Format("Please click this code to verify your email address: {0}", ResolveUrl(Me.Context.Request.Url.Scheme & "://" & Replace(Context.Request.Url.Authority, "/", "") & "/" & Replace(Me.Context.Request.ApplicationPath, "/", "") & "/webenrolment.aspx?page=~/account/ccc/EmailVerification.ascx&Email=" & Server.UrlEncode(Email) & "&vc=" & Server.UrlEncode(row.VerificationCode.ToString)))
            ''Fix for an error on testing environment.
            Net.ServicePointManager.SecurityProtocol = Net.SecurityProtocolType.Tls12
            WebMail.Send(mailMessage)
        Catch ex As Exception
            Throw New Exception("The email address is invalid or we were unable to send an email at this time. Please check the email address and try to register again.")
        End Try
        Response.Redirect(GetResourceValue("PendingVerification"))
    End Sub
End Class
