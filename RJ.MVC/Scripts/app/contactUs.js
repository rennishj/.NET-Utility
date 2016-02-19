$(function () {
    
    
    //$('body').on('click', '#cancelbtn', function () {
    //    $('#modal-container').modal('close');
    //})

    //This code is important.This is the one actually telling the validator to parse the form
    $('#modal-container').on('shown.bs.modal', function () {
        $.validator.unobtrusive.parse($('#form-contact'));
        console.log($('#button-submit').text());
        $('#button-submit').click(createContact);
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
    function createContact() {
        var contactUs = {
            firstName: $('#FirstName').val(),
            lastName: $('#LastName').val(),
            email: $('#Email').val(),
            phone:$('#Phone').val()
        }
        RJ.ajaxForJson('/ContactUs/ContactUs', contactUs, success, error);
    }
    function success(){
    
    }
    function error(){    
    
    }   
})
