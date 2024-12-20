<%@ Control Language="VB"  AutoEventWireup="false" CodeFile="checkout_dataprotection_apponly.ascx.vb" Inherits="checkout_dataprotection_apponly"  %>

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
                     <li><a href="webenrolment.aspx?page=~/webcontrols/checkout_quals_on_entry.ascx">Quals on Entry</a></li>
                    <li><a href="webenrolment.aspx?page=~/webcontrols/checkout_attachments.ascx">Attachments</a></li>
                  <li class="active">Declaration</li>
                </ol>

<h3>Declaration<small> Please take time to read through this declaration before proceeding</small></h3>
<br /><br />
    <section>If I am accepted on the course(s) for which I am now applying, I agree to observe all campus regulations.
    I understand that College will store the information on this form and other information while attending the College
    subject to the provisions of the General Data Protection Regulation (GDPR).
    Information will be shared with The States of Guernsey as required to comply with funding requirements and legislation.
    </section>
    <br />
    <p>
    <asp:CheckBox ID="chkConfirm" runat="server" 
            Text="I confirm that I have read the declaration above" Font-Bold="True" /></p>

        <cc1:CCCButton id="btnBack" runat="server" Text="Back" ImageResource="btnBack" LinkResource="checkout_attachments_aspx"/>
        <cc1:CCCButton ID="btnContinue" runat="server" Text="Continue" ImageResource="btnContinue" CausesValidation="true" />


    <br />
    <asp:ValidationSummary ID="ValidationSummary1" runat="server" CssClass="alert alert-danger" ForeColor="" />


