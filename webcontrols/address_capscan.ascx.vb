Imports CompassCC.ProSolution.PSWebEnrolmentKit
Imports CapscanService
Imports System.Collections.Generic
Imports System.Linq
Imports CompassCC.CCCSystem.CCCCommon
Imports com.qas.prowebintegration


Partial Class webcontrols_address_capscan
    Inherits CheckoutBaseControl

    Private CapscanClientID As String = SystemSettings.Capscan_ClientID
    Private CapscanAccessCode As String = SystemSettings.Capscan_AccessCode
    Private datasets As DSGlist
    Private ambiguities As AmbiguousElement()
    Shared capscan As CapscanOnDemandSoapClient
    Private canUseCapscan As Boolean

    Protected Overrides Sub OnInit(e As System.EventArgs)
        MyBase.OnInit(e)

        If Not IsPostBack Then
            capscan = Nothing
        End If

        If capscan Is Nothing Then
            Try
                capscan = New CapscanOnDemandSoapClient("CapscanOnDemandSoap12")
                datasets = capscan.GetDatasets(CapscanClientID, CapscanAccessCode)
                canUseCapscan = True
                SetupFields()
            Catch ex As Exception
                canUseCapscan = False
                SetupFieldsNoCapscan()
            End Try

        End If



    End Sub

    Protected Sub Page_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreRender
        Dim HasEnrolments As Boolean = WorkingData.ShoppingCart IsNot Nothing AndAlso WorkingData.ShoppingCart.ContainsItemsOfType("Enrolment")
        pnlTermTimeAddress.Visible = HasEnrolments And Not PSWebEnrolmentProperties.IsCurrentlyTakingAltAddress

    End Sub


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

         'Clear class attributes first
        Me.Panel1.Attributes("class") = ""
        Me.alert1.Attributes("class") = ""

        'Breadcrumbs
        If WorkingData.ShoppingCart.ContainsItemsOfType("Application") AndAlso WorkingData.ShoppingCart.ContainsItemsOfType("Enrolment") Then
            breadcrumbenrols.Visible = True
            breadcrumbapps.Visible = False
            Me.Panel1.Attributes("class") = "panel panel-danger"
            Me.alert1.Attributes("class") = "alert alert-danger"
        ElseIf WorkingData.ShoppingCart.ContainsItemsOfType("Application") Then
            breadcrumbenrols.Visible = False
            breadcrumbapps.Visible = True
            Me.Panel1.Attributes("class") = "panel panel-success"
            Me.alert1.Attributes("class") = "alert alert-success"
        ElseIf WorkingData.ShoppingCart.ContainsItemsOfType("Enrolment") Then
            breadcrumbenrols.Visible = True
            breadcrumbapps.Visible = False
            Me.Panel1.Attributes("class") = "panel panel-danger"
            Me.alert1.Attributes("class") = "alert alert-danger"
        ElseIf WorkingData.ShoppingCart.ContainsItemsOfType("Enquiry") Then
            breadcrumbenrols.Visible = False
            breadcrumbapps.Visible = False
            Me.Panel1.Attributes("class") = "panel panel-info"
            Me.alert1.Attributes("class") = "alert alert-info"
        Else
            breadcrumbenrols.Visible = False
            breadcrumbapps.Visible = False
            Me.Panel1.Attributes("class") = "panel panel-danger"
            Me.alert1.Attributes("class") = "alert alert-danger"
        End If

        If Not IsPostBack Then
            cboCountry.Items.Add("United Kingdom")
            ''CCCPS-77326: Setting post code set on previous page for Main Address, by default.
            If Not PSWebEnrolmentProperties.IsCurrentlyTakingAltAddress Then
                txtPostcode.Text = WorkingData.EnrolmentRequestRow.PostcodeOut & WorkingData.EnrolmentRequestRow.PostcodeIn
            End If
        Else

            Dim c As Control = GetPostBackControl(Me.Page)

            If Not c Is Nothing Then

            Else
                Dim ctrlname As String = Page.Request.Params.Get("__EVENTTARGET")
                If Not CCCBlankLibrary.IsBlank(ctrlname) AndAlso ctrlname.Contains("amb") Then
                    Dim i As String = ctrlname.Substring(ctrlname.IndexOf("amb") + 3, 2)
                    Dim resp As ResponseGrp = ViewState("ResponseGroup")
                    Dim searchstatus As String = ""

                    For Each a As DataElement In resp.DataLines
                        If a.Display = "SEARCHSTATUS" Then
                            searchstatus = a.Value
                        End If
                    Next
                    Try
                        Dim lResponseGroup As ResponseGrp = capscan.query(CapscanClientID, CapscanAccessCode, i.ToString, searchstatus, "", "1", "", "", "")


                        ViewState.Remove("ResponseGroup")
                        ViewState.Add("ResponseGroup", lResponseGroup)

                        If lResponseGroup.RequestStatus = "OK" Then
                            If lResponseGroup.AddressStatus = "RESOLVED" Then

                                ShowResults()


                                ParseAddressResults(lResponseGroup)

                            ElseIf lResponseGroup.AddressStatus = "AMBIGUITY" Then
                                ambiguities = lResponseGroup.Options

                                If ambiguities.Length > 0 Then
                                    AddAmbiguities()
                                End If
                            End If

                        Else
                            'Can't search, use manual
                            lblNoResults.Visible = True
                            SetupFieldsNoCapscan()
                        End If
                    Catch ex As Exception
                        SetupFieldsNoCapscan()
                    End Try
                End If
            End If

        End If

    End Sub

    Public Overrides Sub ValidateControl()

        If Len(txtAddress1.Text) = 0 Then
            Dim v As New CustomValidator
            v.ErrorMessage = "You must enter the 1st line of the address"
            v.IsValid = False
            Me.Page.Validators.Add(v)
        End If

        If Len(txtAddress3.Text) = 0 Then
            Dim v As New CustomValidator
            v.ErrorMessage = "You must enter the town"
            v.IsValid = False
            Me.Page.Validators.Add(v)
        End If

        If Len(txtOutputPostcode.Text) = 0 Then
            Dim v As New CustomValidator
            v.ErrorMessage = "You must enter the postcode"
            v.IsValid = False
            Me.Page.Validators.Add(v)
        End If

    End Sub
    Protected Sub lnkNoPostCode_Click(sender As Object, e As System.EventArgs) Handles lnkNoPostCode.Click

        rowInputPostalCode.Visible = False
        rowInputStreet.Visible = True
        rowInputTown.Visible = True
        rowInputHouseNumber.Visible = True
        lblKnowPostcode.Visible = True
        lblNoPostcode.Visible = False

    End Sub

    Protected Sub lnkpostcode_Click(sender As Object, e As EventArgs) Handles lnkPostCode.Click

        rowInputPostalCode.Visible = True
        rowInputStreet.Visible = False
        rowInputTown.Visible = False
        rowInputHouseNumber.Visible = True
        lblKnowPostcode.Visible = False
        lblNoPostcode.Visible = True
    End Sub

    Private Sub cboCountry_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles cboCountry.SelectedIndexChanged

    End Sub

    Private Sub btnSearch_Click(sender As Object, e As System.EventArgs) Handles btnSearch.ServerClick

        'Check for required fields
        If txtHouseNo.Visible = True AndAlso Len(txtHouseNo.Text) = 0 Then
            Dim v As New CustomValidator()
            v.ErrorMessage = "You must complete the House No. field"
            v.IsValid = False
            Me.Page.Validators.Add(v)
        End If

        If txtPostcode.Visible = True AndAlso Len(txtPostcode.Text) = 0 Then
            Dim v As New CustomValidator()
            v.ErrorMessage = "You must complete the Postcode field"
            v.IsValid = False
            Me.Page.Validators.Add(v)
        End If

        If txtStreet.Visible = True AndAlso Len(txtStreet.Text) = 0 Then
            Dim v As New CustomValidator()
            v.ErrorMessage = "You must complete the street field"
            v.IsValid = False
            Me.Page.Validators.Add(v)
        End If

        If txtTown.Visible = True AndAlso Len(txtTown.Text) = 0 Then
            Dim v As New CustomValidator()
            v.ErrorMessage = "You must complete the town field"
            v.IsValid = False
            Me.Page.Validators.Add(v)
        End If

        If pnlTermTimeAddress.Visible = True AndAlso Len(txtTown.Text) = 0 Then
            Dim v As New CustomValidator()
            v.ErrorMessage = "You must complete the town field"
            v.IsValid = False
            Me.Page.Validators.Add(v)
        End If


        If Me.Page.Validators.Count = 0 Then



            'Hide Search Button
            btnSearch.Visible = False

            Dim strSearchParameters As New StringBuilder
            If txtHouseNo.Visible = True Then strSearchParameters.Append(txtHouseNo.Text & ",")
            If txtPostcode.Visible = True Then strSearchParameters.Append(txtPostcode.Text & ",")
            If txtStreet.Visible = True Then strSearchParameters.Append(txtStreet.Text & ",")
            If txtTown.Visible = True Then strSearchParameters.Append(txtTown.Text & ",")

            Dim s As String = strSearchParameters.ToString.Remove(strSearchParameters.ToString.Length - 1, 1)

            'Perform Search
            Try
                Dim lResponseGroup As ResponseGrp = capscan.query(CapscanClientID, CapscanAccessCode, "", s, "", "1", "", "", "")

                ViewState.Remove("ResponseGroup")
                ViewState.Add("ResponseGroup", lResponseGroup)

                If lResponseGroup.RequestStatus = "OK" Then
                    If lResponseGroup.AddressStatus = "RESOLVED" Then

                        'Display Results

                        ShowResults()

                        ParseAddressResults(lResponseGroup)

                    ElseIf lResponseGroup.AddressStatus = "AMBIGUITY" Then
                        ambiguities = lResponseGroup.Options

                        If ambiguities.Length > 0 Then
                            AddAmbiguities()
                        End If
                    End If

                Else
                    'Can't search, use manual
                    lblNoResults.Visible = True
                    SetupFieldsNoCapscan()
                End If
            Catch ex As Exception
                SetupFieldsNoCapscan()
            End Try

            ShowResults()
        End If

    End Sub

    Protected Sub btnContinue_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnContinue.Click
        Me.Page.Validate()

        If Me.Page.IsValid Then

            If PSWebEnrolmentProperties.IsCurrentlyTakingAltAddress Then
                WorkingData.EnrolmentRequest(0).AltAddress1 = txtAddress1.Text
                WorkingData.EnrolmentRequest(0).AltAddress2 = txtAddress2.Text
                WorkingData.EnrolmentRequest(0).AltAddress3 = txtAddress3.Text
                WorkingData.EnrolmentRequest(0).AltAddress4 = txtAddress4.Text
                Dim strPostCode As String() = txtOutputPostcode.Text.Trim.Split(" "c)
                If strPostCode.Length > 1 Then
                    WorkingData.EnrolmentRequest(0).AltPostcodeOut = strPostCode(0).ToUpper
                    WorkingData.EnrolmentRequest(0).AltPostcodeIn = strPostCode(1).ToUpper
                ElseIf strPostCode.Length = 1 Then
                    WorkingData.EnrolmentRequest(0).AltPostcodeOut = Left(strPostCode(0), 4)
                    WorkingData.EnrolmentRequest(0).AltPostcodeIn = Right(strPostCode(0), 3)
                End If


            Else
                WorkingData.EnrolmentRequest(0).Address1 = txtAddress1.Text
                WorkingData.EnrolmentRequest(0).Address2 = txtAddress2.Text
                WorkingData.EnrolmentRequest(0).Address3 = txtAddress3.Text
                WorkingData.EnrolmentRequest(0).Address4 = txtAddress4.Text
                Dim strPostCode As String() = txtOutputPostcode.Text.Trim.Split(" "c)
                If strPostCode.Length > 1 Then
                    WorkingData.EnrolmentRequest(0).PostcodeOut = strPostCode(0).ToUpper
                    WorkingData.EnrolmentRequest(0).PostcodeIn = strPostCode(1).ToUpper
                ElseIf strPostCode.Length = 1 Then
                    WorkingData.EnrolmentRequest(0).PostcodeOut = Left(strPostCode(0), 4)
                    WorkingData.EnrolmentRequest(0).PostcodeIn = Right(strPostCode(0), 3)
                End If
            End If


            Dim blnApplicationsOnly As Boolean = WorkingData.ShoppingCart IsNot Nothing AndAlso Not WorkingData.ShoppingCart.ContainsItemsOfType("Enrolment")

            If blnApplicationsOnly Then
                Response.Redirect(GetResourceValue("checkout_applications2_aspx"))

            ElseIf optAnotherAddress.SelectedValue = "0" OrElse PSWebEnrolmentProperties.IsCurrentlyTakingAltAddress Then
                Response.Redirect(GetResourceValue("checkout_enrolments2_aspx"))
            Else
                'Need to capture another address
                PSWebEnrolmentProperties.IsCurrentlyTakingAltAddress = True
                Server.Transfer(GetResourceValue("address_0_aspx"))
            End If

        End If




    End Sub


    Private Sub btnClear_Click(sender As Object, e As System.EventArgs) Handles btnClear.ServerClick


        'Clear and hide output fields
        txtAddress1.Text = ""
        txtAddress2.Text = ""
        txtAddress3.Text = ""
        txtAddress4.Text = ""
        txtOutputPostcode.Text = ""
        txtPostcode.Text = ""
        txtStreet.Text = ""
        txtTown.Text = ""
        txtHouseNo.Text = ""

        rowOutputAddress1.Visible = False
        rowOutputAddress2.Visible = False
        rowOutputAddress3.Visible = False
        rowOutputAddress4.Visible = False
        rowOutputPostcode.Visible = False

        'Show Search Button
        btnSearch.Visible = True

        'Hide Ambiguities list
        pnlAmb.Visible = False

        'Restore default search
        rowInputPostalCode.Visible = True
        rowInputHouseNumber.Visible = True
        rowInputStreet.Visible = False
        rowInputTown.Visible = False
        lblKnowPostcode.Visible = False
        lblNoPostcode.Visible = True
        lblManualAddress.Visible = True

    End Sub

    Private Sub AddAmbiguities()

        Dim rowCount As Integer = 0
        pnlAmb.Visible = True

        For Each a As AmbiguousElement In ambiguities
            Dim tRow As New TableRow()
            Dim tCell As New TableCell
            Dim hCell As New LinkButton()
            hCell.Attributes.Add("runat", "server")

            tCell.ID = "tCellamb" & Right("00" & rowCount.ToString, 2)
            hCell.ID = "hCellamb" & Right("00" & rowCount.ToString, 2)
            hCell.Text = a.text
            hCell.CommandArgument = rowCount



            tCell.Controls.Add(hCell)
            tRow.Cells.Add(tCell)

            AmbTable.Rows.Add(tRow)

            rowCount += 1
        Next

    End Sub



    'Private Function GetPostBackControl(page As Page) As Control

    '    Dim ctrl As Control = Nothing


    '    Dim ctrlname As String = page.Request.Params.Get("__EVENTTARGET")

    '    If Not ctrlname Is Nothing AndAlso ctrlname.Length > 0 Then

    '        ctrl = page.FindControl(ctrlname)

    '    Else

    '        For Each ctl As String In page.Request.Form

    '            Dim c As Control = page.FindControl(ctl)
    '            If Not c Is Nothing AndAlso c.GetType Is GetType(System.Web.UI.WebControls.Button) Then

    '                Return c
    '            End If

    '        Next

    '    End If

    '    Return ctrl

    'End Function




    Protected Sub lnkManualAddress_Click(sender As Object, e As System.EventArgs) Handles lnkManualAddress.Click

        rowOutputAddress1.Visible = True
        rowOutputAddress2.Visible = True
        rowOutputAddress3.Visible = True
        rowOutputAddress4.Visible = True
        rowOutputPostcode.Visible = True


        btnSearch.Visible = False
        btnClear.Visible = False

        pnlAmb.Visible = False

        rowInputPostalCode.Visible = False
        rowInputStreet.Visible = False
        rowInputTown.Visible = False
        rowInputHouseNumber.Visible = False
        lblKnowPostcode.Visible = False
        lblNoPostcode.Visible = False
        lblManualAddress.Visible = False

        canUseCapscan = False
    End Sub

    Private Sub ParseAddressResults(resp As ResponseGrp)

        Dim strBuildingNo As String = ""
        Dim strBuildingName As String = ""
        Dim strStreet As String = ""
        Dim strTown As String = ""
        Dim strCounty As String = ""
        Dim strPostcode As String = ""
        Dim strLocality As String = ""



        For Each item As DataElement In resp.DataLines
            If item.Name = "BUILDINGNAME" Then
                strBuildingName = item.Value
            ElseIf item.Name = "BUILDINGNUMBER" Then
                strBuildingNo = item.Value
            ElseIf item.Name = "COUNTY" Then
                strCounty = item.Value
            ElseIf item.Name = "STREET" Then
                strStreet = item.Value
            ElseIf item.Name = "TOWN" Or item.Name = "POSTTOWN" Then
                strTown = item.Value
            ElseIf item.Name = "POSTCODE" Then
                strPostcode = item.Value
            ElseIf item.Name = "LOCALITY" Then
                strLocality = item.Value
            End If
        Next

        txtAddress1.Text = CCCBlankLibrary.NoBlank(strBuildingNo, strBuildingName) & " " & strStreet
        txtAddress2.Text = strLocality
        txtAddress3.Text = strTown
        txtAddress4.Text = strCounty
        txtOutputPostcode.Text = strPostcode

        If txtAddress1.Text = " " AndAlso txtAddress2.Text = "" AndAlso txtAddress3.Text = "" AndAlso txtAddress4.Text = "" AndAlso txtOutputPostcode.Text = "" Then
            lblNoResults.Visible = True
        End If

    End Sub

    Private Sub SetupFields()
        rowInputHouseNumber.Visible = True
        rowInputPostalCode.Visible = True
        rowInputStreet.Visible = False
        rowInputTown.Visible = False

        btnSearch.Visible = True
        btnClear.Visible = True

        lnkManualAddress.Visible = True
        lblKnowPostcode.Visible = False
        lblNoPostcode.Visible = True

        rowOutputAddress1.Visible = False
        rowOutputAddress2.Visible = False
        rowOutputAddress3.Visible = False
        rowOutputAddress4.Visible = False
        rowOutputPostcode.Visible = False
    End Sub

    Private Sub SetupFieldsNoCapscan()
        rowInputHouseNumber.Visible = False
        rowInputPostalCode.Visible = False
        rowInputStreet.Visible = False
        rowInputTown.Visible = False

        btnSearch.Visible = False
        btnClear.Visible = False

        lnkManualAddress.Visible = False
        lblKnowPostcode.Visible = False
        lblNoPostcode.Visible = False

        rowOutputAddress1.Visible = True
        rowOutputAddress2.Visible = True
        rowOutputAddress3.Visible = True
        rowOutputAddress4.Visible = True
        rowOutputPostcode.Visible = True
    End Sub


    Private Sub ShowResults()
        rowInputHouseNumber.Visible = False
        rowInputPostalCode.Visible = False

        rowOutputAddress1.Visible = True
        rowOutputAddress2.Visible = True
        rowOutputAddress3.Visible = True
        rowOutputAddress4.Visible = True
        rowOutputPostcode.Visible = True

        btnSearch.Visible = False
        btnClear.Visible = False
        lnkManualAddress.Visible = False

        lnkManualAddress.Visible = False
        lblKnowPostcode.Visible = False
        lblNoPostcode.Visible = False

    End Sub

    Protected Sub btnBack_Click(sender As Object, e As EventArgs) Handles btnBack.Click
        ''CCCPS-77326: Redirecting to main address again when back pressed from term address screen.
        If PSWebEnrolmentProperties.IsCurrentlyTakingAltAddress Then
            PSWebEnrolmentProperties.IsCurrentlyTakingAltAddress = False
            Server.Transfer(GetResourceValue("address_capscan_aspx"))
        ElseIf WorkingData.ShoppingCart.ContainsItemsOfType("Application") Then
            Response.Redirect(GetResourceValue("checkout_applications_aspx"))
        ElseIf WorkingData.ShoppingCart.ContainsItemsOfType("Enrolment") Then
            Response.Redirect(GetResourceValue("checkout_enrolments_aspx"))
        ElseIf WorkingData.ShoppingCart.ContainsItemsOfType("Enquiry") Then
            Response.Redirect(GetResourceValue("checkout_enquiries_aspx"))
        End If

    End Sub


End Class

