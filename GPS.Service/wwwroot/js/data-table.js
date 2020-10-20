var table = $('.mydatatable').DataTable({
    lengthMenu: [[5, 10, 25, 50, 100, -1], [5, 10, 25, 50, 100, "All"]],
    //searching: false
});

//searching box
$('.mydatatable tfoot th').each(function () {
    var title = $(this).text();
    $(this).html('<input type="text" placeholder="Search" />');
});

//search functionality
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