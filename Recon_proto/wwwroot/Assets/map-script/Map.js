var districtid = "";
var districtName = "";
var blockid = "";
var blockName = "";
var villageid = "";
var villageName = "";
var ROLEID;
var _siteData = "";
var _siteInfo = "";
var _QBMasterData = "";

$(document).ready(function () {

    getGISDashboardData(0);

    $.ajax({
        url: GetDataListForDropDown.DataDropDownURL,
        type: "GET",
        contentType: "application/json;charset=utf-8",
        traditional: true,
        success: function (result) {

            if (result._lstDistrictList != null && result._lstDistrictList.length != 0) {
                comboboxBind(result._lstDistrictList, "ddlDistrict", "DistrictName", "DistrictID");

                for (var i = 0; i < result._lstDistrictList.length; i++) {
                    districtid = districtid + result._lstDistrictList[i].DistrictID + ",";
                    districtName = districtName + result._lstDistrictList[i].DistrictName + ",";
                }
                districtid = districtid.replace(/,\s*$/, "");
                districtName = districtName.replace(/,\s*$/, "");
                districtName = '\'' + districtName.split(',').join('\',\'') + '\'';

            }
            else {
                $("#ddlDistrict").html("");
                $("#ddlDistrict").append
               ($('<option></option>').val(0).html("---Select ---"));
            }

            if (result._lstBlockList != null && result._lstBlockList.length != 0) {

                comboboxBind(result._lstBlockList, "ddlBlock", "BlockName", "BlockID");

                for (var i = 0; i < result._lstBlockList.length; i++) {
                    blockid = blockid + result._lstBlockList[i].BlockID + ",";
                    blockName = blockName + result._lstBlockList[i].BlockName + ",";
                }
                blockid = blockid.replace(/,\s*$/, "");
                blockName = blockName.replace(/,\s*$/, "");
                blockName = '\'' + blockName.split(',').join('\',\'') + '\'';
              
            }
            else {

                $("#ddlBlock").html("");
                $("#ddlBlock").append
               ($('<option></option>').val(0).html("---Select---"));
            }

            if (result._lstVillageList != null && result._lstVillageList.length != 0) {

                comboboxBind(result._lstVillageList, "ddlVillage", "VillageName", "VillageID");
                for (var i = 0; i < result._lstVillageList.length; i++) {
                    villageid = villageid + result._lstVillageList[i].VillageID + ",";
                    villageName = villageName + result._lstVillageList[i].VillageName + ",";

                }
                villageid = villageid.replace(/,\s*$/, "");
                villageName = villageName.replace(/,\s*$/, "");
                villageName = '\'' + villageName.split(',').join('\',\'') + '\'';
                      
            }
            else {
                $("#ddlVillage").html("");
                $("#ddlVillage").append($('<option></option>').val(0).html("---Select---"));
            }
            SelectDropDown(result);
        },
        error: function (e) {
            alert(e);
        }
    });

    function SelectDropDown(resultData) {

        if (ROLEID == 3) { //dpmu
            $("#ddlDistrict option:contains(" + resultData._lstDistrictList[0].DistrictName + ")").attr('selected', 'selected');
            $('#ddlDistrict').prop("disabled", true);
        }
        else if (ROLEID == 4) {  //bpmu
            $("#ddlDistrict option:contains(" + resultData._lstDistrictList[0].DistrictName + ")").attr('selected', 'selected');
            $("#ddlBlock option:contains(" + resultData._lstBlockList[0].BlockName + ")").attr('selected', 'selected');
            $('#ddlDistrict').prop("disabled", true);
            $('#ddlBlock').prop("disabled", true);
        }
        else {

            if (ROLEID != 1 && ROLEID != 2 && ROLEID != 3 && ROLEID != 4) {
                var villageid = resultData._lstVillageList[0].VillageID;
              //  GetVNRMCData(villageId);
                $("#ddlVillage option:contains(" + resultData._lstVillageList[0].VillageName + ")").attr('selected', 'selected');
                $("#ddlDistrict option:contains(" + resultData._lstDistrictList[0].DistrictName + ")").attr('selected', 'selected');
                $("#ddlBlock option:contains(" + resultData._lstBlockList[0].BlockName + ")").attr('selected', 'selected');
                $('#ddlDistrict').prop("disabled", true);
                $('#ddlBlock').prop("disabled", true);
                $('#ddlVillage').prop("disabled", true);
            }
        }

    }


    _siteData = getSiteData();
    if (_siteData != "" && _siteData != undefined) {
        if (_siteData.InterventionType) {
            comboboxBind(_siteData.InterventionType, "ddlSiteId", "InterventionType", "InterventionTypeID");
        }
        if (_siteData.LandownershipType) {
            comboboxBind(_siteData.LandownershipType, "ddlLandownerId", "LandOwnershipType", "LandOwnershipID");
        }
        if (_siteData.PriorityType) {
            comboboxBind(_siteData.PriorityType, "ddlPriorityId", "PriorityType", "PriorityTypeID");
        }
        if (_siteData.InterventionActivityType) {
            comboboxBind(_siteData.InterventionActivityType, "ddlInterventionActivityType", "InterventionActivityType", "InterventionActivityTypeID");
        }
    }
    //OnInterventionSiteChange('ddlSiteId', _siteData);
    //OnInterventionSiteTypeChange('ddlInterventionActivityType', _siteData.InterventionActivityType);

  //  _masterData = getMasterData("LoginApi");

    if (ROLEID == 1 || ROLEID == 2 || ROLEID == 3) {
        OnDistrictChangeMap('ddlDistrict', _masterData);
        OnBlockChangeMap('ddlBlock', _masterData);
    }

    function OnDistrictChangeMap(id, masterData) {
        var filterdata = null;
        $("#" + id).on("change", function () {
            var Id = $("#" + id).val();
            if (Id != null) {
                filterdata = jQuery.grep(masterData.Block, function (obj) {
                    return obj.DistrictID == Id;
                });
                if (filterdata != "") {
                    comboboxBind(filterdata, "cmbBlock", "BlockName", "BlockID");
                    comboboxBind(filterdata, "ddlBlock", "BlockName", "BlockID");
                    $("#ddlBlock option:contains('All')").attr('class', 'd-none');
                    //comboboxBind(filterdata, "ddBlock", "BlockName", "BlockID");
                }

                else {
                    $('#ddlBlock').empty();
                    $('#ddlVillage').empty();
                    $("#ddlVillage").append
                        ($('<option></option>').val(0).html("---Select ---"));
                    $("#ddlBlock").append
                       ($('<option></option>').val(0).html("---Select ---"));
                    $('#cmbBlock').empty();
                    $('#cmbVillage').empty();
                }
                //var chosenOption = $("#" + id + " option:selected").text();
                //var option = "Other";
                if (chosenOption == 'DPMU') {
                    $("#cmbBlock option:contains('All')").attr('selected', 'selected');
                    $('#cmbBlock').trigger('change');
                }
                if (chosenOption == 'BPMU') {
                    $("#cmbBlock option:contains('All')").attr('class', 'd-none');
                }
                if (chosenOption == 'VNMRC Member' || chosenOption == 'VNMRC Purchase Committee' || chosenOption == 'VNMRC Executive') {
                    $("#cmbBlock option:contains('All')").attr('class', 'd-none');
                }
            }

        });
    }

    function OnBlockChangeMap(id, masterData) {
        var filterdata = null;
        $("#" + id).on("change", function () {
            var Id = $("#" + id).val();
            if (Id != null) {
                filterdata = jQuery.grep(masterData.Village, function (obj) {
                    return obj.BlockID == Id;
                });
                if (filterdata != "") {
                    comboboxBind(filterdata, "cmbVillage", "VillageName", "VillageID");
                    comboboxBind(filterdata, "ddlVillage", "VillageName", "VillageID");
                    $("#ddlVillage option:contains('All')").attr('class', 'd-none');
                    // comboboxBind(filterdata, "ddVillage", "VillageName", "VillageID");
                }
                else {
                    $('#cmbVillage').empty();
                    $('#ddlVillage').empty();
                    $("#ddlVillage").append
                        ($('<option></option>').val(0).html("---Select ---"));
                }
                if (chosenOption == 'DPMU' || chosenOption == 'BPMU') {
                    $("#cmbVillage option:contains('All')").attr('selected', 'selected');

                }
                if (chosenOption == 'VNMRC Member' || chosenOption == 'VNMRC Purchase Committee' || chosenOption == 'VNMRC Executive') {
                    $("#cmbVillage option:contains('All')").attr('class', 'd-none');
                }
            }
        });
    }

    function getSiteData() {
        try {
            var siteData = "";
            $.ajax({
                url: APPURL + "/MapViewer/" + "/FillAttributeEditor/",
                type: "get",
                async: false,
                success: function (succ) {
                    siteData = JSON.parse(succ);
                }
            });
            return siteData;
        }
        catch (e) {
            console.log("siteData :" + e.message);
            $(".animationload").css("display", "none");
        }
    }

   
    //var activityTypeId = "";
    //$("#ddlInterventionActivityType").on("change", function () {

    //    var activityTypeId = $("#ddlInterventionActivityType").val();
        
    //    if (activityTypeId != null && activityTypeId != 0) {
    //        var selectId = document.getElementById("ddlInterventionActivityType");
    //        var selectedText = selectId.options[selectId.selectedIndex].text;
            
    //        document.getElementById('SiteActivityTypeName').innerHTML = selectedText;
    //        showMarkedSiteTypesFromDB(activityTypeId);
    //    }
    //    else {
    //        $("#SitesIDs").hide();
    //    }
        
    //});

    //------------Function to get sample values in value div------//
    var HtmlValueID = "";
    //_QBMasterData = getSiteData();

    $("#GetSample").click(function () {
        HtmlValueID = "";
        //if ($("#QBattributeID option:selected").val() == "FID" || $("#QBattributeID option:selected").val() == "VillageID") {
        //    $("#message").text("");
        //    if (_siteData.VillageData != null) {

        //        $('#QBvalueID').find('option').remove();// clear before appending new list    
        //        for (var i = 0; i < _siteData.VillageData.length; i++) {
        //            HtmlValueID += "<option value='" + _siteData.VillageData[i].VillageID + "'>" + _siteData.VillageData[i].VillageName + "</option>";
        //        }
        //        $("#QBvalueID").append(HtmlValueID);

        //    }

        //}
         if ($("#QBattributeID option:selected").val() == "InterventionTypeID") {
            $("#message").text("");
            if (_siteData.InterventionType != null) {

                $('#QBvalueID').find('option').remove();// clear before appending new list    
                for (var i = 0; i < _siteData.InterventionType.length; i++) {
                    HtmlValueID += "<option value='" + _siteData.InterventionType[i].InterventionTypeID + "'>" + _siteData.InterventionType[i].InterventionType + "</option>";
                }
                $("#QBvalueID").append(HtmlValueID);

            }

        }
        else if ($("#QBattributeID option:selected").val() == "InterventionActivityTypeID") {
            $("#message").text("");
            if (_siteData.InterventionActivityType != null) {

                $('#QBvalueID').find('option').remove();// clear before appending new list    
                for (var i = 0; i < _siteData.InterventionActivityType.length; i++) {
                    HtmlValueID += "<option value='" + _siteData.InterventionActivityType[i].InterventionActivityTypeID + "'>" + _siteData.InterventionActivityType[i].InterventionActivityType + "</option>";
                }
                $("#QBvalueID").append(HtmlValueID);

            }

        }
        else if ($("#QBattributeID option:selected").val() == "LandOwnershipTypeID") {
            $("#message").text("");
            if (_siteData.LandownershipType != null) {

                $('#QBvalueID').find('option').remove();// clear before appending new list    
                for (var i = 0; i < _siteData.LandownershipType.length; i++) {
                    HtmlValueID += "<option value='" + _siteData.LandownershipType[i].LandOwnershipID + "'>" + _siteData.LandownershipType[i].LandOwnershipType + "</option>";
                }
                $("#QBvalueID").append(HtmlValueID);

            }

        }
        else if ($("#QBattributeID option:selected").val() == "PriorityTypeID") {
            $("#message").text("");
            if (_siteData.PriorityType != null) {

                $('#QBvalueID').find('option').remove();// clear before appending new list    
                for (var i = 0; i < _siteData.PriorityType.length; i++) {
                    HtmlValueID += "<option value='" + _siteData.PriorityType[i].PriorityTypeID + "'>" + _siteData.PriorityType[i].PriorityType + "</option>";
                }
                $("#QBvalueID").append(HtmlValueID);

            }

        }
        else if ($("#QBattributeID option:selected").val() == "VILLNA2011" || $("#QBattributeID option:selected").val() == "InterventionSiteID" || $("#QBattributeID option:selected").val() == "InterventionSiteName") {

            $("#QBvalueID").empty();
            $("#message").text("* Sample values are not available for this field");
           // $("#message").bs_info("Sample values are not available for this field");
        }
    });
    //--------Function Ended------------------------//


});



