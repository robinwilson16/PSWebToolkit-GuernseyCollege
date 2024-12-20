Imports CompassCC.ProSolution.PSWebEnrolmentKit
Imports CompassCC.CCCSystem.CCCCommon

Partial Class account_ccc_Login
    Inherits webenrolmentcontrolvalidate

    Private m_errortext As String

    Protected Sub btnLogin_Click(sender As Object, e As EventArgs) Handles btnLogin.Click
        m_errortext = ""
        ValidateControl()

        If Me.Page.IsValid Then
            Dim tblWebUser As WebUserDataTable = WorkingData.WebUser
            Dim vWebUser As CCCDataViewDataSet = CCCDataViewDataSet.CreateDataView(tblWebUser)
            vWebUser.Columns.AddDBColumns(False, False)
            vWebUser.Filters.SetColumnFilter(tblWebUser.EmailColumn, Request.Form("pre[email]"))
            tblWebUser.TableAdapter.Load(tblWebUser, vWebUser)

            If tblWebUser.Rows.Count = 1 Then
                'User already exists - log in
                Dim rowWebUser As WebUserRow = tblWebUser(0)
                If rowWebUser.VerificationStatusID = 0 Then
                    tblWebUser.Clear()
                    m_errortext = "Account verification pending, please check your Email for the verification link we sent."
                    lnkReSend.Visible = True
                ElseIf CCCAuthenticationLibrary.DoesSpecifiedPasswordMatchEncryptedPassword(Request.Form("password"), rowWebUser.Password) Then
                    ''CCCPS-75553: Locking logic should be applied only if IsLockingEnabledOnInvalidPasswordAttempts flag is True.
                    If SystemSettings.IsLockingEnabledOnInvalidPasswordAttempts AndAlso rowWebUser.IsLockedOut Then
                        ''CCCPS-75553: Check if lock time has already passed the specified period. If it is then unlock the user And allow normal access.
                        If (DateTime.Now > Convert.ToDateTime(rowWebUser.LockedDateTime).AddMinutes(SystemSettings.AutoUnlockUserAfter)) Then
                            ''Reset all the fields to unlock the user.
                            rowWebUser.IsLockedOut = False
                            rowWebUser.LockedDateTime = Nothing
                            rowWebUser.AttemptCount = 0
                            tblWebUser.TableAdapter.Save(tblWebUser)
                            ''CCCPS-77638: Resetting session ID on successful login.
                            WorkingData.RegenerateSessionID()
                            Response.Redirect(GetResourceValue("UserAccount"))
                        Else
                            m_errortext = "Your account has been locked. Please contact us to unlock your account."
                        End If
                    Else
                        ''CCCPS-77638: Resetting session ID on successful login.
                        WorkingData.RegenerateSessionID()
                        ' WorkingData.CurrentLoggedOnUserRow = row
                        Response.Redirect(GetResourceValue("UserAccount"))
                    End If
                Else
                    ''CCCPS-75553: Apply locking logic only if below flag is set to true.
                    If SystemSettings.IsLockingEnabledOnInvalidPasswordAttempts Then
                        ''CCCPS-75553: Increase the attempt counter if invalid password attemped. Lock the user when number of allowed attempts are bypassed.
                        rowWebUser.AttemptCount = rowWebUser.AttemptCount + 1
                        If rowWebUser.AttemptCount = SystemSettings.AllowedInvalidPasswordAttempts Then
                            m_errortext = "Your account has been locked due to " & SystemSettings.AllowedInvalidPasswordAttempts & " unsuccessful attempts. Please contact us to unlock your account."
                            rowWebUser.IsLockedOut = True
                            rowWebUser.LockedDateTime = DateTime.Now
                        ElseIf rowWebUser.AttemptCount > SystemSettings.AllowedInvalidPasswordAttempts Then
                            m_errortext = "Your account has been locked. Please contact us to unlock your account."
                            rowWebUser.LockedDateTime = DateTime.Now
                        Else
                            m_errortext = "Invalid Email or Password." 'CCCPS-78453 security issue 
                        End If
                        tblWebUser.TableAdapter.Save(tblWebUser)
                    Else
                        m_errortext = "Invalid Email or Password" 'CCCPS-78453 security issue 
                    End If
                    'password incorrect
                    tblWebUser.Clear()
                End If
            Else
                ''Had to clear here because it was causing an issue when try to login with different user back to back.
                tblWebUser.Clear()
                m_errortext = "Invalid Email or Password." 'CCCPS-78453 security issue 
            End If
        End If

    End Sub

    Protected Overrides Sub OnLoad(e As EventArgs)
        errtext.InnerText = m_errortext
    End Sub

    Protected Overrides Sub OnPreRender(e As EventArgs)
        MyBase.OnPreRender(e)
        errtext.InnerText = m_errortext
    End Sub
End Class
