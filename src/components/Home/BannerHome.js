import React from 'react'
import banner4 from '../../assets/images/banner-4.png'
import banner5 from '../../assets/images/banner-5.png'
import banner6 from '../../assets/images/banner-6.png'
const BannerHome = () => {
    return (
        <div className='grid-item banner'>
            <div className='banner-item'>
                <img src={banner4} alt='' />
            </div>
            <div className='banner-item'>
                <img src={banner5} alt='' />
            </div>
            <div className='banner-item'>
                <img src={banner6} alt='' />
            </div>
        </div>
    )
}

export default BannerHome