Imports Elmah

Partial Class GenericError
    Inherits System.Web.UI.Page

    Private Property ExceptionContainer() As Exception
        Get
            ExceptionContainer = DirectCast(Session("LastError"), Exception)
        End Get
        Set(ByVal value As Exception)
            Session("LastError") = value
        End Set
    End Property

    Public ImageTooLarge As Boolean = False

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If Not IsPostBack Then
            Elmah.ErrorSignal.FromCurrentContext.Raise(ExceptionContainer)
            Try
                'Get the error type
                If ExceptionContainer.InnerException IsNot Nothing Then
                    If ExceptionContainer.InnerException.GetType Is GetType(HttpUnhandledException) Then
                        ExceptionContainer = ExceptionContainer.GetBaseException
                    Else
                        ExceptionContainer = ExceptionContainer.InnerException
                    End If
                End If

                'Show the error type on screen
                lblErrorType.Text = HttpUtility.HtmlEncode(Replace(ExceptionContainer.GetType.ToString, ControlChars.NewLine, "<br/>"))

                'Only try to get the referring page if there is one
                If Request.UrlReferrer IsNot Nothing Then
                    lblErrorPage.Text = Request.UrlReferrer.PathAndQuery.ToString
                End If
                lblErrorMessage.Text = Replace(ExceptionContainer.Message, ControlChars.NewLine, "<br/>")
                'lblErrorLocation.Text = Replace(ExceptionContainer.StackTrace, ControlChars.NewLine, "<br/>")

                'Add Check To See If Image Was Too Large To Redirect Back
                If lblErrorMessage.Text = "Maximum request length exceeded." Then
                    ImageTooLarge = True
                End If

                If ExceptionContainer.Message.Contains("Unable to get DataSource") Then
                    lblErrorType.Text = "Database connection error"
                    lblErrorMessage.Text = "Unable to connect to database with the specified connection details. The server was not found or is not accessible. "
                End If

                'CCCPS-75550 - Handing exception and giving generic error message rather than full exception message details.
                If ExceptionContainer.GetType Is GetType(HttpRequestValidationException) Then
                    lblErrorMessage.Text = "You have entered the data that could be considered hostile."
                End If

                'catch any errors that might be caused by the error page itself
            Catch ex As Exception
                lblErrorType.Text = HttpUtility.HtmlEncode(Replace(ex.GetType.ToString, ControlChars.NewLine, "<br/>"))
                lblErrorPage.Text = HttpUtility.HtmlEncode(Request.UrlReferrer.PathAndQuery.ToString)
                lblErrorMessage.Text = HttpUtility.HtmlEncode(Replace(ex.Message, ControlChars.NewLine, "<br/>"))
                'lblErrorLocation.Text = "Error on Error Page <br/>" &
                'Replace(ex.StackTrace, ControlChars.NewLine, "<br/>")
            End Try
        End If
    End Sub
End Class