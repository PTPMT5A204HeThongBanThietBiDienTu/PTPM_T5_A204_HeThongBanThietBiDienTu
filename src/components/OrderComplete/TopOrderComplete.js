import React from 'react'

const TopOrderComplete = () => {
    return (
        <div className='top'>
            <div className='icon text-danger'>
                <i className="fa-solid fa-cart-shopping" style={{ border: '2px solid red' }}></i>
                <p>Select product</p>
            </div>
            <span className="icon-separator text-danger fw-bold fs-5">-----</span>
            <div className='icon text-danger'>
                <i class="fa-solid fa-address-card" style={{ border: '2px solid red' }}></i>
                <p>Order information</p>
            </div>
            <span className="icon-separator text-danger fw-bold fs-5">-----</span>
            <div className='icon text-danger'>
                <i class="fa-solid fa-box-open" style={{ border: '2px solid red' }}></i>
                <p>Complete order</p>
            </div>
        </div>
    )
}

export default TopOrderComplete