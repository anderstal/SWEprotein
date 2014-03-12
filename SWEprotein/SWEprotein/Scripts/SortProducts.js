$(document).ready(function() {

    $("#ddlSortProducts").change(function() {
        window.location.href = window.location.href.replace( /[\?#].*|$/, "?sort=" + $("#ddlSortProducts").val());
    });
});