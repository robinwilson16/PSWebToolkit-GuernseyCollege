<%@ Control Language="VB" AutoEventWireup="false" CodeFile="checkout_emergency_contacts_GCFE.ascx.vb" Inherits="webcontrols_checkout_emergency_contacts_GCFE" %>

<%@ Register Assembly="PSWebEnrolmentKit" Namespace="CompassCC.ProSolution.PSWebEnrolmentKit"
    TagPrefix="cc1" %>

<nav aria-label="breadcrumb">
    <ol class="breadcrumb breadcrumb-arrow p-0">
        <li class="icon breadcrumb-item"><a href="http://guernseycollege.ac.gg/wp/the-guernsey-apprenticeship/" class="pl-3"><i class="fa-solid fa-house"></i> <span class="d-none d-sm-inline">GCFE Home</span></a></li>
        <li class="breadcrumb-item pl-0"><a href="webenrolment.aspx?page=~/apply/courseapply_GCFE.ascx&OfferingID=0"><i class="fa-regular fa-folder-open"></i> <span class="d-none d-sm-inline">Area</span></a></li>
        <li class="breadcrumb-item pl-0"><a href="webenrolment.aspx?page=~/apply/courseapply_GCFE.ascx&Dept=DEPT"><i class="fa-solid fa-book"></i> <span class="d-none d-sm-inline"><%=Course.Code%> - <%=Course.Name %></span></a></li>
        <li class="breadcrumb-item pl-0"><a href="webenrolment.aspx?page=~/webcontrols/checkout_personal_details_GCFE.ascx"><i class="fa-solid fa-user"></i> <span class="d-none d-sm-inline">Personal Details</span></a></li>
        <li class="breadcrumb-item pl-0"><a href="webenrolment.aspx?page=~/webcontrols/checkout_photo_GCFE.ascx"><i class="fa-solid fa-camera"></i> <span class="d-none d-sm-inline">Photo</span></a></li>
        <li class="breadcrumb-item pl-0"><a href="webenrolment.aspx?page=~/webcontrols/checkout_further_details_GCFE.ascx"><i class="fa-solid fa-hand-holding-medical"></i> <span class="d-none d-sm-inline">Supporting You</span></a></li>
        <li aria-current="page" class="breadcrumb-item pl-0 active pl-4"><i class="fa-solid fa-address-book"></i> Emergency Contact Details</li>
    </ol>
</nav>

<div class="progress mb-4" role="progressbar" aria-label="Animated striped example" aria-valuenow="40" aria-valuemin="0" aria-valuemax="100">
    <div class="progress-bar progress-bar-striped progress-bar-animated" style="width: 40%">40%</div>
</div>

<div class="alert alert-warning" role="alert">
    <div class="row">
        <div class="col-sm-1">
            <h3><i class="fa-solid fa-triangle-exclamation"></i></h3>
        </div>
        <div class="col-sm-11">
            <%If RequiresNextOfKin = True %>
            As you were born after <%=ConsentDate.ToShortDateString %> you must provide us with the details of at least one parent or guardian.
            <%Else %>
            You must provide us with at least one emergency contact.
            <%End If %>
        </div>
    </div>
</div>

<div class="bd-callout bd-callout-gcfe bd-callout-grey">
    <h4><i class="fa-solid fa-address-book"></i> <asp:Label runat="server" ID="Contact1Label">Contact Details - Emergency Contact 1</asp:Label></h4>

