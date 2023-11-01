const { DataTypes, Model } = require("sequelize")
const { sequelize } = require("../config/db/mssql.db")

class Customer extends Model { }

Customer.init(
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
        phone: {
            type: DataTypes.STRING,
            allowNull: false
        },
        address: {
            type: DataTypes.TEXT
        }
    },
    {
        timestamps: false,
        sequelize,
        modelName: 'Customer'
    }
)

module.exports = Customer