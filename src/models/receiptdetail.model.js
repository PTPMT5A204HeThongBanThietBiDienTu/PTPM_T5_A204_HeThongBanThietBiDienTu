const { DataTypes, Model } = require("sequelize")
const { sequelize } = require("../config/db/mssql.db")

class ReceiptDetail extends Model { }

ReceiptDetail.init(
    {
        id: {
            type: DataTypes.UUID,
            primaryKey: true,
            defaultValue: DataTypes.UUIDV4
        },
        receiptId: {
            type: DataTypes.UUID,
            allowNull: false
        },
        proId: {
            type: DataTypes.UUID,
            allowNull: false
        },
        price: {
            type: DataTypes.DOUBLE,
            defaultValue: 0,
            allowNull: false
        },
        quantity: {
            type: DataTypes.INTEGER,
            allowNull: false
        }
    },
    {
        timestamps: false,
        sequelize,
        modelName: 'ReceiptDetail'
    }
)

module.exports = ReceiptDetail