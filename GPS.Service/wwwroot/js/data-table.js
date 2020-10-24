$('.mydatatable').ready(function () {
    //implements 'ultimate date/time sorting' plugin
    $.fn.dataTable.moment('M/D/YYYY HH:mm:ss');

    $('.mydatatable').DataTable({
    lengthMenu: [[5, 10, 25, 50, 100, -1], [5, 10, 25, 50, 100, "All"]],
    //searching: false
    });

});

//searching box
$('.mydatatable tfoot th').each(function () {
    $(this).html('<input type="text" placeholder="Search" />');
});

table.columns().indexes().flatten().each(function (i) {
    var column = table.column(i);
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
        table.columns().every(function () {
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

