<%@ Control Language="VB" AutoEventWireup="false" CodeFile="checkout_personal_details_GCFE.ascx.vb" Inherits="checkout_personal_details_GCFE" %>
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
        <li aria-current="page" class="breadcrumb-item pl-0 active pl-4"><i class="fa-solid fa-user"></i> Personal Details</li>
    </ol>
</nav>

<div class="progress mb-4" role="progressbar" aria-label="Animated striped example" aria-valuenow="15" aria-valuemin="0" aria-valuemax="100">
    <div class="progress-bar progress-bar-striped progress-bar-animated" style="width: 15%">15%</div>
</div>

<div class="bd-callout bd-callout-gcfe bd-callout-grey">
    <h4><i class="fa-solid fa-user"></i> Personal Details</h4>

    <div class="mb-3 form-group">
        <div class="alert alert-primary" role="alert">
            <i class="fa-solid fa-circle-info"></i> Fields in bold are mandatory
        </div>
    </div>
    <div class="mb-3 form-group">
        <cc1:StudentEnrolmentField StudentEnrolmentFieldType="FirstForename" ID="fldFirstForename" runat="server" IsRequired="true" LabelWidth="200" AutoFocus="true" CustomCaption="First Name (Legal Name)" Placeholder="First Legal Name (enter any additional middle names into the Middle Name box)" />
        <asp:CustomValidator ID="fldFirstForenameValidator" runat="server"></asp:CustomValidator>
    </div>
    <div class="mb-3 form-group">
        <cc1:StudentEnrolmentField StudentEnrolmentFieldType="KnownAs" ID="fldKnownAs" runat="server" LabelWidth="200" AutoFocus="true" CustomCaption="Known As Name (if different to legal first name)" Placeholder="e.g. Billy instead of William" />
    </div>
    <div class="mb-3 form-group">
        <cc1:StudentEnrolmentField StudentEnrolmentFieldType="OtherForenames" ID="fldOtherForenames" runat="server" LabelWidth="200" CustomCaption="Middle Name" />
    </div>
    <div class="mb-3 form-group">
        <cc1:StudentEnrolmentField StudentEnrolmentFieldType="Surname" ID="fldSurname" runat="server" IsRequired="true" LabelWidth="200" />
    </div>
    <div class="mb-3 form-group">
        <cc1:StudentEnrolmentField StudentEnrolmentFieldType="Title" ID="fldTitle" runat="server" ClientIDMode="Static" IsRequired="false" LabelWidth="200" />
    </div>
    <div class="mb-3 form-group">
        <cc1:StudentEnrolmentField ID="fldDateOfBirth" runat="server" IsRequired="true" StudentEnrolmentFieldType="DateOfBirth" LabelWidth="200" ClientIDMode="Static" HTML5InputType="date" />
        <asp:CustomValidator ID="fldDateOfBirthValidator" runat="server"></asp:CustomValidator>
        <div class="alert alert-secondary" role="alert" id="AgeInfo">
            &nbsp;
        </div>
    </div>
    <div class="mb-3 form-group">
        <cc1:StudentEnrolmentField StudentEnrolmentFieldType="Sex" ID="fldSex" runat="server" IsRequired="true" LabelWidth="200" ClientIDMode="Static" />
    </div>
    <div class="mb-3 form-group">
        <%--CCCPS-77565 Add preferredpronoun field--%>
        <cc1:StudentEnrolmentField StudentEnrolmentFieldType="PreferredPronounID" ID="fldPreferredPronoun" runat="server" ClientIDMode="Static" LabelWidth="200" CustomCaption="Preferred Pronoun" />
    </div>
</div>

