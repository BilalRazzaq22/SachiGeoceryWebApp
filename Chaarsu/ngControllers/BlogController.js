
var objCommon = new Common();

suchiapp.controller("BlogsController", function ($scope) {
    $scope.loader = true;
    $scope.GetAllBlogs = function (pageindex) {
        $scope.loader = true;
        $("body").css({ "opacity": "0.5" });
        $scope.pageIndex = pageindex;
        $scope.pageSize = 100;
        $scope.sortColumn = "CreatedDate";
        $scope.sortOrder = "desc";
        $scope.searchText = "";

        var data = {
            PageIndex: $scope.pageIndex,
            PageSize: $scope.pageSize,
            SortColumn: $scope.sortColumn,
            SortOrder: $scope.sortOrder,
            SearchText: $scope.searchText
        }

        objCommon.AjaxCallAPS("Blogs/GetAllBlogs", $.param(data), "GET", true, function (data) {
            $scope.Blogs = data.allBlogs;
            $scope.loader = false;
            $("body").css({ "opacity": "1" });
            $scope.$apply();
        }, null, "Error while getting all blogs, Please try again.", $scope);
    }
});
