import React from 'react'
import Banner1 from '../../assets/images/banner-1.png'
import Banner2 from '../../assets/images/banner-2.png'
import Banner3 from '../../assets/images/banner-3.png'
import { MenuItems } from './MenuItems.js';
import '../../styles/Navbar.scss';
import { Link } from 'react-router-dom';
import axios from 'axios';

const Navbar = ({ name }) => {
    // const navigate = useNavigate();
    const handleLogout = () => {
        axios.post('http://localhost:1234/api/v1/auth/logout')
            .then(res => {
                if (res && res.data.success === true && res.data.message === 'Logout success') {
                    window.location.reload();
                }
            }).catch(err => console.log(err));
    }
    return (
        <div className='all-nav flex flex-col'>
            <div className='top'>
                <div><img src={Banner1} alt='banner' /></div>
                <div><img src={Banner2} alt='banner' /></div>
                <div><img src={Banner3} alt='banner' /></div>
            </div>
            <div className='bottom'>
                <nav className='NavbarItems'>
                    <Link to={'/'} className='decoration-transparent'><h3 className='navbar-logo'>TS Mobile</h3></Link>
                    <div className='all-search d-flex flex-column'>
                        <div className='search-form w-100 d-flex'>
                            <input type='text' placeholder='Search...' />
                            <button type='submit'><i class="fa-solid fa-magnifying-glass"></i></button>
                        </div>
                    </div>
                    <ul className='nav-menu'>
                        {MenuItems.map((item, index) => (
                            <li key={index}>
                                <Link className={item.cName} to={item.url}>
                                    <i className={item.icon}></i>
                                    {item.title}
                                </Link>
                            </li>
                        ))}
                        {
                            name !== '' ?
                                <>
                                    <li>
                                        <Link className='nav-links' to={'#'}>
                                            <i className='fa-solid fa-user'></i>
                                            {name}
                                        </Link>
                                    </li>
                                    <li>
                                        <div className='nav-links' onClick={handleLogout}>
                                            <i class="fa-solid fa-right-from-bracket"></i>
                                            Đăng xuất
                                        </div>
                                    </li>
                                </> :
                                <li>
                                    <Link className='nav-links' to={'/login'}>
                                        <i class="fa-solid fa-right-to-bracket"></i>
                                        Đăng nhập
                                    </Link>
                                </li>
                        }
                    </ul>
                </nav>

            </div>
        </div >
    )
}

export default Navbar