<%@ Control Language="VB" AutoEventWireup="false" CodeFile="checkout_attachments_GCFE.ascx.vb" Inherits="webcontrols_checkout_attachments_GCFE" %>
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
        <li class="breadcrumb-item pl-0"><a href="webenrolment.aspx?page=~/webcontrols/checkout_quals_on_entry_GCFE.ascx"><i class="fa-solid fa-address-book"></i> <span class="d-none d-sm-inline">Qualifications on Entry</span></a></li>
        <li aria-current="page" class="breadcrumb-item pl-0 active pl-4"><i class="fa-solid fa-paperclip"></i> Attachments</li>
    </ol>
</nav>

<div class="progress mb-4" role="progressbar" aria-label="Animated striped example" aria-valuenow="70" aria-valuemin="0" aria-valuemax="100">
    <div class="progress-bar progress-bar-striped progress-bar-animated" style="width: 70%">70%</div>
</div>

<div class="bd-callout bd-callout-gcfe bd-callout-grey">
    <h4><i class="fa-solid fa-paperclip"></i> Attachments</h4>

         <p>
        <strong>Please use this page to upload evidence to support your enrolment.  You can take a picture using your phone as long as the image is clear.</strong>    
    </p>
    <p><strong>Failure to upload the required documents will delay or prevent your enrolment to the College.</strong></p>
    <ul runat="server" id="ulList">
        <li>You must upload 1 proof of identity such as a passport, birth certificate or driving licence</li>
        <li>You must upload copies of certificates for any qualifications you have already achieved. If you are awaiting your results you do not need to upload them here.</li>
  
    </ul>

    <p><strong>The maximum file size is up to 5 MB per file, the supported file formats allowed are jpg, png, jpeg, heic)</strong>
    </p>

    <div class="row">
        <div class="col-md-12 mb-4 d-flex align-items-stretch">
            <div class="card w-100">
                <div class="card-body d-flex flex-column">
                    <div class="row">
                        <div class="form-group col-md-6 col-xl-3">
                            <label for="ddlTypeOfEvidence">Type of Evidence</label>
                            <asp:DropDownList ID="ddlTypeOfEvidence" runat="server" CSSClass="form-control">
                                <asp:ListItem Text="Select" Value=""></asp:ListItem>
                            </asp:DropDownList>
                        </div>
                        <div class="form-group col-md-6 col-xl-4">
                            <label for="txtNotes">Notes</label>
                            <asp:TextBox ID="txtNotes" runat="server" TextMode="MultiLine" Rows="4" CssClass="form-control"></asp:TextBox>
                        </div>
                        <div class="form-group col-md-6 col-xl-3">
                            <label for="fuAttachment" class="mb-2">File</label>
                            <cc1:CCCFileUpload ID="fuAttachment" runat="server" data-html="true"  ClientIDMode="static"  IsRequired="true" SupportedFileTypes=".jpg, .jpeg, .png, .gif, .bmp, .heic, .pdf, .doc, .docx" accept=".jpg,.jpeg,.png,.gif,.bmp,.heic,.pdf,.doc,.docx" MaxAllowedFileSize="5000" MaxAllowedFileSizeErrorMessage="File size is larger than 5 MB" CSSClass="form-control pt-2 pb-3" />
                        </div>
                        <div class="form-group col-md-6 col-xl-2 text-end mt-auto">
                            <asp:LinkButton ID="btnUpload" runat="server" CssClass="btn btn-primary" CausesValidation="False"><i class="fa-solid fa-file-import"></i> Upload</asp:LinkButton>
                        </div>
                        <asp:CustomValidator ID="ddlTypeOfEvidenceValidator" runat="server"></asp:CustomValidator>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <asp:CustomValidator ID="btnUploadValidator" runat="server"></asp:CustomValidator>

    <hr />

     <div class="panel panel-success"  runat="server" id="divMain">

        <!--
            Following properties are there to use in the File Upload control
            - IsRequired (To set control mandatory)
            - RequiredErrorMessage (A message to display when user is not uploading file)
            - MaxAllowedFileSize (Maximum size allowed for a file upload in KB). Use this to override value set in System Setting of same name.
            - MaxAllowedFileSizeErrorMessage  (A message to display when user has uploaded larger file than allowed)
            - SupportedFileTypes (File Types supported by the control (.jpg, .png, .xlsx)). Use this to override value set in System Setting of same name.
            - SupportedFileTypesErrorMessage (A message to display when user has uploaded file of type which is not valid)
            - HideUserInfoOnTooltip (To prevent showing of information related to file upload requirements)
            - UserInfo (User Information related to file upload requirements - if need to display at any specific location manually)
            -->
    
         <%If rptAttachments.Items.Count = 0 %>
            <div runat="server" visible="<%#rptAttachments.Items.Count = 0 %>">
                <div class="alert alert-primary hstack gap-3" role="alert">
                    <div>
                        <i class="fa-solid fa-file-import"></i>
                    </div>
                    <div>
                        <p>
                            No files uploaded yet
                        </p>
                    </div>
                </div>
            </div>
        <%End If %>
        <asp:Repeater ID="rptAttachments" runat="server" OnItemCommand="rptAttachments_ItemCommand">
            <ItemTemplate>
                <div class="row">
                    <div class="col-md-12 mb-4 d-flex align-items-stretch">
                        <div class="card w-100">
                            <div class="card-body d-flex flex-column">
                                <div class="row">
                                    <div class="form-group col-md-6 col-xl">
                                        <p><strong>Type of Evidence</strong></p>
                                        <p><%# Eval("TypeOfEvidence") %></p>
                                    </div>
                                    <div class="form-group col-md-6 col-xl pt-2">
                                        <p><strong>Notes</strong></p>
                                        <asp:PlaceHolder runat="server" Visible='<%#displayNotesIcon(DataBinder.Eval(Container.DataItem, "Notes").ToString) %>'>
                                            <p><i class="fa-solid fa-note-sticky"></i> <%# Eval("Notes") %></p>
                                        </asp:PlaceHolder>
                                    </div>
                                    <div class="form-group col-md-6 col-xl pt-2">
                                        <p><strong>Attachment</strong></p>
                                        <p><i class="fa-solid fa-file-lines"></i> <%# Eval("FileName") %></p>
                                    </div>
                                    <div class="form-group col-md-12 col-xl text-end">
                                        <p><strong>Remove</strong></p>
                                        <asp:Button ID="btnRemove" runat="server" Text="X" CssClass="btn btn-danger" CommandName="RemoveAttachment" CommandArgument='<%# Eval("AttachmentID") %>' OnClientClick="return confirmDeletion()" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </ItemTemplate>
        </asp:Repeater>
         <asp:CustomValidator ID="AttachmentsValidatorType" runat="server"></asp:CustomValidator>
         <asp:CustomValidator ID="AttachmentsValidatorFiles" runat="server"></asp:CustomValidator>

         <asp:HiddenField runat="server" ID="hiddenvalidator" Visible="true"/>
             <p class="mt-5"><strong>Not Able To Upload Your File/s</strong></p>
        <div class=" form-group">
            <cc1:StudentEnrolmentField runat="server" ID="StudentDetailUserDefined25" CustomFieldType="Lookup" StudentEnrolmentFieldType="StudentDetailUserDefined25" ClientIDMode="Static" CustomCaption="Can't upload your file/s at the moment - please select a reason why" />
        </div>
    </div>
