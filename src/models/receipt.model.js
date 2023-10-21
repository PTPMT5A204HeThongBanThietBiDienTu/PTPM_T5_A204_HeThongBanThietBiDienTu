const { DataTypes, Model } = require("sequelize")
const { sequelize } = require("../config/db/mssql.db")

class Receipt extends Model { }

Receipt.init(
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
        orderId: {
            type: DataTypes.UUID,
            allowNull: false
        },
        total: {
            type: DataTypes.DOUBLE,
            defaultValue: 0
        },
        createdAt: {
            type: DataTypes.DATE
        }
    },
    {
        timestamps: false,
        sequelize,
        modelName: 'Receipt'
    }
)

module.exports = Receipt