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
  
}