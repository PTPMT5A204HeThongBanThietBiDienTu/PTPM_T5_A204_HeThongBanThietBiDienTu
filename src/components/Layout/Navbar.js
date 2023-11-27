import React, { useEffect, useState } from 'react'
import Banner1 from '../../assets/images/banner-1.png'
import Banner2 from '../../assets/images/banner-2.png'
import Banner3 from '../../assets/images/banner-3.png'
import { MenuItems } from './MenuItems.js';
import '../../styles/Navbar.scss';
import { Link } from 'react-router-dom';
import axios from 'axios';
import swal from 'sweetalert';
import OutsideClickHandler from 'react-outside-click-handler';
import ModalContact from './ModalContact.js';

const Navbar = ({ name }) => {
    // const navigate = useNavigate();
    const [search, setSearch] = useState('');
    const [resultSearch, setResultSearch] = useState([]);
    const [showModalContact, setShowModalContact] = useState(false);
    const handleClose = () => {
        setShowModalContact(false);
    }
    const handleFilterSearch = (value) => {
        setSearch(value);
    };
    const closeSearch = () => {
        setSearch('');
    }
    useEffect(() => {
        if (search !== '') {
            const handleSearch = () => {
                const valueSearch = {
                    content: search
                }
                axios.post(`http://localhost:7777/api/v1/product/search`, valueSearch)
                    .then(res => {
                        if (res && res.data.success === true) {
                            setResultSearch(res.data.data.slice(0, 4));
                        } else {
                            setResultSearch([]);
                        }
                    }).catch(err => console.log(err));
            }
            handleSearch();
        }
    }, [search]);
    const handleLogout = () => {
        axios.post('http://localhost:7777/api/v1/auth/logout')
            .then(res => {
                if (res && res.data.success === true && res.data.message === 'Logout success') {
                    swal({
                        title: "Đăng xuất thành công!",
                        icon: "success",
                    }).then(() => {
                        window.location.reload();
                    });
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
                    <OutsideClickHandler onOutsideClick={() => closeSearch()} >
                        <div className='all-search d-flex flex-column'>
                            <div className='search-form w-100 d-flex'>
                                <input type='text' placeholder='Search...' value={search} onChange={(e) => handleFilterSearch(e.target.value)} />
                                <button type='submit'><i class="fa-solid fa-magnifying-glass"></i></button>
                            </div>
                        </div>
                        {
                            search !== '' ?
                                <>
                                    <div className='search-result'>
                                        <h5>Sản phẩm gợi ý</h5>
                                        {
                                            resultSearch.map((data) => (
                                                <Link style={{ textDecoration: "none", color: "#222" }} to={`/product/${data.id}`} onClick={() => closeSearch()}>
                                                    <div key={data.id} className='search-item d-flex'>
                                                        <div className='image'>
                                                            <img src={`http://localhost:7777/${data.img}`} alt='' />
                                                        </div>
                                                        <div className='name'>
                                                            {data.name}
                                                        </div>
                                                    </div>
                                                </Link>
                                            ))
                                        }
                                    </div>
                                </> : <></>
                        }
                    </OutsideClickHandler>
                    <ul className='nav-menu'>
                        {MenuItems.map((item, index) => {
                            if (item.title !== "Liên hệ") {
                                return (
                                    <li key={index}>
                                        <Link className={item.cName} to={item.url}>
                                            <i className={item.icon}></i>
                                            {item.title}
                                        </Link>
                                    </li>
                                );
                            } else {
                                return (
                                    <li key={index} onClick={() => setShowModalContact(true)}>
                                        <div className={item.cName}>
                                            <i className={item.icon}></i>
                                            {item.title}
                                        </div>
                                    </li>
                                );
                            }
                        })}
                        {name !== '' ? (
                            <>
                                <li>
                                    <Link className='nav-links' to={'#'}>
                                        <i className='fa-solid fa-user'></i>
                                        {name}
                                    </Link>
                                </li>
                                <li>
                                    <div className='nav-links' onClick={handleLogout}>
                                        <i className="fa-solid fa-right-from-bracket"></i>
                                        Đăng xuất
                                    </div>
                                </li>
                            </>
                        ) : (
                            <li>
                                <Link className='nav-links' to={'/login'}>
                                    <i className="fa-solid fa-right-to-bracket"></i>
                                    Đăng nhập
                                </Link>
                            </li>
                        )}
                    </ul>
                </nav>

            </div>
            <ModalContact show={showModalContact} handleClose={handleClose} />
        </div >
    )
}

export default Navbar