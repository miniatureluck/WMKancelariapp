/*!
* Start Bootstrap - Simple Sidebar v6.0.6 (https://startbootstrap.com/template/simple-sidebar)
* Copyright 2013-2023 Start Bootstrap
* Licensed under MIT (https://github.com/StartBootstrap/startbootstrap-simple-sidebar/blob/master/LICENSE)
*/
// 
// Scripts
// 
$(document).ready(function () {
    var getFilteredClientUrl = $('#data-container').data('get-filtered-client-url');
    var getFilteredCasesUrl = $('#data-container').data('get-filtered-cases-url');

    var caseDropdown = $('#Case_Id');
    var clientDropdown = $('#Client_Id');


    caseDropdown.change(function () {
        var selectedCaseId = $(this).val();

        if (selectedCaseId === '0' && clientDropdown.length == 1) {
            clientDropdown.addClass('blink-red');
        } else {
            clientDropdown.addClass('blink-green');
        }

        $.get(getFilteredClientUrl, { caseId: selectedCaseId }, function (data) {
            clientDropdown.empty();

            $.each(data, function (index, item) {
                var option = $('<option>', {
                    value: item.value,
                    text: item.text
                });
                clientDropdown.append(option);
            });

            if (data.length === 1) {
                clientDropdown.val(data[0].value);
                clientDropdown.prop('readonly', true);
                clientDropdown.addClass('dropdown-disabled');
            } else {
                clientDropdown.removeClass('dropdown-disabled');
                clientDropdown.prop('readonly', false);
                clientDropdown.prepend($('<option>', {
                    value: 'all',
                    text: 'Brak'
                }));
                clientDropdown.val('all');
            }
            clientDropdown.multipleSelect('refresh');

            if (selectedCaseId === '0' && clientDropdown.length == 1) {
                clientDropdown.removeClass('blink-red');
            } else {
                clientDropdown.removeClass('blink-green');
            }
        });

        if (selectedCaseId === '0') {
            caseDropdown.addClass('blink-green');
            refreshCases('all');
            setTimeout(function () {
                caseDropdown.removeClass('blink-green');
            }, 500);
        }
    });

    clientDropdown.change(function () {
        var selectedClientId = $(this).val();

        caseDropdown.addClass('blink-green');

        refreshCases(selectedClientId);
    });

    function refreshCases(clientId) {
        $.get(getFilteredCasesUrl, { clientId: clientId }, function (data) {
            caseDropdown.empty();

            $.each(data, function (index, item) {
                var option = $('<option>', {
                    value: item.value,
                    text: item.text
                });
                caseDropdown.append(option);
            });

            caseDropdown.prepend($('<option>', {
                value: '0',
                text: 'Brak'
            }));
            caseDropdown.val('0');
            caseDropdown.multipleSelect('refresh');

            caseDropdown.removeClass('blink-green');
        });
    }
});


$(function () {
    $('.multiple-select').multipleSelect({
        filter: true,
        filterPlaceholder: 'Filtruj',
        selectAll: false,
        showClear: true
    })
});
$(function () {
    $('.select-dropbox').multipleSelect({
        filter: true, filterPlaceholder: 'Filtruj'
    })
});


$('#filter-toggle').click(function () {
    $('.filter-section').slideToggle(500);
    $('#filter-toggle').toggleClass("button-normal-toggled")
});

function filterNames(inputName, tableName, colNumber) {

    var input, filter, table, tr, td, i, txtValue;
    input = document.getElementById(inputName);
    filter = input.value.toUpperCase();
    table = document.getElementById(tableName);
    tr = table.getElementsByTagName("tr");

    for (i = 0; i < tr.length; i++) {
        td = tr[i].getElementsByTagName("td")[colNumber];
        if (td) {
            txtValue = td.textContent || td.innerText;
            if (txtValue.toUpperCase().indexOf(filter) > -1) {
                tr[i].style.display = "";
            } else {
                tr[i].style.display = "none";
            }
        }
    }
};

window.addEventListener('DOMContentLoaded', event => {

    const sidebarToggle = document.body.querySelector('#sidebarToggle');
    if (sidebarToggle) {

        if (localStorage.getItem('sb|sidebar-toggle') === 'true') {
            document.body.classList.toggle('sb-sidenav-toggled');
        }
        sidebarToggle.addEventListener('click', event => {
            event.preventDefault();
            document.body.classList.toggle('sb-sidenav-toggled');
            localStorage.setItem('sb|sidebar-toggle', document.body.classList.contains('sb-sidenav-toggled'));
        });
    }

});
