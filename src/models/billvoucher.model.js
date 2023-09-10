import { DataTypes, Model } from "sequelize"
import { sequelize } from "../config/db/mysql.db"

class BillVoucher extends Model { }

BillVoucher.init(
    {
        id: {
            type: DataTypes.UUID,
            primaryKey: true,
            defaultValue: DataTypes.UUIDV4
        },
        vouId: {
            type: DataTypes.UUID,
            allowNull: false
        },
        discount: {
            type: DataTypes.DOUBLE,
            allowNull: false
        },
        billId: {
            type: DataTypes.UUID,
            allowNull: false
        }
    },
    {
        sequelize,
        modelName: 'BillVoucher'
    }
)

export default BillVoucher