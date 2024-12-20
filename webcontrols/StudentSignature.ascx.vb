Imports jSignature.Tools
Imports Svg
Imports System.Drawing.Imaging
Imports System.IO
Imports CompassCC.CCCSystem.CCCCommon

Partial Class webcontrols_StudentSignature
    Inherits System.Web.UI.UserControl

    Public ReadOnly Property Signature As Byte()
        Get
            If Len(hidSignature.Value) > 0 Then
                Dim output As String = Replace(hidSignature.Value, "image/jsignature;base30,", "")
                Dim base30Converter As New Base30Converter()
                Dim arrBase30Data As Integer()()() = base30Converter.GetData(output)
                Dim strSVG As String = SVGConverter.ToSVG(arrBase30Data)

                'first convert it to SVG
                Dim mySVG As SvgDocument
                Dim newStream As New MemoryStream(ASCIIEncoding.[Default].GetBytes(strSVG))

                mySVG = SvgDocument.Open(Of SvgDocument)(newStream, Nothing)

                Dim tempStream As New MemoryStream()
                mySVG.Draw().Save(tempStream, ImageFormat.Png)
                Dim arrImageSS As Byte() = tempStream.GetBuffer
                tempStream.Close()

                Return arrImageSS
            Else
                Return Nothing
            End If
        End Get

    End Property

End Class
