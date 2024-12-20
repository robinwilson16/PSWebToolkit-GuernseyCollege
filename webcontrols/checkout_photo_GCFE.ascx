<%@ Control Language="VB" AutoEventWireup="false" CodeFile="checkout_photo_GCFE.ascx.vb" Inherits="checkout_photo_GCFE" %>
<%@ Register Assembly="PSWebEnrolmentKit" Namespace="CompassCC.ProSolution.PSWebEnrolmentKit"
    TagPrefix="cc1" %>
<%@ Register Src="~/webcontrols/StudentSignature.ascx" TagPrefix="uc1" TagName="StudentSignature" %>



<br />
<br />

<nav aria-label="breadcrumb">
    <ol class="breadcrumb breadcrumb-arrow p-0">
        <li class="icon breadcrumb-item"><a href="http://guernseycollege.ac.gg/wp/the-guernsey-apprenticeship/" class="pl-3"><i class="fa-solid fa-house"></i> <span class="d-none d-sm-inline">GCFE Home</span></a></li>
        <li class="breadcrumb-item pl-0"><a href="webenrolment.aspx?page=~/apply/courseapply_GCFE.ascx&OfferingID=0"><i class="fa-regular fa-folder-open"></i> <span class="d-none d-sm-inline">Area</span></a></li>
        <li class="breadcrumb-item pl-0"><a href="webenrolment.aspx?page=~/apply/courseapply_GCFE.ascx&Dept=DEPT"><i class="fa-solid fa-book"></i> <span class="d-none d-sm-inline"><%=Course.Code%> - <%=Course.Name %></span></a></li>
        <li class="breadcrumb-item pl-0"><a href="webenrolment.aspx?page=~/webcontrols/checkout_personal_details_GCFE.ascx"><i class="fa-solid fa-user"></i> <span class="d-none d-sm-inline">Personal Details</span></a></li>
        <li aria-current="page" class="breadcrumb-item pl-0 active pl-4"><i class="fa-solid fa-camera"></i> Photo</li>
    </ol>
</nav>

<div class="progress mb-4" role="progressbar" aria-label="Animated striped example" aria-valuenow="20" aria-valuemin="0" aria-valuemax="100">
    <div class="progress-bar progress-bar-striped progress-bar-animated" style="width: 20%">20%</div>
</div>

