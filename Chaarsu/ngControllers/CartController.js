
var objCommon = new Common();

suchiapp.controller("CartController", function ($scope, $window) {
    $scope.loader = true;
    $scope.Name = '';
    $scope.Mobile = '';
    $scope.Address = '';
    var address = localStorage.getItem("address");
    $scope.Address = address;

    objCommon.AjaxCallAPS("Cart/GetPaymentModes", $.param({}), "GET", true, function (data) {
        console.log(data.response);
        if (data.Status == true) {
            $scope.PaymentModes = data.response;
            $scope.loader = false;
            $scope.$apply();
        }
        else {
            objCommon.ShowMessage(data.RetMessage, "error");
        }
    })

    $scope.Valid = function () {
        if ($scope.Name == '' || $scope.Name == null) {
            objCommon.ShowMessage("Name is required", "error");
            return false;
        }
        else if ($scope.Mobile == '' || $scope.Mobile == null) {
            objCommon.ShowMessage("Mobile number is required", "error");
            return false;
        }
        else if ($scope.Address == '' || $scope.Address == null) {
            objCommon.ShowMessage("Address is required", "error");
            return false;
        }
        else if ($scope.TotalPrice == 0 || $scope.TotalPrice == null) {
            objCommon.ShowMessage("Your cart is empty. No any product in the cart. ", "error");
            return false;
        }
        else {
            return true;
        }
    }
   
  

    $scope.PlaceOrder = function () {
        if ($scope.Valid() == true) {

            var long = localStorage.getItem("lang");
            var lat = localStorage.getItem("lat");

            var radioValue = $("input[name='payment']:checked").val();
            if (radioValue) {
                $("body").css({ "opacity": "0.5" });
                $scope.loader = true;
                var data = {
                    CUSTOMER_ID: 0,
                    NAME: $scope.Name,
                    MOBILE: $scope.Mobile,
                    ADDRESS: $scope.Address,
                    DELIVERY_DESCRIPTION: $scope.DELIVERY_DESCRIPTION,
                    PAYMENT_MODE_ID: radioValue,
                    LATITUDE: lat,
                    LONGITUDE: long
                }
              

                objCommon.AjaxCall("Cart/InsertOrder", JSON.stringify(data), "POST", true, function (order) {
                    var arrayObjects = [];
                    if (order.Status == true) {
                        var products = JSON.parse(localStorage.getItem('Cart'));
                        console.log(products);
                        $.each(products, function (i) {
                            var obj = { OREDER_PRODUCT_ID: 0, ORDER_ID: order.OrderId, PRODUCT_ID: products[i].PRODUCT_ID, QUANTITY: products[i].QUANTITY, IS_ACTIVE: 1, BAR_CODE: products[i].BAR_CODE }
                            arrayObjects.push(obj);
                        });
                        objCommon.AjaxCall("Cart/InsertOrderProducts", JSON.stringify(arrayObjects), "POST", true, function (d) {
                            $("body").css({ "opacity": "1" });
                            $scope.loader = false;
                            if (d.Status == true) {
                                localStorage.clear();
                                window.location.replace(objCommon.baseUrl + "Cart/OrderSuccess?OrderId=" + order.OrderId);
                                $scope.$apply();
                                localStorage.setItem("Cart", JSON.stringify([]));
                            }
                        });
                       
                    }
                    else {
                        objCommon.ShowMessage(order.RetMessage, "error");
                    }
                });
            }
            else {
                objCommon.ShowMessage("Please select payment mode", "error");
            }
        }
    }

});