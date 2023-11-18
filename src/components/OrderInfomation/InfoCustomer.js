import React from 'react'

const InforCustomer = (props) => {
    const { city, district, ward, handleCityChange, handleDistrictChange, handleWardChange, handleHousenumberChange } = props
    return (
        <>
            <div className='row my-2'>
                <div className='col-6'>
                    <select className='form-control' onChange={handleCityChange}>
                        <option value=''>-- Chọn Thành Phố --</option>
                        {
                            city.map((select_city) => (
                                <option key={select_city.id} value={`${select_city.id}-${select_city.name}`}>{select_city.name}</option>
                            ))
                        }
                    </select>
                </div>
                <div className='col-6'>
                    <select className='form-control' onChange={handleDistrictChange}>
                        <option value=''>-- Chọn Quận --</option>
                        {district.length === 0 ?
                            <></>
                            : (
                                district.map((select_district) => (
                                    <>
                                        <option value={`${select_district.id}-${select_district.name}`}>{select_district.name}</option>
                                    </>
                                ))

                            )
                        }
                    </select>
                </div>
            </div>
            <div className='row  my-2'>
                <div className='col-6'>
                    <select className='form-control' onChange={handleWardChange}>
                        <option value=''>-- Chọn Phường --</option>
                        {ward.length > 0 ?
                            ward.map((select_ward) => (
                                <>
                                    <option value={select_ward.name}>{select_ward.name}</option>
                                </>
                            ))
                            : <></>
                        }
                    </select>
                </div>
                <div className='col-6'>
                    <input type="text" placeholder='Số nhà, tên đường' name='housenumber' onChange={(e) => handleHousenumberChange(e)} className='form-control' />
                </div>
            </div>
        </>
    )
}

export default InforCustomer