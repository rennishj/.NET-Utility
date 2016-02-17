var RJ = RJ || {};
RJ.ajaxForJson = function (url, data, success, error) {

    $.ajax({
        url: url,
        type: 'POST',
        data: JSON.stringify(data),
        contentType: 'application/json',
        success: function (data) {
            if (data) {
                success(data);
            }
        },
        error: function (jqXHr) {
            if (error) {
                error();
            }
        }
    });
}