import { Sequelize } from "sequelize"
require('dotenv').config()

export const sequelize = new Sequelize(
    process.env.DB_DATABASE,
    process.env.DB_USERNAME,
    process.env.DB_PASSWORD, {
    host: process.env.DB_HOST,
    dialect: process.env.DB_DIALECT,
    timezone: process.env.DB_TIMEZONE,
    dialectOptions: {
        dateStrings: true,
        typeCast: true
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

export default connectDB