import axios from 'axios';
import React from 'react'
import { useNavigate } from 'react-router-dom';
import { toast } from 'react-toastify';

const PaymentDetail = (props) => {
    const { quantity, name, productByID } = props;
    const navigate = useNavigate();
    const handleSoldOut = () => {
        toast.warn(`Phonenumber: 058 792 8264 - Email: 0995086534ts@gmail.com`);
    }

    const handleBuyNow = async () => {
        if (name === '') {
            toast.error('Bạn phải đăng nhập mới được thêm sản phẩm vào giỏ hàng !!!');
        } else {
            try {
                const dataCart = {
                    proId: productByID.id
                }
                axios.post(`http://localhost:7777/api/v1/cart/create`, dataCart)
                    .then(res => {
                        if (res && res.data.success === true) {
                            navigate('/cart');
                        } else {
                            toast.error(`Thêm sản phẩm vào giỏ hàng thất bại`);
                        }
                    }).catch(err => {
                        if (err.response.data.message === "Quantity has reached the limit") {
                            toast.warning('Đã đạt giới hạn thêm vào giỏ hàng !!! Vui lòng liên hệ: 0587928264 nếu muốn mua nhiều hơn');
                            navigate('/cart');
                        } else if (err.response.data.message === "The product is out of stock") {
                            toast.warning('Đang không có sẵn hàng !!! Vui lòng liên hệ: 0587928264 nếu muốn mua nhiều hơn');
                            navigate('/cart');
                        } else {
                            console.log(err);
                        }
                    });
            } catch (error) {
                console.error(error);
            }

        }
    }
    const handleAddtoCart = async () => {
        if (name === '') {
            toast.error('Bạn phải đăng nhập mới được thêm sản phẩm vào giỏ hàng !!!');
        } else {
            try {
                const dataCart = {
                    proId: productByID.id
                }
                axios.post(`http://localhost:7777/api/v1/cart/create`, dataCart)
                    .then(res => {
                        if (res && res.data.success === true) {
                            toast.success('Đã thêm sản phẩm vào giỏ hàng');
                        } else {
                            toast.error(`Thêm sản phẩm vào giỏ hàng thất bại`);
                        }
                    }).catch(err => {
                        if (err.response.data.message === "Quantity has reached the limit") {
                            toast.warning('Đã đạt giới hạn thêm vào giỏ hàng !!! Vui lòng liên hệ: 0587928264 nếu muốn mua nhiều hơn');
                        } else if (err.response.data.message === "The product is out of stock") {
                            toast.warning('Đang không có sẵn hàng !!! Vui lòng liên hệ: 0587928264 nếu muốn mua nhiều hơn');
                        } else {
                            console.log(err);
                        }
                    });
            } catch (error) {
                console.error(error);
            }

        }
    };
    return (
        <>
            <div className='promotion'>
                <div className='title'>
                    <h6><i class="fa-solid fa-gift mx-3"></i>Khuyến mãi</h6>
                </div>
                <div className='content'>
                    <div className='content-item my-4 mx-2'><b>1</b><p className='mx-2'>Giảm 10% khi thanh toán bằng thẻ tín dụng.</p></div>
                    <div className='content-item mx-2'><b>2</b><p className='mx-2'>Giảm thêm 5% khi có thẻ thành viên.</p></div>
                </div>
            </div>
            <div className='payment'>
                {
                    quantity > 0 ?
                        <>
                            <button className='buy-now my-2' onClick={handleBuyNow}><b>Mua ngay</b><p>(Giao hàng nhanh trong 2h hoặc nhận hàng tại cửa hàng)</p></button>
                            <button className='add-to-cart my-2 mx-2' onClick={handleAddtoCart}><i class="fa-solid fa-cart-plus"></i><p>Thêm vào giỏ hàng</p></button>
                        </> :
                        <button className='btn-sold-out col-12 my-2' onClick={handleSoldOut}><b>Sắp về hàng</b><p>(Vui lòng liên hệ trực tiếp với chúng tôi)</p></button>
                }
            </div>
        </>
    )
}

export default PaymentDetail