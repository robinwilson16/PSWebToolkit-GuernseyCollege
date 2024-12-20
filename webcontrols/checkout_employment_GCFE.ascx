<%@ Control Language="VB" AutoEventWireup="false" CodeFile="checkout_employment_GCFE.ascx.vb" Inherits="webcontrols_checkout_employment_GCFE" %>
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
        <li aria-current="page" class="breadcrumb-item pl-0 active pl-4"><i class="fa-solid fa-city"></i> Employment</li>
    </ol>
</nav>

<div class="progress mb-4" role="progressbar" aria-label="Animated striped example" aria-valuenow="80" aria-valuemin="0" aria-valuemax="100">
    <div class="progress-bar progress-bar-striped progress-bar-animated" style="width: 80%">80%</div>
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
            <li aria-current="page" class="breadcrumb-item pl-0 active pl-4"><i class="fa-solid fa-city"></i> Employment</li>
        </ol>
    </nav>

    <div class="progress mb-4" role="progressbar" aria-label="Animated striped example" aria-valuenow="80" aria-valuemin="0" aria-valuemax="100">
        <div class="progress-bar progress-bar-striped progress-bar-animated" style="width: 80%">80%</div>
    </div>
<%End If %>

<div class="bd-callout bd-callout-gcfe bd-callout-grey">
    <h4><i class="fa-solid fa-city"></i> Employment</h4>
        <div class="mb-3">
            <div class="alert alert-primary hstack gap-3" role="alert">
                <div>
                    <i class="fa-solid fa-circle-info"></i>
                </div>
                <div>
                    <p>
                        Fields in bold are mandatory
                    </p>
                </div>
            </div>


            <%If Course.EnrolmentTypeID = 6 Or Course.EnrolmentTypeID = 7 Then %> <!--Part-Time Work Related Or ACL-->
            <div class="alert alert-secondary hstack gap-3" role="alert">
                <div>
                    <i class="fa-solid fa-circle-info"></i>
                </div>
                <div>
                    <p>
                        As you have selected that your employer is paying your fees, please add their details below
                    </p>
                </div>
            </div>
            <%End If %>

            <select id="EmployerList" runat="server" ClientIDMode="Static" hidden>

            </select>
        </div>
            <p>
                Please enter the details of your employer
            </p>

   

    <!--<a href="#" class="show_hide btn btn-primary">No previous employment</a>-->
    <%--<asp:button runat="server" id="btnNoPreviousEmployment" cssClass="show_hide btn btn-primary" text="No previous employment" ToolTip="Click to remove all the items listed below (And hide the list)"></asp:button>--%>
<%--    <br />
<br />--%>
    <div id="slidingdiv" class="slidingDiv" runat="server">
<%--<asp:table class="table text-center" id="tblEmployment" runat="server">

</asp:table>--%>
    <%--<asp:button runat="server" id="btnAdd" cssClass="btn btn-primary" text="Add Row"></asp:button>--%>

<div class="row" runat="server" id="EmploymentRecords">

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
                <button type="button" class="btn btn-primary btn-lg NextButton">Next </button>
                <cc1:CCCButton ID="btnContinue" CssClass="d-none" class="d-none" ClientIDMode="Static" runat="server" Text="Continue" ImageResource="btnContinue" CausesValidation="true" EnableEnterKey="true" SaveForLater="true" SaveForLaterIn="EmploymentHistory" />
            </span>
        </div>
    </div>
</div>

<asp:ValidationSummary ID="ValidationSummary1" runat="server" CssClass="alert alert-danger"  />
    
<%If HasFormErrors = True Then%>
    <div class="alert alert-warning" role="alert">
        <%If Course.EnrolmentTypeID = 1 %>
            <i class="fa-solid fa-circle-question"></i> If you are having trouble with this form, please contact the Apprenticeship Team on <i class="fa-solid fa-phone"></i> <a href="tel:01481 227500">01481 227500</a> or by email at <i class="fa-solid fa-envelope"></i> <a href="mailto:apprenticeships@gcfe.net?subject=Online Application Form Query">apprenticeships@gcfe.net</a>
        <%Else %>
            <i class="fa-solid fa-circle-question"></i> If you are having trouble with this form, please contact Reception on <i class="fa-solid fa-phone"></i> <a href="tel:01481 227500">01481 227500</a> or by email at <i class="fa-solid fa-envelope"></i> <a href="mailto:enrolment@gcfe.net?subject=Online Application Form Query">enrolment@gcfe.net</a>
        <%End If %>
    </div>
<%End If %>
   
        <asp:HiddenField  id="NoEmployment" value="false" runat="server"  ClientIDMode="static" />
     <asp:HiddenField  id="intEmploymentRows" value="0" runat="server"  ClientIDMode="static" />

