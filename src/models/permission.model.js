const { DataTypes, Model } = require("sequelize")
const { sequelize } = require("../config/db/mssql.db")

class Permission extends Model { }

Permission.init(
    {
        id: {
            type: DataTypes.UUID,
            primaryKey: true,
            defaultValue: DataTypes.UUIDV4
        },
        roleId: {
            type: DataTypes.UUID,
            allowNull: false
        },
        screenId: {
            type: DataTypes.UUID,
            allowNull: false
        },
        is_Permission: {
            type: DataTypes.BOOLEAN,
            defaultValue: true
        }
    },
    {
        timestamps: false,
        sequelize,
        modelName: 'Permission'
    }
)

module.exports = Permission