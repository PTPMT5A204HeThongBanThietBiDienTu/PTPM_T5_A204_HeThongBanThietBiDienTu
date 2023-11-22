import axios from 'axios';
import React, { useEffect, useState } from 'react'
import { useParams } from 'react-router-dom'
import '../styles/Detail.scss';
import InfoDetail from '../components/Detail/InfoDetail';
import PaymentDetail from '../components/Detail/PaymentDetail';
import SpecificationDetail from '../components/Detail/SpecificationDetail';
import SimilarProductDetail from '../components/Detail/SimilarProductDetail';
import sold from '../assets/icons/sold.png';

const Detail = ({ name }) => {
    const { id } = useParams();
    const [productByID, setProductByID] = useState([]);
    useEffect(() => {
        axios.get(`http://localhost:7777/api/v1/product/${id}`)
            .then(res => {
                if (res && res.data) {
                    setProductByID(res.data.data);
                }
            }).catch(err => console.log(err));
    }, [setProductByID, id]);
    function formatCurrency(amount) {
        const formatter = new Intl.NumberFormat('vi-VN', {
            style: 'currency',
            currency: 'VND',
        });
        return formatter.format(amount);
    }
    return (
        <>
            <div className='detail-container'>
                <div className='all-detail'>
                    <div className='d-flex justify-content-between'>
                        <div className='detail-name pt-4'><h3>{productByID.proName}</h3></div>
                        <div className='detail-price pt-4'><h3>{formatCurrency(productByID.price)}</h3></div>
                    </div>
                    <hr />
                    <div className='image'>
                        <img src={`http://localhost:7777/${productByID.img}`} alt='' />
                        <div className='love'>
                            <i className='fa-solid fa-heart'></i>
                        </div>
                        {
                            productByID.quantity > 0 ? <></>
                                : <div className='sold-out'>
                                    <img src={sold} alt='' />
                                </div>
                        }
                    </div>
                    <div className='detail-content'>
                        <div className='detail-left'>
                            <InfoDetail />
                        </div>
                        <div className='detail-right mt-2'>
                            <PaymentDetail quantity={productByID.quantity} name={name} productByID={productByID} />
                        </div>
                    </div>
                    <SpecificationDetail id={id} />
                    <SimilarProductDetail />
                    {/* <div className='reviews-comments'>
                        <b className='title'>Reviews & comments</b>
                        {
                            reviews.length > 0 ? reviews.map((data) => {
                                let star = parseInt(data.star);
                                return (
                                    <div key={data.rvId}>
                                        <div className='top'>
                                            <div className='content'>
                                                <div className='name-customer d-flex '><i class="fa-solid fa-user"></i><b>{data.customer.name}</b></div>
                                                <div className='star mt-2'><StarRatings
                                                    rating={star}
                                                    starRatedColor="#ffbf00"
                                                    numberOfStars={5}
                                                    name='rating'
                                                    starDimension="20px"
                                                    starSpacing="1px"
                                                /></div>
                                            </div>
                                            {
                                                role === 'admin' ? <i onClick={() => handleRemoveRating(data.rvId)} class="fa-solid fa-trash trash"></i> : <></>
                                            }
                                        </div>
                                        <div className='comment'>
                                            <p>{data.content}</p>
                                        </div>
                                        <hr />
                                    </div>
                                )
                            }) : <></>
                        }
                    </div> : <></> */}

                </div>
            </div>
        </>
    )
}

export default Detail