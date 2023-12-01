import axios from 'axios'
import React, { useEffect, useState } from 'react'
import "../styles/OrderHistory.scss"
import { format } from 'date-fns'
import { useNavigate } from 'react-router-dom'
import PageDoseNotExist from './Page_Does_Not_Exist/PageDoseNotExist'
const OrderHistory = ({ name }) => {
    const navigate = useNavigate()
    const [allOrder, setAllOrder] = useState([]);
    useEffect(() => {
        const getAllOrder = async () => {
            axios.get(`http://localhost:7777/api/v1/bill/getAllByUserId`)
                .then(res => {
                    if (res && res.data.success === true) {
                        setAllOrder(res.data.data);
                    }
                }).catch(err => console.log(err));
        }
        getAllOrder();
    }, []);
    let totalPrice = 0;
    if (allOrder.length > 0) {
        allOrder.map((data) => {
            totalPrice += data.total;
            return (<></>);
        })
    }
    function formatCurrency(amount) {
        const formatter = new Intl.NumberFormat('vi-VN', {
            style: 'currency',
            currency: 'VND',
        });
        return formatter.format(amount);
    }
    const handleShowDetailOrder = (id) => {
        navigate('/order-detail', { state: { id: id } })
    }
    return (
        name !== '' ?
            <div className='all-manage d-flex vh-100 bg-white align-items-center flex-column'>
                <div className='manage w-75 mt-5'>
                    <div className='title d-flex flex-column'>
                        <p className='title-item text-center'>LỊCH SỬ MUA HÀNG</p>
                        <div className='parameter d-flex justify-content-center'>
                            <div className='parameter-item d-flex flex-column text-center'>
                                <i class="fa-solid fa-truck-fast"></i>
                                <p>{allOrder.length} đơn hàng</p>
                            </div>
                            <div className='parameter-item d-flex flex-column text-center'>
                                <i class="fa-solid fa-wallet"></i>
                                <p>{formatCurrency(totalPrice)}</p>
                            </div>
                        </div>
                    </div>
                    <div className='list-order my-5'>
                        <table className={`table table-light text-xl`}>
                            <thead>
                                <tr className='text-center'>
                                    <th>Ngày đặt hàng</th>
                                    <th>Trạng thái</th>
                                    <th>Tổng tiền</th>
                                    <th></th>
                                </tr>
                            </thead>
                            {
                                allOrder.length > 0 && allOrder.map((data) => (
                                    <tbody className='text-center my-28' key={data.id} onClick={() => handleShowDetailOrder(data.id)}>
                                        <td>{format(new Date(data.createdAt), 'dd/MM/yyyy HH:mm:ss')}</td>
                                        <td>{data.status}</td>
                                        <td>{formatCurrency(data.total)}</td>
                                    </tbody>
                                ))
                            }

                        </table>
                    </div>
                </div>
            </div> : <PageDoseNotExist />
    )
}

export default OrderHistory