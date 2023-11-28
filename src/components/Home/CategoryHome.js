import axios from 'axios'
import React, { useEffect, useState } from 'react'
import { Link } from 'react-router-dom';

const Category = () => {
    const [category, setCategory] = useState([]);
    const [brand, setBrand] = useState([]);
    useEffect(() => {
        axios.get('http://localhost:7777/api/v1/category/')
            .then(res => {
                if (res && res.data.success === true) {
                    setCategory(res.data.data)
                }
            })
    }, [])
    const dataBrand = (catId) => {
        axios.get(`http://localhost:7777/api/v1/brand/getAllByCatId/${catId}`)
            .then(res => {
                if (res && res.data.success === true) {
                    setBrand(res.data.data);
                }
            }).catch(err => console.log(err));
    }
    return (
        <div className='all-category'>
            <div className='grid-item category flex flex-col'>
                {
                    category.length > 0 && category.map((data) => (

                        <div className='grid-item category flex flex-col'>
                            <Link to={`/category/${data.id}`} style={{ textDecoration: "none", color: "black" }}
                                onMouseEnter={() => dataBrand(data.id)}>
                                <div className='category-item justify-between flex'>

                                    <div className='flex'><span class="material-icons">
                                        {
                                            data.name === "Điện thoại" ?
                                                <>smartphone</> : data.name === "Laptop" ?
                                                    <>laptop</> : data.name === "Màn hình" ?
                                                        <>desktop_windows</> : data.name === "PC" ?
                                                            <>devices</> : data.name === "Chuột" ?
                                                                <>mouse</> : data.name === "Bàn phím" ?
                                                                    <>keyboard</> : data.name === "Ghế gaming" ?
                                                                        <>chair_alt</> : data.name === "Phụ kiện" ?
                                                                            <>earbuds_battery</> : <></>
                                        }
                                    </span> <p>{data.name}</p></div>
                                    <div className='text-2xl'>{'>'}</div>
                                </div>
                            </Link>
                        </div>
                    ))
                }
                <div className='brand-list'>
                    {
                        brand.length > 0 && brand.map((data) => (
                            <Link to={`/category/${data.catId}/${data.braId}`} className='btn btn-light m-1'>{data.brand.name}</Link>
                        ))
                    }
                </div>
            </div>

        </div>
    )
}

export default Category