import { DataTypes, Model } from "sequelize"
import { sequelize } from "../config/db/mysql.db"

class BillProduct extends Model { }

BillProduct.init(
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
        price: {
            type: DataTypes.DOUBLE,
            allowNull: false
        },
        quantity: {
            type: DataTypes.INTEGER,
            allowNull: false
        },
        billId: {
            type: DataTypes.UUID,
            allowNull: false
        }
    },
    {
        sequelize,
        modelName: 'BillProduct'
    }
)

export default BillProduct