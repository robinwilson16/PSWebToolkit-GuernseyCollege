Imports CompassCC.ProSolution.PSWebEnrolmentKit

Partial Class webcontrols_address_QASProOnDemandProcess
    Inherits CheckoutBaseControl

    Protected Overrides Sub OnLoad(e As EventArgs)
        MyBase.OnLoad(e)

        If Not IsPostBack Then
            WorkingData.EnrolmentRequestRow.Address1 = Request.Form("address1")
            WorkingData.EnrolmentRequestRow.Address2 = Request.Form("address2")
            WorkingData.EnrolmentRequestRow.Address3 = Request.Form("address3")
            WorkingData.EnrolmentRequestRow.Address4 = Request.Form("address4")
            Dim pcode As String = Request.Form("postcode")
            If Len(pcode) > 0 Then

                'avoid dodgy postcodes breaking system
                Try
                    WorkingData.EnrolmentRequestRow.PostcodeOut = pcode.Substring(0, pcode.Length - 3)
                Catch ex As ArgumentOutOfRangeException
                    WorkingData.EnrolmentRequestRow.PostcodeOut = ""
                End Try

                Try
                    WorkingData.EnrolmentRequestRow.PostcodeIn = Right(pcode, 3)
                Catch ex As ArgumentOutOfRangeException
                    WorkingData.EnrolmentRequestRow.PostcodeIn = ""
                End Try


            End If
            WorkingData.EnrolmentRequestRow.Country = Request.Form("country")

        End If

        Dim blnApplicationsOnly As Boolean = WorkingData.ShoppingCart IsNot Nothing AndAlso Not WorkingData.ShoppingCart.ContainsItemsOfType("Enrolment")

        If blnApplicationsOnly Then
            Response.Redirect(GetResourceValue("checkout_applications2_aspx"))
        Else
            Response.Redirect(GetResourceValue("checkout_enrolments2_aspx"))
        End If

    End Sub

End Class
