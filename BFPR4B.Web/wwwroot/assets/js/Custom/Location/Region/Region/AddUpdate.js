"use strict";

var KTAddUpdate = function () {
    var form;
    var submitButton;
    var validator;
    var modal;
    let REGION_DATA = {};
    let username = 'Spyrix19';
    let userno = 1;

    // Initialize form validation
    var initValidation = async function () {
        validator = FormValidation.formValidation(
            form,
            {
                fields: {
                    'region_code': {
                        validators: {
                            notEmpty: {
                                message: 'Region code is required'
                            }
                        }
                    },
                    'region_name': {
                        validators: {
                            notEmpty: {
                                message: 'Region name is required'
                            }
                        }
                    },
                    'region_type': {
                        validators: {
                            notEmpty: {
                                message: 'Division is required'
                            }
                        }
                    }
                },
                plugins: {
                    trigger: new FormValidation.plugins.Trigger({
                        event: {
                            password: false
                        }
                    }),
                    bootstrap: new FormValidation.plugins.Bootstrap5({
                        rowSelector: '.fv-row',
                        eleInvalidClass: '',  // comment to enable invalid state icons
                        eleValidClass: '' // comment to enable valid state icons
                    })
                }
            }
        );
    };

    async function populateDivisionDropdown(tablename, dropdownId) {
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

    // Fetch user data and populate the form
    var fetchAndUpdateData = async function (regionno) {
        try {

            document.getElementById('kt_regionno').value = '';
            document.getElementById('kt_region_code').value = '';
            document.getElementById('kt_region_name').value = '';
            document.getElementById('kt_region_type').value = '';

            const response = await fetch(`/region/details?regionno=${regionno}`);
            if (response.ok) {

                const regionData = await response.json();

                REGION_DATA = regionData;

                if (regionData !== null) {

                    $('#kt_regionno').val(regionData.data.Regionno);
                    $('#kt_region_code').val(regionData.data.Regioncode);
                    $('#kt_region_name').val(regionData.data.Regionname);
                    $('#kt_region_type').val(regionData.data.Divisionno).trigger('change.select2');
                }
            } else {
                console.error('Failed to fetch user data:', response.status, response.statusText);
            }
        } catch (error) {
            console.error('An error occurred:', error);
        }
    };

    //Function to get data-userno attribute value from the selected row in DataTable and fetch user data
    async function FetchData() {
        const regionnoInput = document.getElementById('kt_regionno');
        const regionnoValue = regionnoInput.value.trim(); // Trim any leading/trailing whitespace

        let dataregionno= regionnoValue === '' ? '0' : regionnoValue;

        if (dataregionno) {
            // If the attribute exists, call the fetchAndUpdateUserData function with the user number
            if (dataregionno !== '0') {
                fetchAndUpdateData(dataregionno);
            }
        } else {
            console.error('data-regionno attribute is missing in the selected row.');
        }
    }

    // Function to reset the filter values of Filters
    async function resetAddUpdate() {
        // Get all the select elements within the modal
        const selectElements = document.querySelectorAll('.modal-dialog [data-kt-select2]');
        const inputElements = document.querySelectorAll('.modal-dialog [type="text"]');


        // Remove validation icons
        const validationIcons = document.querySelectorAll('.modal-dialog .fv-plugins-icon');
        validationIcons.forEach((icon) => {
            // Remove the icon element
            icon.parentElement.removeChild(icon);
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

        // Check if the button clicked is the "Cancel" button
        const cancelButton = document.querySelector('[data-kt-add-region-modal-action="cancel"]');
        if (cancelButton) {
            cancelButton.addEventListener('click', function (event) {
                Swal.fire({
                    text: "Are you sure you would like to cancel?",
                    icon: "warning",
                    showCancelButton: true,
                    buttonsStyling: false,
                    confirmButtonText: "Yes, cancel it!",
                    cancelButtonText: "No, return",
                    customClass: {
                        confirmButton: "btn btn-primary",
                        cancelButton: "btn btn-active-light"
                    }
                }).then(function (result) {
                    if (result.value) {
                        form.reset(); // Reset form	
                        $('#kt_modal_add_region').modal('hide'); // Hide modal

                        // Check if the validator instance exists and destroy it
                        if (typeof validator !== 'undefined') {
                            validator.destroy();
                        }
                    }
                });
            });
        }

        // Check if the button clicked is the "Cancel" button
        const closeButton = document.querySelector('[data-kt-add-region-modal-action="close"]');
        if (closeButton) {
            closeButton.addEventListener('click', function (event) {
                Swal.fire({
                    text: "Are you sure you would like to cancel?",
                    icon: "warning",
                    showCancelButton: true,
                    buttonsStyling: false,
                    confirmButtonText: "Yes, cancel it!",
                    cancelButtonText: "No, return",
                    customClass: {
                        confirmButton: "btn btn-primary",
                        cancelButton: "btn btn-active-light"
                    }
                }).then(function (result) {
                    if (result.value) {
                        form.reset(); // Reset form	
                        $('#kt_modal_add_region').modal('hide'); // Hide modal

                        // Check if the validator instance exists and destroy it
                        if (typeof validator !== 'undefined') {
                            validator.destroy();
                        }
                    }
                });
            });
        }

        initValidation();
    }

    // Function to update the DataTable with new data
    var updateDataTable = async function () {
        const table = $('#kt_table_region').DataTable();
        table.ajax.reload(); // Reload the DataTable to fetch updated data
    };

    var handleFormSubmit = async function (e) {
        const status = await validator.validate();
        if (status === 'Valid') {

            Swal.fire({
                text: "Are you sure you want to submit this data?",
                icon: "question",
                showCancelButton: true,
                buttonsStyling: false,
                confirmButtonText: "Yes, submit it!",
                cancelButtonText: "No, cancel",
                customClass: {
                    confirmButton: "btn btn-primary",
                    cancelButton: "btn btn-active-light"
                }
            }).then(async function (result) {
                if (result.value) {
                    submitButton.setAttribute('data-kt-indicator', 'on');
                    submitButton.disabled = true;

                    const addregionUrl = '/region/create';

                    const regionnoInput = document.getElementById('kt_regionno');
                    const regionnoValue = regionnoInput.value.trim(); // Trim any leading/trailing whitespace

                    const requestBody = JSON.stringify({
                        regionno: regionnoValue === '' ? '0' : regionnoValue, // Set to '0' if empty
                        regioncode: document.getElementById('kt_region_code').value,
                        regionname: document.getElementById('kt_region_name').value,
                        divisionno: document.getElementById('kt_region_type').value,
                        encodedby: userno,
                    });

                    try {

                        const response = await fetch(addregionUrl, {
                            method: 'POST',
                            headers: {
                                'Content-Type': 'application/json',
                                'Accept': 'application/json'
                            },
                            body: requestBody
                        });


                        if (response.ok) {


                            let regionnojournal = regionnoValue === '' ? '0' : regionnoValue;

                            if (regionnojournal !== '0') {
                                const addjournalUrl = '/region/journal/create';

                                const journalrequestBody = {
                                    regionno: regionnoValue === '' ? '0' : regionnoValue, // Set to '0' if empty
                                    encodedby: userno,
                                    journallist: [] // Initialize journallist as an empty array
                                };

                                if (REGION_DATA.data.Regioncode !== null) {
                                    if (REGION_DATA.data.Regioncode !== document.getElementById('kt_region_code').value) {
                                        journalrequestBody.journallist.push({
                                            'description': username + ' : Changed Region Code from ' + REGION_DATA.data.Regioncode + ' to ' + document.getElementById('kt_region_code').value
                                        });
                                    }
                                }

                                if (REGION_DATA.data.Regionname !== null) {
                                    if (REGION_DATA.data.Regionname !== document.getElementById('kt_region_name').value) {
                                        journalrequestBody.journallist.push({
                                            'description': username + ' : Changed Region Name from ' + REGION_DATA.data.Regionname + ' to ' + document.getElementById('kt_region_name').value
                                        });
                                    }
                                }

                                if (REGION_DATA.data.Divisionno !== null || REGION_DATA.data.Divisionno !== '0') {
                                    if (REGION_DATA.data.Divisionno !== document.getElementById('kt_region_type').value) {

                                        var dropdown2 = document.getElementById('kt_region_type');

                                        var selectedText2 = dropdown2.options[dropdown2.selectedIndex].text;

                                        journalrequestBody.journallist.push({
                                            'description': username + ' : Changed Division from ' + REGION_DATA.data.Divisionname + ' to ' + selectedText2
                                        });
                                    }
                                }

                                try {
                                    const response = await fetch(addjournalUrl, {
                                        method: 'POST',
                                        headers: {
                                            'Content-Type': 'application/json',
                                            'Accept': 'application/json'
                                        },
                                        body: JSON.stringify(journalrequestBody)
                                    });
                                } catch (error) {
                                    Swal.fire({
                                        text: error.message || 'An error occurred',
                                        icon: 'error',
                                        buttonsStyling: false,
                                        confirmButtonText: 'Ok, got it!',
                                        customClass: {
                                            confirmButton: 'btn btn-primary'
                                        }
                                    });
                                }
                            }

                            form.reset();
                            Swal.fire({
                                text: "Data has successfully been submitted!",
                                icon: "success",
                                buttonsStyling: false,
                                confirmButtonText: "Ok, got it!",
                                customClass: {
                                    confirmButton: "btn btn-primary"
                                }
                            }).then((result) => {
                                if (result.isConfirmed) {
                                    $('#kt_modal_add_region').modal('hide'); // Close the modal
                                    updateDataTable();
                                }
                            });
                        } else {
                            const errorData = await response.json();
                            Swal.fire({
                                text: errorData.errorMessages || 'An error occurred',
                                icon: 'error',
                                buttonsStyling: false,
                                confirmButtonText: 'Ok, got it!',
                                customClass: {
                                    confirmButton: 'btn btn-primary'
                                }
                            });
                        }
                    } catch (error) {
                        Swal.fire({
                            text: error.message || 'An error occurred',
                            icon: 'error',
                            buttonsStyling: false,
                            confirmButtonText: 'Ok, got it!',
                            customClass: {
                                confirmButton: 'btn btn-primary'
                            }
                        });
                    } finally {
                        submitButton.removeAttribute('data-kt-indicator');
                        submitButton.disabled = false;
                    }
                }
            });

        } else {
            Swal.fire({
                text: 'Please fill in all required fields.',
                icon: 'error',
                buttonsStyling: false,
                confirmButtonText: 'Ok, got it!',
                customClass: {
                    confirmButton: 'btn btn-primary'
                }
            });
        }

    };


    return {
        init: function () {
            form = document.querySelector('#kt_modal_add_region_form');
            submitButton = document.querySelector('#kt_add_region_submit');
            modal = document.getElementById('kt_modal_add_region');

            populateDivisionDropdown('DIVISION', 'kt_region_type');

            // Only initialize validation when the form is submitted
            if ((form || submitButton) && modal) {
                form.addEventListener('submit', function (e) {
                    e.preventDefault(); // Prevent the default form submission
                    if (!validator) {
                        // Initialize validation if it's not already initialized
                        initValidation();
                    }
                    validator.validate().then(function (status) {
                        if (status === 'Valid') {
                            // Form is valid; you can submit the form or perform other actions
                            handleFormSubmit();
                        }
                    });
                });
            }

            // Listen for Bootstrap modal shown event
            if (modal) {
                $(modal).on('shown.bs.modal', function () {
                    // Get selected user data when the modal is shown
                    resetAddUpdate();
                    FetchData();
                });
            }
        }
    };
}();

// Initialize the KTUsersAddUser module when the DOM is ready
document.addEventListener('DOMContentLoaded', function () {
    KTAddUpdate.init();
});
