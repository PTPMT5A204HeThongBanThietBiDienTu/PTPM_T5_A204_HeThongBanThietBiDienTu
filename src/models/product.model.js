const { DataTypes, Model } = require("sequelize")
const { sequelize } = require("../config/db/mssql.db")

class Product extends Model { }

Product.init(
    {
        id: {
            type: DataTypes.UUID,
            primaryKey: true,
            defaultValue: DataTypes.UUIDV4
        },
        name: {
            type: DataTypes.STRING,
            allowNull: false
        },
        price: {
            type: DataTypes.DOUBLE,
            allowNull: false
        },
        quantity: {
            type: DataTypes.INTEGER,
            allowNull: false
        },
        description: {
            type: DataTypes.TEXT
        },
        img: {
            type: DataTypes.STRING,
            allowNull: false
        },
        catId: {
            type: DataTypes.UUID,
            allowNull: false
        },
        braId: {
            type: DataTypes.UUID,
            allowNull: false
        }
    },
    {
        timestamps: false,
        sequelize,
        modelName: 'Product'
    }
)

module.exports = Product