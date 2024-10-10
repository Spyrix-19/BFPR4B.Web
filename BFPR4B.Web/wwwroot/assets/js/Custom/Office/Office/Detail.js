//// Function to fetch office details from the API
//async function getOfficeDetails(officeno) {
//    console.log(`Fetching details for Office No: ${officeno}`); // Debug log
//    try {
//        const response = await fetch(`/office/details?officeno=${officeno}`, {
//            method: 'GET',
//            headers: {
//                'Content-Type': 'application/json'
//            }
//        });

//        // Check for a successful response
//        if (!response.ok) {
//            const errorResponse = await response.json();
//            throw new Error(errorResponse.ErrorMessages || 'Failed to fetch office details.');
//        }

//        const data = await response.json();
//        console.log('API Response:', data); // Debug log

//        // Check if data is available
//        if (data && data.data) {
//            populateOfficeModal(data.data);
//        } else {
//            console.error('No data returned from API');
//        }
//    } catch (error) {
//        console.error('Error fetching office details:', error);
//        alert('An error occurred while fetching office details: ' + error.message);
//    }
//}

//// Function to populate the modal with the office details
//function populateOfficeModal(officeData) {
//    console.log('Populating modal with:', officeData); // Debug log
//    document.getElementById('kt_officeno').value = officeData.officeno;
//    document.getElementById('kt_office_code').value = officeData.officecode;
//    document.getElementById('kt_office_name').value = officeData.officename;

//    // If there are other fields to populate, do so here
//    // Example:
//    // document.getElementById('encoded_by').value = officeData.encodedby;
//    // document.getElementById('encoded_by_name').value = officeData.encodedbyname;
//    // document.getElementById('date_encoded').value = officeData.dateencoded.toISOString().split('T')[0]; // Format as needed
//}

//// Example of how to open the modal and load the data
//function openOfficeModal(officeno) {
//    getOfficeDetails(officeno); // Fetch and bind the office details

//    // Open the modal (assuming you're using Bootstrap)
//    $('#kt_modal_add_office').modal('show');
//}

//// Event listener to trigger the modal and load data
//document.addEventListener('DOMContentLoaded', function () {
//    // Attach click event to edit buttons created by DataTables
//    document.querySelectorAll('[data-kt-office-table-filter="edit_office"]').forEach(button => {
//        button.addEventListener('click', function () {
//            const officeno = this.getAttribute('data-officeno');
//            console.log(`Edit button clicked for Office No: ${officeno}`); // Debug log
//            openOfficeModal(officeno);
//        });
//    });
//});
