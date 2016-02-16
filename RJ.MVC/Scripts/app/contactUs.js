$(function () {
    //https://codemagik.wordpress.com/2015/04/07/render-mvc-partial-view-inside-bootstrap-modal-dialog/ refer to this for getting this to work
    $('body').on('click', '.modal-link', function (e) {        
        e.preventDefault();
        
    })

    $('body').on('click', '#cancelbtn', function () {
        $('#modal-container').modal('close');
    })

    $('#modal-container').on('hidden.bs.modal', function () {
        $(this).removeData('bs.modal');
    })

    //$('#cancelbtn').on('click', function () {
    //    $('input').val(' ');        
    //    return false;
    //})

    $('.modal-link').click(function () {
        var url = 'ContactUs/ContactUs';
        $.get(url, function (data) {
            $('.modal-header').html('<h2>Contact Us</h2>');
            $('.modal-body').html(data);
            $('#modal-container').modal('show');
        });
    })
})
