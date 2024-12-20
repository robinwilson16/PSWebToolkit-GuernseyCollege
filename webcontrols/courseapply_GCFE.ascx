<%@ control Language="VB"  AutoEventWireup="false" CodeFile="courseapply_GCFE.ascx.vb" Inherits="courseapply_GCFE"  %>
<%@ Register Assembly="PSWebEnrolmentKit" Namespace="CompassCC.ProSolution.PSWebEnrolmentKit"
    TagPrefix="cc1" %>

<br /><br />

<nav aria-label="breadcrumb">
    <ol class="breadcrumb breadcrumb-arrow p-0">
        <li class="icon breadcrumb-item"><a href="http://guernseycollege.ac.gg/wp/the-guernsey-apprenticeship/" class="pl-3"><i class="fa-solid fa-house"></i> <span class="d-none d-sm-inline">GCFE Home</span></a></li>
        <li class="breadcrumb-item pl-0"><a href="webenrolment.aspx?page=~/courseapply_GCFE.ascx&OfferingID=0"><i class="fa-regular fa-folder-open"></i> <span class="d-none d-sm-inline">Dept</span></a></li>
        <li class="breadcrumb-item pl-0"><a href="webenrolment.aspx?page=~/courseapply_GCFE.ascx&Dept=DEPT"><i class="fa-solid fa-book"></i> <span class="d-none d-sm-inline">Courses</span></a></li>
        <li aria-current="page" class="breadcrumb-item pl-0 active pl-4"><i class="fa-solid fa-graduation-cap"></i> <%=Course.Code%> - <%=Course.Name %></li>
    </ol>
</nav>

<div class="progress mb-4" role="progressbar" aria-label="Animated striped example" aria-valuenow="10" aria-valuemin="0" aria-valuemax="100">
    <div class="progress-bar progress-bar-striped progress-bar-animated" style="width: 10%">10%</div>
</div>

    <!--The CourseApplyAction adds the offering to the basket as an application-->
    <cc1:CourseApplyAction ID="CourseApplyAction" runat="server" />
    <h3>
        <%If Course.EnrolmentTypeID = 1 %>
            You are applying for the following apprenticeship:
        <%Else %>
            You are applying for the following course:
        <%End If %>
    </h3>
   
<%If Not Course Is Nothing Then %>
    <div class="bd-callout bd-callout-gcfe">
        <h4><i class="fa-solid fa-award"></i> <%=Course.Code%> - <%=Course.Name %></h4>

        <%If Course.EnrolmentTypeID = 1 %>
            
        <%Else %>
            <div class="card w-100">
                <div class="card-body d-flex flex-column">
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
                                    <%=Convert.ToDateTime(Course.StartTime).ToString("HH:mm")%>
                                <%End If %>
                        </div>
                        <div class="col-md-6 col-xl-3 mb-3">
                            <i class="fa-regular fa-clock"></i> End Time: 
                                <%If String.IsNullOrEmpty(Course.EndTime.ToString()) Then %>
                                    TBC
                                <%Else %>
                                    <%=Convert.ToDateTime(Course.EndTime).ToString("HH:mm")%>
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
                        <div class="col-md-6 col-xl-3 mb-3">
                            <i class="fa-solid fa-sterling-sign"></i> Tuition Fee:
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
                    </div>
                </div>
            </div>

            <%--<table class="table">
                <thead>
                <tr>
                    <th scope="col"><i class="fa-solid fa-calendar-days"></i> Qualification Code</th>
                    <th scope="col"><i class="fa-solid fa-flag-checkered"></i> Qualification Title</th>
                </tr>
                </thead>
                <tbody>
                    <tr>
                        <td><%=Course.QualID%></td>
                        <td><%=LearningAimTitle%></td>
                    </tr>
                </tbody>
            </table>--%>
        <%End If %>
        
    </div>

