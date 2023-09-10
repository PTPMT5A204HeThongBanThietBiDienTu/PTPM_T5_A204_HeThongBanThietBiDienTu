import { DataTypes, Model } from "sequelize"
import { sequelize } from "../config/db/mysql.db"

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
        sumPrice: {
            type: DataTypes.DOUBLE,
            defaultValue: 0
        },
        sumDiscount: {
            type: DataTypes.DOUBLE,
            defaultValue: 0
        },
        total: {
            type: DataTypes.DOUBLE,
            defaultValue: 0
        },
        status: {
            type: DataTypes.INTEGER,
            defaultValue: 0
        }
    },
    {
        sequelize,
        modelName: 'Bill'
    }
)

export default Bill