<%--    <div class="mb-3 form-group">
        <cc1:StudentEnrolmentField StudentEnrolmentFieldType="Contact1ContactTypeID" ID="fldContact1ContactTypeID" runat="server" IsRequired="false" LabelWidth="200" CustomCaption="Contact Type" />
    </div>--%>
    <div class="mb-3 form-group">
        <label for="Contact1Forename" style="font-weight: bold">Forename</label>
        <input runat="server" type="text" id="Contact1Forename" class="form-control" name="Contact1Forename" IsRequired="true" ClientIDMode="Static" title="Please enter a valid Forename" />
        <asp:CustomValidator ID="Contact1ForenameValidator" runat="server"></asp:CustomValidator>
    </div>
    <div class="mb-3 form-group">
        <label for="Contact1Surname" style="font-weight: bold">Surname</label>
        <input runat="server" type="text" id="Contact1Surname" class="form-control" name="Contact1Surname" IsRequired="true" ClientIDMode="Static" title="Please enter a valid Surname" />
        <asp:CustomValidator ID="Contact1SurnameValidator" runat="server"></asp:CustomValidator>
    </div>
    <div class="mb-3 form-group d-none">
        <cc1:StudentEnrolmentField StudentEnrolmentFieldType="Contact1" ID="fldContact1Name" CssClass="d-block" runat="server" IsRequired="true" ClientIDMode="Static" LabelWidth="200" CustomCaption="Name" />
    </div>    
    <div class="mb-3 form-group">
        <cc1:StudentEnrolmentField StudentEnrolmentFieldType="Contact1Title" ID="fldContact1Title" runat="server" ClientIDMode="Static" IsRequired="false" LabelWidth="200" CustomCaption="Title" />
    </div>
    <div class="mb-3 form-group">
        <cc1:StudentEnrolmentField StudentEnrolmentFieldType="Contact1RelationshipID" ID="fldContact1RelationshipID" ClientIDMode="Static" runat="server" IsRequired="true" LabelWidth="200" CustomCaption="Relationship" />
    </div>
    <div class="mb-3 form-group">
        <cc1:StudentEnrolmentField StudentEnrolmentFieldType="Contact1MobileTel" ID="fldContact1MobileTel" runat="server" IsRequired="false" LabelWidth="200" HTML5InputType="tel" CustomCaption="Mobile" Pattern="^(07[\d]{8,12}|447[\d]{7,11})$" Title="Please enter a valid Mobile Telephone Number containing 11 digits" />
        <asp:CustomValidator ID="fldContact1MobileTelValidator" runat="server"></asp:CustomValidator>
    </div>
    <div class="mb-3 form-group">
        <div class="alert alert-secondary" role="alert">
            <i class="fa-solid fa-phone"></i> If the mobile number does not start with 07 then please enter it into the Home Landline box below instead
        </div>
    </div>
    <div class="mb-3 form-group">
        <cc1:StudentEnrolmentField StudentEnrolmentFieldType="Contact1Tel" ID="fldContact1Tel" runat="server" LabelWidth="200" HTML5InputType="tel" CustomCaption="Home Landline" Pattern="^\d{11}$" Title="Please enter a valid Home Telephone Number containing 11 digits" />
        <asp:CustomValidator ID="fldContact1TelValidator" runat="server"></asp:CustomValidator>
        <strong>* Please enter at least one phone number</strong>
    </div>
    <%If Not (Course.EnrolmentTypeID = 6 Or Course.EnrolmentTypeID = 7) %>
        <div class="mb-3 form-group">
            <cc1:StudentEnrolmentField StudentEnrolmentFieldType="Contact1EmailAddress" ID="fldContact1EmailAddress" runat="server" IsRequired="true" LabelWidth="200" HTML5InputType="email" CustomCaption="Email" Pattern="^[a-zA-Z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-zA-Z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-zA-Z0-9](?:[a-zA-Z0-9-]*[a-zA-Z0-9])?\.)+[a-zA-Z0-9](?:[a-zA-Z0-9-]*[a-zA-Z0-9])?$" />
        </div>
        <div class="mb-3 form-group">
            <cc1:StudentEnrolmentField StudentEnrolmentFieldType="IsLivingWithContact1" ID="fldIsLivingWithContact1" runat="server" IsRequired="false" LabelWidth="400" CustomCaption="Are You Living with this Person?" />
        </div>
    <%End If %>
    <%--<div class="mb-3 form-group">
        <cc1:StudentEnrolmentField StudentEnrolmentFieldType="Contact1ConsentGiven" ID="fldContact1ConsentGiven" runat="server" IsRequired="false" LabelWidth="400" CustomCaption="May We Contact this Person About Your Studies?" />
    </div>
    <div class="mb-3 form-group">
        <cc1:StudentEnrolmentField StudentEnrolmentFieldType="Contact1AcceptMarketingConsent" ID="fldContact1AcceptMarketingConsent" runat="server" IsRequired="false" LabelWidth="400" CustomCaption="May We Contact this Person for Marketing and Future Course Opportunities?" />
    </div>

    <div class="mb-3 form-group">
        <cc1:StudentEnrolmentField StudentEnrolmentFieldType="Contact1ConsentGivenDate" ID="fldContact1ConsentGivenDate" ClientIDMode="Static" HTML5InputType="date" runat="server" IsRequired="false" LabelWidth="200" CustomCaption="Consent Given Date" />
    </div>
    <div class="mb-3 form-group">
        <cc1:StudentEnrolmentField StudentEnrolmentFieldType="Contact1AcceptShareInfoConsent" ID="fldContact1AcceptShareInfoConsent" runat="server" IsRequired="false" LabelWidth="400" CustomCaption="May We Share Your Information with this Person?" />
    </div>--%>
