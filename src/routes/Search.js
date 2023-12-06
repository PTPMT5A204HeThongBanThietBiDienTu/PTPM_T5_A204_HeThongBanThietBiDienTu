import axios from 'axios'
import React, { useCallback, useEffect, useState } from 'react'
import { Link, useParams } from 'react-router-dom'
import sold from '../assets/icons/sold.png'
import "../styles/Search.scss"
import Slider from 'rc-slider'

const Search = () => {
    const { search } = useParams();
    const [product, setProduct] = useState([])
    const [totalCount, setTotalCount] = useState(0)
    const [page, setPage] = useState(1);
    const [totalPage, setTotalPage] = useState(0);
    const [pagination, setPagination] = useState([]);
    const [priceRange, setPriceRange] = useState([0, 100000000]);
    const [inputSearch, setInputSearch] = useState(false);
    const handleInputChange = (e, type) => {
        const value = parseFloat(e.target.value);
        if (!isNaN(value)) {
            setPriceRange((prevRange) => ({
                ...prevRange,
                [type === 'min' ? 0 : 1]: value,
            }));
        }
    };
    const handleSliderChange = (value) => {
        setPriceRange(value);
    }
    const resultSearchPrice = () => {
        const dataSearchPrice = {
            minPrice: priceRange[0],
            maxPrice: priceRange[1],
            content: search
        }
        axios.post(`http://localhost:7777/api/v1/product/search/?page=${page}`, dataSearchPrice)
            .then(res => {
                if (res && res.data.success === true) {
                    setProduct(res.data.data);
                    setTotalPage(res.data.totalPages)
                    setTotalCount(res.data.totalCount)
                }
            }).catch(err => console.log(err))
    }
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
        if (search !== '') {
            const handleSearch = () => {
                const valueSearch = {
                    content: search,
                    ...(priceRange && {
                        minPrice: priceRange[0],
                        maxPrice: priceRange[1],
                    }),
                };
                axios.post(`http://localhost:7777/api/v1/product/search/?page=${page}`, valueSearch)
                    .then(res => {
                        if (res && res.data.success === true) {
                            setProduct(res.data.data)
                            setTotalPage(res.data.totalPages)
                            setTotalCount(res.data.totalCount)
                        }
                    }).catch(err => console.log(err));
            }
            handleSearch();
        }
    }, [search, page, priceRange]);
    console.log(product);
    function formatCurrency(amount) {
        const formatter = new Intl.NumberFormat('vi-VN', {
            style: 'currency',
            currency: 'VND',
        });
        return formatter.format(amount);
    }
    return (
        <div className='fluid-search'>
            {
                product.length > 0 ?
                    <div className='all-search'>
                        <div className='left'>
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
                                        {/* <button className='close-btn mx-2' onClick={() => setSearchPrice(false)}>
                                            Đóng
                                        </button> */}
                                        <button className='search-btn mx-2' onClick={() => resultSearchPrice()}>
                                            Xem kết quả
                                        </button>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div className='right'>
                            <h3 className='mt-3'>Có <b>{totalCount}</b> kết quả tìm kiếm</h3>
                            <div className='search'>
                                {product.map((data) => (
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
                                ))}
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
                    </div> : <div className='all-info d-flex vh-100 justify-content-center align-items-center flex-column'>
                        <h3 className='fw-bold text-danger'>Không có kết quả tìm kiếm !!!</h3>
                    </div>
            }
        </div>
    )
}

export default Search