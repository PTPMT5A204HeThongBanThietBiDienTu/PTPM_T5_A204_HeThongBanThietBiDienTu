import React, { useCallback, useState } from 'react';
import '../styles/Payment.scss';
import { useLocation, useNavigate } from 'react-router-dom';
import axios from 'axios';
import vnpay from '../assets/logo/vnpay.png';
import moca from '../assets/logo/moca.png';
import onepay from '../assets/logo/onepay.png';
import zalopay from '../assets/logo/zalopay.png';
import shopeepay from '../assets/logo/shopeepay.png';
import kredivo from '../assets/logo/kredivo.png';
import { toast } from 'react-toastify';
import swal from 'sweetalert';

const Payment = () => {
    const location = useLocation();
    const { dataOrderInfo, total } = location.state || {};
    const [selectedItem, setSelectedItem] = useState('');
    const navigate = useNavigate();
    const handleItemClick = useCallback((item) => {
        setSelectedItem(item);
    }, []);
    const handleSubmit = async (event) => {
        event.preventDefault();
        if (selectedItem) {
            axios.post(`http://localhost:7777/api/v1/bill/create`, dataOrderInfo)
                .then(res => {
                    if (res && res.data.success === true) {
                        const billId = res.data.data.id;
                        axios.delete(`http://localhost:7777/api/v1/cart/deleteByUserId`)
                            .then(res => {
                                if (res && res.data.success === true) {
                                    swal({
                                        title: "Đặt hàng thành công!",
                                        icon: "success",
                                    }).then(() => {
                                        navigate('/order-complete', { state: { billId: billId } });
                                    });
                                }
                            }).catch(err => console.log(err));
                    }
                }).catch(err => console.log(err));
        } else {
            toast.error(`Vui lòng chọn phương thức thanh toán !!!`);
        }
    };
    function formatCurrency(amount) {
        const formatter = new Intl.NumberFormat('vi-VN', {
            style: 'currency',
            currency: 'VND',
        });
        return formatter.format(amount);
    }
    return (
        <div className='all-payment py-4'>
            <div className='payment'>
                <div className='title'>
                    <h3 className='giohang'>Thanh Toán</h3>
                </div>
                <hr />
                <div className='payment-content'>
                    <div className='content'>
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
                            <span className="icon-separator text-danger fw-bold fs-5">-----</span>
                            <div className='icon text-danger'>
                                <i class="fa-solid fa-credit-card" style={{ border: '2px solid red' }}></i>
                                <p>Thanh toán</p>
                            </div>
                            <span className="icon-separator fw-bold fs-5">-----</span>
                            <div className='icon'>
                                <i class="fa-solid fa-box-open" style={{ border: '2px solid #222' }}></i>
                                <p>Hoàn tất đơn hàng</p>
                            </div>
                        </div>
                        <div className='order-information'>
                            <div className='center d-flex flex-column'>
                                <div className='center-title w-100 text-center'><h1>Thông tin đơn hàng</h1></div>
                                <table className='table'>
                                    <tbody>
                                        <tr>
                                            <th>Tên:</th>
                                            <td>{dataOrderInfo.name}</td>
                                        </tr>
                                        <tr>
                                            <th>Số điện thoại:</th>
                                            <td>{dataOrderInfo.phone}</td>
                                        </tr>
                                        <tr>
                                            <th>Địa chỉ:</th>
                                            <td>{dataOrderInfo.address}</td>
                                        </tr>
                                        <tr>
                                            <th>Total price:</th>
                                            <td className='text-danger' style={{ fontWeight: 'bold' }}>{formatCurrency(total)}</td>
                                        </tr>
                                    </tbody>
                                </table>
                            </div>
                        </div>
                        {
                            dataOrderInfo.total !== 0 ? (
                                <form onSubmit={handleSubmit}>
                                    <div className='payment-method d-flex flex-column p-3'>
                                        <div className='method-item d-flex'>
                                            <div
                                                className={`item d-flex flex-column ${selectedItem === 'Payment at the store' ? 'selected' : ''}`}
                                                style={{ marginRight: '4%' }}
                                                onClick={() => handleItemClick('Payment at the store')}
                                            >
                                                <div className='title'><p>Thanh toán khi nhận hàng</p></div>
                                                <div className='icon mx-2'><i class="fa-solid fa-store text-danger"></i></div>
                                                <i class="fa-solid fa-circle-check"></i>
                                            </div>
                                            <div className={`item d-flex flex-column ${selectedItem === 'Transfer' ? 'selected' : ''}`} onClick={() => handleItemClick('Transfer')}>
                                                <div className='title'><p>Chuyển khoản</p></div>
                                                <div className='icon'><i class="fa-solid fa-money-bill-transfer text-success"></i></div>
                                                <i class="fa-solid fa-circle-check"></i>
                                            </div>
                                        </div>
                                        <div className='method-item d-flex'>
                                            <div
                                                className={`item d-flex flex-column ${selectedItem === 'Payment via vnpay' ? 'selected' : ''}`}
                                                style={{ marginRight: '4%' }}
                                                onClick={() => handleItemClick('Payment via vnpay')}
                                            >
                                                <div className='title'><p>VN Pay</p></div>
                                                <div className='icon'><img src={vnpay} alt='' /></div>
                                                <i class="fa-solid fa-circle-check"></i>
                                            </div>
                                            <div className={`item d-flex flex-column ${selectedItem === 'Payment via moca' ? 'selected' : ''}`} onClick={() => handleItemClick('Payment via moca')}>
                                                <div className='title'><p>Moca</p></div>
                                                <div className='icon'><img src={moca} alt='' style={{ scale: '0.8' }} /></div>
                                                <i class="fa-solid fa-circle-check"></i>
                                            </div>
                                        </div>
                                        <div className='method-item d-flex'>
                                            <div
                                                className={`item d-flex flex-column ${selectedItem === 'Payment via onepay' ? 'selected' : ''}`}
                                                style={{ marginRight: '4%' }}
                                                onClick={() => handleItemClick('Payment via onepay')}
                                            >
                                                <div className='title'><p style={{ fontSize: '0.9rem' }}>Thanh toán bằng thẻ Visa/Master/JCB/Napas</p></div>
                                                <div className='icon'><img src={onepay} alt='' style={{ scale: '3' }} /></div>
                                                <i class="fa-solid fa-circle-check"></i>
                                            </div>
                                            <div className={`item d-flex flex-column ${selectedItem === 'Payment via zalopay' ? 'selected' : ''}`} onClick={() => handleItemClick('Payment via zalopay')}>
                                                <div className='title'><p>Zalo Pay</p></div>
                                                <div className='icon'><img src={zalopay} alt='' style={{ scale: '0.8' }} /></div>
                                                <i class="fa-solid fa-circle-check"></i>
                                            </div>
                                        </div>
                                        <div className='method-item d-flex'>
                                            <div
                                                className={`item d-flex flex-column ${selectedItem === 'Payment via shopeepay' ? 'selected' : ''}`}
                                                style={{ marginRight: '4%' }}
                                                onClick={() => handleItemClick('Payment via shopeepay')}
                                            >
                                                <div className='title'><p>Shopee Pay</p></div>
                                                <div className='icon'><img src={shopeepay} alt='' /></div>
                                                <i class="fa-solid fa-circle-check"></i>
                                            </div>
                                            <div className={`item d-flex flex-column ${selectedItem === 'Payment via kredivo' ? 'selected' : ''}`} onClick={() => handleItemClick('Payment via kredivo')}>
                                                <div className='title'><p>Kredivo</p></div>
                                                <div className='icon'> <img src={kredivo} alt='' style={{ scale: '0.8' }} /></div>
                                                <i class="fa-solid fa-circle-check"></i>
                                            </div>
                                        </div>
                                    </div>
                                    <div className='payment'>
                                        <div className='button-payment'><button type="submit">Tiếp tục</button></div>
                                    </div>
                                </form>
                            ) : <></>
                        }
                    </div>
                </div>
            </div>
        </div>
    )
}

export default Payment