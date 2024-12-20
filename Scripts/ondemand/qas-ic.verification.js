/*********************************************************************************************************************************************************************
 **********************************************************************************************************************************************************************
 *
 *QAS Best Practices AJAX Sample Code
 *Release v5.1
 *Date: 2/7/2011
 *
 **********************************************************************************************************************************************************************
 *
 *Tested with:
 *	Proweb:
 *		v6.45
 *	Browsers:
 *		Firefox v3.6
 *		Chrome v6.0
 *		Safari v4.0
 *		Opera v10.62
 *		IE v6, v7, v8
 *
 **********************************************************************************************************************************************************************
 *
 *This code is written to be used in conjunction with QAS Pro Web and the provided Common Classes along with a language specific qas_proxy file. It is dependent
 *on both jQuery and jQueryUI.
 *
 *This code processes all addresses on a web page through the proweb engine and requests interaction from an end-user when appropriate. All cleansed
 *addresses are then returned to the proper form. All unique addresses will be processed exactly once, if there are two addresses with the same input ignoring case
 *and after extraneous spaces have been stripped they will be considered the same and processed only once. If any user interaction is needed, it will be requested
 *only once and used for both addresses.
 *
 *
 *All settings are created at the top of the code and can be changed to properly integrate into a website. QAS_Verify() is the function that should be called by
 *a website in order to initiate address verificaton. The Classes are as follows:
 *
 *Main
 *	Instantiates all objects
 *	Loops through addresses
 *	Calls function to return all results
 *	Calls pre and post validation functions
 *
 *Address
 *	Retrieves and stores addresses
 *	Determines unique addresses
 *	Builds search strings
 *	Stores cleaned addresses
 *	Returns addresses to web page
 *
 *Clean
 *	Used to clean a single address by making an AJAX call to qas_proxy
 *	Cleans/Refines/Formats addresses
 *	Stores verifylevel/cleansed result/picklist
 *
 *Business
 *	All business logic is handled here
 *	Controls interaction
 *
 *Interface
 *	creates div tags
 *	populates tags with appropriate messages
 *	displays pop up
 *	accepts user interaction
 *
 **********************************************************************************************************************************************************************
 *
 *Programmer: Jonathan Reimels
 *Date: 10/5/2010
 *
 **********************************************************************************************************************************************************************
 *Please log any internal changes to the code here with the following format(Programmer, Date, Reason for change, Change made)
 *
 *UPDATES:
 *
 *Programmer:  Zulfiqar Ahmad
 *Date: 12/15/10
 *Reason: Renaming/Reorganization for clarity and consistency with prior versions of BP
 *Change: Alphabetized countries in stripPostCode switch statement.   JS updated to match proxy name changes: dpv->dpvstatus; matchtype->verifylevel; isfull->fulladdress
 *
 *Programmer:  Jonathan Reimels
 *Date: 12/15/10
 *Reason: Messaging added for when secondary info inputed on prompt is out of range, as per older versions of BP
 *Change:  Additions to lines 156, 164, 979-986, 994-1001
 *
 *Programmer: Ng Kah Ching
 *Date 8/2/11
 *Reason: Update prior version of BP to use Implementation Code framework
 *Change: Using JSON instead of xml, datamap will be get from OnDemand ws, update logic to handle search result
 **********************************************************************************************************************************************************************
 *********************************************************************************************************************************************************************/

/*********************************************************************************************************************************************************************
 *
 *Settings
 *
 *Set all variables here to properly integrate into website
 *
 *********************************************************************************************************************************************************************/

/*global $*/
/**
 * @namespace Namespace to provide qas feature
 */
var qas = qas || {};

/**
 * @namespace Namespace to provide verification feature
 */
qas.verification = qas.verification || {};

/**
 * This is an array of string arrays, the id's for each set of address fields (excluding country fields) should be listed
 * in an individual string array. These should be listed to match with the proweb config.
 * For each cleaned result, the first item (ie Address Line 1) in the config will go into the first field in the string array
 * @type (array)
 */
qas.verification.address_field_ids = [["add1", "add2", "add3", "city", "state", "zip"], ["billadd1", "billadd2", "billadd3", "billcity", "billstate", "billzip"], ["altadd1", "altadd2", "altadd3", "altcity", "altstate", "altzip"]];

qas.verification.edit_address_ids = ["editadd1", "editadd2", "editadd3", "editcity", "editstate", "editzip"];

/**
 * Country field id's, these should be listed in the same order as the string arrays in qas.verification.address_field_ids.
 * If a layout doesn't have a country field, then enter false in for the appropriate address within the string.
 * @type (array)
 */
qas.verification.country_fields_ids = ["country", "billcountry", "altcountry"];

/**
 * This is only for Canadian addresses.
 * The proweb configuration should be setup to include LVR and Building name as one of the last lines in order to properly handle CAN apartments.
 * This variable should be set to the line number within the config that contains these fields.
 * @type (int)
 */
qas.verification.lvr = 7;

/**
 *	Regular Expression to check for LVR  
 */
qas.verification.lvr_regular_expression = /\|?\d+[A-Za-z]*\s*-\s*\d+/;

/**
 * prompt user for information to correct address when needed
 * @type (boolean)
 */
qas.verification.interaction_required = true;

//number of lines to display to user in an interaction required address, this will prevent, additional data, such as dpv indicator, or lat/long from being displayed to user
/**
 * number of lines to display to user in an interaction required address,
 * to prevent additional data to be displayed to user.
 * @type (int)
 */
qas.verification.display_lines = 6;

//Instantiate global instance for QASCapture
var capture = new qas.search.QASCapture();
capture.setEngine(qas.search.EngineType.VERIFICATION);
capture.setIsFlatten(true);

//Instantiate global instance of LanguageController
var languageController = new qas.lang.LanguageController();
/**
 *
 * @class Verification Init
 */
