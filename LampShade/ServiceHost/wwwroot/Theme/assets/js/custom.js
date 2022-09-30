
const cookieName = "cart-items";

function addToCart(id, name, price, picture,slug) {
    //debugger;

    //gereftan cookie be name cart-item 
    let products = $.cookie(cookieName);
    //emkan dare asasan cart-item az ghabl vojod nadashte bashe age vojod nadash ono mikone ye Arratye empty
    if (products === undefined) {
        products = [];
    } else {
        //chon cookie az no e string hast tabdil mikonim be json
        //item pars product ro tabdil mokone be ye Object
        products = JSON.parse(products);
    }
    //oni ke Id barabar ba productCount , valuesho migire 
    const count = $("#productCount").val();
    //gereftane id product jari
    const currentProduct = products.find(x => parseInt(x.id) === parseInt(id));
    //on Id az ghabl vojod dasht
    if (currentProduct !== undefined) {
        //migim age on kala ro az ghabl to listesh dasht age dobare ezefe kard be listesh ezafe kon
        products.find(x => parseInt(x.id) === parseInt(id)).count = parseInt(currentProduct.count) + parseInt(count);
    }
    //age az ghabl vojod nadash  , migirim vaulue hash ro
    else {
        const product = {
            id,
            name,
            unitPrice: price,
            picture,
            count,
            slug
        }
        //ezafe kardane entekhabe jadid be list ghabli
        products.push(product);
    }
    //save dar cookie var zaman expire va masir ke ba path da masir root yane "/" ast
    //dar baghe Update cookie ast
    $.cookie(cookieName, JSON.stringify(products), { expires: 2, path: "/" });
    updateCart();
}

function updateCart() {
    let products = $.cookie(cookieName);
    products = JSON.parse(products);
    $("#cart_items_count").text(products.length);
    const cartItemsWrapper = $("#cart_items_wrapper");
    cartItemsWrapper.html('');
    products.forEach(x => {
        const product = `<div class="single-cart-item">
                            <a href="javascript:void(0)" class="remove-icon" onclick="removeFromCart('${x.id}')">
                                <i class="ion-android-close"></i>
                            </a>
                            <div class="image">
                                <a href="single-product.html">
                                    <img src="/ProductPictures/${x.picture}"
                                         class="img-fluid" alt="">
                                </a>
                            </div>
                            <div class="content">
                                <p class="product-title">
                                    <a href="single-product.html">محصول: ${x.name}</a>
                                </p>
                                <p class="count">تعداد: ${x.count}</p>
                                <p class="count">قیمت واحد: ${x.unitPrice}</p>
                            </div>
                        </div>`;

        cartItemsWrapper.append(product);
    });
    }

function removeFromCart(id) {
    let products = $.cookie(cookieName);
    //length tedad item ha ro dare
    products = JSON.parse(products);
    //find kardan oni ke id barabar ba paraneter vordi
    const itemToRemove = products.findIndex(x => x.id === id);
    //mige az on be bad ro chanta hazf konam
    products.splice(itemToRemove, 1);
    //save dar cookie var zaman expire va masir ke ba path da masir root yane "/" ast
    //dar baghe Update cookie ast
    $.cookie(cookieName, JSON.stringify(products), { expires: 2, path: "/" });
    updateCart();


}

function changeCartItemCount(id, totalId, count) {
   
    var products = $.cookie(cookieName);
    products = JSON.parse(products);
    const productIndex = products.findIndex(x => x.id == id);
    products[productIndex].count = count;
    const product = products[productIndex];
    const newPrice = parseInt(product.unitPrice) * parseInt(count);
    $(`#${totalId}`).text(newPrice);
    //products[productIndex].totalPrice = newPrice;
    $.cookie(cookieName, JSON.stringify(products), { expires: 2, path: "/" });
    updateCart();

    //const data = {
    //    'productId': parseInt(id),
    //    'count': parseInt(count)
    //};

    //$.ajax({
    //    url: url,
    //    type: "post",
    //    data: JSON.stringify(data),
    //    contentType: "application/json",
    //    dataType: "json",
    //    success: function (data) {
    //        if (data.isInStock == false) {
    //            const warningsDiv = $('#productStockWarnings');
    //            if ($(`#${id}-${colorId}`).length == 0) {
    //                warningsDiv.append(`<div class="alert alert-warning" id="${id}-${colorId}">
    //                    <i class="fa fa-exclamation-triangle"></i>
    //                    <span>
    //                        <strong>${data.productName} - ${color
    //                    } </strong> در حال حاضر در انبار موجود نیست. <strong>${data.supplyDays
    //                    } روز</strong> زمان برای تامین آن نیاز است. ادامه مراحل به منزله تایید این زمان است.
    //                    </span>
    //                </div>
    //                `);
    //            }
    //        } else {
    //            if ($(`#${id}-${colorId}`).length > 0) {
    //                $(`#${id}-${colorId}`).remove();
    //            }
    //        }
    //    },
    //    error: function (data) {
    //        alert("خطایی رخ داده است. لطفا با مدیر سیستم تماس بگیرید.");
    //    }
    //});


    const settings = {
        "url": "https://localhost:1369/api/Inventory",
        "method": "POST",
        "timeout": 0,
        "headers": {
            "Content-Type": "application/json"
        },
        "data": JSON.stringify({ "productId": id, "count": count })
    };
    //  $.ajax(settings).done yani age anjam shod 
    $.ajax(settings).done(function (data) {
        debugger;
        if (data.isInStock == false) {
            const warningsDiv = $('#productStockWarnings');
            if ($(`#${id}`).length == 0) {
                warningsDiv.append(`
                    <div class="alert alert-warning" id="${id}">
                        <i class="fa fa-warning"></i> کالای
                        <strong>${data.productName}</strong>
                        در انبار کمتر از تعداد درخواستی موجود است.
                    </div>
                `);
            }
        } else {
            if ($(`#${id}`).length > 0) {
                $(`#${id}`).remove();
            }
        }
    });
}
