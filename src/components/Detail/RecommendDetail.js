import axios from 'axios';
import React, { useCallback, useEffect, useState } from 'react'
import Carousel from 'react-multi-carousel';
import { Link } from 'react-router-dom';
import { toast } from 'react-toastify';

const RecommendDetail = (props) => {
    const { formatCurrency, handleScrollToTop, productByID, name } = props
    const [recommend, setRecommend] = useState(false)
    const [proById, setProById] = useState([])
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
    const getAllAccompany = useCallback(() => {
        axios.get(`http://localhost:7777/api/v1/product/getAllAccompany/${productByID.id}`).then(res => {
            if (res && res.data.success === true) {
                const accompany = res.data.data[0].accompany;
                const accompanyArray = accompany.split(',');
                accompanyArray.pop();
                setProduct(accompanyArray);
            }
        }).catch(err => console.log(err));
    }, [productByID.id])
    useEffect(() => {
        getAllAccompany()
    }, [getAllAccompany])
    const handleAddtoCart = async (proId) => {
        if (name === '') {
            toast.error('Bạn phải đăng nhập mới được thêm sản phẩm vào giỏ hàng !!!');
        } else {
            try {
                const dataCart = {
                    proId: proId
                }
                axios.post(`http://localhost:7777/api/v1/cart/create`, dataCart)
                    .then(res => {
                        if (res && res.data.success === true) {
                            toast.success('Đã thêm sản phẩm vào giỏ hàng');
                        } else {
                            toast.error(`Thêm sản phẩm vào giỏ hàng thất bại`);
                        }
                    }).catch(err => {
                        if (err.response.data.message === "Quantity has reached the limit") {
                            toast.warning('Đã đạt giới hạn thêm vào giỏ hàng !!! Vui lòng liên hệ: 0587928264 nếu muốn mua nhiều hơn');
                        } else if (err.response.data.message === "The product is out of stock") {
                            toast.warning('Đang không có sẵn hàng !!! Vui lòng liên hệ: 0587928264 nếu muốn mua nhiều hơn');
                        } else {
                            console.log(err);
                        }
                    });
            } catch (error) {
                console.error(error);
            }

        }
    };
    const getProductDetails = useCallback(async () => {
        try {
            const promises = product.map(productId =>
                axios.get(`http://localhost:7777/api/v1/product/${productId}`)
            );

            const results = await Promise.all(promises);

            const productDetails = results.map(res => res.data.data);

            setProById(productDetails);
        } catch (error) {
            console.error('Error fetching product details:', error);
        }
    }, [product])
    useEffect(() => {
        if (product.length > 0) {
            getProductDetails();
        }
    }, [product, getProductDetails]);
    return (
        proById.length > 0 &&
        <div className='similar-product my-4'>
            <button className={`btn btn-${recommend === false ? 'success' : 'danger'}`} onClick={() => setRecommend(recommend === false ? true : false)}>
                Mua kèm giá sốc
            </button>
            {
                recommend === true &&
                <Carousel responsive={responsive} autoPlay={true} infinite={true}>
                    {
                        proById.map((value) => (
                            <div className='card-product' key={value.id} >
                                <Link style={{ textDecoration: "none", color: "#222" }} to={`/product/${value.id}`} onClick={handleScrollToTop}>
                                    <div className='card-image'>
                                        <img src={`http://localhost:7777/${value.img}`} alt='' />
                                    </div>
                                    <div className='card-name'>
                                        <b>{value.name}</b>
                                    </div>
                                    <div className='price-cost'>
                                        <div className='price'>{formatCurrency(value.price)}</div>
                                    </div>
                                </Link>
                                <button className='add-to-cart-recommend btn btn-danger' onClick={() => handleAddtoCart(value.id)}>Thêm vào giỏ hàng</button>
                            </div>
                        ))
                    }
                </Carousel>
            }
        </div>
    )
}

export default RecommendDetail