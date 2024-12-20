<%@ Control Language="VB" AutoEventWireup="false" CodeFile="checkout_fees_GCFE.ascx.vb" Inherits="webcontrols_checkout_fees_GCFE" %>
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
            <li aria-current="page" class="breadcrumb-item pl-0 active pl-4"><i class="fa-solid fa-sterling-sign"></i> Paying Your Fees</li>
        </ol>
    </nav>

    <div class="progress mb-4" role="progressbar" aria-label="Animated striped example" aria-valuenow="50" aria-valuemin="0" aria-valuemax="100">
        <div class="progress-bar progress-bar-striped progress-bar-animated" style="width: 50%">50%</div>
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
            <li aria-current="page" class="breadcrumb-item pl-0 active pl-4"><i class="fa-solid fa-sterling-sign"></i> Paying Your Fees</li>
        </ol>
    </nav>

    <div class="progress mb-4" role="progressbar" aria-label="Animated striped example" aria-valuenow="80" aria-valuemin="0" aria-valuemax="100">
        <div class="progress-bar progress-bar-striped progress-bar-animated" style="width: 80%">80%</div>
    </div>
<%End If %>
   
<div class="bd-callout bd-callout-gcfe bd-callout-grey">
    <h4><i class="fa-solid fa-sterling-sign"></i> Paying Your Fees</h4>

    <div class="mb-3 form-group">
        <div class="alert alert-primary" role="alert">
            <i class="fa-solid fa-circle-info"></i> Fields in bold are mandatory
        </div>
    </div>
    <div class="card w-100 mb-3">
        <div class="card-body d-flex flex-column">
            <div class="card-title"><strong><%=Course.Code%> - <%=Course.Name %></strong></div>
            <div class="row">
                <div class="col-md-6 col-xl-3 mb-3">
                    <i class="fa-solid fa-calendar-days"></i> Start Date: 
                        <%=Convert.ToDateTime(Course.StartDate).ToString("dd/MM/yyyy")%>
                </div>
                <div class="col-md-6 col-xl-3 mb-3">
                    <i class="fa-solid fa-flag-checkered"></i> End Date: 
                        <%=Convert.ToDateTime(Course.EndDate).ToString("dd/MM/yyyy")%>
                </div>
                <div class="col-md-6 col-xl-3 mb-3">
                    <i class="fa-solid fa-clock"></i> Start Time: 
                        <%If String.IsNullOrEmpty(Course.StartTime.ToString()) Then %>
                            TBC
                        <%Else %>
                            <%=Course.StartTime%>
                        <%End If %>
                </div>
                <div class="col-md-6 col-xl-3 mb-3">
                    <i class="fa-regular fa-clock"></i> End Time: 
                        <%If String.IsNullOrEmpty(Course.EndTime.ToString()) Then %>
                            TBC
                        <%Else %>
                            <%=Course.EndTime%>
                        <%End If %>
                </div>
                <div class="col-md-6 col-xl-3 mb-3">
                    <i class="fa-solid fa-calendar-day"></i> Day(s): 
                        <%If String.IsNullOrEmpty(Course.DayOfWeek) Then %>
                            TBC
                        <%Else %>
                            <%=Course.DayOfWeek%>
                        <%End If %>
                </div>
                <div class="col-md-6 col-xl-3 mb-3">
                    <i class="fa-solid fa-hourglass-half"></i> Suitable for Ages: 
                        <kbd>18+</kbd>
                </div>
            </div>
        </div>
    </div>

    <div class="card w-100">
        <div class="card-body d-flex flex-column">
            <div class="card-title"><strong>Course Fees</strong></div>
            <div class="row">
                <div class="col-md-6 col-xl-6 mb-3">
                    <i class="fa-solid fa-sterling-sign"></i> Tuition Fee:
                </div>
                <div class="col-md-6 col-xl-6 mb-3 text-end">
                    <i class="fa-solid fa-sterling-sign"></i>
                        <%If Not IsNothing(CourseFee) Then %>
                            <%If CourseFee.Amount > 0 Then %>
                                <span class="NonFreeCourse"><%=FormatCurrency(CourseFee.Amount, 0)%></span>
                            <%Else %>
                                <span class="FreeCourse">Free</span>
                            <%End If %>
                        <%Else %>
                            <span class="FeeUnknownCourse">TBC</span>
                        <%End If %>
                </div>
                <div class="col-md-6 col-xl-6 pt-3 border-top">
                    <h5><i class="fa-solid fa-sterling-sign"></i> Total Fees To Pay:</h5>
                </div>
                <div class="col-md-6 col-xl-6 pt-3 text-end border-top">
                    <h5>
                    <i class="fa-solid fa-sterling-sign"></i>
                        <%If Not IsNothing(CourseFee) Then %>
                            <%If CourseFee.Amount > 0 Then %>
                                <span class="NonFreeCourse"><%=FormatCurrency(CourseFee.Amount, 0)%></span>
                            <%Else %>
                                <span class="FreeCourse">Free</span>
                            <%End If %>
                        <%Else %>
                            <span class="FeeUnknownCourse">TBC</span>
                        <%End If %>
                    </h5>
                </div>
            </div>
        </div>
    </div>
    <p class="text-body-secondary">
        <i>Please note that additional fees for examinations and for any learning support may be applicable and will be discussed with you on enrolment.</i>
    </p>

    <div class="mt-3 mb-3 form-group">
        <span class="textfieldlabelrequired">Who Is Paying Your Course Fees?</span>
        <asp:DropDownList ID="SourceOfTuitionFees" runat="server" CssClass="textfield form-control" ClientIDMode="Static">
            <asp:ListItem Selected="True" Value="">-- Please Select --</asp:ListItem>
            <asp:ListItem Value="STUDENT">Yourself</asp:ListItem>
            <asp:ListItem Value="EMPLOYER">Your Employer</asp:ListItem>
            <asp:ListItem Value="OTHER">Other</asp:ListItem>
        </asp:DropDownList>
        <asp:CustomValidator ID="SourceOfTuitionFeesValidator" runat="server"></asp:CustomValidator>
    </div>
    <div class="mb-3 form-group alert alert-warning d-none" id="ApplicationUserDefined17Row">
        <cc1:StudentEnrolmentField StudentEnrolmentFieldType="ApplicationUserDefined17" ID="fldApplicationUserDefined17" ClientIDMode="Static" runat="server" LabelWidth="400"
            CustomCaption="Please State Who Is Paying Your Fees" />
        <asp:CustomValidator ID="fldApplicationUserDefined17Validator" runat="server"></asp:CustomValidator>
    </div>
    <div class="mb-3 form-group" id="ApplicationUserDefined18Row">
        <cc1:StudentEnrolmentField StudentEnrolmentFieldType="ApplicationUserDefined18" ID="fldApplicationUserDefined18" ClientIDMode="Static" runat="server" LabelWidth="400"
            CustomCaption="Please Confirm The Payment Method That Will Be Used" />
        <select id="PaymentMethodDropdown" class="textfield form-control" runat="server" ClientIDMode="Static">
        </select>
        <asp:CustomValidator ID="fldApplicationUserDefined18Validator" runat="server"></asp:CustomValidator>
    </div>
    <div class="alert alert-primary hstack gap-3" role="alert">
            <div>
                <h1><i class="fa-solid fa-circle-info"></i></h1>
            </div>
            <div>
                <h4 class="alert-heading">Please Note We Do Not Require Payment At This Stage</h4>
                <p>
                    We will confirm your place once minimum numbers have been reached and we will then request payment. 
                </p>
                <p>
                    Fees are non-refundable unless the course is cancelled by The Guernsey Institute. 
                </p>
                <p>
                    Please refer to our Course Refund Policy at <a href="https://www.guernseycollege.ac.gg/wp/policies" target="_blank" rel="noopener">https://www.guernseycollege.ac.gg/wp/policies <i class="fa-solid fa-arrow-up-right-from-square"></i></a>. 
                </p>
            </div>
    </div>
