var pageNumber = 1;
var placeholderElement = $('#modal-placeholder');
var deleteModal = $('#confirm-delete');

$(document).ready(function () {
    GetPartialResults(null, null, null, null);
});

function InitButtons() {
    Unbind();
    InitFilters();
    InitEditButtons();
    InitPagination();
    InitDeleteButtons();
}


function InitEditButtons() {
    $('button[data-toggle="ajax-modal"]').click(function (event) {
        var url = $(this).data('url');
        $.get(url).done(function (data) {
            placeholderElement.html(data);
            placeholderElement.find('.modal').modal('show');
        });
    });

    $('a[data-toggle="ajax-modal"].edit-button').click(function (event) {
        event.preventDefault();
        var url = $(this).data('url');
        $.get(url).done(function (data) {
            event.preventDefault();
            placeholderElement.html(data);
            placeholderElement.find('.modal').modal('show');
        });
    });

    //Al guardar la edición
    placeholderElement.on('click', '#ModalSave', function (event) {
        event.preventDefault();
        var form = $(this).parents('.modal').find('form');
        var actionUrl = form.attr('action');
        var dataToSend = form.serialize();
        $.post(actionUrl, dataToSend).done(function (data) {
            var newBody = $('.modal-body', data);
            placeholderElement.find('.modal-body').replaceWith(newBody);
            placeholderElement.find('.modal').modal('hide');
            var text = "Guardado correctamente";
            noty({
                text: text,
                type: 'success',
                timeout: 1000,
                progressBar: true,
                callback: {
                    onCloseClick: function () {
                        window.location.href = GetAjaxCallbackUrl();
                    },
                    onClose: function () {
                        window.location.href = GetAjaxCallbackUrl();
                    }
                }
            });
        });
    });
}

function InitDeleteButtons() {
    $('.delete-button').click(function (event) {
        event.preventDefault();
        var code = $(this).closest("td").find(".code").val();
        var desc = $(this).closest("td").find(".desc").val();
        var message = $("#DeleteKeyMessage").val();
        $("#modal-delete-item").text(message + " " + desc);
        $("#DeleteModalCode").val(code);
    });

    deleteModal.on('click', '#ModalDelete', function (event) {
        event.preventDefault();
        var form = $("#deleteForm");
        var actionUrl = form.attr('action');
        var dataToSend = form.serialize();
        $.post(actionUrl, dataToSend).done(function (data) {
            placeholderElement.find('.modal').modal('hide');
            var text = "Borrado correctamente";
            noty({
                text: text,
                type: 'success',
                timeout: 1000,
                progressBar: true,
                callback: {
                    onCloseClick: function () {
                        window.location.href = GetAjaxCallbackUrl();
                    },
                    onClose: function () {
                        window.location.href = GetAjaxCallbackUrl();
                    }
                }
            });
        });
    });
}

function InitPagination() {
    $('#paginationFirstPage').click(function (event) {
        event.preventDefault();
        pageNumber = 1;
        GetPartialResults(pageNumber, null, null, null);
    });

    $('#paginationBackPage').click(function (event) {
        event.preventDefault();
        pageNumber = GetCurrentPage() - 1;
        GetPartialResults(pageNumber, null, null, null);
    });

    $('#paginationNextPage').click(function (event) {
        event.preventDefault();
        pageNumber = GetCurrentPage() + 1;
        GetPartialResults(pageNumber, null, null, null);
    });

    $('#paginationLastPage').click(function (event) {
        event.preventDefault();
        pageNumber = GetTotalPages();
        GetPartialResults(pageNumber, null, null, null);
    });

    $('.paginationNextNumber').click(function (event) {
        event.preventDefault();
        pageNumber = $(this).text();
        GetPartialResults(pageNumber, null, null, null);
    });
}

function InitFilters() {
    $(".result").on('click', '.table-filter', function (event) {
        event.preventDefault();
        var value = $(this).prev('input').val();
        var splitValue = value.split('_');

        var asc = splitValue[0];
        var column = splitValue[1];

        GetPartialResults(null, null, asc, column);
    });
}

function Unbind() {
    var placeholderElement = $('#modal-placeholder');
    var deleteModal = $('#confirm-delete');
    $('a[data-toggle="ajax-modal"].edit-button').unbind("click");
    $('button[data-toggle="ajax-modal"]').unbind("click");
    $('.delete-button').unbind("click");
    $('#ModalSave').unbind("click");
    $('#ModalDelete').unbind("click");

    $('#paginationFirstPage').unbind("click");
    $('#paginationBackPage').unbind("click");
    $('#paginationNextPage').unbind("click");
    $('#paginationLastPage').unbind("click");

    deleteModal.off('*');
    placeholderElement.off('*');
    deleteModal.off('click #ModalDelete');
    placeholderElement.off('click #ModalSave');
    placeholderElement.unbind("click");
    deleteModal.unbind("click", '#ModalDelete');
    placeholderElement.unbind("click", '#ModalSave');
}

function GetCurrentPage() {
    return parseInt($('#currentPage').val());
}

function GetTotalPages() {
    return parseInt($('#totalPage').val());
}

function GetAjaxCallbackUrl() {
    return $('#ajaxCallback').val();
}

function ChangeItemsPerPage(selectCombo) {
    var itemsPerPage = selectCombo.value;

    GetPartialResults("", itemsPerPage, null, null, null);
}

function GetPartialResults(pageNum, itemsPerPage, asc, sort, searchString) {
    var currentFilter = $('#currentFilter').val();
    var thisPageNum = $('#currentPage').val();
    var perPage = $('#itemsPerPage').val();
    var ascDesc = $('#ascOrDescOrder').val();
    var sortOrder = $('#currentSort').val();
    var search = "";

    if (pageNum != null && pageNum != undefined) {
        thisPageNum = pageNum;
    }

    if (itemsPerPage != null && itemsPerPage != undefined) {
        perPage = itemsPerPage;
    }

    if (asc != null && asc != undefined) {
        ascDesc = asc;
    }

    if (sort != null && sort != undefined) {
        sortOrder = sort;
    }

    if (searchString != null && searchString != undefined) {
        search = searchString;
    }

    var data = {
        currentFilter: currentFilter,
        itemsPerPage: perPage,
        ascOrDescOrder: ascDesc,
        pageNumber: thisPageNum,
        sortOrder: sortOrder,
        searchString: search
    };

    $.ajax({
        url: 'Colores/GetPartialResults',
        type: 'GET',
        data: data,
        success: function (data) {
            $("#results").html(data);
        }
    });
}