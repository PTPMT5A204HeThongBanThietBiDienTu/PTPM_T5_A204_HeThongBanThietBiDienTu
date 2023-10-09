const { DataTypes, Model } = require("sequelize")
const { sequelize } = require("../config/db/mssql.db")

class BillProduct extends Model { }

BillProduct.init(
    {
        id: {
            type: DataTypes.UUID,
            primaryKey: true,
            defaultValue: DataTypes.UUIDV4
        },
        proId: {
            type: DataTypes.UUID,
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
        billId: {
            type: DataTypes.UUID,
            allowNull: false
        }
    },
    {
        timestamps: false,
        sequelize,
        modelName: 'BillProduct'
    }
)

module.exports = BillProduct