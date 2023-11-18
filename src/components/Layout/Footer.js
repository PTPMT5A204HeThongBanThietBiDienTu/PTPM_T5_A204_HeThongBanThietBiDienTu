import React from 'react'
import '../../styles/Footer.scss';
import { Link } from 'react-router-dom';
const Footer = () => {
    return (
        <>
            <div className='footer'>
                <div className='top'>
                    <Link href='/' style={{ textDecoration: "none" }}><p>TS Mobile</p></Link>
                </div>
                <div className='bottom'>
                    <div>
                        <h4>Địa chỉ</h4>
                        <p>140 Lê Trọng Tấn, Tây Thạnh, Tân Phú, Thành phố Hồ Chí Minh, Việt Nam</p>
                        {/* <p>Store 2: 164 Le Quoc Hung Street, Ward 13, District 4, HCM City</p>
                        <p>Store 3: 10/24 Nguyen Dinh Chieu Street, Da Kao Ward, Binh Thanh District, HCM City</p>
                        <p>Store 4: S5/6 Cu Xa Phu Lam A Street, Ward 12, District 6, HCM City</p>
                        <p>Store 5: 637/36/5 Tinh Lo 10 Street, Binh Tri Dong B Ward, Binh Tan District, HCM City</p> */}
                        <p>Hotline: 0587 928 264 (Mr.Tuan)</p>
                        <span className='social'>
                            <a href='/'><i class="fa-brands fa-facebook"></i></a>
                            <a href='/'><i class="fa-brands fa-instagram"></i></a>
                            <a href='/'><i class="fa-brands fa-youtube"></i></a>
                        </span>
                    </div>
                    <div>
                        <h4>MENU</h4>
                        <a href='/'>Trang chủ</a>
                        <a href='/'>Tài khoản</a>
                        <a href='/'>Giỏ hàng</a>
                    </div>
                    <div className='address'>
                        <h4>Address</h4>
                        <iframe src="https://www.google.com/maps/embed?pb=!1m18!1m12!1m3!1d3919.067226855183!2d106.62608107587653!3d10.80616315864282!2m3!1f0!2f0!3f0!3m2!1i1024!2i768!4f13.1!3m3!1m2!1s0x31752be27d8b4f4d%3A0x92dcba2950430867!2zVHLGsOG7nW5nIMSQ4bqhaSBo4buNYyBDw7RuZyBUaMawxqFuZyBUUC4gSOG7kyBDaMOtIE1pbmg!5e0!3m2!1svi!2s!4v1699713353589!5m2!1svi!2s"
                            width="600"
                            height="450"
                            style={{ border: "0" }}
                            allowfullscreen=""
                            loading="lazy"
                            title="Google Map"
                        ></iframe>
                    </div>
                </div>
            </div>
        </>
    )
}

export default Footer