//function showMarkedSiteTypesFromDB(activityTypeId) {

//    var tableData = "";
//    var url = GetInterventionSiteActivityTypeData.InterventionActivitySiteURL;

//    $.post(url, { InterventionActivityTypeId: activityTypeId }, function (data) {

//        _siteInfo = JSON.parse(data);

//        if (_siteInfo.ReturnData != null && _siteInfo.ReturnData.length != 0) {


//            for (var i = 0; i < _siteInfo.ReturnData.length; i++) {

//               // if (_siteInfo.ReturnData[i].InterventionActivityTypeID == activityTypeId) {
//                    tableData += "<tr>";

//                    if (_siteInfo.ReturnData[i].InterventionSiteName != null) {
//                        tableData += "<td>" + _siteInfo.ReturnData[i].InterventionSiteName + "</td>";
//                    }

//                    if (_siteInfo.ReturnData[i].Area_Ha != null) {
//                        tableData += "<td>" + _siteInfo.ReturnData[i].Area_Ha + "</td>";
//                    }
//                    tableData += "</tr>";

//               // }

//            }

//        }

//        if (tableData != "") {

//            $('#SiteTable').empty().append(tableData);
//            $("#SitesIDs").show();

//        }
//        else {
//            $('#SiteTable').empty();
//            $("#SitesIDs").show();
//            $('#SiteTable').empty().append("<tr><td>No marked site present</td></tr>");
//        }
        
