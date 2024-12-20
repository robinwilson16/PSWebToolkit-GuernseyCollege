Imports Microsoft.VisualBasic
Imports System.Web
Namespace CustomControl
    Public Class RemoveResponseHeader
        Implements IHttpModule

        Public Sub New()
        End Sub

        ' In the Init function, register for HttpApplication  
        ' events by adding your handlers.  
        Public Sub Init(ByVal app As HttpApplication) Implements IHttpModule.Init
            AddHandler app.PreSendRequestHeaders, AddressOf Me.PreSendRequestHeaders
        End Sub

        Public Sub PreSendRequestHeaders(ByVal s As Object, ByVal e As EventArgs)
            Dim app As HttpApplication = CType(s, HttpApplication)
            app.Context.Response.Headers.Remove("Server")
        End Sub

        Public Sub Dispose() Implements IHttpModule.Dispose
        End Sub
    End Class
End Namespace