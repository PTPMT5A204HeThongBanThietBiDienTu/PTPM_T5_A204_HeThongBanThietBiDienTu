import React, { useEffect, useState } from 'react'
import '../styles/Register.scss';
import { useNavigate } from 'react-router-dom';
import axios from 'axios';
import { toast } from 'react-toastify';

const Register = () => {
    const navigate = useNavigate();
    const [values, setValues] = useState({
        name: '',
        email: '',
        phone: '',
        address: '',
        password: '',
        confirmpassword: ''
    });
    const handleInputChange = (event) => {
        setValues(prev => ({ ...prev, [event.target.name]: event.target.value }))
    };
    const handleSubmit = (e) => {
        e.preventDefault();
        if (values.confirmpassword === values.password) {
            const registerData = {
                name: values.name,
                email: values.email,
                phone: values.phone,
                address: values.address,
                password: values.password
            };
            axios.post(`http://localhost:1234/api/v1/auth/register`, registerData)
                .then(res => {
                    if (res && res.data.message === 'Register success') {
                        toast.success('Register successfully !!!');
                        navigate('/login');
                    }
                }).catch(err => console.log(err));
        } else {
            toast.error('Confirm password does not match !!!');
        }
    }
    useEffect(() => {
        const verify = async () => {
            await axios.get('http://localhost:1234/api/v1/auth/getInfo')
                .then(res => {
                    if (res && res.data.success === true) {
                        navigate('/');
                    } else if (res && res.data.success === false) {
                        return;
                    }
                }).catch(err => console.log(err));
        }
        verify();
    }, [navigate])
    return (
        <div className='all-register'>
            <h1 className='registerTitle'>Đăng ký</h1>
            <div className='register-wrapper'>
                <div className="d-flex flex-column justify-content-center align-items-center h-100 border">
                    <form className="w-75 d-flex flex-column" onSubmit={handleSubmit}>
                        <div className='mb-3'>
                            <label htmlFor='name'>Họ tên (*)</label>
                            <input type='text' placeholder='Enter your Name' name='name' id='name' className='form-control' onChange={(e) => handleInputChange(e)} required />
                        </div>
                        <div className='row'>
                            <div className='mb-3 col-6'>
                                <label htmlFor='phone'>Số điện thoại (*)</label>
                                <input type='number' name='phone' placeholder='Enter your Phone' id='phone' className='form-control' onChange={handleInputChange} required />
                            </div>
                            <div className='mb-3 col-6'>
                                <label htmlFor='email'>Email (*)</label>
                                <input type='text' name='email' placeholder='Enter your Email' id='email' className='form-control' onChange={handleInputChange} required />
                            </div>
                        </div>
                        <div className='mb-3'>
                            <label htmlFor='address'>Địa chỉ (*)</label>
                            <input type='text' name='address' placeholder='Enter your Address' id='address' className='form-control' onChange={handleInputChange} required />
                        </div>
                        <div className='row'>
                            <div className="mb-3 col-6">
                                <label htmlFor="password" className="form-label">Mật khẩu (*)</label>
                                <input type="password" placeholder='Enter your Password' className="form-control" id="password" name='password' onChange={handleInputChange} required />
                            </div>
                            <div className="mb-3 col-6">
                                <label htmlFor="confirmpassword" className="form-label">Xác nhận mật khẩu (*)</label>
                                <input type="password" placeholder='Enter your Confirm Password' className="form-control" id="confirmpassword" name='confirmpassword' onChange={handleInputChange} required />
                            </div>
                        </div>
                        <div className="d-flex justify-content-center mt-4">
                            <button type="submit" className="btn btn-dark btn-lg w-25">Đăng ký</button>
                        </div>
                    </form>
                    <a href='/login' className='mt-2'>Đã có tài khoản?</a>
                    {/* <p className='text-center'>{notmatch}</p> */}
                </div>
            </div>
        </div>
    )
}

export default Register