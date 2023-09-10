import { DataTypes, Model } from "sequelize"
import { sequelize } from "../config/db/mysql.db"

class Role extends Model { }

Role.init(
    {
        id: {
            type: DataTypes.UUID,
            primaryKey: true,
            defaultValue: DataTypes.UUIDV4
        },
        name: {
            type: DataTypes.STRING,
            allowNull: false,
            unique: true
        }
    },
    {
        sequelize,
        modelName: 'Role'
    }
)

export default Role