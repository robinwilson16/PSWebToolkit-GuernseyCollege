<%@ Control Language="VB" AutoEventWireup="false" CodeFile="checkout_dataprotection_enquiry.ascx.vb" Inherits="webcontrols_checkout_dataprotection_enquiry" %>
<%@ Register Assembly="PSWebEnrolmentKit" Namespace="CompassCC.ProSolution.PSWebEnrolmentKit"
    TagPrefix="cc1" %>

<br /><br />
                <ol class="breadcrumb">
                  <li><a href="default.aspx">Home</a></li>
                    <li><a href="webenrolment.aspx?page=~/webcontrols/checkout.ascx">Checkout</a></li>
                    <li><a href="webenrolment.aspx?page=~/webcontrols/checkout_enquiries.ascx">Personal Details</a></li>                   
                  <li class="active">Declaration</li>
                </ol>

<h3>Declaration<small> Please take time to read through this declaration before proceeding</small></h3>
<br /><br />
    <section>
    I understand that College will store the information on this form and other information while attending the College
    subject to the provisions of the Data Protection Act 1998 and the registration of the College under the Act.
    </section>
    <br />
    <p>
    <asp:CheckBox ID="chkConfirm" runat="server" 
            Text="I confirm that I have read the declaration above" Font-Bold="True" /></p>

        <cc1:CCCButton id="btnBack" runat="server" Text="Back" ImageResource="btnBack" LinkResource="checkout_enquiries"/>
        <cc1:CCCButton ID="btnContinue" runat="server" Text="Continue" ImageResource="btnContinue" CausesValidation="true" />


    <br />
    <asp:ValidationSummary ID="ValidationSummary1" runat="server" CssClass="alert alert-danger" ForeColor="" />


