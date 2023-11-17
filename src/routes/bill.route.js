const { Router } = require("express")
const {
    getAllByUserId,
    getById,
    create
} = require("../controllers/bill.controller")
const { validateBody } = require("../middlewares/validate.middleware")
const { validateBill } = require("../utils/validations")
const authenticate = require("../middlewares/auth.middleware")
const { asyncHandle } = require("../middlewares/errorHandle.middleware")

const router = Router()

router.get('/getAllByUserId', authenticate, asyncHandle(getAllByUserId))
router.get('/:id', authenticate, asyncHandle(getById))
router.post('/create', authenticate, validateBody(validateBill), asyncHandle(create))

module.exports = router