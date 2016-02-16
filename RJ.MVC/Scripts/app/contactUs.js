$(function () {
    //https://codemagik.wordpress.com/2015/04/07/render-mvc-partial-view-inside-bootstrap-modal-dialog/ refer to this for getting this to work
    $('body').on('click', '.modal-link', function (e) {        
        e.preventDefault();
        $(this).attr('data-target', '#modal-container');
        $(this).attr('data-toggle', 'modal');
    })

    $('body').on('click', '.modal-close-btn', function () {
        $('#modal-container').modal('hide');
    })

    $('#modal-container').on('hidden.bs.modal', function () {

        $(this).removeData('bs.modal');
    })

    $('#cancelbtn').on('click', function () {
        return false;
    })
})
