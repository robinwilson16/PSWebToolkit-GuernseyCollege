<%@ Control Language="VB" AutoEventWireup="false" CodeFile="checkout_parent_guardian.ascx.vb" Inherits="webcontrols_checkout_parent_guardian" %>

<%@ Register Assembly="PSWebEnrolmentKit" Namespace="CompassCC.ProSolution.PSWebEnrolmentKit"
    TagPrefix="cc1" %>


<br />
<br />
<ol class="breadcrumb" id="breadcrumbapps" runat="server">
    <li><a href="default.aspx">Home</a></li>
    <li><a href="webenrolment.aspx?page=~/webcontrols/checkout.ascx">Checkout</a></li>
    <li><a href="webenrolment.aspx?page=~/webcontrols/checkout_applications.ascx">Personal Details</a></li>

    <li><a href="webenrolment.aspx?page=~/webcontrols/checkout_applications2.ascx">Further Details</a></li>
    <li class="active">Parent / Guardian</li>
</ol>
<ol class="breadcrumb" id="breadcrumbenrols" runat="server">
    <li><a href="default.aspx">Home</a></li>
    <li><a href="webenrolment.aspx?page=~/webcontrols/checkout.ascx">Checkout</a></li>
    <li><a href="webenrolment.aspx?page=~/webcontrols/checkout_enrolments.ascx">Personal Details</a></li>

    <li><a href="webenrolment.aspx?page=~/webcontrols/checkout_enrolments2.ascx">Further Details</a></li>
    <li class="active">Parent / Guardian</li>
</ol>

