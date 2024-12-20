<%@ Control Language="VB" AutoEventWireup="false" CodeFile="UserAccount.ascx.vb" Inherits="account_ccc_UserAccount" %>
<%@ Register Assembly="PSWebEnrolmentKit" Namespace="CompassCC.ProSolution.PSWebEnrolmentKit" TagPrefix="cc1" %>


<br /><br />
                <ol class="breadcrumb">
                  <li><a href="default.aspx">Home</a></li>
                  <li class="active">User Account</li>
                </ol>
<link href="<%= ResolveUrl("~/account/ccc/UserAccount.css") %>" rel="stylesheet" />
          <h3>User Account</h3>
            <p>Here you can see the status of any previous applications you have made, along with the details on interviews that have been arranged.</p>
           
            <p>Your college reference number: <span runat="server" id="strRefNo" style="font-weight:700"></span></p>
            <br />

            <a href="#Details">Update your details</a><br /><br />
     
      <asp:ListView runat="server" ID="lstDetails">
          <ItemTemplate>
              <div class="">
                     <asp:Label runat="server" ID="lblCourse" CssClass="courseHeader" text='<%#: Eval("OfferingName")%>'></asp:Label><br />
               
             <hr />
              <ol class="progtrckr" data-progtrckr-steps="5" runat="server" id="progtrckr">
                <li class="progtrckr-done" runat="server" id="stage0">Submitted</li>    
                <li class="progtrckr-todo" runat="server" id="stage1">Application Created</li>    
                <li class="progtrckr-todo" runat="server" id="stage2">Interview Created</li>
                <li class="progtrckr-todo" runat="server" id="stage3">College Offer Made</li>
                <li class="progtrckr-todo" runat="server" id="stage4">Student Enrolled</li>
                </ol>
                  <ol class="progtrckr" data-progtrckr-steps="5" runat="server" id="Ol1">
                <li  runat="server" id="stage0Details" class="prog-details done">
                    <div >
                        <b>Application Submitted</b><br />
                             <b>Request Date:</b><asp:Label runat="server" ID="lblRequestDate" text='<%#: Eval("RequestDate", "{0:d}")%>'></asp:Label>
                    </div>

                </li>    
                <li  runat="server" id="stage1Details" >
                    <div >
                        <b>Application Processed:</b><br />
                         <b>Date:</b> <asp:Label runat="server" ID="iApplicationCreatedDate" text='<%#: Eval("ApplicationCreatedDate", "{0:d}")%>'></asp:Label><br />
                    </div>

                </li>    
                <li  runat="server" id="stage2Details" >
                   <div>
                <b>Interview Details:</b><br />
                        <div runat="server" id="interviewDetails">
                       
                       
                           <b>Date/Time:</b> <asp:Label runat="server" ID="iDateTime"  text='<%#: Eval("DateTime", "{0:d}")%>'></asp:Label><br />
                           <b>Site:</b> <asp:Label runat="server" ID="iSite"  text='<%#: Eval("Site")%>'></asp:Label><br />
                           <b>Type:</b> <asp:Label runat="server" ID="iType" text='<%#: Eval("InterviewType")%>'></asp:Label><br />
                           <b>Duration:</b> <asp:Label runat="server" ID="iDuration" text='<%#: Eval("Duration")%>'></asp:Label><br />
                            </div>
                      </div>

                </li>
                <li  runat="server" id="stage3Details" >
                    <div >
                        <b>Offer Details:</b><br />
                    <asp:Label runat="server" ID="iOffer" text='<%#: Eval("ApplicationOffer")%>'></asp:Label><br />
                    </div>

                </li>
                <li  runat="server" id="stage4Details" >
                    <div >
                        <b>Enrolment Details:</b><br />
                        <div runat="server" id="enrolmentDetails">
                        <b>Start Date:</b><asp:Label runat="server" id="iEnrolmentStartDate"  text='<%#: Eval("EnrolmentStartDate", "{0:d}") %>'></asp:Label><br />
                         <b>Expected End Date:</b><asp:Label runat="server" id="iEnrolmentExpectedEndDate"  text='<%#: Eval("EnrolmentExpectedEndDate", "{0:d}")%>'></asp:Label><br />
                         <b>Course Code:</b><asp:Label runat="server" id="iEnrolmentCourseCode"  text='<%#: Eval("EnrolmentCourseCode")%>'></asp:Label><br />
                         <b>Course Name:</b><asp:Label runat="server" id="iEnrolmentCourseName" text='<%#: Eval("EnrolmentCourseName")%>'></asp:Label><br />
                         <b>Group Code:</b><asp:Label runat="server" id="iEnrolmentGroupCode" text='<%#: Eval("EnrolmentGroupCode")%>'></asp:Label><br />
                            </div>
                    </div>

                </li>
                </ol>
               
            
              </div>
<br />           
           
          </ItemTemplate>
      </asp:ListView>
<a name="Details">
       <br /><br />
