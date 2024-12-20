<%@ Control Language="VB"  AutoEventWireup="false" CodeFile="checkout_dataprotection_apponly_GCFE.ascx.vb" Inherits="checkout_dataprotection_apponly_GCFE"  %>

<%@ Register Assembly="PSWebEnrolmentKit" Namespace="CompassCC.ProSolution.PSWebEnrolmentKit"
    TagPrefix="cc1" %>

<%If Course.EnrolmentTypeID = 6 Or Course.EnrolmentTypeID = 7 Then %> <!--Part-Time Work Related Or ACL-->
    <nav aria-label="breadcrumb">
        <ol class="breadcrumb breadcrumb-arrow p-0">
            <li class="icon breadcrumb-item"><a href="http://guernseycollege.ac.gg/wp/the-guernsey-apprenticeship/" class="pl-3"><i class="fa-solid fa-house"></i> <span class="d-none d-sm-inline">GCFE Home</span></a></li>
            <li class="breadcrumb-item pl-0"><a href="webenrolment.aspx?page=~/apply/courseapply_GCFE.ascx&OfferingID=0"><i class="fa-regular fa-folder-open"></i> <span class="d-none d-sm-inline">Area</span></a></li>
            <li class="breadcrumb-item pl-0"><a href="webenrolment.aspx?page=~/apply/courseapply_GCFE.ascx&Dept=DEPT"><i class="fa-solid fa-book"></i> <span class="d-none d-sm-inline"><%=Course.Code%> - <%=Course.Name %></span></a></li>
            <li class="breadcrumb-item pl-0"><a href="webenrolment.aspx?page=~/webcontrols/checkout_personal_details_GCFE.ascx"><i class="fa-solid fa-user"></i> <span class="d-none d-sm-inline">Personal Details</span></a></li>
            <li class="breadcrumb-item pl-0"><a href="webenrolment.aspx?page=~/webcontrols/checkout_photo_GCFE.ascx"><i class="fa-solid fa-camera"></i> <span class="d-none d-sm-inline">Photo</span></a></li>
            <li class="breadcrumb-item pl-0"><a href="webenrolment.aspx?page=~/webcontrols/checkout_further_details_GCFE.ascx"><i class="fa-solid fa-hand-holding-medical"></i> <span class="d-none d-sm-inline">Supporting You</span></a></li>
            <li class="breadcrumb-item pl-0"><a href="webenrolment.aspx?page=~/webcontrols/checkout_emergency_contacts_GCFE.ascx"><i class="fa-solid fa-address-book"></i> <span class="d-none d-sm-inline">Emergency Contact Details</span></a></li>
            <li class="breadcrumb-item pl-0"><a href="webenrolment.aspx?page=~/webcontrols/checkout_fees_GCFE.ascx"><i class="fa-solid fa-sterling-sign"></i> <span class="d-none d-sm-inline">Paying Your Fees</span></a></li>
            <li class="breadcrumb-item pl-0"><a href="webenrolment.aspx?page=~/webcontrols/checkout_employment_GCFE.ascx"><i class="fa-solid fa-city"></i> <span class="d-none d-sm-inline">Employment</span></a></li>
            <li aria-current="page" class="breadcrumb-item pl-0 active pl-4"><i class="fa-solid fa-pen-to-square"></i> Declaration</li>
        </ol>
    </nav>

    <div class="progress mb-4" role="progressbar" aria-label="Animated striped example" aria-valuenow="90" aria-valuemin="0" aria-valuemax="100">
        <div class="progress-bar progress-bar-striped progress-bar-animated" style="width: 90%">90%</div>
    </div>
<%Else %>
    <nav aria-label="breadcrumb">
        <ol class="breadcrumb breadcrumb-arrow p-0">
            <li class="icon breadcrumb-item"><a href="http://guernseycollege.ac.gg/wp/the-guernsey-apprenticeship/" class="pl-3"><i class="fa-solid fa-house"></i> <span class="d-none d-sm-inline">GCFE Home</span></a></li>
            <li class="breadcrumb-item pl-0"><a href="webenrolment.aspx?page=~/apply/courseapply_GCFE.ascx&OfferingID=0"><i class="fa-regular fa-folder-open"></i> <span class="d-none d-sm-inline">Area</span></a></li>
            <li class="breadcrumb-item pl-0"><a href="webenrolment.aspx?page=~/apply/courseapply_GCFE.ascx&Dept=DEPT"><i class="fa-solid fa-book"></i> <span class="d-none d-sm-inline"><%=Course.Code%> - <%=Course.Name %></span></a></li>
            <li class="breadcrumb-item pl-0"><a href="webenrolment.aspx?page=~/webcontrols/checkout_personal_details_GCFE.ascx"><i class="fa-solid fa-user"></i> <span class="d-none d-sm-inline">Personal Details</span></a></li>
            <li class="breadcrumb-item pl-0"><a href="webenrolment.aspx?page=~/webcontrols/checkout_photo_GCFE.ascx"><i class="fa-solid fa-camera"></i> <span class="d-none d-sm-inline">Photo</span></a></li>
            <li class="breadcrumb-item pl-0"><a href="webenrolment.aspx?page=~/webcontrols/checkout_further_details_GCFE.ascx"><i class="fa-solid fa-hand-holding-medical"></i> <span class="d-none d-sm-inline">Supporting You</span></a></li>
            <li class="breadcrumb-item pl-0"><a href="webenrolment.aspx?page=~/webcontrols/checkout_emergency_contacts_GCFE.ascx"><i class="fa-solid fa-address-book"></i> <span class="d-none d-sm-inline">Emergency Contact Details</span></a></li>
            <li class="breadcrumb-item pl-0"><a href="webenrolment.aspx?page=~/webcontrols/checkout_quals_on_entry_GCFE.ascx"><i class="fa-solid fa-address-book"></i> <span class="d-none d-sm-inline">Qualifications on Entry</span></a></li>
            <li class="breadcrumb-item pl-0"><a href="webenrolment.aspx?page=~/webcontrols/checkout_attachments_GCFE.ascx"><i class="fa-solid fa-paperclip"></i> <span class="d-none d-sm-inline">Attachments</span></a></li>
            <li class="breadcrumb-item pl-0"><a href="webenrolment.aspx?page=~/webcontrols/checkout_fees_GCFE.ascx"><i class="fa-solid fa-sterling-sign"></i> <span class="d-none d-sm-inline">Paying Your Fees</span></a></li>
            <li class="breadcrumb-item pl-0"><a href="webenrolment.aspx?page=~/webcontrols/checkout_employment_GCFE.ascx"><i class="fa-solid fa-city"></i> <span class="d-none d-sm-inline">Employment</span></a></li>
            <li aria-current="page" class="breadcrumb-item pl-0 active pl-4"><i class="fa-solid fa-pen-to-square"></i> Declaration</li>
        </ol>
    </nav>

    <div class="progress mb-4" role="progressbar" aria-label="Animated striped example" aria-valuenow="90" aria-valuemin="0" aria-valuemax="100">
        <div class="progress-bar progress-bar-striped progress-bar-animated" style="width: 90%">90%</div>
    </div>
