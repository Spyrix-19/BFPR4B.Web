"use strict";

var KTAddUpdate = function () {
    var form;
    var submitButton;
    var validator;
    var modal;
    let STATION_DATA = {};
    let username = 'Spyrix19';
    let userno = 1;

    console.log('validate');

    // Initialize form validation
    var initValidation = async function () {
        validator = FormValidation.formValidation(
            form,
            {
                fields: {
                    'station_code': {
                        validators: {
                            notEmpty: {
                                message: 'Station Code is required'
                            }
                        }
                    },
                    'station_name': {
                        validators: {
                            notEmpty: {
                                message: 'Station Name is required'
                            }
                        }
                    },
                    'station_type': {
                        validators: {
                            notEmpty: {
                                message: 'Station Type is required',
                            }
                        }
                    },
                    'area_assign': {
                        validators: {
                            notEmpty: {
                                message: 'Area of Assignment is required'
                            }
                        }
                    },
                    'street_address': {
                        validators: {
                            notEmpty: {
                                message: 'Street Address is required'
                            }
                        }
                    },
                    'city': {
                        validators: {
                            notEmpty: {
                                message: 'City is required'
                            },
                        }
                    },
                    'province': {
                        validators: {
                            notEmpty: {
                                message: 'Province is required'
                            },
                        }
                    },
                    'latitude': {
                        validators: {
                            notEmpty: {
                                message: 'Latitude is required'
                            }
                        }
                    },
                    'longitude': {
                        validators: {
                            notEmpty: {
                                message: 'Longitude is required'
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

    async function populateLocationDropdown(locationtype, parentcode, dropdownId) {
        try {
            const loginUrl = new URL('system/location', window.location.origin);
            loginUrl.searchParams.append('locationtype', locationtype);
            loginUrl.searchParams.append('parentcode', parentcode);

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
    var fetchAndUpdateData = async function (stationno) {
        try {

            document.getElementById('kt_stationno').value = '';
            document.getElementById('kt_station_code').value = '';
            document.getElementById('kt_station_name').value = '';
            document.getElementById('kt_station_type').value = '';
            document.getElementById('kt_area_assign').text = '';
            document.getElementById('kt_street_address').text = '';
            document.getElementById('kt_city').text = '';
            document.getElementById('kt_province').text = '';
            document.getElementById('kt_latitude').text = '';
            document.getElementById('kt_longitude').text = '';

            const responses = await fetch(`/station/details?stationno=${stationno}`);
            if (responses.ok) {
                const stationData = await responses.json();

                STATION_DATA = stationData;

                if (stationData !== null) {

                    $('#kt_stationno').val(stationData.data.Stationno);
                    $('#kt_station_code').val(stationData.data.Unitcode);
                    $('#kt_station_name').val(stationData.data.Stationname);
                    $('#kt_station_type').val(stationData.data.Stationtype).trigger('change.select2');
                    $('#kt_area_assign').val(stationData.data.Areaassign).trigger('change.select2');
                    $('#kt_street_address').val(stationData.data.Streetaddress);
                    $('#kt_city').val(stationData.data.Cityno).trigger('change.select2');
                    $('#kt_province').val(stationData.data.Provinceno).trigger('change.select2');
                    $('#kt_latitude').val(stationData.data.Latitude);
                    $('#kt_longitude').val(stationData.data.Longitude);
                }
            } else {
                console.error('Failed to fetch user data:', responses.status, responses.statusText);
            }
        } catch (error) {
            console.error('An error occurred:', error);
        }
    };

    //Function to get data-userno attribute value from the selected row in DataTable and fetch user data
    async function FetchData() {
        //const dataStationno = document.getElementById('kt_stationno').value;

        const stationnoInput = document.getElementById('kt_stationno');
        const stationnoValue = stationnoInput.value.trim(); // Trim any leading/trailing whitespace

        let dataStationno = stationnoValue === '' ? '0' : stationnoValue;

        if (dataStationno) {
            // If the attribute exists, call the fetchAndUpdateUserData function with the user number
            if (dataStationno !== '0') {
                fetchAndUpdateData(dataStationno);
            }
        } else {
            console.error('data-stationno attribute is missing in the selected row.');
        }
    }

    // Function to reset the filter values of Filters
    async function resetAddUpdate() {
        // Get all the select elements within the modal
        const selectElements = document.querySelectorAll('.modal-dialog [data-kt-select2]');
        const selectElements2 = document.querySelectorAll('.modal-dialog [data-control="select2"]');
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

        // Reset the values of the select elements to their default (empty) state
        selectElements2.forEach((select) => {
            select.value = '';
            const event = new Event('change', { bubbles: true });
            select.dispatchEvent(event);
        });

        //// Reset the values of select elements with the data-kt-select2 attribute
        //selectElements2.forEach((select) => {
        //    // Assuming you are using Select2 library, you can trigger a change event
        //    // to reset the Select2 select element to its default state
        //    $(select).val(0).trigger('change');

        //    // Clear validation error for the corresponding field using FormValidation
        //    const fieldName = select.getAttribute('name');
        //    if (fieldName && validator) {
        //        validator.updateFieldStatus(fieldName, 'NotValidated');
        //        validator.updateFieldStatus(fieldName, 'Valid');
        //    }
        //});

        // Reset the values of text input fields to empty
        inputElements.forEach((input) => {
            input.value = '';
        });

        // Check if the button clicked is the "Cancel" button
        const cancelButton = document.querySelector('[data-kt-add-station-modal-action="cancel"]');
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
                        $('#kt_modal_add_station').modal('hide'); // Hide modal

                        // Check if the validator instance exists and destroy it
                        if (typeof validator !== 'undefined') {
                            validator.destroy();
                        }
                    }
                });
            });
        }

        // Check if the button clicked is the "Cancel" button
        const closeButton = document.querySelector('[data-kt-add-station-modal-action="close"]');
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
                        $('#kt_modal_add_station').modal('hide'); // Hide modal

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
        const table = $('#kt_table_station').DataTable();
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

                    const addtrainingUrl = '/station/create';

                    const stationnoInput = document.getElementById('kt_stationno');
                    const stationnoValue = stationnoInput.value.trim(); // Trim any leading/trailing whitespace

                    const requestBody = JSON.stringify({
                        stationno: stationnoValue === '' ? '0' : stationnoValue, // Set to '0' if empty
                        unitcode: document.getElementById('kt_station_code').value,
                        stationname: document.getElementById('kt_station_name').value,
                        stationtype: document.getElementById('kt_station_type').value,
                        areaassign: document.getElementById('kt_area_assign').value,
                        streetaddress: document.getElementById('kt_street_address').value,
                        cityno: document.getElementById('kt_city').value,
                        provinceno: document.getElementById('kt_province').value,
                        latitude: document.getElementById('kt_latitude').value,
                        longitude: document.getElementById('kt_longitude').value,
                        encodedby: userno,
                    });

                    try {
                        const response = await fetch(addtrainingUrl, {
                            method: 'POST',
                            headers: {
                                'Content-Type': 'application/json',
                                'Accept': 'application/json'
                            },
                            body: requestBody
                        });
                        if (response.ok) {

                            let stationnojournal = stationnoValue === '' ? '0' : stationnoValue;

                            if (stationnojournal !== '0') {
                                const addjournalUrl = '/station/journal/create';

                                const journalrequestBody = {
                                    stationno: stationnoValue === '' ? '0' : stationnoValue, // Set to '0' if empty
                                    encodedby: userno,
                                    journallist: [] // Initialize journallist as an empty array
                                };

                                if (STATION_DATA.data.Unitcode !== null) {
                                    if (STATION_DATA.data.Unitcode !== document.getElementById('kt_station_code').value) {
                                        journalrequestBody.journallist.push({
                                            'description': username + ' : Changed Unit Code from ' + STATION_DATA.data.Unitcode + ' to ' + document.getElementById('kt_station_code').value
                                        });
                                    }
                                }

                                if (STATION_DATA.data.Stationname !== null) {
                                    if (STATION_DATA.data.Stationname !== document.getElementById('kt_station_name').value) {
                                        journalrequestBody.journallist.push({
                                            'description': username + ' : Changed Station Name from ' + STATION_DATA.data.Stationname + ' to ' + document.getElementById('kt_station_name').value
                                        });
                                    }
                                }

                                if (STATION_DATA.data.Stationtype !== null || STATION_DATA.data.Stationtype !== '0') {
                                    if (STATION_DATA.data.Stationtype !== document.getElementById('kt_station_type').value) {

                                        var dropdown2 = document.getElementById('kt_station_type');

                                        var selectedText2 = dropdown2.options[dropdown2.selectedIndex].text;

                                        journalrequestBody.journallist.push({
                                            'description': username + ' : Changed Station Type from ' + STATION_DATA.data.Stationtypename + ' to ' + selectedText2
                                        });
                                    }
                                }

                                if (STATION_DATA.data.Areaassign !== null || STATION_DATA.data.Areaassign !== '0') {
                                    if (STATION_DATA.data.Areaassign !== document.getElementById('kt_area_assign').value) {

                                        var dropdown2 = document.getElementById('kt_area_assign');

                                        var selectedText2 = dropdown2.options[dropdown2.selectedIndex].text;

                                        journalrequestBody.journallist.push({
                                            'description': username + ' : Changed Area of Assignment from ' + STATION_DATA.data.Areaassignname + ' to ' + selectedText2
                                        });
                                    }
                                }                                

                                if (STATION_DATA.data.Streetaddress !== null) {
                                    if (STATION_DATA.data.Streetaddress !== document.getElementById('kt_street_address').value) {
                                        journalrequestBody.journallist.push({
                                            'description': username + ' : Changed Street address from ' + STATION_DATA.data.Streetaddress + ' to ' + document.getElementById('kt_street_address').value
                                        });
                                    }
                                }

                                if (STATION_DATA.data.Cityno !== null || STATION_DATA.data.Cityno !== '0') {
                                    if (STATION_DATA.data.Cityno !== document.getElementById('kt_city').value) {

                                        var dropdown2 = document.getElementById('kt_city');

                                        var selectedText2 = dropdown2.options[dropdown2.selectedIndex].text;

                                        journalrequestBody.journallist.push({
                                            'description': username + ' : Changed City from ' + STATION_DATA.data.Cityname + ' to ' + selectedText2
                                        });
                                    }
                                }

                                if (STATION_DATA.data.Provinceno !== null || STATION_DATA.data.Provinceno !== '0') {
                                    if (STATION_DATA.data.Provinceno !== document.getElementById('kt_province').value) {

                                        var dropdown2 = document.getElementById('kt_province');

                                        var selectedText2 = dropdown2.options[dropdown2.selectedIndex].text;

                                        journalrequestBody.journallist.push({
                                            'description': username + ' : Changed Province from ' + STATION_DATA.data.Provincename + ' to ' + selectedText2
                                        });
                                    }
                                }

                                if (STATION_DATA.data.Latitude !== null) {
                                    if (STATION_DATA.data.Latitude !== document.getElementById('kt_latitude').value) {
                                        journalrequestBody.journallist.push({
                                            'description': username + ' : Changed Latitude from ' + STATION_DATA.data.Latitude + ' to ' + document.getElementById('kt_latitude').value
                                        });
                                    }
                                }

                                if (STATION_DATA.data.Longitude !== null) {
                                    if (STATION_DATA.data.Longitude !== document.getElementById('kt_longitude').value) {
                                        journalrequestBody.journallist.push({
                                            'description': username + ' : Changed Longitude from ' + STATION_DATA.data.Longitude + ' to ' + document.getElementById('kt_longitude').value
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
                                    $('#kt_modal_add_station').modal('hide'); // Close the modal
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
            form = document.querySelector('#kt_modal_add_station_form');
            submitButton = document.querySelector('#kt_add_station_submit');
            modal = document.getElementById('kt_modal_add_station');

            populateLocationDropdown('PROVINCES', 0, 'kt_province');
            populateLocationDropdown('CITIES', 0, 'kt_city');
            populateDropdown('STATION TYPE', 'kt_station_type');
            populateDropdown('AREA OF ASSIGNMENT', 'kt_area_assign');

            //// Initialize validation and other functionality
            //if (form && submitButton) {
            //    initValidation();
            //    form.addEventListener('submit', handleFormSubmit);

            //    // Listen for Bootstrap modal shown event
            //    $(modal).on('shown.bs.modal', function () {
            //        // Get selected user data when the modal is shown
            //        resetAddUpdate();
            //        FetchData();
            //    });
            //}

            // Initialize other functionality

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
