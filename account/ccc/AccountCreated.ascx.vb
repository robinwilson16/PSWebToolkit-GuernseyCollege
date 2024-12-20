Imports CompassCC.ProSolution.PSWebEnrolmentKit
Imports CompassCC.CCCSystem.CCCCommon

Partial Class account_ccc_AccountCreated
    Inherits webenrolmentcontrolvalidate

    Protected Overrides Sub OnLoad(e As EventArgs)
        MyBase.OnLoad(e)

        Response.AddHeader("REFRESH", "3;URL=default.aspx")


    End Sub
End Class
