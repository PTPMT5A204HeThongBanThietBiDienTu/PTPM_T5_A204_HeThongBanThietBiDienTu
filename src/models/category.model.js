import { DataTypes, Model } from "sequelize"
import { sequelize } from "../config/db/mysql.db"

class Category extends Model { }

Category.init(
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
        modelName: 'Category'
    }
)

export default Category