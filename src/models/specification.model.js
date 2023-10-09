const { DataTypes, Model } = require("sequelize")
const { sequelize } = require("../config/db/mssql.db")

class Specification extends Model { }

Specification.init(
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
        attributeName: {
            type: DataTypes.STRING,
            allowNull: false
        },
        attributeValue: {
            type: DataTypes.STRING,
            allowNull: false
        }
    },
    {
        timestamps: false,
        sequelize,
        modelName: 'Specification'
    }
)

module.exports = Specification