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
            dt.ajax.reload(); // Reload DataTable on clear
        });
    }

    // Private functions
    var initDatatable = function () {
        // Initialize DataTable if it doesn't exist
        if (!dt) {
            dt = $("#kt_table_province_journal").DataTable({
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
                    url: "/province/journal/ledger",
                    type: "GET",
                    data: function (d) {
                        // Use the DataTables `ajax.data` option to customize the data sent in the request
                        d.searchkey = $("#searchJournalInput").val();
                        d.provinceno = $("#kt_provinceno").val();
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
                ]
            });

            // Hide the numeric cells on draw
            dt.on('draw', function () {
                $('.dt-type-numeric').hide();
            });
        }
    }

    // Public methods
    return {
        init: function () {
            initDatatable(); // Initialize the DataTable

            let modal = document.getElementById('kt_modal_province_journal');

            // Listen for Bootstrap modal shown event
            $(modal).on('shown.bs.modal', function () {
                // Reload data when the modal is shown
                dt.ajax.reload();
            });

            // Event handler for the "x" button in Search
            $('[data-kt-province-journal-table-search="search"]').on('click', resetSearch);

            // Event handler for input field change in search when enter is hit
            $('#searchJournalInput').on('keydown', function (e) {
                if (e.key === 'Enter') { // Check if Enter key is pressed
                    dt.ajax.reload(); // Reload DataTable on Enter key
                }
            });
        }
    }
}();

// On document ready
KTUtil.onDOMContentLoaded(function () {
    KTDatatablesJournal.init();
});
