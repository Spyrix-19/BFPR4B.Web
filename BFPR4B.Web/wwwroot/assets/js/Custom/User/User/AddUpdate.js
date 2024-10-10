"use strict";

var KTAddUpdate = function () {
    var form;
    var submitButton;
    var validator;
    var passwordMeter;
    var modal;

    // Initialize form validation
    var initValidation = async function () {
        validator = FormValidation.formValidation(
            form,
            {
                fields: {
                    'nup': {
                        validators: {
                            notEmpty: {
                                message: 'NUP is required'
                            }
                        }
                    },
                    'account': {
                        validators: {
                            notEmpty: {
                                message: 'Account number is required. (If NUP, please specify your desired account number)'
                            }
                        }
                    },
                    'name': {
                        validators: {
                            notEmpty: {
                                message: 'Name is required'
                            }
                        }
                    },
                    'emailadd': {
                        validators: {
                            regexp: {
                                regexp: /^[^\s@]+@[^\s@]+\.[^\s@]+$/,
                                message: 'The value is not a valid email address',
                            },
                            notEmpty: {
                                message: 'Email address is required'
                            }
                        }
                    },
                    'mobile': {
                        validators: {
                            notEmpty: {
                                message: 'Mobile is required'
                            }
                        }
                    },
                    'areaassign': {
                        validators: {
                            notEmpty: {
                                message: 'Area of assignment is required'
                            }
                        }
                    },
                    'unitassign': {
                        validators: {
                            notEmpty: {
                                message: 'Unit of assignment is required'
                            }
                        }
                    },
                    'tfa': {
                        validators: {
                            notEmpty: {
                                message: 'Two Factor Authentication is required'
                            }
                        }
                    },
                    'role': {
                        validators: {
                            notEmpty: {
                                message: 'Account Role is required'
                            }
                        }
                    },
                    'password': {
                        validators: {
                            notEmpty: {
                                message: 'The password is required'
                            },
                            callback: {
                                message: 'Please enter a valid password',
                                callback: function (input) {
                                    if (input.value.length > 0) {
                                        return validatePassword();
                                    }
                                }
                            }
                        }
                    },
                    'confirm-password': {
                        validators: {
                            notEmpty: {
                                message: 'The password confirmation is required'
                            },
                            identical: {
                                compare: function () {
                                    return form.querySelector('[name="password"]').value;
                                },
                                message: 'The password and its confirm are not the same'
                            }
                        }
                    },
                    'toc': {
                        validators: {
                            notEmpty: {
                                message: 'You must accept the terms and conditions'
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
                        eleValidClass: ''
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

    var togglePasswordVisibility = async function () {
        // Toggle password visibility in the modal
        const confirmpasswordInput = document.getElementById("kt_confirmpassword");
        const passwordIcon = document.getElementById("password-icon");
        const passwordToggle = document.getElementById("password-toggle");

        passwordToggle.addEventListener("click", () => {
            if (confirmpasswordInput.type === "password") {
                confirmpasswordInput.type = "text";
                passwordIcon.classList.remove("bi-eye-slash");
                passwordIcon.classList.add("bi-eye");
            } else {
                confirmpasswordInput.type = "password";
                passwordIcon.classList.remove("bi-eye");
                passwordIcon.classList.add("bi-eye-slash");
            }
        });
    };

    // Fetch user data and populate the form
    var fetchAndUpdateUserData = async function (userno) {
        try {

            document.getElementById('kt_userno').value = '';
            document.getElementById('kt_nup').checked = false;
            document.getElementById('kt_account').value = '';
            document.getElementById('kt_name').value = '';
            document.getElementById('kt_emailadd').value = '';
            document.getElementById('kt_mobile').value = '';
            document.getElementById('kt_areaassign').value = '';
            document.getElementById('kt_areaassign').text = '';
            document.getElementById('kt_unitassign').value = '';
            document.getElementById('kt_unitassign').text = '';
            document.getElementById('kt_tfa').value = '';
            document.getElementById('kt_tfa').text = '';
            document.getElementById('kt_role').value = '';
            document.getElementById('kt_role').text = '';

            const response = await fetch(`/users/details?userno=${userno}`);
            if (response.ok) {
                const userData = await response.json();

                if (userData !== null) {

                    $('#kt_userno').val(userData.data.Userno);
                    $('#kt_nup').prop('checked', userData.data.Nup);
                    $('#kt_account').val(userData.data.Accountnumber);
                    $('#kt_name').val(userData.data.Username);
                    $('#kt_emailadd').val(userData.data.Emailadd);
                    $('#kt_mobile').val(userData.data.Mobileno);

                    $('#kt_areaassign').val(userData.data.Areaassign).trigger('change.select2');
                    $('#kt_unitassign').val(userData.data.Unitassign).trigger('change.select2');
                    $('#kt_tfa').val(userData.data.Tfa).trigger('change.select2');
                    $('#kt_role').val(userData.data.Role).trigger('change.select2');
                }
            } else {
                console.error('Failed to fetch user data:', response.status, response.statusText);
            }
        } catch (error) {
            console.error('An error occurred:', error);
        }
    };

    //Function to get data-userno attribute value from the selected row in DataTable and fetch user data
    async function getSelectedUserAndFetchData() {
        //const dataUserno = document.getElementById('kt_userno').value;

        //if (dataUserno) {
        //    // If the attribute exists, call the fetchAndUpdateUserData function with the user number
        //    fetchAndUpdateUserData(dataUserno);
        //} else {
        //    console.error('data-userno attribute is missing in the selected row.');
        //}

        const usernoInput = document.getElementById('kt_userno');
        const usernoValue = usernoInput.value.trim(); // Trim any leading/trailing whitespace

        let dataUserno = usernoValue === '' ? '0' : usernoValue;

        if (dataUserno) {
            // If the attribute exists, call the fetchAndUpdateUserData function with the user number
            if (dataUserno !== '0') {
                fetchAndUpdateData(dataUserno);
            }
        } else {
            console.error('data-userno attribute is missing in the selected row.');
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

    //async function togglePasswordFields() {
    //    var passwordContainer = document.getElementById("password-container"); // Set the password container element
    //    var passwordInput = document.getElementById("kt_password"); // Set the password input element
    //    var confirmPasswordInput = document.getElementById("kt_confirm-password"); // Set the confirm password input element
    //    const ktUserno = document.getElementById("kt_userno").value;

    //    if (ktUserno === "0" || ktUserno === "") {
    //        // Show the password fields
    //        passwordContainer.style.display = "block";
    //    } else {
    //        // Hide the password fields
    //        passwordContainer.style.display = "none";
    //        passwordInput.value = ""; // Clear the password input
    //        confirmPasswordInput.value = ""; // Clear the confirm password input            
    //    }
    //}

    // Function to update the DataTable with new data
    var updateDataTable = async function () {
        const table = $('#kt_table_users').DataTable();
        table.ajax.reload(); // Reload the DataTable to fetch updated data
    };

    var handleFormSubmit = async function (e) {
        e.preventDefault();
        const status = await validator.validate();
        if (status === 'Valid') {
            submitButton.setAttribute('data-kt-indicator', 'on');
            submitButton.disabled = true;

            const adduserUrl = '/users/create';
            const requestBody = JSON.stringify({
                userno: document.getElementById('kt_userno').value,
                nup: document.getElementById('kt_nup').checked,
                username: document.getElementById('kt_name').value,
                accountnumber: document.getElementById('kt_account').value,
                mobileno: document.getElementById('kt_mobile').value,
                emailadd: document.getElementById('kt_emailadd').value,
                activeuser: false,
                areaassign: document.getElementById('kt_areaassign').value,
                unitassign: document.getElementById('kt_unitassign').value,
                tfa: document.getElementById('kt_tfa').value,
                role: document.getElementById('kt_role').value,
            });

            try {
                const response = await fetch(adduserUrl, {
                    method: 'POST',
                    headers: {
                        'Content-Type': 'application/json',
                        'Accept': 'application/json'
                    },
                    body: requestBody
                });
                if (response.ok) {
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
                            modal.hide();
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

    var validatePassword = function () {
        return (passwordMeter.getScore() > 50);
    };


    return {
        init: function () {
            form = document.querySelector('#kt_modal_add_user_form');
            submitButton = document.querySelector('#kt_add_users_submit');
            passwordMeter = KTPasswordMeter.getInstance(form.querySelector('[data-kt-password-meter="true"]'));
            modal = document.getElementById('kt_modal_add_user');

            // Initialize validation and other functionality
            if (form && submitButton) {
                initValidation();
                form.addEventListener('submit', handleFormSubmit);

                // Listen for Bootstrap modal shown event
                $(modal).on('shown.bs.modal', function () {
                    // Get selected user data when the modal is shown

                    resetAddUpdate();
                    getSelectedUserAndFetchData();

                    //// Call the togglePasswordFields function initially
                    //togglePasswordFields();

                    //// Add an event listener to kt_userno to handle changes
                    //document.getElementById("kt_userno").addEventListener("change", togglePasswordFields);
                });
            }

            populateDropdown('AREA OF ASSIGNMENT', 'kt_areaassign');
            populateDropdown('ACCOUNT ROLE', 'kt_role');
            populateDropdown('TWO FACTOR AUTHENTICATION', 'kt_tfa');
            // PopulateDropdown('UNIT OF ASSIGNMENT', 'kt_unitassign'); // Uncomment if needed

            togglePasswordVisibility();


        }
    };
}();

// Initialize the KTUsersAddUser module when the DOM is ready
document.addEventListener('DOMContentLoaded', function () {
    KTAddUpdate.init();
});
