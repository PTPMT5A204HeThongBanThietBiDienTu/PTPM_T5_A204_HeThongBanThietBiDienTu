const { Router } = require("express")
const {
    getAllByBillId
} = require("../controllers/billproduct.controller")
const { asyncHandle } = require("../middlewares/errorHandle.middleware")

const router = Router()

router.get('/getAllByBillId/:id', asyncHandle(getAllByBillId))

module.exports = router