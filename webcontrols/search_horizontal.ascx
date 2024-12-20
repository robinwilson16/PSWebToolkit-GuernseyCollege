<%@ Control Language="VB" AutoEventWireup="false" CodeFile="search_horizontal.ascx.vb" Inherits="webcontrols_search_horizontal" %>

<%@ Register Assembly="PSWebEnrolmentKit" Namespace="CompassCC.ProSolution.PSWebEnrolmentKit"
    TagPrefix="cc1" %>
<%@ Register Namespace="CustomControl"
    TagPrefix="cc2" %>

<br /><br />
<ol class="breadcrumb">
    <li class="active">Course Search</li>
</ol>


<%--@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@ --%>
<%--@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@ --%>
<h2>Replace search.ascx markup with this if using horizontal search bar</h2>
<%--@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@ --%>
<%--@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@ --%>

<link href="<%= ResolveUrl("~/webcontrols/search_horizontal.css") %>" rel="stylesheet" />


<div class="row text-center">
 
        <h3>Search Criteria</h3>

<cc2:CourseSearchPanel_ListView ID="CourseSearchPanel_ListView" runat="server" ClientIDMode="Static"
                ShowSite="false"
                ShowModeOfAttendance="True"
                MarketingField1Caption="Subject Area"
                MarketingField1CourseInformationTypeID="46"
                UseMarketingField1ForImage="true"
                UseMarketingField1AsFilter="true"
                ShowCollegeLevel="false"
                ShowItemsPerPage="False"
                ShowMarketingField1="True"
                ExcludedSiteIDs=""
                MarketingField2Caption="Sub"
                MarketingField2CourseInformationTypeID="64"
                ShowMarketingField2="false"
                MarketingField3Caption="MOA"
                MarketingField3CourseInformationTypeID="58"
                ShowMarketingField3="false"
                MarketingField2ParentCourseInformationTypeID="61"
                UseMarketingField2ForSubImage="false"
                SearchOrientation="Horizontal"
     />

  </div>     
                               <div class="row text-center">
  <span style="font-size:large">Key:</span>
                                    <img src="_images/icon_unavailable.png" alt="Course Unavailable" />                              
                                    <img  src="_images/icon_applynow.png" alt="Apply Now" />
                                    <img  src="_images/icon_enrolnow.png" alt="Enrol Now"/>
                                    <img  src="_images/icon_enquirenow.png" alt="Enquire Now"/>
                               </div>
                                  
                             
                                   


<hr />
<br />

<div class="row usercontrol">
       
        <div class="col-lg-12 resultspanel"> 
            <div runat="server" id="imgdiv">
            <asp:ListView ID="listviewGraphic" runat="server">
                <LayoutTemplate>
                    <ul class="MarketingFieldListView">
                        <asp:PlaceHolder ID="itemPlaceHolder" runat="server"></asp:PlaceHolder>
                    </ul>
                </LayoutTemplate>
                <ItemTemplate>
                    <li>
                        <div style="position: relative; display: inline-block;">
                            <asp:Button ID="btnMarketingGraphic" runat="server" CssClass="MarketingField1Image" /><br >
                            <figcaption style="text-align: center; -ms-text-overflow: ellipsis; -o-text-overflow: ellipsis; text-overflow: ellipsis; overflow:hidden; width:130px">
                            <asp:Label id="lblCourseInformationType" runat="server"></asp:Label></figcaption>
                            <br>
                        </div>
                    </li>
                </ItemTemplate>
            </asp:ListView>
            </div>
            <asp:ListView ID="SearchGrid" runat="server" DataKeyNames="CourseInformationID"
                SelectedIndex="0">

                <LayoutTemplate>
                    <div style="display: table;width:100%">
                        <div style="display: table-row">

                            <div style="display: table-cell"></div>
                            <div style="display: table-cell" class="searchgridheader">Academic Year</div>
                            <div style="display: table-cell" class="searchgridheader">Description</div>

                            <div style="display: table-cell" class="searchgridheader">Relevance</div>
                        </div>

                        <asp:PlaceHolder ID="itemPlaceHolder" runat="server"></asp:PlaceHolder>

                    </div>

                </LayoutTemplate>
                <ItemTemplate>
                    <div id="tblrow" style="display: table-row; height: 35px;">
                        <div style="display: table-cell">
                            <asp:HyperLink ID="ApplyOrEnrolmentBtn" runat="server" /></div>
                        <div style="display: table-cell" ><asp:Label ID="lblAcademicYear"  runat="server" CssClass="searchgridcell" Text='<%#: Eval("AcademicYearID")%>' /></div>
                        <div style="display: table-cell">
                            <asp:HyperLink ID="hlnkDescription" runat="server" CssClass="searchgridcell" Text='<%#: Eval("Description")%>' NavigateUrl='<%#: String.Format("~/webenrolment.aspx?page=~/webcontrols/coursedisplay.ascx&CourseInformationID={0}", Eval("CourseInformationID")) %>'></asp:HyperLink></div>
                        <div style="display: table-cell">
                            <asp:Label ID="lblScore" runat="server" CssClass="searchgridcell" /></div>
                    </div>
                </ItemTemplate>
                <EmptyDataTemplate>
                    <p>
                        No relevant courses found, please try different search criteria.
                    </p>
                    <p>
                        HINT: Try alternative wording E.g. If &#39;Computer&#39; doesn&#39;t return results
                       try &#39;Computing&#39; or &#39;Computers&#39;.
                    </p>
                </EmptyDataTemplate>
            </asp:ListView>
           

            <div class="searchgridfooter text-center">

                <asp:DataPager ID="dpSearchGrid" runat="server" PagedControlID="SearchGrid" PageSize="10" EnableViewState="True" ClientIDMode="Static">
                    <Fields>
                        <asp:NextPreviousPagerField FirstPageText="&lt;&lt;" ShowFirstPageButton="False" ShowNextPageButton="False" ShowPreviousPageButton="True" PreviousPageText="&lt;" />
                        <asp:NumericPagerField ButtonCount="20" />
                        <asp:NextPreviousPagerField LastPageText="&gt;&gt;" ShowLastPageButton="False" ShowNextPageButton="True" ShowPreviousPageButton="False" NextPageText="&gt;" />
                    </Fields>


                </asp:DataPager>
            </div>
         </div>

    </div>
<div class="row">
    <section id="enquire" class="pull-right">
<hr>            
            <p class="text-left">
                Alternatively, if you are unsure of the exact course you would like to apply to, or would just like someone from the College to contact you with further information, send an enquiry 
            <a class="btn btn-warning" href="webenrolment.aspx?page=~/webcontrols/checkout_generic_enquiry.ascx">here</a></p>
     </section>
    </div>