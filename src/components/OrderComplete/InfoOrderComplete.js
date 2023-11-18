import axios from 'axios';
import React, { useCallback, useEffect, useState } from 'react'

const InfoOrderComplete = ({ billId, formatCurrency }) => {
    const [customer, setCustomer] = useState([]);
    const loadDataCompleteOrder = useCallback(() => {
        axios.get(`http://localhost:1234/api/v1/bill/${billId}`)
            .then(res => {
                setCustomer(res.data.data);
            }).catch(err => console.log(err));
    }, [billId])
    useEffect(() => {
        loadDataCompleteOrder();
    }, [loadDataCompleteOrder])
    return (
        <>
            {
                billId !== undefined && customer.length !== 0 ? <div className='complete-order d-flex flex-column p-3'>
                    <div className='title'>HOÀN TẤT ĐƠN HÀNG</div>
                    <table className='table table-white  my-2'>
                        <tbody>
                            <tr>
                                <td>Mã bill:</td>
                                <th>{customer.id}</th>
                            </tr>
                            <tr>
                                <td>Họ tên người nhận:</td>
                                <th>{customer.customer.name}</th>
                            </tr>
                            <tr>
                                <td>Số điện thoại:</td>
                                <th>{customer.customer.phone}</th>
                            </tr>
                            <tr>
                                <td>Địa chỉ:</td>
                                <th>{customer.customer.address}</th>
                            </tr>
                            <tr>
                                <td>Tổng tiền:</td>
                                <th className='text-danger' style={{ fontWeight: 'bold' }}>{formatCurrency(customer.total)}</th>
                            </tr>
                        </tbody>
                    </table>
                </div> : <div className='fail-order d-flex flex-column p-3'>
                    <div className='title'>ĐƠN HÀNG THẤT BẠI</div>
                </div>
            }
        </>
    )
}

export default InfoOrderComplete