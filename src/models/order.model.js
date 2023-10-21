const { DataTypes, Model } = require("sequelize")
const { sequelize } = require("../config/db/mssql.db")

class Order extends Model { }

Order.init(
    {
        id: {
            type: DataTypes.UUID,
            primaryKey: true,
            defaultValue: DataTypes.UUIDV4
        },
        userId: {
            type: DataTypes.UUID,
            allowNull: false
        },
        createdAt: {
            type: DataTypes.DATE
        },
        status: {
            type: DataTypes.STRING,
            defaultValue: 'Đợi'
        }
    },
    {
        timestamps: false,
        sequelize,
        modelName: 'Order'
    }
)

module.exports = Order