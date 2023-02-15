var objCommon = new Common();

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
  

    

    $scope.GetAllProducts = function (catid,search,group) {
        $scope.categoryId = catid;
        $scope.searchText = search;
        $scope.groupId = group;
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
        }
        objCommon.AjaxCallAPS("Products/GetAllProducts", $.param(data), "GET", true, function (data) {
         
            $scope.Products = data.response;
            $scope.loader = false;
            $("body").css({ "opacity": "1" });
            $scope.$apply();

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
            }, 300)
          
        });        
    }


    $scope.GetProductsByCategory = function (categoryId) {
        $("body").css({ "opacity": "0.5" });
        $scope.loader = true;
        $scope.pageSize = 15;
        $scope.categoryId = categoryId;
        window.location.replace(objCommon.baseUrl + "Products/Index?category=" + categoryId + "&search=" + $scope.searchText + "&group=" + $scope.groupId);

       // $scope.GetAllProducts(categoryId, $scope.searchText);
    }

    $scope.LoadeMoreProducts = function () {
        $("body").css({ "opacity": "0.5" });
        $scope.loader = true;
        $scope.pageSize = $scope.pageSize + 15;
        $scope.GetAllProducts();
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

});
