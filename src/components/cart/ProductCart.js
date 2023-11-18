import axios from 'axios';
import React, { useState } from 'react'
import { Link } from 'react-router-dom';
import { toast } from 'react-toastify';
import classNames from 'classnames';
const ProductCart = (props) => {
    const { cart, loadCart, formatCurrency } = props;
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
    const handleDeleteCart = async (cartId) => {
        axios.delete(`http://localhost:1234/api/v1/cart/${cartId}`)
            .then(res => {
                if (res && res.data.success === true) {
                    toast.success('Đã xóa sản phẩm ra khỏi giỏ hàng');
                    loadCart();
                }
            }).catch(err => console.log(err));
    }
    const handleQuantityChange = (event, data) => {
        const newQuantity = Math.max(parseInt(event.target.value, 10), 0);
        updateQuantityCart(newQuantity, data.id);
    };
    const updateQuantityCart = (newQuantity, cartId) => {
        const dataUpdateCart = {
            id: cartId,
            quantity: newQuantity,
        }
        if (newQuantity > 0) {
            axios.patch(`http://localhost:1234/api/v1/cart/${cartId}`, dataUpdateCart)
                .then(res => {
                    if (res && res.data.success === true) {
                        loadCart();
                    }
                }).catch(err => {
                    if (err.response.data.message === "Quantity has reached the limit") {
                        toast.warning('Đã đạt giới hạn thêm vào giỏ hàng !!! Vui lòng liên hệ: 0587928264 nếu muốn mua nhiều hơn');
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
                                <img src={`http://localhost:1234/${data.product.img}`} alt='' />
                            </div>
                            <div className='card-info'>
                                <div className='card-name'><Link style={{ textDecoration: 'none', color: '#666' }} to={`/product/${data.product.id}`}><b>{data.product.name}</b></Link></div>
                                <div className='card-price'>
                                    <div className='price text-danger'><b>{formatCurrency(data.product.price)}</b></div>
                                </div>
                            </div>
                            <div className='card-action'>
                                <div className='trash' onClick={() => handleDeleteCart(`${data.id}`)}>
                                    <i
                                        className={classNames('fa-solid', 'fa-trash', { 'fa-shake': hoveredItems[data.productid] })}
                                        onMouseEnter={() => handleIconHover(data.productid)}
                                        onMouseLeave={() => handleIconLeave(data.productid)}
                                    ></i>
                                </div>
                                <div className='quantity'>
                                    <input type="number" min={'0'} className='form-control' value={data.quantity} onChange={(e) => handleQuantityChange(e, data)} />
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