// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
var results = $("#Results");
function begin() {
    $('#mdlSpinner').modal('show');
}
function success(data) {

    $('#mdlSpinner').modal('hide');
}
function failure() {

    $("#mdlSpinner").modal('hide');
    console.log("failure!");
}
function complete() {
    $("#mdlSpinner").modal('hide');
    console.log("complete!");
}