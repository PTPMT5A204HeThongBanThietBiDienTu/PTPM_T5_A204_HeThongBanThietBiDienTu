import React from 'react'
import { Link } from 'react-router-dom';

const PaymentOrderInfo = (props) => {
    const { cart } = props;
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
                    <div className='payment'>
                        <div className='total'>
                            <div className='title'><b>Tổng tiền:</b></div>
                            <div className='total'><b>{formatCurrency(totalPrice)}</b></div>
                        </div>
                        <div className='button-payment'><button type="submit">Tiếp tục</button></div>
                        <Link to={'/'} style={{ textDecoration: 'none', color: '#222' }}><div className='button-buy'><button>Chọn thêm sản phẩm</button></div></Link>
                    </div>
                ) : <div className='fail-order d-flex flex-column p-3'>
                    <div className='title'>KHÔNG CÓ SẢN PHẨM TRONG GIỎ HÀNG</div>
                </div>
            }
        </>
    )
}

export default PaymentOrderInfo