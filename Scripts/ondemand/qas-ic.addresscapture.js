/*global LanguageController*/
/*global $*/
/*global LANGUAGES*/
/*global Datamap*/
/*global alert*/
/*global Experian*/

/**
 * @namespace Namespace to provide qas feature
 */
var qas = qas || {};
/**
 * @namespace Namespace to provide qas ui feature
 */
qas.ui = qas.ui || {};
/**
 * @namespace Namespace to provide address capture feature
 */
qas.addresscapture = qas.addresscapture || {};

qas.search.DISPLAY_ERROR = false;
/**
 * @class Enum of address capture routes
 */
qas.addresscapture.Routes = {
	UNDEFINED : 0,
	OKAY : 1,
	FAILED : 2,
	PRE_SEARCH_FAILED : 3,
	UNSUPPORTED_COUNTRY : 4,
	TOO_MANY_MATCHES : 5,
	NO_MATCHES : 6,
	TIMEOUT : 7,
	CUSTOM : 8
};
/**
 * @class Enum of address capture templates
 */
qas.addresscapture.Template = {
	INPUT : "TemplateFlatPrompt",
	SEARCH : "TemplateFlatSearch",
	REFINE : "TemplateFlatRefine",
	FORMAT : "TemplateFlatAddress",
	FINAL : "TemplateFlatFinalAddress"
};
/**
 * Default JQuery Selector for the container of address capture
 * @type {string}
 * @default ""
 */
qas.addresscapture.JQSELECTORCONTAINER = "";
/**
 * Default JQuery Selector for the datamap selector
 * @type {string}
 * @default ""
 */
qas.addresscapture.JQSELECTOR_DATAMAP = "select#QASCapture_Datamap";
/**
 * Default template extension
 * @type {string}
 * @default ".html"
 */
qas.addresscapture.TEMPLATE_EXTENSION = ".htm";
/**
 * Default template path
 * @type {string}
 * @default "js/template"
 */
qas.addresscapture.TEMPLATE_PATH = "Scripts/ondemand/template/";
/**
 * Timeout to fix IE set focus issue
 * @type {int}
 * @default 500
 */
qas.addresscapture.IE_TIMEOUT = 500;

// Instantiate global instance for language controller
var languageController = new qas.lang.LanguageController();
// Instantiate global instance for QASCapture
var capture = new qas.search.QASCapture();
// Set engine type to Singline Line
capture.setEngine(qas.search.EngineType.SINGLELINE);
// Set flatten to true
capture.setIsFlatten(true);

// Declare global instances for all controllers
var addresscapture_InputController;
var addresscapture_SearchController;
var addresscapture_FormatController;
var addresscapture_RefineController;
var addresscapture_FinalController;

// Initialize global bool to keep track of the loading state
var isLoading = false;

/**
 * Initialize function
 * @param {string} JQuery selector for the container
 * @type void
 */
qas.addresscapture.Initialize = function (jqSelectorContainer) {
	// Initialize the language controller
	languageController.init("body");
	languageController.initComboBox($("select#lang"));
    // start page header codes: can be remove after integration
    languageController.initFlag($("#imgCountry"));
    languageController.initLink($("#guideLink"), "LANGUAGES.COMMON.GuideLink");
    languageController.initLink($("#supportLink"), "LANGUAGES.COMMON.SupportLink");
	// Load once first to prevent untranslated string appear while loading the templates
	languageController.reloadAll();

	qas.addresscapture.JQSELECTORCONTAINER = jqSelectorContainer;

	// Instantiate all controllers
	addresscapture_InputController = new qas.addresscapture.InputController();
	addresscapture_SearchController = new qas.addresscapture.SearchController();
	addresscapture_RefineController = new qas.addresscapture.RefineController();
	addresscapture_FormatController = new qas.addresscapture.FormatController();
	addresscapture_FinalController = new qas.addresscapture.FinalController();

	// Download all the templates using AJAX
	addresscapture_InputController.showLoading();
	addresscapture_InputController.loadTemplate();
	addresscapture_SearchController.loadTemplate();
	addresscapture_RefineController.loadTemplate();
	addresscapture_FormatController.loadTemplate();
	addresscapture_FinalController.loadTemplate();
	addresscapture_InputController.hideLoading();

	// The first screen to be shown is input screen
	addresscapture_InputController.loadDatamap();
};
/**
 * Helper function to render template
 * @param {string} template name
 * @type void
 */
qas.renderTemplate = function (template) {
	// Empty the container
	$(qas.addresscapture.JQSELECTORCONTAINER).empty();
	// Set the HTML
	$.tmpl(template, null).appendTo(qas.addresscapture.JQSELECTORCONTAINER);
};
/**
 * Extend function
 * @param {class} sub class
 * @param {class} super class
 * @type void
 */
qas.extend = function (subClass, superClass) {
	var F = function () {
	};
	F.prototype = superClass.prototype;
	subClass.prototype = new F();
	subClass.prototype.constructor = subClass;
	subClass.superclass = superClass.prototype;
	if (superClass.prototype.constructor === Object.prototype.constructor) {
		superClass.prototype.constructor = superClass;
	}
};
/**
 * @class Abstract Controller
 */
qas.addresscapture.AbstractController = function (templateName) {
	// Assign the template name
	this.templateName = templateName;
	// Assign the template file name
	this.templateFileName = qas.addresscapture.TEMPLATE_PATH + this.templateName + qas.addresscapture.TEMPLATE_EXTENSION;
};
/**
 * Use AJAX Get to load the template file from server
 * @type void
 */
