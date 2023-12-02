import React, { useCallback, useEffect, useState } from 'react'
import TopOrderInfo from '../components/OrderInfomation/TopOrderInfo'
import { useNavigate } from 'react-router-dom';
import '../styles/OrderInformation.scss';
import axios from 'axios';
import { toast } from 'react-toastify';
import InforCustomer from '../components/OrderInfomation/InfoCustomer';
import PaymentOrderInfo from '../components/OrderInfomation/PaymentOrderInfo';
const OrderInformation = (props) => {
    const navigate = useNavigate();
    const { name, cartCookie } = props
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
    const [selectedOption, setSelectedOption] = useState('');
    const [values, setValues] = useState({
        name: '',
        phonenumber: ''
    });
    const handleChange = (event) => {
        setSelectedOption(event.target.value);
    };
    const handleInputChange = (event) => {
        setValues(prev => ({ ...prev, [event.target.name]: event.target.value }))
    };
    const handleSubmit = async (event) => {
        event.preventDefault();
        if (name !== '') {
            if (selectedOption === "2") {
                if (values.name === '' || values.phonenumber === '' || housenumber === '' || wardname === '' || districtname === '' || cityname === '') {
                    toast.error('Vui lòng điền đầy đủ thông tin để đặt hàng !!!');
                } else {
                    const address = `${housenumber}, ${wardname}, ${districtname}, ${cityname}`;
                    let total = 0
                    const dataOrderInfo = {
                        name: values.name,
                        phone: values.phonenumber,
                        address: address,
                        products: cart.map((item) => {
                            total += item.product.price * item.quantity
                            return {
                                proId: item.product.id,
                                price: item.product.price,
                                quantity: item.quantity
                            };
                        })
                    }
                    navigate('/payment', { state: { dataOrderInfo: dataOrderInfo, total: total } });
                }
            } else if (selectedOption === "1") {
                let total = 0
                axios.get(`http://localhost:7777/api/v1/auth/getInfo`)
                    .then(res => {
                        if (res && res.data.success === true) {
                            if (res.data.data.name && res.data.data.phone && res.data.data.address) {
                                const dataOrderInfo = {
                                    name: res.data.data.name,
                                    phone: res.data.data.phone,
                                    address: res.data.data.address,
                                    products: cart.map((item) => {
                                        total += item.product.price * item.quantity
                                        return {
                                            proId: item.product.id,
                                            price: item.product.price,
                                            quantity: item.quantity
                                        };
                                    })
                                }
                                navigate('/payment', { state: { dataOrderInfo: dataOrderInfo, total: total } });
                            }
                            else {
                                toast.error(`Vui lòng cập nhật đầy đủ thông tin tài khoản !!!`)
                            }
                        }
                    })
            } else {
                toast.error(`Vui lòng chọn phương thức nhập thông tin người nhân !!!`)
            }
        } else {
            if (values.name === '' || values.phonenumber === '' || housenumber === '' || wardname === '' || districtname === '' || cityname === '') {
                toast.error('Vui lòng điền đầy đủ thông tin để đặt hàng !!!');
            } else {
                const address = `${housenumber}, ${wardname}, ${districtname}, ${cityname}`;
                let total = 0
                const dataOrderInfo = {
                    name: values.name,
                    phone: values.phonenumber,
                    address: address,
                    products: cart.map((item) => {
                        total += item.product.price * item.quantity
                        return {
                            proId: item.product.id,
                            price: item.product.price,
                            quantity: item.quantity
                        };
                    })
                }
                navigate('/payment', { state: { dataOrderInfo: dataOrderInfo, total: total } });
            }
        }
    }
    const loadCart = useCallback(() => {
        if (name !== '') {
            axios.get(`http://localhost:7777/api/v1/cart/`)
                .then(res => {
                    if (res && res.data) {
                        setCart(res.data.data);
                    }
                }).catch(err => console.log(err));
        }
        else {
            setCart(cartCookie)
        }
    }, [name, cartCookie]);
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
    function formatCurrency(amount) {
        const formatter = new Intl.NumberFormat('vi-VN', {
            style: 'currency',
            currency: 'VND',
        });
        return formatter.format(amount);
    }
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
                            {
                                name !== '' &&
                                <>
                                    <h5 className='my-2 text-left w-full'>Thông tin người nhận</h5>
                                    <select id="mySelect" value={selectedOption} onChange={handleChange} className='form-control my-2'>
                                        <option value="" >-- Chọn --</option>
                                        <option value={1}>Thông tin người nhận mặc định</option>
                                        <option value={2}>Thông tin người nhận mới</option>
                                    </select>
                                </>
                            }

                            <form className='form w-100 d-flex flex-column' onSubmit={handleSubmit}>
                                {
                                    selectedOption === "2" && name !== '' ? (
                                        <>
                                            <div className='title w-100 my-2'><b>Thông tin khách hàng</b></div>
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
                                        </>
                                    ) : name === '' && (
                                        <>
                                            <div className='title w-100 my-2'><b>Thông tin khách hàng</b></div>
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
                                        </>
                                    )
                                }
                                <PaymentOrderInfo cart={cart} />
                            </form>
                        </div>
                    </div>
                </div>
                <div className='card d-flex flex-column p-2 my-4'>
                    {
                        cart.length > 0 ? cart.map((order) => {
                            const subTotal = order.quantity * order.product.price;
                            return (
                                <div className='card-orderdetail my-2 d-flex' key={order.id}>
                                    <div className='card-image my-2'>
                                        <img src={`http://localhost:7777/${order.product.img}`} alt='' />
                                    </div>
                                    <div className='card-content d-flex flex-column mx-3 my-2'>
                                        <div className='card-name'><b>{order.product.name}</b></div>
                                        <div className='price-cost d-flex'>
                                            <div className='price-title'>Giá:</div>
                                            <div className='price mx-2 fw-bold'>{formatCurrency(order.product.price)}</div>
                                        </div>
                                        <div className='card-quantity d-flex'>
                                            <div className='quantity-title'>Số lượng:</div>
                                            <div className='quantity fw-bold mx-2'>{order.quantity}</div>
                                        </div>
                                        <div className='card-subtotal d-flex'>
                                            <div className='subtotal-title'>Thành tiền:</div>
                                            <div className='subtotal mx-2 fw-bold'>{formatCurrency(subTotal)}</div>
                                        </div>
                                    </div>
                                </div>
                            )
                        }) : <h3 className='text-center text-danger'>Sản phẩm không tồn tại</h3>
                    }
                </div>
            </div>
        </div>
    )
}

export default OrderInformation