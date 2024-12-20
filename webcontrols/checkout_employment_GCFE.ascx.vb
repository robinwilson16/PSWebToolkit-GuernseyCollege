Imports CompassCC.ProSolution.PSWebEnrolmentKit
Imports CompassCC.CCCSystem.CCCCommon

Partial Class webcontrols_checkout_employment_GCFE
    Inherits CheckoutBaseControl

    Public HasFormErrors As Boolean = False
    Public Course As OfferingRow

    Protected Overrides Sub OnLoad(e As EventArgs)
        MyBase.OnLoad(e)

        If Not IsNothing(WorkingData.ShoppingCart.Items(0)) Then
            Course = OfferingDataTable.FetchByOfferingID(WorkingData.ShoppingCart.Items(0).OfferingID)
        Else
            Course = Nothing
        End If

        ''Clear class attributes first
        'Me.panel1.Attributes("class") = ""

        ''Breadcrumbs
        'If WorkingData.ShoppingCart.ContainsItemsOfType("Application") AndAlso WorkingData.ShoppingCart.ContainsItemsOfType("Enrolment") Then
        '    breadcrumbenrols.Visible = True
        '    Me.panel1.Attributes("class") = "panel panel-danger"
        'ElseIf WorkingData.ShoppingCart.ContainsItemsOfType("Application") Then
        '    breadcrumbenrols.Visible = False
        '    Me.panel1.Attributes("class") = "panel panel-success"
        'ElseIf WorkingData.ShoppingCart.ContainsItemsOfType("Enrolment") Then
        '    breadcrumbenrols.Visible = True
        '    Me.panel1.Attributes("class") = "panel panel-danger"
        'ElseIf WorkingData.ShoppingCart.ContainsItemsOfType("Enquiry") Then
        '    breadcrumbenrols.Visible = False
        '    Me.panel1.Attributes("class") = "panel panel-warning"
        'Else
        '    breadcrumbenrols.Visible = False
        '    Me.panel1.Attributes("class") = "panel panel-danger"
        'End If

        Dim organisationDataTable As New OrganisationDataTable
        Dim organisationDataView As CCCDataViewDataSet = CCCDataViewDataSet.CreateDataView(organisationDataTable)
        organisationDataView.Columns.AddDBColumns(True, False)
        organisationDataView.Filters.SetColumnFilter(organisationDataTable.UserDefined2Column, "TGA", FilterOperator.OperatorEquals)
        organisationDataTable.TableAdapter.Load(organisationDataTable, organisationDataView)
        EmployerList.Items.Clear()
        EmployerList.DataSource = organisationDataTable
        EmployerList.DataTextField = "Name"
        EmployerList.DataValueField = "Name"
        EmployerList.DataSourceID = String.Empty
        EmployerList.DataBind()
        EmployerList.Items.Insert(0, New ListItem("", String.Empty, False))
    End Sub

    Public Overrides Sub ValidateControl()
        Dim rowCount As Integer = 0

        For Each row As EnrolmentRequestEmploymentHistoryRow In WorkingData.EnrolmentRequestEmploymentHistory.Rows
            rowCount += 1
            'row.Cells(0).Controls(0).ToString

            If rowCount >= 1 Then
                Dim contactEmail As String = row.Email

                If String.IsNullOrEmpty(contactEmail) Then
                    Dim v As New CustomValidator
                    v.ErrorMessage = "<i class=""fa-solid fa-triangle-exclamation""></i> Error on line " + (rowCount).ToString + " as no contact email has been provided. Please complete all mandatory fields."
                    v.IsValid = False
                    Me.Page.Validators.Add(v)
                End If
            End If
        Next

        'If Not Me.Page.IsValid Then
        '    HasFormErrors = True
        'End If
    End Sub

    Protected Overrides Sub CreateChildControls()
        MyBase.CreateChildControls()

        If IsPostBack Then
            Dim c As Control = GetPostBackControl(Me.Page)
            If c.GetType Is GetType(Button) Then
                Dim btn = DirectCast(c, Button)
                If btn.Text = "Add Row" Then
                    intCurrentEmploymentRecords += 1
                ElseIf btn.ID = "btnNoPreviousEmployment" Then
                    intCurrentEmploymentRecords = 0
                    WorkingData.EnrolmentRequestEmploymentHistory.Clear()
                    slidingdiv.Visible = Not slidingdiv.Visible
                    If slidingdiv.Visible Then
                        btn.Text = "No previous employment"
                        btn.ToolTip = "Click to remove all the items listed below (And hide the list)"
                    Else
                        btn.Text = "Add previous employment"
                        btn.ToolTip = "Click to show a list below that you can add previous employment records to"
                    End If
                End If
            End If
        End If

        If slidingdiv.Visible Then

            For i = 0 To intCurrentEmploymentRecords
                'Dim row1, row2, row3 As New TableRow
                'Dim cell1, cell2, cell3, cell4, cell5, cell6, cell7, cell8, cell9, cell10 As New TableCell
                'Dim ctl1, ctl2, ctl3, ctl4, ctl5, ctl6, ctl7, ctl8, ctl9, ctl10 As New StudentEmploymentHistoryField

                Dim employerName, jobTitle, dateFrom, contactEmail, contactTel As New StudentEmploymentHistoryField
                Dim employerInputCustomLabel As New Label
                Dim employerInput As New HtmlSelect
                Dim employerInputCustom As New HtmlInputText

                employerName.StudentEmploymentHistoryFieldType = StudentEmploymentHistoryFieldType.EmployerName
                employerName.StudentEmploymentHistoryRowNumber = i
                employerName.IsRequired = True
                employerName.Title = "Please enter the name of your employer"
                'cell1.Controls.Add(employerName)
                'employerInput.ID = "EmployerList" + i.ToString()
                employerInput.Name = "EmployerList"
                Dim PleaseSelectListItem As New ListItem("-- Please Select --")

                employerInput.Items.Add(PleaseSelectListItem)
                employerInput.Attributes.Add("class", "form-control EmployerInput mb-3")
                'cell1.Controls.Add(employerInput)

                employerInputCustomLabel.Text = "Employer (if not in list)"
                'cell2.Controls.Add(employerInputCustomLabel)

                employerInputCustom.Attributes.Add("class", "form-control EmployerInputCustom mb-3")
                'cell2.Controls.Add(employerInputCustom)

                jobTitle.StudentEmploymentHistoryFieldType = StudentEmploymentHistoryFieldType.JobTitle
                jobTitle.StudentEmploymentHistoryRowNumber = i
                jobTitle.IsRequired = True
                jobTitle.Title = "Please enter your job title"
                'cell3.Controls.Add(ctl3)

                dateFrom.StudentEmploymentHistoryFieldType = StudentEmploymentHistoryFieldType.DateFrom
                dateFrom.StudentEmploymentHistoryRowNumber = i
                dateFrom.IsRequired = True
                dateFrom.Title = "Please confirm when you  started this job"
                dateFrom.CustomFieldType = CCCFieldType.Date
                dateFrom.HTML5InputType = HTML5InputType.date
                'cell4.Controls.Add(ctl4)

                'ctl4.StudentEmploymentHistoryFieldType = StudentEmploymentHistoryFieldType.DateTo
                'ctl4.StudentEmploymentHistoryRowNumber = i
                'ctl4.CustomFieldType = CCCFieldType.Date
                'ctl4.HTML5InputType = HTML5InputType.date
                'cell4.Controls.Add(ctl4)

                'ctl5.StudentEmploymentHistoryFieldType = StudentEmploymentHistoryFieldType.OrganisationContactID
                'ctl5.StudentEmploymentHistoryRowNumber = i
                'ctl5.IsRequired = True
                'ctl5.CustomCaption = "Contact Name"
                'cell5.Controls.Add(ctl5)

                'Dim employerContactTable As New Table
                'employerContactTable.Attributes.Add("class", "table")
                'Dim eRow1 As New TableRow
                'Dim eCell1, eCell2, eCell3 As New TableCell


                Dim employerContactNameLabel As New Label
                employerContactNameLabel.Text = "Contact Name"
                employerContactNameLabel.Attributes.Add("class", "fw-bold")
                'eCell1.Controls.Add(employerContactNameLabel)

                Dim employerContactName As New HtmlInputText
                employerContactName.Attributes.Add("class", "form-control EmployerContactName mb-3")
                'eCell1.Controls.Add(employerContactName)

                Dim employerContactEmailLabel As New Label
                employerContactEmailLabel.Text = "Contact Email"
                employerContactEmailLabel.Attributes.Add("class", "fw-bold")
                'eCell2.Controls.Add(employerContactEmailLabel)

                Dim employerContactEmail As New HtmlInputText
                employerContactEmail.Attributes.Add("class", "form-control EmployerContactEmail mb-3") 'Hidden for now
                'eCell2.Controls.Add(employerContactEmail)

                contactEmail.StudentEmploymentHistoryFieldType = StudentEmploymentHistoryFieldType.Email
                contactEmail.CssClass = "EmployerContactEmailOriginal"
                contactEmail.StudentEmploymentHistoryRowNumber = i
                contactEmail.IsRequired = True
                contactEmail.CustomCaption = "Contact Email (Hidden)"
                'eCell2.Controls.Add(ctl6)

                'ctl5.StudentEmploymentHistoryFieldType = StudentEmploymentHistoryFieldType.ReasonForUnemploymentID
                'ctl5.StudentEmploymentHistoryRowNumber = i
                'cell5.Controls.Add(ctl5)

                'ctl6.StudentEmploymentHistoryFieldType = StudentEmploymentHistoryFieldType.EmploymentStatusID
                'ctl6.StudentEmploymentHistoryRowNumber = i
                'ctl6.CustomCaption = "Employment Status"
                'cell6.Controls.Add(ctl6)

                contactTel.StudentEmploymentHistoryFieldType = StudentEmploymentHistoryFieldType.ContactTel
                contactTel.StudentEmploymentHistoryRowNumber = i
                contactTel.IsRequired = True
                contactTel.CustomCaption = "Contact Tel"
                'eCell3.Controls.Add(ctl7)

                Dim removeRowButton As New Button
                removeRowButton.Text = "X"
                removeRowButton.CssClass = "btn btn-danger"
                removeRowButton.CommandArgument = i
                removeRowButton.CommandName = "RemoveRowButton"

                AddHandler removeRowButton.Command, AddressOf RemoveRowButton_Click

                'row1.Cells.Add(cell1)
                'row1.Cells.Add(cell2)
                'row2.Cells.Add(cell3)

                'row2.Cells.Add(cell4)
                'row3.Cells.Add(cell5)
                'row3.Cells.Add(cell6)

                'If i Mod 2 = 0 Then
                '    row1.Attributes.Add("style", "background-color:#f9f9f9")
                '    row3.Attributes.Add("style", "background-color:#f9f9f9")
                'End If

                'tblEmployment.Rows.Add(row1)
                'tblEmployment.Rows.Add(row2)
                'tblEmployment.Rows.Add(row3)

                'New Div Tag For Better Responsiveness
                Dim empOuterBox As New HtmlGenericControl("DIV")
                empOuterBox.Attributes.Add("class", "col-md-12 mb-4 d-flex align-items-stretch")
                Dim empCard As New HtmlGenericControl("DIV")
                empCard.Attributes.Add("class", "card w-100")

                Dim empEntry As New HtmlGenericControl("DIV")
                empEntry.Attributes.Add("class", "card-body d-flex flex-column")

                'Rows of fields
                Dim employerRow As New HtmlGenericControl("DIV")
                employerRow.Attributes.Add("class", "row")

                Dim employerContactRow As New HtmlGenericControl("DIV")
                employerContactRow.Attributes.Add("class", "row")

                'Add controls above to rows
                Dim employerNameBox As New HtmlGenericControl("DIV")
                employerNameBox.Attributes.Add("class", "form-group col-md-6 col-xl")
                employerNameBox.Controls.Add(employerName)
                employerNameBox.Controls.Add(employerInput)
                employerRow.Controls.Add(employerNameBox)

                Dim employerInputCustomBox As New HtmlGenericControl("DIV")
                employerInputCustomBox.Attributes.Add("class", "form-group col-md-6 col-xl")
                employerInputCustomBox.Controls.Add(employerInputCustomLabel)
                employerInputCustomBox.Controls.Add(employerInputCustom)
                employerRow.Controls.Add(employerInputCustomBox)

                Dim jobTitleBox As New HtmlGenericControl("DIV")
                jobTitleBox.Attributes.Add("class", "form-group col-md-6 col-xl mb-3")
                jobTitleBox.Controls.Add(jobTitle)
                employerRow.Controls.Add(jobTitleBox)

                Dim dateFromBox As New HtmlGenericControl("DIV")
                dateFromBox.Attributes.Add("class", "form-group col-md-6 col-xl mb-3")
                dateFromBox.Controls.Add(dateFrom)
                employerRow.Controls.Add(dateFromBox)

                Dim employerContactNameBox As New HtmlGenericControl("DIV")
                employerContactNameBox.Attributes.Add("class", "form-group col-md-6 col-xl")
                employerContactNameBox.Controls.Add(employerContactNameLabel)
                employerContactNameBox.Controls.Add(employerContactName)
                employerContactRow.Controls.Add(employerContactNameBox)

                Dim employerContactEmailBox As New HtmlGenericControl("DIV")
                employerContactEmailBox.Attributes.Add("class", "form-group col-md-6 col-xl")
                employerContactEmailBox.Controls.Add(employerContactEmailLabel)
                employerContactEmailBox.Controls.Add(employerContactEmail)
                employerContactEmailBox.Controls.Add(contactEmail)
                employerContactRow.Controls.Add(employerContactEmailBox)

                Dim employerContactTelBox As New HtmlGenericControl("DIV")
                employerContactTelBox.Attributes.Add("class", "form-group col-md-6 col-xl")
                employerContactTelBox.Controls.Add(contactTel)
                employerContactRow.Controls.Add(employerContactTelBox)

                'Add HTML items to page
                empEntry.Controls.Add(employerRow)
                empEntry.Controls.Add(employerContactRow)
                empCard.Controls.Add(empEntry)
                empOuterBox.Controls.Add(empCard)
                EmploymentRecords.Controls.Add(empOuterBox)
            Next
        End If

    End Sub

    Public Property intCurrentEmploymentRecords() As Integer
        Get
            Return CInt(HttpContext.Current.Session("intEmploymentRows"))
        End Get
        Set(ByVal value As Integer)
            HttpContext.Current.Session("intEmploymentRows") = value
        End Set
    End Property

    Sub RemoveRowButton_Click(sender As Object, e As CommandEventArgs)
        Dim rowToDelete As Integer = CInt(e.CommandArgument)

        Dim rowNumber As Integer = 0
        Dim rowDeleted As Boolean = False

        For Each row As EnrolmentRequestEmploymentHistoryRow In WorkingData.EnrolmentRequestEmploymentHistory.Rows
            If rowNumber = rowToDelete Then
                row.Delete()
                rowDeleted = True

                Exit For
            End If

            rowNumber += 1
        Next

        If rowDeleted = True Then
            HttpContext.Current.Session("intEmploymentRows") = intCurrentEmploymentRecords - 1
        End If
    End Sub

    Protected Sub btnBack_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBack.Click
        Dim numQuals = WorkingData.ApplicationRequestQualsOnEntry.Count()
        If Course.EnrolmentTypeID = 6 Or Course.EnrolmentTypeID = 7 Then 'Part-Time Work Related Or ACL
            Response.Redirect(GetResourceValue("checkout_fees_GCFE_aspx"))
        ElseIf numQuals > 0 Then
            Response.Redirect(GetResourceValue("checkout_attachments_GCFE_aspx"))
        Else
            Response.Redirect(GetResourceValue("checkout_quals_on_entry_GCFE_aspx"))
        End If
    End Sub

    Private Sub btnContinue_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnContinue.Click

        Me.Page.Validate()

        If Me.Page.IsValid Then
            ''CCCPS-75837: Redirecting user to new attachment step.
            If WorkingData.ShoppingCart.ContainsItemsOfType("Application") OrElse WorkingData.ShoppingCart.ContainsItemsOfType("Enrolment") Then
                Response.Redirect(GetResourceValue("checkout_dataprotection_apponly_GCFE_aspx"))
            ElseIf WorkingData.ShoppingCart.ContainsItemsOfType("Enquiry") Then
                Response.Redirect(GetResourceValue("checkout_dataprotection_enquiry"))
            End If
        Else
            HasFormErrors = True
        End If
    End Sub


End Class
