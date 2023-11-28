import React, { useState } from 'react'
import { Link, useNavigate } from 'react-router-dom'
import axios from 'axios';
import swal from 'sweetalert';
import PageDoseNotExist from './Page_Does_Not_Exist/PageDoseNotExist';

const ChangePassword = ({ name }) => {
    const navigate = useNavigate()
    const [values, setValues] = useState({
        currentpass: '',
        newpass: '',
        confirmnewpass: ''
    });
    const handleInputChange = (event) => {
        setValues(prev => ({ ...prev, [event.target.name]: event.target.value }))
    };
    const handleSubmit = (e) => {
        e.preventDefault();
        if (values.newpass === values.confirmnewpass) {
            const dataChangePassword = {
                passOld: values.currentpass,
                passNew: values.newpass
            }
            axios.post(`http://localhost:7777/api/v1/auth/changePass`, dataChangePassword)
                .then(res => {
                    if (res && res.data.success === true) {
                        swal({
                            title: "Đăng nhập thành công!",
                            icon: "success",
                        }).then(() => {
                            navigate('/')
                        });
                    }
                }).catch(err => swal({
                    title: "Mật khẩu không đúng!",
                    icon: "error",
                }))
        }
        else {
            swal({
                title: "Mật khẩu xác nhận không đúng!",
                icon: "error",
            })
        }
    }
    return (
        name !== '' ?
            <div className='all-info d-flex vh-100 justify-content-center bg-white align-items-center'>
                <div className='info p-3 w-50'>
                    <form onSubmit={handleSubmit}>
                        <Link to={'/'} className='btn-back btn btn-warning'>Trở về</Link>
                        <div className='title'><p className='text-center fw-bold'>Đổi mật khẩu</p></div>
                        <div className='mb-3'>
                            <label htmlFor='username' className='fw-bold'>Mật khẩu hiện tại(*)</label>
                            <input type='password' name='currentpass' onChange={handleInputChange} className='form-control' required />
                        </div>
                        <div className='mb-3'>
                            <label htmlFor='username' className='fw-bold'>Mật khẩu mới(*)</label>
                            <input type='password' name='newpass' onChange={handleInputChange} className='form-control' required />
                        </div>
                        <div className='mb-3'>
                            <label htmlFor='username' className='fw-bold'>Xác nhận mật khẩu mới(*)</label>
                            <input type='password' name='confirmnewpass' onChange={handleInputChange} className='form-control' required />
                        </div>
                        <button type='submit' className='btn btn-dark w-25'>Lưu</button>
                    </form>
                </div>
            </div> : <PageDoseNotExist />
    )
}

export default ChangePassword