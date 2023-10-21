const { DataTypes, Model } = require("sequelize")
const { sequelize } = require("../config/db/mssql.db")

class OrderDetail extends Model { }

OrderDetail.init(
    {
        id: {
            type: DataTypes.UUID,
            primaryKey: true,
            defaultValue: DataTypes.UUIDV4
        },
        orderId: {
            type: DataTypes.UUID,
            allowNull: false
        },
        proId: {
            type: DataTypes.UUID,
            allowNull: false
        },
        quantity: {
            type: DataTypes.INTEGER,
            allowNull: false,
            defaultValue: 1
        }
    },
    {
        timestamps: false,
        sequelize,
        modelName: 'OrderDetail'
    }
)

module.exports = OrderDetail