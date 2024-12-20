var LANGUAGES = {
    "INDEX" : {
        "GeneralDescription": "This page lists the " + qas.lang.PROGRAMMING_LANGUAGE + " implementation code for available search types.",

        "ClickHereForComparisonMatrix" : "Compare search types",

        "AddressCapture" : "Find an address",
        "AddressCaptureFurtherDescription" : "<strong>Capture a customer's address using key address information only.</strong>",
        "AddressCaptureTypicalUseDescription" : "Ideal where address entry needs to performed quickly.<p>The user selects the country and is then prompted to enter key elements of the address (a postcode and house number, for example). If this results in multiple matches, the user is prompted to pick from a list of suggested addresses. The user is also able to edit the address once it is returned.</p>",
        "Verification" : "Verify an address",
        "VerificationFurtherDescription" : "<strong>Verify a customer's address and replace missing or incorrect information.</strong>",
        "VerificationTypicalUseDescription" : "Ideal for untrained users, address information is entered in the same way as on an envelope.<p>Matched addresses are completed, formatted and returned. If a match is not found, the user selects from a number of suggested alternatives or proceeds with the address as they entered it.</p>"
    },
    "MATRIX" : {
        "GeneralDescription": "<p>Each search type demonstrates a different way of integrating address management into a website.</p><p>This table summarises the key features and how they differ between search types.</p>",

        "TableHeaderAddressCapture": "Find an address",
        "TableHeaderVerification": "Verify an address",
        
        "Overview": "Overview",
        "Features": "Features",
        
        "TypicalUseRowHeader": "Typical user and environment",
        "TypicalUseDescAddressCapture": "Customer on a website.<br>Also for casual users on a self-service intranet application.",
        "TypicalUseDescVerification": "Customer on a website.<br>Often used in countries where users prefer to verify a complete address.",
        
        "ProcessRowHeader": "How it fits into existing address entry process",
        "ProcessDescAddressCapture": "Requires additional pages to guide the user through the capture process.",
        "ProcessDescVerification": "Sits behind existing address entry form. Interaction with user only required when the address cannot be verified as correct.",
        
        "EntryRowHeader": "Entering address information",
        "EntryDescAddressCapture": "A separate page prompts the user for key elements of the address. If the post code is not known, alterative prompts are displayed.",
        "EntryDescVerification": "Entered into existing address entry form.",
        
        "DataSelectionRowHeader": "Data selection",
        "DataSelectionDescAddressCapture": "A separate page prompts the user to pick from a list of countries. This step can be omitted if only a single country is available.",
        "DataSelectionDescVerification": "Provided by the main address entry form. If this is not defined, a default country is assumed.",
        
        "UsePicklistRowHeader": "Use of picklists to display multiple matches",
        "UsePicklistDescAddressCapture": "A simple picklist of all correct addresses, each displaying a complete address.",
        "UsePicklistDescVerification": "A simple picklist of all correct addresses, each displaying a complete address.<br>If minimal user interaction is required then this stage can be omitted.",
        
        "RefinePicklistRowHeader": "Ability to enter text to refine picklists",
        "RefinePicklistDesc": "The user simply selects the required address.",
        
        "MatchScoreRowHeader": "Display of percentage match scores",
        "MatchScoreDesc": "Scores are not shown, in order to simplify interaction for the user.",
        
        "PostalCodeWarningsRowHeader": "Postal code recode and bordering locality warning messages",
        "PostalCodeWarningsDesc": "Warnings are not shown, in order to simplify interaction for the user.",
        
        "PicklistHistoryRowHeader": "Display of picklist history",
        "PicklistHistoryDesc": "Multiple levels of picklist are not supported.",
        
        "InformationalPromptsRowHeader": "Use of informational prompts (in picklist)",
        "InformationalPromptsDesc": "Informational prompts are not shown, in order to simplify interaction for the user.",
        
        "MaxMatchesRowHeader": "Maximum number of displayed matches",
        "MaxMatchesDesc": "Picklist displays a maximum of 250 matches.",
        
        "AutoStepRowHeader": "Automatic step-in to a single exact match (100%)",
        "AutoStepDescAddressCapture": "A picklist is not displayed if a single exact match occurs.",
        "AutoStepDescVerification": "This is not applicable as picklists are only shown if the address cannot be verified.",
        
        "StepBackWarningRowHeader": "Warning to step-back to view close matches (after an automatic step-in)",
        "StepBackWarningDescAddressCapture": "This is to simplify the interaction for the user.",
        "StepBackWarningDescVerification": "Automatic step-ins do not occur.",
                
        "UnrecognisedPremiseRowHeader": "Entry of unrecognised premise or sub-premise information",
        "UnrecognisedPremiseDescAddressCapture": "The user selects a suitable address and any changes on the address confirmation page.",
        "UnrecognisedPremiseDescVerification": "All information is retained unless overridden by the user.",
                
        "AddressConfirmationRowHeader": "Address confirmation page",
        "AddressConfirmationDescAddressCapture": "If the address is passed through to an alternative page which allows address modification, this can be omitted.",
        "AddressConfirmationDescVerification": "Interaction with the user is not required for addresses that are verified as correct.",
        
        "AdvanceSearchRowHeader": "Use of advanced search functionality",
        "AdvanceSearchDescAddressCapture": "Wildcard characters (* and ?) are supported but not generally used.",
        "AdvanceSearchDescVerification": "The Verification engine is designed to take a complete address.",
        
        "ReconfigurablePromptSetRowHeader": "Re-configurable prompt sets",
        "ReconfigurablePromptSetDesc": "The format of the input address can be configured or constrained as required. See the Implementation Guide for more details.",
        
        "NamesDataRowHeader": "Support for names data",
        "NamesDataDesc": "Configured and returned as part of the formatted address.<br>Only available when using Names data.",
        
        "BusinessDataRowHeader": "Support for business data",
        "BusinessDataDesc": "Configured and returned as part of the formatted address.<br>Only available when using Business data.",
        
        "DataplusRowHeader": "Support for DataPlus sets",
        "DataplusDesc": "Configured and returned as part of the formatted address.",
        
        "ChooseSearchType": "Choose a search type"
    },
    "COMMON" : {
        "QASProOnDemand" : "QAS Pro On Demand",
        "ImplementationCode" : qas.lang.PROGRAMMING_LANGUAGE + " Implementation Code",
        "HomePage" : qas.lang.PROGRAMMING_LANGUAGE + " home page",
        
        // links
        "HeaderLinkGuide": "Guide",
        "HeaderLinkHelp": "Help",
        "HeaderLinkSupport": "Support",
        "SupportLink": "http://support.qas.com/?sc=uk",
        "GuideLink": "http://support.qas.com/ImplementationCode-v1_en-uk_CSharp_guide",
        
        // Route
        "Defined" : "Defined",
        "Okay" : "Okay",
        "Failed" : "Failed",
        "PreSearchFailed" : "Pre-search failed",
        "UnsupportedCountry" : "Unsupported country",
        "TooManyMatches" : "Too many matches",
        "NoMatches" : "No matches",
        "Timeout" : "Timeout"
    },
    "ADDRESS_CAPTURE" : {
        // Common
        "Title" : "QAS Pro On Demand - Find an address",
        "AddressCapture" : "Find an address",
        "ButtonNext" : "    Next >   ",
        "ButtonBack" : "   < Back   ",
        "ButtonSearchAgain" : "Search Again",

        // Input screen - country selection
        "PleaseSelectACountryOrDatamap" : "To begin, select your country.",
        "DatamapsAvailable" : "--  Select your country --",
        "Other" : "Other",
        "TheOnDemandServerIsNotAvailable": "The On Demand server is not available",

        // Input screen - prompt
        "EnterTheAddressElementsRequestedBelow" : "Enter your address details below.",
        "IfYouDoNotKnowThePostalCode" : "Don't know your postcode? Click here",
        "PleaseEnterSomeAddressDetails" : "Please enter some address details to continue.",
        "ReturnToPostcode" : "Return to postcode search",

        // Search screen
        "SelectOneOfTheFollowingAddress" : "Select your address:",

        // Format screen
        "PleaseConfirmYourAddressBelow" : "Confirm your address.",
        "AutomaticAddressCaptureDidNotSucceed" : "Address lookup failed.",
        "PleaseSearchAgainOrEnterYourAddressBelow" : "Please search again, or enter your address below.",
        "AutomaticAddressCaptureIsNotAvailable": "Address lookup unavailable.",
        "PleaseEnterYourAddressBelow" : "Please enter your address below.",
        "ButtonAccept" : "   Accept Address   ",
        "DPVValidated" : "Address has been DPV validated",
        "Warning_DPVNotValidated" : "WARNING - Address has not been DPV validated",
        "DPVValidatedButSecondaryNumberIncorrectOrMissing" : "Address has been DPV validated, but secondary number is incorrect or missing",
        "Warning_DPVValidationLocked" : "WARNING - DPV validation is locked",
        "Warning_DPV_SeedAddressHit" : "WARNING - a DPV seed address has been hit",
        "ThisCombinationOfCountry_Scheme_SearchEngine_IsNotSupported_OrUserDoesNotHaveAppropriateLicense" : "This combination of country and search engine is not supported, or the user does not have an appropriate licence. [0]",
        "IntegrationInformation_SearchResultWas" : "Integrator information: search result was ",
        "Line1": "Property/Street",
        "Line2": "Locality",
        "Line3": "Line 3",
        "CityOrTown": "City or Town",
        "StateProvideOrCounty": "State, Province or County",
        "ZipOrPostCode" : "Zip or Post Code",
        "Country" : "Country",

        // Refine screen
        "PleaseEnterExactDetails" : "Please enter exact details.",
        "YourSelectionRequiresSecondaryInformation_EnterYourExactDetails" : "Your selection requires flat, suite or unit information. Please enter your exact address.",
        "YourSelectionCoversARangeOfAddresses_EnterYourExactDetails" : "Your selection covers a range of addresses. Please enter your exact address.",
        "YouEntered" : "You entered ",
        "ButThisAddressIsOutsideOfTheRange" : "but this address is outside of the valid range of addresses. ",
        "PleaseTryAgainOrClickBackAndSelectTheCorrectRange" : "Please try again or click Back and select the correct range.",

        // Final screen
        "FinalCaptureAddress" : "Final address:"
    },
        "VERIFICATION": {
		"Title": "QAS Pro On Demand - Verify an address",
		"Verification": "Verify an address",
		"EnterYourAddress": "Enter your address details",
        "MailingAddress": "Mailing Address",
        "BillingAddress": "Billing Address",
        "AlternateAddress": "Alternate Address",
		"InputAddress1": "Line 1:",
		"InputAddress2": "Line 2:",
		"InputAddress3": "Line 3:",
		"InputCity": "Town:",
		"InputState": "County:",
		"InputZip": "Postcode:",
        "InputCountry": "Country:",
        "ButtonSubmit": "   Submit    ",
        "ButtonClear": "   Clear All   ",
		"OnDemandServerNoAvaialbe": "The Pro On Demand server is currently unavailable. Please try again later.",
        "PromptInteractionRequiredHeader": "<b>We think that your address may be incorrect or incomplete.</b><br />To proceed, please choose one of the options below.",
        "PromptInteractionRequiredPrompt": "We recommend:",
        "PromptInteractionRequiredButton": "Use suggested address",
		"PromptPremisesPartialHeader": "<b>Your flat, suite or unit may be missing or incorrect</b><br />To proceed, please enter your flat, suite or unit, or use the address you entered.",
		"PromptPremisesPartialPrompt": "Confirm your flat, suite or unit number:",
		"PromptPremisesPartialButton": "Confirm building number",
        "PromptPremisesPartialShowPicklist": "Show all potential matches",
		"PromptPremisesPartialInvalidRange": "Flat, suite or unit number is not within the valid range of addresses",
		"PromptStreetPartialHeader": "<b>Sorry, we don't recognize your house or building number.</b><br />To proceed, please choose from one of the options below.",
		"PromptStreetPartialPrompt": "Confirm your building number:",
        "PromptStreetPartialButton": "Confirm number",
        "PromptStreetPartialShowPicklist": "Show all potential matches",
		"PromptStreetPartialInvalidRange": "Building number is not within the valid range of addresses",
		"PromptDPVPartialHeader": "<b>Sorry, we don't recognize your house or building number.</b><br />To proceed, please choose from one of the options below.",
		"PromptDPVPartialPrompt": "Confirm your building number:",
        "PromptDPVPartialButton": "Confirm number",
		"PromptAptAppendHeader": "<b>Sorry, we don't recognize your house or building number.</b><br />To proceed, please choose from one of the options below.",
		"PromptAptAppendPrompt": "Confirm your building number:",
        "PromptAptAppendButton": "Confirm number",
		"PromptAptAppendNoApt": "Your flat, suite or unit number is missing",
        "PromptMultipleHeader": "<b>We found more than one match for your address.</b><br />To proceed, please choose one of the options below.",
        "PromptMultiplePrompt": "Our suggested matches:",
        "PromptNoneHeader": "<b>Sorry, we could not find a match for your address.</b><br />To proceed, please choose one of the options below.",
        "PromptRightSidePrompt": "You Entered:",
		"PromptRightSideEdit": "[Edit]",
        "PromptRightSideButton": "Use Address As Entered*",
        "PromptRightSideWarning": "<b>*Your address may be undeliverable</b>",
        "PromptRightSideFinishEdit": "Finish Edit",
        "PromptWaitingMessage": "Please wait, your address is being verified",
        "PromptTitle": "Verify your address details",
        "TheOnDemandServerIsNotAvailable": "The On Demand server is not available",
        "AjaxCallError": "Error with AJAX call. Check to make sure pro web is running correctly."
    }
};
