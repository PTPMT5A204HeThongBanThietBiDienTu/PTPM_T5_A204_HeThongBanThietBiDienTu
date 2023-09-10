import { DataTypes, Model } from "sequelize"
import { sequelize } from "../config/db/mysql.db"

class Voucher extends Model { }

Voucher.init(
    {
        id: {
            type: DataTypes.UUID,
            primaryKey: true,
            defaultValue: DataTypes.UUIDV4
        },
        detail: {
            type: DataTypes.TEXT
        },
        discount: {
            type: DataTypes.DOUBLE,
            defaultValue: 0
        }
    },
    {
        sequelize,
        modelName: 'Voucher'
    }
)

export default Voucher