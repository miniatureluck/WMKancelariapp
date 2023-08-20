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