var objCommon = new Common();
var isFilter = false;
var catId = 0;
suchiapp.controller("ProductsController", function ($scope) {

    $scope.loader = true;
    $scope.pageIndex = 1;
    $scope.pageSize = 15;
    $scope.sortColumn = "CREATED_DATE";
    $scope.sortOrder = "asc";
    $scope.searchText = "";
    $scope.categoryId = "";
    $scope.subCategoryId = "";
    $scope.groupId = "";
    $scope.isMenu = "";
    $scope.MinPrice = 0;
    $scope.MaxPrice = 0;

    $scope.GetAllProducts = function (catid, search, group, isFromMenu) {
        isFilter = false;
        $scope.categoryId = catid;
        $scope.searchText = search;
        $scope.groupId = group;
        $scope.isMenu = isFromMenu;

        if (isFromMenu) {
            $scope.GetProductsByCategory(catid, "False");
            return;
        }

        if ($scope.categoryId == undefined || $scope.categoryId == null)
            $scope.categoryId = "";
        if ($scope.searchText == undefined || $scope.searchText == null)
            $scope.searchText = "";
        if ($scope.groupId == undefined || $scope.groupId == null)
            $scope.groupId = "";

        $("body").css({ "opacity": "0.5" });
        $scope.loader = true;
        var data = {
            PageIndex: $scope.pageIndex,
            PageSize: $scope.pageSize,
            SortColumn: $scope.sortColumn,
            SortOrder: $scope.sortOrder,
            SearchText: $scope.searchText,
            CategoryId: $scope.categoryId,
            SubCategoryId: $scope.subCategoryId,
            GroupId: $scope.groupId,
            MinPrice: $scope.MinPrice,
            MaxPrice: $scope.MaxPrice
        }
        objCommon.AjaxCallAPS("Products/GetAllProducts", $.param(data), "GET", true, function (data) {

            $scope.Products = data.response;
            $scope.loader = false;
            $("body").css({ "opacity": "1" });
            $scope.$apply();
            initPriceFilter();
            if (data.response.length == 0) {
                $('#btnloadmore').addClass('d-none');
                $('#txtloadmore').removeClass('d-none');
            }
            else {
                $('#btnloadmore').removeClass('d-none');
                $('#txtloadmore').addClass('d-none');
            }

            if (data.response[0].TotalRecords < $scope.pageSize) {
                $('#btnloadmore').addClass('d-none');
            }

            $(".anchortag").css("font-weight", "200");
            $("#" + $scope.categoryId + "anchor").css("font-weight", "700");
            setTimeout(function () {
                $("#maincategory").val($scope.groupId + "");
                $("#txtSearchBox").val($scope.searchText);
            }, 300);
        }, null, "Error while getting products, Please try again.", $scope);
    }


    $scope.GetProductsByCategory = function (categoryId, isLoadMore) {
        catId = categoryId;
        $("body").css({ "opacity": "0.5" });
        $scope.loader = true;
        $scope.categoryId = categoryId;
        if (isLoadMore == "False") {
            $scope.pageIndex = 1;
        }
        isFilter = true;
        var data = {
            CategoryId: $scope.categoryId,
            PageIndex: $scope.pageIndex,
            PageSize: $scope.pageSize
        }
        objCommon.AjaxCallAPS("Products/GetAllProductsByCategoryId", $.param(data), "GET", true, function (data) {
            isFilter = true;
            $scope.Products = data.response;
            $scope.loader = false;
            $("body").css({ "opacity": "1" });
            $scope.$apply();

            if (data.response.length == 0) {
                $('#btnloadmore').addClass('d-none');
                //$("#btnloadmore").prop('disabled', true);
                $('#txtloadmore').text('No More Products');
                //$('#txtloadmore').removeClass('d-none');
            }
            else {
                //$('#btnloadmore').removeClass('d-none');
                $('#txtloadmore').addClass('d-none');
                $("#btnloadmore").prop('disabled', false);
            }

            if (data.response[0].TotalRecords < $scope.pageSize) {
                $('#btnloadmore').addClass('d-none');
                //$("#btnloadmore").prop('disabled', true);
                $('#txtloadmore').text('No More Products');
            }

            //$(".anchortag").css("font-weight", "200");
            //$("#" + $scope.categoryId + "anchor").css("font-weight", "700");
            //setTimeout(function () {
            //    $("#maincategory").val($scope.groupId + "");
            //    $("#txtSearchBox").val($scope.searchText);
            //}, 300)
        }, null, "Error while getting products by category, Please try again.", $scope);

        //window.location.replace(objCommon.baseUrl + "Products/Index?category=" + categoryId + "&search=" + $scope.searchText + "&group=" + $scope.groupId);

        // $scope.GetAllProducts(categoryId, $scope.searchText);
    }

    $scope.LoadeMoreProducts = function () {
        $("body").css({ "opacity": "0.5" });
        $scope.loader = true;
        $scope.pageIndex = $scope.pageIndex + 1;
        if (isFilter) {
            $scope.GetProductsByCategory(catId, "True");
        } else {
            $scope.GetAllProducts();
        }
    }

    $scope.ClearAllFilters = function () {
        $("body").css({ "opacity": "0.5" });
        $scope.loader = true;
        $scope.pageIndex = 1;
        $scope.pageSize = 15;
        $scope.sortColumn = "CREATED_DATE";
        $scope.sortOrder = "asc";
        $scope.searchText = "";
        $scope.categoryId = "";
        $scope.subCategoryId = "";
        $scope.groupId = "";
        $scope.MinPrice = 0;
        $scope.MaxPrice = 0;
        $scope.GetAllProducts();
    }

    function getUrlParameter(sParam) {
        var sPageURL = window.location.search.substring(1),
            sURLVariables = sPageURL.split('&'),
            sParameterName,
            i;

        for (i = 0; i < sURLVariables.length; i++) {
            sParameterName = sURLVariables[i].split('=');

            if (sParameterName[0] === sParam) {
                return sParameterName[1] === undefined ? true : decodeURIComponent(sParameterName[1]);
            }
        }
    };


    function initPriceFilter() {
        var lowerSlider = document.querySelector('#lower');
        var upperSlider = document.querySelector('#upper');

        document.querySelector('#two').value = upperSlider.value;
        document.querySelector('#one').value = lowerSlider.value;

        var lowerVal = parseInt(lowerSlider.value);
        var upperVal = parseInt(upperSlider.value);

        upperSlider.oninput = function () {
            lowerVal = parseInt(lowerSlider.value);
            upperVal = parseInt(upperSlider.value);

            $scope.MinPrice = lowerVal;
            $scope.MaxPrice = upperVal;

            if (upperVal < lowerVal + 4) {
                lowerSlider.value = upperVal - 4;
                if (lowerVal == lowerSlider.min) {
                    upperSlider.value = 4;
                }
            }
            document.querySelector('#two').value = this.value;
        };

        lowerSlider.oninput = function () {
            lowerVal = parseInt(lowerSlider.value);
            upperVal = parseInt(upperSlider.value);



            $scope.MinPrice = lowerVal;
            $scope.MaxPrice = upperVal;

            if (lowerVal > upperVal - 4) {
                upperSlider.value = lowerVal + 4;
                if (upperVal == upperSlider.max) {
                    lowerSlider.value = parseInt(upperSlider.max) - 4;
                }
            }
            document.querySelector('#one').value = this.value;
        };

        if ($scope.MinPrice != 0 && $scope.MaxPrice != 0) {
            document.querySelector('#one').value = $scope.MinPrice;
            document.querySelector('#two').value = $scope.MaxPrice;
            lowerSlider.value = $scope.MinPrice;
            upperSlider.value = $scope.MaxPrice;
        }


        // gets a reference to the heartDOm
        const heartDOM = document.querySelector('.js-heart');
        // initialized like to false when user hasnt selected
        let liked = false;

        // create a onclick listener
        heartDOM.onclick = (event) => {
            // check if liked 
            liked = !liked; // toggle the like ( flipping the variable)

            // this is what we clicked on
            const target = event.currentTarget;

            if (liked) {
                // remove empty heart if liked and add the full heart
                target.classList.remove('far');
                target.classList.add('fafas', 'pulse');
            } else {
                // remove full heart if unliked and add empty heart
                target.classList.remove('fafas');
                target.classList.add('far');
            }
        }
    }

    
});
