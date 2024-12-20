Imports CompassCC.ProSolution.PSWebEnrolmentKit
Imports CompassCC.CCCSystem.CCCCommon

Partial Class checkout_quals_on_entry
    Inherits CheckoutBaseControl


    Protected Overrides Sub OnLoad(e As EventArgs)
        MyBase.OnLoad(e)

        'Breadcrumbs
        If WorkingData.ShoppingCart.ContainsItemsOfType("Application") AndAlso WorkingData.ShoppingCart.ContainsItemsOfType("Enrolment") Then
            breadcrumbenrols.Visible = True
            breadcrumbapps.Visible = False
        ElseIf WorkingData.ShoppingCart.ContainsItemsOfType("Application") Then
            breadcrumbenrols.Visible = False
            breadcrumbapps.Visible = True
        ElseIf WorkingData.ShoppingCart.ContainsItemsOfType("Enrolment") Then
            breadcrumbenrols.Visible = True
            breadcrumbapps.Visible = False
        ElseIf WorkingData.ShoppingCart.ContainsItemsOfType("Enquiry") Then
            breadcrumbenrols.Visible = False
            breadcrumbapps.Visible = False
        Else
            breadcrumbenrols.Visible = False
            breadcrumbapps.Visible = False
        End If

        'Custom code to colour the form based on the contents of the shopping basket
        'Use this when sharing pages between types (Application/Enrolment)

        'Clear class attributes first
        Me.panel1.Attributes("class") = ""



        If WorkingData.ShoppingCart.ContainsItemsOfType("Enrolment") Then
            Me.panel1.Attributes("class") = "panel panel-danger"
        ElseIf WorkingData.ShoppingCart.ContainsItemsOfType("Application") Then
            Me.panel1.Attributes("class") = "panel panel-success"
        ElseIf WorkingData.ShoppingCart.ContainsItemsOfType("Enquiry") Then
            Me.panel1.Attributes("class") = "panel panel-info"
        Else
            Me.panel1.Attributes("class") = "panel panel-danger"
        End If
    End Sub

    Protected Overrides Sub CreateChildControls()
        MyBase.CreateChildControls()

        'If IsPostBack Then

        'Recreate the header row
        Dim rowH As New TableRow

        Dim c1, c2, c3, c4, c5 As New TableCell
        c1.Text = "Qualification"
        c1.Attributes.Add("class", "text=center")
        c2.Text = "Subject (if not in list)"
        c3.Text = "Grade"
        c4.Text = "Predicted Grade"
        c5.Text = "Date Awarded"
        rowH.Cells.AddRange({c1, c2, c3, c4, c5})

        tblQuals.Rows.Add(rowH)

        If IsPostBack Then
            Dim c As Control = GetPostBackControl(Me.Page)
            If c.GetType Is GetType(Button) Then
                Dim btn = DirectCast(c, Button)
                If btn.Text = "Add Row" Then
                    intCurrentQuals += 1
                ElseIf btn.ID = "btnNoPriorQualifications" Then
                    intCurrentQuals = 0
                    WorkingData.EnrolmentRequestQualsOnEntry.Clear()
                    slidingdiv.Visible = Not slidingdiv.Visible
                    If slidingdiv.Visible Then
                        btn.Text = "No prior qualifications"
                        btn.ToolTip = "Click to remove all the items listed below (And hide the list)"
                    Else
                        btn.Text = "Add prior qualifications"
                        btn.ToolTip = "Click to show a list below that you can add Qualifications to"
                    End If
                End If
            End If
        End If

        If slidingdiv.Visible Then

            For i = 0 To intCurrentQuals
                Dim row As New TableRow
                Dim cell1, cell2, cell3, cell4, cell5 As New TableCell
                Dim ctl1, ctl2, ctl3, ctl4, ctl5 As New StudentQualsOnEntryField


                ctl1.StudentQualsOnEntryFieldType = StudentQualsOnEntryFieldType.QualID
                ctl1.StudentQualsOnEntryRowNumber = i
                ctl1.LabelWidth = 0
                cell1.Controls.Add(ctl1)

                ctl2.StudentQualsOnEntryFieldType = StudentQualsOnEntryFieldType.Subject
                ctl2.StudentQualsOnEntryRowNumber = i
                ctl2.LabelWidth = 0
                cell2.Controls.Add(ctl2)

                ctl3.StudentQualsOnEntryFieldType = StudentQualsOnEntryFieldType.Grade
                ctl3.StudentQualsOnEntryRowNumber = i
                ctl3.LabelWidth = 0
                'ctl3.ExcludedIDValues = "1,2,3,4,5,6,7,8,9,0" 'This would exclude certain grades from the drop down list
                cell3.Controls.Add(ctl3)

                ctl4.StudentQualsOnEntryFieldType = StudentQualsOnEntryFieldType.PredictedGrade
                ctl4.StudentQualsOnEntryRowNumber = i
                ctl4.LabelWidth = 0
                cell4.Controls.Add(ctl4)

                ctl5.StudentQualsOnEntryFieldType = StudentQualsOnEntryFieldType.DateAwarded
                ctl5.StudentQualsOnEntryRowNumber = i
                ctl5.LabelWidth = 0
                ctl5.CustomFieldType = CCCFieldType.Date
                cell5.Controls.Add(ctl5)

                row.Cells.AddRange({cell1, cell2, cell3, cell4, cell5})
                tblQuals.Rows.Add(row)

            Next
        End If
    End Sub
   
    Public Property intCurrentQuals() As Integer
        Get
            Return CInt(HttpContext.Current.Session("intQualRows"))
        End Get
        Set(ByVal value As Integer)
            HttpContext.Current.Session("intQualRows") = value
        End Set
    End Property


    Private Sub btnContinue_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnContinue.Click

        Me.Page.Validate()

        If Me.Page.IsValid Then
            If WorkingData.ShoppingCart.ContainsItemsOfType("Application") AndAlso WorkingData.ShoppingCart.ContainsItemsOfType("Enrolment") Then
                Response.Redirect(GetResourceValue("checkout_disability_aspx"))
            ElseIf WorkingData.ShoppingCart.ContainsItemsOfType("Application") Then
                ''CCCPS-75837: Redirecting user to new Attachment step instead of data protection directly.
                Response.Redirect(GetResourceValue("checkout_disability_aspx"))
            ElseIf WorkingData.ShoppingCart.ContainsItemsOfType("Enrolment") Then
                Response.Redirect(GetResourceValue("checkout_disability_aspx"))
            ElseIf WorkingData.ShoppingCart.ContainsItemsOfType("Enquiry") Then
                Response.Redirect(GetResourceValue("checkout_disability_aspx"))
            End If
        End If
    End Sub


End Class
