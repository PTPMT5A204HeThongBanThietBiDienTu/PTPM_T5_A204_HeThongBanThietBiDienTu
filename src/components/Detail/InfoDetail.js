import React from 'react'
const InfoDetail = () => {
    return (
        <div className='information'>
            <div className='store'>
                <p>Có <b>1</b> cửa hàng có sản phẩm</p>
                <table class="table table-striped table-hover">
                    <tbody>
                        <tr><td>140 Lê Trọng Tấn, Tây Thạnh, Tân Phú, Thành phố Hồ Chí Minh, Việt Nam</td></tr>
                    </tbody>
                </table>
            </div>
            <div className='product-information'>
                <h5>Thông tin sản phẩm</h5>
                <p>
                    <i class="fa-solid fa-mobile m-2"></i>Mới, đầy đủ phụ kiện từ nhà sản xuất<br />
                    <i class="fa-solid fa-shield m-2"></i>Bảo hành 24 tháng chính hãng, 1 đổi 1 trong 15 ngày nếu có lỗi phần cứng từ NSX.<br />
                    <i class="fa-solid fa-box-open m-2"></i>Giá sản phẩm đã bao gồm VAT.<br />
                </p>
            </div>
        </div>
    )
}

export default InfoDetail