<div class="bd-callout bd-callout-warning">
    <h5>
        Before you begin please have with you:
    </h5>

    <ul class="list-group">
        <%If Course.EnrolmentTypeID = 1 Then %>
            <li class="list-group-item list-group-item-action"><i class="fa-solid fa-check"></i> Details of your previous qualifications</li>
            <li class="list-group-item list-group-item-action"><i class="fa-solid fa-check"></i> Information about your parents/emergency contacts</li>
            <li class="list-group-item list-group-item-action"><i class="fa-solid fa-check"></i> A digital passport-style photograph</li>    
            <%If Not Course.UserDefined23 = "True" Then %>
                <li class="list-group-item list-group-item-action"><i class="fa-solid fa-check"></i> Details of your employment including contact details for your employer</li>
            <%End If %>
        <%Else %>
            <%If Not (Course.EnrolmentTypeID = 6 Or Course.EnrolmentTypeID = 7) Then %>
                <li class="list-group-item list-group-item-action"><i class="fa-solid fa-check"></i> Details of your previous qualifications</li>
            <%End If %>
            <li class="list-group-item list-group-item-action"><i class="fa-solid fa-check"></i> Information about your emergency contacts</li>
            <li class="list-group-item list-group-item-action"><i class="fa-solid fa-check"></i> A digital passport-style photograph (optional)</li>
            <li class="list-group-item list-group-item-action"><i class="fa-solid fa-check"></i> Details of your employer if they will be funding your studies</li>
        <%End If %>
    </ul>
</div>


<div class="bd-callout bd-callout-gcfe bd-callout-grey">
  <h4 class="alert-heading"><i class="fa-solid fa-door-open"></i> Welcome and thank you for your interest in The Guernsey Institute</h4>
    <p>
        Please read the following information in full before you proceed
    </p>
  <hr>

    <div class="row">
        <div class="col-md-3">
            <div id="DeclarationList" class="list-group">
                <%If Course.EnrolmentTypeID = 1 Then %>
                    <a class="list-group-item list-group-item-action" href="#IntroductionApp"><i class="fa-solid fa-angles-right"></i> Introduction</a>
                    <%If Not Course.UserDefined23 = "True" Then %>
                        <a class="list-group-item list-group-item-action" href="#Employment"><i class="fa-solid fa-angles-right"></i> Employment</a>
                    <%End If %>
                <%Else %>
                    <a class="list-group-item list-group-item-action" href="#IntroductionNonApp"><i class="fa-solid fa-angles-right"></i> Introduction</a>
                <%End If %>
                <a class="list-group-item list-group-item-action" href="#PrivacyPolicy"><i class="fa-solid fa-angles-right"></i> Privacy Policy</a>
            </div>
        </div>
        <div class="col-md-9 mt-3 mt-md-0">
            <div data-bs-spy="scroll" data-bs-target="#DeclarationList" data-bs-smooth-scroll="true" id="DeclarationText" class="DeclarationText" tabindex="0">
                
                <%If Course.EnrolmentTypeID = 1 %>
                    <h4 id="IntroductionApp"><i class="fa-solid fa-angles-right"></i> Introduction</h4>
                    <p>
                        The Guernsey Apprenticeship offers you the opportunity to earn a salary and gain a nationally recognised technical, professional or vocational qualification in a job you love.
                    </p>
                    <p>
                        Guernsey has been offering a range of apprentices for over 70 years across a wide range of sectors and many former apprentices have gone on to run their own business and/or enjoy highly successful careers in their chosen profession.
                    </p>
                    <%If Not Course.UserDefined23 = "True" Then %>
                        <h4 id="Employment"><i class="fa-solid fa-angles-right"></i> Employment</h4>
                        <p>
                            You will need to have secured employment before completing this form. 
                        </p>
                        <p>
                            If you are not currently employed, you can email apprenticeships@gcfe.net for a current list of employers in your chosen sector, or <strong><a href="http://www.guernseycollege.ac.gg/wp/apprenticeship-vacancies/" target="_blank" rel="noopener"><i class="fa-solid fa-arrow-up-right-from-square"></i> click here</a></strong> for current vacancies.
                        </p>
                    <%End If %>
                    
                <%Else %>
                    <h4 id="IntroductionNonApp"><i class="fa-solid fa-angles-right"></i> Introduction</h4>
                    <p>
                        Welcome to Guernsey College, which offers a range of courses for all ages.
                    </p>
                    <p>
                        Guernsey has been offering a range of courses for over 70 years across a wide range of sectors and many former students have gone on to run their own business and/or enjoy highly successful careers in their chosen profession whilst others have learned additional invaluable skills or been able to persue an interest for leisure.
                    </p>
                <%End If %>
                
                <h4 id="PrivacyPolicy"><i class="fa-solid fa-angles-right"></i> Privacy Policy</h4>
                <p>
                    Guernsey College is committed to protecting your privacy and personal data. 
                </p>
                <p>
                    The data captured on this form will be used for processing your application and we may contact you using the information provided. 
                </p>
                <p>
                    The College will not share this information with any other organisation outside of the States of Guernsey. 
                </p>
                <p>
                    For more information on what data we collect, how we use it, and your rights under data protection law please <strong><a href="http://www.guernseycollege.ac.gg/wp/wp-content/uploads/2022/04/FPN-2022-GCFE.pdf" target="_blank" rel="noopener"><i class="fa-solid fa-file-pdf"></i> click here</a></strong>
                </p>
                <h5>We hope to welcome you to the College very soon and look forward to supporting you on your future learning journey.</h5>
            </div>
        </div>
        </div>
        <div class="row">
        <div class="col-md text-end">
            <strong>Please scroll to the end before proceeding</strong>
        </div>
    </div>
