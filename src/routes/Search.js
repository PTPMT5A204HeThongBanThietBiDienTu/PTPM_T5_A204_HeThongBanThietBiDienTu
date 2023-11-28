import axios from 'axios'
import React, { useEffect, useState } from 'react'
import { Link, useLocation } from 'react-router-dom'
import sold from '../assets/icons/sold.png'
import "../styles/Search.scss"

const Search = () => {
    const location = useLocation();
    const { search } = location.state || {};
    const [product, setProduct] = useState([])
    useEffect(() => {
        if (search !== '') {
            const handleSearch = () => {
                const valueSearch = {
                    content: search
                }
                axios.post(`http://localhost:7777/api/v1/product/search`, valueSearch)
                    .then(res => {
                        if (res && res.data.success === true) {
                            setProduct(res.data.data)
                        }
                    }).catch(err => console.log(err));
            }
            handleSearch();
        }
    }, [search]);

    function formatCurrency(amount) {
        const formatter = new Intl.NumberFormat('vi-VN', {
            style: 'currency',
            currency: 'VND',
        });
        return formatter.format(amount);
    }
    return (
        <div className='container-fluid'>
            <div className='all-search'>
                {
                    product.length > 0 ?
                        <>
                            <h3 className='mt-3'>Có <b>{product.length}</b> kết quả tìm kiếm</h3>
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
                        </> : <div className='all-info d-flex vh-100 justify-content-center align-items-center flex-column'>
                            <h3 className='fw-bold text-danger'>Không có kết quả tìm kiếm !!!</h3>
                        </div>
                }
            </div>
        </div>
    )
}

export default Search