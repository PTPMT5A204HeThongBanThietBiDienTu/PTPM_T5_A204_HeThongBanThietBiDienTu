import { sequelize } from "../config/db/mysql.db"
import Role from "./role.model"
import User from "./user.model"
import Category from "./category.model"
import Product from "./product.model"
import Image from "./image.model"
import Cart from "./cart.model"
import Bill from "./bill.model"
import Voucher from "./voucher.model"
import BillProduct from "./billproduct.model"
import BillVoucher from "./billvoucher.model"

//--------------------------------- FOREIGN KEY ON TABLE USER -------------------------------------//
//--relationship between Role and User--//
Role.hasMany(User, {
    foreignKey: 'roleId',
    as: 'user'
})

User.belongsTo(Role, {
    foreignKey: 'roleId',
    as: 'role'
})

//--------------------------------- FOREIGN KEY ON TABLE PRODUCT -------------------------------------//
//--relationship between Category and Product--//
Category.hasMany(Product, {
    foreignKey: 'catId',
    as: 'product'
})

Product.belongsTo(Category, {
    foreignKey: 'catId',
    as: 'category'
})

//--------------------------------- FOREIGN KEY ON TABLE IMAGE -------------------------------------//
//--relationship between Product and Image--//
Product.hasMany(Image, {
    foreignKey: 'proId',
    as: 'image'
})

Image.belongsTo(Product, {
    foreignKey: 'proId',
    as: 'product'
})

//--------------------------------- FOREIGN KEY ON TABLE CART -------------------------------------//
//--relationship between Product and Cart--//
Product.hasMany(Cart, {
    foreignKey: 'proId',
    as: 'cart'
})

Cart.belongsTo(Product, {
    foreignKey: 'proId',
    as: 'product'
})

//--relationship between User and Cart--//
User.hasOne(Cart, {
    foreignKey: 'userId',
    as: 'cart'
})

Cart.belongsTo(User, {
    foreignKey: 'userId',
    as: 'user'
})

//--------------------------------- FOREIGN KEY ON TABLE BILL -------------------------------------//
//--relationship between User and Bill--//
User.hasMany(Bill, {
    foreignKey: 'userId',
    as: 'bill'
})

Bill.belongsTo(User, {
    foreignKey: 'userId',
    as: 'user'
})

//--------------------------------- FOREIGN KEY ON TABLE BILLPRODUCT -------------------------------------//
//--relationship between Bill and BillProduct--//
Bill.hasMany(BillProduct, {
    foreignKey: 'billId',
    as: 'billproduct'
})

BillProduct.belongsTo(Bill, {
    foreignKey: 'billId',
    as: 'bill'
})

//--relationship between Product and BillProduct--//
Product.hasMany(BillProduct, {
    foreignKey: 'proId',
    as: 'billproduct'
})

BillProduct.belongsTo(Product, {
    foreignKey: 'proId',
    as: 'product'
})

//--------------------------------- FOREIGN KEY ON TABLE BILLVOUCHER -------------------------------------//
//--relationship between Bill and BillVoucher--//
Bill.hasMany(BillVoucher, {
    foreignKey: 'billId',
    as: 'billvoucher'
})

BillVoucher.belongsTo(Bill, {
    foreignKey: 'billId',
    as: 'bill'
})

//--relationship between Voucher and BillVoucher--//
Voucher.hasMany(BillVoucher, {
    foreignKey: 'vouId',
    as: 'billvoucher'
})

BillVoucher.belongsTo(Voucher, {
    foreignKey: 'vouId',
    as: 'voucher'
})


//====================//====================//====================//====================//====================

const createAllTable = () => {
    sequelize.sync({ force: false })
        .then(() => {
            console.log('Tables synchronized successfully')
        })
        .catch(err => {
            console.log(err);
        })
}

module.exports = {
    Role,
    User,
    Category,
    Product,
    Image,
    Cart,
    Bill,
    Voucher,
    BillProduct,
    BillVoucher,
    createAllTable
}