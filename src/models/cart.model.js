import { DataTypes, Model } from "sequelize"
import { sequelize } from "../config/db/mysql.db"

class Cart extends Model { }

Cart.init(
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
        proId: {
            type: DataTypes.UUID,
            allowNull: false
        },
        quantity: {
            type: DataTypes.INTEGER,
            defaultValue: 1
        }
    },
    {
        sequelize,
        modelName: 'Cart'
    }
)

export default Cart