"use strict";

var KTAddUpdate = function () {
    var form;
    var submitButton;
    var validator;
    var modal;
    let CITY_DATA = {};
    let username = 'Spyrix19';
    let userno = 1;

    // Initialize form validation
    var initValidation = async function () {
        validator = FormValidation.formValidation(
            form,
            {
                fields: {
                    'regionDropdown': {
                        validators: {
                            notEmpty: {
                                message: 'Region is required'
                            }
                        }
                    },
                    'provinceDropdown': {
                        validators: {
                            notEmpty: {
                                message: 'Province is required'
                            }
                        }
                    },
                    'kt_city_name': {
                        validators: {
                            notEmpty: {
                                message: 'City is required'
                            }
                        }
                    },
                    'kt_zip_code': {
                        validators: {
                            notEmpty: {
                                message: 'Zip Code is required'
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


    // Fetch user data and populate the form
    var fetchAndUpdateData = async function (cityno) {
        try {

            document.getElementById('kt_cityno').value = '';
            document.getElementById('kt_city_name').value = '';
            document.getElementById('kt_zip_code').value = '';
            document.getElementById('provinceDropdown').value = '';
            document.getElementById('regionDropdown').text = '';

            const response = await fetch(`/city/details?cityno=${cityno}`);
            if (response.ok) {
                const cityData = await response.json();

                CITY_DATA = cityData;

                if (cityData !== null) {

                    $('#kt_cityno').val(cityData.data.Cityno);
                    $('#kt_city_name').val(cityData.data.Cityname);
                    $('#kt_zip_code').val(cityData.data.Zipcode);
                    $('#provinceDropdown').val(cityData.data.Provinceno).trigger('change.select2');
                    $('#regionDropdown').val(cityData.data.Regionno).trigger('change.select2');
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
        const citynoInput = document.getElementById('kt_cityno');
        const citynoValue = citynoInput.value.trim(); // Trim any leading/trailing whitespace

        let dataCityno = citynoValue === '' ? '0' : citynoValue;

        if (dataCityno) {
            // If the attribute exists, call the fetchAndUpdateUserData function with the user number
            if (dataCityno !== '0') {
                fetchAndUpdateData(dataCityno);
            }
        } else {
            console.error('data-cityno attribute is missing in the selected row.');
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
        const cancelButton = document.querySelector('[data-kt-add-city-modal-action="cancel"]');
        if (cancelButton) {
            cancelButton.addEventListener('click', function (event) {
                // Prevent the default behavior that closes the modal
                event.preventDefault();

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
                        $('#kt_modal_add_city').modal('hide'); // Hide modal

                        // Check if the validator instance exists and destroy it
                        if (typeof validator !== 'undefined') {
                            validator.destroy();
                        }
                    }
                });
            });
        }

        // Check if the button clicked is the "Cancel" button
        const closeButton = document.querySelector('[data-kt-add-city-modal-action="close"]');
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
                        $('#kt_modal_add_city').modal('hide'); // Hide modal

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
        const table = $('#kt_table_city').DataTable();
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

                    const addCityUrl = '/city/create';

                    const citynoInput = citynoInput.getElementById('kt_cityno');
                    const citynoValue = detnoInput.value.trim(); // Trim any leading/trailing whitespace

                    const requestBody = JSON.stringify({
                        cityno: citynoValue === '' ? '0' : citynoValue, // Set to '0' if empty
                        zipcode: document.getElementById('kt_zip_code').value,
                        cityname: document.getElementById('kt_city_name').value,
                        provinceno: document.getElementById('provinceDropdown').value,
                        encodedby: userno,
                    });

                    try {
                        const response = await fetch(addCityUrl, {
                            method: 'POST',
                            headers: {
                                'Content-Type': 'application/json',
                                'Accept': 'application/json'
                            },
                            body: requestBody
                        });
                        if (response.ok) {

                            let citynojournal = citynoValue === '' ? '0' : citynoValue;

                            if (citynojournal !== '0') {
                                const addjournalUrl = '/city/journal/create';

                                const journalrequestBody = {
                                    cityno: citynoValue === '' ? '0' : citynoValue, // Set to '0' if empty
                                    encodedby: userno,
                                    journallist: [] // Initialize journallist as an empty array
                                };

                                if (CITY_DATA.data.Cityname !== null) {
                                    if (CITY_DATA.data.Cityname !== document.getElementById('kt_city_name').value) {
                                        journalrequestBody.journallist.push({
                                            'description': username + ' : Changed City from ' + CITY_DATA.data.Cityname + ' to ' + document.getElementById('kt_city_name').value
                                        });
                                    }
                                }

                                if (CITY_DATA.data.Zipcode !== null) {
                                    if (CITY_DATA.data.Zipcode !== document.getElementById('kt_zip_code').value) {
                                        journalrequestBody.journallist.push({
                                            'description': username + ' : Changed Zipcode from ' + CITY_DATA.data.Zipcode + ' to ' + document.getElementById('kt_zip_code').value
                                        });
                                    }
                                }

                                if (CITY_DATA.data.Provinceno !== null || CITY_DATA.data.Provinceno !== '0') {
                                    if (CITY_DATA.data.Provinceno !== document.getElementById('provinceDropdown').value) {

                                        var dropdown2 = document.getElementById('provinceDropdown');

                                        var selectedText2 = dropdown2.options[dropdown2.selectedIndex].text;

                                        journalrequestBody.journallist.push({
                                            'description': username + ' : Changed Province from ' + CITY_DATA.data.Provincename + ' to ' + selectedText2
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
                                    $('#kt_modal_add_city').modal('hide');
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
            form = document.querySelector('#kt_modal_add_city_form');
            submitButton = document.querySelector('#kt_add_city_submit');
            modal = document.getElementById('kt_modal_add_city');

            populateProvinceDropdown(0, 'provinceDropdown');
            populateRegionDropdown('regionDropdown');

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
                    populateProvinceDropdown(0, 'provinceDropdown');
                    populateRegionDropdown('regionDropdown');
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
