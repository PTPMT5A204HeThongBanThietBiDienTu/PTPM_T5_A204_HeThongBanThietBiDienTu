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
import Cookies from "js-cookie";

function App() {
  axios.defaults.withCredentials = true;
  const [cartCookie, setCartCookie] = useState([])
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
  const addToCart = async (proId) => {
    const currentCartCookie = Cookies.get('CartCookie');
    const cartCookieArray = currentCartCookie ? JSON.parse(currentCartCookie) : [];

    const existingItemIndex = cartCookieArray.findIndex(item => item.proId === proId);

    try {
      axios.get(`http://localhost:7777/api/v1/product/${proId}`)
        .then(res => {
          if (res && res.data.success === true) {
            const productInfo = res.data.data;

            if (existingItemIndex !== -1) {
              const newQuantity = Number(cartCookieArray[existingItemIndex].quantity) + 1;
              const maxProductQuantity = productInfo.quantity;
              cartCookieArray[existingItemIndex].quantity = newQuantity > maxProductQuantity ? maxProductQuantity : newQuantity;
            } else {
              cartCookieArray.push({
                proId,
                quantity: 1,
                product: productInfo,
              });
            }
            setCartCookie(cartCookieArray);
            Cookies.set('CartCookie', JSON.stringify(cartCookieArray), { expires: 7 });
          }
        })
    } catch (error) {
      console.error('Error fetching product information:', error);
    }
  };
  useEffect(() => {
    const savedCartCookie = Cookies.get('CartCookie') || '[]';
    const parsedCart = JSON.parse(savedCartCookie);
    setCartCookie(parsedCart);
  }, []);
  return (
    <>
      <Navbar name={name} />
      <Routes>
        <Route path="/" element={<Home handleScrollToTop={handleScrollToTop} />} />
        <Route path="/product/:id" element={<Detail name={name} handleScrollToTop={handleScrollToTop} addToCart={addToCart} />} />
        <Route path="/login" element={<Login />} />
        <Route path="/register" element={<Register />} />
        <Route path="/cart" element={<Cart name={name} cartCookie={cartCookie} setCartCookie={setCartCookie} />} />
        <Route path="/order-information" element={<OrderInformation name={name} cartCookie={cartCookie} />} />
        <Route path="/order-complete" element={<OrderComplete name={name} />} />
        <Route path="/category/:catID" element={<Category />} />
        <Route path="/category/:catID/:braId" element={<Category />} />
        <Route path="/payment" element={<Payment name={name} setCartCookie={setCartCookie} />} />
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
