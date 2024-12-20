<%@ Control Language="VB" AutoEventWireup="false" CodeFile="checkout_applications2.ascx.vb" Inherits="webcontrols_checkout_applications2" %>
<%@ Register Assembly="PSWebEnrolmentKit" Namespace="CompassCC.ProSolution.PSWebEnrolmentKit"
    TagPrefix="cc1" %>

<br /><br />
                <ol class="breadcrumb">
                  <li><a href="default.aspx">Home</a></li>
                    <li><a href="webenrolment.aspx?page=~/webcontrols/checkout.ascx">Checkout</a></li>
                    <li><a href="webenrolment.aspx?page=~/webcontrols/checkout_applications.ascx">Personal Details</a></li>
                   
                  <li class="active">Further Details</li>
                </ol>
   
<div class="panel panel-success">
    <div class="panel-heading">Further Details - Please fill in these optional fields to help us process your application</div>
    <div class=" form-group">
        <cc1:StudentEnrolmentField StudentEnrolmentFieldType="DisabilityCategory1ID" ID="fldDisabilityID" runat="server" IsRequired="false" LabelWidth="300"
            CustomCaption="Do you have any disabilities or learning difficulties?" />
    </div>
    <div class=" form-group">
        <cc1:StudentEnrolmentField StudentEnrolmentFieldType="DisabilityCategory2ID" ID="fldLearningDifficultyID" runat="server" IsRequired="false" LabelWidth="300"
            CustomCaption="Do you have any disabilities or learning difficulties?" />
    </div>
    <div class=" form-group">
        <cc1:StudentEnrolmentField StudentEnrolmentFieldType="HeardAboutCollegeID" ID="fldHeardAboutCollegeID" runat="server" IsRequired="false" LabelWidth="300"
            CustomCaption="How did you hear about the college?" />
    </div>
    <div class=" form-group">
        <cc1:StudentEnrolmentField StudentEnrolmentFieldType="NationalityID" ID="fldNationalityID" runat="server" LabelWidth="300"
            CustomCaption="Nationality" />
    </div>
    <div class=" form-group">
        <cc1:StudentEnrolmentField StudentEnrolmentFieldType="EuroResidentID" ID="fldEuroResidentID" runat="server"  LabelWidth="300"
            CustomCaption="Have you been a European resident for the past 3 years?" Width="40"/>
    </div>
    <div class=" form-group">
        <cc1:StudentEnrolmentField StudentEnrolmentFieldType="EthnicGroupID" ID="fldEthnicGroupID" runat="server" IsRequired="false" LabelWidth="300"
            CustomCaption="What is your ethnicity?" />
    </div>
     <div class=" form-group">
              <cc1:StudentEnrolmentField ID="StudentEnrolmentField1"  StudentEnrolmentFieldType="NI"  runat="server" IsRequired="false" LabelWidth="600"  CustomCaption="NI Number" Pattern="^(?!BG)(?!GB)(?!NK)(?!KN)(?!TN)(?!NT)(?!ZZ)(?:[A-CEGHJ-PR-TW-Z][A-CEGHJ-NPR-TW-Z])(?:\s*\d\s*){6}([A-D]|\s)$" Title="Please enter an NI number in the correct format (AA123456A)" />
        </div>
    
</div>
<cc1:NonEUValidator ID="nonEUValidator1" runat="server"></cc1:NonEUValidator>



        <cc1:CCCButton id="btnBack" runat="server" Text="Back" ImageResource="btnBack" LinkResource="checkout_applications_aspx"/>
        <cc1:CCCButton ID="btnContinue" runat="server" Text="Continue" ImageResource="btnContinue" LinkResource="checkout_parentguardian_aspx" CausesValidation="true" SaveForLater="true" SaveForLaterIn="Request" />
    <br />
    <asp:ValidationSummary ID="ValidationSummary1" runat="server" CssClass="alert alert-danger" ForeColor="" />
