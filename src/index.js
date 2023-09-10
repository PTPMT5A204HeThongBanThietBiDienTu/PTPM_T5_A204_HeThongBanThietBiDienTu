import express from "express"
require('dotenv').config()
import connectDB from "./config/db/mysql.db"
import { createAllTable } from "./models/index"
import useRouter from "./routes/index"
import { errorHandle } from "./middlewares/errorHandle.middleware"
import useMiddleware from "./middlewares"

const port = process.env.PORT || 2002
const app = express()

async function runApp() {
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