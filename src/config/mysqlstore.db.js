const mysql = require("mysql2")
const session = require("express-session")
const MySQLStore = require("express-mysql-session")(session)
require("dotenv").config()

const options = {
    host: process.env.DB_HOST,
    port: process.env.DB_PORT,
    user: process.env.DB_USERNAME,
    password: process.env.DB_PASSWORD,
    database: process.env.DB_DATABASE
}

const connection = mysql.createConnection(options)
const sessionStore = new MySQLStore({}, connection)

module.exports = sessionStore