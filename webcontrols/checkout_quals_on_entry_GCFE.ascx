<%@ control  Language="VB"  AutoEventWireup="false" CodeFile="checkout_quals_on_entry_GCFE.ascx.vb" Inherits="checkout_quals_on_entry_GCFE" %>
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
        <li class="breadcrumb-item pl-0"><a href="webenrolment.aspx?page=~/webcontrols/checkout_emergency_contacts_GCFE.ascx"><i class="fa-solid fa-address-book"></i> <span class="d-none d-sm-inline">Emergency Contact Details</span></a></li>
        <li aria-current="page" class="breadcrumb-item pl-0 active pl-4"><i class="fa-solid fa-award"></i> Qualifications on Entry</li>
    </ol>
</nav>

<div class="progress mb-4" role="progressbar" aria-label="Animated striped example" aria-valuenow="55" aria-valuemin="0" aria-valuemax="100">
    <div class="progress-bar progress-bar-striped progress-bar-animated" style="width: 55%">55%</div>
</div>

<div class="bd-callout bd-callout-gcfe bd-callout-grey">
    <h4><i class="fa-solid fa-award"></i> Qualifications on Entry</h4>
     
        <div class="row">
        <div class="col-sm-12 columns form-group">

            <cc1:studentenrolmentfield id="fldPriorAttainmentLevelID" runat="server" customcaption="What is the highest level of qualification you already have?" ExcludedIDValues="" isrequired="true" labelwidth="450" StudentEnrolmentFieldType="PriorAttainmentLevelID" ClientIDMode="Static"></cc1:studentenrolmentfield>
            <asp:CustomValidator ID="fldPriorAttainmentLevelIDValidator" runat="server"></asp:CustomValidator>
        <span id="showItem"><a class="btn btn-primary mt-3 mb-3" role="button">Unsure? Click me to help you decide</a></span>
            </div>
    </div>  

       <div id="priorDiv">
               <p>
        Use the following table to help you pick the correct qualification level...
    </p>
    <table class="table table-striped table-bordered">
          <tr class="active"><th>Level</th><th>Academic Qualification Equivalent<div style="float:right" ><span id="hideItem"><a class="btn btn-secondary" role="button">Hide table</a></span></div></th></tr>
           
            <tr>
                <td>Entry Level</td>
                <td>First certificate
GCSE - grades 3, 2, 1 or grades D, E, F, G
level 1 award
level 1 certificate
level 1 diploma
level 1 ESOL
level 1 essential skills
level 1 functional skills
level 1 national vocational qualification (NVQ)
music grades 1, 2 and 3</td>
            </tr>
        <tr>
            <td>Level 1</td>
            <td>For example, GCSE grades 1-3/F-D, First certificate, Level 1 Award/Certificate/Diploma, Functional Skill Level 1
            </td>

        </tr>
        <tr>
            <td>Full Level 2</td>
            <td>GCSE/O Level (5 or more GCSEs grades A*-C),
GCSE (9-1) (5 or more GCSEs grade 4 or above),
2 or 3 AS Levels (for AS qualifications regulated before 1 September 2015),
CSE Grade 1 (5 or more),
1 A Level,
Higher 14-19 Diploma,
QCF Diploma level 2,
QCF Certificate level 2 at 13 or 14 credits undertaken before 2014 to 2015,
Level 2 Principal Learning,
NVQ level 2,
GNVQ Intermediate,
            </td>

        </tr>
        <tr>
            <td>Full Level 3</td>
            <td>A Levels (2 or more advanced level passes),
