Option Explicit On
Option Strict On

Imports CompassCC.CCCSystem.CCCCommon
Imports CompassCC.ProSolution.PSWebEnrolmentKit

Partial Class courseapply
    Inherits webenrolmentcontrolvalidate

    Public ReadOnly Property OfferingID() As Integer
        Get
            If Me.DesignMode OrElse CCCBlankLibrary.IsBlank(Me.Page.Request("OfferingID")) Then
                Return -1
            Else
                Return CInt(Me.Page.Request("OfferingID"))
            End If
        End Get
    End Property

    Protected Overrides Sub OnPreRender(e As EventArgs)
        MyBase.OnPreRender(e)

        ' Response.Redirect(GetResourceValue("checkout_applications_aspx"))
    End Sub
   
End Class