qas.verification.Init = function () {
	
	var error = null;
	/**
	 * Display error message with alert
	 * @type void
	 */
	var displayDatamapError = function () {
		if (null !== error) {
			if (qas.search.DISPLAY_ERROR) {
				alert(LANGUAGES.VERIFICATION.TheOnDemandServerIsNotAvailable + "\n" + error);
			}
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
		error = errorMessage;
		displayDatamapError();
	};
	/**
	 * populate the datamap and load the translated string
	 * @type void
	 */
	var loadData = function () {

		languageController.init("body");

		var datamap = new qas.ui.Datamap(capture);
		datamap.populateCountryControl($('select.countrySelector'), false, null, datamapLoadError);
		languageController.init("body");
		languageController.initComboBox($("select#lang"));
        // start page header codes: can be remove after integration
        languageController.initFlag($("#imgCountry"));
        languageController.initLink($("#guideLink"), "LANGUAGES.COMMON.GuideLink");
        languageController.initLink($("#supportLink"), "LANGUAGES.COMMON.SupportLink");
        // end page header codes
		languageController.reloadAll();
	};
	// Constructor
	loadData();
};
/**
 * to clear Verification form fields value
 * @class QAS ClearForm 
 */
qas.verification.QAS_ClearForm = function () {
	$(".verifyFields").val("");
}
/**
 * the initial function to call from the webpage in order to initiate address verification, set onclick events inside this function
 * @class QAS Verify
 */
qas.verification.QAS_Verify = function () {
	//set any onclick events and submit buttons to use pre and post validation
	var preOnclick = null;
	var postOnclick = null;
	var buttonID = "";
	var isValid = false;
	var i;
	var m = null;

	//check if the country is selected
	for (i = 0; i < qas.verification.country_fields_ids.length; i++) {
		var dropDownValue = $('#' + qas.verification.country_fields_ids[i]).val();

		if ((dropDownValue)) {
			isValid = true;
			break;
		}
	}

	//if country not selected, alert
	if (!isValid) {
		alert("Please select the country code");
		return false;
	}

	if (preOnclick === null) {
		m = new qas.verification.Main(postOnclick, buttonID);
		m.process();
	} else if (preOnclick()) {
		m = new qas.verification.Main(postOnclick, buttonID);
		m.process();
	}

	return false;
};
/*********************************************************************************************************************************************************************
 *
 *Main Class
 *
 *Public Methods
 *	process		- instantiate Interface and Clean, perform clean and sent result to Business
 *	next		- store cleaned address, move on to next address
 *	finish		- put cleaned addresses in form, submit form
 *	ajaxError	- handle any errors during the ajax call to proweb
 *
 *********************************************************************************************************************************************************************/

qas.verification.Main = function (clickEvent, buttonID) {

	//Private Variables
	var me = this;

	var m_click = clickEvent;
	var m_button = buttonID;

	//instantiate Address, and build the search string
	var add = new qas.verification.Address(qas.verification.address_field_ids, qas.verification.country_fields_ids);
	var strings = add.getSearchStrings();
	var countries = add.getSearchCountries();
	var orig = add.getOriginalAddresses();

	var inter;

	var clean;

	//keep track of address to be processed (the 'next' method controls this)
	var procIndex = 0;

	/**
	 * process an address - part 1, search the address
	 * @type void
	 */
	this.process = function () {
		//hide select boxes to handle bug with ie6, where select boxes show through the pop-up window
		$('select').css('visibility', 'hidden');

		//instantiate Interface to handle all user interaction
		inter = new qas.verification.Interface(me.returnEarly);

		//instantiate Clean, to process address
		clean = new qas.verification.Clean(strings[procIndex], countries[procIndex], me.ajaxError);

		//if string isn't false process it (false string means it is either an empty address or the country isn't in Avaliable DATA_SETS)
		if (strings[procIndex]) {
			//open the waiting widget, clean address, close waiting widget
			inter.waitOpen();
			clean.search(me.process2);
		} else {
			//if string is false use original address
			clean.result = orig[procIndex];
			me.next();
		}
	};
	/**
	 * process an address - part 2, process the result after callback from ajax call
	 * @type void
	 */
	this.process2 = function () {
		inter.waitClose();

		//instantiate a new Business object and process the cleaned result
		var business = new qas.verification.Business(me.next, clean, orig[procIndex], inter);

		//call appropriate business function to process address depending on whether end-user interaction is allowed
		if (qas.verification.interaction_required) {
			business.processResult();
		} else {
			business.noInteraction();
		}
	};
	/**
	 * this is called in order to store an address and increment procIndex so that if another address exists it will be cleaned
	 * @type void
	 */
	this.next = function () {
		//add verify level to result, commented out so DPVDefault layout can display result correctly
		//clean.result.push(clean.verifylevel);

		//store cleaned address
		add.storeCleanedAddress(clean.result);

		//increase procIndex to point to the next address
		procIndex++;

		//if another address exists, process it, otherwise move to end
		if (procIndex < strings.length) {
			me.process();
		} else {
			me.finish();
		}
	};
	/**
	 * returns cleaned addresses to webpage, calls submit functions if any exist
	 * @type void
	 */
	this.finish = function () {
		//unhide select boxes to handle bug with ie6, where select boxes show through the pop-up window
		$('select').css('visibility', '');

		inter.waitClose();
		//return cleaned addresses
		add.returnCleanAddresses();

		//if an onclick event exists, call it
		if (m_click !== null) {
			m_click();
		}

		//if a submit button exists, click it
		if (m_button !== "") {
			$('#' + m_button).attr('onclick', '');
			$('#' + m_button).parent('form').attr('onsubmit', '');
			$('#' + m_button).click();
		}
	};
	/**
	 * used for clicks on the edit button to return any addresses already cleaned
	 * @type void
	 */
	this.returnEarly = function () {
		//unhide select boxes to handle bug with ie6, where select boxes show through the pop-up window
		$('select').css('visibility', '');

		//return cleaned addresses
		add.returnCleanAddresses();
	};
	/**
	 * handle ajax errors
	 * @param json
	 * @param text
	 * @param msg
	 */
	this.ajaxError = function (json, text, msg) {

		if (text === "timeout") {
			//set match type to timeout
			clean.verifylevel = "Timeout";
		} else {
			//set match type to error
			clean.verifylevel = "Error";
		}

		//close the waiting widget
		inter.waitClose();

		//if display errors is set, then display the error
		if (qas.search.DISPLAY_ERROR) {
			alert(LANGUAGES.VERIFICATION.TheOnDemandServerIsNotAvailable + "\n" + text);
		}

		//set restult to the original address entered
		clean.result = orig[procIndex];

		//move onto next record
		me.next();
	};
};
//End Main Class

/*********************************************************************************************************************************************************************
 *
 *Address Class
 *
 *Public Methods
 *	getSearchStrings		- returns an array of strings ready to be sent to qas, a value of false means the address should not be processed
 *	getSearchCountries		- returns an array of countries corresponding to the search strings
 *	getOriginalAddresses	- returns an array of original addresses corresponding to the search strings
 *	storeCleanedAddress		- stores a cleaned address
 *	returnCleanAddresses	- returns cleaned addresses to the webpage
 *
 *********************************************************************************************************************************************************************/

qas.verification.Address = function (addressIds, countryIds) {
	
	/**************************PRIVATE**************************/
	var ids = addressIds;
	var cIds = countryIds;
	var addresses = [];
	var uniqueAddresses = [];
	var uniqueTracker = new Array(ids.length);
	var searchStrings = [];
	var searchCountries = [];
	var cleanedAddresses = [];
	var i;
	var j;

	//retrieve address values from forms and return array
	var getAddresses = function () {
		//loop through forms
		for (i = 0; i < ids.length; i++) {
			//a variable to temporarily store an address form
			var tempAddress = [];

			//loop through fields in form
			for (j = 0; j < ids[i].length; j++) {
				//get data in address field
				var fieldValue = $('#' + ids[i][j]).val();

				//if this field is undefined and display errors is on, display an error, otherwise this will be handled later
				if (fieldValue === undefined) {
					if (qas.search.DISPLAY_ERROR) {
						alert("ID '" + ids[i][j] + "' is undefined");
					}
				} else {
					//trim whitespace
					fieldValue = fieldValue.replace(/^\s+|\s+$/g, "");
				}

				//push the value into the temporary variable
				tempAddress.push(fieldValue);
			}

			//get the country from the form
			var c3 = $('#' + cIds[i]).val();

			//push country into the temporary variable
			tempAddress.push(c3);

			//push temporary address into array of addresses
			addresses.push(tempAddress);
		}
	};
	
	//determine which forms contain unique addresses
	var getUnique = function () {
		var isUnique = true;
		var j = 0;

		//loop through addresses
		for (i = 0; i < addresses.length; i++) {
			//assume address is unique, point uniqueTracker to where address will be added in uniqueAddresses, and set isUnique to true
			uniqueTracker[i] = uniqueAddresses.length;
			isUnique = true;
			j = 0;

			//loop through unique addresses until the current address either matches a unique
			//address or no more unique addresses are left, in which case the address is unique
			//and is added to the unique address list - if this is the first address it will
			//be unique by default
			while (isUnique && (j < uniqueAddresses.length)) {
				if (addresses[i].toString().toLowerCase() === uniqueAddresses[j].toString().toLowerCase()) {
					isUnique = false;
					uniqueTracker[i] = j;
				}
				j++;
			}

			if (isUnique) {
				uniqueAddresses.push(addresses[i]);
			}
		}
	};
	
	//check if an address should be cleaned
	var cleanCheck = function (address, country) {
		var addNotEmpty = false;
		var j = 0;

		//if an address is empty or has an undefined field, then false will be returned
		while (j < address.length) {
			if (address[j] !== "") {
				addNotEmpty = true;
			}

			if (address[j] === undefined) {
				return false;
			}
			j++;
		}

		//if the country is not in the list, return false
		if (addNotEmpty) {
			addNotEmpty = capture.checkIsAvailableCountry(country);

			if (!addNotEmpty) {
				if (window.console) {
					console.log("The country is not in available datamap, by default accept them");
				}
			}
		}
		return addNotEmpty;
	};
	
	//build the SearchString array from the unique addresses
	var buildSearchStrings = function () {
		for (i = 0; i < uniqueAddresses.length; i++) {
			searchCountries.push(uniqueAddresses[i].pop());

			if (cleanCheck(uniqueAddresses[i], searchCountries[i])) {
				searchStrings.push(uniqueAddresses[i].join("|"));
			} else {
				searchStrings.push(false);
			}
		}
	};
	
	//return cleansed address
	var returnAddresses = function () {
		for (i = 0; i < ids.length; i++) {
			//if edit is clicked, not all addresses will have been validated, only update validated addresses in this case
			if (cleanedAddresses[uniqueTracker[i]] !== undefined) {
				for (j = 0; j < ids[i].length; j++) {
					$('#' + ids[i][j]).val(cleanedAddresses[uniqueTracker[i]][j]);
				}
			}
		}
	};
	/**************************END OF PRIVATE**************************/
	
	/**************************PUBLIC**************************/
	this.getSearchStrings = function () {
		return searchStrings;
	};
	this.getSearchCountries = function () {
		return searchCountries;
	};
	this.getOriginalAddresses = function () {
		return uniqueAddresses;
	};
	this.storeCleanedAddress = function (cleanAddress) {
		cleanedAddresses.push(cleanAddress);
	};
	this.returnCleanAddresses = function () {
		returnAddresses();
	};
	/**************************END OF PUBLIC**************************/
	//constructor
	getAddresses();
	getUnique();
	buildSearchStrings();

};
//end Address Class

/*********************************************************************************************************************************************************************
 *
 *Clean Class
 *
 *Public Properties
 *	result		- cleaned result from proweb, either a picklist, or a cleaned address
 *	verifylevel	- match type from the cleaning process
 *	dpv			- dpv information
 *	country		- country of cleaned address
 *
 *Public Methods
 *	search					- main search, to be used to process an address
 *	searchPremisesPartial	- reprocesses a premises partial address
 *	searchStreetPartial		- reprocesses a street partial address
 *	searchDPVPartial		- reprocesses an address that failed dpv
 *	formatAddress			- get a formatted address
 *	refineAddress			- refine on a picklist
 *
 *********************************************************************************************************************************************************************/

qas.verification.Clean = function (searchString, country_3, ajaxErr) {

	var me = this;
	var m_ajaxErr = ajaxErr;
	var premClean = false;
	var strClean = false;
	var partialAddress = "";
	var m_callback;

	/**************************PRIVATE**************************/

	var origSearchString = searchString;
	var i;

	//append each line from the returned xml to result
	var saveAddress = function (line) {
		me.result.push(line);
	};
	
	//build array of picklist items from the returned xml
	var savePickList = function (items) {
		////try-catch here

		var partialText = items.PartialAddress;
		var addressText = items.Text;
		var postCode = items.Postcode;
		var moniker = items.Moniker;
		var fulladdress = items.IsFullAddress;

		me.result.push({
			"partialText" : partialText,
			"addressText" : addressText,
			"postCode" : postCode,
			"moniker" : moniker,
			"fulladdress" : fulladdress
		});
	};
	
	//get a partial address within a picklist that is not a full address
	//this is used to append building or apt info, and research on the resulting address
	var getPartialAddress = function () {
		for (i = 0; i < me.result.length; i++) {
			if (me.result[i].fulladdress.toString().toLowerCase() === "false") {
				return me.result[i].partialText;
			}
		}
		return null;
	};
	
	//strip postcodes from strings based on country
	//used to strip the postcode out of premises and street
	//partial addresses prior to address being re-submitted
	var stripPostCode = function (str) {
		switch (me.country) {
		case "AUS":
			str = str.replace(/\d{4}$/, "");
			break;
		case "DEU":
			str = str.replace(/\d{5}-\d{5}$/, "");
			break;
		case "DNK":
			str = str.replace(/\s\d{4}\s/, " ");
			break;
		case "FRA":
			str = str.replace(/\s\d{5}\s/, " ");
			break;
		case "GBR":
			str = str.replace(/\w{1,2}\d{1,2}\w?\s\d\w{2}$/, "");
			break;
		case "LUX":
			str = str.replace(/\s\d{4}\s/, "");
			break;
		case "NLD":
			str = str.replace(/\s\d{4}\s\w{2}\s/, " ");
			break;
		case "NZL":
			str = str.replace(/\d{4}$/, "");
			break;
		case "SGP":
			str = str.replace(/\d{6}$/, "");
			break;
		case "USA":
			str = str.replace(/-\d{4}$/, "");
			break;
		}

		return str;
	};
	
	//process result from ajax call
	var saveResult = function (json) {

		me.verifylevel = json.VerifyLevel;
		me.dpv = "";
		var i;

		if (json.Address) {
			me.dpv = json.Address.DPVStatus;
		} else {
			me.dpv = json.DPVStatus;
		}

		//if a premisesPartial is searched on and a premisesPartial is returned,
		//keep old result, so as not to retain the incorectly entered premise info
		if (premClean && (me.verifylevel === "PremisesPartial")) {
			premClean = false;
		} else if (strClean && (me.verifylevel === "StreetPartial")) {
			strClean = false;
		} else {
			//re-initialize this.result
			me.result = [];
			premClean = false;
			strClean = false;

			//save each line of the address if result is 'Verified' or 'InteractionRequired' || (me.dpv == 0)
			if ((me.verifylevel === "Verified") || (me.verifylevel === "InteractionRequired") || (me.verifylevel === "VerifiedStreet") || (me.verifylevel === "VerifiedPlace") || (me.dpv)) {

				var addressLine = [];
				
				if (json.Address) {
					addressLine = json.Address.AddressLines;
				} else if (json.AddressLines) {
					addressLine = json.AddressLines;
				} else if (json.Picklist.Items) {
					if (json.Picklist.Items.length > 1) {
						me.verifylevel = "PremisesPartial";
						for (i = 0; i < json.Picklist.Items.length; i++) {
							savePickList(json.Picklist.Items[i]);
						}
						partialAddress = getPartialAddress();
						if (partialAddress === null) {
							me.verifylevel = "Multiple";
						}
					} else {
						me.result.push(json.Picklist.Items[0].Text);
					}
				}
				for (i = 0; i < addressLine.length; i++) {
					saveAddress(addressLine[i].Line);
				}
			} else { //otherwise save each picklist item

				if (typeof json.Picklist === 'undefined') {
					for (i = 0; i < json.Items.length; i++) {
						savePickList(json.Items[i]);
					}
					me.verifylevel = "PremisesPartial";
				} else if (typeof json.Picklist.Items !== 'undefined' && json.Picklist.Items !== null) {
					for (i = 0; i < json.Picklist.Items.length; i++) {
						savePickList(json.Picklist.Items[i]);
					}
				}

				if ((me.verifylevel === "PremisesPartial") || (me.verifylevel === "StreetPartial")) {
					partialAddress = getPartialAddress();
					if (partialAddress === null) {
						me.verifylevel = "Multiple";
					}
				}
			}
		}
		m_callback();
	};
	
	//build up ajax parameters for verification search, and call ajax search
	var doSearch = function (address, c3, ajaxError) {

		capture.setSuccessCallback(function (json) {
			saveResult(json);
		});
		capture.setErrorCallback(function (json, text, msg) {
			ajaxError(json, text, msg);
		});
		capture.setCountryId(c3);

		capture.search(address);
	};
	//build up ajax parameters for format, and call ajax
	var doFormat = function (moniker, ajaxError) {

		capture.setSuccessCallback(function (json) {
			saveResult(json);
		});
		capture.setErrorCallback(function (json, text, msg) {
			ajaxError(json, text, msg);
		});
		capture.getFormattedAddress(moniker);
	};
	//build up ajax parameters for refine, and call ajax
	var doRefine = function (moniker, ajaxError) {

		capture.setSuccessCallback(function (json) {
			saveResult(json);
		});
		capture.setErrorCallback(function (json, text, msg) {
			ajaxError(json, text, msg);
		});
		capture.refine(moniker, "");
	};
	
	/**************************END OF PRIVATE**************************/
	
	/**************************PUBLIC**************************/

	this.result = [];
	this.verifylevel = "";
	this.dpv = "";

	this.country = country_3;

	this.search = function (callback) {
		m_callback = callback;
		doSearch(origSearchString, me.country, m_ajaxErr);
	};
	this.searchPremisesPartial = function (aptNo, callback) {
		m_callback = callback;
		premClean = true;
		//strip the +4 from a partial address and append the apt to the end of the first line
		var noPost = stripPostCode(partialAddress);
		var aptAddress = noPost.replace(/,/, " # " + aptNo + ",");

		//process address
		doSearch(aptAddress, me.country, m_ajaxErr);
	};
	this.searchStreetPartial = function (buildingNo, callback) {
		m_callback = callback;
		strClean = true;
		//strip the +4 from a partial address and append the building number to the start of the first line
		var noPost = stripPostCode(partialAddress);
		var buildAddress = buildingNo + " " + noPost;

		//process address
		doSearch(buildAddress, me.country, m_ajaxErr);
	};
	this.searchDPVPartial = function (buildingNo, callback) {
		m_callback = callback;

		//replace old building number with new building number to original address
		var wholeAddress = me.result.join("|");
		wholeAddress = wholeAddress.replace(/\|?\d+\w*\s/, "|" + buildingNo + " ");

		//process address
		doSearch(wholeAddress, me.country, m_ajaxErr);
	};
	this.formatAddress = function (moniker, callback) {
		m_callback = callback;

		//format on the moniker
		doFormat(moniker, m_ajaxErr);
	};
	this.refineAddress = function (moniker, callback) {
		m_callback = callback;
		//refine on the moniker
		doRefine(moniker, m_ajaxErr);
	};
	this.editSearch = function (editAdd, callback) {
		m_callback = callback;
		doSearch(editAdd, me.country, m_ajaxErr);
	};
	/**************************END OF PUBLIC**************************/
};
//end Clean Class

/*********************************************************************************************************************************************************************
 *
 *Business Class
 *
 *The public methods of this class are used to process a cleansed address, prompt for interaction if necessary, handle interaction, pass address back to main
 *
 *********************************************************************************************************************************************************************/

qas.verification.Business = function (callback, clean, orig, inter) {
	var me = this;

	var m_callback = callback;
	var m_clean = clean;
	var m_orig = orig;
	var m_inter = inter;

	//used for double street partials and double premise partials
	var previousMatch = "";
	var count = 0;

	//handle addresses with no end-user interaction
	this.noInteraction = function () {
		if ((m_clean.verifylevel === "Verified") || (m_clean.verifylevel === "InteractionRequired")) {
			m_callback();
		} else {
			me.useOriginal();
		}
	};
	
	this.acceptInter = function () {
		//accept interaction address
		m_callback();
	};
	this.acceptMoniker = function (moniker) {
		//get formatted address associated with moniker and accept it
		m_clean.formatAddress(moniker, m_callback);
	};
	this.refineApt = function () {
		//clean a premisespartial address and process it
		var aptNo = $('#QAS_RefineText').val();
		m_clean.searchPremisesPartial(aptNo, me.processResult);
	};
	this.refineBuild = function () {
		//clean a streetpartial address and process it
		var buildNo = $('#QAS_RefineText').val();
		m_clean.searchStreetPartial(buildNo, me.processResult);
	};
	this.refineDPV = function () {
		//clean an address that failed dpv and process it
		var buildNo = $('#QAS_RefineText').val();
		m_clean.searchDPVPartial(buildNo, me.processResult);
	};
	this.appendApt = function () {
		//append apt to address and accept it
		var aptNo = $('#QAS_RefineText').val();

		var aptIndex = 0;
		var aptLine = false;

		//find address line one and add apt to it
		while ((!aptLine) && (aptIndex < m_clean.result.length)) {
			if (m_clean.result[aptIndex].search(/^\d+\s/) !== -1) {
				aptLine = true;
				m_clean.result[aptIndex] = aptNo + "-" + m_clean.result[aptIndex];
			}
			aptIndex++;
		}
		m_callback();
	};
	this.refineMult = function (moniker) {
		//refine on multiple address and process the result
		m_clean.refineAddress(moniker, me.processResult);
	};
	this.useOriginal = function () {
		//accept orignally entered address
		m_clean.result = m_orig;
		m_callback();
	};
	var aptCheck = function (lvrLine) {
		var isApt = "";

		//check if address should have apt
		isApt = m_clean.result[lvrLine];

		//if address should have apt, check if it already does have an apt
		if (isApt) {
			//search on wholeaddress as address line 1 is unknown
			var wholeAddress = m_clean.result.join("|");
			if (wholeAddress.search(qas.verification.lvr_regular_expression) !== -1) {
				return true;
			} else {
				return false;
			}
		} else {
			return true;
		}
	};
	this.editSearch = function (editAdd) {
		//search the editted address
		previousMatch = "";
		count = 0;
		m_clean.editSearch(editAdd, me.processResult);
	};
	
	this.processResult = function () {
		count++;

		//handle address based on verifylevel
		switch (m_clean.verifylevel) {
		case "Verified":
			//if address is USA, then check DPV status
			if (m_clean.country === "USA") {
				//if dpv is not confirmed or dpv seed hit, prompt for Building Number
				if (((clean.dpv === "DPVNotConfirmed") || (clean.dpv === "DPVSeedHit")) && count <= 1) {
					m_inter.setDPVPartial(m_orig, me.refineDPV, me.useOriginal, me.editSearch);
					m_inter.display();
				} else if (clean.dpv === "DPVConfirmedMissingSec") {
					//if dpv is missing secondary, treat address as an Interactino Required
					m_inter.setInterReq(m_clean.result, m_orig, me.acceptInter, me.useOriginal, me.editSearch);
					m_inter.display();
				} else { //otherwise, dpv was passed or not set. Accept the address
					m_callback();
				}
			} else if (m_clean.country === "CAN") { //if address is Canadian, check to see if there should be an apartment
				//if there should be an apt and the address currently doesn't have one, prompt for an apt
				if (!aptCheck(qas.verification.lvr - 1)) {
					m_inter.setAptAppend(m_orig, me.appendApt, m_callback, me.useOriginal, me.editSearch);
					m_inter.display();
				} else { //otherwise, apartment was already entered, or address doesn't need an apt
					m_callback();
				}
			} else { //all other countries, accept verified address
				m_callback();
			}
			break;

		case "InteractionRequired":

			//if there should be an apt and the address currently doesn't have one, prompt for an apt
			if ((m_clean.country === "CAN") && (!aptCheck(qas.verification.lvr - 1))) {
				m_inter.setAptAppend(m_orig, me.appendApt, m_callback, me.useOriginal, me.editSearch);
				m_inter.display();
			} else if (count > 1) { //if interaction has already happened and resulting address is an interaction required, accept the address without further interaction
				m_callback();
			} else { //otherwise display interaction required dialog
				m_inter.setInterReq(m_clean.result, m_orig, me.acceptInter, me.useOriginal, me.editSearch);
				m_inter.display();
			}
			break;

		case "VerifiedPlace":
			//if there should be an apt and the address currently doesn't have one, prompt for an apt
			if ((m_clean.country === "CAN") && (!aptCheck(qas.verification.lvr - 1))) {
				m_inter.setAptAppend(m_orig, me.appendApt, m_callback, me.useOriginal, me.editSearch);
				m_inter.display();
			} else if (count > 1) { //if interaction has already happened and resulting address is an interaction required, accept the address without further interaction
				m_callback();
			} else { //otherwise display interaction required dialog
				m_inter.setInterReq(m_clean.result, m_orig, me.acceptInter, me.useOriginal, me.editSearch);
				m_inter.display();
			}
			break;

		case "VerifiedStreet":
			//if there should be an apt and the address currently doesn't have one, prompt for an apt
			if ((m_clean.country === "CAN") && (!aptCheck(qas.verification.lvr - 1))) {
				m_inter.setAptAppend(m_orig, me.appendApt, m_callback, me.useOriginal, me.editSearch);
				m_inter.display();
			} else if (count > 1) { //if interaction has already happened and resulting address is an interaction required, accept the address without further interaction
				m_callback();
			} else { //otherwise display interaction required dialog
				m_inter.setInterReq(m_clean.result, m_orig, me.acceptInter, me.useOriginal, me.editSearch);
				m_inter.display();
			}
			break;

		case "PremisesPartial":

			//display premises partial dialog
			m_inter.setPremisesPartial(m_clean.result, m_orig, me.refineApt, me.acceptMoniker, me.useOriginal, me.editSearch);
			m_inter.display();

			//if previous address was a PremisesPartial, inform user that invalid range was entered
			if (previousMatch === "PremisesPartial") {
				alert(LANGUAGES.VERIFICATION.PromptPremisesPartialInvalidRange);
			}

			//set previous match type
			previousMatch = "PremisesPartial";
			break;

		case "StreetPartial":

			//display street partial dialog
			m_inter.setStreetPartial(m_clean.result, m_orig, me.refineBuild, me.acceptMoniker, me.useOriginal, me.editSearch);
			m_inter.display();

			//if previous address was a StreetPartial, inform user that invalid range was entered
			if (previousMatch === "StreetPartial") {
				alert(LANGUAGES.VERIFICATION.PromptStreetPartialInvalidRange);
			}

			//set previous match type
			previousMatch = "StreetPartial";
			break;

		case "Multiple":
			//display multiple dialog
			m_inter.setMultiple(m_clean.result, m_orig, me.acceptMoniker, me.refineMult, me.useOriginal, me.editSearch);
			m_inter.display();
			break;

		case "None":
			//display none dialog
			m_inter.setNone(m_orig, me.useOriginal, me.editSearch);
			m_inter.display();
			break;
		}
	};
};
//end Business Class

/*********************************************************************************************************************************************************************
 *
 *Interface Class
 *
 *	Display dialog to user
 *
 *********************************************************************************************************************************************************************/

qas.verification.Interface = function (editCall) {
	
	/**************************PRIVATE**************************/
	var me = this;
	var m_editCall = editCall;
	var m_pickList;
	var m_orig;
	var m_pickHtml = "";
	var i = 0;

	//create a picklist
	var buildPick = function () {
		//reinitialize
		m_pickHtml = "";

		for (i = 0; i < m_pickList.length; i++) {
			if (m_pickList[i].fulladdress.toString().toLowerCase() === "true") {
				m_pickHtml += "<tr><td NOWRAP><a href='#' class='QAS_StepIn' moniker='" + m_pickList[i].moniker + "'>" + m_pickList[i].addressText + "</a></td><td NOWRAP><a href='#' class='QAS_StepIn' moniker='" + m_pickList[i].moniker + "'>" + m_pickList[i].postCode + "</a></td></tr>";
			} else {
				m_pickHtml += "<tr><td NOWRAP>" + m_pickList[i].addressText + "</td><td NOWRAP>" + m_pickList[i].postCode + "</td></tr>";
			}
		}
	};
	//create a picklist for multiple address, all items must be clickable
	var buildMultPick = function () {
		//reinitialize
		m_pickHtml = "";

		for (i = 0; i < m_pickList.length; i++) {
			if (m_pickList[i].fulladdress.toString().toLowerCase() === "true") {
				m_pickHtml += "<tr><td NOWRAP><a href='#' class='QAS_StepIn' moniker='" + m_pickList[i].moniker + "'>" + m_pickList[i].addressText + "</a></td><td NOWRAP><a href='#' class='QAS_StepIn' moniker='" + m_pickList[i].moniker + "'>" + m_pickList[i].postCode + "</a></td></tr>";
			} else {
				m_pickHtml += "<tr><td NOWRAP><a href='#' class='QAS_Refine' moniker='" + m_pickList[i].moniker + "'>" + m_pickList[i].addressText + "</a></td><td NOWRAP><a href='#' class='QAS_Refine' moniker='" + m_pickList[i].moniker + "'>" + m_pickList[i].postCode + "</a></td></tr>";
			}
		}
	};
	//build display of original address and button to click
	var buildRightSide = function (origCallback, editCallback) {
		var origHtml = "";
		var editHtml = "";

		for (i = 0; i < m_orig.length; i++) {
			origHtml += "<tr><td>" + m_orig[i] + "</td></tr>";
		}

		$(".QAS_RightDetails").html("<div class='QAS_RightSidePrompt'>" + "<div class='QAS_RightSidePromptText'>" + "<span class='translation'>" + LANGUAGES.VERIFICATION.PromptRightSidePrompt + "</span>" +  //"&nbsp;[<a href='#' id='QAS_Edit'>" + qas.verification.qas_prompts.RightSide.edit + "</a>]"	+
			"<span class='QAS_EditLink'><a href='#' id='QAS_Edit'>" + LANGUAGES.VERIFICATION.PromptRightSideEdit + "</a></span>" + "</div>" +
			//"<br />"	+
			"<input type='button' id='QAS_AcceptOriginal' class='translate' value='" + LANGUAGES.VERIFICATION.PromptRightSideButton + "' />" + "</div>" +
			//"<br />"	+
			"<div class='QAS_OriginalMessage'>" + "<table>" + origHtml + "</table>" + "</div>" + "<div class='QAS_EditMessage'>" + "</div>" +
			//"<br />"	+
			"<div class='QAS_DeliverableWarning'>" + "<span class='translation'>" + LANGUAGES.VERIFICATION.PromptRightSideWarning + "</span>" + "</div>");

		$(".QAS_EditMessage").html("");
		$(".QAS_OriginalMessage").show();

		$('#QAS_AcceptOriginal').button();

		$('.QAS_RightDetails').css('float', 'right');
		
		//assign onclick for accepting original address
		$('#QAS_AcceptOriginal').click(function () {
			$('#QAS_Dialog').dialog('close');
			origCallback();
		});
		//assign onclick for edit button
		$('#QAS_Edit').click(function () {

			for (i = 0; i < m_orig.length; i++) {
				editHtml += "<tr><td>" + "<input id='" + qas.verification.edit_address_ids[i] + "' type='text'" + " value='" + m_orig[i] + "' />" + "</td></tr>";
			}
			editHtml += "<tr><td>" + "<input type='button' id='QAS_FinishEdit' class='translate' value='" + LANGUAGES.VERIFICATION.PromptRightSideFinishEdit + "' />" + "</td></tr>";

			$(".QAS_EditMessage").html("<table>" + editHtml + "</table>");

			$('#QAS_Edit').hide();
			$('#QAS_FinishEdit').button();
			$(".QAS_OriginalMessage").html("");
			$(".QAS_EditMessage").show();
			$('#QAS_AcceptOriginal').unbind();
			
			$('#QAS_AcceptOriginal').click(function () {
				for (i = 0; i < qas.verification.edit_address_ids.length; i++) {
					m_orig[i] = $('#' + qas.verification.edit_address_ids[i]).val();
				}
				$('#QAS_Dialog').dialog('close');
				origCallback();
			});
			
			//asign onclick for finish edit button
			$('#QAS_FinishEdit').click(function () {
				var editAdd = "";
				for (i = 0; i < qas.verification.edit_address_ids.length; i++) {
					editAdd += $('#' + qas.verification.edit_address_ids[i]).val() + "|";
					m_orig[i] = $('#' + qas.verification.edit_address_ids[i]).val();
				}
				$('#QAS_Dialog').dialog('close');
				me.waitOpen();
				editCallback(editAdd);
			});
		});
	};
	//load div tags to page and set modal dialogs
	var load = function () {
		//remove the dialog if it already exists
		$("#QAS_Dialog").remove();
		$("#QAS_Wait").remove();

		//add div tag to page
		$(document.body).append("<div id='QAS_Dialog' class='translate' title='" + LANGUAGES.VERIFICATION.PromptTitle + "'>" + "<div class='QAS_Header ui-state-highlight'></div>" + "<div class='QAS_Prompt'>" + "<div class='QAS_PromptText'></div>" + "<div class='QAS_Input'></div>" + "<div class='QAS_PromptData'></div>" + "</div>" + "<div class='QAS_RightDetails'></div>" + "<div class='QAS_Picklist'>" + "<div class='QAS_MultPick'></div>" + "<div class='QAS_ShowPick'></div>" + "<div class='QAS_Pick'></div>" + "</div>" + "</div>" + "<div id='QAS_Wait' class='translate' title = '" + LANGUAGES.VERIFICATION.PromptWaitingMessage + "'></div>");

		//add jqueryui modal dialog to div tag, for user interaction
		$("#QAS_Dialog").dialog({
			modal : true,
			//height: 450,	////causes issues with IE
			width : 650,
			autoOpen : false,
			closeOnEscape : false,
			resizable : false,
			draggable : false
		});

		//add jqueryui modal dialog to div tag, for waiting dialog
		$("#QAS_Wait").dialog({
			modal : true,
			height : 100,
			width : 200,
			autoOpen : false,
			closeOnEscape : false,
			resizable : false,
			draggable : false
		});

		//add slide toggle to show pick list
		$(".QAS_ShowPick").click(function () {
			$(".QAS_Pick").slideToggle("slow");
		});
		//re-center popup when window is resized
		$(window).resize(function () {
			$("#QAS_Dialog").dialog("option", "position", 'center');
		});
	};
	/**************************END OF PRIVATE**************************/
	
	/**************************PUBLIC**************************/
	//open waiting diaglog
	this.waitOpen = function () {
		$('#QAS_Wait').dialog('open');
		//remove close button from top right of dialog
		$('.ui-dialog-content').hide();
		$('.ui-dialog-titlebar-close').css('display', 'none');
	};
	//close waiting dialog
	this.waitClose = function () {
		$('#QAS_Wait').dialog('close');
	};
	//display interaction dialog
	this.display = function () {
		window.scroll(0, 0);

		$('#QAS_Dialog').dialog('open');

		//remove close button from top right of dialog
		$('.ui-dialog-titlebar-close').css('display', 'none');

		//remove the default focus from interaction required button(so that it is not highlighted as if mouse is hovering on it)
		$('#QAS_RefineBtn').blur();
		$('.QAS_Header').focus();
	};
	//set dialog to handle interaction required address
	this.setInterReq = function (cleaned, orig, acceptCallback, origCallback, editCallback) {
		m_orig = orig;

		var cleanedHtml = "";

		//build right side of dialog
		buildRightSide(origCallback, editCallback);

		//build cleansed address to show to end-user
		for (i = 0; i < qas.verification.display_lines; i++) {
			if (cleaned[i]) {
				cleanedHtml += "<tr><td>" + cleaned[i] + "</td></tr>";
			} else {
				cleanedHtml += "<tr><td></td></tr>";
			}
		}

		//display proper messages
		$(".QAS_Header").html("<span class='translation'>" + LANGUAGES.VERIFICATION.PromptInteractionRequiredHeader + "</span>");

        $(".QAS_Prompt").show();
        $(".QAS_PromptText").show();
		$(".QAS_PromptText").html("<span class='translation'>" + LANGUAGES.VERIFICATION.PromptInteractionRequiredPrompt + "</span>");
        $(".QAS_PromptData").show();
        $(".QAS_Input").show();
		$(".QAS_PromptData").html("<br /><br />" + "<table>" + cleanedHtml + "</table>");
		$(".QAS_Input").html("<input type='button' id='QAS_RefineBtn' class='translation' value='" + LANGUAGES.VERIFICATION.PromptInteractionRequiredButton + "' />");
		$(".QAS_MultPick").html("");
		$(".QAS_ShowPick").html("");
		$(".QAS_Pick").html("");

		$(".QAS_MultPick").hide();

		//add jqueryui button
		$('#QAS_RefineBtn').button();

		//add onclick event to the button
		$('#QAS_RefineBtn').click(function () {
			$('#QAS_Dialog').dialog('close');
			me.waitOpen();
			acceptCallback();
		});
	};
	//set dialog to handle premises partial addresses
	this.setPremisesPartial = function (pickList, orig, refineCallback, monikerCallback, origCallback, editCallback) {
		m_pickList = pickList;
		m_orig = orig;

		//build picklist to display and right side of dialog
		buildPick();
		buildRightSide(origCallback, editCallback);

		//display proper messages and picklist
		$(".QAS_Header").html("<span class='translation'>" + LANGUAGES.VERIFICATION.PromptPremisesPartialHeader + "</span>");
		$(".QAS_Prompt").show();
        $(".QAS_PromptText").show();
		$(".QAS_PromptText").html("<span class='translation'>" + LANGUAGES.VERIFICATION.PromptPremisesPartialPrompt + "</span>");
		$(".QAS_PromptData").html("");
        $(".QAS_Input").show();
		$(".QAS_Input").html("<input type='text' id='QAS_RefineText' />" + "<input type='button' id='QAS_RefineBtn' class='translation' value='" + LANGUAGES.VERIFICATION.PromptPremisesPartialButton + "' />");
		$(".QAS_MultPick").html("");
		$(".QAS_ShowPick").show();
		$(".QAS_ShowPick").html("<a href='#'>" + "<span class='translation'>" + LANGUAGES.VERIFICATION.PromptPremisesPartialShowPicklist + "</span>" + "</a>");
		$(".QAS_Pick").html("<table>" + m_pickHtml + "</table>");

		$(".QAS_MultPick").hide();

		//add jqueryui button
		$('#QAS_RefineBtn').button();

		//add onclick event to the button
		$('#QAS_RefineBtn').click(function () {
			if ($('#QAS_RefineText').val() === "") { //if no value was entered in field, display error message
				alert("No value entered");
			} else {
				$('#QAS_Dialog').dialog('close');
				me.waitOpen();
				refineCallback();
			}
		});
		//add onclick event to any full addresses in the picklist
		$('.QAS_StepIn').click(function () {
			$('#QAS_Dialog').dialog('close');
			me.waitOpen();
			var mon = $(this).attr('moniker');
			monikerCallback(mon);
		});
	};
	//set dialog to handle street partial addresses
	this.setStreetPartial = function (pickList, orig, refineCallback, monikerCallback, origCallback, editCallback) {
		m_pickList = pickList;
		m_orig = orig;

		//build picklist to display and right side of dialog
		buildPick();
		buildRightSide(origCallback, editCallback);

		//display proper messages and picklist
		$(".QAS_Header").html("<span class='translation'>" + LANGUAGES.VERIFICATION.PromptStreetPartialHeader + "</span>");
		$(".QAS_Prompt").show();
        $(".QAS_PromptText").show();
		$(".QAS_PromptText").html("<span class='translation'>" + LANGUAGES.VERIFICATION.PromptStreetPartialPrompt + "</span>");
		$(".QAS_PromptData").html("");
		$(".QAS_Input").show();
		$(".QAS_Input").html("<input type='text' id='QAS_RefineText' />" + "<input type='button' id='QAS_RefineBtn' class='translation' value='" + LANGUAGES.VERIFICATION.PromptStreetPartialButton + "' />");
		$(".QAS_MultPick").html("");
		$(".QAS_ShowPick").show();
		$(".QAS_ShowPick").html("<a href='#'>" + "<span class='translation'>" + LANGUAGES.VERIFICATION.PromptStreetPartialShowPicklist + "</span>" + "</a>");
		$(".QAS_Pick").html("<table>" + m_pickHtml + "</table>");

		$(".QAS_MultPick").hide();

		//add jqueryui button
		$('#QAS_RefineBtn').button();

		//add onclick event to the button
		$('#QAS_RefineBtn').click(function () {
			if ($('#QAS_RefineText').val() === "") { //if no value was entered in field, display error message
				alert("No value entered");
			} else {
				$('#QAS_Dialog').dialog('close');
				me.waitOpen();
				refineCallback();
			}
		});
		//add onclick event to any full addresses in the picklist
		$('.QAS_StepIn').click(function () {
			$('#QAS_Dialog').dialog('close');
			me.waitOpen();
			var mon = $(this).attr('moniker');
			monikerCallback(mon);
		});
	};
	//set dialog to handle addresses that fail dpv
	this.setDPVPartial = function (orig, refineCallback, origCallback, editCallback) {
		m_orig = orig;

		//build right side of dialog
		buildRightSide(origCallback, editCallback);

		//display proper messages
		$(".QAS_Header").html("<span class='translation'>" + LANGUAGES.VERIFICATION.PromptDPVPartialHeader + "</span>");
		$(".QAS_Prompt").show();
        $(".QAS_PromptText").show();
		$(".QAS_PromptText").html("<span class='translation'>" + LANGUAGES.VERIFICATION.PromptDPVPartialPrompt + "</span>");
		$(".QAS_PromptData").html("");
		$(".QAS_Input").show();
		$(".QAS_Input").html("<input type='text' id='QAS_RefineText' />" + "<input type='button' id='QAS_RefineBtn'  class='translation' value='" + LANGUAGES.VERIFICATION.PromptDPVPartialButton + "' />");
		$(".QAS_MultPick").html("");

		$(".QAS_MultPick").hide();

		//add jqueryui button
		$('#QAS_RefineBtn').button();

		//add onclick event to the button
		$('#QAS_RefineBtn').click(function () {
			if ($('#QAS_RefineText').val() === "") { //if no value was entered in field, display error message
				alert("No value entered");
			} else {
				$('#QAS_Dialog').dialog('close');
				me.waitOpen();
				refineCallback();
			}
		});
	};
	//set dialog to handle addresses missing apt info
	this.setAptAppend = function (orig, refineCallback, noAptCallback, origCallback, editCallback) {
		m_orig = orig;

		//build right side of dialog
		buildRightSide(origCallback, editCallback);

		//display proper messages
		$(".QAS_Header").html("<span class='translation'>" + LANGUAGES.VERIFICATION.PromptAptAppendHeader + "</span>");
		$(".QAS_Prompt").show();
        $(".QAS_PromptText").show();
		$(".QAS_PromptText").html("<span class='translation'>" + LANGUAGES.VERIFICATION.PromptAptAppendPrompt + "</span>");
		$(".QAS_PromptData").html("");
		$(".QAS_Input").show();
		$(".QAS_Input").html("<input type='text' id='QAS_RefineText' />" + "<input type='button' id='QAS_RefineBtn' class='translate' value='" + LANGUAGES.VERIFICATION.PromptAptAppendButton + "' />" + "<br />" + "<input type='button' id='QAS_NoApt' class='translate' value='" + LANGUAGES.VERIFICATION.PromptAptAppendNoApt + "' />");
		$(".QAS_MultPick").html("");

		$(".QAS_MultPick").hide();

		//add jqueryui button
		$('#QAS_RefineBtn').button();
		$('#QAS_NoApt').button();

		//add onclick event to the button
		$('#QAS_RefineBtn').click(function () {
			if ($('#QAS_RefineText').val() === "") { //if no value was entered in field, display error message
				alert("No value entered");
			} else {
				$('#QAS_Dialog').dialog('close');
				me.waitOpen();
				refineCallback();
			}
		});
		//add onclick event to button, in order to accept cleaned address as is, with no apt
		$('#QAS_NoApt').click(function () {
			$('#QAS_Dialog').dialog('close');
			me.waitOpen();
			noAptCallback();
		});
	};
	//set dialog to handle multiple addresses
	this.setMultiple = function (pickList, orig, formatCallback, refineCallback, origCallback, editCallback) {
		m_pickList = pickList;
		m_orig = orig;

		//build picklist to display and right side of dialog
		buildMultPick();
		buildRightSide(origCallback, editCallback);

		//display proper messages and picklist
		$(".QAS_Header").html("<span class='translation'>" + LANGUAGES.VERIFICATION.PromptMultipleHeader + "</span>");
		$(".QAS_Prompt").show();
        $(".QAS_PromptText").show();
		$(".QAS_PromptText").html("<span class='translation'>" + LANGUAGES.VERIFICATION.PromptMultiplePrompt + "</span>");
		$(".QAS_PromptData").html("");
		$(".QAS_Input").html("");
		$(".QAS_MultPick").html("<table>" + m_pickHtml + "</table>");
		$(".QAS_ShowPick").html("");
		$(".QAS_Pick").html("");

		$(".QAS_MultPick").show();

		//step into any full address
		$('.QAS_StepIn').click(function () {
			$('#QAS_Dialog').dialog('close');
			me.waitOpen();
			var mon = $(this).attr('moniker');
			formatCallback(mon);
		});
		//refine on non-full address
		$('.QAS_Refine').click(function () {
			$('#QAS_Dialog').dialog('close');
			me.waitOpen();
			var mon = $(this).attr('moniker');
			refineCallback(mon);
		});
	};
	//set display for none verifylevel
	this.setNone = function (orig, origCallback, editCallback) {
		m_orig = orig;

		buildRightSide(origCallback, editCallback);

		$(".QAS_Header").html("<span class='translation'>" + LANGUAGES.VERIFICATION.PromptNoneHeader + "</span>");
        $(".QAS_Prompt").hide();
        $(".QAS_Input").hide();
		$(".QAS_MultPick").html("");
		$(".QAS_ShowPick").hide();
		$(".QAS_Pick").hide();
		$('.QAS_RightDetails').css('float', 'left');

		$(".QAS_MultPick").hide();
	};
	/**************************END OF PUBLIC**************************/
	
	//constructor
	load();

};	//end Interface Class