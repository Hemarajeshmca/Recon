	  $(document).ready(function() {
	      $(window).resize(function() {
	          if ($(window).width() < 768) {
	              var height1 = $(window).height() - 85;
	              $("#basicNode,#basicNode2,#basicNode3,#basicNode4,#basicNode5").css('height', height1);
	              $('.select-sites').html("");
	              $('.analyze-sites').html("");
	          } else {
	              var height1 = $(window).height() * .8;
	              $("#basicNode,#basicNode2,#basicNode3,#basicNode4,#basicNode5").css('max-height', height1);
	              $('.select-sites').text("Identify sites");
	              $('.analyze-sites').text("Analyze Sites");
	          }
	      });

	      if ($(window).width() < 768) {
	          var height1 = $(window).height() - 85;
	          $("#basicNode,#basicNode2,#basicNode3,#basicNode4,#basicNode5").css('height', height1);

	      } else {
	          var height1 = $(window).height() * .8;
	          $("#basicNode,#basicNode2,#basicNode3,#basicNode4,#basicNode5").css('max-height', height1);
	      }


	  })

	  $(document).ready(function() {
	      $('#toggleButton1').click(function() {
	          $('#basicNode1').slideToggle("slow");
	          $(this).toggleClass("chevron-up chevron-down");
	      });


	      if ($(window).width() < 767) {
	          $('.select-sites').html("");
	          $('.analyze-sites').html("");
	      } else {
	          $('.select-sites').text("Identify sites");
	          $('.analyze-sites').text("Analyze Sites");
	      }

	      /////////////////workfoce demography checkbox/////////////
	      $(".checkPopulation").click(function() {
	          $("#checkoutPopulation").toggle(this.checked);
	      }).triggerHandler('click');

	      $(".checkRadius").click(function() {
	          $("#checkoutRadius").toggle(this.checked);
	      }).triggerHandler('click');

	      //radio options to hide or show//
	      $("div.desc").hide();
	      $("input[name$='radio-2-set']").click(function() {
	          var test = $(this).val();
	          $("div.desc").hide();
	          $("#" + test).show();
	      });

	      $("div.desc").hide();
	      $("input[name$='radio-3-set']").click(function() {
	          var test = $(this).val();
	          $("div.desc").hide();
	          $("#" + test).show();
	      });

	      //click to next for Industries group
	      $('#Next_Indutries').on('click', function() {
	          $('.radioBtnGroup').addClass('hideCounty');
	          $('.radioBtnGroup_2').removeClass('hideCounty');
	          $('.switchIndustries').css('display', 'block');

	      });
	      $('#Submit_Category').on('click', function() {
	          $('.tools-frame-bottom').css('display', 'block');
	          $("#ResultContentPanel").css('display', 'block');
	      });





	  });


	  /////////////////////////////////////////////
	  //          Bottom quickLayerToggle       //
	  ////////////////////////////////////////////
	  $(document).ready(function() {
	      $('#quickToolBarToggler').click(function() {
	          $(this).toggleClass("quick-toggler-btn1 quick-toggler-btn");
	          $('.quick-layer-toggle').toggleClass('quick-layer-toggle-small');
	          var hidden = $('#quickToolBarUl');
	          if (hidden.hasClass('visible')) {
	              hidden.animate({
	                  "left": ""
	              }).removeClass('visible');
	              $("#quick-layer-toggle-title").hide()

	          } else {
	              hidden.animate({
	                  "left": "0px"
	              }).addClass('visible');
	              $("#quick-layer-toggle-title").show()

	          }
	      });



	  });
	  /////////////////////////////////////////////
	  //        Bottom quickLayerToggle End      //
	  ////////////////////////////////////////////


	  //////////////////////////////////////
	  //       Filter  --End            //
	  ////////////////////////////////////  

	  $(function() {
	      $("#slider-range").slider({
	          range: true,
	          min: 0,
	          max: 500,
	          values: [75, 300],
	          slide: function(event, ui) {
	              $("#amount").val("$" + ui.values[0] + " - $" + ui.values[1]);
	          }
	      });
	      $("#amount").val("$" + $("#slider-range").slider("values", 0) +
	          " - $" + $("#slider-range").slider("values", 1));
	  });

	  //////////////////////////////////////
	  //       Filter  --End             //
	  ////////////////////////////////////  



	  ////////////////////////////////
	  //      select sites         //
	  //////////////////////////////
	  $(function() {
	      $('#togglePaneShow5').on('click', function() {
	          $("#sidebarContent").removeClass('margin-left')
	          $('ul.top-menu li span.selected').css({
	              'background': 'url(images/top-menu-selected_1.png) no-repeat'
	          });
	          $('ul.top-menu li span.viewBTN').css({
	              'background': 'url(images/top-menu-selected.png) no-repeat'
	          });
	          $('ul.top-menu li a.select-sites').css({
	              'background': 'url(images/select-site-active.png) no-repeat 6px 3px',
	              'color': '#ffffff'
	          });
	      });

	      $(".closeBtn6").on('click', function() {
	          $("#sidebarContent").addClass('margin-left')
	          $('ul.top-menu li span.selected').css({
	              'background': 'url(images/top-menu-selected.png) no-repeat'
	          });
	          $('ul.top-menu li a.select-sites').css({
	              'background': 'url(images/select-site.png) no-repeat 6px 3px',
	              'color': '#5d5d5d'
	          });
	          $('ul.top-menu li span.viewBTN').css({
	              'background': 'url(images/top-menu-selected_1.png) no-repeat'
	          });
	          $('#pane7').hide();
	          $('#pane8').hide();
	          $('#pane9').hide();
	          $('#pane10').hide();
	          $('#pane11').hide();
	          $('#pane12').hide();
	          $('#pane13').hide();
	          $('#pane14').hide();
	          $('#' + 'pane' + id).css('display');
	          $('#' + 'pane' + id).show();

	      })


	      $('.viewBTN').on('click', function() {
	          $("#sidebarContent").addClass('margin-left')
	          $('ul.top-menu li span.selected').css({
	              'background': 'url(images/top-menu-selected.png) no-repeat'
	          });
	          $('ul.top-menu li a.select-sites').css({
	              'background': 'url(images/select-site.png) no-repeat 6px 3px',
	              'color': '#5d5d5d'
	          });
	          $('ul.top-menu li span.viewBTN').css({
	              'background': 'url(images/top-menu-selected_1.png) no-repeat'
	          });
	          $('#pane7').hide();
	          $('#pane8').hide();
	          $('#pane9').hide();
	          $('#pane10').hide();
	          $('#pane11').hide();
	          $('#pane12').hide();
	          $('#pane13').hide();
	          $('#' + 'pane' + id).css('display');
	          $('#' + 'pane' + id).show();

	      });

	  })
	  $(document).ready(function() {
	      $('.button_blue').on('click', function() {
	          $(this).removeClass('activeBTN');
	          $(this).addClass('activeBTN');
	      });


	      //click to open intervention sites details Grid
	      $('.openResultGrid').click(function () {
	          $(".addInterventionSites").removeClass('activeTools');
	          $(".openInterventionModal").removeClass('activeTools');
	          $(this).addClass('activeTools');
	          $(".tools-frame-bottom").removeClass('hideCounty').css('display', 'block');
	      });
	      
	      $('.addInterventionSites').click(function () {
	          $(".openResultGrid").removeClass('activeTools');
	          $(".openInterventionModal").removeClass('activeTools');
	          $(this).addClass('activeTools');
	          //$("#ResultContentPanel").hide();

	      });
	      $('.openInterventionModal').click(function () {
	          $(".addInterventionSites").removeClass('activeTools');
	          $(".openResultGrid").removeClass('activeTools');
	          $(this).addClass('activeTools');
	          //$("#ResultContentPanel").hide();

	      });

	      //   $("#ResultPaneClose").click(function() {
	      //       $('.openResultGrid').removeClass('activeTools');
	      //   })
	  });


	  //****on select option open filter****//
	  /*$(function() {
    $('').change(function(e) {
        var selected = $(e.target).val();
        //console.dir(selected);
		$('#pane15').show();
    }); 
});*/


	  $(document).ready(function() {
	      $("#pane15").hide();
	      $("#msaSelectOption").change(function() {
	          var selectedValue = $(this).val();
	          if (selectedValue !== "0") {
	              $("#pane15").show();
	              $(selectedValue).hide();
	          }
	      });



	      $("#FlipResultTitle").click(function() {
	          //$(this).find('i').toggleClass('fa-plus-circle fa-minus-circle');
	          $("#ResultContentPanel").slideToggle("fast");
	      });

	      $("#FlipResultTitle1").click(function () {
	          //$(this).find('i').toggleClass('fa-plus-circle fa-minus-circle');
	          $("#ResultContentPanel1").slideToggle("fast");
	      });

	      $('.tools-frame-bottom > .tools-close').on('click', function() {
	          $('.tools-frame-bottom').hide();
	      })

	      $('.OpenResultCont').on('click', function() {
	          $('.tools-frame-bottom').toggleClass('hideCounty');
	      });
	  });


	  //***ResultPanel End***//


	  ////////////////////////////////
	  //      select sits  End     //
	  //////////////////////////////




	  ////////////////////////////////
	  //      Popup for all      //
	  //////////////////////////////
	  function a_onClick(id) {
	      //$('#pane1').hide();
	      //$('#pane2').hide();
	      //$('#pane3').hide();
	      //$('#pane4').hide();
	      //$('#pane5').hide();
	      //$('#pane6').hide();
	      //$('#pane7').hide();
	      //$('#pane8').hide();
	      //$('#pane9').hide();
	      //$('#pane10').hide();
	      //$('#pane11').hide();
	      //$('#pane12').hide();
	      //$('#pane13').hide();
	      //$('#pane14').hide();

	      $('#' + 'pane' + id).css('display');
	      $('#' + 'pane' + id).show();
	      //for site button add remove active
	      $('.button_blue').removeClass('activeBTN');
	      $('.switchDirveTime').css('display', 'block');

	  }

	  function closePane(id) {
	      $('#' + 'pane' + id).hide();
	  }


	  function maxMinPane(id) {
	      $('#' + 'showHide' + id).toggleClass("tools-minimize1 tools-minimize");
	      var hide1 = $('#' + 'showBasic' + id).css('display');
	      var paneId = '#' + 'pane' + id;
	      var panshowhide = paneId + ", " + paneId + " .tools-title ";
	      if (hide1 == 'block') {
	          hide1 = false;
	          $('#' + 'showBasic' + id).css('display', 'none');
	          $('#' + 'hideBtn' + id).css('display', 'none');
	          $('#' + 'showHide' + id).css('right', '5px');
	          $(panshowhide).css('width', '150px');
	      } else {
	          hide1 = true;
	          $('#' + 'showBasic' + id).css('display', 'block');
	          $('#' + 'hideBtn' + id).css('display', 'block');
	          $('#' + 'showHide' + id).css('right', '40px');
	          $(panshowhide).css('width', '320px');
	      }
	  }
       
	  //var districtid="";
	  //var districtName="";
	  //var blockid="";
	  //var blockName="";
	  //var villageid="";
	  //var villageName= "";

	  //$(document).ready(function () {

	  //    $.ajax({
	  //        url: GetDataListForDropDown.DataDropDownURL,
	  //        type: "GET",
	  //        contentType: "application/json;charset=utf-8",
	  //        traditional: true,
	  //        success: function (result) {

	  //            if (result._lstDistrictList != null && result._lstDistrictList.length != 0) {
	  //                comboboxBind(result._lstDistrictList, "ddlDistrict", "DistrictName", "DistrictID");

	  //                for (var i = 0; i < result._lstDistrictList.length; i++) {
	  //                    districtid =districtid + result._lstDistrictList[i].DistrictID + ",";
	  //                    districtName = districtName + result._lstDistrictList[i].DistrictName + ",";
	  //                }
	  //                districtid = districtid.replace(/,\s*$/, "");
	  //                districtName = districtName.replace(/,\s*$/, "");
	  //                districtName = '\'' + districtName.split(',').join('\',\'') + '\'';
                       
	  //              //  var idx = districtid.lastIndexof(",");
	  //               // districtid = districtid.remove(idx);
	  //                //    $("#ddlDistrict").html("");
	  //                //    $("#ddlDistrict").append
	  //                //($('<option></option>').val(0).html("---Select District---"));
	  //                //for (var i = 0; i < result._lstDistrictList.length;i++)
	  //                //{
	  //                //    $("#ddlDistrict").append($('<option></option>').val(result._lstDistrictList[i].DistrictID).html(result._lstDistrictList[i].DistrictName))
	  //                //}

	  //            }
	  //            else {
	  //                $("#ddlDistrict").html("");
	  //                $("#ddlDistrict").append
	  //               ($('<option></option>').val(0).html("---Select ---"));
	  //            }

	  //            if (result._lstBlockList != null && result._lstBlockList.length != 0) {

	  //                comboboxBind(result._lstBlockList, "ddlBlock", "BlockName", "BlockID");

	  //                for (var i = 0; i < result._lstBlockList.length; i++) {
	  //                    blockid = blockid + result._lstBlockList[i].BlockID + ",";
	  //                    blockName = blockName + result._lstBlockList[i].BlockName + ",";
	  //                }
	  //                blockid = blockid.replace(/,\s*$/, "");
	  //                blockName = blockName.replace(/,\s*$/, "");
	  //                blockName = '\'' + blockName.split(',').join('\',\'') + '\'';
	  //                // $("#ddlBlock").html("");
	  //                // $("#ddlBlock").append
	  //                //($('<option></option>').val(0).html("---Select Blocks---"));
	  //                // for (var i = 0; i < result._lstBlockList.length; i++)
	  //                //     {
	  //                //     $("#ddlBlock").append($('<option></option>').val(result._lstBlockList[i].BlockID).html(result._lstBlockList[i].BlockName))
	  //                //     }
	  //            }
	  //            else {

	  //                 $("#ddlBlock").html("");
	  //                 $("#ddlBlock").append
	  //                ($('<option></option>').val(0).html("---Select---"));
	  //            }
	             
	  //            if (result._lstVillageList != null && result._lstVillageList.length != 0) {

	  //                comboboxBind(result._lstVillageList, "ddlVillage", "VillageName", "VillageID");
	  //                for (var i = 0; i < result._lstVillageList.length; i++) {
	  //                    villageid = villageid + result._lstVillageList[i].VillageID + ",";
	  //                    villageName = villageName +  result._lstVillageList[i].VillageName  + ",";
	                   
	  //                }
	  //                villageid = villageid.replace(/,\s*$/, "");
	  //                villageName = villageName.replace(/,\s*$/, "");
	  //                villageName = '\'' + villageName.split(',').join('\',\'') + '\'';
	  //                //	                  $("#ddlVillage").html("");
	  //                //	                  $("#ddlVillage").append
	  //                //($('<option></option>').val(0).html("---Select Villages---"));
	  //                //	                  for (var i = 0; i < result._lstVillageList.length; i++)
	  //                //                          {
	  //                //	                      $("#ddlVillage").append($('<option></option>').val(result._lstVillageList[i].VillageID).html(result._lstVillageList[i].VillageName))
	  //                //	                  }
	  //            }
	  //            else {
	  //                 $("#ddlVillage").html("");
	  //                $("#ddlVillage").append($('<option></option>').val(0).html("---Select---"));
	  //            }


	  //           // OnDistrictChange('ddlDistrict', result);
	  //           // OnBlockChange('ddlBlock', result);
	  //           // OnBlockChange('ddlVillage', result);
	             
	  //        },
	  //        error: function (e)
	  //        {
	  //            alert(e);
	  //        }
	  //    });

	  //});
	 
	 

  //   $(document).ready(function () {

  //       districtid = $('#ddlDistrict:selected').val();
  //       districtName = $('#ddlDistrict :selected').text();
  //       blockid = $('#ddlBlock :selected').val();
  //         blockName = $('#ddlBlock :selected').text();
  //         villageid = $('#ddlVillage :selected').val();
  //         villageName = $('#ddlVillage :selected').text();

  //});
     
    

	  /////////////////////////////
	  //   Popup for all End     //
	  //////////////////////////////


	  // JavaScript Document