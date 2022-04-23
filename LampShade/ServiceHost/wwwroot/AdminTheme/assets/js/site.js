var SinglePage = {};

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
            //برای اینکه تاریخ به صورت خودکار در فیلد پر شود
            $('.PersianDateInput').persianDatepicker({

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
    //    function (e)
    //ما داریم به ورودی یک آتچکت می دیم
        function (e) {
            //اگه اتفاقی بیافته و فرم اجرا نشه این خط باعث میشه کل فرم اجرا نشه
            //و درواقع مقادیر پیش فرض فرم را کمسل میککنه
            e.preventDefault();
            //اون فرم رو بگیر
            var form = $(this);
            //اتریبیوت فرم رو بگیر که ما در فر ایجاد گروه محصول پست گذاشتیم
            const method = form.attr("method").toLocaleLowerCase();
            //اکشن آن فرم هم هست
            //asp-page="./Index" asp-page-handler="Create"
            //یعنی رویدادپست برای آن باید نوشت
            const url = form.attr("action");
            //اتفاقی که قرار است بعد ارز ارسال فرم رخ می دهد
            var action = form.attr("data-action");

            //اگه متد گت بود
            //const data = form.serializeArray();
            //به فرم تمام تکست نگاه کن
            if (method === "get") {
                //ما تا اینجا فثط تکست ارسال می کردیم 
                //اگه بخواهیم شی هم ارسال کنیم یا همان فایل آپلود را ازسال کنیم
                //دیگر نمی توانیم از
                //form.serializeArray();
                //استفاده کنیم
                const data = form.serializeArray();
                $.get(url,
                    data,
                    function (data) {
                        CallBackHandler(data, action, form);
                    });
            }
            ///اگه متد گت نبود یعنی پست بود این کارها  رو بکن
            else {
                //که در وقع یک فرم می گیرد با تمام آیتم ها    
                var formData = new FormData(this);
                $.ajax({
                    url: url,
                    type: "post",
                    //چهار خط زیر برای ارسال اطلاعات به جز تکست به فرم هست
                    data: formData,
                    enctype: "multipart/form-data",
                    dataType: "json",
                    //که دیتای که میگیره رو پردازش نکنه
                    processData: false,
                    //که هیچ ایرادی به دیتای ارسالی نگیرید
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

///ولیدیشن سمت کلایننت که دقییقا این متد باید همین نام باشه
jQuery.validator.addMethod("maxFileSize",
    function (value, element, params) {
        var size = element.files[0].size;
        //برای تبدیل به مگابایت
        //debugger;
        //اگه بخواهیم در جی کوئری یک تریس انجام بریم از این طریق میشه انجام داد.
        var maxSize = 3 * 1024 *1024;
        if (size > maxSize)
            return false;
        else {
            return true;
        }
    });
jQuery.validator.unobtrusive.adapters.addBool("maxFileSize");


jQuery.validator.addMethod("fileExtentionlimite",
    function (value, element, params) {
        var extention = "jpg|jpeg|png";
        var extentionfile = value.split(".").pop();
        var FileTypeInSearch = extention.includes(extentionfile);
        /*debugger;*/
        if (FileTypeInSearch === false)
            return false;
        else {
            return true;
        }
    });
jQuery.validator.unobtrusive.adapters.addBool("fileExtentionlimite");


