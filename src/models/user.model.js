const { DataTypes, Model } = require("sequelize")
const { sequelize } = require("../config/db/mssql.db")
const bcrypt = require("bcrypt")

class User extends Model { }

User.init(
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
        email: {
            type: DataTypes.STRING,
            allowNull: false,
            unique: true
        },
        address: {
            type: DataTypes.TEXT
        },
        phone: {
            type: DataTypes.STRING
        },
        password: {
            type: DataTypes.STRING,
            allowNull: false
        },
        roleId: {
            type: DataTypes.UUID,
            allowNull: false
        },
        is_Active: {
            type: DataTypes.BOOLEAN,
            defaultValue: true,
            allowNull: false
        }
    },
    {
        timestamps: false,
        sequelize,
        modelName: 'User',
        hooks: {
            beforeCreate(user) {
                const salt = bcrypt.genSaltSync()
                user.password = bcrypt.hashSync(user.password, salt)
            },
            beforeUpdate(user) {
                if (user.changed('password')) {
                    const salt = bcrypt.genSaltSync()
                    user.password = bcrypt.hashSync(user.password, salt)
                }
            }
        }
    }
)

module.exports = User