<div class="panel panel-success" id="parent1panel" runat="server">
    <div class="panel-heading">Contact Details - Parent/Guardian 1</div>
    <div class=" form-group">
        <cc1:StudentEnrolmentField StudentEnrolmentFieldType="ParentFirstName" ID="StudentEnrolmentField2" runat="server" IsRequired="false" LabelWidth="200" CustomCaption="First Name" />
    </div>
    <div class=" form-group">
        <cc1:StudentEnrolmentField StudentEnrolmentFieldType="ParentSurname" ID="StudentEnrolmentField6" runat="server" IsRequired="false" LabelWidth="200" CustomCaption="Surname" />
    </div>
    <div class=" form-group">
        <cc1:StudentEnrolmentField StudentEnrolmentFieldType="ParentTitle" ID="StudentEnrolmentField8" runat="server" IsRequired="false" LabelWidth="200" CustomCaption="Title" />
    </div>
    <div class=" form-group">
        <cc1:StudentEnrolmentField StudentEnrolmentFieldType="ParentRelationshipID" ID="StudentEnrolmentField1" runat="server" IsRequired="false" LabelWidth="200" CustomCaption="Relationship" />
    </div>
    <div class=" form-group">
        <cc1:StudentEnrolmentField StudentEnrolmentFieldType="ParentPhoneNumber" ID="StudentEnrolmentField10" runat="server" LabelWidth="200" CustomCaption="Tel" />
    </div>
    <div class=" form-group">
        <cc1:StudentEnrolmentField StudentEnrolmentFieldType="ParentEmailAddress" ID="StudentEnrolmentField11" runat="server" LabelWidth="200" CustomCaption="Email" Pattern="^[a-zA-Z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-zA-Z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-zA-Z0-9](?:[a-zA-Z0-9-]*[a-zA-Z0-9])?\.)+[a-zA-Z0-9](?:[a-zA-Z0-9-]*[a-zA-Z0-9])?$" />
    </div>
    <div class=" form-group">
        <cc1:StudentEnrolmentField StudentEnrolmentFieldType="ParentMobileTel" ID="StudentEmailField1" runat="server" IsRequired="false" LabelWidth="200" CustomCaption="Mobile" />
    </div>
    <div class=" form-group">
        <cc1:StudentEnrolmentField StudentEnrolmentFieldType="IsLivingWithParent" ID="StudentEnrolmentField16" runat="server" IsRequired="false" LabelWidth="200" Width="40" CustomCaption="Is Living With Parent?" />
    </div>
    <div class="row">
        <div class="col-sm-2">
            <div class=" form-group">
                <cc1:StudentEnrolmentField StudentEnrolmentFieldType="ParentConsentGiven" ID="StudentEnrolmentField4" runat="server" IsRequired="false" LabelWidth="200" CustomCaption="Consent Given" />
            </div>
            <div class=" form-group">
                <cc1:StudentEnrolmentField StudentEnrolmentFieldType="ParentAcceptMarketingConsent" ID="StudentEnrolmentField5" runat="server" IsRequired="false" LabelWidth="200" CustomCaption="Accept Marketing Consent" />
            </div>
        </div>
        <div class="col-sm-2">
            <div class=" form-group">
                <cc1:StudentEnrolmentField StudentEnrolmentFieldType="ParentConsentGivenDate" ID="StudentEnrolmentField7" runat="server" IsRequired="false" LabelWidth="200" CustomCaption="Consent Given Date" />
            </div>
            <div class=" form-group">
                <cc1:StudentEnrolmentField StudentEnrolmentFieldType="ParentAcceptShareInfoConsent" ID="StudentEnrolmentField18" runat="server" IsRequired="false" LabelWidth="200" CustomCaption="Accept Share Info Consent" />
            </div>
        </div>
        <div class="col-sm-1">
        </div>
        <div class="col-sm-3">
            <div class="row">
                <div>
                    <span>Allow Contact</span>
                </div>
            </div>
            <div class="row" style="border: 1px solid #ccc">
                <div class="col-sm-6">
                    <div class=" form-group">
                        <cc1:StudentEnrolmentField StudentEnrolmentFieldType="ParentContactAllowedByTelephone" ID="StudentEnrolmentField19" runat="server" IsRequired="false" LabelWidth="200" CustomCaption="By Telephone" />
                    </div>
                    <div class=" form-group">
                        <cc1:StudentEnrolmentField StudentEnrolmentFieldType="ParentContactAllowedByPost" ID="StudentEnrolmentField20" runat="server" IsRequired="false" LabelWidth="200" CustomCaption="By Post" />
                    </div>
                    <div class=" form-group">
                        <cc1:StudentEnrolmentField StudentEnrolmentFieldType="ParentCanBeContactedBySMS" ID="StudentEnrolmentField21" runat="server" IsRequired="false" LabelWidth="200" CustomCaption="By SMS" />
                    </div>
                </div>
                <div class="col-sm-6">
                    <div class=" form-group">
                        <cc1:StudentEnrolmentField StudentEnrolmentFieldType="ParentContactAllowedByEmail" ID="StudentEnrolmentField22" runat="server" IsRequired="false" LabelWidth="200" CustomCaption="By Email" />
                    </div>
                    <div class=" form-group">
                        <cc1:StudentEnrolmentField StudentEnrolmentFieldType="ParentCanBeContactedBySocialMedia" ID="StudentEnrolmentField23" runat="server" IsRequired="false" LabelWidth="200" CustomCaption="By Social Media" />
                    </div>
                </div>
            </div>
        </div>
        <div class="col-sm-1">
        </div>
        <div class="col-sm-2">
            <div class="row">
                <div>
                    <span>Allow Share</span>
                </div>
            </div>
            <div class="row" style="border: 1px solid #ccc">
                <div class="col-sm-12">
                    <div class=" form-group">
                        <cc1:StudentEnrolmentField StudentEnrolmentFieldType="ParentCanBeSharedByEmail" ID="StudentEnrolmentField24" runat="server" IsRequired="false" LabelWidth="200" CustomCaption="By Email" />
                    </div>
                    <div class=" form-group">
                        <cc1:StudentEnrolmentField StudentEnrolmentFieldType="ParentCanBeSharedByWebSite" ID="StudentEnrolmentField25" runat="server" IsRequired="false" LabelWidth="200" CustomCaption="By Web Site" />
                    </div>
                    <div class=" form-group">
                        <cc1:StudentEnrolmentField StudentEnrolmentFieldType="ParentCanBeSharedBySocialMedia" ID="StudentEnrolmentField26" runat="server" IsRequired="false" LabelWidth="200" CustomCaption="By Social Media" />
                    </div>
                </div>
            </div>

        </div>
        <div class="col-sm-1">
        </div>

    </div>
