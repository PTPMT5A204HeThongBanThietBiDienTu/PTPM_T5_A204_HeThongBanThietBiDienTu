import { DataTypes, Model } from "sequelize"
import { sequelize } from "../config/db/mysql.db"

class User extends Model { }

User.init(
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
        email: {
            type: DataTypes.STRING,
            allowNull: false,
            unique: true
        },
        address: {
            type: DataTypes.TEXT
        },
        phone: {
            type: DataTypes.STRING
        },
        password: {
            type: DataTypes.STRING,
            allowNull: false
        },
        roleId: {
            type: DataTypes.UUID,
            allowNull: false
        }
    },
    {
        sequelize,
        modelName: 'User'
    }
)

export default User