qas.addresscapture.AbstractController.prototype.loadTemplate = function () {
	var me = this;
	$.ajax({
		type : "GET",
		url : me.templateFileName,
		async : false,
		success : function (data) {
			$.template(me.templateName, data);
		}
	});
};
/**
 * Show loading
 * @type void
 */
qas.addresscapture.AbstractController.prototype.showLoading = function () {
	// Only call showLoading if current state is not loading, otherwise the alpha level will be keep decreasing
	if (!isLoading) {
		isLoading = true;
		$(qas.addresscapture.JQSELECTORCONTAINER).showLoading();
		// Workaround for z-index issue of IE 6 combo box
		if ($(qas.addresscapture.JQSELECTOR_DATAMAP).length === 1) {
			$(qas.addresscapture.JQSELECTOR_DATAMAP).hide();
		}
	}
};
/**
 * Hide loading
 * @type void
 */
qas.addresscapture.AbstractController.prototype.hideLoading = function () {
	if (isLoading) {
		isLoading = false;
		$(qas.addresscapture.JQSELECTORCONTAINER).hideLoading();
		// Workaround for z-index issue of IE 6 combo box
		if ($(qas.addresscapture.JQSELECTOR_DATAMAP).length === 1) {
			$(qas.addresscapture.JQSELECTOR_DATAMAP).show();
		}		
	}
};
/**
 * Go error page
 * @type void
 */
qas.addresscapture.AbstractController.prototype.goErrorPage = function () {
	// Set the route
	addresscapture_FormatController.setRoute(qas.addresscapture.Routes.FAILED);
	// Load the data
	addresscapture_FormatController.loadData();	
};
/**
 * Display error and go error page
 * @param {string} errorMessage
 * @type void
 */
qas.addresscapture.AbstractController.prototype.displayErrorAndGoErrorPage = function (errorMessage) {
	// Show alert
	if (qas.search.DISPLAY_ERROR) {
		alert(errorMessage);
	}
	// Set the route
	addresscapture_FormatController.setRoute(qas.addresscapture.Routes.FAILED);
	// Load the data
	addresscapture_FormatController.loadData();	
};
/**
 * Go error page with route
 * @param {qas.addresscapture.Routes} route
 * @type void
 */
qas.addresscapture.AbstractController.prototype.goErrorPageWithRoute = function (route) {
	// Set the route
	addresscapture_FormatController.setRoute(route);
	// Load the data
	addresscapture_FormatController.loadData();
};
/**
 * Go error page with route and error message
 * @param {qas.addresscapture.Routes} route
 * @param {string} errorMessage
 * @type void
 */
qas.addresscapture.AbstractController.prototype.goErrorPageWithRouteAndErrorMessage = function (route, errorMessage) {
	// Set the route
	addresscapture_FormatController.setRoute(route);
	// Set the error message
	addresscapture_FormatController.setError(errorMessage);
	// Load the data
	addresscapture_FormatController.loadData();
};
/**
 * Go format page
 * @param {string} moniker
 * @type void
 */
qas.addresscapture.AbstractController.prototype.goFormatPage = function (moniker) {
	// Set the moniker
	addresscapture_FormatController.setMoniker(moniker);
	// Set the route
	addresscapture_FormatController.setRoute(qas.addresscapture.Routes.OKAY);
	// Load the data
	addresscapture_FormatController.loadData();
};
/**
 * Go refinement page
 * @param {string} moniker
 * @type void
 */
qas.addresscapture.AbstractController.prototype.goRefinementPage = function (moniker) {
	// Set the refine moniker
	addresscapture_RefineController.setRefineMoniker(moniker);
	// Load the data
	addresscapture_RefineController.loadData();
};
/**
 * Reload Language
 * @type void
 */
qas.addresscapture.AbstractController.prototype.reloadLanguage = function () {
	languageController.reload($(qas.addresscapture.JQSELECTORCONTAINER));
};
/**
 * Set focus
 * @param {JQuery Object} JQuery Object for the input
 * @type void
 */
qas.addresscapture.AbstractController.prototype.setFocus = function (jqInput) {
	setTimeout(function () { jqInput.focus();
	    }, qas.addresscapture.IE_TIMEOUT);
};
/**
 * Display error
 * @param {String} Error message
 * @type void
 */
qas.addresscapture.AbstractController.prototype.displayError = function (errorMessage) {
	if (qas.search.DISPLAY_ERROR) {
		alert(errorMessage);
	}
};
/**
 * @class Input Controller, display country combo box and prompt set
 */
