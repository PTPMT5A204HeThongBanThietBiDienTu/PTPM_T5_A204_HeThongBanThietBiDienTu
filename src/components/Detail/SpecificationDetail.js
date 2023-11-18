import axios from 'axios'
import React, { useEffect, useState } from 'react'

const SpecificationDetail = ({ id }) => {
    const [specification, setSpecification] = useState([]);
    useEffect(() => {
        axios.get(`http://localhost:1234/api/v1/specification/getByProId/${id}`)
            .then(res => {
                if (res && res.data) {
                    setSpecification(res.data.data);
                }
            }).catch(err => console.log(err));
    }, [id])
    return (
        <div className='specifications my-2'>
            <h3>
                Thông số kĩ thuật
            </h3>
            <table className='table table-striped'>
                <tbody>
                    {
                        specification.length > 0 && specification.map((value) => (
                            <tr>

                                <>
                                    <th>{value.attributeName}</th>
                                    <td>{value.attributeValue}</td>
                                </>
                            </tr>
                        ))
                    }
                </tbody>
            </table>
        </div>
    )
}

export default SpecificationDetail