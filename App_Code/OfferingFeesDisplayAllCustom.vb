Option Strict On
Option Explicit On

Imports System.Web.UI
Imports CompassCC.CCCSystem.CCCCommon
Imports System.Web.UI.WebControls
Imports CompassCC.ProSolution.PSWebEnrolmentKit

Namespace CustomControl


    Public Class OfferingFeesDisplayAllCustom
        Inherits OfferingFeesDisplay

        Protected Overrides Sub CreateChildControls()
            If OfferingID <> -1 Then
                Dim tblOffering As New OfferingDataTable
                Dim v1 As CCCDataViewDataSet = CCCDataViewDataSet.CreateDataView(tblOffering)
                v1.Columns.AddDBColumns(False, False)
                v1.Filters.SetColumnFilter(tblOffering.OfferingIDColumn, OfferingID)
                tblOffering.TableAdapter.Load(tblOffering, v1)

                Dim lblCourseDescription As New Label
                Me.Controls.Add(New LiteralControl("<h3>Course Description: " & tblOffering(0).GetRowDescription & "</h3>"))
                Me.Controls.Add(New LiteralControl("<div class=""card"">"))
                Me.Controls.Add(New LiteralControl("<div class="" card-body"">"))
                Me.Controls.Add(New LiteralControl("<h5 class="" card-title"">" & tblOffering(0).GetRowDescription & "</h5>"))
                Me.Controls.Add(New LiteralControl("<p class="" card-text"">Other details here...</p>"))
                Me.Controls.Add(New LiteralControl("<a href=""#"" Class=""btn btn-primary"">Start Application Form</a>"))
                Me.Controls.Add(New LiteralControl("</div>"))
                Me.Controls.Add(New LiteralControl("</div>"))
                Me.Controls.Add(New LiteralControl("<br>"))
                Me.Controls.Add(New LiteralControl("<h3>Course Fees</h3>"))


                Dim tbl As New vFeesByTypeDataTable
                Dim v As CCCDataViewDataSet = CCCDataViewDataSet.CreateDataView(tbl)
                v.Columns.AddPKColumns()
                v.Columns.EnsureColumnsAreSelected(True, False, tbl.FeeTypeColumn, tbl.AmountStandardColumn, tbl.AmountOverseasColumn, tbl.Amount24PlusColumn, tbl.FeeTypeIDColumn)
                v.Filters.SetColumnFilter(tbl.OfferingIDColumn, OfferingID)
                v.SortOrders.SetColumnSortOrder(CompassCC.CCCSystem.CCCCommon.SortDirection.Ascending, tbl.FeeTypeSortOrderColumn, False)
                tbl.TableAdapter.Load(tbl, v)


                BuildGrid(tbl)
            Else
                Me.Controls.Add(New LiteralControl("<h4>Course Description: ???</h4>"))
                Me.Controls.Add(New LiteralControl("<br>"))
                Me.Controls.Add(New LiteralControl("<h4>Course Fees</h4>"))
                Me.Controls.Add(New LiteralControl("<br>"))
            End If
        End Sub

        Public Overrides Sub RenderBeginTag(writer As HtmlTextWriter)
            writer.RenderBeginTag(HtmlTextWriterTag.Div)
        End Sub

        Private Sub BuildGrid(ByVal tbl As vFeesByTypeDataTable)

            Dim pnl As New Panel
            pnl.ID = "Panel1"
            Me.Controls.Add(pnl)
            With pnl.Controls

                .Add(New LiteralControl("<table ID=""FeeGrid"" class=""table table-striped table-bordered text-center"">")) 'Table Start
                .Add(New LiteralControl("<tbody>"))
                .Add(New LiteralControl("<tr ID=""FeeGridHeaderRow"" >")) 'Row Start

                .Add(New LiteralControl("<td ID=""FeeGridFeeTypeCell"" >")) 'Cell Start
                .Add(New LiteralControl("<p>Fee Type&nbsp;</p>"))
                .Add(New LiteralControl("</td>")) 'Cell End


                .Add(New LiteralControl("<td ID=""FeeGridAmountCell"" colspan=""3"" >")) 'Cell Start
                .Add(New LiteralControl("<p>Amount&nbsp;</p>"))
                .Add(New LiteralControl("</tr>")) 'Row End

                .Add(New LiteralControl("<tr ID=""FeeGridHeaderRow"" >")) 'Row Start

                .Add(New LiteralControl("<td ID=""FeeGridFeeTypeCell"" >")) 'Cell Start
                .Add(New LiteralControl("<p>&nbsp;</p>"))
                .Add(New LiteralControl("</td>")) 'Cell End

                .Add(New LiteralControl("<td ID=""FeeGridFeeTypeCell"" >")) 'Cell Start
                .Add(New LiteralControl("<p>Standard</p>"))
                .Add(New LiteralControl("</td>")) 'Cell End

                .Add(New LiteralControl("</tr>")) 'Row End


                For Each row As vFeesByTypeRow In tbl.Rows

                    .Add(New LiteralControl("<tr ID=""FeeGridRow"" >")) 'Row Start
                    .Add(New LiteralControl("<td ID=""FeeGridFeeTypeCell"" >")) 'Cell Start
                    .Add(New LiteralControl("<p>" & row.FeeType.ToString & "&nbsp;</p>"))
                    .Add(New LiteralControl("</td>")) 'Cell End

                    .Add(New LiteralControl("<td ID=""FeeGridAmountCell"" >")) 'Cell Start
                    .Add(New LiteralControl("<p>" & CDec(CCCBlankLibrary.NoBlank(tbl.Compute("SUM(AmountStandard)", String.Format("FeeTypeID={0}", row.FeeTypeID)), 0)) & "&nbsp;</p>"))
                    .Add(New LiteralControl("</td>")) 'Cell End

                    .Add(New LiteralControl("</tr>")) 'Row End

                Next

                .Add(New LiteralControl("<tr ID=""FeeGridRow"" >")) 'Row Start
                .Add(New LiteralControl("<td ID=""FeeGridTotalCell"" >")) 'Cell Start
                .Add(New LiteralControl("<p>Total</p>"))
                .Add(New LiteralControl("</td>")) 'Cell End

                .Add(New LiteralControl("<td ID=""FeeGridAmountCell"" >")) 'Cell Start
                .Add(New LiteralControl("<p>" & CDec(CCCBlankLibrary.NoBlank(tbl.Compute("SUM(AmountStandard)", ""), 0)) & "&nbsp;</p>"))
                .Add(New LiteralControl("</td>")) 'Cell End


                .Add(New LiteralControl("<td ID=""FeeGridAmountCell"" >")) 'Cell Start
                .Add(New LiteralControl("<p>" & CDec(CCCBlankLibrary.NoBlank(tbl.Compute("SUM(AmountOverseas)", ""), 0)) & "&nbsp;</p>"))
                .Add(New LiteralControl("</td>")) 'Cell End


                .Add(New LiteralControl("<td ID=""FeeGridAmountCell"" >")) 'Cell Start
                .Add(New LiteralControl("<p>" & CDec(CCCBlankLibrary.NoBlank(tbl.Compute("SUM(Amount24Plus)", ""), 0)) & "&nbsp;</p>"))
                .Add(New LiteralControl("</td>")) 'Cell End

                .Add(New LiteralControl("</tr>")) 'Row End

                .Add(New LiteralControl("</tbody>"))
                .Add(New LiteralControl("</table>")) 'Table End


            End With

        End Sub


    End Class

End Namespace