qas.addresscapture.InputController = function () {
	// Trigger base constructor
	qas.addresscapture.InputController.superclass.constructor.call(this, qas.addresscapture.Template.INPUT);

	// Hold self instance
	var me = this;

	// Data
	var promptLines = null;
	var inputLines = [];
	var promptset = qas.search.PromptsetType.OPTIMAL;
	var error = null;
	var isPreSearchSuccess = false;
	var preSearchError = "";
	var datamap = new qas.ui.Datamap(capture);
	var datamapError = false;
	var previousSelectedDatamap = null;

	// User Interface
	// HTML Element ID for input line of promptset
	var uiPromptsetName = "QASCapture_Promptset";
	// JQuery selector for input line of promptset
	var jqSelectorInputLine = "input[name=\'" + uiPromptsetName + "\']";
	// JQuery selector for alternate hyperlink of promptset
	var jqSelectorAlternate = "a#QASCapture_HyperlinkAlternate";
	// JQuery selector for promptset division
	var jqSelectorPromptsetDiv = "#QASCapture_PromptDiv";
	// JQuery selector for country combo box
	var jqSelectorCountry = qas.addresscapture.JQSELECTOR_DATAMAP;
	// JQuery selector for qas Table in prompt div
	var jqSelectorQasTable = "table#QASCapture_QASTable";

	/**
	 * Get user's input
	 * @type void
	 */
	var getUserInput = function () {
		// Get user's input
		var jqUserInput = $(jqSelectorInputLine);
		var i = 0;
		inputLines.length = 0;
		for (i = 0; i < jqUserInput.length; i++) {
			// Set into local variable
			inputLines[i] = jqUserInput[i].value;
		}
	};
	/**
	 * Check whether current promptset is alternate
	 * @type bool
	 */
	var isAlternate = function () {
		if (promptset === qas.search.PromptsetType.OPTIMAL) {
			return false;
		}
		return true;
	};
	/**
	 * Get user selected country, store in local variable
	 * Return true if user selects valid country and vice versa
	 * @type bool
	 */
	var setDatasetValue = function () {
		var selected_dataset = $(jqSelectorCountry).val();

		if (selected_dataset.length > 0) {
			return true;
		} else {
			return false;
		}
	};
	/**
	 * Update the display of hyperlink based on current promptset
	 * @type void
	 */
	var updateAlternatePromptDisplay = function () {
		var jqHyperlink = $(jqSelectorAlternate);
		if (isAlternate()) {
			jqHyperlink.text(LANGUAGES.ADDRESS_CAPTURE.ReturnToPostcode);
		} else {
			jqHyperlink.text(LANGUAGES.ADDRESS_CAPTURE.IfYouDoNotKnowThePostalCode);
		}
	};	
	/**
	 * Show the user interface for promptset
	 * @type void
	 */
	var showPromptsetTemplate = function () {
		me.hideLoading();
		qas.renderTemplate(me.templateName);

		// Populate the datamap
		datamap.populateCountryControl($(jqSelectorCountry), true, null, null);

		// Grant on change behavior to the country combo box
		setCountryComboBoxOnChangeBehavior();

		// Clear the promptset
		var allHTML = "";

		// Update dynamic contents
		var i = 0;
		for (i = 0; i < promptLines.Lines.length; i++) {
			var sValue = "";
			if (i < inputLines.length && i < promptLines.Lines.length) {
				sValue = inputLines[i];
			}
			var rawHTML = "<div>" + "<span>" + promptLines.Lines[i].Prompt + "</span>"  + "<span>" + "<input type='text' size='" + promptLines.Lines[i].SuggestedInputLength + "' value='" + sValue + "' name='" + uiPromptsetName + "' &nbsp;&nbsp;<i>(e.g." + promptLines.Lines[i].Example + ")</i>" + "</span>" + "</div>";
			allHTML += rawHTML;
		}

		var jqTable = $(jqSelectorQasTable);
		jqTable.prepend(allHTML);

		$(jqSelectorPromptsetDiv).show();
		updateAlternatePromptDisplay();

		me.reloadLanguage();

		// Set focus to the input lines
		me.setFocus($(jqSelectorInputLine)[0]);
	};
	/**
	 * Load promptset
	 * @type void
	 */
	var loadPromptset = function () {
		me.showLoading();

		capture.setPromptsetType(promptset);
		capture.setSuccessCallback(function (result) {
			promptLines = result;
			showPromptsetTemplate();
		});
		capture.setErrorCallback(function (xhr, errorMessage, optional) {
			me.hideLoading();
			me.displayErrorAndGoErrorPage(errorMessage);
			return;
		});
		capture.getPromptSet();
	};	
	/**
	 * Load canSearch
	 * @type void
	 */
	var loadCanSearch = function () {
		me.showLoading();

		capture.setSuccessCallback(function (result) {
			var canSearch = result;
			if (canSearch.IsOk) {
				isPreSearchSuccess = true;
				preSearchError = "";
				loadPromptset();
			} else {
				me.hideLoading();
				isPreSearchSuccess = false;
				preSearchError = canSearch.ErrorMessage;
				me.goErrorPageWithRouteAndErrorMessage(qas.addresscapture.Routes.PRE_SEARCH_FAILED, preSearchError);
				return;
			}
		});
		capture.setErrorCallback(function (xhr, errorMessage, optional) {
			me.hideLoading();
			error = errorMessage;
			me.displayErrorAndGoErrorPage(error);
			return;
		});
		capture.canSearch();
	};
	/**
	 * Assign on change behavior to the country combo box
	 * @type void
	 */
	var setCountryComboBoxOnChangeBehavior = function () {
		datamap.bindCountryControlToCaptureObject($(jqSelectorCountry));
		$(jqSelectorCountry).change(function () {
			// Fixed for IE focusing but firing onchange event
			if ($(jqSelectorCountry).val() === previousSelectedDatamap) {
				return;
			}
			previousSelectedDatamap = $(jqSelectorCountry).val(); 
			inputLines.length = 0;
			// Save the selected dataset and go to next screen
			if (setDatasetValue()) {
				if (capture.checkIsAvailableCountry($(jqSelectorCountry).val())) {
					// Set promptset to Optimal (Default value) upon country selection changed
					promptset = qas.search.PromptsetType.OPTIMAL;
					promptLines = null;
					inputLines = [];
					isPreSearchSuccess = false;
					preSearchError = "";
					loadCanSearch();
				} else {
					me.goErrorPageWithRouteAndErrorMessage(qas.addresscapture.Routes.PRE_SEARCH_FAILED, LANGUAGES.ADDRESS_CAPTURE.ThisCombinationOfCountry_Scheme_SearchEngine_IsNotSupported_OrUserDoesNotHaveAppropriateLicense);
					return;
				}
			} else {
				alert(LANGUAGES.ADDRESS_CAPTURE.PleaseSelectACountryOrDatamap);
			}
		});
	};	
	/**
	 * Callback when the loading of datamap is success
	 * @param {string} Result in json format
	 * @type void
	 */
	var datamapLoadSuccess = function (result) {
		me.hideLoading();
		datamapError = false;
		// Set focus to country dropdown
		$(jqSelectorCountry).focus();

		setCountryComboBoxOnChangeBehavior();
		
		// loadCanSearch automatically if selected datamap is valid datamap
		if (capture.getCountryId()) {
			loadCanSearch();
		}
	};
	/**
	 * Callback when the loading of datamap is failed
	 * @param {XmlHttpRequest} XHR object
	 * @param {string} Error message
	 * @param {string} Unknown
	 * @type void
	 */
	var datamapLoadError = function (xhr, errorMessage, optional) {
		me.hideLoading();
		datamapError = true;
		// Set focus to country dropdown
		$(jqSelectorCountry).focus();

		setCountryComboBoxOnChangeBehavior();

		me.displayError(LANGUAGES.ADDRESS_CAPTURE.TheOnDemandServerIsNotAvailable + "\n" + errorMessage);
	};
	/**
	 * Check whether the user's input is not empty when next button is pressed
	 * @type bool
	 */
	var validate = function () {
		var jqUserInput = $(jqSelectorInputLine);
		var i = 0;
		for (i = 0; i < jqUserInput.length; i++) {
			if (jqUserInput[i].value !== "") {
				return true;
			}
		}
		// Set focus to the first input line
		me.setFocus($(jqSelectorInputLine)[0]);

		// Show error message
		alert(LANGUAGES.ADDRESS_CAPTURE.PleaseEnterSomeAddressDetails);
		return false;
	};	
	/**
	 * Show the user interface for datamap
	 * @type void
	 */
	var showDatamapTemplate = function () {
		me.hideLoading();
		qas.renderTemplate(me.templateName);

		datamap.populateCountryControl($(jqSelectorCountry), true, null, null);

		// Set focus to country dropdown
		$(jqSelectorCountry).focus();

		setCountryComboBoxOnChangeBehavior();
		$(jqSelectorPromptsetDiv).hide();
		updateAlternatePromptDisplay();

		me.reloadLanguage();
	};
	/**
	 * Get user's input, concatenated with delimiter
	 * @type string
	 */
	this.getInputLines = function () {
		var sSearch = null;
		var i = 1;
		sSearch = inputLines[0];
		for (i = 1; i < inputLines.length; i++) {
			sSearch += qas.search.DEFAULT_DELIMITER;
			sSearch += inputLines[i];
		}

		return sSearch;
	};
	/**
	 * Event handler for next button pressed
	 * @type void
	 */
	this.nextButtonPressed = function () {
		// Check whether datamap is set
		if (setDatasetValue()) {
			if (null !== error) {
				me.goErrorPageWithRouteAndErrorMessage(qas.addresscapture.Routes.FAILED, error);
				return;
			}
			if (!capture.checkIsAvailableCountry($(jqSelectorCountry).val())) {
				me.goErrorPage();
				return;
			}

			if (!isPreSearchSuccess) {
				me.goErrorPageWithRouteAndErrorMessage(qas.addresscapture.Routes.PRE_SEARCH_FAILED, preSearchError);
				return;
			}

			// Validate the input is not empty
			if (validate()) {
				// Get user's input
				getUserInput();

				// Call next controller to load data
				addresscapture_SearchController.loadData();
			}
		} else {
			alert(LANGUAGES.ADDRESS_CAPTURE.PleaseSelectACountryOrDatamap);
		}
	};
	/**
	 * Event handler for alternate hyperlink pressed
	 * @type void
	 */
	this.alternatelLinkPressed = function () {
		getUserInput();
		
		if (isAlternate()) {
			promptset = qas.search.PromptsetType.OPTIMAL;		
		}
		else {
			promptset = qas.search.PromptsetType.ALTERNATE;
		}
		loadPromptset();
	};
	/**
	 * Load datamap
	 * @type void
	 */
	this.loadDatamap = function () {	
		qas.renderTemplate(me.templateName);
		$(jqSelectorPromptsetDiv).hide();
		updateAlternatePromptDisplay();
		me.reloadLanguage();
		
		me.showLoading();
		// Construct datamap
		datamap.populateCountryControl($(jqSelectorCountry), true, datamapLoadSuccess, datamapLoadError);
	};
	/**
	 * Show template
	 * @type void
	 */
	this.showTemplate = function () {
		if (capture.checkIsAvailableCountry(capture.getCountryId()) && null !== promptLines) {
			showPromptsetTemplate();
		} else if (datamapError) {
			me.loadDatamap();
		} else {
			showDatamapTemplate();
		}
	};
};
// Extend from abstract class
qas.extend(qas.addresscapture.InputController, qas.addresscapture.AbstractController);

