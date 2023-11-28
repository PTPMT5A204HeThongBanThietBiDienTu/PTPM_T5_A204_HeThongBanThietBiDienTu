import React from 'react'
import { Link } from 'react-router-dom'

const ActionOrderComplete = () => {
    return (
        <div className='action d-flex p-2'>
            <Link to={'/order-history'} className='check-order btn btn-primary p-3 d-flex flex-column'>
                <div className='title'>
                    Kiểm tra đơn hàng của bạn
                </div>
                <div className='icon my-1'>
                    <i class="fa-solid fa-circle-check"></i>
                </div>
            </Link>
            <div className='continue-shoping'>
                <Link to={'/'} className='btn btn-danger p-3 d-flex flex-column'>
                    <div className='title'>
                        Tiếp tục mua hàng
                    </div>
                    <div className='icon my-1'>
                        <i class="fa-solid fa-cart-plus"></i>
                    </div>
                </Link>
            </div>
        </div>
    )
}

export default ActionOrderComplete