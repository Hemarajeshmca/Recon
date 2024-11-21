
var map;
var USERID;
var DISTRICTID;
var BLOCKID;
var VILLAGEID;
var ROLEID;
//var ab = globalVariable.x;
var districtid;
var districtName;
var blockid;
var blockName;
var villageid;
var villageName;
var drawSelectionToolbar;
var Imagefiles;
var MapImagePath;
var DynamicLayerURL;
var StateLayerURL;
var DistrictLayerURL;
var BlockLayerURL;
var VillageLayerURL;
var PointLayerURL;
var PolylineLayerURL;
var PolygonLayerURL;
var LULCLayerURL;

require(["esri/map",
    "esri/layers/ArcGISDynamicMapServiceLayer",
    "esri/renderers/BlendRenderer",
     "esri/renderers/UniqueValueRenderer",
     "esri/config",
       "esri/symbols/TextSymbol",
     "esri/tasks/ProjectParameters",
    "esri/toolbars/draw",
    "esri/tasks/QueryTask",
    "esri/dijit/Legend",
     "esri/symbols/SimpleMarkerSymbol", "esri/symbols/SimpleLineSymbol",
     "esri/symbols/PictureFillSymbol", "esri/symbols/CartographicLineSymbol",
     "esri/TimeExtent", "esri/dijit/TimeSlider", "esri/layers/TimeInfo", "esri/dijit/HorizontalSlider",
     "esri/symbols/SimpleFillSymbol",
     "esri/dijit/OpacitySlider",
     "dijit/form/HorizontalSlider",
     "esri/geometry/Polyline",
      "esri/geometry/Point",
      "esri/geometry/Polygon",
      "esri/geometry/geodesicUtils",
      "esri/geometry/geometryEngine",
      "esri/units",
     "esri/SpatialReference",
       "esri/graphic",
       "esri/layers/GraphicsLayer",
        "esri/tasks/query",
        "esri/tasks/GeometryService",
    "esri/request",
    "esri/geometry/scaleUtils",
     "esri/renderers/SimpleRenderer",
    "esri/layers/FeatureLayer",
    "esri/toolbars/navigation",
    "esri/dijit/LayerList",
     "esri/dijit/Measurement",
      "esri/dijit/HomeButton",
      "esri/dijit/InfoWindow",
       "esri/InfoTemplate",
       "esri/tasks/IdentifyTask",
       "esri/tasks/IdentifyParameters",
        "dojo/dom",
        "dojo/_base/connect",
        "dojo/json",
        "dojo/on",
        "dojo/parser", "dojo/sniff",
        "dojo/_base/array",
        "esri/Color",
        "esri/layers/LabelClass",
        "dojo/domReady!",
        "dojo/_base/lang"],

        function (Map, ArcGISDynamicMapServiceLayer, BlendRenderer, UniqueValueRenderer, esriConfig, TextSymbol, ProjectParameters, Draw, QueryTask, Legend, SimpleMarkerSymbol, SimpleLineSymbol, PictureFillSymbol, CartographicLineSymbol, TimeExtent, TimeSlider, TimeInfo, HorizontalSlider, SimpleFillSymbol,
          OpacitySlider, HorizontalSlider, Polyline, Point, Polygon, geodesicUtils, geometryEngine, Units, SpatialReference, Graphic, GraphicsLayer, Query, GeometryService, request, scaleUtils, SimpleRenderer, FeatureLayer, Navigation, LayerList, Measurement,
            HomeButton, InfoWindow, InfoTemplate, IdentifyTask, IdentifyParameters,
            dom, connect, JSON, on, parser, sniff, arrayUtils, Color, LabelClass, domReady, lang) {

            var pointMarkerLayer = [];
            var polylineMarkerLayer = [];
            var polygonMarkerLayer = [];


            map = new Map("mapDiv", {
                basemap: "satellite",
                center: [91.88, 25.57],
                zoom: 8
            });
            //  https://nw047gcs.rmsi.com:6443/arcgis/rest/services/meghalaya/MegInterventionSite/MapServer
            //  https://geoportal.rmsi.com/server/rest/services/meghalaya/MegInterventionSite/MapServer
            // https://nw047gcs.rmsi.com/arcgis/rest/services/meghalaya/MegInterventionSite/MapServer/1
            //   var meghalayaLayer = new ArcGISDynamicMapServiceLayer("", { "visible": true, "id": "meghalaya" });
            var StateLayer = new FeatureLayer(StateLayerURL, { "visible": true, "id": "state" });
            var DistrictLayer = new FeatureLayer(DistrictLayerURL, { "visible": true, "id": "district" });
            var BlockLayer = new FeatureLayer(BlockLayerURL, { "visible": true, "id": "block" });
            var VillageLayer = new FeatureLayer(VillageLayerURL, { "visible": true, "id": "village", "outFields": "[" * "]", });
            var LULCLayer = new FeatureLayer(LULCLayerURL, { "visible": true, "id": "lulc", "outFields": "[" * "]", });

            var PointLayer = new FeatureLayer(PointLayerURL,
                {
                    "visible": true,
                    "id": "pointlyr",
                    "outFields": ["*"]

                    //"outFields": ["OBJECTID","InterventionTypeID","InterventionSiteID"]
                });
            var PolylineLayer = new FeatureLayer(PolylineLayerURL,
                {
                    "visible": true, "id": "linelyr",
                    "outFields": ["*"]

                });
            var PolygonLayer = new FeatureLayer(PolygonLayerURL,
                {
                    "visible": true, "id": "polygonlyr",
                    "outFields": ["*"]

                });

            var lineSymbol = new SimpleLineSymbol(
         SimpleLineSymbol.STYLE_SOLID,
         new Color([255, 0, 0]), 3);

            map.on("load", function () {

                init();
                $("#InterventionSitePanelDiv").draggable(true);

            });

          
            $('#ddlDistrict').on('change', function () {

                districtid = $('#ddlDistrict :selected').val();
                if (districtid != 0) {
                    DistrictLayer.setDefinitionExpression("DistrictID =" + districtid + "");
                    ZoomToDistrict(districtid);
                }
                else {
                    DistrictLayer.setDefinitionExpression("");
                    DistrictLayer.refresh();
                    BlockLayer.setDefinitionExpression("");
                    BlockLayer.refresh();
                    map.setZoom(8);
                }
                ShowInterventionSiteData();
            });

            $('#ddlBlock').on('change', function () {

                blockid = $('#ddlBlock :selected').val();
                if (blockid != 0) {
                    BlockLayer.setDefinitionExpression("BlockID =" + blockid + "");
                    ZoomToBlock(blockid);
                }
                else {
                    BlockLayer.setDefinitionExpression("");
                    BlockLayer.refresh();
                    VillageLayer.setDefinitionExpression("");
                    VillageLayer.refresh();
                    districtid = $('#ddlDistrict :selected').val();
                    ZoomToDistrict(districtid);
                }
                ShowInterventionSiteData();

            });

            $('#ddlVillage').on('change', function () {

                
                villageid = $('#ddlVillage :selected').val();
                getGISDashboardData(villageid)
                if (villageid != 0) {
                    VillageLayer.setDefinitionExpression("VILLAGEID =" + villageid + "");
                    ZoomToVillage(villageid);
                }
                else {
                    VillageLayer.setDefinitionExpression("");
                    VillageLayer.refresh();
                    blockid = $('#ddlBlock :selected').val();
                    ZoomToBlock(blockid);
                }
                ShowInterventionSiteData();

            });

          
            function init() {

                var distid = "";
                var blkid = "";
                var vlgid = "";

                if (ROLEID != 1 && ROLEID != 2 && ROLEID != 3 && ROLEID != 4) {

                    vlgid = $('#ddlVillage :selected').val();
                    ZoomToVillage(vlgid);

                    PointLayer.setDefinitionExpression("VillageID IN (" + VILLAGEID + ")");
                    PolylineLayer.setDefinitionExpression("VillageID IN (" + VILLAGEID + ")");
                    PolygonLayer.setDefinitionExpression("VillageID in (" + VILLAGEID + ")");


                }
                if (ROLEID == 3) {
                    distid = $('#ddlDistrict :selected').val();
                    ZoomToDistrict(distid);
                }
                if (ROLEID == 4) {

                    blkid = $('#ddlBlock :selected').val();
                    ZoomToBlock(blkid);
                }

                if (ROLEID != 1 && ROLEID != 2) {

                    DistrictLayer.setDefinitionExpression("DistrictID =" + distid + "");
                    BlockLayer.setDefinitionExpression("BlockID =" + blkid + "");
                    VillageLayer.setDefinitionExpression("VILLAGEID =" + vlgid + "");
                }

               
                map.addLayers([StateLayer, DistrictLayer, BlockLayer, VillageLayer, PointLayer, PolylineLayer, PolygonLayer, LULCLayer]);

                map.reorderLayer(PolygonLayer, 7);
                map.reorderLayer(PolylineLayer, 6);
                map.reorderLayer(PointLayer, 5);
                map.reorderLayer(LULCLayer, 4);
                map.reorderLayer(VillageLayer, 3);
                map.reorderLayer(BlockLayer, 2);
                map.reorderLayer(DistrictLayer, 1);
                map.reorderLayer(StateLayer, 0);
            }

            function ZoomToVillage(villageid) {

                var queryZoom = new Query();

                queryZoom.where = "VILLAGEID = '" + villageid + "'";

                VillageLayer.selectFeatures(queryZoom, FeatureLayer.SELECTION_NEW, function (result) {


                    var FeatureResult = result[0].geometry;
                    if (FeatureResult)
                        map.setExtent(result[0].geometry.getExtent());
                    else
                    $('#alertMsg').bs_warning("This village boundary is not found in database.");

                }, function (error) {                  
                });
            }
            function ZoomToDistrict(districtid) {

                var queryZoom = new Query();
                queryZoom.where = "DistrictID = " + districtid + "";

                DistrictLayer.selectFeatures(queryZoom, FeatureLayer.SELECTION_NEW, function (result) {


                    var FeatureResult = result[0].geometry;
                    map.setExtent(result[0].geometry.getExtent());


                }, function (error) {
                });
            }
            function ZoomToBlock(blockid) {

                var queryZoom = new Query();
                queryZoom.where = "BlockID = " + blockid + "";

                BlockLayer.selectFeatures(queryZoom, FeatureLayer.SELECTION_NEW, function (result) {


                    var FeatureResult = result[0].geometry;
                    map.setExtent(result[0].geometry.getExtent());


                }, function (error) {
                   
                });
            }

            var navToolbar;
            $(document).ready(function () {

                navToolbar = new Navigation(map);
                on(navToolbar, "onExtentHistoryChange", extentHistoryChangeHandler);

                $("#zoomin").click(function () {
                    navToolbar.activate(Navigation.ZOOM_IN);
                });

                $("#zoomout").click(function () {
                    navToolbar.activate(Navigation.ZOOM_OUT);
                });

                $("#zoomfullext").click(function () {
                    map.setZoom(7);
                });

                $("#zoomprev").click(function () {
                    navToolbar.zoomToPrevExtent();
                });

                $("#zoomnext").click(function () {
                    navToolbar.zoomToNextExtent();
                });

                $("#pan").click(function () {
                    navToolbar.activate(Navigation.PAN);
                });

                function extentHistoryChangeHandler() {
                    registry.byId("zoomprev").disabled = navToolbar.isFirstExtent();
                    registry.byId("zoomnext").disabled = navToolbar.isLastExtent();
                }

                var home = new HomeButton(
               {
                   map: map
               }, "HomeButton");
                home.startup();

                var measurement = new Measurement({
                    map: map,
                }, dom.byId("MeasurementDiv"));
                measurement.startup();

            });

            var LayerListWidget;
            var layersetValue = [];

            LayerListWidget = new LayerList({
                map: map,
                layers: layersetValue,
                showLegend: true,
                showOpacitySlider: true,
                showSubLayers: true
            }, "LayerListDiv");

            on(map, "layers-add-result", function (layers) {
                LayerListWidget.refresh();

                for (var i = 0; i < layers.layers.length; i++) {

                    layersetValue[i] =
                                {
                                    layer: layers.layers[i].layer,
                                    showLegend: true,
                                    showSubLayers: true,
                                    visibility: true
                                }

                }

                LayerListWidget.startup();
            });

            $("#InfoIcon").click(function () {

                if ($('#InfoIcon').hasClass('active')) {
                    $('#InfoIcon').removeClass('active');
                    map.infoWindow.hide();
                    dojo.disconnect(OnClickInfo);

                }
                else {
                    $('#InfoIcon').addClass('active');
                    FindInfo();
                }

            });

            var OnClickInfo;
            var identifyTask, identifyParams;

            function FindInfo() {

                OnClickInfo = map.on("click", executeIdentifyTask);

            }

            function executeIdentifyTask(event) {

                var count = 0;
                identifyTask = new IdentifyTask(DynamicLayerURL);
                identifyParams = new IdentifyParameters();
                identifyParams.tolerance = 4;
                identifyParams.geometryPrecision = 1;
                identifyParams.layerIds = [1, 3, 4, 5, 6, 7, 8, 9];
                identifyParams.layerOption = IdentifyParameters.LAYER_OPTION_VISIBLE;
                identifyParams.width = map.width;
                identifyParams.height = map.height;
                identifyParams.geometry = event.mapPoint;
                identifyParams.mapExtent = map.extent;
                var deferred = identifyTask.execute(identifyParams).addCallback(function (response) {


                    return arrayUtils.map(response, function (result) {


                        var feature = result.feature;
                        var layerName = result.layerName;


                        if (layerName == 'State Boundary') {

                            var StateDtlTemplate = new InfoTemplate("Layer Information", "STATE  : ${State}");

                            feature.setInfoTemplate(StateDtlTemplate);
                        }
                        else if (layerName == 'Block Boundary') {

                            var BlockDtlTemplate = new InfoTemplate("Layer Information", "BLOCK : ${BlockName}");

                            feature.setInfoTemplate(BlockDtlTemplate);
                        }
                        else if (layerName == 'Districts Boundary') {

                            var SubCountyDtlTemplate = new InfoTemplate("Layer Information", "DISTRICT : ${DistrictName}");

                            feature.setInfoTemplate(SubCountyDtlTemplate);
                        }
                        else if (layerName == 'Villages') {

                            var CountyDtlTemplate = new InfoTemplate("Layer Information", "VILLAGE : ${VILLAGENAME} <br/> DISTRICT : ${DISTRICTNAME}");

                            feature.setInfoTemplate(CountyDtlTemplate);
                        }
                        else if (layerName == 'Intervention Site Points') {

                            var CountyDtlTemplate = new InfoTemplate("Layer Information", "InterventionSiteName  : ${InterventionSiteName}  <br/> Created Date : ${CreatedDate}");

                            feature.setInfoTemplate(CountyDtlTemplate);
                        }
                        else if (layerName == 'Intervention Site Lines') {

                            var CountyDtlTemplate = new InfoTemplate("Layer Information", "InterventionSiteName  : ${InterventionSiteName} <br/> Created Date : ${CreatedDate}");

                            feature.setInfoTemplate(CountyDtlTemplate);
                        }
                        else if (layerName == 'Intervention Site Polygon') {

                            var CountyDtlTemplate = new InfoTemplate("Layer Information", "InterventionSiteName  : ${InterventionSiteName} <br/> Area : ${Area_Ha} <br/> Created Date : ${CreatedDate}");

                            feature.setInfoTemplate(CountyDtlTemplate);
                        }
                        else if (layerName == 'LULC Map') {

                            var CountyDtlTemplate = new InfoTemplate("Layer Information", "Name : ${Name}");

                            feature.setInfoTemplate(CountyDtlTemplate);
                        }
                        if (layerName == 'Villages' || layerName == 'State Boundary' || layerName == 'Block Boundary' || layerName == 'Districts Boundary' || layerName == 'Intervention Site Points' || layerName == 'Intervention Site Lines' || layerName == 'Intervention Site Polygon' || layerName == 'LULC Map') {

                            //  map.infoWindow.setTitle("Layer Information");
                            return feature;

                        }

                    });


                });
                //  map.infoWindow.setTitle("Layer Information");
                map.infoWindow.setFeatures([deferred]);
                map.infoWindow.show(event.mapPoint);
            }


            var markerSymbol = new SimpleMarkerSymbol(SimpleMarkerSymbol.STYLE_CIRCLE, 10,
             new SimpleLineSymbol(SimpleLineSymbol.STYLE_SOLID,
            new Color([255, 0, 0]), 1),
            new Color([0, 255, 0, 0.25]));

            var SelectionMarkerSymbol = new SimpleMarkerSymbol(
            SimpleMarkerSymbol.STYLE_CIRCLE, 12, new SimpleLineSymbol(SimpleLineSymbol.STYLE_SQUARE, new Color([66, 255, 242]), 1), new Color([66, 255, 242]));

            var lineSymbol = new SimpleLineSymbol(SimpleLineSymbol.STYLE_SOLID, new Color([255, 0, 0]), 3);
            var SelectionlineSymbol = new SimpleLineSymbol(SimpleLineSymbol.STYLE_SOLID, new Color([66, 255, 242]), 3);

            var fillSymbol = new SimpleFillSymbol(SimpleFillSymbol.STYLE_SOLID, new SimpleLineSymbol(SimpleLineSymbol.STYLE_SOLID, new Color([255, 0, 0]), 2), new Color([255, 255, 0, 0.25]));
            var SelectionfillSymbol = new SimpleFillSymbol(SimpleFillSymbol.STYLE_SOLID, new SimpleLineSymbol(SimpleLineSymbol.STYLE_SQUARE, new Color([66, 255, 242]), 2), new Color([66, 255, 242]));

            function ShowInterventionSitesOnMap() {

                var url = GetInterventionSiteData.InterventionSiteURL;

                $.post(url, { VillageId: VILLAGEID, RoleId: ROLEID, DistrictId: DISTRICTID, BlockId: BLOCKID }, function (data) {

                    siteData = JSON.parse(data);

                    if (siteData.GeomString != null && siteData.GeomString.length != 0) {
                        ShowMarkedSitesOnMap(siteData.GeomString);
                    }
                });

            }


            $("#ShowHideSites").click(function () {
                SitesVisibility(this);
            });

            function SitesVisibility(chkId) {
                try {
                    var Id = chkId.id;

                    if (document.getElementById(Id).checked == true) {
                        ShowInterventionSitesOnMap();
                    }
                    else if (document.getElementById(Id).checked == false) {
                        map.graphics.clear();
                    }
                }
                catch (err) {

                    DisplayError(err.Message);
                }
            }

            $("#Point").click(function () {

                tb = new Draw(map);
                map.disableMapNavigation();
                tb.activate("point");
                tb.on("draw-end", addGraphic);
                $(this).addClass('current-active');
                $("#Polyline").removeClass('current-active');
                $("#Polygon").removeClass('current-active');

            });

            $("#Polyline").click(function () {

                tb = new Draw(map);
                //  var tool = evt.target.id.toLowerCase();
                map.disableMapNavigation();
                tb.activate("polyline");
                tb.on("draw-end", addGraphic);
                $("#Point").removeClass('current-active');
                $("#Polygon").removeClass('current-active');
                $(this).addClass('current-active')

            });

            $("#Polygon").click(function () {

                tb = new Draw(map);
                //  var tool = evt.target.id.toLowerCase();
                map.disableMapNavigation();
                tb.activate("polygon");
                tb.on("draw-end", addGraphic);
                $("#Point").removeClass('current-active');
                $("#Polyline").removeClass('current-active');
                $(this).addClass('current-active')

            });

            var geomFeature = "";
            var graphic = "";
            function addGraphic(evt) {
                //deactivate the toolbar and clear existing graphics 
                tb.deactivate();
                map.enableMapNavigation();

                // figure out which symbol to use
                var symbol;
                if (evt.geometry.type === "point" || evt.geometry.type === "multipoint") {
                    symbol = markerSymbol;
                } else if (evt.geometry.type === "line" || evt.geometry.type === "polyline") {
                    symbol = lineSymbol;
                }
                else {
                    symbol = fillSymbol;
                }
                // map.graphics.add(new Graphic(evt.geometry, symbol));

                SaveGeometry(evt.geometry, symbol);
                geomFeature = evt.geometry;

            }

            var objAttribute = [];
            var xpoint = "";
            var ypoint = "";
            var xminL = "";
            var yminL = "";
            var xmaxL = "";
            var ymaxL = "";
            var geom = "";
            var str = "";
            var p;
            var area = "";



            function SaveGeometry(geom, symbol) {

                if (geom.type == "point") {
                    xpoint = geom.x;
                    ypoint = geom.y;

                    var point = geom;
                    var outSR = new SpatialReference(102100);

                    var gsvc = new GeometryService("https://utility.arcgisonline.com/ArcGIS/rest/services/Geometry/GeometryServer");

                    gsvc.project([point], outSR, function (projectedPoints) {
                        pt = projectedPoints[0];
                        projectToLatLong(pt);
                    });

                    function projectToLatLong(pt) {
                        var outSR = new SpatialReference(4326);
                        var params = new ProjectParameters();
                        params.geometries = [pt.normalize()];
                        params.outSR = outSR;

                        gsvc.project(params, function (projectedPoints) {
                            pt = projectedPoints[0];
                            dom.byId("latlong").innerHTML = "<span>Latitude: </span> " +
                              pt.y.toFixed(3) + "<br><span>Longitude:</span>" + pt.x.toFixed(3);
                        });
                    }

                    p = 0;
                }
                else if (geom.type == "polyline") {

                    //xminL = geom.cache._extent.xmin;
                    //xmaxL= geom.cache._extent.xmax;
                    //yminL = geom.cache._extent.ymin;
                    //ymaxL = geom.cache._extent.ymax;
                    xminL = geom.paths[0][0][0];
                    yminL = geom.paths[0][0][1];
                    xmaxL = geom.paths[0][1][0];
                    ymaxL = geom.paths[0][1][1];
                    p = 1;
                }

                else if (geom.rings[0].length != 0 && geom.type == "polygon") {

                    for (var i = 0; i < geom.rings[0].length; i++) {
                        str += geom.rings[0][i][0] + " " + geom.rings[0][i][1] + ',';

                    }
                    area = geometryEngine.geodesicArea(geom, "hectares").toFixed(3);
                    str = str.replace(/,\s*$/, "");
                    p = 2;

                    var line = geom;
                    var outSR = new SpatialReference(102100);

                    var gsvc = new GeometryService("https://utility.arcgisonline.com/ArcGIS/rest/services/Geometry/GeometryServer");

                    gsvc.project([line], outSR, function (projectedPoints) {
                        pt = projectedPoints[0];
                        projectToLatLong(pt);
                    });

                    function projectToLatLong(pt) {
                        var outSR = new SpatialReference(4326);
                        var params = new ProjectParameters();
                        params.geometries = pt;
                        params.outSR = outSR;

                        gsvc.project(params, function (projectedPoints) {
                            pt = projectedPoints[0];
                            ///   dom.byId("latlong").innerHTML = "<span>Latitude: </span> " +
                            //  pt.y.toFixed(3) + "<br><span>Longitude:</span>" + pt.x.toFixed(3);
                        });
                    }


                }

                QueryRelatedFeatures(geom, symbol);

            }

            function QueryRelatedFeatures(geom, symbol) {

                var geometryextent;

                var query = new Query();
                query.where = "1=1";
                query.outSpatialReference = new SpatialReference(102100);
                query.outFields = ["*"];
                query.returnGeometry = "true";
                query.geometry = geom;

                if (ROLEID == 1 || ROLEID == 2) //sysadmin && spmu
                {
                    query.objectIds = [1];

                    try {
                        StateLayer.queryFeatures(query, RESULT);
                    }
                    catch (err) {
                        alert(err);
                    }
                }

                else if (ROLEID == 3) //dpmu
                {
                    query.objectIds = [DISTRICTID];

                    try {
                        DistrictLayer.queryFeatures(query, RESULT);

                    }
                    catch (err) {
                        alert(err);
                    }
                }
                else if (ROLEID == 4) //bpmu
                {
                    query.objectIds = [27];

                    try {
                        BlockLayer.queryFeatures(query, RESULT);
                    }
                    catch (err) {
                        alert(err);
                    }
                }

                else //other users
                {
                    query.objectIds = [VILLAGEID];

                    try {
                        VillageLayer.queryFeatures(query, RESULT);

                    }
                    catch (err) {
                        alert(err);
                    }
                }


                function RESULT(result) {

                    var resultFeature = result.features;
                    geometryextent = esri.graphicsExtent(resultFeature);

                    if (geometryextent != null && geometryextent != "undefined") {

                        graphic = new Graphic(geom, symbol);
                        map.graphics.add(graphic);
                        // map.graphics.add(new Graphic(geom, symbol));
                        clearAtrributeEditor();
                        $("#pane2").show();
                    }
                    else {
                        alert("User do not have permission to mark sites at this location");
                        $("#pane2").hide();
                        map.graphics.remove(graphic)
                    }

                }


            }

            function clearAtrributeEditor() {

                $("#ddlSiteId").val(0);
                $("#ddlActivityId").val(0);
                $("#ddlLandownerId").val(0);
                $("#ddlPriorityId").val(0);
                $("#sitenameId").val("");
                $("#startdateId").val("");
                $("#enddateId").val("");
                $("#amountid").val("");

            }


            $("#hideBtn2").click(function () {

                clearAtrributeEditor();

                map.graphics.remove(graphic);

            });

            $("#SaveAttributes").click(function () {

                checkValidations();

            });

            function checkValidations() {

                if ($("#ddlSiteId option:selected").val() != 0 && $("#ddlActivityId option:selected").val() != 0 &&
                    $("#ddlLandownerId option:selected").val() != 0 && $("#ddlPriorityId option:selected").val() != 0 &&
                    $("#sitenameId").val() != "" && $("#startdateId").val() != "" && $("#enddateId").val() != "" &&
                     $("#amountid").val() != "") {

                    SaveMarkedSiteAttributes();

                }
                else {

                    alert("Please fill all the mandatory fields");

                }

            }
            function SaveMarkedSiteAttributes() {

                if (p == 0) {
                    objAttribute = [
                  {
                      VillageID: 274892, InterventionTypeID: $("#ddlSiteId option:selected").val(),
                      InterventionActivityTypeID: $("#ddlActivityId option:selected").val(), LandOwnershipID: $("#ddlLandownerId option:selected").val(), PriorityTypeID: $("#ddlPriorityId option:selected").val(), InterventionSiteName: $("#sitenameId").val(),
                      PlannedStartDate: $("#startdateId").val(), PlannedEndDate: $("#enddateId").val(), Latitude: ypoint, Longitude: xpoint, Area_Ha: 0, Amount: $("#amountid").val(), CreatedBy: USERID,
                      GeometryType: 1, UserID: USERID, RoleID: ROLEID, geomstring: "abc"
                  },
                    ];
                }
                else if (p == 1) {

                    objAttribute = [
                  {
                      VillageID: 274892, InterventionTypeID: $("#ddlSiteId option:selected").val(),
                      InterventionActivityTypeID: $("#ddlActivityId option:selected").val(), LandOwnershipID: $("#ddlLandownerId option:selected").val(), PriorityTypeID: $("#ddlPriorityId option:selected").val(), InterventionSiteName: $("#sitenameId").val(),
                      PlannedStartDate: $("#startdateId").val(), PlannedEndDate: $("#enddateId").val(), Area_Ha: 0, Amount: $("#amountid").val(), CreatedBy: USERID,
                      xmin: xminL, ymin: yminL, xmax: xmaxL, ymax: ymaxL, GeometryType: 2, UserID: USERID, RoleID: ROLEID, geomstring: "abc"
                  },
                    ];

                }
                else if (p == 2) {

                    objAttribute = [
                  {
                      VillageID: 274892, InterventionTypeID: $("#ddlSiteId option:selected").val(),
                      InterventionActivityTypeID: $("#ddlActivityId option:selected").val(), LandOwnershipID: $("#ddlLandownerId option:selected").val(), PriorityTypeID: $("#ddlPriorityId option:selected").val(), InterventionSiteName: $("#sitenameId").val(),
                      PlannedStartDate: $("#startdateId").val(), PlannedEndDate: $("#enddateId").val(), Area_Ha: area, Amount: $("#amountid").val(), CreatedBy: USERID,
                      geomstring: str, GeometryType: 3, UserID: USERID, RoleID: ROLEID
                  },
                    ];

                }

                var url = SaveInterventionSiteData.InterventionSiteDataURL;
                var siteData = "";
                $.post(url, { Attributes: JSON.stringify(objAttribute) }, function (data) {

                    siteData = JSON.parse(data);

                    if (siteData != "" && siteData != undefined) {
                        if (siteData.ReturnMessage[0].ReturnMessage == "SUCCESS") {
                            alert("Intervention Sites marked Successfully");
                        }
                        if (siteData.GeomString != null) {
                            ShowMarkedSitesOnMap(siteData.GeomString);
                        }
                    }
                });

            }

            function ShowMarkedSitesOnMap(MarkedSites) {

                for (var i = 0; i < MarkedSites.length; i++) {

                    var SavedSite = "";

                    if (MarkedSites[i].ReturnGeomString.search("POINT") != -1) {

                        SavedSite = MarkedSites[i].ReturnGeomString.replace('POINT', '');
                        SavedSite = SavedSite.replace(/[{()}]/g, '');
                        SavedSite = SavedSite.trim();
                        var latlng = SavedSite.split(' ');

                        var point = new Point(latlng[0], latlng[1], new SpatialReference({ wkid: 102100 }));


                        map.graphics.add(new Graphic(point, SelectionMarkerSymbol));

                        pointMarkerLayer.push(point);

                        //Pointgraphic.on("click", function (event) {
                        //    alert("hii");
                        //});

                        //point.on("click", pointMarkerLayerClickEvent);
                        //  on(Pointgraphic, "click", pointMarkerLayerClickEvent);
                        // var handle = on(point, "click", pointMarkerLayerClickEvent);
                        // dojo.connect(Pointgraphic, "onclick", pointMarkerLayerClickEvent);
                        // pointMarkerLayer.push(MarkedSites[i]);

                        //on(point, "click", pointMarkerLayerClickEvent);

                        //on(Pointgraphic, "click", function (e) {

                        //    alert(e);
                        //});

                    }

                    else if (MarkedSites[i].ReturnGeomString.search("LINESTRING") != -1) {

                        SavedSite = MarkedSites[i].ReturnGeomString.replace('LINESTRING', '');
                        SavedSite = SavedSite.replace(/[{()}]/g, '');
                        SavedSite = SavedSite.trim();
                        var polylineGeomPath = SavedSite.split(',');
                        //latlng = SavedSite.split(' ');
                        var Pline = new Polyline(new SpatialReference({ wkid: 102100 }));
                        var arr = [];


                        for (j = 0; j < polylineGeomPath.length; j++) {

                            var latLng = polylineGeomPath[j].trim();
                            latLng = latLng.split(" ");

                            arr.push([parseInt(latLng[0]), parseInt(latLng[1])]);

                        }
                        Pline.addPath(arr);
                        map.graphics.add(new Graphic(Pline, SelectionlineSymbol));

                        polylineMarkerLayer.push(Pline);

                    }
                    else if (MarkedSites[i].ReturnGeomString.search("POLYGON") != -1) {

                        var poly = new Polygon(new SpatialReference({ wkid: 102100 }));

                        SavedSite = MarkedSites[i].ReturnGeomString.replace('POLYGON', '');
                        SavedSite = SavedSite.replace(/[{()}]/g, '');
                        SavedSite = SavedSite.trim();


                        var polygonGeomPath = SavedSite.split(",");
                        var arr = [];
                        for (j = 0; j < polygonGeomPath.length; j++) {

                            var latLng = polygonGeomPath[j].trim();
                            latLng = latLng.split(" ");
                            arr.push([parseInt(latLng[0]), parseInt(latLng[1])]);
                            // poly.addPath([[latlng[0]], [latlng[1]]]);

                        }
                        poly.addRing(arr);
                        map.graphics.add(new Graphic(poly, SelectionfillSymbol));

                        polygonMarkerLayer.push(poly);

                    }


                }

            }

            ///--------------------------------To Add Layers On Map------------------------------------------------------------------------///

            var portalUrl = "https://www.arcgis.com";

            esriConfig.defaults.io.proxyUrl = "/proxy/";

            on(dom.byId("uploadForm"), "change", function (event) {
                var fileName = event.target.value.toLowerCase();

                if (sniff("ie")) { //filename is full path in IE so extract the file name
                    var arr = fileName.split("\\");
                    fileName = arr[arr.length - 1];
                }
                if (fileName.indexOf(".zip") !== -1) {//is file a zip - if not notify user
                    generateFeatureCollection(fileName);
                }
                else {
                    dom.byId('upload-status').innerHTML = '<p style="color:red">Add shapefile as .zip file</p>';
                }
            });
            function generateFeatureCollection(fileName) {
                var name = fileName.split(".");

                name = name[0].replace("c:\\fakepath\\", "");

                //Define the input params for generate see the rest doc for details
                //http://www.arcgis.com/apidocs/rest/index.html?generate.html
                var params = {
                    'name': name,
                    'targetSR': map.spatialReference,
                    'maxRecordCount': 1000,
                    'enforceInputFileSizeLimit': true,
                    'enforceOutputJsonSizeLimit': true
                };


                var extent = scaleUtils.getExtentForScale(map, 40000);
                var resolution = extent.getWidth() / map.width;
                params.generalize = true;
                params.maxAllowableOffset = resolution;
                params.reducePrecision = true;
                params.numberOfDigitsAfterDecimal = 0;

                var myContent = {
                    'filetype': 'shapefile',
                    'publishParameters': JSON.stringify(params),
                    'f': 'json',
                    'callback.html': 'textarea'
                };

                //use the rest generate operation to generate a feature collection from the zipped shapefile
                request({
                    url: portalUrl + '/sharing/rest/content/features/generate',
                    content: myContent,
                    form: dom.byId('uploadForm'),
                    handleAs: 'json',
                    load: lang.hitch(this, function (response) {
                        if (response.error) {
                            errorHandler(response.error);
                            return;
                        }
                        var layerName = response.featureCollection.layers[0].layerDefinition.name;
                        dom.byId('upload-status').innerHTML = '<b>Loaded: </b>' + layerName;
                        addShapefileToMap(response.featureCollection);
                    }),
                    error: lang.hitch(this, errorHandler)
                });
            }
            function errorHandler(error) {
                alert(error.message);
            }
            function addShapefileToMap(featureCollection) {

                var fullExtent;
                var layers = [];

                arrayUtils.forEach(featureCollection.layers, function (layer) {
                    var infoTemplate = new InfoTemplate("Details", "${*}");
                    var featureLayer = new FeatureLayer(layer, {
                        infoTemplate: infoTemplate,
                        // id: "l1"
                    });
                    //associate the feature with the popup on click to enable highlight and zoom to
                    featureLayer.on('click', function (event) {
                        map.infoWindow.setFeatures([event.graphic]);
                    });
                    //change default symbol if desired. Comment this out and the layer will draw with the default symbology
                    changeRenderer(featureLayer);
                    fullExtent = fullExtent ?
                      fullExtent.union(featureLayer.fullExtent) : featureLayer.fullExtent;
                    layers.push(featureLayer);
                });
                map.addLayers(layers);
                map.setExtent(fullExtent.expand(1.25), true);

                dom.byId('upload-status').innerHTML = "";
            }

            function changeRenderer(layer) {
                //change the default symbol for the feature collection for polygons and points
                var symbol = null;
                switch (layer.geometryType) {
                    case 'esriGeometryPoint':
                        symbol = new PictureMarkerSymbol({
                            'angle': 0,
                            'xoffset': 0,
                            'yoffset': 0,
                            'type': 'esriPMS',
                            'url': 'https://static.arcgis.com/images/Symbols/Shapes/BluePin1LargeB.png',
                            'contentType': 'image/png',
                            'width': 20,
                            'height': 20
                        });
                        break;
                    case 'esriGeometryPolygon':
                        symbol = new SimpleFillSymbol(SimpleFillSymbol.STYLE_SOLID,
                          new SimpleLineSymbol(SimpleLineSymbol.STYLE_SOLID,
                            new Color([112, 112, 112]), 1), new Color([216, 191, 216, 0.25]));
                        break;
                }
                if (symbol) {
                    layer.setRenderer(new SimpleRenderer(symbol));
                }
            }


            on(map, "layers-add-result", function (layers) {

                for (var i = 0; i < layers.layers.length; i++) {

                    var id = layers.layers[i].layer.layerId;
                    var layerid = layers.layers[i].layer.id;

                    //  var TableRow = $("<tr style='border:1px solid black; id=" + layerid + "'><td><input type='radio' name='RadioLayers' id=" + layerid + "/></td><td id=" + layerid + " style='font-size:14px; font-weight:200'>" + layers.layers[i].layer._name + "</td><td><input class='up' type='image' value='" + layerid + "' src='../../Areas/MapView/assets/images/map-img/up.jpg'/></td><td><input class='down' type='image' value='" + layerid + "' src='../../Areas/MapView/assets/images/map-img/down.jpg'/></td></tr>");
                    var TableRow = $("<tr style='border:1px solid black; id=" + layerid + "'><td><input type='radio' name='RadioLayers' id=" + layerid + "/></td><td id=" + layerid + " style='font-size:14px; font-weight:200'>" + layers.layers[i].layer._name + "</td><td><a class='up' value='" + layerid + "' href='#' id='" + layerid + "' ><i class='fa fa-arrow-up'  title='View'></i></a></td><td><a class='down' value='" + layerid + "' href='#' id='" + layerid + "' ><i class='fa fa-arrow-down'  title='View'></i></a></td></tr>");
                    $("#mapLayers").append(TableRow);


                    if (id == 1 || id == 3 || id == 4 || id == 5 || id == 6 || id == 7 || id == 8 || id == 9) {

                        var radios = document.getElementsByName('RadioLayers');
                        for (var j = 0, r = radios, l = r.length; j < l; j++) {
                            radios[i].disabled = true;
                        }
                    }
                }

                $(".up,.down").click(function () {

                    var row = $(this).parents("tr:first");

                    //  var lid = $(this).val();
                    var lid = $(this)[0].id;

                    if ($(this).is(".up")) {

                        var mapLayer1 = map.getLayer(lid);

                        var currentrowidx = $('#' + lid + '').parent().index();

                        if (row.prev() != null && row.prev() != 'undefined' && row.prev().length != 0) {
                            var prevrowidx = row.prev()[0].rowIndex;

                            var prevVal = row.prev()[0].cells[1].id;
                            var mapLayer2 = map.getLayer(prevVal);

                            map.reorderLayer(mapLayer1, prevrowidx);
                            map.reorderLayer(mapLayer2, currentrowidx);

                            row.insertBefore(row.prev());
                        }


                    } else if ($(this).is(".down")) {

                        var mapLayer1 = map.getLayer(lid);

                        var currentrowidx = $('#' + lid + '').parent().index();

                        if (row.next() != null && row.next() != 'undefined' && row.next().length != 0) {

                            var nxtrowidx = row.next()[0].rowIndex;

                            var nxtVal = row.next()[0].cells[1].id;
                            var mapLayer2 = map.getLayer(nxtVal);

                            map.reorderLayer(mapLayer1, nxtrowidx);
                            map.reorderLayer(mapLayer2, currentrowidx);

                            row.insertAfter(row.next());
                        }

                    }
                });

            });



            $("#RemoveLayer").click(function () {

                var idlayer = $('input[type=radio][name=RadioLayers]:checked').attr('id');
                idlayer = idlayer.replace('/', '');
                idlayer = idlayer.trim();

                var mapLayer = map.getLayer(idlayer);
                map.removeLayer(mapLayer);

                var row_index = $('#' + idlayer + '').parent().index();
                document.getElementById("addremoveTab").deleteRow(row_index);

            });

            //--------------------------------------------------Query Builder---------------------------------------------------------------//

            $("#QueryBuilderBtn").click(function () {

                $("#Loading_Spinner_QB").hide();
                $('#QBvalueID').empty();
                // $("#QueryBuilderDiv").toggle();
                ResetQueryBuilder();

            });

            //-----Function to Reset query builder------//
            function ResetQueryBuilder() {

                $('#QBLayername').val(0);
                $('#QBattributeID').empty();
                $('#QBvalueID').val();
                $('#QBGraphicSelection').val(1);
                $('#QBTextArea').val('').empty();
                $("#message").text("");
            }
            //-------Function Ended----------------------//  

            $("#QBLayername").change(function () {
                var layername = this.value;
                QBChangeLayer(layername);
            });

            function QBChangeLayer(Layer) {

                var HtmlFieldID = "";

                // var ILayerFields = ["InterventionSiteName", "InterventionType", "InterventionActivityType", "LandOwnershipType", "PriorityType"];
                var ILayerFields = ["InterventionSiteName", "InterventionType", "InterventionActivityType", "LandOwnershipType", "PriorityType"];

                if (Layer == "IPoints" || Layer == "ILines" || Layer == "IPolygons") {
                    for (var i = 0; i < ILayerFields.length; i++) {

                        var fieldids = "";
                        if (ILayerFields[i] == "InterventionType") {

                            fieldids = "InterventionTypeID";
                        }
                        else if (ILayerFields[i] == "InterventionActivityType") {

                            fieldids = "InterventionActivityTypeID";
                        }
                        else if (ILayerFields[i] == "LandOwnershipType") {

                            fieldids = "LandOwnershipTypeID";
                        }
                        else if (ILayerFields[i] == "PriorityType") {

                            fieldids = "PriorityTypeID";
                        }
                        else {
                            fieldids = "InterventionSiteName";
                        }

                        HtmlFieldID += "<option value='" + fieldids + "'>" + ILayerFields[i] + "</option>";
                    }
                    $("#QBattributeID").empty().append(HtmlFieldID);
                    $('#QBTextArea').val('').empty();
                }
                else if (Layer == "0") {

                    $("#QBattributeID").empty();
                    $("#QBvalueID").empty();
                    $('#QBTextArea').val('').empty();
                }

            }


            $("#QBClear").click(function () {
                $('#QBvalueID').empty();
                $("#QBTextArea").val("");
            });

            $("#QBattributeID").dblclick(function () {
                var text = $('#QBTextArea').val();
                $('#QBTextArea').val(text + " " + $(this).val());
            });

            $(".Operators").click(function () {
                var text = $('#QBTextArea').val();
                $('#QBTextArea').val(text + " " + $(this).val());
            });

            $("#QBvalueID").dblclick(function () {
                var text = $('#QBTextArea').val();
                $('#QBTextArea').val(text + " " + $(this).val());
            });

            $("#QBClearSelection").click(function () {

                map.graphics.clear();
                VillageLayer.clearSelection();
                PointLayer.clearSelection();
                PolylineLayer.clearSelection();
                PolygonLayer.clearSelection();

                PointLayer.refresh();
                PolylineLayer.refresh();
                PolygonLayer.refresh();

            });

            $("#ResultPaneClose1").click(function () {
                $("#QBResultPanel").hide();
            });

            $("#QBSave").click(function () {
                $("#pane7").show();
            });

            $("#QBLoad").click(function () {
                $("#pane8").show();
            });


            ////-------------------------------------------------Query Creation-------------------------------------------------------------//

            var LayerSelectionPoint;
            var LayerSelectionLine;
            var LayerSelectionPolygon;
            var featureSelection;
            LayerSelectionPoint = PointLayer.on("selection-complete", inputFeatureLayerLayerSelectionComplete);
            LayerSelectionLine = PolylineLayer.on("selection-complete", inputFeatureLayerLayerSelectionComplete);
            LayerSelectionPolygon = PolygonLayer.on("selection-complete", inputFeatureLayerLayerSelectionComplete);


            $("#QBApply").click(function () {

                if ($('#QBGraphicSelection').val() == "0") {
                    $("#message").text("*Select Graphic Selection Type");
                }
                else if ($('#QBattributeID').val() == "0" || $('#QBTextArea').val() == "") {
                    $("#QBResultPanel").hide();
                    $("#message").text("*Select Proper Query to Execute");
                }
                else {
                    SQLQueryCreation();
                }
            });

            function SQLQueryCreation() {

                $("#Loading_Spinner_QB").show();
                var query = new Query();
                var data = $('#QBTextArea').val().toString();
                query.where = data;
                InitiateSelectionProcess(query);

            }
            var inputFeatureLayer;


            function InitiateSelectionProcess(query) {

                if ($("#QBLayername option:selected").val() == "Village") {
                    inputFeatureLayer = VillageLayer;
                }
                if ($("#QBLayername option:selected").val() == "IPoints") {
                    inputFeatureLayer = PointLayer;
                }
                if ($("#QBLayername option:selected").val() == "ILines") {
                    inputFeatureLayer = PolylineLayer;
                }
                if ($("#QBLayername option:selected").val() == "IPolygons") {
                    inputFeatureLayer = PolygonLayer;
                }

                ApplySelection(inputFeatureLayer, query);
            }

            function ApplySelection(inputFeatureLayer, query) {

                var LayerSelection;
                if ($('#QBGraphicSelection').val() == 1) {
                    NewSelection(inputFeatureLayer, query);

                }
                else if ($('#QBGraphicSelection').val() == 2) {
                    AddSelection(inputFeatureLayer, query);

                }
                else if ($('#QBGraphicSelection').val() == 3) {
                    RemoveSelection(inputFeatureLayer, query);
                }
                else if ($('#QBGraphicSelection').val() == 4) {
                    SelectFromSelection(inputFeatureLayer, query);
                }

            }

            function NewSelection(inputFeatureLayer, query) {

                map.graphics.clear();
                inputFeatureLayer.clearSelection();
                inputFeatureLayer.refresh();
                inputFeatureLayer.selectFeatures(query, FeatureLayer.SELECTION_NEW, function (result) {

                }, function (error) {
                    $("#message").text("Select Proper Query to Execute");
                    $("#Loading_Spinner_QB").hide();
                });

            }

            function AddSelection(inputFeatureLayer, query) {
                inputFeatureLayer.selectFeatures(query, FeatureLayer.SELECTION_ADD, function (result) {

                });
            }

            function SelectFromSelection(inputFeatureLayer, query) {

                if (mapSelection == 0) {

                    $("#message").text("Need Selection on Map");
                    $("#Loading_Spinner_QB").hide();

                }

                else {
                    try {
                        var FeaturesObjectId = "";
                        for (i = 0; i < mapSelection.length; i++) {
                            FeaturesObjectId = FeaturesObjectId + "," + mapSelection[i].attributes.OBJECTID;
                        }
                        var FeaturesObjectIdNew = FeaturesObjectId.replace(/(^,)|(,$)/g, "");
                        var array = FeaturesObjectIdNew.split(',');


                        var NewObjectId = "";
                        var NewObjectId1 = "";
                        var NewArray = "";
                        var NewArray1 = "";

                        inputFeatureLayer.queryFeatures(query, function (resultquery) {

                            for (var j = 0; j < resultquery.features.length; j++) {
                                NewObjectId = NewObjectId + "," + resultquery.features[j].attributes.OBJECTID;
                                NewObjectId1 = NewObjectId.replace(/(^,)|(,$)/g, "");
                                NewArray = NewObjectId1.split(',');

                            }

                            var objids = "";
                            for (k = 0; k < array.length; k++) {

                                for (m = 0; m < NewArray.length; m++) {

                                    if (NewArray[m] == array[k]) {

                                        objids = objids + "," + NewArray[m];
                                        objids1 = objids.replace(/(^,)|(,$)/g, "");
                                        NewArray1 = objids1.split(',');

                                    }
                                }
                            }

                            if (NewArray1 != "") {

                                inputFeatureLayer.selectFeatures(query, FeatureLayer.NEW, function (result) {

                                });
                            }
                            else {
                                $("#message").text("Selected feature is not present in the current selection");
                                $("#Loading_Spinner_QB").hide();
                            }

                        });

                    }
                    catch (err) {
                        $("#Loading_Spinner_QB").hide();
                        alert("Error Occured");

                    }

                }

            }

            function RemoveSelection(inputFeatureLayer, query) {

                inputFeatureLayer.selectFeatures(query, FeatureLayer.SELECTION_SUBTRACT, function (result) {

                });
            }



            function inputFeatureLayerLayerSelectionComplete(result) {

                try {
                    if (result.features.length <= 150) {
                        $("#message").text("");
                        MapSelectionAndZoomExtent(inputFeatureLayer, result);
                    }
                    else {
                        alert("Only 150 Features can be shown on Map");
                        $("#Loading_Spinner_QB").hide();
                    }
                }
                catch (err) {
                    alert("Error Occured");
                    $("#Loading_Spinner_QB").hide();
                }
            }

            var mapSelection;
            function MapSelectionAndZoomExtent(inputFeatureLayer, selectionresults) {

                if (inputFeatureLayer == "" || inputFeatureLayer == undefined) {
                    inputFeatureLayer = featureLayer;
                }

                mapSelection = inputFeatureLayer.getSelectedFeatures();

                var FeatureResult = inputFeatureLayer.getSelectedFeatures();

                if ($('#QBGraphicSelection').val() == 3 || $('#QBGraphicSelection').val() == 4) {
                    map.graphics.clear();
                    inputFeatureLayer.clearSelection();
                    inputFeatureLayer.refresh();
                }

                //  var FeatureResult = selectionresults.features;
                var geometryextent = "";
                var geometrycenter = "";
                if (FeatureResult.length == 0) {
                    $("#message").text("No Related Feature is Found");
                }
                else if (FeatureResult.length == 1)//Single Selection
                {

                    geometrycenter = FeatureResult[0].geometry;
                    if (geometrycenter.type == "point") {
                        map.centerAndZoom(geometrycenter, 19);
                        map.graphics.add(FeatureResult[0].setSymbol(SelectionMarkerSymbol));
                    }
                    else if (geometrycenter.type == "polyline") {

                        map.setExtent(FeatureResult[0]._extent);
                        map.graphics.add(FeatureResult[0].setSymbol(SelectionlineSymbol));

                    }
                    else if (geometrycenter.type == "polygon") {

                        map.setExtent(FeatureResult[0]._extent);
                        // if (inputFeatureLayer != "") {
                        map.graphics.add(FeatureResult[0].setSymbol(SelectionfillSymbol));
                        //  }

                    }

                }
                else if (FeatureResult.length > 1 && FeatureResult.length <= 150)//Multiple Selection
                {
                    for (var i = 0; i < FeatureResult.length; i++) {

                        if (FeatureResult[i].geometry.type == "point") {

                            map.graphics.add(FeatureResult[i].setSymbol(SelectionMarkerSymbol));


                        }
                        else if (FeatureResult[i].geometry.type == "polyline") {


                            map.graphics.add(FeatureResult[i].setSymbol(SelectionlineSymbol));

                        }
                        else if (FeatureResult[i].geometry.type == "polygon") {


                            map.graphics.add(FeatureResult[i].setSymbol(SelectionfillSymbol));

                        }
                    }
                    geometryextent = esri.graphicsExtent(FeatureResult);
                    map.setExtent(geometryextent.getExtent().expand(17));

                }
                else {
                    alert("Only 150 Features can be shown on Map");
                }
                //   mapSelection = inputFeatureLayer.getSelectedFeatures();
                InitializeResultGrid(FeatureResult);

            }

            function InitializeResultGrid(resultFeatures) {

                var thead = "";
                var tbody = "";
                try {

                    for (var i = 0; i < resultFeatures[0]._sourceLayer.fields.length; i++) {

                        if (resultFeatures[0]._sourceLayer.fields[i].name != "SHAPE" && resultFeatures[0]._sourceLayer.fields[i].name != "TransactionID" &&
                            resultFeatures[0]._sourceLayer.fields[i].name != "VillageID" && resultFeatures[0]._sourceLayer.fields[i].name != "ActionStatusID"
                            && resultFeatures[0]._sourceLayer.fields[i].name != "InterventionTypeID" && resultFeatures[0]._sourceLayer.fields[i].name != "InterventionActivityTypeID" &&
                            resultFeatures[0]._sourceLayer.fields[i].name != "LandOwnershipID" && resultFeatures[0]._sourceLayer.fields[i].name != "PriorityTypeID"
                            && resultFeatures[0]._sourceLayer.fields[i].name != "BenefitedHousehold_BPL" && resultFeatures[0]._sourceLayer.fields[i].name != "BenefitedHousehold_APL"
                            && resultFeatures[0]._sourceLayer.fields[i].name != "IsActive" && resultFeatures[0]._sourceLayer.fields[i].name != "CreatedBy" && resultFeatures[0]._sourceLayer.fields[i].name != "ModifiedBy" &&
                            resultFeatures[0]._sourceLayer.fields[i].name != "InterventionSiteID" && resultFeatures[0]._sourceLayer.fields[i].name != "VNRMCPRAExerciseID" && resultFeatures[0]._sourceLayer.fields[i].name != "OBJECTID"
                            && resultFeatures[0]._sourceLayer.fields[i].name != "Latitude" && resultFeatures[0]._sourceLayer.fields[i].name != "Longitude" && resultFeatures[0]._sourceLayer.fields[i].name != "ModifiedDate" && resultFeatures[0]._sourceLayer.fields[i].name != "SHAPE.STLength()" && resultFeatures[0]._sourceLayer.fields[i].name != "SHAPE.STArea()") {

                            thead = thead + "<th style='cursor: pointer;'>" + resultFeatures[0]._sourceLayer.fields[i].name + "</th>";
                        }

                    }
                    dom.byId("headquery").innerHTML = thead;
                    $("#QBResultPanel").show();

                    var heading = resultFeatures[0]._sourceLayer.fields;

                    var trow = "";
                    for (var i = 0; i < resultFeatures.length; i++) {
                        var tcol = "";

                        for (var j = 0; j < heading.length; j++) {
                            if (heading[j].name != "SHAPE" && heading[j].name != "TransactionID" &&
                            heading[j].name != "VillageID" && heading[j].name != "ActionStatusID"
                            && heading[j].name != "InterventionTypeID" && heading[j].name != "InterventionActivityTypeID" &&
                            heading[j].name != "LandOwnershipID" && heading[j].name != "PriorityTypeID" && heading[j].name != "BenefitedHousehold_BPL" && heading[j].name != "BenefitedHousehold_APL" &&
                              heading[j].name != "IsActive" && heading[j].name != "CreatedBy" && heading[j].name != "InterventionSiteID" && heading[j].name != "VNRMCPRAExerciseID" && heading[j].name != "ModifiedBy" && heading[j].name != "OBJECTID"
                                && heading[j].name != "Latitude" && heading[j].name != "Longitude" && heading[j].name != "ModifiedDate" && heading[j].name != "SHAPE.STLength()" && heading[j].name != "SHAPE.STArea()") {

                                if (heading[j].name == "CreatedDate") {

                                    if (resultFeatures[i].attributes["CreatedDate"] != "" && resultFeatures[i].attributes["CreatedDate"] != null) {

                                        var CreateDate = resultFeatures[i].attributes["CreatedDate"];
                                        var create_date_string = new Date(CreateDate).toUTCString();
                                        tcol = tcol + "<td>" + create_date_string + "</td>";
                                    }
                                    else {
                                        tcol = tcol + "<td></td>";
                                    }
                                }
                                else if (heading[j].name == "PlannedStartDate") {

                                    if (resultFeatures[i].attributes["PlannedStartDate"] != "" && resultFeatures[i].attributes["PlannedStartDate"] != null) {

                                        var PlanStart = resultFeatures[i].attributes["PlannedStartDate"];
                                        var planstart_date_string = new Date(PlanStart).toUTCString();
                                        tcol = tcol + "<td>" + planstart_date_string + "</td>";
                                    }
                                    else {
                                        tcol = tcol + "<td></td>";
                                    }
                                }
                                else if (heading[j].name == "PlannedEndDate") {

                                    if (resultFeatures[i].attributes["PlannedEndDate"] != "" && resultFeatures[i].attributes["PlannedEndDate"] != null) {

                                        var PlanEnd = resultFeatures[i].attributes["PlannedEndDate"];
                                        var planend_date_string = new Date(PlanStart).toUTCString();
                                        tcol = tcol + "<td>" + planend_date_string + "</td>";
                                    }
                                    else {
                                        tcol = tcol + "<td></td>";
                                    }
                                }
                                else {
                                    tcol = tcol + "<td>" + resultFeatures[i].attributes[heading[j].name]; +"</td>";
                                }
                            }
                        }

                        trow = trow + "<tr id=" + resultFeatures[i].attributes["OBJECTID"] + " class='ZoomToIS' >" + tcol + "</tr>";
                    }


                    if (typeof dataTable === 'undefined') {
                        dom.byId("querydata").innerHTML = trow;
                        dataTable = $('#QBResultPanelTable').dataTable({

                            'columnDefs': [{ 'orderable': false, 'targets': [0, 8] }],
                            "pageLength": 10,
                            "bFilter": false,
                            "paging": true
                        });
                    } else {
                        dataTable.fnClearTable();
                        dataTable.fnDestroy();
                        dom.byId("querydata").innerHTML = trow;
                        dataTable = $('#QBResultPanelTable').dataTable({

                            'columnDefs': [{ 'orderable': false, 'targets': [0, 8] }],
                            "pageLength": 10,
                            "bFilter": false,
                            "paging": true
                        });
                    };

                    //$(function () {
                    //    $('#QBResultPanelTable').DataTable({
                    //        "bFilter": false,
                    //        'columnDefs': [{ 'orderable': false, 'targets': [0, 22] }],
                    //        //  'aaSorting': [[1, 'desc']],
                    //        "bLengthChange": false,
                    //        "paging": true

                    //    });
                    //    responsive: true;

                    //});
                    $("#Loading_Spinner_QB").hide();
                }

                catch (err) {

                    $("#Loading_Spinner_QB").hide();
                }



            }

            $("#QBResultPanelTable").on("click", ".ZoomToIS", function () {

                var shapeType = "";
                if ($("#QBLayername option:selected").val() == 'IPoints' || Shape == "1") {
                    shapeType = 1;
                }
                else if ($("#QBLayername option:selected").val() == 'ILines' || Shape == "2") {
                    shapeType = 2;
                }
                else if ($("#QBLayername option:selected").val() == 'IPolygons' || Shape == "3") {
                    shapeType = 3;
                }

                var objid = $(this)[0].id;
                ZoomtoSite(objid, shapeType);

            });

            //For Adding Query in Query Builder
            $("#AddQuery").click(function () {

                var qname = $("#QueryText").val().toString();
                var query = $('#QBTextArea').val().toString();

                if (qname != null && qname != "" && query != null && query != "") {

                    $.ajax({
                        url: SaveQueryInDataBase.SavedQueryURL,
                        type: "POST",
                        data: { QueryName: qname, Query: query, UserID: USERID },
                        dataType: "json",
                        traditional: true,
                        success: function (CheckedData) {

                            try {
                                if (CheckedData == "1") {
                                    alert("Query name already exists!");
                                }
                                else if (CheckedData == "2") {
                                    alert("Query saved successfully!");
                                }

                            }
                            catch (err) {
                                alert("Something went wrong");
                            }
                        },
                        error: function (jqXHR, textStatus, err) {

                            alert("Error Occured");
                        }
                    });
                }
                else {
                    alert("either query or query name is null")
                }
            });

            //For loading Query From Query Builder
            $("#QBLoad").click(function () {

                $("#SelectQueryName").empty();
                HtmlValueName = "";
                HtmlValueName += "<option value='0' disabled='disabled' selected='selected'>Select</option>";
                $.ajax({
                    url: LoadQueryFromDataBase.LoadQueryURL,
                    type: "POST",
                    dataType: "JSON",
                    data: { UserID: USERID },
                    success: function (data) {
                        try {
                            // clear before appending new list    
                            $.each(data, function (i, data) {
                                HtmlValueName += "<option value='" + data.QueryBuilderID + "'>" + data.QueryName + "</option>";
                            });
                            $("#SelectQueryName").append(HtmlValueName);

                            $("#LoadQuery").click(function () {

                                for (var i = 0; i < data.length; i++) {
                                    if ($("#SelectQueryName option:selected").text() == data[i].QueryName) {
                                        var sto = data[i].Query;
                                        $("#QBTextArea").val(sto);
                                    }
                                }

                            });

                        }
                        catch (err) {
                            errorAlert("Error Occured");
                        }
                    }
                });
            });


            ////////-----------------------------------View Intervention Sites----------------------------------------------------//////////

            $("#ViewIntervention").click(function () {

                ShowInterventionSiteData();

            });
            
            var siteDetails;

            function ShowInterventionSiteData() {

                var districtid = $('#ddlDistrict').val();
                var blockid = $('#ddlBlock').val();
                var villageid = $('#ddlVillage').val();

                $.ajax({
                    url: GetInterventionSites.SiteURL,
                    type: "POST",
                    data: { RoleID: ROLEID, DistrictID: districtid, BlockID: blockid, VillageID: villageid },
                    dataType: "json",
                    traditional: true,
                    success: function (resultData) {
                        try {

                            BindInterventionSitesTable(resultData);

                        }
                        catch (err) {
                            alert("Something went wrong");
                        }
                    },
                    error: function (jqXHR, textStatus, err) {

                      //  errorAlert("Error Occured");
                    }
                });



            }

            function BindInterventionSitesTable(resultData) {

                var path = "../../Areas/MapView/assets/images/map-img/search_plush.png";
                var tabdata = "";
                if (resultData.InterventionSiteTable != null && resultData.InterventionSiteTable.length != 0) {

                    for (var i = 0; i < resultData.InterventionSiteTable.length; i++) {
                        tabdata = tabdata + "<tr id='" + resultData.InterventionSiteTable[i].InterventionSiteID + "'><td>" + resultData.InterventionSiteTable[i].InterventionSiteName + "</td><td>" + resultData.InterventionSiteTable[i].InterventionType + "</td><td>" + resultData.InterventionSiteTable[i].area_ha + "</td><td>" + resultData.InterventionSiteTable[i].ActionName + "</td><td class='text-center' id=" + resultData.InterventionSiteTable[i].ShapeType + "><a class='btnViewSites' href='#'  id='" + resultData.InterventionSiteTable[i].OBJECTID + "' ><i class='fa fa-eye'  title='View'></i></a></td><td class='text-center' id='" + resultData.InterventionSiteTable[i].ShapeType + "'><a class='btnZoomToSites' href='#'  id='" + resultData.InterventionSiteTable[i].OBJECTID + "' ><i class='fa fa-search'  title='View'></i></a></td></tr>";

                    }

                }
            

                if (typeof dataTable === 'undefined') {

                    dom.byId("InterventionSitesTable").innerHTML = tabdata;
                    dataTable = $('#InterventionSiteTable').dataTable({

                        'columnDefs': [{ 'orderable': false, 'targets': [0, 5] }],
                        "pageLength": 5,
                        "bFilter": false,
                        "paging": true
                    });
                } else {
                    dataTable.fnClearTable();
                    dataTable.fnDestroy();
                    dom.byId("InterventionSitesTable").innerHTML = tabdata;
                    dataTable = $('#InterventionSiteTable').dataTable({

                        'columnDefs': [{ 'orderable': false, 'targets': [0, 5] }],
                        "pageLength": 5,
                        "bFilter": false,
                        "paging": true
                    });
                };

              

            }

            $("#InterventionSiteTable").on("click", ".btnViewSites", function () {

                var divs = document.getElementById('outerdiv').children;
                var len = divs.length;
                if (len != 0) {

                    for (i = 0; i < len; i++) {

                        var div = document.getElementById("div" + i);
                        div.parentNode.removeChild(div);
                    }
                }

                // var shapeType = $(this).val();
                var shapeType = $(this)[0].parentElement.id;
                var objid = $(this)[0].id;
                var siteid = $(this)[0].parentElement.parentElement.id;
              //  ZoomtoSite(objid, shapeType);
                showSitesbyIds(siteid, shapeType);
            });


            $("#InterventionSiteTable").on("click", ".btnZoomToSites", function () {

                var objid = $(this)[0].id;
                var siteid = $(this)[0].parentElement.parentElement.id;
                var shapeType = $(this)[0].parentElement.id;
                ZoomtoSite(objid, shapeType);
               
            });


            // var OnClickInfo1 = map.on("click", executeIdentifyTask1);

            //function executeIdentifyTask1(event) {

            //    var count = 0;
            //    identifyTask = new IdentifyTask("https://nw047gcs.rmsi.com:6443/arcgis/rest/services/meghalaya/MegInterventionSite/MapServer");
            //    identifyParams = new IdentifyParameters();
            //    identifyParams.tolerance = 4;
            //    identifyParams.geometryPrecision = 1;
            //    identifyParams.layerIds = [0, 2, 3];
            //    identifyParams.layerOption = IdentifyParameters.LAYER_OPTION_VISIBLE;
            //    identifyParams.width = map.width;
            //    identifyParams.height = map.height;
            //    identifyParams.geometry = event.mapPoint;
            //    identifyParams.mapExtent = map.extent;
            //    var deferred = identifyTask.execute(identifyParams).addCallback(function (response) {
            //        return arrayUtils.map(response, function (result) {

            //            var feature = result.feature;
            //            var layerName = result.layerName;

            //            var siteID = result.feature.attributes.InterventionSiteID;
            //            if (layerName == 'Intervention Site Points') {

            //                var shapeType = 1;
            //              showSitesbyIds(siteID, shapeType);
            //            }

            //            else if (layerName == 'Intervention Site Lines') {

            //                var shapeType = 2;
            //                showSitesbyIds(siteID, shapeType);
            //            }
            //            else if (layerName == 'Intervention Site Polygon') {

            //                var shapeType = 3;
            //                showSitesbyIds(siteID, shapeType);
            //            }

            //        });


            //    });
            //    map.infoWindow.setFeatures([deferred]);
            //}

            function showSitesbyIds(siteID, shapeType) {

                var ImageName = "";
                var ImagePath = "";

                $("#interventionSiteModal").modal();

                $.ajax({
                    url: GetInterventionSitesByID.SiteIDURL,
                    type: "POST",
                    data: { InterventionSiteID: siteID, ShapeType: shapeType },
                    dataType: "json",
                    traditional: true,
                    success: function (returnData) {
                        try {

                            siteDetails = returnData;

                            if (siteDetails != null) {

                                if (siteDetails.Table != undefined) {
                                    document.getElementById('siteNameid').innerHTML = returnData.Table[0].InterventionSiteName;
                                }
                                FillInterventionSiteDetails();

                                // divDetails divImages
                                document.getElementById("divDetails").style.display = "block";
                                document.getElementById("divImages").style.display = "none";

                                $("#picdiv").removeClass('active');
                                $("#detailsdiv").addClass('active');

                                if (siteDetails.Table1 != undefined) {

                                    document.getElementById("timeSliderDiv").style.display = "block";
                                    if (siteDetails.Table1.length != 0) {
                                        var iDiv = document.createElement('div');
                                        iDiv.id = 'div0';
                                        iDiv.className = 'carousel-item active';
                                        document.getElementById('outerdiv').appendChild(iDiv);

                                        var img = document.createElement('img');
                                        var ImageName = siteDetails.Table1[0].DocumentName;
                                        //  var ImagePath = '/Document/MapSitesImages/' + ImageName;
                                        //  var ImagePath = MapImagePath + ImageName;
                                        var ImagePath = APPURL + MapImagePath + ImageName;
                                        img.src = ImagePath;
                                        img.id = 'img0';
                                        img.className = 'img-fluid mx-auto d-block';
                                        img.className = 'scroll-img-wrap';
                                        img.alt = 'slide 1';
                                        document.getElementById(iDiv.id).appendChild(img);


                                        if (siteDetails.Table1.length > 0) {
                                            CreateImageDiv();
                                        }
                                        //  if (siteDetails.Table1.length > 1) {
                                        initSlider();
                                        //  }
                                    }

                                }
                                else {

                                    document.getElementById("timeSliderDiv").style.display = "none";
                                   // timeSliderDiv

                                }

                            }

                        }
                        catch (err) {
                            //   alert("Something went wrong");
                        }
                    },
                    error: function (jqXHR, textStatus, err) {

                        errorAlert("Error Occured");
                    }
                });



            }

            function ZoomtoSite(siteID, shapeType) {

                var queryZoom = new Query();
                queryZoom.objectIds = [siteID];
                queryZoom.outSpatialReference = new SpatialReference(102100);
                queryZoom.outFields = ["*"];
                queryZoom.returnGeometry = "true";
                try {
                    if (shapeType == 1) {
                        PointLayer.queryFeatures(queryZoom, RESULTZOOM);
                    }
                    if (shapeType == 2) {
                        PolylineLayer.queryFeatures(queryZoom, RESULTZOOM);
                    }
                    if (shapeType == 3) {
                        PolygonLayer.queryFeatures(queryZoom, RESULTZOOM);
                    }


                    function RESULTZOOM(resultZoom) {

                        if (shapeType == 1) {
                            var center = resultZoom.features[0].geometry;
                            map.centerAndZoom(center, 20);
                        }
                        else {
                            var resultFeature = resultZoom.features;
                            var geometryextent = esri.graphicsExtent(resultFeature);
                            map.setExtent(geometryextent);
                        }
                    }
                }
                catch (err) {
                    errorAlert("Assigned Data is missing");
                }

            }

            function FillInterventionSiteDetails() {

                if (siteDetails != null && siteDetails != undefined) {
                    if (siteDetails.Table[0].length != 0) {

                        if (siteDetails.Table[0].InterventionSiteName != null && siteDetails.Table[0].InterventionSiteName != undefined) {
                            document.getElementById('sitename').innerHTML = siteDetails.Table[0].InterventionSiteName;
                        }
                        else {
                            document.getElementById('sitename').innerHTML = "--";
                        }
                        if (siteDetails.Table[0].InterventionType != null && siteDetails.Table[0].InterventionType != undefined) {
                            document.getElementById('sitetypeid').innerHTML = siteDetails.Table[0].InterventionType;
                        }
                        else {
                            document.getElementById('sitetypeid').innerHTML = "--";
                        }
                        if (siteDetails.Table[0].InterventionActivityType != null && siteDetails.Table[0].InterventionActivityType != undefined) {
                            document.getElementById('siteactivitytypeid').innerHTML = siteDetails.Table[0].InterventionActivityType;
                        }
                        else {
                            document.getElementById('siteactivitytypeid').innerHTML = "--";
                        }
                        if (siteDetails.Table[0].area_ha != null && siteDetails.Table[0].area_ha != undefined) {
                            document.getElementById('areaid').innerHTML = siteDetails.Table[0].area_ha;
                        }
                        else {
                            document.getElementById('areaid').innerHTML = "--";
                        }
                        if (siteDetails.Table[0].ActionName != null && siteDetails.Table[0].ActionName != undefined) {
                            document.getElementById('statusid').innerHTML = siteDetails.Table[0].ActionName;
                        }
                        else {
                            document.getElementById('statusid').innerHTML = "--";
                        }
                        if (siteDetails.Table[0].VillageName != "" && siteDetails.Table[0].VillageName != undefined) {
                            document.getElementById('villageid').innerHTML = siteDetails.Table[0].VillageName;
                        }
                        else {
                            document.getElementById('villageid').innerHTML = "--";
                        }
                        if (siteDetails.Table[0].Amount != null && siteDetails.Table[0].Amount != undefined) {
                            document.getElementById('amountid1').innerHTML = siteDetails.Table[0].Amount;
                        }
                        else {
                            document.getElementById('amountid1').innerHTML = "--";
                        }
                        if (siteDetails.Table[0].CreatedDate != null && siteDetails.Table[0].CreatedDate != undefined) {
                            document.getElementById('createdateid').innerHTML = siteDetails.Table[0].CreatedDate;
                        }
                        else {
                            document.getElementById('createdateid').innerHTML = "--";
                        }
                        if (siteDetails.Table[0].LandOwnershipType != null && siteDetails.Table[0].LandOwnershipType != undefined) {
                            document.getElementById('landownerid').innerHTML = siteDetails.Table[0].LandOwnershipType;
                        }
                        else {
                            document.getElementById('landownerid').innerHTML = "--";
                        }

                    }
                }

            }

            $(document).ready(function () {

                document.getElementById("divDetails").click();


            });

            $("#clearDiv").click(function () {

                //var div = document.getElementById('outerdiv');
                var divs = document.getElementById('outerdiv').children;
                var len = divs.length;
                if (len != 0) {

                    for (i = 0; i < len; i++) {

                        var div = document.getElementById("div" + i);
                        div.parentNode.removeChild(div);


                    }
                }

            });

            var timeSlider = new TimeSlider({
                style: "width: 100%;"
            }, dom.byId("timeSliderDiv"));
            map.setTimeSlider(timeSlider);

            function initSlider() {

         
                //  var length = siteDetails.Table1.length;
                var length = siteDetails.Table1.length;

                var dates = [];
                var dateLabels = [];
                var monthNames = ["Jan", "Feb", "Mar", "Apr", "May", "Jun",
               "Jul", "Aug", "Sep", "Oct", "Nov", "Dec"];
                for (i = 0; i < siteDetails.Table1.length; i++) {

                    var date = new Date(siteDetails.Table1[i].CreatedDate);
                    dates.push(date);
                    var monthname = monthNames[date.getMonth()];
                    var year = date.getFullYear();
                    dateLabels.push(monthname + ' ' + year);
                }

                var startDate = siteDetails.Table1[0].CreatedDate;
                var endDate = siteDetails.Table1[length - 1].CreatedDate;

                var labels = dateLabels;
                timeSlider.setLabels(labels);

                var timeExtent = new TimeExtent();
                timeExtent.startTime = new Date(startDate);
                timeExtent.endTime = new Date(endDate);

                // timeSlider.setThumbCount(2);
                if (length > 1) {
                    timeSlider.createTimeStopsByCount(timeExtent, length)
                }
                // timeSlider.setThumbMovingRate(2000);
                timeSlider.timeStops = dates;
                timeSlider.startup();

                timeSlider.on("time-extent-change", extentChanged);
                function extentChanged(evt) {

                    var startVal = evt.startTime;
                    var endVal = evt.endTime;
                    var d = new Date(endVal);
                    var endvalnew = d.toGMTString();
                    for (i = 0; i < siteDetails.Table1.length; i++) {
                        var date2 = new Date(siteDetails.Table1[i].CreatedDate);
                        var datenew = date2.toGMTString();
                        if (endvalnew == datenew) {
                            var img = siteDetails.Table1[i].DocumentName;
                            var img1 = img.split('.');
                            var img2 = img1[0];
                            var divs = document.getElementById('outerdiv').children;
                            var len = divs.length;
                            for (j = 0; j < len; j++) {

                                var div = document.getElementById("div" + j);
                                var divChilds = document.getElementById("div" + j).children;
                                var imgId = "img" + j;
                                var imgg = document.getElementById("img" + j);
                                var source = document.getElementById("img" + j).src;
                                var last = source.substring(source.lastIndexOf("/") + 1, source.length);
                                var img3 = last.split('.');
                                var img4 = img3[0];
                                // var n = source.lastIndexOf("/");
                                //  var sourceIdx = source.split(n);
                                if (img4 != img2) {
                                    if (div.className == 'carousel-item active') {
                                        div.classList.remove("active");
                                    }
                                }

                                if (img4 == img2) {
                                    $('#div' + j).addClass('carousel-item active');
                                    $('#img' + j).css("display", "block");
                                }
                            }
                        }
                    }

                }


            }

            function CreateImageDiv() {


                var iDiv = "";
                var count = "";
                var img = "";
                var ImageName = "";
                var ImagePath = "";
                if (siteDetails != null && siteDetails != undefined) {

                    if (siteDetails.Table1.length != 0) {

                        for (i = 1; i < siteDetails.Table1.length; i++) {

                            count = i + 1;
                            iDiv = document.createElement('div');
                            iDiv.id = 'div' + i;
                            iDiv.className = 'carousel-item';
                            document.getElementById('outerdiv').appendChild(iDiv);

                            img = document.createElement('img');
                            ImageName = siteDetails.Table1[i].DocumentName;
                            ImagePath = APPURL + MapImagePath + ImageName;
                            // ImagePath = MapImagePath + ImageName;
                            // ImagePath = '/Document/MapSitesImages/' + ImageName;
                            img.src = ImagePath;
                            img.id = 'img' + i;
                            img.className = 'img-fluid mx-auto d-block';
                            // img.className = 'scroll-img-wrap';
                            img.alt = 'slide' + ' ' + count;
                            document.getElementById(iDiv.id).appendChild(img);
                        }


                    }
                }
            }

            $("#picdiv").click(function () {


                var i, tabcontent, tablinks;
                tabcontent = document.getElementsByClassName("tabcontent");
                for (i = 0; i < tabcontent.length; i++) {
                    tabcontent[i].style.display = "none";
                }

                tablinks = document.getElementsByClassName("tablinks");
                for (i = 0; i < tablinks.length; i++) {
                    tablinks[i].className = tablinks[i].className.replace("active", "");
                }

                document.getElementById("divImages").style.display = "block";
                $(this).addClass('active');
                //  CreateImageDiv();

            });



            $("#detailsdiv").click(function () {

                var i, tabcontent, tablinks;
                tabcontent = document.getElementsByClassName("tabcontent");
                for (i = 0; i < tabcontent.length; i++) {
                    tabcontent[i].style.display = "none";
                }

                tablinks = document.getElementsByClassName("tablinks");
                for (i = 0; i < tablinks.length; i++) {
                    tablinks[i].className = tablinks[i].className.replace("active", "");
                }

                document.getElementById("divDetails").style.display = "block";
                $(this).addClass('active');



            });

            //-------------------------Thematic Visualization ----------------------------------------------------------------------//

            //Opacity slider

            var slider = new HorizontalSlider({
                map: map,
                value: 1,
                minimum: 0,
                maximum: 1,
                showButtons: false,
            }, dojo.byId("OpacitySliderDiv"));

            slider.startup();

            on(slider, "change", lang.hitch(this, function (evt) {

                DistrictLayer.setOpacity(slider.value);
                PointLayer.setOpacity(slider.value);
                PolylineLayer.setOpacity(slider.value);
                PolygonLayer.setOpacity(slider.value);

            }));

            $("#BaseMapGallery").change(function () {

                var selectedText = $(this).find("option:selected").text();
                var selectedValue = $(this).val();
                ChangeBaseMap(selectedValue);
            });

            function ChangeBaseMap(selectedValue) {

                map.graphics.clear();

                if (selectedValue == 1) {
                    map.setBasemap("gray");
                }
                else if (selectedValue == 2) {
                    map.setBasemap("satellite");
                }
                else if (selectedValue == 3) {
                    map.setBasemap("streets");
                }
                else if (selectedValue == 4) {
                    map.setBasemap("topo");
                }
                else if (selectedValue == 5) {
                    map.setBasemap("hybrid");
                }
                else if (selectedValue == 6) {
                    map.setBasemap("osm");
                }

            }

            var graphic1 = "", graphic2 = "", graphic3 = "", graphic4 = "", graphic5 = "", graphic6 = "";


            $("#OpenImplementationSite").click(function () {

                var renderer = new UniqueValueRenderer(defaultSymbol, "InterventionTypeID");

                var SelectionMarkerSymbolBlue = new SimpleMarkerSymbol(
         SimpleMarkerSymbol.STYLE_CIRCLE, 12, new SimpleLineSymbol(SimpleLineSymbol.STYLE_SQUARE, new Color([66, 255, 242]), 1),
                       new Color([66, 255, 242])); //blue 

                var SelectionMarkerSymbolGreen = new SimpleMarkerSymbol(
      SimpleMarkerSymbol.STYLE_CIRCLE, 12, new SimpleLineSymbol(SimpleLineSymbol.STYLE_SQUARE, new Color([82, 139, 84]), 1),
                    new Color([82, 139, 84])); //green

                var SelectionMarkerSymbolYellow = new SimpleMarkerSymbol(
      SimpleMarkerSymbol.STYLE_CIRCLE, 12, new SimpleLineSymbol(SimpleLineSymbol.STYLE_SQUARE, new Color([255, 255, 0]), 1),
                    new Color([255, 255, 0])); //yellow

                var SelectionMarkerSymbolBrown = new SimpleMarkerSymbol(
      SimpleMarkerSymbol.STYLE_CIRCLE, 12, new SimpleLineSymbol(SimpleLineSymbol.STYLE_SQUARE, new Color([128, 64, 0]), 1),
                    new Color([128, 64, 0])); //brown

                var SelectionMarkerSymbolOrange = new SimpleMarkerSymbol(
    SimpleMarkerSymbol.STYLE_CIRCLE, 12, new SimpleLineSymbol(SimpleLineSymbol.STYLE_SQUARE, new Color([230, 115, 0]), 1),
                  new Color([230, 115, 0])); //orange

                var SelectionMarkerSymbolLgreen = new SimpleMarkerSymbol(
    SimpleMarkerSymbol.STYLE_CIRCLE, 12, new SimpleLineSymbol(SimpleLineSymbol.STYLE_SQUARE, new Color([102, 255, 102]), 1),
                  new Color([102, 255, 102])); //Light Green

                var defaultSymbol = new SimpleMarkerSymbol(
    SimpleMarkerSymbol.STYLE_CIRCLE, 12, new SimpleLineSymbol(SimpleLineSymbol.STYLE_SQUARE, new Color([255, 0, 0]), 1),
                  new Color([255, 0, 0]));


                renderer.addValue("1", SelectionMarkerSymbolLgreen);
                renderer.addValue("2", SelectionMarkerSymbolYellow);
                renderer.addValue("3", SelectionMarkerSymbolGreen);
                renderer.addValue("4", SelectionMarkerSymbolBlue);
                renderer.addValue("5", SelectionMarkerSymbolBrown);
                renderer.addValue("6", SelectionMarkerSymbolOrange);


                PointLayer.setRenderer(renderer);
                PointLayer.refresh();

                var defaultlineSymbol = new SimpleLineSymbol(SimpleLineSymbol.STYLE_SOLID, new Color([66, 0, 158]), 3); //dark blue
                var lineSymbolSGreen = new SimpleLineSymbol(SimpleLineSymbol.STYLE_SOLID, new Color([78, 238, 148]), 3); //seagreen
                var lineSymbolYellow = new SimpleLineSymbol(SimpleLineSymbol.STYLE_SOLID, new Color([205, 205, 0]), 3); //yellow2
                var lineSymbolGreen = new SimpleLineSymbol(SimpleLineSymbol.STYLE_SOLID, new Color([0, 255, 127]), 3); //green
                var lineSymbolBlue = new SimpleLineSymbol(SimpleLineSymbol.STYLE_SOLID, new Color([131, 111, 255]), 3); //blue
                var lineSymbolBrown = new SimpleLineSymbol(SimpleLineSymbol.STYLE_SOLID, new Color([205, 112, 84]), 3); //brownSalmon
                var lineSymbolOrange = new SimpleLineSymbol(SimpleLineSymbol.STYLE_SOLID, new Color([238, 154, 0]), 3); //orange1

                var renderer1 = new UniqueValueRenderer(defaultlineSymbol, "InterventionTypeID");

                renderer1.addValue("1", lineSymbolSGreen);
                renderer1.addValue("2", lineSymbolYellow);
                renderer1.addValue("3", lineSymbolGreen);
                renderer1.addValue("4", lineSymbolBlue);
                renderer1.addValue("5", lineSymbolBrown);
                renderer1.addValue("6", lineSymbolOrange);

                PolylineLayer.setRenderer(renderer1);
                PolylineLayer.refresh();

                var DefaultSelectionfillSymbol = new SimpleFillSymbol(SimpleFillSymbol.STYLE_SOLID,
               new SimpleLineSymbol(SimpleLineSymbol.STYLE_SQUARE, new Color([85, 255, 0]), 2), new Color([85, 255, 0])); //lightgreen

                var SelectionfillSymbolGreen = new SimpleFillSymbol(SimpleFillSymbol.STYLE_SOLID,
            new SimpleLineSymbol(SimpleLineSymbol.STYLE_SQUARE, new Color([154, 205, 50]), 2), new Color([154, 205, 50])); //olive

                var SelectionfillSymbolYellow = new SimpleFillSymbol(SimpleFillSymbol.STYLE_SOLID,
                new SimpleLineSymbol(SimpleLineSymbol.STYLE_SQUARE, new Color([238, 201, 0]), 2), new Color([238, 201, 0])); //gold

                var SelectionfillSymbolDGreen = new SimpleFillSymbol(SimpleFillSymbol.STYLE_SOLID,
               new SimpleLineSymbol(SimpleLineSymbol.STYLE_SQUARE, new Color([0, 139, 0]), 2), new Color([0, 139, 0])); //Dark green

                var SelectionfillSymbolBlue = new SimpleFillSymbol(SimpleFillSymbol.STYLE_SOLID,
              new SimpleLineSymbol(SimpleLineSymbol.STYLE_SQUARE, new Color([96, 123, 139]), 2), new Color([96, 123, 139])); //Light sky blue

                var SelectionfillSymbolBrown = new SimpleFillSymbol(SimpleFillSymbol.STYLE_SOLID,
             new SimpleLineSymbol(SimpleLineSymbol.STYLE_SQUARE, new Color([139, 62, 47]), 2), new Color([139, 62, 47])); //Brown

                var SelectionfillSymbolOrange = new SimpleFillSymbol(SimpleFillSymbol.STYLE_SOLID,
             new SimpleLineSymbol(SimpleLineSymbol.STYLE_SQUARE, new Color([205, 133, 63]), 2), new Color([205, 133, 63])); //Peru

                var renderer2 = new UniqueValueRenderer(DefaultSelectionfillSymbol, "InterventionTypeID");


                renderer2.addValue("1", SelectionfillSymbolGreen);
                renderer2.addValue("2", SelectionfillSymbolYellow);
                renderer2.addValue("3", SelectionfillSymbolDGreen);
                renderer2.addValue("4", SelectionfillSymbolBlue);
                renderer2.addValue("5", SelectionfillSymbolBrown);
                renderer2.addValue("6", SelectionfillSymbolOrange);


                PolygonLayer.setRenderer(renderer2);
                PolygonLayer.refresh();

                $("#pane10").show();
            });
            //    $("#ThematicDateSelection").change(function () {

            //        var PointSiteIds = [];
            //        var LineSiteIds = [];
            //        var PolygonSiteIds = [];

            //        var selectedDate = $(this).val();
            //        var dt = new Date(selectedDate);
            //        dt.setDate(dt.getDate() + 1);
            //        var dt1 = dt.toISOString().split('T');
            //        var dt2 = dt1[0];

            //        var query = new Query();
            //     //   query.where = "CreatedDate between '2020-07-17' and  '2020-07-18'";
            //        query.where = "CreatedDate between '" + selectedDate + "' and '" + dt2+"'";
            //       // query.where = "CreatedDate like '2020-07-18%'";
            //        query.ReturnGeometry = true;

            //        PointLayer.queryFeatures(query, function (featureSet) {

            //            var result = featureSet;

            //            var SelectionMarkerSymbolBlue = new SimpleMarkerSymbol(
            //  SimpleMarkerSymbol.STYLE_CIRCLE, 12, new SimpleLineSymbol(SimpleLineSymbol.STYLE_SQUARE, new Color([66, 255, 242]), 1),
            //                new Color([66, 255, 242])); //blue 

            //            var SelectionMarkerSymbolGreen = new SimpleMarkerSymbol(
            //  SimpleMarkerSymbol.STYLE_CIRCLE, 12, new SimpleLineSymbol(SimpleLineSymbol.STYLE_SQUARE, new Color([82, 139, 84]), 1),
            //                new Color([82, 139, 84])); //green

            //            var SelectionMarkerSymbolYellow = new SimpleMarkerSymbol(
            //  SimpleMarkerSymbol.STYLE_CIRCLE, 12, new SimpleLineSymbol(SimpleLineSymbol.STYLE_SQUARE, new Color([255, 255, 0]), 1),
            //                new Color([255, 255, 0])); //yellow

            //            var SelectionMarkerSymbolBrown = new SimpleMarkerSymbol(
            //  SimpleMarkerSymbol.STYLE_CIRCLE, 12, new SimpleLineSymbol(SimpleLineSymbol.STYLE_SQUARE, new Color([128, 64, 0]), 1),
            //                new Color([128, 64, 0])); //brown

            //            var SelectionMarkerSymbolOrange = new SimpleMarkerSymbol(
            //SimpleMarkerSymbol.STYLE_CIRCLE, 12, new SimpleLineSymbol(SimpleLineSymbol.STYLE_SQUARE, new Color([230, 115, 0]), 1),
            //              new Color([230, 115, 0])); //orange

            //            var SelectionMarkerSymbolLgreen = new SimpleMarkerSymbol(
            //SimpleMarkerSymbol.STYLE_CIRCLE, 12, new SimpleLineSymbol(SimpleLineSymbol.STYLE_SQUARE, new Color([102, 255, 102]), 1),
            //              new Color([102, 255, 102])); //Light Green

            //            var defaultSymbol = new SimpleMarkerSymbol(
            //SimpleMarkerSymbol.STYLE_CIRCLE, 12, new SimpleLineSymbol(SimpleLineSymbol.STYLE_SQUARE, new Color([255, 0, 0]), 1),
            //              new Color([255, 0, 0]));
            //           // var defaultSymbol = new SimpleMarkerSymbol().setStyle(SimpleMarkerSymbol.STYLE_NULL);
            //           // defaultSymbol.outline.setStyle(SimpleLineSymbol.STYLE_NULL);

            //            var renderer = new UniqueValueRenderer(defaultSymbol, "InterventionTypeID");

            //            for (var i = 0; i < featureSet.features.length; i++) {

            //                var InterventionTypeID = featureSet.features[i].attributes.InterventionTypeID;
            //                PointSiteIds.push(InterventionTypeID);

            //                if (InterventionTypeID == "1") {
            //                    renderer.addValue(featureSet.features[i].attributes.InterventionTypeID, SelectionMarkerSymbolLgreen);
            //                }
            //               else if (InterventionTypeID == "2") {
            //                   renderer.addValue(featureSet.features[i].attributes.InterventionTypeID, SelectionMarkerSymbolYellow);
            //                }
            //               else if (InterventionTypeID == "3") {
            //                   renderer.addValue(featureSet.features[i].attributes.InterventionTypeID, SelectionMarkerSymbolGreen);
            //                }
            //               else if (InterventionTypeID == "4") {
            //                   renderer.addValue(featureSet.features[i].attributes.InterventionTypeID, SelectionMarkerSymbolBlue);
            //                }
            //               else if (InterventionTypeID == "5") {
            //                   renderer.addValue(featureSet.features[i].attributes.InterventionTypeID, SelectionMarkerSymbolBrown);
            //                }
            //               else if (InterventionTypeID == "6") {
            //                   renderer.addValue(featureSet.features[i].attributes.InterventionTypeID, SelectionMarkerSymbolOrange);
            //               }
            //               else {
            //                   renderer.addValue(featureSet.features[i].attributes.InterventionTypeID, defaultSymbol);
            //               }
            //            }
            //            PointLayer.setRenderer(renderer);
            //            PointLayer.refresh();
            //            //for (var i = 0; i < featureSet.features.length; i++) {

            //            //    var InterventionTypeID = featureSet.features[i].attributes.InterventionTypeID;
            //            //    if (InterventionTypeID == "1") {

            //            //        graphic1 = featureSet.features[i];
            //            //        graphic1.setSymbol(SelectionMarkerSymbolLgreen);

            //            //    }
            //            //    else if (InterventionTypeID == "0") {

            //            //        graphic2 = featureSet.features[i];
            //            //        graphic2.setSymbol(SelectionMarkerSymbolYellow);

            //            //   }
            //            //    else if (InterventionTypeID == "3") {

            //            //       graphic3 = featureSet.features[i];
            //            //       graphic3.setSymbol(SelectionMarkerSymbolGreen);
            //            //       map.graphics.add(graphic3);
            //            //   }
            //            //    else if (InterventionTypeID == "4") {

            //            //       graphic4 = featureSet.features[i];
            //            //       graphic4.setSymbol(SelectionMarkerSymbolBlue);
            //            //       map.graphics.add(graphic4);
            //            //   }
            //            //    else if (InterventionTypeID == "5") {

            //            //       graphic5 = featureSet.features[i];
            //            //       graphic5.setSymbol(SelectionMarkerSymbolBrown);
            //            //       map.graphics.add(graphic5);
            //            //   }
            //            //    else if (InterventionTypeID == "6") {

            //            //       graphic6 = featureSet.features[i];
            //            //       graphic6.setSymbol(SelectionMarkerSymbolOrange);
            //            //       map.graphics.add(graphic6);
            //            //   }
            //            //}
            //            //map.graphics.add(graphic1);
            //            //map.graphics.add(graphic2);


            //            //var defaultSymbol = new SimpleMarkerSymbol().setStyle(SimpleMarkerSymbol.STYLE_NULL);
            //            //defaultSymbol.outline.setStyle(SimpleLineSymbol.STYLE_NULL);
            //         //   map.graphics.add(new Graphic(result.features[0], SelectionMarkerSymbol));
            //        //    map.graphics.add(new Graphic(result.features[1], SelectionMarkerSymbol1));
            //          //  var renderer = new UniqueValueRenderer(defaultSymbol, "InterventionTypeID");
            //           /// renderer.addValue(result.features[0].attributes.InterventionTypeID, SelectionMarkerSymbol);
            //          //  renderer.addValue("1", SelectionMarkerSymbol1);
            //          //  PointLayer.setRenderer(renderer);

            //        });

            //        PolylineLayer.queryFeatures(query, function (featureSet) {

            //            var defaultlineSymbol = new SimpleLineSymbol(SimpleLineSymbol.STYLE_SOLID, new Color([66, 0, 158]), 3); //dark blue
            //            var lineSymbolSGreen = new SimpleLineSymbol(SimpleLineSymbol.STYLE_SOLID, new Color([78,238,148]), 3); //seagreen
            //            var lineSymbolYellow = new SimpleLineSymbol(SimpleLineSymbol.STYLE_SOLID, new Color([205,205,0]), 3); //yellow2
            //            var lineSymbolGreen = new SimpleLineSymbol(SimpleLineSymbol.STYLE_SOLID, new Color([0, 255, 127]), 3); //green
            //            var lineSymbolBlue = new SimpleLineSymbol(SimpleLineSymbol.STYLE_SOLID, new Color([131,111,255]), 3); //blue
            //            var lineSymbolBrown = new SimpleLineSymbol(SimpleLineSymbol.STYLE_SOLID, new Color([205,112,84]), 3); //brownSalmon
            //            var lineSymbolOrange = new SimpleLineSymbol(SimpleLineSymbol.STYLE_SOLID, new Color([238,154,0]), 3); //orange1


            //            var renderer = new UniqueValueRenderer(defaultlineSymbol, "InterventionTypeID");

            //            if (featureSet!=null)
            //            {
            //                if (featureSet.features.length != 0) {


            //                    for (var i = 0; i < featureSet.features.length; i++) {

            //                        var InterventionTypeID = featureSet.features[i].attributes.InterventionTypeID;
            //                        LineSiteIds.push(InterventionTypeID);
            //                        if (InterventionTypeID == "1") {
            //                            renderer.addValue(featureSet.features[i].attributes.InterventionTypeID, lineSymbolSGreen);
            //                        }
            //                        else if (InterventionTypeID == "2") {
            //                            renderer.addValue(featureSet.features[i].attributes.InterventionTypeID, lineSymbolYellow);
            //                        }
            //                        else if (InterventionTypeID == "3") {
            //                            renderer.addValue(featureSet.features[i].attributes.InterventionTypeID, lineSymbolGreen);
            //                        }
            //                        else if (InterventionTypeID == "4") {
            //                            renderer.addValue(featureSet.features[i].attributes.InterventionTypeID, lineSymbolBlue);
            //                        }
            //                        else if (InterventionTypeID == "5") {
            //                            renderer.addValue(featureSet.features[i].attributes.InterventionTypeID, lineSymbolBrown);
            //                        }
            //                        else if (InterventionTypeID == "6") {
            //                            renderer.addValue(featureSet.features[i].attributes.InterventionTypeID, lineSymbolOrange);
            //                        }
            //                        else {
            //                            renderer.addValue(featureSet.features[i].attributes.InterventionTypeID, defaultlineSymbol);
            //                        }
            //                    }
            //                }
            //            }
            //            PolylineLayer.setRenderer(renderer);
            //            PolylineLayer.refresh();

            //        });

            //        PolygonLayer.queryFeatures(query, function (featureSet) {

            //            var DefaultSelectionfillSymbol = new SimpleFillSymbol(SimpleFillSymbol.STYLE_SOLID,
            //          new SimpleLineSymbol(SimpleLineSymbol.STYLE_SQUARE, new Color([85, 255, 0]), 2), new Color([85, 255, 0])); //lightgreen

            //            var SelectionfillSymbolGreen = new SimpleFillSymbol(SimpleFillSymbol.STYLE_SOLID,
            //        new SimpleLineSymbol(SimpleLineSymbol.STYLE_SQUARE, new Color([154, 205, 50]), 2), new Color([154, 205, 50])); //olive

            //            var SelectionfillSymbolYellow = new SimpleFillSymbol(SimpleFillSymbol.STYLE_SOLID,
            //            new SimpleLineSymbol(SimpleLineSymbol.STYLE_SQUARE, new Color([238, 201, 0]), 2), new Color([238, 201, 0])); //gold

            //            var SelectionfillSymbolDGreen = new SimpleFillSymbol(SimpleFillSymbol.STYLE_SOLID,
            //           new SimpleLineSymbol(SimpleLineSymbol.STYLE_SQUARE, new Color([0, 139, 0]), 2), new Color([0, 139, 0])); //Dark green

            //            var SelectionfillSymbolBlue = new SimpleFillSymbol(SimpleFillSymbol.STYLE_SOLID,
            //          new SimpleLineSymbol(SimpleLineSymbol.STYLE_SQUARE, new Color([96, 123, 139]), 2), new Color([96, 123, 139])); //Light sky blue

            //            var SelectionfillSymbolBrown = new SimpleFillSymbol(SimpleFillSymbol.STYLE_SOLID,
            //         new SimpleLineSymbol(SimpleLineSymbol.STYLE_SQUARE, new Color([139, 62, 47]), 2), new Color([139, 62, 47])); //Brown

            //            var SelectionfillSymbolOrange = new SimpleFillSymbol(SimpleFillSymbol.STYLE_SOLID,
            //         new SimpleLineSymbol(SimpleLineSymbol.STYLE_SQUARE, new Color([205,133,63]), 2), new Color([205,133,63])); //Peru

            //            var renderer = new UniqueValueRenderer(DefaultSelectionfillSymbol, "InterventionTypeID");

            //            if (featureSet != null) {
            //                if (featureSet.features.length != 0) {


            //                    for (var i = 0; i < featureSet.features.length; i++) {

            //                        var InterventionTypeID = featureSet.features[i].attributes.InterventionTypeID;
            //                        PolygonSiteIds.push(InterventionTypeID);
            //                        if (InterventionTypeID == "1") {
            //                            renderer.addValue(featureSet.features[i].attributes.InterventionTypeID, SelectionfillSymbolGreen);
            //                        }
            //                        else if (InterventionTypeID == "2") {
            //                            renderer.addValue(featureSet.features[i].attributes.InterventionTypeID, SelectionfillSymbolYellow);
            //                        }
            //                        else if (InterventionTypeID == "3") {
            //                            renderer.addValue(featureSet.features[i].attributes.InterventionTypeID, SelectionfillSymbolDGreen);
            //                        }
            //                        else if (InterventionTypeID == "4") {
            //                            renderer.addValue(featureSet.features[i].attributes.InterventionTypeID, SelectionfillSymbolBlue);
            //                        }
            //                        else if (InterventionTypeID == "5") {
            //                            renderer.addValue(featureSet.features[i].attributes.InterventionTypeID, SelectionfillSymbolBrown);
            //                        }
            //                        else if (InterventionTypeID == "6") {
            //                            renderer.addValue(featureSet.features[i].attributes.InterventionTypeID, SelectionfillSymbolOrange);
            //                        }
            //                        else {
            //                            renderer.addValue(featureSet.features[i].attributes.InterventionTypeID, DefaultSelectionfillSymbol);
            //                        }
            //                    }
            //                }
            //            }
            //            PolygonLayer.setRenderer(renderer);
            //            PolygonLayer.refresh();
            //        });

            //        $("#pane10").show();

            //    });

            map.on("load", function () {
                //SessionTiomeout();
                QBresultViewOnMap();

            });

            var featureLayer = "";
            var Shape = "";
            function QBresultViewOnMap() {

                var featureURL = "";

                var queryString = getQueryStrings();
                var shapeType = queryString["ShapeType"];

                if (shapeType == "1") {
                    featureURL = PointLayerURL;
                    featureLayer = PointLayer;
                    Shape = "1";
                }
                else if (shapeType == "2") {
                    featureURL = PolylineLayerURL;
                    featureLayer = PolylineLayer;
                    Shape = "2";
                }
                else if (shapeType == "3") {
                    featureURL = PolygonLayerURL;
                    featureLayer = PolygonLayer;
                    Shape = "3";
                }

                //  featureSelection = featureLayer.on("selection-complete", inputFeatureLayerLayerSelectionComplete);

                if (queryString != 'error') {
                    var SiteIds = queryString["siteids"];
                    var ConnectionNo;
                    var setSelectionSymbol;
                    var querytest = new Query();
                    //  var QueryTaskViewOnMap = new QueryTask(featureURL);                 
                    querytest.where = "InterventionSiteID in (" + SiteIds + ")";
                    //  querytest.outFields = ["*"];
                    //  querytest.returnGeometry = "true";
                    // QueryTaskViewOnMap.execute(querytest, RESULTSHOWONMAP, resulterr);

                    featureLayer.selectFeatures(querytest, FeatureLayer.SELECTION_NEW, function (result) {

                    });

                }
                //  function RESULTSHOWONMAP(result) {




                //    MapSelectionAndZoomExtent(featureLayer, result)
                //featureLayer.selectFeatures(querytest, FeatureLayer.SELECTION_NEW, function (result) {

                //});

                //   }

                //function resulterr(result) {

                //    var ResultFeatures = result;
                //}
            }

        });



function getQueryStrings() {
    try {
        var assoc = {};
        var decode = function (s) { return decodeURIComponent(s.replace(/\+/g, " ")); };
        var queryString = location.search.substring(1);
        var keyValues = queryString.split('&');

        for (var i in keyValues) {
            var key = keyValues[i].split('=');
            if (key.length >= 1) {
                assoc[decode(key[0])] = decode(key[1]);
            }
        }

    } catch (e) {

        return 'error';
    }
    return assoc;
}