4 or more AS Levels (for AS qualifications regulated before 1 September 2015),
QCF Diploma Level 3,
QAA Access to HE,
Advanced 14-19 Diploma,
GNVQ Advanced,
NVQ level 3,
AVCE double award,
Tech Levels which are in the 16-19 Performance Tables,
Applied Generals which are in the 16-19 Performance Tables (from 1 August
2015),
QCF Certificate at Level 3 which is 30 to 36 credits</td>

        </tr>

        <%If Is19Plus = True Then %>
            <tr>
                <td>Level 4</td>
                <td>For example, Certificate of Higher Education, Higher Apprenticeship, HNC</td>

            </tr>
            <tr>
                <td>Level 5</td>
                <td>For example, Diploma of Higher Education, Foundation Degree, HND
                </td>

            </tr>
            <tr>

                <td>Level 6
                </td>
                <td>For example, Degree Apprenticeship, Honours Degree
                </td>


            </tr>
            <tr>
                <td>Level 7
                </td>
                <td>For example, Masters level degree, PgDip

                </td>

            </tr>
            <tr>
                <td>Level 8
                </td>
                <td>For example, Doctorate/PhD

                </td>


            </tr>
        <%End If %>
    </table>

</div>
    <p>
        If you have not yet taken your exams, please provide us with your predicted grades. 
        Please include information on all qualifications. ie GCSE, NVQs, BTEC, Diplomas, A levels or any other subjects you may have studied. 
    </p> 
    <p>
        Please enter the details of your prior qualifications, choosing from the drop-down lists below.

    </p>

    <div class="alert alert-primary" role="alert">
        <div class="grid-container">
            <div class="grid-item">
                <h1><i class="fa-solid fa-circle-info"></i></h1>
            </div>
            <div class="grid-item">
                <p>
                    Apprentices should achieve the English and/or maths at a level below the qualification level attached to the apprenticeship. For example, if you are studying on a Level 3 apprenticeship course, you should achieve a Level 2 in both English and maths before the end of the programme. 
                </p>
                <p>
                    If you have already achieved the required level of English and maths before starting the apprenticeship, there will be no requirement to study these qualifications. You will need to supply evidence to support this.
                </p>
            </div>
        </div>
    </div>

    <asp:button runat="server" id="btnNoPriorQualifications" cssClass="show_hide btn btn-primary" text="No prior qualifications" ToolTip="Click to remove all the items listed below (And hide the list)"></asp:button>
    <br />
<br />
    <div id="slidingdiv" class="slidingDiv" runat="server">
<%--<asp:table class="table table-striped text-center" id="tblQuals" runat="server">--%>
<%--<tr><th>Qualification</th><th>Subject (if not in list)</th><th>Predicted Grade</th><th>Grade</th><th>Date Awarded</th></tr>--%>

<%--</asp:table>--%>

    <div class="row" runat="server" id="QOEs">

    </div>

    <asp:button runat="server" id="btnAdd" cssClass="btn btn-primary" text="Add Row"></asp:button>

    </div>
   
    </div>

<div class="alert alert-secondary" role="alert">
    <div class="row gap-3">
        <div class="col-md">
            <span class="d-grid gap-2 d-md-block">
                <button type="button" class="btn btn-secondary btn-lg BackButton"> Back</button>
                <cc1:CCCButton ID="btnBack" CssClass="d-none" class="d-none" ClientIDMode="Static" runat="server" Text="Back" ImageResource="btnBack" CausesValidation="false" LinkResource="checkout_emergency_contacts_GCFE_aspx" />
            </span>
        </div>
        <div class="col-md text-end">
            <span class="d-grid gap-2 d-md-block">
                <button type="button" class="btn btn-primary btn-lg NextButton">Next </button>
                <cc1:CCCButton ID="btnContinue" CssClass="d-none" class="d-none" ClientIDMode="Static" runat="server" Text="Continue" ImageResource="btnContinue" CausesValidation="true" EnableEnterKey="true" SaveForLater="true" SaveForLaterIn="QualsOnEntry" />
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
   
        <asp:HiddenField  id="NoQuals" value="false" runat="server"  ClientIDMode="static" />
     <asp:HiddenField  id="intQualRows" value="0" runat="server"  ClientIDMode="static" />

<select id="GradeList" runat="server" ClientIDMode="Static" hidden>

</select>

<select id="GradeListGCSE" runat="server" ClientIDMode="Static" hidden>

</select>

