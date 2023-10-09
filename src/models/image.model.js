const { DataTypes, Model } = require("sequelize")
const { sequelize } = require("../config/db/mssql.db")

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
        timestamps: false,
        sequelize,
        modelName: 'Image'
    }
)

module.exports = Image