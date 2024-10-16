
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
        const resetButton = document.querySelector('[data-kt-user-table-filter="reset"]');
        if (resetButton) {
            resetButton.addEventListener('click', function (event) {
                // Prevent the default behavior that closes the menu
                event.preventDefault();
            });
        }

        const table = $('#kt_table_user').DataTable();
        table.ajax.reload(); // Reload the DataTable to fetch updated data
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
        const cancelButton = document.querySelector('[data-kt-add-user-modal-action="cancel"]');
        if (cancelButton) {
            cancelButton.addEventListener('click', function (event) {
                // Prevent the default behavior that closes the modal
                event.preventDefault();
            });
        }
    }

    // Function to open the "Add User" modal
    async function openAddUserModal(userno) {
        // Here, you can use the userno to customize the modal content or perform other actions as needed

        // Change the modal title here
        var modalTitle = document.getElementById("kt_add_user_title");
        modalTitle.innerText = userno === 0 ? "Add User" : "Update User"; // Change "New Title" to your desired text

        $('#kt_user_userno').val(userno);

        $('#kt_modal_add_user').modal('show');


    }    

    // Function to open the "Add Rank" modal
    async function openUserJournalModal(userno) {
        // Here, you can use the userno to customize the modal content or perform other actions as needed

        $('#kt_user_journal_userno').val(userno);

        $('#kt_modal_user_journal').modal('show');

    }

    // Function to update the DataTable with new data
    var updateDataTable = async function () {
        const table = $('#kt_table_user').DataTable();
        table.ajax.reload(); // Reload the DataTable to fetch updated data
    };

    async function handleRowDeletion(userno) {
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
                const response = await fetch(`/user/remove?userno=${userno}`, {
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

        populateDropdown('ELIGIBILITY LEVEL', 'roleDropdown');

        // Calculate height directly within the DataTable initialization
        const windowHeight = $(window).height();
        const headerHeight = $('.header-class').outerHeight() || 0; // Replace with your header's actual class
        const footerHeight = $('.footer-class').outerHeight() || 0; // Replace with your footer's actual class
        const dataTableHeight = Math.max(windowHeight - headerHeight - footerHeight - 25, windowHeight * 0.5); // Adjust for any extra spacing

        dt = $("#kt_table_user").DataTable({
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
                url: "/user/ledger",
                type: "GET",
                data: function (d) {
                    // Use the DataTables `ajax.data` option to customize the data sent in the request
                    d.role = $("#roleDropdown").val();
                    d.searchkey = $("#searchInput").val();
                }
            },
            columns: [
                {
                    data: 'Userno',
                    render: function (data) {
                        return `
                                <div class="btn-group">
                                    <a href="#" class="btn btn-primary btn-sm dropdown-toggle" data-bs-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
                                        <i class="fa fa-cog"></i> Option <span class="caret"></span>
                                    </a>
                                    <div class="dropdown-menu">
                                        <div class="dropdown-item px-3">
                                            <a style="cursor: pointer;" class="menu-link px-3" data-kt-user-table-filter="change_user_profile" data-userno="${data}">
                                                Change User Profile
                                            </a>
                                        </div>
                                        <div class="separator border-gray-200"></div>
                                        <div class="dropdown-item px-3">
                                            <a style="cursor: pointer;" class="menu-link px-3" data-kt-user-table-filter="change_user_password" data-userno="${data}">
                                                Change User Password
                                            </a>
                                        </div>
                                         <div class="dropdown-item px-3">
                                            <a style="cursor: pointer;" class="menu-link px-3" data-kt-user-table-filter="reset_user_password" data-userno="${data}">
                                                Reset User Password
                                            </a>
                                        </div>
                                         <div class="dropdown-item px-3">
                                            <a style="cursor: pointer;" class="menu-link px-3" data-kt-user-table-filter="update_user_password_expiry" data-userno="${data}">
                                                Update Password Expiry
                                            </a>
                                        </div>
                                        <div class="separator border-gray-200"></div>
                                         <div class="dropdown-item px-3">
                                            <a style="cursor: pointer;" class="menu-link px-3" data-kt-user-table-filter="activate_user" data-userno="${data}">
                                                Activate User
                                            </a>
                                        </div>
                                        <div class="dropdown-item px-3">
                                            <a style="cursor: pointer;" class="menu-link px-3" data-kt-user-table-filter="lock_user" data-userno="${data}">
                                                Lock User
                                            </a>
                                        </div>     
                                        <div class="separator border-gray-200"></div>
                                        <div class="dropdown-item px-3">
                                            <a style="cursor: pointer;" class="menu-link px-3" data-kt-user-table-filter="view_user_journal" data-userno="${data}">
                                                View User Journal
                                            </a>
                                        </div>
                                    </div>
                                </div>
                                <a class="btn btn-sm btn-primary btn-icon btn-icon-md" data-kt-user-table-filter="edit_user" data-toggle="tooltip" data-placement="top" title="Change" data-userno="${data}">
                                    <i class="la la-edit"></i>
                                </a>
                                <a class="btn btn-sm btn-danger btn-icon btn-icon-md" data-kt-user-table-filter="delete_user" data-toggle="tooltip" data-placement="top" title="Delete" data-userno="${data}">
                                     <i class="bi bi-trash3"></i>
                                </a>`;
                    },
                    className: 'text-center'
                },
                {
                    data: 'Picture', // The field containing the byte array
                    render: function (data, type, row) {
                        // Check if the PictureType value is valid
                        const pictureType = row.Picturetype ? row.Picturetype.replace('.', '') : ''; // Remove the dot for the MIME type

                        // Determine the correct MIME type
                        const mimeType = pictureType.toLowerCase() === 'png' ? 'image/png' :
                            pictureType.toLowerCase() === 'jpg' || pictureType.toLowerCase() === 'jpeg' ? 'image/jpeg' :
                                pictureType.toLowerCase() === 'gif' ? 'image/gif' : '';

                        // HTML structure for the cell
                        let cellContent = '';

                        // Check if data is defined and has length
                        if (data && data.length > 0) {
                            // Convert byte array to Base64
                            const byteArray = new Uint8Array(data); // Ensure data is in Uint8Array
                            let binaryString = '';
                            for (let i = 0; i < byteArray.length; i++) {
                                binaryString += String.fromCharCode(byteArray[i]); // Convert byte to binary string
                            }
                            const base64String = btoa(binaryString); // Convert to Base64

                            // Create the image source
                            const imgSrc = `data:${mimeType};base64,${base64String}`;

                            // Construct the HTML for the image and user details
                            cellContent = `
                                <div style="display: flex; align-items: center;">
                                    <img src="${imgSrc}" alt="User Picture" style="width:50px; height:50px; margin-right: 10px;" />
                                    <div style="display: flex; flex-direction: column;">
                                        <span style="text-transform: uppercase; font-weight: bold;">${row.Fullname}</span>
                                        <span>${row.Accountnumber}</span>
                                    </div>
                                </div>
                            `;
                        } else {
                            // Return the default image path if no picture data is available
                            const defaultImagePath = '/assets/media/logos/mimaropa.png'; // Ensure the path is correct
                            cellContent = `
                                <div style="display: flex; align-items: center;">
                                    <img src="${defaultImagePath}" alt="Default Image" style="width:50px; height:50px; margin-right: 10px;" />
                                    <div style="display: flex; flex-direction: column;">
                                        <span style="text-transform: uppercase; font-weight: bold;">${row.Fullname}</span>
                                        <span>${row.Accountnumber}</span>
                                    </div>
                                </div>
                            `;
                        }

                        return cellContent; // Return the constructed HTML content
                    },
                    className: 'text-left'
                },
                { data: 'Mobileno' },
                { data: 'Emailaddress' },
                {
                    data: 'Activeuser',
                    render: function (data, type, row) {
                        // Check if data is a boolean or a string
                        if (data === "false" || data === false) {
                            return `<span class="badge py-3 px-4 fs-7 badge-light-danger">Inactive</span>`;
                        } else {
                            return `<span class="badge py-3 px-4 fs-7 badge-light-success">Active</span>`;
                        }
                    },
                    className: 'text-center'
                },
                {
                    data: 'Inactivedate',
                    render: function (data) {

                        const inactiveDate = data ? new Date(data) : null;

                        // Check if lockDate is valid and extract the year
                        const inactiveYear = inactiveDate ? inactiveDate.getFullYear() : null;

                        // If the year is 1900, return an empty string; otherwise, format the date
                        if (inactiveYear === 1900) {
                            return '';
                        } else {
                            return inactiveDate ? inactiveDate.toLocaleDateString('en-US') : '';
                        }
                    },
                    className: 'text-center'
                },
                {
                    data: 'Passwordlock',
                    render: function (data, type, row) {
                        // Check if data is a boolean or a string
                        if (data === "true" || data === true) {
                            return `<span class="badge py-3 px-4 fs-7 badge-light-danger">Lock</span>`;
                        } else {
                            return `<span class="badge py-3 px-4 fs-7 badge-light-success">Unlock</span>`;
                        }
                    },
                    className: 'text-center'
                },
                {
                    data: 'lockdate',
                    render: function (data) {
                        // Create a date object from the lockdate
                        const lockDate = data ? new Date(data) : null;

                        // Check if lockDate is valid and extract the year
                        const lockYear = lockDate ? lockDate.getFullYear() : null;

                        // If the year is 1900, return an empty string; otherwise, format the date
                        if (lockYear === 1900) {
                            return '';
                        } else {
                            return lockDate ? lockDate.toLocaleDateString('en-US') : '';
                        }
                    },
                    className: 'text-center'

                },
                {
                    data: 'Passwordexpiry',
                    render: function (data, type, row) {
                        // Create a date object from Passwordexpiry
                        const expiryDate = data ? new Date(data) : null;

                        // Get the current date
                        const currentDate = new Date();

                        // Check if expiryDate is valid
                        if (expiryDate) {
                            // Compare the expiry date with the current date
                            if (expiryDate < currentDate) {
                                return `<span class="badge py-3 px-4 fs-7 badge-light-danger">Expired</span>`;
                            } else {
                                return `<span class="badge py-3 px-4 fs-7 badge-light-success">Active</span>`;
                            }
                        } else {
                            return `<span class="badge py-3 px-4 fs-7 badge-light-warning">No Expiry</span>`; // Optional: handle null case
                        }
                    },
                    className: 'text-center'
                },
                {
                    data: 'Passwordexpiry',
                    render: function (data) {
                        // Create a date object from the Passwordexpiry
                        const expiryDate = data ? new Date(data) : null;

                        // Check if expiryDate is valid and extract the year
                        const expiryYear = expiryDate ? expiryDate.getFullYear() : null;

                        // If the year is 1900, return an empty string; otherwise, format the date
                        if (expiryYear === 1900) {
                            return '';
                        } else {
                            return expiryDate ? expiryDate.toLocaleDateString('en-US') : '';
                        }
                    },
                    className: 'text-center'

                },
                {                   
                    data: 'Lastaccess',
                    render: function (data) {
                        // Create a date object from the Lastaccess
                        const lastAccessDate = data ? new Date(data) : null;

                        // Check if lastAccessDate is valid and extract the year
                        const lastAccessYear = lastAccessDate ? lastAccessDate.getFullYear() : null;

                        // If the year is 1900, return an empty string; otherwise, format the date and time
                        if (lastAccessYear === 1900) {
                            return '';
                        } else {
                            // Format the date and time
                            const options = {
                                year: 'numeric',
                                month: '2-digit',
                                day: '2-digit',
                                hour: '2-digit',
                                minute: '2-digit',
                                second: '2-digit',
                                fractionalSecondDigits: 3, // Include milliseconds
                                hour12: false // Use 24-hour format
                            };

                            return lastAccessDate.toLocaleString('en-US', options);
                        }
                    },
                    className: 'text-center'
                },
                { data: 'Rolename' },
                { data: 'Encodedbyname' },
                {
                    data: 'Dateencoded',
                    render: function (data) {
                        // Format the date as "MM/DD/yyyy"
                        return data ? new Date(data).toLocaleDateString('en-US') : '';
                    },
                    className: 'text-center'
                }
            ],
            columnDefs: [
                {
                    
                }
            ],
            scrollY: dataTableHeight + 'px', // Set height directly
            scrollCollapse: true, // Collapse when there are few records         
            initComplete: function () {
                // Check if there are more than 10 rows to apply scroll
                const numRows = this.api().rows().count();
                if (numRows > 10) {
                    $(this).css('height', dataTableHeight + 'px');
                    $(this).DataTable().settings()[0].oScroll.sY = dataTableHeight + 'px';
                } else {
                    $(this).css('height', 'auto');
                    $(this).DataTable().settings()[0].oScroll.sY = ''; // Disable scroll if not enough data
                }
            }

        });

        table = dt.$;

        // Re-init functions on every table re-draw -- more info: https://datatables.net/reference/event/draw
        dt.on('draw', function () {

        });

        // Event handler for clearing search input when x is click
        $('[data-kt-user-table-filter="add_user"]').on('click', function () {
            const userno = 0;
            // Now you can use the userId to identify the user and open the modal accordingly
            openAddUserModal(userno);
        });

        // Event handler for opening the "Add User" modal when the button is clicked
        $('#kt_table_user').on('click', '[data-kt-user-table-filter="edit_user"]', function () {
            const userno = $(this).data('userno');
            // Now you can use the userId to identify the user and open the modal accordingly
            openAddUserModal(userno);
        });

        // Event handler for opening the "Add User" modal when the button is clicked
        $('#kt_table_user').on('click', '[data-kt-user-table-filter="view_user_journal"]', function () {
            const detno = $(this).data('detno');
            // Now you can use the userId to identify the user and open the modal accordingly
            openUserJournalModal(detno);
        });

        // Event handler for the "x" button in Search
        $('[data-kt-user-table-search="search"]').on('click', resetSearch);

        // Event handler for the "Reset" button in Filter
        $('[data-kt-user-table-filter="reset"]').on('click', resetFilter);

        // Event handler for the "Reset" button in Filter
        $('[data-kt-add-user-modal-action="cancel"]').on('click', resetAddUpdate);

        $('#kt_table_user').on('click', '[data-kt-user-table-filter="delete_user"]', function () {
            const userno = $(this).data('userno');

            // Proceed with row deletion
            handleRowDeletion(userno);
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