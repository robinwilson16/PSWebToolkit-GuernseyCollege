Imports System.Collections.Generic
Imports System.Drawing
Imports System.Drawing.Imaging
Imports System.IO
Imports System.Linq
Imports CompassCC.CCCSystem.CCCCommon
Imports CompassCC.ProSolution.PSWebEnrolmentKit

''' <summary>
''' CCCPS-75837: A sample step to depict integration of new Attachemnt option for user to upload data against Enrolment and Application requests.
''' </summary>
Partial Class webcontrols_checkout_attachments
    Inherits CheckoutBaseControl
    ''' <summary>
    ''' To keep last uploaded file id. This will be needed to delete attachment.
    ''' </summary>
    ''' <returns>_lastAttachmentID</returns>
    Private Property _lastAttachmentID() As Integer
        Get
            Return CInt(HttpContext.Current.Session("LastAttachmentID"))
        End Get
        Set(ByVal value As Integer)
            HttpContext.Current.Session("LastAttachmentID") = value
        End Set
    End Property
    Protected Overrides Sub OnLoad(e As EventArgs)
        MyBase.OnLoad(e)

        'Clear class attributes first
        Me.divMain.Attributes("class") = ""

        'Breadcrumbs
        If WorkingData.ShoppingCart.ContainsItemsOfType("Application") AndAlso WorkingData.ShoppingCart.ContainsItemsOfType("Enrolment") Then
            breadcrumbenrols.Visible = True
            Me.divMain.Attributes("class") = "panel panel-danger"
        ElseIf WorkingData.ShoppingCart.ContainsItemsOfType("Application") Then
            breadcrumbenrols.Visible = False
            Me.divMain.Attributes("class") = "panel panel-success"
        ElseIf WorkingData.ShoppingCart.ContainsItemsOfType("Enrolment") Then
            breadcrumbenrols.Visible = True
            Me.divMain.Attributes("class") = "panel panel-danger"
        ElseIf WorkingData.ShoppingCart.ContainsItemsOfType("Enquiry") Then
            breadcrumbenrols.Visible = False
            Me.divMain.Attributes("class") = "panel panel-warning"
        Else
            breadcrumbenrols.Visible = False
            Me.divMain.Attributes("class") = "panel panel-danger"
        End If
        If Not IsPostBack Then
            loadAttachments()
        End If
    End Sub
    ''' <summary>
    ''' To remove attachment on click of remove button against particular attachment.
    ''' </summary>
    ''' <param name="source">source</param>
    ''' <param name="e">e</param>
    Protected Sub rptAttachments_ItemCommand(source As Object, e As RepeaterCommandEventArgs)
        If (e.CommandName = "RemoveAttachment") Then
            Dim attachmentID = CCCDataTypeConverter.Convert(Of Integer)(e.CommandArgument)
            Dim rowToDelete = WorkingData.EnrolmentRequestAttachments.FindByAttachmentID(attachmentID)
            If (rowToDelete IsNot Nothing) Then
                WorkingData.EnrolmentRequestAttachments.RemoveRow(rowToDelete)
                loadAttachments()
            End If
        End If
    End Sub

    ''' <summary>
    ''' Save attachment in in-memory data table which we will be using to save in final step of the wizard.
    ''' </summary>
    ''' <param name="sender">sender</param>
    ''' <param name="e">e</param>
    Private Sub btnUpload_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUpload.Click
        Me.Page.Validate()
        ''Following line is important to perform validations on File Upload control based on setup done on control.
        Me.fuAttachment.ValidateFile()

        If Not Me.Page.IsValid Then
            Return
        End If

        Dim rowAttachment = WorkingData.EnrolmentRequestAttachments.NewRow
        _lastAttachmentID -= 1

        'CCCPS-81189 - Image resizer logic should only checked for images not for any other type of documents. If attachment is of image type extension then only Image Resizer logic will checked.
        Dim extensions As String() = SystemSettings.SupportedFileTypes.Split(","c)
        Dim imageExtensions As List(Of String) = extensions.Select(Function(extension) extension.Trim()) _
                                                  .Where(Function(trimmedExtension) CCCPageLibrary.IsImageExtension(trimmedExtension)) _
                                                  .ToList()
        Dim isValidImageExtension As Boolean = imageExtensions.Contains(IO.Path.GetExtension(fuAttachment.FileName).ToLower())

        'CCCPS-81189 - Call this logic only when UseImageResizeFeature is true and a valid image extension.
        If SystemSettings.UseImageResizeFeature AndAlso fuAttachment.HasFile AndAlso isValidImageExtension Then
            Dim originalImage As Image = Image.FromStream(fuAttachment.PostedFile.InputStream)
            Dim FileBytes As Byte() = CCCPageLibrary.GetImageFileBytes(originalImage)
            With rowAttachment
                .AttachmentID = _lastAttachmentID
                .TypeOfEvidence = ddlTypeOfEvidence.SelectedValue
                .Notes = txtNotes.Text.Trim
                .Attachment = FileBytes
                .FileName = fuAttachment.FileName
                .Size = FileBytes.Length
                If CCCAttachmentThumbnailGenerator.FilenameIndicatesFileIsCompatibleImage(fuAttachment.FileName) Then
                    Try
                        .ImageThumb = CCCAttachmentThumbnailGenerator.CreateThumbnailAsByteArray(.Attachment, 96)
                    Catch ex As Exception
                        ' Cannot create thumbnail - ignore error
                        .ImageThumb = Nothing ' No thumbnail
                    End Try
                End If

                .CreatedDate = DateTime.Now
            End With
        Else
            With rowAttachment
                .AttachmentID = _lastAttachmentID
                .TypeOfEvidence = ddlTypeOfEvidence.SelectedValue
                .Notes = txtNotes.Text.Trim
                .Attachment = fuAttachment.FileBytes
                .FileName = fuAttachment.FileName
                .Size = fuAttachment.FileBytes.Length
                If CCCAttachmentThumbnailGenerator.FilenameIndicatesFileIsCompatibleImage(fuAttachment.FileName) Then
                    Try
                        .ImageThumb = CCCAttachmentThumbnailGenerator.CreateThumbnailAsByteArray(.Attachment, 96)
                    Catch ex As Exception
                        ' Cannot create thumb nail- ignore error
                        .ImageThumb = Nothing ' No thumb-nail
                    End Try
                End If
                .CreatedDate = DateTime.Now
            End With
        End If
        WorkingData.EnrolmentRequestAttachments.AddRow(rowAttachment)
        ''Redirecting to self instead of just resetting controls to avoid issue of attachment being added on browser refresh.
        Response.Redirect(GetResourceValue("checkout_attachments_aspx"))
    End Sub

    Private Sub btnContinue_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnContinue.Click
        If WorkingData.ShoppingCart.ContainsItemsOfType("Application") AndAlso WorkingData.ShoppingCart.ContainsItemsOfType("Enrolment") Then
            Response.Redirect(GetResourceValue("checkout_dataprotection_aspx"))
        ElseIf WorkingData.ShoppingCart.ContainsItemsOfType("Application") Then
            Response.Redirect(GetResourceValue("checkout_dataprotection_apponly_aspx"))
        ElseIf WorkingData.ShoppingCart.ContainsItemsOfType("Enrolment") Then
            Response.Redirect(GetResourceValue("checkout_dataprotection_aspx"))
        ElseIf WorkingData.ShoppingCart.ContainsItemsOfType("Enquiry") Then
            Response.Redirect(GetResourceValue("checkout_dataprotection_enquiry"))
        End If
    End Sub
    ''' <summary>
    ''' Binding Attachment table to repeater to display already uploaded attachments to user.
    ''' </summary>
    Private Sub loadAttachments()
        rptAttachments.DataSource = WorkingData.EnrolmentRequestAttachments
        rptAttachments.DataBind()
        If (WorkingData.EnrolmentRequestAttachments.Rows.Count = 0) Then
            trNoItems.Visible = True
        Else
            trNoItems.Visible = False
        End If
    End Sub

End Class
