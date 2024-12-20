Imports Microsoft.VisualBasic

Public Module Library
    Dim m_WebSiteMode As WebSiteMode

    Public Property CurrentWebSiteMode() As WebSiteMode
        Get
            Return m_WebSiteMode
        End Get
        Set(ByVal value As WebSiteMode)
            m_WebSiteMode = value
        End Set
    End Property

    Public Function GetResourceValue(ByVal resourceKey As String) As String
        'Get the value text from the resources file
        Return CStr(Web.HttpContext.GetGlobalResourceObject("Resource", resourceKey))
    End Function

    Public Sub CopyRow(ByVal fromFields As System.Reflection.PropertyInfo(), ByVal toFields As System.Reflection.PropertyInfo(), ByVal fromRecord As Object, ByVal toRecord As Object)

        If fromFields Is Nothing OrElse toFields Is Nothing Then
            Return
        End If

        Dim fromObjectType As Type = fromRecord.[GetType]()
        Dim toObjectType As Type = toRecord.[GetType]()

        For Each fromProperty As System.Reflection.PropertyInfo In fromObjectType.GetProperties()
            If fromProperty.CanRead Then
                Dim propertyName As String = fromProperty.Name
                Dim propertyType As Type = fromProperty.PropertyType

                Dim toProperty As System.Reflection.PropertyInfo = FindPropertyInfo(toObjectType, propertyName)
                If IsNothing(toProperty) Then Continue For
                Dim toPropertyType As Type = toProperty.PropertyType

                If toProperty IsNot Nothing AndAlso toProperty.CanWrite Then

                    Dim fromValue As Object = fromProperty.GetValue(fromRecord, Nothing)
                    If fromProperty.PropertyType Is GetType(Date) AndAlso fromValue = Date.MinValue Then
                        fromValue = Nothing
                    End If

                    fromValue = CompassCC.CCCSystem.CCCCommon.CCCDataTypeConverter.Convert(fromValue, toProperty.PropertyType)

                    toProperty.SetValue(toRecord, fromValue, Nothing)
                End If
            End If
        Next

    End Sub

    Private Function FindPropertyInfo(toObjectType As Type, PropertyName As String) As System.Reflection.PropertyInfo
        Dim toProperty As System.Reflection.PropertyInfo = toObjectType.GetProperty(PropertyName)
        If toProperty Is Nothing Then
            'Check (could be a different case)
            For Each prop In toObjectType.GetProperties()
                If prop.Name.ToLower = PropertyName.ToLower Then
                    toProperty = prop
                    Exit For
                End If
            Next
        End If
        Return toProperty
    End Function

End Module





