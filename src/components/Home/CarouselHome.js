import React from 'react'
import Carousel from "react-multi-carousel";
import carousel1 from '../../assets/images/carousel-1.jpg'
import carousel2 from '../../assets/images/carousel-2.jpg'
import carousel3 from '../../assets/images/carousel-3.jpg'
import carousel4 from '../../assets/images/carousel-4.jpg'
import carousel5 from '../../assets/images/carousel-5.jpg'
const CarouselHome = () => {
    const poster = [carousel1, carousel2, carousel3, carousel4, carousel5];
    const Responsive = {
        superLargeDesktop: {
            breakpoint: { max: 4000, min: 3000 },
            items: 1
        },
        desktop: {
            breakpoint: { max: 3000, min: 1200 },
            items: 1
        },
        tablet: {
            breakpoint: { max: 1200, min: 1000 },
            items: 1
        },
        mobile: {
            breakpoint: { max: 1000, min: 0 },
            items: 1
        },
        mini_mobile: {
            breakpoint: { max: 700, min: 0 },
            items: 1
        }
    };
    return (
        <div className='grid-item carousel'>
            <Carousel
                additionalTransfrom={0}
                arrows
                autoPlay
                autoPlaySpeed={3000}
                centerMode={false}
                className=""
                containerClass="container-with-dots"
                dotListClass=""
                draggable
                focusOnSelect={false}
                infinite
                itemClass=""
                keyBoardControl
                minimumTouchDrag={80}
                renderButtonGroupOutside={false}
                renderDotsOutside
                showDots={true}
                ssr={true}
                responsive={Responsive}>
                {
                    poster.map((data) => (
                        <img src={data} alt='' />
                    ))
                }
            </Carousel>
        </div>
    )
}

export default CarouselHome