<%@ Control Language="VB" AutoEventWireup="false" CodeFile="checkout_employment.ascx.vb" Inherits="webcontrols_checkout_employment" %>
<%@ Register Assembly="PSWebEnrolmentKit" Namespace="CompassCC.ProSolution.PSWebEnrolmentKit"
    TagPrefix="cc1" %>

<br /><br />
                <%--<ol class="breadcrumb" id="breadcrumbapps" runat="server">
                  <li><a href="default.aspx">Home</a></li>
                    <li><a href="webenrolment.aspx?page=~/webcontrols/checkout.ascx">Checkout</a></li>
                    <li><a href="webenrolment.aspx?page=~/webcontrols/checkout_applications.ascx">Personal Details</a></li>
                   
                    <li><a href="webenrolment.aspx?page=~/webcontrols/checkout_applications2.ascx">Further Details</a></li>
                    <li><a href="webenrolment.aspx?page=~/webcontrols/checkout_parent_guardian.ascx">Parent / Guardian</a></li>
                    <li><a href="webenrolment.aspx?page=~/webcontrols/checkout_school_employer.ascx">School / Employer</a></li>
                    <li><a href="webenrolment.aspx?page=~/webcontrols/checkout_prior_attainment.ascx">Prior Attainment</a></li>
                  <li class="active">Quals on Entry</li>
                </ol>--%>
                 <ol class="breadcrumb" id="breadcrumbenrols" runat="server">
                  <li><a href="default.aspx">Home</a></li>
                    <li><a href="webenrolment.aspx?page=~/webcontrols/checkout.ascx">Checkout</a></li>
                    <li><a href="webenrolment.aspx?page=~/webcontrols/checkout_enrolments.ascx">Personal Details</a></li>
                   
                    <li><a href="webenrolment.aspx?page=~/webcontrols/checkout_enrolments2.ascx">Further Details</a></li>
                    <li><a href="webenrolment.aspx?page=~/webcontrols/checkout_parent_guardian.ascx">Parent / Guardian</a></li>
                     <li><a href="webenrolment.aspx?page=~/webcontrols/checkout_other_contacts.ascx">Other Contacts</a></li>
                    <li><a href="webenrolment.aspx?page=~/webcontrols/checkout_school_employer.ascx">School / Employer</a></li>
                    <li><a href="webenrolment.aspx?page=~/webcontrols/checkout_prior_attainment.ascx">Prior Attainment</a></li>
                     <li><a href="webenrolment.aspx?page=~/webcontrols/checkout_quals_on_entry.ascx">Quals on Entry</a></li>
                     <li><a href="webenrolment.aspx?page=~/webcontrols/checkout_references.ascx">References</a></li>
                  <li class="active">Employment</li>
                </ol>

<div class="panel panel-info" id="panel1" runat="server">
    <div class="panel-heading">Employment</div>
     
                <p>
                Please enter the details of your employment history (if applicable)</p>

   

    <!--<a href="#" class="show_hide btn btn-primary">No previous employment</a>-->
    <asp:button runat="server" id="btnNoPreviousEmployment" cssClass="show_hide btn btn-primary" text="No previous employment" ToolTip="Click to remove all the items listed below (And hide the list)"></asp:button>
    <br />
<br />
    <div id="slidingdiv" class="slidingDiv" runat="server">
<asp:table class="table text-center" id="tblEmployment" runat="server">

</asp:table>
    <asp:button runat="server" id="btnAdd" cssClass="btn btn-success" text="Add Row"></asp:button>

    </div>
   
    </div>

 <cc1:CCCButton ID="btnBack" runat="server" Text="Back" ImageResource="btnBack" LinkResource="checkout_references_aspx" />
<cc1:CCCButton ID="btnContinue" runat="server" Text="Continue" ImageResource="btnContinue"   CausesValidation="true" SaveForLater="true"  SaveForLaterIn="EmploymentHistory" />
<br />

<asp:ValidationSummary ID="ValidationSummary1" runat="server" CssClass="alert alert-danger"  />
    
   
        <asp:HiddenField  id="NoEmployment" value="false" runat="server"  ClientIDMode="static" />
     <asp:HiddenField  id="intEmploymentRows" value="0" runat="server"  ClientIDMode="static" />
