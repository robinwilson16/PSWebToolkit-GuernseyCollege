Imports Microsoft.VisualBasic
Imports CompassCC.ProSolution.PSWebEnrolmentKit
Imports System.Collections
Imports CompassCC.CCCSystem.CCCCommon
Imports System.Net.Mail
Imports System
Imports System.Text
Imports System.Web

Namespace CustomControl

    Public Class CustomPaymentSubmitter
        Inherits PaymentSubmitter


        Protected Overrides Sub SendEmailConfirmation()

            Dim surname, firstname, email As String

            surname = CCCBlankLibrary.NoBlank(CCCBlankLibrary.NoBlank(WorkingData.EnrolmentRequestRow.Surname, WorkingData.ApplicationRequestRow.Surname), WorkingData.EnquiryRequestRow.Surname)
            firstname = CCCBlankLibrary.NoBlank(CCCBlankLibrary.NoBlank(WorkingData.EnrolmentRequestRow.FirstForename, WorkingData.ApplicationRequestRow.FirstForename), WorkingData.EnquiryRequestRow.FirstForename)
            email = CCCBlankLibrary.NoBlank(CCCBlankLibrary.NoBlank(WorkingData.EnrolmentRequestRow.Email, WorkingData.ApplicationRequestRow.Email), WorkingData.EnquiryRequestRow.EMailAddress)

            If Len(email) = 0 Then Exit Sub

            Try
                Dim WebMail As New SmtpClient()
                Dim msgtext As New StringBuilder

                Dim emailLocation As String
                If CCCBlankLibrary.IsBlank(GetResourceValue("CustomEmailContent")) Then
                    emailLocation = "~/content/email.html"
                Else
                    emailLocation = GetResourceValue("CustomEmailContent")
                End If
                Dim s As String = IO.File.ReadAllText(HttpContext.Current.Server.MapPath(emailLocation))
                s = s.Replace("{{FirstName}}", firstname).Replace("{{Surname}}", surname)

                msgtext.AppendLine(s)


                Dim mailMessage As New MailMessage()
                mailMessage.[To].Add(email)
                mailMessage.Subject = "Subject"
                mailMessage.IsBodyHtml = True
                mailMessage.Body = s
                WebMail.Send(ProcessEmbeddingImages(mailMessage))
            Catch ex As Exception
                CCCApplicationLoggingLibrary.WriteExceptionToEventLog(ex)
            End Try

        End Sub

        Private Shared Function ProcessEmbeddingImages(ByRef oMail As System.Net.Mail.MailMessage) As MailMessage
            Dim oLinkedResources As New Hashtable()
            oMail.Body = PadSrcDataImage(oMail.Body, oLinkedResources)
            If oLinkedResources.Count > 0 Then

                Dim oAlternateView As System.Net.Mail.AlternateView = System.Net.Mail.AlternateView.CreateAlternateViewFromString(oMail.Body, Nothing, "text/html")
                oAlternateView.ContentId = "htmlView"
                oAlternateView.TransferEncoding = Net.Mime.TransferEncoding.SevenBit

                For Each oItem As DictionaryEntry In oLinkedResources
                    Dim oKey As String() = Split(oItem.Key, "-")
                    Dim i As Integer = oKey(0)
                    Dim sExt As String = oKey(1)
                    Dim sData As String = oItem.Value

                    Dim oLinkedResource As New System.Net.Mail.LinkedResource(New IO.MemoryStream(System.Convert.FromBase64String(sData)))
                    oLinkedResource.ContentId = "MyImg" & i
                    oLinkedResource.TransferEncoding = Net.Mime.TransferEncoding.Base64
                    oLinkedResource.ContentType = New System.Net.Mime.ContentType("image/" & sExt)
                    oAlternateView.LinkedResources.Add(oLinkedResource)
                Next

                oMail.AlternateViews.Add(oAlternateView)
            End If

            Return oMail

        End Function

        Private Shared Function PadSrcDataImage(ByVal sHtml As String, ByRef oLinkedResources As Hashtable) As String

            Dim oList As New ArrayList
            Dim sSearch As String = "\ssrc=['""]data:image/(.*?);base64,(.*?)['""]"

            Dim oMatches As System.Text.RegularExpressions.MatchCollection = System.Text.RegularExpressions.Regex.Matches(sHtml, sSearch, System.Text.RegularExpressions.RegexOptions.IgnoreCase)
            For Each m As System.Text.RegularExpressions.Match In oMatches
                If m.Groups.Count >= 2 Then
                    Dim sExt As String = m.Groups(1).Value
                    Dim sData As String = m.Groups(2).Value

                    If sData.Length > 7 AndAlso Right(sData, 6) = "%3D%3D" Then
                        'Replace trailing %3D%3D with ==
                        sData = Left(sData, sData.Length - 6) & "=="
                    End If

                    oLinkedResources.Add(oLinkedResources.Count & "-" & sExt, sData)

                    Dim iPos1 As Integer = m.Groups(1).Index - "data:image/".Length
                    Dim iPos2 As Integer = m.Groups(2).Index + m.Groups(2).Length
                    Dim iPoints As Integer() = {iPos1, iPos2}
                    oList.Add(iPoints)
                End If
            Next

            Dim sRet As new StringBuilder

            If oList.Count = 1 Then 'One img
                Dim iPoints As Integer() = oList(0)
                Dim sStr1 As String = sHtml.Substring(0, iPoints(0))
                Dim sStr2 As String = sHtml.Substring(iPoints(1))
                sRet.Append(sStr1)
                sRet.Append("cid:MyImg0")
                sRet.Append(sStr2)

            ElseIf oList.Count > 1 Then

                For i As Integer = 0 To oList.Count - 1
                    Dim iPoints As Integer() = oList(i)
                    Dim iPos1 As Integer = iPoints(0)

                    If i = 0 Then
                        'First img
                        Dim sStr1 As String = sHtml.Substring(0, iPos1)
                        sRet.Append(sStr1)
                        sRet.Append("cid:MyImg")
                        sRet.Append(i)

                    Else 'Rest imgs
                        Dim iPrevPos2 As Integer = oList(i - 1)(1)
                        Dim sStr1 As String = sHtml.Substring(iPrevPos2, iPos1 - iPrevPos2)
                        sRet.Append(sStr1)
                        sRet.Append("cid:MyImg")
                        sRet.Append(i)

                        If i = oList.Count - 1 Then    'Last
                            sRet.Append(sHtml.Substring(iPoints(1)))
                        End If
                    End If
                Next
            End If

            If sRet.Length > 0 Then
                Return sRet.ToString
            Else
                Return sHtml
            End If
        End Function
    End Class
End Namespace
