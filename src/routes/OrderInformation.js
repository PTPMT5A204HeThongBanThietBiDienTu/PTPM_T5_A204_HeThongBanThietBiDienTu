import React, { useCallback, useEffect, useState } from 'react'
import TopOrderInfo from '../components/OrderInfomation/TopOrderInfo'
import { useNavigate } from 'react-router-dom';
import '../styles/OrderInformation.scss';
import axios from 'axios';
import { toast } from 'react-toastify';
import InforCustomer from '../components/OrderInfomation/InfoCustomer';
import PaymentOrderInfo from '../components/OrderInfomation/PaymentOrderInfo';
const OrderInformation = () => {
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
    const [cart, setCart] = useState([]);
    const [values, setValues] = useState({
        name: '',
        phonenumber: ''
    });
    const handleInputChange = (event) => {
        setValues(prev => ({ ...prev, [event.target.name]: event.target.value }))
    };
    const handleSubmit = async (event) => {
        event.preventDefault();
        if (values.name === '' || values.phonenumber === '' || housenumber === '' || wardname === '' || districtname === '' || cityname === '') {
            toast.error('Vui lòng điền đầy đủ thông tin để đặt hàng !!!');
        } else {
            const address = `${housenumber}, ${wardname}, ${districtname}, ${cityname}`;
            const dataOrderInfo = {
                name: values.name,
                phone: values.phonenumber,
                address: address,
                products: cart.map((item) => {
                    return {
                        proId: item.product.id,
                        price: item.product.price,
                        quantity: item.quantity
                    };
                })
            }
            axios.post(`http://localhost:7777/api/v1/bill/create`, dataOrderInfo)
                .then(res => {
                    if (res && res.data.success === true) {
                        const billId = res.data.data.id;
                        console.log(billId);
                        axios.delete(`http://localhost:7777/api/v1/cart/deleteByUserId`)
                            .then(res => {
                                if (res && res.data.success === true) {
                                    navigate('/order-complete', { state: { billId: billId } });
                                }
                            }).catch(err => console.log(err));
                    }
                }).catch(err => console.log(err));
        }
    }
    const loadCart = useCallback(() => {
        axios.get(`http://localhost:7777/api/v1/cart/`)
            .then(res => {
                if (res && res.data) {
                    setCart(res.data.data);
                }
            }).catch(err => console.log(err));
    }, []);
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
        loadCart();
    }, [fetchDataCity, fetchDataDistrict, fetchDataWard, loadCart]);
    return (
        <div className='all-order py-4'>
            <div className='order'>
                <div className='title'>
                    <a href='/cart'><h3 className='back'>Trở về</h3></a>
                    <h3 className='giohang'>Thông tin đặt hàng</h3>
                </div>
                <hr />
                <div className='order-content'>
                    <div className='content'>
                        <TopOrderInfo />
                        <div className='center'>
                            <div className='title w-100 my-2'><b>Thông tin khách hàng</b></div>
                            <form className='form w-100 d-flex flex-column' onSubmit={handleSubmit}>
                                <input type='text' value={values.name} name='name' id='name' placeholder='Tên người nhận (*)' className='form-control my-2' onChange={handleInputChange} />
                                <input type='number' value={values.phonenumber} name='phonenumber' id='phonenumber' placeholder='Số điện thoại người nhận (*)' onChange={handleInputChange} className='form-control my-2' />
                                <InforCustomer
                                    city={city}
                                    provinceid={provinceid}
                                    district={district}
                                    ward={ward}
                                    handleCityChange={handleCityChange}
                                    handleDistrictChange={handleDistrictChange}
                                    handleWardChange={handleWardChange}
                                    handleHousenumberChange={handleHousenumberChange}
                                />
                                <PaymentOrderInfo cart={cart} />
                            </form>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    )
}

export default OrderInformation