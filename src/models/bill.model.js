const { DataTypes, Model } = require("sequelize")
const { sequelize } = require("../config/db/mssql.db")
const BillStatus = require("./BillStatus")

class Bill extends Model { }

Bill.init(
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
        total: {
            type: DataTypes.DOUBLE,
            defaultValue: 0
        },
        status: {
            type: DataTypes.ENUM,
            values: Object.values(BillStatus),
            defaultValue: BillStatus.UNPAID
        },
        createdAt: {
            type: DataTypes.DATE
        }
    },
    {
        timestamps: false,
        sequelize,
        modelName: 'Bill'
    }
)

module.exports = Bill