Imports CompassCC.ProSolution.PSWebEnrolmentKit
Imports Newtonsoft.Json
Imports System.Collections.Generic
Imports System.Linq
Imports CompassCC.CCCSystem.CCCCommon
Imports com.qas.prowebintegration


Partial Class webcontrols_address_matchcode360
    Inherits CheckoutBaseControl

    Private canUseGetAddress As Boolean = SystemSettings.UseGetAddress
    Public Property SearchResults() As List(Of SimpleAddress)
        Get
            If ViewState("_SearchResults") IsNot Nothing Then
                Return ViewState("_SearchResults")
            Else
                ViewState("_SearchResults") = New List(Of SimpleAddress)
                Return ViewState("_SearchResults")
            End If
        End Get
        Set(ByVal value As List(Of SimpleAddress))
            ViewState("_SearchResults") = value
        End Set
    End Property

    Protected Overrides Sub OnInit(e As System.EventArgs)
        MyBase.OnInit(e)
        SetupFields()
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
                    ParseAddressResults(SearchResults(CInt(i)))
                End If
            End If

        End If

    End Sub

    Private Function BuildWebAddress() As String
        Dim url As String = "https://api.getAddress.io/v2/uk/"
        url = url & txtPostcode.Text
        url = url & "?api-key="
        Dim apiKey As String = SystemSettings.GetAddressIOAPIKey
        If CCCBlankLibrary.IsBlank(apiKey) Then
            Throw New CCCTechnicalException("The API Key is blank, please specify your API Key in System Settings")
        End If
        url = url & apiKey
        Return url
    End Function
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

        'If pnlTermTimeAddress.Visible = True AndAlso Len(txtTown.Text) = 0 Then
        '    Dim v As New CustomValidator()
        '    v.ErrorMessage = "You must complete the town field"
        '    v.IsValid = False
        '    Me.Page.Validators.Add(v)
        'End If
        If Me.Page.Validators.Count = 0 Then
            'Hide Search Button
            btnSearch.Visible = False


            SearchForAddress()
            'Perform Search
            Try

                If SearchResults.Count = 1 Then
                    ParseAddressResults(SearchResults(0))
                ElseIf SearchResults.Count > 1 Then
                    AddAmbiguities()
                Else
                    'Can't search, use manual
                    lblNoResults.Visible = True
                    SetupFieldsNoMatchcode360()
                End If
            Catch ex As Exception
                SetupFieldsNoMatchcode360()
            End Try

            ShowResults()
        End If

    End Sub

    Private Sub SearchForAddress()


        Dim wr As Net.WebRequest = Net.WebRequest.Create(BuildWebAddress)
        Dim myResponse As Net.HttpWebResponse

        Try
            myResponse = TryCast(wr.GetResponse, Net.HttpWebResponse)
            ProcessAddressResponse(myResponse)

        Catch ex As Net.WebException
            Dim AdditionalInfo As String = ""
            Dim resp As Net.HttpWebResponse

            resp = TryCast(ex.Response, Net.HttpWebResponse)

            'Returns a 'successful' response 200. Your request was successful.
            'Returns 'not found' error 404. No addresses could be found for this lookup text.
            'Returns 'bad request' error 400. Your lookup text is not valid.
            'Returns 'forbidden' error 401. Your api-credentials are not valid.
            Select Case CInt(resp.StatusCode)
                Case Net.HttpStatusCode.Unauthorized, Net.HttpStatusCode.Forbidden
                    AdditionalInfo = "The API credentials are invalid."
                Case Net.HttpStatusCode.NotFound
                    AdditionalInfo = "Address could not be found for this postcode."
                Case Net.HttpStatusCode.BadRequest
                    AdditionalInfo = "Postcode is invalid or your API key has expired."
                Case 429
                    AdditionalInfo = "You have made more requests than your allowed limit."
                Case 500
                    AdditionalInfo = "Server Error"
            End Select
        End Try

    End Sub

    Private Sub ProcessAddressResponse(myResponse As Net.HttpWebResponse)

        Dim readStream As New IO.StreamReader(myResponse.GetResponseStream)
        Dim AddressData As String = readStream.ReadToEnd()

        If AddressData IsNot Nothing Then
            Dim jObject = Linq.JObject.Parse(AddressData)
        End If

        Dim JsonObject As AddressIOAddresses = Newtonsoft.Json.JsonConvert.DeserializeObject(Of AddressIOAddresses)(AddressData)

        For Each item As String In JsonObject.Addresses
            Dim sa As SimpleAddress = ParseAddressStringToAddress(item)
            'sa.Text = sa.ToString
            SearchResults.Add(sa)
        Next

        readStream.Close()
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
        rowInputHouseNumber.Visible = False
        rowInputStreet.Visible = False
        rowInputTown.Visible = False
        lblKnowPostcode.Visible = False
        lblNoPostcode.Visible = True
        lblManualAddress.Visible = True

    End Sub

    Private Sub AddAmbiguities()
        Dim rowCount As Integer = 0
        pnlAmb.Visible = True
        For Each a As SimpleAddress In SearchResults
            Dim tRow As New TableRow()
            Dim tCell As New TableCell
            Dim hCell As New LinkButton()
            hCell.Attributes.Add("runat", "server")
            tCell.ID = "tCellamb" & Right("00" + rowCount.ToString, 2)
            hCell.ID = "hCellamb" & Right("00" + rowCount.ToString, 2)
            hCell.Text = a.ToString
            hCell.CommandArgument = rowCount
            tCell.Controls.Add(hCell)
            tRow.Cells.Add(tCell)
            AmbTable.Rows.Add(tRow)
            rowCount += 1
        Next
    End Sub


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

        canUseGetAddress = False
    End Sub

    Private Sub ParseAddressResults(resp As SimpleAddress)
        txtAddress1.Text = resp.Address1
        txtAddress2.Text = resp.Address2
        txtAddress3.Text = resp.Address3
        txtAddress4.Text = resp.Address4
        txtOutputPostcode.Text = resp.PostcodeOut & " " & resp.PostcodeIn
        pnlAmb.Visible = False
        If txtAddress1.Text = " " AndAlso txtAddress2.Text = "" AndAlso txtAddress3.Text = "" AndAlso txtAddress4.Text = "" AndAlso txtOutputPostcode.Text = "" Then
            lblNoResults.Visible = True
        End If
    End Sub
    Private Function RemoveEmptyAddressLines(ByVal Address As String) As String
        If Address.Contains(", ,") Then
            Address = Address.Replace(", ,", ",")
            Address = RemoveEmptyAddressLines(Address)
        End If
        Return Address
    End Function
    Private Function ParseAddressStringToAddress(Address As String) As SimpleAddress
        Dim a As String()
        Address = RemoveEmptyAddressLines(Address)
        a = (From x In Address.Split(New Char() {","c})
             Where x.Trim.Length > 0
             Select x).ToArray
        Dim sa As New SimpleAddress

        Dim count As Integer = 0
        For Each addressLine As String In a
            addressLine = addressLine.Trim
            Select Case count
                Case 0
                    sa.Address1 = addressLine
                Case 1
                    sa.Address2 = addressLine
                Case 2
                    sa.Address3 = addressLine
                Case 3
                    sa.Address4 = addressLine
                Case Else
                    If Not CCCBlankLibrary.IsBlank(addressLine) Then
                        If a.Length - 1 = count Then

                        Else
                            If CCCBlankLibrary.IsBlank(sa.Address4) Then
                                sa.Address4 = addressLine
                            Else
                                sa.Address4 = sa.Address4 & ", " & addressLine
                            End If
                        End If

                    End If
            End Select
            count = count + 1
        Next

        sa.PostcodeOut = GetPostcodeOut(txtPostcode.Text)
        sa.PostcodeIn = GetPostcodeIn(txtPostcode.Text)

        Return sa
    End Function
    Private Function GetPostcodeOut(ByVal postcode As String) As String
        Dim strPostcodeOut As String = ""
        postcode = postcode.Replace(" ", "").Trim

        strPostcodeOut = postcode.Substring(0, Len(postcode) - 3)

        Return strPostcodeOut
    End Function
    Private Function GetPostcodeIn(ByVal postcode As String) As String
        Dim strPostcodeIn As String = ""
        postcode = postcode.Replace(" ", "").Trim

        strPostcodeIn = postcode.Substring(Len(postcode) - 3)

        Return strPostcodeIn
    End Function
    Private Sub SetupFields()
        rowInputHouseNumber.Visible = False
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

    Private Sub SetupFieldsNoMatchcode360()
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
            Server.Transfer(GetResourceValue("address_getaddress_aspx"))
        ElseIf WorkingData.ShoppingCart.ContainsItemsOfType("Application") Then
            Response.Redirect(GetResourceValue("checkout_applications_aspx"))
        ElseIf WorkingData.ShoppingCart.ContainsItemsOfType("Enrolment") Then
            Response.Redirect(GetResourceValue("checkout_enrolments_aspx"))
        ElseIf WorkingData.ShoppingCart.ContainsItemsOfType("Enquiry") Then
            Response.Redirect(GetResourceValue("checkout_enquiries_aspx"))
        End If

    End Sub


    Private Class AddressMatches
        Public matches As List(Of String)
    End Class
End Class

Public Class AddressIOAddresses
    Property Latitude As String
    Property Longitude As String
    Property Addresses As String()
End Class

<Serializable()>
Public Class SimpleAddress
    Property Address1 As String
    Property Address2 As String
    Property Address3 As String
    Property Address4 As String
    Property PostcodeOut As String
    Property PostcodeIn As String

    Public Overrides Function ToString() As String
        Return Address1 + ", " + Address2 + ", " + Address3 + ", " + Address4 & ", " + PostcodeOut & " " & PostcodeIn
        Return MyBase.ToString()
    End Function
End Class



