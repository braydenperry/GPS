var localDataTable;
const Tag_Name = 0;
const ID = 1;
const SVN = 2; 
const Start_Time = 3;
const End_Time = 4;
const Type = 5;
const Reference = 6;

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
        "width": "100%",
        "order": [[6, 'asc']],

        initComplete: function () {
            this.api().columns([Tag_Name, Type]).every(function () {
                var column = this;
                var select = $('<select><option value=""></option></select>')
                    .appendTo($(column.footer()).empty())
                    .on('change', function () {
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
            });

            this.api().columns([ID, SVN, Start_Time, Type]).every(function () {
                localDataTable.columns().every(function () {
                var column = this;
                    $('input', this.footer()).on('keyup change', function () {
                        var val = $.fn.dataTable.util.escapeRegex(
                            $(this).val()
                        );
                        if (column.search(val ? '^' + val : '', true, false)) {
                            column.draw();
                        }
                    })
                })
            });
        }
    });
}

$('.mydatatable').ready(function () {

    $.fn.dataTable.moment('M/D/YYYY HH:mm:ss');

    $('.mydatatable tfoot th').each(function () {
        $(this).html('<input type="text" placeholder="Search" />');
    });
});