const { DataTypes, Model } = require("sequelize")
const { sequelize } = require("../config/db/mssql.db")

class Role extends Model { }

Role.init(
    {
        id: {
            type: DataTypes.UUID,
            primaryKey: true,
            defaultValue: DataTypes.UUIDV4
        },
        roleName: {
            type: DataTypes.STRING,
            allowNull: false,
            unique: true
        }
    },
    {
        timestamps: false,
        sequelize,
        modelName: 'Role'
    }
)

module.exports = Role