</div>

<div class="bd-callout bd-callout-gcfe bd-callout-grey">
    <h4><i class="fa-solid fa-address-book"></i> <asp:Label runat="server" ID="Contact2Label">Contact Details - Emergency Contact 2</asp:Label></h4>

    <div class="mb-3 form-group">
        <div class="alert alert-primary" role="alert">
            <i class="fa-solid fa-circle-info"></i> Fields in bold are mandatory
        </div>
    </div>
    <%--<div class="mb-3 form-group">
        <cc1:StudentEnrolmentField StudentEnrolmentFieldType="Contact2ContactTypeID" ID="StudentEnrolmentField39" runat="server" IsRequired="false" LabelWidth="200" CustomCaption="Contact Type" />
    </div>--%>
    <div class="mb-3 form-group">
        <label for="Contact1Forename" style="font-weight: normal">Forename</label>
        <input runat="server" type="text" id="Contact2Forename" class="form-control" name="Contact2Forename" IsRequired="false" ClientIDMode="Static" title="Please enter a valid Forename" />
    </div>
    <div class="mb-3 form-group">
        <label for="Contact1Surname" style="font-weight: normal">Surname</label>
        <input runat="server" type="text" id="Contact2Surname" class="form-control" name="Contact2Surname" IsRequired="false" ClientIDMode="Static" title="Please enter a valid Surname" />
    </div>
    <div class="mb-3 form-group d-none">
        <cc1:StudentEnrolmentField StudentEnrolmentFieldType="Contact2" ID="fldContact2Name" runat="server" IsRequired="false" ClientIDMode="Static" LabelWidth="200" CustomCaption="Name" />
    </div>    
    <div class="mb-3 form-group">
        <cc1:StudentEnrolmentField StudentEnrolmentFieldType="Contact2Title" ID="fldContact2Title" runat="server" ClientIDMode="Static" IsRequired="false" LabelWidth="200" CustomCaption="Title" />
    </div>
    <div class="mb-3 form-group">
        <cc1:StudentEnrolmentField StudentEnrolmentFieldType="Contact2RelationshipID" ID="fldContact2RelationshipID" ClientIDMode="Static" runat="server" IsRequired="false" LabelWidth="200" CustomCaption="Relationship" />
    </div>
    <div class="mb-3 form-group">
        <cc1:StudentEnrolmentField StudentEnrolmentFieldType="Contact2MobileTel" ID="fldContact2MobileTel" runat="server" IsRequired="false" LabelWidth="200" HTML5InputType="tel" CustomCaption="Mobile" Pattern="^(07[\d]{8,12}|447[\d]{7,11})$" Title="Please enter a valid Mobile Telephone Number containing 11 digits" />
        <asp:CustomValidator ID="fldContact2MobileTelValidator" runat="server"></asp:CustomValidator>
        <strong>* Please enter at least one phone number</strong>
    </div>
    <div class="mb-3 form-group">
        <div class="alert alert-secondary" role="alert">
            <i class="fa-solid fa-phone"></i> If the mobile number does not start with 07 then please enter it into the Home Landline box below instead
        </div>
    </div>
    <div class="mb-3 form-group">
        <cc1:StudentEnrolmentField StudentEnrolmentFieldType="Contact2Tel" ID="fldContact2Tel" runat="server" LabelWidth="200" HTML5InputType="tel" Pattern="^\d{11}$" Title="Please enter a valid Home Telephone Number containing 11 digits" CustomCaption="Home Landline" />
        <asp:CustomValidator ID="fldContact2TelValidator" runat="server"></asp:CustomValidator>
    </div>
    <%If Not (Course.EnrolmentTypeID = 6 Or Course.EnrolmentTypeID = 7) %>
        <div class="mb-3 form-group">
            <cc1:StudentEnrolmentField StudentEnrolmentFieldType="Contact2EmailAddress" ID="fldContact2EmailAddress" runat="server" LabelWidth="200" HTML5InputType="email" CustomCaption="Email" Pattern="^[a-zA-Z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-zA-Z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-zA-Z0-9](?:[a-zA-Z0-9-]*[a-zA-Z0-9])?\.)+[a-zA-Z0-9](?:[a-zA-Z0-9-]*[a-zA-Z0-9])?$" />
        </div>
        <div class="mb-3 form-group">
            <cc1:StudentEnrolmentField StudentEnrolmentFieldType="IsLivingWithContact2" ID="fldIsLivingWithContact2" runat="server" IsRequired="false" LabelWidth="400" CustomCaption="Are You Living with this Person?" />
        </div>
    <%End If %>
    <%--<div class="mb-3 form-group">
        <cc1:StudentEnrolmentField StudentEnrolmentFieldType="Contact2ConsentGiven" ID="fldContact2ConsentGiven" runat="server" IsRequired="false" LabelWidth="400" CustomCaption="May We Contact this Person About Your Studies?" />
    </div>
    <div class="mb-3 form-group">
        <cc1:StudentEnrolmentField StudentEnrolmentFieldType="Contact2AcceptMarketingConsent" ID="fldContact2AcceptMarketingConsent" runat="server" IsRequired="false" LabelWidth="400" CustomCaption="May We Contact this Person for Marketing and Future Course Opportunities?" />
    </div>
    <div class="mb-3 form-group">
        <cc1:StudentEnrolmentField StudentEnrolmentFieldType="Contact2ConsentGivenDate" ID="fldContact2ConsentGivenDate" ClientIDMode="Static" HTML5InputType="date" runat="server" IsRequired="false" LabelWidth="200" CustomCaption="Consent Given Date" />
    </div>
    <div class="mb-3 form-group">
        <cc1:StudentEnrolmentField StudentEnrolmentFieldType="Contact2AcceptShareInfoConsent" ID="fldContact2AcceptShareInfoConsent" runat="server" IsRequired="false" LabelWidth="400" CustomCaption="May We Share Your Information with this Person?" />
    </div>--%>
