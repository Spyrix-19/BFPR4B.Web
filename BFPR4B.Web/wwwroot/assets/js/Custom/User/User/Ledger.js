"use strict";

// Class definition
var KTDatatablesServerSide = function () {
    // Shared variables
    var table;
    var dt;

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
        const resetButton = document.querySelector('[data-kt-user-table-filter="reset"]');
        if (resetButton) {
            resetButton.addEventListener('click', function (event) {
                // Prevent the default behavior that closes the menu
                event.preventDefault();
            });
        }
    }

    // Function to reset the Export values in the modal
    async function resetExport() {
        // Get the modal element
        const modal = document.getElementById('kt_modal_export_users');

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
            const resetButton = modal.querySelector('[data-kt-users-export-action="reset"]');
            if (resetButton) {
                resetButton.addEventListener('click', function (event) {
                    // Prevent the default behavior that closes the modal
                    event.preventDefault();
                });
            }
        }
    }

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
        const checkElements = document.querySelectorAll('.modal-dialog [type="checkbox"]');
        const selectElements = document.querySelectorAll('.modal-dialog [data-kt-select2]');
        const inputElements = document.querySelectorAll('.modal-dialog [type="text"]');
        const inputPasswordElements = document.querySelectorAll('.modal-dialog [type="password"]');

        // Reset the values of checkboxes to unchecked state
        checkElements.forEach((checkbox) => {
            checkbox.checked = false;
        });

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

        // Reset the values of password input fields to empty
        inputPasswordElements.forEach((input) => {
            input.value = '';
        });

        // Check if the button clicked is the "Cancel" button
        const cancelButton = document.querySelector('[data-kt-add-users-modal-action="cancel"]');
        if (cancelButton) {
            cancelButton.addEventListener('click', function (event) {
                // Prevent the default behavior that closes the modal
                event.preventDefault();
            });
        }
    }

    // Function to open the "Add User" modal
    async function openAddUserModal(userId) {
        // Here, you can use the userId to customize the modal content or perform other actions as needed

        // Change the modal title here
        var modalTitle = document.getElementById("kt_add_user_title");
        modalTitle.innerText = userId === 0 ? "Add User" : "Update User"; // Change "New Title" to your desired text

        $('#kt_userno').val(userId);

        $('#kt_modal_add_user').modal('show');
    }

    // Initialize DataTable and event handlers
    var initDatatable = function () {
        if (dt) {
            // If the DataTable instance already exists, destroy it
            dt.destroy();
        }

        populateDropdown('AREA OF ASSIGNMENT', 'areaAssignmentDropdown');
        populateDropdown('ACCOUNT ROLE', 'roleDropdown');
        populateDropdown('TWO FACTOR AUTHENTICATION', 'tfaDropdown');
        //populateDropdown('UNIT OF ASSIGNMENT', 'unitassignDropdown');

        dt = $("#kt_table_users").DataTable({
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
                url: "/users/ledger",
                type: "GET",
                data: function (d) {
                    // Use the DataTables `ajax.data` option to customize the data sent in the request
                    d.role = $("#roleDropdown").val();
                    d.areaassign = $("#areaAssignmentDropdown").val();
                    d.tfa = $("#tfaDropdown").val();
                    d.unitassign = $("#unitassignDropdown").val();
                    d.searchkey = $("#searchInput").val();
                }
            },
            columns: [
                {
                    data: null,
                    render: function (data) {
                        return `<div class="btn-group">
									<a href="#" class="btn btn-primary btn-sm dropdown-toggle" data-bs-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
										<i class="fa fa-cog"></i>
										Option
										<span class="caret"></span>
									</a>
									<div class="dropdown-menu">
										<div class="dropdown-item px-3">
											<a href="#" class="menu-link px-3">
												View User Journal
											</a>
										</div>
										<div class="dropdown-item px-3">
											<a href="#" class="menu-link px-3">
												View User Access
											</a>
										</div>
									</div>
								</div>`;
                    }
                },
                {
                    data: 'Userno',
                    render: function (data) {
                        return `<a class="btn btn-sm btn-primary btn-icon btn-icon-md" data-kt-user-table-filter="edit_user" data-toggle="tooltip" data-placement="top" title="Change" data-userno="${data}">
                                     <i class="la la-edit"></i>
                                </a>`;
                    }
                },
                { data: 'Username' },
                { data: 'Accountnumber' },
                { data: 'Mobileno' },
                { data: 'Emailadd' },
                {
                    data: 'Activeuser',
                    render: function (data) {
                        // Conditional formatting based on the value of Activeuser
                        if (data) {
                            return '<span class="badge py-3 px-4 fs-7 badge-light-success">Active</span>';
                        } else {
                            return '<span class="badge py-3 px-4 fs-7 badge-light-danger">Inactive</span>';
                        }
                    }
                },
                { data: 'Areaassignname' },
                { data: 'Stationname' },
                {
                    data: 'Passwordexpiry',
                    render: function (data) {
                        // Format the date as "MM/DD/yyyy"
                        return data ? new Date(data).toLocaleDateString('en-US') : '';
                    }
                },
                { data: 'Rolename' },
                {
                    data: 'Dateencoded',
                    render: function (data) {
                        // Format the date as "MM/DD/yyyy"
                        return data ? new Date(data).toLocaleDateString('en-US') : '';
                    }
                },
                {
                    data: 'Userno',
                    render: function (data) {
                        return `<a class="btn btn-sm btn-danger btn-icon btn-icon-md" data-kt-user-table-filter="delete_user" data-toggle="tooltip" data-placement="top" title="Delete" data-userno="${data}">
                                     <i class="bi bi-trash3"></i>
                                </a>`;
                    }
                },
            ],
            columnDefs: [
                {
                    targets: [0, 1, 6, 9, 11, 12],
                    className: 'text-center',
                }
            ]
        });

        table = dt.$;

        // Re-init functions on every table re-draw
        dt.on('draw', function () {
            // ... (your existing code for handling table redraw)
        });

        //// Event handler for opening the "Add User" modal when the "Change" button is clicked
        //dt.on('click', '[data-kt-user-table-filter="add_user"]', function () {
        //    $('#kt_modal_add_user').modal('show');
        //});

        // Event handler for clearing search input when x is click
        $('[data-kt-user-table-filter="add_user"]').on('click', function () {
            const userId = 0;
            // Now you can use the userId to identify the user and open the modal accordingly
            openAddUserModal(userId);
        });

        // Event handler for opening the "Add User" modal when the button is clicked
        $('#kt_table_users').on('click', '[data-kt-user-table-filter="edit_user"]', function () {
            const userId = $(this).data('userno');
            // Now you can use the userId to identify the user and open the modal accordingly
            openAddUserModal(userId);
        });

        // Event handler for the "Reset" button in Filter
        $('[data-kt-user-table-filter="reset"]').on('click', resetFilter);

        // Event handler for the "Discard" button in Export
        $('[data-kt-users-export-action="reset"]').on('click', resetExport);

        // Event handler for the "x" button in Search
        $('[data-kt-user-table-search="search"]').on('click', resetSearch);

        // Event handler for the "Reset" button in Filter
        $('[data-kt-add-users-modal-action="cancel"]').on('click', resetAddUpdate);

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
    };

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
