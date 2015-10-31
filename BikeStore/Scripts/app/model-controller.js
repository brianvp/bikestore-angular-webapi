﻿var app = angular.module('BikeStoreApp', ['ngRoute']);
console.log("init Application");
app.config(['$routeProvider','$locationProvider',
function ($routeProvider, $locationProvider) {

    $routeProvider.when('/', {
        templateUrl: 'partials/model-list.html',
        controller: 'ModelListCtrl'
    }).
    when('/:modelId', {
        templateUrl: 'partials/model-detail.html',
        controller: 'ModelDetailCtrl'
    }).
    otherwise({
      redirectTo: '/'
    })
}]);



 app.controller('ModelListCtrl',  function ($scope, $http) {

            $http.get("/api/status").success(
                function (data) {
                    $scope.statusList = data;
                });

            $http.get("/api/manufacturers").success(
               function (data) {
                   $scope.manufacturers = data;
               });

            $http.get("/api/categories").success(
                function (data) {
                    $scope.categories = data;
                });

            $http.get("/api/models").success(
                function (data) {
                    $scope.models = data;
                });

 });

 app.controller('ModelDetailCtrl', ['$scope', '$http', '$routeParams', '$location', function ($scope, $http, $routeParams,$location) {


     $http.get("/api/status").success(
         function (data) {
             $scope.statusList = data;
         });

     $http.get("/api/manufacturers").success(
        function (data) {
            $scope.manufacturers = data;
        });

     $http.get("/api/categories").success(
         function (data) {
             $scope.categories = data;
         });

     if ($routeParams.modelId.toLowerCase() === "new") {
         $scope.inEditMode = true;

         //any way around doing this?  Querying /api/models/-1 returns null
         $scope.model = {
             modelId:null,
             manufacturerName: '',
             manufactuerID: null,
             categoryName: '',
             categoryId: null,
             manufacturerCode: '',
             modelName: '',
             statusName: '',
             statusId: null,
             listPrice: 0.00,
             description:''
         }
     }
     else {
         $scope.inEditMode = false;
   

         $http.get("/api/models/" + $routeParams.modelId).success(
           function (data) {
               $scope.model = data;
           });

     }
     
     $scope.saveModel = function () {
       
         if ($scope.model.modelId > 0)
         {
             $http.put("/api/models/" + $scope.model.modelId, $scope.model).success(function (data, status) {
                 $scope.inEditMode = false;
             });
             
         }
         else
         {
             $http.post("/api/models/", $scope.model).success(function (data, status) {
                 $scope.inEditMode = false;
                 $location.path('/');
             });
         }
         
     }

     $scope.enterEditMode = function () {

         if ($scope.inEditMode === true)
             $scope.inEditMode = false;
         else {
             $scope.inEditMode = true;
         }
 
     }

 }]);
