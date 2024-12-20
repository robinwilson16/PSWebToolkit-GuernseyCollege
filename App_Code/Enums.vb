Imports Microsoft.VisualBasic

Public Enum WebSiteAvailability As Byte
    Notavailableonwebsite = 0
    Availablebutreadonly = 1
    Availableforapplications = 2
    Availableforenrolments = 3
End Enum

Public Enum WebSiteMode
    WebEnrolment = 0
    StudentAssistedEnrolment = 1
    StaffAssistedEnrolment = 2
End Enum