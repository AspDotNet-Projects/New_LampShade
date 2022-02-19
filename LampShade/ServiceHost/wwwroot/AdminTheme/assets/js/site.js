﻿var SinglePage = {};

///SinglePage.LoadModal  is fuction
//اکه کال بشه 
SinglePage.LoadModal = function () {
    //window.location.hash
    //یعنی بعد از یو آر ال هرچی بیاد میشه هش . مثل
    //https://localhost:5001/Administration/Shop/ProductCategories#Create
    //#ShowModal ----->منظور ما هست
    var url = window.location.hash.toLowerCase();
    //showmodalاگز هش  با
    //شروزع نشده بود برو بیرون
    if (!url.startsWith("#showmodal")) {
        return;
    }
    //در غیر اینصورت 
    //#ShowModal/Create
    //اسپلیت میکنه 
    //و میرسه به /Create
    url = url.split("showmodal=")[1];
    $.get(url,
        null,
        //یک اج تی ام ال پیج میمده به ما
        function (htmlPage) {
            //حالا هش تغییر میکنه در خطوط پایین تر ما گفنیم که اکه هش تغییر کنه 
            //حالا شو مودال رو صدا بزن
            $("#ModalContent").html(htmlPage);
            //این چهار خط باعث میشه که کلاینت ساید بشه ولیدیتور هست
            const container = document.getElementById("ModalContent");
            const forms = container.getElementsByTagName("form");
            const newForm = forms[forms.length - 1];
            $.validator.unobtrusive.parse(newForm);
            showModal();
        }).fail(function (error) {
            alert("خطایی رخ داده، لطفا با مدیر سیستم تماس بگیرید.");
        });
};

function showModal() {
    $("#MainModal").modal("show");
}

function hideModal() {
    $("#MainModal").modal("hide");
}

//اگه شو مودال تغییر کنه حالا لود مدال رو تغییر بده
$(document).ready(function () {
    window.onhashchange = function () {
        SinglePage.LoadModal();
    };
    $("#MainModal").on("shown.bs.modal",
        function () {
            window.location.hash = "##";
            $('.persianDateInput').persianDatepicker({
                format: 'YYYY/MM/DD',
                autoClose: true
            });
        });

//اگر کاربر رویداد ساب میت رو انجام داد مکه فقط مروبوط به ایجاد محصول هست
//و این شرط زیر برقرار بود
    //form[data - ajax= "true"]
    //که در فرم ایحاد گروه محصول دیتا ای جکس رو ترو کردیم
    $(document).on("submit",
        'form[data-ajax="true"]',
        function (e) {
            e.preventDefault();
            //اون فرم رو بگیر
            var form = $(this);
            //اتریبیوت فرم رو بگیر که ما در فر ایجاد گروه محصول پست گذاشتیم
            const method = form.attr("method").toLocaleLowerCase();
            //اکشن آن فرم هم هست
            //asp-page="./Index" asp-page-handler="Create"
            //یعنی رویدادپست برای آن باید نوشت
            const url = form.attr("action");
            const data = form.serializeArray();
            //اتفاقی که قرار است بعد ارز ارسال فرم رخ می دهد
            var action = form.attr("data-action");

            if (method === "get") {
                const data = form.serializeArray();
                $.get(url,
                    data,
                    function (data) {
                        CallBackHandler(data, action, form);
                    });
            } else {
                var formData = new FormData(this);
                $.ajax({
                    url: url,
                    type: "post",
                    data: formData,
                    enctype: "multipart/form-data",
                    dataType: "json",
                    processData: false,
                    contentType: false,
                    success: function (data) {
                        CallBackHandler(data, action, form);
                    },
                    error: function (data) {
                        alert("خطایی رخ داده است. لطفا با مدیر سیستم تماس بگیرید.");
                    }
                });
            }
            return false;
        });
});

function CallBackHandler(data, action, form) {
    switch (action) {
        case "Message":
            alert(data.Message);
            break;
        case "Refresh":
            if (data.isSuccedded) {
                window.location.reload();
            } else {
                alert(data.message);
            }
            break;
        case "RefereshList":
            {
                hideModal();
                const refereshUrl = form.attr("data-refereshurl");
                const refereshDiv = form.attr("data-refereshdiv");
                get(refereshUrl, refereshDiv);
            }
            break;
        case "setValue":
            {
                const element = form.data("element");
                $(`#${element}`).html(data);
            }
            break;
        default:
    }
}

function get(url, refereshDiv) {
    const searchModel = window.location.search;
    $.get(url,
        searchModel,
        function (result) {
            $("#" + refereshDiv).html(result);
        });
}

function makeSlug(source, dist) {
    const value = $('#' + source).val();
    $('#' + dist).val(convertToSlug(value));
}

var convertToSlug = function (str) {
    var $slug = '';
    const trimmed = $.trim(str);
    $slug = trimmed.replace(/[^a-z0-9-آ-ی-]/gi, '-').replace(/-+/g, '-').replace(/^-|-$/g, '');
    return $slug.toLowerCase();
};

function checkSlugDuplication(url, dist) {
    const slug = $('#' + dist).val();
    const id = convertToSlug(slug);
    $.get({
        url: url + '/' + id,
        success: function (data) {
            if (data) {
                sendNotification('error', 'top right', "خطا", "اسلاگ نمی تواند تکراری باشد");
            }
        }
    });
}


//برای پر کردن خودککار متا دسکریپتیون از روی دسکریپتیون
function fillField(source, dist) {
    const value = $('#' + source).val();
    $('#' + dist).val(value);
}

$(document).on("click",
    'button[data-ajax="true"]',
    function () {
        const button = $(this);
        const form = button.data("request-form");
        const data = $(`#${form}`).serialize();
        let url = button.data("request-url");
        const method = button.data("request-method");
        const field = button.data("request-field-id");
        if (field !== undefined) {
            const fieldValue = $(`#${field}`).val();
            url = url + "/" + fieldValue;
        }
        if (button.data("request-confirm") == true) {
            if (confirm("آیا از انجام این عملیات اطمینان دارید؟")) {
                handleAjaxCall(method, url, data);
            }
        } else {
            handleAjaxCall(method, url, data);
        }
    });

function handleAjaxCall(method, url, data) {
    if (method === "post") {
        $.post(url,
            data,
            "application/json; charset=utf-8",
            "json",
            function (data) {

            }).fail(function (error) {
                alert("خطایی رخ داده است. لطفا با مدیر سیستم تماس بگیرید.");
            });
    }
}

jQuery.validator.addMethod("maxFileSize",
    function (value, element, params) {
        var size = element.files[0].size;
        var maxSize = 3 * 1024 * 1024;
        if (size > maxSize)
            return false;
        else {
            return true;
        }
    });
jQuery.validator.unobtrusive.adapters.addBool("maxFileSize");

//jQuery.validator.addMethod("maxFileSize",
//    function (value, element, params) {
//        var size = element.files[0].size;
//        var maxSize = 3 * 1024 * 1024;
//        debugger;
//        if (size > maxSize)
//            return false;
//        else {
//            return true;
//        }
//    });
//jQuery.validator.unobtrusive.adapters.addBool("maxFileSize");
