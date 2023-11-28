import React from 'react'
import { Link } from 'react-router-dom'
import "../styles/Profile.scss"
const Profile = () => {
    return (
        <div className='all-info d-flex vh-100 justify-content-center align-items-center'>
            <div className='info p-3 w-50'>
                <form>
                    <Link to={'/'} className='btn-back btn btn-warning'>Trở về</Link>
                    <div className='title'><p className='text-center fw-bold'>Thông tin người dùng</p></div>
                    <div className='mb-3 fs-4'>
                        <p>Mã người dùng: <b>hihihaha</b></p>
                    </div>
                    <div className='row'>
                        <div className='mb-3 col-6'>
                            <label htmlFor='name' className='fw-bold'>Họ tên</label>
                            <input type='text' name='name' className='form-control' required />
                        </div>
                        <div className='mb-3 col-6'>
                            <label htmlFor='phonenumber' className='fw-bold'>Số điện thoại</label>
                            <input type='number' name='phonenumber' className='form-control' required />
                        </div>
                    </div>
                    <div className='row'>
                        <div className='mb-3 col-6'>
                            <label htmlFor='email' className='fw-bold'>Email</label>
                            <input type='email' name='email' className='form-control' required />
                        </div>
                        <div className='mb-3 col-6'>
                            <label htmlFor='address' className='fw-bold'>Địa chỉ</label>
                            <input type='text' name='address' className='form-control' required />
                        </div>
                    </div>
                    <button type='submit' className='btn btn-dark w-25'>Lưu</button>
                </form>
            </div>
        </div >
    )
}

export default Profile