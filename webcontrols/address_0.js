/* Reload the country selection and set the focus to the country dropdown */
function init() {
    var sDataID = $("#hdnDataID").val();
    eval('var objFieldDataID=document.FlatCountry.' + $("#hdnFieldDataID").val());
    if (sDataID != "") {
        objFieldDataID.value = sDataID;
    }
    objFieldDataID.focus();
}

/* Store the text of the DataID select control in the CountryName field */
function setCountryValue() {
    var theForm = document.forms[0];
    eval('var objFieldDataID=theForm.' + $("#hdnFieldDataID").val());
    eval('var objCountryName=theForm.' + $("#hdnCountryName").val());
    var iSelected = objFieldDataID.selectedIndex;
    objCountryName.value = objFieldDataID.options[iSelected].text;
}