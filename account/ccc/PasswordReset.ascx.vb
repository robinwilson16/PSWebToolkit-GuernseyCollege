Imports CompassCC.ProSolution.PSWebEnrolmentKit
Imports CompassCC.CCCSystem.CCCCommon
Imports System.Net.Mail


Partial Class account_ccc_PasswordReset
    Inherits webenrolmentcontrolvalidate

    Protected Sub btnResetPassword_Click(sender As Object, e As EventArgs) Handles btnResetPassword.Click

        CCCCheck.CheckNotNothing(Request.Form("pre[email]"), "Email")
        Dim row As WebUserRow = WebUserDataTable.FindUserRow(Request.Form("pre[email]"))

        If row IsNot Nothing Then
            If row.VerificationStatusID = 0 Then
                Dim v As New CustomValidator()
                v.ErrorMessage = "Account not verified. We have re-sent you a verification code, please check your email."
                v.IsValid = False
                Me.Page.Validators.Add(v)
                Me.ValidationSummary1.ShowSummary = True

                ResendVerificationCode()

                Exit Sub
            End If

            row.VerificationCode = Guid.NewGuid
            row.VerificationStatusID = 2
            row.Table.TableAdapter.Save(row.Table)

            Dim WebMail As New SmtpClient("")
            Dim mailMessage As New MailMessage()
            mailMessage.[To].Add(Request.Form("pre[email]"))

            mailMessage.Subject = "Password Reset Requested"
            mailMessage.Body = String.Format("Please click this code to reset your password: {0}", ResolveUrl(Me.Context.Request.Url.Scheme & "://" & Replace(Context.Request.Url.Authority, "/", "") & "/" & Replace(Me.Context.Request.ApplicationPath, "/", "") & "/webenrolment.aspx?page=~/account/ccc/DoPasswordReset.ascx&Email=" & Request.Form("pre[email]") & "&vc=" & row.VerificationCode.ToString))
            ''Fix for an error on testing environment.
            Net.ServicePointManager.SecurityProtocol = Net.SecurityProtocolType.Tls12
            WebMail.Send(mailMessage)

            altSuccess.Visible = True
            btnResetPassword.Enabled = False
        Else
            Dim v As New CustomValidator()
            v.ErrorMessage = "You will receive an email containing a link to reset your password if the provided email address is valid." 'CCCPS-78453 security issue 
            v.IsValid = False
            Me.Page.Validators.Add(v)
            Me.ValidationSummary1.ShowSummary = True
        End If
    End Sub


    Private Sub ResendVerificationCode()
        CCCCheck.CheckNotNothing(Request.Form("pre[email]"), "Email")
        Dim row As WebUserRow = WebUserDataTable.FindUserRow(Request.Form("pre[email]"))

        If row IsNot Nothing Then
            If row.VerificationStatusID = 1 Then
                Dim v As New CustomValidator()
                v.ErrorMessage = "Account already verified, please log in to continue."
                v.IsValid = False
                Me.Page.Validators.Add(v)
                Me.ValidationSummary1.ShowSummary = True

                Exit Sub
            End If

            row.VerificationCode = Guid.NewGuid
            row.VerificationStatusID = 0
            row.Table.TableAdapter.Save(row.Table)

            Dim WebMail As New SmtpClient("")
            Dim mailMessage As New MailMessage()
            mailMessage.[To].Add(Request.Form("pre[email]"))

            mailMessage.Subject = "Email Verification Requested"
            mailMessage.Body = String.Format("Please click this code to verify your email address: {0}", ResolveUrl(Me.Context.Request.Url.Scheme & "://" & Replace(Context.Request.Url.Authority, "/", "") & "/" & Replace(Me.Context.Request.ApplicationPath, "/", "") & "/webenrolment.aspx?page=~/account/ccc/EmailVerification.ascx&Email=" & Request.Form("pre[email]") & "&vc=" & row.VerificationCode.ToString))
            Net.ServicePointManager.SecurityProtocol = Net.SecurityProtocolType.Tls12
            WebMail.Send(mailMessage)
        Else
            Dim v As New CustomValidator()
            v.ErrorMessage = "You will receive an email containing a link to reset your password if the provided email address is valid." 'CCCPS-78453 security issue 
            v.IsValid = False
            Me.Page.Validators.Add(v)
            Me.ValidationSummary1.ShowSummary = True
        End If
    End Sub

End Class
