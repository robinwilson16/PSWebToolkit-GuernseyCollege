<%@ WebHandler Language="VB" Class="ImageHandler" %>

Imports System
Imports System.Web
Imports CompassCC.ProSolution.PSWebEnrolmentKit
Imports CompassCC.CCCSystem.CCCCommon

Public Class ImageHandler : Implements IHttpHandler
    
    Public Sub ProcessRequest(ByVal context As HttpContext) Implements IHttpHandler.ProcessRequest
        If context.Request.QueryString("CourseInformationImageID") Is Nothing Then
            Return
        End If
        
        Dim tblCII As New CourseInformationImageDataTable
        Dim v As CCCDataViewDataSet = CCCDataViewDataSet.CreateDataView(tblCII)
        v.Columns.AddDBColumns(False, False)
        v.Filters.SetColumnFilter(tblCII.CourseInformationImageIDColumn, context.Request.QueryString("CourseInformationImageID"))
        tblCII.TableAdapter.Load(tblCII, v)
        
        If tblCII.Rows.Count = 1 Then
            context.Response.ContentType = "image/gif"
            context.Response.BinaryWrite(DirectCast(DirectCast(tblCII.Rows(0), CourseInformationImageRow).Image, Byte()))
        Else
            Return
        End If
        
    End Sub
 
    Public ReadOnly Property IsReusable() As Boolean Implements IHttpHandler.IsReusable
        Get
            Return False
        End Get
    End Property

End Class