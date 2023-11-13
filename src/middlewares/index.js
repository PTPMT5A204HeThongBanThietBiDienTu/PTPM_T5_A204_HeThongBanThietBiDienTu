const express = require("express")
const session = require("express-session")
const cors = require("cors")
require("dotenv").config()
const sessionStore = require('../config/mysqlstore.db')

const useMiddleware = (app) => {
    app.use(cors({
        origin: 'http://localhost:3000',
        methods: ['GET', 'POST', 'PUT', 'PATCH', 'DELETE'],
        credentials: true
    }))
    app.use(express.json())
    app.use(express.urlencoded({ extended: true }))
    app.use(session({
        name: 'CellphoneS_API',
        secret: process.env.SESSION_SECRET,
        store: sessionStore,
        resave: false,
        saveUninitialized: false,
        cookie: {
            httpOnly: true,
            maxAge: 3600 * 24 * 7 * 1000
        }
    }))
}

module.exports = useMiddleware