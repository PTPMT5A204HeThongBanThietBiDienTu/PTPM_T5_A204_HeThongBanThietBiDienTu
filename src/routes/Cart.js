import axios from 'axios';
import React, { useCallback, useEffect, useState } from 'react'
import { Link, useNavigate } from 'react-router-dom';
import '../styles/Cart.scss';
import ProductCart from '../components/cart/ProductCart';
import EmptyCart from '../components/cart/EmptyCart';
const Cart = () => {
    const navigate = useNavigate();
    const [cart, setCart] = useState([]);
    const loadCart = useCallback(() => {
        axios.get(`http://localhost:1234/api/v1/cart/`)
            .then(res => {
                if (res && res.data) {
                    setCart(res.data.data);
                }
            }).catch(err => console.log(err));
    }, []);
    useEffect(() => {
        loadCart();
    }, [loadCart]);
    let totalPrice = 0;
    function formatCurrency(amount) {
        const formatter = new Intl.NumberFormat('vi-VN', {
            style: 'currency',
            currency: 'VND',
        });
        return formatter.format(amount);
    }
    if (cart.length > 0) {
        cart.map((data) => {
            totalPrice += data.quantity * data.product.price;
            return (<></>);
        })
    }

    return (
        <>
            {
                cart.length > 0 ? (
                    <div className='all-cart py-4'>
                        <div className='cart'>
                            <div className='title'>
                                <a href='/'><h3 className='back'>Trở về</h3></a>
                                <h3 className='giohang'>Giỏ hàng</h3>
                            </div>
                            <hr />
                            <ProductCart cart={cart} loadCart={loadCart} formatCurrency={formatCurrency} />
                            <div className='payment'>
                                <div className='total'>
                                    <div className='title'><b>Tổng tiền:</b></div>
                                    <div className='total'><b>{formatCurrency(totalPrice)}</b></div>
                                </div>
                                <div className='button-payment'><button onClick={() => navigate(`/order-information`)}>Đặt hàng ngay</button></div>
                                <Link to={'/'} style={{ textDecoration: 'none', color: '#222' }}><div className='button-buy'><button>Tiếp tục mua hàng</button></div></Link>
                            </div>
                        </div >
                    </div >
                ) : (
                    <EmptyCart />
                )
            }
        </>
    )
}

export default Cart