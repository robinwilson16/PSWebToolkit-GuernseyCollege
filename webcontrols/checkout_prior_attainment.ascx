<%@ control Language="VB" AutoEventWireup="false" CodeFile="checkout_prior_attainment.vb" Inherits="checkout_prior_attainment"  %>
<%@ Register Assembly="PSWebEnrolmentKit" Namespace="CompassCC.ProSolution.PSWebEnrolmentKit"
    TagPrefix="cc1" %>

<br /><br />
                <ol class="breadcrumb" id="breadcrumbapps" runat="server">
                  <li><a href="default.aspx">Home</a></li>
                    <li><a href="webenrolment.aspx?page=~/webcontrols/checkout.ascx">Checkout</a></li>
                    <li><a href="webenrolment.aspx?page=~/webcontrols/checkout_applications.ascx">Personal Details</a></li>
                   
                    <li><a href="webenrolment.aspx?page=~/webcontrols/checkout_applications2.ascx">Further Details</a></li>
                    <li><a href="webenrolment.aspx?page=~/webcontrols/checkout_parent_guardian.ascx">Parent / Guardian</a></li>
                    <li><a href="webenrolment.aspx?page=~/webcontrols/checkout_other_contacts.ascx">Other Contacts</a></li>
                  <li class="active">Prior Attainment</li>
                </ol>
                <ol class="breadcrumb" id="breadcrumbenrols" runat="server">
                  <li><a href="default.aspx">Home</a></li>
                    <li><a href="webenrolment.aspx?page=~/webcontrols/checkout.ascx">Checkout</a></li>
                    <li><a href="webenrolment.aspx?page=~/webcontrols/checkout_enrolments.ascx">Personal Details</a></li>
                   
                    <li><a href="webenrolment.aspx?page=~/webcontrols/checkout_enrolments2.ascx">Further Details</a></li>
                    <li><a href="webenrolment.aspx?page=~/webcontrols/checkout_parent_guardian.ascx">Parent / Guardian</a></li>
                    <li><a href="webenrolment.aspx?page=~/webcontrols/checkout_other_contacts.ascx">Other Contacts</a></li>
                    <li><a href="webenrolment.aspx?page=~/webcontrols/checkout_school_employer.ascx">School / Employer</a></li>
                  <li class="active">Prior Attainment</li>
                </ol>

<div class="panel panel-info" id="panel1" runat="server">
    <div class="panel-heading">Prior Attainment Level</div>
    <div class=" form-group">
    <cc1:studentenrolmentfield id="StudentEnrolmentField17" runat="server" customcaption="What is your highest level of Qualification?"
         isrequired="False" labelwidth="300" StudentEnrolmentFieldType="PriorAttainmentLevelID"></cc1:studentenrolmentfield>
    </div>
    
    <p>
        Use the following table to help you pick the correct qualification level...</p>
<table class="table table-striped table-bordered">
    <tr class="active"><th>Level</th><th>Academic Qualification Equivalent</th><th>Vocational Qualification Equivalent</th></tr>
    <tr><td>
        Entry Level</td><td>
            Word Power / Number Power</td><td>&nbsp;</td></tr>
        <tr>
            <td>
                Level 1</td>
            <td>
                GCSE/O Level Grades D-G (or fewer than 5 at grades A-C) CSE below Grade 1 1 AS 
                Level
            </td>
            <td>
                BEC General Certificate BEC Diploma BTEC First Certificate City &amp; Guilds 
                Operative Awards CPVE Year 1 (Technician) GNVQ Foundation LCCI Elementary/First 
                Level NVQ Level 1 PEI Elementary/First Level RSA Elementary/First Level RSA 
                Vocational Certificate</td>
        </tr>
        <tr>
            <td>
                Level 2</td>
            <td>
                GCSE/O Level (5 or more at Grades A-C) CSE Grade 1 (5 or more) 1 A Level 2/3 AS 
                Levels
            </td>
            <td>
                BEC General Certificate with Credit BEC Diploma with Credit BTEC First Diploma 
                City &amp; Guilds Higher Operative/Craft GNVQ Intermediate LCCI Certificate (Second 
                Level) NVQ Level 2 PEI Stage 2 Pitmans Intermediate Level 2 Diploma Certificate 
                RSA Diploma
            </td>
        </tr>
        <tr>
            <td>
                Level 3</td>
            <td>
                2 or more A Levels 4 or more AS Levels</td>
            <td>
                BEC National ONC/OND BTEC National ONC/OND City &amp; Guilds Advanced Craft GNVQ 
                Advanced LCCI Diploma (Third Level) NVQ Level 3 Pitmans Level 3 Advanced Higher 
                Certificate RSA Stage 3 Advanced Diploma TEC Certificate/Diploma Access to 
                Higher Education Courses ESOL &amp; Foreign Language Advanced Awards
            </td>
        </tr>
        <tr>
            <td>
                Level 4</td>
            <td>
                Teaching Qualifications (Including PGCE) First Degree</td>
            <td>
                BEC National HNC/HND BTEC National HNC/HND Higher Education Certificate Higher 
                Education Diploma LCCI Advanced Level NVQ 4 Nursing (SRN) RSA Advanced 
                Certificate RSA Higher Diploma</td>
        </tr>
        <tr>
            <td>
                Level 5</td>
            <td>
                Master&#8217;s degree Ph.D. or other doctorates
                </td>
            <td>
                &nbsp;</td>
        </tr>

    </table>

    </div>
<cc1:CCCButton id="btnBack" runat="server" Text="Back" ImageResource="btnBack" LinkResource="checkout_school_employer_aspx" CausesValidation="false"/>
        <cc1:CCCButton ID="btnContinue" runat="server" Text="Continue" ImageResource="btnContinue" LinkResource="checkout_quals_on_entry_aspx" CausesValidation="true" EnableEnterKey="true" SaveForLater="true" SaveForLaterIn="Request" />
    <br />
    <asp:ValidationSummary ID="ValidationSummary1" runat="server" CssClass="alert alert-danger" />


