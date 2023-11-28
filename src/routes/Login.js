import React, { useEffect, useState } from 'react'
import Google from '../assets/images/google.png';
import Github from '../assets/images/github.png';
import Facebook from '../assets/images/facebook.png';
import '../styles/Login.scss';
import axios from 'axios';
import { useNavigate } from 'react-router-dom';
import { toast } from 'react-toastify';
import swal from 'sweetalert';
const Login = () => {
    // axios.defaults.withCredentials = true;
    const navigate = useNavigate();
    const [values, setValues] = useState({
        email: '',
        password: ''
    });
    const handleInputChange = (event) => {
        setValues(prev => ({ ...prev, [event.target.name]: event.target.value }))
    };
    const handleSubmit = (event) => {
        event.preventDefault();
        axios.post('http://localhost:7777/api/v1/auth/login', values)
            .then(res => {
                if (res && res.data.message === 'Login success') {
                    swal({
                        title: "Đăng nhập thành công!",
                        icon: "success",
                    }).then(() => {
                        window.location.reload();
                    });
                }
            })
            .catch(err => {
                if (err && err.response && err.response.data) {
                    const { message } = err.response.data.message;
                    switch (message) {
                        case 'Your account is locked':
                            toast.error('Tài khoản của bạn đã bị khóa !!!');
                            break;
                        case 'Email not found':
                            toast.error('Email không tồn tại !!!');
                            break;
                        case 'Incorrect password':
                            toast.error('Sai mật khẩu !!!');
                            break;
                        // case '"password" length must be 5 characters long':
                        //     toast.error('Mật khẩu chỉ được tối đa 5 kí tự !!!');
                        //     break;
                        default:
                            toast.error('Sai mật khẩu hoặc tài khoản !!!');
                    }
                } else {
                    toast.error('Sai mật khẩu hoặc tài khoản !!!');
                }
                console.log(err);
            });
    };


    useEffect(() => {
        const verify = () => {
            axios.get('http://localhost:7777/api/v1/auth/getInfo')
                .then(res => {
                    console.log(res);
                    if (res && res.data.success === true) {
                        navigate('/');
                    } else {
                        return;
                    }
                }).catch(err => {
                    if (err.response.data.message === 'jwt expired') {
                        axios.post(`http://localhost:7777/api/v1/auth/refreshToken`)
                            .then(res => {
                                if (res && res.data.success === true && res.data.message === 'Refresh token success') {
                                    return;
                                }
                            }).catch(error => console.log(error));
                    } else {
                        console.log(err);
                    }
                });
        }
        verify();
    }, [navigate])

    return (
        <div className='all-login'>
            <h1 className='loginTitle'>Chọn phương thức đăng nhập</h1>
            <div className='wrapper'>
                <div className='left'>
                    {/* onClick={google} */}
                    <div className='loginButton google'>
                        <img src={Google} alt='' className='icon' />
                        Google
                    </div>
                    <div className='loginButton facebook'>
                        <img src={Facebook} alt='' className='icon' />
                        Facebook
                    </div>
                    <div className='loginButton github'>
                        <img src={Github} alt='' className='icon' />
                        Github
                    </div>
                </div>
                <div className='center'>
                    <div className='or'>Hoặc</div>
                </div>
                <div className='right'>
                    <form className='w-75' onSubmit={handleSubmit}>
                        <div className="mb-3">
                            <label htmlFor="email" class="form-label">Email (*)</label>
                            <input type='text' name='email' id='email' className='form-control' onChange={handleInputChange} required />
                        </div>
                        <div className="mb-3 col-12">
                            <label htmlFor="password" class="form-label">Mật khẩu (*)</label>
                            <input type="password" className="form-control" id="password" name='password' onChange={handleInputChange} required />
                        </div>
                        <div className='d-flex justify-content-center'>
                            <button type="submit" class="btn btn-dark btn-lg w-50">Đăng nhập</button>
                        </div>
                    </form>
                    <a href='/register' className='new-account btn btn-success'>Đăng ký tài khoản mới</a>
                </div>
            </div>
        </div>
    )
}

export default Login