</div>

<div class="alert alert-secondary" role="alert">
    <div class="row gap-3">
        <div class="col-md">
            <span class="d-grid gap-2 d-md-block">
                <button type="button" class="btn btn-secondary btn-lg BackButton"> Back</button>
                <cc1:CCCButton ID="btnBack" CssClass="d-none" class="d-none" ClientIDMode="Static" runat="server" Text="Back" ImageResource="btnBack" CausesValidation="false" />
            </span>
        </div>
        <div class="col-md text-end">
            <span class="d-grid gap-2 d-md-block">
                <button type="button" class="btn btn-primary btn-lg NextButton">Next </button>
                <cc1:CCCButton ID="btnContinue" CssClass="d-none" class="d-none" ClientIDMode="Static" runat="server" Text="Continue" ImageResource="btnContinue" CausesValidation="true" EnableEnterKey="true" SaveForLater="true" SaveForLaterIn="Request" />
            </span>
        </div>
    </div>
</div>

<asp:ValidationSummary ID="ValidationSummary1" runat="server" CssClass="alert alert-danger" ForeColor="" />

<%If HasFormErrors = True Then%>
    <div class="alert alert-warning" role="alert">
        <%If Course.EnrolmentTypeID = 1 %>
            <i class="fa-solid fa-circle-question"></i> If you are having trouble with this form, please contact the Apprenticeship Team on <i class="fa-solid fa-phone"></i> <a href="tel:01481 227500">01481 227500</a> or by email at <i class="fa-solid fa-envelope"></i> <a href="mailto:apprenticeships@gcfe.net?subject=Online Application Form Query">apprenticeships@gcfe.net</a>
        <%Else %>
            <i class="fa-solid fa-circle-question"></i> If you are having trouble with this form, please contact Reception on <i class="fa-solid fa-phone"></i> <a href="tel:01481 227500">01481 227500</a> or by email at <i class="fa-solid fa-envelope"></i> <a href="mailto:enrolment@gcfe.net?subject=Online Application Form Query">enrolment@gcfe.net</a>
        <%End If %>
    </div>