<div class="bd-callout bd-callout-gcfe bd-callout-grey">
    <h4><i class="fa-solid fa-comment"></i> Contact Details</h4>

    <div class="mb-3 form-group">
        <cc1:StudentEnrolmentField StudentEnrolmentFieldType="MobileTel" ID="fldMobileTel" runat="server" ClientIDMode="Static" LabelWidth="200" HTML5InputType="tel" Pattern="^(07[\d]{8,12}|447[\d]{7,11})$" Title="Please enter a valid Mobile Telephone Number starting with 07" />
        <asp:CustomValidator ID="fldMobileTelValidator" runat="server"></asp:CustomValidator>
    </div>
    <div class="mb-3 form-group">
        <div class="alert alert-secondary" role="alert">
            <i class="fa-solid fa-phone"></i> If your mobile number does not start with 07 then please enter it into the Home Landline box below instead
        </div>
    </div>
    <div class="mb-3 form-group">
        <cc1:StudentEnrolmentField StudentEnrolmentFieldType="Tel" ID="fldTel" runat="server" ClientIDMode="Static" LabelWidth="200" HTML5InputType="tel" Pattern="^\d{11}$" Title="Please enter a valid Home Telephone Number containing 11 digits" CustomCaption="Home Landline" />
        <asp:CustomValidator ID="fldTelValidator" runat="server"></asp:CustomValidator>
        <strong>* Please enter at least one phone number</strong>
    </div>
    <div class="mb-3 form-group">
        <cc1:StudentEmailField StudentEnrolmentFieldType="Email" ID="fldEmail" runat="server" IsRequired="true" LabelWidth="200" HTML5InputType="email" Placeholder="e.g. name@domain.com" Pattern="^[a-zA-Z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-zA-Z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-zA-Z0-9](?:[a-zA-Z0-9-]*[a-zA-Z0-9])?\.)+[a-zA-Z0-9](?:[a-zA-Z0-9-]*[a-zA-Z0-9])?$" Title="Please enter a valid Email Address" />
    </div>
</div>

<div class="bd-callout bd-callout-gcfe bd-callout-grey">
    <h4><i class="fa-solid fa-address-book"></i> Address Details</h4>

    <div class="mb-3 form-group">
        <cc1:StudentEnrolmentField runat="server" ID="fldAddress1" StudentEnrolmentFieldType="Address1" IsRequired="true" ClientIDMode="Static" CustomCaption="First Line of Address" LabelWidth="200" Placeholder="Start typing your address here..." />
    </div>
    <div class="mb-3 form-group">
        <cc1:StudentEnrolmentField runat="server" ID="fldAddress2" StudentEnrolmentFieldType="Address2" ClientIDMode="Static" CustomCaption="Second Line of Address" LabelWidth="200" />
    </div>
    <div class="mb-3 form-group">
        <cc1:StudentEnrolmentField runat="server" ID="fldAddress3" StudentEnrolmentFieldType="Address3" ClientIDMode="Static" CustomCaption="Town" LabelWidth="200" />
    </div>
    <div class="mb-3 form-group">
        <cc1:StudentEnrolmentField runat="server" ID="fldAddress4" StudentEnrolmentFieldType="Address4" ClientIDMode="Static" CustomCaption="Parish" LabelWidth="200" />
    </div>
    <div class="mb-3 form-group">
        <cc1:StudentEnrolmentField runat="server" ID="fldCountry" StudentEnrolmentFieldType="Country" CustomCaption="Island" LabelWidth="200" IsRequired="true" ClientIDMode="Static" />
        <select id="CountryDropdown" class="textfield form-control" runat="server" ClientIDMode="Static">
        </select>
    </div>
    <div class="mb-3 form-group">
        <label for="postcode" style="font-weight: bold">Postcode</label>
        <input runat="server" type="text" id="postcode" class="form-control" name="pre[postalcode]" IsRequired="true" ClientIDMode="Static" placeholder="Your postcode here..." autocomplete="off" pattern="^([Gg][Ii][Rr] 0[Aa]{2})|((([A-Za-z][0-9]{1,2})|(([A-Za-z][A-Ha-hJ-Yj-y][0-9]{1,2})|(([A-Za-z][0-9][A-Za-z])|([A-Za-z][A-Ha-hJ-Yj-y][0-9][A-Za-z]?))))\s?[0-9][A-Za-z]{2})$" title="Please enter a valid Postcode" />
        <asp:CustomValidator ID="postcodeValidator" runat="server"></asp:CustomValidator>
        <%--pattern="^([Gg][Ii][Rr] 0[Aa]{2})|((([A-Za-z][0-9]{1,2})|(([A-Za-z][A-Ha-hJ-Yj-y][0-9]{1,2})|(([A-Za-z][0-9][A-Za-z])|([A-Za-z][A-Ha-hJ-Yj-y][0-9][A-Za-z]?))))\s?[0-9][A-Za-z]{2})$" Title="Please enter a valid Postcode"--%>

        <asp:Textbox runat="server" ID="Age31stAug" Placeholder="Age31stAug" type="number" ClientIDMode="Static" class="d-none" />
    </div>
    <%--<uc1:StudentSignature runat="server" ID="StudentSignature" />--%>

