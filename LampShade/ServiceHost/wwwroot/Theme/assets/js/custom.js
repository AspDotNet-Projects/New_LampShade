const cookieName = "cart-items";

function addToCart(id, name, price, picture) {
    debugger;

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
    const currentProduct = products.find(x => x.id === id);
    //on Id az ghabl vojod dasht
    if (currentProduct !== undefined) {
        //migim age on kala ro az ghabl to listesh dasht age dobare ezefe kard be listesh ezafe kon
        products.find(x => x.id === id).count = parseInt(currentProduct.count) + parseInt(count);
    }
    //age az ghabl vojod nadash  , migirim vaulue hash ro
    else {
        const product = {
            id,
            name,
            unitPrice: price,
            picture,
            count
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
        //length tedad item ha ro dare
        products = JSON.parse(products);
        $("#cart_items_count").text(products.length);
        const cartItemsWrapper = $("#cart_items_wrapper");
        //cod haye html dakhelesh ro khali kon
        cartItemsWrapper.html('');
        //be ezzaye har product in code haye html ro ezafe kon
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
            //ezaje kardan Item be in tag
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
