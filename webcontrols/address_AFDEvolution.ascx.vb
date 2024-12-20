Imports CompassCC.ProSolution.PSWebEnrolmentKit
Imports CompassCC.CCCSystem.CCCCommon


Partial Class webcontrols_address_AFDEvolution
    Inherits CheckoutBaseControl


    Private ReadOnly Property AFD_SERVER As String
        Get
            Return SystemSettings.AFDEvolution_Server
        End Get
    End Property

    Private ReadOnly Property AFD_SERIAL_NUMBER As String
        Get
            Return SystemSettings.AFDEvolution_SerialNumber
        End Get
    End Property

    Private ReadOnly Property AFD_PASSWORD As String
        Get
            Return SystemSettings.AFDEvolution_Password
        End Get
    End Property

    Private ReadOnly Property AFD_USER_ID As String
        Get
            Return SystemSettings.AFDEvolution_UserID
        End Get
    End Property
#Region "CCCPS-82076: Properties needed to optimize usage of AFD Postcode lookup"
    ''' <summary>
    ''' Set 'False' if you want to stop maintaining postcode and addresses locally during single session. Keeping this on will help us to reduce number of API calls made to find and retrieve addresses.
    ''' </summary>
    ''' <returns></returns>
    Private ReadOnly Property MaintainDataLocally As Boolean
        Get
            Return True
        End Get
    End Property
    ''' <summary>
    ''' Set 'True' if you want user to use lookup with a postcode in a valid format only.
    ''' </summary>
    ''' <returns></returns>
    Private ReadOnly Property ValidateBeforeSearch As Boolean
        Get
            Return False
        End Get
    End Property
    ''' <summary>
    ''' This property is needed to limit user from performing too many lookup operations.
    ''' </summary>
    ''' <returns></returns>
    Private Property LookupLimit As Integer
        Get
            If Session("LookupLimit") Is Nothing Then
                Session("LookupLimit") = -1 ''Set a number here to limit a user to perform lookup. Any number less than 0 means no limit will be applied.
            End If
            Return Session("LookupLimit")
        End Get
        Set(value As Integer)
            Session("LookupLimit") = value
        End Set
    End Property
    ''' <summary>
    ''' A property to hold addresses of already searched postcodes. With this, we will be able to fetch address list locally without calling an API, when same postcode is searched repetitively.
    ''' </summary>
    ''' <returns></returns>
    Private Property SearchedCodes As System.Collections.Generic.Dictionary(Of String, System.Xml.XmlDocument)
        Get
            If Session("SearchedCodes") Is Nothing Then
                Session("SearchedCodes") = New System.Collections.Generic.Dictionary(Of String, System.Xml.XmlDocument)
            End If
            Return Session("SearchedCodes")
        End Get
        Set(value As System.Collections.Generic.Dictionary(Of String, System.Xml.XmlDocument))
            Session("SearchedCodes") = value
        End Set
    End Property
    ''' <summary>
    ''' A property to hold selected addresses (by user). With this, we will be able to retrieve address details locally without calling an API, when same address is selected repetitively.
    ''' </summary>
    ''' <returns></returns>
    Private Property FetchedAddresses As System.Collections.Generic.Dictionary(Of String, System.Xml.XmlDocument)
        Get
            If Session("FetchedAddresses") Is Nothing Then
                Session("FetchedAddresses") = New System.Collections.Generic.Dictionary(Of String, System.Xml.XmlDocument)
            End If
            Return Session("FetchedAddresses")
        End Get
        Set(value As System.Collections.Generic.Dictionary(Of String, System.Xml.XmlDocument))
            Session("FetchedAddresses") = value
        End Set
    End Property
