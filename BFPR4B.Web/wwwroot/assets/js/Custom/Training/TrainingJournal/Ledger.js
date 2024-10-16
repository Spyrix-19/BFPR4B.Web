﻿
"use strict";

// Class definition
var KTDatatablesJournal = function () {
    // Shared variables
    var table;
    var dt;

    async function resetSearch() {
        const searchInput = document.getElementById("searchJournalInput");
        const clearButton = document.getElementById("clearJournalButton");

        searchInput.addEventListener("input", function () {
            clearButton.style.display = searchInput.value.trim() !== "" ? "block" : "none";
        });

        clearButton.addEventListener("click", function () {
            searchInput.value = "";
            clearButton.style.display = "none";
        });
    }

    // Private functions
    var initDatatable = function () {

        if (dt) {
            // If the DataTable instance already exists, destroy it
            dt.destroy();
        }

        // Calculate height directly within the DataTable initialization
        const windowHeight = $(window).height();
        const headerHeight = $('.header-class').outerHeight() || 0; // Replace with your header's actual class
        const footerHeight = $('.footer-class').outerHeight() || 0; // Replace with your footer's actual class
        const dataTableHeight = Math.max(windowHeight - headerHeight - footerHeight - 25, windowHeight * 0.5); // Adjust for any extra spacing

        dt = $("#kt_table_training_journal").DataTable({
            searchDelay: 500,
            processing: true,
            serverSide: false,
            order: [],
            ordering: false, // Disable sorting for all columns
            stateSave: false,
            select: {
                style: 'multi',
                selector: 'td:first-child input[type="checkbox"]',
                className: 'row-selected'
            },
            ajax: {
                url: "/training/journal/ledger",
                type: "GET",
                data: function (d) {
                    // Use the DataTables `ajax.data` option to customize the data sent in the request
                    d.searchkey = $("#searchJournalInput").val();
                    d.gendetno = $("#kt_training_journal_detno").val();
                },
                complete: function () {
                    // Reset to page 1 whenever a new set of data is loaded
                    dt.page(0).draw(false);
                }
            },
            columns: [
                { data: 'Detno' },
                { data: 'Description' },
                { data: 'Encodedbyname' },
                {
                    data: 'Dateencoded',
                    render: function (data) {
                        // Format the date as "MM/DD/yyyy"
                        return data ? new Date(data).toLocaleDateString('en-US') : '';
                    }
                }
            ],
            columnDefs: [
                {
                    targets: [3],
                    className: 'text-center',
                },
                {
                    targets: [0], // First column (index 0)
                    visible: false // Hide the column
                }
            ],
            scrollY: dataTableHeight + 'px', // Set height directly
            scrollCollapse: true, // Collapse when there are few records
            paging: true,
            pagingType: "full",
            pageLength: 10,
            lengthMenu: [10, 25, 50, 100],
            language: {
                paginate: {
                    first: "<<",
                    last: ">>",
                    next: ">",
                    previous: "<"
                }
            },
            initComplete: function () {
                // Set initial height of the DataTable container
                $(this).css('height', dataTableHeight + 'px');
            }

        });

        dt.column(0).visible(false); // Hide the first column (index 0)

        table = dt.$;

        // Re-init functions on every table re-draw -- more info: https://datatables.net/reference/event/draw
        dt.on('draw', function () {

        });

        // Event handler for the "x" button in Search
        $('[data-kt-training-journal-table-search="search"]').on('click', resetSearch);

        // Event handler for input field change in search when enter is hit
        $('#searchJournalInput').on('keydown', function (e) {
            if (e.key === 'Enter') { // Check if Enter key (key code 13) is pressed
                dt.ajax.reload();
            }
        });

        // Event handler for clearing search input when x is click
        $('#clearJournalButton').on('click', function () {
            $('#searchJournalInput').val('');
            dt.ajax.reload();
        });
    }

    // Public methods
    return {
        init: function () {
            initDatatable();

            let modal = document.getElementById('kt_modal_training_journal');

            // Listen for Bootstrap modal shown event
            $(modal).on('shown.bs.modal', function () {
                // Get selected user data when the modal is shown
                initDatatable();
            });
        }
    }
}();

// On document ready
KTUtil.onDOMContentLoaded(function () {
    KTDatatablesJournal.init();
});