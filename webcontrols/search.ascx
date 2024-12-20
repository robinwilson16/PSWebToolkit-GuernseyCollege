<%@ Control Language="VB" AutoEventWireup="false" CodeFile="search.ascx.vb" Inherits="webcontrols_search" %>

<%@ Register Assembly="PSWebEnrolmentKit" Namespace="CompassCC.ProSolution.PSWebEnrolmentKit"
    TagPrefix="cc1" %>
<%@ Register Namespace="CustomControl"
    TagPrefix="cc2" %>

<br /><br />
<ol class="breadcrumb">
    <li class="active">Home</li>
</ol>
<div class="row usercontrol">
   
        <div class="col-lg-4 searchpanel text-center">
            <a href="http://www.acollege.ac.uk" title="Go to Compass website"><img class="img-responsive" src="_images/advanced-logo-header.png" alt="College Logo" /></a>
            <p>
                Welcome to Advanced College course search.
                <br />
            
             
            </p>
            <hr />
            <h3>Search Criteria</h3>
        

<cc2:CourseSearchPanel_ListView ID="CourseSearchPanel_ListView" runat="server" ClientIDMode="Static"
                ShowSite="false"
                ShowModeOfAttendance="False"
                MarketingField1Caption="Subject Area"
                MarketingField1CourseInformationTypeID="0"
                UseMarketingField1ForImage="false"
                UseMarketingField1AsFilter="false"
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
                SearchOrientation="Vertical"
     />

<asp:ValidationSummary ID="ValidationSummary1" runat="server" CssClass="alert alert-danger" />

            <hr>
            
                    <div>
                       
                        
                                <h3>Key</h3>
                                 <div class="row">
                    <div class="col-xs-1 col-xs-offset-4 col-md-3 col-md-offset-0">
                        <img src="_images/icon_unavailable.png" alt="Course Unavailable" />
                    </div>
                    <div class="col-xs-1 col-md-3">
                        <img  src="_images/icon_applynow.png" alt="Apply Now" />
                    </div>
                    <div class="col-xs-1 col-md-3">
                        <img  src="_images/icon_enrolnow.png" alt="Enrol Now" />
                    </div>
                    <div class="col-xs-1 col-md-3">
                        <img  src="_images/icon_enquirenow.png" alt="Enquire Now" />
                    </div>
                </div>
                                
                             

                           
                      

                    </div>
               <hr>
     </div>
      
        <div class="col-lg-8 resultspanel"> 
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
                            
                            <asp:Label id="lblCourseInformationType" runat="server" Width="130" CssClass="imageLabel"></asp:Label>
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
                    <div class="tblrow" style="display: table-row; height: 35px;">
                        <div style="display: table-cell">
                            <asp:HyperLink ID="ApplyOrEnrolmentBtn" runat="server" /></div>
                        <div style="display: table-cell" ><asp:Label ID="lblAcademicYear"  runat="server" CssClass="searchgridcell" Text='<%#: Eval("AcademicYearID")%>' /></div>
                        <div style="display: table-cell">
                            <asp:HyperLink ID="hlnkDescription" runat="server" CssClass="searchgridcell" Text='<%#: Eval("Description")%>' NavigateUrl='<%# String.Format("~/webenrolment.aspx?page=~/webcontrols/coursedisplay.ascx&CourseInformationID={0}", Eval("CourseInformationID")) %>'></asp:HyperLink></div>
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
    <div id="enquire" class="pull-right">
<hr>            
            <p class="text-center">
                Alternatively, if you are unsure of the exact course you would like to apply to, or would just like someone from the College to contact you with further information, click 
            <a class="btn btn-warning" href="webenrolment.aspx?page=~/webcontrols/checkout_generic_enquiry.ascx">here</a> to send a generic enquiry.
            </p>
     </div>
    </div>