//    });

         
        

//}

//function OnInterventionSiteChange(id, _siteData) {
//    var filterdata = null;
//    $("#" + id).on("change", function () {
//        var Id = $("#" + id).val();
//        if (Id != null) {
//            filterdata = jQuery.grep(_siteData.InterventionActivityType, function (obj) {
//                return obj.InterventionTypeID == Id;
//            });
//            if (filterdata != "") {

//                comboboxBind(filterdata, "ddlActivityId", "InterventionActivityType", "InterventionTypeID");
//            }
//            else {
//                $('#ddlActivityId').empty();

//                $("#ddlActivityId").append
//                    ($('<option></option>').val(0).html("---Select ---"));
//            }
//        }
//    });
//}

function IsAlphabet(evt, inputid) {
    try {
        var specialKeys = new Array();
        specialKeys.push(8); //Backspace
        specialKeys.push(9); //Tab
        specialKeys.push(46); //Delete
        specialKeys.push(36); //Home
        specialKeys.push(35); //End
        specialKeys.push(37); //Left
        specialKeys.push(39); //Right

        var keyCode = evt.keyCode == 0 ? evt.charCode : evt.keyCode;
        var ret = ((keyCode == 32) || keyCode >= 65 && keyCode <= 90) || (keyCode >= 97 && keyCode <= 122) || (specialKeys.indexOf(evt.keyCode) != -1 && evt.charCode != evt.keyCode);

        document.getElementById(inputid + "_validatemessage").style.display = ret ? "none" : "inline";
        $("#" + inputid + "_validatemessage").fadeOut(5000);
        return ret;
    }
    catch (err) {
        errorAlert("Validation Fail..!!");
    }
}
function ValidateNo(evt, inputid) {
    try {
        var specialKeys = new Array();
        specialKeys.push(8); //Backspace
        specialKeys.push(9); //Tab
        specialKeys.push(46); //Delete
        specialKeys.push(36); //Home
        specialKeys.push(35); //End
        specialKeys.push(37); //Left
        specialKeys.push(39); //Right

            var keyCode = evt.keyCode == 0 ? evt.charCode : evt.keyCode;
            var ret = ((keyCode >= 48 && keyCode <= 57) || (specialKeys.indexOf(evt.keyCode) != -1 && evt.charCode != evt.keyCode));
            document.getElementById(inputid + "_allownumeric").style.display = ret ? "none" : "inline";
            $("#" + inputid + "_allownumeric").fadeOut(5000);

            return ret;
        
    }
    catch (err) {
        errorAlert("Validation Fail..!!");
    }
}

