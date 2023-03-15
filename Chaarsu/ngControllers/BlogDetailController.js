
var objCommon = new Common();

suchiapp.controller("BlogsController", function ($scope) {
    $("body").css({ "opacity": "0.5" });
    $scope.loader = true;

    $scope.GetBlogDetail = function (BlogTitleUrl) {
        $scope.loader = true;

        var data = {
            blogTitleUrl: BlogTitleUrl,
        }

        objCommon.AjaxCallAPS("Blogs/GetBlogDetail", $.param(data), "GET", true, function (d) {
            $scope.BlogDetail = d.response.BlogDetail;
            $scope.GetRecentBlogs = d.response.recentBlog;
            $("body").css({ "opacity": "1" });
            $scope.loader = false;
            $scope.$apply();
        }, null, "Error while getting blog details, Please try again.", $scope);

    }
    
});