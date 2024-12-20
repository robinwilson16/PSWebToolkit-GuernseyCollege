Imports CompassCC.ProSolution.PSWebEnrolmentKit



Partial Class webcontrols_address_AFD
    Inherits CheckoutBaseControl

    Private cancelFlag As Boolean

    Protected Overrides Sub OnLoad(e As EventArgs)
        MyBase.OnLoad(e)

         'Clear class attributes first
        Me.panel1.Attributes("class") = ""
        Me.alert1.Attributes("class") = ""

        'Breadcrumbs
        If WorkingData.ShoppingCart.ContainsItemsOfType("Application") AndAlso WorkingData.ShoppingCart.ContainsItemsOfType("Enrolment") Then
            breadcrumbenrols.Visible = True
            breadcrumbapps.Visible = False
            Me.panel1.Attributes("class") = "panel panel-danger"
            Me.alert1.Attributes("class") = "alert alert-danger"
        ElseIf WorkingData.ShoppingCart.ContainsItemsOfType("Application") Then
            breadcrumbenrols.Visible = False
            breadcrumbapps.Visible = True
            Me.panel1.Attributes("class") = "panel panel-success"
            Me.alert1.Attributes("class") = "alert alert-success"
        ElseIf WorkingData.ShoppingCart.ContainsItemsOfType("Enrolment") Then
            breadcrumbenrols.Visible = True
            breadcrumbapps.Visible = False
            Me.panel1.Attributes("class") = "panel panel-danger"
            Me.alert1.Attributes("class") = "alert alert-danger"
        ElseIf WorkingData.ShoppingCart.ContainsItemsOfType("Enquiry") Then
            breadcrumbenrols.Visible = False
            breadcrumbapps.Visible = False
            Me.panel1.Attributes("class") = "panel panel-info"
            Me.alert1.Attributes("class") = "alert alert-info"
        Else
            breadcrumbenrols.Visible = False
            breadcrumbapps.Visible = False
            Me.panel1.Attributes("class") = "panel panel-danger"
            Me.alert1.Attributes("class") = "alert alert-danger"
        End If


        If IsPostBack Then
            UpdateAddress()
        Else
            postcode.Value = WorkingData.EnrolmentRequestRow.PostcodeOut + WorkingData.EnrolmentRequestRow.PostcodeIn
        End If
    End Sub

    Private Sub RunSearch()
        lstResult.Enabled = False
        'Windows.Forms.Cursor.Current = Windows.Forms.Cursors.WaitCursor

        Static running As Boolean
        Dim details As New AFDPostCodeAPI.afdAddressData
        Dim retVal As Long

        ' Check if we are already running to prevent crossing over items in the listbox
        If running Then Exit Sub
        running = True

        ' Make object instance
        Dim afdObj As AFDPostCodeAPI
        afdObj = New AFDPostCodeAPI

        ' Replace lstResult with the name of your list box for the results
        With lstResult

            ' Clear out any existing items in the list
            .Items.Clear()

            ' Reset Cancel Flag
            cancelFlag = False

            ' Set the lookup
            details.Lookup = txtLookup.Text ' Change txtLookup to your lookup entry textbox      ' Carry out the lookup (no need to alter the line below, unless you want to add a sector skip option - see constants)
            retVal = afdObj.AFDData(AFDPostCodeAPI.afdFieldSpec, AFDPostCodeAPI.AFD_FASTFIND_LOOKUP + AFDPostCodeAPI.AFD_SHOW_ERROR, details)

            If retVal < 0 Then ' Error/User Cancelled
                running = False
                Exit Sub
            End If
            .Items.Add(" ")
            ' Now add matching records to the list box
            Do While retVal >= 0
                If retVal <> AFDPostCodeAPI.AFD_RECORD_BREAK Then
                    ' Add the item to the list box, including hidden key item for easy record retrieval
                    .Items.Add(details.List + Space(512) + details.Key)
                End If
                retVal = afdObj.AFDData(AFDPostCodeAPI.afdFieldSpec, AFDPostCodeAPI.AFD_GET_NEXT + AFDPostCodeAPI.AFD_FASTFIND_LOOKUP + AFDPostCodeAPI.AFD_SHOW_ERROR, details)
                If cancelFlag = True Then
                    Dim v As New CustomValidator
                    v.ErrorMessage = "Lookup cancelled. Please enter your address manually."
                    v.IsValid = False
                    Me.Page.Validators.Add(v)
                    running = False
                    Exit Sub
                End If
            Loop

            ' Check results have been returned
            If .Items.Count() = 0 Then
                Dim v As New CustomValidator
                v.ErrorMessage = "No results found. Please enter your address manually."
                v.IsValid = False
                Me.Page.Validators.Add(v)
            Else
                .SelectedIndex = 0 ' Select First item in the list
            End If

            running = False

        End With
        lstResult.Enabled = True
        'Windows.Forms.Cursor.Current = Windows.Forms.Cursors.Default
    End Sub

    Private Sub btnFind_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFind.Click
        Try
            RunSearch()
        Catch ex As Exception
            Dim v As New CustomValidator
            v.ErrorMessage = "An error occurred when attempting to search. There may be a problem with the postcode software. Please enter your address manually."
            v.IsValid = False
            Me.Page.Validators.Add(v)

        End Try
    End Sub

    Private Sub UpdateAddress()
        Dim details As New AFDPostCodeAPI.afdAddressData
        Dim lstStr As String
        Dim retVal As Long
        ' Make object instance
        Dim afdObj As AFDPostCodeAPI
        afdObj = New AFDPostCodeAPI

        ' Replace lstResult with the name of your list box for the results
        With lstresult

            ' Check a valid item is selected
            If .SelectedIndex < 1 Then
                Dim v As New CustomValidator
                v.ErrorMessage = "No Item Selected."
                v.IsValid = False
                Me.Page.Validators.Add(v)
                Exit Sub
            End If

            lstStr = CStr(.Items.Item(.SelectedIndex).ToString)

            ' Set DLL parameters to retrieve the selected record
            details.Key = lstStr.Substring(lstStr.Length - 40, 40).Trim

        End With

        ' Carry out the lookup (no need to alter the line below, unless you want to add a sector skip option - see constants)
        retVal = afdObj.AFDData(AFDPostCodeAPI.afdFieldSpec, AFDPostCodeAPI.AFD_RETRIEVE_RECORD, details)

        ' Abort with Message if error
        If retVal < 0 Then
            MsgBox(afdObj.AFDErrorText(retVal))
            Exit Sub
        End If

        ' Now Assign required fields to your application
        ' These are any of the members of the details. structure
        txtAddress1.Value = Trim(Trim(details.Prop) & " " & Trim(details.Street))
        txtAddress2.Value = Trim(details.Locality)
        txtAddress3.Value = Trim(details.Town)
        txtAddress4.Value = Trim(details.PostalCounty)
        postcode.Value = Trim(details.Postcode)
    End Sub

    Private Sub btnContinue_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnContinue.Click
       
        Me.Page.Validate()

        If Me.Page.IsValid Then

         
                WorkingData.EnrolmentRequest(0).Address1 = txtAddress1.Value
                WorkingData.EnrolmentRequest(0).Address2 = txtAddress2.Value
                WorkingData.EnrolmentRequest(0).Address3 = txtAddress3.Value
                WorkingData.EnrolmentRequest(0).Address4 = txtAddress4.Value

            Dim pcode As String = Replace(postcode.Value, " ", "")
            If Len(pcode) > 0 Then

                'avoid dodgy postcodes breaking system
                Try
                    WorkingData.EnrolmentRequest(0).PostcodeOut = pcode.Substring(0, pcode.Length - 3)
                Catch ex As ArgumentOutOfRangeException
                    WorkingData.EnrolmentRequest(0).PostcodeOut = ""
                End Try

                Try
                    WorkingData.EnrolmentRequest(0).PostcodeIn = Right(pcode, 3)
                Catch ex As ArgumentOutOfRangeException
                    WorkingData.EnrolmentRequest(0).PostcodeIn = ""
                End Try


            End If

            Dim blnApplicationsOnly As Boolean = WorkingData.ShoppingCart IsNot Nothing AndAlso Not WorkingData.ShoppingCart.ContainsItemsOfType("Enrolment")

            If blnApplicationsOnly Then
                Response.Redirect(GetResourceValue("checkout_applications2_aspx"))
            Else
                Response.Redirect(GetResourceValue("checkout_enrolments2_aspx"))
            End If

        End If

    End Sub

    Public Overrides Sub ValidateControl()

        If Len(txtAddress1.Value) = 0 Then
            Dim v As New CustomValidator
            v.ErrorMessage = "You must enter the 1st line of the address"
            v.IsValid = False
            Me.Page.Validators.Add(v)
        End If

        If Len(txtAddress3.Value) = 0 Then
            Dim v As New CustomValidator
            v.ErrorMessage = "You must enter the town"
            v.IsValid = False
            Me.Page.Validators.Add(v)
        End If

         If Len(postcode.Value) = 0 Then
            Dim v As New CustomValidator
            v.ErrorMessage = "You must enter the postcode"
            v.IsValid = False
            Me.Page.Validators.Add(v)
        End If

    End Sub

    Protected Sub btnBack_Click(sender As Object, e As EventArgs) Handles btnBack.Click


        If WorkingData.ShoppingCart.ContainsItemsOfType("Application") Then
            Response.Redirect(GetResourceValue("checkout_applications_aspx"))
        ElseIf WorkingData.ShoppingCart.ContainsItemsOfType("Enrolment") Then
            Response.Redirect(GetResourceValue("checkout_enrolments_aspx"))
        ElseIf WorkingData.ShoppingCart.ContainsItemsOfType("Enquiry") Then
            Response.Redirect(GetResourceValue("checkout_enquiries_aspx"))
        End If

    End Sub
End Class
