var app = angular.module('MyApp', []);

app.directive('numbersOnly', function () {
    return {
        require: 'ngModel',
        link: function (scope, element, attr, MyController) {
            function fromUser(text) {
                if (text) {
                    var transformedInput = text.replace(/[^0-9]/g, '');

                    if (transformedInput !== text) {
                        MyController.$setViewValue(transformedInput);
                        MyController.$render();
                    }
                    return transformedInput;
                }
                return undefined;
            }
            MyController.$parsers.push(fromUser);
        }
    };
});