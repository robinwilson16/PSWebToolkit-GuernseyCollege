<%@ control  Language="VB"  AutoEventWireup="false" CodeFile="checkout_quals_on_entry.ascx.vb" Inherits="checkout_quals_on_entry" %>
<%@ Register Assembly="PSWebEnrolmentKit" Namespace="CompassCC.ProSolution.PSWebEnrolmentKit"
    TagPrefix="cc1" %>

<br /><br />
                <ol class="breadcrumb" id="breadcrumbapps" runat="server">
                  <li><a href="default.aspx">Home</a></li>
                    <li><a href="webenrolment.aspx?page=~/webcontrols/checkout.ascx">Checkout</a></li>
                    <li><a href="webenrolment.aspx?page=~/webcontrols/checkout_applications.ascx">Personal Details</a></li>
                   
                    <li><a href="webenrolment.aspx?page=~/webcontrols/checkout_applications2.ascx">Further Details</a></li>
                    <li><a href="webenrolment.aspx?page=~/webcontrols/checkout_parent_guardian.ascx">Parent / Guardian</a></li>         
                    <li><a href="webenrolment.aspx?page=~/webcontrols/checkout_other_contacts.ascx">Other Contacts</a></li>
                    <li><a href="webenrolment.aspx?page=~/webcontrols/checkout_prior_attainment.ascx">Prior Attainment</a></li>
                  <li class="active">Quals on Entry</li>
                </ol>
                 <ol class="breadcrumb" id="breadcrumbenrols" runat="server">
                  <li><a href="default.aspx">Home</a></li>
                    <li><a href="webenrolment.aspx?page=~/webcontrols/checkout.ascx">Checkout</a></li>
                    <li><a href="webenrolment.aspx?page=~/webcontrols/checkout_enrolments.ascx">Personal Details</a></li>
                   
                    <li><a href="webenrolment.aspx?page=~/webcontrols/checkout_enrolments2.ascx">Further Details</a></li>
                    <li><a href="webenrolment.aspx?page=~/webcontrols/checkout_parent_guardian.ascx">Parent / Guardian</a></li>
                     <li><a href="webenrolment.aspx?page=~/webcontrols/checkout_other_contacts.ascx">Other Contacts</a></li>
                    <li><a href="webenrolment.aspx?page=~/webcontrols/checkout_school_employer.ascx">School / Employer</a></li>
                    <li><a href="webenrolment.aspx?page=~/webcontrols/checkout_prior_attainment.ascx">Prior Attainment</a></li>
                  <li class="active">Quals on Entry</li>
                </ol>

<div class="panel panel-info" id="panel1" runat="server">
    <div class="panel-heading">Qualifications on Entry</div>
     
   <p>If you have not yet taken your exams, please provide us with your predicted grades. 
                Please include information on all qualifications. ie GCSE, NVQs, BTEC, Diplomas, A levels or any other subjects you may have studied. </p> 
                <p>
                Please enter the details of your prior qualifications, choosing from the 
                drop-down lists below.</p>

    <asp:button runat="server" id="btnNoPriorQualifications" cssClass="show_hide btn btn-primary" text="No prior qualifications" ToolTip="Click to remove all the items listed below (And hide the list)"></asp:button>
    <br />
<br />
    <div id="slidingdiv" class="slidingDiv" runat="server">
<asp:table class="table table-striped text-center" id="tblQuals" runat="server">
<%--<tr><th>Qualification</th><th>Subject (if not in list)</th><th>Predicted Grade</th><th>Grade</th><th>Date Awarded</th></tr>--%>

</asp:table>
    <asp:button runat="server" id="btnAdd" cssClass="btn btn-success" text="Add Row"></asp:button>

    </div>
   
    </div>

 <cc1:CCCButton ID="btnBack" runat="server" Text="Back" ImageResource="btnBack" LinkResource="checkout_priorattainment_aspx" />
<cc1:CCCButton ID="btnContinue" runat="server" Text="Continue" ImageResource="btnContinue" CausesValidation="true" EnableEnterKey="true" SaveForLater="true" SaveForLaterIn="QualsOnEntry" />
<br />

<asp:ValidationSummary ID="ValidationSummary1" runat="server" CssClass="alert alert-danger"  />
    
   
        <asp:HiddenField  id="NoQuals" value="false" runat="server"  ClientIDMode="static" />
     <asp:HiddenField  id="intQualRows" value="0" runat="server"  ClientIDMode="static" />

