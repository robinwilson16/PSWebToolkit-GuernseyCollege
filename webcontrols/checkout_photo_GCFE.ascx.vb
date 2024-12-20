Option Explicit On
Option Strict On

Imports System.Drawing
Imports CompassCC.CCCSystem.CCCCommon
Imports System.Drawing.Imaging
Imports System.IO
Imports System.Linq
Imports CompassCC.ProSolution.PSWebEnrolmentKit


Partial Class checkout_photo_GCFE
    Inherits CheckoutBaseControl

    Protected Overrides Sub RenderChildren(writer As HtmlTextWriter)
        MyBase.RenderChildren(writer)
    End Sub

    Public Overrides Sub RenderControl(writer As HtmlTextWriter)
        MyBase.RenderControl(writer)

        'Dim ctl As DropDownList = TryCast(fldStudentFirstLanguageID.InternalFieldControl, DropDownList)
        'Dim itemsInDropDown = ctl.Items

    End Sub

    Public HasFormErrors As Boolean = False
    Public Course As OfferingRow
    Public IsPhotoRequired As Boolean
    Public IsPhotoValidExtension As Boolean = True

    Protected Overrides Sub OnLoad(ByVal e As System.EventArgs)
        MyBase.OnLoad(e)

        If Not IsNothing(WorkingData.ShoppingCart.Items(0)) Then
            Course = OfferingDataTable.FetchByOfferingID(WorkingData.ShoppingCart.Items(0).OfferingID)
        Else
            Course = Nothing
        End If

        'Make Email Not Mandatory For Non-Apprenticeship Courses
        If Course.EnrolmentTypeID = 6 Or Course.EnrolmentTypeID = 7 Then 'Part-Time Work Related or ACL
            IsPhotoRequired = False
        Else
            IsPhotoRequired = True
        End If

        If Not WorkingData.EnrolmentRequestRow.Photo Is Nothing Then
            Dim base64String As String = Convert.ToBase64String(WorkingData.EnrolmentRequestRow.Photo, 0, WorkingData.EnrolmentRequestRow.Photo.Length)
            Image1.ImageUrl = "data:image/png;base64," & base64String
        Else
            Image1.ImageUrl = "/PSWebEnrolment/images/BlankPhoto.png"
        End If

        If Not IsPostBack Then
            Session("Image") = Nothing
        Else
            Session("Image") = FileUpload1.PostedFile

        End If
    End Sub

    Protected Sub Upload(ByVal sender As Object, ByVal e As EventArgs)
        Dim validExtensions As String() = {".jpg", ".jpeg", ".png", ".gif", ".bmp", ".heic"}

        Page.MaintainScrollPositionOnPostBack = True
        Session("Image") = FileUpload1.PostedFile
        Dim fs As Stream = FileUpload1.PostedFile.InputStream()
        Dim br As BinaryReader = New BinaryReader(fs)
        Dim bytes As Byte() = br.ReadBytes(CInt(fs.Length))
        Dim base64String As String = Convert.ToBase64String(bytes, 0, bytes.Length)
        Dim imgPath As String = "data:image/png;base64," & base64String
        Dim fileName As String = FileUpload1.PostedFile.FileName
        PhotoFilename.Text = fileName

        'Panel1.Visible = True
        'Response.BinaryWrite(bytes)

        'Check Image is Valid
        If Not IsNothing(fileName) Then
            If String.IsNullOrEmpty(fileName) And WorkingData.EnrolmentRequestRow.Photo Is Nothing Then
                PhotoPathValidator.ErrorMessage = "Please click Choose File above first to browse your device for the photo you wish to use. If you cannot provide evidence at present, please instead select a reason why below."
                PhotoPathValidator.IsValid = False
                PhotoPathValidator.CssClass = "error alert alert-danger"
                FileUpload1.Attributes.Add("Class", "textfield form-control ErrorInput")
            ElseIf fileName.LastIndexOf(".") <= 0 Then
                PhotoPathValidator.ErrorMessage = "This type of file is not valid. Please upload a valid image file"
                PhotoPathValidator.IsValid = False
                PhotoPathValidator.CssClass = "error alert alert-danger"
                FileUpload1.Attributes.Add("Class", "textfield form-control ErrorInput")
            ElseIf validExtensions.Contains(fileName.Substring(fileName.LastIndexOf(".")).ToLower) = False Then
                PhotoPathValidator.ErrorMessage = "This type of file is not valid. Please upload a valid image file"
                PhotoPathValidator.IsValid = False
                PhotoPathValidator.CssClass = "error alert alert-danger"
                FileUpload1.Attributes.Add("Class", "textfield form-control ErrorInput")
            ElseIf FileUpload1.FileBytes.Length > 5 * 1000 * 1000 Then
                PhotoPathValidator.ErrorMessage = "This file is " + Math.Round((FileUpload1.FileBytes.Length / 1000 / 1000), 2).ToString + "MB which is too large as the maximum permitted file size is 5MB. Please choose a smaller file."
                PhotoPathValidator.IsValid = False
                PhotoPathValidator.CssClass = "error alert alert-danger"
                FileUpload1.Attributes.Add("Class", "textfield form-control ErrorInput")
            Else
                Image1.ImageUrl = imgPath
                WorkingData.EnrolmentRequestRow.Photo = bytes
            End If
        End If
    End Sub

    Protected Sub Save(ByVal sender As Object, ByVal e As EventArgs)

        Page.MaintainScrollPositionOnPostBack = True
        'Panel1.Visible = False
        'Panel2.Visible = True

    End Sub

    Protected Sub Cancel(ByVal sender As Object, ByVal e As EventArgs)
        Page.MaintainScrollPositionOnPostBack = True
        Session("Image") = Nothing
        WorkingData.EnrolmentRequestRow.Photo = Nothing
        Response.Redirect(Request.Url.AbsoluteUri)

    End Sub

    Public Overrides Sub ValidateControl()
        Dim validExtensions As String() = {".jpg", ".jpeg", ".png", ".gif", ".bmp"}

        If Not IsNothing(PhotoPath) And WorkingData.EnrolmentRequestRow.Photo Is Nothing Then
            If String.IsNullOrEmpty(PhotoPath.Value) And IsPhotoRequired = True Then
                PhotoPathValidator.ErrorMessage = "<i class=""fa-solid fa-triangle-exclamation""></i> Please upload your photo by clicking on Choose File"
                PhotoPathValidator.IsValid = False
                PhotoPathValidator.CssClass = "error alert alert-danger"
                FileUpload1.Attributes.Add("Class", "textfield form-control ErrorInput")
            End If
            If PhotoPath.Value.Length = 0 Then
                'Do Nothing
            ElseIf PhotoPath.Value.LastIndexOf(".") <= 0 Then
                PhotoPathValidator.ErrorMessage = "<i class=""fa-solid fa-triangle-exclamation""></i> This type of file is not valid. Please upload a valid image file"
                PhotoPathValidator.IsValid = False
                PhotoPathValidator.CssClass = "error alert alert-danger"
                FileUpload1.Attributes.Add("Class", "textfield form-control ErrorInput")
            ElseIf validExtensions.Contains(PhotoPath.Value.Substring(PhotoPath.Value.LastIndexOf("."))) = False Then
                PhotoPathValidator.ErrorMessage = "<i class=""fa-solid fa-triangle-exclamation""></i> This type of file is not valid. Please upload a valid image file"
                PhotoPathValidator.IsValid = False
                PhotoPathValidator.CssClass = "error alert alert-danger"
                FileUpload1.Attributes.Add("Class", "textfield form-control ErrorInput")
            End If
        End If

        MyBase.ValidateControl()
    End Sub


    Private Sub btnContinue_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnContinue.Click

        Me.Page.Validate()

        If Me.Page.IsValid Then

            If FileUpload1.HasFile Then
                'CCCPS-81336 - Feature to reduce the size of image in Photo Control. Have changed control from ASP FileUpload control CCCFileUpload but yes I am Not validating this as of now.
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
        Else
            HasFormErrors = True
        End If
    End Sub
End Class