// Search Controller
qas.addresscapture.SearchController = function () {
	// Trigger base constructor
	qas.addresscapture.SearchController.superclass.constructor.call(this, qas.addresscapture.Template.SEARCH);

	// Hold self instance
	var me = this;

	// Data
	var picklist = null;
	var picklistMoniker = "";
	var selectedItem = null;
	var selectedIndex = 0;

	// User Interface
	var jqSelectorTable = "table#QASCapture_QASTable";

	/**
	 * Clear all cache data
	 * @type void
	 */
	var clearAllData = function () {
		picklist = null;
		picklistMoniker = "";
		selectedItem = null;
		selectedIndex = 0;
	};
	/**
	 * Perform a search.
	 * @param {PicklistItem} picklist item
	 * @type bool
	 */
	var mustRefine = function (item) {
		return (item.IsIncompleteAddress || item.IsUnresolvableRange || item.IsPhantomPrimaryPoint);
	};
	/**
	 * Perform the workflow for search
	 * @type void
	 */
	var doSearchFlow = function () {
		if (picklist.IsTimeout) {
			clearAllData();
			me.goErrorPageWithRoute(qas.addresscapture.Routes.TIMEOUT);
			return;
		} else if (picklist.IsMaxMatches) {
			clearAllData();
			me.goErrorPageWithRoute(qas.addresscapture.Routes.TOO_MANY_MATCHES);
			return;
		} else if (picklist.Items === null || picklist.Total === 0) {
			clearAllData();
			me.goErrorPageWithRoute(qas.addresscapture.Routes.NO_MATCHES);
			return;
		}

		if (picklist.IsAutoformatSafe || picklist.IsAutoformatPastClose) {
			me.goFormatPage(picklist.Items[0].Moniker);
			return;
		} else if (picklist.Items !== null && picklist.Items.length === 1 && mustRefine(picklist.Items[0])) {
			me.goRefinementPage(picklist.Items[0].Moniker);
			return;
		}

		me.showTemplate();
	};
	/**
	 * Callback when refine is failed
	 * @param {XmlHttpRequest} XHR object
	 * @param {string} Error message
	 * @param {string} Unknown
	 * @type void
	 */
	var refineFailed = function (xhr, errorMessage, optional) {
		clearAllData();
		me.displayErrorAndGoErrorPage(errorMessage);
		return;
	};
	/**
	 * Callback when refine is success
	 * @param {string} Result in json format
	 * @type void
	 */
	var refineSuccess = function (result) {
		picklist = result;
		if (picklist.IsAutoStepinSafe) {
			capture.setSuccessCallback(refineSuccess);
			capture.setErrorCallback(refineFailed);
			capture.refine(picklist.Items[0].Moniker);
		} else {
			picklistMoniker = picklist.FullPicklistMoniker;
			doSearchFlow();
		}
	};
	/**
	 * Callback when search is success
	 * @param {string} Result in json format
	 * @type void
	 */
	var searchSuccess = function (result) {
		picklist = result.Picklist;

		if (picklist.IsAutoStepinSafe) {
			capture.setSuccessCallback(refineSuccess);
			capture.setErrorCallback(refineFailed);
			capture.refine(picklist.Items[0].Moniker, "", "");
		} else {
			picklistMoniker = picklist.Moniker;
			doSearchFlow();
		}
	};
	/**
	 * Callback when search is failed
	 * @param {XmlHttpRequest} XHR object
	 * @param {string} Error message
	 * @param {string} Unknown
	 * @type void
	 */
	var searchFailed = function (xhr, errorMessage, optional) {
		clearAllData();
		me.displayErrorAndGoErrorPage(errorMessage);
		return;
	};
	/**
	 * Perform the search
	 * @type void
	 */
	var doSearch = function () {
		var inputLines = addresscapture_InputController.getInputLines();

		capture.setSuccessCallback(searchSuccess);
		capture.setErrorCallback(searchFailed);
		capture.search(inputLines);
	};
	/**
	 * Event handler for next button pressed
	 * @type void
	 */
	this.nextButtonPressed = function () {
		selectedItem = picklist.Items[selectedIndex];
		if (mustRefine(selectedItem)) {
			me.goRefinementPage(selectedItem.Moniker);
			return;
		} else {
			me.goFormatPage(selectedItem.Moniker);
			return;
		}
	};
	/**
	 * Event handler for back button pressed
	 * @type void
	 */
	this.backButtonPressed = function () {
		clearAllData();
		addresscapture_InputController.showTemplate();
	};
	/**
	 * Load the data
	 * @type void
	 */
	this.loadData = function () {
		me.showLoading();
		doSearch();
	};
	/**
	 * Show template
	 * @type void
	 */
	this.showTemplate = function () {
		me.hideLoading();
		qas.renderTemplate(this.templateName);
		var allHTML = "";
		var picklistItems = picklist.Items;
		var i = 0;

		// Update dynamic contents
		for (i = 0; i < picklistItems.length; i++) {
			var rawHTML = "<div><span>" + "<a href='javascript:addresscapture_SearchController.submitCommand(" + i + ");'>" + picklistItems[i].Text + "</a></span><span>" + picklistItems[i].Postcode + "</span></div>";
			allHTML += rawHTML;
		}
		var jqTable = $(jqSelectorTable);
		jqTable.prepend(allHTML);
		me.reloadLanguage();
	};
	/**
	 * Submit when selected
	 * @param {int} index
	 * @type void
	 */
	this.submitCommand = function (iIndex) {
		selectedIndex = iIndex;
		me.nextButtonPressed();
	};
};
qas.extend(qas.addresscapture.SearchController, qas.addresscapture.AbstractController);

