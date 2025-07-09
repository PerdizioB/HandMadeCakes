// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
$('#isSelected').click(function () {
    $("#confirmation").toggle(this.checked);
});

$('#isSelectedM').click(function () {
    $("#confirmationM").toggle(this.checked);
});

$('#isSelectedL').click(function () {
    $("#confirmationL").toggle(this.checked);
});
