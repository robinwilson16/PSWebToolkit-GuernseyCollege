<%@ Control Language="VB" AutoEventWireup="false" CodeFile="checkout_other_contacts.ascx.vb" Inherits="webcontrols_checkout_other_contacts" %>

<%@ Register Assembly="PSWebEnrolmentKit" Namespace="CompassCC.ProSolution.PSWebEnrolmentKit"
    TagPrefix="cc1" %>


<br />
<br />
<ol class="breadcrumb" id="breadcrumbapps" runat="server">
    <li><a href="default.aspx">Home</a></li>
    <li><a href="webenrolment.aspx?page=~/webcontrols/checkout.ascx">Checkout</a></li>
    <li><a href="webenrolment.aspx?page=~/webcontrols/checkout_applications.ascx">Personal Details</a></li>

    <li><a href="webenrolment.aspx?page=~/webcontrols/checkout_applications2.ascx">Further Details</a></li>
    <li><a href="webenrolment.aspx?page=~/webcontrols/checkout_parent_guardian.ascx">Parent / Guardian</a></li>
    <li class="active">Other Contacts</li>
</ol>
<ol class="breadcrumb" id="breadcrumbenrols" runat="server">
    <li><a href="default.aspx">Home</a></li>
    <li><a href="webenrolment.aspx?page=~/webcontrols/checkout.ascx">Checkout</a></li>
    <li><a href="webenrolment.aspx?page=~/webcontrols/checkout_enrolments.ascx">Personal Details</a></li>

    <li><a href="webenrolment.aspx?page=~/webcontrols/checkout_enrolments2.ascx">Further Details</a></li>
    <li><a href="webenrolment.aspx?page=~/webcontrols/checkout_parent_guardian.ascx">Parent / Guardian</a></li>
    <li class="active">Other Contacts</li>
</ol>