<script>
    document.addEventListener("DOMContentLoaded", function (event) {
        let gradeAllDropDown = document.getElementById(`GradeList`);
        let gradeGCSEDropDown = document.getElementById(`GradeListGCSE`);

        let selectBoxes = document.querySelectorAll(`#aspnetForm select`);

        let qoeChangeEvent = new Event('change');

        selectBoxes.forEach(function (select) {
            if (select.name.indexOf(`cboQualID`) >= 0) {
                //Get elements
                let subjectDropdown = select.id;
                let subjectFieldNum = subjectDropdown.replace(`ctl00_MainContentPlaceholder_ctl00_ctl`, ``).replace(`_cboQualID`, ``);
                let subjectCustomFieldNum = parseInt(subjectFieldNum) + 2
                let predictedGradeFieldNum = parseInt(subjectFieldNum) + 4
                let gradeFieldNum = parseInt(subjectFieldNum) + 6 //Was 4

                let subjectCustomFieldNumTxt =
                    subjectCustomFieldNum.toLocaleString(
                        `en-GB`,
                        {
                            minimumIntegerDigits: 2,
                            useGrouping: false
                        }
                    );

                let predictedGradeFieldNumTxt =
                    predictedGradeFieldNum.toLocaleString(
                        `en-GB`,
                        {
                            minimumIntegerDigits: 2,
                            useGrouping: false
                        }
                    );

                let gradeFieldNumTxt =
                    gradeFieldNum.toLocaleString(
                        `en-GB`,
                        {
                            minimumIntegerDigits: 2,
                            useGrouping: false
                        }
                    );

                let subjectInputBox = addSearchableDropDown(select);

                let subjectCustomField = document.getElementById(`ctl00_MainContentPlaceholder_ctl00_ctl${subjectCustomFieldNumTxt}_txtSubject`);
                let predictedGradeDropDown = document.getElementById(`ctl00_MainContentPlaceholder_ctl00_ctl${predictedGradeFieldNumTxt}_cboPredictedGrade`);
                let gradeDropDown = document.getElementById(`ctl00_MainContentPlaceholder_ctl00_ctl${gradeFieldNumTxt}_cboGrade`);

                let predictedGradeInputBox = addSearchableDropDown(predictedGradeDropDown);
                let gradeInputBox = addSearchableDropDown(gradeDropDown);

                //If custom field entered then clear original drop-down
                subjectCustomField.addEventListener(`change`, function (event) {
                    select.value = ``;
                    subjectInputBox.value = `-- Please Select --`;

                    //select.dispatchEvent(qoeChangeEvent);
                });

                select.addEventListener(`change`, function (event) {
                    //Clear custom value if one chosen from drop-down
                    subjectCustomField.value = ``;

                    let selectText = select.options[select.selectedIndex].text;

                    if (selectText.indexOf("GCSE") >= 0) {
                        gradeDropDown.innerHTML = gradeGCSEDropDown.innerHTML;
                        predictedGradeDropDown.innerHTML = gradeGCSEDropDown.innerHTML;
                    }
                    else {
                        gradeDropDown.innerHTML = gradeAllDropDown.innerHTML;
                        predictedGradeDropDown.innerHTML = gradeAllDropDown.innerHTML;
                    }
                    gradeDropDown.selectedIndex = 0;
                    predictedGradeDropDown.selectedIndex = 0;
                });
            }
        });
    });
</script>

  <script type="text/javascript">
      var divToHide = $('div#priorDiv');
      var hideBtn = $('span#hideItem');
      var hideBtn2 = $('span#hideItem2');
      var showBtn = $('span#showItem');

      showBtn.click(function () {
          divToHide.show();
      });

      hideBtn.click(function () {
          divToHide.hide();
      });

      hideBtn2.click(function () {
          divToHide.hide();
      });

      $(document).ready(function () {
          //alert();
          divToHide.hide();
<%--          $("#divYourQuals").hide();
        $('#<%= fldPriorAttainmentLevelID.ClientID %>').change(function () {
             if ($("#<%=fldPriorAttainmentLevelID.ClientID%>").val() == "99") {
                 $("#divYourQuals").hide();
                 return;
             }
             else {
                $("#divYourQuals").show();
             }
         });--%>

  });
  </script>