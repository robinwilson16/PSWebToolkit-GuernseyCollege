Imports CompassCC.ProSolution.PSWebEnrolmentKit
''' <summary>
''' ''CCCPS-76339: New step in a wizard to enable user to provide additional 2 contact information.
''' </summary>
Partial Class webcontrols_checkout_emergency_contacts_GCFE
    Inherits CheckoutBaseControl

    Public HasFormErrors As Boolean = False
    Public ConsentDate As Date
    Public RequiresNextOfKin As Boolean
    Public Course As OfferingRow

    Protected Overrides Sub OnLoad(e As EventArgs)
        MyBase.OnLoad(e)

        If Not IsNothing(WorkingData.ShoppingCart.Items(0)) Then
            Course = OfferingDataTable.FetchByOfferingID(WorkingData.ShoppingCart.Items(0).OfferingID)
        Else
            Course = Nothing
        End If

        Dim dateToCheckDOB As Date = CDate(Today().Year & "-08-31")
        Dim parentalConsentDOB As Date = dateToCheckDOB.AddYears(-18)
        ConsentDate = parentalConsentDOB

        If WorkingData.ApplicationRequestRow.DateOfBirth > parentalConsentDOB Then
            RequiresNextOfKin = True

            Contact1Label.Text = "Parent/Guardian 1"
            Contact2Label.Text = "Parent/Guardian 2"
            WorkingData.StudentDetailRow.Contact1ContactType = 1 'Parent/Guardian
            WorkingData.StudentDetailRow.Contact2ContactType = 1 'Parent/Guardian
        Else
            RequiresNextOfKin = False

            Contact1Label.Text = "Emergency Contact 1"
            Contact2Label.Text = "Emergency Contact 2"
            WorkingData.StudentDetailRow.Contact1ContactType = 4 'Emergency Contact
            WorkingData.StudentDetailRow.Contact2ContactType = 4 'Emergency Contact
        End If

        'Breadcrumbs
        'If WorkingData.ShoppingCart.ContainsItemsOfType("Application") AndAlso WorkingData.ShoppingCart.ContainsItemsOfType("Enrolment") Then
        '    breadcrumbenrols.Visible = True
        '    breadcrumbapps.Visible = False
        'ElseIf WorkingData.ShoppingCart.ContainsItemsOfType("Application") Then
        '    breadcrumbenrols.Visible = False
        '    breadcrumbapps.Visible = True
        'ElseIf WorkingData.ShoppingCart.ContainsItemsOfType("Enrolment") Then
        '    breadcrumbenrols.Visible = True
        '    breadcrumbapps.Visible = False
        'ElseIf WorkingData.ShoppingCart.ContainsItemsOfType("Enquiry") Then
        '    breadcrumbenrols.Visible = False
        '    breadcrumbapps.Visible = False
        'Else
        '    breadcrumbenrols.Visible = False
        '    breadcrumbapps.Visible = False
        'End If

        'Custom code to colour the form based on the contents of the shopping basket
        'Use this when sharing pages between types (Application/Enrolment)

        'Clear class attributes first
        'Me.Contact1panel.Attributes("class") = ""
        'Me.Contact2panel.Attributes("class") = ""

        'If WorkingData.ShoppingCart.ContainsItemsOfType("Application") AndAlso WorkingData.ShoppingCart.ContainsItemsOfType("Enrolment") Then
        '    Me.Contact1panel.Attributes("class") = "panel panel-danger"
        '    Me.Contact2panel.Attributes("class") = "panel panel-danger"
        'ElseIf WorkingData.ShoppingCart.ContainsItemsOfType("Enrolment") Then
        '    Me.Contact1panel.Attributes("class") = "panel panel-danger"
        '    Me.Contact2panel.Attributes("class") = "panel panel-danger"
        'ElseIf WorkingData.ShoppingCart.ContainsItemsOfType("Application") Then
        '    Me.Contact1panel.Attributes("class") = "panel panel-success"
        '    Me.Contact2panel.Attributes("class") = "panel panel-success"
        'ElseIf WorkingData.ShoppingCart.ContainsItemsOfType("Enquiry") Then
        '    Me.Contact1panel.Attributes("class") = "panel panel-info"
        '    Me.Contact2panel.Attributes("class") = "panel panel-info"
        'Else
        '    Me.Contact1panel.Attributes("class") = "panel panel-danger"
        '    Me.Contact2panel.Attributes("class") = "panel panel-danger"
        'End If

    End Sub

    Public Overrides Sub ValidateControl()

        'Forename - Contact 1
        If Not IsNothing(Contact1Forename) Then
            If Contact1Forename.Value.ToString.Length = 0 Then
                Contact1ForenameValidator.ErrorMessage = "<i class=""fa-solid fa-triangle-exclamation""></i> Please enter a first name"
                Contact1ForenameValidator.IsValid = False
                Contact1ForenameValidator.CssClass = "error alert alert-danger"
                Contact1Forename.Attributes.Add("Class", "textfield form-control ErrorInput")
            End If
        End If

        'Surname - Contact 1
        If Not IsNothing(Contact1Surname) Then
            If Contact1Surname.Value.ToString.Length = 0 Then
                Contact1SurnameValidator.ErrorMessage = "<i class=""fa-solid fa-triangle-exclamation""></i> Please enter a surname"
                Contact1SurnameValidator.IsValid = False
                Contact1SurnameValidator.CssClass = "error alert alert-danger"
                Contact1Surname.Attributes.Add("Class", "textfield form-control ErrorInput")
            End If
        End If

        'Mobile and Tel - Contact 1
        Dim regexLandlineTel As New Regex("^(?:\+44\s?|0)[1238]\d\s?(?:\d\s?){7,8}$")
        Dim regexLandlineTelGuernsey As New Regex("^\d{11}$")
        Dim regexMobileTel As New Regex("(07[\d]{8,12}|447[\d]{7,11})$")
        Dim matchTel1Contact1 As Match = regexLandlineTel.Match(fldContact1Tel.Value.ToString())
        Dim matchTel2Contact1 As Match = regexMobileTel.Match(fldContact1Tel.Value.ToString()) 'Allow a mobile for other tel
        Dim matchTel3Contact1 As Match = regexLandlineTelGuernsey.Match(fldContact1Tel.Value.ToString()) 'Allow a Guernsey number
        Dim matchMobileTelContact1 As Match = regexMobileTel.Match(fldContact1MobileTel.Value.ToString())

        If Not IsNothing(fldContact1MobileTel) Then
            If String.IsNullOrEmpty(fldContact1Tel.Value.ToString) And String.IsNullOrEmpty(fldContact1MobileTel.Value.ToString) Then
                fldContact1MobileTelValidator.ErrorMessage = "<i class=""fa-solid fa-triangle-exclamation""></i> Please enter at least a home phone number or a mobile number"
                fldContact1MobileTelValidator.IsValid = False
                fldContact1MobileTelValidator.CssClass = "error alert alert-danger"
                fldContact1MobileTel.CssClass = "ErrorInput"
            End If
        End If

        If Not IsNothing(fldContact1MobileTel) Then
            If Not String.IsNullOrEmpty(fldContact1MobileTel.Value.ToString) And Not matchMobileTelContact1.Success Then
                fldContact1MobileTelValidator.ErrorMessage = "<i class=""fa-solid fa-triangle-exclamation""></i> Please enter a valid UK mobile number starting 07 containing no spaces"
                fldContact1MobileTelValidator.IsValid = False
                fldContact1MobileTelValidator.CssClass = "error alert alert-danger"
                fldContact1MobileTel.CssClass = "ErrorInput"
            End If
        End If

        If Not IsNothing(fldContact1Tel) Then
            If Not String.IsNullOrEmpty(fldContact1Tel.Value.ToString) And Not (matchTel1Contact1.Success Or matchTel2Contact1.Success Or matchTel3Contact1.Success) Then
                fldContact1TelValidator.ErrorMessage = "<i class=""fa-solid fa-triangle-exclamation""></i> Please enter a valid Telephone Number"
                fldContact1TelValidator.IsValid = False
                fldContact1TelValidator.CssClass = "error alert alert-danger"
                fldContact1Tel.CssClass = "ErrorInput"
            End If
        End If

        'Mobile and Tel - Contact 2
        Dim matchTel1Contact2 As Match = regexLandlineTel.Match(fldContact2Tel.Value.ToString())
        Dim matchTel2Contact2 As Match = regexMobileTel.Match(fldContact2Tel.Value.ToString()) 'Allow a mobile for other tel
        Dim matchTel3Contact2 As Match = regexLandlineTelGuernsey.Match(fldContact2Tel.Value.ToString()) 'Allow a Guernsey number
        Dim matchMobileTelContact2 As Match = regexMobileTel.Match(fldContact2MobileTel.Value.ToString())

        If Not IsNothing(fldContact2MobileTel) Then
            If Not String.IsNullOrEmpty(fldContact2MobileTel.Value.ToString) And Not matchMobileTelContact2.Success Then
                fldContact2MobileTelValidator.ErrorMessage = "<i class=""fa-solid fa-triangle-exclamation""></i> Please enter a valid UK mobile number starting 07 containing no spaces"
                fldContact2MobileTelValidator.IsValid = False
                fldContact2MobileTelValidator.CssClass = "error alert alert-danger"
                fldContact2MobileTel.CssClass = "ErrorInput"
            End If
        End If

        If Not IsNothing(fldContact2Tel) Then
            If Not String.IsNullOrEmpty(fldContact2Tel.Value.ToString) And Not (matchTel1Contact2.Success Or matchTel2Contact2.Success Or matchTel3Contact2.Success) Then
                fldContact2TelValidator.ErrorMessage = "<i class=""fa-solid fa-triangle-exclamation""></i> Please enter a valid Telephone Number"
                fldContact2TelValidator.IsValid = False
                fldContact2TelValidator.CssClass = "error alert alert-danger"
                fldContact2Tel.CssClass = "ErrorInput"
            End If
        End If

        MyBase.ValidateControl()
    End Sub

    Protected Sub btnBack_Click(sender As Object, e As EventArgs) Handles btnBack.Click
        Response.Redirect(GetResourceValue("checkout_supporting_you_GCFE_aspx"))
    End Sub

    Public Result As String

    Private Sub btnContinue_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnContinue.Click

        Dim dateToCheckDOB As Date = CDate(Today().Year & "-08-31")
        Dim parentalConsentDOB As Date = dateToCheckDOB.AddYears(-18)

        If (WorkingData.StudentDetailRow.DateOfBirth < parentalConsentDOB) Then
            WorkingData.StudentDetailRow.Contact1ContactType = 1 'Parent/Guardian
            WorkingData.StudentDetailRow.Contact2ContactType = 1 'Parent/Guardian
        Else
            WorkingData.StudentDetailRow.Contact1ContactType = 4 'Emergency Contact
            WorkingData.StudentDetailRow.Contact2ContactType = 4 'Emergency Contact
        End If

        Me.Page.Validate()

        If Me.Page.IsValid Then
            If WorkingData.ShoppingCart.ContainsItemsOfType("Application") Then
                If Course.EnrolmentTypeID = 6 Or Course.EnrolmentTypeID = 7 Then 'Part-Time Work Related or ACL
                    Response.Redirect(GetResourceValue("checkout_fees_GCFE_aspx"))
                Else
                    Response.Redirect(GetResourceValue("checkout_quals_on_entry_GCFE_aspx"))
                End If

            ElseIf WorkingData.ShoppingCart.ContainsItemsOfType("Enrolment") Then
                    Response.Redirect(GetResourceValue("checkout_school_employer_aspx"))
            End If
        Else
            Result = String.Join("; ", Me.Page.ModelState.Values.ToString)
            HasFormErrors = True
        End If
    End Sub

End Class
