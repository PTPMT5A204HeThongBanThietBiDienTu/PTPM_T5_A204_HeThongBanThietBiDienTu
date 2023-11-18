import React, { useState } from 'react'
import '../styles/Home.scss'
import Category from '../components/Home/CategoryHome';
import CarouselHome from '../components/Home/CarouselHome'
import BannerHome from '../components/Home/BannerHome';
import PhoneHome from '../components/Home/PhoneHome';
import LaptopHome from '../components/Home/LaptopHome';

const Home = () => {
    const [iphone, setIphone] = useState(false);
    const [samsung, setSamsung] = useState(false);
    const [acer, setAcer] = useState(false);
    const [asus, setAsus] = useState(false);

    return (
        <div className='wallpaper'>
            <div className='top grid-container'>
                <Category />
                <CarouselHome />
                <BannerHome />
            </div>
            <div className='mid mt-8'>
                <div className='title flex'>
                    <h4>ĐIỆN THOẠI NỔI BẬT NHẤT</h4>
                    <div>
                        <button className={`btn ${iphone && !samsung ? 'btn-danger' : 'btn-light'} ml-2`} onClick={() => { setIphone(true); setSamsung(false) }}>IPhone</button>
                        <button className={`btn ${!iphone && samsung ? 'btn-danger' : 'btn-light'} ml-2`} onClick={() => { setIphone(false); setSamsung(true) }}>Samsung</button>
                        <button className={`btn ${!iphone && !samsung ? 'btn-danger' : 'btn-light'} ml-2`} onClick={() => { setIphone(false); setSamsung(false) }}>Xem tất cả</button>
                    </div>
                </div>

                <div className='content mt-4'>
                    <PhoneHome iphone={iphone} samsung={samsung} />
                </div>

            </div>
            <div className='mid mt-8'>
                <div className='title flex'>
                    <h4>LAPTOP NỔI BẬT NHẤT</h4>
                    <div>
                        <button className={`btn ${acer && !asus ? 'btn-danger' : 'btn-light'} ml-2`} onClick={() => { setAcer(true); setAsus(false) }}>Acer</button>
                        <button className={`btn ${!acer && asus ? 'btn-danger' : 'btn-light'} ml-2`} onClick={() => { setAcer(false); setAsus(true) }}>Asus</button>
                        <button className={`btn ${!acer && !asus ? 'btn-danger' : 'btn-light'} ml-2`} onClick={() => { setAcer(false); setAsus(false) }}>Xem tất cả</button>
                    </div>
                </div>

                <div className='content mt-4'>
                    <LaptopHome acer={acer} asus={asus} />
                </div>

            </div>
        </div>
    )
}

export default Home