</a><div class="panel panel-info" >
    <div class="panel-heading">Update Details</div>
        <div>         
            
                <%--<form  id="registerform">--%>
                
                    <div class="form-group">
                        <label for="txtSurname" class="label2 textfieldlabelrequired">Surname</label>
                        <asp:TextBox ID="txtSurname" runat="server" CssClass="form-control" placeholder="Your surname here..." autocomplete="off" required autofocus />

                    </div>
                    <div class="form-group">
                        <label for="txtFirstName" class="label2 textfieldlabelrequired">First Name</label>
                        <asp:TextBox ID="txtFirstName" runat="server" CssClass="form-control" placeholder="Your first name here..." autocomplete="off" required  />

                    </div>
                    <div class="form-group">
                        <label for="ddlTitle" class="label2 textfieldlabelrequired">Title</label>
                        <asp:DropDownList ID="ddlTitle" runat="server" CssClass="form-control" title="Title" >
                            <asp:ListItem Text="" Value=""></asp:ListItem>
                            <asp:ListItem Text="Mr" Value="Mr"></asp:ListItem>
                            <asp:ListItem Text="Mrs" Value="Mrs"></asp:ListItem>
                            <asp:ListItem Text="Miss" Value="Miss"></asp:ListItem>
                            <asp:ListItem Text="Ms" Value="Ms"></asp:ListItem>
                            <asp:ListItem Text="Dr" Value="Dr"></asp:ListItem>
                            <asp:ListItem Text="Br" Value="Br"></asp:ListItem>
                        </asp:DropDownList>     
                    </div>
                    <div class="form-group">
                        <label for="ddlSex" class="label2 textfieldlabelrequired">Sex</label>
                        <asp:DropDownList ID="ddlSex" runat="server" CssClass="form-control" title="Sex">
                            <asp:ListItem Text="" Value=""></asp:ListItem>
                            <asp:ListItem Text="Male" Value="M"></asp:ListItem>
                            <asp:ListItem Text="Female" Value="F"></asp:ListItem>
                        </asp:DropDownList>               
                    </div>
                    <div class="form-group">
                       <label for="txtDOB" class="label2 textfieldlabelrequired">Date of Birth</label>
                        <asp:TextBox ID="txtDOB" runat="server" CssClass="form-control" TextMode="Date" autocomplete="off"  />
                    </div>
                    <div class="form-group">
                        <label for="txtAddress1" class="label2">Address Line 1</label>
                        <asp:TextBox ID="txtAddress1" runat="server" CssClass="form-control" autocomplete="off" />
                    </div>
                    <div class="form-group">
                        <label for="txtAddress2" class="label2">Address Line 2</label>
                        <asp:TextBox ID="txtAddress2" runat="server" CssClass="form-control" autocomplete="off" />
                    </div>
                    <div class="form-group">
                        <label for="txtAddress3" class="label2">Address Line 3</label>
                        <asp:TextBox ID="txtAddress3" runat="server" CssClass="form-control" autocomplete="off" />
                    </div>
                     <div class="form-group">
                         <label for="txtAddress4" class="label2">Address Line 4</label>
                        <asp:TextBox ID="txtAddress4" runat="server" CssClass="form-control" autocomplete="off" />
                    </div>
                    <div class="form-group">
                         <label for="txtPostcode" class="label2">Postcode</label>
                        <asp:TextBox ID="txtPostcode" runat="server" CssClass="form-control" placeholder="Your postcode here..." autocomplete="off" />
                    </div>
                    <div class="form-group">
                         <label for="txtEmail" class="label2 textfieldlabelrequired">Email</label>
                        <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" TextMode="Email" placeholder="Your E-mail here..." autocomplete="off" required />
                    </div>
                    <div class="panel panel-info">
                        <div class="panel-heading">To change your password, enter your existing password and then your new password below</div>
                     <div class="form-group">
                        <label for="txtPassword" class="label2 ">Current Password</label>
                        <asp:TextBox ID="txtPassword" ClientIDMode="Static" runat="server" Cssclass="form-control" TextMode="Password" placeholder="Your current password here..." autocomplete="off" />
                    </div>                   
                    <div class="form-group">
                        <label for="txtNewPassword" class="label2 textfieldlabelrequired">New Password</label>
                        <asp:TextBox ID="txtNewPassword" ClientIDMode="Static" runat="server" Cssclass="form-control" TextMode="Password" placeholder="Your new password here..." autocomplete="off" />
                        <asp:Label ID="lblPasswordInfo" runat="server" Font-Size="12px"></asp:Label>
                    </div>
                    <div class="form-group">
                        <label for="txtPassword_Mirror" class="label2 textfieldlabelrequired">Retype New Password</label>
                        <asp:TextBox ID="txtPassword_Mirror" runat="server" Cssclass="form-control" TextMode="Password" placeholder="Retype your new password..." autocomplete="off" oninput="check(this)"/>
                    </div>
                   </div>
                    <div class="form-group" runat="server" id="errtext" style="color:red">
                        
                    </div>
                    <div class="form-group">
                        
                       <asp:Button runat="server" ID="btnUpdate" Text="Update" CssClass="btn btn-primary" OnClick="btnSubmit_OnClick"/>
                        
                    </div>
            <div class="form-group" style="text-align:center;">
            <label id="lblError" runat="server" visible="false" class="text-center text-danger"></label>
               </div>
                <%--</form>--%>
            </div>

      
    </div>
<br /><br />


<script type="text/javascript" src="<%= ResolveUrl("~/account/ccc/UserAccount.js") %>"></script>
