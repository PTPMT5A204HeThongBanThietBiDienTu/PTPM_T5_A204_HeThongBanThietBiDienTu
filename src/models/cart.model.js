const { DataTypes, Model } = require("sequelize")
const { sequelize } = require("../config/db/mssql.db")

class Cart extends Model { }

Cart.init(
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
        proId: {
            type: DataTypes.UUID,
            allowNull: false
        },
        quantity: {
            type: DataTypes.INTEGER,
            defaultValue: 1
        }
    },
    {
        timestamps: false,
        sequelize,
        modelName: 'Cart'
    }
)

module.exports = Cart