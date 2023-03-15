var objCommon = new Common();
suchiapp.controller("UserController", function ($scope) {
    $scope.loader = true;
    $scope.GetAllOrders = function (orderStatusId) {
        $("body").css({ "opacity": "0.5" });
        $scope.loader = true;
        $scope.pageIndex = 1;
        $scope.pageSize = 20;
        $scope.sortColumn = "";
        $scope.sortOrder = "";
        $scope.searchText = "";
        $scope.orderStatusId = orderStatusId;
        var data = {
            PageIndex: $scope.pageIndex,
            PageSize: $scope.pageSize,
            SortColumn: $scope.sortColumn,
            SortOrder: $scope.sortOrder,
            SearchText: $scope.searchText,
            OrderStatusId: $scope.orderStatusId
        };
        objCommon.AjaxCall("User/GetAllOrders", $.param(data), "GET", true, function (data) {
            if (data == "AccessDenied") {
                objCommon.ShowMessage("Access Denied Your Session is Time Out.", "error");
            }
            else {
                $scope.Orders = data;
            }
            $scope.$apply();
        }, null, "Error while getting all orders, Please try again.", $scope);
        $scope.AccountDetail();
    }

    $scope.AccountDetail = function () {
        objCommon.AjaxCall("User/GetUserDetail", $.param({}), "GET", true, function (data) {
            if (data == "Invalid") {
                objCommon.ShowMessage("Access Denied Your Session is Time Out.", "error");
            }
            else {
                $scope.username = data.USERNAME;
                $scope.email = data.EMAIL;
                $scope.mobile = data.MOBILE_NO;
                $scope.address = data.ADDRESS;
            }
            $("body").css({ "opacity": "1" });
            $scope.loader = false;
            $scope.$apply();
        }, null, "Error while getting user details, Please try again.", $scope);
    }

    $scope.UpdateUserAccount = function () {
        if ($scope.username == "" || $scope.username == null) {
            objCommon.ShowMessage("Username is required", "error");
        }
        else if ($scope.email == "" || $scope.email == null) {
            objCommon.ShowMessage("Email is required", "error");
        }
        else if ($scope.mobile == "" || $scope.mobile == null) {
            objCommon.ShowMessage("Mobile number is required", "error");
        }
        else if ($scope.address == "" || $scope.address == null) {
            objCommon.ShowMessage("Address is required", "error");
        }
        else {
            $("body").css({ "opacity": "0.5" });
            $scope.loader = true;
            var data = {
                username: $scope.username,
                email: $scope.email,
                mobile: $scope.mobile,
                address: $scope.address,
            };
            objCommon.AjaxCall("User/UpdateUserDetail", $.param(data), "GET", true, function (response) {
                $scope.loader = false;
                $("body").css({ "opacity": "1" });
                if (response.Status == true) {
                    objCommon.ShowMessage(response.RetMessage, "success");
                }
                else {
                    objCommon.ShowMessage(response.RetMessage, "error");
                }
                $scope.$apply();
            }, null, "Error while updating user details, Please try again.", $scope);
        }  
    };

    $scope.AddressesList = function () {
        $("body").css({ "opacity": "0.5" });
        $scope.loader = true;
        var data = {};
        objCommon.AjaxCall("User/GetAddressList", $.param(data), "GET", true, function (data) {
            if (data == "Invalid") {
                objCommon.ShowMessage("Access Denied Your Session is Time Out.", "error");
            }
            else {
                $scope.AddressList = data;
            }
            $("body").css({ "opacity": "1" });
            $scope.loader = false;
            $scope.$apply();
        }, null, "Error while getting address list, Please try again.", $scope)

    }

    $scope.ChangePassword = function ($event) {
        if ($scope.OldPassword == "" || $scope.OldPassword == null) {
            objCommon.ShowMessage("Please enter old password", "error");
        }
        else if ($scope.NewPassword == "" || $scope.NewPassword == null) {
            objCommon.ShowMessage("Please enter new password", "error");
        }
        else if ($scope.ConfirmPassword == "" || $scope.ConfirmPassword == null) {
            objCommon.ShowMessage("Please enter confirm password", "error");
        }
        else if ($scope.NewPassword != $scope.ConfirmPassword) {
            objCommon.ShowMessage("New password and confirm password not matched", "error");
        }
        else {
            $("body").css({ "opacity": "0.5" });
            $scope.loader = true;
            var data = {
                OldPassword: $scope.OldPassword,
                NewPassword: $scope.NewPassword
            };
            objCommon.AjaxCall("User/ChangePassword", $.param(data), "GET", true, function (response) {
                $("body").css({ "opacity": "1" });
                $scope.loader = false;
                if (response.Status == true) {
                    objCommon.ShowMessage(response.RetMessage, "success");
                    window.location.replace(objCommon.baseUrl + "Accounts/SignIn");
                }
                else {
                    objCommon.ShowMessage(response.RetMessage, "error");
                }
                $scope.$apply();
            }, null, "Error while changing password, Please try again.", $scope);
        }
    };

    $scope.SignOut = function () {
        objCommon.AjaxCall("Accounts/SignOut", $.param({}), "GET", true, function (d) {
            if (d == "success") {
                window.location.replace(objCommon.baseUrl + "Home/Index");
            }
            else {
                objCommon.ShowMessage(d, "error");
            }
        }, null, "Error while sign out user, Please try again.", $scope);
    }

});