</div>

<div class="alert alert-secondary" role="alert">
    <div class="row gap-3">
        <div class="col-md">
            <span class="d-grid gap-2 d-md-block">
                <button type="button" class="btn btn-secondary btn-lg BackButton"> Back</button>
                <cc1:CCCButton ID="btnBack" CssClass="d-none" class="d-none" ClientIDMode="Static" runat="server" Text="Back" ImageResource="btnBack" CausesValidation="false" />
            </span>
        </div>
        <div class="col-md text-end">
            <span class="d-grid gap-2 d-md-block">
                <button type="button" class="btn btn-primary btn-lg NextButton">Next </button>
                <cc1:CCCButton ID="btnContinue" CssClass="d-none" class="d-none" ClientIDMode="Static" runat="server" Text="Continue" ImageResource="btnContinue" LinkResource="checkout_photo_GCFE" CausesValidation="true" SaveForLater="true" SaveForLaterIn="Request" />
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
        //Title
        let fldTitle = document.getElementById(`cboTitle`);
        sortSelectMoveToTop(fldTitle, "Ms");
        sortSelectMoveToTop(fldTitle, "Miss");
        sortSelectMoveToTop(fldTitle, "Mrs");
        sortSelectMoveToTop(fldTitle, "Mr");
        let fldTitleInputBox = addSearchableDropDown(fldTitle);

        //Sex
        let fldSex = document.getElementById("cboSex");
        fldSex.options[1].textContent = "Female";
        fldSex.options[2].textContent = "Male";
        let fldSexInputBox = addSearchableDropDown(fldSex);

        //Preferred Pronoun
        let fldPreferredPronounID = document.getElementById(`cboPreferredPronounID`);
        sortSelectMoveToTop(fldPreferredPronounID, "T");
        sortSelectMoveToTop(fldPreferredPronounID, "U");
        sortSelectMoveToTop(fldPreferredPronounID, "I");
        sortSelectMoveToTop(fldPreferredPronounID, "S");
        sortSelectMoveToTop(fldPreferredPronounID, "H");
        let fldPreferredPronounIDInputBox = addSearchableDropDown(fldPreferredPronounID);

        //Country - Custom
        let fldCountryCustom = document.getElementById(`CountryDropdown`);
        sortSelectMoveToTop(fldCountryCustom, "GSK");
        sortSelectMoveToTop(fldCountryCustom, "GHM");
        sortSelectMoveToTop(fldCountryCustom, "GAL");
        sortSelectMoveToTop(fldCountryCustom, "GG");
        sortSelectMoveToTop(fldCountryCustom, "XF");
        let fldCountryCustomInputBox = addSearchableDropDown(fldCountryCustom);

        //Country - Hide Original Text Box
        let fldCountry = document.getElementById(`txtCountry`);
        fldCountry.classList.add("d-none");

        //Country - Update Primary Text Field on Custom One Change
        fldCountryCustom.addEventListener(`change`, function (event) {
            fldCountry.value = fldCountryCustom.options[fldCountryCustom.selectedIndex].text;
        });

        //Country - Update Select If Text Value is Updated by Address Software
        fldCountry.addEventListener(`change`, function (event) {
            let changeEvent = new Event(`change`);
            fldCountryCustom.selectedIndex = [...fldCountryCustom.options].findIndex(option => option.text === fldCountry.value);
            fldCountryCustom.dispatchEvent(changeEvent);
        });

        let tel = document.getElementById(`txtTel`);
        let mobileTel = document.getElementById(`txtMobileTel`);

        //mobileTel.oninvalid = function (event) {
        //    event.target.setCustomValidity(`Mobile number is invald`);
        //}

        //Field validation
        tel.addEventListener(`blur`, function (event) {
            event.preventDefault();
            validateField(this, event)
        });

        tel.addEventListener(`valid`, function (event) {
            event.preventDefault();
            validateField(this, event)
        });

        tel.addEventListener(`invalid`, function (event) {
            event.preventDefault();
            validateField(this, event);
        });

        mobileTel.addEventListener(`blur`, function (event) {
            event.preventDefault();
            validateField(this, event)
        });

        mobileTel.addEventListener(`valid`, function (event) {
            event.preventDefault();
            validateField(this, event)
        });

        mobileTel.addEventListener(`invalid`, function (event) {
            event.preventDefault();
            validateField(this, event);
        });

        function validateField(field, event) {
            let lastElem = field.parentNode.lastChild;

            if (event.target.validity.valid) {
                if (lastElem.classList.contains("error")) {
                    field.parentNode.removeChild(lastElem);
                }
            }
            else {
                if (!lastElem.classList.contains("error")) {
                    let errorMsgElem = document.createElement(`span`);
                    field.parentNode.insertBefore(errorMsgElem, field.nextSibling);

                    errorMsgElem.textContent = field.title;
                    errorMsgElem.classList.add(`error`);
                }
            }
        }

        // Get Address IO
        let fldAddress4 = document.getElementById(`txtAddress4`);
        let fldPostCode = document.getElementById(`postcode`);
        document.addEventListener("getaddress-autocomplete-suggestions", function (e) {
            console.log(e.suggestions);
        })

        document.addEventListener("getaddress-autocomplete-suggestions-failed", function (e) {
            console.log(e.status);
            console.log(e.message);
        })

        document.addEventListener("getaddress-autocomplete-address-selected", function (e) {
            console.log(e.address);

            fldPostCode.dispatchEvent(postCodeKeyup); //Can call a custom function after selection
            fldAddress4.dispatchEvent(address4Change);
        })

        document.addEventListener("getaddress-autocomplete-address-selected-failed", function (e) {
            console.log(e.status);
            console.log(e.message);
        })

        const autocomplete = getAddress.autocomplete(
            'txtAddress1',
            //'ROVbeBy1-0yONiM6WBChSA42153'
            '45VgWReQcEaaPAtheoJfYg42377'
        );

        autocomplete.addEventListener("getaddress-autocomplete-address-selected", function (e) {
            document.getElementById('txtAddress1').value = e.address.formatted_address[0];
            document.getElementById('txtAddress2').value = e.address.formatted_address[1];
            document.getElementById('txtAddress3').value = e.address.formatted_address[2];
            document.getElementById('txtAddress4').value = e.address.formatted_address[3];
            document.getElementById('txtCountry').value = e.address.formatted_address[4];
            document.getElementById('postcode').value = e.address.postcode;
        })


        //Get Address IO --Overwrite country from Channel Islands to Guernsey so Country Drop-Down Will Select This Option Otherwise Populate with Selected Country If In List
        let postCodeKeyup = new Event('keyup');

        fldPostCode.addEventListener(`keyup`, function (event) {
            let changeEvent = new Event(`change`);

            if (fldPostCode.value.substring(0, 2) === "GY") {
                if (fldCountry.value !== "Guernsey") {
                    fldCountry.value = "Guernsey";

                    fldCountry.dispatchEvent(changeEvent);
                }
            }
            else {
                fldCountry.dispatchEvent(changeEvent);
            }
        });

        let address4Change = new Event('change');

        fldAddress4.addEventListener(`change`, function (event) {
            let fldAddress4Value = fldAddress4.value;

            if (fldAddress4Value.includes(", Guernsey")) {
                fldAddress4.value = fldAddress4Value.replace(", Guernsey", "");
            }
        });

        //Age Calculation
        let dob = document.getElementById(`txtDateOfBirth`);

        if (dob.value !== null) {
            //If DOB already has a value then attempt to show age from DOB
            //alert(`|` + dob.value + `|`);
            showAge();
        }
        dob.addEventListener(`keyup`, function (event) {
            showAge();
        });
        dob.addEventListener(`blur`, function (event) {
            showAge();
        });

        function showAge() {
            let today = new Date();
            let date31stAug = new Date(today.getFullYear() + `-08-31`);
            let dateOfBirth = new Date(dob.value);
            let age31stAug = calculateAge(dateOfBirth);
            let AgeInfo = document.getElementById(`AgeInfo`);
            let AgeField = document.getElementById(`Age31stAug`);

            if (isNaN(dateOfBirth) || (dateOfBirth === ` `)) {
                AgeInfo.innerHTML = `&nbsp;`;
                AgeField.value = ``;
            }
            else {
                AgeInfo.innerHTML = `<i class="fa-solid fa-calendar-day"></i> Age on ${date31stAug.getDate()}${nth(date31stAug.getDate())} ${date31stAug.toLocaleString(`default`, { month: `long` })} ${date31stAug.getFullYear()}: <kbd>${age31stAug}</kbd>`;
                AgeField.value = age31stAug;
            }
        }
    });
</script>