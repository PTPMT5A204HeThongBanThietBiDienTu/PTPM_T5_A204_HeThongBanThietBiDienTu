import axios from 'axios';
import React, { useEffect, useState } from 'react'
import Carousel from 'react-multi-carousel';
import { Link } from 'react-router-dom';
const SimilarProductDetail = ({ catId, braId, handleScrollToTop }) => {
    const responsive = {
        superLargeDesktop: {
            breakpoint: { max: 4000, min: 3000 },
            items: 5
        },
        desktop: {
            breakpoint: { max: 3000, min: 1024 },
            items: 4
        },
        tablet: {
            breakpoint: { max: 1200, min: 1071 },
            items: 3
        },
        mobile: {
            breakpoint: { max: 1070, min: 723 },
            items: 2
        },
        super_mobile: {
            breakpoint: { max: 722, min: 0 },
            items: 1
        }
    };
    const [product, setProduct] = useState([]);
    useEffect(() => {
        axios.get(`http://localhost:7777/api/v1/product/getAllByCatIdAndBraId/?catId=${catId}&braId=${braId}`).then(res => {
            if (res && res.data) {
                setProduct(res.data.data);
            }
        }).catch(err => console.log(err));
    }, [catId, braId])
    return (
        <div className='similar-product'>
            <h3>
                Sản phẩm tương tự
            </h3>
            <Carousel responsive={responsive} autoPlay={true} infinite={true}>
                {
                    product.map((value) => (

                        <div className='card-product' key={value._id} >
                            <Link style={{ textDecoration: "none", color: "#222" }} to={`/product/${value.id}`} onClick={handleScrollToTop}>
                                <div className='card-image'>
                                    <img src={`http://localhost:7777/${value.img}`} alt='' />
                                </div>
                                <div className='card-name'>
                                    <b>{value.name}</b>
                                </div>
                                <div className='price-cost'>
                                    <div className='price'>{value.price.toLocaleString()}đ</div>
                                </div>
                                <div className='love flex'>
                                    <p className='text-sm fw-bold mr-1'>Yêu thích</p>
                                    <i className='fa-solid fa-heart'></i>
                                </div>
                            </Link>
                        </div>

                    ))
                }
            </Carousel>
        </div>
    )
}

export default SimilarProductDetail