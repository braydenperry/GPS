var dataTable;

$(document).ready(function () {
    loadList();
});

function loadList() {
    dataTable = $('#DT_load').DataTable({
        "ajax": {
            "url": "/api/queryoutages",
            "type": "GET",
            "datatype": "json",
            "dataSrc": ""
        },
        "columns": [
            { data: "tagName" },
            { data: "spaceVehicleId" },
            { data: "spaceVehicleNumber" },
            { data: "startTime" },
            { data: "endTime" },
            { data: "type" },
            { data: "reference" }
        ],
        "language": {
            "emptyTable": "no data found."
        },
        "width": "100%"
    });
}