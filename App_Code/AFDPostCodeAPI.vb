Public Class AFDPostCodeAPI

    ' Structure for setting and retrieving data fields
    Public Structure afdAddressData
        Public Lookup As String
        Public Name As String
        Public Organisation As String
        Public Prop As String
        Public Street As String
        Public Locality As String
        Public Town As String
        Public PostalCounty As String
        Public AbbreviatedPostalCounty As String
        Public OptionalCounty As String
        Public AbbreviatedOptionalCounty As String
        Public TraditionalCounty As String
        Public AdministrativeCounty As String
        Public Postcode As String
        Public DPS As String
        Public PostcodeFrom As String
        Public PostcodeType As String
        Public MailsortCode As String
        Public Phone As String
        Public GridE As String
        Public GridN As String
        Public Miles As String
        Public Km As String
        Public Latitude As String
        Public Longitude As String
        Public JustBuilt As String
        Public UrbanRuralCode As String
        Public UrbanRuralName As String
        Public WardCode As String
        Public WardName As String
        Public Constituency As String
        Public EERCode As String
        Public EERName As String
        Public AuthorityCode As String
        Public Authority As String
        Public LEACode As String
        Public LEAName As String
        Public TVRegion As String
        Public Occupancy As String
        Public OccupancyDescription As String
        Public AddressType As String
        Public AddressTypeDescription As String
        Public UDPRN As String
        Public NHSCode As String
        Public NHSName As String
        Public NHSRegionCode As String
        Public NHSRegionName As String
        Public PCTCode As String
        Public PCTName As String
        Public CensationCode As String
        Public Affluence As String
        Public Lifestage As String
        Public AdditionalCensusInfo As String
        Public Residency As String
        Public HouseholdComposition As String
        Public Business As String
        Public Size As String
        Public SICCode As String
        Public OnEditedRoll As String
        Public Gender As String
        Public Forename As String
        Public MiddleInitial As String
        Public Surname As String
        Public DateOfBirth As String
        Public PhoneMatchType As String
        Public DataSet As String
        Public CouncilTaxBand As String
        Public Product As String
        Public Key As String
        Public List As String
    End Structure

    ' Internal Function to lookup Address data
    Private Declare Function InternalAFDData Lib "afddata.dll" Alias "AFDData" (ByVal dataName As String, ByVal operation As Integer, ByVal tData As String) As Integer

    ' String to let the DLL know the fields required
    Public Const afdFieldSpec As String = "Address@@Lookup:255@Name:120@Organisation:120@Property:120@Street:120@Locality:70@Town:30@PostalCounty:30@AbbreviatedPostalCounty:30@OptionalCounty:30@AbbreviatedOptionalCounty:30@TraditionalCounty:30@AdministrativeCounty:30@Postcode:10@DPS:2@PostcodeFrom:8@PostcodeType:6@MailsortCode:5@Phone:20@GridE:10@GridN:10@Miles:6@Km:6@Latitude:10@Longitude:10@JustBuilt:10@UrbanRuralCode:2@UrbanRuralName:60@WardCode:6@WardName:30@Constituency:50@EERCode:2@EERName:40@AuthorityCode:4@Authority:50@LEA Code:3@LEA Name:50@TVRegion:30@Occupancy:6@OccupancyDescription:30@AddressType:6@AddressTypeDescription:55@UDPRN:8@NHSCode:6@NHSName:50@NHSRegionCode:6@NHSRegionName:40@PCTCode:6@PCTName:50@CensationCode:10@Affluence:30@Lifestage:100@AdditionalCensusInfo:200@Residency:6@HouseholdComposition:106@Business:100@Size:6@SICCode:10@OnEditedRoll:6@Gender:6@Forename:30@MiddleInitial:6@Surname:30@DateOfBirth:10@PhoneMatchType:6@DataSet:16@CouncilTaxBand:6@Product:40@Key:40@List:512"

    ' Function Type Constants
    Public Const AFD_POSTCODE_LOOKUP As Long = 0
    Public Const AFD_POSTCODE_PROPERTY_LOOKUP As Long = 1
    Public Const AFD_FASTFIND_LOOKUP As Long = 2
    Public Const AFD_SEARCH As Long = 3
    Public Const AFD_RETRIEVE_RECORD As Long = 4
    Public Const AFD_CLEAN As Long = 7
    Public Const AFD_GET_NEXT As Long = 32
    Public Const AFD_LIST_BOX As Long = 64
    Public Const AFD_SHOW_ERROR As Long = 128

    ' Sector Skip Constants
    Public Const AFD_NO_SKIP As Long = 0
    Public Const AFD_ADDRESS_SKIP As Long = 512
    Public Const AFD_POSTCODE_SKIP As Long = 1024
    Public Const AFD_SECTOR_SKIP As Long = 1536
    Public Const AFD_OUTCODE_SKIP As Long = 2048
    Public Const AFD_POST_TOWN_SKIP As Long = 2560
    Public Const AFD_POSTCODE_AREA_SKIP As Long = 3072

    ' Success Code Constants
    Public Const AFD_RECORD_BREAK As Long = 0
    Public Const AFD_SUCCESS As Long = 1

    ' Error Code Constants
    Public Const AFD_ERROR_INVALID_FIELDSPEC As Long = -1
    Public Const AFD_ERROR_NO_RESULTS_FOUND As Long = -2
    Public Const AFD_ERROR_INVALID_RECORD_NUMBER As Long = -3
    Public Const AFD_ERROR_OPENING_FILES As Long = -4
    Public Const AFD_ERROR_FILE_READ As Long = -5
    Public Const AFD_ERROR_END_OF_SEARCH As Long = -6
    Public Const AFD_ERROR_DATA_LICENCE_ERROR As Long = -7
    Public Const AFD_ERROR_CONFLICTING_SEARCH_PARAMETERS As Long = -8
    Public Const AFD_USER_CANCELLED As Long = -99

    ' Refiner Cleaning Code Constants
    Public Const AFD_REFINER_PAF_MATCH As Long = 100
    Public Const AFD_REFINER_POSTCODE_MATCH As Long = 200
    Public Const AFD_REFINER_CHANGED_POSTCODE As Long = 201
    Public Const AFD_REFINER_ASSUME_POSTCODE_CORRECT As Long = 202
    Public Const AFD_REFINER_ASSUME_CHANGED_POSTCODE_CORRECT As Long = 203
    Public Const AFD_REFINER_ASSUME_POSTCODE_ADDED_PROPERTY As Long = 204
    Public Const AFD_REFINER_ASSUME_CHANGED_POSTCODE_ADDED_PROPERTY As Long = 205
    Public Const AFD_REFINER_FULL_DPS_MATCH As Long = 300
    Public Const AFD_REFINER_FULL_DPS_MATCH_NO_ORG As Long = 301
    Public Const AFD_REFINER_FULL_DPS_MATCH_LIMITED As Long = 302
    Public Const AFD_REFINER_STREET_MATCH As Long = 400
    Public Const AFD_REFINER_NO_MATCH_FOUND As Long = -101
    Public Const AFD_REFINER_AMBIGUOUS_POSTCODE As Long = -102
    Public Const AFD_REFINER_SUGGEST_RECORD As Long = -103
    Public Const AFD_REFINER_AMBIGUOUS_MATCH As Long = -104
    Public Const AFD_REFINER_INTERNATIONAL_ADDRESS As Long = -105
    Public Const AFD_REFINER_NO_RECORD_DATA As Long = -106

    ' Declarations for String Utilities (not required for the core API)
    Public Structure afdStringData
        Public Lookup As String
        Public Outcode As String
        Public Incode As String
        Public Search As String
        Public Replace As String
    End Structure
    Public Const afdStringFieldSpec As String = "String@@Lookup:255@Outcode:4@Incode:3@Search:255@Replace:255"
    Public Const AFD_STRING_SEARCH_REPLACE As Long = 0
    Public Const AFD_STRING_SEARCH_REPLACE_CASE As Long = 1
    Public Const AFD_STRING_CAPITALISE As Long = 2
    Public Const AFD_STRING_CLEAN_LINE As Long = 3
    Public Const AFD_STRING_CHECK_POSTCODE As Long = 4
    Public Const AFD_STRING_CLEAN_POSTCODE As Long = 5
    Public Const AFD_STRING_ABBREVIATE_COUNTY As Long = 6

    ' Declarations for Grid Utilities (not required for the core API)
    Public Structure afdGridData
        Public Lookup As String
        Public GBGridE As String
        Public GBGridN As String
        Public NIGridE As String
        Public NIGridN As String
        Public Latitude As String
        Public Longitude As String
        Public TextualLatitude As String
        Public TextualLongitude As String
        Public Km As String
        Public Miles As String
        Public GBGridEFrom As String
        Public GBGridNFrom As String
        Public NIGridEFrom As String
        Public NIGridNFrom As String
        Public LatitudeFrom As String
        Public LongitudeFrom As String
        Public TextualLatitudeFrom As String
        Public TextualLongitudeFrom As String
    End Structure
    Public Const afdGridFieldSpec As String = "Grid@@Lookup:255@GBGridE:10@GBGridN:10@NIGridE:10@NIGridN:10@Latitude:10@Longitude:10@TextualLatitude:15@TextualLongitude:15@Km:6@Miles:6@GBGridEFrom:10@GBGridNFrom:10@NIGridEFrom:10@NIGridNFrom:10@LatitudeFrom:10@LongitudeFrom:10@TextualLatitudeFrom:15@TextualLongitudeFrom:15"
    Public Const AFD_GRID_CONVERT As Long = 0
    Public Const AFD_GRID_LOOKUP_LOCATION As Long = 1
    Public Const AFD_GRID_DISTANCE As Long = 2

    ' Declarations for Map Utilities (not required for the core API)
    Public Structure AFDMapData
        Public GBGridE As String
        Public GBGridN As String
        Public NIGridE As String
        Public NIGridN As String
        Public Latitude As String
        Public Longitude As String
        Public TextualLatitude As String
        Public TextualLongitude As String
        Public ImageWidth As String
        Public ImageHeight As String
        Public ImageScale As String
        Public OutputType As String
        Public Filename As String
    End Structure
    Public Const afdMapFieldSpec As String = "Map@@GBGridE:10@GBGridN:10@NIGridE:10@NIGridN:10@Latitude:10@Longitude:10@TextualLatitude:15@TextualLongitude:15@ImageWidth:4@ImageHeight:4@ImageScale:1@OutputType:1@Filename:255"
    Public Const AFD_MAP_BMP As String = "0"
    Public Const AFD_MAP_PNG As String = "1"
    Public Const AFD_MAP_MINI As Long = 0
    Public Const AFD_MAP_FULL As Long = 1

    ' Function to lookup Address data
    Public Function AFDData(ByVal dataName As String, ByVal operation As Long, ByRef details As AFDAddressData) As Long

        Dim passDetails As String
        Dim retVal As Integer

        ' Compose the DLL string from the supplied structure
        passDetails = (details.Lookup & Space(255)).Substring(0, 255)
        passDetails = passDetails & (details.Name & Space(120)).Substring(0, 120)
        passDetails = passDetails & (details.Organisation & Space(120)).Substring(0, 120)
        passDetails = passDetails & (details.Prop & Space(120)).Substring(0, 120)
        passDetails = passDetails & (details.Street & Space(120)).Substring(0, 120)
        passDetails = passDetails & (details.Locality & Space(70)).Substring(0, 70)
        passDetails = passDetails & (details.Town & Space(30)).Substring(0, 30)
        passDetails = passDetails & (details.PostalCounty & Space(30)).Substring(0, 30)
        passDetails = passDetails & (details.AbbreviatedPostalCounty & Space(30)).Substring(0, 30)
        passDetails = passDetails & (details.OptionalCounty & Space(30)).Substring(0, 30)
        passDetails = passDetails & (details.AbbreviatedOptionalCounty & Space(30)).Substring(0, 30)
        passDetails = passDetails & (details.TraditionalCounty & Space(30)).Substring(0, 30)
        passDetails = passDetails & (details.AdministrativeCounty & Space(30)).Substring(0, 30)
        passDetails = passDetails & (details.Postcode & Space(10)).Substring(0, 10)
        passDetails = passDetails & (details.DPS & Space(2)).Substring(0, 2)
        passDetails = passDetails & (details.PostcodeFrom & Space(8)).Substring(0, 8)
        passDetails = passDetails & (details.PostcodeType & Space(6)).Substring(0, 6)
        passDetails = passDetails & (details.MailsortCode & Space(5)).Substring(0, 5)
        passDetails = passDetails & (details.Phone & Space(20)).Substring(0, 20)
        passDetails = passDetails & (details.GridE & Space(10)).Substring(0, 10)
        passDetails = passDetails & (details.GridN & Space(10)).Substring(0, 10)
        passDetails = passDetails & (details.Miles & Space(6)).Substring(0, 6)
        passDetails = passDetails & (details.Km & Space(6)).Substring(0, 6)
        passDetails = passDetails & (details.Latitude & Space(10)).Substring(0, 10)
        passDetails = passDetails & (details.Longitude & Space(10)).Substring(0, 10)
        passDetails = passDetails & (details.JustBuilt & Space(10)).Substring(0, 10)
        passDetails = passDetails & (details.UrbanRuralCode & Space(2)).Substring(0, 2)
        passDetails = passDetails & (details.UrbanRuralName & Space(60)).Substring(0, 60)
        passDetails = passDetails & (details.WardCode & Space(6)).Substring(0, 6)
        passDetails = passDetails & (details.WardName & Space(30)).Substring(0, 30)
        passDetails = passDetails & (details.Constituency & Space(50)).Substring(0, 50)
        passDetails = passDetails & (details.EERCode & Space(2)).Substring(0, 2)
        passDetails = passDetails & (details.EERName & Space(40)).Substring(0, 40)
        passDetails = passDetails & (details.AuthorityCode & Space(4)).Substring(0, 4)
        passDetails = passDetails & (details.Authority & Space(50)).Substring(0, 50)
        passDetails = passDetails & (details.LEACode & Space(3)).Substring(0, 3)
        passDetails = passDetails & (details.LEAName & Space(50)).Substring(0, 50)
        passDetails = passDetails & (details.TVRegion & Space(30)).Substring(0, 30)
        passDetails = passDetails & (details.Occupancy & Space(6)).Substring(0, 6)
        passDetails = passDetails & (details.OccupancyDescription & Space(30)).Substring(0, 30)
        passDetails = passDetails & (details.AddressType & Space(6)).Substring(0, 6)
        passDetails = passDetails & (details.AddressTypeDescription & Space(55)).Substring(0, 55)
        passDetails = passDetails & (details.UDPRN & Space(8)).Substring(0, 8)
        passDetails = passDetails & (details.NHSCode & Space(6)).Substring(0, 6)
        passDetails = passDetails & (details.NHSName & Space(50)).Substring(0, 50)
        passDetails = passDetails & (details.NHSRegionCode & Space(6)).Substring(0, 6)
        passDetails = passDetails & (details.NHSRegionName & Space(40)).Substring(0, 40)
        passDetails = passDetails & (details.PCTCode & Space(6)).Substring(0, 6)
        passDetails = passDetails & (details.PCTName & Space(50)).Substring(0, 50)
        passDetails = passDetails & (details.CensationCode & Space(10)).Substring(0, 10)
        passDetails = passDetails & (details.Affluence & Space(30)).Substring(0, 30)
        passDetails = passDetails & (details.Lifestage & Space(100)).Substring(0, 100)
        passDetails = passDetails & (details.AdditionalCensusInfo & Space(200)).Substring(0, 200)
        passDetails = passDetails & (details.Residency & Space(6)).Substring(0, 6)
        passDetails = passDetails & (details.HouseholdComposition & Space(106)).Substring(0, 106)
        passDetails = passDetails & (details.Business & Space(100)).Substring(0, 100)
        passDetails = passDetails & (details.Size & Space(6)).Substring(0, 6)
        passDetails = passDetails & (details.SICCode & Space(10)).Substring(0, 10)
        passDetails = passDetails & (details.OnEditedRoll & Space(6)).Substring(0, 6)
        passDetails = passDetails & (details.Gender & Space(6)).Substring(0, 6)
        passDetails = passDetails & (details.Forename & Space(30)).Substring(0, 30)
        passDetails = passDetails & (details.MiddleInitial & Space(6)).Substring(0, 6)
        passDetails = passDetails & (details.Surname & Space(30)).Substring(0, 30)
        passDetails = passDetails & (details.DateOfBirth & Space(10)).Substring(0, 10)
        passDetails = passDetails & (details.PhoneMatchType & Space(6)).Substring(0, 6)
        passDetails = passDetails & (details.DataSet & Space(16)).Substring(0, 16)
        passDetails = passDetails & (details.CouncilTaxBand & Space(6)).Substring(0, 6)
        passDetails = passDetails & (details.Product & Space(40)).Substring(0, 40)
        passDetails = passDetails & (details.Key & Space(40)).Substring(0, 40)
        passDetails = passDetails & (details.List & Space(512)).Substring(0, 512)

        ' Call the DLL function
        retVal = InternalAFDData(dataName, CInt(operation), passDetails)

        ' Retrieve the data structure from the string returned by the DLL
        details.Lookup = passDetails.Substring(0, 255).Trim
        details.Name = passDetails.Substring(255, 120).Trim
        details.Organisation = passDetails.Substring(375, 120).Trim
        details.Prop = passDetails.Substring(495, 120).Trim
        details.Street = passDetails.Substring(615, 120).Trim
        details.Locality = passDetails.Substring(735, 70).Trim
        details.Town = passDetails.Substring(805, 30).Trim
        details.PostalCounty = passDetails.Substring(835, 30).Trim
        details.AbbreviatedPostalCounty = passDetails.Substring(865, 30).Trim
        details.OptionalCounty = passDetails.Substring(895, 30).Trim
        details.AbbreviatedOptionalCounty = passDetails.Substring(925, 30).Trim
        details.TraditionalCounty = passDetails.Substring(955, 30).Trim
        details.AdministrativeCounty = passDetails.Substring(985, 30).Trim
        details.Postcode = passDetails.Substring(1015, 10).Trim
        details.DPS = passDetails.Substring(1025, 2).Trim
        details.PostcodeFrom = passDetails.Substring(1027, 8).Trim
        details.PostcodeType = passDetails.Substring(1035, 6).Trim
        details.MailsortCode = passDetails.Substring(1041, 5).Trim
        details.Phone = passDetails.Substring(1046, 20).Trim
        details.GridE = passDetails.Substring(1066, 10).Trim
        details.GridN = passDetails.Substring(1076, 10).Trim
        details.Miles = passDetails.Substring(1086, 6).Trim
        details.Km = passDetails.Substring(1092, 6).Trim
        details.Latitude = passDetails.Substring(1098, 10).Trim
        details.Longitude = passDetails.Substring(1108, 10).Trim
        details.JustBuilt = passDetails.Substring(1118, 10).Trim
        details.UrbanRuralCode = passDetails.Substring(1128, 2).Trim
        details.UrbanRuralName = passDetails.Substring(1130, 60).Trim
        details.WardCode = passDetails.Substring(1190, 6).Trim
        details.WardName = passDetails.Substring(1196, 30).Trim
        details.Constituency = passDetails.Substring(1226, 50).Trim
        details.EERCode = passDetails.Substring(1276, 2).Trim
        details.EERName = passDetails.Substring(1278, 40).Trim
        details.AuthorityCode = passDetails.Substring(1318, 4).Trim
        details.Authority = passDetails.Substring(1322, 50).Trim
        details.LEACode = passDetails.Substring(1372, 3).Trim
        details.LEAName = passDetails.Substring(1375, 50).Trim
        details.TVRegion = passDetails.Substring(1425, 30).Trim
        details.Occupancy = passDetails.Substring(1455, 6).Trim
        details.OccupancyDescription = passDetails.Substring(1461, 30).Trim
        details.AddressType = passDetails.Substring(1491, 6).Trim
        details.AddressTypeDescription = passDetails.Substring(1497, 55).Trim
        details.UDPRN = passDetails.Substring(1552, 8).Trim
        details.NHSCode = passDetails.Substring(1560, 6).Trim
        details.NHSName = passDetails.Substring(1566, 50).Trim
        details.NHSRegionCode = passDetails.Substring(1616, 6).Trim
        details.NHSRegionName = passDetails.Substring(1622, 40).Trim
        details.PCTCode = passDetails.Substring(1662, 6).Trim
        details.PCTName = passDetails.Substring(1668, 50).Trim
        details.CensationCode = passDetails.Substring(1718, 10).Trim
        details.Affluence = passDetails.Substring(1728, 30).Trim
        details.Lifestage = passDetails.Substring(1758, 100).Trim
        details.AdditionalCensusInfo = passDetails.Substring(1858, 200).Trim
        details.Residency = passDetails.Substring(2058, 6).Trim
        details.HouseholdComposition = passDetails.Substring(2064, 106).Trim
        details.Business = passDetails.Substring(2170, 100).Trim
        details.Size = passDetails.Substring(2270, 6).Trim
        details.SICCode = passDetails.Substring(2276, 10).Trim
        details.OnEditedRoll = passDetails.Substring(2286, 6).Trim
        details.Gender = passDetails.Substring(2292, 6).Trim
        details.Forename = passDetails.Substring(2298, 30).Trim
        details.MiddleInitial = passDetails.Substring(2328, 6).Trim
        details.Surname = passDetails.Substring(2334, 30).Trim
        details.DateOfBirth = passDetails.Substring(2364, 10).Trim
        details.PhoneMatchType = passDetails.Substring(2374, 6).Trim
        details.DataSet = passDetails.Substring(2380, 16).Trim
        details.CouncilTaxBand = passDetails.Substring(2396, 6).Trim
        details.Product = passDetails.Substring(2402, 40).Trim
        details.Key = passDetails.Substring(2442, 40).Trim
        details.List = passDetails.Substring(2482, 512).Trim

        ' Return with the function return value
        Return retVal

    End Function

    ' Function to lookup String Utility data
    Public Function AFDData(ByVal dataName As String, ByVal operation As Long, ByRef details As afdStringData) As Long

        Dim passDetails As String
        Dim retVal As Integer

        ' Compose the DLL string from the supplied structure
        passDetails = (details.Lookup & Space(255)).Substring(0, 255)
        passDetails = passDetails & (details.Outcode & Space(4)).Substring(0, 4)
        passDetails = passDetails & (details.Incode & Space(3)).Substring(0, 3)
        passDetails = passDetails & (details.Search & Space(255)).Substring(0, 255)
        passDetails = passDetails & (details.Replace & Space(255)).Substring(0, 255)

        ' Call the DLL function
        retVal = InternalAFDData(dataName, CInt(operation), passDetails)

        ' Retrieve the data structure from the string returned by the DLL
        details.Lookup = passDetails.Substring(0, 255).Trim
        details.Outcode = passDetails.Substring(255, 4).Trim
        details.Incode = passDetails.Substring(259, 3).Trim
        details.Search = passDetails.Substring(262, 255).Trim
        details.Replace = passDetails.Substring(517, 255).Trim

        ' Return with the function return value
        Return retVal

    End Function

    ' Function to lookup Grid Utility data
    Public Function AFDData(ByVal dataName As String, ByVal operation As Long, ByRef details As afdGridData) As Long

        Dim passDetails As String
        Dim retVal As Integer

        ' Compose the DLL string from the supplied structure
        passDetails = (details.Lookup & Space(255)).Substring(0, 255)
        passDetails = passDetails & (details.GBGridE & Space(10)).Substring(0, 10)
        passDetails = passDetails & (details.GBGridN & Space(10)).Substring(0, 10)
        passDetails = passDetails & (details.NIGridE & Space(10)).Substring(0, 10)
        passDetails = passDetails & (details.NIGridN & Space(10)).Substring(0, 10)
        passDetails = passDetails & (details.Latitude & Space(10)).Substring(0, 10)
        passDetails = passDetails & (details.Longitude & Space(10)).Substring(0, 10)
        passDetails = passDetails & (details.TextualLatitude & Space(15)).Substring(0, 15)
        passDetails = passDetails & (details.TextualLongitude & Space(15)).Substring(0, 15)
        passDetails = passDetails & (details.Km & Space(6)).Substring(0, 6)
        passDetails = passDetails & (details.Miles & Space(6)).Substring(0, 6)
        passDetails = passDetails & (details.GBGridEFrom & Space(10)).Substring(0, 10)
        passDetails = passDetails & (details.GBGridNFrom & Space(10)).Substring(0, 10)
        passDetails = passDetails & (details.NIGridEFrom & Space(10)).Substring(0, 10)
        passDetails = passDetails & (details.NIGridNFrom & Space(10)).Substring(0, 10)
        passDetails = passDetails & (details.LatitudeFrom & Space(10)).Substring(0, 10)
        passDetails = passDetails & (details.LongitudeFrom & Space(10)).Substring(0, 10)
        passDetails = passDetails & (details.TextualLatitudeFrom & Space(15)).Substring(0, 15)
        passDetails = passDetails & (details.TextualLongitudeFrom & Space(15)).Substring(0, 15)

        ' Call the DLL function
        retVal = InternalAFDData(dataName, CInt(operation), passDetails)

        ' Retrieve the data structure from the string returned by the DLL
        details.Lookup = passDetails.Substring(0, 255).Trim
        details.GBGridE = passDetails.Substring(255, 10).Trim
        details.GBGridN = passDetails.Substring(265, 10).Trim
        details.NIGridE = passDetails.Substring(275, 10).Trim
        details.NIGridN = passDetails.Substring(285, 10).Trim
        details.Latitude = passDetails.Substring(295, 10).Trim
        details.Longitude = passDetails.Substring(305, 10).Trim
        details.TextualLatitude = passDetails.Substring(315, 15).Trim
        details.TextualLongitude = passDetails.Substring(330, 15).Trim
        details.Km = passDetails.Substring(345, 6).Trim
        details.Miles = passDetails.Substring(351, 6).Trim
        details.GBGridEFrom = passDetails.Substring(357, 10).Trim
        details.GBGridNFrom = passDetails.Substring(367, 10).Trim
        details.NIGridEFrom = passDetails.Substring(377, 10).Trim
        details.NIGridNFrom = passDetails.Substring(387, 10).Trim
        details.LatitudeFrom = passDetails.Substring(397, 10).Trim
        details.LongitudeFrom = passDetails.Substring(407, 10).Trim
        details.TextualLatitudeFrom = passDetails.Substring(417, 15).Trim
        details.TextualLongitudeFrom = passDetails.Substring(432, 15).Trim

        ' Return with the function return value
        Return retVal

    End Function

    ' Function to lookup Map Utility data
    Public Function AFDData(ByVal dataName As String, ByVal operation As Long, ByRef details As AFDMapData) As Long

        Dim passDetails As String
        Dim retVal As Integer

        ' Compose the DLL string from the supplied structure
        passDetails = (details.GBGridE & Space(10)).Substring(0, 10)
        passDetails = passDetails & (details.GBGridN & Space(10)).Substring(0, 10)
        passDetails = passDetails & (details.NIGridE & Space(10)).Substring(0, 10)
        passDetails = passDetails & (details.NIGridN & Space(10)).Substring(0, 10)
        passDetails = passDetails & (details.Latitude & Space(10)).Substring(0, 10)
        passDetails = passDetails & (details.Longitude & Space(10)).Substring(0, 10)
        passDetails = passDetails & (details.TextualLatitude & Space(15)).Substring(0, 15)
        passDetails = passDetails & (details.TextualLongitude & Space(15)).Substring(0, 15)
        passDetails = passDetails & (details.ImageWidth & Space(4)).Substring(0, 4)
        passDetails = passDetails & (details.ImageHeight & Space(4)).Substring(0, 4)
        passDetails = passDetails & (details.ImageScale & Space(1)).Substring(0, 1)
        passDetails = passDetails & (details.OutputType & Space(1)).Substring(0, 1)
        passDetails = passDetails & (details.Filename & Space(255)).Substring(0, 255)

        ' Call the DLL function
        retVal = InternalAFDData(dataName, CInt(operation), passDetails)

        ' Retrieve the data structure from the string returned by the DLL
        details.GBGridE = passDetails.Substring(0, 10).Trim
        details.GBGridN = passDetails.Substring(10, 10).Trim
        details.NIGridE = passDetails.Substring(20, 10).Trim
        details.NIGridN = passDetails.Substring(30, 10).Trim
        details.Latitude = passDetails.Substring(40, 10).Trim
        details.Longitude = passDetails.Substring(50, 10).Trim
        details.TextualLatitude = passDetails.Substring(60, 15).Trim
        details.TextualLongitude = passDetails.Substring(75, 15).Trim
        details.ImageWidth = passDetails.Substring(90, 4).Trim
        details.ImageHeight = passDetails.Substring(94, 4).Trim
        details.ImageScale = passDetails.Substring(98, 1).Trim
        details.OutputType = passDetails.Substring(99, 1).Trim
        details.Filename = passDetails.Substring(100, 255).Trim

        ' Return with the function return value
        Return retVal

    End Function

    ' Function to return text corresponding to the error code supplied, e.g. for display in a Message Box
    Public Function AFDErrorText(ByVal errorCode As Long) As String

        Select Case errorCode
            Case AFD_ERROR_INVALID_FIELDSPEC
                AFDErrorText = "Application Internal Error" ' The fieldSpec string passed to the AFD API was invalid
            Case AFD_ERROR_NO_RESULTS_FOUND
                AFDErrorText = "No Results Found"
            Case AFD_ERROR_INVALID_RECORD_NUMBER
                AFDErrorText = "Invalid Record Number" ' Only possible if an invalid key is passed when re-looking up a result
            Case AFD_ERROR_OPENING_FILES
                AFDErrorText = "Error Opening AFD Data Files"
            Case AFD_ERROR_FILE_READ
                AFDErrorText = "AFD Data File Read Error"
            Case AFD_ERROR_END_OF_SEARCH
                AFDErrorText = "End Of Search" ' Shouldn't normally be displayed to the user
            Case AFD_ERROR_DATA_LICENCE_ERROR
                AFDErrorText = "Data Licence Error - Check software is registered"
            Case AFD_ERROR_CONFLICTING_SEARCH_PARAMETERS
                AFDErrorText = "Conflicting Search Parameters" ' The Name and Organisation fields cannot be searched at the same time
                ' The UDPRN field must be searched independently of any other fields
            Case AFD_USER_CANCELLED
                AFDErrorText = "User Cancelled" ' User Cancelled DLL Internal ListBox
            Case Else
                If errorCode >= 0 Then
                    AFDErrorText = "" ' Success - Shouldn't call this function in this case!
                Else
                    AFDErrorText = "Unknown Error " & Format(errorCode)
                End If
        End Select

    End Function

    ' Function to clear AFDAddressData Structure
    Public Sub ClearAFDAddressData(ByVal details As AFDAddressData)

        With details
            .Lookup = ""
            .Name = ""
            .Organisation = ""
            .Prop = ""
            .Street = ""
            .Locality = ""
            .Town = ""
            .PostalCounty = ""
            .AbbreviatedPostalCounty = ""
            .OptionalCounty = ""
            .AbbreviatedOptionalCounty = ""
            .TraditionalCounty = ""
            .AdministrativeCounty = ""
            .Postcode = ""
            .DPS = ""
            .PostcodeFrom = ""
            .PostcodeType = ""
            .MailsortCode = ""
            .Phone = ""
            .GridE = ""
            .GridN = ""
            .Miles = ""
            .Km = ""
            .Latitude = ""
            .Longitude = ""
            .JustBuilt = ""
            .UrbanRuralCode = ""
            .UrbanRuralName = ""
            .WardCode = ""
            .WardName = ""
            .Constituency = ""
            .EERCode = ""
            .EERName = ""
            .AuthorityCode = ""
            .Authority = ""
            .LEACode = ""
            .LEAName = ""
            .TVRegion = ""
            .Occupancy = ""
            .OccupancyDescription = ""
            .AddressType = ""
            .AddressTypeDescription = ""
            .UDPRN = ""
            .NHSCode = ""
            .NHSName = ""
            .NHSRegionCode = ""
            .NHSRegionName = ""
            .PCTCode = ""
            .PCTName = ""
            .CensationCode = ""
            .Affluence = ""
            .Lifestage = ""
            .AdditionalCensusInfo = ""
            .Residency = ""
            .HouseholdComposition = ""
            .Business = ""
            .Size = ""
            .SICCode = ""
            .OnEditedRoll = ""
            .Gender = ""
            .Forename = ""
            .MiddleInitial = ""
            .Surname = ""
            .DateOfBirth = ""
            .PhoneMatchType = ""
            .DataSet = ""
            .CouncilTaxBand = ""
            .Product = ""
            .Key = ""
            .List = ""
        End With

    End Sub
    ' Function to return text corresponding to the Refiner Cleaning Result
    Public Function AFDRefinerCleaningText(ByVal returnCode As Long) As String

        Select Case returnCode
            Case AFD_REFINER_PAF_MATCH
                AFDRefinerCleaningText = "Record identically matches PAF Record"
            Case AFD_REFINER_POSTCODE_MATCH
                AFDRefinerCleaningText = "Postcode Match"
            Case AFD_REFINER_CHANGED_POSTCODE
                AFDRefinerCleaningText = "Postcode Match with Changed Postcode"
            Case AFD_REFINER_ASSUME_POSTCODE_CORRECT
                AFDRefinerCleaningText = "Postcode, Property Match Assuming The Postcode Is Correct"
            Case AFD_REFINER_ASSUME_CHANGED_POSTCODE_CORRECT
                AFDRefinerCleaningText = "Postcode, Property Match with Changed Postcode Assuming The Postcode Is Correct"
            Case AFD_REFINER_ASSUME_POSTCODE_ADDED_PROPERTY
                AFDRefinerCleaningText = "Postcode, Property Match with Non-Matched Property Added In"
            Case AFD_REFINER_ASSUME_CHANGED_POSTCODE_ADDED_PROPERTY
                AFDRefinerCleaningText = "Postcode, Property Match with Changed Postcode and Non-Matched Property Added In"
            Case AFD_REFINER_FULL_DPS_MATCH
                AFDRefinerCleaningText = "Full DPS Match"
            Case AFD_REFINER_FULL_DPS_MATCH_NO_ORG
                AFDRefinerCleaningText = "Full DPS Match (Ambiguous Organisation Not Retained)"
            Case AFD_REFINER_FULL_DPS_MATCH_LIMITED
                AFDRefinerCleaningText = "Limited DPS Match (Locality or Town information not present)"
            Case AFD_REFINER_STREET_MATCH
                AFDRefinerCleaningText = "Street (Non DPS) Match"
            Case AFD_REFINER_NO_MATCH_FOUND
                AFDRefinerCleaningText = "No Match Found"
            Case AFD_REFINER_AMBIGUOUS_POSTCODE
                AFDRefinerCleaningText = "Ambiguous Postcode (Non DPS Match)"
            Case AFD_REFINER_SUGGEST_RECORD
                AFDRefinerCleaningText = "Suggested Record"
            Case AFD_REFINER_AMBIGUOUS_MATCH
                AFDRefinerCleaningText = "Ambiguous Match"
            Case AFD_REFINER_INTERNATIONAL_ADDRESS
                AFDRefinerCleaningText = "International Address"
            Case AFD_REFINER_NO_RECORD_DATA
                AFDRefinerCleaningText = "No Record Data Supplied"
            Case Else
                AFDRefinerCleaningText = AFDErrorText(returnCode)
        End Select

    End Function

End Class