</div>

<input type="text" id="HasReadDeclaration" runat="server" class="form-control d-none" ClientIDMode="Static" value="N" />

     <%--<cc1:OfferingFeesDisplayAll runat="server"  />--%>
    <div class="alert alert-primary" role="alert">
        <%If Course.EnrolmentTypeID = 1 %>
            <i class="fa-solid fa-person-drowning"></i> If you need any help and support with this form, please contact the Apprenticeship Team on <i class="fa-solid fa-phone"></i> <a href="tel:01481 227500">01481 227500</a> or by email at <i class="fa-solid fa-envelope"></i> <a href="mailto:apprenticeships@gcfe.net?subject=Online Application Form Query">apprenticeships@gcfe.net</a>
        <%Else %>
            <i class="fa-solid fa-circle-question"></i> If you need any help and support with this form, please contact Reception on <i class="fa-solid fa-phone"></i> <a href="tel:01481 227500">01481 227500</a> or by email at <i class="fa-solid fa-envelope"></i> <a href="mailto:enrolment@gcfe.net?subject=Online Application Form Query">enrolment@gcfe.net</a>
        <%End If %>
    </div>

    <div class="alert alert-warning" role="alert">
        <i class="fa-solid fa-circle-info"></i> Acceptance onto this course is subject to you meeting any entry requirements that may be in place and subject to availability on the programme
    </div>

      <%--  <br />
        <br />
        <p>
            Total courses selected: &nbsp;<span class="wglyphicon glyphicon-shopping-cart"></span><cc1:ShoppingBasketTotals ID="ShoppingBasket1"
                runat="server" HideIcon="true" />
        </p>--%>

    <div class="alert alert-secondary" role="alert">
        <div class="row gap-3">
            <div class="col-md">
        
            </div>
            <div class="col-md text-end">
                <span class="d-grid gap-2 d-md-block">
                    <button type="button" class="btn btn-primary btn-lg NextButton">Next </button>
                    <cc1:CCCButton ID="btnContinue" CssClass="d-none" class="d-none" ClientIDMode="Static" runat="server" Text="Continue to Checkout" ImageResource="btnContinue" LinkResource="checkout_personal_details_GCFE_aspx" CausesValidation="true" />
                </span>
            </div>
        </div>
    </div>

    <div class="alert alert-danger" role="alert">
        <i class="fa-solid fa-stopwatch"></i> Please ensure you complete the form within an hour otherwise you will receive an error and have to start again
    </div>

<% Else %>
<div class="alert alert-danger" role="alert">
    <h4 class="alert-heading"><i class="fa-solid fa-triangle-exclamation"></i> Course Not Found</h4>
    <p>Sorry this course was not found or is not set up for applications</p>
    <p class="text-center"><a class="btn btn-danger" href="http://www.guernseycollege.ac.gg/" role="button"><i class="fa-solid fa-rotate-left"></i> Return to the Guernsey Institute Website</a></p>
    <hr>
    <p class="mb-0">Contact the Apprenticeship Team on <i class="fa-solid fa-phone"></i> <a href="tel:01481 227500">01481 227500</a> or by email at <i class="fa-solid fa-envelope"></i> <a href="mailto:apprenticeships@gcfe.net?subject=Course Not Found on Website">apprenticeships@gcfe.net</a></p>
</div>
<% End If %>

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
    let declarationText = document.getElementById(`DeclarationText`);
    let hasReadDeclaration = document.getElementById(`HasReadDeclaration`);

    declarationText.addEventListener(`scroll`, function (event) {
        let scrolledAmount = 0;

        if (this.scrollTop < scrolledAmount) {
            return;
        }
        else {
            scrolledAmount = this.scrollTop <= 0 ? 0 : this.scrollTop;
        }

        //Had to add 1 to this
        if (scrolledAmount + this.offsetHeight + 1 >= this.scrollHeight) {
            hasReadDeclaration.value = `Y`;
        }
    });
</script>