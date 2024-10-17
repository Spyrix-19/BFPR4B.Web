
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
        const resetButton = document.querySelector('[data-kt-member-table-filter="reset"]');
        if (resetButton) {
            resetButton.addEventListener('click', function (event) {
                // Prevent the default behavior that closes the menu
                event.preventDefault();
            });
        }

        const table = $('#kt_table_member').DataTable();
        table.ajax.reload(); // Reload the DataTable to fetch updated data
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
        const cancelButton = document.querySelector('[data-kt-add-member-modal-action="cancel"]');
        if (cancelButton) {
            cancelButton.addEventListener('click', function (event) {
                // Prevent the default behavior that closes the modal
                event.preventDefault();
            });
        }
    }

    // Function to open the "Add User" modal
    async function openAddMemberModal(memberno) {
        // Here, you can use the userno to customize the modal content or perform other actions as needed

        // Change the modal title here
        var modalTitle = document.getElementById("kt_add_member_title");
        modalTitle.innerText = memberno === 0 ? "Add Member" : "Update Member"; // Change "New Title" to your desired text

        $('#kt_member_memberno').val(memberno);

        $('#kt_modal_add_member').modal('show');


    }

    // Function to open the "Add Rank" modal
    async function openMemberDetailModal(memberno) {
        // Here, you can use the userno to customize the modal content or perform other actions as needed

        $('#kt_member_detail_memberno').val(memberno);

        $('#kt_modal_member_detail').modal('show');

    }

    // Function to update the DataTable with new data
    var updateDataTable = async function () {
        const table = $('#kt_table_member').DataTable();
        table.ajax.reload(); // Reload the DataTable to fetch updated data
    };

    async function handleRowDeletion(memberno) {
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
                const response = await fetch(`/member/remove?memberno=${memberno}`, {
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
                        text: "Failed to delete member data.",
                        icon: "error",
                        buttonsStyling: false,
                        confirmButtonText: "Ok, got it!",
                        customClass: {
                            confirmButton: "btn fw-bold btn-primary",
                        }
                    });
                    console.error('Failed to delete member data:', response.status, response.statusText);
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

        //// Calculate height directly within the DataTable initialization
        //const windowHeight = $(window).height();
        //const headerHeight = $('.header-class').outerHeight() || 0; // Replace with your header's actual class
        //const footerHeight = $('.footer-class').outerHeight() || 0; // Replace with your footer's actual class
        //const dataTableHeight = Math.max(windowHeight - headerHeight - footerHeight - 25, windowHeight * 0.5); // Adjust for any extra spacing

        dt = $("#kt_table_member").DataTable({
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
                url: "/member/ledger",
                type: "GET",
                data: function (d) {
                    // Use the DataTables `ajax.data` option to customize the data sent in the request
                    d.rankno = $("#rankDropdown").val();
                    d.areaassign = $("#areaassignDropdown").val();
                    d.dutystatus = $("#dutystatusDropdown").val();
                    d.appstatus = $("#appstatusDropdown").val();
                    d.gender = $("#genderDropdown").val();
                    d.province = $("#provinceDropdown").val();
                    d.searchkey = $("#searchInput").val();
                },
                complete: function () {
                    // Reset to page 1 whenever a new set of data is loaded
                    dt.page(0).draw(false);
                }
            },
            columns: [
                {
                    data: 'Memberno',
                    render: function (data) {
                        return `
                                <div class="btn-group">
                                    <a href="#" class="btn btn-primary btn-sm dropdown-toggle" data-bs-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
                                        <i class="fa fa-cog"></i> Option <span class="caret"></span>
                                    </a>
                                    <div class="dropdown-menu">                                        
                                        <div class="dropdown-item px-3">
                                            <a style="cursor: pointer;" class="menu-link px-3" data-kt-member-table-filter="view_member_detail" data-memberno="${data}">
                                                View Member Detail
                                            </a>
                                        </div>
                                    </div>
                                </div>
                                <a class="btn btn-sm btn-primary btn-icon btn-icon-md" data-kt-member-table-filter="edit_member" data-toggle="tooltip" data-placement="top" title="Change" data-memberno="${data}">
                                    <i class="la la-edit"></i>
                                </a>
                                <a class="btn btn-sm btn-danger btn-icon btn-icon-md" data-kt-member-table-filter="delete_member" data-toggle="tooltip" data-placement="top" title="Delete" data-memberno="${data}">
                                     <i class="bi bi-trash3"></i>
                                </a>`;
                    },
                    className: 'text-center'
                },
                //{
                //    data: 'Picture', // The field containing the byte array
                //    render: function (data, type, row) {
                //        // Check if the PictureType value is valid
                //        const pictureType = row.Picturetype ? row.Picturetype.replace('.', '') : ''; // Remove the dot for the MIME type

                //        // Determine the correct MIME type
                //        const mimeType = pictureType.toLowerCase() === 'png' ? 'image/png' :
                //            pictureType.toLowerCase() === 'jpg' || pictureType.toLowerCase() === 'jpeg' ? 'image/jpeg' :
                //                pictureType.toLowerCase() === 'gif' ? 'image/gif' : '';

                //        // HTML structure for the cell
                //        let cellContent = '';

                //        // Check if data is defined and has length
                //        if (data && data.length > 0) {
                //            // Convert byte array to Base64
                //            const byteArray = new Uint8Array(data); // Ensure data is in Uint8Array
                //            let binaryString = '';
                //            for (let i = 0; i < byteArray.length; i++) {
                //                binaryString += String.fromCharCode(byteArray[i]); // Convert byte to binary string
                //            }
                //            const base64String = btoa(binaryString); // Convert to Base64

                //            // Create the image source
                //            const imgSrc = `data:${mimeType};base64,${base64String}`;

                //            // Construct the HTML for the image and user details
                //            cellContent = `
                //                <div style="display: flex; align-items: center;">
                //                    <img src="${imgSrc}" alt="User Picture" style="width:50px; height:50px; margin-right: 10px;" />
                //                    <div style="display: flex; flex-direction: column;">
                //                        <span style="text-transform: uppercase; font-weight: bold;">${row.Fullname}</span>
                //                        <span>${row.Accountnumber}</span>
                //                    </div>
                //                </div>
                //            `;
                //        } else {
                //            // Return the default image path if no picture data is available
                //            const defaultImagePath = '/assets/media/logos/mimaropa.png'; // Ensure the path is correct
                //            cellContent = `
                //                <div style="display: flex; align-items: center;">
                //                    <img src="${defaultImagePath}" alt="Default Image" style="width:50px; height:50px; margin-right: 10px;" />
                //                    <div style="display: flex; flex-direction: column;">
                //                        <span style="text-transform: uppercase; font-weight: bold;">${row.Fullname}</span>
                //                        <span>${row.Accountnumber}</span>
                //                    </div>
                //                </div>
                //            `;
                //        }

                //        return cellContent; // Return the constructed HTML content
                //    },
                //    className: 'text-left'
                //},
                {
                    data: 'Badgeno',
                    className: 'text-center'
                },
                {
                    data: 'Itemno',
                    className: 'text-center'
                },
                {
                    data: 'Fullname'
                },
                {
                    data: 'Unitassignname'
                },
                {
                    data: 'Areaassignname'
                },
                {
                    data: 'Designation'
                },
                {
                    data: 'Gender',
                    className: 'text-center'
                },
                {
                    data: 'Birthday',
                    render: function (data) {
                        const bdateDate = data ? new Date(data) : null;

                        // Check if expiryDate is valid and extract the year
                        const bdayYear = bdateDate ? bdateDate.getFullYear() : null;

                        // If the year is 1900, return an empty string; otherwise, format the date
                        if (bdayYear === 1900) {
                            return '';
                        } else {
                            return bdateDate ? bdateDate.toLocaleDateString('en-US') : '';
                        }
                    },
                    className: 'text-center'

                },
                {
                    data: 'Age',                    
                    className: 'text-center'
                },
                { data: 'Civilstatusname' },
                { data: 'Dutystatusname' },
                { data: 'Appstatusname' },
                { data: 'Lengthofservice' },
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
            //scrollY: dataTableHeight + 'px', // Set height directly
            //scrollCollapse: true, // Collapse when there are few records         
            //initComplete: function () {
            //    // Check if there are more than 10 rows to apply scroll
            //    const numRows = this.api().rows().count();
            //    if (numRows > 10) {
            //        $(this).css('height', dataTableHeight + 'px');
            //        $(this).DataTable().settings()[0].oScroll.sY = dataTableHeight + 'px';
            //    } else {
            //        $(this).css('height', 'auto');
            //        $(this).DataTable().settings()[0].oScroll.sY = ''; // Disable scroll if not enough data
            //    }
            //}

        });

        table = dt.$;

        // Re-init functions on every table re-draw -- more info: https://datatables.net/reference/event/draw
        dt.on('draw', function () {

        });

        // Event handler for clearing search input when x is click
        $('[data-kt-member-table-filter="add_member"]').on('click', function () {
            const memberno = 0;
            // Now you can use the userId to identify the user and open the modal accordingly
            openAddMemberModal(memberno);
        });

        // Event handler for opening the "Add User" modal when the button is clicked
        $('#kt_table_member').on('click', '[data-kt-member-table-filter="edit_member"]', function () {
            const memberno = $(this).data('memberno');
            // Now you can use the userId to identify the user and open the modal accordingly
            openAddMemberModal(memberno);
        });

        // Event handler for opening the "Add User" modal when the button is clicked
        $('#kt_table_member').on('click', '[data-kt-member-table-filter="view_member_detail"]', function () {
            const memberno = $(this).data('memberno');
            // Now you can use the userId to identify the user and open the modal accordingly
            openMemberDetailModal(memberno);
        });

        // Event handler for the "x" button in Search
        $('[data-kt-member-table-search="search"]').on('click', resetSearch);

        // Event handler for the "Reset" button in Filter
        $('[data-kt-member-table-filter="reset"]').on('click', resetFilter);

        // Event handler for the "Reset" button in Filter
        $('[data-kt-add-member-modal-action="cancel"]').on('click', resetAddUpdate);

        $('#kt_table_member').on('click', '[data-kt-member-table-filter="delete_member"]', function () {
            const memberno = $(this).data('memberno');

            // Proceed with row deletion
            handleRowDeletion(memberno);
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