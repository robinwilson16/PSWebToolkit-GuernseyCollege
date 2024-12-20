<%@ control Language="VB"  AutoEventWireup="false" CodeFile="checkout_makepayment.ascx.vb" Inherits="checkout_makepayment"  %>

<br /><br />
                <ol class="breadcrumb" id="breadcrumbapps" runat="server">
                  <li><a href="default.aspx">Home</a></li>
                    <li><a href="webenrolment.aspx?page=~/webcontrols/checkout.ascx">Checkout</a></li>
                    <li><a href="webenrolment.aspx?page=~/webcontrols/checkout_applications.ascx">Personal Details</a></li>
                   
                    <li><a href="webenrolment.aspx?page=~/webcontrols/checkout_applications2.ascx">Further Details</a></li>
                    <li><a href="webenrolment.aspx?page=~/webcontrols/checkout_parent_guardian.ascx">Parent / Guardian</a></li>
                    <li><a href="webenrolment.aspx?page=~/webcontrols/checkout_other_contacts.ascx">Other Contacts</a></li>
                    <li><a href="webenrolment.aspx?page=~/webcontrols/checkout_school_employer.ascx">School / Employer</a></li>
                    <li><a href="webenrolment.aspx?page=~/webcontrols/checkout_prior_attainment.ascx">Prior Attainment</a></li>
                    <li><a href="webenrolment.aspx?page=~/webcontrols/checkout_quals_on_entry.ascx">Quals on Entry</a></li>
                    <li><a href="webenrolment.aspx?page=~/webcontrols/checkout_dataprotection_apponly.ascx">Declaration</a></li>
                  <li class="active">Processing...</li>
                </ol>

                <ol class="breadcrumb" id="breadcrumbenrols"  runat="server">
                  <li><a href="default.aspx">Home</a></li>
                    <li><a href="webenrolment.aspx?page=~/webcontrols/checkout.ascx">Checkout</a></li>
                    <li><a href="webenrolment.aspx?page=~/webcontrols/checkout_enrolments.ascx">Personal Details</a></li>
                   
                    <li><a href="webenrolment.aspx?page=~/webcontrols/checkout_enrolments2.ascx">Further Details</a></li>
                    <li><a href="webenrolment.aspx?page=~/webcontrols/checkout_parent_guardian.ascx">Parent / Guardian</a></li>
                    <li><a href="webenrolment.aspx?page=~/webcontrols/checkout_other_contacts.ascx">Other Contacts</a></li>
                    <li><a href="webenrolment.aspx?page=~/webcontrols/checkout_school_employer.ascx">School / Employer</a></li>
                    <li><a href="webenrolment.aspx?page=~/webcontrols/checkout_prior_attainment.ascx">Prior Attainment</a></li>
                    <li><a href="webenrolment.aspx?page=~/webcontrols/checkout_quals_on_entry.ascx">Quals on Entry</a></li>
                    <li><a href="webenrolment.aspx?page=~/webcontrols/checkout_dataprotection_apponly.ascx">Declaration</a></li>
                  <li class="active">Processing...</li>
                </ol>

    &nbsp;
<cc1:PaymentSubmitter id="PaymentSubmitter1" runat="server" DisplayTextForNoCost="Thank you, processing your information..." DisplayTextForWorldPayTransfer="Thank you, you are now being forwarded to WorldPay to complete this transaction..." MessageDisplayDelayTime="5000"></cc1:PaymentSubmitter>

<%--Enable this one for custom emails and/or overriding some of the methods such as GetCapitaV2Reference or GetFundCode--%>
<%--<cc2:CustomPaymentSubmitter id="PaymentSubmitter2" runat="server" DisplayTextForNoCost="Thank you, processing your information..." DisplayTextForWorldPayTransfer="Thank you, you are now being forwarded to WorldPay to complete this transaction..." MessageDisplayDelayTime="5000"></cc2:CustomPaymentSubmitter>--%>
    
    <asp:Panel ID="Panel1" runat="server" Height="50px" Width="100%" ForeColor="Red">
    <p style="color:red">
        NOTE: Scripts do not appear to be allowed to run in your browser, this will most
        likely prevent submission to World Pay.</p>
    </asp:Panel>
    


