var localDataTable;
const Tag_Name = 0;
const ID = 1;
const SVN = 2; 
const Start_Time = 3;
const End_Time = 4;
const Type = 5;
const Reference = 6;
let StartDateRange = "";
let EndDateRange = "";

$(document).ready(function () {
    loadList();
});

function loadList(datefilter) {
    localDataTable = $('#DT_load').DataTable({
        lengthMenu: [[5, 10, 25, 50, 100, -1], [5, 10, 25, 50, 100, "All"]],
        "ajax": {
            "url": (datefilter == null) ? "/v1/outages" : "/v1/outages" + datefilter,
            "type": "GET",
            "datatype": "json",
            "dataSrc": ""
        },
        "columns": [
            { data: "tagName" },
            { data: "satelliteVehicleId" },
            { data: "satelliteVehicleNumber" },
            { data: "startTime", render: function (data) { return moment(data).format('M/D/YYYY HH:mm:ss'); } },
            {
                data: "endTime", render: function (data) {
                    let momentEndDate = moment(data).format('M/D/YYYY HH:mm:ss');
                    let endDate = (momentEndDate === 'Invalid date') ? '' : momentEndDate;
                    
                    return endDate;
                }
            },
            { data: "type" },
            { data: "reference" }
        ],
        "language": {
            "emptyTable": "no data found."
        },
        "width": "100%",
        bAutoWidth: false,
        responsive: true,
        "order": [[6, 'asc']],

        //Function to set up all of the search/dropdown/daterangepicker boxes at the bottom of each column
        initComplete: function () {
            //Drop down boxes for TagName and Type
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
                    if (d != null) {
                        select.append('<option value="' + d + '">' + d + '</option>')
                    }
                });
            });

            //Search box for ID, SVN, and Reference
            this.api().columns([ID, SVN, Reference]).every(function () {
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

            //DateRangePicker for start time
            this.api().columns([Start_Time]).every(function () {
                var column = this;
                $('<input type="text" class="dateFilter" id="startDateFilter">')
                    .appendTo($(column.footer()).empty())
                    .on('change', function () {
                    });
            });

            //DateRangePicker for end time
            this.api().columns([End_Time]).every(function () {
                var column = this;
                $('<input type="text" class="dateFilter" id="endDateFilter">')
                    .appendTo($(column.footer()).empty())
                    .on('change', function () {
                    });
            });

            //Make the filter buttons into a daterangepicker.
            $("#startDateFilter").daterangepicker({
                autoUpdateInput: false,
                linkedCalendars: false,
                showDropdowns: true,
                startDate: "01/01/1993",
                endDate: "12/31/2020",
                locale: {
                    format: 'MM/DD/YYYY',
                    cancelLabel: 'Clear'
                }
            });

            //Same as above but for the End Time input
            $("#endDateFilter").daterangepicker({
                autoUpdateInput: false,
                linkedCalendars: false,
                showDropdowns: true,
                startDate: "01/01/1993",
                endDate: "12/31/2020",
                locale: {
                    format: 'MM/DD/YYYY',
                    cancelLabel: 'Clear'
                }
            });

            //If there is a global variable set, populte the value with that global (using a global because of the destoy function)
            $('#startDateFilter').val(StartDateRange);
            $('#endDateFilter').val(EndDateRange);

            //What happens when they click apply
            $('input[class="dateFilter"]').on('apply.daterangepicker', function (ev, picker) {
                $(this).val(picker.startDate.format('MM/DD/YYYY') + ' - ' + picker.endDate.format('MM/DD/YYYY'));
                filterByDate();
            });
            //What happens when they click clear
            $('input[class="dateFilter"]').on('cancel.daterangepicker', function (ev, picker) {
                //Clear the text box
                $(this).val("");
                //Clear the globals
                if (this.id == "startDateFilter") {
                    StartDateRange = "";
                } else if (this.id == "endDateFilter") {
                    EndDateRange = "";
                }
                //Call our api with the newly updated info
                filterByDate();
                
            });
        }
    });
}

function filterByDate() {
    //If no date is chosen, the min will be == "", and the max will be undefiined.
    //Before parsing a date will appear as: 01/01/2019 - 12/12/2020
    //After parsing a date will appear as: 01-01-201912-12-2020
    //If a date is not selected, the end date range will be "".
    var startDateRange = $('#startDateFilter').val();
    StartDateRange = startDateRange;
    var startDateRange = startDateRange.replace(/-/g, "_");
    var startDateRange = startDateRange.replace(/ /g, "");
    var startDateRange = startDateRange.replace(/\//g, "-");

    var endDateRange = $('#endDateFilter').val();
    EndDateRange = endDateRange;
    var endDateRange = endDateRange.replace(/-/g, "_");
    var endDateRange = endDateRange.replace(/ /g, "");
    var endDateRange = endDateRange.replace(/\//g, "-");


    // Destroy the old data table and reload it with the new filter
    localDataTable.destroy();
    if (startDateRange == "") {
        startDateRange = " "
    }

    if (startDateRange != null && endDateRange == null) {
        loadList("?startDateMinMax=" + startDateRange);
    }
    else if (startDateRange == null && endDateRange != null) {
        loadList("?endDateMinMax=" + endDateRange);
    }
    else if (startDateRange != null && endDateRange != null) {
        loadList("?startDateMinMax=" + startDateRange + "&endDateMinMax=" + endDateRange);
    }
}

$('.mydatatable').ready(function () {

    $.fn.dataTable.moment('M/D/YYYY HH:mm:ss');

    $('.mydatatable tfoot th').each(function () {
        $(this).html('<input type="text" placeholder="Search" />');
    });
});