#End Region

    Private Enum SearchType
        FastFind
        Search
        Retrieve
    End Enum


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
            If Request.Form("__EVENTTARGET") IsNot Nothing Then
                ''CCCPS-82076: We have to handle click event like this because of StudentEnrolmentField which we have used to display address details.
                ''Some internal logic of StudentEnrolmentField gets executed before the Buton Click event which causes an issue in loading proper data.
                If Request.Form("__EVENTTARGET").EndsWith("btnFind") Then
                    btnFetchAddress.Enabled = False
                    findAddress()
                    If lstresult.SelectedIndex >= 0 Then
                        btnFetchAddress.Enabled = True
                    End If
                ElseIf Request.Form("__EVENTTARGET").EndsWith("btnFetchAddress") Then
                    UpdateAddress()
                ElseIf Request.Form("__EVENTTARGET").EndsWith("btnCSA") Then
                    SearchedCodes.Clear()
                    FetchedAddresses.Clear()
                End If
            End If
        Else
            btnFetchAddress.Enabled = False
            postcode.Value = WorkingData.EnrolmentRequestRow.PostcodeOut & WorkingData.EnrolmentRequestRow.PostcodeIn
        End If
    End Sub

    Private Sub UpdateAddress()

        ' Declare XML Objects and variables
        Dim xmlDoc As System.Xml.XmlDocument
        Dim lstStr As String
        Dim xmlLocation As String

        ' Replace lstResult with the name of your list box for the results
        With lstresult

            If lstresult.Items.Count = 0 Then
                Exit Sub
            Else
                ' Check a valid item is selected
                If .SelectedIndex < 0 Then
                    Dim v As New CustomValidator
                    v.ErrorMessage = "Please select an item from the list."
                    v.IsValid = False
                    Me.Page.Validators.Add(v)

                    Exit Sub
                End If
            End If

            ' Set XML parameter to retrieve the selected record
            lstStr = .Items.Item(.SelectedIndex).ToString
            ' Finished with the list box
        End With
        Dim addressKey As String = lstStr.Substring(lstStr.Length - 40, 40).Trim
        ''CCCPS-82076: No need to call an API to fetch address if we have it locally. 
        If MaintainDataLocally AndAlso FetchedAddresses.ContainsKey(addressKey) Then
            bindAddress(FetchedAddresses(addressKey))
            Return
        End If
        ' Create the XML Document Object Instance
        xmlDoc = New System.Xml.XmlDocument()

        ' Build up the XML query string
        xmlLocation = BuildServerDetails(SearchType.Retrieve)
        xmlLocation = xmlLocation & "&Key=" & addressKey

        ' Load the XML from the webserver with the query string
        Try
            xmlDoc.Load(xmlLocation)
            ''CCCPS-82076: Maintain retrieved address in session to reload locally if user selects it again. 
            If MaintainDataLocally Then FetchedAddresses.Add(addressKey, xmlDoc)
        Catch
            Dim v As New CustomValidator
            v.ErrorMessage = "Error loading address results, please enter address manually"
            v.IsValid = False
            Me.Page.Validators.Add(v)

            Exit Sub

        End Try

        bindAddress(xmlDoc)

    End Sub
    ''' <summary>
    ''' CCCPS-82076: Separated from updateAddress() method as this part was needed to load address from session as well.
    ''' </summary>
    ''' <param name="xmlDoc"></param>
    Private Sub bindAddress(xmlDoc As System.Xml.XmlDocument)
        Dim root As System.Xml.XmlNode
        Dim dataNode As System.Xml.XmlNode
        Dim itemNodes As System.Xml.XmlNodeList
        ' Check if PCE returned an error and if the document is valid
        root = xmlDoc.DocumentElement
        dataNode = root.SelectSingleNode("Result")
        itemNodes = root.SelectNodes("Item")
        If dataNode Is Nothing Or itemNodes Is Nothing Then
            Dim v As New CustomValidator
            v.ErrorMessage = "Error loading address results, please enter address manually"
            v.IsValid = False
            Me.Page.Validators.Add(v)
            Exit Sub
        End If
        If Val(dataNode.InnerText) < 1 Then
            dataNode = root.SelectSingleNode("ErrorText")
            If dataNode Is Nothing Then
                Dim v As New CustomValidator
                v.ErrorMessage = "Error loading address results, please enter address manually"
                v.IsValid = False
                Me.Page.Validators.Add(v)
            Else
                Dim v As New CustomValidator
                v.ErrorMessage = dataNode.InnerText & vbCrLf & "Please enter address manually"
                v.IsValid = False
                Me.Page.Validators.Add(v)

            End If
            Exit Sub
        End If


        Dim strProperty As String = String.Empty

        dataNode = itemNodes(0).SelectSingleNode("Property")
        If Not (dataNode Is Nothing) Then strProperty = dataNode.InnerText

        dataNode = itemNodes(0).SelectSingleNode("Street")
        If Not (dataNode Is Nothing) Then
            If Not CCCBlankLibrary.IsBlank(strProperty) Then
                txtAddress1.Value = String.Format("{0}, {1}", strProperty, dataNode.InnerText)
            Else
                txtAddress1.Value = dataNode.InnerText
            End If
        End If

        dataNode = itemNodes(0).SelectSingleNode("Locality")
        If Not (dataNode Is Nothing) Then txtAddress2.Value = dataNode.InnerText

        dataNode = itemNodes(0).SelectSingleNode("Town")
        If Not (dataNode Is Nothing) Then txtAddress3.Value = dataNode.InnerText

        dataNode = itemNodes(0).SelectSingleNode("PostalCounty")
        If Not (dataNode Is Nothing) Then txtAddress4.Value = dataNode.InnerText

        dataNode = itemNodes(0).SelectSingleNode("Postcode")
        If Not (dataNode Is Nothing) Then
            'txtpostcodeout.Value = Trim(dataNode.InnerText.Substring(0, InStr(1, dataNode.InnerText, " ")))
            'txtpostcodein.Value = Trim(Mid(dataNode.InnerText, InStr(1, dataNode.InnerText, " ") + 1))
            If Not (dataNode Is Nothing) Then postcode.Value = dataNode.InnerText
        End If
    End Sub

    Private Sub findAddress()
        ' Clear out any existing items in the list
        lstresult.Items.Clear()
        If String.IsNullOrWhiteSpace(txtLookup.Text) Then
            Dim v As New CustomValidator
            v.ErrorMessage = "Please enter a search term."
            v.IsValid = False
            Me.Page.Validators.Add(v)
            Return
        End If
        ''CCCPS-82076: Validating postcode format before performing lookup (Only if ValidateBeforeSearch is set to True). 
        If ValidateBeforeSearch Then
            Dim regexPostCode As New Regex("^([Gg][Ii][Rr] 0[Aa]{2})|((([A-Za-z][0-9]{1,2})|(([A-Za-z][A-Ha-hJ-Yj-y][0-9]{1,2})|(([A-Za-z][0-9][A-Za-z])|([A-Za-z][A-Ha-hJ-Yj-y][0-9][A-Za-z]?))))\s?[0-9][A-Za-z]{2})$")
            Dim match As Match = regexPostCode.Match(txtLookup.Text)
            If Not match.Success Then
                Dim v As New CustomValidator
                v.ErrorMessage = "Please enter a valid Postcode"
                v.IsValid = False
                Me.Page.Validators.Add(v)
                Return
            End If
        End If
        ''CCCPS-82076: No need to call an API to fetch address list if we have it locally. 
        If MaintainDataLocally AndAlso SearchedCodes.ContainsKey(txtLookup.Text) Then
            GetSearchResults(SearchedCodes(txtLookup.Text))
            Return
        End If
        ''CCCPS-82076: Stop user from performing lookup limit is exhausted.
        If LookupLimit = 0 Then
            Dim v As New CustomValidator
            v.ErrorMessage = "The address has been looked up too many times. Please enter manually."
            v.IsValid = False
            Me.Page.Validators.Add(v)
            Return
        End If
        ' Declare XML Objects and variables
        Dim xmlDoc As System.Xml.XmlDocument
        Dim xmlLocation As String

        ' Create the XML Document Object Instance
        xmlDoc = New System.Xml.XmlDocument()

        ' Replace lstResult with the name of your list box for the results
        With lstresult
            ' Build up the XML query string
            xmlLocation = BuildServerDetails(SearchType.FastFind)

            ' Set the Country Name or ISO code for International operations
            xmlLocation = xmlLocation & "&Country=UK"

            ' Set the lookup string
            xmlLocation = xmlLocation & "&Lookup=" & txtLookup.Text ' Change txtLookup to your lookup entry textbox

            ' Load the XML from the webserver with the query string
            Try
                xmlDoc.Load(xmlLocation)
                ''CCCPS-82076: Maintain retrieved address list in session to reload locally if user searches for same postcode again. 
                If MaintainDataLocally Then SearchedCodes.Add(txtLookup.Text, xmlDoc)
                LookupLimit -= 1
            Catch
                Dim v As New CustomValidator
                v.ErrorMessage = "Error loading address results, please enter address manually"
                v.IsValid = False
                Me.Page.Validators.Add(v)
                Exit Sub
            End Try
            GetSearchResults(xmlDoc)
        End With
    End Sub



    Private Sub GetSearchResults(xmlDoc As System.Xml.XmlDocument)
        Dim root As System.Xml.XmlNode
        Dim pcFromNode As System.Xml.XmlNode
        Dim dataNode As System.Xml.XmlNode
        Dim itemNodes As System.Xml.XmlNodeList
        Dim listNode As System.Xml.XmlNode
        Dim keyNode As System.Xml.XmlNode
        With lstresult
            ' Check if PCE returned an error and if the document is valid
            root = xmlDoc.DocumentElement
            dataNode = root.SelectSingleNode("Result")
            itemNodes = root.SelectNodes("Item")
            If dataNode Is Nothing Or itemNodes Is Nothing Then
                Dim v As New CustomValidator
                v.ErrorMessage = "Error loading address results, please enter address manually"
                v.IsValid = False
                Me.Page.Validators.Add(v)
                Exit Sub
            End If
            If Val(dataNode.InnerText) < 1 Then
                dataNode = root.SelectSingleNode("ErrorText")
                If dataNode Is Nothing Then
                    Dim v As New CustomValidator
                    v.ErrorMessage = "Error loading address results, please enter address manually"
                    v.IsValid = False
                    Me.Page.Validators.Add(v)
                Else
                    Dim v As New CustomValidator
                    v.ErrorMessage = dataNode.InnerText & vbCrLf & "Please enter address manually"
                    v.IsValid = False
                    Me.Page.Validators.Add(v)
                End If
                Exit Sub
            End If

            ' Display any changed postcode if applicable
            pcFromNode = itemNodes(0).SelectSingleNode("PostcodeFrom")
            dataNode = itemNodes(0).SelectSingleNode("Postcode")
            If Not (pcFromNode Is Nothing) And Not (dataNode Is Nothing) Then
                If pcFromNode.InnerText <> "" Then
                    Dim v As New CustomValidator
                    v.ErrorMessage = "Postcode has changed from " & pcFromNode.InnerText & " to " & dataNode.InnerText
                    v.IsValid = True
                    Me.Page.Validators.Add(v)
                End If
            End If

            ' Now add matching records to the list box
            For Each dataNode In itemNodes
                ' Get the data nodes
                listNode = dataNode.SelectSingleNode("List")
                keyNode = dataNode.SelectSingleNode("Key")
                If Not (listNode Is Nothing) And Not (keyNode Is Nothing) Then
                    ' Add the item to the list box with hidden key
                    .Items.Add(listNode.InnerText & Space(512) & keyNode.InnerText)
                End If
            Next

            If .Items.Count() <> 0 Then .SelectedIndex = 0 ' Select First item in the list

        End With
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

    Private Function BuildServerDetails(ByVal Type As SearchType) As String
        Dim xmlLocation As String
        ' Build up the XML query string
        xmlLocation = AFD_SERVER & "/afddata.pce?"
        xmlLocation = xmlLocation & "Serial=" & AFD_SERIAL_NUMBER & "&"
        xmlLocation = xmlLocation & "Password=" & AFD_PASSWORD & "&"
        xmlLocation = xmlLocation & "UserID=" & AFD_USER_ID & "&"

        Select Case Type
            Case SearchType.FastFind
                xmlLocation = xmlLocation & "Data=Address&Task=FastFind&Fields=List"
            Case SearchType.Search
                xmlLocation = xmlLocation & "Data=Address&Task=Search&Fields=List"
            Case SearchType.Retrieve
                xmlLocation = xmlLocation & "Data=Address&Task=Retrieve&Fields=Standard"
            Case Else

        End Select

        ' Set the maximum number of records to return
        xmlLocation = xmlLocation & "&MaxQuantity=100"

        Return xmlLocation
    End Function

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
