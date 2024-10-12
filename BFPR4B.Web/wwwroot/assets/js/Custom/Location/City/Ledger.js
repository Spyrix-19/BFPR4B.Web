
"use strict";

// Class definition
var KTDatatablesServerSide = function () {
    // Shared variables
    var table;
    var dt;

    async function resetSearch() {
        const searchInput = document.getElementById("searchInput");
        const clearButton = document.getElementById("clearButton");

        searchInput.addEventListener("input", function () {
            clearButton.style.display = searchInput.value.trim() !== "" ? "block" : "none";
        });

        clearButton.addEventListener("click", function () {
            searchInput.value = "";
            clearButton.style.display = "none";
        });
    }

    // Function to reset the filter values of Filters
    async function resetFilter() {
        // Get all the select elements within the filter menu
        const selectElements = document.querySelectorAll('.menu-sub [data-kt-select2]');

        // Reset the values of the select elements to their default (empty) state
        selectElements.forEach((select) => {
            select.value = '';
            const event = new Event('change', { bubbles: true });
            select.dispatchEvent(event);
        });

        // Check if the button clicked is the "Reset" button
        const resetButton = document.querySelector('[data-kt-city-table-filter="reset"]');
        if (resetButton) {
            resetButton.addEventListener('click', function (event) {
                // Prevent the default behavior that closes the menu
                event.preventDefault();
            });
        }

        const table = $('#kt_table_city').DataTable();
        table.ajax.reload(); // Reload the DataTable to fetch updated data
    }

    // Function to reset the Export values in the modal
    async function resetExport() {
        // Get the modal element
        const modal = document.getElementById('kt_modal_export_city');

        // Check if the modal exists
        if (modal) {
            // Find all the select elements within the modal content
            const selectElements = modal.querySelectorAll('[data-control="select2"]');

            // Reset the values of the select elements to their default (empty) state
            selectElements.forEach((select) => {
                select.value = '';
                const event = new Event('change', { bubbles: true });
                select.dispatchEvent(event);
            });

            // Find the "Reset" button within the modal
            const resetButton = modal.querySelector('[data-kt-city-table-filter="reset"]');
            if (resetButton) {
                resetButton.addEventListener('click', function (event) {
                    // Prevent the default behavior that closes the modal
                    event.preventDefault();
                });
            }
        }
    }
    
    async function populateProvinceDropdown(regionno, dropdownId) {
        try {
            const loginUrl = new URL('system/getprovince', window.location.origin);
            loginUrl.searchParams.append('regionno', regionno);

            const response = await fetch(loginUrl, {
                method: 'GET',
                headers: {
                    'Content-Type': 'application/json',
                    'Accept': 'application/json'
                }
            });

            if (response.ok) {
                const data = await response.json();
                const dropdown = document.getElementById(dropdownId);

                dropdown.innerHTML = ''; // Clear existing options
                const emptyOption = document.createElement('option');
                dropdown.appendChild(emptyOption);

                data.forEach(option => {
                    const optionElement = document.createElement('option');
                    optionElement.value = option.value;
                    optionElement.text = option.text;
                    dropdown.appendChild(optionElement);
                });
            } else {
                console.error('Failed to fetch data:', response.status, response.statusText);
            }
        } catch (error) {
            console.error('An error occurred:', error);
        }
    }

    async function populateRegionDropdown(dropdownId) {
        try {
            const loginUrl = new URL('system/getregion', window.location.origin);

            const response = await fetch(loginUrl, {
                method: 'GET',
                headers: {
                    'Content-Type': 'application/json',
                    'Accept': 'application/json'
                }
            });

            if (response.ok) {
                const data = await response.json();
                const dropdown = document.getElementById(dropdownId);

                dropdown.innerHTML = ''; // Clear existing options
                const emptyOption = document.createElement('option');
                dropdown.appendChild(emptyOption);

                data.forEach(option => {
                    const optionElement = document.createElement('option');
                    optionElement.value = option.value;
                    optionElement.text = option.text;
                    dropdown.appendChild(optionElement);
                });
            } else {
                console.error('Failed to fetch data:', response.status, response.statusText);
            }
        } catch (error) {
            console.error('An error occurred:', error);
        }
    }

    // Function to reset the filter values of Filters
    async function resetAddUpdate() {
        // Get all the select elements within the modal
        const selectElements = document.querySelectorAll('.modal-dialog [data-kt-select2]');
        const inputElements = document.querySelectorAll('.modal-dialog [type="text"]');


        // Reset the values of select elements with the data-kt-select2 attribute
        selectElements.forEach((select) => {
            // Assuming you are using Select2 library, you can trigger a change event
            // to reset the Select2 select element to its default state
            $(select).val(0).trigger('change');
        });

        // Reset the values of text input fields to empty
        inputElements.forEach((input) => {
            input.value = '';
        });

        // Check if the button clicked is the "Cancel" button
        const cancelButton = document.querySelector('[data-kt-add-city-modal-action="cancel"]');
        if (cancelButton) {
            cancelButton.addEventListener('click', function (event) {
                // Prevent the default behavior that closes the modal
                event.preventDefault();
            });
        }
    }

    // Function to open the "Add City" modal
    async function openAddCityModal(cityno) {
        // Here, you can use the detno to customize the modal content or perform other actions as needed

        // Change the modal title here
        var modalTitle = document.getElementById("kt_add_city_title");
        modalTitle.innerText = cityno === 0 ? "Add City" : "Update City"; // Change "New Title" to your desired text

        $('#kt_city_cityno').val(cityno);

        $('#kt_modal_add_city').modal('show');


    }

    // Function to open the "Add City" modal
    async function openExportCityModal() {

        $('#kt_modal_export_city').modal('show');

    }

    // Function to open the "Add City" modal
    async function openCityJournalModal(cityno) {
        // Here, you can use the detno to customize the modal content or perform other actions as needed

        $('#kt_city_journal_cityno').val(cityno);

        $('#kt_modal_city_journal').modal('show');

    }

    // Function to update the DataTable with new data
    var updateDataTable = async function () {
        const table = $('#kt_table_city').DataTable();
        table.ajax.reload(); // Reload the DataTable to fetch updated data
    };

    async function handleRowDeletion(cityno, required) {
        try {

            // Get the row data from the DataTable
            //const rowData = dt.row($('#kt_table_rank tbody tr[data-detno="' + detno + '"]')).data();

            if (required) {
                await Swal.fire({
                    text: "This record is required.",
                    icon: "info",
                    buttonsStyling: false,
                    confirmButtonText: "Ok, got it!",
                    customClass: {
                        confirmButton: "btn fw-bold btn-primary",
                    }
                });
            }
            else {
                // SweetAlert2 pop-up...
                const result = await Swal.fire({
                    text: "Are you sure you want to delete this record?",
                    icon: "warning",
                    showCancelButton: true,
                    buttonsStyling: false,
                    confirmButtonText: "Yes, delete!",
                    cancelButtonText: "No, cancel",
                    customClass: {
                        confirmButton: "btn fw-bold btn-danger",
                        cancelButton: "btn fw-bold btn-active-light-primary"
                    }
                });

                if (result.value) {
                    const response = await fetch(`/city/remove?cityno=${cityno}`, {
                        method: 'DELETE',
                        //headers: {
                        //    'Content-Type': 'application/json',
                        //    'Accept': 'application/json'
                        //},
                    });

                    if (response.ok) {
                        // Success message
                        await Swal.fire({
                            text: "You have successfully deleted this record.",
                            icon: "success",
                            buttonsStyling: false,
                            confirmButtonText: "Ok, got it!",
                            customClass: {
                                confirmButton: "btn fw-bold btn-primary",
                            }
                        });

                        updateDataTable();

                    } else {
                        // Error message
                        await Swal.fire({
                            text: "Failed to delete user data.",
                            icon: "error",
                            buttonsStyling: false,
                            confirmButtonText: "Ok, got it!",
                            customClass: {
                                confirmButton: "btn fw-bold btn-primary",
                            }
                        });
                        console.error('Failed to delete user data:', response.status, response.statusText);
                    }
                } else if (result.dismiss === 'cancel') {
                    // Cancelled action
                    await Swal.fire({
                        text: "The record was not deleted.",
                        icon: "error",
                        buttonsStyling: false,
                        confirmButtonText: "Ok, got it!",
                        customClass: {
                            confirmButton: "btn fw-bold btn-primary",
                        }
                    });
                }

            }

        } catch (error) {
            console.error('An error occurred while handling row deletion:', error);
        }
    }

    // Private functions
    var initDatatable = function () {

        if (dt) {
            // If the DataTable instance already exists, destroy it
            dt.destroy();
        }

        populateProvinceDropdown(0, 'provinceDropdown');
        populateRegionDropdown('regionDropdown');

        dt = $("#kt_table_city").DataTable({
            searchDelay: 500,
            processing: true,
            serverSide: false,
            order: [],
            ordering: false, // Disable sorting for all columns
            stateSave: false,
            page: 1, // Set the default page to 1
            select: {
                style: 'multi',
                selector: 'td:first-child input[type="checkbox"]',
                className: 'row-selected'
            },
            ajax: {
                url: "/city/ledger",
                type: "GET",
                data: function (d) {
                    // Use the DataTables `ajax.data` option to customize the data sent in the request
                    d.searchkey = $("#searchInput").val();
                    d.provinceno = $("#provinceDropdown").val();
                    d.regionno = $("#regionDropdown").val();
                }
            },
            columns: [
                {
                    data: 'Cityno',
                    render: function (data) {
                        return `<div class="btn-group">
									<a href="#" class="btn btn-primary btn-sm dropdown-toggle" data-bs-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
										<i class="fa fa-cog"></i>
										Option
										<span class="caret"></span>
									</a>
									<div class="dropdown-menu">
										<div class="dropdown-item px-3">
											<a style="cursor: pointer;" class="menu-link px-3" data-kt-city-table-filter="view_city_journal" data-cityno="${data}">
												View City Journal
											</a>
										</div>
									</div>
								</div>`;
                    }
                },
                {
                    data: 'Cityno',
                    render: function (data) {
                        return `<a class="btn btn-sm btn-primary btn-icon btn-icon-md" data-kt-city-table-filter="edit_city" data-toggle="tooltip" data-placement="top" title="Change" data-cityno="${data}">
                                     <i class="la la-edit"></i>
                                </a>`;
                    }
                },
                { data: 'Cityname' },
                { data: 'Zipcode' },
                { data: 'Provincename' },
                { data: 'Regioncode' },
                { data: 'Encodedbyname' },
                {
                    data: 'Dateencoded',
                    render: function (data) {
                        // Format the date as "MM/DD/yyyy"
                        return data ? new Date(data).toLocaleDateString('en-US') : '';
                    }
                },
                {
                    data: 'Cityno',
                    render: function (data, type, row) {

                        // Assuming row contains the data from your API response
                        var reqValue = row.Required; // Replace 'REQ' with the actual field name from your API

                        return `<a class="btn btn-sm btn-danger btn-icon btn-icon-md" data-kt-city-table-filter="delete_city" data-toggle="tooltip" data-placement="top" title="Delete" data-cityno="${data}" data-required="${reqValue}">
                                     <i class="bi bi-trash3"></i>
                                </a>`;
                    }
                },
            ],
            columnDefs: [
                {
                    targets: [0, 1, 7],
                    className: 'text-center',
                }
            ]

        });

        table = dt.$;

        // Re-init functions on every table re-draw -- more info: https://datatables.net/reference/event/draw
        dt.on('draw', function () {

        });

        // Event handler for clearing search input when x is click
        $('[data-kt-city-table-filter="add_city"]').on('click', function () {
            const cityno = 0;
            // Now you can use the userId to identify the user and open the modal accordingly
            openAddCityModal(cityno);
        });

        // Event handler for opening the "Add User" modal when the button is clicked
        $('#kt_table_city').on('click', '[data-kt-city-table-filter="edit_city"]', function () {
            const cityno = $(this).data('cityno');
            // Now you can use the userId to identify the user and open the modal accordingly
            openAddCityModal(cityno);
        });

        // Event handler for clearing search input when x is click
        $('[data-kt-city-table-filter="export_city"]').on('click', function () {
            // Now you can use the userId to identify the user and open the modal accordingly
            openExportCityModal();
        });

        // Event handler for opening the "Add User" modal when the button is clicked
        $('#kt_table_city').on('click', '[data-kt-city-table-filter="view_city_journal"]', function () {
            const cityno = $(this).data('cityno');
            // Now you can use the userId to identify the user and open the modal accordingly
            openCityJournalModal(cityno);
        });

        // Event handler for the "x" button in Search
        $('[data-kt-city-table-search="search"]').on('click', resetSearch);

        // Event handler for the "Reset" button in Filter
        $('[data-kt-city-table-filter="reset"]').on('click', resetFilter);

        // Event handler for the "Discard" button in Export
        $('[data-kt-city-export-action="reset"]').on('click', resetExport);

        // Event handler for the "Reset" button in Filter
        $('[data-kt-add-city-modal-action="cancel"]').on('click', resetAddUpdate);

        // Event handler for opening the "Add User" modal when the button is clicked
        $('#kt_table_city').on('click', '[data-kt-city-table-filter="delete_city"]', function () {
            const cityno = $(this).data('cityno');
            const required = $(this).data('required');
            // Now you can use the userId to identify the user and open the modal accordingly
            handleRowDeletion(cityno, required);
        });

        // Event handler for input field change in search when enter is hit
        $('#searchInput').on('keydown', function (e) {
            if (e.keyCode === 13) { // Check if Enter key (key code 13) is pressed
                dt.ajax.reload();
            }
        });

        // Event handler for clearing search input when x is click
        $('#clearButton').on('click', function () {
            $('#searchInput').val('');
            dt.ajax.reload();
        });

        // Event handler for clearing search input
        $('#submitFilter').on('click', function () {
            dt.ajax.reload();
        });
    }

    // Public methods
    return {
        init: function () {
            initDatatable();
        }
    }
}();

// On document ready
KTUtil.onDOMContentLoaded(function () {
    KTDatatablesServerSide.init();
});