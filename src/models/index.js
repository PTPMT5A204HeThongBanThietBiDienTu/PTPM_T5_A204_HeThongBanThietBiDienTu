const { sequelize } = require("../config/db/mssql.db")
const Role = require("./role.model")
const User = require("./user.model")
const Category = require("./category.model")
const Product = require("./product.model")
const Image = require("./image.model")
const Specification = require("./specification.model")
const Cart = require("./cart.model")
const Bill = require("./bill.model")
const BillProduct = require("./billproduct.model")
const Brand = require('./brand.model')
const Screen = require('./screen.model')
const Permission = require("./permission.model")
const Order = require("./order.model")
const OrderDetail = require("./orderdetail.model")
const Receipt = require("./receipt.model")
const ReceiptDetail = require('./receiptdetail.model')

//--------------------------------- FOREIGN KEY ON TABLE USER -------------------------------------//
//--relationship between Role and User--//
Role.hasMany(User, {
    foreignKey: 'roleId',
    as: 'user',
    onDelete: 'NO ACTION'
})

User.belongsTo(Role, {
    foreignKey: 'roleId',
    as: 'role'
})

//--------------------------------- FOREIGN KEY ON TABLE PRODUCT -------------------------------------//
//--relationship between Category and Product--//
Category.hasMany(Product, {
    foreignKey: 'catId',
    as: 'product',
    onDelete: 'NO ACTION'
})

Product.belongsTo(Category, {
    foreignKey: 'catId',
    as: 'category'
})

//--relationship between Brand and Product--//
Brand.hasMany(Product, {
    foreignKey: 'braId',
    as: 'product',
    onDelete: 'NO ACTION'
})

Product.belongsTo(Brand, {
    foreignKey: 'braId',
    as: 'brand'
})

//--------------------------------- FOREIGN KEY ON TABLE IMAGE -------------------------------------//
//--relationship between Product and Image--//
Product.hasMany(Image, {
    foreignKey: 'proId',
    as: 'image',
    onDelete: 'NO ACTION'
})

Image.belongsTo(Product, {
    foreignKey: 'proId',
    as: 'product'
})

//--------------------------------- FOREIGN KEY ON TABLE SPECIFICATION -------------------------------------//
//--relationship between Product and Specification--//
Product.hasMany(Specification, {
    foreignKey: 'proId',
    as: 'specification',
    onDelete: 'NO ACTION'
})

Specification.belongsTo(Product, {
    foreignKey: 'proId',
    as: 'product'
})

//--------------------------------- FOREIGN KEY ON TABLE CART -------------------------------------//
//--relationship between Product and Cart--//
Product.hasMany(Cart, {
    foreignKey: 'proId',
    as: 'cart',
    onDelete: 'NO ACTION'
})

Cart.belongsTo(Product, {
    foreignKey: 'proId',
    as: 'product'
})

//--relationship between User and Cart--//
User.hasOne(Cart, {
    foreignKey: 'userId',
    as: 'cart',
    onDelete: 'NO ACTION'
})

Cart.belongsTo(User, {
    foreignKey: 'userId',
    as: 'user'
})

//--------------------------------- FOREIGN KEY ON TABLE BILL -------------------------------------//
//--relationship between User and Bill--//
User.hasMany(Bill, {
    foreignKey: 'userId',
    as: 'bill',
    onDelete: 'NO ACTION'
})

Bill.belongsTo(User, {
    foreignKey: 'userId',
    as: 'user'
})

//--------------------------------- FOREIGN KEY ON TABLE BILLPRODUCT -------------------------------------//
//--relationship between Bill and BillProduct--//
Bill.hasMany(BillProduct, {
    foreignKey: 'billId',
    as: 'billproduct',
    onDelete: 'NO ACTION'
})

BillProduct.belongsTo(Bill, {
    foreignKey: 'billId',
    as: 'bill'
})

//--relationship between Product and BillProduct--//
Product.hasMany(BillProduct, {
    foreignKey: 'proId',
    as: 'billproduct',
    onDelete: 'NO ACTION'
})

BillProduct.belongsTo(Product, {
    foreignKey: 'proId',
    as: 'product'
})

//--------------------------------- FOREIGN KEY ON TABLE PERMISSION -------------------------------------//
//--relationship between Role and Permission--//
Role.hasMany(Permission, {
    foreignKey: 'roleId',
    as: 'permission',
    onDelete: 'NO ACTION'
})

Permission.belongsTo(Role, {
    foreignKey: 'roleId',
    as: 'role'
})

//--relationship between Screen and Permission--//
Screen.hasMany(Permission, {
    foreignKey: 'screenId',
    as: 'permission',
    onDelete: 'NO ACTION'
})

Permission.belongsTo(Screen, {
    foreignKey: 'screenId',
    as: 'screen'
})

//--------------------------------- FOREIGN KEY ON TABLE ORDER -------------------------------------//
//--relationship between User and Order--//
User.hasMany(Order, {
    foreignKey: 'userId',
    as: 'order',
    onDelete: 'NO ACTION'
})

Order.belongsTo(User, {
    foreignKey: 'userId',
    as: 'user'
})

//--------------------------------- FOREIGN KEY ON TABLE ORDERDETAIL -------------------------------------//
//--relationship between Order and OrderDetail--//
Order.hasMany(OrderDetail, {
    foreignKey: 'orderId',
    as: 'orderdetail',
    onDelete: 'NO ACTION'
})

OrderDetail.belongsTo(Order, {
    foreignKey: 'orderId',
    as: 'order'
})

//--relationship between Product and OrderDetail--//
Product.hasMany(OrderDetail, {
    foreignKey: 'proId',
    as: 'orderdetail',
    onDelete: 'NO ACTION'
})

OrderDetail.belongsTo(Product, {
    foreignKey: 'proId',
    as: 'product'
})

//--------------------------------- FOREIGN KEY ON TABLE RECEIPT -------------------------------------//
//--relationship between User and Receipt--//
User.hasMany(Receipt, {
    foreignKey: 'userId',
    as: 'receipt',
    onDelete: 'NO ACTION'
})

Receipt.belongsTo(User, {
    foreignKey: 'userId',
    as: 'user'
})

//--relationship between Order and Receipt--//
Order.hasMany(Receipt, {
    foreignKey: 'orderId',
    as: 'receipt',
    onDelete: 'NO ACTION'
})

Receipt.belongsTo(Order, {
    foreignKey: 'orderId',
    as: 'order'
})

//--------------------------------- FOREIGN KEY ON TABLE RECEIPTDETAIL -------------------------------------//
//--relationship between Receipt and ReceiptDetail--//
Receipt.hasMany(ReceiptDetail, {
    foreignKey: 'receiptId',
    as: 'receiptdetail',
    onDelete: 'NO ACTION'
})

ReceiptDetail.belongsTo(Receipt, {
    foreignKey: 'receiptId',
    as: 'receipt'
})

//--relationship between Product and ReceiptDetail--//
Product.hasMany(ReceiptDetail, {
    foreignKey: 'proId',
    as: 'receiptdetail',
    onDelete: 'NO ACTION'
})

ReceiptDetail.belongsTo(Product, {
    foreignKey: 'proId',
    as: 'product'
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
    Brand,
    Product,
    Image,
    Specification,
    Cart,
    Bill,
    BillProduct,
    createAllTable
}