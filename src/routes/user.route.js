const { Router } = require("express")
const {
    getAll,
    create
} = require("../controllers/user.controller")
const { validateBody } = require("../middlewares/validate.middleware")
const { validateUser } = require("../utils/validations")

const router = Router()

router.get('/', getAll)
router.post('/create', validateBody(validateUser), create)

module.exports = router