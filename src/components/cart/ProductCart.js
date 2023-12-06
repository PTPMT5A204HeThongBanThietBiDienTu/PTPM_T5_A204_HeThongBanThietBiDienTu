import axios from 'axios';
import React, { useState } from 'react'
import { Link } from 'react-router-dom';
import { toast } from 'react-toastify';
import classNames from 'classnames';
import Cookies from 'js-cookie';
const ProductCart = (props) => {
    const { cart, loadCart, formatCurrency, name, setCart, setCartCookie } = props;
    const [hoveredItems, setHoveredItems] = useState('');
    const handleIconHover = (index) => {
        setHoveredItems((prevHoveredItems) => {
            const updatedHoveredItems = [...prevHoveredItems];
            updatedHoveredItems[index] = true;
            return updatedHoveredItems;
        });
    };

    const handleIconLeave = (index) => {
        setHoveredItems((prevHoveredItems) => {
            const updatedHoveredItems = [...prevHoveredItems];
            updatedHoveredItems[index] = false;
            return updatedHoveredItems;
        });
    };

    const handleDeleteCartCookie = (proId) => {
        const currentCartCookie = Cookies.get('CartCookie');
        const cartCookieArray = currentCartCookie ? JSON.parse(currentCartCookie) : [];
        const updatedCart = cartCookieArray.filter(item => item.proId !== proId);
        setCartCookie(updatedCart)
        setCart(updatedCart);
        Cookies.set('CartCookie', JSON.stringify(updatedCart), { expires: 7 });
        toast.success('Đã xóa sản phẩm ra khỏi giỏ hàng');
    }
    const handleDeleteCart = async (cartId) => {
        axios.delete(`http://localhost:7777/api/v1/cart/${cartId}`)
            .then(res => {
                if (res && res.data.success === true) {
                    toast.success('Đã xóa sản phẩm ra khỏi giỏ hàng');
                    loadCart();
                }
            }).catch(err => console.log(err));
    }
    const handleQuantityChangeCartCookie = (event, data) => {
        const newQuantity = Number(event.target.value);
        const currentCartCookie = Cookies.get('CartCookie');
        const cartCookieArray = currentCartCookie ? JSON.parse(currentCartCookie) : [];
        const existingItemIndex = cartCookieArray.findIndex(item => item.proId === data.proId);

        if (existingItemIndex !== -1) {
            // const maxProductQuantity = data.product.quantity;
            const limitedQuantity = Math.min(newQuantity, 1000);

            if (limitedQuantity <= 0) {
                handleDeleteCartCookie(data.proId);
            } else {
                setCart(prevCart => {
                    const updatedCart = [...prevCart];
                    updatedCart[existingItemIndex].quantity = limitedQuantity;
                    return updatedCart;
                });

                cartCookieArray[existingItemIndex].quantity = limitedQuantity;
                Cookies.set('CartCookie', JSON.stringify(cartCookieArray), { expires: 7 });
            }
        }
    };



    const handleQuantityChange = (event, data) => {
        const newQuantity = Math.max(parseInt(event.target.value, 10), 0);
        updateQuantityCart(newQuantity, data.id);
    };
    const updateQuantityCart = (newQuantity, cartId) => {
        if (newQuantity > 1000) {
            newQuantity = 1000
        }
        const dataUpdateCart = {
            id: cartId,
            quantity: newQuantity,
        }
        if (newQuantity > 0) {
            axios.patch(`http://localhost:7777/api/v1/cart/${cartId}`, dataUpdateCart)
                .then(res => {
                    if (res && res.data.success === true) {
                        loadCart();
                    }
                }).catch(err => {
                    if (err.response.data.message === "Quantity has reached the limit") {
                        toast.warning('Đã đạt giới hạn thêm vào giỏ hàng !!! Vui lòng liên hệ: 0587928264 nếu muốn mua nhiều hơn');
                    } else if (err.response.data.message === "The rest quantity of product is not enough") {
                        toast.warning('Đang không có sẵn hàng !!! Vui lòng liên hệ: 0587928264 nếu muốn mua nhiều hơn');
                    } else {
                        console.log(err);
                    }
                });
        } else if (newQuantity < 1) {
            handleDeleteCart(cartId);
        }
    }
    let subTotal = 0;
    return (
        <div className='cart-center'>
            {
                cart.map((data) => {
                    subTotal = data.quantity * data.product.price;
                    return (
                        <div className='card-cart' key={data.userId}>
                            <div className='card-image'>
                                <img src={`http://localhost:7777/${data.product.img}`} alt='' />
                            </div>
                            <div className='card-info'>
                                <div className='card-name'><Link style={{ textDecoration: 'none', color: '#666' }} to={`/product/${data.product.id}`}><b>{data.product.name}</b></Link></div>
                                <div className='card-price'>
                                    <div className='price text-danger'><b>{formatCurrency(data.product.price)}</b></div>
                                </div>
                            </div>
                            <div className='card-action'>
                                <div className='trash' onClick={() => {
                                    if (name !== '') {
                                        handleDeleteCart(`${data.id}`)
                                    } else {
                                        handleDeleteCartCookie(data.proId)
                                    }
                                }}>
                                    <i
                                        className={classNames('fa-solid', 'fa-trash', { 'fa-shake': hoveredItems[data.productid] })}
                                        onMouseEnter={() => handleIconHover(data.productid)}
                                        onMouseLeave={() => handleIconLeave(data.productid)}
                                    ></i>
                                </div>
                                <div className='quantity'>
                                    <input
                                        type="number"
                                        min={'0'}
                                        className='form-control'
                                        value={data.quantity}
                                        onChange={(e) => {
                                            if (name !== '') {
                                                handleQuantityChange(e, data)
                                            } else {
                                                handleQuantityChangeCartCookie(e, data)
                                            }
                                        }} />
                                </div>
                            </div>
                            <div className='subtotal'><b>Tổng: {formatCurrency(subTotal)}</b></div>
                        </div>
                    )
                })
            }
        </div>
    )
}

export default ProductCart