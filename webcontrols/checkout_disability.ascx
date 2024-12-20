<%@ Control Language="VB" AutoEventWireup="false" CodeFile="checkout_disability.ascx.vb" Inherits="checkout_disability" %>
<%@ Register Assembly="PSWebEnrolmentKit" Namespace="CompassCC.ProSolution.PSWebEnrolmentKit"
    TagPrefix="cc1" %>

<br />
<br />
<ol class="breadcrumb" id="breadcrumbapps" runat="server">
    <li><a href="default.aspx">Home</a></li>
    <li><a href="webenrolment.aspx?page=~/webcontrols/checkout.ascx">Checkout</a></li>
    <li><a href="webenrolment.aspx?page=~/webcontrols/checkout_applications.ascx">Personal Details</a></li>
    <li><a href="webenrolment.aspx?page=~/webcontrols/checkout_applications2.ascx">Further Details</a></li>
    <li><a href="webenrolment.aspx?page=~/webcontrols/checkout_parent_guardian.ascx">Parent / Guardian</a></li>
    <li><a href="webenrolment.aspx?page=~/webcontrols/checkout_other_contacts.ascx">Other Contacts</a></li>
    <li><a href="webenrolment.aspx?page=~/webcontrols/checkout_prior_attainment.ascx">Prior Attainment</a></li>
    <li><a href="webenrolment.aspx?page=~/webcontrols/checkout_quals_on_entry.ascx">Quals on Entry</a></li>
    <li class="active">Disability</li>
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
    <li><a href="webenrolment.aspx?page=~/webcontrols/checkout_quals_on_entry.ascx">Quals on Entry</a></li>
    <li class="active">Disability</li>
</ol>

<div class="panel panel-info" id="panel1" runat="server">
    <div class="panel-heading">Disability Category</div>
    <p></p>
    <asp:Button runat="server" ID="btnNoDisability" CssClass="show_hide btn btn-primary" Text="No Disability" ToolTip="Click to remove all the items listed below (And hide the list)"></asp:Button>
    <br />
    <br />
    <div id="slidingdiv" class="slidingDiv" runat="server">
        <asp:Table class="table table-striped text-center" ID="tblDisability" runat="server">
            <%--<tr><th>Qualification</th><th>Subject (if not in list)</th><th>Predicted Grade</th><th>Grade</th><th>Date Awarded</th></tr>--%>
        </asp:Table>
        <asp:Button runat="server" ID="btnAdd" CssClass="btn btn-success" Text="Add Row"></asp:Button>

    </div>

</div>

<cc1:CCCButton ID="btnBack" runat="server" Text="Back" ImageResource="btnBack" LinkResource="checkout_quals_on_entry_aspx" />
<cc1:CCCButton ID="btnContinue" runat="server" Text="Continue" ImageResource="btnContinue" CausesValidation="true" EnableEnterKey="true" SaveForLater="true" SaveForLaterIn="Disability" />
<br />

<asp:ValidationSummary ID="ValidationSummary1" runat="server" CssClass="alert alert-danger" />


<asp:HiddenField ID="NoDisability" Value="false" runat="server" ClientIDMode="static" />
<asp:HiddenField ID="intDisabilityRows" Value="0" runat="server" ClientIDMode="static" />