</div>

<div class="alert alert-secondary" role="alert">
    <div class="row gap-3">
        <div class="col-md">
            <span class="d-grid gap-2 d-md-block">
                <button type="button" class="btn btn-secondary btn-lg BackButton"> Back</button>
                <cc1:CCCButton ID="btnBack" CssClass="d-none" class="d-none" ClientIDMode="Static" runat="server" Text="Back" ImageResource="btnBack" LinkResource="checkout_emergency_contacts_GCFE_aspx" CausesValidation="false" />
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
        //Source Of Tuition Fees - Custom
        let fldSourceOfTuitionFeesCustom = document.getElementById(`SourceOfTuitionFees`);
        let fldSourceOfTuitionFeesCustomInputBox = addSearchableDropDown(fldSourceOfTuitionFeesCustom);

        //Source Of Tuition Fees - Hide Original Text Box
        let fldSourceOfTuitionFees = document.getElementById(`txtApplicationUserDefined17`);
        //fldSourceOfTuitionFees.classList.add("d-none");

        //Source Of Tuition Fees - Update Primary Text Field on Custom One Change
        fldSourceOfTuitionFeesCustom.addEventListener(`change`, function (event) {
            fldSourceOfTuitionFees.value = fldSourceOfTuitionFeesCustom.options[fldSourceOfTuitionFeesCustom.selectedIndex].text;
        });

        //Source of Tuition Fees - Other
        let applicationUserDefined17Row = document.getElementById("ApplicationUserDefined17Row");
        let applicationUserDefined18Row = document.getElementById("ApplicationUserDefined18Row");
        let fldPaymentMethod = document.getElementById("txtApplicationUserDefined18");

        checkTuitionFeesOptions();
        fldSourceOfTuitionFeesCustom.addEventListener(`change`, function (event) {
            checkTuitionFeesOptions();
        });

        //Payment Method - Custom
        let fldPaymentMethodCustom = document.getElementById(`PaymentMethodDropdown`);
        let fldPaymentMethodCustomInputBox = addSearchableDropDown(fldPaymentMethodCustom);

        //Payment Method - Hide Original Text Box
        fldPaymentMethod.classList.add("d-none");

        //Payment Method - Update Primary Text Field on Custom One Change
        fldPaymentMethodCustom.addEventListener(`change`, function (event) {
            fldPaymentMethod.value = fldPaymentMethodCustom.options[fldPaymentMethodCustom.selectedIndex].text;
        });

        function checkTuitionFeesOptions() {
            if (fldSourceOfTuitionFeesCustom.value == `OTHER`) {
                applicationUserDefined17Row.classList.remove("d-none");
                fldSourceOfTuitionFees.value = null;
            }
            else {
                applicationUserDefined17Row.classList.add("d-none");
                //fldSourceOfTuitionFees.value = null;
            }

            if (fldSourceOfTuitionFeesCustom.value == `EMPLOYER`) {
                applicationUserDefined18Row.classList.add("d-none");
                fldPaymentMethod.value = null;
            }
            else {
                applicationUserDefined18Row.classList.remove("d-none");
                //fldSourceOfTuitionFees.value = null;
            }
        }
    });
</script>