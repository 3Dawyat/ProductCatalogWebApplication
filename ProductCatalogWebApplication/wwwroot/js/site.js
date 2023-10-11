var updatedRow;
var datatable;
function onModalBegin() {
    $('body :submit').attr('disabled', 'disabled').text('Please Wait ...');
}
function onModalComplete() {
    $('body :submit').removeAttr('disabled').text('Save');
}
function showErrorMessage(response = 'Something went wrong !') {
    Swal.fire({
        icon: 'error',
        title: 'Oops...',
        text: response.responseJSON !== undefined ? response.responseJSON.value : response,
        customClass: {
            confirmButton: "btn btn-primary"
        }
    });
}
function showSuccessMessage(message = 'Saved successfully !') {
    Swal.fire({
        icon: 'success',
        title: 'Good Job',
        text: message,
        customClass: {
            confirmButton: "btn btn-primary"
        }
    });
}
function onModalSuccess(row) {
    showSuccessMessage();
    $('#Modal').modal('hide');
    if (updatedRow !== undefined) {

        datatable.row(updatedRow).remove().draw();
        updatedRow = undefined;

    }
    var newRow = $(row);
    datatable.row.add(newRow).draw();

}
$(document).ready(function () {
     // DataTable
    if ($('#js-table').length > 0) {
        datatable = $('#js-table').DataTable({
            "columnDefs": [
                {
                    "targets": [1],
                    "orderable": true
                }
            ],
            "order": []
        });
    }
    // render modal
    $('body').delegate('.js-render-modal', 'click', function () {

        var btn = $(this);
        var modal = $('#Modal');
        if (btn.data('update') !== undefined) {
            updatedRow = btn.parents("tr");
        }
        modal.find('#ModalLabel').text(btn.data('title'));
        $.get({
            url: btn.data('url'),
            success: function (form) {
                modal.find('.modal-body').html(form);
                $.validator.unobtrusive.parse(modal);
            },
            error: function () {
                showErrorMessage();
            }
        });
        modal.modal('show');
    });
    // btn filter by category
    $('body').delegate('.js-btn-filter', 'click', function () {

        $('.js-btn-filter').removeClass('btn-warning').addClass('btn-primary');
        var btn = $(this);
        btn.removeClass('btn-primary').addClass('btn-warning');


        $.get({
            url: btn.data('url'),
            data: {
                '__RequestVerificationToken': $('input[name="__RequestVerificationToken"]').val()
            },
            success: function (items) {

                var htmlcCard = "";
                $.each(items, function (i, item) {
                    htmlcCard += ` <div class="col col-3 mt-3">
                                        <div class="card" style="width :18rem;">
                                            <h5 class="card-title">${item.name}</h5>
                                            <h6 class="card-subtitle mb-2 text-muted">Price : ${item.price}</h6>
                                            <a href="/Products/Details?key=${item.key}" class="card-link">Details</a>
                                        </div>
                                    </div>`;
                });
                $('#card-perant').html(htmlcCard);
            },
            error: function () {
                showErrorMessage();
            }
        });
    });
    // toggle status
    $('body').delegate('.js-toggle-status', 'click', function () {
        var btn = $(this);
        bootbox.confirm({
            message: "Are you sure that you need to change  status ?",
            buttons: {
                confirm: {
                    label: 'Yes',
                    className: 'btn-danger'
                },
                cancel: {
                    label: 'No',
                    className: 'btn-secondary'
                }
            },
            callback: function (result) {
                if (result) {
                    $.post({
                        url: btn.data('url'),
                        data: {
                            '__RequestVerificationToken': $('input[name="__RequestVerificationToken"]').val()
                        },
                        success: function (newUpdatedData) {
                            var row = btn.parents('tr');
                            var newAvailability = row.find('.js-availability');
                            var newStatus = row.find('.js-status');

                            row.find('.js-lastUpdatedOn').html(newUpdatedData.lastUpdatedOn);
                            row.find('.js-lastUpdatedBy').html(newUpdatedData.lastUpdatedBy);

                            if (newUpdatedData.availability)
                                newAvailability.html('Supposed to show').removeClass('text-bg-danger').addClass('text-bg-warning');
                            else
                                newAvailability.html('Not supposed to show').removeClass('text-bg-warning').addClass('text-bg-danger');


                            if (newUpdatedData.status)
                                newStatus.html('Deleted').removeClass('text-bg-success').addClass('text-bg-danger');
                            else
                                newStatus.html('Available').removeClass('text-bg-danger').addClass('text-bg-success');


                            showSuccessMessage();
                        },

                        error: function () {
                            showErrorMessage()
                        }
                    });
                }
            }
        });
    });
});

