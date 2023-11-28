import React from 'react'
import { Link } from 'react-router-dom'

const ChangePassword = () => {
    return (
        <div className='all-info d-flex vh-100 justify-content-center bg-white align-items-center'>
            <div className='info p-3 w-50'>
                <form >
                    <Link to={'/'} className='btn-back btn btn-warning'>Trở về</Link>
                    <div className='title'><p className='text-center fw-bold'>Đổi mật khẩu</p></div>
                    <div className='mb-3'>
                        <label htmlFor='username' className='fw-bold'>Mật khẩu hiện tại(*)</label>
                        <input type='password' name='currentpass' className='form-control' required />
                    </div>
                    <div className='mb-3'>
                        <label htmlFor='username' className='fw-bold'>Mật khẩu mới(*)</label>
                        <input type='password' name='newpass' className='form-control' required />
                    </div>
                    <div className='mb-3'>
                        <label htmlFor='username' className='fw-bold'>Xác nhận mật khẩu mới(*)</label>
                        <input type='password' name='confirmnewpass' className='form-control' required />
                    </div>
                    <button type='submit' className='btn btn-dark w-25'>Lưu</button>
                </form>
            </div>
        </div>
    )
}

export default ChangePassword