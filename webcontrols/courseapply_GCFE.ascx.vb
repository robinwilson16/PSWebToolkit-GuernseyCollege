Option Explicit On
Option Strict On

Imports CompassCC.CCCSystem.CCCCommon
Imports CompassCC.ProSolution.PSWebEnrolmentKit

Partial Class courseapply_GCFE
    Inherits webenrolmentcontrolvalidate

    Public Course As OfferingRow
    Public LearningAimTitle As String
    Public CourseFee As OfferingFeeRow
    Public CollegeLevel As String

    Public HasFormErrors As Boolean = False

    Public EnrolmentType As String

    Protected Overrides Sub OnLoad(ByVal e As System.EventArgs)
        MyBase.OnLoad(e)

        'Clear anything else from car first before adding new courses
        WorkingData.ClearCart()

        Course = OfferingDataTable.FetchByOfferingID(OfferingID())

        EnrolmentType = Course.EnrolmentType

        If Not IsNothing(Course) Then
            'Save Offering ID So Can Be Accessed Later
            'WorkingData.ApplicationRequestRow.Offering1ID = Course.OfferingID

            'Set Application Type
            'If Course.EnrolmentTypeID = 1 Then 'Apprenticeship
            '    WorkingData.ApplicationRequestRow.ApplicationTypeID = 2 'Apprentice
            'End If

            LearningAimTitle = Course.LearningAimTitle
            'LearningAimTitle = Learning_AimDataTableAdapter.LoadDataSource_LearningAim(OfferingRow.QualID).GetLearningAimTitle(OfferingRow.QualID)

            If Not IsNothing(Course) Then
                CourseFee = OfferingFeeDataTableAdapter.GetOfferingFeeRow(CInt(Course.OfferingID), 9, 0) '9 = Tuition
            Else
                CourseFee = Nothing
            End If

            CollegeLevel = Course.CollegeLevelName
        End If

    End Sub

    Public Overrides Sub ValidateControl()

        If HasReadDeclaration.Value <> "Y" Then
            Dim v As New CustomValidator
            v.ErrorMessage = "Please scroll to the end of the declation before proceeding"
            v.IsValid = False
            Me.Page.Validators.Add(v)
        End If

        MyBase.ValidateControl()
    End Sub

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

    Private Sub btnContinue_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnContinue.Click

        Me.Page.Validate()



        If Me.Page.IsValid Then

        Else
            HasFormErrors = True
        End If
    End Sub

End Class
