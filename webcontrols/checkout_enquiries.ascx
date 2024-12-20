<%@ Control Language="VB" AutoEventWireup="false" CodeFile="checkout_enquiries.ascx.vb" Inherits="webcontrols_checkout_enquiries" %>
<%@ Register Assembly="PSWebEnrolmentKit" Namespace="CompassCC.ProSolution.PSWebEnrolmentKit"
    TagPrefix="cc1" %>

<br /><br />
                <ol class="breadcrumb">
                  <li><a href="default.aspx">Home</a></li>
                    <li><a href="webenrolment.aspx?page=~/webcontrols/checkout.ascx">Checkout</a></li>
                  <li class="active">Personal Details</li>
                </ol>

  
<div class="panel panel-info">
    <div class="panel-heading">Personal Details</div>
    <div class=" form-group">
        <cc1:StudentEnrolmentField StudentEnrolmentFieldType="Surname" ID="fldSurname" runat="server" IsRequired="true" LabelWidth="200"  AutoFocus="true"/>
    </div>
    <div class=" form-group">
        <cc1:StudentEnrolmentField StudentEnrolmentFieldType="FirstForename" ID="StudentEnrolmentField1" runat="server" IsRequired="true" LabelWidth="200" />
    </div>
    <div class=" form-group">
        <cc1:StudentEnrolmentField StudentEnrolmentFieldType="Title" ID="StudentEnrolmentField7" runat="server" IsRequired="true" LabelWidth="200" />
    </div>
    <div class=" form-group">
        <cc1:StudentEnrolmentField id="datepicker" runat="server" IsRequired="false" StudentEnrolmentFieldType="DateOfBirth"  LabelWidth="200" ClientIDMode="Static" />
    </div>
    <div class=" form-group">
        <cc1:StudentEnrolmentField StudentEnrolmentFieldType="Sex" ID="StudentEnrolmentField4" runat="server" LabelWidth="200" />
    </div>
    <div class=" form-group"> <%--CCCPS-77565 Add preferredpronoun field--%>
        <cc1:StudentEnrolmentField StudentEnrolmentFieldType="PreferredPronounID" ID="StudentEnrolmentField6" runat="server" LabelWidth="200" CustomCaption="Preferred Pronoun" />
    </div>
    <div class=" form-group">
        <cc1:StudentEnrolmentField StudentEnrolmentFieldType="Tel" ID="StudentEnrolmentField5" runat="server"  LabelWidth="200" Pattern="^((\(?0\d{4}\)?\s?\d{3}\s?\d{3})|(\(?0\d{3}\)?\s?\d{3}\s?\d{4})|(\(?0\d{2}\)?\s?\d{4}\s?\d{4}))(\s?#(\d{4}|\d{3}))?$"/>
    </div>
    <div class=" form-group">
        <cc1:StudentEnrolmentField StudentEnrolmentFieldType="MobileTel" ID="StudentEnrolmentField2" runat="server"  LabelWidth="200" Pattern="^(07[\d]{8,12}|447[\d]{7,11})$"/>
    </div>
    <div class=" form-group">
        <cc1:StudentEmailField StudentEnrolmentFieldType="Email" ID="StudentEnrolmentField3" runat="server" IsRequired="true" LabelWidth="200" HTML5InputType="email" Placeholder="e.g. name@domain.com" Pattern="^[a-zA-Z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-zA-Z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-zA-Z0-9](?:[a-zA-Z0-9-]*[a-zA-Z0-9])?\.)+[a-zA-Z0-9](?:[a-zA-Z0-9-]*[a-zA-Z0-9])?$" />
    </div>
    <div class=" form-group">
        <cc1:StudentEnrolmentField StudentEnrolmentFieldType="Enquiry" ID="StudentEnrolmentFieldEnquiry" runat="server" IsRequired="true" 
            LabelWidth="200" CustomFieldType="Other" HTML5InputType="text" Placeholder="Please provide details of your enquiry" />
    </div>
</div>

        <cc1:CCCButton id="btnBack" runat="server" Text="Back" ImageResource="btnBack" LinkResource="checkout_aspx"/>
        <cc1:CCCButton ID="btnContinue" runat="server" Text="Continue" ImageResource="btnContinue" LinkResource="checkout_makepayment_aspx" CausesValidation="true" />
    <br />
    <asp:ValidationSummary ID="ValidationSummary1" runat="server" CssClass="alert alert-danger" ForeColor="" />