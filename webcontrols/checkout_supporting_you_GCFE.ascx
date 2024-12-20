<%@ Control Language="VB" AutoEventWireup="false" CodeFile="checkout_supporting_you_GCFE.ascx.vb" Inherits="webcontrols_checkout_supporting_you_GCFE" %>
<%@ Register Assembly="PSWebEnrolmentKit" Namespace="CompassCC.ProSolution.PSWebEnrolmentKit"
    TagPrefix="cc1" %>

<nav aria-label="breadcrumb">
    <ol class="breadcrumb breadcrumb-arrow p-0">
        <li class="icon breadcrumb-item"><a href="http://guernseycollege.ac.gg/wp/the-guernsey-apprenticeship/" class="pl-3"><i class="fa-solid fa-house"></i> <span class="d-none d-sm-inline">GCFE Home</span></a></li>
        <li class="breadcrumb-item pl-0"><a href="webenrolment.aspx?page=~/apply/courseapply_GCFE.ascx&OfferingID=0"><i class="fa-regular fa-folder-open"></i> <span class="d-none d-sm-inline">Area</span></a></li>
        <li class="breadcrumb-item pl-0"><a href="webenrolment.aspx?page=~/apply/courseapply_GCFE.ascx&Dept=DEPT"><i class="fa-solid fa-book"></i> <span class="d-none d-sm-inline"><%=Course.Code%> - <%=Course.Name %></span></a></li>
        <li class="breadcrumb-item pl-0"><a href="webenrolment.aspx?page=~/webcontrols/checkout_personal_details_GCFE.ascx"><i class="fa-solid fa-user"></i> <span class="d-none d-sm-inline">Personal Details</span></a></li>
        <li class="breadcrumb-item pl-0"><a href="webenrolment.aspx?page=~/webcontrols/checkout_photo_GCFE.ascx"><i class="fa-solid fa-camera"></i> <span class="d-none d-sm-inline">Photo</span></a></li>
        <li aria-current="page" class="breadcrumb-item pl-0 active pl-4"><i class="fa-solid fa-hand-holding-medical"></i> Supporting You</li>
    </ol>
</nav>

<div class="progress mb-4" role="progressbar" aria-label="Animated striped example" aria-valuenow="30" aria-valuemin="0" aria-valuemax="100">
    <div class="progress-bar progress-bar-striped progress-bar-animated" style="width: 30%">30%</div>
</div>
   
<div class="bd-callout bd-callout-gcfe bd-callout-grey">
    <h4><i class="fa-solid fa-hand-holding-medical"></i> Supporting You</h4>

    <div class="mb-3 form-group">
        <div class="alert alert-primary" role="alert">
            <i class="fa-solid fa-circle-info"></i> Fields in bold are mandatory
        </div>
    </div>
        <div class="mb-3 form-group">
            <label for="selectLearnDiff" class="form-label textfieldlabelrequired">Do you consider that you have a learning difficulty, disability or health condition?</label>
      
            <asp:DropDownList runat="server" ID="selectLearnDiff"  CssClass="textfield form-control" AutoPostBack="true">
                <asp:ListItem Text="--Please Select--" Value=""></asp:ListItem>
                <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                <asp:ListItem Text="No" Value="2"></asp:ListItem>
                <%--<asp:ListItem Text="Rather not say" Value="9"></asp:ListItem>--%>
            </asp:DropDownList>
            <asp:CustomValidator ID="selectLearnDiffValidator" runat="server"></asp:CustomValidator>
          <div style="display:none">
             <cc1:StudentEnrolmentField Visible="true" ClientIDMode="Static" StudentEnrolmentFieldType="LearningDiffOrDisID" ID="fldLearningDiffOrDisID" runat="server" IsRequired="false"   />
      </div>
        <i> *It is your responsibility to disclose any support requirements. This will help the College make reasonable adjustments, limited by funding available.</i>
          </div>
    <br />

<div class="alert alert-secondary" id="slidingdiv" runat="server">
<%--    <div class="panel-heading">Disability Category</div>--%>
    <asp:Button runat="server" visible="false" ID="btnNoDisability" CssClass="button button-orange" Text="No learning difficulty, disability or health condition" ToolTip="Click to remove all the items listed below (And hide the list)"></asp:Button>

        <%--<asp:Table class="table table-striped text-center" ID="tblDisability" runat="server">--%>
            <%--<tr><th>Qualification</th><th>Subject (if not in list)</th><th>Predicted Grade</th><th>Grade</th><th>Date Awarded</th></tr>--%>
        <%--</asp:Table>--%>

        <p><strong>Learning difficulty, disability or health condition</strong></p>
        <p>Please indicate your primary 'main' condition which could impact on your studies</p>
        <div class="row" runat="server" id="Disabilities">

        </div>

        <asp:Button runat="server" ID="btnAdd" CssClass="btn btn-primary mb-3" Text="Add Row" CausesValidation="false"></asp:Button>

    <asp:CustomValidator ID="DisabilitiesValidator" runat="server"></asp:CustomValidator>

