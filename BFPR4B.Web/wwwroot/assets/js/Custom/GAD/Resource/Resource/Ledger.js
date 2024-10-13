
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
    
    // Function to reset the Export values in the modal
    async function resetExport() {
        // Get the modal element
        const modal = document.getElementById('kt_modal_export_resource');

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
            const resetButton = modal.querySelector('[ data-kt-resource-export-action="reset"]');
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
        const cancelButton = document.querySelector('[data-kt-add-resource-modal-action="cancel"]');
        if (cancelButton) {
            cancelButton.addEventListener('click', function (event) {
                // Prevent the default behavior that closes the modal
                event.preventDefault();
            });
        }
    }

    // Function to open the "Add Resource" modal
    async function openAddEventModal(resourceno) {
        // Here, you can use the detno to customize the modal content or perform other actions as needed

        // Change the modal title here
        var modalTitle = document.getElementById("kt_add_resource_title");
        modalTitle.innerText = resourceno === 0 ? "Add Resource" : "Update Resource"; // Change "New Title" to your desired text

        $('#kt_resource_resourceno').val(resourceno);

        $('#kt_modal_add_resource').modal('show');

    }

    // Function to open the "Add Resource" modal
    async function openExportResourceModal() {

        $('#kt_modal_export_resource').modal('show');

    }

    // Function to open the "Add Resource" modal
    async function openResourceAttachmentModal(resourceno) {
        // Here, you can use the detno to customize the modal content or perform other actions as needed

        $('#kt_resource_attachment_resourceno').val(resourceno);

        $('#kt_modal_resource_attachment').modal('show');

    }

    // Function to open the "Add Resource" modal
    async function openResourceJournalModal(resourceno) {
        // Here, you can use the detno to customize the modal content or perform other actions as needed

        $('#kt_resource_journal_resourceno').val(resourceno);

        $('#kt_modal_resource_journal').modal('show');

    }

    // Function to update the DataTable with new data
    var updateDataTable = async function () {
        const table = $('#kt_table_resource').DataTable();
        table.ajax.reload(); // Reload the DataTable to fetch updated data
    };

    async function handleRowDeletion(resourceno) {
        try {

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
                const response = await fetch(`/resource/remove?resourceno=${resourceno}`, {
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

        dt = $("#kt_table_resource").DataTable({
            searchDelay: 500,
            processing: true,
            serverSide: false,
            order: [],
            ordering: false, // Disable sorting for all columns
            stateSave: true,
            select: {
                style: 'multi',
                selector: 'td:first-child input[type="checkbox"]',
                className: 'row-selected'
            },
            ajax: {
                url: "/resource/ledger",
                type: "GET",
                data: function (d) {
                    d.searchkey = $("#searchInput").val();
                }
            },
            columns: [
                {
                    data: 'Resourceno',
                    render: function (data) {
                        console.log("Resourceno:", data); // Check what value is passed here
                        return `<div class="btn-group">
									<a href="#" class="btn btn-primary btn-sm dropdown-toggle" data-bs-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
										<i class="fa fa-cog"></i>
										Option
										<span class="caret"></span>
									</a>
									<div class="dropdown-menu">
										<div class="dropdown-item px-3">
											<a style="cursor: pointer;" class="menu-link px-3" data-kt-resource-table-filter="view_resource_attachment" data-resourceno="${data}">
												View Resource Attachment
											</a>
										</div>
                                        <div class="dropdown-item px-3">
											<a style="cursor: pointer;" class="menu-link px-3" data-kt-resource-table-filter="view_resource_journal" data-resourceno="${data}">
												View Resource Journal
											</a>
										</div>
									</div>
								</div>`;
                    }
                },
                {
                    data: 'Resourceno',
                    render: function (data) {
                        return `<a class="btn btn-sm btn-primary btn-icon btn-icon-md" data-kt-resource-table-filter="edit_resource" data-toggle="tooltip" data-placement="top" title="Change" data-resourceno="${data}">
                                     <i class="la la-edit"></i>
                                </a>`;
                    }
                },
                { data: 'Title' },       
                { data: 'Description' },                
                { data: 'Encodedbyname' },
                {
                    data: 'Dateencoded',
                    render: function (data) {
                        // Format the date as "MM/DD/yyyy"
                        return data ? new Date(data).toLocaleDateString('en-US') : '';
                    }
                },
                {
                    data: 'Resourceno',
                    render: function (data, type, row) {
                        return `<a class="btn btn-sm btn-danger btn-icon btn-icon-md" data-kt-resource-table-filter="delete_resource" data-toggle="tooltip" data-placement="top" title="Delete" data-resourceno="${data}" >
                                     <i class="bi bi-trash3"></i>
                                </a>`;
                    }
                },
            ],
            columnDefs: [
                {
                    targets: [0, 1, 4, 5, 6],
                    className: 'text-center',
                }
            ]

        });

        table = dt.$;

        // Re-init functions on every table re-draw -- more info: https://datatables.net/reference/event/draw
        dt.on('draw', function () {

        });

        // Event handler for clearing search input when x is click
        $('[data-kt-resource-table-filter="add_resource"]').on('click', function () {
            const resourceno = 0;
            // Now you can use the userId to identify the user and open the modal accordingly
            openAddResourceModal(resourceno);
        });

        // Event handler for opening the "Add User" modal when the button is clicked
        $('#kt_table_resource').on('click', '[data-kt-resource-table-filter="edit_resource"]', function () {
            const resourceno = $(this).data('resourceno');
            // Now you can use the userId to identify the user and open the modal accordingly
            openAddEventModal(resourceno);
        });

        // Event handler for clearing search input when x is click
        $('[data-kt-resource-table-filter="export_resource"]').on('click', function () {
            // Now you can use the userId to identify the user and open the modal accordingly
            openExportResourceModal();
        });

        // Event handler for opening the "Add User" modal when the button is clicked
        $('#kt_table_resource').on('click', '[data-kt-resource-table-filter="view_resource_journal"]', function () {
            const resourceno = $(this).data('resourceno');
            // Now you can use the userId to identify the user and open the modal accordingly
            openEventJournalModal(resourceno);
        });

        // Event handler for opening the "Add User" modal when the button is clicked
        $('#kt_table_resource').on('click', '[data-kt-resource-table-filter="view_resource_attachment"]', function () {
            const resourceno = $(this).data('resourceno');
            // Now you can use the userId to identify the user and open the modal accordingly
            openEventAttachmentModal(resourceno);
        });


        // Event handler for the "x" button in Search
        $('[data-kt-resource-table-search="search"]').on('click', resetSearch);

        // Event handler for the "Discard" button in Export
        $('[data-kt-resource-export-action="reset"]').on('click', resetExport);

        // Event handler for the "Reset" button in Filter
        $('[data-kt-add-resource-modal-action="cancel"]').on('click', resetAddUpdate);

        // Event handler for opening the "Add User" modal when the button is clicked
        $('#kt_table_resource').on('click', '[data-kt-resource-table-filter="delete_resource"]', function () {
            const resourceno = $(this).data('resourceno');
            // Now you can use the userId to identify the user and open the modal accordingly
            handleRowDeletion(resourceno);
        });

        // Event handler for input field change in search when enter is hit
        $('#searchInput').on('keydown', function (e) {
            if (e.key === 'Enter') { // Check if Enter key (key code 13) is pressed
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