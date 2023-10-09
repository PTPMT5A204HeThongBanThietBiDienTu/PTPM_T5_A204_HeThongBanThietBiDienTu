const { Sequelize } = require("sequelize")
require('dotenv').config()

const sequelize = new Sequelize(
    process.env.DB_DATABASE_SQL,
    process.env.DB_USERNAME_SQL,
    process.env.DB_PASSWORD_SQL, {
    host: process.env.DB_HOST_SQL,
    dialect: process.env.DB_DIALECT_SQL,
    dialectOptions: {
        dateStrings: true,
        typeCast: true,
        options: {
            encrypt: false
        }
    },
    logging: false,
}
)

const connectDB = () => {
    sequelize.authenticate()
        .then(() => {
            console.log('Connection has been established successfully.')
        })
        .catch(error => {
            console.error('Unable to connect to the database:', error)
        })
}

module.exports = {
    sequelize,
    connectDB
}