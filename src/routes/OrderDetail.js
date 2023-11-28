import axios from 'axios';
import React, { useState } from 'react'
import "../styles/OrderDetail.scss"
import { Link, useLocation } from 'react-router-dom';
import { toast } from 'react-toastify';
import PageDoseNotExist from './Page_Does_Not_Exist/PageDoseNotExist';

const OrderDetail = ({ name }) => {
    const location = useLocation();
    const { id } = location.state || {}
    const [orderDetail, setOrderDetail] = useState([]);
    const [customer, setCustomer] = useState([])
    useState(() => {
        axios.get(`http://localhost:7777/api/v1/billPro/getAllByBillId/${id}`)
            .then(res => {
                if (res && res.data.success === true) {
                    setOrderDetail(res.data.data);
                }
            }).catch(err => console.log(err));
        axios.get(`http://localhost:7777/api/v1/bill/getAllByUserId`)
            .then(res => {
                if (res && res.data.success === true) {
                    setCustomer(res.data.data);
                }
            }).catch(err => console.log(err));
    }, [id])

    const handleAddtoCart = async (id) => {
        if (name === '') {
            toast.error('Bạn phải đăng nhập mới được thêm sản phẩm vào giỏ hàng !!!');
        } else {
            try {
                const dataCart = {
                    proId: id
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
    const handleBuyAllAgain = async () => {
        try {
            orderDetail.forEach((product) => {
                const dataCart = {
                    proId: product.proId
                }
                axios.post(`http://localhost:7777/api/v1/cart/create`, dataCart)
                    .then(res => {
                        if (res && res.data.success === true) {

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
            });
            toast.success('Mua lại sản phẩm thành công')
        } catch (error) {
            console.error('Error fetching order quantity:', error);
        }
    }
    function formatCurrency(amount) {
        const formatter = new Intl.NumberFormat('vi-VN', {
            style: 'currency',
            currency: 'VND',
        });
        return formatter.format(amount);
    }
    let totalPrice = 0;
    console.log(id);
    return (
        orderDetail.length > 0 && id !== undefined ?
            (
                <div className='all-orderdetail py-4'>
                    <div className='orderdetail'>
                        <div className='title'>
                            <Link to={`/order-history`}><h3 className='back'>Trở về</h3></Link>
                            <h3 className='giohang'>Chi tiết đơn hàng</h3>
                        </div>

                        <hr />

                        <div className='card d-flex flex-column p-2'>
                            {
                                orderDetail.length > 0 ? orderDetail.map((order) => {
                                    const subTotal = order.quantity * order.price;
                                    return (
                                        <div className='card-orderdetail my-2 d-flex' key={order.id}>
                                            <div className='card-image my-2'>
                                                <img src={`http://localhost:7777/${order.product.img}`} alt='' />
                                            </div>
                                            <div className='card-content d-flex flex-column mx-3 my-2'>
                                                <div className='card-name'><b>{order.product.name}</b></div>
                                                <div className='price-cost d-flex'>
                                                    <div className='price-title'>Giá:</div>
                                                    <div className='price mx-2 fw-bold'>{formatCurrency(order.price)}</div>
                                                </div>
                                                <div className='card-quantity d-flex'>
                                                    <div className='quantity-title'>Số lượng:</div>
                                                    <div className='quantity fw-bold mx-2'>{order.quantity}</div>
                                                </div>
                                                <div className='card-subtotal d-flex'>
                                                    <div className='subtotal-title'>Thành tiền:</div>
                                                    <div className='subtotal mx-2 fw-bold'>{formatCurrency(subTotal)}</div>
                                                </div>
                                            </div>
                                            <button onClick={() => handleAddtoCart(order.proId)}>Mua lại</button>
                                        </div>
                                    )
                                }) : <h3 className='text-center text-danger'>Sản phẩm không tồn tại</h3>
                            }
                        </div>
                        <div className='info-payment d-flex flex-column p-2'>
                            <div class="title my-2 mx-2">
                                <p><i class="fa-solid fa-money-check-dollar"></i> Thông tin thanh toán</p>
                            </div>


                            {
                                orderDetail.length > 0 && (
                                    <table className='table '>
                                        <tr>
                                            <th>Tổng tiền:</th>
                                            <td>{
                                                orderDetail.map((order) => {
                                                    totalPrice += order.price * order.quantity;
                                                    return <></>
                                                })
                                            }<b>{formatCurrency(totalPrice)}</b></td>
                                        </tr>
                                        <tr>
                                            <th>Phí vận chuyển:</th>
                                            <td>Miễn phí</td>
                                        </tr>
                                        <tr>
                                            <td colspan="4">
                                                <div class="hr-container">
                                                    <div class="hr-line"></div>
                                                </div>
                                            </td>
                                        </tr>

                                        <tr>
                                            <th>Số tiền phải trả:</th>
                                            <td><b>{formatCurrency(totalPrice)}</b></td>
                                        </tr>
                                    </table>
                                )
                            }
                        </div>
                        <div className='info-customer d-flex flex-column p-2'>
                            <div class="title my-2 mx-2">
                                <p><i class="fa-solid fa-circle-info"></i>    Thông tin người nhận</p>
                            </div>
                            <div className='content mx-2'>
                                <p><i class="fa-solid fa-user"></i> {customer[0].customer.name}</p>
                                <p><i class="fa-solid fa-phone"></i> {customer[0].customer.phone}</p>
                                <p><i class="fa-solid fa-location-dot"></i> {customer[0].customer.address}</p>
                            </div>
                        </div>

                        <div className='clearfix'>
                            <div className='cancel-order my-2 border' style={{ float: 'right' }}>
                                <button className='btn btn-primary hover' onClick={handleBuyAllAgain}>Mua lại hết</button>
                            </div>
                        </div>

                    </div>
                </div>
            ) : <PageDoseNotExist />
    )
}

export default OrderDetail