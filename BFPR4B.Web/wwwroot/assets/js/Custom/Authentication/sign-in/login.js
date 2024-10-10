"use strict";

// Class definition
var KTSigninGeneral = function () {
    // Elements
    var form;
    var submitButton;
    var validator;

    // Initialize form validation
    var initValidation = function () {
        validator = FormValidation.formValidation(
            form,
            {
                fields: {
                    'account': {
                        validators: {
                            notEmpty: {
                                message: 'Account number is required'
                            }
                        }
                    },
                    'password': {
                        validators: {
                            notEmpty: {
                                message: 'The password is required'
                            }
                        }
                    }
                },
                plugins: {
                    trigger: new FormValidation.plugins.Trigger(),
                    bootstrap: new FormValidation.plugins.Bootstrap5({
                        rowSelector: '.fv-row',
                        eleInvalidClass: '',  // comment to enable invalid state icons
                        eleValidClass: '' // comment to enable valid state icons
                    })
                }
            }
        );
    };

    async function resetSearch() {
        const searchInput = document.getElementById("kt_forgot_account");
        const clearButton = document.getElementById("clearButton");

        searchInput.addEventListener("input", function () {
            clearButton.style.display = searchInput.value.trim() !== "" ? "block" : "none";
        });

        clearButton.addEventListener("click", function () {
            searchInput.value = "";
            clearButton.style.display = "none";
        });
    }

    // Handle form submission
    var handleFormSubmit = async function (e) {
        e.preventDefault();

        const status = await validator.validate();

        if (status === 'Valid') {
            submitButton.setAttribute('data-kt-indicator', 'on');
            submitButton.disabled = true;

            const accountInput = document.getElementById('kt_account').value;
            const passwordInput = document.getElementById('kt_password').value;

            const loginUrl = form.getAttribute('data-login-url'); // Get the URL from the data attribute

            const requestBody = JSON.stringify({
                accountnumber: accountInput,
                userpass: passwordInput,
                client_id: 'dfdsf',
                client_secret: 'fdsfs',
                grant_type: 'fsdfds',
                login_type: 'fsdfds'
            });

            try {
                const response = await fetch(loginUrl, {
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
                        text: "You have successfully logged in!",
                        icon: "success",
                        buttonsStyling: false,
                        confirmButtonText: "Ok, got it!",
                        customClass: {
                            confirmButton: "btn btn-primary"
                        }
                    }).then((result) => {
                        if (result.isConfirmed) {
                            const redirectUrl = form.getAttribute('data-kt-redirect-url');
                            if (redirectUrl) {
                                location.href = redirectUrl;
                            }
                        }
                    });

                    //const redirectUrl = form.getAttribute('data-kt-redirect-url');
                    //if (redirectUrl) {
                    //    location.href = redirectUrl;
                    //}
                } else {

                    const errorData = await response.json();

                    // Handle error messages using Swal
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
                    text: error.message,
                    icon: "error",
                    buttonsStyling: false,
                    confirmButtonText: "Ok, got it!",
                    customClass: {
                        confirmButton: "btn btn-primary"
                    }
                });
            } finally {
                submitButton.removeAttribute('data-kt-indicator');
                submitButton.disabled = false;
            }

        } else {
            Swal.fire({
                text: "Please fill in all required fields.",
                icon: "error",
                buttonsStyling: false,
                confirmButtonText: "Ok, got it!",
                customClass: {
                    confirmButton: "btn btn-primary"
                }
            });
        }
    };

    // Public functions
    return {
        // Initialization
        init: function () {
            form = document.querySelector('#kt_sign_in_form');
            submitButton = document.querySelector('#kt_sign_in_submit');

            initValidation();
            form.addEventListener('submit', handleFormSubmit);

            // Event handler for clearing search input
            $('#clearButton').on('click', function () {
                $('#kt_account').val('');
                dt.ajax.reload();
            });
        }
    };
}();

// On document ready
KTUtil.onDOMContentLoaded(function () {
    KTSigninGeneral.init();
});