/**
 * @class Format Controller, display formatted address and error message
 */
qas.addresscapture.FormatController = function () {
	// Trigger base constructor
	qas.addresscapture.FormatController.superclass.constructor.call(this, qas.addresscapture.Template.FORMAT);

	// Hold self instance
	var me = this;

	// Data
	var errorInfo = "";
	var eRoute = qas.addresscapture.Routes.UNDEFINED;
	var DPVStatus = null;
	var asLabels = [];
	var asLines = [];
	var moniker = "";

	// User Interface
	// JQuery selector for literal message
	var uiAddressLineName = "QASCapture_AddressLine";
	var uiCountryName = "QASCapture_CountryName";
	var jqSelectorLiteralMessage = "span#QASCapture_LiteralMessage";
	var jqSelectorDPVMessage = "span#QASCapture_DPVMessage";
	var jqSelectorTable = "table#QASCapture_QASTable";
	var jqSelectorAddressLine = "input[name*=\'" + uiAddressLineName + "\']";

	/**
	 * Build display address array
	 * @param {array} addressLines
	 * @type void
	 */
	var buildDisplayAddressArray = function (addressLines) {
		var i = 0;
		for (i = 0; i < addressLines.length; i++) {
			asLabels[i] = addressLines[i].Label;
			asLines[i] = addressLines[i].Line;
		}
	};
	/**
	 * Perform the flow for format address
	 * @type void
	 */
	var formatAddressFlow = function () {
		if (0 === asLabels.length || 0 === asLines.length) {
			asLabels.length = 0;
			asLines.length = 0;
			asLabels = [LANGUAGES.ADDRESS_CAPTURE.Line1, LANGUAGES.ADDRESS_CAPTURE.Line2,LANGUAGES.ADDRESS_CAPTURE.CityOrTown, LANGUAGES.ADDRESS_CAPTURE.StateProvideOrCounty, LANGUAGES.ADDRESS_CAPTURE.ZipOrPostCode];
			asLines = ["", "",  "", "", ""];
		}

		me.showTemplate();
	};
	/**
	 * Format address
	 * @type void
	 */
	var formatAddress = function () {
		asLabels.length = 0;
		asLines.length = 0;

		if (!(qas.addresscapture.Routes.PRE_SEARCH_FAILED === eRoute || qas.addresscapture.Routes.FAILED === eRoute)) {

			if (qas.addresscapture.Routes.OKAY === eRoute) {
				capture.setSuccessCallback(function (result) {
					var addressLines = result.AddressLines;
					buildDisplayAddressArray(addressLines);
					DPVStatus = result.DPVStatus;
					formatAddressFlow();
				});
				capture.setErrorCallback(function (xhr, errorMessage, optional) {
					addresscapture_FormatController.setRoute(qas.addresscapture.Routes.FAILED);
					addresscapture_FormatController.setError(errorMessage);
					formatAddressFlow();
				});
				capture.getFormattedAddress(moniker);
			} else {
				capture.setSuccessCallback(function (result) {
					var addressLines = result[0].Address.AddressLines;
					buildDisplayAddressArray(addressLines);
					formatAddressFlow();
				});
				capture.setErrorCallback(function (xhr, errorMessage, optional) {
					addresscapture_FormatController.setRoute(qas.addresscapture.Routes.FAILED);
					addresscapture_FormatController.setError(errorMessage);
					formatAddressFlow();
				});
				capture.getExampleAddresses();
			}
		} else {
			formatAddressFlow();
		}
	};
	/**
	 * Set welcome message depends on the eRoute
	 * @type void
	 */
	var setWelcomeMessage = function () {
		var welcomeMessage = "";
		switch (eRoute) {
		case qas.addresscapture.Routes.OKAY:
			welcomeMessage = LANGUAGES.ADDRESS_CAPTURE.PleaseConfirmYourAddressBelow;
			break;
		case qas.addresscapture.Routes.NO_MATCHES:
		case qas.addresscapture.Routes.TIMEOUT:
		case qas.addresscapture.Routes.TOO_MANY_MATCHES:
			welcomeMessage = LANGUAGES.ADDRESS_CAPTURE.AutomaticAddressCaptureDidNotSucceed + "<br /><br />" + LANGUAGES.ADDRESS_CAPTURE.PleaseSearchAgainOrEnterYourAddressBelow;
			break;
		default:
			welcomeMessage = LANGUAGES.ADDRESS_CAPTURE.AutomaticAddressCaptureIsNotAvailable + "<br /><br />" + LANGUAGES.ADDRESS_CAPTURE.PleaseEnterYourAddressBelow;
			break;
		}
		$(jqSelectorLiteralMessage).html(welcomeMessage);
	};
	/**
	 * Set DPV Message
	 * @type void
	 */
	var setDPVMessage = function () {
		if (null !== DPVStatus) {
			var message = me.getDPVMessage();
			$(jqSelectorDPVMessage).html(message);
		}
	};
	/**
	 * Get route in string
	 * @param qas.addresscapture.Routes
	 * @type string
	 */
	var getRouteInString = function () {
		switch (eRoute) {
		case qas.addresscapture.Routes.UNDEFINED:
			return LANGUAGES.COMMON.Defined;
		case qas.addresscapture.Routes.OKAY:
			return LANGUAGES.COMMON.Okay;
		case qas.addresscapture.Routes.FAILED:
			return LANGUAGES.COMMON.Failed;
		case qas.addresscapture.Routes.PRE_SEARCH_FAILED:
			return LANGUAGES.COMMON.PreSearchFailed;
		case qas.addresscapture.Routes.UNSUPPORTED_COUNTRY:
			return LANGUAGES.COMMON.UnsupportedCountry;
		case qas.addresscapture.Routes.TOO_MANY_MATCHES:
			return LANGUAGES.COMMON.TooManyMatches;
		case qas.addresscapture.Routes.NO_MATCHES:
			return LANGUAGES.COMMON.NoMatches;
		case qas.addresscapture.Routes.TIMEOUT:
			return LANGUAGES.COMMON.Timeout;
		}
	};
	/**
	 * Clear all the data
	 * @type void
	 */
	var clearAllData = function () {
		errorInfo = "";
		eRoute = qas.addresscapture.Routes.UNDEFINED;
		DPVStatus = null;
		asLabels = [];
		asLines = [];
		moniker = "";
	};
	/**
	 * Get DPV Message
	 * @type string
	 */
	this.getDPVMessage = function () {
		var message = "";
		if (null !== DPVStatus) {
			switch (DPVStatus) {
			case qas.search.DPVStatus.CONFIRMED:
				message = LANGUAGES.ADDRESS_CAPTURE.DPVValidated;
				break;
			case qas.search.DPVStatus.NOT_CONFIRMED:
				message = LANGUAGES.ADDRESS_CAPTURE.Warning_DPVNotValidated;
				break;
			case qas.search.DPVStatus.CONFIRMED_MISSING_SEC:
				message = LANGUAGES.ADDRESS_CAPTURE.DPVValidatedButSecondaryNumberIncorrectOrMissing;
				break;
			case qas.search.DPVStatus.LOCKED:
				message = LANGUAGES.ADDRESS_CAPTURE.Warning_DPVValidationLocked;
				break;
			case qas.search.DPVStatus.SEED_HIT:
				message = LANGUAGES.ADDRESS_CAPTURE.Warning_DPV_SeedAddressHit;
				break;
			default:
				message = "";
				break;
			}
		}
		return message;
	};
	/**
	 * Get error info
	 * @type string
	 */
	this.getError = function () {
		return errorInfo;
	};
	/**
	 * Set error info
	 * @param {string} error message
	 * @type void
	 */
	this.setError = function (errorMessage) {
		errorInfo = errorMessage;
	};
	/**
	 * Set route
	 * @param {qas.addresscapture.Routes} eRoute
	 * @type void
	 */
	this.setRoute = function (route) {
		eRoute = route;
	};
	/**
	 * Set moniker
	 * @param {string} sMoniker
	 * @type void
	 */
	this.setMoniker = function (sMoniker) {
		moniker = sMoniker;
	};
	/**
	 * Get moniker
	 * @type string
	 */
	this.getMoniker = function () {
		return moniker;
	};
	/**
	 * Get address lines
	 * @type array
	 */
	this.getAddressLines = function () {
		return asLines;
	};
	/**
	 * Event handler when next button is pressed
	 * @type void
	 */
	this.nextButtonPressed = function () {
		// Get user's input
		var jqUserInput = $(jqSelectorAddressLine);
		var i = 0;
		for (i = 0; i < jqUserInput.length; i++) {
			// Set into local variable
			asLines[i] = jqUserInput[i].value;
		}

		addresscapture_FinalController.showTemplate();
	};
	/**
	 * Event handler when back button is pressed
	 * @type void
	 */
	this.backButtonPressed = function () {
		switch (eRoute) {
		case qas.addresscapture.Routes.NO_MATCHES:
		case qas.addresscapture.Routes.TIMEOUT:
		case qas.addresscapture.Routes.TOO_MANY_MATCHES:
			addresscapture_InputController.showTemplate();
			break;
		case qas.addresscapture.Routes.OKAY:
			if ("" !== addresscapture_RefineController.getRefineMoniker()) {
				addresscapture_RefineController.showTemplate();
			} else {
				addresscapture_SearchController.showTemplate();
			}
			break;
		default:
			addresscapture_InputController.showTemplate();
			break;
		}
		clearAllData();
	};
	/**
	 * Load data
	 * @type void
	 */
	this.loadData = function () {
		me.showLoading();
		formatAddress();
	};
	/**
	 * Show the user interface
	 * @type void
	 */
	this.showTemplate = function () {
		me.hideLoading();
		qas.renderTemplate(this.templateName);

		setWelcomeMessage();

		var allHTML = "";

		// Update dynamic contents
		if (null !== asLines && null !== asLabels) {
			var i = 0;
			for (i = 0; i < asLines.length; i++) {

				var rawHTML = "<div><span>" + asLabels[i] + "</span><span><input type='text' name='" + uiAddressLineName + "' class='form-control' value='" + asLines[i] + "'/></span></div>";
				allHTML += rawHTML;
			}
		}

		var countryHTML = "<div><span>" + LANGUAGES.ADDRESS_CAPTURE.Country + "</span><span><input type='text' name='" + uiCountryName + "' class='form-control' value='" + capture.getCountryName() + "' readonly class='readonly' tabindex='-1'></span></div>";
		allHTML += countryHTML;

		var jqTable = $(jqSelectorTable);
		jqTable.prepend(allHTML);

		setDPVMessage();

		me.reloadLanguage();
		
		var errorMessage = "";
		if (eRoute !== qas.addresscapture.Routes.OKAY) {
			errorMessage = LANGUAGES.ADDRESS_CAPTURE.IntegrationInformation_SearchResultWas + getRouteInString();

			if ("" !== errorInfo) {
				errorMessage += "\n" + errorInfo;
			}
			
			me.displayError(errorMessage);
		}		
	};
};
qas.extend(qas.addresscapture.FormatController, qas.addresscapture.AbstractController);