<div class="bd-callout bd-callout-gcfe bd-callout-grey">
    <h4><i class="fa-solid fa-camera"></i> Photo Upload</h4>

    <%If IsPhotoRequired = True Then %>
        <p>You are required to upload a photo of your face for your Student ID Card. Please do this here:</p>
    <%Else %>
        <p>You may wish to upload a photo of your face for your Student ID Card. Please do this here:</p>
    <%End If %>

    <div class="alert alert-secondary" role="alert">
        <h5 class="alert-heading mb-3">To ensure your photo is suitable and there are no delays to obtaining your ID card please consider the following:</h5>
        <div class="row">
            <div class="col-md-3 mb-3">
                <div class="card">
                    <div class="card-body">
                        <asp:Image ID="Image1" runat="server" class="img-fluid" />
                    </div>
                    <div class="card-footer">
                        <asp:Label ID="PhotoFilename" runat="server" />
                    </div>
                </div>
            </div>
            <div class="col-md-9 mb-3">
                <div class="card">
                    <div class="card-body">
                        <ul class="list-group">
                            <li class="list-group-item NoBorder">Include your head and shoulders.</li>
                            <li class="list-group-item NoBorder">Be appropriately dressed for a learning environment.</li>
                            <li class="list-group-item NoBorder">Ensure no other people are visible on the photo. Use a clear background if possible.</li>
                            <li class="list-group-item NoBorder">Be facing forwards and looking straight at the camera.</li>
                            <li class="list-group-item NoBorder">Ensure your face is not covered and your eyes are visible without hair in the way.</li>
                            <li class="list-group-item NoBorder">Not wear any hats or head coverings (unless it's for religious or medical reasons).</li>
                            <li class="list-group-item NoBorder">Check the environment is well-lit and the picture is not blurred.</li>
                            <li class="list-group-item NoBorder">Make sure the file is not too large to send to us.</li>
                        </ul>    
                    </div>
                </div>
            </div>
        </div>
    </div>

    <div class="alert alert-primary" role="alert">
        <div class="row">
            <div class="col-md-auto d-flex align-items-center">
                <h3><i class="fa-brands fa-apple"></i></h3>
            </div>
            <div class="col-md">
                <p>
                    Please note, if you upload an HEIC (Apple iPhone) format file it is not currently possible to show you a preview of this file above so please check your device to ensure the photo you sent us is suitable rather than relying on the preview above.
                </p>
            </div>
        </div>
    </div>

    <div class="mb-3">
        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server"
            ControlToValidate="FileUpload1"
            ErrorMessage="Only .jpg, .png, .jpeg, .gif, .heic Files are allowed" Font-Bold="True"
            Font-Size="Medium"
            ValidationExpression="(.*?)\.(jpg|jpeg|png|gif|heic|JPG|JPEG|PNG|GIF|HEIC)$"></asp:RegularExpressionValidator>
        <asp:FileUpload ID="FileUpload1" runat="server" CssClass="form-control" IsRequired="false" ClientIDMode="Static" />
        <input type="text" id="PhotoPath" runat="server" class="form-control d-none" ClientIDMode="Static" accept=".jpg,.jpeg,.png,.gif,.bmp,.heic" />
        <asp:CustomValidator ID="PhotoPathValidator" runat="server"></asp:CustomValidator>

        <asp:LinkButton ID="UploadPhoto" runat="server" OnClick="Upload" CssClass="btn btn-primary mb-3" CausesValidation="false"><i class="fa-solid fa-upload"></i> Upload and Preview</asp:LinkButton>

        <p><strong>Browse for your photo and click to upload it and review how it looks above. If it does not look right, browse for another and click to upload again.</strong></p>
        <p><i>Please ensure your photo is oriented the correct way round</i></p>
        <p>Once you are happy your photo looks acceptable please click on the Next button below</p>
    </div>
    <p class="mt-5"><strong>Not Able To Upload Your Photograph</strong></p>
    <div class=" form-group">
        <cc1:StudentEnrolmentField runat="server" ID="StudentDetailUserDefined24" CustomFieldType="Lookup" StudentEnrolmentFieldType="StudentDetailUserDefined24" ClientIDMode="Static" CustomCaption="Can't upload your photo at the moment - please select a reason why" />
    </div>
</div>

<div class="alert alert-secondary" role="alert">
    <div class="row gap-3">
        <div class="col-md">
            <span class="d-grid gap-2 d-md-block">
                <button type="button" class="btn btn-secondary btn-lg BackButton"> Back</button>
                <cc1:CCCButton ID="btnBack" CssClass="d-none" class="d-none" ClientIDMode="Static" runat="server" Text="Back" ImageResource="btnBack" LinkResource="checkout_personal_details_GCFE_aspx" CausesValidation="false" />
            </span>
        </div>
        <div class="col-md text-end">
            <span class="d-grid gap-2 d-md-block">
                <button type="button" class="btn btn-primary btn-lg NextButton">Next </button>
                <cc1:CCCButton ID="btnContinue" CssClass="d-none" class="d-none" ClientIDMode="Static" runat="server" Text="Continue" ImageResource="btnContinue" LinkResource="checkout_supporting_you_GCFE" CausesValidation="true" SaveForLater="true" SaveForLaterIn="Request" />
            </span>
        </div>
    </div>
</div>

<asp:Panel ID="Panel1" runat="server" Visible="false">

<%--        <asp:Image ID="Image1" runat="server" />--%>
    <br />
    <asp:Button ID="btnSave" runat="server" Text="Upload Photo" OnClick="Save" CssClass="button" CausesValidation="false" />
    <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="Cancel" CssClass="button" CausesValidation="false" />


</asp:Panel>
<asp:Panel ID="Panel2" runat="server" Visible="false" CssClass="alert alert-success">

    <p><i>Thank you - File Uploaded</i></p>
</asp:Panel>

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
        FileUpload1.addEventListener(`change`, function (event) {
            event.preventDefault();

            if (FileUpload1.files[0] == null) {
                PhotoPath.value = ``;
            }
            else {
                PhotoPath.value = FileUpload1.files[0].name;
            }
        });
    });
</script>