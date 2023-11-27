import React, { useCallback, useEffect, useState } from 'react';
import axios from 'axios';
import { Link, useParams } from 'react-router-dom';
import "../styles/Category.scss";
import Slider from 'rc-slider';
import 'rc-slider/assets/index.css';
import sold from '../assets/icons/sold.png'
const Category = () => {
    const { catID } = useParams();
    const [product, setProduct] = useState([]);
    const [iphone, setIphone] = useState(false);
    const [samsung, setSamsung] = useState(false);
    const [acer, setAcer] = useState(false);
    const [asus, setAsus] = useState(false);
    const [dell, setDell] = useState(false);
    const [edra, setEdra] = useState(false);
    const [asusPC, setAsusPC] = useState(false);
    const [cellphones, setCellphones] = useState(false);
    const [logitech, setLogitech] = useState(false);
    const [dareu, setDareu] = useState(false);
    const [akko, setAkko] = useState(false);

    const [searchPrice, setSearchPrice] = useState(false);
    const [priceRange, setPriceRange] = useState([0, 100000000]);
    const handleSliderChange = (value) => {
        setPriceRange(value);
    }
    const resultSearchPrice = () => {
        const dataSearchPrice = {
            minPrice: priceRange[0],
            maxPrice: priceRange[1]
        }
        axios.post(`http://localhost:7777/api/v1/product/getAllByPrice?catId=${catID}`, dataSearchPrice)
            .then(res => {
                if (res && res.data.success === true) {
                    setProduct(res.data.data);
                    setSearchPrice(false);
                    setTotalPage(res.data.totalPages)
                }
            }).catch(err => console.log(err))
    }
    const [page, setPage] = useState(1);
    const [totalPage, setTotalPage] = useState(0);
    const [pagination, setPagination] = useState([]);
    const handlePrev = () => {
        if (page > 1) setPage(page - 1);
    };

    const handleNext = () => {
        if (page < totalPage) setPage(page + 1);
    };

    const renderPagination = useCallback(() => {
        let paginationItems = [];
        for (let i = 1; i <= totalPage; i++) {
            paginationItems.push(
                <Link
                    to='#'
                    key={i}
                    className={i === page ? 'active' : ''}
                    onClick={() => setPage(i)}
                >
                    {i}
                </Link>
            );
        }
        setPagination(paginationItems);
    }, [totalPage, page, setPage, setPagination]);

    useEffect(() => {
        renderPagination();
    }, [totalPage, page, renderPagination]);
    useEffect(() => {
        if (catID === "47845903-009d-4294-80cb-cd6549bd2dab") {
            if (iphone === false && samsung === false) {
                axios.get(`http://localhost:7777/api/v1/product/getByCatId/47845903-009d-4294-80cb-cd6549bd2dab?page=${page}`).then(res => {
                    if (res && res.data) {
                        setProduct(res.data.data);
                        setTotalPage(res.data.totalPages)
                    }
                }).catch(err => console.log(err));
            } else if (iphone === true) {
                axios.get(`http://localhost:7777/api/v1/product/getAllByCatIdAndBraId/?catId=${catID}&braId=9e5b2654-a901-46b5-8f23-819dcec28457&?page=${page}`).then(res => {
                    if (res && res.data) {
                        setProduct(res.data.data);
                        setTotalPage(res.data.totalPages)
                    }
                }).catch(err => console.log(err));
            } else if (samsung === true) {
                axios.get(`http://localhost:7777/api/v1/product/getAllByCatIdAndBraId/?catId=${catID}&braId=ab84deca-6afa-4f4a-a826-46342f72d80e&?page=${page}`).then(res => {
                    if (res && res.data) {
                        setProduct(res.data.data)
                        setTotalPage(res.data.totalPages)
                    }
                }).catch(err => console.log(err));
            }
        }
        else if (catID === "3289f294-65b0-4dd8-a5dc-ef6ca442eb68") {
            if (acer === false && asus === false) {
                axios.get(`http://localhost:7777/api/v1/product/getByCatId/3289f294-65b0-4dd8-a5dc-ef6ca442eb68?page=${page}`).then(res => {
                    if (res && res.data) {
                        setProduct(res.data.data)
                        setTotalPage(res.data.totalPages)
                    }
                }).catch(err => console.log(err));
            } else if (acer === true) {
                axios.get(`http://localhost:7777/api/v1/product/getAllByCatIdAndBraId/?catId=${catID}&braId=90320d05-772f-41e5-a077-411532c68145&?page=${page}`).then(res => {
                    if (res && res.data) {
                        setProduct(res.data.data)
                        setTotalPage(res.data.totalPages)
                    }
                }).catch(err => console.log(err));
            } else if (asus === true) {
                axios.get(`http://localhost:7777/api/v1/product/getAllByCatIdAndBraId/?catId=${catID}&braId=9757c182-3f7d-44d7-a2b6-b0cef9917778&?page=${page}`).then(res => {
                    if (res && res.data) {
                        setProduct(res.data.data)
                        setTotalPage(res.data.totalPages)
                    }
                }).catch(err => console.log(err));
            }
        }
        else if (catID === "176d9ec8-f0d2-4159-b657-264fb8ec79f1") {
            if (dell === false && edra === false) {
                axios.get(`http://localhost:7777/api/v1/product/getByCatId/176d9ec8-f0d2-4159-b657-264fb8ec79f1?page=${page}`).then(res => {
                    if (res && res.data) {
                        setProduct(res.data.data)
                        setTotalPage(res.data.totalPages)
                    }
                }).catch(err => console.log(err));
            }
            else if (dell === true) {
                axios.get(`http://localhost:7777/api/v1/product/getAllByCatIdAndBraId/?catId=${catID}&braId=9b04c31a-f0b4-4e1a-921b-3f6d889776ee&?page=${page}`).then(res => {
                    if (res && res.data) {
                        setProduct(res.data.data)
                        setTotalPage(res.data.totalPages)
                    }
                }).catch(err => console.log(err));
            }
            else if (edra === true) {
                axios.get(`http://localhost:7777/api/v1/product/getAllByCatIdAndBraId/?catId=${catID}&braId=5b43922d-0349-454b-a4af-7577a586caec&?page=${page}`).then(res => {
                    if (res && res.data) {
                        setProduct(res.data.data)
                        setTotalPage(res.data.totalPages)
                    }
                }).catch(err => console.log(err));
            }
        }
        else if (catID === "b14de4e4-cf6e-4a4b-8996-9198940164a2") {
            if (asusPC === false && cellphones === false) {
                axios.get(`http://localhost:7777/api/v1/product/getByCatId/${catID}?page=${page}`).then(res => {
                    if (res && res.data) {
                        setProduct(res.data.data)
                        setTotalPage(res.data.totalPages)
                    }
                }).catch(err => console.log(err));
            }
            else if (asusPC === true) {
                axios.get(`http://localhost:7777/api/v1/product/getAllByCatIdAndBraId/?catId=${catID}&braId=9757c182-3f7d-44d7-a2b6-b0cef9917778&?page=${page}`).then(res => {
                    if (res && res.data) {
                        setProduct(res.data.data)
                        setTotalPage(res.data.totalPages)
                    }
                }).catch(err => console.log(err));
            }
            else if (cellphones === true) {
                axios.get(`http://localhost:7777/api/v1/product/getAllByCatIdAndBraId/?catId=${catID}&braId=ba755243-70ea-403e-a448-01fe7b531d48&?page=${page}`).then(res => {
                    if (res && res.data) {
                        setProduct(res.data.data)
                        setTotalPage(res.data.totalPages)
                    }
                }).catch(err => console.log(err));
            }
        }
        else if (catID === "917bd3c0-666c-4753-af14-3832b037da98") {
            if (logitech === false && dareu === false) {
                axios.get(`http://localhost:7777/api/v1/product/getByCatId/${catID}?page=${page}`).then(res => {
                    if (res && res.data) {
                        setProduct(res.data.data)
                        setTotalPage(res.data.totalPages)
                    }
                }).catch(err => console.log(err));
            }
            else if (logitech === true) {
                axios.get(`http://localhost:7777/api/v1/product/getAllByCatIdAndBraId/?catId=${catID}&braId=04c1e199-ff76-40c0-9877-87c76422d43c&?page=${page}`).then(res => {
                    if (res && res.data) {
                        setProduct(res.data.data)
                        setTotalPage(res.data.totalPages)
                    }
                }).catch(err => console.log(err));
            }
            else if (dareu === true) {
                axios.get(`http://localhost:7777/api/v1/product/getAllByCatIdAndBraId/?catId=${catID}&braId=fe9c79d1-298e-41f0-9bf3-4a9addf6d8d7&?page=${page}`).then(res => {
                    if (res && res.data) {
                        setProduct(res.data.data)
                        setTotalPage(res.data.totalPages)
                    }
                }).catch(err => console.log(err));
            }
        }
        else if (catID === "e57ad09d-6f21-482d-a074-1249dc32ebe4") {
            if (akko === false) {
                axios.get(`http://localhost:7777/api/v1/product/getByCatId/${catID}?page=${page}`).then(res => {
                    if (res && res.data) {
                        setProduct(res.data.data)
                        setTotalPage(res.data.totalPages)
                    }
                }).catch(err => console.log(err));
            }
            else if (akko === true) {
                axios.get(`http://localhost:7777/api/v1/product/getAllByCatIdAndBraId/?catId=${catID}&braId=100240e0-9702-483d-b4a8-02a9a8114aa9&?page=${page}`).then(res => {
                    if (res && res.data) {
                        setProduct(res.data.data)
                        setTotalPage(res.data.totalPages)
                    }
                }).catch(err => console.log(err));
            }
        }
    }, [iphone, samsung, acer, asus, dell, edra, asusPC, cellphones, logitech, dareu, akko, page, catID])
    function formatCurrency(amount) {
        const formatter = new Intl.NumberFormat('vi-VN', {
            style: 'currency',
            currency: 'VND',
        });
        return formatter.format(amount);
    }
    return (
        <div className='container-fluid'>
            <div className='all-product'>
                {
                    catID === "47845903-009d-4294-80cb-cd6549bd2dab" ?
                        <div className='category-product'>
                            <button className={`btn btn-${iphone === true && samsung === false ? "danger" : "light"} border mx-2`} onClick={() => { setIphone(true); setSamsung(false) }}>
                                IPhone
                            </button>
                            <button className={`btn btn-${iphone === false && samsung === true ? "danger" : "light"} border mx-2`} onClick={() => { setIphone(false); setSamsung(true) }}>
                                Samsung
                            </button>
                            <button className={`btn btn-${iphone === false && samsung === false ? "danger" : "light"} border mx-2`} onClick={() => { setIphone(false); setSamsung(false) }}>
                                Xem tất cả
                            </button>
                            <button className={`btn mx-2 ${searchPrice === true ? 'btn-danger' : 'btn-light border'}`} onClick={() => setSearchPrice(searchPrice === true ? false : true)}>
                                Giá
                            </button>
                            {searchPrice && (
                                <div className='search-price-modal'>
                                    <div className='search-price-content'>
                                        <div className='price-from flex justify-between mb-2'>
                                            <span>{formatCurrency(priceRange[0])}</span>
                                            <span>{formatCurrency(priceRange[1])}</span>
                                        </div>
                                        <Slider
                                            min={0}
                                            max={100000000}
                                            range
                                            defaultValue={priceRange}
                                            onChange={handleSliderChange}
                                        />
                                        <div className='action'>
                                            <button className='close-btn' onClick={() => setSearchPrice(false)}>
                                                Đóng
                                            </button>
                                            <button className='search-btn mx-2' onClick={() => resultSearchPrice()}>
                                                Xem kết quả
                                            </button>
                                        </div>
                                    </div>
                                </div>
                            )}
                        </div> : catID === "3289f294-65b0-4dd8-a5dc-ef6ca442eb68" ?
                            <div className='category-product'>
                                <button className={`btn btn-${acer === true && asus === false ? "danger" : "light"} border my-2`} onClick={() => { setAcer(true); setAsus(false) }}>
                                    Acer
                                </button>
                                <button className={`btn btn-${acer === false && asus === true ? "danger" : "light"} border mx-2`} onClick={() => { setAcer(false); setAsus(true) }}>
                                    Asus
                                </button>
                                <button className={`btn btn-${acer === false && asus === false ? "danger" : "light"} border mx-2`} onClick={() => { setAcer(false); setAsus(false) }}>
                                    Xem tất cả
                                </button>
                                <button className={`btn mx-2 ${searchPrice === true ? 'btn-danger' : 'btn-light border'}`} onClick={() => setSearchPrice(searchPrice === true ? false : true)}>
                                    Giá
                                </button>
                                {searchPrice && (
                                    <div className='search-price-modal'>
                                        <div className='search-price-content'>
                                            <div className='price-from flex justify-between mb-2'>
                                                <span>{formatCurrency(priceRange[0])}</span>
                                                <span>{formatCurrency(priceRange[1])}</span>
                                            </div>
                                            <Slider
                                                min={0}
                                                max={100000000}
                                                range
                                                defaultValue={priceRange}
                                                onChange={handleSliderChange}
                                            />
                                            <div className='action'>
                                                <button className='close-btn' onClick={() => setSearchPrice(false)}>
                                                    Đóng
                                                </button>
                                                <button className='search-btn mx-2' onClick={() => resultSearchPrice()}>
                                                    Xem kết quả
                                                </button>
                                            </div>
                                        </div>
                                    </div>
                                )}
                            </div> : catID === "176d9ec8-f0d2-4159-b657-264fb8ec79f1" ?
                                <div className='category-product'>
                                    <button className={`btn btn-${dell === true && edra === false ? "danger" : "light"} border my-2`} onClick={() => { setDell(true); setEdra(false) }}>
                                        Dell
                                    </button>
                                    <button className={`btn btn-${dell === false && edra === true ? "danger" : "light"} border mx-2`} onClick={() => { setDell(false); setEdra(true) }}>
                                        E-Dra
                                    </button>
                                    <button className={`btn btn-${dell === false && edra === false ? "danger" : "light"} border mx-2`} onClick={() => { setDell(false); setEdra(false) }}>
                                        Xem tất cả
                                    </button>
                                    <button className={`btn mx-2 ${searchPrice === true ? 'btn-danger' : 'btn-light border'}`} onClick={() => setSearchPrice(searchPrice === true ? false : true)}>
                                        Giá
                                    </button>
                                    {searchPrice && (
                                        <div className='search-price-modal'>
                                            <div className='search-price-content'>
                                                <div className='price-from flex justify-between mb-2'>
                                                    <span>{formatCurrency(priceRange[0])}</span>
                                                    <span>{formatCurrency(priceRange[1])}</span>
                                                </div>
                                                <Slider
                                                    min={0}
                                                    max={100000000}
                                                    range
                                                    defaultValue={priceRange}
                                                    onChange={handleSliderChange}
                                                />
                                                <div className='action'>
                                                    <button className='close-btn' onClick={() => setSearchPrice(false)}>
                                                        Đóng
                                                    </button>
                                                    <button className='search-btn mx-2' onClick={() => resultSearchPrice()}>
                                                        Xem kết quả
                                                    </button>
                                                </div>
                                            </div>
                                        </div>
                                    )}
                                </div> : catID === "b14de4e4-cf6e-4a4b-8996-9198940164a2" ?
                                    <div className='category-product'>
                                        <button className={`btn btn-${asusPC === true && cellphones === false ? "danger" : "light"} border my-2`} onClick={() => { setAsusPC(true); setCellphones(false) }}>
                                            Asus
                                        </button>
                                        <button className={`btn btn-${asusPC === false && cellphones === true ? "danger" : "light"} border mx-2`} onClick={() => { setAsusPC(false); setCellphones(true) }}>
                                            Cellphones
                                        </button>
                                        <button className={`btn btn-${asusPC === false && cellphones === false ? "danger" : "light"} border mx-2`} onClick={() => { setAsusPC(false); setCellphones(false) }}>
                                            Xem tất cả
                                        </button>
                                        <button className={`btn mx-2 ${searchPrice === true ? 'btn-danger' : 'btn-light border'}`} onClick={() => setSearchPrice(searchPrice === true ? false : true)}>
                                            Giá
                                        </button>
                                        {searchPrice && (
                                            <div className='search-price-modal'>
                                                <div className='search-price-content'>
                                                    <div className='price-from flex justify-between mb-2'>
                                                        <span>{formatCurrency(priceRange[0])}</span>
                                                        <span>{formatCurrency(priceRange[1])}</span>
                                                    </div>
                                                    <Slider
                                                        min={0}
                                                        max={100000000}
                                                        range
                                                        defaultValue={priceRange}
                                                        onChange={handleSliderChange}
                                                    />
                                                    <div className='action'>
                                                        <button className='close-btn' onClick={() => setSearchPrice(false)}>
                                                            Đóng
                                                        </button>
                                                        <button className='search-btn mx-2' onClick={() => resultSearchPrice()}>
                                                            Xem kết quả
                                                        </button>
                                                    </div>
                                                </div>
                                            </div>
                                        )}
                                    </div> : catID === "917bd3c0-666c-4753-af14-3832b037da98" ?
                                        <div className='category-product'>
                                            <button className={`btn btn-${logitech === true && dareu === false ? "danger" : "light"} border my-2`} onClick={() => { setLogitech(true); setDareu(false) }}>
                                                Logitech
                                            </button>
                                            <button className={`btn btn-${logitech === false && dareu === true ? "danger" : "light"} border mx-2`} onClick={() => { setLogitech(false); setDareu(true) }}>
                                                Dareu
                                            </button>
                                            <button className={`btn btn-${logitech === false && dareu === false ? "danger" : "light"} border mx-2`} onClick={() => { setLogitech(false); setDareu(false) }}>
                                                Xem tất cả
                                            </button>
                                            <button className={`btn mx-2 ${searchPrice === true ? 'btn-danger' : 'btn-light border'}`} onClick={() => setSearchPrice(searchPrice === true ? false : true)}>
                                                Giá
                                            </button>
                                            {searchPrice && (
                                                <div className='search-price-modal'>
                                                    <div className='search-price-content'>
                                                        <div className='price-from flex justify-between mb-2'>
                                                            <span>{formatCurrency(priceRange[0])}</span>
                                                            <span>{formatCurrency(priceRange[1])}</span>
                                                        </div>
                                                        <Slider
                                                            min={0}
                                                            max={100000000}
                                                            range
                                                            defaultValue={priceRange}
                                                            onChange={handleSliderChange}
                                                        />
                                                        <div className='action'>
                                                            <button className='close-btn' onClick={() => setSearchPrice(false)}>
                                                                Đóng
                                                            </button>
                                                            <button className='search-btn mx-2' onClick={() => resultSearchPrice()}>
                                                                Xem kết quả
                                                            </button>
                                                        </div>
                                                    </div>
                                                </div>
                                            )}
                                        </div> : catID === "e57ad09d-6f21-482d-a074-1249dc32ebe4" ?
                                            <div className='category-product'>
                                                <button className={`btn btn-${akko === true ? "danger" : "light"} border my-2`} onClick={() => { setAkko(true) }}>
                                                    Akko
                                                </button>
                                                <button className={`btn btn-${akko === false ? "danger" : "light"} border mx-2`} onClick={() => { setAkko(false); }}>
                                                    Xem tất cả
                                                </button>
                                                <button className={`btn mx-2 ${searchPrice === true ? 'btn-danger' : 'btn-light border'}`} onClick={() => setSearchPrice(searchPrice === true ? false : true)}>
                                                    Giá
                                                </button>
                                                {searchPrice && (
                                                    <div className='search-price-modal'>
                                                        <div className='search-price-content'>
                                                            <div className='price-from flex justify-between mb-2'>
                                                                <span>{formatCurrency(priceRange[0])}</span>
                                                                <span>{formatCurrency(priceRange[1])}</span>
                                                            </div>
                                                            <Slider
                                                                min={0}
                                                                max={100000000}
                                                                range
                                                                defaultValue={priceRange}
                                                                onChange={handleSliderChange}
                                                            />
                                                            <div className='action'>
                                                                <button className='close-btn' onClick={() => setSearchPrice(false)}>
                                                                    Đóng
                                                                </button>
                                                                <button className='search-btn mx-2' onClick={() => resultSearchPrice()}>
                                                                    Xem kết quả
                                                                </button>
                                                            </div>
                                                        </div>
                                                    </div>
                                                )}
                                            </div> : <></>
                }
                <div className='product'>
                    {catID === "47845903-009d-4294-80cb-cd6549bd2dab" ? product.map((data) => (
                        <div className='card-product'>
                            <Link style={{ textDecoration: "none", color: "#222" }} to={`/product/${data.id}`}>
                                <div className='card-image'>
                                    <img src={`http://localhost:7777/${data.img}`} alt='' />
                                </div>
                                <div className='card-name'>
                                    <b>{data.name}</b>
                                </div>
                                <div className='price-cost'>
                                    <div className='price'>{formatCurrency(data.price)}</div>
                                </div>
                                <div className='love'>
                                    <i className='fa-solid fa-heart'></i>
                                </div>
                                {
                                    data.quantity > 0 ? <></>
                                        : <div className='sold-out'>
                                            <img src={sold} alt='' />
                                        </div>
                                }
                            </Link>
                        </div>
                    )) : catID === "3289f294-65b0-4dd8-a5dc-ef6ca442eb68" ?
                        product.map((data) => (
                            <div className='card-product'>
                                <Link style={{ textDecoration: "none", color: "#222" }} to={`/product/${data.id}`}>
                                    <div className='card-image'>
                                        <img src={`http://localhost:7777/${data.img}`} alt='' />
                                    </div>
                                    <div className='card-name'>
                                        <b>{data.name}</b>
                                    </div>
                                    <div className='price-cost'>
                                        <div className='price'>{formatCurrency(data.price)}</div>
                                    </div>
                                    <div className='love'>
                                        <i className='fa-solid fa-heart'></i>
                                    </div>
                                    {
                                        data.quantity > 0 ? <></>
                                            : <div className='sold-out'>
                                                <img src={sold} alt='' />
                                            </div>
                                    }
                                </Link>
                            </div>
                        )) : catID === "176d9ec8-f0d2-4159-b657-264fb8ec79f1" ?
                            product.map((data) => (
                                <div className='card-product'>
                                    <Link style={{ textDecoration: "none", color: "#222" }} to={`/product/${data.id}`}>
                                        <div className='card-image'>
                                            <img src={`http://localhost:7777/${data.img}`} alt='' />
                                        </div>
                                        <div className='card-name'>
                                            <b>{data.name}</b>
                                        </div>
                                        <div className='price-cost'>
                                            <div className='price'>{formatCurrency(data.price)}</div>
                                        </div>
                                        <div className='love'>
                                            <i className='fa-solid fa-heart'></i>
                                        </div>
                                        {
                                            data.quantity > 0 ? <></>
                                                : <div className='sold-out'>
                                                    <img src={sold} alt='' />
                                                </div>
                                        }
                                    </Link>
                                </div>
                            )) : catID === "b14de4e4-cf6e-4a4b-8996-9198940164a2" ?
                                product.map((data) => (
                                    <div className='card-product'>
                                        <Link style={{ textDecoration: "none", color: "#222" }} to={`/product/${data.id}`}>
                                            <div className='card-image'>
                                                <img src={`http://localhost:7777/${data.img}`} alt='' />
                                            </div>
                                            <div className='card-name'>
                                                <b>{data.name}</b>
                                            </div>
                                            <div className='price-cost'>
                                                <div className='price'>{formatCurrency(data.price)}</div>
                                            </div>
                                            <div className='love'>
                                                <i className='fa-solid fa-heart'></i>
                                            </div>
                                            {
                                                data.quantity > 0 ? <></>
                                                    : <div className='sold-out'>
                                                        <img src={sold} alt='' />
                                                    </div>
                                            }
                                        </Link>
                                    </div>
                                )) : catID === "917bd3c0-666c-4753-af14-3832b037da98" ?
                                    product.map((data) => (
                                        <div className='card-product'>
                                            <Link style={{ textDecoration: "none", color: "#222" }} to={`/product/${data.id}`}>
                                                <div className='card-image'>
                                                    <img src={`http://localhost:7777/${data.img}`} alt='' />
                                                </div>
                                                <div className='card-name'>
                                                    <b>{data.name}</b>
                                                </div>
                                                <div className='price-cost'>
                                                    <div className='price'>{formatCurrency(data.price)}</div>
                                                </div>
                                                <div className='love'>
                                                    <i className='fa-solid fa-heart'></i>
                                                </div>
                                                {
                                                    data.quantity > 0 ? <></>
                                                        : <div className='sold-out'>
                                                            <img src={sold} alt='' />
                                                        </div>
                                                }
                                            </Link>
                                        </div>
                                    )) : catID === "e57ad09d-6f21-482d-a074-1249dc32ebe4" ?
                                        product.map((data) => (
                                            <div className='card-product'>
                                                <Link style={{ textDecoration: "none", color: "#222" }} to={`/product/${data.id}`}>
                                                    <div className='card-image'>
                                                        <img src={`http://localhost:7777/${data.img}`} alt='' />
                                                    </div>
                                                    <div className='card-name'>
                                                        <b>{data.name}</b>
                                                    </div>
                                                    <div className='price-cost'>
                                                        <div className='price'>{formatCurrency(data.price)}</div>
                                                    </div>
                                                    <div className='love'>
                                                        <i className='fa-solid fa-heart'></i>
                                                    </div>
                                                    {
                                                        data.quantity > 0 ? <></>
                                                            : <div className='sold-out'>
                                                                <img src={sold} alt='' />
                                                            </div>
                                                    }
                                                </Link>
                                            </div>
                                        )) : <></>
                    }
                </div>
                {
                    totalPage > 1 &&
                    <div className='pagination'>
                        <Link to='#' onClick={handlePrev}>
                            Prev
                        </Link>
                        {pagination}
                        <Link to='#' onClick={handleNext}>
                            Next
                        </Link>
                    </div>
                }
            </div>
        </div>
    );
};

export default Category;