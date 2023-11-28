import React, { useCallback, useEffect, useState } from 'react'
import { Link, useNavigate } from 'react-router-dom'
import "../styles/Profile.scss"
import axios from 'axios'
import { toast } from 'react-toastify'
import swal from 'sweetalert'

const Profile = () => {
    const navigate = useNavigate();
    const [profile, setProfile] = useState([])
    const [housenumber, setHousenumber] = useState('');
    const [wardname, setWardname] = useState('');
    const [districtname, setDistrictname] = useState('');
    const [cityname, setCityname] = useState('');
    const [city, setCity] = useState([]);
    const [provinceid, setProvinceid] = useState(0);
    const [district, setDistrict] = useState([]);
    const [districtid, setDistrictid] = useState(0);
    const [ward, setWard] = useState([]);
    const handleInputChange = (event) => {
        setProfile(prev => ({ ...prev, [event.target.name]: event.target.value }))
    };
    useEffect(() => {
        axios.get(`http://localhost:7777/api/v1/auth/getInfo`)
            .then(res => {
                if (res && res.data.success === true) {
                    setProfile(res.data.data)
                }
            }).catch(err => console.log(err))
    }, [])
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
    const handleSubmit = (e) => {
        e.preventDefault();
        if (profile.name === '' || profile.phone === '' || profile.email === '' || housenumber === '' || wardname === '' || districtname === '' || cityname === '') {
            toast.error('Vui lòng điền đầy đủ thông tin để đặt hàng !!!');
        } else {
            const address = `${housenumber}, ${wardname}, ${districtname}, ${cityname}`;
            const dataUpdateProfile = {
                name: profile.name,
                phone: profile.phone,
                email: profile.email,
                address: address
            }
            console.log(dataUpdateProfile);
            axios.patch(`http://localhost:7777/api/v1/auth/updateInfo`, dataUpdateProfile)
                .then(res => {
                    if (res && res.data.success === true) {
                        swal({
                            title: "Cập nhật thông tin thành công!",
                            icon: "success",
                        }).then(() => {
                            navigate('/');
                        });
                    }
                }).catch(err => console.log(err))
        }
    }
    return (
        <div className='all-info d-flex vh-100 justify-content-center align-items-center'>
            <div className='info p-3 w-50'>
                <form onSubmit={handleSubmit}>
                    <Link to={'/'} className='btn-back btn btn-warning'>Trở về</Link>
                    <div className='title'><p className='text-center fw-bold'>Thông tin người dùng</p></div>
                    <div className='mb-3 fs-5'>
                        <p>ID: <b>{profile.id}</b></p>
                    </div>
                    <div className='row'>
                        <div className='mb-3 col-6'>
                            <label htmlFor='name' className='fw-bold'>Họ tên</label>
                            <input type='text' value={profile.name} name='name' onChange={handleInputChange} className='form-control' required />
                        </div>
                        <div className='mb-3 col-6'>
                            <label htmlFor='email' className='fw-bold'>Email</label>
                            <input type='email' name='email' value={profile.email} onChange={handleInputChange} className='form-control' required />
                        </div>

                    </div>
                    <div className='mb-3'>
                        <label htmlFor='phone' className='fw-bold'>Số điện thoại</label>
                        <input type='number' name='phone' value={profile.phone} onChange={handleInputChange} className='form-control' required />
                    </div>

                    <div className='mb-3'>
                        <label htmlFor='address' className='fw-bold'>Địa chỉ</label>
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
                        {
                            profile.address !== null &&
                            <div className='mb-3'>
                                <input type='text' name='address' value={profile.address} className='form-control' readOnly />
                            </div>
                        }
                    </div>
                    <button type='submit' className='btn btn-dark w-25'>Lưu</button>
                </form>
            </div>
        </div >
    )
}

export default Profile