</div>

<div class="panel panel-success" id="parent2panel" runat="server">
    <div class="panel-heading">Contact Details - Parent/Guardian 2</div>
   <div class=" form-group">
        <cc1:StudentEnrolmentField StudentEnrolmentFieldType="Parent2FirstName" ID="StudentEnrolmentField3" runat="server" IsRequired="false" LabelWidth="200" CustomCaption="First Name" />
    </div>
    <div class=" form-group">
        <cc1:StudentEnrolmentField StudentEnrolmentFieldType="Parent2Surname" ID="StudentEnrolmentField9" runat="server" IsRequired="false" LabelWidth="200" CustomCaption="Surname" />
    </div>
    <div class=" form-group">
        <cc1:StudentEnrolmentField StudentEnrolmentFieldType="Parent2Title" ID="StudentEnrolmentField12" runat="server" IsRequired="false" LabelWidth="200" CustomCaption="Title" />
    </div>
    <div class=" form-group">
        <cc1:StudentEnrolmentField StudentEnrolmentFieldType="Parent2RelationshipID" ID="StudentEnrolmentField13" runat="server" IsRequired="false" LabelWidth="200" CustomCaption="Relationship" />
    </div>
    <div class=" form-group">
        <cc1:StudentEnrolmentField StudentEnrolmentFieldType="Parent2PhoneNumber" ID="StudentEnrolmentField14" runat="server" LabelWidth="200" CustomCaption="Tel" />
    </div>
    <div class=" form-group">
        <cc1:StudentEnrolmentField StudentEnrolmentFieldType="Parent2EmailAddress" ID="StudentEnrolmentField15" runat="server" LabelWidth="200" CustomCaption="Email" Pattern="^[a-zA-Z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-zA-Z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-zA-Z0-9](?:[a-zA-Z0-9-]*[a-zA-Z0-9])?\.)+[a-zA-Z0-9](?:[a-zA-Z0-9-]*[a-zA-Z0-9])?$" />
    </div>
    <div class=" form-group">
        <cc1:StudentEnrolmentField StudentEnrolmentFieldType="Parent2MobileTel" ID="StudentEnrolmentField17" runat="server" IsRequired="false" LabelWidth="200" CustomCaption="Mobile" />
    </div>
    <div class=" form-group">
        <cc1:StudentEnrolmentField StudentEnrolmentFieldType="IsLivingWithParent2" ID="StudentEnrolmentField39" runat="server" IsRequired="false" LabelWidth="200" Width="40" CustomCaption="Is Living With Parent?" />
    </div>
    <div class="row">
        <div class="col-sm-2">
            <div class=" form-group">
                <cc1:StudentEnrolmentField StudentEnrolmentFieldType="Parent2ConsentGiven" ID="StudentEnrolmentField27" runat="server" IsRequired="false" LabelWidth="200" CustomCaption="Consent Given" />
            </div>
            <div class=" form-group">
                <cc1:StudentEnrolmentField StudentEnrolmentFieldType="Parent2AcceptMarketingConsent" ID="StudentEnrolmentField28" runat="server" IsRequired="false" LabelWidth="200" CustomCaption="Accept Marketing Consent" />
            </div>
        </div>
        <div class="col-sm-2">
            <div class=" form-group">
                <cc1:StudentEnrolmentField StudentEnrolmentFieldType="Parent2ConsentGivenDate" ID="StudentEnrolmentField29" runat="server" IsRequired="false" LabelWidth="200" CustomCaption="Consent Given Date" />
            </div>
            <div class=" form-group">
                <cc1:StudentEnrolmentField StudentEnrolmentFieldType="Parent2AcceptShareInfoConsent" ID="StudentEnrolmentField30" runat="server" IsRequired="false" LabelWidth="200" CustomCaption="Accept Share Info Consent" />
            </div>
        </div>
        <div class="col-sm-1">
        </div>
        <div class="col-sm-3">
            <div class="row">
                <div>
                    <span>Allow Contact</span>
                </div>
            </div>
            <div class="row" style="border: 1px solid #ccc">
                <div class="col-sm-6">
                    <div class=" form-group">
                        <cc1:StudentEnrolmentField StudentEnrolmentFieldType="Parent2ContactAllowedByTelephone" ID="StudentEnrolmentField31" runat="server" IsRequired="false" LabelWidth="200" CustomCaption="By Telephone" />
                    </div>
                    <div class=" form-group">
                        <cc1:StudentEnrolmentField StudentEnrolmentFieldType="Parent2ContactAllowedByPost" ID="StudentEnrolmentField32" runat="server" IsRequired="false" LabelWidth="200" CustomCaption="By Post" />
                    </div>
                    <div class=" form-group">
                        <cc1:StudentEnrolmentField StudentEnrolmentFieldType="Parent2CanBeContactedBySMS" ID="StudentEnrolmentField33" runat="server" IsRequired="false" LabelWidth="200" CustomCaption="By SMS" />
                    </div>
                </div>
                <div class="col-sm-6">
                    <div class=" form-group">
                        <cc1:StudentEnrolmentField StudentEnrolmentFieldType="Parent2ContactAllowedByEmail" ID="StudentEnrolmentField34" runat="server" IsRequired="false" LabelWidth="200" CustomCaption="By Email" />
                    </div>
                    <div class=" form-group">
                        <cc1:StudentEnrolmentField StudentEnrolmentFieldType="Parent2CanBeContactedBySocialMedia" ID="StudentEnrolmentField35" runat="server" IsRequired="false" LabelWidth="200" CustomCaption="By Social Media" />
                    </div>
                </div>
            </div>
        </div>
        <div class="col-sm-1">
        </div>
        <div class="col-sm-2">
            <div class="row">
                <div>
                    <span>Allow Share</span>
                </div>
            </div>
            <div class="row" style="border: 1px solid #ccc">
                <div class="col-sm-12">
                    <div class=" form-group">
                        <cc1:StudentEnrolmentField StudentEnrolmentFieldType="Parent2CanBeSharedByEmail" ID="StudentEnrolmentField36" runat="server" IsRequired="false" LabelWidth="200" CustomCaption="By Email" />
                    </div>
                    <div class=" form-group">
                        <cc1:StudentEnrolmentField StudentEnrolmentFieldType="Parent2CanBeSharedByWebSite" ID="StudentEnrolmentField37" runat="server" IsRequired="false" LabelWidth="200" CustomCaption="By Web Site" />
                    </div>
                    <div class=" form-group">
                        <cc1:StudentEnrolmentField StudentEnrolmentFieldType="Parent2CanBeSharedBySocialMedia" ID="StudentEnrolmentField38" runat="server" IsRequired="false" LabelWidth="200" CustomCaption="By Social Media" />
                    </div>
                </div>
            </div>

        </div>
        <div class="col-sm-1">
        </div>

    </div>
</div>


<cc1:CCCButton ID="btnBack" runat="server" Text="Back" ImageResource="btnBack" />
<cc1:CCCButton ID="btnContinue" runat="server" Text="Continue" ImageResource="btnContinue" CausesValidation="true" EnableEnterKey="true" SaveForLater="true" SaveForLaterIn="Request" />
<br />
<asp:ValidationSummary ID="ValidationSummary1" runat="server" CssClass="alert alert-danger" ForeColor="" />

