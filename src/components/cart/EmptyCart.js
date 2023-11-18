import React from 'react'

const EmptyCart = () => {
    return (
        <div className='all-cart py-4'>
            <div className='cart'>
                <div className='title'>
                    <a href='/'><h3 className='back'>Trờ về</h3></a>
                    <h3 className='giohang'>Giỏ hàng</h3>
                </div>
                <hr />
                <div className='cart-content'>
                    <div className='cart-empty'>
                        <div className='icon-sad my-5'><i class="fa-solid fa-face-frown fa-beat"></i></div>
                        <p>Không có sản phẩm trong giỏ hàng, vui lòng quay lại</p>
                        <a href='/' className='btn btn-danger my-5'>Trở về trang chủ</a>
                    </div>
                </div>
            </div>
        </div>
    )
}

export default EmptyCart