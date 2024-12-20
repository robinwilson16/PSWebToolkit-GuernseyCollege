<%@ Control Language="VB" AutoEventWireup="false" CodeFile="checkout_school_employer.ascx.vb" Inherits="webcontrols_checkout_school_employer" %>
<%@ Register Assembly="PSWebEnrolmentKit" Namespace="CompassCC.ProSolution.PSWebEnrolmentKit"
    TagPrefix="cc1" %>

<br /><br />
                <ol class="breadcrumb"  id="breadcrumbapps" runat="server">
                  <li><a href="default.aspx">Home</a></li>
                    <li><a href="webenrolment.aspx?page=~/webcontrols/checkout.ascx">Checkout</a></li>
                    <li><a href="webenrolment.aspx?page=~/webcontrols/checkout_applications.ascx">Personal Details</a></li>
                   
                    <li><a href="webenrolment.aspx?page=~/webcontrols/checkout_applications2.ascx">Further Details</a></li>
                    <li><a href="webenrolment.aspx?page=~/webcontrols/checkout_parent_guardian.ascx">Parent / Guardian</a></li>
                    <li><a href="webenrolment.aspx?page=~/webcontrols/checkout_other_contacts.ascx">Other Contacts</a></li>
                  <li class="active">School / Employer</li>
                </ol>

                  <ol class="breadcrumb"  id="breadcrumbenrols" runat="server">
                  <li><a href="default.aspx">Home</a></li>
                    <li><a href="webenrolment.aspx?page=~/webcontrols/checkout.ascx">Checkout</a></li>
                    <li><a href="webenrolment.aspx?page=~/webcontrols/checkout_enrolments.ascx">Personal Details</a></li>
                   
                    <li><a href="webenrolment.aspx?page=~/webcontrols/checkout_enrolments2.ascx">Further Details</a></li>
                    <li><a href="webenrolment.aspx?page=~/webcontrols/checkout_parent_guardian.ascx">Parent / Guardian</a></li>
                      <li><a href="webenrolment.aspx?page=~/webcontrols/checkout_other_contacts.ascx">Other Contacts</a></li>
                  <li class="active">School / Employer</li>
                </ol>

 

    <div id="schoolpanel" runat="server" class="panel panel-info">
     <a href="#" class="show_hide1"><div class="panel-heading">School Details</div></a>
<div class="slidingDiv1">
    <div class=" form-group">
         <cc1:StudentEnrolmentField StudentEnrolmentFieldType="SchoolID" ID="fldSchoolID" runat="server" IsRequired="false" LabelWidth="300"
           CustomCaption="Previous/Current School" />
    </div>
    <div class=" form-group">
         <cc1:StudentEnrolmentField StudentEnrolmentFieldType="SchoolAttendedFrom" ID="StudentEnrolmentField1" runat="server" IsRequired="false" LabelWidth="300"
           CustomCaption="Date From" ClientIDMode="Static" />
    </div>
    <div class=" form-group">
         <cc1:StudentEnrolmentField StudentEnrolmentFieldType="SchoolAttendedTo" ID="StudentEnrolmentField2" runat="server" IsRequired="false" LabelWidth="300"
           CustomCaption="Date To" ClientIDMode="Static" />
    </div>
</div>
</div>

<br /><br />

<div id="employerpanel" runat="server" class="panel panel-info" ClientIDMode="static">
     <a href="#" class="show_hide2"><div class="panel-heading">Employment Details</div></a>
<div class="slidingDiv2">
    <%--<div class=" form-group">
         <cc1:StudentEnrolmentField StudentEnrolmentFieldType="EmployerID" ID="fldEmployerID" runat="server" IsRequired="false" LabelWidth="300"
           CustomCaption="Current Employer" ClientIDMode="Static" />
    </div>
    <div id="empnote" runat="server" class="alert alert-info">If you can't find your employer in the list, you can enter the name here instead</div>--%>
    <div class=" form-group">
         <cc1:StudentEnrolmentField StudentEnrolmentFieldType="EmployerName" ID="StudentEnrolmentField3" runat="server" IsRequired="false" LabelWidth="300"
           CustomCaption="Employer Name" ClientIDMode="Static"/>
    </div>
    <hr />
    <div class=" form-group">
         <cc1:StudentEnrolmentField StudentEnrolmentFieldType="EmployerAddress1" ID="StudentEnrolmentField4" runat="server" IsRequired="false" LabelWidth="300"
           CustomCaption="Employer Address 1" />
    </div>
    <div class=" form-group">
         <cc1:StudentEnrolmentField StudentEnrolmentFieldType="EmployerAddress2" ID="StudentEnrolmentField5" runat="server" IsRequired="false" LabelWidth="300"
           CustomCaption="Employer Address 2" />
    </div>
    <div class=" form-group">
         <cc1:StudentEnrolmentField StudentEnrolmentFieldType="EmployerAddress3" ID="StudentEnrolmentField6" runat="server" IsRequired="false" LabelWidth="300"
           CustomCaption="Employer Address 3" />
    </div>
    <div class=" form-group">
         <cc1:StudentEnrolmentField StudentEnrolmentFieldType="EmployerAddress4" ID="StudentEnrolmentField7" runat="server" IsRequired="false" LabelWidth="300"
           CustomCaption="Employer Address 4" />
    </div>
    <div class=" form-group">
         <label for="employerpostcode" style="width:300px;font-weight:normal; ">Employer Postcode</label><input type="text" class="form-control" runat="server"  id="employerpostcode" title="Postcode" placeholder="Enter Postcode" />
    </div>
    <div class=" form-group">
         <cc1:StudentEnrolmentField StudentEnrolmentFieldType="EmployerEmail" ID="StudentEnrolmentField8" runat="server" IsRequired="false" LabelWidth="300"
           CustomCaption="Employer Email" Pattern="^[a-zA-Z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-zA-Z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-zA-Z0-9](?:[a-zA-Z0-9-]*[a-zA-Z0-9])?\.)+[a-zA-Z0-9](?:[a-zA-Z0-9-]*[a-zA-Z0-9])?$" />
    </div>
    <div class=" form-group">
         <cc1:StudentEnrolmentField StudentEnrolmentFieldType="EmployerTel"  ID="StudentEnrolmentField9" runat="server" IsRequired="false" LabelWidth="300"
           CustomCaption="Employer Tel" />
    </div>
</div>
</div>



 <cc1:CCCButton id="btnBack" runat="server" Text="Back" ImageResource="btnBack" LinkResource="checkout_other_contacts_aspx"/>
        <cc1:CCCButton ID="btnContinue" runat="server" Text="Continue" ImageResource="btnContinue" LinkResource="checkout_priorattainment_aspx" CausesValidation="true" EnableEnterKey="true" SaveForLater="true" SaveForLaterIn="Request" />
    <br />
    <asp:ValidationSummary ID="ValidationSummary1" runat="server" CssClass="alert alert-danger" ForeColor="" />

<script type="text/javascript" src="<%= ResolveUrl("~/webcontrols/checkout_school_employer.js") %>"></script>

