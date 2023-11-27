import axios from 'axios'
import React, { useEffect, useState } from 'react'
import { Link } from 'react-router-dom';

const Category = () => {
    const [category, setCategory] = useState([]);
    useEffect(() => {
        axios.get('http://localhost:7777/api/v1/category/')
            .then(res => {
                if (res && res.data.success === true) {
                    setCategory(res.data.data)
                }
            })
    }, [])
    return (
        <div className='all-category'>
            <div className='grid-item category flex flex-col'>
                {
                    category.length > 0 && category.map((data) => (

                        <div className='grid-item category flex flex-col'>
                            <Link to={`/category/${data.id}`} style={{ textDecoration: "none", color: "black" }}>
                                <div className='category-item justify-between flex'>

                                    <div className='flex'><span class="material-icons">
                                        {
                                            data.name === "Điện thoại" ? <>smartphone</> : data.name === "Laptop" && <>laptop</>
                                        }
                                    </span> <p>{data.name}</p></div>
                                    <div className='text-2xl'>{'>'}</div>
                                </div>
                            </Link>
                        </div>

                    ))
                }
            </div>
        </div>

        // <div className='grid-item category flex flex-col'>
        //     <div className='category-item justify-between flex'>
        //         <div className='flex'><span class="material-icons">
        //             smartphone
        //         </span> <p>Điện thoại</p></div>
        //         <div className='text-2xl'>{'>'}</div>
        //     </div>
        //     <div className='category-item justify-between flex'>
        //         <div className='flex'><span class="material-icons">
        //             laptop
        //         </span> <p>Laptop</p></div><div className='text-2xl'>{'>'}</div>
        //     </div>
        //     <div className='category-item justify-between flex'>
        //         <div className='flex'><span class="material-icons">
        //             headphones
        //         </span> <p>Âm thanh</p></div><div className='text-2xl'>{'>'}</div>
        //     </div>
        //     <div className='category-item justify-between flex'>
        //         <div className='flex'><span class="material-icons">
        //             watch
        //         </span> <p>Đồng hồ</p></div><div className='text-2xl'>{'>'}</div>
        //     </div>
        //     <div className='category-item justify-between flex'>
        //         <div className='flex'><span class="material-icons">
        //             other_houses
        //         </span> <p>Gia dụng, Smarthome</p></div><div className='text-2xl'>{'>'}</div>
        //     </div>
        //     <div className='category-item justify-between flex'>
        //         <div className='flex'><span class="material-icons">
        //             battery_charging_full
        //         </span> <p>Phụ kiện</p></div><div className='text-2xl'>{'>'}</div>
        //     </div>
        //     <div className='category-item justify-between flex'>
        //         <div className='flex'><span class="material-icons">
        //             devices
        //         </span> <p>Pc, Màn hình</p></div><div className='text-2xl'>{'>'}</div>
        //     </div>
        //     <div className='category-item justify-between flex'>
        //         <div className='flex'><span class="material-icons">
        //             tv
        //         </span> <p>Tivi</p></div><div className='text-2xl'>{'>'}</div>
        //     </div>
        //     <div className='category-item justify-between flex'>
        //         <div className='flex'><span class="material-icons">
        //             currency_exchange
        //         </span> <p>Thu cũ đổi mới</p></div><div className='text-2xl'>{'>'}</div>
        //     </div>
        //     <div className='category-item justify-between flex'>
        //         <div><span class="material-icons">
        //             edgesensor_low
        //         </span> <p>Hàng cũ</p></div><div className='text-2xl'>{'>'}</div>
        //     </div>
        //     <div className='category-item justify-between flex'>
        //         <div className='flex'><span class="material-icons">
        //             campaign
        //         </span> <p>Khuyến mãi</p></div><div className='text-2xl'>{'>'}</div>
        //     </div>
        //     <div className='category-item justify-between flex'>
        //         <div className='flex'><span class="material-icons">
        //             feed
        //         </span> <p>Tin công nghệ</p></div><div className='text-2xl'>{'>'}</div>
        //     </div>
        // </div>
    )
}

export default Category