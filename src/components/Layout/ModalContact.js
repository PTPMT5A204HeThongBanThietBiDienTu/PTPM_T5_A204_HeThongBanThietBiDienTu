import React, { useState } from 'react'
import { Button, Modal } from 'react-bootstrap'
import '../../styles/ModalContact.scss';
import swal from 'sweetalert';
import axios from 'axios';

const ModalContact = (props) => {
    const { handleClose, show } = props;
    const [values, setValues] = useState({
        name: '',
        email: '',
        content: ''
    });
    const handleInputChange = (event) => {
        setValues(prev => ({ ...prev, [event.target.name]: event.target.value }))
    };
    const handleSubmit = (e) => {
        e.preventDefault();
        const dataMailer = {
            name: values.name,
            email: values.email,
            content: values.content
        }
        axios.post(`http://localhost:7777/api/v1/nodemailer/sendMail`, dataMailer)
            .then(res => {
                if (res && res.data.success === true) {
                    swal({
                        title: "Cảm ơn bạn đã gửi phản hồi!",
                        icon: "success",
                    }).then(() => {
                        window.location.reload();
                    });
                }
            }).catch(err => console.log(err))
    }
    return (
        <>
            <Modal show={show} onHide={handleClose} dialogClassName="modal-dialog-centered">
                <div className='title'>
                    <Modal.Header closeButton closeVariant='white'>
                        <Modal.Title><b>Liên hệ</b></Modal.Title>
                    </Modal.Header>
                </div>
                <form onSubmit={handleSubmit}>
                    <Modal.Body>
                        <div className='row'>
                            <div className='mb-3 col-6'>
                                <input type='text' name='name' placeholder='Họ tên' className='form-control' onChange={handleInputChange} required />
                            </div>
                            <div className='mb-3 col-6'>
                                <input type='text' name='email' placeholder='Email' className='form-control' onChange={handleInputChange} required />
                            </div>
                        </div>
                        <div>
                            <textarea rows={10} name='content' className='form-control' onChange={handleInputChange} required />
                        </div>
                    </Modal.Body>
                    <Modal.Footer>
                        <Button variant='secondary' onClick={handleClose}>Đóng</Button>
                        <Button variant='danger' type='submit'>Gửi</Button>
                    </Modal.Footer>
                </form>
            </Modal>
        </>
    )
}

export default ModalContact