</div>


    <div class="mb-3 form-group">
        <cc1:StudentEnrolmentField StudentEnrolmentFieldType="DisabilityNotes" ID="StudentEnrolmentField1" runat="server" IsRequired="false" LabelWidth="400"
            CustomCaption="Please provide details of any allergies you have" />
    </div>
    <div class="mb-3 form-group">
        <cc1:StudentEnrolmentField StudentEnrolmentFieldType="NationalityID" ID="fldNationalityID" ClientIDMode="Static" runat="server" IsRequired="true" LabelWidth="400"
            c />
        <asp:CustomValidator ID="fldNationalityIDValidator" runat="server"></asp:CustomValidator>
    </div>
    <div class="mb-3 form-group">
        <cc1:StudentEnrolmentField runat="server" ID="fldStudentFirstLanguageID" ClientIDMode="Static" CustomFieldType="Lookup" StudentEnrolmentFieldType="StudentFirstLanguageID" IsRequired="true" LabelWidth="400" CustomCaption="First Language" Title="Please provide your first language" />
        <asp:CustomValidator ID="fldStudentFirstLanguageIDValidator" runat="server"></asp:CustomValidator>
    </div>
    <div class="mb-3 form-group">
        <cc1:StudentEnrolmentField StudentEnrolmentFieldType="EthnicGroupID" ID="fldEthnicGroupID" ClientIDMode="Static" runat="server" IsRequired="false" LabelWidth="400"
            CustomCaption="Ethnic Origin" />
        <asp:CustomValidator ID="fldEthnicGroupIDValidator" runat="server"></asp:CustomValidator>
    </div>
</div>
<cc1:NonEUValidator ID="nonEUValidator1" runat="server"></cc1:NonEUValidator>

<div class="alert alert-secondary" role="alert">
    <div class="row gap-3">
        <div class="col-md">
            <span class="d-grid gap-2 d-md-block">
                <button type="button" class="btn btn-secondary btn-lg BackButton"> Back</button>
                <cc1:CCCButton ID="btnBack" CssClass="d-none" class="d-none" ClientIDMode="Static" runat="server" Text="Back" ImageResource="btnBack" LinkResource="checkout_photo_GCFE_aspx" CausesValidation="false" />
            </span>
        </div>
        <div class="col-md text-end">
            <span class="d-grid gap-2 d-md-block">
                <button type="button" class="btn btn-primary btn-lg NextButton">Next </button>
                <cc1:CCCButton ID="btnContinue" CssClass="d-none" class="d-none" ClientIDMode="Static" runat="server" Text="Continue" ImageResource="btnContinue" LinkResource="checkout_emergency_contacts_GCFE_aspx" CausesValidation="true" SaveForLater="true" SaveForLaterIn="Request" />
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
        //Disabilities and Learning Difficulties
        let fldLearningDiffOrDisID = document.getElementById(`cboLearningDiffOrDisID`);

        //Rename options
        [...fldLearningDiffOrDisID.options].forEach(function (option) {
            if (option.value === `1`) {
                option.text = `I have a learning difficulty or disability`;
            }
            else if (option.value === `2`) {
                option.text = `I do not have a learning difficulty or disability`;
            }
            else if (option.value === `9`) {
                option.remove();
            }
        });

        let fldLearningDiffOrDisIDInputBox = addSearchableDropDown(fldLearningDiffOrDisID);

        //Disability Category
        //let fldDisabilityCategory1ID = document.getElementById("cboDisabilityCategory1ID");
        //let fldDisabilityCategory1IDInputBox = addSearchableDropDown(fldDisabilityCategory1ID);

        //Show or hide disability choice depending on if yes is selected
        let disabilityCategory1IDRow = document.getElementById("DisabilityCategory1IDRow");
        
        //if (fldLearningDiffOrDisID.value == 1) {
        //    disabilityCategory1IDRow.classList.remove("d-none");
        //}
        //else {
        //    disabilityCategory1IDRow.classList.add("d-none");
        //    fldDisabilityCategory1ID.value = null;
        //}

        fldLearningDiffOrDisID.addEventListener(`change`, function (event) {
            if (fldLearningDiffOrDisID.value == 1) {
                disabilityCategory1IDRow.classList.remove("d-none");
            }
            else {
                disabilityCategory1IDRow.classList.add("d-none");
                fldDisabilityCategory1ID.value = null;
            }
        });


        //Nationality
        let fldNationalityID = document.getElementById("cboNationalityID");
        sortSelectMoveToTop(fldNationalityID, "BRIT");

        //Remove Options
        [...fldNationalityID.options].forEach(function (option) {
            //Remove so can be re-added at the top
            if (option.value === `5805`) {
                option.remove();
            }
            else if (option.value === `5802`) {
                option.remove();
            }
            else if (option.value === `XF`) {
                option.remove();
            }
        });

        let fldNationalityIDInputBox = addSearchableDropDown(fldNationalityID);

        //First Language
        let fldStudentFirstLanguageID = document.getElementById("cboStudentFirstLanguageID");
        let fldStudentFirstLanguageIDInputBox = addSearchableDropDown(fldStudentFirstLanguageID);

        //Ethnic Group
        let fldEthnicGroupID = document.getElementById("cboEthnicGroupID");
        let fldEthnicGroupIDInputBox = addSearchableDropDown(fldEthnicGroupID);

        let selectBoxes = document.querySelectorAll(`#aspnetForm select`);

        selectBoxes.forEach(function (select) {
            if (select.name.indexOf(`cboDisabilityCategoryID`) >= 0) {
                let disabilityCategoryIDInputBox = addSearchableDropDown(select);
            }
        });

        let checkBoxes = document.querySelectorAll(`#aspnetForm input[type=checkbox]`);

        checkBoxes.forEach(function (checkbox) {
            if (checkbox.name.indexOf(`chkIsPrimary`) >= 0) {
                checkbox.addEventListener(`change`, function (event) {
                    untickOtherPrimaryOptions(checkbox);
                });
            }
        });

        function untickOtherPrimaryOptions(clickedCheckbox) {
            let checkBoxes = document.querySelectorAll(`#aspnetForm input[type=checkbox]`);

            checkBoxes.forEach(function (checkbox) {
                if (checkbox.name.indexOf(`chkIsPrimary`) >= 0) {
                    if (checkbox.id != clickedCheckbox.id) {
                        checkbox.checked = false;
                    }
                }
            });
        }
    });
</script>