Option Explicit On
Option Strict On

Imports CompassCC.CCCSystem.CCCCommon
Imports CompassCC.ProSolution.PSWebEnrolmentKit

Imports System
Imports System.Collections
Imports System.ComponentModel
Imports System.Data
Imports System.Drawing
Imports System.Web
Imports System.Web.SessionState
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.HtmlControls
Imports com.qas.proweb       'QuickAddress services
Imports com.qas.prowebintegration


Partial Class address_4
    Inherits CheckAddressBaseControl

    'Result arrays
    Protected m_asAddressLines As String()
    Protected m_asAddressLabels As String()
    Protected m_eRoute As Constants.Routes = Constants.Routes.Undefined


    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

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

        SetRoute(MyBase.GetRoute())
        If Not IsPostBack Then
            FormatAddress(m_asAddressLabels, m_asAddressLines)
            SetWelcomeMessage(m_eRoute)
        Else
            FormatAddress(m_asAddressLabels, m_asAddressLines)
        End If
        'Else leave it to the event handlers (Accept, Back)

    End Sub

    '** Operations **


    ' Retrieve the formatted address from the Moniker, or create a set of blank lines
    Protected Sub FormatAddress(ByRef asLabels As String(), ByRef asLines As String())

        If Not (m_eRoute.Equals(Constants.Routes.PreSearchFailed) Or m_eRoute.Equals(Constants.Routes.Failed)) Then

            Try

                Dim searchService As QuickAddress = NewQuickAddress()

                'Retrieve layout from web.config
                Dim sLayout As String = "Database Layout"  'Constants.KEY_LAYOUT

                Dim aLines As AddressLine()
                If m_eRoute.Equals(Constants.Routes.Okay) Or m_eRoute.Equals(Constants.Routes.Undefined) Then
                    'Perform address formatting
                    aLines = searchService.GetFormattedAddress(GetMoniker(), sLayout).AddressLines
                Else
                    'Use first example address to get line labels
                    aLines = searchService.GetExampleAddresses(GetDataID(), sLayout)(0).AddressLines
                End If

                'Build display address arrays
                Dim iSize As Integer = aLines.Length
                ReDim asLabels(iSize - 1)
                ReDim asLines(iSize - 1)

                Dim i As Integer
                For i = 0 To iSize - 1
                    asLabels(i) = aLines(i).Label
                    asLines(i) = aLines(i).Line
                Next i

            Catch x As Exception

                m_eRoute = Constants.Routes.Failed
                SetErrorInfo(x.Message)

            End Try

        End If


        If asLabels Is Nothing Or asLines Is Nothing Then

            'Provide default (empty) address for manual entry
            ReDim asLabels(4)
            asLabels(0) = "Address Line 1"
            asLabels(1) = "Address Line 2"
            asLabels(2) = "Town"
            asLabels(3) = "County"
            If Request("DataID") = "GBR" Then
                asLabels(4) = "Postal Code"
                ReDim asLines(4)
            Else
                ReDim asLines(3)
            End If
           

        End If

        'Added by MPG 23Jul2007
        If Len(asLines(0)) = 0 Then
            If PSWebEnrolmentProperties.IsCurrentlyTakingAltAddress Then
                asLines(0) = WorkingData.EnrolmentRequestRow.AltAddress1
                asLines(1) = WorkingData.EnrolmentRequestRow.AltAddress2
                asLines(2) = WorkingData.EnrolmentRequestRow.AltAddress3
                asLines(3) = WorkingData.EnrolmentRequestRow.AltAddress4
                If Request("DataID") = "GBR" Then
                    asLines(4) = WorkingData.EnrolmentRequestRow.AltPostcodeOut & " " & WorkingData.EnrolmentRequestRow.AltPostcodeIn
                    asLines(4) = asLines(4).Trim
                End If


            Else
                asLines(0) = WorkingData.EnrolmentRequestRow.Address1
                asLines(1) = WorkingData.EnrolmentRequestRow.Address2
                asLines(2) = WorkingData.EnrolmentRequestRow.Address3
                asLines(3) = WorkingData.EnrolmentRequestRow.Address4
                If Request("DataID") = "GBR" Then
                    asLines(4) = CCCBlankLibrary.NoBlank(WorkingData.EnrolmentRequestRow.PostcodeOut, "").ToString.Trim & " " & CCCBlankLibrary.NoBlank(WorkingData.EnrolmentRequestRow.PostcodeIn, "").ToString.Trim
                    asLines(4) = asLines(4).Trim
                End If


            End If
        End If

    End Sub

    ' Update the page welcome depending on the route we took to get here
    Private Sub SetWelcomeMessage(ByVal eRoute As Constants.Routes)

        Select Case eRoute

            Case Constants.Routes.Okay
                LiteralMessage.Text = "Please confirm your address below."

            Case Constants.Routes.NoMatches, Constants.Routes.Timeout, Constants.Routes.TooManyMatches
                LiteralMessage.Text = "Automatic address capture did not succeed.<br /><br />Please search again or enter your address below."

            Case Else
                LiteralMessage.Text = "Automatic address capture is not available.<br /><br />Please enter your address below."

        End Select

    End Sub

    ' Current search state, how we arrived on the address format page (i.e. too many matches)
    Protected Shadows Function GetRoute() As Constants.Routes
        Return m_eRoute
    End Function

    Protected Sub SetRoute(ByVal eRoute As Constants.Routes)
        m_eRoute = eRoute
    End Sub



    Public Overrides Sub ValidateControl()

        Dim strLines As String() = Me.Request.Form.GetValues(Constants.FIELD_ADDRESS_LINES)
        If strLines.Length >= 5 Then
            If Len(strLines(0)) = 0 Then
                Dim v As New CustomValidator
                v.ErrorMessage = "You must enter the 1st line of the address"
                v.IsValid = False
                Me.Page.Validators.Add(v)
            End If

            If Len(strLines(strLines.Length - 1)) = 0 AndAlso Request("DataID") = "GBR" Then
                Dim v As New CustomValidator
                v.ErrorMessage = "You must enter the post code"
                v.IsValid = False
                Me.Page.Validators.Add(v)
            ElseIf Len(strLines(strLines.Length - 1)) > 0 AndAlso Request("DataID") = "GBR" Then
                Dim strPostCode As String() = strLines(strLines.Length - 1).Trim.Split(" "c)
                If strPostCode.Length > 1 Then
                    If Len(strPostCode(0)) > 4 Then
                        Dim v As New CustomValidator
                        v.ErrorMessage = "The first part of the postcode must be no longer than 4 characters."
                        v.IsValid = False
                        Me.Page.Validators.Add(v)
                    End If
                    If Len(strPostCode(1)) > 3 Then
                        Dim v As New CustomValidator
                        v.ErrorMessage = "The second part of the postcode must be no longer than 3 characters."
                        v.IsValid = False
                        Me.Page.Validators.Add(v)
                    End If
                ElseIf strPostCode.Length = 1 Then
                    If Len(strPostCode(0)) > 7 Then
                        Dim v As New CustomValidator
                        v.ErrorMessage = "The postcode must be no longer than 7 characters."
                        v.IsValid = False
                        Me.Page.Validators.Add(v)
                    End If
                End If
            End If

        End If

    End Sub

    Protected Sub btnContinue_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnContinue.Click
        Me.Page.Validate()

        If Me.Page.IsValid Then
            'Save address fields
            Dim strAddressLines As String() = Me.Request.Form.GetValues(Constants.FIELD_ADDRESS_LINES)
            If strAddressLines.Length >= 5 Then
                If PSWebEnrolmentProperties.IsCurrentlyTakingAltAddress Then
                    WorkingData.EnrolmentRequestRow.AltAddress1 = strAddressLines(0)
                    WorkingData.EnrolmentRequestRow.AltAddress2 = strAddressLines(1)
                    WorkingData.EnrolmentRequestRow.AltAddress3 = strAddressLines(2)
                    WorkingData.EnrolmentRequestRow.AltAddress4 = strAddressLines(3)
                    If Request("DataID") = "GBR" Then
                        Dim strPostCode As String() = strAddressLines(strAddressLines.Length - 1).Trim.Split(" "c)
                        If strPostCode.Length > 1 Then
                            WorkingData.EnrolmentRequestRow.AltPostcodeOut = strPostCode(0).ToUpper.Trim
                            WorkingData.EnrolmentRequestRow.AltPostcodeIn = strPostCode(1).ToUpper.Trim
                        ElseIf strPostCode.Length = 1 Then
                            WorkingData.EnrolmentRequestRow.AltPostcodeOut = Left(strPostCode(0), 4).ToUpper.Trim
                            WorkingData.EnrolmentRequestRow.AltPostcodeIn = Right(strPostCode(0), 3).ToUpper.Trim
                        End If
                    End If

                    'WorkingData.EnrolmentRequestRow.AltCountry = strAddressLines(5)

                Else
                    WorkingData.EnrolmentRequestRow.Address1 = strAddressLines(0)
                    WorkingData.EnrolmentRequestRow.Address2 = strAddressLines(1)
                    WorkingData.EnrolmentRequestRow.Address3 = strAddressLines(2)
                    WorkingData.EnrolmentRequestRow.Address4 = strAddressLines(3)
                    If Request("DataID") = "GBR" Then
                        Dim strPostCode As String() = strAddressLines(strAddressLines.Length - 1).Trim.Split(" "c)
                        If strPostCode.Length > 1 Then
                            WorkingData.EnrolmentRequestRow.PostcodeOut = strPostCode(0).ToUpper.Trim
                            WorkingData.EnrolmentRequestRow.PostcodeIn = strPostCode(1).ToUpper.Trim
                        ElseIf strPostCode.Length = 1 Then
                            WorkingData.EnrolmentRequestRow.PostcodeOut = Left(strPostCode(0), 4).ToUpper.Trim
                            WorkingData.EnrolmentRequestRow.PostcodeIn = Right(strPostCode(0), 3).ToUpper.Trim
                        End If
                    End If

                    'WorkingData.EnrolmentRequestRow.Country = strAddressLines(5)
                End If


                If WorkingData.ShoppingCart.ContainsItemsOfType("Enrolment") Then
                    Response.Redirect(GetResourceValue("checkout_enrolments2"))
                ElseIf WorkingData.ShoppingCart.ContainsItemsOfType("Application") Then
                    Response.Redirect(GetResourceValue("checkout_applications2_aspx"))
                    'GoFinalPage()
                ElseIf WorkingData.ShoppingCart.ContainsItemsOfType("Enquiry") Then
                    Response.Redirect(GetResourceValue("checkout_dataprotection_enquiry"))
                Else
                    'Need to capture another address
                    PSWebEnrolmentProperties.IsCurrentlyTakingAltAddress = True
                    GoFirstPage()
                End If



            End If
        End If

    End Sub

    Protected Sub btnback_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBack.Click
        Select Case GetRoute()

            Case Constants.Routes.NoMatches, Constants.Routes.Timeout, Constants.Routes.TooManyMatches
                GoInputPage()

            Case Constants.Routes.Okay
                If Not GetRefineMoniker() = "" Then
                    GoRefinementPage(Nothing)
                Else
                    GoSearchPage()
                End If

            Case Else
                GoFirstPage()

        End Select
    End Sub
End Class
