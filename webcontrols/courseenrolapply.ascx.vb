Option Explicit On
Option Strict On

Imports CompassCC.CCCSystem.CCCCommon
Imports CompassCC.ProSolution.PSWebEnrolmentKit

Partial Class courseenrolapply
    Inherits webenrolmentcontrolvalidate

    Protected Overrides Sub OnLoad(ByVal e As System.EventArgs)
        MyBase.OnLoad(e)
        Session.Item("_CCCCallingPage") = Me.Request.RawUrl
    End Sub

    Public ReadOnly Property OfferingID() As Integer
        Get
            If Me.Request("OfferingID") Is Nothing Then
                Return -1
            Else
                Return CInt(Request("OfferingID"))
            End If
        End Get
    End Property

    Protected Sub imgenrol_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgenrol.Click
        Response.Redirect(GetResourceValue("courseenrol_aspx") & "&OfferingID=" & OfferingID)
    End Sub

    Protected Sub imgapply_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgapply.Click
        Response.Redirect(GetResourceValue("courseapply_aspx") & "&OfferingID=" & OfferingID)
    End Sub
End Class
