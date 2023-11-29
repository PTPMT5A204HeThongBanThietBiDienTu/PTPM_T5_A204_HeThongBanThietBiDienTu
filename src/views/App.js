import { Routes, Route } from "react-router-dom";
import 'react-toastify/dist/ReactToastify.css';
import 'bootstrap/dist/css/bootstrap.min.css';
import 'bootstrap-icons/font/bootstrap-icons.css';
import "react-multi-carousel/lib/styles.css";
import "react-responsive-carousel/lib/styles/carousel.min.css";
import { ToastContainer } from 'react-toastify';
import 'react-toastify/dist/ReactToastify.css';
import Navbar from "../components/Layout/Navbar";
import Footer from "../components/Layout/Footer";
import Home from "../routes/Home";
import Detail from "../routes/Detail";
import Login from "../routes/Login";
import Register from "../routes/Register";
import axios from "axios";
import { useEffect, useState } from "react";
import PageDoesNotExist from '../routes/Page_Does_Not_Exist/PageDoseNotExist';
import Cart from "../routes/Cart";
import OrderInformation from "../routes/OrderInformation";
import OrderComplete from "../routes/OrderComplete";
import Category from "../routes/Category";
import Payment from "../routes/Payment";
import Search from "../routes/Search";
import OrderHistory from "../routes/OrderHistory";
import OrderDetail from "../routes/OrderDetail";
import Profile from "../routes/Profile";
import ChangePassword from "../routes/ChangePassword";

function App() {
  axios.defaults.withCredentials = true;
  const [isAtTop, setIsAtTop] = useState(true);
  const [name, setName] = useState('');
  const handleScrollToTop = () => {
    window.scrollTo({
      top: 0,
      behavior: 'smooth',
    });
  };
  useEffect(() => {
    const handleScroll = () => {
      if (window.pageYOffset > 300) {
        setIsAtTop(false);
      } else {
        setIsAtTop(true);
      }
    };

    window.addEventListener('scroll', handleScroll);
    return () => {
      window.removeEventListener('scroll', handleScroll);
    };
  }, []);
  useEffect(() => {
    axios.get('http://localhost:7777/api/v1/auth/getInfo')
      .then(res => {
        if (res && res.data.success === true) {
          setName(res.data.data.name);
        }
      }).catch(err => {
        if (err.response.data.message === 'jwt expired') {
          axios.post(`http://localhost:7777/api/v1/auth/refreshToken`)
            .then(res => {
              if (res && res.data.message === 'Refresh token success') {
                return;
              }
            }).catch(error => console.log(error));
        } else {
          console.log(err);
        }
      });
  }, [])

  return (
    <>
      <Navbar name={name} />
      <Routes>
        <Route path="/" element={<Home handleScrollToTop={handleScrollToTop} />} />
        <Route path="/product/:id" element={<Detail name={name} handleScrollToTop={handleScrollToTop} />} />
        <Route path="/login" element={<Login />} />
        <Route path="/register" element={<Register />} />
        <Route path="/cart" element={<Cart />} />
        <Route path="/order-information" element={<OrderInformation />} />
        <Route path="/order-complete" element={<OrderComplete />} />
        <Route path="/category/:catID" element={<Category />} />
        <Route path="/category/:catID/:braId" element={<Category />} />
        <Route path="/payment" element={<Payment />} />
        <Route path="/search" element={<Search />} />
        <Route path="/order-history" element={<OrderHistory name={name} />} />
        <Route path="/order-detail" element={<OrderDetail name={name} />} />
        <Route path="/profile" element={<Profile name={name} />} />
        <Route path="/change-password" element={<ChangePassword name={name} />} />
        <Route path='*' element={<PageDoesNotExist />} />
      </Routes>
      <div className={`scroll-to-top ${isAtTop ? 'hidden' : 'visible'}`} onClick={handleScrollToTop}>
        <button
          className={`scroll-to-top-button`}

        >
          <i className="fa-solid fa-arrow-up"></i>
        </button>
      </div>
      <Footer />
      <ToastContainer className='toast-container'
        style={{ marginTop: "5%", position: "fixed" }}
        position="top-center"
        autoClose={2000}
        hideProgressBar={false}
        newestOnTop={false}
        closeOnClick
        rtl={false}
        pauseOnFocusLoss
        draggable
        pauseOnHover
        theme="light"
      />
    </>
  );
}

export default App;
