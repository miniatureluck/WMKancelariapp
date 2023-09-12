$(document).ready(function () {
    $('#deadlineStatusModal').on('show.bs.modal', function (event) {
        var link = $(event.relatedTarget);
        var description = link.data('description');
        var usercase = link.data('case');
        var status = link.data('status');
        var deadlineId = link.data('deadline-id');

        var modal = $(this);
        var modalDescription = modal.find('#deadline-description');
        var modalCase = modal.find('#deadline-case');
        var modalStatus = modal.find('#deadline-status');

        modalDescription.text(description);
        modalCase.text(usercase);
        modalStatus.text(status);

        var deadlineActionLink = modal.find('#deadline-action');
        deadlineActionLink.attr('href', '/Deadlines/RevertStatus/' + deadlineId);
    });
});
