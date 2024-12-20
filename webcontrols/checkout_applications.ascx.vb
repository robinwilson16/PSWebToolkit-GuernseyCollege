Option Explicit On
Option Strict On
Imports System.Drawing
Imports CompassCC.CCCSystem.CCCCommon
Imports System.Drawing.Imaging
Imports System.IO
Imports System.Linq
Imports CompassCC.ProSolution.PSWebEnrolmentKit

Partial Class checkout_applications
    Inherits CheckoutBaseControl

    Protected Overrides Sub RenderChildren(writer As HtmlTextWriter)
        MyBase.RenderChildren(writer)
    End Sub

    Public Overrides Sub RenderControl(writer As HtmlTextWriter)
        MyBase.RenderControl(writer)

        Dim ctl As DropDownList = TryCast(fldStudentFirstLanguageID.InternalFieldControl, DropDownList)
        Dim itemsInDropDown = ctl.Items

    End Sub


    Protected Overrides Sub OnLoad(ByVal e As System.EventArgs)
        MyBase.OnLoad(e)

        Dim ctl = fldStudentFirstLanguageID.InternalFieldControl

        If PaymentSubmitter.AllowEmptyBasket And WorkingData.ShoppingCart.Items.Count = 0 Then
            Session("RequestMode") = RequestMode.ApplicationRequest
        End If

        If Not IsPostBack Then

            postcode.Value = WorkingData.EnrolmentRequestRow.PostcodeOut & WorkingData.EnrolmentRequestRow.PostcodeIn
            WorkingData.EnrolmentRequestRow.RestrictedUseAllowContactByEmail = True
        End If
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
            'postcode stuff            
            ''CCCPS-77326: Setting flag to false so Main Address screen opens when coming from this page.
            PSWebEnrolmentProperties.IsCurrentlyTakingAltAddress = False
            If Len(postcode.Value.Trim) > 0 Then
                WorkingData.EnrolmentRequestRow.PostcodeOut = postcode.Value.Trim.Substring(0, postcode.Value.Trim.Length - 3).Trim
                WorkingData.EnrolmentRequestRow.PostcodeIn = Right(postcode.Value.Trim, 3).Trim

                WorkingData.ApplicationRequestRow.PostcodeOut = postcode.Value.Trim.Substring(0, postcode.Value.Trim.Length - 3).Trim
                WorkingData.ApplicationRequestRow.PostcodeIn = Right(postcode.Value.Trim, 3).Trim
            End If

            If FileUpload1.HasFile Then
                'CCCPS-81336 - Feature to reduce the size of image in Photo Control. Have changed control from ASP FileUpload control CCCFileUpload but yes I am not validating this as of now.
                Dim validExtensions As String() = {".jpg", ".jpeg", ".png", ".gif", ".bmp"}
                Dim fileExtension As String = IO.Path.GetExtension(FileUpload1.FileName).ToLower()

                If SystemSettings.UseImageResizeFeature AndAlso validExtensions.Contains(fileExtension) Then
                    Dim originalImage As Image = Image.FromStream(FileUpload1.PostedFile.InputStream)
                    WorkingData.EnrolmentRequestRow.Photo = CCCPageLibrary.GetImageFileBytes(originalImage)
                Else
                    Dim fs As System.IO.Stream = FileUpload1.PostedFile.InputStream()
                    Dim MyData(CInt(fs.Length)) As Byte
                    fs.Read(MyData, 0, CInt(fs.Length))
                    fs.Close()

                    WorkingData.EnrolmentRequestRow.Photo = MyData
                End If
            End If
        End If
    End Sub
End Class
