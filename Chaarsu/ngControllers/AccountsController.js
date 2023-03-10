
var objCommon = new Common();

suchiapp.controller("AccountsController", function ($scope, $window) {
    $scope.password = '';
    $scope.emailOrmobile = '';
    $scope.loader = false;
    $scope.SingIn = function () {
        if ($scope.emailOrmobile == '' || $scope.emailOrmobile == null) {
            objCommon.ShowMessage("Please Enter Email Or Mobile No", "error");
        }
        else if ($scope.password == '' || $scope.password == null) {
            objCommon.ShowMessage("Please Enter Password", "error");
        }
        else {
            $("body").css({ "opacity": "0.5" });
            var data = {
                EmailOrmobile: $scope.emailOrmobile,
                Password: $scope.password,
            };
            objCommon.AjaxCall("Accounts/signInUser", $.param(data), "get", true, function (response) {
                $("body").css({ "opacity": "1" });
                if (response.Status == true) {
                    objCommon.ShowMessage(response.RetMessage, "success");
                    window.location.replace(objCommon.baseUrl + "User/Profile");
                }
                else {
                    objCommon.ShowMessage(response.RetMessage, "error");
                }
            });
        }       
    };

    $scope.Gender = 'Male';

    $scope.SingUp = function () {

        if ($scope.Username == '' || $scope.Username == null)
        {
            objCommon.ShowMessage("Name is required", "error");
        }
        else if ($scope.Mobile == '' || $scope.Mobile == null)
        {
            objCommon.ShowMessage("Mobile Number is required", "error");
        }
        else if ($scope.Address == '' || $scope.Address == null)
        {
            objCommon.ShowMessage("Address is required", "error");
        }
        else if ($scope.Password == '' || $scope.Password == null)
        {
            objCommon.ShowMessage("Password is required", "error");
        }
        else if ($scope.ConfirmPassword == '' || $scope.ConfirmPassword == null) {
            objCommon.ShowMessage("Confirm Password is required", "error");
        }
        else
        {
            if ($scope.Password != $scope.ConfirmPassword)
            {
                objCommon.ShowMessage("Password and Confirm Password Does Not Match", "error");
            }
            else
            {
                $("body").css({ "opacity": "0.5" });
                var data = {
                    USERNAME: $scope.Username,
                    MOBILE_NO: $scope.Mobile,
                    EMAIL: $scope.Email,
                    ADDRESS: $scope.Address,
                    PASSWORD: $scope.Password,
                    IS_GUEST: false
                };
                objCommon.AjaxCall("Accounts/signUpUser", JSON.stringify(data), "POST", true, function (response) {
                    $("body").css({ "opacity": "1" });
                    if (response.Status == true) {
                        objCommon.ShowMessage(response.RetMessage, "success");
                        window.location.replace(objCommon.baseUrl + "Accounts/SignIn");
                    }
                    else {
                        objCommon.ShowMessage(response.RetMessage, "error");
                    }
                });
            }
        }
        $scope.$apply();
    };

    $scope.ForgotPassword = function () {
        if ($scope.email == null || $scope.email == undefined || $scope.email == "") {
            objCommon.ShowMessage("Please Enter Your Email Address", "error");
        }
        else
        {
            $("body").css({ "opacity": "0.5" });
            var data = {
            email: $scope.email
            };
            objCommon.AjaxCall("Accounts/PasswordForgot", $.param(data), "GET", true, function (response) {
                $("body").css({ "opacity": "1" });
               if (response.Status == true) {
                  objCommon.ShowMessage(response.RetMessage, "success");
               }
               else {
                  objCommon.ShowMessage(response.RetMessage, "error");
               }
            });
        }
        
    };

    $scope.GuestUser = function () {
        if ($scope.GuestName == "" || $scope.Password != null) {
            objCommon.ShowMessage("Name is required", "error");
        }
        else {
            $("body").css({ "opacity": "0.5" });
            var data = {
                USERNAME: $scope.GuestName,
                MOBILE_NO: "",
                EMAIL: "",
                ADDRESS: "",
                PASSWORD: "",
                IS_GUEST: true
            };
            objCommon.AjaxCall("Accounts/signUpUser", JSON.stringify(data), "POST", true, function (response) {
                $("body").css({ "opacity": "1" });
                if (response.Status == true) {
                    $scope.GuestName = '';
                    objCommon.ShowMessage(response.RetMessage, "success");
                    CloseSignUpForm();
                    window.location.replace(objCommon.baseUrl + "Cart/CheckOut");
                }
                else {
                    objCommon.ShowMessage(response.RetMessage, "error");
                }
            });
        }

        $scope.$apply();
    };
});
