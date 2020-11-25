// Uploads an SOF file
async function uploadFile() {
	let file = document.getElementById('file-upload');
	let formData = new FormData();

	if (isValidFile(file)) {
		formData.append('file', file.files[0]);

		await fetch('api/manageoutages', {
			method: 'POST',
			body: formData
		});

		// Reload the page
		location.reload();
	}
	else {
		alert("Invalid file type. Please choose a file with a .sof extension.");
	}

}

// Validates SOF file
function isValidFile(file) {
	// Get file extension
	var extension = file.value.split('.').pop();

	// Check file extension
	if (extension != 'sof') {
		return false;
	}
	else {
		return true;
	}

}