import React, { useCallback, useEffect, useState } from 'react';
import axios from 'axios';
import { Link, useParams } from 'react-router-dom';
import "../styles/Category.scss";
import Slider from 'rc-slider';
import 'rc-slider/assets/index.css';
import sold from '../assets/icons/sold.png'
const Category = () => {
    const { catID, braId } = useParams();
    const [product, setProduct] = useState([]);
    const [selectedOption, setSelectedOption] = useState('');
    const [brand, setBrand] = useState([])
    const [searchPrice, setSearchPrice] = useState(false);
    const [inputSearch, setInputSearch] = useState(false)
    const handleChange = (event) => {
        setSelectedOption(event.target.value);
    };
    const handleSliderChange = (value) => {
        setPriceRange(value);
    }
    const [priceRange, setPriceRange] = useState([0, 100000000]);
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
    const viewAllData = useCallback(() => {
        axios.get(`http://localhost:7777/api/v1/product/getByCatId/${catID}?page=${page}`).then(res => {
            if (res && res.data) {
                setProduct(res.data.data);
                setTotalPage(res.data.totalPages)
            }
        }).catch(err => console.log(err));
    }, [catID, page])
    useEffect(() => {
        if (selectedOption === "" && braId === undefined) {
            viewAllData();
        } else if (selectedOption !== "") {
            axios.get(`http://localhost:7777/api/v1/product/getAllByCatIdAndBraId?catId=${catID}&braId=${selectedOption}`)
                .then(res => {
                    if (res && res.data) {
                        setProduct(res.data.data);
                        setTotalPage(res.data.totalPages);
                    }
                })
                .catch(err => console.log(err));
        } else if (braId !== undefined) {
            axios.get(`http://localhost:7777/api/v1/product/getAllByCatIdAndBraId?catId=${catID}&braId=${braId}`)
                .then(res => {
                    if (res && res.data) {
                        setProduct(res.data.data);
                        setTotalPage(res.data.totalPages);
                    }
                })
                .catch(err => console.log(err));
        }
    }, [viewAllData, catID, braId, selectedOption]);

    useEffect(() => {
        axios.get(`http://localhost:7777/api/v1/brand/getAllByCatId/${catID}`)
            .then(res => {
                if (res && res.data.success === true) {
                    setBrand(res.data.data)
                }
            }).catch(err => console.log(err))
    }, [catID])
    function formatCurrency(amount) {
        const formatter = new Intl.NumberFormat('vi-VN', {
            style: 'currency',
            currency: 'VND',
        });
        return formatter.format(amount);
    }
    const handleInputChange = (e, type) => {
        const value = parseFloat(e.target.value);
        if (!isNaN(value)) {
            setPriceRange((prevRange) => ({
                ...prevRange,
                [type === 'min' ? 0 : 1]: value,
            }));
        }
    };
    return (
        <div className='container-fluid'>
            <div className='all-product'>
                <div className='all-action flex mx-2'>
                    <select id="mySelect" value={selectedOption} onChange={handleChange} className='form-control '>
                        <option value="" >-- Chọn --</option>
                        {
                            brand.map((data) => (
                                <option value={data.braId} >{data.brand.name}</option>
                            ))
                        }
                    </select>
                    <button className={`btn btn-light`} onClick={viewAllData}>
                        Xem tất cả
                    </button>
                    <div className='search-price'>
                        <button className={`search-price btn ${searchPrice === true ? 'btn-danger' : 'btn-light border'}`} onClick={() => setSearchPrice(searchPrice === true ? false : true)}>
                            Giá
                        </button>
                        {searchPrice && (
                            <div className='search-price-modal'>
                                <div className='search-price-content'>
                                    <div className='price-from mb-2 flex flex-col'>
                                        <button className={`btn btn-${inputSearch === true ? "danger" : "light"} w-25 my-2`} onClick={() => setInputSearch(inputSearch === true ? false : true)}>Nhập</button>
                                        {
                                            inputSearch === true &&
                                            <div className='flex justify-between'>
                                                <span className='mx-2'>
                                                    <input
                                                        type='number'
                                                        value={priceRange[0]}
                                                        min={"0"}
                                                        max={"100000000"}
                                                        onChange={(e) => handleInputChange(e, 'min')}
                                                        className='form-control'
                                                    />
                                                </span>
                                                <span className='mx-2'>
                                                    <input
                                                        type='number'
                                                        value={priceRange[1]}
                                                        min={"0"}
                                                        max={"100000000"}
                                                        onChange={(e) => handleInputChange(e, 'max')}
                                                        className='form-control'
                                                    />
                                                </span>
                                            </div>
                                        }
                                        <div className='flex justify-between'>
                                            <span>{formatCurrency(priceRange[0])}</span>
                                            <span>{formatCurrency(priceRange[1])}</span>
                                        </div>
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
                                        <button className='search-btn' onClick={() => resultSearchPrice()}>
                                            Xem kết quả
                                        </button>
                                    </div>
                                </div>
                            </div>
                        )}
                    </div>
                </div>
                <div className='product'>
                    {catID !== "" ? product.map((data) => (
                        <Link style={{ textDecoration: "none", color: "#222" }} to={`/product/${data.id}`}>
                            <div className='card-product'>

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
                            </div>
                        </Link>
                    )) : <div className='all-info d-flex vh-100 justify-content-center align-items-center flex-column'>
                        <h1 className='fw-bold text-danger'>Không có kết quả</h1>
                    </div>
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