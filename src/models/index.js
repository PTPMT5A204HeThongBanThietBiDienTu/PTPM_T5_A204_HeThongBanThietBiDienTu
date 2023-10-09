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