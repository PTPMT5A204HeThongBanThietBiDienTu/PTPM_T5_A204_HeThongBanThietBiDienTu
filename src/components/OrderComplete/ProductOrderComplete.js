import axios from 'axios';
import React, { useCallback, useEffect, useState } from 'react'

const ProductOrderComplete = ({ billId, formatCurrency }) => {
    const [bill, setBill] = useState([]);

    const loadBill = useCallback(() => {
        axios.get(`http://localhost:1234/api/v1/billPro/getAllByBillId/${billId}`)
            .then(res => {
                if (res && res.data) {
                    setBill(res.data.data);
                }
            }).catch(err => console.log(err));
    }, [billId]);
    useEffect(() => {
        loadBill();
    }, [loadBill]);
    return (
        <div className='card d-flex flex-column p-2'>
            {
                bill.map((value) => {
                    const subTotal = value.quantity * value.price;
                    return (
                        <div className='card-complete my-2 d-flex' key={value.productid}>
                            <div className='card-image my-2'>
                                <img src={`http://localhost:1234/${value.product.img}`} alt='' />
                            </div>
                            <div className='card-content d-flex flex-column mx-3 my-2'>
                                <div className='card-name'><b>{value.product.name}</b></div>
                                <div className='price-cost d-flex'>
                                    <div className='price-title'>Giá:</div>
                                    <div className='price mx-2 fw-bold'>{formatCurrency(value.price)}</div>
                                </div>
                                <div className='card-quantity d-flex'>
                                    <div className='quantity-title'>Số lượng:</div>
                                    <div className='quantity fw-bold mx-2'>{value.quantity}</div>
                                </div>
                                <div className='card-subtotal d-flex'>
                                    <div className='subtotal-title'>Tổng:</div>
                                    <div className='subtotal mx-2 fw-bold'>{formatCurrency(subTotal)}</div>
                                </div>
                            </div>
                        </div>
                    )
                })
            }
        </div>
    )
}

export default ProductOrderComplete