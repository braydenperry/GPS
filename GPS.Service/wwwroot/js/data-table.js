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
		lengthMenu: [[5, 10, 25, 50, 100, -1], [5, 10, 25, 50, 100, "All"]],
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
						//here is where we will pass the info over to the controller to get the new fields
						var date_range = $('#startDateFilter').val();
						var dates = date_range.split(" - ");
						var min = dates[0];
						var max = dates[1];
					});
			});

			//DateRangePicker for end time
			this.api().columns([End_Time]).every(function () {
				var column = this;
				$('<input type="text" class="dateFilter" id="endDateFilter">')
					.appendTo($(column.footer()).empty())
					.on('change', function () {
						//here is where we will pass the info over to the controller to get the new fields
						var date_range = $('#endDateFilter').val();
						var dates = date_range.split(" - ");
						var min = dates[0];
						var max = dates[1];
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
			//What happens when they click apply
			$('input[class="dateFilter"]').on('apply.daterangepicker', function (ev, picker) {
				$(this).val(picker.startDate.format('MM/DD/YYYY') + ' - ' + picker.endDate.format('MM/DD/YYYY'));
			});
			//What happens when they click clear
			$('input[class="dateFilter"]').on('cancel.daterangepicker', function (ev, picker) {
				$(this).val('');
			});
		}
	});
}

function filterByDate() {
	let test = $('#startTimeFilterButton').data.Start_Time;
}

$('.mydatatable').ready(function () {

	$.fn.dataTable.moment('M/D/YYYY HH:mm:ss');

	$('.mydatatable tfoot th').each(function () {
		$(this).html('<input type="text" placeholder="Search" />');
	});
});

function ValidateUploadFile() {
	//allowed extension
	var validFileExtension = "sof";
	var fileInput = document.getElementById('my-file-selector');	
	var fileExtension = fileInput.value.split('.').pop();

	if (fileExtension != validFileExtension) {
		alert('Invalid file type. Please upload an SOF.');
	}
	else {
		//$('#upload-file-info').html(this.files[0].name);
		//this.form.submit();
	}
}