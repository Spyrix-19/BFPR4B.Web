
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
        const resetButton = document.querySelector('[data-kt-event-table-filter="reset"]');
        if (resetButton) {
            resetButton.addEventListener('click', function (event) {
                // Prevent the default behavior that closes the menu
                event.preventDefault();
            });
        }

        const table = $('#kt_table_event').DataTable();
        table.ajax.reload(); // Reload the DataTable to fetch updated data
    }

    // Function to reset the Export values in the modal
    async function resetExport() {
        // Get the modal element
        const modal = document.getElementById('kt_modal_export_event');

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
            const resetButton = modal.querySelector('[ data-kt-event-export-action="reset"]');
            if (resetButton) {
                resetButton.addEventListener('click', function (event) {
                    // Prevent the default behavior that closes the modal
                    event.preventDefault();
                });
            }
        }
    }

    async function populateDropdown(tablename, dropdownId) {
        try {
            const loginUrl = new URL('system/gentables', window.location.origin);
            loginUrl.searchParams.append('tablename', tablename);

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
        const cancelButton = document.querySelector('[data-kt-add-event-modal-action="cancel"]');
        if (cancelButton) {
            cancelButton.addEventListener('click', function (event) {
                // Prevent the default behavior that closes the modal
                event.preventDefault();
            });
        }
    }

    // Function to open the "Add Event" modal
    async function openAddEventModal(eventno) {
        // Here, you can use the detno to customize the modal content or perform other actions as needed

        // Change the modal title here
        var modalTitle = document.getElementById("kt_add_event_title");
        modalTitle.innerText = eventno === 0 ? "Add Event" : "Update Event"; // Change "New Title" to your desired text

        $('#kt_event_eventno').val(eventno);

        $('#kt_modal_add_event').modal('show');

    }

    // Function to open the "Add Event" modal
    async function openExportEventModal() {

        $('#kt_modal_export_event').modal('show');

    }

    // Function to open the "Add Rank" modal
    async function openEventJournalModal(eventno) {
        // Here, you can use the detno to customize the modal content or perform other actions as needed

        $('#kt_event_journal_eventno').val(eventno);

        $('#kt_modal_event_journal').modal('show');

    }

    // Function to update the DataTable with new data
    var updateDataTable = async function () {
        const table = $('#kt_table_event').DataTable();
        table.ajax.reload(); // Reload the DataTable to fetch updated data
    };

    async function handleRowDeletion(eventno) {
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
                const response = await fetch(`/event/remove?eventno=${eventno}`, {
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

        populateDropdown('EVENT TYPE', 'eventtypeDropdown');
        populateDropdown('GAD STATUS', 'statusDropdown');

        dt = $("#kt_table_event").DataTable({
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
                url: "/event/ledger",
                type: "GET",
                data: function (d) {
                    // Use the DataTables `ajax.data` option to customize the data sent in the request
                    d.status = $("#statusDropdown").val();
                    d.eventtype = $("#eventtypeDropdown").val();
                    d.searchkey = $("#searchInput").val();
                }
            },
            columns: [
                {
                    data: 'Eventno',
                    render: function (data) {
                        console.log("Eventno:", data); // Check what value is passed here
                        return `<div class="btn-group">
									<a href="#" class="btn btn-primary btn-sm dropdown-toggle" data-bs-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
										<i class="fa fa-cog"></i>
										Option
										<span class="caret"></span>
									</a>
									<div class="dropdown-menu">
										<div class="dropdown-item px-3">
											<a style="cursor: pointer;" class="menu-link px-3" data-kt-event-table-filter="view_event_attachment" data-eventno="${data}">
												View Event Attachment
											</a>
										</div>
                                        <div class="dropdown-item px-3">
											<a style="cursor: pointer;" class="menu-link px-3" data-kt-event-table-filter="view_event_journal" data-eventno="${data}">
												View Event Journal
											</a>
										</div>
									</div>
								</div>`;
                    }
                },
                {
                    data: 'Eventno',
                    render: function (data) {
                        return `<a class="btn btn-sm btn-primary btn-icon btn-icon-md" data-kt-event-table-filter="edit_event" data-toggle="tooltip" data-placement="top" title="Change" data-eventno="${data}">
                                     <i class="la la-edit"></i>
                                </a>`;
                    }
                },
                {
                    data: 'Eventname',
                    render: function (data) {
                        return `<div style="text-align: left !important;">${data || 'N/A'}</div>`;
                    },
                    className: 'text-left'
                },
                {
                    data: 'StatusName',
                    render: function (data, type, row) {
                        switch (data) {
                            case "PENDING":
                                return `<span class="badge badge-warning badge-md text-white">${data}</span>`;
                            case "ON GOING":
                                return `<span class="badge badge-primary badge-md text-white">${data}</span>`;
                            case "CANCELLED":
                                return `<span class="badge badge-danger badge-md text-white">${data}</span>`;
                            case "DONE":
                                return `<span class="badge badge-success badge-md text-white">${data}</span>`;
                            default:
                                return `<span class="badge badge-secondary badge-md text-white"></span>`;
                        }
                    }
                },
                { data: 'EventtypeName' },
                { data: 'Male' },
                { data: 'Female' },
                {
                    data: null,
                    render: function (data) {
                        return (data.Male || 0) + (data.Female || 0);
                    },
                    title: 'Total',
                    className: 'text-center'
                },
                {
                    data: 'Eventdatefrom',
                    render: function (data) {
                        return data ? new Date(data).toLocaleDateString('en-US') : '';
                    }
                },
                {
                    data: 'Eventdateto',
                    render: function (data) {
                        return data ? new Date(data).toLocaleDateString('en-US') : '';
                    },
                    className: 'text-center'
                },
                { data: 'Encodedbyname' },
                {
                    data: 'Dateencoded',
                    render: function (data) {
                        // Format the date as "MM/DD/yyyy"
                        return data ? new Date(data).toLocaleDateString('en-US') : '';
                    }
                },
                {
                    data: 'Eventno',
                    render: function (data, type, row) {
                        return `<a class="btn btn-sm btn-danger btn-icon btn-icon-md" data-kt-event-table-filter="delete_event" data-toggle="tooltip" data-placement="top" title="Delete" data-eventno="${data}" >
                                     <i class="bi bi-trash3"></i>
                                </a>`;
                    }
                },
            ],
            columnDefs: [
                {
                    targets: [0, 1, 3, 4, 5, 6, 7, 8, 9, 10, 11],
                    className: 'text-center',
                }
            ]

        });

        table = dt.$;

        // Re-init functions on every table re-draw -- more info: https://datatables.net/reference/event/draw
        dt.on('draw', function () {

        });

        // Event handler for clearing search input when x is click
        $('[data-kt-event-table-filter="add_event"]').on('click', function () {
            const eventno = 0;
            // Now you can use the userId to identify the user and open the modal accordingly
            openAddEventModal(eventno);
        });

        // Event handler for opening the "Add User" modal when the button is clicked
        $('#kt_table_event').on('click', '[data-kt-event-table-filter="edit_event"]', function () {
            const eventno = $(this).data('eventno');
            // Now you can use the userId to identify the user and open the modal accordingly
            openAddEventModal(eventno);
        });

        // Event handler for clearing search input when x is click
        $('[data-kt-event-table-filter="export_event"]').on('click', function () {
            // Now you can use the userId to identify the user and open the modal accordingly
            openExportEventModal();
        });

        // Event handler for opening the "Add User" modal when the button is clicked
        $('#kt_table_event').on('click', '[data-kt-event-table-filter="view_event_journal"]', function () {
            const eventno = $(this).data('eventno');
            // Now you can use the userId to identify the user and open the modal accordingly
            openEventJournalModal(eventno);
        });

        // Event handler for the "x" button in Search
        $('[data-kt-event-table-search="search"]').on('click', resetSearch);

        // Event handler for the "Reset" button in Filter
        $('[data-kt-event-table-filter="reset"]').on('click', resetFilter);

        // Event handler for the "Discard" button in Export
        $('[data-kt-event-export-action="reset"]').on('click', resetExport);

        // Event handler for the "Reset" button in Filter
        $('[data-kt-add-event-modal-action="cancel"]').on('click', resetAddUpdate);

        // Event handler for opening the "Add User" modal when the button is clicked
        $('#kt_table_event').on('click', '[data-kt-event-table-filter="delete_event"]', function () {
            const eventno = $(this).data('eventno');
            // Now you can use the userId to identify the user and open the modal accordingly
            handleRowDeletion(eventno);
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