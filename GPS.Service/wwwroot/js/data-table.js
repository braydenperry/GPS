var table = $('.mydatatable').DataTable({
    lengthMenu: [[5, 10, 25, 50, 100, -1], [5, 10, 25, 50, 100, "All"]],
    responsive: true
    //searching: false
});

$('.mydatatable tfoot th').each(function () {
    var title = $(this).text();
    $(this).html('<input type="text" placeholder="Search ' + title + '" />');
});

table.columns().every(function () {
    var that = this;
    $('input', this.footer()).on('keyup change', function () {
        if (that.search() !== this.value) {
            that.search(this.value).draw();
        }
    })
})