const express = require("express")
require('dotenv').config()
const { connectDB } = require("./config/db/mssql.db")
const { createAllTable } = require("./models/index")
const useRouter = require("./routes/index")
const { errorHandle } = require("./middlewares/errorHandle.middleware")
const useMiddleware = require("./middlewares")

const port = process.env.PORT || 2002
const app = express()

async function runApp() {
    app.use(express.static('public'))
    await connectDB()
    // await createAllTable()
    await useMiddleware(app)
    await useRouter(app)


    app.listen(port, () => {
        console.log(`Server is running at http://localhost:${port}`)
    })

    app.use(errorHandle)
}

runApp()