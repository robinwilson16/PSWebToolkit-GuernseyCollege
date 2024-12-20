Imports CompassCC.ProSolution.PSWebEnrolmentKit
Imports CompassCC.CCCSystem.CCCCommon

Partial Class webcontrols_checkout_employment
    Inherits CheckoutBaseControl

    Protected Overrides Sub OnLoad(e As EventArgs)
        MyBase.OnLoad(e)

        'Clear class attributes first
        Me.panel1.Attributes("class") = ""

        'Breadcrumbs
        If WorkingData.ShoppingCart.ContainsItemsOfType("Application") AndAlso WorkingData.ShoppingCart.ContainsItemsOfType("Enrolment") Then
            breadcrumbenrols.Visible = True
            Me.panel1.Attributes("class") = "panel panel-danger"
        ElseIf WorkingData.ShoppingCart.ContainsItemsOfType("Application") Then
            breadcrumbenrols.Visible = False
            Me.panel1.Attributes("class") = "panel panel-success"
        ElseIf WorkingData.ShoppingCart.ContainsItemsOfType("Enrolment") Then
            breadcrumbenrols.Visible = True
            Me.panel1.Attributes("class") = "panel panel-danger"
        ElseIf WorkingData.ShoppingCart.ContainsItemsOfType("Enquiry") Then
            breadcrumbenrols.Visible = False
            Me.panel1.Attributes("class") = "panel panel-warning"
        Else
            breadcrumbenrols.Visible = False
            Me.panel1.Attributes("class") = "panel panel-danger"
        End If

       
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
                Dim row1, row2 As New TableRow
                Dim cell1, cell2, cell3, cell4, cell5, cell6, cell7, cell8, cell9, cell10 As New TableCell
                Dim ctl1, ctl2, ctl3, ctl4, ctl5, ctl6, ctl7, ctl8, ctl9, ctl10 As New StudentEmploymentHistoryField


                ctl1.StudentEmploymentHistoryFieldType = StudentEmploymentHistoryFieldType.EmployerName
                ctl1.StudentEmploymentHistoryRowNumber = i
                cell1.Controls.Add(ctl1)

                ctl2.StudentEmploymentHistoryFieldType = StudentEmploymentHistoryFieldType.JobTitle
                ctl2.StudentEmploymentHistoryRowNumber = i
                cell2.Controls.Add(ctl2)

                ctl3.StudentEmploymentHistoryFieldType = StudentEmploymentHistoryFieldType.DateFrom
                ctl3.StudentEmploymentHistoryRowNumber = i
                cell3.Controls.Add(ctl3)

                ctl4.StudentEmploymentHistoryFieldType = StudentEmploymentHistoryFieldType.DateTo
                ctl4.StudentEmploymentHistoryRowNumber = i
                cell4.Controls.Add(ctl4)

                'ctl5.StudentEmploymentHistoryFieldType = StudentEmploymentHistoryFieldType.ReasonForUnemploymentID
                'ctl5.StudentEmploymentHistoryRowNumber = i
                'cell5.Controls.Add(ctl5)

                ctl6.StudentEmploymentHistoryFieldType = StudentEmploymentHistoryFieldType.EmploymentStatusID
                ctl6.StudentEmploymentHistoryRowNumber = i
                ctl6.CustomCaption = "Employment Status"
                cell6.Controls.Add(ctl6)


                'ctl7.StudentEmploymentHistoryFieldType = StudentEmploymentHistoryFieldType.BenefitStatusIndicatorID
                'ctl7.StudentEmploymentHistoryRowNumber = i
                'cell7.Controls.Add(ctl7)

                ctl8.StudentEmploymentHistoryFieldType = StudentEmploymentHistoryFieldType.EmploymentIntensityID
                ctl8.StudentEmploymentHistoryRowNumber = i
                ctl8.CustomCaption = "Emp Intensity"
                cell8.Controls.Add(ctl8)


                ctl9.StudentEmploymentHistoryFieldType = StudentEmploymentHistoryFieldType.LengthOfEmploymentID
                ctl9.StudentEmploymentHistoryRowNumber = i
                ctl9.CustomCaption = "Length of Employment"
                cell9.Controls.Add(ctl9)

                'ctl10.StudentEmploymentHistoryFieldType = StudentEmploymentHistoryFieldType.LengthOfUnemploymentID
                'ctl10.StudentEmploymentHistoryRowNumber = i
                'cell10.Controls.Add(ctl10)

                row1.Cells.Add(cell1)
                row1.Cells.Add(cell2)
                row1.Cells.Add(cell3)
                row1.Cells.Add(cell4)

                row2.Cells.Add(cell6)

                row2.Cells.Add(cell8)
                row2.Cells.Add(cell9)

                If i Mod 2 = 0 Then
                    row1.Attributes.Add("style", "background-color:#f9f9f9")
                    row2.Attributes.Add("style", "background-color:#f9f9f9")
                End If

                tblEmployment.Rows.Add(row1)
                tblEmployment.Rows.Add(row2)

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


    Private Sub btnContinue_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnContinue.Click

        Me.Page.Validate()

        If Me.Page.IsValid Then
            ''CCCPS-75837: Redirecting user to new attachment step.
            If WorkingData.ShoppingCart.ContainsItemsOfType("Application") OrElse WorkingData.ShoppingCart.ContainsItemsOfType("Enrolment") Then
                Response.Redirect(GetResourceValue("checkout_attachments_aspx"))
            ElseIf WorkingData.ShoppingCart.ContainsItemsOfType("Enquiry") Then
                Response.Redirect(GetResourceValue("checkout_dataprotection_enquiry"))
            End If
        End If
    End Sub


End Class
