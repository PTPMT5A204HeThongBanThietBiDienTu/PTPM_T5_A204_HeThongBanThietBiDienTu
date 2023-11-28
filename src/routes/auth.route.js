const { Router } = require("express")
const {
    login,
    logout,
    refreshToken,
    register,
    getInfo,
    updateInfo,
    changePass
} = require("../controllers/auth.controller")
const { validateBody } = require("../middlewares/validate.middleware")
const {
    validateRegister,
    validateLogin,
    validateUpdateInfo,
    validateChangePass
} = require("../utils/validations")
const { asyncHandle } = require("../middlewares/errorHandle.middleware")
const authenticate = require("../middlewares/auth.middleware")

const router = Router()

router.post('/register', validateBody(validateRegister), asyncHandle(register))
router.post('/login', validateBody(validateLogin), asyncHandle(login))
router.post('/logout', authenticate, asyncHandle(logout))
router.post('/refreshToken', asyncHandle(refreshToken))
router.get('/getInfo', authenticate, asyncHandle(getInfo))
router.patch('/updateInfo', authenticate, validateBody(validateUpdateInfo), asyncHandle(updateInfo))
router.post('/changePass', authenticate, validateBody(validateChangePass), asyncHandle(changePass))

module.exports = router