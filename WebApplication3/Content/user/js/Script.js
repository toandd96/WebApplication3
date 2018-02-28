function jquerryAjaxPost(form) {
    $.validator.unobtrusive.parse();
    if ($(form).validator()) {
        var ajaxConfig = {
            type: "POST",
            url: form.action,
            data: new FormData(form),
            success: function () {
                $("#view_all").html(response);
                refreshAddNewTab($(form).attr(data_resetUrl), true);
            }
        }
        if ($(form).attr('enctype') = "multipart/form-data") {
            ajaxConfig["contentType"] = false;
            ajaxConfig["processData"] = false;
        }
        $.ajax(ajaxConfig);
    }
    return false;
}

function refreshAddNewTab(resetURL, showviewTab) {
    $.ajax({
        type: 'GET',
        url: resetURL,
        success: function (response) {
            if (response.success) {
                $('#create_new').html(response);
                $('ul.nav.nav-tab a:eq(1)').html('Create new');
                $.notify(response.message, "success");
            }
            else {
                $.notify(response.message, "error")
            }
            if (showviewTab) $('ul.nav.nav-tab a:eq(0)').tab('show');
        }
    });
}
function Edit(url) {
    $.ajax({
        type: 'GET',
        url: url,
        success: function (response) {
            $('#create_new').html(response);
            $('ul.nav.nav-tabs a:eq(1)').html('Edit');
            $('ul.nav.nav-tabs a:eq(1)').text('Edit');
            $('ul.nav.nav-tabs a:eq(1)').tab('show');
        }
    });
}
function Create(url) {
    $.ajax({
        type: 'GET',
        url: url,
        success: function (response) {
            $('#create_new').html(response);
            //$('ul.nav.nav-tabs a:eq(1)').html('Create');
            $('ul.nav.nav-tabs a:eq(1)').text('Create');
            $('ul.nav.nav-tabs a:eq(1)').tab('show');
        }
    });
}
function Detail(url) {
    $.ajax({
        type: 'GET',
        url: url,
        success: function (response) {
            $('#create_new').html(response);
            $('ul.nav.nav-tabs a:eq(1)').html('Detail');
            //$('ul.nav.nav-tabs a:eq(1)').text('Detail');
            $('ul.nav.nav-tabs a:eq(1)').tab('show');
        }
    });
}
function Delete(url) {
    $.ajax({
        type: 'GET',
        url: url,
        success: function (response) {
            $('#create_new').html(response);
            $('ul.nav.nav-tabs a:eq(1)').html('Delete');
            //$('ul.nav.nav-tabs a:eq(1)').text('Detail');
            $('ul.nav.nav-tabs a:eq(1)').tab('show');
        }
    });
}
function giao_hang(url) {
     if (confirm("Bạn có chắc chắn về điều này?"))
     {
        $.ajax({
            type: "POST",
            url: url,
            success: function () {
                location.reload();
            }
        });
     }
}

    function show_detail(url)
    {
        $.ajax({
            type: "GET",
            url: url,
            success: function () {
                var button_detail = $("#detail-order");
                for (var i = 0; i < button_detail.length; i++)
                {

                }
            }
        });
}

    function readURL(input) {
        if (input.files && input.files[0]) {
            var reader = new FileReader();

            reader.onload = function (e) {
                $('#image_edit').removeAttr('src');
                $('#image_edit').attr('src', e.target.result);
            };

            reader.readAsDataURL(input.files[0]);
        }
    }
