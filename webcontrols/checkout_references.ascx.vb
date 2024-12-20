Imports CompassCC.ProSolution.PSWebEnrolmentKit
Imports CompassCC.CCCSystem.CCCCommon

Partial Class webcontrols_checkout_references
    Inherits CheckoutBaseControl


    Protected Overrides Sub OnLoad(e As EventArgs)
        MyBase.OnLoad(e)

        'Breadcrumbs
        If WorkingData.ShoppingCart.ContainsItemsOfType("Application") AndAlso WorkingData.ShoppingCart.ContainsItemsOfType("Enrolment") Then
            breadcrumbenrols.Visible = True
            'breadcrumbapps.Visible = False
        ElseIf WorkingData.ShoppingCart.ContainsItemsOfType("Application") Then
            breadcrumbenrols.Visible = False
            'breadcrumbapps.Visible = True
        ElseIf WorkingData.ShoppingCart.ContainsItemsOfType("Enrolment") Then
            breadcrumbenrols.Visible = True
            'breadcrumbapps.Visible = False
        ElseIf WorkingData.ShoppingCart.ContainsItemsOfType("Enquiry") Then
            breadcrumbenrols.Visible = False
            'breadcrumbapps.Visible = False
        Else
            breadcrumbenrols.Visible = False
            'breadcrumbapps.Visible = False
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


        If IsPostBack Then
            Dim c As Control = GetPostBackControl(Me.Page)
            If c.GetType Is GetType(Button) Then
                Dim btn = DirectCast(c, Button)
                If btn.Text = "Add Row" Then
                    intCurrentReferences += 1
                ElseIf btn.ID = "btnNoReferences" Then
                    intCurrentReferences = 0
                    WorkingData.EnrolmentRequestHE.Clear()
                    slidingdiv.Visible = Not slidingdiv.Visible
                    If slidingdiv.Visible Then
                        btn.Text = "No References"
                        btn.ToolTip = "Click to remove all the items listed below (And hide the list)"
                    Else
                        btn.Text = "Add References"
                        btn.ToolTip = "Click to show a list below that you can add References to"
                    End If
                End If
            End If
        End If

        If slidingdiv.Visible Then

            For i = 0 To intCurrentReferences
                Dim row1, row2 As New HtmlTableRow
                Dim cell1, cell2, cell3, cell4, cell5, cell6 As New HtmlTableCell
                Dim ctl1, ctl2, ctl3, ctl4, ctl5, ctl6 As New StudentReferenceField

                ctl1.StudentReferenceFieldType = StudentReferenceFieldType.RefereeSurname
                ctl1.StudentReferenceRowNumber = i

                cell1.Controls.Add(ctl1)

                ctl2.StudentReferenceFieldType = StudentReferenceFieldType.RefereeForename
                ctl2.StudentReferenceRowNumber = i

                cell2.Controls.Add(ctl2)

                ctl3.StudentReferenceFieldType = StudentReferenceFieldType.RefereeTel
                ctl3.StudentReferenceRowNumber = i
                ctl3.CustomCaption = "Tel"
                cell3.Controls.Add(ctl3)

                ctl4.StudentReferenceFieldType = StudentReferenceFieldType.RefereeEmail
                ctl4.StudentReferenceRowNumber = i
                ctl4.CustomCaption = "Email"

                cell4.Controls.Add(ctl4)

                ctl5.StudentReferenceFieldType = StudentReferenceFieldType.RefereeRelationshipToStudent
                ctl5.StudentReferenceRowNumber = i
                ctl5.CustomCaption = "Relationship to you"
                cell5.Controls.Add(ctl5)

                ctl6.StudentReferenceFieldType = StudentReferenceFieldType.RefereeJobTitle
                ctl6.StudentReferenceRowNumber = i
                ctl6.CustomCaption = "Referee Job Title"
                cell6.Controls.Add(ctl6)

                row1.Cells.Add(cell1)
                row1.Cells.Add(cell2)
                row1.Cells.Add(cell3)
                row2.Cells.Add(cell4)
                row2.Cells.Add(cell5)
                row2.Cells.Add(cell6)

                If i Mod 2 = 0 Then
                    row1.Attributes.Add("style", "background-color:#f9f9f9")
                    row2.Attributes.Add("style", "background-color:#f9f9f9")
                End If

                tblReferences.Rows.Add(row1)
                tblReferences.Rows.Add(row2)
            Next
        End If
    End Sub

    Public Property intCurrentReferences() As Integer
        Get
            Return CInt(HttpContext.Current.Session("intReferenceRows"))
        End Get
        Set(ByVal value As Integer)
            HttpContext.Current.Session("intReferenceRows") = value
        End Set
    End Property


    Private Sub btnContinue_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnContinue.Click

        Me.Page.Validate()

        If Me.Page.IsValid Then
            If WorkingData.ShoppingCart.ContainsItemsOfType("Application") AndAlso WorkingData.ShoppingCart.ContainsItemsOfType("Enrolment") Then
                Response.Redirect(GetResourceValue("checkout_employment"))
            ElseIf WorkingData.ShoppingCart.ContainsItemsOfType("Application") Then
                ''CCCPS-75837: Redirecting user to new Attachment step instead of data protection directly.
                Response.Redirect(GetResourceValue("checkout_attachments_aspx"))
            ElseIf WorkingData.ShoppingCart.ContainsItemsOfType("Enrolment") Then
                Response.Redirect(GetResourceValue("checkout_employment"))
            ElseIf WorkingData.ShoppingCart.ContainsItemsOfType("Enquiry") Then
                Response.Redirect(GetResourceValue("checkout_dataprotection_enquiry"))
            End If
        End If
    End Sub
End Class
