import { DataTypes, Model } from "sequelize"
import { sequelize } from "../config/db/mysql.db"

class Image extends Model { }

Image.init(
    {
        id: {
            type: DataTypes.UUID,
            primaryKey: true,
            defaultValue: DataTypes.UUIDV4
        },
        imgSrc: {
            type: DataTypes.STRING,
            allowNull: false
        },
        proId: {
            type: DataTypes.UUID,
            allowNull: false
        }
    },
    {
        sequelize,
        modelName: 'Image'
    }
)

export default Image