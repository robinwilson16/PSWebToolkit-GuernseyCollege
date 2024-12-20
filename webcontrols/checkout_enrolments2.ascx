<%@ Control Language="VB" AutoEventWireup="false" CodeFile="checkout_enrolments2.vb" Inherits="webcontrols_checkout_enrolments2" %>

<%@ Register Assembly="PSWebEnrolmentKit" Namespace="CompassCC.ProSolution.PSWebEnrolmentKit"
    TagPrefix="cc1" %>

<br /><br />
                <ol class="breadcrumb">
                  <li><a href="default.aspx">Home</a></li>
                    <li><a href="webenrolment.aspx?page=~/webcontrols/checkout.ascx">Checkout</a></li>
                    <li><a href="webenrolment.aspx?page=~/webcontrols/checkout_enrolments.ascx">Personal Details</a></li>
                   
                  <li class="active">Further Details</li>
                </ol>
   
<div class="panel panel-danger">
    <div class="panel-heading">Personal Details - continued</div>
    <div class=" form-group">
        <cc1:StudentEnrolmentField StudentEnrolmentFieldType="knownas" ID="fldKnownAs" runat="server" IsRequired="false" LabelWidth="200"
            CustomCaption="Known As" />
    </div>
    <div class=" form-group">
        <cc1:StudentEnrolmentField StudentEnrolmentFieldType="UniqueLearnerNo" ID="StudentEnrolmentField1" runat="server" IsRequired="false" LabelWidth="200"
            CustomCaption="Unique Learner No. (if known)" />
    </div>
   <div class=" form-group">
              <cc1:StudentEnrolmentField ID="StudentEnrolmentField7"  StudentEnrolmentFieldType="NI"  runat="server" IsRequired="false" LabelWidth="600"  CustomCaption="NI Number" Pattern="^(?!BG)(?!GB)(?!NK)(?!KN)(?!TN)(?!NT)(?!ZZ)(?:[A-CEGHJ-PR-TW-Z][A-CEGHJ-NPR-TW-Z])(?:\s*\d\s*){6}([A-D]|\s)$" Title="Please enter an NI number in the correct format (AA123456A)" />
        </div>
    <div class=" form-group">
        <cc1:StudentEnrolmentField StudentEnrolmentFieldType="HeardAboutCollegeID" ID="StudentEnrolmentField4" runat="server" LabelWidth="200" CustomCaption="How did you hear 
            about the College?" />
    </div>
    <div class=" form-group">
        <cc1:StudentEnrolmentField StudentEnrolmentFieldType="GCSEMathsAchievementID" ID="StudentEnrolmentField5" runat="server"  LabelWidth="200"
          />
    </div>
    <div class=" form-group">
        <cc1:StudentEnrolmentField StudentEnrolmentFieldType="GCSEEnglishAchievementID" ID="StudentEnrolmentField3" runat="server" IsRequired="false" LabelWidth="200"
            />
    </div>
    
</div>

<div class="panel panel-danger">
    <div class="panel-heading">Vehicle Details</div>
    <div class=" form-group">
        <cc1:StudentEnrolmentField StudentEnrolmentFieldType="CarMake" ID="StudentEnrolmentField2" runat="server" IsRequired="false" LabelWidth="200" />
    </div>
    <div class=" form-group">
        <cc1:StudentEnrolmentField StudentEnrolmentFieldType="CarModel" ID="StudentEnrolmentField6" runat="server" IsRequired="false" LabelWidth="200" />
    </div>
    <div class=" form-group">
        <cc1:StudentEnrolmentField StudentEnrolmentFieldType="CarReg" ID="StudentEnrolmentField8" runat="server" IsRequired="false" LabelWidth="200" />
    </div>
</div>




        <cc1:CCCButton id="btnBack" runat="server" Text="Back" ImageResource="btnBack" LinkResource="checkout_enrolments_aspx"/>
        <cc1:CCCButton ID="btnContinue" runat="server" Text="Continue" ImageResource="btnContinue" LinkResource="checkout_parentguardian_aspx" CausesValidation="true" EnableEnterKey="true" SaveForLater="true" SaveForLaterIn="Request" />
    <br />
    <asp:ValidationSummary ID="ValidationSummary1" runat="server" CssClass="alert alert-danger" ForeColor="" />
