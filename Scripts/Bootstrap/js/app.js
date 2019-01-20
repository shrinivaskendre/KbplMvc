//'use strict';


//var app = angular.module('MyApp', []);

//app.directive('datetimepicker', function () {
//    return {
//        require: '?ngModel',
//        restrict: 'A',
//        link: function (scope, element, attrs, ngModel) {

//            if (!ngModel) return; // do nothing if no ng-model

//            ngModel.$render = function () {
//                element.find('input').val(ngModel.$viewValue || '');
//            }

//            element.datetimepicker({
//                language: 'it'
//            });

//            element.on('dp.change', function () {
//                scope.$apply(read);
//            });

//            read();

//            function read() {
//                var value = element.find('input').val();
//                ngModel.$setViewValue(value);
//            }
//        }
//    }
//});

//app.controller('MyController', function ($scope) {
//    $scope.Issuedate = moment();
//});







//var app = angular.module('MyApp', [])
//app.controller('MyController', function ($scope, $http, $window) {

//    //  $scope.Issuedate = new Date();

//    $scope.Issuedate = moment();

//    $scope.factorylist = "";
//    var get = $http({
//        method: "GET",
//        url: "/MaterialIssueSlip/GetFactory",
//        dataType: 'json',
//        headers: { "Content-Type": "application/json" }
//    });
//    get.success(function (result) {
//        $scope.factorylist = result;
//        console.log(result);

//    })
//    .error(function (result) {
//        $window.alert("Error occurs..");
//    });

//    function BindDT() {
//        var get = $http({
//            method: "GET",
//            url: "/MaterialIssueSlip/BindDataTable",
//            dataType: 'json',
//            headers: { "Content-Type": "application/json" }
//        });
//        get.success(function (result) {
//            $scope.itemList = result;
//        })
//        .error(function (result) {
//            $window.alert("Error occurs data table..");
//        });
//    }

//    $scope.CostCenter = function () {
//        $http.get('/MaterialIssueSlip/GetCostCenter?fid=' + $scope.FId).then(function (d) {
//            $scope.costlist = d.data;
//            BindDT();
//        });
//    };

//    function ValidationCheck() {

//        debugger;

//        if ($scope.Issueno == undefined) {
//            $window.alert("Please enter Issue No");
//            return false;
//        }

//        if ($scope.FId == undefined) {
//            $window.alert("Please select factory");
//            return false;
//        }
//        if ($scope.CId == undefined) {
//            $window.alert("Please select cost center");
//            return false;
//        }
//        if ($scope.Personname == undefined) {
//            $window.alert("Please enter Person name");
//            return false;
//        }
//        if ($scope.Personname != undefined) {
//            var regexp = /^[a-zA-Z]+$/; if ($scope.Personname.match(regexp))
//            { }
//            else
//            {
//                $window.alert("Please enter characters only.");
//                return false;
//            }
//        }
//        if ($scope.remark == undefined) {
//            $window.alert("Please enter remark No");
//            return false;
//        }
//        if ($scope.remark != undefined) {
//            var regexp = /^[a-zA-Z]+$/; if ($scope.remark.match(regexp))
//            { }
//            else
//            {
//                $window.alert("Please enter characters only.");
//                return false;
//            }
//        }
//        //if ($scope.Issueno == "") {
//        //    $window.alert("Please enter Issue No");
//        //}
//        return true;
//    }


//    //upload Item Data
//    $scope.SaveData = function () {

//        if (ValidationCheck() == false) {

//        }
//        else {
//            $scope.fields = [];
//            for (i in $scope.itemList) {
//                var obj = $scope.itemList[i];
//                $scope.fields.push({
//                    "code": obj.code, "itemCode": obj.itemCode, "itemDesc": obj.itemDesc, "unit": obj.unit,
//                    "issuePurpose": obj.issuePurpose, "issueQty": obj.issueQty
//                })
//            }
//            console.log($scope.fields);

//            var itemJson = {
//                IssueNo: $scope.Issueno, IssueDate: $scope.Issuedate, Factory: $scope.FId, costcenter: $scope.CId,
//                PersonName: $scope.Personname, Remark: $scope.remark, listitm: $scope.fields
//            }

//            var post = $http({
//                method: "POST",
//                url: "/MaterialIssueSlip/AddItem",
//                dataType: 'json',
//                data: JSON.stringify(itemJson),
//                headers: { "Content-Type": "application/json" }
//            });

//            post.success(function (data, status) {
//                $window.alert("Upload Successs");
//                // $scope.StudentList = result;
//            });

//            post.error(function (data, status) {
//                $window.alert(data.Message);
//            });
//        }
//    }
//});

//// here we define our unique filter
//app.filter('unique', function () {
//    // we will return a function which will take in a collection
//    // and a keyname
//    return function (collection, keyname) {
//        // we define our output and keys array;
//        var output = [],
//            keys = [];

//        // we utilize angular's foreach function
//        // this takes in our original collection and an iterator function
//        angular.forEach(collection, function (item) {
//            // we check to see whether our object exists
//            var key = item[keyname];
//            // if it's not already part of our keys array
//            if (keys.indexOf(key) === -1) {
//                // add it to our keys array
//                keys.push(key);
//                // push this item to our final output array
//                output.push(item);
//            }
//        });
//        // return our array which should be devoid of
//        // any duplicates
//        return output;
//    };
//});

//app.directive('onlyLettersInput', onlyLettersInput);

//function onlyLettersInput() {
//    return {
//        require: 'ngModel',
//        link: function (scope, element, attr, MyController) {
//            function fromUser(text) {
//                var transformedInput = text.replace(/[^a-zA-Z]/g, '');
//                //console.log(transformedInput);
//                if (transformedInput !== text) {
//                    MyController.$setViewValue(transformedInput);
//                    MyController.$render();
//                }
//                return transformedInput;
//            }
//            MyController.$parsers.push(fromUser);
//        }
//    };
//};

//app.directive('numbersOnly', function () {
//    return {
//        require: 'ngModel',
//        link: function (scope, element, attr, MyController) {
//            function fromUser(text) {
//                if (text) {
//                    var transformedInput = text.replace(/[^0-9]/g, '');

//                    if (transformedInput !== text) {
//                        MyController.$setViewValue(transformedInput);
//                        MyController.$render();
//                    }
//                    return transformedInput;
//                }
//                return undefined;
//            }
//            MyController.$parsers.push(fromUser);
//        }
//    };
//});

//app.directive('datetimepicker', function () {
//    return {
//        require: '?ngModel',
//        restrict: 'A',
//        link: function (scope, element, attrs, ngModel) {

//            if (!ngModel) return; // do nothing if no ng-model

//            ngModel.$render = function () {
//                element.find('input').val(ngModel.$viewValue || '');
//            }

//            element.datetimepicker({
//                language: 'it'
//            });

//            element.on('dp.change', function () {
//                scope.$apply(read);
//            });

//            read();

//            function read() {
//                var value = element.find('input').val();
//                ngModel.$setViewValue(value);
//            }
//        }
//    }
//});