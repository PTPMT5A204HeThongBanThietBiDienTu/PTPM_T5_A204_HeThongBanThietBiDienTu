const { Router } = require("express")
const {
    sendMail
} = require("../controllers/nodemailler.controller")
const { asyncHandle } = require("../middlewares/errorHandle.middleware")

const router = Router()

router.post('/sendMail', asyncHandle(sendMail))

module.exports = router