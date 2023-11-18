import React from 'react'
import { useLocation } from 'react-router-dom';
import '../styles/CompleteOrder.scss';
import TopOrderComplete from '../components/OrderComplete/TopOrderComplete';
import InfoOrderComplete from '../components/OrderComplete/InfoOrderComplete';
import ProductOrderComplete from '../components/OrderComplete/ProductOrderComplete';
import ActionOrderComplete from '../components/OrderComplete/ActionOrderComplete';
const OrderComplete = () => {
    const location = useLocation();
    const { billId } = location.state || "";

    function formatCurrency(amount) {
        const formatter = new Intl.NumberFormat('vi-VN', {
            style: 'currency',
            currency: 'VND',
        });
        return formatter.format(amount);
    }
    return (
        <div className='all-complete py-4'>
            <div className='complete'>
                <div className='title'>
                    <h3 className='giohang'>Complete Order</h3>
                </div>
                <hr />
                <div className='complete-content'>
                    <div className='content'>
                        <TopOrderComplete />
                        <InfoOrderComplete billId={billId} formatCurrency={formatCurrency} />
                        <ProductOrderComplete billId={billId} formatCurrency={formatCurrency} />
                        <ActionOrderComplete />
                    </div>
                </div>
            </div>
        </div >
    )
}

export default OrderComplete