/* override functions api.jquery.js */
Shopify.onItemAdded = function(line_item) {
  Shopify.getCart();
};

Shopify.onCartUpdate = function(cart) {
  Shopify.cartUpdateInfo(cart, '.miniproduct ul');
};

Shopify.onError = function(XMLHttpRequest, textStatus) {
  var data = eval('(' + XMLHttpRequest.responseText + ')');
  if (!!data.message) {
    var str = data.description;
  } else {
   	var str = 'Error : ' + Shopify.fullMessagesFromErrors(data).join('; ') + '.';
  }
  jQuery('#modalAddToCartError .error_message').text(str);
  jQuery('#modalAddToCartError').modal("toggle");
}

Shopify.addItem = function(variant_id, quantity, callback) {
  var quantity = quantity || 1;
  var params = {
    type: 'POST',
    url: '/cart/add.js',
    data: 'quantity=' + quantity + '&id=' + variant_id,
    dataType: 'json',
    success: function(line_item) {
      if ((typeof callback) === 'function') {
        callback(line_item);
      }
      else {
        Shopify.cartPopap(variant_id);
        Shopify.onItemAdded(line_item);
      }
    },
    error: function(XMLHttpRequest, textStatus) {
      Shopify.onError(XMLHttpRequest, textStatus);
    }
  };
  jQuery.ajax(params);
};

Shopify.addItemFromForm = function(form_id, variant_id,callback) {
    var params = {
      type: 'POST',
      url: '/cart/add.js',
      beforeSend: function(){
        if(form_id == "add-item-qv") {
          jQuery('#' + form_id).find(".addtocartqv").html(jQuery(".quickViewModal_info .button_wait").html());
        }
      },
      data: jQuery('#' + form_id).serialize(),
      dataType: 'json',
      success: function(line_item) {
        if ((typeof callback) === 'function') {
          callback(line_item);
        }
        else {
          if(form_id != "add-item-qv") {
            Shopify.cartPopapForm(variant_id);
          } else {
          	jQuery('#' + form_id).find(".addtocartqv").html(jQuery(".quickViewModal_info .button_added").html());
            jQuery('#' + form_id).find(".addtocartqv").addClass("added_in_cart");
            setTimeout(function(){
              	jQuery('#' + form_id).find(".addtocartqv").removeClass("added_in_cart");
            	jQuery('#' + form_id).find(".addtocartqv").html(jQuery(".quickViewModal_info .button").html());
            }, 2000);
          }
          Shopify.onItemAdded(line_item);
        }
      },
      error: function(XMLHttpRequest, textStatus) {
        if(form_id != "add-item-qv") {
          Shopify.onError(XMLHttpRequest, textStatus);
        } else {
          jQuery('#' + form_id).find(".addtocartqv").html(jQuery(".quickViewModal_info .button_error").html());
          jQuery('#' + form_id).find(".addtocartqv").addClass("error_in_cart");
          setTimeout(function(){
            jQuery('#' + form_id).find(".addtocartqv").removeClass("error_in_cart");
            jQuery('#' + form_id).find(".addtocartqv").html(jQuery(".quickViewModal_info .button").html());
          }, 2000);
        }
        Shopify.onItemAdded(line_item);
      }
    };
    jQuery.ajax(params);
};

/* user functions */

Shopify.addItemFromFormStart = function(form, product_id) {
  Shopify.addItemFromForm(form, product_id);
}

Shopify.cartPopap = function(variant_id) {
  	var title = jQuery('.'+variant_id+':first .product_title').text();
  	jQuery('.productmsg').text('');
  	jQuery('#modalAddToCart').modal("toggle");
}
Shopify.cartPopapForm = function(variant_id) {
  	var title = jQuery('.product-info__title:first h2').text();
	jQuery('.productmsg').text('');
	jQuery('#modalAddToCart').modal("toggle");
}

Shopify.cartUpdateInfo = function(cart, cart_cell_id) {
  var formatMoney = window.money_format;
  if ((typeof cart_cell_id) === 'string') {
    var cart_cell = jQuery(cart_cell_id);
    if (cart_cell.length) {

      cart_cell.empty();

      jQuery.each(cart, function(key, value) {
        if (key === 'items') {
          
          if (value.length == 1) {
            jQuery(".item-content-list, .item-single-item-count").css({"display": "block"});
            jQuery(".item-empty-list, .item-multiple-item-count").css({"display": "none"});
            
            var table = jQuery(cart_cell_id);
            jQuery.each(value, function(i, item) {
              if(i > 19){
                  return false;
              }
              jQuery('<li><div class="item01 d-flex"><div class="thumb"><a href="' + item.url + '"><img src="' + item.image + '" alt="product images"></a></div><div class="content"><h6><a href="' + item.url + '">' + item.title + '</a></h6><span class="prize">' + Shopify.formatMoney(item.price, formatMoney) + '</span><div class="product_prize d-flex justify-content-between"><span class="qun">Qty: ' + item.quantity + '</span><ul class="d-flex justify-content-end"><li><a href="/cart"><i class="zmdi zmdi-settings"></i></a></li><li class="cart__item__delete"><a href="javascript:void(0);" onclick="Shopify.removeItem(' + item.variant_id + ')"><span>'+jQuery(".cart_messages .delete").text()+'</span><i class="zmdi zmdi-delete"></i></a></li></ul></div></div></div></li>').appendTo(table);
            });
          }			
          else if (value.length) {
            jQuery(".item-content-list, .item-multiple-item-count").css({"display": "block"});
            jQuery(".item-empty-list, .item-single-item-count").css({"display": "none"});
            
            var table = jQuery(cart_cell_id);
            jQuery.each(value, function(i, item) {
              if(i > 19){
                  return false;
              }
              jQuery('<li><div class="item01 d-flex"><div class="thumb"><a href="' + item.url + '"><img src="' + item.image + '" alt="product images"></a></div><div class="content"><h6><a href="' + item.url + '">' + item.title + '</a></h6><span class="prize">' + Shopify.formatMoney(item.price, formatMoney) + '</span><div class="product_prize d-flex justify-content-between"><span class="qun">Qty: ' + item.quantity + '</span><ul class="d-flex justify-content-end"><li><a href="/cart"><i class="zmdi zmdi-settings"></i></a></li><li class="cart__item__delete"><a href="javascript:void(0);" onclick="Shopify.removeItem(' + item.variant_id + ')"><span>'+jQuery(".cart_messages .delete").text()+'</span><i class="zmdi zmdi-delete"></i></a></li></ul></div></div></div></li>').appendTo(table);
            });
          }
          else {
            jQuery(".item-content-list").css({"display": "none"});
            jQuery(".item-empty-list").css({"display": "block"});
          }
        }
      });
    }
  }

  changeHtmlValue(".shopping-cart__total", Shopify.formatMoney(cart.total_price, formatMoney));
  changeHtmlValue(".bigcounter", cart.item_count);

  jQuery('.currency .active').trigger('click');
}

//Utils
function changeHtmlValue (cell, value) {
  var $cartLinkText = jQuery(cell);
  $cartLinkText.html(value);
};