<%End If %>

<%If Not (Course.EnrolmentTypeID = 6 Or Course.EnrolmentTypeID = 7) Then %> <!--Part-Time Work Related Or ACL-->
    <div class="bd-callout bd-callout-gcfe bd-callout-grey">
        <h4><i class="fa-solid fa-handcuffs"></i> Criminal Convictions</h4>

        <p>
            You do not generally have to disclose details of ‘spent’ convictions. However, if the course you are applying for is exempt from the Rehabilitation of Offenders Law 2002 because it could involve access to persons who are disabled, addicted to drugs, alcohol or under 18 or over 65 years of age, you must disclose details of all convictions, spent or otherwise. 
        </p>
        <p>
            Courses requiring a full disclosure are also subject to a Disclosure & Barring Service (DBS) check. Disclosure of convictions does not automatically affect your admission to the course/programme, but allows the College to undertake a risk assessment and provide support as appropriate. 
        </p>

        <div class="mb-3 form-group">
            <cc1:StudentEnrolmentField StudentEnrolmentFieldType="CriminalConvictionID" ID="fldCriminalConvictionID" runat="server" IsRequired="true" LabelWidth="400"
                CustomCaption="Do you have any unspent convictions, cautions, reprimands and final warnings (or spent if applying for an area that is exempt from the Law)" ClientIDMode="Static" Title="Please state whether you have any criminal convictions" />
            <asp:CustomValidator ID="fldCriminalConvictionIDValidator" runat="server"></asp:CustomValidator>
        </div>
    </div>
<%End If %>

<div class="bd-callout bd-callout-gcfe bd-callout-grey">
    <h4><i class="fa-solid fa-gavel"></i> TGI Policies and Procedures</h4>
    <p>
        If I am accepted on to the course for which I am now applying, I agree to observe all <strong><a href="http://www.guernseycollege.ac.gg/wp/policies/" target="_blank" rel="noopener"><i class="fa-solid fa-arrow-up-right-from-square"></i> TGI Policies and Procedures</a></strong>.
    </p>
    <p>
        I understand that the College will store the information on this form in accordance with data protection legislation.
    </p>

    <div class="mb-3 form-group">
        <div class="grid-container-checkbox">
            <div class="grid-item-checkbox">
                <cc1:StudentEnrolmentField StudentEnrolmentFieldType="AcceptShareInfoConsent" ID="chkConfirm1" runat="server" IsRequired="true" LabelWidth="400"
                    CustomCaption=" " ClientIDMode="Static" Title="I agree to observe all TGI Policies and Procedures" />
            </div>
            <div class="grid-item fw-bold">
                <p>
                    I agree to observe all TGI Policies and Procedures
                </p>
            </div>
        </div>
        <asp:CustomValidator ID="chkConfirm1Validator" runat="server"></asp:CustomValidator>
    </div>

<%--       <p>
        <asp:CheckBox ID="chkConfirm" runat="server" Text="I agree to observe all TGI Policies and Procedures" Font-Bold="True" />
    </p>--%>
</div>

<div class="alert alert-primary" role="alert">
    <div class="grid-container">
        <div class="grid-item">
            <i class="fa-solid fa-comment"></i>
        </div>
        <div class="grid-item">
            <p>
                By submitting this application form, you agree for the College to contact you through any of its digital or other communications channels including (but not limited to) email, post, telephone, text.
            </p>
        </div>
    </div>
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
                <button type="button" class="btn btn-primary btn-lg NextButton">Submit </button>
                <cc1:CCCButton ID="btnContinue" CssClass="d-none" class="d-none" ClientIDMode="Static" runat="server" Text="Continue" ImageResource="btnContinue" CausesValidation="true" EnableEnterKey="true" />
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