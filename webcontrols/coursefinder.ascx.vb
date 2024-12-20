Imports CompassCC.CCCSystem.CCCCommon
Imports CompassCC.ProSolution.PSWebEnrolmentKit

Partial Class webcontrols_coursefinder
    Inherits webenrolmentcontrolvalidate


    Protected Overrides Sub OnLoad(e As EventArgs)
        MyBase.OnLoad(e)

        If Not IsPostBack Then
            Dim tbl As New OfferingDataTable
            Dim v As CCCDataViewDataSet = CCCDataViewDataSet.CreateDataView(tbl)
            v.Columns.AddDBColumns(False, False)
            v.Columns.EnsureColumnsAreSelected(True, False, tbl.AcademicYearIDColumn, tbl.CodeColumn, tbl.NameColumn, tbl.WebSiteAvailabilityIDColumn)
            v.Filters.SetColumnFilter(tbl.AcademicYearIDColumn, "14/15")
            v.Filters.SetColumnFilter(tbl.WebSiteAvailabilityIDColumn, 2)
            v.Filters.SetColumnFilter(tbl.OfferingStatusIDColumn, 1)
            tbl.TableAdapter.Load(tbl, v)


            For Each row As OfferingRow In tbl.Rows
                Dim l As New ListItem
                l.Text = row.Name
                l.Value = row.OfferingID
                cboOfferings.Items.Add(l)
            Next
        Else

        End If


    End Sub



    Protected Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click

        If Len(cboOfferings.SelectedValue) = 0 Then Exit Sub

        Dim intOfferingID As Integer = cboOfferings.SelectedValue
        If intOfferingID <> -1 Then

            Dim tblOffering As New OfferingDataTable
            tblOffering.TableAdapter.Load(tblOffering, CCCDataViewDataSet.CreateDataViewForSinglePrimaryKey(tblOffering, intOfferingID))
            If tblOffering.Count > 0 AndAlso tblOffering(0).WebSiteAvailabilityID.HasValue Then

                Dim item As New ShoppingCartItem()
                With tblOffering(0)
                    item.Cost = 0
                    item.Description = .GetRowDescription
                    item.ItemType = "Application"
                    item.OfferingID = .OfferingID.Value
                    item.CourseInfoID = .CourseInformationID.GetValueOrDefault
                End With
                With WorkingData.ShoppingCart
                    If Not .Items.Contains(item) Then
                        .Items.Add(item)
                        lblAdded.Text = "Course Added to Basket"
                    Else
                        'The Item Already exists in Shopping Cart
                    End If

                End With

            End If
        End If
    End Sub
End Class
