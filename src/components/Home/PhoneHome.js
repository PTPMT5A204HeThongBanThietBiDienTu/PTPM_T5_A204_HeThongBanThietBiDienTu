import React, { useEffect, useState } from 'react'
import axios from 'axios';
import Carousel from "react-multi-carousel";
import "react-multi-carousel/lib/styles.css";
import { Link } from 'react-router-dom';

const ProductHome = (props) => {
    const { iphone, samsung } = props;
    const Responsive = {
        superLargeDesktop: {
            breakpoint: { max: 4000, min: 3000 },
            items: 5
        },
        desktop: {
            breakpoint: { max: 3000, min: 1200 },
            items: 5
        },
        tablet: {
            breakpoint: { max: 1200, min: 1000 },
            items: 4
        },
        mobile: {
            breakpoint: { max: 1000, min: 0 },
            items: 3
        },
        mini_mobile: {
            breakpoint: { max: 700, min: 0 },
            items: 2
        },
        min_mobile: {
            breakpoint: { max: 483, min: 0 },
            items: 1
        }
    };
    const [product, setProduct] = useState([]);

    useEffect(() => {
        if (iphone === false && samsung === false) {
            axios.get(`http://localhost:7777/api/v1/product/getByCatId/47845903-009d-4294-80cb-cd6549bd2dab`).then(res => {
                if (res && res.data) {
                    setProduct(res.data.data);
                }
            }).catch(err => console.log(err));
        } else if (iphone === true) {
            axios.get(`http://localhost:7777/api/v1/product/getAllByCatIdAndBraId/?catId=47845903-009d-4294-80cb-cd6549bd2dab&braId=9e5b2654-a901-46b5-8f23-819dcec28457`).then(res => {
                if (res && res.data) {
                    setProduct(res.data.data);
                }
            }).catch(err => console.log(err));
        } else if (samsung === true) {
            axios.get(`http://localhost:7777/api/v1/product/getAllByCatIdAndBraId/?catId=47845903-009d-4294-80cb-cd6549bd2dab&braId=ab84deca-6afa-4f4a-a826-46342f72d80e`).then(res => {
                if (res && res.data) {
                    setProduct(res.data.data)
                }
            }).catch(err => console.log(err));
        }
    }, [iphone, samsung])
    function formatCurrency(amount) {
        const formatter = new Intl.NumberFormat('vi-VN', {
            style: 'currency',
            currency: 'VND',
        });
        return formatter.format(amount);
    }
    return (
        <Carousel
            additionalTransfrom={0}
            arrows
            autoPlay
            autoPlaySpeed={3000}
            centerMode={false}
            className=""
            containerClass="container-with-dots"
            dotListClass=""
            draggable
            focusOnSelect={false}
            infinite
            itemClass=""
            keyBoardControl
            minimumTouchDrag={80}
            renderButtonGroupOutside={false}
            reverse
            // showDots={true}
            ssr={true}
            responsive={Responsive}>
            {
                product.map((value) => {
                    return (
                        <div className='card-product'>
                            <Link style={{ textDecoration: "none", color: "#222" }} to={`/product/${value.id}`}>
                                <div className='card-image'>
                                    <img src={`http://localhost:7777/${value.img}`} alt='' />
                                </div>
                                <div className='card-name'>
                                    <b>{value.name}</b>
                                </div>
                                <div className='price'>{formatCurrency(value.price)}</div>
                                <div className='love flex'>
                                    <p className='text-sm fw-bold mr-1'>Yêu thích</p>
                                    <i className='fa-solid fa-heart'></i>
                                </div>
                            </Link>
                        </div>
                    )
                })
            }
        </Carousel>
    )
}

export default ProductHome