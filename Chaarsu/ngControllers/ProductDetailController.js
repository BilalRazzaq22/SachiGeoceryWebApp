
var objCommon = new Common();

suchiapp.controller("ProductDetailController", function ($scope) {
    $scope.loader = true;
    $scope.start = 0;
    $scope.TotalReviews = 0;
    $scope.d = {};
    $scope.d.message = '';
    GetTrendingProducts = function () {
        var data = {
            PageIndex: 1,
            PageSize: 12,
            SortColumn: "",
            SortOrder: "",
            SearchText: "",
            CategoryId: "29",
            SubCategoryId: "",
            GroupId: ""
        }
        objCommon.AjaxCall("Products/GetAllProducts", $.param(data), "GET", true, function (data) {
            console.log(data);
            $scope.TrandingProductList = data.response;
            $scope.$apply();
        });
    }

    $scope.GetProductDetail = function (productNameUrl) {
        $("body").css({ "opacity": "0.5" });
        $scope.loader = true;
        GetTrendingProducts();
        var data = {
            productNameUrl: productNameUrl
        }
        objCommon.AjaxCall("Products/GetProductDetail", $.param(data), "GET", true, function (d) {
            console.log(d);
            $scope.ProductDetail = d.response.ProductDetail;
            $scope.ProductImages = d.response.ProductImages;
            $scope.GetAllProductReviews(d.response.ProductDetail.PRODUCT_ID);
            $("body").css({ "opacity": "1" });
            $scope.loader = false;
            $scope.$apply();
            $('.product-slick').slick({
                slidesToShow: 1,
                slidesToScroll: 1,
                arrows: false,
                fade: true,
                asNavFor: '.slider-nav'
            });
            $('.slider-nav').slick({
                vertical: false,
                slidesToShow: 4,
                slidesToScroll: 1,
                centerMode: true,
                asNavFor: '.product-slick',
                arrows: true,
                dots: false,
                focusOnSelect: true
            });
            $('.add-product img').elevateZoom({
                zoomType: "inner",
                scrollZoom: true
            });
            (function ($) {
                "use strict";
                // trending-product-container swiper slider init
                var trendingContainer = new Swiper('.trending-product-container', {
                    slidesPerView: 4,
                    loop: true,
                    navigation: {
                        nextEl: '.trending-slider-next',
                        prevEl: '.trending-slider-prev',
                    },
                    spaceBetween: 30,
                    breakpoints: {
                        1200: {
                            slidesPerView: 3
                        },
                        990: {
                            slidesPerView: 3
                        },
                        768: {
                            slidesPerView: 2
                        },
                        540: {
                            slidesPerView: 1
                        },
                        400: {
                            slidesPerView: 1
                        }
                    }
                });
            })(jQuery);
            $(document).ready(function () {
                $("input[type='radio']").click(function () {
                    var sim = $("input[type='radio']:checked").val();
                    //alert(sim);
                    if (sim < 3) { $('.myratings').css('color', 'red'); $(".myratings").text(sim); }
                    else { $('.myratings').css('color', 'green'); $(".myratings").text(sim); }
                });
            });
        });
    }

    $scope.GetAllProductReviews = function (id) {
        var data = {
            PRODUCT_ID: id,
            PageIndex: 1,
            PageSize: 5,
            SortColumn: 'CREATED_DATE',
            SortOrder: 'desc',
            SearchText: ''
        }
        objCommon.AjaxCall("Products/GetAllProductReviews", $.param(data), "GET", true, function (d) {
            console.log(d);
            $scope.TotalReviews = d.response.ProductReviews[0].TotalRecords;
            $scope.ProductReviews = d.response.ProductReviews;
            $scope.$apply();
        });

    }

    $scope.startValue = function (start) {
        $scope.start = start;
    }

    $scope.SubmitReview = function (productId) {
        if ($scope.d.message == '') {
            objCommon.ShowMessage("Message field is empty", "error");
        }
        else {
            var data = {
                PRODUCT_REVIEW_ID: 0,
                USER_ID: 0,
                PRODUCT_ID: productId,
                REVIEW: $scope.start,
                COMMENT: $scope.d.message
            }
            objCommon.AjaxCall("Products/AddReviews", JSON.stringify(data), "POST", true, function (d) {
                objCommon.ShowMessage(d.RetMessage, "success");
                $scope.start = 0;
                $scope.message = '';
                $('#star1,#star2,#star3,#star4,#star5').prop('checked', false);
                $scope.GetAllProductReviews(productId);
                $scope.$apply();
            })
        }
    }

});