<div class="panel panel-success" id="Contact1panel" runat="server">
    <div class="panel-heading">Other Contact Details - 1</div>
    <div class=" form-group">
        <cc1:StudentEnrolmentField StudentEnrolmentFieldType="Contact1ContactTypeID" ID="StudentEnrolmentField17" runat="server" IsRequired="false" LabelWidth="200" CustomCaption="Contact Type" />
    </div>
    <div class=" form-group">
        <cc1:StudentEnrolmentField StudentEnrolmentFieldType="Contact1" ID="StudentEnrolmentField2" runat="server" IsRequired="false" LabelWidth="200" CustomCaption="Name" />
    </div>    
    <div class=" form-group">
        <cc1:StudentEnrolmentField StudentEnrolmentFieldType="Contact1Title" ID="StudentEnrolmentField8" runat="server" IsRequired="false" LabelWidth="200" CustomCaption="Title" />
    </div>
    <div class=" form-group">
        <cc1:StudentEnrolmentField StudentEnrolmentFieldType="Contact1RelationshipID" ID="StudentEnrolmentField1" runat="server" IsRequired="false" LabelWidth="200" CustomCaption="Relationship" />
    </div>
    <div class=" form-group">
        <cc1:StudentEnrolmentField StudentEnrolmentFieldType="Contact1Tel" ID="StudentEnrolmentField10" runat="server" LabelWidth="200" CustomCaption="Tel" />
    </div>
    <div class=" form-group">
        <cc1:StudentEnrolmentField StudentEnrolmentFieldType="Contact1EmailAddress" ID="StudentEnrolmentField11" runat="server" LabelWidth="200" CustomCaption="Email" Pattern="^[a-zA-Z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-zA-Z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-zA-Z0-9](?:[a-zA-Z0-9-]*[a-zA-Z0-9])?\.)+[a-zA-Z0-9](?:[a-zA-Z0-9-]*[a-zA-Z0-9])?$" />
    </div>
    <div class=" form-group">
        <cc1:StudentEnrolmentField StudentEnrolmentFieldType="Contact1MobileTel" ID="StudentEmailField1" runat="server" IsRequired="false" LabelWidth="200" CustomCaption="Mobile" />
    </div>
    <div class=" form-group">
        <cc1:StudentEnrolmentField StudentEnrolmentFieldType="IsLivingWithContact1" ID="StudentEnrolmentField16" runat="server" IsRequired="false" LabelWidth="200" Width="40" CustomCaption="Is Living With Contact?" />
    </div>
    <div class="row">
        <div class="col-sm-2">
            <div class=" form-group">
                <cc1:StudentEnrolmentField StudentEnrolmentFieldType="Contact1ConsentGiven" ID="StudentEnrolmentField4" runat="server" IsRequired="false" LabelWidth="200" CustomCaption="Consent Given" />
            </div>
            <div class=" form-group">
                <cc1:StudentEnrolmentField StudentEnrolmentFieldType="Contact1AcceptMarketingConsent" ID="StudentEnrolmentField5" runat="server" IsRequired="false" LabelWidth="200" CustomCaption="Accept Marketing Consent" />
            </div>
        </div>
        <div class="col-sm-2">
            <div class=" form-group">
                <cc1:StudentEnrolmentField StudentEnrolmentFieldType="Contact1ConsentGivenDate" ID="fldContact1ConsentGivenDate" runat="server" IsRequired="false" LabelWidth="200" CustomCaption="Consent Given Date" />
            </div>
            <div class=" form-group">
                <cc1:StudentEnrolmentField StudentEnrolmentFieldType="Contact1AcceptShareInfoConsent" ID="StudentEnrolmentField18" runat="server" IsRequired="false" LabelWidth="200" CustomCaption="Accept Share Info Consent" />
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
                        <cc1:StudentEnrolmentField StudentEnrolmentFieldType="Contact1ContactAllowedByTelephone" ID="StudentEnrolmentField19" runat="server" IsRequired="false" LabelWidth="200" CustomCaption="By Telephone" />
                    </div>
                    <div class=" form-group">
                        <cc1:StudentEnrolmentField StudentEnrolmentFieldType="Contact1ContactAllowedByPost" ID="StudentEnrolmentField20" runat="server" IsRequired="false" LabelWidth="200" CustomCaption="By Post" />
                    </div>
                    <div class=" form-group">
                        <cc1:StudentEnrolmentField StudentEnrolmentFieldType="Contact1CanBeContactedBySMS" ID="StudentEnrolmentField21" runat="server" IsRequired="false" LabelWidth="200" CustomCaption="By SMS" />
                    </div>
                </div>
                <div class="col-sm-6">
                    <div class=" form-group">
                        <cc1:StudentEnrolmentField StudentEnrolmentFieldType="Contact1ContactAllowedByEmail" ID="StudentEnrolmentField22" runat="server" IsRequired="false" LabelWidth="200" CustomCaption="By Email" />
                    </div>
                    <div class=" form-group">
                        <cc1:StudentEnrolmentField StudentEnrolmentFieldType="Contact1CanBeContactedBySocialMedia" ID="StudentEnrolmentField23" runat="server" IsRequired="false" LabelWidth="200" CustomCaption="By Social Media" />
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
                        <cc1:StudentEnrolmentField StudentEnrolmentFieldType="Contact1CanBeSharedByEmail" ID="StudentEnrolmentField24" runat="server" IsRequired="false" LabelWidth="200" CustomCaption="By Email" />
                    </div>
                    <div class=" form-group">
                        <cc1:StudentEnrolmentField StudentEnrolmentFieldType="Contact1CanBeSharedByWebSite" ID="StudentEnrolmentField25" runat="server" IsRequired="false" LabelWidth="200" CustomCaption="By Web Site" />
                    </div>
                    <div class=" form-group">
                        <cc1:StudentEnrolmentField StudentEnrolmentFieldType="Contact1CanBeSharedBySocialMedia" ID="StudentEnrolmentField26" runat="server" IsRequired="false" LabelWidth="200" CustomCaption="By Social Media" />
                    </div>
                </div>
            </div>

        </div>
        <div class="col-sm-1">
        </div>

    </div>
</div>

