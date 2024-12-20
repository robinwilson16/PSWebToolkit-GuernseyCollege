<%@ Control Language="VB" AutoEventWireup="false" CodeFile="checkout_attachments.ascx.vb" Inherits="webcontrols_checkout_attachments" %>
<%@ Register Assembly="PSWebEnrolmentKit" Namespace="CompassCC.ProSolution.PSWebEnrolmentKit"
    TagPrefix="cc1" %>

<br />
<br />

<ol class="breadcrumb" id="breadcrumbenrols" runat="server">
    <li><a href="default.aspx">Home</a></li>
    <li><a href="webenrolment.aspx?page=~/webcontrols/checkout.ascx">Checkout</a></li>
    <li><a href="webenrolment.aspx?page=~/webcontrols/checkout_enrolments.ascx">Personal Details</a></li>
    <li><a href="webenrolment.aspx?page=~/webcontrols/checkout_other_contacts.ascx">Other Contacts</a></li>
    <li><a href="webenrolment.aspx?page=~/webcontrols/checkout_enrolments2.ascx">Further Details</a></li>
    <li><a href="webenrolment.aspx?page=~/webcontrols/checkout_parent_guardian.ascx">Parent / Guardian</a></li>
    <li><a href="webenrolment.aspx?page=~/webcontrols/checkout_other_contacts.ascx">Other Contacts</a></li>
    <li><a href="webenrolment.aspx?page=~/webcontrols/checkout_school_employer.ascx">School / Employer</a></li>
    <li><a href="webenrolment.aspx?page=~/webcontrols/checkout_prior_attainment.ascx">Prior Attainment</a></li>
    <li><a href="webenrolment.aspx?page=~/webcontrols/checkout_quals_on_entry.ascx">Quals on Entry</a></li>
    <li><a href="webenrolment.aspx?page=~/webcontrols/checkout_disability.ascx">Disability</a></li>
    <li><a href="webenrolment.aspx?page=~/webcontrols/checkout_supportneed.ascx">Support Needs</a></li>
    <li><a href="webenrolment.aspx?page=~/webcontrols/checkout_references.ascx">References</a></li>
    <li><a href="webenrolment.aspx?page=~/webcontrols/checkout_employment.ascx">Employment</a></li>
    <li class="active">Attachments</li>

</ol>

<div class="panel panel-info" id="divMain" runat="server">
    <div class="panel-heading">Attachments</div>

    <p>
        Please use this page to upload evidence to support your request.        
    </p>


    <div>        
        <table class="table table-striped text-center">
            <tbody>
                <tr>
                    <td class="text=center" style="width: 20%">Type of Evidence</td>
                    <td style="width: 50%">Notes</td>
                    <td style="width: 20%">Upload File</td>
                    <td style="width: 10%"></td>
                </tr>
                <tr>
                    <td>
                        <div>
                            <asp:DropDownList ID="ddlTypeOfEvidence" runat="server">
                                <asp:ListItem Text="Select" Value=""></asp:ListItem>
                                <asp:ListItem Text="Passport" Value="Passport"></asp:ListItem>
                                <asp:ListItem Text="Utility Bill/Proof of Address" Value="Utility Bill/Proof of Address"></asp:ListItem>
                                <asp:ListItem Text="Proof of Benefits" Value="Proof of Benefits"></asp:ListItem>
                                <asp:ListItem Text="Residence status" Value="Residence status"></asp:ListItem>
                                <asp:ListItem Text="Other…" Value="Other…"></asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </td>
                    <td>
                        <div>
                            <asp:TextBox ID="txtNotes" runat="server" TextMode="MultiLine" Rows="8" Width="100%"></asp:TextBox>
                        </div>
                    </td>
                    <td>
                        <div>
                            <%--
                                Following properties are there to use in the File Upload control
                                - IsRequired (To set control mandatory)
                                - RequiredErrorMessage (A message to display when user is not uploading file)
                                - MaxAllowedFileSize (Maximum size allowed for a file upload in KB). Use this to override value set in System Setting of same name.
                                - MaxAllowedFileSizeErrorMessage  (A message to display when user has uploaded larger file than allowed)
                                - SupportedFileTypes (File Types supported by the control (.jpg, .png, .xlsx)). Use this to override value set in System Setting of same name.
                                - SupportedFileTypesErrorMessage (A message to display when user has uploaded file of type which is not valid)
                                - HideUserInfoOnTooltip (To prevent showing of information related to file upload requirements)
                                - UserInfo (User Information related to file upload requirements - if need to display at any specific location manually)
                                --%>
                            <cc1:CCCFileUpload ID="fuAttachment" runat="server" data-html="true"  ClientIDMode="static"  IsRequired="true" />
                        </div>
                    </td>
                    <td>
                        <div>
                            <asp:Button ID="btnUpload" runat="server" Text="Upload" CssClass="btn btn-success" CausesValidation="true" />
                        </div>
                    </td>
                </tr>
            </tbody>
        </table>
    </div>
    <br />
    <asp:ValidationSummary ID="vsAttachments" runat="server" CssClass="alert alert-danger" />
    <hr />
    <div>
        <table id="tblUploadedInfo" class="table table-striped table-bordered text-center" style="border-style: None;">
            <tbody>
                <asp:Repeater ID="rptAttachments" runat="server" OnItemCommand="rptAttachments_ItemCommand">
                    <HeaderTemplate>
                        <tr class="searchgridheader">
                            <th scope="col" style="width: 20%">Type of Evidence</th>
                            <th scope="col">Notes</th>
                            <th scope="col" style="width: 15%">Attachment</th>
                            <th scope="col" style="width: 10%">Remove</th>
                        </tr>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <tr class="searchgridcell text-left">
                            <td><%#: Eval("TypeOfEvidence") %></td>
                            <td><%#: Eval("Notes") %></td>
                            <td><%#: Eval("FileName") %></td>
                            <td class="text-center">
                                <asp:Button ID="btnRemove" runat="server" Text="X" CssClass="btn btn-danger" CommandName="RemoveAttachment" CommandArgument='<%#: Eval("AttachmentID") %>' OnClientClick="return confirmDeletion()" /></td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
                <tr id="trNoItems" runat="server" class="searchgridcell" visible="false">
                    <td colspan="4">No attachments uploaded yet
                    </td>
                </tr>
            </tbody>
        </table>
    </div>
</div>

<cc1:CCCButton ID="btnBack" runat="server" Text="Back" ImageResource="btnBack" LinkResource="checkout_employment" />
<cc1:CCCButton ID="btnContinue" runat="server" Text="Continue" ImageResource="btnContinue" SaveForLater="true" SaveForLaterIn="Attachment" />


<script type="text/javascript" src="<%= ResolveUrl("~/webcontrols/checkout_attachments.js") %>"></script>
