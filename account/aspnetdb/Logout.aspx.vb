Imports CompassCC.ProSolution.PSWebEnrolmentKit

Partial Class account_aspnetdb_Logout
    Inherits System.Web.UI.Page

    Protected Overrides Sub OnLoad(e As EventArgs)
        MyBase.OnLoad(e)

        FormsAuthentication.SignOut()
        HttpContext.Current.Session.Abandon()
        Response.Redirect(GetResourceValue("search_aspx"))
    End Sub
End Class
