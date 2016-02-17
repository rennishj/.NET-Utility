$(function () {
    

    //$('body').on('click', '#cancelbtn', function () {
    //    $('#modal-container').modal('close');
    //})

    //This code is important.This is the one actually telling the validator to parse the form
    $('#modal-container').on('shown.bs.modal', function () {
        $.validator.unobtrusive.parse($('#form-contact'));
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
    //$('.btn-primary').on('click', function () {
    //    $.validator.unobtrusive.parse('form-contact');
    //    $('#form-contact').validate();
    //    return false;
    //});

    $('#form-contact').submit(function () {
        createContact();
        return false;
    });

    function createContact() {

    }
    
})
