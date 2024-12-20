Option Explicit On
Option Strict On

Imports System.IO
Imports CompassCC.CCCSystem.CCCCommon
Imports CompassCC.ProSolution.PSWebEnrolmentKit

Partial Class checkout_personal_details_GCFE
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

    Protected Overrides Sub OnLoad(ByVal e As System.EventArgs)
        MyBase.OnLoad(e)

        If Not IsNothing(WorkingData.ShoppingCart.Items(0)) Then
            Course = OfferingDataTable.FetchByOfferingID(WorkingData.ShoppingCart.Items(0).OfferingID)
        Else
            Course = Nothing
        End If

        'Remove any leading and trailing spaces
        If Not IsNothing(fldFirstForename.Value) Then
            fldFirstForename.Value = CStr(fldFirstForename.Value).Trim
        End If
        If Not IsNothing(fldKnownAs.Value) Then
            fldKnownAs.Value = CStr(fldKnownAs.Value).Trim
        End If
        If Not IsNothing(fldOtherForenames.Value) Then
            fldOtherForenames.Value = CStr(fldOtherForenames.Value).Trim
        End If
        If Not IsNothing(fldSurname.Value) Then
            fldSurname.Value = CStr(fldSurname.Value).Trim
        End If

        'Make Email Not Mandatory For Non-Apprenticeship Courses
        If Course.EnrolmentTypeID = 6 Or Course.EnrolmentTypeID = 7 Then 'Part-Time Work Related or ACL
            fldEmail.IsRequired = False
        End If

        Dim countryDataTable As New CountryDataTable
        Dim countryDataView As CCCDataViewDataSet = CCCDataViewDataSet.CreateDataView(countryDataTable)
        countryDataView.Columns.AddDBColumns(True, False)
        countryDataView.Filters.SetColumnFilter(countryDataTable.CountryColumn, "%", FilterOperator.OperatorLike)
        countryDataTable.TableAdapter.Load(countryDataTable, countryDataView)
        CountryDropdown.Items.Clear()
        CountryDropdown.DataSource = countryDataTable
        CountryDropdown.DataTextField = "Description"
        CountryDropdown.DataValueField = "CountryID"
        CountryDropdown.DataSourceID = String.Empty
        CountryDropdown.DataBind()
        CountryDropdown.Items.Insert(0, New ListItem("", String.Empty, False))

        'Dim ctl = fldStudentFirstLanguageID.InternalFieldControl

        If PaymentSubmitter.AllowEmptyBasket And WorkingData.ShoppingCart.Items.Count = 0 Then
            Session("RequestMode") = RequestMode.ApplicationRequest
        End If

        If Not IsPostBack Then

            postcode.Value = WorkingData.EnrolmentRequestRow.PostcodeOut & WorkingData.EnrolmentRequestRow.PostcodeIn
            WorkingData.EnrolmentRequestRow.RestrictedUseAllowContactByEmail = True
        End If

    End Sub

    Public Overrides Sub ValidateControl()
        'First Forename
        If Not IsNothing(fldFirstForename) Then
            If fldFirstForename.Value.ToString Like "* *" Then
                fldFirstForenameValidator.ErrorMessage = "<i class=""fa-solid fa-triangle-exclamation""></i> The First Name should only contain the single first forename. Please add any other names into the Middle Name field"
                fldFirstForenameValidator.IsValid = False
                fldFirstForenameValidator.CssClass = "error alert alert-danger"
                fldFirstForename.CssClass = "ErrorInput"
            End If
        End If

        'DOB
        If Not IsNothing(fldDateOfBirth) Then
            Dim dateOfBirthDate As Date?

            If Not String.IsNullOrEmpty(fldDateOfBirth.Value.ToString) Then
                dateOfBirthDate = CType(fldDateOfBirth.Value, Date)
            End If

            Dim dateToCheckDOB As Date = CDate(Today().Year & "-08-31")
            Dim minAllowedDOB As Date = dateToCheckDOB.AddYears(-16)
            Dim maxAllowedDOB As Date = dateToCheckDOB.AddYears(-70)

            If Not IsNothing(dateOfBirthDate) And dateOfBirthDate > minAllowedDOB Then
                fldDateOfBirthValidator.ErrorMessage = "<i class=""fa-solid fa-triangle-exclamation""></i> You cannot be aged under 16 (on " & dateToCheckDOB.ToString("dd MMM yyyy") & ")"
                fldDateOfBirthValidator.IsValid = False
                fldDateOfBirthValidator.CssClass = "error alert alert-danger"
                fldDateOfBirth.CssClass = "ErrorInput"
            ElseIf Not IsNothing(dateOfBirthDate) And dateOfBirthDate < maxAllowedDOB Then
                fldDateOfBirthValidator.ErrorMessage = "<i class=""fa-solid fa-triangle-exclamation""></i> You cannot be aged over 70 (on " & dateToCheckDOB.ToString("dd MMM yyyy") & ")"
                fldDateOfBirthValidator.IsValid = False
                fldDateOfBirthValidator.CssClass = "error alert alert-danger"
                fldDateOfBirth.CssClass = "ErrorInput"
            End If
        End If

        'Post Code
        If Not IsNothing(postcode) Then
            Dim regexPostCode As New Regex("^([Gg][Ii][Rr] 0[Aa]{2})|((([A-Za-z][0-9]{1,2})|(([A-Za-z][A-Ha-hJ-Yj-y][0-9]{1,2})|(([A-Za-z][0-9][A-Za-z])|([A-Za-z][A-Ha-hJ-Yj-y][0-9][A-Za-z]?))))\s?[0-9][A-Za-z]{2})$")
            Dim match As Match = regexPostCode.Match(postcode.Value)
            If Not match.Success Then
                postcodeValidator.ErrorMessage = "<i class=""fa-solid fa-triangle-exclamation""></i> Please enter a valid Postcode"
                postcodeValidator.IsValid = False
                postcode.Attributes.Add("Class", "textfield form-control ErrorInput")
            End If
        End If

        'Mobile Tel and Tel
        Dim regexTel As New Regex("^(?:\+44\s?|0)[1238]\d\s?(?:\d\s?){7,8}$")
        Dim regexTelGuernsey As New Regex("^\d{11}$")
        Dim matchTel As Match = regexTelGuernsey.Match(fldTel.Value.ToString())

        Dim regexMobileTel As New Regex("(07[\d]{8,12}|447[\d]{7,11})$")
        'Dim regexMobileTelGuernsey As New Regex("^\d{11}$")
        Dim matchMobileTel As Match = regexMobileTel.Match(fldMobileTel.Value.ToString())

        If Not IsNothing(fldMobileTel) Then
            If String.IsNullOrEmpty(fldTel.Value.ToString) And String.IsNullOrEmpty(fldMobileTel.Value.ToString) Then
                fldMobileTelValidator.ErrorMessage = "<i class=""fa-solid fa-triangle-exclamation""></i> Please enter at least a home phone number or a mobile number"
                fldMobileTelValidator.IsValid = False
                fldMobileTelValidator.CssClass = "error alert alert-danger"
                fldMobileTel.CssClass = "ErrorInput"
            End If
        End If

        If Not IsNothing(fldMobileTel) Then
            If Not String.IsNullOrEmpty(fldMobileTel.Value.ToString) And Not matchMobileTel.Success Then
                fldMobileTelValidator.ErrorMessage = "<i class=""fa-solid fa-triangle-exclamation""></i> Please enter a valid UK mobile number starting 07 containing no spaces"
                fldMobileTelValidator.IsValid = False
                fldMobileTelValidator.CssClass = "error alert alert-danger"
                fldMobileTel.CssClass = "ErrorInput"
            End If
        End If

        If Not IsNothing(fldTel) Then
            If Not String.IsNullOrEmpty(fldTel.Value.ToString) And Not matchTel.Success Then
                fldTelValidator.ErrorMessage = "<i class=""fa-solid fa-triangle-exclamation""></i> Please enter a valid Telephone Number"
                fldTelValidator.IsValid = False
                fldTelValidator.CssClass = "error alert alert-danger"
                fldTel.CssClass = "ErrorInput"
            End If
        End If

        MyBase.ValidateControl()
    End Sub

    Private Sub btnBack_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBack.Click

        Dim offeringID As Integer? = WorkingData.ShoppingCart.Items.Item(0).OfferingID
        If Not offeringID >= 0 Then
            offeringID = 0
        End If

        Response.Redirect("/PSWebEnrolment/webenrolment.aspx?page=~/webcontrols/courseapply_GCFE.ascx&OfferingID=" & offeringID.ToString())
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
        Else
            HasFormErrors = True
        End If
    End Sub
End Class
