import React, { useCallback, useEffect, useState } from 'react';
import axios from 'axios';
import { Link, useParams } from 'react-router-dom';
import "../styles/Category.scss";
import Slider from 'rc-slider';
import 'rc-slider/assets/index.css';
const Category = () => {
    const { catID } = useParams();
    const [product, setProduct] = useState([]);
    const [iphone, setIphone] = useState(false);
    const [samsung, setSamsung] = useState(false);
    const [acer, setAcer] = useState(false);
    const [asus, setAsus] = useState(false);
    const [searchPrice, setSearchPrice] = useState(false);
    const [priceRange, setPriceRange] = useState([0, 100000000]);

    const handleSliderChange = (value) => {
        setPriceRange(value);
    }
    const resultSearchPrice = () => {
        console.log(priceRange);
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
            setPagination(paginationItems);
        }
    }, [totalPage, page]);
    useEffect(() => {
        if (catID === "47845903-009d-4294-80cb-cd6549bd2dab") {
            if (iphone === false && samsung === false) {
                axios.get(`http://localhost:7777/api/v1/product/getByCatId/47845903-009d-4294-80cb-cd6549bd2dab`).then(res => {
                    if (res && res.data) {
                        setProduct(res.data.data);
                    }
                }).catch(err => console.log(err));
            } else if (iphone === true) {
                axios.get(`http://localhost:7777/api/v1/product/getByBraId/9e5b2654-a901-46b5-8f23-819dcec28457`).then(res => {
                    if (res && res.data) {
                        setProduct(res.data.data);
                    }
                }).catch(err => console.log(err));
            } else {
                axios.get(`http://localhost:7777/api/v1/product/getByBraId/ab84deca-6afa-4f4a-a826-46342f72d80e`).then(res => {
                    if (res && res.data) {
                        setProduct(res.data.data);
                    }
                }).catch(err => console.log(err));
            }
        }
        else if (catID === "3289f294-65b0-4dd8-a5dc-ef6ca442eb68") {
            if (acer === false && asus === false) {
                axios.get(`http://localhost:7777/api/v1/product/getByCatId/3289f294-65b0-4dd8-a5dc-ef6ca442eb68`).then(res => {
                    if (res && res.data) {
                        setProduct(res.data.data);
                    }
                }).catch(err => console.log(err));
            } else if (acer === true) {
                axios.get(`http://localhost:7777/api/v1/product/getByBraId/90320d05-772f-41e5-a077-411532c68145`).then(res => {
                    if (res && res.data) {
                        setProduct(res.data.data);
                    }
                }).catch(err => console.log(err));
            } else {
                axios.get(`http://localhost:7777/api/v1/product/getByBraId/9757c182-3f7d-44d7-a2b6-b0cef9917778`).then(res => {
                    if (res && res.data) {
                        setProduct(res.data.data);
                    }
                }).catch(err => console.log(err));
            }
        }
    }, [iphone, samsung, acer, asus, catID])
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
                        </div> : catID === "3289f294-65b0-4dd8-a5dc-ef6ca442eb68" &&
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
                        </div>
                }


                <div className='sort'>
                    {/* <b>Sort by price</b>
                    <div className='icon-sort'>
                        <div className='sort-desc my-2'>
                            <button className='btn btn-light border' onClick={() => sortDESC()}>
                                <i className='fa-solid fa-arrow-down-wide-short' style={{ color: '#000000' }}></i>
                                High - Low
                            </button>
                        </div>
                        <div className='sort-asc mx-2 my-2'>
                            <button className='btn btn-light border' onClick={() => sortASC()}>
                                <i className='fa-solid fa-arrow-down-short-wide' style={{ color: '#000000' }}></i>
                                Low - High
                            </button>
                        </div>
                        <div className='refresh mx-2 my-2'>
                            <button className='btn btn-light border' onClick={normal}>
                                <i className='fa-solid fa-arrows-rotate' style={{ color: '#000000' }}></i>
                                Refresh
                            </button>
                        </div>
                    </div> */}
                </div>
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
                                {/* {
                                data.quantity > 0 ? <></>
                                    : <div className='sold-out'>
                                        <img src={`http://localhost:1234/icon/sold.png`} alt='' />
                                    </div>
                            } */}
                            </Link>
                        </div>
                    )) : catID === "3289f294-65b0-4dd8-a5dc-ef6ca442eb68" &&
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
                                {/* {
                                data.quantity > 0 ? <></>
                                    : <div className='sold-out'>
                                        <img src={`http://localhost:1234/icon/sold.png`} alt='' />
                                    </div>
                            } */}
                            </Link>
                        </div>
                    ))

                    }
                </div>
                {/* {
                    !isSeries && !isSorted &&
                    (
                        <div className='pagination' onClick={scrollToTop}>
                            <Link to='#' onClick={handlePrev}>
                                Prev
                            </Link>
                            {pagination}
                            <Link to='#' onClick={handleNext}>
                                Next
                            </Link>
                        </div>
                    )
                } */}
            </div>
        </div>
    );
};

export default Category;