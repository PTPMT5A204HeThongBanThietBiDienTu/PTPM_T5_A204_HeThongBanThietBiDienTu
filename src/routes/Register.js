import React, { useCallback, useEffect, useState } from 'react'
import '../styles/Register.scss';
import { useNavigate } from 'react-router-dom';
import axios from 'axios';
import { toast } from 'react-toastify';
import swal from 'sweetalert';

const Register = () => {
    const navigate = useNavigate();
    const [housenumber, setHousenumber] = useState('');
    const [wardname, setWardname] = useState('');
    const [districtname, setDistrictname] = useState('');
    const [cityname, setCityname] = useState('');
    const [city, setCity] = useState([]);
    const [provinceid, setProvinceid] = useState(0);
    const [district, setDistrict] = useState([]);
    const [districtid, setDistrictid] = useState(0);
    const [ward, setWard] = useState([]);
    const [values, setValues] = useState({
        name: '',
        email: '',
        phone: '',
        password: '',
        confirmpassword: ''
    });
    const handleInputChange = (event) => {
        setValues(prev => ({ ...prev, [event.target.name]: event.target.value }))
    };
    const handleSubmit = (e) => {
        e.preventDefault();
        if (values.name === '' || values.phone === '' || values.email === '' || housenumber === '' || wardname === '' || districtname === '' || cityname === '') {
            toast.error('Vui lòng nhập đầy đủ thông tin để đăng ký');
        } else {
            const address = `${housenumber}, ${wardname}, ${districtname}, ${cityname}`;
            if (values.confirmpassword === values.password) {
                const registerData = {
                    name: values.name,
                    email: values.email,
                    phone: values.phone,
                    address: address,
                    password: values.password
                };
                console.log(registerData);
                axios.post(`http://localhost:7777/api/v1/auth/register`, registerData)
                    .then(res => {
                        if (res && res.data.message === 'Register success') {
                            swal({
                                title: "Đăng ký thành công!",
                                icon: "success",
                            }).then(() => {
                                navigate('/login');
                            });
                        }
                    }).catch(err => console.log(err));
            } else {
                toast.error('Mật khẩu xác nhận không trùng khớp !!!');
            }
        }
    }
    useEffect(() => {
        const verify = async () => {
            await axios.get('http://localhost:7777/api/v1/auth/getInfo')
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
    const handleHousenumberChange = (event) => {
        setHousenumber(event.target.value);
    };
    const handleCityChange = (event) => {
        const name_city = event.target.value;
        setProvinceid(name_city.split('-')[0]);
        setCityname(name_city.split('-')[1]);
    }
    const handleDistrictChange = (event) => {
        const name_district = event.target.value;
        setDistrictid(name_district.split('-')[0]);
        setDistrictname(name_district.split('-')[1]);
    }
    const handleWardChange = (event) => {
        setWardname(event.target.value);
    }
    const fetchDataCity = useCallback(async () => {
        try {
            const res = await axios.get('http://localhost:7777/api/province/');
            const formatCity = res.data.results.map(city => ({
                id: city.province_id,
                name: city.province_name
            }));
            setCity(formatCity);
        } catch (error) {
            console.error(error);
        }
    }, []);
    const fetchDataDistrict = useCallback(async () => {
        try {
            const res = await axios.get(`http://localhost:7777/api/province/district/${provinceid}`);
            const formatDistrict = res.data.results.map(district => ({
                id: district.district_id,
                name: district.district_name
            }));
            setDistrict(formatDistrict);
        } catch (error) {
            console.log(error);
        }
    }, [provinceid]);
    const fetchDataWard = useCallback(async () => {
        try {
            const res = await axios.get(`http://localhost:7777/api/province/ward/${districtid}`);
            const formatWard = res.data.results.map(ward => ({
                id: ward.ward_id,
                name: ward.ward_name
            }));
            setWard(formatWard);
        } catch (error) {
            console.log(error);
        }
    }, [districtid]);
    useEffect(() => {
        fetchDataCity();
        fetchDataDistrict();
        fetchDataWard();
    }, [fetchDataCity, fetchDataDistrict, fetchDataWard]);
    return (
        <div className='all-register'>
            <h1 className='registerTitle'>Đăng ký</h1>
            <div className='register-wrapper'>
                <div className="d-flex flex-column justify-content-center align-items-center h-100 border">
                    <form className="w-75 d-flex flex-column" onSubmit={handleSubmit}>
                        <div className='mb-3'>
                            <label htmlFor='name'>Họ tên (*)</label>
                            <input type='text' name='name' id='name' className='form-control' onChange={(e) => handleInputChange(e)} required />
                        </div>
                        <div className='row'>
                            <div className='mb-3 col-6'>
                                <label htmlFor='phone'>Số điện thoại (*)</label>
                                <input type='number' name='phone' id='phone' className='form-control' onChange={handleInputChange} required />
                            </div>
                            <div className='mb-3 col-6'>
                                <label htmlFor='email'>Email (*)</label>
                                <input type='text' name='email' id='email' className='form-control' onChange={handleInputChange} required />
                            </div>
                        </div>
                        <div className='mb-3'>
                            <label htmlFor='address'>Địa chỉ (*)</label>
                            <div className='row my-2'>
                                <div className='col-6'>
                                    <select className='form-control' onChange={handleCityChange}>
                                        <option value=''>-- Chọn Thành Phố --</option>
                                        {
                                            city.map((select_city) => (
                                                <option key={select_city.id} value={`${select_city.id}-${select_city.name}`}>{select_city.name}</option>
                                            ))
                                        }
                                    </select>
                                </div>
                                <div className='col-6'>
                                    <select className='form-control' onChange={handleDistrictChange}>
                                        <option value=''>-- Chọn Quận --</option>
                                        {district.length === 0 ?
                                            <></>
                                            : (
                                                district.map((select_district) => (
                                                    <>
                                                        <option value={`${select_district.id}-${select_district.name}`}>{select_district.name}</option>
                                                    </>
                                                ))

                                            )
                                        }
                                    </select>
                                </div>
                            </div>
                            <div className='row  my-2'>
                                <div className='col-6'>
                                    <select className='form-control' onChange={handleWardChange}>
                                        <option value=''>-- Chọn Phường --</option>
                                        {ward.length > 0 ?
                                            ward.map((select_ward) => (
                                                <>
                                                    <option value={select_ward.name}>{select_ward.name}</option>
                                                </>
                                            ))
                                            : <></>
                                        }
                                    </select>
                                </div>
                                <div className='col-6'>
                                    <input type="text" placeholder='Số nhà, tên đường' name='housenumber' onChange={(e) => handleHousenumberChange(e)} className='form-control' />
                                </div>
                            </div>
                        </div>
                        <div className='row'>
                            <div className="mb-3 col-6">
                                <label htmlFor="password" className="form-label">Mật khẩu (*)</label>
                                <input type="password" className="form-control" id="password" name='password' onChange={handleInputChange} required />
                            </div>
                            <div className="mb-3 col-6">
                                <label htmlFor="confirmpassword" className="form-label">Xác nhận mật khẩu (*)</label>
                                <input type="password" className="form-control" id="confirmpassword" name='confirmpassword' onChange={handleInputChange} required />
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