<script>
    document.addEventListener("DOMContentLoaded", function (event) {
        let employerDropDown = document.getElementById(`EmployerList`);
        //Sort Employer List
        sortSelectByText(employerDropDown);

        let selectBoxesEmp = document.querySelectorAll(`#aspnetForm select`);
        let rowNum = 0;

        let changeEvent = new Event(`change`);

        selectBoxesEmp.forEach(function (select) {
            if (select.classList.contains(`EmployerInput`) === true) {
                rowNum += 1;

                //Give the select an ID
                select.id = `EmployerInput${rowNum}`

                //Get elements
                let employerFieldNumFromID = select.id.replace(`ctl00_MainContentPlaceholder_ctl00_ctl`, ``);
                let employerFieldNumFromName = select.name.replace(`ctl00$MainContentPlaceholder$ctl00$ctl`, ``);
                let employerNameFieldNum = parseInt(employerFieldNumFromName) -1
                let employerInputCustomNum = parseInt(employerFieldNumFromName) + 3

                let employerNameFieldTxt = 
                    employerNameFieldNum.toLocaleString(
                        `en-GB`,
                        {
                            minimumIntegerDigits: 2,
                            useGrouping: false
                        }
                    );

                let employerInputCustomTxt = 
                    employerInputCustomNum.toLocaleString(
                        `en-GB`,
                        {
                            minimumIntegerDigits: 2,
                            useGrouping: false
                        }
                    );

                let fldEmployerName = document.getElementById(`ctl00_MainContentPlaceholder_ctl00_ctl${employerNameFieldTxt}_txtEmployerName`);
                let fldEmployerNameCustom = document.getElementsByName(`ctl00$MainContentPlaceholder$ctl00$ctl${employerInputCustomTxt}`)[0];

                //Hide original field
                fldEmployerName.classList.add("d-none");

                let selectInputBox = addSearchableDropDown(select);

                //Assign the drop-down
                select.innerHTML = employerDropDown.innerHTML;

                //Update original text field if the selection changes from the drop down
                select.addEventListener(`change`, function (event) {
                    fldEmployerName.value = select.options[select.selectedIndex].text;

                    //Clear out other fields so both cannot have values at once
                    fldEmployerNameCustom.value = ``;
                });

                //Update original text field from custom value if inputted
                fldEmployerNameCustom.addEventListener(`change`, function (event) {
                    fldEmployerName.value = fldEmployerNameCustom.value;

                    //Clear out drop-down so both cannot have values at once
                    select.value = ``;
                    selectInputBox.value = ``;
                    //select.dispatchEvent(changeEvent);
                });

                //Combine Contact Name and Email into Original Contact Email Field
                let contactNameFieldNum = parseInt(employerFieldNumFromName) + 11
                let contactEmailFieldNum = parseInt(employerFieldNumFromName) + 14
                let fldEmailNum = parseInt(employerFieldNumFromName) + 15

                let contactNameFieldTxt = 
                    contactNameFieldNum.toLocaleString(
                        `en-GB`,
                        {
                            minimumIntegerDigits: 2,
                            useGrouping: false
                        }
                    );

                let contactEmailFieldTxt = 
                    contactEmailFieldNum.toLocaleString(
                        `en-GB`,
                        {
                            minimumIntegerDigits: 2,
                            useGrouping: false
                        }
                    );

                let fldEmailTxt = 
                    fldEmailNum.toLocaleString(
                        `en-GB`,
                        {
                            minimumIntegerDigits: 2,
                            useGrouping: false
                        }
                    );

                let contactName = document.getElementsByName(`ctl00$MainContentPlaceholder$ctl00$ctl${contactNameFieldNum}`)[0];
                let contactEmail = document.getElementsByName(`ctl00$MainContentPlaceholder$ctl00$ctl${contactEmailFieldTxt}`)[0];
                let fldEmail = document.getElementById(`ctl00_MainContentPlaceholder_ctl00_ctl${fldEmailTxt}_txtEmail`);

                //Hide original field
                let fldEmailElement = contactEmail.nextSibling;
                fldEmailElement.classList.add(`d-none`);

                contactName.addEventListener(`keyup`, function (event) {
                    let nameAndEmail = `${contactName.value}|${contactEmail.value}`;

                    fldEmail.value = nameAndEmail;
                });

                contactEmail.addEventListener(`keyup`, function (event) {
                    let nameAndEmail = `${contactName.value}|${contactEmail.value}`;

                    fldEmail.value = nameAndEmail;
                });
            }
        });

    });
</script>