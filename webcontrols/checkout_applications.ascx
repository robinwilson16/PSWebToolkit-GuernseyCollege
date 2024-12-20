<%@ Control Language="VB" AutoEventWireup="false" CodeFile="checkout_applications.ascx.vb" Inherits="checkout_applications" %>
<%@ Register Assembly="PSWebEnrolmentKit" Namespace="CompassCC.ProSolution.PSWebEnrolmentKit"
    TagPrefix="cc1" %>
<%@ Register Src="~/webcontrols/StudentSignature.ascx" TagPrefix="uc1" TagName="StudentSignature" %>



<br />
<br />
<ol class="breadcrumb">
    <li><a href="default.aspx">Home</a></li>
    <li><a href="webenrolment.aspx?page=~/webcontrols/checkout.ascx">Checkout</a></li>
    <li class="active">Personal Details</li>
</ol>


<div class="panel panel-success">
    <div class="panel-heading">Personal Details</div>
    <div class=" form-group">
        <cc1:StudentEnrolmentField StudentEnrolmentFieldType="surname" ID="fldSurname" runat="server" IsRequired="true" LabelWidth="200" AutoFocus="true" />
    </div>
    <div class=" form-group">
        <cc1:StudentEnrolmentField StudentEnrolmentFieldType="FirstForename" ID="StudentEnrolmentField1" runat="server" IsRequired="true" LabelWidth="200" />
    </div>
    <div class=" form-group">
        <cc1:StudentEnrolmentField StudentEnrolmentFieldType="Title" ID="StudentEnrolmentField7" runat="server" IsRequired="true" LabelWidth="200" />
    </div>
    <div class=" form-group">
        <cc1:StudentEnrolmentField ID="datepicker" runat="server" IsRequired="false" StudentEnrolmentFieldType="DateOfBirth" LabelWidth="200" ClientIDMode="Static" Placeholder="dd/mm/yyyy" />
    </div>

    <div class=" form-group">
        <cc1:StudentEnrolmentField StudentEnrolmentFieldType="Sex" ID="StudentEnrolmentField4" runat="server" LabelWidth="200" />
    </div>
    <div class=" form-group">
        <%--CCCPS-77565 Add preferredpronoun field--%>
        <cc1:StudentEnrolmentField StudentEnrolmentFieldType="PreferredPronounID" ID="StudentEnrolmentField6" runat="server" LabelWidth="200" CustomCaption="Preferred Pronoun" />
    </div>
    <div class=" form-group">
        <cc1:StudentEnrolmentField StudentEnrolmentFieldType="Tel" ID="StudentEnrolmentField5" runat="server" LabelWidth="200" Pattern="^((\(?0\d{4}\)?\s?\d{3}\s?\d{3})|(\(?0\d{3}\)?\s?\d{3}\s?\d{4})|(\(?0\d{2}\)?\s?\d{4}\s?\d{4}))(\s?#(\d{4}|\d{3}))?$" Title="Please enter a valid Telephone Number" />
    </div>
    <div class=" form-group">
        <cc1:StudentEnrolmentField StudentEnrolmentFieldType="MobileTel" ID="StudentEnrolmentField2" runat="server" LabelWidth="200" Pattern="^(07[\d]{8,12}|447[\d]{7,11})$" Title="Please enter a valid Mobile Telephone Number" />
    </div>
    <div class=" form-group">
        <cc1:StudentEmailField StudentEnrolmentFieldType="Email" ID="StudentEnrolmentField3" runat="server" IsRequired="false" LabelWidth="200" HTML5InputType="email" Placeholder="e.g. name@domain.com" Pattern="^[a-zA-Z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-zA-Z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-zA-Z0-9](?:[a-zA-Z0-9-]*[a-zA-Z0-9])?\.)+[a-zA-Z0-9](?:[a-zA-Z0-9-]*[a-zA-Z0-9])?$" Title="Please enter a valid Email Address" />
    </div>
    <div class=" form-group">
        <cc1:StudentEnrolmentField runat="server" ID="fldAddress1" StudentEnrolmentFieldType="Address1" />
    </div>
    <div class=" form-group">
        <cc1:StudentEnrolmentField runat="server" ID="fldAddress2" StudentEnrolmentFieldType="Address2" />
    </div>
    <div class=" form-group">
        <cc1:StudentEnrolmentField runat="server" ID="fldAddress3" StudentEnrolmentFieldType="Address3" />
    </div>
    <div class=" form-group">
        <cc1:StudentEnrolmentField runat="server" ID="fldStudentFirstLanguageID" CustomFieldType="Lookup" StudentEnrolmentFieldType="StudentFirstLanguageID" />
    </div>
    <div class=" form-group">
        <label for="postcode" style="font-weight: normal">Postcode</label>
        <input runat="server" type="text" id="postcode" class="form-control" name="pre[postalcode]" placeholder="Your postcode here..." autocomplete="off" pattern="^([Gg][Ii][Rr] 0[Aa]{2})|((([A-Za-z][0-9]{1,2})|(([A-Za-z][A-Ha-hJ-Yj-y][0-9]{1,2})|(([A-Za-z][0-9][A-Za-z])|([A-Za-z][A-Ha-hJ-Yj-y][0-9][A-Za-z]?))))\s?[0-9][A-Za-z]{2})$" title="Please enter a valid Postcode" />
        <%--pattern="^([Gg][Ii][Rr] 0[Aa]{2})|((([A-Za-z][0-9]{1,2})|(([A-Za-z][A-Ha-hJ-Yj-y][0-9]{1,2})|(([A-Za-z][0-9][A-Za-z])|([A-Za-z][A-Ha-hJ-Yj-y][0-9][A-Za-z]?))))\s?[0-9][A-Za-z]{2})$" Title="Please enter a valid Postcode"--%>
    </div>
    <uc1:StudentSignature runat="server" ID="StudentSignature" />

</div>


<div class="panel panel-success">
    <div class="panel-heading">Photo</div>
    <p>If you would like to upload a photo, you can do that here. </p>
    <div class=" form-group">
        <asp:FileUpload ID="FileUpload1" runat="server" />
    </div>
</div>



<cc1:CCCButton ID="btnBack" runat="server" Text="Back" ImageResource="btnBack" LinkResource="checkout_aspx" />
<cc1:CCCButton ID="btnContinue" runat="server" Text="Continue" ImageResource="btnContinue" LinkResource="address_0_aspx" CausesValidation="true" SaveForLater="true" SaveForLaterIn="Request" />
<br />
<asp:ValidationSummary ID="ValidationSummary1" runat="server" CssClass="alert alert-danger" ForeColor="" />

