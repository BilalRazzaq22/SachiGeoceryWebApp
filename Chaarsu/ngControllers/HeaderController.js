var objCommon = new Common();

suchiapp.controller("HeaderController", function ($scope, $window) {

    $scope.pageIndex = 1;
    $scope.pageSize = 15;
    $scope.TotalPrice = 0;
    $scope.TotalCartItems = 0;
    $scope.TextSearch = '';
    $scope.GuestName = '';

    if (localStorage.getItem('Cart') == null) {
        localStorage.setItem('Cart', JSON.stringify([]));
    }

    $scope.loader = true;
    $scope.CategoriesList = [];
    $scope.GroupCategoriesList = [];
    $scope.GroupList = [];
    $scope.CartArray = [];

    objCommon.AjaxCall("Wishes/TotalWishesItems", JSON.stringify({}), "GET", true, function (d) {
        $scope.TotalWishesItems = d;
        $scope.$apply();
    }, null, "Error while getting wishes items, Please try again.", $scope);

    $scope.GetCompanyInfo = function () {

        var lang = localStorage.getItem("lang");
        var lot = localStorage.getItem("lat");

        objCommon.AjaxCall("Home/GetCompanyInfo", $.param({ lang: lang, lot: lot }), "GET", true, function (d) {
            $scope.CompanyInfo = d;
        }, null, "Error while getting company info, Please try again.", $scope);
    }

    $scope.GetBranchId = function () {

        var lang = localStorage.getItem("lang");
        var lot = localStorage.getItem("lat");
        console.log(lang);
        console.log(lot);
        objCommon.AjaxCallAPS("Home/GetBranchId", $.param({ Lang: lang, Lat: lot }), "GET", true, function (d) {

        }, null, "Error while getting branch check the location is ON or not, Please try again.", $scope);
    }

    $scope.GetHeaderCategories = function () {

        var data = {
        };

        objCommon.AjaxCall("Home/GetHeaderCategories", $.param(data), "GET", true, function (d) {


            $scope.CategoriesList = d.Data;
            $scope.$apply();
            setTimeout(function () {
                renderDropdown();
            }, 200)

        }, null, "Error while header categories, Please try again.", $scope);

    }

    $scope.GetGroupAndCategories = function () {

        var data = {
        };

        objCommon.AjaxCall("Home/GetGroupAndCategories", $.param(data), "GET", true, function (d) {
            $scope.GroupCategoriesList = d.Data.GroupAndCatList;
            // $scope.GroupList = d.Data.GroupList;
            if ($("#maincategory")[0].length == 0) {

                $("#maincategory").html("");
                $("#maincategory").append("<option value='0'>Select Category</option>")
                for (var i = 0; i < d.Data.GroupList.length; i++) {
                    var rw = d.Data.GroupList[i];
                    $("#maincategory").append("<option value='" + rw.GROUP_ID + "'>" + rw.NAME + "</option>")
                }
            }


            $scope.$apply();
            setTimeout(function () {
                renderDropdown();
            }, 300)

        }, null, "Error while getting group with categories, Please try again.", $scope);

    }

    $scope.GetProductByGroup = function (NAME) {
        $window.location.href = objCommon.baseUrl + 'Products/Index?group=' + NAME;
    }
    $scope.GetProductByCategory = function (NAME) {
        $window.location.href = objCommon.baseUrl + 'Products/Index?category=' + NAME;
    }
    $scope.SignOut = function () {

        objCommon.AjaxCall("Accounts/SignOut", $.param({}), "GET", true, function (d) {
            if (d == "success") {
                localStorage.setItem('Username', "");
                localStorage.setItem('Mobile', "");
                window.location.replace(objCommon.baseUrl + "Home/Index");
            }
            else {
                objCommon.ShowMessage(d, "error");
            }
        }, null, "Error while sign out user, Please try again.", $scope);

    }

    $scope.AddToWishlist = function (productId) {
        var data = {
            USER_ID: 0,
            PRODUCT_ID: productId,
            CREATED_DATE: '2018-01-18 23:35:59.763'
        };
        objCommon.AjaxCall("Wishes/Add", JSON.stringify(data), "POST", true, function (d) {
            if (d.Status == true) {
                objCommon.AjaxCall("Wishes/TotalWishesItems", JSON.stringify({}), "GET", true, function (d) {
                    $scope.TotalWishesItems = d;
                    $scope.$apply();
                }, null, "Error while getting total wish items, Please try again.", $scope);
                objCommon.ShowMessage(d.RetMessage, "success");
            }
            else {
                objCommon.ShowMessage(d.RetMessage, "error");
            }
        }, null, "Error while adding wish items, Please try again.", $scope);
    }

    $scope.DeleteProductFromWishList = function (productId) {
        $scope.loader = true;
        $("body").css({ "opacity": "0.5" });
        var data = {
            id: productId
        }
        objCommon.AjaxCall("Wishes/Delete", $.param(data), "GET", true, function (d) {
            if (d == "AccessDenied") {
                window.location.replace(objCommon.baseUrl + "Accounts/SignIn");
            }
            else {
                objCommon.AjaxCall("Wishes/TotalWishesItems", JSON.stringify({}), "GET", true, function (d) {
                    $scope.TotalWishesItems = d;
                }, null, "Error while getting wish items, Please try again.", $scope);
                objCommon.ShowMessage("Product remove successfully from your wish list", "success");
                $scope.GetWishlist();
            }
            $scope.$apply();
        }, null, "Error while deleting wish items, Please try again.", $scope);
    }

    $scope.GetWishlist = function () {
        $scope.loader = true;
        $("body").css({ "opacity": "0.5" });
        var data = {
            PageIndex: 1,
            PageSize: 50,
            SortColumn: "CREATED_DATE",
            SortOrder: "desc",
            SearchText: ""
        }
        objCommon.AjaxCall("Wishes/GetWishList", $.param(data), "GET", true, function (d) {
            if (d == "AccessDenied") {
                window.location.replace(objCommon.baseUrl + "Accounts/SignIn");
            }
            else {
                $scope.Wishlist = d;
                $("body").css({ "opacity": "1" });
                $scope.loader = false;
                $scope.$apply();
            }
        }, null, "Error while getting wish list, Please try again.", $scope);
    }

    $scope.CartCheckOut = function () {

        if ($scope.TotalPrice < 1500) {
            objCommon.ShowMessage("Total order amount must be equal or more than 1500", "info");
            return;
        }

        $scope.loader = true;
        $("body").css({ "opacity": "0.5" });

        //$.ajax({
        //    type: "GET",
        //    //dataType: 'json',
        //    crossDomain: true,
        //    //contentType: 'application/json; charset=utf-8', // text for IE, xml for the rest ,
        //    url: '/Cart/ProcessCheckOut',
        //    //data: data,
        //    //async: isAsync,
        //    success: function (response) {

        //        if (!response.Status) {
        //            window.location.replace(objCommon.baseUrl + "Accounts/SignIn");
        //        } else {
        //            window.location.replace(objCommon.baseUrl + "Cart/CheckOut");
        //        }

        //        $("body").css({ "opacity": "1" });
        //        $scope.loader = false;
        //        $scope.$apply();
        //    },
        //    error: function (err) {
        //        $("body").css({ "opacity": "1" });
        //        $scope.loader = false;
        //        $scope.$apply();
        //    }
        //});



        objCommon.AjaxCall("/Cart/ProcessCheckOut", null, "GET", true, function (d) {
            if (!d.Status) {
                window.location.replace(objCommon.baseUrl + "Accounts/SignIn");
            } else {
                window.location.replace(objCommon.baseUrl + "Cart/CheckOut");
            }
            $("body").css({ "opacity": "1" });
            $scope.loader = false;
            $scope.$apply();
        }, null, "Error while processing check out, Please try again.", $scope);
    }

    $scope.GetCartList = function () {
        $scope.TotalPrice = 0;
        $scope.TotalCartItems = 0;
        if (localStorage.getItem("Cart") == null) {
            localStorage.setItem("Cart", JSON.stringify([]));
        }
        $scope.CartList = JSON.parse(localStorage.getItem('Cart'));
        $.each($scope.CartList, function (i) {
            $scope.TotalPrice += $scope.CartList[i].QUANTITY * $scope.CartList[i].PRICE;
            $scope.TotalCartItems += 1;
        });

    }

    $scope.LoadHeaderData = function () {
        // $scope.GetHeaderCategories();
        $scope.GetGroupAndCategories();
        $scope.GetCartList();
        //$scope.GetCompanyInfo();
        $scope.GetBranchId();
    }

    $scope.AddToCart = function (ProductId, Name, Packing, Price, Price2, Image, Quantity, ProductNameUrl, Barcode) {
        if (localStorage.getItem('Cart') == null) {
            localStorage.setItem('Cart', JSON.stringify([]));
        }
        var exist = false;
        var oldItems = JSON.parse(localStorage.getItem('Cart')) || [];
        $.each(oldItems, function (i) {
            if (oldItems[i].PRODUCT_ID === ProductId) {
                objCommon.ShowMessage("Product already exist in the cart", 'error');
                exist = true;
                return false
            }
        });
        if (exist == false) {
            var newItem = { PRODUCT_ID: ProductId, NAME: Name, PACKING: Packing, PRICE: Price, PRICE2: Price2, IMAGE_THUMBNAIL_PATH: Image, QUANTITY: Quantity, PRODUCT_NAME_URL: ProductNameUrl, BAR_CODE: Barcode };

            oldItems.push(newItem);
            localStorage.setItem('Cart', JSON.stringify(oldItems));
            $scope.GetCartList();
        }


    }

    $scope.DeleteProductFromCart = function (productId) {
        if (confirm("Are you sure you want to delete this item??")) {
            var oldItems = JSON.parse(localStorage.getItem('Cart'));
            $.each(oldItems, function (i) {
                if (oldItems[i].PRODUCT_ID === productId) {
                    oldItems.splice(i, 1);
                    localStorage.clear();
                    localStorage.setItem('Cart', JSON.stringify(oldItems));
                    return false
                }
            });
            $scope.GetCartList();
        }
    }

    $scope.IncreaseOrDecreaseQuantity = function (productId, quantity, status) {

        if (status == 0 && quantity > 1) {
            var oldItems = JSON.parse(localStorage.getItem('Cart'));
            $.each(oldItems, function (i) {
                if (oldItems[i].PRODUCT_ID === productId) {
                    oldItems[i].QUANTITY = oldItems[i].QUANTITY - 1;
                    localStorage.clear();
                    localStorage.setItem('Cart', JSON.stringify(oldItems));
                    return false
                }
            });
        }
        if (status == 1) {
            var oldItems = JSON.parse(localStorage.getItem('Cart'));
            $.each(oldItems, function (i) {
                if (oldItems[i].PRODUCT_ID === productId) {
                    oldItems[i].QUANTITY = oldItems[i].QUANTITY + 1;
                    localStorage.clear();
                    localStorage.setItem('Cart', JSON.stringify(oldItems));
                    return false
                }
            });
        }
        $scope.GetCartList();
    }

    $scope.GetProductByCategory = function (categoryId) {
        //$("body").css({ "opacity": "0.5" });
        //$scope.loader = true;
        //$scope.categoryId = categoryId;
        //isFilter = true;
        //var data = {
        //    CategoryId: $scope.categoryId,
        //    PageIndex: $scope.pageIndex,
        //    PageSize: $scope.pageSize
        //}
        //objCommon.AjaxCallAPS("Products/GetAllProductsByCategoryId", $.param(data), "GET", true, function (data) {
        //    isFilter = true;
        //    $scope.Products = data.response;
        //    $scope.loader = false;
        //    $("body").css({ "opacity": "1" });
        //    $scope.$apply();

        //    if (data.response.length == 0) {
        //        $('#btnloadmore').addClass('d-none');
        //        //$("#btnloadmore").prop('disabled', true);
        //        $('#txtloadmore').text('No More Products');
        //        //$('#txtloadmore').removeClass('d-none');
        //    }
        //    else {
        //        //$('#btnloadmore').removeClass('d-none');
        //        $('#txtloadmore').addClass('d-none');
        //        $("#btnloadmore").prop('disabled', false);
        //    }

        //    if (data.response[0].TotalRecords < $scope.pageSize) {
        //        $('#btnloadmore').addClass('d-none');
        //        //$("#btnloadmore").prop('disabled', true);
        //        $('#txtloadmore').text('No More Products');
        //    }

        //    //$(".anchortag").css("font-weight", "200");
        //    //$("#" + $scope.categoryId + "anchor").css("font-weight", "700");
        //    //setTimeout(function () {
        //    //    $("#maincategory").val($scope.groupId + "");
        //    //    $("#txtSearchBox").val($scope.searchText);
        //    //}, 300)

        //});

        //window.location.replace(objCommon.baseUrl + "Products/Index?category=" + categoryId + "&search=" + $scope.searchText + "&group=" + $scope.groupId);

        // $scope.GetAllProducts(categoryId, $scope.searchText);
        //$window.location.href = objCommon.baseUrl + 'Products/GetAllProductsByCategoryId?CategoryId=' + categoryId + '&PageIndex=1&PageSize=15';
        $window.location.href = objCommon.baseUrl + 'Products/Index?category=' + categoryId + '&search=&group=&isFromMenu=true';
    }

    $scope.GetProductBySearch = function () {
        if ($("#maincategory option:selected").text() !== "Select Category" && $("#maincategory option:selected").text() !== "" && $("#maincategory option:selected").text() !== "0") {
            $window.location.href = objCommon.baseUrl + 'Products/Index?search=' + $scope.TextSearch + '&group=' + $("#maincategory option:selected").val();
        } else {
            $window.location.href = objCommon.baseUrl + 'Products/Index?search=' + $scope.TextSearch;
        }
    }
});

