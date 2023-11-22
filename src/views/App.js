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
function App() {
  axios.defaults.withCredentials = true;
  const [name, setName] = useState('');
  useEffect(() => {
    axios.get('http://localhost:7777/api/v1/auth/getInfo')
      .then(res => {
        if (res && res.data.success === true) {
          setName(res.data.data.name);
        }
      }).catch(err => console.log(err));
  }, [])
  // useEffect(() => {
  //   const verify = () => {
  //     axios.get('http://localhost:7777/api/v1/auth/getInfo')
  //       .then(res => {
  //         // if (res && res.data.success === true) {
  //         // } else {
  //         //     return;
  //         // }
  //         console.log(res);
  //       }).catch(err => {
  //         if (err.response.data.message === 'jwt expired') {
  //           axios.post(`http://localhost:7777/api/v1/auth/refreshToken`)
  //             .then(res => {
  //               if (res && res.data.message === 'Refresh token success') {
  //                 return;
  //               }
  //             }).catch(error => console.log(error));
  //         } else {
  //           console.log(err);
  //         }
  //       });
  //   }
  //   verify();
  // }, [])

  return (
    <>
      <Navbar name={name} />
      <Routes>
        <Route path="/" element={<Home />} />
        <Route path="/product/:id" element={<Detail name={name} />} />
        <Route path="/login" element={<Login />} />
        <Route path="/register" element={<Register />} />
        <Route path="/cart" element={<Cart />} />
        <Route path="/order-information" element={<OrderInformation />} />
        <Route path="/order-complete" element={<OrderComplete />} />
        <Route path='*' element={<PageDoesNotExist />} />
      </Routes>
      <Footer />
      <ToastContainer className='toast-container'
        style={{ marginTop: "5%" }}
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
