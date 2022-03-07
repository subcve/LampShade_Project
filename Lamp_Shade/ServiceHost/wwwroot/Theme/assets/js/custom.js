const cookieName = "cart-items";
function addToCart(id, name, price, picture) {
	let products = $.cookie(cookieName);
	if (products === undefined) {
		products = [];
	} else {
		products = JSON.parse(products);
	}
	const count = $("#productCount").val();
	const currentProduct = products.find(c => c.id === id);
	if (currentProduct !== undefined) {
		products.find(c => c.id === id).count = parseInt(currentProduct.count) + parseInt(count);
	} else {
		const product = {
			id,
			name,
			unitPrice: price,
			picture,
			count,
		}
		products.push(product);
	}
	$.cookie(cookieName, JSON.stringify(products), { expiers: 1, path: "/" });
	updateCart();
}
function updateCart() {
	let products = $.cookie(cookieName);
	products = JSON.parse(products);
	$("#cart-items-count").text(products.length);
	const cartItemsWrapper = $("#cart_items_wapper");
	cartItemsWrapper.html('');
	for (var item of products) {
		const product = `<div class="single-cart-item">
							<a href="javascript:void(0)" class="remove-icon" onclick="removeFromCart(${item.id})">
								<i class="ion-android-close"></i>
							</a>
							<div class="image">
								<a href="single-product.html">
									<img src="${item.picture}"
										 class="img-fluid" alt="">
								</a>
							</div>
							<div class="content">
								<p class="product-title">
									<a href="single-product.html">محصول : ${item.name}</a>
								</p>
								<p class="count">
								<span>${item.count}</span>
								${item.unitPrice}</p>
							</div>
						</div>`;
		cartItemsWrapper.append(product);
	}
}
function removeFromCart(id) {
	let products = $.cookie(cookieName);
	products = JSON.parse(products);
	let itemToRemove = products.findIndex(x => x.id === id);
	products.splice(itemToRemove, 1);
	$.cookie(cookieName, JSON.stringify(products), { expiers: 1, path: "/" });
	updateCart();
}

function UpdateCartItemCount(id, totalPriceId, count) {
	let products = $.cookie(cookieName);
	products = JSON.parse(products);
	let productIndex = products.findIndex(c => c.id == id);
	products[productIndex].count = count;
	let product = products[productIndex];
	const newPrice = parseInt(product.unitPrice) * parseInt(count);
	$(`#${totalPriceId}`).text(newPrice);
	$.cookie(cookieName, JSON.stringify(products), { expiers: 1, path: "/" });
	updateCart();
}