/**
 * @class Refine Controller
 */
qas.addresscapture.RefineController = function () {
	// Trigger base constructor
	qas.addresscapture.RefineController.superclass.constructor.call(this, qas.addresscapture.Template.REFINE);

	// Hold self instance
	var me = this;

	// Data
	var refinementText = "";
	var picklist = null;
	var refineMoniker = "";
	var picklistItem = null;
	var literalRefineLine = "";

	// User Interface
	// JQuery selector for literal message
	var jqSelectorTextRefinement = "input#QASCapture_TextRefinement";
	var jqSelectorLiteralRefineLine = "span#QASCapture_LiteralRefineLine";
	var jqSelectorLiteralRefineAddress = "span#QASCapture_LiteralRefineAddress";
	var jqSelectorLiteralMessage = "span#QASCapture_LiteralMessage";

	/**
	 * Clear all the data
	 * @type void
	 */
	var clearAllData = function () {
		refinementText = "";
		picklist = null;
		refineMoniker = "";
		picklistItem = null;
		literalRefineLine = "";
	};
	/**
	 * Check whether the user's input is not empty when next button is pressed
	 * @type bool
	 */
	var validate = function () {
		var jqUserInput = $(jqSelectorTextRefinement);
		if (jqUserInput.val() !== "") {
			refinementText = jqUserInput.val();
			return true;
		}

		// Set focus to the input line
		me.setFocus($(jqUserInput));
		// Show error message
		alert(LANGUAGES.ADDRESS_CAPTURE.PleaseEnterExactDetails);
		return false;
	};
	/**
	 * Init function
	 * @type void
	 */
	var init = function () {
		// Set focus to the input line
		me.setFocus($(jqSelectorTextRefinement));
		$(jqSelectorTextRefinement).select();
	};
	/**
	 * Perform the refine flow
	 * @type void
	 */
	var refineFlow = function () {
		picklistItem = picklist.Items[0];
		if ("" === literalRefineLine) {
			literalRefineLine = picklistItem.Text;
		}

		if ("" !== refinementText && !picklistItem.IsUnresolvableRange) {
			me.goFormatPage(picklistItem.Moniker);
			return;
		} else {
			me.showTemplate();
		}
	};
	/**
	 * Callback when the refine is failed
	 * @param {XmlHttpRequest} XHR object
	 * @param {string} Error message
	 * @param {string} Unknown
	 * @type void
	 */
	var refineFail = function (xhr, errorMessage, optional) {
		clearAllData();
		me.displayErrorAndGoErrorPage(errorMessage);
		return;
	};
	/**
	 * Callback when the refine is success
	 * @param {string} Result in json format
	 * @type void
	 */
	var refineSuccess = function (result) {
		picklist = result;
		if (null === picklist.Items) {
			capture.setSuccessCallback(function (json) {
				picklist = json;
				refineFlow();
			});
			capture.setErrorCallback(refineFail);
			capture.refine(refineMoniker, "", "");
			return;
		}

		refineFlow();
	};
	/**
	 * Refine address
	 * @type void
	 */
	var refineAddress = function () {
		capture.setSuccessCallback(refineSuccess);
		capture.setErrorCallback(refineFail);
		capture.refine(refineMoniker, refinementText, "");
	};
	/**
	 * Set refine moniker
	 * @param {string} moniker
	 * @type void
	 */
	this.setRefineMoniker = function (moniker) {
		refineMoniker = moniker;
	};
	/**
	 * Get refine moniker
	 * @type string
	 */	
	this.getRefineMoniker = function () {
		return refineMoniker;
	};
	/**
	 * Event handler when next button is pressed
	 * @type void
	 */
	this.nextButtonPressed = function () {
		if (validate()) {
			me.loadData();
		}
	};
	/**
	 * Event handler when back button is pressed
	 * @type void
	 */
	this.backButtonPressed = function () {
		clearAllData();
		addresscapture_SearchController.showTemplate();
	};
	/**
	 * Load data
	 * @type void
	 */
	this.loadData = function () {
		me.showLoading();
		refineAddress();
	};
	/**
	 * Show the user interface
	 * @type void
	 */
	this.showTemplate = function () {
		me.hideLoading();
		qas.renderTemplate(this.templateName);

		// Update page content
		$(jqSelectorLiteralRefineLine).html(literalRefineLine);
		$(jqSelectorLiteralRefineAddress).html(picklistItem.PartialAddress);

		var text = "";

		if ("" === refinementText) {
			// First time through - just display
			var bIsPhantom = picklistItem.IsPhantomPrimaryPoint;

			if (bIsPhantom) {
				text = LANGUAGES.ADDRESS_CAPTURE.YourSelectionRequiresSecondaryInformation_EnterYourExactDetails;
			} else {
				text = LANGUAGES.ADDRESS_CAPTURE.YourSelectionCoversARangeOfAddresses_EnterYourExactDetails;
			}
		} else if (picklistItem.IsUnresolvableRange) {
			// Redisplay with explanation
			text = LANGUAGES.ADDRESS_CAPTURE.YouEntered + "'" + refinementText + "', " + LANGUAGES.ADDRESS_CAPTURE.ButThisAddressIsOutsideOfTheRange + LANGUAGES.ADDRESS_CAPTURE.PleaseTryAgainOrClickBackAndSelectTheCorrectRange;
		}

		$(jqSelectorLiteralMessage).html(text);
		me.reloadLanguage();
		init();
	};
};
qas.extend(qas.addresscapture.RefineController, qas.addresscapture.AbstractController);

/**
 * @class Final Controller
 */
qas.addresscapture.FinalController = function () {
	qas.addresscapture.FinalController.superclass.constructor.call(this, qas.addresscapture.Template.FINAL);
	// Hold self instance
	var me = this;
	// User Interface
	var jqSelectorTableContent = "tr#QASCapture_QASTableContent";

	/**
	 * Show the user interface
	 * @type void
	 */
	this.showTemplate = function () {
		me.hideLoading();
		qas.renderTemplate(this.templateName);

		var allHTML = "";
		allHTML += "<td><h4>";
		var addressLines = addresscapture_FormatController.getAddressLines();
		console.log(addressLines);
		// Update dynamic contents
		var i = 0;
		for (i = 0; i < addressLines.length; i++) {
			var rawHTML = "<p class='formtext' id='addLine" + i + "'>" + addressLines[i] + "</p><br>";
			allHTML += rawHTML;
		}
		allHTML += "<p class='formtext' id='country'>" + capture.getCountryName() + "</p><br>";
		allHTML += "</td></h3>";
		var jqTable = $(jqSelectorTableContent);
		jqTable.html(allHTML);

		me.reloadLanguage();
	};
};
qas.extend(qas.addresscapture.FinalController, qas.addresscapture.AbstractController);