<%End If %>

<script>
    document.addEventListener("DOMContentLoaded", function (event) {
        let contact1Forename = document.getElementById(`Contact1Forename`);
        let contact1Surname = document.getElementById(`Contact1Surname`);
        let contact2Forename = document.getElementById(`Contact2Forename`);
        let contact2Surname = document.getElementById(`Contact2Surname`);

        //Set initial values if contact field already has a value in it
        setContactName(`txtContact1`, `Contact1Forename`, `Contact1Surname`);
        setContactName(`txtContact2`, `Contact2Forename`, `Contact2Surname`);

        contact1Forename.addEventListener(`keyup`, function (event) {
            updateContactName(`txtContact1`, `Contact1Forename`, `Contact1Surname`);
        });
        contact1Surname.addEventListener(`keyup`, function (event) {
            updateContactName(`txtContact1`, `Contact1Forename`, `Contact1Surname`);
        });
        contact2Forename.addEventListener(`keyup`, function (event) {
            updateContactName(`txtContact2`, `Contact2Forename`, `Contact2Surname`);
        });
        contact2Surname.addEventListener(`keyup`, function (event) {
            updateContactName(`txtContact2`, `Contact2Forename`, `Contact2Surname`);
        });

        //Re-order fields so most common values are moved to the top
        let titleContact1Dropdown = document.getElementById(`cboContact1Title`);
        sortSelectMoveToTop(titleContact1Dropdown, "Ms");
        sortSelectMoveToTop(titleContact1Dropdown, "Miss");
        sortSelectMoveToTop(titleContact1Dropdown, "Mrs");
        sortSelectMoveToTop(titleContact1Dropdown, "Mr");
        sortSelectMoveToTop(titleContact1Dropdown, "");
        let titleContact1DropdownInputBox = addSearchableDropDown(titleContact1Dropdown);

        let fldContact1RelationshipID = document.getElementById(`cboContact1RelationshipID`);
        sortSelectMoveToTop(fldContact1RelationshipID, "2");
        let fldContact1RelationshipIDInputBox = addSearchableDropDown(fldContact1RelationshipID);

        let titleContact2Dropdown = document.getElementById(`cboContact2Title`);
        sortSelectMoveToTop(titleContact2Dropdown, "Ms");
        sortSelectMoveToTop(titleContact2Dropdown, "Miss");
        sortSelectMoveToTop(titleContact2Dropdown, "Mrs");
        sortSelectMoveToTop(titleContact2Dropdown, "Mr");
        sortSelectMoveToTop(titleContact2Dropdown, "");
        let titleContact2DropdownInputBox = addSearchableDropDown(titleContact2Dropdown);

        let fldContact2RelationshipID = document.getElementById(`cboContact2RelationshipID`);
        sortSelectMoveToTop(fldContact2RelationshipID, "2");
        let fldContact2RelationshipIDInputBox = addSearchableDropDown(fldContact2RelationshipID);
    });
</script>