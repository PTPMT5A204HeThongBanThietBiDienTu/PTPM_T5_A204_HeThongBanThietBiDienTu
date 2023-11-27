import React from 'react'

const TopOrderInfo = () => {
    return (
        <div className='top'>
            <div className='icon text-danger'>
                <i className="fa-solid fa-cart-shopping" style={{ border: '2px solid red' }}></i>
                <p>Chọn sản phẩm</p>
            </div>
            <span className="icon-separator text-danger fw-bold fs-5">-----</span>
            <div className='icon text-danger'>
                <i class="fa-solid fa-address-card" style={{ border: '2px solid red' }}></i>
                <p>Thông tin đặt hàng</p>
            </div>
            {/* <span className="icon-separator fw-bold fs-5">-----</span>
            <div className='icon'>
                <i class="fa-solid fa-ticket" style={{ border: '2px solid #222' }}></i>
                <p>Mã giảm giá</p>
            </div> */}
            <span className="icon-separator fw-bold fs-5">-----</span>
            <div className='icon'>
                <i class="fa-solid fa-credit-card" style={{ border: '2px solid #222' }}></i>
                <p>Thanh toán</p>
            </div>
            <span className="icon-separator fw-bold fs-5">-----</span>
            <div className='icon'>
                <i class="fa-solid fa-box-open" style={{ border: '2px solid #222' }}></i>
                <p>Hoàn tất đơn hàng</p>
            </div>
        </div>
    )
}

export default TopOrderInfo