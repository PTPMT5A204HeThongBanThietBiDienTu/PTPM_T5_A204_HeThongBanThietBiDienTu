const { DataTypes, Model } = require("sequelize")
const { sequelize } = require("../config/db/mssql.db")

class Category_Brand extends Model { }

Category_Brand.init(
    {
        catId: {
            type: DataTypes.UUID,
            primaryKey: true
        },
        braId: {
            type: DataTypes.UUID,
            primaryKey: true
        }
    },
    {
        timestamps: false,
        sequelize,
        modelName: 'Category_Brand'
    }
)

module.exports = Category_Brand