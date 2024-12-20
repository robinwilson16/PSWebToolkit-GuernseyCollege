<%@ Control Language="VB" AutoEventWireup="false" CodeFile="checkout_generic_enquiry.ascx.vb" Inherits="webcontrols_checkout_generic_enquiry" %>

<%@ Register Assembly="PSWebEnrolmentKit" Namespace="CompassCC.ProSolution.PSWebEnrolmentKit"
    TagPrefix="cc1" %>
   



 <br /><br />
                <ol class="breadcrumb">
                  <li><a href="default.aspx">Home</a></li>
                  <li class="active">Generic Enquiry</li>
                </ol>
   
<div class="panel panel-info">
    <div class="panel-heading">Enquiry Form</div>
    <div class=" form-group">
        <cc1:StudentEnrolmentField StudentEnrolmentFieldType="Surname" ID="fldSurname" runat="server" IsRequired="true" LabelWidth="200" />
    </div>
    <div class=" form-group">
        <cc1:StudentEnrolmentField StudentEnrolmentFieldType="FirstForename" ID="fldFirstForename" runat="server" IsRequired="true" LabelWidth="200" />
    </div>
    <div class=" form-group">
        <cc1:StudentEnrolmentField StudentEnrolmentFieldType="Title" ID="fldTitle" runat="server" IsRequired="true" LabelWidth="200" />
    </div>
    <div class=" form-group">
        <cc1:StudentEnrolmentField StudentEnrolmentFieldType="DateOfBirth" ID="datepicker" runat="server" LabelWidth="200" ClientIDMode="Static"/>
    </div>
    <div class=" form-group">
        <cc1:StudentEnrolmentField StudentEnrolmentFieldType="Sex" ID="fldSex" runat="server" LabelWidth="200" />
    </div>
    <div class=" form-group">
        <cc1:StudentEnrolmentField StudentEnrolmentFieldType="Tel1" ID="fldTel1" runat="server"  LabelWidth="200" CustomCaption="Tel"/>
    </div>
    <div class=" form-group">
        <cc1:StudentEmailField StudentEnrolmentFieldType="EmailAddress" ID="fldEmailAddress" runat="server" IsRequired="true" LabelWidth="200" CustomCaption="Email" Pattern="^[a-zA-Z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-zA-Z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-zA-Z0-9](?:[a-zA-Z0-9-]*[a-zA-Z0-9])?\.)+[a-zA-Z0-9](?:[a-zA-Z0-9-]*[a-zA-Z0-9])?$" />
    </div>
    <div class=" form-group">
        <cc1:StudentEnrolmentField StudentEnrolmentFieldType="Enquiry" ID="fldEnquiry" runat="server" CustomFieldType="Other" IsRequired="true" LabelWidth="200" CustomCaption="Please provide some details about your enquiry"/>
    </div>
</div>

<div class="panel panel-info">
    <div class="panel-heading">Are there any subjects that you are particularly interested in studying? (Pick up to 5)</div>
    <div class=" form-group">
        <cc1:StudentEnrolmentField StudentEnrolmentFieldType="SubjectID" ID="fldSubjectID" runat="server" IsRequired="false" LabelWidth="200"
            CustomCaption=" " />
    </div>
    <div class=" form-group">
        <cc1:StudentEnrolmentField StudentEnrolmentFieldType="Subject2ID" ID="fldSubject2ID" runat="server" IsRequired="false" LabelWidth="200"
            CustomCaption=" " />
    </div>
    <div class=" form-group">
        <cc1:StudentEnrolmentField StudentEnrolmentFieldType="Subject3ID" ID="fldSubject3ID" runat="server" IsRequired="false" LabelWidth="200"
            CustomCaption=" " />
    </div>
    <div class=" form-group">
        <cc1:StudentEnrolmentField StudentEnrolmentFieldType="Subject4ID" ID="fldSubject4ID" runat="server" IsRequired="false" LabelWidth="200"
            CustomCaption=" " />
    </div>
    <div class=" form-group">
        <cc1:StudentEnrolmentField StudentEnrolmentFieldType="Subject5ID" ID="fldSubject5ID" runat="server" IsRequired="false" LabelWidth="200"
            CustomCaption=" " />
    </div>
</div>

        <cc1:CCCButton id="btnBack" runat="server" Text="Back" ImageResource="btnBack" LinkResource="search_aspx"/>
        <cc1:CCCButton ID="btnContinue" runat="server" Text="Continue" ImageResource="btnContinue" CausesValidation="true" />

    <br />
    <asp:ValidationSummary ID="ValidationSummary1" runat="server" CssClass="alert alert-danger" ForeColor="" />
