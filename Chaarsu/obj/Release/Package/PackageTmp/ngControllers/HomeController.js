
var objCommon = new Common();
suchiapp.controller("HomeController", function ($scope, $window) {
    $scope.loader = false;
 
   



    $scope.GetProductByCategory = function (NAME) {
        $window.location.href = objCommon.baseUrl + 'Products/Index?group=' + NAME;
    }
    
});

$("body").css({ "opacity": "1" });

function cartopen() {
    document.getElementById("sitebar-cart").classList.add('open-cart');
    document.getElementById("sitebar-drawar").classList.add('hide-drawer');
}

function cartclose() {
    document.getElementById("sitebar-cart").classList.remove('open-cart');
    document.getElementById("sitebar-drawar").classList.remove('hide-drawer');
}

$(document).ready(function () {
    $("#preloader").hide();
    $("input[type='radio']").click(function () {
        var sim = $("input[type='radio']:checked").val();
        //alert(sim);
        if (sim < 3) { $('.myratings').css('color', 'red'); $(".myratings").text(sim); }
        else { $('.myratings').css('color', 'green'); $(".myratings").text(sim); }
    });

    (function ($) {
        "use strict";

        // catagory-container swiper slider init
        var catagoryContainer2 = new Swiper('.category-container2', {
            slidesPerView: 6,
            autoplay: {
                delay: 2500,
                disableOnInteraction: false,
            },
            loop: true,
            navigation: {
                nextEl: '.catagory-slider-next',
                prevEl: '.catagory-slider-prev',
            },
            spaceBetween: 30,
            breakpoints: {
                1300: {
                    slidesPerView: 4
                },
                768: {
                    slidesPerView: 3
                },
                540: {
                    slidesPerView: 2
                },
                400: {
                    slidesPerView: 2
                }
            }
        });

        // catagory-container swiper slider init
        var catagoryContainer = new Swiper('.catagory-container', {
            slidesPerView: 6,
            loop: true,
            navigation: {
                nextEl: '.catagory-slider-next',
                prevEl: '.catagory-slider-prev',
            },
            spaceBetween: 30,
            breakpoints: {
                990: {
                    slidesPerView: 4
                },
                768: {
                    slidesPerView: 2
                },
                540: {
                    slidesPerView: 2
                },
                400: {
                    slidesPerView: 2
                }
            }
        });


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

        // trending-product-container swiper slider init
        var recommendContainer = new Swiper('.recommend-product-container', {
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

        // brand-feature-product-container swiper slider init
        var recommendContainer = new Swiper('.feature-brand-container', {
            slidesPerView: 5,
            loop: true,
            navigation: {
                nextEl: '.brand-feature-slider-next',
                prevEl: '.brand-feature-slider-prev',
            },
            spaceBetween: 30,
            breakpoints: {
                1200: {
                    slidesPerView: 4
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

        // trending-product-container swiper slider init
        var testimonialContainer = new Swiper('.testimonial-container', {
            slidesPerView: 1,
            loop: true,
            navigation: {
                nextEl: '.testimonial-slider-next',
                prevEl: '.testimonial-slider-prev',
            },
            spaceBetween: 30,
        });

        // banner-slider-container swiper slider init
        var banneSliderConainer = new Swiper('.banner-slider-container', {
            slidesPerView: 1,
            loop: true,
            spaceBetween: 0,
            speed: 900,
            pagination: {
                el: '.swiper-pagination',
                clickable: true
            }
        });

        // infoBoxContainer swiper slider init
        var infoBoxContainer = new Swiper('.info-box-container', {
            slidesPerView: 3,
            loop: true,
            centeredSlides: true,
            initialSlide: 2,
            spaceBetween: 30,
            autoplay: {
                delay: 3500,
                disableOnInteraction: false,
            },
            breakpoints: {
                990: {
                    slidesPerView: 2
                },
                767: {
                    slidesPerView: 1
                }
            }
        });


        //popup
        $('.popup-close,.popup-overlay').on("click", function () {
            $('#popup').hide();
        });
        $(document).ready(function () {
            $("#popup").delay(2000).fadeIn();
        });

        if ($(window).width() > 990) {
            $(document).ready(function () {
                $('.sidebar')
                    .theiaStickySidebar({
                        additionalMarginTop: 110
                    });
            });
        }

        $(function () {
            setNavigation();
        });

        function setNavigation() {
            var pathArray = window.location.pathname.split('/');
            var lastItem = pathArray.pop();
            $(".menu a").each(function () {
                var href = $(this).attr('href');
                if (lastItem.substring(0, href.length) === href) {
                    var myLi = $(this).closest('li');
                    myLi.addClass('active');
                    myLi.parent().parent().addClass('active');
                }
            });
        }

        $('.view').on('click', function () {
            // $(this).text("Show Less"); 
            $(this).parents('.order-card').addClass("show")
        });
        $('.show-less').on('click', function () {
            // $(this).text("Show Less"); 
            $(this).parents('.order-card').removeClass("show")
        });

        $(".right-nav-menu-toggle").on("click", function (e) {
            $('.right-nav-menu').toggleClass('open');
        });

    })(jQuery);

});


