var localDataTable;

$(document).ready(function () {
    loadList();
});

function loadList() {
    localDataTable = $('#DT_load').DataTable({
        "ajax": {
            "url": "/api/queryoutages",
            "type": "GET",
            "datatype": "json",
            "dataSrc": ""
        },
        "columns": [
            { data: "tagName" },
            { data: "satelliteVehicleId" },
            { data: "satelliteVehicleNumber" },
            //copied directly from Brayden's code, but it is not sorting correctly
            { data: "startTime", render: function (data) { return moment(data).format('M/D/YYYY HH:mm:ss'); } },
            { data: "endTime", render: function (data) { return moment(data).format('M/D/YYYY HH:mm:ss'); } },
            { data: "type" },
            { data: "reference" }
        ],
        "language": {
            "emptyTable": "no data found."
        },
        "width": "100%"
    });
}


$('.mydatatable').ready(function () {

    $.fn.dataTable.moment('M/D/YYYY HH:mm:ss');

    $('.mydatatable tfoot th').each(function () {
        $(this).html('<input type="text" placeholder="Search" />');
    });

    localDataTable.columns().indexes().flatten().each(function (i) {
        var column = localDataTable.column(i);
        // Dropdown columns ('Tag Name' and 'Type')
        if (i === 0 | i === 5) {
            var select = $('<select><option value=""></option></select>')
                .appendTo($(column.footer()).empty())
                .on('change', function () {
                    // Escape the expression so we can perform a regex match
                    var val = $.fn.dataTable.util.escapeRegex(
                        $(this).val()
                    );

                    column
                        .search(val ? '^' + val + '$' : '', true, false)
                        .draw();

                });
            column.data().unique().sort().each(function (d, j) {
                select.append('<option value="' + d + '">' + d + '</option>')
            });
        }
        // Search columns
        else {
            localDataTable.columns().every(function () {
                var that = this;
                $('input', this.footer()).on('keyup change', function () {
                    //create a special variable using .escapeRegex function
                    //see https://datatables.net/reference/api/$.fn.dataTable.util.escapeRegex()
                    var val = $.fn.dataTable.util.escapeRegex(
                        $(this).val()
                    );
                    //"val ? '^' + val : ''" expression functions as the string argument in 'search()' function
                    //'^' in a regular expression means the tested string must START with the respective string
                    if (that.search(val ? '^' + val : '', true, false)) {
                        that.draw();
                    }
                })
            })
        }
    });
});