</div>

<asp:ValidationSummary ID="vsAttachments" runat="server" CssClass="alert alert-danger" />

<div class="alert alert-secondary" role="alert">
    <div class="row gap-3">
        <div class="col-md">
            <span class="d-grid gap-2 d-md-block">
                <button type="button" class="btn btn-secondary btn-lg BackButton"> Back</button>
                <cc1:CCCButton ID="btnBack" CssClass="d-none" class="d-none" ClientIDMode="Static" runat="server" Text="Back" ImageResource="btnBack" CausesValidation="false" LinkResource="checkout_quals_on_entry_GCFE" />
            </span>
        </div>
        <div class="col-md text-end">
            <span class="d-grid gap-2 d-md-block">
                <button type="button" class="btn btn-primary btn-lg NextButton">Next </button>
                <cc1:CCCButton ID="btnContinue" CssClass="d-none" class="d-none" ClientIDMode="Static" runat="server" Text="Continue" ImageResource="btnContinue" CausesValidation="true" EnableEnterKey="true" SaveForLater="true" SaveForLaterIn="Attachment" />
            </span>
        </div>
    </div>
</div>

<%If HasFormErrors = True Then%>
    <div class="alert alert-warning" role="alert">
        <%If Course.EnrolmentTypeID = 1 %>
            <i class="fa-solid fa-circle-question"></i> If you are having trouble with this form, please contact the Apprenticeship Team on <i class="fa-solid fa-phone"></i> <a href="tel:01481 227500">01481 227500</a> or by email at <i class="fa-solid fa-envelope"></i> <a href="mailto:apprenticeships@gcfe.net?subject=Online Application Form Query">apprenticeships@gcfe.net</a>
        <%Else %>
            <i class="fa-solid fa-circle-question"></i> If you are having trouble with this form, please contact Reception on <i class="fa-solid fa-phone"></i> <a href="tel:01481 227500">01481 227500</a> or by email at <i class="fa-solid fa-envelope"></i> <a href="mailto:enrolment@gcfe.net?subject=Online Application Form Query">enrolment@gcfe.net</a>
        <%End If %>
    </div>
<%End If %>

<script type="text/javascript">
    $("#btnChooseFile").click(function () {
        $('#fuAttachment').click();
    });
    function confirmDeletion() {
        var result = confirm("Are you sure you want to delete this attachment? Press OK to continue deletion.");
        if (result) {
            return true;
        }
        return false;
    }

    $(function () {
        $('.example-popover').popover({
            container: 'body'
        })
    })
</script>

<asp:TextBox runat="server" ID="FileChosen" ClientIDMode="Static" Value="N" class="d-none" />

<script>
    let fuAttachment = document.getElementById("fuAttachment");
    let fileChosen = document.getElementById("FileChosen");

    fuAttachment.addEventListener('change', function (event) {
        fileChosen.value = "Y";
    });
</script>
