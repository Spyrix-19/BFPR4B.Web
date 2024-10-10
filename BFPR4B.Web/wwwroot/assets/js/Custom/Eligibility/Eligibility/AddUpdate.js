"use strict";

var KTAddUpdate = function () {
    var form;
    var submitButton;
    var validator;
    var modal;
    let ELIGIBILITY_DATA = {};
    let username = 'Spyrix19';
    let userno = 1;

    // Initialize form validation
    var initValidation = async function () {
        validator = FormValidation.formValidation(
            form,
            {
                fields: {
                    'eligibility_code': {
                        validators: {
                            notEmpty: {
                                message: 'Eligibility code is required'
                            }
                        }
                    },
                    'eligibility_name': {
                        validators: {
                            notEmpty: {
                                message: 'Eligibility name is required'
                            }
                        }
                    },
                    'eligibility_type': {
                        validators: {
                            notEmpty: {
                                message: 'Eligibility level is required'
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

    // Fetch user data and populate the form
    var fetchAndUpdateData = async function (detno) {
        try {

            document.getElementById('kt_eligibility_detno').value = '';
            document.getElementById('kt_eligibility_code').value = '';
            document.getElementById('kt_eligibility_name').value = '';
            document.getElementById('kt_eligibility_type').value = '';
            document.getElementById('kt_eligibility_type').text = '';

            const response = await fetch(`/eligibility/details?detno=${detno}`);
            if (response.ok) {
                const eligibilityData = await response.json();

                ELIGIBILITY_DATA = eligibilityData;

                if (eligibilityData !== null) {

                    $('#kt_eligibility_detno').val(eligibilityData.data.Detno);
                    $('#kt_eligibility_code').val(eligibilityData.data.Recordcode);
                    $('#kt_eligibility_name').val(eligibilityData.data.Description);
                    $('#kt_eligibility_type').val(eligibilityData.data.Parentcode).trigger('change.select2');
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
        //const dataDetno = document.getElementById('kt_eligibility_detno').value;

        //if (dataDetno) {
        //    // If the attribute exists, call the fetchAndUpdateUserData function with the user number
        //    fetchAndUpdateData(dataDetno);
        //} else {
        //    console.error('data-detno attribute is missing in the selected row.');
        //}

        const detnoInput = document.getElementById('kt_eligibility_detno');
        const detnoValue = detnoInput.value.trim(); // Trim any leading/trailing whitespace

        let dataDetno = detnoValue === '' ? '0' : detnoValue;

        if (dataDetno) {
            // If the attribute exists, call the fetchAndUpdateUserData function with the user number
            if (dataDetno !== '0') {
                fetchAndUpdateData(dataDetno);
            }
        } else {
            console.error('data-detno attribute is missing in the selected row.');
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
        const cancelButton = document.querySelector('[data-kt-add-eligibility-modal-action="cancel"]');
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
                        $('#kt_modal_add_eligibility').modal('hide'); // Hide modal

                        // Check if the validator instance exists and destroy it
                        if (typeof validator !== 'undefined') {
                            validator.destroy();
                        }
                    }
                });
            });
        }

        // Check if the button clicked is the "Cancel" button
        const closeButton = document.querySelector('[data-kt-add-eligibility-modal-action="close"]');
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
                        $('#kt_modal_add_eligibility').modal('hide'); // Hide modal

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
        const table = $('#kt_table_eligibility').DataTable();
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

                    const addeligibilityUrl = '/eligibility/create';

                    const detnoInput = document.getElementById('kt_eligibility_detno');
                    const detnoValue = detnoInput.value.trim(); // Trim any leading/trailing whitespace

                    const requestBody = JSON.stringify({
                        detno: detnoValue === '' ? '0' : detnoValue, // Set to '0' if empty
                        recordcode: document.getElementById('kt_eligibility_code').value,
                        description: document.getElementById('kt_eligibility_name').value,
                        parentcode: document.getElementById('kt_eligibility_type').value,
                        encodedby: userno,
                    });

                    try {
                        const response = await fetch(addeligibilityUrl, {
                            method: 'POST',
                            headers: {
                                'Content-Type': 'application/json',
                                'Accept': 'application/json'
                            },
                            body: requestBody
                        });
                        if (response.ok) {
                            let detnojournal = detnoValue === '' ? '0' : detnoValue;

                            if (detnojournal !== '0') {
                                const addjournalUrl = '/eligibility/journal/create';

                                const journalrequestBody = {
                                    gendetno: detnoValue === '' ? '0' : detnoValue, // Set to '0' if empty
                                    tablename: 'ELIGIBILITY',
                                    encodedby: userno,
                                    journallist: [] // Initialize journallist as an empty array
                                };

                                if (ELIGIBILITY_DATA.data.Recordcode !== null) {
                                    if (ELIGIBILITY_DATA.data.Recordcode !== document.getElementById('kt_eligibility_code').value) {
                                        journalrequestBody.journallist.push({
                                            'description': username + ' : Changed Eligibility Code from ' + ELIGIBILITY_DATA.data.Recordcode + ' to ' + document.getElementById('kt_eligibility_code').value
                                        });
                                    }
                                }

                                if (ELIGIBILITY_DATA.data.Description !== null) {
                                    if (ELIGIBILITY_DATA.data.Description !== document.getElementById('kt_eligibility_name').value) {
                                        journalrequestBody.journallist.push({
                                            'description': username + ' : Changed Eligibility Name from ' + ELIGIBILITY_DATA.data.Description + ' to ' + document.getElementById('kt_eligibility_name').value
                                        });
                                    }
                                }

                                if (ELIGIBILITY_DATA.data.Parentcode !== null || ELIGIBILITY_DATA.data.Parentcode !== '0') {
                                    if (ELIGIBILITY_DATA.data.Parentcode !== document.getElementById('kt_eligibility_type').value) {

                                        var dropdown2 = document.getElementById('kt_eligibility_type');

                                        var selectedText2 = dropdown2.options[dropdown2.selectedIndex].text;

                                        journalrequestBody.journallist.push({
                                            'description': username + ' : Changed Eligibility Level from ' + ELIGIBILITY_DATA.data.Parentcodename + ' to ' + selectedText2
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
                                    $('#kt_modal_add_eligibility').modal('hide');
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
            form = document.querySelector('#kt_modal_add_eligibility_form');
            submitButton = document.querySelector('#kt_add_eligibility_submit');
            modal = document.getElementById('kt_modal_add_eligibility');

            populateDropdown('ELIGIBILITY LEVEL', 'kt_eligibility_type');

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




        }
    };
}();

// Initialize the KTUsersAddUser module when the DOM is ready
document.addEventListener('DOMContentLoaded', function () {
    KTAddUpdate.init();
});