function renderDropdown() {
    var x, i, j, l, ll, selElmnt, a, b, c;
    /*look for any elements with the class "flux-custom-select":*/
    x = document.getElementsByClassName("flux-custom-select");
    l = x.length;
    for (i = 0; i < l; i++) {
        selElmnt = x[i].getElementsByTagName("select")[0];
        ll = selElmnt.length;
        /*for each element, create a new DIV that will act as the selected item:*/
        a = document.createElement("DIV");
        a.setAttribute("class", "select-selected");
        a.innerHTML = selElmnt.options[selElmnt.selectedIndex].innerHTML;
        x[i].appendChild(a);
        /*for each element, create a new DIV that will contain the option list:*/
        b = document.createElement("DIV");
        b.setAttribute("class", "select-items select-hide");
        for (j = 1; j < ll; j++) {
            /*for each option in the original select element,
            create a new DIV that will act as an option item:*/
            c = document.createElement("DIV");
            c.innerHTML = selElmnt.options[j].innerHTML;
            c.addEventListener("click", function (e) {
                /*when an item is clicked, update the original select box,
                and the selected item:*/
                var y, i, k, s, h, sl, yl;
                s = this.parentNode.parentNode.getElementsByTagName("select")[0];
                sl = s.length;
                h = this.parentNode.previousSibling;
                for (i = 0; i < sl; i++) {
                    if (s.options[i].innerHTML == this.innerHTML) {
                        s.selectedIndex = i;
                        h.innerHTML = this.innerHTML;
                        y = this.parentNode.getElementsByClassName("same-as-selected");
                        yl = y.length;
                        for (k = 0; k < yl; k++) {
                            y[k].removeAttribute("class");
                        }
                        this.setAttribute("class", "same-as-selected");
                        break;
                    }
                }
                h.click();
            });
            b.appendChild(c);
        }
        x[i].appendChild(b);
        a.addEventListener("click", function (e) {
            /*when the select box is clicked, close any other select boxes,
            and open/close the current select box:*/
            e.stopPropagation();
            closeAllSelect(this);
            this.nextSibling.classList.toggle("select-hide");
            this.classList.toggle("select-arrow-active");
        });
    }
    function closeAllSelect(elmnt) {
        /*a function that will close all select boxes in the document,
        except the current select box:*/
        var x, y, i, xl, yl, arrNo = [];
        x = document.getElementsByClassName("select-items");
        y = document.getElementsByClassName("select-selected");
        xl = x.length;
        yl = y.length;
        for (i = 0; i < yl; i++) {
            if (elmnt == y[i]) {
                arrNo.push(i)
            } else {
                y[i].classList.remove("select-arrow-active");
            }
        }
        for (i = 0; i < xl; i++) {
            if (arrNo.indexOf(i)) {
                x[i].classList.add("select-hide");
            }
        }
    }
    /*if the user clicks anywhere outside the select box,
    then close all select boxes:*/
    document.addEventListener("click", closeAllSelect);


}