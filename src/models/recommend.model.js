const { DataTypes, Model } = require("sequelize")
const { sequelize } = require("../config/db/mssql.db")

class Recommend extends Model { }

Recommend.init(
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
        accompany: {
            type: DataTypes.TEXT,
            allowNull: false
        }
    },
    {
        timestamps: false,
        sequelize,
        modelName: 'Recommend'
    }
)

module.exports = Recommend