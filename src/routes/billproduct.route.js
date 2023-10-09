const { Router } = require("express")
const {
    getAllByBillId
} = require("../controllers/billproduct.controller")
const authenticate = require("../middlewares/auth.middleware")
const { asyncHandle } = require("../middlewares/errorHandle.middleware")

const router = Router()

router.get('/getAllByBillId/:id', authenticate, asyncHandle(getAllByBillId))

module.exports = router