//Click to close and Resize Result Panel
$('document').ready(function () {
    $('.CollapseResults').on('click', function () {
        $('#QBResultPanel').toggleClass('ResultPaneBottom');

        $(this).find('.toggleIcons').toggleClass('fa fa-minus  fa fa-plus');

    });
    $('.closeResultPanes').on('click', function () {
        $('#QBResultPanel').css({ 'display': 'none' });

    });
});

function getGISDashboardData(id) {
    try {
        showLoading();
        $.ajax({
            "type": "GET",
            "url": APPURL + "/api/GISDashBoardApi/" + id,
            "contentType": "application/json",
            "success": function (succ) {
                dashBoardCount = JSON.parse(succ);
                if (dashBoardCount.VillageCount) {
                    $('#h1VillageNo').html(dashBoardCount.VillageCount[0].Column1)
                }
                if (dashBoardCount.LULCCount) {
                    $('#h1LULC').html(dashBoardCount.LULCCount[0].Column1)
                }
                if (dashBoardCount.CNRMCount) {
                    $('#h1CNRMP').html(dashBoardCount.CNRMCount[0].Column1)
                }
                if (dashBoardCount.SiteCount) {
                    $('#h1Intervention').html(dashBoardCount.SiteCount[0].Column1)
                }
                if (dashBoardCount.ProposedCount) {
                    $('#h1ProposedCount').html(dashBoardCount.ProposedCount[0].Column1)
                }
                if (dashBoardCount.ApprovedCount) {
                    $('#h1ApprovedCount').html(dashBoardCount.ApprovedCount[0].Column1)
                }
                if (dashBoardCount.ImplementedCount) {
                    $('#h1ImplementedCount').html(dashBoardCount.ImplementedCount[0].Column1)
                }

                hideLoading();
            },
            error: function (err) {
            }
        });
    } catch (ex) {
        console.log("getSensitization()" + ex.message);
        sessionMgmt(ex);
        hideLoading();
    }
}