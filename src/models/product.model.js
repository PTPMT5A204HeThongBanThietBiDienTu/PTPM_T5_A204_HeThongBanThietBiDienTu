import { DataTypes, Model } from "sequelize"
import { sequelize } from "../config/db/mysql.db"

class Product extends Model { }

Product.init(
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
        price: {
            type: DataTypes.DOUBLE,
            allowNull: false,
            defaultValue: 0
        },
        description: {
            type: DataTypes.TEXT
        },
        catId: {
            type: DataTypes.UUID,
            allowNull: false
        }
    },
    {
        sequelize,
        modelName: 'Product'
    }
)

export default Product