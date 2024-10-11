
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
        const resetButton = document.querySelector('[data-kt-office-table-filter="reset"]');
        if (resetButton) {
            resetButton.addEventListener('click', function (event) {
                // Prevent the default behavior that closes the menu
                event.preventDefault();
            });
        }

        const table = $('#kt_table_office').DataTable();
        table.ajax.reload(); // Reload the DataTable to fetch updated data
    }

    // Function to reset the Export values in the modal
    async function resetExport() {
        // Get the modal element
        const modal = document.getElementById('kt_modal_export_office');

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
            const resetButton = modal.querySelector('[data-kt-office-export-action="reset"]');
            if (resetButton) {
                resetButton.addEventListener('click', function (event) {
                    // Prevent the default behavior that closes the modal
                    event.preventDefault();
                });
            }
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
        const cancelButton = document.querySelector('[data-kt-add-office-modal-action="cancel"]');
        if (cancelButton) {
            cancelButton.addEventListener('click', function (event) {
                // Prevent the default behavior that closes the modal
                event.preventDefault();
            });
        }
    }

    // Function to open the "Add office" modal
    async function openAddOfficeModal(officeno) {
        // Here, you can use the officeno to customize the modal content or perform other actions as needed

        // Change the modal title here
        var modalTitle = document.getElementById("kt_add_office_title");
        modalTitle.innerText = officeno === 0 ? "Add Office" : "Update Office"; // Change "New Title" to your desired text

        $('#kt_officeno').val(officeno);

        $('#kt_modal_add_office').modal('show');


    }

    // Function to open the "Add Course" modal
    async function openExportOfficeModal() {

        $('#kt_modal_export_office').modal('show');

    }

    // Function to open the "Add Rank" modal
    async function openOfficeJournalModal(officeno) {
        // Here, you can use the detno to customize the modal content or perform other actions as needed

        $('#kt_office_journal_officeno').val(officeno);

        $('#kt_modal_office_journal').modal('show');

    }

    // Function to update the DataTable with new data
    var updateDataTable = async function () {
        const table = $('#kt_table_office').DataTable();
        table.ajax.reload(); // Reload the DataTable to fetch updated data
    };

    async function handleRowDeletion(officeno, required) {
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
                    const response = await fetch(`/office/remove?officeno=${officeno}`, {
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

        dt = $("#kt_table_office").DataTable({
            searchDelay: 500,
            processing: true,
            serverSide: false,
            order: [],
            ordering: false, // Disable sorting for all columns
            stateSave: false,
            select: {
                style: 'multi',
                selector: 'td:first-child input[type="checkbox"]',
                className: 'row-selected'
            },
            ajax: {
                url: "/office/ledger",
                type: "GET",
                data: function (d) {
                    // Use the DataTables `ajax.data` option to customize the data sent in the request
                    d.searchkey = $("#searchInput").val();
                    d.officeno = $("#kt_office_journal_officeno").val();
                },
                cache: true,
            },
            columns: [
                {
                    data: 'Officeno',
                    render: function (data) {
                        console.log("Officeno:", data); // Check what value is passed here
                        return `<div class="btn-group">
									<a href="#" class="btn btn-primary btn-sm dropdown-toggle" data-bs-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
										<i class="fa fa-cog"></i>
										Option
										<span class="caret"></span>
									</a>
									<div class="dropdown-menu">
										<div class="dropdown-item px-3">
											<a style="cursor: pointer;" class="menu-link px-3" data-kt-office-table-filter="view_office_journal" data-officeno="${data}">
												View Office Journal
											</a>
										</div>
									</div>
								</div>`;
                    }
                },
                {
                    data: 'Officeno',
                    render: function (data) {
                        return `<a class="btn btn-sm btn-primary btn-icon btn-icon-md" data-kt-office-table-filter="edit_office" data-toggle="tooltip" data-placement="top" title="Change" data-officeno="${data}">
                                     <i class="la la-edit"></i>
                                </a>`;
                    }
                },
                { data: 'Officecode' },
                { data: 'Officename' },
                { data: 'Encodedbyname' },
                {
                    data: 'Dateencoded',
                    render: function (data) {
                        // Format the date as "MM/DD/yyyy"
                        return data ? new Date(data).toLocaleDateString('en-US') : '';
                    }
                },
                {
                    data: 'Officeno',
                    render: function (data, type, row) {

                        var reqValue = row.required;

                        return `<a class="btn btn-sm btn-danger btn-icon btn-icon-md" data-kt-office-table-filter="delete_office" data-toggle="tooltip" data-placement="top" title="Delete" data-officeno="${data}" data-required="${reqValue}">
                                     <i class="bi bi-trash3"></i>
                                </a>`;
                    }
                },
            ],
            columnDefs: [
                {
                    targets: [0, 1, 5, 6],
                    className: 'text-center',
                }
            ]

        });

        table = dt.$;

        // Re-init functions on every table re-draw -- more info: https://datatables.net/reference/event/draw
        dt.on('draw', function () {

        });

        // Event handler for clearing search input when x is click
        $('[data-kt-office-table-filter="add_office"]').on('click', function () {
            const officeno = 0;
            // Now you can use the userId to identify the user and open the modal accordingly
            openAddOfficeModal(officeno);
        });

        // Event handler for opening the "Add User" modal when the button is clicked
        $('#kt_table_office').on('click', '[data-kt-office-table-filter="edit_office"]', function () {
            const officeno = $(this).data('officeno');
            // Now you can use the userId to identify the user and open the modal accordingly
            openAddOfficeModal(officeno);
        });

        // Event handler for clearing search input when x is click
        $('[data-kt-office-table-filter="export_office"]').on('click', function () {
            // Now you can use the userId to identify the user and open the modal accordingly
            openExportOfficeModal();
        });

        // Event handler for opening the "Add User" modal when the button is clicked
        $('#kt_table_office').on('click', '[data-kt-office-table-filter="view_office_journal"]', function () {
            const officeno = $(this).data('officeno');
            // Now you can use the userId to identify the user and open the modal accordingly
            openOfficeJournalModal(officeno);
        });


        // Event handler for the "x" button in Search
        $('[data-kt-office-table-search="search"]').on('click', resetSearch);

        // Event handler for the "Reset" button in Filter
        $('[data-kt-office-table-filter="reset"]').on('click', resetFilter);

        // Event handler for the "Discard" button in Export
        $('[data-kt-office-export-action="reset"]').on('click', resetExport);

        // Event handler for the "Reset" button in Filter
        $('[data-kt-add-office-modal-action="cancel"]').on('click', resetAddUpdate);


        // Event handler for opening the "Add User" modal when the button is clicked
        $('#kt_table_office').on('click', '[data-kt-office-table-filter="delete_office"]', function () {
            const officeno = $(this).data('officeno');
            const required = $(this).data('required');
            // Now you can use the userId to identify the user and open the modal accordingly
            handleRowDeletion(officeno, required);
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