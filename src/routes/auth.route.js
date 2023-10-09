const { Router } = require("express")
const {
    login,
    logout,
    refreshToken,
    register,
    getInfo
} = require("../controllers/auth.controller")
const { validateBody } = require("../middlewares/validate.middleware")
const {
    validateRegister,
    validateLogin
} = require("../utils/validations")
const { asyncHandle } = require("../middlewares/errorHandle.middleware")
const authenticate = require("../middlewares/auth.middleware")

const router = Router()

router.post('/register', validateBody(validateRegister), asyncHandle(register))
router.post('/login', validateBody(validateLogin), asyncHandle(login))
router.post('/logout', authenticate, asyncHandle(logout))
router.post('/refreshToken', asyncHandle(refreshToken))
router.get('/getInfo', authenticate, asyncHandle(getInfo))

module.exports = router