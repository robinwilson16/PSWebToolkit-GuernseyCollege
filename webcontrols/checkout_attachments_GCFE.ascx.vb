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
Partial Class webcontrols_checkout_attachments_GCFE
    Inherits CheckoutBaseControl

    Public OfferingID As Integer
    Public IsAttachmentRequired As Boolean = True
    Public UploadButtonClicked As Boolean = False
    Public HasSelectedEvidenceType As Boolean = True
    Public FileIsValid As Boolean = False

    Public Is19Plus As Boolean
    Public Date31stAug As Date
    Public DateIsAdult As Date

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

    Public HasFormErrors As Boolean = False

    Public Course As OfferingRow

    Protected Overrides Sub OnLoad(e As EventArgs)
        MyBase.OnLoad(e)

        If Not IsNothing(WorkingData.ShoppingCart.Items(0)) Then
            Course = OfferingDataTable.FetchByOfferingID(WorkingData.ShoppingCart.Items(0).OfferingID)
        Else
            Course = Nothing
        End If

        'Check age to see if 19+ fields should be displayed
        Date31stAug = CDate(Today().Year & "-08-31")
        DateIsAdult = Date31stAug.AddYears(-19)

        If WorkingData.ApplicationRequestRow.DateOfBirth < DateIsAdult Then
            Is19Plus = True
        Else
            Is19Plus = False
        End If

        'Clear class attributes first
        Me.divMain.Attributes("class") = ""
        If Not IsPostBack Then
            loadAttachments()
        End If
    End Sub

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

    Protected Overrides Sub CreateChildControls()
        MyBase.CreateChildControls()

        Dim evidenceOfCertificates As New ListItem("Certificates", "Certificates")
        Dim evidenceOfIdentity As New ListItem("Proof Of Identity", "Proof Of Identity")
        Dim evidenceOfLowWage As New ListItem("Evidence Of Low Wage", "Evidence Of Low Wage")
        Dim evidenceOfBenefits As New ListItem("Proof Of Benefits", "Proof Of Benefits")
        Dim evidenceOfDrivingLicence As New ListItem("Driving Licence", "Driving Licence")
        Dim evidenceOfPassport As New ListItem("Passport", "Passport")
        Dim evidenceOfUtilityBill As New ListItem("Utility Bill", "Utility Bill")
        Dim evidenceOfBankStatement As New ListItem("Bank Statement", "Bank Statement")
        'Driving Licence/Passport/Utility Bill/Bank Statement/Benefit statement
        ddlTypeOfEvidence.Items.Add(evidenceOfCertificates)
        ddlTypeOfEvidence.Items.Add(evidenceOfIdentity)

        If Is19Plus = True Then
            ddlTypeOfEvidence.Items.Add(evidenceOfLowWage)
            ddlTypeOfEvidence.Items.Add(evidenceOfBenefits)
            ddlTypeOfEvidence.Items.Add(evidenceOfDrivingLicence)
            ddlTypeOfEvidence.Items.Add(evidenceOfPassport)
            ddlTypeOfEvidence.Items.Add(evidenceOfUtilityBill)
            ddlTypeOfEvidence.Items.Add(evidenceOfBankStatement)
        End If
    End Sub

    ''' <summary>
    ''' Save attachment in in-memory data table which we will be using to save in final step of the wizard.
    ''' </summary>
    ''' <param name="sender">sender</param>
    ''' <param name="e">e</param>
    Private Sub btnUpload_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUpload.Click
        Dim validExtensions As String() = {".jpg", ".jpeg", ".png", ".gif", ".bmp", ".heic", ".pdf", ".doc", ".docx"}

        FileChosen.Text = "N" 'Reset back to No
        UploadButtonClicked = True


        If HasSelectedEvidenceType = True Then
            'Me.fuAttachment.ValidateFile()

            Dim rowAttachment = WorkingData.EnrolmentRequestAttachments.NewRow
            _lastAttachmentID -= 1
            With rowAttachment

                Dim ValidatorAppend As String = .TypeOfEvidence
                .AttachmentID = _lastAttachmentID
                .TypeOfEvidence = ddlTypeOfEvidence.SelectedValue
                .Notes = txtNotes.Text.Trim
                .Attachment = fuAttachment.FileBytes
                .FileName = fuAttachment.FileName
                .Size = fuAttachment.FileBytes.Length
                hiddenvalidator.Value = (hiddenvalidator.Value + ValidatorAppend)
                ''test.Text = (ddlTypeOfEvidence.SelectedValue)

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

            'Check Image is Valid
            If Not IsNothing(rowAttachment.FileName) Then
                If String.IsNullOrEmpty(rowAttachment.FileName) Then
                    ddlTypeOfEvidenceValidator.ErrorMessage = "Please upload your attachment by clicking on Choose File"
                    ddlTypeOfEvidenceValidator.IsValid = False
                    ddlTypeOfEvidenceValidator.CssClass = "error alert alert-danger"
                    fuAttachment.Attributes.Add("Class", "textfield form-control ErrorInput")
                ElseIf rowAttachment.FileName.LastIndexOf(".") <= 0 Then
                    ddlTypeOfEvidenceValidator.ErrorMessage = "This type of file is not valid. Please upload a valid image file"
                    ddlTypeOfEvidenceValidator.IsValid = False
                    ddlTypeOfEvidenceValidator.CssClass = "error alert alert-danger"
                    fuAttachment.Attributes.Add("Class", "textfield form-control ErrorInput")
                ElseIf validExtensions.Contains(rowAttachment.FileName.Substring(rowAttachment.FileName.LastIndexOf(".")).ToLower) = False Then
                    ddlTypeOfEvidenceValidator.ErrorMessage = "This type of file is not valid. Please upload a valid image file"
                    ddlTypeOfEvidenceValidator.IsValid = False
                    ddlTypeOfEvidenceValidator.CssClass = "error alert alert-danger"
                    fuAttachment.Attributes.Add("Class", "textfield form-control ErrorInput")
                ElseIf fuAttachment.FileBytes.Length > 5 * 1000 * 1000 Then
                    ddlTypeOfEvidenceValidator.ErrorMessage = "This file is " + Math.Round((fuAttachment.FileBytes.Length / 1000 / 1000), 2).ToString + "MB which is too large as the maximum permitted file size is 5MB. Please choose a smaller file."
                    ddlTypeOfEvidenceValidator.IsValid = False
                    ddlTypeOfEvidenceValidator.CssClass = "error alert alert-danger"
                    fuAttachment.Attributes.Add("Class", "textfield form-control ErrorInput")
                Else
                    WorkingData.EnrolmentRequestAttachments.AddRow(rowAttachment)
                    FileIsValid = True
                    'Redirecting to self instead of just resetting controls to avoid issue of attachment being added on browser refresh.
                    Response.Redirect(GetResourceValue("checkout_attachments_GCFE_aspx"))
                End If
            End If
        End If

        'trNoItems.Visible = False
        'Me.Page.Validate()
        ''Following line is important to perform validations on File Upload control based on setup done on control.
        'Me.fuAttachment.ValidateFile()

        'If Not Me.Page.IsValid Then
        '    Return
        'End If

        'If ddlTypeOfEvidence.SelectedValue = "" Then
        '    Dim v As New CustomValidator
        '    v.ErrorMessage = "Please select Type of Evidence for the file you are uploading"
        '    v.IsValid = False
        '    Me.Page.Validators.Add(v)
        '    Exit Sub
        'End If

        'Dim validExtensions As String() = {".jpg", ".jpeg", ".png", ".gif", ".bmp", ".heic, .pdf, .doc, .docx"}
        'If Not IsNothing(fuAttachment) Then
        '    'If String.IsNullOrEmpty(PhotoPath.Value) And IsPhotoRequired = True Then
        '    '    PhotoPathValidator.ErrorMessage = "<i class=""fa-solid fa-triangle-exclamation""></i> Please upload your photo by clicking on Choose File"
        '    '    PhotoPathValidator.IsValid = False
        '    '    PhotoPathValidator.CssClass = "error alert alert-danger"
        '    '    FileUpload1.Attributes.Add("Class", "textfield form-control ErrorInput")
        '    'End If
        '    If fuAttachment.FileName.ToString.Length = 0 Then
        '        'Do Nothing
        '    ElseIf fuAttachment.FileName.ToString.LastIndexOf(".") <= 0 Then
        '        FilePathValidator.ErrorMessage = "<i class=""fa-solid fa-triangle-exclamation""></i> This type of file is not valid. Please upload a valid file"
        '        FilePathValidator.IsValid = False
        '        FilePathValidator.CssClass = "error alert alert-danger"
        '        fuAttachment.Attributes.Add("Class", "textfield form-control ErrorInput")
        '    ElseIf validExtensions.Contains(fuAttachment.FileName.ToString.Substring(fuAttachment.FileName.ToString.LastIndexOf(".")).ToLower) = False Then
        '        FilePathValidator.ErrorMessage = "<i class=""fa-solid fa-triangle-exclamation""></i> This type of file is not valid. Please upload a valid file"
        '        FilePathValidator.IsValid = False
        '        FilePathValidator.CssClass = "error alert alert-danger"
        '        fuAttachment.Attributes.Add("Class", "textfield form-control ErrorInput")
        '    End If
        'End If

        'Dim rowAttachment = WorkingData.EnrolmentRequestAttachments.NewRow
        '_lastAttachmentID -= 1
        'With rowAttachment

        '    Dim ValidatorAppend As String = .TypeOfEvidence
        '    .AttachmentID = _lastAttachmentID
        '    .TypeOfEvidence = ddlTypeOfEvidence.SelectedValue
        '    ' .Notes = txtNotes.Text.Trim
        '    .Attachment = fuAttachment.FileBytes
        '    .FileName = fuAttachment.FileName
        '    .Size = fuAttachment.FileBytes.Length
        '    hiddenvalidator.Value = (hiddenvalidator.Value + ValidatorAppend)
        '    If CCCAttachmentThumbnailGenerator.FilenameIndicatesFileIsCompatibleImage(fuAttachment.FileName) Then
        '        Try
        '            .ImageThumb = CCCAttachmentThumbnailGenerator.CreateThumbnailAsByteArray(.Attachment, 96)
        '        Catch ex As Exception
        '            ' Cannot create thumb nail- ignore error
        '            .ImageThumb = Nothing ' No thumb-nail
        '        End Try
        '    End If
        '    .CreatedDate = DateTime.Now
        'End With
        'WorkingData.EnrolmentRequestAttachments.AddRow(rowAttachment)
        ''Redirecting to self instead of just resetting controls to avoid issue of attachment being added on browser refresh.

        'Response.Redirect(GetResourceValue("checkout_attachments_HE_aspx"))
    End Sub

    ''' <summary>
    ''' Binding Attachment table to repeater to display already uploaded attachments to user.
    ''' </summary>
    Private Sub loadAttachments()
        rptAttachments.DataSource = WorkingData.EnrolmentRequestAttachments
        rptAttachments.DataBind()
        If (WorkingData.EnrolmentRequestAttachments.Rows.Count = 0) Then
            'trNoItems.Visible = True
        Else
            'trNoItems.Visible = False
        End If
    End Sub

    Public Function displayNotesIcon(notes As String) As Boolean
        If Len(notes) > 0 Then
            Return True
        Else
            Return False
        End If

    End Function

    Public Overrides Sub ValidateControl()

        'Type of Evidence
        If Not IsNothing(ddlTypeOfEvidence) And UploadButtonClicked = True Then
            If ddlTypeOfEvidence.SelectedValue = "" Then
                ddlTypeOfEvidenceValidator.ErrorMessage = "Please select Type of Evidence for the file you are uploading"
                ddlTypeOfEvidenceValidator.IsValid = False
                ddlTypeOfEvidenceValidator.CssClass = "error alert alert-danger"
                ddlTypeOfEvidence.CssClass = "ErrorInput"

                HasSelectedEvidenceType = False
                Return
            End If
        End If

        If UploadButtonClicked = False Then
            'File picked but upload not pressed
            If Not IsNothing(btnUpload) Then
                If FileChosen.Text = "Y" Then
                    btnUploadValidator.ErrorMessage = "Please press Upload once you have picked a file before pressing Next"
                    btnUploadValidator.IsValid = False
                    btnUploadValidator.CssClass = "error alert alert-danger"
                    'btnUpload.CssClass = "ErrorInput"
                End If
            End If


            'File Validation
            Dim validExtensions As String() = {".jpg", ".jpeg", ".png", ".gif", ".bmp", ".heic", ".pdf", ".doc", ".docx"}

            If Not IsNothing(StudentDetailUserDefined25) Then
                If WorkingData.EnrolmentRequestAttachments.Count = 0 And (CType(StudentDetailUserDefined25.Value, String) = "" Or CType(StudentDetailUserDefined25.Value, String) = "OK") And IsAttachmentRequired = True Then
                    btnUploadValidator.ErrorMessage = "Please upload your attachment/s by clicking on Choose File. If you cannot upload your attachment/s then please state the reason why."
                    btnUploadValidator.IsValid = False
                    btnUploadValidator.CssClass = "error alert alert-danger"
                    fuAttachment.Attributes.Add("Class", "textfield form-control ErrorInput")
                End If
            Else
                If WorkingData.EnrolmentRequestAttachments.Count = 0 And IsAttachmentRequired = True Then
                    btnUploadValidator.ErrorMessage = "Please upload your attachment/s by clicking on Choose File. If you cannot upload your attachment/s then please state the reason why."
                    btnUploadValidator.IsValid = False
                    btnUploadValidator.CssClass = "error alert alert-danger"
                    fuAttachment.Attributes.Add("Class", "textfield form-control ErrorInput")
                End If
            End If
        End If

        Dim attachmentTypes As List(Of String) = New List(Of String)
        Dim attachmentFiles As List(Of String) = New List(Of String)
        For Each row As EnrolmentRequestAttachmentsRow In WorkingData.EnrolmentRequestAttachments.Rows
            attachmentTypes.Add(row.TypeOfEvidence.ToString)
            attachmentFiles.Add(row.FileName)
        Next

        Dim duplicateTypes As List(Of String) =
               attachmentTypes.GroupBy(Function(n) n) _
               .Where(Function(g) g.Count() > 1) _
               .Select(Function(g) g.First) _
               .ToList()

        Dim duplicateTypesOnly As String() = duplicateTypes.ToArray
        Dim duplicateTypesString As String = String.Join(", ", duplicateTypesOnly)

        If Not IsNothing(duplicateTypesString) Then
            If Not String.IsNullOrEmpty(duplicateTypesString) Then
                AttachmentsValidatorType.ErrorMessage = "The following attachment types have been entered more than once: <strong>""" + duplicateTypesString + """</strong>. Please remove the duplicated options."
                AttachmentsValidatorType.IsValid = False
                AttachmentsValidatorType.CssClass = "error alert alert-danger"
            End If
        End If

        Dim duplicateFiles As List(Of String) =
               attachmentFiles.GroupBy(Function(n) n) _
               .Where(Function(g) g.Count() > 1) _
               .Select(Function(g) g.First) _
               .ToList()

        Dim duplicateFilesOnly As String() = duplicateFiles.ToArray
        Dim duplicateFilesString As String = String.Join(", ", duplicateFilesOnly)

        If Not IsNothing(duplicateFilesString) Then
            If Not String.IsNullOrEmpty(duplicateFilesString) Then
                AttachmentsValidatorFiles.ErrorMessage = "The following files have been attached more than once: <strong>""" + duplicateFilesString + """</strong>. Please remove the duplicated files."
                AttachmentsValidatorFiles.IsValid = False
                AttachmentsValidatorFiles.CssClass = "error alert alert-danger"
            End If
        End If

        MyBase.ValidateControl()
    End Sub

    Private Sub btnContinue_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnContinue.Click
        Me.Page.Validate()

        If Me.Page.IsValid Then
            If WorkingData.ShoppingCart.ContainsItemsOfType("Application") AndAlso WorkingData.ShoppingCart.ContainsItemsOfType("Enrolment") Then
                Response.Redirect(GetResourceValue("checkout_dataprotection_aspx"))
            ElseIf WorkingData.ShoppingCart.ContainsItemsOfType("Application") Then
                If Not IsNothing(Course) And Course.UserDefined23 = "True" Then
                    'Course has option to Skip Employment Details Ticked
                    Response.Redirect(GetResourceValue("checkout_dataprotection_apponly_GCFE_aspx"))
                Else
                    Response.Redirect(GetResourceValue("checkout_employment_GCFE"))
                End If
            ElseIf WorkingData.ShoppingCart.ContainsItemsOfType("Enrolment") Then
                Response.Redirect(GetResourceValue("checkout_dataprotection_aspx"))
            ElseIf WorkingData.ShoppingCart.ContainsItemsOfType("Enquiry") Then
                Response.Redirect(GetResourceValue("checkout_dataprotection_enquiry"))
            End If
        Else
            HasFormErrors = True
        End If
    End Sub

End Class