<div class="panel panel-success" id="Contact2panel" runat="server">
    <div class="panel-heading">Other Contact Details - 2</div>
    <div class=" form-group">
        <cc1:StudentEnrolmentField StudentEnrolmentFieldType="Contact2ContactTypeID" ID="StudentEnrolmentField39" runat="server" IsRequired="false" LabelWidth="200" CustomCaption="Contact Type" />
    </div>
    <div class=" form-group">
        <cc1:StudentEnrolmentField StudentEnrolmentFieldType="Contact2" ID="StudentEnrolmentField9" runat="server" IsRequired="false" LabelWidth="200" CustomCaption="Contact2 Name" />
    </div>    
    <div class=" form-group">
        <cc1:StudentEnrolmentField StudentEnrolmentFieldType="Contact2Title" ID="StudentEnrolmentField3" runat="server" IsRequired="false" LabelWidth="200" CustomCaption="Title" />
    </div>
    <div class=" form-group">
        <cc1:StudentEnrolmentField StudentEnrolmentFieldType="Contact2RelationshipID" ID="StudentEnrolmentField6" runat="server" IsRequired="false" LabelWidth="200" CustomCaption="Relationship" />
    </div>
    <div class=" form-group">
        <cc1:StudentEnrolmentField StudentEnrolmentFieldType="Contact2Tel" ID="StudentEnrolmentField12" runat="server" LabelWidth="200" CustomCaption="Tel" />
    </div>
    <div class=" form-group">
        <cc1:StudentEnrolmentField StudentEnrolmentFieldType="Contact2EmailAddress" ID="StudentEnrolmentField13" runat="server" LabelWidth="200" CustomCaption="Email" Pattern="^[a-zA-Z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-zA-Z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-zA-Z0-9](?:[a-zA-Z0-9-]*[a-zA-Z0-9])?\.)+[a-zA-Z0-9](?:[a-zA-Z0-9-]*[a-zA-Z0-9])?$" />
    </div>
    <div class=" form-group">
        <cc1:StudentEnrolmentField StudentEnrolmentFieldType="Contact2MobileTel" ID="StudentEnrolmentField14" runat="server" IsRequired="false" LabelWidth="200" CustomCaption="Mobile" />
    </div>
    <div class=" form-group">
        <cc1:StudentEnrolmentField StudentEnrolmentFieldType="IsLivingWithContact2" ID="StudentEnrolmentField15" runat="server" IsRequired="false" LabelWidth="200" Width="40" CustomCaption="Is Living With Contact?" />
    </div>
    <div class="row">
        <div class="col-sm-2">
            <div class=" form-group">
                <cc1:StudentEnrolmentField StudentEnrolmentFieldType="Contact2ConsentGiven" ID="StudentEnrolmentField27" runat="server" IsRequired="false" LabelWidth="200" CustomCaption="Consent Given" />
            </div>
            <div class=" form-group">
                <cc1:StudentEnrolmentField StudentEnrolmentFieldType="Contact2AcceptMarketingConsent" ID="StudentEnrolmentField28" runat="server" IsRequired="false" LabelWidth="200" CustomCaption="Accept Marketing Consent" />
            </div>
        </div>
        <div class="col-sm-2">
            <div class=" form-group">
                <cc1:StudentEnrolmentField StudentEnrolmentFieldType="Contact2ConsentGivenDate" ID="fldContact2ConsentGivenDate" runat="server" IsRequired="false" LabelWidth="200" CustomCaption="Consent Given Date" />
            </div>
            <div class=" form-group">
                <cc1:StudentEnrolmentField StudentEnrolmentFieldType="Contact2AcceptShareInfoConsent" ID="StudentEnrolmentField30" runat="server" IsRequired="false" LabelWidth="200" CustomCaption="Accept Share Info Consent" />
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
                        <cc1:StudentEnrolmentField StudentEnrolmentFieldType="Contact2ContactAllowedByTelephone" ID="StudentEnrolmentField31" runat="server" IsRequired="false" LabelWidth="200" CustomCaption="By Telephone" />
                    </div>
                    <div class=" form-group">
                        <cc1:StudentEnrolmentField StudentEnrolmentFieldType="Contact2ContactAllowedByPost" ID="StudentEnrolmentField32" runat="server" IsRequired="false" LabelWidth="200" CustomCaption="By Post" />
                    </div>
                    <div class=" form-group">
                        <cc1:StudentEnrolmentField StudentEnrolmentFieldType="Contact2CanBeContactedBySMS" ID="StudentEnrolmentField33" runat="server" IsRequired="false" LabelWidth="200" CustomCaption="By SMS" />
                    </div>
                </div>
                <div class="col-sm-6">
                    <div class=" form-group">
                        <cc1:StudentEnrolmentField StudentEnrolmentFieldType="Contact2ContactAllowedByEmail" ID="StudentEnrolmentField34" runat="server" IsRequired="false" LabelWidth="200" CustomCaption="By Email" />
                    </div>
                    <div class=" form-group">
                        <cc1:StudentEnrolmentField StudentEnrolmentFieldType="Contact2CanBeContactedBySocialMedia" ID="StudentEnrolmentField35" runat="server" IsRequired="false" LabelWidth="200" CustomCaption="By Social Media" />
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
                        <cc1:StudentEnrolmentField StudentEnrolmentFieldType="Contact2CanBeSharedByEmail" ID="StudentEnrolmentField36" runat="server" IsRequired="false" LabelWidth="200" CustomCaption="By Email" />
                    </div>
                    <div class=" form-group">
                        <cc1:StudentEnrolmentField StudentEnrolmentFieldType="Contact2CanBeSharedByWebSite" ID="StudentEnrolmentField37" runat="server" IsRequired="false" LabelWidth="200" CustomCaption="By Web Site" />
                    </div>
                    <div class=" form-group">
                        <cc1:StudentEnrolmentField StudentEnrolmentFieldType="Contact2CanBeSharedBySocialMedia" ID="StudentEnrolmentField38" runat="server" IsRequired="false" LabelWidth="200" CustomCaption="By Social Media" />
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